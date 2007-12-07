﻿//#define PRODUCE_UNSAFE_MATCHERS // todo: what for ?
#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define NO_EDGE_LOOKUP
//#define NO_ADJUST_LIST_HEADS
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define DUMP_SEARCHPROGRAMS
#define OLDMAPPEDFIELDS
//#define OPCOST_WITH_GEO_MEAN
//#define VSTRUCT_VAL_FOR_EDGE_LOOKUP

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.lgsp;
using System.IO;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;


namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class generating matcher programs out of rules.
    /// </summary>
    public class LGSPMatcherGenerator
    {
        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        private IGraphModel model;

        /// <summary>
        /// If true, the generated matcher functions are commented to improve understanding the source code.
        /// </summary>
        public bool CommentSourceCode = false;

        /// <summary>
        /// If true, the source code of dynamically generated matcher functions are dumped to a file in the current directory.
        /// </summary>
        public bool DumpDynSourceCode = false;

        /// <summary>
        /// If true, generated search plans are dumped in VCG and TXT files in the current directory.
        /// </summary>
        public bool DumpSearchPlan = false;

        /// <summary>
        /// Instantiates a new instance of LGSPMatcherGenerator with the given graph model.
        /// </summary>
        /// <param name="model">The model for which the matcher functions shall be generated.</param>
        public LGSPMatcherGenerator(IGraphModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Builds a plan graph out of a given pattern graph.
        /// </summary>
        /// <param name="graph">The host graph to optimize the matcher program for, 
        /// providing statistical information about its structure </param>
        public PlanGraph GeneratePlanGraph(LGSPGraph graph, PatternGraph patternGraph, bool negativePatternGraph)
        {
            PlanNode[] nodes = new PlanNode[patternGraph.Nodes.Length + patternGraph.Edges.Length];
            // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)
            List<PlanEdge> edges = new List<PlanEdge>(patternGraph.Nodes.Length + 5 * patternGraph.Edges.Length);

            int nodesIndex = 0;

            PlanNode root = new PlanNode("root");

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.Nodes.Length; i++)
            {
                PatternNode node = patternGraph.nodes[i];
                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && node.PatternElementType == PatternElementType.Normal)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
#if OPCOST_WITH_GEO_MEAN
                    cost = graph.nodeLookupCosts[node.TypeID];
#else
                    cost = graph.nodeCounts[node.TypeID];
#endif
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                node.TempPlanMapping = nodes[nodesIndex];
                nodesIndex++;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.Edges.Length; i++)
            {
                PatternEdge edge = patternGraph.edges[i];

                bool isPreset;
#if !NO_EDGE_LOOKUP
                float cost;
                SearchOperationType searchOperationType;
                if(edge.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
#if VSTRUCT_VAL_FOR_EDGE_LOOKUP
                    int sourceTypeID;
                    if(edge.source != null) sourceTypeID = edge.source.TypeID;
                    else sourceTypeID = model.NodeModel.RootType.TypeID;
                    int targetTypeID;
                    if(edge.target != null) targetTypeID = edge.target.TypeID;
                    else targetTypeID = model.NodeModel.RootType.TypeID;
  #if MONO_MULTIDIMARRAY_WORKAROUND
                    cost = graph.vstructs[((sourceTypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                        + targetTypeID) * 2 + (int) LGSPDir.Out];
  #else
                    cost = graph.vstructs[sourceTypeID, edge.TypeID, targetTypeID, (int) LGSPDir.Out];
  #endif
#elif OPCOST_WITH_GEO_MEAN 
                    cost = graph.edgeLookupCosts[edge.TypeID];
#else
                    cost = graph.edgeCounts[edge.TypeID];
#endif

                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
#else
                SearchOperationType searchOperationType = SearchOperationType.Lookup;       // lookup as dummy
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else isPreset = false;
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
                if(isPreset)
                {
                    PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], 0);
                    edges.Add(rootToNodeEdge);
                    nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                }
#endif
                // only add implicit source operation if edge source is needed and the edge source is not a preset node
                if(edge.source != null && !edge.source.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 0);
#else
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 1);
#endif
                    edges.Add(implSrcEdge);
                    edge.source.TempPlanMapping.IncomingEdges.Add(implSrcEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(edge.target != null && !edge.target.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 0);
#else
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 1);
#endif
                    edges.Add(implTgtEdge);
                    edge.target.TempPlanMapping.IncomingEdges.Add(implTgtEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset
                if(!isPreset)
                {
                    // no outgoing if no source
                    if(edge.source != null)
                    {
                        int targetTypeID;
                        if(edge.target != null) targetTypeID = edge.target.TypeID;
                        else targetTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float normCost = graph.vstructs[((edge.source.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + targetTypeID) * 2 + (int) LGSPDir.Out];
#else
                        float normCost = graph.vstructs[edge.source.TypeID, edge.TypeID, targetTypeID, (int) LGSPDir.Out];
#endif
                        if(graph.nodeCounts[edge.source.TypeID] != 0)
                            normCost /= graph.nodeCounts[edge.source.TypeID];
                        PlanEdge outEdge = new PlanEdge(SearchOperationType.Outgoing, edge.source.TempPlanMapping, nodes[nodesIndex], normCost);
                        edges.Add(outEdge);
                        nodes[nodesIndex].IncomingEdges.Add(outEdge);
                    }

                    // no incoming if no target
                    if(edge.target != null)
                    {
                        int sourceTypeID;
                        if(edge.source != null) sourceTypeID = edge.source.TypeID;
                        else sourceTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float revCost = graph.vstructs[((edge.target.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + sourceTypeID) * 2 + (int) LGSPDir.In];
#else
                        float revCost = graph.vstructs[edge.target.TypeID, edge.TypeID, sourceTypeID, (int) LGSPDir.In];
#endif
                        if(graph.nodeCounts[edge.target.TypeID] != 0)
                            revCost /= graph.nodeCounts[edge.target.TypeID];
                        PlanEdge inEdge = new PlanEdge(SearchOperationType.Incoming, edge.target.TempPlanMapping, nodes[nodesIndex], revCost);
                        edges.Add(inEdge);
                        nodes[nodesIndex].IncomingEdges.Add(inEdge);
                    }
                }

                nodesIndex++;
            }
            return new PlanGraph(root, nodes, edges.ToArray(), patternGraph);
        }

        /// <summary>
        /// Marks the minimum spanning arborescence of a plan graph by setting the IncomingMSAEdge
        /// fields for all nodes
        /// </summary>
        /// <param name="planGraph">The plan graph to be marked</param>
        /// <param name="dumpName">Names the dump targets if dump compiler flags are set</param>
        public void MarkMinimumSpanningArborescence(PlanGraph planGraph, String dumpName)
        {
            if(DumpSearchPlan)
                DumpPlanGraph(planGraph, dumpName, "initial");

            // nodes not already looked at
            Dictionary<PlanNode, bool> leftNodes = new Dictionary<PlanNode, bool>(planGraph.Nodes.Length);
            foreach(PlanNode node in planGraph.Nodes)
                leftNodes.Add(node, true);

            // epoch = search run
            Dictionary<PlanPseudoNode, bool> epoch = new Dictionary<PlanPseudoNode, bool>();
            LinkedList<PlanSuperNode> superNodeStack = new LinkedList<PlanSuperNode>();

            // work left ?
            while(leftNodes.Count > 0)
            {
                // get first remaining node
                Dictionary<PlanNode, bool>.Enumerator enumerator = leftNodes.GetEnumerator();
                enumerator.MoveNext();
                PlanPseudoNode curNode = enumerator.Current.Key;

                // start a new search run
                epoch.Clear();
                do
                {
                    // next node in search run
                    epoch.Add(curNode, true);
                    if(curNode is PlanNode) leftNodes.Remove((PlanNode) curNode);

                    // cheapest incoming edge of current node
                    float cost;
                    PlanEdge cheapestEdge = curNode.GetCheapestIncoming(curNode, out cost);
                    if(cheapestEdge == null)
                        break;
                    curNode.IncomingMSAEdge = cheapestEdge;

                    // cycle found ?
                    while(epoch.ContainsKey(cheapestEdge.Source.TopNode))
                    {
                        // contract the cycle to a super node
                        PlanSuperNode superNode = new PlanSuperNode(cheapestEdge.Source.TopNode);
                        superNodeStack.AddFirst(superNode);
                        epoch.Add(superNode, true);
                        if(superNode.IncomingMSAEdge == null)
                            goto exitSecondLoop;
                        // continue with new super node as current node
                        curNode = superNode;
                        cheapestEdge = curNode.IncomingMSAEdge;
                    }
                    curNode = cheapestEdge.Source.TopNode;
                }
                while(true);
exitSecondLoop: ;
            }

            if(DumpSearchPlan)
            {
                DumpPlanGraph(planGraph, dumpName, "contracted");
                DumpContractedPlanGraph(planGraph, dumpName);
            }

            // breaks all cycles represented by setting the incoming msa edges of the 
            // representative nodes to the incoming msa edges according to the supernode
            /*no, not equivalent:
            foreach(PlanSuperNode superNode in superNodeStack)
                superNode.Child.IncomingMSAEdge = superNode.IncomingMSAEdge;*/
            foreach (PlanSuperNode superNode in superNodeStack)
            {
                PlanPseudoNode curNode = superNode.IncomingMSAEdge.Target;
                while (curNode.SuperNode != superNode) curNode = curNode.SuperNode;
                curNode.IncomingMSAEdge = superNode.IncomingMSAEdge;
            }

            if(DumpSearchPlan)
                DumpFinalPlanGraph(planGraph, dumpName);
        }

        #region Dump functions
        private String GetDumpName(PlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root) return "root";
            else if(node.NodeType == PlanNodeType.Node) return "node_" + node.PatternElement.Name;
            else return "edge_" + node.PatternElement.Name;
        }

        public void DumpPlanGraph(PlanGraph planGraph, String dumpname, String namesuffix)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-plangraph-" + namesuffix + ".vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(PlanNode node in planGraph.Nodes)
            {
                DumpNode(sw, node);
            }
            foreach(PlanEdge edge in planGraph.Edges)
            {
                DumpEdge(sw, edge, true);
            }
            sw.WriteLine("}\n}\n");
            sw.Close();
        }

        private void DumpNode(StreamWriter sw, PlanNode node)
        {
            if(node.NodeType == PlanNodeType.Edge)
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\" shape:ellipse}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            else
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\"}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
        }

        private void DumpEdge(StreamWriter sw, PlanEdge edge, bool markRed)
        {
            String typeStr = " #";
            switch(edge.Type)
            {
                case SearchOperationType.Outgoing: typeStr = "--"; break;
                case SearchOperationType.Incoming: typeStr = "->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
            }

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / {3:0.00} ({4:0.00}) \"{5}}}",
                GetDumpName(edge.Source), GetDumpName(edge.Target), typeStr, edge.mstCost, edge.Cost,
                markRed ? " color:red" : "");
        }

        private void DumpSuperNode(StreamWriter sw, PlanSuperNode superNode, Dictionary<PlanSuperNode, bool> dumpedSuperNodes)
        {
            PlanPseudoNode curNode = superNode.Child;
            sw.WriteLine("graph:{title:\"super node\" label:\"super node\" status:clustered color:lightgrey");
            dumpedSuperNodes.Add(superNode, true);
            do
            {
                if(curNode is PlanSuperNode)
                {
                    DumpSuperNode(sw, (PlanSuperNode) curNode, dumpedSuperNodes);
                    DumpEdge(sw, curNode.IncomingMSAEdge, true);
                }
                else
                {
                    DumpNode(sw, (PlanNode) curNode);
                    DumpEdge(sw, curNode.IncomingMSAEdge, false);
                }
                curNode = curNode.IncomingMSAEdge.Source;
                while(curNode.SuperNode != null && curNode.SuperNode != superNode)
                    curNode = curNode.SuperNode;
            }
            while(curNode != superNode.Child);
            sw.WriteLine("}");
        }

        private void DumpContractedPlanGraph(PlanGraph planGraph, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-contractedplangraph.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            Dictionary<PlanSuperNode, bool> dumpedSuperNodes = new Dictionary<PlanSuperNode, bool>();

            foreach(PlanNode node in planGraph.Nodes)
            {
                PlanSuperNode superNode = node.TopSuperNode;
                if(superNode != null)
                {
                    if(dumpedSuperNodes.ContainsKey(superNode)) continue;
                    DumpSuperNode(sw, superNode, dumpedSuperNodes);
                    DumpEdge(sw, superNode.IncomingMSAEdge, true);
                }
                else
                {
                    DumpNode(sw, node);
                    DumpEdge(sw, node.IncomingMSAEdge, false);
                }
            }

            sw.WriteLine("}\n}");
            sw.Close();
        }

        private void DumpFinalPlanGraph(PlanGraph planGraph, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-finalplangraph.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(PlanNode node in planGraph.Nodes)
            {
                DumpNode(sw, node);
                DumpEdge(sw, node.IncomingMSAEdge, false);
            }

            sw.WriteLine("}\n}");
            sw.Close();
        }

        private String GetDumpName(SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root) return "root";
            else if(node.NodeType == PlanNodeType.Node) return "node_" + node.PatternElement.Name;
            else return "edge_" + node.PatternElement.Name;
        }

        private void DumpNode(StreamWriter sw, SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Edge)
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\" shape:ellipse}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            else
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\"}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
        }

        private void DumpEdge(StreamWriter sw, SearchOperationType opType, SearchPlanNode source, SearchPlanNode target, float cost, bool markRed)
        {
            String typeStr = " #";
            switch(opType)
            {
                case SearchOperationType.Outgoing: typeStr = "--"; break;
                case SearchOperationType.Incoming: typeStr = "->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
            }

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / {3:0.00}\"{4}}}",
                GetDumpName(source), GetDumpName(target), typeStr, cost, markRed ? " color:red" : "");
        }

        private String SearchOpToString(SearchOperation op)
        {
            String typeStr = "  ";
            SearchPlanNode src = op.SourceSPNode as SearchPlanNode;
            SearchPlanNode tgt = op.Element as SearchPlanNode;
            switch(op.Type)
            {
                case SearchOperationType.Outgoing: typeStr = src.PatternElement.Name + "-" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.Incoming: typeStr = src.PatternElement.Name + "<-" + tgt.PatternElement.Name + "-"; break;
                case SearchOperationType.ImplicitSource: typeStr = "<-" + src.PatternElement.Name + "-" + tgt.PatternElement.Name; break;
                case SearchOperationType.ImplicitTarget: typeStr = "-" + src.PatternElement.Name + "->" + tgt.PatternElement.Name; break;
                case SearchOperationType.Lookup: typeStr = "*" + tgt.PatternElement.Name; break;
                case SearchOperationType.MaybePreset: typeStr = "p" + tgt.PatternElement.Name; break;
                case SearchOperationType.NegPreset: typeStr = "np" + tgt.PatternElement.Name; break;
                case SearchOperationType.Condition:
                    typeStr = " ?(" + String.Join(",", ((Condition) op.Element).NeededNodes) + ")("
                        + String.Join(",", ((Condition) op.Element).NeededEdges) + ")";
                    break;
                case SearchOperationType.NegativePattern:
                    typeStr = " !(" + ScheduleToString(((ScheduledSearchPlan) op.Element).Operations) + " )";
                    break;
            }
            return typeStr;
        }

        private String ScheduleToString(IEnumerable<SearchOperation> schedule)
        {
            StringBuilder str = new StringBuilder();

            foreach(SearchOperation searchOp in schedule)
                str.Append(' ').Append(SearchOpToString(searchOp));

            return str.ToString();
        }

        private void DumpScheduledSearchPlan(ScheduledSearchPlan ssp, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.txt", false);
            sw.WriteLine(ScheduleToString(ssp.Operations));
            sw.Close();

/*            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes: yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");
            SearchPlanNode root = new SearchPlanNode("root");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(SearchOperation op in ssp.Operations)
            {
                switch(op.Type)
                {
                    case SearchOperationType.Lookup:
                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                    {
                        SearchPlanNode spnode = (SearchPlanNode) op.Element;
                        DumpNode(sw, spnode);
                        SearchPlanNode src;
                        switch(op.Type)
                        {
                            case SearchOperationType.Lookup:
                            case SearchOperationType.MaybePreset:
                            case SearchOperationType.NegPreset:
                                src = root;
                                break;
                            default:
                                src = op.SourceSPNode;
                                break;
                        }
                        DumpEdge(sw, op.Type, src, spnode, op.CostToEnd, false);
                        break;
                    }
                    case SearchOperationType.Condition:
                        sw.WriteLine("node:{title:\"Condition\" label:\"CONDITION\"}\n");
                        break;
                    case SearchOperationType.NegativePattern:
                        sw.WriteLine("node:{title:\"NAC\" label:\"NAC\"}\n");
                        break;
                }
            }*/
        }
        #endregion Dump functions

        /// <summary>
        /// Generate search plan graph out of the plan graph, i.e. construct a new graph with outgoing
        /// edges for nodes and only tree edges
        /// </summary>
        /// <param name="planGraph">The source plan graph</param>
        /// <returns>A new search plan graph</returns>
        public SearchPlanGraph GenerateSearchPlanGraph(PlanGraph planGraph)
        {
            SearchPlanNode root = new SearchPlanNode("search plan root");
            SearchPlanNode[] nodes = new SearchPlanNode[planGraph.Nodes.Length];
            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Nodes.Length - 1 + 1];    // +1 for root
            // for generating edges
            Dictionary<PlanNode, SearchPlanNode> planMap = new Dictionary<PlanNode, SearchPlanNode>(planGraph.Nodes.Length);
            planMap.Add(planGraph.Root, root);

            // generate the search plan graph nodes
            int i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType == PlanNodeType.Edge)
                    nodes[i] = new SearchPlanEdgeNode(node, null, null);
                else
                    nodes[i] = new SearchPlanNodeNode(node);
                planMap.Add(node, nodes[i]);
                i++;
            }

            // generate the search plan graph edges
            i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                PlanEdge edge = node.IncomingMSAEdge;
                edges[i] = new SearchPlanEdge(edge.Type, planMap[edge.Source], planMap[edge.Target], edge.Cost);
                planMap[edge.Source].OutgoingEdges.Add(edges[i]);
                if(node.NodeType == PlanNodeType.Edge)
                {
                    SearchPlanEdgeNode spedgenode = (SearchPlanEdgeNode) planMap[node];
                    SearchPlanNode patelem;
                    if(edge.Target.PatternEdgeSource != null && planMap.TryGetValue(edge.Target.PatternEdgeSource, out patelem))
                    {
                        spedgenode.PatternEdgeSource = (SearchPlanNodeNode) patelem;
                        spedgenode.PatternEdgeSource.OutgoingPatternEdges.AddLast(spedgenode);
                    }
                    if(edge.Target.PatternEdgeTarget != null && planMap.TryGetValue(edge.Target.PatternEdgeTarget, out patelem))
                    {
                        spedgenode.PatternEdgeTarget = (SearchPlanNodeNode) patelem;
                        spedgenode.PatternEdgeTarget.IncomingPatternEdges.AddLast(spedgenode);
                    }
                }
                i++;
            }
            return new SearchPlanGraph(root, nodes, edges, planGraph.PatternGraph);
        }

        /// <summary>
        /// Generates a scheduled search plan for a given search plan graph and optional negative search plan graphs
        /// </summary>
        /// <param name="negSpGraphs">a list - possibly empty - if a positive search plan graph is given,
        /// null if a negative search plan graph is scheduled </param>
        public ScheduledSearchPlan ScheduleSearchPlan(SearchPlanGraph spGraph, SearchPlanGraph[] negSpGraphs)
        {
            // todo: erst implicit node, dann negative, auch wenn negative mit erstem implicit moeglich wird

            List<SearchOperation> operations = new List<SearchOperation>();

            //
            // schedule positive search plan
            //
            
            // a set of search plan edges representing the currently reachable not yet visited elements
            PriorityQueue<SearchPlanEdge> activeEdges = new PriorityQueue<SearchPlanEdge>();

            // first schedule all preset elements
            foreach(SearchPlanNode node in spGraph.Nodes)
            {
                if(!node.IsPreset) continue;

                foreach(SearchPlanEdge edge in node.OutgoingEdges)
                    activeEdges.Add(edge);

                SearchOperation newOp = new SearchOperation(
                    negSpGraphs == null ? SearchOperationType.NegPreset : SearchOperationType.MaybePreset,
                    node, spGraph.Root, 0);

                operations.Add(newOp);
            }

            // iterate over all reachable elements until the whole graph has been scheduled(/visited),
            // choose next cheapest operation, update the reachable elements and the search plan costs
            SearchPlanNode lastNode = spGraph.Root;
            for(int i = 0; i < spGraph.Nodes.Length - spGraph.NumPresetElements; i++)
            {
                foreach (SearchPlanEdge edge in lastNode.OutgoingEdges)
                {
                    /* no, that if is needed */
                    if(edge.Type == SearchOperationType.MaybePreset || edge.Type == SearchOperationType.NegPreset) continue;
                    activeEdges.Add(edge);
                }
                SearchPlanEdge minEdge = activeEdges.DequeueFirst();
                lastNode = minEdge.Target;

                SearchOperation newOp = new SearchOperation(minEdge.Type, lastNode,
                    minEdge.Source, minEdge.Cost);

                foreach(SearchOperation op in operations)
                    op.CostToEnd += minEdge.Cost;

                operations.Add(newOp);
            }

            // insert negative search plans into the operation schedule
            if(negSpGraphs != null)
            {
                // schedule each negative search plan

                ScheduledSearchPlan[] negScheduledSPs = new ScheduledSearchPlan[negSpGraphs.Length];
                for(int i = 0; i < negSpGraphs.Length; i++)
                    negScheduledSPs[i] = ScheduleSearchPlan(negSpGraphs[i], null);

                // which elements belong to the positive pattern

                Dictionary<String, bool> positiveElements = new Dictionary<String, bool>();
                foreach(SearchPlanNode spnode in spGraph.Nodes)
                    positiveElements.Add(spnode.PatternElement.Name, true);

                // iterate over all negative scheduled search plans (TODO: order?)
                Dictionary<String, bool> neededElements = new Dictionary<String, bool>();
                foreach(ScheduledSearchPlan negSSP in negScheduledSPs)
                {
                    int bestFitIndex = operations.Count;
                    float bestFitCostToEnd = 0;

                    // calculate needed elements for the negative pattern
                    // (elements from the positive graph needed in order to execute the nac)

                    neededElements.Clear();
                    foreach(SearchOperation op in negSSP.Operations)
                    {
                        if(op.Type == SearchOperationType.NegativePattern) continue;
                        if(op.Type == SearchOperationType.Condition)
                        {
                            Condition cond = (Condition) op.Element;
                            foreach(String neededNode in cond.NeededNodes)
                            {
                                if(positiveElements.ContainsKey(neededNode))
                                    neededElements[neededNode] = true;
                            }
                            foreach(String neededEdge in cond.NeededEdges)
                            {
                                if(positiveElements.ContainsKey(neededEdge))
                                    neededElements[neededEdge] = true;
                            }
                        }
                        else
                        {
                            SearchPlanNode spnode = (SearchPlanNode) op.Element;
                            if(positiveElements.ContainsKey(spnode.PatternElement.Name))
                                neededElements[spnode.PatternElement.Name] = true;
                        }
                    }

                    // find best place for this pattern
                    for(int i = operations.Count - 1; i >= 0; i--)
                    {
                        SearchOperation op = operations[i];
                        if(op.Type == SearchOperationType.NegativePattern 
                            || op.Type == SearchOperationType.Condition) continue;
                        // needed element matched at this operation?
                        if(neededElements.ContainsKey(((SearchPlanNode) op.Element).PatternElement.Name))
                            break;                      // yes, stop here
                        if(negSSP.Cost <= op.CostToEnd)
                        {
                            // best fit as CostToEnd is monotonously growing towards operation[0]
                            bestFitIndex = i;
                            bestFitCostToEnd = op.CostToEnd;
                        }
                    }

                    // and insert pattern there
                    operations.Insert(bestFitIndex, new SearchOperation(SearchOperationType.NegativePattern,
                        negSSP, null, bestFitCostToEnd + negSSP.Cost));

                    // update costs of operations before bestFitIndex
                    for(int i = 0; i < bestFitIndex; i++)
                        operations[i].CostToEnd += negSSP.Cost;
                }
            }

            // Schedule conditions at the earliest position possible

            Condition[] conditions = spGraph.PatternGraph.Conditions;
            for(int j = conditions.Length - 1; j >= 0; j--)            
            {
                Condition condition = conditions[j];
                int k;
                float costToEnd = 0;
                for(k = operations.Count - 1; k >= 0; k--)
                {
                    SearchOperation op = operations[k];
                    if(op.Type == SearchOperationType.NegativePattern || op.Type == SearchOperationType.Condition) continue;
                    String curElemName = ((SearchPlanNode) op.Element).PatternElement.Name;
                    bool isNeededElem = false;
                    if(((SearchPlanNode) op.Element).NodeType == PlanNodeType.Node)
                    {
                        foreach(String neededNode in condition.NeededNodes)
                        {
                            if(curElemName == neededNode)
                            {
                                isNeededElem = true;
                                costToEnd = op.CostToEnd;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach(String neededEdge in condition.NeededEdges)
                        {
                            if(curElemName == neededEdge)
                            {
                                isNeededElem = true;
                                costToEnd = op.CostToEnd;
                                break;
                            }
                        }
                    }
                    // does the current operation retrieve a needed element ?
                    if(isNeededElem)
                        break;          // yes, so the condition cannot be scheduled earlier
                }
                operations.Insert(k + 1, new SearchOperation(SearchOperationType.Condition, condition, null, costToEnd));
            }

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(operations.ToArray(), spGraph.PatternGraph, cost);
        }

        /// <summary>
        /// Appends homomorphy information to each operation of the scheduled search plan
         /// </summary>
        public void AppendHomomorphyInformation(ScheduledSearchPlan ssp)
        {
            // no operation -> nothing which could be homomorph
            if (ssp.Operations.Length == 0)
            {
                return;
            }

            // iterate operations of the search plan to append homomorphy checks
            for (int i = 0; i < ssp.Operations.Length; ++i)
            {
                if (ssp.Operations[i].Type == SearchOperationType.Condition)
                {
                    continue;
                }

                if (ssp.Operations[i].Type == SearchOperationType.NegativePattern)
                {
                    AppendHomomorphyInformation((ScheduledSearchPlan)ssp.Operations[i].Element);
                    continue;
                }

                DetermineAndAppendHomomorphyChecks(ssp, i);
            }
        }

        /// <summary>
        /// Determines which homomorphy check operations are necessary 
        /// at the operation of the given position within the scheduled search plan
        /// and appends them.
        /// </summary>
        public void DetermineAndAppendHomomorphyChecks(ScheduledSearchPlan ssp, int j)
        {
            ///////////////////////////////////////////////////////////////////////////
            // first handle special cases pure homomorphy and pure isomorphy

            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

            bool[] homToAll;

            if (spn_j.NodeType == PlanNodeType.Node) {
                homToAll = ssp.PatternGraph.HomomorphicToAllNodes;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                homToAll = ssp.PatternGraph.HomomorphicToAllEdges;
            }

            if (homToAll[spn_j.ElementID - 1])
            {
                // operation is allowed to be homomorph with everything
                // no checks for isomorphy or restricted homomorphy needed
                return;
            }

            ///////////////////////////////////////////////////////////////////////////
            // no pure homomorphy or isomorphy, so we have restricted homomorphy
            // and need to inspect the operations before together with the homomorphy matrix 
            // for determining the necessary homomorphy checks

            GrGenType[] types;
            bool[,] hom;

            if (spn_j.NodeType == PlanNodeType.Node) {
                types = model.NodeModel.Types;
                hom = ssp.PatternGraph.HomomorphicNodes;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                types = model.EdgeModel.Types;
                hom = ssp.PatternGraph.HomomorphicEdges;
            }

            // order operation to check against all elements it's not allowed to be homomorph to

            // iterate through the operations before our position
            for (int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern)
                {
                    continue;
                }

                SearchPlanNode spn_i = (SearchPlanNode)ssp.Operations[i].Element;

                // don't compare nodes with edges
                if (spn_i.NodeType != spn_j.NodeType)
                {
                    continue;
                }

                // don't check homomorph elements
                if (hom[spn_i.ElementID - 1, spn_j.ElementID - 1])
                {
                    continue;
                }
                
                // find out whether element types are disjoint
                    // todo: optimization: check type constraints
                    // todo: why not check it before and combine it into hom-matrix?
                GrGenType type_i = types[spn_i.PatternElement.TypeID];
                GrGenType type_j = types[spn_j.PatternElement.TypeID];
                bool disjoint = true;
                foreach (GrGenType subtype_i in type_i.SubOrSameTypes)
                {
                    if (type_j.IsA(subtype_i) || subtype_i.IsA(type_j)) // IsA==IsSuperTypeOrSameType
                    {
                        disjoint = false;
                        break;
                    }
                }

                // don't check elements if their types are disjoint
                if (disjoint)
                {
                    continue;
                }

                // the generated matcher code has to check 
                // that pattern element j doesn't get bound to the same graph element
                // the pattern element i is already bound to 
                if (ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst == null) {
                    ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst = new List<SearchPlanNode>();
                }
                ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Add(spn_i);

                // order operation to set the is-matched-bit after all checks succeeded
                ssp.Operations[i].Isomorphy.SetIsMatchedBit = true;
            }

            // only if elements, the operation must be isomorph to, were matched before
            // (otherwise there were only elements, the operation is allowed to be homomorph to,
            //  matched before, so no check needed here)
            if (ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst != null
                && ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Count > 0)
            {
                // order operation to check whether the is-matched-bit is set
                ssp.Operations[j].Isomorphy.CheckIsMatchedBit = true;
            }
        }
     
        /// <summary>
        /// Generates the source code of the matcher function for the given scheduled search plan
        /// new version building first abstract search program then search program code
        /// </summary>
        public String GenerateMatcherSourceCode(ScheduledSearchPlan scheduledSearchPlan,
            String actionName, LGSPRulePattern rulePattern)
        {
            // build pass: build nested program from scheduled search plan
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(
                scheduledSearchPlan, 
                "myMatch", null, null, rulePattern, model);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(actionName + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // build additional: create extra search subprogram per MaybePreset operation
            // will be called when preset element is not available
            searchProgramBuilder.BuildAddionalSearchSubprograms(scheduledSearchPlan,
                searchProgram, rulePattern);

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter searchProgramCompleter = new SearchProgramCompleter();
            searchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(actionName + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // emit pass: emit source code from search program 
            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            searchProgram.Emit(sourceCode);
            return sourceCode.ToString();
        }

        /// <summary>
        /// Generates an LGSPAction object for the given scheduled search plan.
        /// </summary>
        /// <param name="action">Needed for the rule pattern and the name</param>
        /// <param name="sourceOutputFilename">null if no output file needed</param>
        public LGSPAction GenerateMatcher(ScheduledSearchPlan scheduledSearchPlan, LGSPAction action,
            String modelAssemblyLocation, String actionAssemblyLocation, String sourceOutputFilename)
        {
            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyLocation);
            compParams.ReferencedAssemblies.Add(actionAssemblyLocation);

            compParams.GenerateInMemory = true;
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);

            // generate class setup
            sourceCode.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + model.GetType().Namespace + ";\nusing " + action.RulePattern.GetType().Namespace + ";\n\n"
                + "namespace de.unika.ipd.grGen.lgspActions\n{\n    public class DynAction_" + action.Name + " : LGSPAction\n    {\n"
                + "        public DynAction_" + action.Name + "() { rulePattern = " + action.RulePattern.GetType().Name
                + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, " + action.RulePattern.PatternGraph.Nodes.Length
                + ", " + action.RulePattern.PatternGraph.Edges.Length + "); matchesList = matches.matches; }\n"
                + "        public override string Name { get { return \"" + action.Name + "\"; } }\n"
                + "        private LGSPMatches matches;\n"
                + "        private LGSPMatchesList matchesList;\n");

            // generate the matching function
            sourceCode.Append(GenerateMatcherSourceCode(scheduledSearchPlan, action.Name, action.rulePattern));

            // close namespace   TODO: GenerateMatcherSourceCode should not close class
            sourceCode.Append("}");

            // write generated source to file if requested
            if(sourceOutputFilename != null)
            {
                StreamWriter writer = new StreamWriter(sourceOutputFilename);
                writer.Write(sourceCode.ToString());
                writer.Close();
            }
//            Stopwatch compilerWatch = Stopwatch.StartNew();

            // compile generated code
            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                throw new ArgumentException("Illegal dynamic C# source code produced for actions \"" + action.Name + "\": " + errorMsg);
            }

            // create action instance
            Object obj = compResults.CompiledAssembly.CreateInstance("de.unika.ipd.grGen.lgspActions.DynAction_" + action.Name);

//            long compSourceTicks = compilerWatch.ElapsedTicks;
//            Console.WriteLine("GenMatcher: Compile source: {0} us", compSourceTicks / (Stopwatch.Frequency / 1000000));
            return (LGSPAction) obj;
        }

        public LGSPAction[] GenerateSearchPlans(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, 
            params LGSPAction[] actions)
        {
            if(actions.Length == 0) throw new ArgumentException("No actions provided!");

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            sourceCode.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + model.GetType().Namespace + ";\nusing " + actions[0].RulePattern.GetType().Namespace + ";\n\n"
                + "namespace de.unika.ipd.grGen.lgspActions\n{\n");

            foreach(LGSPAction action in actions)
            {
                PlanGraph planGraph = GeneratePlanGraph(graph, (PatternGraph) action.RulePattern.PatternGraph, false);
                MarkMinimumSpanningArborescence(planGraph, action.Name);
                SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);

                SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[action.RulePattern.NegativePatternGraphs.Length];
                for(int i = 0; i < action.RulePattern.NegativePatternGraphs.Length; i++)
                {
                    PlanGraph negPlanGraph = GeneratePlanGraph(graph, (PatternGraph) action.RulePattern.NegativePatternGraphs[i], true);
                    MarkMinimumSpanningArborescence(negPlanGraph, action.Name + "_neg_" + (i + 1));
                    negSearchPlanGraphs[i] = GenerateSearchPlanGraph(negPlanGraph);
                }

                ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(searchPlanGraph, negSearchPlanGraphs);

                AppendHomomorphyInformation(scheduledSearchPlan);

                sourceCode.Append("    public class DynAction_" + action.Name + " : LGSPAction\n    {\n"
                    + "        public DynAction_" + action.Name + "() { rulePattern = "
                    + action.RulePattern.GetType().Name + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, "
                    + action.RulePattern.PatternGraph.Nodes.Length + ", " + action.RulePattern.PatternGraph.Edges.Length + "); "
                    + "matchesList = matches.matches; }\n"
                    + "        public override string Name { get { return \"" + action.Name + "\"; } }\n"
                    + "        private LGSPMatches matches;\n"
                    + "        private LGSPMatchesList matchesList;\n");

                sourceCode.Append(GenerateMatcherSourceCode(scheduledSearchPlan, action.Name, action.rulePattern));
            }
            sourceCode.Append("}");

            if(DumpDynSourceCode)
            {
                using(StreamWriter writer = new StreamWriter("dynamic_" + actions[0].Name + ".cs"))
                    writer.Write(sourceCode.ToString());
            }

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyName);
            compParams.ReferencedAssemblies.Add(actionsAssemblyName);

            compParams.GenerateInMemory = true;
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif

            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                throw new ArgumentException("Internal error: Illegal dynamic C# source code produced: " + errorMsg);
            }

            LGSPAction[] newActions = new LGSPAction[actions.Length];
            for(int i = 0; i < actions.Length; i++)
            {
                newActions[i] = (LGSPAction) compResults.CompiledAssembly.CreateInstance(
                    "de.unika.ipd.grGen.lgspActions.DynAction_" + actions[i].Name);
                if(newActions[i] == null)
                    throw new ArgumentException("Internal error: Generated assembly does not contain action '"
                        + actions[i].Name + "'!");
            }
            return newActions;
        }

        public LGSPAction GenerateSearchPlan(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, LGSPAction action)
        {
            return GenerateSearchPlans(graph, modelAssemblyName, actionsAssemblyName, action)[0];
        }
    }
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A class holding the state/context of a recording session
    /// </summary>
    class RecordingState
    {
        public RecordingState(StreamWriter writer, GraphExportContext exportContext)
        {
            this.writer = writer;
            this.exportContext = exportContext;
        }

        public StreamWriter writer;
        public GraphExportContext exportContext;
    }

    /// <summary>
    /// A class for recording changes (and their causes) applied to a graph into a file,
    /// so that they can get replayed.
    /// </summary>
    public class Recorder : IRecorder
    {
        INamedGraph graph = null;
        IGraphProcessingEnvironment procEnv = null;

        private IDictionary<string, RecordingState> recordings = new Dictionary<string, RecordingState>();

        /// <summary>
        /// Create a recorder
        /// </summary>
        /// <param name="graph">The named graph whose changes are to be recorded</param>
        /// <param name="procEnv">The graph processing environment receiving some of the action events, may be null if only graph changes are requested</param>
        public Recorder(INamedGraph graph, IGraphProcessingEnvironment procEnv)
        {
            Initialize(graph, procEnv);
        }

        /// <summary>
        /// Initializes a recorder after creation, needed if actions are selected later
        /// </summary>
        /// <param name="graph">The named graph whose changes are to be recorded</param>
        /// <param name="procEnv">The graph processing environment receiving some of the action events, may be null if only graph changes are requested</param>
        public void Initialize(INamedGraph graph, IGraphProcessingEnvironment procEnv)
        {
            this.graph = graph;
            this.procEnv = procEnv;
        }

        public void StartRecording(string filename)
        {
            if(!recordings.ContainsKey(filename))
            {
                if(recordings.Count == 0)
                    SubscribeEvents();

                StreamWriter writer = null;
                if(filename.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                    FileStream filewriter = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(new GZipStream(filewriter, CompressionMode.Compress));
                } else {
                    writer = new StreamWriter(filename);
                }

                String pathPrefix = "";
                if(filename.LastIndexOf("/")!=-1 || filename.LastIndexOf("\\")!=-1)
                {
                    int lastIndex = filename.LastIndexOf("/");
                    if(lastIndex==-1) lastIndex = filename.LastIndexOf("\\");
                    pathPrefix = filename.Substring(0, lastIndex+1);
                }
                GraphExportContext mainGraphContext = GRSExport.ExportYouMustCloseStreamWriter(graph, writer, pathPrefix);

                recordings.Add(new KeyValuePair<string, RecordingState>(filename, 
                    new RecordingState(writer, mainGraphContext)));
            }
        }

        public void StopRecording(string filename)
        {
            if(recordings.ContainsKey(filename))
            {
                recordings[filename].writer.Close();
                recordings.Remove(filename);

                if(recordings.Count == 0)
                    UnsubscribeEvents();
            }
        }

        public bool IsRecording(string filename)
        {
            return recordings.ContainsKey(filename);
        }

        public void Write(string value)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.Write(value);
        }

        public void WriteLine(string value)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.Write(value + "\n");
        }

        public void Flush()
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.Flush();
        }

        private void SubscribeEvents()
        {
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            graph.OnRemovingNode += RemovingNode;
            graph.OnRemovingEdge += RemovingEdge;
            graph.OnChangingNodeAttribute += ChangingAttribute;
            graph.OnChangingEdgeAttribute += ChangingAttribute;
            graph.OnRetypingNode += RetypingNode;
            graph.OnRetypingEdge += RetypingEdge;
            graph.OnVisitedAlloc += VisitedAlloc;
            graph.OnVisitedFree += VisitedFree;
            graph.OnSettingVisited += SettingVisited;

            if(procEnv != null)
            {
                procEnv.OnFinishing += BeforeFinish;
                procEnv.OnRewritingNextMatch += RewriteNextMatch;
                procEnv.OnFinished += AfterFinish;
                procEnv.OnSwitchingToSubgraph += SwitchToGraph;
                procEnv.OnReturnedFromSubgraph += ReturnFromGraph;
            }
        }

        private void UnsubscribeEvents()
        {
            graph.OnNodeAdded -= NodeAdded;
            graph.OnEdgeAdded -= EdgeAdded;
            graph.OnRemovingNode -= RemovingNode;
            graph.OnRemovingEdge -= RemovingEdge;
            graph.OnChangingNodeAttribute -= ChangingAttribute;
            graph.OnChangingEdgeAttribute -= ChangingAttribute;
            graph.OnRetypingNode -= RetypingNode;
            graph.OnRetypingEdge -= RetypingEdge;
            graph.OnVisitedAlloc -= VisitedAlloc;
            graph.OnVisitedFree -= VisitedFree;
            graph.OnSettingVisited -= SettingVisited;

            if(procEnv != null)
            {
                procEnv.OnFinishing -= BeforeFinish;
                procEnv.OnRewritingNextMatch += RewriteNextMatch;
                procEnv.OnFinished -= AfterFinish;
                procEnv.OnSwitchingToSubgraph -= SwitchToGraph;
                procEnv.OnReturnedFromSubgraph -= ReturnFromGraph;
            }
        }

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Event handler for IGraph.OnNodeAdded.
        /// </summary>
        /// <param name="node">The added node.</param>
        void NodeAdded(INode node)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("new :" + node.Type.Name + "($=\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnEdgeAdded.
        /// </summary>
        /// <param name="edge">The added edge.</param>
        void EdgeAdded(IEdge edge)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("new @(\"" + graph.GetElementName(edge.Source)
                    + "\") -:" + edge.Type.Name + "($=\"" + graph.GetElementName(edge) + "\")-> @(\""
                    + graph.GetElementName(edge.Target) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingNode.
        /// </summary>
        /// <param name="node">The node to be deleted.</param>
        void RemovingNode(INode node)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("delete node @(\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingEdge.
        /// </summary>
        /// <param name="edge">The edge to be deleted.</param>
        void RemovingEdge(IEdge edge)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("delete edge @(\"" + graph.GetElementName(edge) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingNode.
        /// </summary>
        /// <param name="oldNode">The node to be retyped.</param>
        /// <param name="newNode">The new node with the common attributes, but without the correct connections, yet.</param>
        void RetypingNode(INode oldNode, INode newNode)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("retype @(\"" + graph.GetElementName(oldNode) + "\")<" + newNode.Type.Name + ">");
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingEdge.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but without the correct connections, yet.</param>
        void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("retype -@(\"" + graph.GetElementName(oldEdge) + "\")<" + newEdge.Type.Name + ">->");
        }

        /// <summary>
        /// Event handler for IGraph.OnChangingNodeAttribute and IGraph.OnChangingEdgeAttribute.
        /// </summary>
        /// <param name="element">The node or edge whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
        void ChangingAttribute(IGraphElement element, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            foreach(RecordingState recordingState in recordings.Values)
            {
                GraphExportContext exportContext = recordingState.exportContext;
                AddSubgraphsAsNeeded(exportContext, element, attrType, newValue, recordingState.writer);
                AddSubgraphsAsNeeded(exportContext, element, attrType, keyValue, recordingState.writer);
                switch(changeType)
                {
                case AttributeChangeType.Assign:
                    recordingState.writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name + " = ");
                    GRSExport.EmitAttribute(exportContext, attrType, newValue, graph, recordingState.writer);
                    recordingState.writer.WriteLine();
                    break;
                case AttributeChangeType.PutElement:
                    recordingState.writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        recordingState.writer.Write(".add(");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        recordingState.writer.Write(".add(");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, attrType.KeyType, graph));
                        recordingState.writer.Write(", ");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.ArrayAttr:
                        if(keyValue == null)
                        {
                            recordingState.writer.Write(".add(");
                            recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                            recordingState.writer.WriteLine(")");
                        }
                        else
                        {
                            recordingState.writer.Write(".add(");
                            recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                            recordingState.writer.Write(", ");
                            recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, typeof(int)), graph));
                            recordingState.writer.WriteLine(")");
                        }
                        break;
                    case AttributeKind.DequeAttr:
                        if(keyValue == null)
                        {
                            recordingState.writer.Write(".add(");
                            recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                            recordingState.writer.WriteLine(")");
                        }
                        else
                        {
                            recordingState.writer.Write(".add(");
                            recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                            recordingState.writer.Write(", ");
                            recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, typeof(int)), graph));
                            recordingState.writer.WriteLine(")");
                        }
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.RemoveElement:
                    recordingState.writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        recordingState.writer.Write(".rem(");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        recordingState.writer.Write(".rem(");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, attrType.KeyType, graph));
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.ArrayAttr:
                        recordingState.writer.Write(".rem(");
                        if(keyValue!=null)
                            recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, typeof(int)), graph));
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.DequeAttr:
                        recordingState.writer.Write(".rem(");
                        if(keyValue != null)
                            recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, typeof(int)), graph));
                        recordingState.writer.WriteLine(")");
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.AssignElement:
                    recordingState.writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.ArrayAttr:
                        recordingState.writer.Write("[");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, typeof(int)), graph));
                        recordingState.writer.Write("] = ");
                        recordingState.writer.WriteLine(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                        break;
                    case AttributeKind.DequeAttr:
                        recordingState.writer.Write("[");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, typeof(int)), graph));
                        recordingState.writer.Write("] = ");
                        recordingState.writer.WriteLine(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                        break;
                    case AttributeKind.MapAttr:
                        recordingState.writer.Write("[");
                        recordingState.writer.Write(GRSExport.ToString(exportContext, keyValue, attrType.KeyType, graph));
                        recordingState.writer.Write("] = ");
                        recordingState.writer.WriteLine(GRSExport.ToString(exportContext, newValue, attrType.ValueType, graph));
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                default:
                    throw new Exception("Unknown attribute change type");
                }
            }
        }

        private bool AddSubgraphsAsNeeded(GraphExportContext exportContext,
            IGraphElement element, AttributeType attrType, Object value, StreamWriter writer)
        {
            if(!GRSExport.IsGraphUsedInAttribute(attrType))
                return false;

            if(value == null)
                return false;

            if(!(value is INamedGraph))
                return false;
            
            bool wasAdded = GRSExport.AddSubgraphAsNeeded(exportContext, (INamedGraph)value);
            if(wasAdded)
            {
            restart:
                foreach(KeyValuePair<string, GraphExportContext> kvp in exportContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    if(!context.isExported)
                    {
                        wasAdded = GRSExport.ExportSingleGraph(exportContext, context, writer);
                        if(wasAdded)
                            goto restart;
                    }
                }
                AddGraphAttributes(exportContext, writer);
                writer.WriteLine("in \"" + exportContext.graphToContext[graph].name + "\" # after emitting new subgraph for attribute");
                return true;
            }
            return false;
        }

        private static void AddGraphAttributes(GraphExportContext exportContext, StreamWriter writer)
        {
            foreach(KeyValuePair<string, GraphExportContext> kvp in exportContext.nameToContext)
            {
                GraphExportContext context = kvp.Value;
                GRSExport.EmitSubgraphAttributes(exportContext, context, writer);
            }
        }

        ////////////////////////////////////////////////////////////////////////
        
        public void VisitedAlloc(int visitorID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# valloc " + visitorID);
        }

        public void VisitedFree(int visitorID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# vfree " + visitorID);
        }

        public void SettingVisited(IGraphElement elem, int visitorID, bool newValue)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# visited[" + visitorID + "] = " + newValue);
        }

        ////////////////////////////////////////////////////////////////////////

        void BeforeFinish(IMatches matches, bool special)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# rewriting " + matches.Producer.Name + "..");
        }

        void RewriteNextMatch()
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# rewriting next match");
        }

        void AfterFinish(IMatches matches, bool special)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# ..rewritten " + matches.Producer.Name);
        }

        ////////////////////////////////////////////////////////////////////////

        public void SwitchToGraph(IGraph newGraph)
        {
            IGraph oldGraph = procEnv.Graph;

            foreach(RecordingState recordingState in recordings.Values)
            {
                AddSubgraphsAsNeeded((INamedGraph)newGraph, recordingState);

                recordingState.writer.WriteLine("in \"" + recordingState.exportContext.graphToContext[(INamedGraph)newGraph].name + "\" # due to switch, before: " + oldGraph.Name);
            }

            graph = (INamedGraph)newGraph;
        }

        public void ReturnFromGraph(IGraph oldGraph)
        {
            INamedGraph newGraph = (INamedGraph)procEnv.Graph;
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("in \"" + recordingState.exportContext.graphToContext[newGraph].name + "\" # due to return, before: " + oldGraph.Name);

            graph = newGraph;
        }

        private static bool AddSubgraphsAsNeeded(INamedGraph potentialNewGraph, RecordingState recordingState)
        {
            bool wasAdded = GRSExport.AddSubgraphAsNeeded(recordingState.exportContext, potentialNewGraph);
            if(wasAdded)
            {
            restart:
                foreach(KeyValuePair<string, GraphExportContext> kvp in recordingState.exportContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    if(!context.isExported)
                    {
                        wasAdded = GRSExport.ExportSingleGraph(recordingState.exportContext, context, recordingState.writer);
                        if(wasAdded)
                            goto restart;
                    }
                }
                AddGraphAttributes(recordingState.exportContext, recordingState.writer);
                return true;
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////

        public void TransactionStart(int transactionID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# begin transaction " + transactionID);
        }

        public void TransactionCommit(int transactionID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# commit transaction " + transactionID);
        }

        public void TransactionRollback(int transactionID, bool start)
        {
            if(start)
                foreach(RecordingState recordingState in recordings.Values)
                    recordingState.writer.WriteLine("# rolling back transaction " + transactionID + "..");
            else
                foreach(RecordingState recordingState in recordings.Values)
                    recordingState.writer.WriteLine("# ..rolled back transaction " + transactionID);
        }
    }
}

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

using System.Net;
using System.Net.Sockets;

namespace de.unika.ipd.grGen.grShell
{
    class Debugger
    {
        GrShellImpl grShellImpl;
        ShellGraph shellGraph;

        Process viewerProcess = null;
        YCompClient ycompClient = null;
        Sequence debugSequence = null;
        bool stepMode = true;
        bool detailedMode = false;
        bool recordMode = false;
        bool alwaysShow = true;
        Sequence curStepSequence = null;

        IRulePattern curRulePattern = null;
        int nextAddedNodeIndex = 0;
        int nextAddedEdgeIndex = 0;

        Dictionary<INode, bool> markedNodes = new Dictionary<INode, bool>();
        Dictionary<IEdge, bool> markedEdges = new Dictionary<IEdge, bool>();

        LinkedList<Sequence> loopList = new LinkedList<Sequence>();
        LinkedList<INode> addedNodes = new LinkedList<INode>();
        LinkedList<String> deletedNodes = new LinkedList<String>();
        LinkedList<IEdge> addedEdges = new LinkedList<IEdge>();
        LinkedList<String> deletedEdges = new LinkedList<String>();

        public Debugger(GrShellImpl grShellImpl) : this(grShellImpl, "Orthogonal") {}

        public Debugger(GrShellImpl grShellImpl, String debugLayout)
        {
            this.grShellImpl = grShellImpl;
            this.shellGraph = grShellImpl.CurrentShellGraph;

            int ycompPort = GetFreeTCPPort();
            if(ycompPort < 0)
            {
                throw new Exception("Didn't find a free TCP port in the range 4242-4251!");
            }
            try
            {
                viewerProcess = Process.Start("ycomp", "-p " + ycompPort);
            }
            catch(Exception e)
            {
                throw new Exception("Unable to start ycomp: " + e.ToString());
            }

            try
            {
                ycompClient = new YCompClient(shellGraph.Graph, debugLayout, 20000, ycompPort, shellGraph.DumpInfo);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to YComp at port " + ycompPort + ": " + ex.Message);
            }

            ycompClient.OnConnectionLost += new ConnectionLostHandler(DebugOnConnectionLost);
            shellGraph.Graph.ReuseOptimization = false;

            try
            {
                UploadGraph();
            }
            catch(OperationCanceledException)
            {
                return;
            }

            RegisterLibGrEvents();
        }

        /// <summary>
        /// Closes the debugger.
        /// </summary>
        public void Close()
        {
            if(ycompClient == null)
                throw new InvalidOperationException("The debugger has already been closed!");

            UnregisterLibGrEvents();

            shellGraph.Graph.ReuseOptimization = true;
            ycompClient.Close();
            ycompClient = null;
            viewerProcess.Close();
            viewerProcess = null;
        }

        public void InitNewRewriteSequence(Sequence seq, bool withStepMode)
        {
            debugSequence = seq;
            curStepSequence = null;
            stepMode = withStepMode;
            recordMode = false;
            alwaysShow = false;
            detailedMode = false;
        }

        public void AbortRewriteSequence()
        {
            stepMode = false;
            detailedMode = false;
            loopList.Clear();
        }

        public void FinishRewriteSequence()
        {
            alwaysShow = true;
            try
            {
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
            }
            catch(OperationCanceledException) { }
        }

        public ShellGraph CurrentShellGraph
        {
            get { return shellGraph; }
            set
            {
                // switch to new graph in YComp
                UnregisterLibGrEvents();
                ycompClient.ClearGraph();
                shellGraph = value;
                UploadGraph();
                RegisterLibGrEvents();

                // TODO: reset any state when inside a rule debugging session
            }
        }

        public void ForceLayout()
        {
            ycompClient.ForceLayout();
        }

        public void UpdateYCompDisplay()
        {
            ycompClient.UpdateDisplay();
        }

        public void SetLayout(String layout)
        {
            ycompClient.SetLayout(layout);
        }

        public void GetLayoutOptions()
        {
            String str = ycompClient.GetLayoutOptions();
            Console.WriteLine("Available layout options and their current values:\n\n" + str);
        }

        public void SetLayoutOption(String optionName, String optionValue)
        {
            String errorMessage = ycompClient.SetLayoutOption(optionName, optionValue);
            if(errorMessage != null)
                Console.WriteLine(errorMessage);
        }


/*        private int GetPrecedence(Sequence.OperandType type)
        {
            switch(type)
            {
                case Sequence.OperandType.Concat: return 0;
                case Sequence.OperandType.Xor: return 1;
                case Sequence.OperandType.And: return 2;
                case Sequence.OperandType.Star: return 3;
                case Sequence.OperandType.Max: return 3;
                case Sequence.OperandType.Rule: return 4;
                case Sequence.OperandType.RuleAll: return 4;
                case Sequence.OperandType.Def: return 4;
            }
            return -1;
        }*/

        /// <summary>
        /// Prints the given child sequence inside the parent context adding parentheses around the child if needed.
        /// </summary>
        /// <param name="seq">The child to be printed</param>
        /// <param name="parent">The parent of the child or null if the child is a root</param>
        /// <param name="highlightSeq">A sequence to be highlighted or null</param>
        /// <param name="bpposcounter">A counter increased for every potential breakpoint position and printed next to a potential breakpoint.
        ///     If bpposcounter is smaller than zero, no such counter is used or printed.</param>
        private static void PrintChildSequence(Sequence seq, Sequence parent, Sequence highlightSeq, IWorkaround workaround, ref int bpposCounter)
        {
            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence) Console.Write("(");

            switch(seq.SequenceType)
            {
                case SequenceType.LazyOr:
                case SequenceType.LazyAnd:
                case SequenceType.StrictOr:
                case SequenceType.Xor:
                case SequenceType.StrictAnd:
                {
                    bool first = true;
                    foreach(Sequence child in seq.Children)
                    {
                        if(!first)
                            Console.Write(" " + seq.Symbol + " ");
                        else
                            first = false;
                        PrintChildSequence(child, seq, highlightSeq, workaround, ref bpposCounter);
                    }
                    break;
                }
                case SequenceType.Not:
                {
                    foreach(Sequence child in seq.Children)
                    {
                        Console.Write(seq.Symbol);
                        PrintChildSequence(child, seq, highlightSeq, workaround, ref bpposCounter);
                    }
                    break;
                }
                case SequenceType.Min:
                {
                    SequenceMin seqMin = (SequenceMin)seq;
                    PrintChildSequence(seqMin.Seq, seq, highlightSeq, workaround, ref bpposCounter);
                    Console.Write("[" + seqMin.Min + ":*]");
                    break;
                }
                case SequenceType.MinMax:
                {
                    SequenceMinMax seqMinMax = (SequenceMinMax)seq;
                    PrintChildSequence(seqMinMax.Seq, seq, highlightSeq, workaround, ref bpposCounter);
                    Console.Write("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]");
                    break;
                }

                case SequenceType.Rule:
                case SequenceType.RuleAll:
                case SequenceType.True:
                case SequenceType.False:
                {
                    if(bpposCounter >= 0)
                        workaround.PrintHighlighted("<<" + (bpposCounter++) + ">>");
                    goto case SequenceType.Def;                             // fall through
                }
                case SequenceType.Def:
                case SequenceType.AssignVarToVar:
                case SequenceType.AssignElemToVar:
                {
                    if(seq == highlightSeq)
                        workaround.PrintHighlighted(seq.Symbol);
                    else
                        Console.Write(seq.Symbol);
                    break;
                }

                case SequenceType.Transaction:
                {
                    Console.Write("<");
                    IEnumerator<Sequence> iter = seq.Children.GetEnumerator();
                    iter.MoveNext();
                    PrintSequence(iter.Current, highlightSeq, workaround);
                    Console.Write(">");
                    break;
                }
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence) Console.Write(")");
        }

        public static void PrintSequence(Sequence seq, Sequence highlightSeq, IWorkaround workaround)
        {
            int counter = -1;
            PrintChildSequence(seq, null, highlightSeq, workaround, ref counter);
        }

        /// <summary>
        /// Searches in the given sequence seq for the parent sequence of the sequence childseq.
        /// </summary>
        /// <returns>The parent sequence of childseq or null, if no parent has been found.</returns>
        Sequence GetParentSequence(Sequence childseq, Sequence seq)
        {
            Sequence res = null;
            foreach(Sequence child in seq.Children)
            {
                if(child == childseq) return seq;
                res = GetParentSequence(childseq, child);
                if(res != null) return res;
            }
            return res;
        }

        /// <summary>
        /// Reads a key from the keyboard using the workaround manager of grShellImpl.
        /// If CTRL+C is pressed, grShellImpl.Cancel() is called.
        /// </summary>
        /// <returns>The ConsoleKeyInfo object for the pressed key.</returns>
        ConsoleKeyInfo ReadKeyWithCancel()
        {
            if(grShellImpl.OperationCancelled) grShellImpl.Cancel();
            Console.TreatControlCAsInput = true;
            ConsoleKeyInfo key = grShellImpl.Workaround.ReadKey(true);
            Console.TreatControlCAsInput = false;
            if(key.Key == ConsoleKey.C && (key.Modifiers & ConsoleModifiers.Control) != 0) grShellImpl.Cancel();
            return key;
        }

        SequenceSpecial GetSequenceAtBreakpointPosition(Sequence seq, int bppos, ref int counter)
        {
            if(seq is SequenceSpecial)
            {
                if(counter == bppos)
                    return (SequenceSpecial) seq;
                counter++;
            }
            foreach(Sequence child in seq.Children)
            {
                SequenceSpecial res = GetSequenceAtBreakpointPosition(child, bppos, ref counter);
                if(res != null) return res;
            }
            return null;
        }

        void HandleToggleBreakpoints()
        {
            Console.Write("Available breakpoint positions:\n  ");
            int numbppos = 0;
            PrintChildSequence(debugSequence, null, null, grShellImpl.Workaround, ref numbppos);
            Console.WriteLine();

            if(numbppos == 0)
            {
                Console.WriteLine("No breakpoint positions available!");
                return;
            }
            while(true)
            {
                Console.WriteLine("Choose the position of the breakpoint you want to toggle (-1 for no toggle): ");
                String numStr = Console.ReadLine();
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < -1 || num >= numbppos)
                    {
                        Console.WriteLine("You must specify a number between -1 and " + (numbppos - 1) + "!");
                        continue;
                    }
                    if(num != -1)
                    {
                        int bpcounter = 0;
                        SequenceSpecial bpseq = GetSequenceAtBreakpointPosition(debugSequence, num, ref bpcounter);
                        bpseq.Special = !bpseq.Special;
                    }
                    break;
                }
            }
        }

        void DebugEntereringSequence(Sequence seq)
        {
            bool breakpointReached;
            // Entering a loop?
            if(seq.SequenceType == SequenceType.Min || seq.SequenceType == SequenceType.MinMax)
                loopList.AddFirst(seq);

            // Breakpoint reached?
            if((seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll
                || seq.SequenceType == SequenceType.True || seq.SequenceType == SequenceType.False)
                    && ((SequenceSpecial)seq).Special)
            {
                stepMode = true;
                breakpointReached = true;
            }
            else breakpointReached = false;

            if(stepMode)
            {
                if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll || breakpointReached)
                {
                    ycompClient.UpdateDisplay();
                    ycompClient.Sync();
                    PrintSequence(debugSequence, seq, grShellImpl.Workaround);
                    Console.WriteLine();

                    while(true)
                    {
                        ConsoleKeyInfo key = ReadKeyWithCancel();
                        switch(key.KeyChar)
                        {
                            case 's':
                                detailedMode = false;
                                return;
                            case 'd':
                                detailedMode = true;
                                return;
                            case 'n':
                                detailedMode = false;
                                stepMode = false;
                                curStepSequence = GetParentSequence(seq, debugSequence);
                                return;
                            case 'o':
                                stepMode = false;
                                detailedMode = false;
                                if(loopList.Count == 0) curStepSequence = null;         // execute until the end
                                else curStepSequence = loopList.First.Value;            // execute until current loop has been exited
                                return;
                            case 'r':
                                stepMode = false;
                                detailedMode = false;
                                curStepSequence = null;                                 // execute until the end
                                return;
                            case 'b':
                                HandleToggleBreakpoints();
                                PrintSequence(debugSequence, seq, grShellImpl.Workaround);
                                Console.WriteLine();
                                break;
                            case 'a':
                                grShellImpl.Cancel();
                                return;                                                 // never reached
                            default:
                                Console.WriteLine("Illegal command (Key = " + key.Key
                                    + ")! Only (s)tep, (n)ext, step (o)ut, (d)etailed step, (r)un, toggle (b)reakpoints and (a)bort allowed!");
                                break;
                        }
                    }
                }
            }
        }

        void DebugExitingSequence(Sequence seq)
        {
            if(stepMode == false && seq == curStepSequence)
                stepMode = true;
            if(seq.SequenceType == SequenceType.Min || seq.SequenceType == SequenceType.MinMax)
                loopList.RemoveFirst();
        }

        void DebugMatched(IMatches matches, bool special)
        {
            if(detailedMode == false || matches.Count == 0) return;

            markedNodes.Clear();
            markedEdges.Clear();

            foreach(IMatch match in matches)
            {
                int i = 0;
                foreach(INode node in match.Nodes)
                {
                    ycompClient.ChangeNode(node, ycompClient.MatchedNodeRealizer);
                    ycompClient.AnnotateElement(node, matches.Producer.RulePattern.PatternGraph.Nodes[i].Name.Substring(5));
                    markedNodes[node] = true;
                    i++;
                }
                i = 0;
                foreach(IEdge edge in match.Edges)
                {
                    ycompClient.ChangeEdge(edge, ycompClient.MatchedEdgeRealizer);
                    ycompClient.AnnotateElement(edge, matches.Producer.RulePattern.PatternGraph.Edges[i].Name.Substring(5));
                    markedEdges[edge] = true;
                    i++;
                }
            }
            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to apply rewrite...");
            ReadKeyWithCancel();

            foreach(IMatch match in matches)
            {
                int i = 0;
                foreach(INode node in match.Nodes)
                {
                    ycompClient.ChangeNode(node, null);
//                    ycompClient.AnnotateElement(node, null);
                    i++;
                }
                i = 0;
                foreach(IEdge edge in match.Edges)
                {
                    ycompClient.ChangeEdge(edge, null);
//                    ycompClient.AnnotateElement(edge, null);
                    i++;
                }
            }

            recordMode = true;
            ycompClient.NodeRealizer = ycompClient.NewNodeRealizer;
            ycompClient.EdgeRealizer = ycompClient.NewEdgeRealizer;
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
            curRulePattern = matches.Producer.RulePattern;
        }

        void DebugNodeAdded(INode node)
        {
            ycompClient.AddNode(node);
            if(recordMode)
            {
                addedNodes.AddLast(node);
                ycompClient.AnnotateElement(node, curRulePattern.AddedNodeNames[nextAddedNodeIndex++]);
            }
            else if(alwaysShow) ycompClient.UpdateDisplay();
        }

        void DebugEdgeAdded(IEdge edge)
        {
            ycompClient.AddEdge(edge);
            if(recordMode)
            {
                addedEdges.AddLast(edge);
                ycompClient.AnnotateElement(edge, curRulePattern.AddedEdgeNames[nextAddedEdgeIndex++]);
            }
            else if(alwaysShow) ycompClient.UpdateDisplay();
        }

        void DebugDeletingNode(INode node)
        {
            if(!recordMode)
            {
                ycompClient.DeleteNode(node);
                if(alwaysShow) ycompClient.UpdateDisplay();
            }
            else
            {
                markedNodes.Remove(node);
                ycompClient.ChangeNode(node, ycompClient.DeletedNodeRealizer);

                String name = ycompClient.Graph.GetElementName(node);
                ycompClient.RenameNode(name, "zombie_" + name);
                deletedNodes.AddLast("zombie_" + name);
            }
        }

        void DebugDeletingEdge(IEdge edge)
        {
            if(!recordMode)
            {
                ycompClient.DeleteEdge(edge);
                if(alwaysShow) ycompClient.UpdateDisplay();
            }
            else
            {
                markedEdges.Remove(edge);
                ycompClient.ChangeEdge(edge, ycompClient.DeletedEdgeRealizer);

                String name = ycompClient.Graph.GetElementName(edge);
                ycompClient.RenameEdge(name, "zombie_" + name);
                deletedEdges.AddLast("zombie_" + name);
            }
        }

        void DebugClearingGraph()
        {
            ycompClient.ClearGraph();
        }

        void DebugChangingNodeAttribute(INode node, AttributeType attrType, object oldValue, object newValue)
        {
            ycompClient.ChangeNodeAttribute(node, attrType, newValue.ToString());
        }

        void DebugChangingEdgeAttribute(IEdge edge, AttributeType attrType, object oldValue, object newValue)
        {
            ycompClient.ChangeEdgeAttribute(edge, attrType, newValue.ToString());
        }

        void DebugRetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            ycompClient.RetypingElement(oldElem, newElem);
        }

        void DebugFinished(IMatches matches, bool special)
        {
            if(detailedMode == false) return;

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to continue...");
            ReadKeyWithCancel();

            foreach(INode node in addedNodes)
            {
                ycompClient.ChangeNode(node, null);
                ycompClient.AnnotateElement(node, null);
            }
            foreach(IEdge edge in addedEdges)
            {
                ycompClient.ChangeEdge(edge, null);
                ycompClient.AnnotateElement(edge, null);
            }
            foreach(String edgeName in deletedEdges)
            {
                ycompClient.DeleteEdge(edgeName);
            }
            foreach(String nodeName in deletedNodes)
            {
                ycompClient.DeleteNode(nodeName);
            }
            foreach(INode node in markedNodes.Keys)
            {
                ycompClient.AnnotateElement(node, null);
            }
            foreach(IEdge edge in markedEdges.Keys)
            {
                ycompClient.AnnotateElement(edge, null);
            }
            ycompClient.NodeRealizer = null;
            ycompClient.EdgeRealizer = null;

            addedNodes.Clear();
            addedEdges.Clear();
            deletedEdges.Clear();
            deletedNodes.Clear();
            recordMode = false;
        }

        void DebugOnConnectionLost()
        {
            Console.WriteLine("Connection to YComp lost!");
            grShellImpl.SetDebugMode(false);
            grShellImpl.Cancel();
        }

        /// <summary>
        /// Registers event handlers for needed LibGr events
        /// </summary>
        void RegisterLibGrEvents()
        {
            shellGraph.Graph.OnNodeAdded += new NodeAddedHandler(DebugNodeAdded);
            shellGraph.Graph.OnEdgeAdded += new EdgeAddedHandler(DebugEdgeAdded);
            shellGraph.Graph.OnRemovingNode += new RemovingNodeHandler(DebugDeletingNode);
            shellGraph.Graph.OnRemovingEdge += new RemovingEdgeHandler(DebugDeletingEdge);
            shellGraph.Graph.OnClearingGraph += new ClearingGraphHandler(DebugClearingGraph);
            shellGraph.Graph.OnChangingNodeAttribute += new ChangingNodeAttributeHandler(DebugChangingNodeAttribute);
            shellGraph.Graph.OnChangingEdgeAttribute += new ChangingEdgeAttributeHandler(DebugChangingEdgeAttribute);
            shellGraph.Graph.OnRetypingNode += new RetypingNodeHandler(DebugRetypingElement);
            shellGraph.Graph.OnRetypingEdge += new RetypingEdgeHandler(DebugRetypingElement);

            if(shellGraph.Actions != null)
            {
                shellGraph.Actions.OnEntereringSequence += new EnterSequenceHandler(DebugEntereringSequence);
                shellGraph.Actions.OnExitingSequence += new ExitSequenceHandler(DebugExitingSequence);
                shellGraph.Actions.OnMatched += new AfterMatchHandler(DebugMatched);
                shellGraph.Actions.OnFinished += new AfterFinishHandler(DebugFinished);
            }
        }

        /// <summary>
        /// Unregisters the events previously registered with RegisterLibGrEvents()
        /// </summary>
        void UnregisterLibGrEvents()
        {
            shellGraph.Graph.OnNodeAdded -= new NodeAddedHandler(DebugNodeAdded);
            shellGraph.Graph.OnEdgeAdded -= new EdgeAddedHandler(DebugEdgeAdded);
            shellGraph.Graph.OnRemovingNode -= new RemovingNodeHandler(DebugDeletingNode);
            shellGraph.Graph.OnRemovingEdge -= new RemovingEdgeHandler(DebugDeletingEdge);
            shellGraph.Graph.OnClearingGraph -= new ClearingGraphHandler(DebugClearingGraph);
            shellGraph.Graph.OnChangingNodeAttribute -= new ChangingNodeAttributeHandler(DebugChangingNodeAttribute);
            shellGraph.Graph.OnChangingEdgeAttribute -= new ChangingEdgeAttributeHandler(DebugChangingEdgeAttribute);
            shellGraph.Graph.OnRetypingNode -= new RetypingNodeHandler(DebugRetypingElement);
            shellGraph.Graph.OnRetypingEdge -= new RetypingEdgeHandler(DebugRetypingElement);

            if(shellGraph.Actions != null)
            {
                shellGraph.Actions.OnEntereringSequence -= new EnterSequenceHandler(DebugEntereringSequence);
                shellGraph.Actions.OnExitingSequence -= new ExitSequenceHandler(DebugExitingSequence);
                shellGraph.Actions.OnMatched -= new AfterMatchHandler(DebugMatched);
                shellGraph.Actions.OnFinished -= new AfterFinishHandler(DebugFinished);
            }
        }

        /// <summary>
        /// Uploads the graph to YComp, updates the display and makes a synchonisation
        /// </summary>
        void UploadGraph()
        {
            foreach(INode node in shellGraph.Graph.Nodes)
                ycompClient.AddNode(node);
            foreach(IEdge edge in shellGraph.Graph.Edges)
                ycompClient.AddEdge(edge);
            ycompClient.UpdateDisplay();
            ycompClient.Sync();
        }

        /// <summary>
        /// Searches for a free TCP port in the range 4242-4251
        /// </summary>
        /// <returns>A free TCP port or -1, if they are all occupied</returns>
        int GetFreeTCPPort()
        {
            for(int i = 4242; i < 4252; i++)
            {
                try
                {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, i);
                    using(Socket socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                    {
                        socket.Bind(endpoint);
                    }
                }
                catch(SocketException)
                {
                    continue;
                }
                return i;
            }
            return -1;
        }
    }
}
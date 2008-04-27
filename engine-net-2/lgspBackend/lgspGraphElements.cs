/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universit�t Karlsruhe, Institut f�r Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

//#define ELEMENTKNOWSVARIABLES

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class implementing nodes in the lib gr search plan backend
    /// </summary>
    public abstract class LGSPNode : INode
    {
        /// <summary>
        /// The node type of the node.
        /// </summary>
        public NodeType type;

#if ELEMENTKNOWSVARIABLES
        /// <summary>
        /// List of variables pointing to this element or null if there is no such variable
        /// </summary>
        public LinkedList<Variable> variableList;
#endif

        /// <summary>
        /// Bit within flags at this position tells whether some variable contains this element
        /// </summary>
        public const uint HAS_VARIABLES = 1 << 0;

        /// <summary>
        /// Bit within flags at this position tells during the matching process whether the element 
        /// is already matched within some enclosing pattern
        /// </summary>
        public const uint IS_MATCHED_BY_ENCLOSING_PATTERN = 1 << 1;

        /// <summary>
        /// Bit within flags at this position tells during the matching process whether the element 
        /// is already matched within the local pattern, shifted by current neg level
        /// </summary>
        public const uint IS_MATCHED = 1 << 2;

        /// <summary>
        /// contains some booleans coded as bitvector
        /// </summary>
        public uint flags;

        /// <summary>
        /// Previous and next node in the list containing all the nodes of one type.
        /// The node is not part of a graph, iff typePrev is null.
        /// If typePrev is null and typeNext is not null, this node has been retyped and typeNext
        /// points to the new node.
        /// These special cases are neccessary to handle the following situations:
        /// "delete node + return edge", "hom + delete + return", "hom + retype + return", "hom + retype + delete",
        /// "hom + retype + delete + return".
        /// </summary>
        public LGSPNode typePrev, typeNext;

        /// <summary>
        /// Entry node into the outgoing edges list - not of type edge head, real edge or null
        /// </summary>
        public LGSPEdge outhead;

        /// <summary>
        /// Entry node into the incoming edges list - not of type edge head, real edge or null
        /// </summary>
        public LGSPEdge inhead;

        /// <summary>
        /// Instantiates an LGSPNode object.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        public LGSPNode(NodeType nodeType)
        {
            type = nodeType;
        }

        /// <summary>
        /// This is true, if this node is a valid graph element, i.e. it is part of a graph.
        /// </summary>
        public bool Valid
        {
            get { return typePrev != null; }
        }

		/// <summary>
		/// The element which replaced this element (Valid is false in this case)
		/// or null, if this element has not been replaced or is still a valid member of a graph.
		/// </summary>
		public IGraphElement ReplacedByElement
		{
			get { return ReplacedByNode; }
		}

		/// <summary>
		/// The node which replaced this node (Valid is false in this case)
		/// or null, if this node has not been replaced or is still a valid member of a graph.
		/// </summary>
		public LGSPNode ReplacedByNode
        {
            get { return typePrev != null ? null : typeNext; }
        }

		/// <summary>
		/// The node which replaced this node (Valid is false in this case)
		/// or null, if this node has not been replaced or is still a valid member of a graph.
		/// </summary>
        INode INode.ReplacedByNode
        {
			get { return ReplacedByNode; }
        }
    

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleOutgoing(EdgeType edgeType)
        {
            if(outhead == null) yield break;
            LGSPEdge cur = outhead.outNext;
            LGSPEdge next;
            while(outhead != null && cur != outhead)
            {
                next = cur.outNext;
                if(cur.Type.IsA(edgeType))
                    yield return cur;
                cur = next;
            }
            if(outhead != null && outhead.Type.IsA(edgeType))
                yield return outhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleIncoming(EdgeType edgeType)
        {
            if(inhead == null) yield break;
            LGSPEdge cur = inhead.inNext;
            LGSPEdge next;
            while(inhead != null && cur != inhead)
            {
                next = cur.inNext;
                if(cur.Type.IsA(edgeType))
                    yield return cur;
                cur = next;
            }
            if(inhead != null && inhead.Type.IsA(edgeType))
                yield return inhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactOutgoing(EdgeType edgeType)
        {
            if(outhead == null) yield break;
            LGSPEdge cur = outhead.outNext;
            LGSPEdge next;
            while(outhead != null && cur != outhead)
            {
                next = cur.outNext;
                if(cur.Type == edgeType)
                    yield return cur;
                cur = next;
            }
            if(outhead != null && outhead.Type == edgeType)
                yield return outhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactIncoming(EdgeType edgeType)
        {
            if(inhead == null) yield break;
            LGSPEdge cur = inhead.inNext;
            LGSPEdge next;
            while(inhead != null && cur != inhead)
            {
                next = cur.inNext;
                if(cur.Type == edgeType)
                    yield return cur;
                cur = next;
            }
            if(inhead != null && inhead.Type == edgeType)
                yield return inhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges
        /// </summary>
        public IEnumerable<IEdge> Outgoing
        {
            get
            {
                if(outhead == null) yield break;
                LGSPEdge cur = outhead.outNext;
                LGSPEdge next;
                while(outhead != null && cur != outhead)
                {
                    next = cur.outNext;
                    yield return cur;
                    cur = next;
                }
                if(outhead != null)
                    yield return outhead;
            }
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges
        /// </summary>
        public IEnumerable<IEdge> Incoming
        {
            get
            {
                if(inhead == null) yield break;
                LGSPEdge cur = inhead.inNext;
                LGSPEdge next;
                while(inhead != null && cur != inhead)
                {
                    next = cur.inNext;
                    yield return cur;
                    cur = next;
                }
                if(inhead != null)
                    yield return inhead;
            }
        }
        internal bool HasOutgoing { get { return outhead != null; } }
        internal bool HasIncoming { get { return inhead != null; } }

        internal void AddOutgoing(LGSPEdge edge)
        {
            if(outhead == null)
            {
                outhead = edge;
                edge.outNext = edge;
                edge.outPrev = edge;
            }
            else
            {
                outhead.outPrev.outNext = edge;
                edge.outPrev = outhead.outPrev;
                edge.outNext = outhead;
                outhead.outPrev = edge;
            }
        }

        internal void AddIncoming(LGSPEdge edge)
        {
            if(inhead == null)
            {
                inhead = edge;
                edge.inNext = edge;
                edge.inPrev = edge;
            }
            else
            {
                inhead.inPrev.inNext = edge;
                edge.inPrev = inhead.inPrev;
                edge.inNext = inhead;
                inhead.inPrev = edge;
            }
        }

        internal void RemoveOutgoing(LGSPEdge edge)
        {
            if(edge == outhead)
            {
                outhead = edge.outNext;
                if(outhead == edge)
                    outhead = null;
            }
            edge.outPrev.outNext = edge.outNext;
            edge.outNext.outPrev = edge.outPrev;

            edge.outNext = null;
            edge.outPrev = null;
        }

        internal void RemoveIncoming(LGSPEdge edge)
        {
            if(edge == inhead)
            {
                inhead = edge.inNext;
                if(inhead == edge)
                    inhead = null;
            }
            edge.inPrev.inNext = edge.inNext;
            edge.inNext.inPrev = edge.inPrev;

            edge.inNext = null;
            edge.inPrev = null;
        }

        /// <summary>
        /// Moves the head of the outgoing list after the given edge.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="edge">The edge.</param>
        public void MoveOutHeadAfter(LGSPEdge edge)
        {
            outhead = edge.outNext;
        }

        /// <summary>
        /// Moves the head of the incoming list after the given edge.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="edge">The edge.</param>
        public void MoveInHeadAfter(LGSPEdge edge)
        {
            inhead = edge.inNext;
        }

        /// <summary>
        /// The NodeType of the node.
        /// </summary>
        public NodeType Type { get { return type; } }

        /// <summary>
        /// The GrGenType of the node.
        /// </summary>
        GrGenType IGraphElement.Type { get { return type; } }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type.
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return type.IsA(otherType);
        }

        /// <summary>
        /// Returns the graph element attribute with the given attribute name.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        public abstract object GetAttribute(string attrName);

        /// <summary>
        /// Sets the graph element attribute with the given attribute name to the given value.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        public abstract void SetAttribute(string attrName, object value);

        /// <summary>
        /// Resets all graph element attributes to their initial values.
        /// </summary>
        public abstract void ResetAllAttributes();

        /// <summary>
        /// Creates a copy of this node.
        /// All attributes will be transfered to the new node.
        /// The node will not be associated to a graph, yet.
        /// So it will not have any adjacent edges nor any assigned variables.
        /// </summary>
        /// <returns>A copy of this node.</returns>
        public abstract INode Clone();

        /// <summary>
        /// Recycles this node. This may pool the node or just ignore it.
        /// </summary>
        public abstract void Recycle();
    }

    /// <summary>
    /// Special head node of the lists containing all the nodes of one type
    /// </summary>
    public class LGSPNodeHead : LGSPNode
    {
        public LGSPNodeHead() : base(null) { }

        public override object GetAttribute(string attrName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetAttribute(string attrName, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override INode Clone()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Recycle()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ResetAllAttributes()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    /// <summary>
    /// Class implementing edges in the lib gr search plan backend
    /// </summary>
    public abstract class LGSPEdge : IEdge
    {
        /// <summary>
        /// The EdgeType of the edge.
        /// </summary>
        public EdgeType type;

#if ELEMENTKNOWSVARIABLES
        /// <summary>
        /// List of variables pointing to this element or null if there is no such variable
        /// </summary>
        public LinkedList<Variable> variableList;
#endif

        /// <summary>
        /// Bit within flags at this position tells whether some variable contains this element
        /// </summary>
        public const uint HAS_VARIABLES = 1 << 0;

        /// <summary>
        /// Bit within flags at this position tells during the matching process whether the element 
        /// is already matched within some enclosing pattern
        /// </summary>
        public const uint IS_MATCHED_BY_ENCLOSING_PATTERN = 1 << 1;

        /// <summary>
        /// Bit within flags at this position tells during the matching process whether the element 
        /// is already matched within the local pattern, shifted by current neg level
        /// </summary>
        public const uint IS_MATCHED = 1 << 2;

        /// <summary>
        /// contains some booleans coded as bitvector
        /// </summary>
        public uint flags;

        /// <summary>
        /// Previous and next edge in the list containing all the edges of one type.
        /// The node is not part of a graph, iff typePrev is null.
        /// If typePrev is null and typeNext is not null, this node has been retyped and typeNext
        /// points to the new node.
        /// These special cases are neccessary to handle the following situations:
        /// "delete node + return edge", "hom + delete + return", "hom + retype + return", "hom + retype + delete",
        /// "hom + retype + delete + return".
        /// </summary>
        public LGSPEdge typeNext, typePrev;

        /// <summary>
        /// source and target nodes of this edge
        /// </summary>
        public LGSPNode source, target;

        /// <summary>
        /// previous and next edge in the incoming list of the target node containing all of it's incoming edges
        /// </summary>
        public LGSPEdge inNext, inPrev;

        /// <summary>
        /// previous and next edge in the outgoing list of the source node containing all of it's outgoing edges
        /// </summary>
        public LGSPEdge outNext, outPrev;

        /// <summary>
        /// Instantiates an LGSPEdge object.
        /// </summary>
        /// <param name="edgeType">The edge type.</param>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="targetNode">The target node.</param>
        public LGSPEdge(EdgeType edgeType, LGSPNode sourceNode, LGSPNode targetNode)
        {
            type = edgeType;
            source = sourceNode;
            target = targetNode;
        }

        /// <summary>
        /// This is true, if this edge is a valid graph element, i.e. it is part of a graph.
        /// </summary>
        public bool Valid
        {
            get { return typePrev != null; }
        }

		/// <summary>
		/// The element which replaced this element (Valid is false in this case)
		/// or null, if this element has not been replaced or is still a valid member of a graph.
		/// </summary>
		public IGraphElement ReplacedByElement
		{
			get { return ReplacedByEdge; }
		}

		/// <summary>
		/// The edge which replaced this edge (Valid is false in this case)
		/// or null, if this edge has not been replaced or is still a valid member of a graph.
		/// </summary>
        public LGSPEdge ReplacedByEdge
        {
            get { return typePrev != null ? null : typeNext; }
        }

		/// <summary>
		/// The edge which replaced this edge (Valid is false in this case)
		/// or null, if this edge has not been replaced or is still a valid member of a graph.
		/// </summary>
		IEdge IEdge.ReplacedByEdge
        {
			get { return ReplacedByEdge; }
        }

        /// <summary>
        /// The source node of the edge.
        /// </summary>
        public INode Source { get { return source; } }

        /// <summary>
        /// The target node of the edge.
        /// </summary>
        public INode Target { get { return target; } }

        /// <summary>
        /// The EdgeType of the edge.
        /// </summary>
        public EdgeType Type { get { return type; } }

        /// <summary>
        /// The GrGenType of the edge.
        /// </summary>
        GrGenType IGraphElement.Type { get { return type; } }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return type.IsA(otherType);
        }

        /// <summary>
        /// Returns the graph element attribute with the given attribute name.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        public abstract object GetAttribute(string attrName);

        /// <summary>
        /// Sets the graph element attribute with the given attribute name to the given value.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        public abstract void SetAttribute(string attrName, object value);

        /// <summary>
        /// Resets all graph element attributes to their initial values.
        /// </summary>
        public abstract void ResetAllAttributes();

        /// <summary>
        /// Creates a copy of this edge.
        /// All attributes will be transfered to the new edge.
        /// The edge will not be associated to a graph, yet.
        /// So it will not have any assigned variables.
        /// </summary>
        /// <param name="newSource">The new source node for the new edge.</param>
        /// <param name="newTarget">The new target node for the new edge.</param>
        /// <returns>A copy of this edge.</returns>
        public abstract IEdge Clone(INode newSource, INode newTarget);

        /// <summary>
        /// Recycles this edge. This may pool the edge or just ignore it.
        /// </summary>
        public abstract void Recycle();
    }

    /// <summary>
    /// Special head edge of the lists containing all the edges of one type
    /// </summary>
    public class LGSPEdgeHead : LGSPEdge
    {
        public LGSPEdgeHead() : base(null, null, null) { }

        public override object GetAttribute(string attrName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetAttribute(string attrName, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IEdge Clone(INode newSource, INode newTarget)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Recycle()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ResetAllAttributes()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

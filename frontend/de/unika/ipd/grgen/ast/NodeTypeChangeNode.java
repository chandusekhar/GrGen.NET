/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Adam Szalkowski
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.RetypedNode;

/**
 *
 */
public class NodeTypeChangeNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(NodeTypeChangeNode.class, "node type change decl");
	}

	private BaseNode oldUnresolved;
	private NodeDeclNode old = null;

	public NodeTypeChangeNode(IdentNode id, BaseNode newType, int context, BaseNode oldid, PatternGraphNode directlyNestingLHSGraph) {
		super(id, newType, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.oldUnresolved = oldid;
		becomeParent(this.oldUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(oldUnresolved, old));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("old");
		return childrenNames;
	}

	private static final DeclarationResolver<NodeDeclNode> nodeResolver = new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();

		old = nodeResolver.resolve(oldUnresolved, this);
		if(old != null)
			old.retypedElem = this;

		return successfullyResolved && old != null;
	}

	/**
	 * @return the original node for this retyped node
	 */
	protected final NodeDeclNode getOldNode() {
		assert isResolved();

		return old;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		boolean res = super.checkLocal()
			& nodeChecker.check(old, error);
		if (!res) {
			return false;
		}

		// check if source node of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode
					|| curr instanceof SubpatternDeclNode
					|| curr instanceof AlternativeCaseNode))
		{
			prev = curr;
			// doesn't matter which parent you choose, in the end you reach RuleDeclNode/SubpatternDeclNode/AlternativeCaseNode
			curr = curr.getParents().iterator().next();
		}
		if (curr instanceof RuleDeclNode && prev == ((RuleDeclNode)curr).right
				|| curr instanceof SubpatternDeclNode && prev == ((SubpatternDeclNode)curr).right
				|| curr instanceof AlternativeCaseNode && prev == ((AlternativeCaseNode)curr).right) {
			reportError("Source node of retype may not be declared in replace/modify part");
			res = false;
		}

		// check if two ambiguous retyping statements for the same node declaration occurs
		Collection<BaseNode> parents = old.getParents();
		for (BaseNode p : parents) {
			// to be erroneous there must be another NodeTypeChangeNode with the same OLD-child
			if (p != this && p instanceof NodeTypeChangeNode) {
				reportError("Two (and hence ambiguous) retype statements for the same node are forbidden,"
								+ " previous retype statement at " + p.getCoords());
				res = false;
			}
		}

		return res & onlyReplacementNodesAreAllowedToChangeType();
	}

	private boolean onlyReplacementNodesAreAllowedToChangeType() {
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			return true;
		}

		constraints.reportError("pattern nodes are not allowed to change type, only replacement nodes are");
		return false;
	}

	@Override
	public Node getNode() {
		return checkIR(Node.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		NodeTypeNode tn = getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		RetypedNode res = new RetypedNode(ident.getIdent(), nt, ident.getAnnotations(),
				isMaybeDeleted(), isMaybeRetyped(), context);

		Node oldNode = old.getNode();
		oldNode.setRetypedNode(res);
		res.setOldNode(oldNode);
		res.directlyNestingLHSGraph = oldNode.directlyNestingLHSGraph;

		if (inheritsType()) {
			res.setTypeof(typeNodeDecl.checkIR(Node.class));
		}

		return res;
	}
}


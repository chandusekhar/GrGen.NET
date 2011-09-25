/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * A class representing a node type
 */
public class NodeTypeNode extends InheritanceTypeNode {
	static {
		setName(NodeTypeNode.class, "node type");
	}

	private CollectNode<NodeTypeNode> extend;

	/**
	 * Create a new node type
	 * @param ext The collect node containing the node types which are extended by this type.
	 * @param body the collect node with body declarations
	 * @param modifiers Type modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body,
						int modifiers, String externalName) {
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(getValidVersion(bodyUnresolved, body));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		return childrenNames;
	}

	private static final CollectResolver<NodeTypeNode> extendResolver =	new CollectResolver<NodeTypeNode>(
			new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class));

	@SuppressWarnings("unchecked")
	private static final CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
			new DeclarationResolver<BaseNode>(MemberDeclNode.class,
					MemberInitNode.class, MapInitNode.class, SetInitNode.class, ArrayInitNode.class,
					ConstructorDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		body = bodyResolver.resolve(bodyUnresolved, this);
		extend = extendResolver.resolve(extendUnresolved, this);

		// Initialize direct sub types
		if (extend != null) {
			for (InheritanceTypeNode type : extend.getChildren()) {
				type.addDirectSubType(this);
			}
		}

		return body != null && extend != null;
	}

	/**
	 * Get the IR node type for this AST node.
	 * @return The correctly casted IR node type.
	 */
	protected NodeType getNodeType() {
		return checkIR(NodeType.class);
	}

	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		NodeType nt = new NodeType(getDecl().getIdentNode().getIdent(),
								   getIRModifiers(), getExternalName());

		if (isIRAlreadySet()) { // break endless recursion in case of member of set/map of node/edge type
			return getIR();
		} else{
			setIR(nt);
		}

		constructIR(nt);

		return nt;
	}

	protected CollectNode<? extends InheritanceTypeNode> getExtends() {
		return extend;
	}

	@Override
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		assert isResolved();

		for(NodeTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
    }

	public static String getKindStr() {
		return "node type";
	}

	public static String getUseStr() {
		return "node type";
	}

	@Override
	protected Collection<NodeTypeNode> getDirectSuperTypes() {
		assert isResolved();

	    return extend.getChildren();
    }

	@Override
	protected void getMembers(Map<String, DeclNode> members) {
		assert isResolved();

		for(BaseNode n : body.getChildren()) {
			if(n instanceof ConstructorDeclNode) continue;
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;

				DeclNode old=members.put(decl.getIdentNode().toString(), decl);
				if(old!=null && !(old instanceof AbstractMemberDeclNode)) {
					// TODO this should be part of a check (that return false)
					error.error(decl.getCoords(), "member " + decl.toString() +" of " +
									getUseString() + " " + getIdentNode() +
									" already defined in " + old.getParents() + "." // TODO improve error message
							   );
				}
			}
		}
	}
}

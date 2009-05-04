/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.IR;

/**
 * An identifier expression.
 */
public class IdentExprNode extends DeclExprNode {
	static {
		setName(IdentExprNode.class, "ident expression");
	}
	
	public IdentExprNode(IdentNode ident) {
		super(ident);
	}
	
	protected boolean resolveLocal() {
		decl = ((DeclaredCharacter) declUnresolved).getDecl();
		if(decl instanceof TypeDeclNode)
			return true;
		
		return super.resolveLocal();
	}
	
	public IdentNode getIdent() {
		return (IdentNode) declUnresolved;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		return childrenNames;
	}
	
	protected IR constructIR() {
		BaseNode declNode = (BaseNode) decl;
		if(declNode instanceof TypeDeclNode)
			return new Constant(BasicTypeNode.typeType.getType(),
					((TypeDeclNode) decl).getDeclType().getIR());
		else 
			return super.constructIR();
	}
}

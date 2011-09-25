/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.awt.Color;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 *
 */
public class EmitNode extends OrderedReplacementNode {
	static {
		setName(EmitNode.class, "emit");
	}

	private Vector<ExprNode> childrenUnresolved = new Vector<ExprNode>();

	public EmitNode(Coords coords) {
		super(coords);
	}

	public void addChild(ExprNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	@Override
	public Collection<? extends BaseNode> getChildren() {
		return childrenUnresolved;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if (childrenUnresolved.isEmpty()) {
			reportError("Emit statement is empty");
			return false;
		}
		return true;
	}

	@Override
	public Color getNodeColor() {
		return Color.PINK;
	}

	@Override
	protected IR constructIR() {
		List<Expression> arguments = new ArrayList<Expression>();
		for(BaseNode child : getChildren())
			arguments.add(child.checkIR(Expression.class));
		Emit res= new Emit(arguments);
		return res;
	}
}

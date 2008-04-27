/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universit�t Karlsruhe, Institut f�r Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Set;

public class Constant extends Expression {

	/** The value of the constant. */
	protected Object value;

	/**
	 * @param type The type of the constant.
	 * @param value The value of the constant.
	 */
	public Constant(Type type, Object value) {
		super("constant", type);
		this.value = value;
	}

	/** @return The value of the constant. */
	public Object getValue() {
		return value;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return getName() + " " + value;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectElementsAndVars(Set<Node> nodes, Set<Edge> edges, Set<Variable> vars) {
	}
}

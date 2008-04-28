/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.BooleanType;
import de.unika.ipd.grgen.ir.IR;

/**
 * The boolean basic type.
 */
public class BooleanTypeNode extends BasicTypeNode
{
	static {
		setName(BooleanTypeNode.class, "boolean type");
	}

	protected IR constructIR() {
		return new BooleanType(getIdentNode().getIdent());
	}
	public String toString() {
		return "boolean";
	}
};

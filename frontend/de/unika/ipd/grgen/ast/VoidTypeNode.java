/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universit�t Karlsruhe, Institut f�r Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.VoidType;

/**
 * The void basic type. It is compatible to no other type.
 */
public class VoidTypeNode extends BasicTypeNode
{
	static {
		setName(VoidTypeNode.class, "void type");
	}

	protected IR constructIR() {
		return new VoidType(getIdentNode().getIdent());
	}

	public String toString() {
		return "void";
	}
};

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ast.BasicTypeNode;

/**
 * A string type.
 */
public class StringType extends PrimitiveType {

	/** @param ident The name of the string type. */
	public StringType(Ident ident) {
		super("string type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_STRING;
	}

	public static Type getType() {
		return BasicTypeNode.stringType.checkIR(Type.class);
	}
}
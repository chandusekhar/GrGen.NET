/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * ObjectType.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BasicTypeNode;

public class ObjectType extends PrimitiveType {
	/** @param ident The name of the boolean type. */
	public ObjectType(Ident ident) {
		super("object type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_OBJECT;
	}
	
	public static Type getType() {
		return BasicTypeNode.objectType.checkIR(Type.class);
	}
}


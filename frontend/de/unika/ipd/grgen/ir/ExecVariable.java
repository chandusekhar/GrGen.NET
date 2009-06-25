/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A variable declared inside an "exec" statement containing nodes, edges or primitive types.
 */
public class ExecVariable extends Entity {
	public ExecVariable(String name, Ident ident, Type type) {
		super(name, ident, type, false);
	}
}

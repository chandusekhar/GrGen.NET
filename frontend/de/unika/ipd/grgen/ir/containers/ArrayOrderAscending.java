/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.exprevals.*;

public class ArrayOrderAscending extends Expression {
	private Expression targetExpr;

	public ArrayOrderAscending(Expression targetExpr) {
		super("array order ascending expr", (ArrayType)targetExpr.getType());
		this.targetExpr = targetExpr;
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}
}

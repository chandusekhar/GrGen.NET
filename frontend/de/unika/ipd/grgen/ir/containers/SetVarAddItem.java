/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ast.BaseNode;

public class SetVarAddItem extends EvalStatement {
	Variable target;
    Expression valueExpr;

	public SetVarAddItem(Variable target, Expression valueExpr) {
		super("set var add item");
		this.target = target;
		this.valueExpr = valueExpr;
	}

	public Variable getTarget() {
		return target;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target) && (target.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(target);

		getValueExpr().collectNeededEntities(needs);

		if(getNext()!=null) {
			getNext().collectNeededEntities(needs);
		}
	}
}

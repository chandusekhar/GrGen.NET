/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.QueueItem;
import de.unika.ipd.grgen.parser.Coords;

public class QueueItemNode extends BaseNode {
	static {
		setName(QueueItemNode.class, "queue item");
	}

	protected ExprNode valueExpr;

	public QueueItemNode(Coords coords, ExprNode valueExpr) {
		super(coords);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		// All checks are done in QueueInitNode
		return true;
	}

	@Override
	protected IR constructIR() {
		return new QueueItem(valueExpr.checkIR(Expression.class));
	}

	protected QueueItem getQueueItem() {
		return checkIR(QueueItem.class);
	}
	
	public boolean noDefElementInCondition() {
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noDefElementInCondition();
		}
		return res;
	}
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

public abstract class EvalStatement extends IR implements OrderedReplacement
{
	EvalStatement next; // may contain following statement, generated by optimization pass breaking up expressions

	public EvalStatement(String name) {
		super(name);
	}

	public EvalStatement getNext() {
		return next;
	}

	public void setNext(EvalStatement next) {
		this.next = next;
	}

	public abstract void collectNeededEntities(NeededEntities needs);
}
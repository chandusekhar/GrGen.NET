/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.VAllocProc;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class VAllocProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(VAllocProcNode.class, "valloc procedure");
	}

	Vector<TypeNode> returnTypes;

	public VAllocProcNode(Coords coords) {
		super(coords);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		VAllocProc vAlloc = new VAllocProc();
		for(TypeNode type : getType()) {
			vAlloc.addReturnType(type.getType());
		}
		return vAlloc;
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(BasicTypeNode.intType);
		}
		return returnTypes;
	}
}

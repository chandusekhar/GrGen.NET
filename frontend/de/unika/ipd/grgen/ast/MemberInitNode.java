/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universit�t Karlsruhe, Institut f�r Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a member initialization.
 * children: LHS:IdentNode, RHS:ExprNode
 */
public class MemberInitNode extends BaseNode {
	static {
		setName(MemberInitNode.class, "member init");
	}

	BaseNode lhsUnresolved;
	DeclNode lhs;
	ExprNode rhs;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param member The member to be initialized.
	 * @param expr The expression, that is assigned.
	 */
	public MemberInitNode(Coords coords, IdentNode member, ExprNode expr) {
		super(coords);
		this.lhsUnresolved = member;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhs));
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>(DeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		//Resolver rhsResolver = new OneOfResolver(new Resolver[] {new DeclResolver(DeclNode.class), new MemberInitResolver(DeclNode.class)});
		//successfullyResolved = rhsResolver.resolve(this, RHS) && successfullyResolved;
		lhs = lhsResolver.resolve(lhsUnresolved, this);

		return lhs != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return typeCheckLocal();
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	protected boolean typeCheckLocal() {
		ExprNode expr = rhs;
		TypeNode targetType = lhs.getDeclType();
		TypeNode exprType = expr.getType();

		if (! exprType.isEqual(targetType)) {
			expr = expr.adjustType(targetType);
			becomeParent(expr);
			rhs = expr;

			if (expr == ConstNode.getInvalid()) {
				String msg;
				if (exprType.isCastableTo(targetType)) {
					msg = "Assignment of " + exprType + " to " + targetType + " without a cast";
				} else {
					msg = "Incompatible assignment from " + exprType + " to " + targetType;
				}
				error.error(getCoords(), msg);
				return false;
			}
		}
		return true;
	}

	/**
	 * Construct the intermediate representation from a member init.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return new MemberInit(lhs.checkIR(Entity.class), rhs.checkIR(Expression.class));
	}
}

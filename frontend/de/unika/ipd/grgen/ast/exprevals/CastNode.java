/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.exprevals.Cast;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.parser.Coords;


/**
 * A cast operator for expressions.
 */
public class CastNode extends ExprNode {
	static {
		setName(CastNode.class, "cast expression");
	}

	// target type of the cast
	private BasicTypeNode type;
	private BaseNode typeUnresolved;

	// expression to be casted
	private ExprNode expr;

	/**
	 * Make a new cast node.
	 * @param coords The source code coordinates.
	 */
	public CastNode(Coords coords) {
		super(coords);
	}

	/**
	 * Make a new cast node with a target type and an expression
	 * @param coords The source code coordinates.
	 * @param targetType The target type.
	 * @param expr The expression to be casted.
	 */
	public CastNode(Coords coords, BaseNode targetType, ExprNode expr) {
		super(coords);
		this.typeUnresolved = targetType;
		becomeParent(this.typeUnresolved);
		this.expr = expr;
		becomeParent(this.expr);
	}

	/**
	 * Make a new cast node with a target type and an expression, which is immediately marked as resolved
	 * Only to be called by type adjusting, after tree was already resolved
	 * @param coords The source code coordinates.
	 * @param targetType The target type.
	 * @param expr The expression to be casted.
	 * @param resolveResult Resolution result (should be true)
	 */
	public CastNode(Coords coords, TypeNode targetType, ExprNode expr, BaseNode parent) {
		this(coords, targetType, expr);
		parent.becomeParent(this);

		resolve();
		check();
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(typeUnresolved, type));
		children.add(expr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("type");
		childrenNames.add("expr");
		return childrenNames;
	}

	private static DeclarationTypeResolver<BasicTypeNode> typeResolver =
		new DeclarationTypeResolver<BasicTypeNode>(BasicTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		type = typeResolver.resolve(typeUnresolved, this);
		successfullyResolved = type!=null && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 * A cast node is valid, if the second child is an expression node
	 * and the first node is a type node identifier.
	 */
	@Override
	protected boolean checkLocal() {
		return typeCheckLocal();
	}

	/**
	 * Check the types of this cast.
	 * Check if the expression can be casted to the given type.
	 * @see de.unika.ipd.grgen.ast.BaseNode#typeCheckLocal()
	 */
	private boolean typeCheckLocal() {
		boolean result = expr.getType().isCastableTo(type);
		if(!result) {
			reportError("Illegal cast from \"" + expr.getType() + "\" to \"" + type + "\"");
		}

		return result;
	}

	/**
	 * Tries to simplify this node by simplifying the target expression and,
	 * if the expression is a constant, applying the cast.
	 * @return The possibly simplified value of the expression.
	 */
	@Override
	public ExprNode evaluate() {
		assert isResolved();

		expr = expr.evaluate();
		return expr instanceof ConstNode ? ((ConstNode)expr).castTo(type): this;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 */
	@Override
	public TypeNode getType() {
		assert isResolved();

		return type;
	}

	@Override
	protected IR constructIR() {
		Type type = this.type.checkIR(Type.class);
		Expression expr = this.expr.checkIR(Expression.class);

		return new Cast(type, expr);
	}
}

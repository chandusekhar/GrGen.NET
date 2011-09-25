/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Set;
import java.util.Vector;


/**
 * AST node for a replacement rule.
 */
// TODO: a rule is not a test, factor the common stuff out into a base class
public class RuleDeclNode extends TestDeclNode {
	static {
		setName(RuleDeclNode.class, "rule declaration");
	}

	protected RhsDeclNode right;
	protected RuleTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode ruleType = new RuleTypeNode();

	/**
	 * Make a new rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 * @param neg The context preventing the rule to match.
	 */
	public RuleDeclNode(IdentNode id, PatternGraphNode left, RhsDeclNode right,
			CollectNode<BaseNode> rets) {
		super(id, ruleType, left, rets);
		this.right = right;
		becomeParent(this.right);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(returnFormalParametersUnresolved, returnFormalParameters));
		children.add(pattern);
		children.add(right);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("ret");
		childrenNames.add("pattern");
		childrenNames.add("right");
		return childrenNames;
	}

	protected static final DeclarationTypeResolver<RuleTypeNode> typeResolver =	new DeclarationTypeResolver<RuleTypeNode>(RuleTypeNode.class);
	private static final CollectResolver<TypeNode> retTypeResolver = new CollectResolver<TypeNode>(
    		new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);
		returnFormalParameters = retTypeResolver.resolve(returnFormalParametersUnresolved, this);

		return type != null && returnFormalParameters != null;
	}

	protected Set<DeclNode> getDelete() {
		return right.getDelete(pattern);
	}

	/**
	 * Check that only graph elements are returned, that are not deleted.
	 *
	 * The check also consider the case that a node is returned and homomorphic
	 * matching is allowed with a deleted node.
	 */
	private boolean checkReturnedElemsNotDeleted() {
		assert isResolved();

		boolean valid = true;
		Set<DeclNode> delete = right.getDelete(pattern);
		Collection<DeclNode> maybeDeleted = right.getMaybeDeleted(pattern);

		for (ExprNode expr : right.graph.returns.getChildren()) {
			if(!(expr instanceof DeclExprNode)) continue;

			ConstraintDeclNode retElem = ((DeclExprNode) expr).getConstraintDeclNode();
			if(retElem == null) continue;

			if (delete.contains(retElem)) {
				valid = false;

				expr.reportError("The deleted " + retElem.getUseString()
						+ " \"" + retElem.ident + "\" must not be returned");
			}
			else if(maybeDeleted.contains(retElem)) {
				retElem.maybeDeleted = true;

				if(!retElem.getIdentNode().getAnnotations().isFlagSet("maybeDeleted")) {
					valid = false;

					String errorMessage = "Returning \"" + retElem.ident + "\" that may be deleted"
							+ ", possibly it's homomorphic with a deleted " + retElem.getUseString();
					errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

					if(retElem instanceof EdgeDeclNode) {
						errorMessage += " or \"" + retElem.ident + "\" is a dangling " + retElem.getUseString()
								+ " and a deleted node exists";
					}
					expr.reportError(errorMessage);
				}
			}
		}
		return valid;
	}

	/**
	 * Check that only graph elements are returned, that are not retyped.
	 *
	 * The check also consider the case that a node is returned and homomorphic
	 * matching is allowed with a retyped node.
	 */
	private boolean checkReturnedElemsNotRetyped() {
		assert isResolved();

		boolean valid = true;

		for (ExprNode expr : right.graph.returns.getChildren()) {
			if(!(expr instanceof DeclExprNode)) continue;

			ConstraintDeclNode retElem = ((DeclExprNode) expr).getConstraintDeclNode();
			if(retElem == null) continue;

			if (retElem.getRetypedElement() != null) {
				valid = false;

				expr.reportError("The retyped " + retElem.getUseString()
						+ " \"" + retElem.ident + "\" must not be returned");
			}
		}
		return valid;
	}

	/**
	 * Check that every graph element is retyped to at most one type.
	 */
	private boolean checkElemsNotRetypedToDifferentTypes() {
		assert isResolved();

		boolean valid = true;

		for(Set<ConstraintDeclNode> homSet : pattern.getHoms()) {
			boolean multipleRetypes = false;
			InheritanceTypeNode type = null;

			for(ConstraintDeclNode elem : homSet) {
				ConstraintDeclNode retypedElem = elem.getRetypedElement();

				if(retypedElem != null) {
					InheritanceTypeNode currentType = retypedElem.getDeclType();

					if (type != null && currentType != type) {
						multipleRetypes = true;
						break;
					}

					type = currentType;
				}
			}

			if (multipleRetypes) {
				valid = false;

				for(ConstraintDeclNode elem : homSet) {
					ConstraintDeclNode retypedElem = elem.getRetypedElement();

					if(retypedElem != null) {
						retypedElem.reportError("The " + elem.getUseString() + " "
								+ elem + " must not retyped to different types");
					}
				}
			}
		}

		return valid;
	}

	/**
	 * Check that only graph elements are retyped, that are not deleted.
	 */
	private boolean checkRetypedElemsNotDeleted() {
		assert isResolved();

		boolean valid = true;

		for (DeclNode decl : getDelete()) {
			if(!(decl instanceof ConstraintDeclNode)) continue;

			ConstraintDeclNode retElem = ((ConstraintDeclNode) decl);

			if (retElem.getRetypedElement() != null) {
				valid = false;

				retElem.reportError("The retyped " + retElem.getUseString()
						+ " \"" + retElem.ident + "\" must not be deleted");
			}
		}
		return valid;
	}


	/**
	 * Check that exec parameters are not deleted.
	 *
	 * The check consider the case that parameters are deleted due to
	 * homomorphic matching.
	 */
	private boolean checkExecParamsNotDeleted() {
		assert isResolved();

		boolean valid = true;
		Set<DeclNode> delete = right.getDelete(pattern);
		Collection<DeclNode> maybeDeleted = right.getMaybeDeleted(pattern);

		for (BaseNode x : right.graph.imperativeStmts.getChildren()) {
			if(!(x instanceof ExecNode)) continue;

			ExecNode exec = (ExecNode) x;
			for(CallActionNode callAction : exec.callActions.getChildren()) {
				for(ExprNode arg : callAction.params.getChildren()) {
					if(!(arg instanceof DeclExprNode)) continue;

					ConstraintDeclNode declNode = ((DeclExprNode) arg).getConstraintDeclNode();
					if(declNode != null) {
						if(delete.contains(declNode)) {
							arg.reportError("The deleted " + declNode.getUseString()
									+ " \"" + declNode.ident + "\" must not be passed to an exec statement");
							valid = false;
						}
						else if (maybeDeleted.contains(declNode)) {
							declNode.maybeDeleted = true;

							if(!declNode.getIdentNode().getAnnotations().isFlagSet("maybeDeleted")) {
								valid = false;

								String errorMessage = "Parameter \"" + declNode.ident + "\" of exec statement may be deleted"
										+ ", possibly it's homomorphic with a deleted " + declNode.getUseString();
								errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

								if(declNode instanceof EdgeDeclNode) {
									errorMessage += " or \"" + declNode.ident + "\" is a dangling " + declNode.getUseString()
											+ " and a deleted node exists";
								}
								arg.reportError(errorMessage);
							}
						}
					}
				}
			}
		}
		return valid;
	}

	// TODO: pull this and the other code duplications up to ActionDeclNode
	/**
	 * Checks, whether the reused nodes and edges of the RHS are consistent with the LHS.
	 * If consistent, replace the dummy nodes with the nodes the pattern edge is
	 * incident to (if these aren't dummy nodes themselves, of course).
	 */
	private boolean checkRhsReuse(PatternGraphNode left, RhsDeclNode right) {
		boolean res = true;
		HashMap<EdgeDeclNode, NodeDeclNode> redirectedFrom = new HashMap<EdgeDeclNode, NodeDeclNode>();
		HashMap<EdgeDeclNode, NodeDeclNode> redirectedTo = new HashMap<EdgeDeclNode, NodeDeclNode>();
		Collection<EdgeDeclNode> alreadyReported = new HashSet<EdgeDeclNode>();
		for (ConnectionNode rConn : right.getReusedConnections(left)) {
			EdgeDeclNode re = rConn.getEdge();

			if (re instanceof EdgeTypeChangeNode) {
				re = ((EdgeTypeChangeNode)re).getOldEdge();
			}

			for (BaseNode lc : left.getConnections()) {
				if (!(lc instanceof ConnectionNode)) {
					continue;
				}

				ConnectionNode lConn = (ConnectionNode) lc;

				EdgeDeclNode le = lConn.getEdge();

				if (!le.equals(re)) {
					continue;
				}

				if (lConn.getConnectionKind() != rConn.getConnectionKind()) {
					res = false;
					rConn.reportError("Reused edge does not have the same connection kind");
					// if you don't add to alreadyReported erroneous errors can occur,
					// e.g. lhs=x-e->y, rhs=y-e-x
					alreadyReported.add(re);
				}

				NodeDeclNode lSrc = lConn.getSrc();
				NodeDeclNode lTgt = lConn.getTgt();
				NodeDeclNode rSrc = rConn.getSrc();
				NodeDeclNode rTgt = rConn.getTgt();

				HashSet<BaseNode> rhsNodes = new HashSet<BaseNode>();
				rhsNodes.addAll(right.getReusedNodes(left));

				if (rSrc instanceof NodeTypeChangeNode) {
					rSrc = ((NodeTypeChangeNode)rSrc).getOldNode();
					rhsNodes.add(rSrc);
				}
				if (rTgt instanceof NodeTypeChangeNode) {
					rTgt = ((NodeTypeChangeNode)rTgt).getOldNode();
					rhsNodes.add(rTgt);
				}

				if ( ! lSrc.isDummy() ) {
					if ( rSrc.isDummy() ) {
						if ( rhsNodes.contains(lSrc) ) {
							//replace the dummy src node by the src node of the pattern connection
							rConn.setSrc(lSrc);
						} else if ( ! alreadyReported.contains(re) ) {
							res = false;
							rConn.reportError("The source node of reused edge \"" + le + "\" must be reused, too");
							alreadyReported.add(re);
						}
					} else if (lSrc != rSrc && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_SOURCE)!=ConnectionNode.REDIRECT_SOURCE && ! alreadyReported.contains(re)) {
						res = false;
						rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes (and is not declared to redirect source)");
						alreadyReported.add(re);
					}
				}

				if ( (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_SOURCE)==ConnectionNode.REDIRECT_SOURCE ) {
					if(rSrc.isDummy()) {
						res = false;
						rConn.reportError("An edge with source redirection must be given a source node.");
					}
					
					if(lSrc.equals(rSrc)) {
						rConn.reportWarning("Redirecting edge to same source again.");
					}
					
					if(redirectedFrom.containsKey(le)) {
						res = false;
						rConn.reportError("Can't redirect edge source more than once.");
					}
					redirectedFrom.put(le, rSrc);
				}

				if ( ! lTgt.isDummy() ) {
					if ( rTgt.isDummy() ) {
						if ( rhsNodes.contains(lTgt) ) {
							//replace the dummy tgt node by the tgt node of the pattern connection
							rConn.setTgt(lTgt);
						} else if ( ! alreadyReported.contains(re) ) {
							res = false;
							rConn.reportError("The target node of reused edge \"" + le + "\" must be reused, too");
							alreadyReported.add(re);
						}
					} else if ( lTgt != rTgt && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_TARGET)!=ConnectionNode.REDIRECT_TARGET && ! alreadyReported.contains(re)) {
						res = false;
						rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes (and is not declared to redirect target)");
						alreadyReported.add(re);
					}
				}
				
				if ( (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_TARGET)==ConnectionNode.REDIRECT_TARGET ) {
					if(rTgt.isDummy()) {
						res = false;
						rConn.reportError("An edge with target redirection must be given a target node.");
					}
					
					if(lTgt.equals(rTgt)) {
						rConn.reportWarning("Redirecting edge to same target again.");
					}
					
					if(redirectedTo.containsKey(le)) {
						res = false;
						rConn.reportError("Can't redirect edge target more than once.");
					}
					redirectedTo.put(le, rSrc);
				}

				//check, whether RHS "adds" a node to a dangling end of a edge
				if ( ! alreadyReported.contains(re) ) {
					if ( lSrc.isDummy() && ! rSrc.isDummy() && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_SOURCE)!=ConnectionNode.REDIRECT_SOURCE ) {
						res = false;
						rConn.reportError("Reused edge dangles on LHS, but has a source node on RHS");
						alreadyReported.add(re);
					}
					if ( lTgt.isDummy() && ! rTgt.isDummy() && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_TARGET)!=ConnectionNode.REDIRECT_TARGET ) {
						res = false;
						rConn.reportError("Reused edge dangles on LHS, but has a target node on RHS");
						alreadyReported.add(re);
					}
				}
			}
		}
		return res;
	}

	private void calcMaybeRetyped() {
		for(Set<ConstraintDeclNode> homSet : pattern.getHoms()) {
			boolean containsRetypedElem = false;
			for(ConstraintDeclNode elem : homSet) {
				if(elem.getRetypedElement() != null) {
					containsRetypedElem = true;
					break;
				}
			}

			// If there was one homomorphic element, which is retyped,
			// all non-retyped elements in the same hom group are marked
			// as maybeRetyped.
			if(containsRetypedElem) {
				for(ConstraintDeclNode elem : homSet) {
					if(elem.getRetypedElement() == null)
						elem.maybeRetyped = true;
				}
			}
		}
	}

	private boolean SameNumberOfRewriteParts() {
		boolean res = true;

		for(AlternativeNode alt : pattern.alts.getChildren()) {
			for(AlternativeCaseNode altCase : alt.getChildren()) {
				if(altCase.right.getChildren().size() != 1) {
					error.error(getCoords(), "Different number of replacement patterns/rewrite parts in rule " + ident.toString()
							+ " and nested alternative case " + altCase.ident.toString());
					res = false;
					continue;
				}
			}
		}

		for(IteratedNode iter : pattern.iters.getChildren()) {
			if(iter.right.getChildren().size() != 1) {
				error.error(getCoords(), "Different number of replacement patterns/rewrite parts in rule " + ident.toString()
						+ " and nested iterated/multiple/optional " + iter.ident.toString());
				res = false;
				continue;
			}
		}

		return res;
	}

	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		right.warnElemAppearsInsideAndOutsideDelete(pattern);

		boolean leftHandGraphsOk = super.checkLocal();

		PatternGraphNode left = pattern;
		GraphNode right = this.right.graph;

		// check if the pattern name equals the rule name
		// named replace/modify parts are only allowed in subpatterns
		String ruleName = ident.toString();
		if (!right.nameOfGraph.equals(ruleName)) {
			this.right.reportError("Named replace/modify parts in rules are not allowed");
		}

		// check if parameters only exists for subpatterns
		if (right.params.getChildren().size() > 0) {
			this.right.reportError("Parameters for the replace/modify part are only allowed in subpatterns");
		}

		boolean noReturnInPatternOk = true;
		if(pattern.returns.children.size() > 0) {
			reportError("No return statements in pattern parts of rules allowed");
			noReturnInPatternOk = false;
		}

		calcMaybeRetyped();

		boolean abstr = true;
		for(NodeDeclNode node : right.getNodes()) {
			if(!node.inheritsType() && node.getDeclType().isAbstract() && !left.getNodes().contains(node) && (node.context&CONTEXT_PARAMETER)!=CONTEXT_PARAMETER) {
				error.error(node.getCoords(), "Instances of abstract nodes are not allowed");
				abstr = false;
			}
		}
		for(EdgeDeclNode edge : right.getEdges()) {
			if(!edge.inheritsType() && edge.getDeclType().isAbstract() && !left.getEdges().contains(edge) && (edge.context&CONTEXT_PARAMETER)!=CONTEXT_PARAMETER) {
				error.error(edge.getCoords(), "Instances of abstract edges are not allowed");
				abstr = false;
			}
		}

		return leftHandGraphsOk & checkRhsReuse(left, this.right)
				& SameNumberOfRewriteParts()
				& noReturnInPatternOk & abstr & checkRetypedElemsNotDeleted()
				& checkReturnedElemsNotDeleted() & checkElemsNotRetypedToDifferentTypes()
				& checkReturnedElemsNotRetyped() & checkExecParamsNotDeleted()
				& checkReturns(right.returns);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		PatternGraph right = this.right.getPatternGraph(left);

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		Rule rule = new Rule(getIdentNode().getIdent(), left, right);

		constructImplicitNegs(left);
		constructIRaux(rule, this.right.graph.returns);

		// add eval statements to the IR
		for (EvalStatement n : this.right.getRHSGraph().getYieldEvalStatements()) {
			rule.addEval(n);
		}

		return rule;
	}

	@Override
	public RuleTypeNode getDeclType() {
		assert isResolved();

		return type;
	}
}

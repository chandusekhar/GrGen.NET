/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;

public class SubpatternReplNode extends BaseNode {
	static {
		setName(SubpatternReplNode.class, "subpattern repl node");
	}

	IdentNode subpatternUnresolved;
	SubpatternUsageNode subpattern;
	CollectNode<IdentNode> replConnectionsUnresolved;
	CollectNode<ConstraintDeclNode> replConnections;


	public SubpatternReplNode(IdentNode n, CollectNode<IdentNode> c) {
		this.subpatternUnresolved = n;
		becomeParent(this.subpatternUnresolved);
		this.replConnectionsUnresolved = c;
		becomeParent(this.replConnectionsUnresolved);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(subpatternUnresolved, subpattern));
		children.add(getValidVersion(replConnectionsUnresolved, replConnections));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subpattern");
		childrenNames.add("replConnections");
		return childrenNames;
	}

	private static final DeclarationResolver<SubpatternUsageNode> subpatternResolver =
		new DeclarationResolver<SubpatternUsageNode>(SubpatternUsageNode.class);
	private static final CollectPairResolver<ConstraintDeclNode> connectionsResolver =
		new CollectPairResolver<ConstraintDeclNode>(new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		subpattern = subpatternResolver.resolve(subpatternUnresolved, this);
		replConnections = connectionsResolver.resolve(replConnectionsUnresolved, this);
		return subpattern!=null && replConnections!=null;
	}

	@Override
	protected boolean checkLocal() {
		Collection<RhsDeclNode> right = subpattern.type.right.getChildren();
		String patternName = subpattern.type.pattern.nameOfGraph;

		// check whether the used pattern contains one rhs
		if(right.size()!=1) {
			error.error(getCoords(), "No dependent replacement specified in \"" + patternName + "\" ");
			return false;
		}

		return checkSubpatternSignatureAdhered();
	}

	/** Check whether the subpattern replacement usage adheres to the signature of the subpattern replacement declaration */
	protected boolean checkSubpatternSignatureAdhered() {
		// check if the number of parameters is correct
		Collection<RhsDeclNode> right = subpattern.type.right.getChildren();
		String patternName = subpattern.type.pattern.nameOfGraph;
		Vector<DeclNode> formalReplacementParameters = right.iterator().next().graph.getParamDecls();
		Vector<ConstraintDeclNode> actualReplacementParameters = replConnections.children;
		int expected = formalReplacementParameters.size();
		int actual = actualReplacementParameters.size();
		if (expected != actual) {
			subpattern.ident.reportError("The dependent replacement specified in \"" + patternName + "\" needs "
			        + expected + " parameters, given by replacement usage " + subpattern.ident.toString() + " are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < formalReplacementParameters.size(); ++i) {
			ConstraintDeclNode actualParameter = actualReplacementParameters.get(i);
			ConstraintDeclNode formalParameter = (ConstraintDeclNode)formalReplacementParameters.get(i);
			InheritanceTypeNode actualParameterType = actualParameter.getDeclType();
			InheritanceTypeNode formalParameterType = formalParameter.getDeclType();
			
			if(!actualParameterType.isA(formalParameterType)) {
				res = false;
				actualParameter.ident.reportError("Subpattern replacement usage parameter \"" + actualParameter.ident.toString() + "\" has wrong type");
			}
		}

		return res;
	}

	@Override
	protected IR constructIR() {
		List<GraphEntity> replConnections = new LinkedList<GraphEntity>();
    	for (ConstraintDeclNode c : this.replConnections.getChildren()) {
    		replConnections.add((GraphEntity) c.checkIR(GraphEntity.class));
    	}
		return new SubpatternDependentReplacement("dependent replacement", subpatternUnresolved.getIdent(),
				(SubpatternUsage)subpattern.getIR(), replConnections);
	}
}

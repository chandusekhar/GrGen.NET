/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import java.awt.Color;

public class EdgeDeclNode extends ConstraintDeclNode implements EdgeCharacter {
	
	static {
		setName(EdgeDeclNode.class, "edge declaration");
	}
	
	private static final Resolver typeResolver =
		new DeclTypeResolver(EdgeTypeNode.class);
	
	public EdgeDeclNode(IdentNode n, BaseNode e, BaseNode constraints) {
		super(n, e, constraints);
		setName("edge");
		addResolver(TYPE, typeResolver);
	}
	
	protected boolean check() {
		return super.check()
			&& checkChild(TYPE, EdgeTypeNode.class);
	}
	
	/**
	 * Edges have more info to give
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	protected String extraNodeInfo() {
		return "";
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.YELLOW;
	}
	
	/**
	 * Get the ir object correctly casted.
	 * @return The edge ir object.
	 */
	public Edge getEdge() {
		return (Edge) checkIR(Edge.class);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This must be ok after checking all nodes.
		TypeNode tn = (TypeNode) getDeclType();
		EdgeType et = (EdgeType) tn.checkIR(EdgeType.class);
		IdentNode ident = getIdentNode();
		
		Edge edge = new Edge(ident.getIdent(), et, ident.getAttributes());
		edge.setConstraints(getConstraints());
		
		return edge;
	}
	
}

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
 * @author Sebastian Hack, Adam Szalkowski
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.NodeType;
import java.util.Iterator;

/**
 *
 */
public class NodeTypeChangeNode extends NodeDeclNode implements NodeCharacter {

	static {
		setName(NodeTypeChangeNode.class, "node type change decl");
	}

	private static final int OLD = CONSTRAINTS + 1;
	
	private static final Resolver nodeResolver =
		new DeclResolver(NodeDeclNode.class);
		
	private static final Resolver typeResolver =
		new DeclTypeResolver(NodeTypeNode.class);

  public NodeTypeChangeNode(IdentNode id, BaseNode newType, BaseNode oldid) {
  		
  	super(id, newType, TypeExprNode.getEmpty() );
	addChild(oldid);
  	addResolver(OLD, nodeResolver);
  }

  /**
   * @return the original node for this retyped node
   */
  public NodeDeclNode getOldNode()
  {
    return (NodeDeclNode) getChild(OLD);
  }
  
  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
    return super.check()
	&&  checkChild(OLD, NodeDeclNode.class);
  }
  
  public Node getNode() {
  	return (Node) checkIR(Node.class);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
        // This cast must be ok after checking.
  	NodeTypeNode newType = (NodeTypeNode) getChild(TYPE);
  	IdentNode nodeDecl = (IdentNode) getChild(IDENT);
  	NodeDeclNode oldNodeDecl = (NodeDeclNode) getChild(OLD);

	// This cast must be ok after checking.
	NodeTypeNode tn = (NodeTypeNode) getDeclType();
	NodeType nt = tn.getNodeType();
	IdentNode ident = getIdentNode();
		
	Node res = new Node(ident.getIdent(), nt, ident.getAttributes());

  	Node node = oldNodeDecl.getNode();
  	node.setRetypedNode(res);
	res.setOldNode(node);
	  
	return res;
  }

}


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
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.Table;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Node;


/**
 * A factory that can make the graph tables.
 */
public interface GraphTableFactory {

	NodeTable nodeTable(Node node);
	EdgeTable edgeTable(Edge edge);
	
  AttributeTable nodeAttrTable(Node node);
  AttributeTable edgeAttrTable(Edge edge);
	
  NodeTable originalNodeTable();
  EdgeTable originalEdgeTable();
  AttributeTable originalNodeAttrTable();
  AttributeTable originalEdgeAttrTable();
  
  NodeTable nodeTable(String alias);
  EdgeTable edgeTable(String alias);
  AttributeTable nodeAttrTable(String alias);
  AttributeTable edgeAttrTable(String alias);
	
	Table neutralTable();
	Table neutralTable(String alias);
}

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
 * PatternGraph.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;

/**
 * A pattern graph is a graph as it occurs in left hand rule
 * sides and negative parts. Additionally it can have
 * conditions referring to its items that restrict the set
 * of possible matchings.
 */
public class PatternGraph extends Graph {
	
	/** A list of all condition expressions. */
	private final List<Expression> conds = new LinkedList<Expression>();
	
	/** A list of all potentially homomorphic sets. */
	private final List<Collection> homs = new LinkedList<Collection>();
	
	/**
	 * Add a condition to the graph.
	 * @param expr The condition's expression.
	 */
	public void addCondition(Expression expr) {
		conds.add(expr);
	}

	/**
	 * Add a potentially homomorphic set to the graph.
	 * @param expr The condition's expression.
	 */
	public void addHomomorphic(Collection<Entity> hom) {
		homs.add(hom);
	}

	/**
	 * Get all conditions in this graph.
	 * @return A collection containing all conditions in this graph.
	 */
	public Collection<Expression> getConditions() {
		return Collections.unmodifiableCollection(conds);
	}

	/**
	 * Get all potentially homomorphic sets.
	 * @return A collection containing all conditions in this graph.
	 */
	public Collection<Collection> getHomomorphic() {
		return Collections.unmodifiableCollection(homs);
	}
	
	public Collection<Node> getHomomorphic(Node n) {
		for(Collection c : homs) {
			if (c.contains(n)) {
				return (Collection<Node>)c;
			}
		}

		Collection<Node> c = new LinkedList<Node>();
		c.add(n);
		return c;
	}

	public Collection<Edge> getHomomorphic(Edge e) {
		for(Collection c : homs) {
			if (c.contains(e)) {
				return (Collection<Edge>)c;
			}
		}
		
		Collection<Edge> c = new LinkedList<Edge>();
		c.add(e);
		return c;
	}
	
	public boolean isHomomorphic(Node n1, Node n2) {
		return getHomomorphic(n1).contains(n2);
	}

	public boolean isHomomorphic(Edge e1, Edge e2) {
		return getHomomorphic(e1).contains(e2);
	}

}


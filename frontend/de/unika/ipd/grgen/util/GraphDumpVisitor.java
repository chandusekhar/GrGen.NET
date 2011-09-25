/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @file GraphDumpVisitor.java
 * @author shack
 * @date Jul 21, 2003
 */
package de.unika.ipd.grgen.util;

/**
 * A visitor that dumps graphs
 * Every object that is visited must implement Walkable and GraphDumpable
 * @see GraphDumpable
 * @see Walkable
 */
public class GraphDumpVisitor extends Base implements Visitor
{
	protected GraphDumper dumper;

	public GraphDumpVisitor(GraphDumper dumper)
	{
		this.dumper = dumper;
	}

	public GraphDumpVisitor()
	{
	}

	public void setDumper(GraphDumper dumper)
	{
		this.dumper = dumper;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.Visitor#visit(de.unika.ipd.grgen.ast.BaseNode)
	 */
	public void visit(Walkable n)
	{
		GraphDumpable gd = (GraphDumpable) n;
		dumper.node(gd);

		int i = 0;
		for (GraphDumpable target : n.getWalkableChildren()) {
			dumper.edge(gd, target, gd.getEdgeLabel(i));
			i++;
		}
	}
}
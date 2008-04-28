/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A walker calling a visitor after ascending from the last child
 */
public class PostWalker extends PrePostWalker
{
	/**
	 * Make a new post walker.
	 * @param post The visitor to use after ascending from the last child of a node
	 */
	public PostWalker(Visitor post)
	{
		super(
			new Visitor()
			{
				public void visit(Walkable w)
				{
				}
			},
			post
		);
	}
}

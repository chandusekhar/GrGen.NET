/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universit�t Karlsruhe, Institut f�r Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Vector;


/**
 * Represents an alternative statement in the IR.
 */
public class Alternative extends IR {
	public Alternative() {
		super("alternative");
	}

	Vector<Rule> alternativeCases = new Vector<Rule>();

	public Collection<Rule> getAlternativeCases() {
		return alternativeCases;
	}

	public void addAlternativeCase(Rule alternativeCaseRule)
	{
		alternativeCases.add(alternativeCaseRule);
	}
}

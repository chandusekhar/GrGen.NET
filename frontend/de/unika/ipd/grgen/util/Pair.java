/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Pair.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

public class Pair<T,S>
{
	public T first;
	public S second;

	public Pair() {
		first = null;
		second = null;
	}

	public Pair(T f, S s) {
		first = f;
		second = s;
	}
}


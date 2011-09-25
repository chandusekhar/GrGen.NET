/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Created on Apr 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;


/**
 * Default annotations implementation.
 */
public class DefaultAnnotations implements Annotations {

	private final Map<String, Object> annots = new HashMap<String, Object>();

	/** @see de.unika.ipd.grgen.util.Annotations#containsKey(java.lang.String) */
	public boolean containsKey(String key) {
		return annots.containsKey(key);
	}

	/** @see de.unika.ipd.grgen.util.Annotations#get(java.lang.String) */
	public Object get(String key) {
		return annots.get(key);
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isBoolean(java.lang.String) */
	public boolean isBoolean(String key) {
		return containsKey(key) && get(key) instanceof Boolean;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isInteger(java.lang.String) */
	public boolean isInteger(String key) {
		return containsKey(key) && get(key) instanceof Integer;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isString(java.lang.String) */
	public boolean isString(String key) {
		return containsKey(key) && get(key) instanceof String;
	}

	public boolean isFlagSet(String key) {
		if(!containsKey(key)) return false;
		Object val = get(key);
		return val instanceof Boolean && ((Boolean) val).booleanValue();
	}

	/** @see de.unika.ipd.grgen.util.Annotations#put(java.lang.String, java.lang.Object) */
	public void put(String key, Object value) {
		annots.put(key, value);
	}

	public Set<String> keySet() {
		return annots.keySet();
	}
}
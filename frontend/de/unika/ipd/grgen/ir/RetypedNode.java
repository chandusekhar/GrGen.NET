/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.Retyped;

public class RetypedNode extends Node implements Retyped {

	/**  The original entity if this is a retyped entity */
	protected Node oldNode = null;

	public RetypedNode(Ident ident, NodeType type, Annotations annots,
			boolean maybeDeleted, boolean maybeRetyped, boolean isDefToBeYieldedTo, int context) {
		super(ident, type, annots, null, maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context);
	}

	public Entity getOldEntity() {
		return oldNode;
	}

	public void setOldEntity(Entity old) {
		this.oldNode = (Node)old;
	}

	/** returns the original node in the graph. */
	public Node getOldNode() {
		return oldNode;
	}

	/** Set the old node being retyped to this one */
	public void setOldNode(Node old) {
		this.oldNode = old;
	}

	public boolean changesType() {
		return false;
	}

	public boolean isRetyped() {
		return true;
	}
}

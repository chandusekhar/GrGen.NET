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
 * ConstraintDeclNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.TypeExpr;

public abstract class ConstraintDeclNode extends DeclNode
{
	TypeExprNode constraints;
	
	int declLocation; // one of the values below
	public static final int DECL_IN_PATTERN = 0;
	public static final int DECL_IN_REPLACEMENT = 1;
	
	ConstraintDeclNode(IdentNode id, BaseNode type, int declLocation, TypeExprNode constraints) {
		super(id, type);
		this.constraints = constraints;
		becomeParent(this.constraints);
		this.declLocation = declLocation;
	}
			
	protected boolean checkLocal() {
		return (new SimpleChecker(TypeExprNode.class)).check(constraints, error)
			& onlyPatternElementsAreAllowedToBeConstrained();
	}
	
	protected boolean onlyPatternElementsAreAllowedToBeConstrained() {
		if(constraints!=TypeExprNode.getEmpty()) {
			if(declLocation!=DECL_IN_PATTERN) {
				constraints.reportError("replacement elements are not allowed to be type constrained, only pattern elements are"); 
				return false;
			}
		}
		return true;
	}
	
	protected final TypeExpr getConstraints() {
		return (TypeExpr) constraints.checkIR(TypeExpr.class);
	}
}


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Action;

/**
 * Base class for all action types (tests, rules)
 */
public abstract class ActionDeclNode extends DeclNode {
    
    public ActionDeclNode(IdentNode id, TypeNode type) {
        super(id, type);
    }
    
    /**
     * Get the IR object for this action node.
     * The IR object is instance of Action here.
     * @see de.unika.ipd.grgen.ir.Action
     * @return The IR object.
     */
    public Action getAction() {
        return (Action) getIR();
    }
    
}

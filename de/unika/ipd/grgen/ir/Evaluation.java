/**
 * Evaluation.java
 *
 * @author Rubino Geiss, Michael Beck
 */

package de.unika.ipd.grgen.ir;
import java.util.LinkedList;
import java.util.Iterator;



public class Evaluation extends IR
{
	/**
	 * The evaluations constituing a Evaluation of Rule.
	 * They are orgnized in a list, since their order is vital.
	 * Applying them in a random order will lead to different results.
	 */
	private LinkedList evaluations = new LinkedList();
	
	/**
	 * Constructor
	 *
	 */
	Evaluation()
	{
		super("eval");
	}
	
	/**
	 * Method add adds an element to the list of evaluations.
	 *
	 * @param    aeval               an IR
	 */
	public void add(IR aeval)
	{
		evaluations.add(aeval);
	}
	
	/**
	 * Method iterator returns a Iterator of its elements.
	 *
	 * @return   an Iterator
	 */
	public Iterator iterator()
	{
		return evaluations.iterator();
	}
}


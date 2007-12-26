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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.*;

/**
 * An class representing a node or an edge.
 */
public abstract class CompoundType extends Type {
	
	/**
	 * Collection containing all members defined in that type.
	 */
	private List<Entity> members = new LinkedList<Entity>();
	
	/**
	 * Make a new compound type.
	 * @param name The name of the type.
	 * @param ident The identifier used to declare this type.
	 */
	public CompoundType(String name, Ident ident) {
		super(name, ident);
	}
	
	/**
	 * Get all members of this compund type.
	 * @return An iterator with all members.
	 */
	public Collection<Entity> getMembers() {
		return Collections.unmodifiableCollection(members);
	}
	
	/**
	 * Add a member to the compound type.
	 * @param member The entity to add.
	 */
	public void addMember(Entity member) {
		members.add(member);
		member.setOwner(this);
	}
	
	protected void canonicalizeLocal() {
		Collections.sort(members, Identifiable.COMPARATOR);
	}
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("members", members.iterator());
	}
	
	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');
		
		int i = 0;
		for(Iterator<Entity> it = members.iterator(); it.hasNext(); i++) {
			Entity ent = it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}
		
		sb.append(']');
	}
	
}

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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.awt.Color;
import java.io.PrintStream;
import java.util.HashMap;

/**
 * A VCG Graph dumper
 */
public class VCGDumper implements GraphDumper {
	
	/** where to put the graph to */
	private PrintStream ps;
	
	/** Index in the vcg colormap for user defined colors */
	private int currSetColor;
	
	/** Prefix for the nodes. */
	private static String prefix = "n";
	
	private static HashMap<Color, String> colorMap;
	private static HashMap<Integer, String> shapeMap;
	private static HashMap<Integer, String> lineStyleMap;
	
	static {
		colorMap = new HashMap<Color, String>();
		shapeMap = new HashMap<Integer, String>();
		lineStyleMap = new HashMap<Integer, String>();
		
		colorMap.put(Color.BLACK, "black");
		colorMap.put(Color.BLUE, "lightblue");
		colorMap.put(Color.CYAN, "cyan");
		colorMap.put(Color.GRAY, "lightgray");
		colorMap.put(Color.DARK_GRAY, "darkgray");
		colorMap.put(Color.MAGENTA, "magenta");
		colorMap.put(Color.ORANGE, "orange");
		colorMap.put(Color.GREEN, "green");
		colorMap.put(Color.RED, "red");
		colorMap.put(Color.PINK, "pink");
		colorMap.put(Color.YELLOW, "yellow");
		colorMap.put(Color.WHITE, "white");
		
		shapeMap.put(new Integer(BOX), "box");
		shapeMap.put(new Integer(RHOMB), "rhomb");
		shapeMap.put(new Integer(ELLIPSE), "ellipse");
		shapeMap.put(new Integer(TRIANGLE), "triangle");
		
		lineStyleMap.put(new Integer(SOLID), "continuous");
		lineStyleMap.put(new Integer(DASHED), "dashed");
		lineStyleMap.put(new Integer(DOTTED), "dotted");
	}
	
	/**
	 * Make a string usable for output.
	 * This escapes anything that has to be escaped.
	 * @param s The input string.
	 * @return A string ready for dumping.
	 */
	private static String escapeString(String s) {
		return s.replaceAll("\"", "\\\\\"");
	}
	
	/**
	 * Make a new VCG dumper.
	 * @param ps The print stream to dump the graph to.
	 */
	public VCGDumper(PrintStream ps) {
		this.ps = ps;
		this.currSetColor = 32;
	}
	
	/**
	 * Dump graph preamble.
	 */
	public void begin() {
		ps.println("graph:{\nlate_edge_labels:yes\ndisplay_edge_labels:yes\n"
								 + "manhattan_edges:yes\nport_sharing:no\n");
	}
	
	/**
	 * Dump epilog.
	 * @see de.unika.ipd.grgen.util.GraphDumper#finish()
	 */
	public void finish() {
		ps.println("}");
		ps.flush();
		ps.close();
	}
	
	/**
	 * Get a VCG color for a Java color.
	 * @param col The Java color.
	 * @return The VCG color.
	 */
	private String getColor(Color col) {
		String res;
		
		if(colorMap.containsKey(col))
			res = colorMap.get(col);
		else if(currSetColor < 256) {
			// Get the current index and increment it
			int index = currSetColor++;
			
			// Convert it to a string and put in the color map
			res = String.valueOf(index);
			colorMap.put(col, res);
			
			// issue a vcg colormap statement
			ps.println("colorentry " + index + ": " +
									 col.getRed() + " " + col.getGreen() + " " + col.getBlue());
		}
		else
			res = "white";
		
		return res;
	}
	
	private String getPrefix() {
		return prefix;
	}
	
	/**
	 * Make a VCG string from the node's attributes.
	 * @param d The node to dump.
	 * @return VCG statements describing the node.
	 */
	private String getNodeAttributes(GraphDumpable d) {
		String col = getColor(d.getNodeColor());
		Integer shp = new Integer(d.getNodeShape());
		
		String info = d.getNodeInfo();
		if(info != null)
			info = escapeString(info);
		
		String label = escapeString(d.getNodeLabel());
		
		String s = "title:\"" + getPrefix() + d.getNodeId()
			+ "\" label:\"" + label + "\"";
		
		if(info != null)
			s += " info1:\"" + info + "\"";
		s += " color:" + col;
		if(shapeMap.containsKey(shp))
			s += " shape:" + shapeMap.get(shp);
		
		return s;
	}
	
	public void node(GraphDumpable d) {
		ps.println("node:{" + getNodeAttributes(d) + "}");
	}
	
	public void edge(GraphDumpable from, GraphDumpable to, String label,
									 int style, Color color) {
		
		String col = getColor(color);
		
		String s = "edge:{sourcename:\"" + getPrefix() + from.getNodeId()
			+ "\" targetname:\"" + getPrefix() + to.getNodeId() + "\"";
		
		if(label != null)
			s += " label:\"" + escapeString(label) + "\"";
		
		s += " color:" + col;
		
		if(style != DEFAULT)
			s += " linestyle:" + lineStyleMap.get(new Integer(style));
		
		s += "}";
		
		ps.println(s);
	}
	
	public void edge(GraphDumpable from, GraphDumpable to, String label, int style) {
		edge(from, to, label, style, Color.BLACK);
	}
	
	public void edge(GraphDumpable from, GraphDumpable to, String label) {
		edge(from, to, label, DEFAULT, Color.BLACK);
	}
	
	public void edge(GraphDumpable from, GraphDumpable to) {
		edge(from, to, null, DEFAULT, Color.BLACK);
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumper#beginSubgraph(java.lang.String)
	 */
	public void beginSubgraph(GraphDumpable d) {
		ps.println("graph:{" + getNodeAttributes(d)
								 + " status:clustered");
	}
	
	public void beginSubgraph(String name) {
		ps.print("graph:{title:\"");
		ps.print(name);
		ps.println('\"');
		ps.println("  status:clustered");
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumper#endSubgraph()
	 */
	public void endSubgraph() {
		ps.println("}\n");
	}
	
}

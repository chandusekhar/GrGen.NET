/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An single precision floating point constant.
 */
public class FloatConstNode extends ConstNode
{
	public FloatConstNode(Coords coords, double v) {
		super(coords, "float", new Float(v));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.floatType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		Float value = (Float) getValue();

		if (type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)(float)value);
		} else if (type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)(float)value);
		} else if (type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), (int)(float)value);
		} else if (type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), (long)(float)value);
		} else if (type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else throw new UnsupportedOperationException();
	}
}
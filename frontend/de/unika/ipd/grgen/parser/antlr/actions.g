header {
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
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski
 * @version $Id$
 */
	package de.unika.ipd.grgen.parser.antlr;

	import java.util.Iterator;
	import java.util.List;
	import java.util.LinkedList;
	import java.util.Map;
	import java.util.HashMap;
	import java.util.Collection;
	import java.io.DataInputStream;
	import java.io.FileInputStream;
	import java.io.FileNotFoundException;
	import java.io.File;
		
	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.report.*;
	import de.unika.ipd.grgen.util.*;
	import de.unika.ipd.grgen.Main;
	
	import antlr.*;
}


/**
 * GRGen grammar
 * @version 0.1
 * @author Sebastian Hack
 */
class GRActionsParser extends GRBaseParser;

options {
  k=3;
	codeGenMakeSwitchThreshold = 2;
	codeGenBitsetTestThreshold = 3;
	defaultErrorHandler = true;
	buildAST = false;
	importVocab = GRBase;
}

/**
 * Build a main node.
 * It has a collect node with the decls as child
 */
text returns [ BaseNode main = env.initNode() ]
    {
  		CollectNode actions;
  		CollectNode mainChilds = new CollectNode();
  		CollectNode modelChilds = new CollectNode();
  		IdentNode id;
  		List modelList = new LinkedList();

	    String actionsName = Util.getActionsNameFromFilename(getFilename());

        id = new IdentNode(
    		env.define(ParserEnvironment.ENTITIES, actionsName,
      			new de.unika.ipd.grgen.parser.Coords(0, 0, getFilename())));

  	    modelChilds.addChild(env.getStdModel());
  	}
  	: (
  	    ( a:ACTIONS i:IDENT {
  	        reportWarning(getCoords(a), "keyword \"actions\" is deprecated");
  	  	    reportWarning(getCoords(i),
  	      	    "the name of this actions component is not set by the identifier " +
  	  		    "after the \"actions\" keyword anymore but derived from the filename");
	      }
	      (
	        usingDecl[modelChilds]
	      | SEMI
	      )
	    )
	    | usingDecl[modelChilds]
  	  )?

  	  ( actions=actionDecls EOF { mainChilds.addChildren(actions); } )?
      {
  	  	main = new UnitNode(id, getFilename());
  	  	main.addChild(modelChilds);
  	  	main.addChild(mainChilds);
    	
      	env.getCurrScope().leaveScope();
   	  }
  	;
  	
identList [ Collection strings ]
  : fid:IDENT { strings.add(fid.getText()); }
    (COMMA sid:IDENT { strings.add(sid.getText()); })*
  ;

usingDecl [ CollectNode modelChilds ]
  { List modelList = new LinkedList(); }
  : u:USING identList[modelList]
    SEMI {
  	  for(Iterator it = modelList.iterator(); it.hasNext();) {
  	    String modelName = (String) it.next();
  	    File modelFile = env.findModel(modelName);

  	    if ( modelFile == null )
  	      reportError(getCoords(u), "model \"" + modelName + "\" could not be found");
  	    else {
		  BaseNode model;
		  model = env.parseModel(modelFile);
		  modelChilds.addChild(model);
        }
  	  }
  	}
  ;
  	
actionDecls returns [ CollectNode c = new CollectNode() ]
  { BaseNode d; }
  : ( d=actionDecl { c.addChild(d); } )+;

actionDecl returns [ BaseNode res = env.initNode() ]
  : res=testDecl
  | res=ruleDecl
  ;

testDecl returns [ BaseNode res = env.initNode() ]
    {
  		IdentNode id;
  		BaseNode tb, pattern;
  		CollectNode params, ret, negs = new CollectNode();
  	}
  	: TEST id=actionIdentDecl pushScope[id] params=parameters ret=returnTypes LBRACE!
  	  pattern=patternPart[negs]
  	  {
      	id.setDecl(new TestDeclNode(id, pattern, negs, params, ret));
      	res = id;
  	  } RBRACE! popScope!;

ruleDecl returns [ BaseNode res = env.initNode() ]
  {
  		IdentNode id;
  		BaseNode rb, left, right;
  		CollectNode params, ret;

  		CollectNode negs = new CollectNode();
  		CollectNode eval = new CollectNode();
  		CollectNode dels = new CollectNode();
  }
  : RULE id=actionIdentDecl pushScope[id] params=parameters ret=returnTypes LBRACE!
  	left=patternPart[negs]
  	(right=replacePart[eval]
  	{
		id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params, ret));
		res = id;
    }|right=modifyPart[eval,dels]
  	{
  	   id.setDecl(new ModifyRuleDeclNode(id, left, right, negs, eval, params, ret, dels));
  	   res = id;
    })
    RBRACE! popScope!
  ;

parameters returns [ CollectNode res = new CollectNode() ]
	: LPAREN (paramList[res])? RPAREN
	|
	;

paramList [ CollectNode params ]
  {
  	BaseNode p;
  }
	: p=param { params.addChild(p); } ( COMMA p=param { params.addChild(p); } )*
	;
	
param returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id;
		BaseNode type;
	}
	: MINUS res=patEdgeDecl RARROW
	| res=patNodeDecl
//	: id=entIdentDecl COLON type=typeIdentUse
//	  { res = new ParamDeclNode(id, type); }
	;

returnTypes returns [ CollectNode res = new CollectNode() ]
    {
    	BaseNode type;
    }
	: COLON LPAREN type=typeIdentUse { res.addChild(type); }
      (COMMA type=typeIdentUse { res.addChild(type); })* RPAREN
	| COLON LPAREN RPAREN
	|
	;

patternPart [ CollectNode negs ] returns [ BaseNode res = env.initNode() ]
	{ int mod=0; }
  : mod=patternModifiers p:PATTERN LBRACE! res=patternBody[getCoords(p), negs, mod] RBRACE!
  ;
  
replacePart [ CollectNode eval ] returns [ BaseNode res = env.initNode() ]
	: r:REPLACE LBRACE! res=replaceBody[getCoords(r), eval] RBRACE!
	;

modifyPart [ CollectNode eval, CollectNode dels ] returns [ BaseNode res = env.initNode() ]
	: r:MODIFY LBRACE! res=modifyBody[getCoords(r), eval, dels] RBRACE!
	;

evalPart [ BaseNode n ]
	: e:EVAL LBRACE evalBody[n] RBRACE
	;
	
evalBody [ BaseNode n  ]
	{ BaseNode a; }
	: (
		a=assignment { n.addChild(a); } SEMI
	  )*
	;
	
patternModifiers returns [ int res = 0 ]{ int mod = 0; }
	:(mod=patternModifier { res |= mod; })*
	;

patternModifier returns [ int res = 0 ]
	: INDUCED {res |= PatternGraphNode.MOD_INDUCED;}
	| DPO {res |= PatternGraphNode.MOD_DPO;}
	;

patternBody [ Coords coords, CollectNode negs, int mod ] returns [ BaseNode res = env.initNode() ]
  {
		BaseNode s;
 		CollectNode connections = new CollectNode();
 		CollectNode conditions = new CollectNode();
 		CollectNode returnz = new CollectNode();
 		CollectNode homs = new CollectNode();
 		res = new PatternGraphNode(coords, connections, conditions, returnz, homs, mod);
 		
		int negCounter = 0;
  }
  // TODO: where to get coordinates from for the statement???
  : (negCounter = patternStmt[connections, conditions, negs, negCounter, returnz, homs])*
  ;

patternStmt [ CollectNode conn, CollectNode cond,
  CollectNode negs, int negCount, CollectNode returnz, CollectNode homs ] returns [ int newNegCount ]
  
	{
		int mod = 0;
  		IdentNode id = env.getDummyIdent();
    	BaseNode e, neg, hom;
    	//nesting of negative Parts is not allowed.
    	CollectNode negsInNegs = new CollectNode();

    	newNegCount = negCount;
	}
	: patConnections[conn] SEMI
	
	// TODO: insert mod=patternModifiers iff nesting of negative parts is allowed
	| p:NEGATIVE pushScopeStr[ "neg" + negCount ] LBRACE!
	  neg=patternBody[getCoords(p), negsInNegs, mod] {
	    newNegCount = negCount + 1;
	    negs.addChild(neg);
	  } RBRACE! popScope! {
	    if(negsInNegs.children() != 0)
	      reportError(getCoords(p), "Nesting of negative parts not allowed");
	  }
	| COND e=expr[false] //'false' means that expr is not an enum item initializer
	  { cond.addChild(e); } SEMI
	| COND LBRACE (
		e=expr[false] //'false' means that expr is not an enum item initializer
		SEMI { cond.addChild(e); }
	  )* RBRACE
	| replaceReturns[returnz] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	;

patConnections [ CollectNode conn ]
  {
    BaseNode n,e;
    boolean forward = true;
    NodeDeclNode dummyNode = env.getDummyNodeDecl();
  }
  : ( e=patForwardEdgeOcc { forward=true; } | e=patBackwardEdgeOcc { forward=false; } )
    (
        n=patNodeContinuation[conn] {
        	/* the edge declared by <code>e</code> dangles on the left */
        	if (forward)
        		conn.addChild(new ConnectionNode(dummyNode, e, n));
        	else
        		conn.addChild(new ConnectionNode(n, e, dummyNode));
        }
      | /* both target and source of the edge <code>e</code> dangle */ {
    		conn.addChild(new ConnectionNode(dummyNode, e, dummyNode));
        }
    )
  | n=patNodeOcc ( patEdgeContinuation[n, conn] | {
    	conn.addChild(new SingleNodeConnNode(n));
    })
  ;

patNodeContinuation [ CollectNode collect ] returns [ BaseNode res = env.initNode() ]
  { BaseNode n; }
  : res=patNodeOcc ( patEdgeContinuation[res, collect] )?
  ;

patEdgeContinuation [ BaseNode left, CollectNode collect ]
  {
    BaseNode n,e;
	boolean forward = true;
  }
  : ( e=patForwardEdgeOcc { forward=true; } | e=patBackwardEdgeOcc { forward=false; } )
    (
    	  n=patNodeContinuation[collect] {
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, n));
    	  		else
    	  			collect.addChild(new ConnectionNode(n, e, left));
    	  }
    	| /* the edge declared by <code>res</code> dangles on the right */ {
			    NodeDeclNode dummyNode = env.getDummyNodeDecl();
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, dummyNode));
    	  		else
    	  			collect.addChild(new ConnectionNode(dummyNode, e, left));
    	  }
    )
  ;

patNodeOcc returns [ BaseNode res = env.initNode() ]
  : res=patAnonNodeOcc
  | res=patKnownNodeOcc
  ;

patAnonNodeOcc returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id = env.getDummyIdent();
  	BaseNode type = env.getNodeRoot();
	BaseNode constr = TypeExprNode.getEmpty();
	Attributes attrs = env.getEmptyAttributes();
	boolean hasAttrs = false;
  }
  :
    (
        d:DOT {
	    	id = env.defineAnonymousEntity("node", getCoords(d));
	    	res = new NodeDeclNode(id, type, constr);
	    }
        ( attrs=attributes { id.setAttributes(attrs); } )?
      | ( attrs=attributes { hasAttrs = true; })?
        c:COLON (type=typeIdentUse (constr=typeConstraint)?| TYPEOF LPAREN type=entIdentUse RPAREN) {
			id = env.defineAnonymousEntity("node", getCoords(c));
			if (hasAttrs) id.setAttributes(attrs);
			res = new NodeDeclNode(id, type, constr);
	    }
    )
  ;

patKnownNodeOcc returns [ BaseNode res = env.initNode() ]
  : res = entIdentUse
  | res = patNodeDecl
  ;

patNodeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id, type;
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : id=entIdentDecl COLON
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (constr=typeConstraint)? {
    	res = new NodeDeclNode(id, type, constr);
    }
  ;

patForwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  {
    BaseNode type = env.getEdgeRoot();
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : MINUS res=patEdgeDecl RARROW
  | MINUS res=entIdentUse RARROW
  | mm:DOUBLE_RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
		res = new EdgeDeclNode(id, type, constr);
    }
  ;

patBackwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  {
    BaseNode type = env.getEdgeRoot();
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : LARROW res=patEdgeDecl MINUS
  | LARROW res=entIdentUse MINUS
  | mm:DOUBLE_LARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
		res = new EdgeDeclNode(id, type, constr);
    }
  ;

patEdgeDecl returns [ BaseNode res = env.initNode() ]
  {
  	BaseNode type = env.getEdgeRoot();
    IdentNode id = env.getDummyIdent();
    BaseNode constr = TypeExprNode.getEmpty();
    Attributes attrs = env.getEmptyAttributes();
    Pair<DefaultAttributes, de.unika.ipd.grgen.parser.Coords> atCo;
  }
  : (
        id=entIdentDecl
        ( attrs=attributes { id.setAttributes(attrs); } )?
        COLON
        ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
        ( constr=typeConstraint )?
      |
        atCo=attributesWithCoords
        (
            c:COLON
            ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
            ( constr=typeConstraint )?
            { id = env.defineAnonymousEntity("edge", getCoords(c)); }
          |
            { id = env.defineAnonymousEntity("edge", atCo.second); }
        )
        { id.setAttributes(atCo.first); }
      |
        cc:COLON
        ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
        ( constr=typeConstraint )?
        { id = env.defineAnonymousEntity("edge", getCoords(cc)); }
        
    )
    { res = new EdgeDeclNode(id, type, constr); }
  ;

/**
 * A statement defining some nodes/edges to be mactched potentially
 * homomorphically
 */
homStatement returns [ BaseNode res = env.initNode() ]
    {
    	BaseNode id;
    }
	: h:HOM {res = new HomNode(getCoords(h)); } LPAREN id=entIdentUse { res.addChild(id); }
      (COMMA id=entIdentUse { res.addChild(id); })* RPAREN
	;

replaceBody [ Coords coords, CollectNode eval ] returns [ BaseNode res = env.initNode() ]
    {
  		CollectNode connections = new CollectNode();
  		CollectNode returnz = new CollectNode();
  	  	res = new GraphNode(coords, connections, returnz);
    }
    // TODO: where to get coordinates from for the statement???
    : ( replaceStmt[coords, connections, returnz, eval] )*
  ;

replaceStmt [ Coords coords, CollectNode connections, CollectNode returnz, CollectNode eval ]
	{ BaseNode n; }
	: replConnections[connections] SEMI
	| replaceReturns[returnz] SEMI
    | evalPart[eval]
	;

modifyBody [ Coords coords, CollectNode eval, CollectNode dels ] returns [ BaseNode res = env.initNode() ]
    {
  		CollectNode connections = new CollectNode();
  		CollectNode returnz = new CollectNode();
  	  	res = new GraphNode(coords, connections, returnz);
    }
    // TODO: where to get coordinates from for the statement???
    : ( modifyStmt[coords, connections, returnz, eval, dels] )*
  ;

modifyStmt [ Coords coords, CollectNode connections, CollectNode returnz, CollectNode eval, CollectNode dels ]
	{ BaseNode n; }
	: replConnections[connections] SEMI
	| replaceReturns[returnz] SEMI
    | deleteStmt[dels] SEMI
    | evalPart[eval]
	;

replConnections [ CollectNode conn ]
  {
    BaseNode n,e;
    boolean forward = true;
    NodeDeclNode dummyNode = env.getDummyNodeDecl();
  }
  : (
        e=replForwardEdgeOcc { forward=true; }
      | e=replBackwardEdgeOcc { forward=false; }
    )
    {
    	BaseNode x = e;
    	if (e instanceof DeclNode) x = ((DeclNode) e).getIdentNode();

    	if (! x.isKept() ) reportError(e.getCoords(),
    		"dangling edges in replace/modify part must already " +
    		"occur in the pattern part");
    }
    (
        n=replNodeContinuation[conn] {
        	/* the edge declared by <code>e</code> dangles on the left */
        	if (forward)
        		conn.addChild(new ConnectionNode(dummyNode, e, n));
        	else
        		conn.addChild(new ConnectionNode(n, e, dummyNode));
        }
      | /* the edge declared by <code>e</code> dangles on both sides */ {
    		conn.addChild(new ConnectionNode(dummyNode, e, dummyNode));
        }
    )
  | n=replNodeOcc ( replEdgeContinuation[n, conn] | {
    	conn.addChild(new SingleNodeConnNode(n));
    })
  ;

replNodeContinuation [ CollectNode collect ] returns [ BaseNode res = env.initNode() ]
  { BaseNode n; }
  : res=replNodeOcc ( replEdgeContinuation[res, collect] )?
  ;

replEdgeContinuation [ BaseNode left, CollectNode collect ]
  {
    BaseNode n,e;
	boolean forward = true;
  }
  : (
        e=replForwardEdgeOcc { forward=true; }
      | e=replBackwardEdgeOcc { forward=false; }
    )
    (
    	  n=replNodeContinuation[collect] {
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, n));
    	  		else
    	  			collect.addChild(new ConnectionNode(n, e, left));
    	  }
    	| /* the edge declared by <code>res</code> dangles on the right */ {

    			NodeDeclNode dummyNode = env.getDummyNodeDecl();
    			
    			BaseNode x = e;
    			if (e instanceof DeclNode) x = ((DeclNode) e).getIdentNode();

    			if (! x.isKept() ) reportError(e.getCoords(),
    				"dangling edges in replace/modify part must already " +
    				"occur in the pattern part");
				
     			if (forward)
    				collect.addChild(new ConnectionNode(left, e, dummyNode));
    			else
    				collect.addChild(new ConnectionNode(dummyNode, e, left));
    	}
    )
  ;

replNodeOcc returns [ BaseNode res = env.initNode() ]
  : res=replAnonNodeOcc
  | res=replKnownNodeOcc
  ;

replAnonNodeOcc returns [ BaseNode res = env.initNode() ]
  {
    BaseNode type = env.getNodeRoot();
    IdentNode id = env.getDummyIdent();
    IdentNode oldid = null;
  }
  : d:DOT {
		id = env.defineAnonymousEntity("node", getCoords(d));
		res = new NodeDeclNode(id, type);
    }
  | c:COLON (type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN) {
		id = env.defineAnonymousEntity("node", getCoords(c));
	}
    (LT oldid=entIdentUse GT)? {
    	if(oldid==null) {
    		res = new NodeDeclNode(id, type);
    	} else {
    		res = new NodeTypeChangeNode(id, type, oldid);
    	}
	}
  ;

replKnownNodeOcc returns [ BaseNode res = env.initNode() ]
  : res = entIdentUse
  | res = replNodeDecl
  ;

replNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, oldid=null;
		BaseNode type;
	}
  : id=entIdentDecl COLON
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (LT oldid=entIdentUse GT)? {
       if(oldid==null) {
           res = new NodeDeclNode(id, type);
       } else {
           res = new NodeTypeChangeNode(id, type, oldid);
       }
	}
	;

replForwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  { BaseNode type = env.getEdgeRoot(); }
  : MINUS res=entIdentUse RARROW { res.setKept(true); }
  | MINUS res=replEdgeDecl RARROW
  | mm:DOUBLE_RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
		res = new EdgeDeclNode(id, type);
    }
  ;

replBackwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  { BaseNode type = env.getEdgeRoot(); }
  : LARROW res=entIdentUse MINUS { res.setKept(true); }
  | LARROW res=replEdgeDecl MINUS
  | mm:DOUBLE_LARROW {
    	IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
    	res = new EdgeDeclNode(id, type);
    }
  ;

replEdgeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id = env.getDummyIdent(), type, oldid = null;
    boolean anonymous = false;
  }
  : ( id=entIdentDecl | { anonymous = true; } )
    d:COLON {
    	if (anonymous) id = env.defineAnonymousEntity("edge", getCoords(d));
    }
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (LT oldid=entIdentUse GT {id.setKept(true);} )?
    {
       if( oldid == null )
           res = new EdgeDeclNode(id, type);
       else
           res = new EdgeTypeChangeNode(id, type, oldid);
	}
  ;

replaceReturns[CollectNode res]
    {
    	BaseNode id;
    	boolean multipleReturns = ! res.getChildren().isEmpty();
    }
	: r:RETURN {
	  		if ( multipleReturns )
	  			reportError(getCoords(r), "multiple occurence of return statement in one rule");
	  }
	  LPAREN id=entIdentUse { if ( !multipleReturns ) res.addChild(id); }
      (COMMA id=entIdentUse { if ( !multipleReturns ) res.addChild(id); })*
      RPAREN
      { res.setCoords(getCoords(r)); }
	;

deleteStmt[CollectNode res]
    {
    	BaseNode id;
    }
	: DELETE LPAREN id=entIdentUse { res.addChild(id); }
      (COMMA id=entIdentUse { res.addChild(id); })* RPAREN
	;

typeConstraint returns [ BaseNode constr = env.initNode() ]
  : BACKSLASH constr=typeUnaryExpr
  ;

typeAddExpr returns [ BaseNode res = env.initNode() ]
  {
    BaseNode op;
  }
  : res=typeIdentUse { res = new TypeConstraintNode(res); }
    (t:PLUS op=typeUnaryExpr {
      res = new TypeExprNode(getCoords(t), TypeExprNode.UNION, res, op);
    })*
  ;

typeUnaryExpr returns [ BaseNode res = env.initNode() ]
  : res=typeIdentUse { res = new TypeConstraintNode(res); }
  | LPAREN res=typeAddExpr RPAREN
  ;








using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_Recursive;

namespace de.unika.ipd.grGen.Action_Recursive
{
	public class Pattern_Blowball : LGSPRulePattern
	{
		private static Pattern_Blowball instance = null;
		public static Pattern_Blowball Instance { get { if (instance==null) { instance = new Pattern_Blowball(); instance.initialize(); } return instance; } }

		public static NodeType[] Blowball_node_head_AllowedTypes = null;
		public static bool[] Blowball_node_head_IsAllowedType = null;
		public enum Blowball_NodeNums { @head, };
		public enum Blowball_EdgeNums { };
		public enum Blowball_SubNums { };
		public enum Blowball_AltNums { @alt_0, };
		public enum Blowball_alt_0_CaseNums { @end, @further, };
		public enum Blowball_alt_0_end_NodeNums { @head, };
		public enum Blowball_alt_0_end_EdgeNums { };
		public enum Blowball_alt_0_end_SubNums { };
		public enum Blowball_alt_0_end_AltNums { };
		public static NodeType[] Blowball_alt_0_end_neg_0_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_end_neg_0_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_end_neg_0_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_end_neg_0_SubNums { };
		public enum Blowball_alt_0_end_neg_0_AltNums { };
		public static NodeType[] Blowball_alt_0_further_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_node__node0_IsAllowedType = null;
		public static EdgeType[] Blowball_alt_0_further_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_further_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_further_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_further_SubNums { @_subpattern0, };
		public enum Blowball_alt_0_further_AltNums { };

#if INITIAL_WARMUP
		public Pattern_Blowball()
#else
		private Pattern_Blowball()
#endif
		{
			name = "Blowball";
			isSubpattern = true;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "Blowball_node_head", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_Blowball;
			PatternNode Blowball_node_head = new PatternNode((int) NodeTypes.@Node, "Blowball_node_head", "head", Blowball_node_head_AllowedTypes, Blowball_node_head_IsAllowedType, 5.5F, 0);
			PatternGraph Blowball_alt_0_end;
			PatternGraph Blowball_alt_0_end_neg_0;
			PatternNode Blowball_alt_0_end_neg_0_node__node0 = new PatternNode((int) NodeTypes.@Node, "Blowball_alt_0_end_neg_0_node__node0", "_node0", Blowball_alt_0_end_neg_0_node__node0_AllowedTypes, Blowball_alt_0_end_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge Blowball_alt_0_end_neg_0_edge__edge0 = new PatternEdge(Blowball_node_head, Blowball_alt_0_end_neg_0_node__node0, (int) EdgeTypes.@Edge, "Blowball_alt_0_end_neg_0_edge__edge0", "_edge0", Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes, Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			Blowball_alt_0_end_neg_0 = new PatternGraph(
				"neg_0",
				"Blowball_alt_0_end_",
				false,
				new PatternNode[] { Blowball_node_head, Blowball_alt_0_end_neg_0_node__node0 }, 
				new PatternEdge[] { Blowball_alt_0_end_neg_0_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			Blowball_alt_0_end = new PatternGraph(
				"end",
				"Blowball_alt_0_",
				false,
				new PatternNode[] { Blowball_node_head }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { Blowball_alt_0_end_neg_0,  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			PatternGraph Blowball_alt_0_further;
			PatternNode Blowball_alt_0_further_node__node0 = new PatternNode((int) NodeTypes.@Node, "Blowball_alt_0_further_node__node0", "_node0", Blowball_alt_0_further_node__node0_AllowedTypes, Blowball_alt_0_further_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge Blowball_alt_0_further_edge__edge0 = new PatternEdge(Blowball_node_head, Blowball_alt_0_further_node__node0, (int) EdgeTypes.@Edge, "Blowball_alt_0_further_edge__edge0", "_edge0", Blowball_alt_0_further_edge__edge0_AllowedTypes, Blowball_alt_0_further_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding Blowball_alt_0_further__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_Blowball.Instance, new PatternElement[] { Blowball_node_head });
			Blowball_alt_0_further = new PatternGraph(
				"further",
				"Blowball_alt_0_",
				false,
				new PatternNode[] { Blowball_node_head, Blowball_alt_0_further_node__node0 }, 
				new PatternEdge[] { Blowball_alt_0_further_edge__edge0 }, 
				new PatternGraphEmbedding[] { Blowball_alt_0_further__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			Alternative Blowball_alt_0 = new Alternative( "alt_0", "Blowball_", new PatternGraph[] { Blowball_alt_0_end, Blowball_alt_0_further } );

			pat_Blowball = new PatternGraph(
				"Blowball",
				"",
				false,
				new PatternNode[] { Blowball_node_head }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { Blowball_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			Blowball_node_head.PointOfDefinition = null;
			Blowball_alt_0_end_neg_0_node__node0.PointOfDefinition = Blowball_alt_0_end_neg_0;
			Blowball_alt_0_end_neg_0_edge__edge0.PointOfDefinition = Blowball_alt_0_end_neg_0;
			Blowball_alt_0_further_node__node0.PointOfDefinition = Blowball_alt_0_further;
			Blowball_alt_0_further_edge__edge0.PointOfDefinition = Blowball_alt_0_further;
			Blowball_alt_0_further__subpattern0.PointOfDefinition = Blowball_alt_0_further;

			patternGraph = pat_Blowball;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Pattern_ChainFrom : LGSPRulePattern
	{
		private static Pattern_ChainFrom instance = null;
		public static Pattern_ChainFrom Instance { get { if (instance==null) { instance = new Pattern_ChainFrom(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFrom_node_from_AllowedTypes = null;
		public static bool[] ChainFrom_node_from_IsAllowedType = null;
		public enum ChainFrom_NodeNums { @from, };
		public enum ChainFrom_EdgeNums { };
		public enum ChainFrom_SubNums { };
		public enum ChainFrom_AltNums { @alt_0, };
		public enum ChainFrom_alt_0_CaseNums { @base, @rec, };
		public enum ChainFrom_alt_0_base_NodeNums { };
		public enum ChainFrom_alt_0_base_EdgeNums { };
		public enum ChainFrom_alt_0_base_SubNums { };
		public enum ChainFrom_alt_0_base_AltNums { };
		public static NodeType[] ChainFrom_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_node_to_IsAllowedType = null;
		public static EdgeType[] ChainFrom_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFrom_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFrom_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFrom_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFrom_alt_0_rec_AltNums { };

#if INITIAL_WARMUP
		public Pattern_ChainFrom()
#else
		private Pattern_ChainFrom()
#endif
		{
			name = "ChainFrom";
			isSubpattern = true;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFrom_node_from", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_ChainFrom;
			PatternNode ChainFrom_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFrom_node_from", "from", ChainFrom_node_from_AllowedTypes, ChainFrom_node_from_IsAllowedType, 5.5F, 0);
			PatternGraph ChainFrom_alt_0_base;
			ChainFrom_alt_0_base = new PatternGraph(
				"base",
				"ChainFrom_alt_0_",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] 			);
			PatternGraph ChainFrom_alt_0_rec;
			PatternNode ChainFrom_alt_0_rec_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFrom_alt_0_rec_node_to", "to", ChainFrom_alt_0_rec_node_to_AllowedTypes, ChainFrom_alt_0_rec_node_to_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFrom_alt_0_rec_edge__edge0 = new PatternEdge(ChainFrom_node_from, ChainFrom_alt_0_rec_node_to, (int) EdgeTypes.@Edge, "ChainFrom_alt_0_rec_edge__edge0", "_edge0", ChainFrom_alt_0_rec_edge__edge0_AllowedTypes, ChainFrom_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFrom_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFrom.Instance, new PatternElement[] { ChainFrom_alt_0_rec_node_to });
			ChainFrom_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFrom_alt_0_",
				false,
				new PatternNode[] { ChainFrom_node_from, ChainFrom_alt_0_rec_node_to }, 
				new PatternEdge[] { ChainFrom_alt_0_rec_edge__edge0 }, 
				new PatternGraphEmbedding[] { ChainFrom_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			Alternative ChainFrom_alt_0 = new Alternative( "alt_0", "ChainFrom_", new PatternGraph[] { ChainFrom_alt_0_base, ChainFrom_alt_0_rec } );

			pat_ChainFrom = new PatternGraph(
				"ChainFrom",
				"",
				false,
				new PatternNode[] { ChainFrom_node_from }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFrom_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			ChainFrom_node_from.PointOfDefinition = null;
			ChainFrom_alt_0_rec_node_to.PointOfDefinition = ChainFrom_alt_0_rec;
			ChainFrom_alt_0_rec_edge__edge0.PointOfDefinition = ChainFrom_alt_0_rec;
			ChainFrom_alt_0_rec__subpattern0.PointOfDefinition = ChainFrom_alt_0_rec;

			patternGraph = pat_ChainFrom;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Pattern_ChainFromComplete : LGSPRulePattern
	{
		private static Pattern_ChainFromComplete instance = null;
		public static Pattern_ChainFromComplete Instance { get { if (instance==null) { instance = new Pattern_ChainFromComplete(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFromComplete_node_from_AllowedTypes = null;
		public static bool[] ChainFromComplete_node_from_IsAllowedType = null;
		public enum ChainFromComplete_NodeNums { @from, };
		public enum ChainFromComplete_EdgeNums { };
		public enum ChainFromComplete_SubNums { };
		public enum ChainFromComplete_AltNums { @alt_0, };
		public enum ChainFromComplete_alt_0_CaseNums { @base, @rec, };
		public enum ChainFromComplete_alt_0_base_NodeNums { @from, };
		public enum ChainFromComplete_alt_0_base_EdgeNums { };
		public enum ChainFromComplete_alt_0_base_SubNums { };
		public enum ChainFromComplete_alt_0_base_AltNums { };
		public static NodeType[] ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_base_neg_0_NodeNums { @from, @_node0, };
		public enum ChainFromComplete_alt_0_base_neg_0_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_base_neg_0_SubNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_AltNums { };
		public static NodeType[] ChainFromComplete_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_node_to_IsAllowedType = null;
		public static EdgeType[] ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFromComplete_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFromComplete_alt_0_rec_AltNums { };

#if INITIAL_WARMUP
		public Pattern_ChainFromComplete()
#else
		private Pattern_ChainFromComplete()
#endif
		{
			name = "ChainFromComplete";
			isSubpattern = true;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromComplete_node_from", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_ChainFromComplete;
			PatternNode ChainFromComplete_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFromComplete_node_from", "from", ChainFromComplete_node_from_AllowedTypes, ChainFromComplete_node_from_IsAllowedType, 5.5F, 0);
			PatternGraph ChainFromComplete_alt_0_base;
			PatternGraph ChainFromComplete_alt_0_base_neg_0;
			PatternNode ChainFromComplete_alt_0_base_neg_0_node__node0 = new PatternNode((int) NodeTypes.@Node, "ChainFromComplete_alt_0_base_neg_0_node__node0", "_node0", ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromComplete_alt_0_base_neg_0_edge__edge0 = new PatternEdge(ChainFromComplete_node_from, ChainFromComplete_alt_0_base_neg_0_node__node0, (int) EdgeTypes.@Edge, "ChainFromComplete_alt_0_base_neg_0_edge__edge0", "_edge0", ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromComplete_alt_0_base_neg_0 = new PatternGraph(
				"neg_0",
				"ChainFromComplete_alt_0_base_",
				false,
				new PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_base_neg_0_node__node0 }, 
				new PatternEdge[] { ChainFromComplete_alt_0_base_neg_0_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			ChainFromComplete_alt_0_base = new PatternGraph(
				"base",
				"ChainFromComplete_alt_0_",
				false,
				new PatternNode[] { ChainFromComplete_node_from }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { ChainFromComplete_alt_0_base_neg_0,  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			PatternGraph ChainFromComplete_alt_0_rec;
			PatternNode ChainFromComplete_alt_0_rec_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFromComplete_alt_0_rec_node_to", "to", ChainFromComplete_alt_0_rec_node_to_AllowedTypes, ChainFromComplete_alt_0_rec_node_to_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromComplete_alt_0_rec_edge__edge0 = new PatternEdge(ChainFromComplete_node_from, ChainFromComplete_alt_0_rec_node_to, (int) EdgeTypes.@Edge, "ChainFromComplete_alt_0_rec_edge__edge0", "_edge0", ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFromComplete_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromComplete.Instance, new PatternElement[] { ChainFromComplete_alt_0_rec_node_to });
			ChainFromComplete_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFromComplete_alt_0_",
				false,
				new PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_rec_node_to }, 
				new PatternEdge[] { ChainFromComplete_alt_0_rec_edge__edge0 }, 
				new PatternGraphEmbedding[] { ChainFromComplete_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			Alternative ChainFromComplete_alt_0 = new Alternative( "alt_0", "ChainFromComplete_", new PatternGraph[] { ChainFromComplete_alt_0_base, ChainFromComplete_alt_0_rec } );

			pat_ChainFromComplete = new PatternGraph(
				"ChainFromComplete",
				"",
				false,
				new PatternNode[] { ChainFromComplete_node_from }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFromComplete_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			ChainFromComplete_node_from.PointOfDefinition = null;
			ChainFromComplete_alt_0_base_neg_0_node__node0.PointOfDefinition = ChainFromComplete_alt_0_base_neg_0;
			ChainFromComplete_alt_0_base_neg_0_edge__edge0.PointOfDefinition = ChainFromComplete_alt_0_base_neg_0;
			ChainFromComplete_alt_0_rec_node_to.PointOfDefinition = ChainFromComplete_alt_0_rec;
			ChainFromComplete_alt_0_rec_edge__edge0.PointOfDefinition = ChainFromComplete_alt_0_rec;
			ChainFromComplete_alt_0_rec__subpattern0.PointOfDefinition = ChainFromComplete_alt_0_rec;

			patternGraph = pat_ChainFromComplete;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Pattern_ChainFromTo : LGSPRulePattern
	{
		private static Pattern_ChainFromTo instance = null;
		public static Pattern_ChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ChainFromTo(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFromTo_node_from_AllowedTypes = null;
		public static NodeType[] ChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ChainFromTo_node_to_IsAllowedType = null;
		public enum ChainFromTo_NodeNums { @from, @to, };
		public enum ChainFromTo_EdgeNums { };
		public enum ChainFromTo_SubNums { };
		public enum ChainFromTo_AltNums { @alt_0, };
		public enum ChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static EdgeType[] ChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_base_SubNums { };
		public enum ChainFromTo_alt_0_base_AltNums { };
		public static NodeType[] ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static EdgeType[] ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFromTo_alt_0_rec_AltNums { };

#if INITIAL_WARMUP
		public Pattern_ChainFromTo()
#else
		private Pattern_ChainFromTo()
#endif
		{
			name = "ChainFromTo";
			isSubpattern = true;

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromTo_node_from", "ChainFromTo_node_to", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_ChainFromTo;
			PatternNode ChainFromTo_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFromTo_node_from", "from", ChainFromTo_node_from_AllowedTypes, ChainFromTo_node_from_IsAllowedType, 5.5F, 0);
			PatternNode ChainFromTo_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFromTo_node_to", "to", ChainFromTo_node_to_AllowedTypes, ChainFromTo_node_to_IsAllowedType, 5.5F, 1);
			PatternGraph ChainFromTo_alt_0_base;
			PatternEdge ChainFromTo_alt_0_base_edge__edge0 = new PatternEdge(ChainFromTo_node_from, ChainFromTo_node_to, (int) EdgeTypes.@Edge, "ChainFromTo_alt_0_base_edge__edge0", "_edge0", ChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromTo_alt_0_base = new PatternGraph(
				"base",
				"ChainFromTo_alt_0_",
				false,
				new PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new PatternEdge[] { ChainFromTo_alt_0_base_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			PatternGraph ChainFromTo_alt_0_rec;
			PatternNode ChainFromTo_alt_0_rec_node_intermediate = new PatternNode((int) NodeTypes.@Node, "ChainFromTo_alt_0_rec_node_intermediate", "intermediate", ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromTo_alt_0_rec_edge__edge0 = new PatternEdge(ChainFromTo_node_from, ChainFromTo_alt_0_rec_node_intermediate, (int) EdgeTypes.@Edge, "ChainFromTo_alt_0_rec_edge__edge0", "_edge0", ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFromTo_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromTo.Instance, new PatternElement[] { ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to });
			ChainFromTo_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFromTo_alt_0_",
				false,
				new PatternNode[] { ChainFromTo_node_from, ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to }, 
				new PatternEdge[] { ChainFromTo_alt_0_rec_edge__edge0 }, 
				new PatternGraphEmbedding[] { ChainFromTo_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			Alternative ChainFromTo_alt_0 = new Alternative( "alt_0", "ChainFromTo_", new PatternGraph[] { ChainFromTo_alt_0_base, ChainFromTo_alt_0_rec } );

			pat_ChainFromTo = new PatternGraph(
				"ChainFromTo",
				"",
				false,
				new PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFromTo_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] 			);
			ChainFromTo_node_from.PointOfDefinition = null;
			ChainFromTo_node_to.PointOfDefinition = null;
			ChainFromTo_alt_0_base_edge__edge0.PointOfDefinition = ChainFromTo_alt_0_base;
			ChainFromTo_alt_0_rec_node_intermediate.PointOfDefinition = ChainFromTo_alt_0_rec;
			ChainFromTo_alt_0_rec_edge__edge0.PointOfDefinition = ChainFromTo_alt_0_rec;
			ChainFromTo_alt_0_rec__subpattern0.PointOfDefinition = ChainFromTo_alt_0_rec;

			patternGraph = pat_ChainFromTo;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_blowball : LGSPRulePattern
	{
		private static Rule_blowball instance = null;
		public static Rule_blowball Instance { get { if (instance==null) { instance = new Rule_blowball(); instance.initialize(); } return instance; } }

		public static NodeType[] blowball_node_head_AllowedTypes = null;
		public static bool[] blowball_node_head_IsAllowedType = null;
		public enum blowball_NodeNums { @head, };
		public enum blowball_EdgeNums { };
		public enum blowball_SubNums { @_subpattern0, };
		public enum blowball_AltNums { };

#if INITIAL_WARMUP
		public Rule_blowball()
#else
		private Rule_blowball()
#endif
		{
			name = "blowball";
			isSubpattern = false;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "blowball_node_head", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_blowball;
			PatternNode blowball_node_head = new PatternNode((int) NodeTypes.@Node, "blowball_node_head", "head", blowball_node_head_AllowedTypes, blowball_node_head_IsAllowedType, 5.5F, 0);
			PatternGraphEmbedding blowball__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_Blowball.Instance, new PatternElement[] { blowball_node_head });
			pat_blowball = new PatternGraph(
				"blowball",
				"",
				false,
				new PatternNode[] { blowball_node_head }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] { blowball__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			blowball_node_head.PointOfDefinition = null;
			blowball__subpattern0.PointOfDefinition = pat_blowball;

			patternGraph = pat_blowball;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_chainFrom : LGSPRulePattern
	{
		private static Rule_chainFrom instance = null;
		public static Rule_chainFrom Instance { get { if (instance==null) { instance = new Rule_chainFrom(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFrom_node_beg_AllowedTypes = null;
		public static bool[] chainFrom_node_beg_IsAllowedType = null;
		public enum chainFrom_NodeNums { @beg, };
		public enum chainFrom_EdgeNums { };
		public enum chainFrom_SubNums { @_subpattern0, };
		public enum chainFrom_AltNums { };

#if INITIAL_WARMUP
		public Rule_chainFrom()
#else
		private Rule_chainFrom()
#endif
		{
			name = "chainFrom";
			isSubpattern = false;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFrom_node_beg", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_chainFrom;
			PatternNode chainFrom_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFrom_node_beg", "beg", chainFrom_node_beg_AllowedTypes, chainFrom_node_beg_IsAllowedType, 5.5F, 0);
			PatternGraphEmbedding chainFrom__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFrom.Instance, new PatternElement[] { chainFrom_node_beg });
			pat_chainFrom = new PatternGraph(
				"chainFrom",
				"",
				false,
				new PatternNode[] { chainFrom_node_beg }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] { chainFrom__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			chainFrom_node_beg.PointOfDefinition = null;
			chainFrom__subpattern0.PointOfDefinition = pat_chainFrom;

			patternGraph = pat_chainFrom;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_chainFromComplete : LGSPRulePattern
	{
		private static Rule_chainFromComplete instance = null;
		public static Rule_chainFromComplete Instance { get { if (instance==null) { instance = new Rule_chainFromComplete(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFromComplete_node_beg_AllowedTypes = null;
		public static bool[] chainFromComplete_node_beg_IsAllowedType = null;
		public enum chainFromComplete_NodeNums { @beg, };
		public enum chainFromComplete_EdgeNums { };
		public enum chainFromComplete_SubNums { @_subpattern0, };
		public enum chainFromComplete_AltNums { };

#if INITIAL_WARMUP
		public Rule_chainFromComplete()
#else
		private Rule_chainFromComplete()
#endif
		{
			name = "chainFromComplete";
			isSubpattern = false;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromComplete_node_beg", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_chainFromComplete;
			PatternNode chainFromComplete_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFromComplete_node_beg", "beg", chainFromComplete_node_beg_AllowedTypes, chainFromComplete_node_beg_IsAllowedType, 5.5F, 0);
			PatternGraphEmbedding chainFromComplete__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromComplete.Instance, new PatternElement[] { chainFromComplete_node_beg });
			pat_chainFromComplete = new PatternGraph(
				"chainFromComplete",
				"",
				false,
				new PatternNode[] { chainFromComplete_node_beg }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] { chainFromComplete__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] 			);
			chainFromComplete_node_beg.PointOfDefinition = null;
			chainFromComplete__subpattern0.PointOfDefinition = pat_chainFromComplete;

			patternGraph = pat_chainFromComplete;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_chainFromTo : LGSPRulePattern
	{
		private static Rule_chainFromTo instance = null;
		public static Rule_chainFromTo Instance { get { if (instance==null) { instance = new Rule_chainFromTo(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFromTo_node_beg_AllowedTypes = null;
		public static NodeType[] chainFromTo_node_end_AllowedTypes = null;
		public static bool[] chainFromTo_node_beg_IsAllowedType = null;
		public static bool[] chainFromTo_node_end_IsAllowedType = null;
		public enum chainFromTo_NodeNums { @beg, @end, };
		public enum chainFromTo_EdgeNums { };
		public enum chainFromTo_SubNums { @_subpattern0, };
		public enum chainFromTo_AltNums { };

#if INITIAL_WARMUP
		public Rule_chainFromTo()
#else
		private Rule_chainFromTo()
#endif
		{
			name = "chainFromTo";
			isSubpattern = false;

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromTo_node_beg", "chainFromTo_node_end", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_chainFromTo;
			PatternNode chainFromTo_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFromTo_node_beg", "beg", chainFromTo_node_beg_AllowedTypes, chainFromTo_node_beg_IsAllowedType, 5.5F, 0);
			PatternNode chainFromTo_node_end = new PatternNode((int) NodeTypes.@Node, "chainFromTo_node_end", "end", chainFromTo_node_end_AllowedTypes, chainFromTo_node_end_IsAllowedType, 5.5F, 1);
			PatternGraphEmbedding chainFromTo__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromTo.Instance, new PatternElement[] { chainFromTo_node_beg, chainFromTo_node_end });
			pat_chainFromTo = new PatternGraph(
				"chainFromTo",
				"",
				false,
				new PatternNode[] { chainFromTo_node_beg, chainFromTo_node_end }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] { chainFromTo__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] 			);
			chainFromTo_node_beg.PointOfDefinition = null;
			chainFromTo_node_end.PointOfDefinition = null;
			chainFromTo__subpattern0.PointOfDefinition = pat_chainFromTo;

			patternGraph = pat_chainFromTo;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createBlowball : LGSPRulePattern
	{
		private static Rule_createBlowball instance = null;
		public static Rule_createBlowball Instance { get { if (instance==null) { instance = new Rule_createBlowball(); instance.initialize(); } return instance; } }

		public enum createBlowball_NodeNums { };
		public enum createBlowball_EdgeNums { };
		public enum createBlowball_SubNums { };
		public enum createBlowball_AltNums { };

#if INITIAL_WARMUP
		public Rule_createBlowball()
#else
		private Rule_createBlowball()
#endif
		{
			name = "createBlowball";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { NodeType_Node.typeVar, };
			outputNames = new string[] { "createBlowball_node_head", };
		}
		public override void initialize()
		{
			PatternGraph pat_createBlowball;
			pat_createBlowball = new PatternGraph(
				"createBlowball",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] 			);

			patternGraph = pat_createBlowball;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node__node3 = Node_Node.CreateNode(graph);
			Node_Node node__node0 = Node_Node.CreateNode(graph);
			Node_Node node_head = Node_Node.CreateNode(graph);
			Node_Node node__node2 = Node_Node.CreateNode(graph);
			Node_Node node__node1 = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_head, node__node3);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_head, node__node1);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_head, node__node0);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_head, node__node2);
			return new IGraphElement[] { node_head, };
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node__node3 = Node_Node.CreateNode(graph);
			Node_Node node__node0 = Node_Node.CreateNode(graph);
			Node_Node node_head = Node_Node.CreateNode(graph);
			Node_Node node__node2 = Node_Node.CreateNode(graph);
			Node_Node node__node1 = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_head, node__node3);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_head, node__node1);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_head, node__node0);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_head, node__node2);
			return new IGraphElement[] { node_head, };
		}
		private static String[] addedNodeNames = new String[] { "_node3", "_node0", "head", "_node2", "_node1" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge3", "_edge1", "_edge0", "_edge2" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createChain : LGSPRulePattern
	{
		private static Rule_createChain instance = null;
		public static Rule_createChain Instance { get { if (instance==null) { instance = new Rule_createChain(); instance.initialize(); } return instance; } }

		public enum createChain_NodeNums { };
		public enum createChain_EdgeNums { };
		public enum createChain_SubNums { };
		public enum createChain_AltNums { };

#if INITIAL_WARMUP
		public Rule_createChain()
#else
		private Rule_createChain()
#endif
		{
			name = "createChain";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			outputNames = new string[] { "createChain_node_beg", "createChain_node_end", };
		}
		public override void initialize()
		{
			PatternGraph pat_createChain;
			pat_createChain = new PatternGraph(
				"createChain",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] 			);

			patternGraph = pat_createChain;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node__node0 = Node_Node.CreateNode(graph);
			Node_Node node_end = Node_Node.CreateNode(graph);
			Node_Node node__node1 = Node_Node.CreateNode(graph);
			Node_Node node_beg = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_beg, node__node0);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node__node1, node_end);
			return new IGraphElement[] { node_beg, node_end, };
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node__node0 = Node_Node.CreateNode(graph);
			Node_Node node_end = Node_Node.CreateNode(graph);
			Node_Node node__node1 = Node_Node.CreateNode(graph);
			Node_Node node_beg = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_beg, node__node0);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node__node1, node_end);
			return new IGraphElement[] { node_beg, node_end, };
		}
		private static String[] addedNodeNames = new String[] { "_node0", "end", "_node1", "beg" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1", "_edge0", "_edge2" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}


    public class PatternAction_Blowball : LGSPSubpatternAction
    {
        public PatternAction_Blowball(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Blowball.Instance.patternGraph;
        }

        public LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Blowball_node_head 
            LGSPNode candidate_Blowball_node_head = Blowball_node_head;
            // Push alternative matching task for Blowball_alt_0
            AlternativeAction_Blowball_alt_0 taskFor_alt_0 = new AlternativeAction_Blowball_alt_0(graph, openTasks, patternGraph.alternatives[(int)Pattern_Blowball.Blowball_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Blowball_node_head = candidate_Blowball_node_head;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Blowball.Blowball_NodeNums.@head] = candidate_Blowball_node_head;
                    match.EmbeddedGraphs[((int)Pattern_Blowball.Blowball_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
                // if enough matches were found, we leave
                if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                {
                    openTasks.Push(this);
                    return;
                }
                openTasks.Push(this);
                return;
            }
            candidate_Blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_Blowball_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_Blowball_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Blowball_alt_0_end 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@end];
                // SubPreset Blowball_node_head 
                LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();
                    }
                    uint prev_neg_0__candidate_Blowball_node_head;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        prev_neg_0__candidate_Blowball_node_head = candidate_Blowball_node_head.flags & LGSPNode.IS_MATCHED<<negLevel;
                        candidate_Blowball_node_head.flags |= LGSPNode.IS_MATCHED<<negLevel;
                    } else {
                        prev_neg_0__candidate_Blowball_node_head = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_node_head) ? 1U : 0U;
                        if(prev_neg_0__candidate_Blowball_node_head==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_Blowball_node_head,candidate_Blowball_node_head);
                    }
                    // Extend outgoing Blowball_alt_0_end_neg_0_edge__edge0 from Blowball_node_head 
                    LGSPEdge head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_node_head.outhead;
                    if(head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge candidate_Blowball_alt_0_end_neg_0_edge__edge0 = head_candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_Edge.isMyType[candidate_Blowball_alt_0_end_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit target Blowball_alt_0_end_neg_0_node__node0 from Blowball_alt_0_end_neg_0_edge__edge0 
                            LGSPNode candidate_Blowball_alt_0_end_neg_0_node__node0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.target;
                            if((negLevel<=MAX_NEG_LEVEL ? (candidate_Blowball_alt_0_end_neg_0_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_alt_0_end_neg_0_node__node0))
                                && candidate_Blowball_alt_0_end_neg_0_node__node0==candidate_Blowball_node_head
                                )
                            {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Blowball_node_head.flags = candidate_Blowball_node_head.flags & ~prev_neg_0__candidate_Blowball_node_head | prev_neg_0__candidate_Blowball_node_head;
                            } else { 
                                if(prev_neg_0__candidate_Blowball_node_head==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Blowball_node_head);
                                }
                            }
                            if(negLevel > MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                            }
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.outNext) != head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 );
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_Blowball_node_head.flags = candidate_Blowball_node_head.flags & ~prev_neg_0__candidate_Blowball_node_head | prev_neg_0__candidate_Blowball_node_head;
                    } else { 
                        if(prev_neg_0__candidate_Blowball_node_head==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Blowball_node_head);
                        }
                    }
                    if(negLevel > MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                    }
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_end_NodeNums.@head] = candidate_Blowball_node_head;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label1;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_end_NodeNums.@head] = candidate_Blowball_node_head;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
                            foundPartialMatches.Add(match);
                        }
                        matchesList.Clear();
                    }
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label2;
                }
                candidate_Blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
label0: ;
label1: ;
label2: ;
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case Blowball_alt_0_further 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@further];
                // SubPreset Blowball_node_head 
                LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // Extend outgoing Blowball_alt_0_further_edge__edge0 from Blowball_node_head 
                LGSPEdge head_candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_node_head.outhead;
                if(head_candidate_Blowball_alt_0_further_edge__edge0 != null)
                {
                    LGSPEdge candidate_Blowball_alt_0_further_edge__edge0 = head_candidate_Blowball_alt_0_further_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_Blowball_alt_0_further_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit target Blowball_alt_0_further_node__node0 from Blowball_alt_0_further_edge__edge0 
                        LGSPNode candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Blowball_alt_0_further_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_alt_0_further_node__node0))
                            && candidate_Blowball_alt_0_further_node__node0==candidate_Blowball_node_head
                            )
                        {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_Blowball taskFor__subpattern0 = new PatternAction_Blowball(graph, openTasks);
                        taskFor__subpattern0.Blowball_node_head = candidate_Blowball_node_head;
                        openTasks.Push(taskFor__subpattern0);
                        candidate_Blowball_alt_0_further_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_further_NodeNums.@head] = candidate_Blowball_node_head;
                                match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_further_NodeNums.@_node0] = candidate_Blowball_alt_0_further_node__node0;
                                match.Edges[(int)Pattern_Blowball.Blowball_alt_0_further_EdgeNums.@_edge0] = candidate_Blowball_alt_0_further_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_Blowball.Blowball_alt_0_further_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_Blowball_alt_0_further_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Blowball_alt_0_further_node__node0.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_Blowball_alt_0_further_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            candidate_Blowball_alt_0_further_node__node0.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            continue;
                        }
                        candidate_Blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_node__node0.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    }
                    while( (candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.outNext) != head_candidate_Blowball_alt_0_further_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFrom : LGSPSubpatternAction
    {
        public PatternAction_ChainFrom(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFrom.Instance.patternGraph;
        }

        public LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFrom_node_from 
            LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
            // Push alternative matching task for ChainFrom_alt_0
            AlternativeAction_ChainFrom_alt_0 taskFor_alt_0 = new AlternativeAction_ChainFrom_alt_0(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFrom.ChainFrom_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFrom_node_from = candidate_ChainFrom_node_from;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFrom.ChainFrom_NodeNums.@from] = candidate_ChainFrom_node_from;
                    match.EmbeddedGraphs[((int)Pattern_ChainFrom.ChainFrom_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
                // if enough matches were found, we leave
                if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                {
                    openTasks.Push(this);
                    return;
                }
                openTasks.Push(this);
                return;
            }
            candidate_ChainFrom_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_ChainFrom_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_ChainFrom_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFrom_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_CaseNums.@base];
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    continue;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
                            foundPartialMatches.Add(match);
                        }
                        matchesList.Clear();
                    }
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    continue;
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case ChainFrom_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_CaseNums.@rec];
                // SubPreset ChainFrom_node_from 
                LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
                // Extend outgoing ChainFrom_alt_0_rec_edge__edge0 from ChainFrom_node_from 
                LGSPEdge head_candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_node_from.outhead;
                if(head_candidate_ChainFrom_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFrom_alt_0_rec_edge__edge0 = head_candidate_ChainFrom_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFrom_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit target ChainFrom_alt_0_rec_node_to from ChainFrom_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFrom_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFrom_alt_0_rec_node_to))
                            && candidate_ChainFrom_alt_0_rec_node_to==candidate_ChainFrom_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFrom taskFor__subpattern0 = new PatternAction_ChainFrom(graph, openTasks);
                        taskFor__subpattern0.ChainFrom_node_from = candidate_ChainFrom_alt_0_rec_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        candidate_ChainFrom_alt_0_rec_node_to.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_NodeNums.@from] = candidate_ChainFrom_node_from;
                                match.Nodes[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_NodeNums.@to] = candidate_ChainFrom_alt_0_rec_node_to;
                                match.Edges[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFrom_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFrom_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ChainFrom_alt_0_rec_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFrom_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            candidate_ChainFrom_alt_0_rec_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            continue;
                        }
                        candidate_ChainFrom_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    }
                    while( (candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFrom_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromComplete : LGSPSubpatternAction
    {
        public PatternAction_ChainFromComplete(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromComplete.Instance.patternGraph;
        }

        public LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromComplete_node_from 
            LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
            // Push alternative matching task for ChainFromComplete_alt_0
            AlternativeAction_ChainFromComplete_alt_0 taskFor_alt_0 = new AlternativeAction_ChainFromComplete_alt_0(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFromComplete.ChainFromComplete_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromComplete_node_from = candidate_ChainFromComplete_node_from;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                    match.EmbeddedGraphs[((int)Pattern_ChainFromComplete.ChainFromComplete_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
                // if enough matches were found, we leave
                if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                {
                    openTasks.Push(this);
                    return;
                }
                openTasks.Push(this);
                return;
            }
            candidate_ChainFromComplete_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_ChainFromComplete_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_ChainFromComplete_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromComplete_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_CaseNums.@base];
                // SubPreset ChainFromComplete_node_from 
                LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();
                    }
                    uint prev_neg_0__candidate_ChainFromComplete_node_from;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        prev_neg_0__candidate_ChainFromComplete_node_from = candidate_ChainFromComplete_node_from.flags & LGSPNode.IS_MATCHED<<negLevel;
                        candidate_ChainFromComplete_node_from.flags |= LGSPNode.IS_MATCHED<<negLevel;
                    } else {
                        prev_neg_0__candidate_ChainFromComplete_node_from = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_node_from) ? 1U : 0U;
                        if(prev_neg_0__candidate_ChainFromComplete_node_from==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ChainFromComplete_node_from,candidate_ChainFromComplete_node_from);
                    }
                    // Extend outgoing ChainFromComplete_alt_0_base_neg_0_edge__edge0 from ChainFromComplete_node_from 
                    LGSPEdge head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_node_from.outhead;
                    if(head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_Edge.isMyType[candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit target ChainFromComplete_alt_0_base_neg_0_node__node0 from ChainFromComplete_alt_0_base_neg_0_edge__edge0 
                            LGSPNode candidate_ChainFromComplete_alt_0_base_neg_0_node__node0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.target;
                            if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_alt_0_base_neg_0_node__node0))
                                && candidate_ChainFromComplete_alt_0_base_neg_0_node__node0==candidate_ChainFromComplete_node_from
                                )
                            {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ChainFromComplete_node_from.flags = candidate_ChainFromComplete_node_from.flags & ~prev_neg_0__candidate_ChainFromComplete_node_from | prev_neg_0__candidate_ChainFromComplete_node_from;
                            } else { 
                                if(prev_neg_0__candidate_ChainFromComplete_node_from==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ChainFromComplete_node_from);
                                }
                            }
                            if(negLevel > MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                            }
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.outNext) != head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 );
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_ChainFromComplete_node_from.flags = candidate_ChainFromComplete_node_from.flags & ~prev_neg_0__candidate_ChainFromComplete_node_from | prev_neg_0__candidate_ChainFromComplete_node_from;
                    } else { 
                        if(prev_neg_0__candidate_ChainFromComplete_node_from==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ChainFromComplete_node_from);
                        }
                    }
                    if(negLevel > MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                    }
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_base_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label4;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_base_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
                            foundPartialMatches.Add(match);
                        }
                        matchesList.Clear();
                    }
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label5;
                }
                candidate_ChainFromComplete_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
label3: ;
label4: ;
label5: ;
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case ChainFromComplete_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_CaseNums.@rec];
                // SubPreset ChainFromComplete_node_from 
                LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
                // Extend outgoing ChainFromComplete_alt_0_rec_edge__edge0 from ChainFromComplete_node_from 
                LGSPEdge head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_node_from.outhead;
                if(head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromComplete_alt_0_rec_edge__edge0 = head_candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromComplete_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit target ChainFromComplete_alt_0_rec_node_to from ChainFromComplete_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromComplete_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_alt_0_rec_node_to))
                            && candidate_ChainFromComplete_alt_0_rec_node_to==candidate_ChainFromComplete_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFromComplete taskFor__subpattern0 = new PatternAction_ChainFromComplete(graph, openTasks);
                        taskFor__subpattern0.ChainFromComplete_node_from = candidate_ChainFromComplete_alt_0_rec_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        candidate_ChainFromComplete_alt_0_rec_node_to.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                                match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_NodeNums.@to] = candidate_ChainFromComplete_alt_0_rec_node_to;
                                match.Edges[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ChainFromComplete_alt_0_rec_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            candidate_ChainFromComplete_alt_0_rec_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            continue;
                        }
                        candidate_ChainFromComplete_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    }
                    while( (candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromTo : LGSPSubpatternAction
    {
        public PatternAction_ChainFromTo(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromTo.Instance.patternGraph;
        }

        public LGSPNode ChainFromTo_node_from;
        public LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromTo_node_from 
            LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
            // SubPreset ChainFromTo_node_to 
            LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
            // Push alternative matching task for ChainFromTo_alt_0
            AlternativeAction_ChainFromTo_alt_0 taskFor_alt_0 = new AlternativeAction_ChainFromTo_alt_0(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFromTo.ChainFromTo_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromTo_node_from = candidate_ChainFromTo_node_from;
            taskFor_alt_0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_NodeNums.@from] = candidate_ChainFromTo_node_from;
                    match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_NodeNums.@to] = candidate_ChainFromTo_node_to;
                    match.EmbeddedGraphs[((int)Pattern_ChainFromTo.ChainFromTo_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
                // if enough matches were found, we leave
                if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                {
                    openTasks.Push(this);
                    return;
                }
                openTasks.Push(this);
                return;
            }
            candidate_ChainFromTo_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_ChainFromTo_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_ChainFromTo_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_ChainFromTo_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode ChainFromTo_node_from;
        public LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromTo_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_CaseNums.@base];
                // SubPreset ChainFromTo_node_from 
                LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
                // SubPreset ChainFromTo_node_to 
                LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
                // Extend outgoing ChainFromTo_alt_0_base_edge__edge0 from ChainFromTo_node_from 
                LGSPEdge head_candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_node_from.outhead;
                if(head_candidate_ChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromTo_alt_0_base_edge__edge0 = head_candidate_ChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromTo_alt_0_base_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ChainFromTo_alt_0_base_edge__edge0.target != candidate_ChainFromTo_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@from] = candidate_ChainFromTo_node_from;
                            match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@to] = candidate_ChainFromTo_node_to;
                            match.Edges[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromTo_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@from] = candidate_ChainFromTo_node_from;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@to] = candidate_ChainFromTo_node_to;
                                match.Edges[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromTo_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromTo_alt_0_base_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_base_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            continue;
                        }
                        candidate_ChainFromTo_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_base_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    }
                    while( (candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromTo_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case ChainFromTo_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_CaseNums.@rec];
                // SubPreset ChainFromTo_node_from 
                LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
                // SubPreset ChainFromTo_node_to 
                LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
                // Extend outgoing ChainFromTo_alt_0_rec_edge__edge0 from ChainFromTo_node_from 
                LGSPEdge head_candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_node_from.outhead;
                if(head_candidate_ChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromTo_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit target ChainFromTo_alt_0_rec_node_intermediate from ChainFromTo_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromTo_alt_0_rec_node_intermediate))
                            && candidate_ChainFromTo_alt_0_rec_node_intermediate==candidate_ChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFromTo taskFor__subpattern0 = new PatternAction_ChainFromTo(graph, openTasks);
                        taskFor__subpattern0.ChainFromTo_node_from = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__subpattern0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[1], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_NodeNums.@from] = candidate_ChainFromTo_node_from;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_NodeNums.@intermediate] = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_NodeNums.@to] = candidate_ChainFromTo_node_to;
                                match.Edges[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFromTo_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromTo_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ChainFromTo_alt_0_rec_node_intermediate.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            candidate_ChainFromTo_alt_0_rec_node_intermediate.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                            continue;
                        }
                        candidate_ChainFromTo_node_from.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_node_to.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.flags &= ~LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    }
                    while( (candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_blowball : LGSPAction
    {
        public Action_blowball() {
            rulePattern = Rule_blowball.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 1+0);
        }

        public override string Name { get { return "blowball"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_blowball instance = new Action_blowball();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset blowball_node_head 
            LGSPNode candidate_blowball_node_head = (LGSPNode) parameters[0];
            if(candidate_blowball_node_head == null) {
                MissingPreset_blowball_node_head(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_Blowball taskFor__subpattern0 = new PatternAction_Blowball(graph, openTasks);
            taskFor__subpattern0.Blowball_node_head = candidate_blowball_node_head;
            openTasks.Push(taskFor__subpattern0);
            candidate_blowball_node_head.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_blowball.blowball_NodeNums.@head] = candidate_blowball_node_head;
                    match.EmbeddedGraphs[(int)Rule_blowball.blowball_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    return matches;
                }
                candidate_blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                return matches;
            }
            candidate_blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            return matches;
        }
        public void MissingPreset_blowball_node_head(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup blowball_node_head 
            int type_id_candidate_blowball_node_head = 0;
            for(LGSPNode head_candidate_blowball_node_head = graph.nodesByTypeHeads[type_id_candidate_blowball_node_head], candidate_blowball_node_head = head_candidate_blowball_node_head.typeNext; candidate_blowball_node_head != head_candidate_blowball_node_head; candidate_blowball_node_head = candidate_blowball_node_head.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_Blowball taskFor__subpattern0 = new PatternAction_Blowball(graph, openTasks);
                taskFor__subpattern0.Blowball_node_head = candidate_blowball_node_head;
                openTasks.Push(taskFor__subpattern0);
                candidate_blowball_node_head.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_blowball.blowball_NodeNums.@head] = candidate_blowball_node_head;
                        match.EmbeddedGraphs[(int)Rule_blowball.blowball_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        return;
                    }
                    candidate_blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    continue;
                }
                candidate_blowball_node_head.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            }
            return;
        }
    }

    public class Action_chainFrom : LGSPAction
    {
        public Action_chainFrom() {
            rulePattern = Rule_chainFrom.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 1+0);
        }

        public override string Name { get { return "chainFrom"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFrom instance = new Action_chainFrom();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFrom_node_beg 
            LGSPNode candidate_chainFrom_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFrom_node_beg == null) {
                MissingPreset_chainFrom_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ChainFrom taskFor__subpattern0 = new PatternAction_ChainFrom(graph, openTasks);
            taskFor__subpattern0.ChainFrom_node_from = candidate_chainFrom_node_beg;
            openTasks.Push(taskFor__subpattern0);
            candidate_chainFrom_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFrom.chainFrom_NodeNums.@beg] = candidate_chainFrom_node_beg;
                    match.EmbeddedGraphs[(int)Rule_chainFrom.chainFrom_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFrom_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    return matches;
                }
                candidate_chainFrom_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                return matches;
            }
            candidate_chainFrom_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            return matches;
        }
        public void MissingPreset_chainFrom_node_beg(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFrom_node_beg 
            int type_id_candidate_chainFrom_node_beg = 0;
            for(LGSPNode head_candidate_chainFrom_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFrom_node_beg], candidate_chainFrom_node_beg = head_candidate_chainFrom_node_beg.typeNext; candidate_chainFrom_node_beg != head_candidate_chainFrom_node_beg; candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFrom taskFor__subpattern0 = new PatternAction_ChainFrom(graph, openTasks);
                taskFor__subpattern0.ChainFrom_node_from = candidate_chainFrom_node_beg;
                openTasks.Push(taskFor__subpattern0);
                candidate_chainFrom_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFrom.chainFrom_NodeNums.@beg] = candidate_chainFrom_node_beg;
                        match.EmbeddedGraphs[(int)Rule_chainFrom.chainFrom_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFrom_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        return;
                    }
                    candidate_chainFrom_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    continue;
                }
                candidate_chainFrom_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            }
            return;
        }
    }

    public class Action_chainFromComplete : LGSPAction
    {
        public Action_chainFromComplete() {
            rulePattern = Rule_chainFromComplete.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 1+0);
        }

        public override string Name { get { return "chainFromComplete"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFromComplete instance = new Action_chainFromComplete();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFromComplete_node_beg 
            LGSPNode candidate_chainFromComplete_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFromComplete_node_beg == null) {
                MissingPreset_chainFromComplete_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ChainFromComplete taskFor__subpattern0 = new PatternAction_ChainFromComplete(graph, openTasks);
            taskFor__subpattern0.ChainFromComplete_node_from = candidate_chainFromComplete_node_beg;
            openTasks.Push(taskFor__subpattern0);
            candidate_chainFromComplete_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFromComplete.chainFromComplete_NodeNums.@beg] = candidate_chainFromComplete_node_beg;
                    match.EmbeddedGraphs[(int)Rule_chainFromComplete.chainFromComplete_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFromComplete_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    return matches;
                }
                candidate_chainFromComplete_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                return matches;
            }
            candidate_chainFromComplete_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            return matches;
        }
        public void MissingPreset_chainFromComplete_node_beg(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromComplete_node_beg 
            int type_id_candidate_chainFromComplete_node_beg = 0;
            for(LGSPNode head_candidate_chainFromComplete_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromComplete_node_beg], candidate_chainFromComplete_node_beg = head_candidate_chainFromComplete_node_beg.typeNext; candidate_chainFromComplete_node_beg != head_candidate_chainFromComplete_node_beg; candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromComplete taskFor__subpattern0 = new PatternAction_ChainFromComplete(graph, openTasks);
                taskFor__subpattern0.ChainFromComplete_node_from = candidate_chainFromComplete_node_beg;
                openTasks.Push(taskFor__subpattern0);
                candidate_chainFromComplete_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromComplete.chainFromComplete_NodeNums.@beg] = candidate_chainFromComplete_node_beg;
                        match.EmbeddedGraphs[(int)Rule_chainFromComplete.chainFromComplete_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromComplete_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        return;
                    }
                    candidate_chainFromComplete_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    continue;
                }
                candidate_chainFromComplete_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            }
            return;
        }
    }

    public class Action_chainFromTo : LGSPAction
    {
        public Action_chainFromTo() {
            rulePattern = Rule_chainFromTo.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 0, 1+0);
        }

        public override string Name { get { return "chainFromTo"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFromTo instance = new Action_chainFromTo();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFromTo_node_beg 
            LGSPNode candidate_chainFromTo_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFromTo_node_beg == null) {
                MissingPreset_chainFromTo_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromTo_node_beg;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_chainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromTo_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromTo_node_beg,candidate_chainFromTo_node_beg);
            }
            // Preset chainFromTo_node_end 
            LGSPNode candidate_chainFromTo_node_end = (LGSPNode) parameters[1];
            if(candidate_chainFromTo_node_end == null) {
                MissingPreset_chainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromTo_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end))
                && candidate_chainFromTo_node_end==candidate_chainFromTo_node_beg
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ChainFromTo taskFor__subpattern0 = new PatternAction_ChainFromTo(graph, openTasks);
            taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
            taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
            openTasks.Push(taskFor__subpattern0);
            candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@beg] = candidate_chainFromTo_node_beg;
                    match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@end] = candidate_chainFromTo_node_end;
                    match.EmbeddedGraphs[(int)Rule_chainFromTo.chainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
            } else { 
                if(prev__candidate_chainFromTo_node_beg==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromTo_node_beg(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromTo_node_beg 
            int type_id_candidate_chainFromTo_node_beg = 0;
            for(LGSPNode head_candidate_chainFromTo_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromTo_node_beg], candidate_chainFromTo_node_beg = head_candidate_chainFromTo_node_beg.typeNext; candidate_chainFromTo_node_beg != head_candidate_chainFromTo_node_beg; candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.typeNext)
            {
                uint prev__candidate_chainFromTo_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_chainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromTo_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromTo_node_beg,candidate_chainFromTo_node_beg);
                }
                // Preset chainFromTo_node_end 
                LGSPNode candidate_chainFromTo_node_end = (LGSPNode) parameters[1];
                if(candidate_chainFromTo_node_end == null) {
                    MissingPreset_chainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromTo_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_chainFromTo_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end))
                    && candidate_chainFromTo_node_end==candidate_chainFromTo_node_beg
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromTo taskFor__subpattern0 = new PatternAction_ChainFromTo(graph, openTasks);
                taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
                taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@beg] = candidate_chainFromTo_node_beg;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@end] = candidate_chainFromTo_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromTo.chainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_chainFromTo_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~prev__candidate_chainFromTo_node_beg | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromTo_node_end(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_chainFromTo_node_beg)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromTo_node_end 
            int type_id_candidate_chainFromTo_node_end = 0;
            for(LGSPNode head_candidate_chainFromTo_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromTo_node_end], candidate_chainFromTo_node_end = head_candidate_chainFromTo_node_end.typeNext; candidate_chainFromTo_node_end != head_candidate_chainFromTo_node_end; candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end))
                    && candidate_chainFromTo_node_end==candidate_chainFromTo_node_beg
                    )
                {
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromTo taskFor__subpattern0 = new PatternAction_ChainFromTo(graph, openTasks);
                taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
                taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@beg] = candidate_chainFromTo_node_beg;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@end] = candidate_chainFromTo_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromTo.chainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        return;
                    }
                    candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    continue;
                }
                candidate_chainFromTo_node_beg.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags &= ~LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            }
            return;
        }
    }

    public class Action_createBlowball : LGSPAction
    {
        public Action_createBlowball() {
            rulePattern = Rule_createBlowball.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createBlowball"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createBlowball instance = new Action_createBlowball();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_createChain : LGSPAction
    {
        public Action_createChain() {
            rulePattern = Rule_createChain.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createChain"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createChain instance = new Action_createChain();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }


    public class RecursiveActions : LGSPActions
    {
        public RecursiveActions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public RecursiveActions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("blowball", (LGSPAction) Action_blowball.Instance);
            actions.Add("chainFrom", (LGSPAction) Action_chainFrom.Instance);
            actions.Add("chainFromComplete", (LGSPAction) Action_chainFromComplete.Instance);
            actions.Add("chainFromTo", (LGSPAction) Action_chainFromTo.Instance);
            actions.Add("createBlowball", (LGSPAction) Action_createBlowball.Instance);
            actions.Add("createChain", (LGSPAction) Action_createChain.Instance);
        }

        public override String Name { get { return "RecursiveActions"; } }
        public override String ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}
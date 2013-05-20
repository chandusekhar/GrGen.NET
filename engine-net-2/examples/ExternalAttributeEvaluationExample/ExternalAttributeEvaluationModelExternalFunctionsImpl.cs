using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_ExternalAttributeEvaluation
{
    public partial class Own
    {
        public bool muh()
        {
            return false;
        }
    }

    public partial class OwnPown : Own
    {
        public string ehe;
    }
	
	public partial class OwnPownHome : OwnPown
    {
        public string aha;
    }

    public partial class AttributeTypeObjectEmitterParser
    {
        public static object ParseImpl(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
        {
            char lookahead = (char)reader.Peek();
            if(lookahead == 'o')
            {
                reader.Read(); // eat 'o'
                return new Own();
            }
            else if(lookahead == 'p')
            {
                reader.Read(); // eat 'p'
                StringBuilder sb = new StringBuilder();
                while(reader.Peek() != ',' && reader.Peek() != ')') // attributes are separated by , a node/edge terminated by ) in .grs
                    sb.Append((char)reader.Read()); // eat non ',', ')'
                OwnPown op = new OwnPown();
                op.ehe = sb.ToString();
                return op;
            }
            else if(lookahead == 'h')
            {
                reader.Read(); // eat 'h'
                StringBuilder sb = new StringBuilder();
                while(reader.Peek() != ';')
                    sb.Append((char)reader.Read()); // eat non ';'
                string ehe = sb.ToString();
                sb.Length = 0;
                reader.Read(); // eat ';'
                while(reader.Peek() != ',' && reader.Peek() != ')') // attributes are separated by , a node/edge terminated by ) in .grs
                    sb.Append((char)reader.Read()); // eat non ',',')'
                OwnPownHome oph = new OwnPownHome();
                oph.ehe = ehe;
                oph.aha = sb.ToString();
                return oph;
            }
            else
            {
                if(reader.Peek() == 'n')
                {
                    reader.Read();
                    if(reader.Peek() == 'u')
                    {
                        reader.Read();
                        if(reader.Peek() == 'l')
                        {
                            reader.Read();
                            if(reader.Peek() == 'l')
                            {
                                reader.Read();
                                return null;
                            }
                        }
                    }
                }
                throw new Exception("parsing failure");
            }
        }

        public static string SerializeImpl(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
        {
            if(attribute == null)
            {
                // attrType.Kind is always ObjectAttr
                if(attrType.Type == typeof(OwnPownHome))
                    return "null";
                else if(attrType.Type == typeof(OwnPown))
                    return "null";
                else if(attrType.Type == typeof(Own))
                    return "null";
                return "null";
            }

            // important: if cascade from most specific to least specific, from subtypes to supertypes
            if(attribute.GetType()==typeof(OwnPownHome))
            {
                OwnPownHome oph = (OwnPownHome)attribute;
                StringBuilder sb = new StringBuilder();
                sb.Append("h");
                sb.Append(oph.ehe);
                sb.Append(";");
                sb.Append(oph.aha);
                return sb.ToString();
            }
            else if(attribute.GetType()==typeof(OwnPown))
            {
                OwnPown op = (OwnPown)attribute;
                StringBuilder sb = new StringBuilder();
                sb.Append("p");
                sb.Append(op.ehe);
                return sb.ToString();
            }
            else if(attribute.GetType() == typeof(Own))
            {
                return "o";
            }
            else
            {
                Console.WriteLine("Warning: Exporting attribute of object type to null");
                return "null";
            }
        }

        public static string EmitImpl(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
        {
            if(attribute == null)
                return "null";

            // important: if cascade from most specific to least specific, from subtypes to supertypes
            if(attribute.GetType() == typeof(OwnPownHome))
            {
                OwnPownHome oph = (OwnPownHome)attribute;
                StringBuilder sb = new StringBuilder();
                sb.Append("OwnPownHome { ehe: ");
                sb.Append(oph.ehe);
                sb.Append(" aha: ");
                sb.Append(oph.aha);
                sb.Append(" }");
                return sb.ToString();
            }
            else if(attribute.GetType() == typeof(OwnPown))
            {
                OwnPown op = (OwnPown)attribute;
                StringBuilder sb = new StringBuilder();
                sb.Append("OwnPown { ehe: ");
                sb.Append(op.ehe);
                sb.Append(" }");
                return sb.ToString();
            }
            else if(attribute.GetType() == typeof(Own))
            {
                return "Own";
            }
            else
            {
                return attribute.ToString();
            }
        }
    }
}

namespace de.unika.ipd.grGen.expression
{
    using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

	public partial class ExternalFunctions
	{
        public static bool foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int a, double b, GRGEN_MODEL.ENUM_Enu c, string d)
        {
            return true;
        }

        public static object bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object a, object b)
        {
            return a ?? b ?? null;
        }

        public static bool isnull(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object a)
        {
            return a == null;
        }

        public static bool bla(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IN a, GRGEN_MODEL.IE b)
        {
            return a.b;
        }

        public static GRGEN_MODEL.IN blo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.INode a, GRGEN_LIBGR.IEdge b)
        {
            return ((GRGEN_MODEL.ExternalAttributeEvaluationGraph)graph).CreateNodeN();
        }

        public static GRGEN_MODEL.OwnPown har(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.Own a, GRGEN_MODEL.OwnPown b)
        {
            return a!=null ? (a.muh() ? (GRGEN_MODEL.OwnPown)a : b) : null;
        }

        public static bool hur(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPown a)
        {
            return a!=null ? a.ehe==null : true;
        }

        public static bool hurdur(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPownHome a)
		{
			return a!=null ? a.aha==null : true;
		}

        public static GRGEN_MODEL.Own own(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            return new GRGEN_MODEL.Own();
        }

        public static GRGEN_MODEL.OwnPown ownPown(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_MODEL.OwnPown op = new GRGEN_MODEL.OwnPown();
            op.ehe = "hahaha";
            return op;
        }

        public static GRGEN_MODEL.OwnPownHome ownPownHome(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_MODEL.OwnPownHome oph = new GRGEN_MODEL.OwnPownHome();
            oph.ehe = "hahaha";
            oph.aha = "lalala";
            return oph;
        }
    }
}

namespace de.unika.ipd.grGen.expression
{
    using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

    public partial class ExternalProcedures
    {
        public static void fooProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int a, double b, GRGEN_MODEL.ENUM_Enu c, string d)
        {
        }

        public static void barProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object a, object b, out object res)
        {
            res = a ?? b ?? null;
        }

        public static void isnullProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object a, out bool res)
        {
            res = a == null;
        }

        public static void blaProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IN a, GRGEN_MODEL.IE b, out bool res1, out bool res2)
        {
            res1 = a.b;
            res2 = !a.b;
            return;
        }

        public static void bloProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.INode a, GRGEN_LIBGR.IEdge b, out GRGEN_MODEL.IN res)
        {
            res = ((GRGEN_MODEL.ExternalAttributeEvaluationGraph)graph).CreateNodeN();
        }

        public static void harProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.Own a, GRGEN_MODEL.OwnPown b, out GRGEN_MODEL.OwnPown res1, out GRGEN_MODEL.Own res2, out GRGEN_MODEL.IN res3)
        {
            res1 = b;
            res2 = b;
            res3 = ((GRGEN_MODEL.ExternalAttributeEvaluationGraph)graph).CreateNodeN();
        }

        public static void hurProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPown a)
        {
        }

        public static void hurdurProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPownHome a)
        {
        }
    }
}

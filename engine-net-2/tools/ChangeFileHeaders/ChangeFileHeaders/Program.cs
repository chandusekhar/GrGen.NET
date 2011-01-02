/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//todo: auch ANTLR-header umschreiben - dazu .g-Datei, 2te Zeile nach header { verarbeiten

using System;
using System.Text;
using System.IO;

namespace ChangeFileHeaders
{
    class Program
    {
        static void Main(string[] args)
        {
            String rootDirectory = args[0];
            DirectoryInfo dir = new DirectoryInfo(rootDirectory);
            Console.Write(ProcessFilesInDirectoryThenDescend(dir));
        }

        private static string ProcessFilesInDirectoryThenDescend(DirectoryInfo dir)
        {
            StringBuilder output = new StringBuilder();
            int nothingWasProcessedInThisDirectoryStringSize;

            ++indentLevel;

            for (int i = 0; i < indentLevel; ++i)
                output.Append("  ");
            output.AppendFormat("/{0}\n", dir.Name);
            nothingWasProcessedInThisDirectoryStringSize = output.Length;

            // handle files in current directory
            FileInfo[] filesInDirectory = dir.GetFiles();
            foreach (FileInfo file in filesInDirectory)
            {
                Encoding encoding = Encoding.Default;

                // we're only interested in Java and C# source files
                if (!(file.Name.EndsWith(".java") || file.Name.EndsWith(".cs")))
                    continue;

                // assembly info encoded in unicode
                if (file.Name == "AssemblyInfo.cs")
                    encoding = Encoding.UTF8;

                // log files processed to the console
                for (int i = 0; i < indentLevel; ++i)
                    output.Append("  ");
                output.AppendFormat("+{0} ... ", file.Name);

                // filter out files not containing the header to change
                if (!containsHeader(file))
                {
                    output.AppendLine("NO HEADER");
                    continue;
                }

                output.Append("REWRITING ... ");

                // change the file header to the new one
                rewriteHeader(file, encoding, output);
            }

            // descend to nested directories
            DirectoryInfo[] nestedDirectories = dir.GetDirectories();
            foreach (DirectoryInfo nestedDirectory in nestedDirectories)
            {
                if (nestedDirectory.Name.EndsWith(".svn"))
                    continue;

                output.Append(ProcessFilesInDirectoryThenDescend(nestedDirectory));
            }

            --indentLevel;

            if (output.Length == nothingWasProcessedInThisDirectoryStringSize) return "";
            return output.ToString();
        }

        private static bool containsHeader(FileInfo file)
        {
            StreamReader reader = file.OpenText();
            string line = reader.ReadLine();
            if (line == null || !line.StartsWith("/*")) goto close_and_fail;
            line = reader.ReadLine();
            if (line == null || !line.Contains("GrGen: graph rewrite generator")) goto close_and_fail;
            line = reader.ReadLine();
            if (line == null || !line.Contains("Copyright (C)")) goto close_and_fail;
            line = reader.ReadLine();
            if (line == null || !line.Contains("licensed under ")) goto close_and_fail;
            line = reader.ReadLine();
            if(line == null || !line.Contains("www.grgen.net")) goto close_and_fail;
            line = reader.ReadLine();
            if(line == null || !line.EndsWith("*/")) goto close_and_fail;
            reader.Close();
            return true;
        close_and_fail:
            reader.Close();
            return false;
        }

        private static bool containsIsGeneratedHeader(FileInfo file)
        {
            StreamReader reader = file.OpenText();
            string line = reader.ReadLine();
            if (line == null) 
            {
                reader.Close();
                return false;
            }
            if (line == "// This file has been generated automatically by GrGen."
                || line.StartsWith("/* Generated By:CSharpCC:")
                || (line.StartsWith("// $ANTLR") && line.Contains("->")))
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }

        private static void rewriteHeader(FileInfo file, Encoding encoding, StringBuilder output)
        {
            string[] lines = File.ReadAllLines(file.FullName, encoding);

            lines[0] = "/*";
            lines[1] = " * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7";
            lines[2] = " * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos";
            lines[3] = " * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)";
            lines[4] = " * www.grgen.net";
            lines[5] = " */";

            if (file.Name == "AssemblyInfo.cs")
            {
                output.Append("ASSEMBLY INFOS ... ");

                for (int i = 0; i < lines.Length; ++i)
                {
                    if (lines[i].StartsWith("[assembly: AssemblyVersion("))
                        lines[i] = "[assembly: AssemblyVersion(\"2.7.0.0\")]";
                    if (lines[i].StartsWith("[assembly: AssemblyInformationalVersionAttribute("))
                        lines[i] = "[assembly: AssemblyInformationalVersionAttribute(\"GrGen.NET 2.7\")]";
                    if (lines[i].StartsWith("[assembly: AssemblyFileVersion("))
                        lines[i] = "[assembly: AssemblyFileVersion(\"2.7.0.0\")]";
                    if (lines[i].StartsWith("[assembly: AssemblyCopyright("))
                        lines[i] = "[assembly: AssemblyCopyright(\"Copyright © 2003-2011 Universitšt Karlsruhe, IPD Goos\")]";
                    if (lines[i].StartsWith("[assembly: AssemblyCompany("))
                        lines[i] = "[assembly: AssemblyCompany(\"Universitšt Karlsruhe, IPD Goos\")]";
                }
            }

            File.WriteAllLines(file.FullName, lines, encoding);

            output.AppendLine("DONE");
        }

        private static void extendingRewriteHeader(FileInfo file, Encoding encoding, StringBuilder output)
        {
            const long NUM_LINES_TO_ADD = 6+1;
            string[] lines = File.ReadAllLines(file.FullName, encoding);
            string[] extendedLines = new string[lines.Length + NUM_LINES_TO_ADD];

            lines.CopyTo(extendedLines, NUM_LINES_TO_ADD);
            
            extendedLines[0] = "/*";
            extendedLines[1] = " * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7";
            extendedLines[2] = " * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos";
            extendedLines[3] = " * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)";
            extendedLines[4] = " * www.grgen.net";
            extendedLines[5] = " */";

            extendedLines[6] = "";

            File.WriteAllLines(file.FullName, extendedLines, encoding);

            output.AppendLine("DONE");
        }

        static int indentLevel = -1; // directory depth indentation level for output of state to console
    }
}

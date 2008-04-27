/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universit�t Karlsruhe, Institut f�r Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using System.IO;

namespace grIO
{
    public class Infrastructure
    {
        public static void Flush(IGraph g)
        {
            FileIO fio = new FileIO(g);
            fio.FlushFiles();
        }

        public static void MsgToConsole(string msg)
        {
            Console.WriteLine(msg);
        }

        public const string GraphModelDefinition =
@"node class grIO_OUTPUT {
    timestamp : int;
}

node class grIO_File {
    path : string;
}

edge class grIO_CreateOrAppend
    connect grIO_OUTPUT [0:1] -> grIO_File [0:*];

edge class grIO_CreateOrOverwrite
    connect grIO_OUTPUT [0:1] -> grIO_File [0:*];

node class grIO_File_Line {
    content : string;
}

edge class grIO_File_ContainsLine
    connect grIO_File [0:1] -> grIO_File_Line [0:*];

edge class grIO_File_NextLine
    connect grIO_File_Line [0:1] -> grIO_File_Line [0:1];
";
    }
}

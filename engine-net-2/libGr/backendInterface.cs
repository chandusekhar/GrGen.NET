/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System.Collections.Generic;
using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Flags which determine, how the specification file should be processed.
    /// </summary>
    [Flags]
    public enum ProcessSpecFlags
    {
        /// <summary>
        /// Do not use existing files.
        /// </summary>
        UseNoExistingFiles = 0,

        /// <summary>
        /// Only use existing C# files generated by the Java frontend.
        /// </summary>
        UseJavaGeneratedFiles = 1,

        /// <summary>
        /// Use all existing C# files.
        /// </summary>
        UseAllGeneratedFiles = 3,

        /// <summary>
        /// Mask for flags specifying whether and how already existing C# files shall be used while processing a specification.
        /// </summary>
        UseExistingMask = 3,

        /// <summary>
        /// Do not delete generated C# files.
        /// </summary>
        KeepGeneratedFiles = 4,

        /// <summary>
        /// Include debug information in the generated assemblies.
        /// </summary>
        CompileWithDebug = 8,

        /// <summary>
        /// Do not process intermediate actions file (stops after model has been processed).
        /// </summary>
        NoProcessActions = 16,

        /// <summary>
        /// Do not compile the generated final actions file.
        /// </summary>
        NoCreateActionsAssembly = 32,

        /// <summary>
        /// Do not fire any events in the generated code. Used for optimization.
        /// </summary>
        NoEvents = 64,

        /// <summary>
        /// Do not try to update the performance info object. Used for optimization.
        /// </summary>
        NoPerformanceInfoUpdates = 128
    }

    /// <summary>
    /// A helper class for backend independent graph and rule handling.
    /// </summary>
    public interface IBackend
    {
        /// <summary>
        /// The name of the backend.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Enumerates the names of the arguments which can be optionally passed to the create/open functions.
        /// Not currently used...
        /// </summary>
        IEnumerable<String> ArgumentNames { get; }

        /// <summary>
        /// Creates a new IGraph backend instance with the given graph model and name.
        /// </summary>
        /// <param name="graphModel">An IGraphModel instance.</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="parameters">Backend specific parameters.</param>
        /// <returns>The new IGraph backend instance.</returns>
        IGraph CreateGraph(IGraphModel graphModel, String graphName, params String[] parameters);

        /// <summary>
        /// Creates a new IGraph backend instance with the graph model provided by the graph model file and a name.
        /// </summary>
        /// <param name="modelFilename">Filename of a graph model file.</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="parameters">Backend specific parameters.</param>
        /// <returns>The new IGraph backend instance.</returns>
        IGraph CreateGraph(String modelFilename, String graphName, params String[] parameters);

        /// <summary>
        /// Creates a new IGraph and BaseActions backend instance from the specified specification file.
        /// If neccessary, any processing steps are performed automatically.
        /// </summary>
        /// <param name="grgFilename">Filename of the rule specification file (.grg).</param>
        /// <param name="graphName">Name of the new graph.</param>
        /// <param name="newGraph">Returns the new graph.</param>
        /// <param name="newActions">Returns the new BaseActions object.</param>
        /// <exception cref="System.Exception">Thrown when something goes wrong.</exception>
        void CreateFromSpec(String grgFilename, String graphName, out IGraph newGraph, out BaseActions newActions);

        /// <summary>
        /// Creates a new IGraph instance from the specified specification file.
        /// If the according dll does not exist or is out of date, the needed processing steps are performed automatically.
        /// </summary>
        /// <param name="gmFilename">Filename of the model specification file (.gm).</param>
        /// <param name="graphName">Name of the new graph.</param>
        /// <exception cref="System.IO.FileNotFoundException">Thrown, when a needed specification file does not exist.</exception>
        /// <exception cref="System.Exception">Thrown, when something goes wrong.</exception>
        /// <returns>The new IGraph backend instance.</returns>
        IGraph CreateFromSpec(String gmFilename, String graphName);

        /// <summary>
        /// Opens an existing graph identified by graphName using the specified IGraphModel.
        /// This may not be supported by the backend, if the data is not persistent.
        /// </summary>
        /// <param name="modelFilename">Filename of a graph model file</param>
        /// <param name="graphName">Name of an existing graph</param>
        /// <param name="parameters">Backend specific parameters</param>
        /// <returns>The IGraph backend instance</returns>
        IGraph OpenGraph(String modelFilename, String graphName, params String[] parameters);

        /// <summary>
        /// An enumerable of KeyValuePairs, where the keys are names of existing graphs and the
        /// values are the names of the appropriate models (not filenames).
        /// </summary>
        IEnumerable<KeyValuePair<String, String>> ExistingGraphs { get; }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <param name="destDir">The directory, where the generated libraries are to be placed.</param>
        /// <param name="intermediateDir">A directory, where intermediate files can be placed.</param>
        /// <param name="flags">Specifies how the specification is to be processed.</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        void ProcessSpecification(String specPath, String destDir, String intermediateDir, ProcessSpecFlags flags);
        
        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        void ProcessSpecification(String specPath);
    }
}

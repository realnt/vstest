// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.TestPlatform.Common.DataCollector
{
    using System;
    using System.Reflection;
#if !NET46
    using System.Runtime.Loader;
#endif
    using Microsoft.VisualStudio.TestPlatform.Common.DataCollector.Interfaces;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
    using System.Linq;

    /// <inheritdoc/>
    internal class DataCollectorLoader : IDataCollectorLoader
    {
        /// <inheritdoc/>
        public DataCollector Load(string location, string assemblyQualifiedName)
        {
            DataCollector dataCollectorInstance = null;
            Assembly assembly = null;

            try
            {
#if NET46
                assembly = Assembly.LoadFrom(location);
#else
                assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(location);
#endif
                var dataCollectorType = assembly.GetTypes().Where(t => t.AssemblyQualifiedName.Equals(assemblyQualifiedName)).FirstOrDefault();
                dataCollectorInstance = Activator.CreateInstance(dataCollectorType) as DataCollector;
            }
            catch (Exception ex)
            {
                if (EqtTrace.IsErrorEnabled)
                {
                    EqtTrace.Error("DataCollectorLoader.Load: Failed to load datacollector of type : {0} from location : {1}. Error : ", assemblyQualifiedName, location, ex.Message);
                }
            }

            return dataCollectorInstance;
        }
    }
}
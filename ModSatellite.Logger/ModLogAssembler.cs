using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.ObjectBuilder2;

namespace ModSatellite.Logger
{
    class ModLogAssembler : TraceListenerAsssembler
    {
        public override TraceListener Assemble(IBuilderContext context, TraceListenerData objectConfiguration, IConfigurationSource configurationSource,
            ConfigurationReflectionCache reflectionCache)
        {
            return new ModLoader();
        }
    }
}

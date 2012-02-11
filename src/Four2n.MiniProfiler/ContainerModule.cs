﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerModule.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) 2008 Daniel Dabrowski - 42n. All rights reserved.
// </copyright>
// <summary>
//   Defines the ContainerModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Orchard.MiniProfiler
{
    using Autofac;

    using Four2n.Orchard.MiniProfiler.Data;

    using global::Orchard.Environment;

    using StackExchange.Profiling;
    using StackExchange.Profiling.SqlFormatters;

    using Module = Autofac.Module;

    public class ContainerModule : Module
    {
        private readonly IOrchardHost orchardHost;

        public ContainerModule(IOrchardHost orchardHost)
        {
            this.orchardHost = orchardHost;
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            InitProfilerSettings();
            var currentLogger = ((DefaultOrchardHost)this.orchardHost).Logger;
            if (currentLogger is OrchardHostProxyLogger)
            {
                return;
            }

            ((DefaultOrchardHost)this.orchardHost).Logger = new OrchardHostProxyLogger(currentLogger);
        }

        private static void InitProfilerSettings()
        {
            MiniProfiler.Settings.SqlFormatter = new PoorMansTSqlFormatter();
            WebRequestProfilerProvider.Settings.UserProvider = new IpAddressIdentity();
        }
    }
}
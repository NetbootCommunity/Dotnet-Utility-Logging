using Netboot.Logging.Models;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netboot.Logging.Extensions
{
    public static class LoggerEnrichmentExtensions
    {
        /// <summary>
        /// Enrich with the default implementations.
        /// </summary>
        /// <param name="enrichmentConfiguration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerEnrichmentConfiguration WithDefault(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));

            enrichmentConfiguration.FromLogContext();
            enrichmentConfiguration.WithClientIp();
            enrichmentConfiguration.WithClientAgent();

            return enrichmentConfiguration;
        }

        /// <summary>
        /// Enrich with the custom implementation.
        /// </summary>
        /// <param name="enrichmentConfiguration"></param>
        /// <returns></returns>
        public static LoggerEnrichmentConfiguration WithCustomImplementation(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            var enricherTypes = GetTypesWithInterface<ICustomLogEventEnricher>();
            foreach (var enricherType in enricherTypes)
            {
                var customEnricher = (ILogEventEnricher)Activator.CreateInstance(enricherType);
                enrichmentConfiguration.With(customEnricher);
            }
            return enrichmentConfiguration;
        }

        /// <summary>
        /// Retrieve all implementations of the selected type.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        private static List<Type> GetTypesWithInterface<TType>()
        {
            var type = typeof(TType);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            return types.ToList();
        }
    }
}
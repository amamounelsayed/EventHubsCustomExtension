using EventHubsCustomExtension;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: WebJobsStartup(typeof(EventHubsCustomExtensionStartup))]

namespace EventHubsCustomExtension
{
    public class EventHubsCustomExtensionStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            // if this extension is registered AFTER the base extension
            // it's converters will replace the built in converters
            // (ConverterManager logic is replace/last one wins)
            builder.AddExtension<EventHubCustomExtensionConfigProvider>();
        }
    }
}

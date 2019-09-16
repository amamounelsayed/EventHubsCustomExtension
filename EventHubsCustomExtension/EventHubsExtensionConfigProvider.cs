using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs.EventHubs;
using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json.Linq;

namespace EventHubsCustomExtension
{
    internal class EventHubCustomExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddConverter<byte[], EventData>(ConvertBytesToEventData);
        }

        private static EventData ConvertBytesToEventData(byte[] bytes)
        {
            string input = Encoding.UTF8.GetString(bytes);

            if (IsJsonObject(input))
            {
                try
                {
                    // attempt to parse as an EventDataEx
                    JObject o = JObject.Parse(input);
                    var partitionKey = (string)o.GetValue("PartitionKey", StringComparison.OrdinalIgnoreCase)?.Value<string>();

                    
                    if (!string.IsNullOrEmpty(partitionKey))
                    {
                        byte[] body = o.GetValue("Body", StringComparison.OrdinalIgnoreCase)?.ToObject<byte[]>();
                        return new EventDataEx(body)
                        {
                            PartitionKey = partitionKey
                        };
                    }
                }
                catch
                {
                    // best effort
                }
            }

            // return default EventData
            return new EventData(bytes);
        }

        private static bool IsJsonObject(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = input.Trim();
            return (input.StartsWith("{", StringComparison.OrdinalIgnoreCase) && input.EndsWith("}", StringComparison.OrdinalIgnoreCase));
        }
    }
}

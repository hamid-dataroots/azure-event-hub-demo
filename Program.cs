using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace _01_SendEvents
{
    class Program
    {
        private const string connectionString = "your_connection_string";
        private const string eventHubName = "event_hub_name";

        static async Task Main()
        {
            // Create a producer client that you can use to send events to an event hub
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                // Create a batch of events 
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("1 event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("2 event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("3 event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("4 event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("5 event")));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("6 event")));

                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("A batch of 6 events has been published.");
            }
        }
    }
}
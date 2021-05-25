using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Newtonsoft.Json;
using Encoding = System.Text.Encoding;

namespace IdentityServer.PubSub
{
    public static class PubSubService
    {
        public static async Task Publish(string topic, object message)
        {
            var publisher = await PublisherClient.CreateAsync(TopicName.FromProjectTopic("kwetter-308618", topic));
            await publisher.PublishAsync(JsonConvert.SerializeObject(message));
        }

        public static Task Subscribe(string subscriptionId)
        {
            var subscriptionName = SubscriptionName.FromProjectSubscription("kwetter-308618", subscriptionId);
            var subscription = SubscriberClient.CreateAsync(subscriptionName).Result;

            return subscription.StartAsync((message, _) =>
            {
                var json = Encoding.UTF8.GetString(message.Data.ToArray());
                Console.WriteLine($"Message {message.MessageId}: {json}");
                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }
    }
}

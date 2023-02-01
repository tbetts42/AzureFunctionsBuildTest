using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionsBuildTest;

public class ServiceBusHandler
{
    private readonly ILogger<ServiceBusHandler> _logger;
    public ServiceBusHandler(ILogger<ServiceBusHandler> logger)
    {
        _logger = logger;
    }

    [Disable(typeof(TestDisablementProvider))]
    [FunctionName("HandleFepApyCodeTablesMessage")]
    public async Task HandleFepApyCodeTablesMessage(
        [ServiceBusTrigger("my-sb-topic", "my-sb-subscription", Connection = $"ServiceBus:my-sb-topic:Listen")]
        ServiceBusReceivedMessage message)
    {
        _logger.LogInformation("{Topic} message received {@Message}",
                               "my-sb-topic",
                               message);
        await Task.CompletedTask;
    }
}

using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Threading.Tasks;

public class MyHandler :
    IHandleMessages<MyMessage>
{
    readonly ILogger<MyHandler> logger;

    public MyHandler(ILogger<MyHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
       logger.LogInformation($"Message received. Id: {message.Id}");
       throw new ArgumentNullException("Uh oh - something went wrong....");
       //throw new DivideByZeroException("DivideByZeroException - something went wrong....");
       //return Task.CompletedTask;
    }
}

using Microsoft.Extensions.Hosting;
using NServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

Console.Title = "Server";

var builder = Host.CreateApplicationBuilder(args);

var endpointConfiguration = new EndpointConfiguration("Server");
var routing = endpointConfiguration.UseTransport(new LearningTransport());
endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.DefineCriticalErrorAction(OnCriticalError);

var recoverability = endpointConfiguration.Recoverability();

recoverability.CustomPolicy(CustomRecoverabilityPolicy.Invoke);

builder.UseNServiceBus(endpointConfiguration);

var app = builder.Build();

app.Run();

static async Task OnCriticalError(ICriticalErrorContext context, CancellationToken cancellationToken)
{
    var fatalMessage =
           $"The following critical error was encountered:{Environment.NewLine}{context.Error}{Environment.NewLine}Process is shutting down. StackTrace: {Environment.NewLine}{context.Exception.StackTrace}";

    try
    {
        await context.Stop(cancellationToken);
    }
    finally
    {
        Environment.FailFast(fatalMessage, context.Exception);
    }
}


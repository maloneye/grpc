using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Grpc.Server;

public class Host : IHostedService
{
    private readonly WebApplication _host;
    
    public Host()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddGrpc();

        _host = builder.Build();
        _host.MapGrpcService<SimpleService>();
    }

    public Task StartAsync(CancellationToken ct) => _host.StartAsync(ct);

    public Task StopAsync(CancellationToken ct) => _host.StopAsync(ct);
}
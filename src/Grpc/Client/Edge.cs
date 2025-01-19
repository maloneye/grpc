using Grpc.Net.Client;

namespace Grpc.Client;

public class Edge : IDisposable
{
    private readonly GrpcChannel _channel;
    
    public Edge(string host)
    {
        var handler = new SocketsHttpHandler
        {
            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            EnableMultipleHttp2Connections = true,
        };

        var options = new GrpcChannelOptions
        {
            HttpHandler=handler,
        };
        
        _channel = GrpcChannel.ForAddress(host);
    }

    public void Dispose() => _channel.Dispose();

    public async Task<IReadOnlyList<ISkill>> RequestSkill(Employee employee)
    {
        var client = new Simple.SimpleClient(_channel);
        var reply = await client.RequestAsync(employee.Map());

        return reply.Map();
    }
}
namespace Infrastructure.Shared.Handlers;

public class TestHttpClientMessageHandler(HttpResponseMessage httpResponseMessage) : DelegatingHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return httpResponseMessage;
    }

    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return httpResponseMessage;
    }
}

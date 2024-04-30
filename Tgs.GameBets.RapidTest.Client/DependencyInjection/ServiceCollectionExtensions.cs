using Tgs.GameBets.RapidTest.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTestClient(this IServiceCollection services, Action<IHttpClientBuilder> httpClientConfigure)
	{
		httpClientConfigure(services.AddHttpClient<IGameBetsRapidTestClient, GameBetsRapidTestClient>());

		return services;
	}
}

using Tgs.GameBets.RapidTest;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTest(
		this IServiceCollection services,
		Action<MongoDBOptions, IServiceProvider> mongoDBconfigure,
		Action<RedisOptions, IServiceProvider> redisOptions)
		=> services
		.AddOptions<RedisOptions>()
		.Configure(redisOptions)
		.Services
		.AddOptions<MongoDBOptions>()
		.Configure(mongoDBconfigure)
		.Services
		.AddSingleton<RedisConnectionManager>()
		.AddSingleton<MongoDBConnectionManager>()
		.AddGameBetsRapidTestCore()
		.AddGameBetsRapidTestRepositories(
			mongoClientResolver: (sp) => sp.GetRequiredService<MongoDBConnectionManager>().MongoClient,
			redisDatabaseResolver: (sp) => sp.GetRequiredService<RedisConnectionManager>().Database);
}

using MongoDB.Driver;
using StackExchange.Redis;
using Tgs.GameBets.RapidTest.Core;
using Tgs.GameBets.RapidTest.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameBetsRapidTestRepositories(
		this IServiceCollection services,
		Func<IServiceProvider, IMongoClient> mongoClientResolver,
		Func<IServiceProvider, IDatabase> redisDatabaseResolver)
		=> services
		.AddTransient<IGameBetRapidTestRepository>((sp) => ActivatorUtilities.CreateInstance<GameBetRapidTestRepository>(sp, mongoClientResolver(sp)))
		.AddTransient<IGameBetRapidTestExpireLockedRepository>(sp => ActivatorUtilities.CreateInstance<GameBetRapidTestExpireLockedRepository>(sp, redisDatabaseResolver(sp)));
}

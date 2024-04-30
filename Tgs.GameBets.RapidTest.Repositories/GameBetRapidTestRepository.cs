using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Polly;
using Tgs.GameBets.RapidTest.Core;
using Tgs.GameBets.RapidTest.Core.Models;
using Tgs.GameBets.RapidTest.Repositories.Extensions;
using Tgs.GameBets.RapidTest.Repositories.Models;

namespace Tgs.GameBets.RapidTest.Repositories;

internal class GameBetRapidTestRepository(
	ILogger<GameBetRapidTestRepository> logger,
	IOptions<PolicySettingsOptions> policySettingsOptions,
	IOptions<ExpireStringsOptions> expireStringsOptions,
	IMongoClient client) : IGameBetRapidTestRepository
{
	private readonly ExpireStringsOptions _expireStringsOptions = expireStringsOptions.Value;
	private readonly PolicySettingsOptions _policySettingsOptions = policySettingsOptions.Value;

	public async ValueTask<bool> MergeAsync(GameBetRapidTestMerge source, CancellationToken cancellationToken = default)
	{
		var collection = GetCollection(source.GameProviderCode);

		var filter = Builders<GameBetRapidTestModel>.Filter.Eq(x => x.GameProviderCode, source.GameProviderCode)
			& Builders<GameBetRapidTestModel>.Filter.Eq(x => x.GameId, source.GameId)
			& Builders<GameBetRapidTestModel>.Filter.Eq(x => x.WalletId, source.WalletId)
			& Builders<GameBetRapidTestModel>.Filter.Eq(x => x.ExternalBetId, source.ExternalBetId);

		var update = Builders<GameBetRapidTestModel>.Update
			.Set(s => s.GameProviderCode, source.GameProviderCode)
			.Set(s => s.SubGameProviderCode, source.SubGameProviderCode)
			.Set(s => s.GameId, source.GameId)
			.Set(s => s.ResellerCode, source.ResellerCode)
			.Set(s => s.LicenseeCode, source.LicenseeCode)
			.Set(s => s.BrandCode, source.BrandCode)
			.Set(s => s.PlayerCode, source.PlayerCode)
			.Set(s => s.Username, source.Username)
			.Set(s => s.CurrencyCode, source.CurrencyCode)
			.Set(s => s.WalletId, source.WalletId)
			.Set(s => s.ExternalRoundId, source.ExternalRoundId)
			.Set(s => s.ExternalBetId, source.ExternalBetId)
			.Set(s => s.ExternalGameId, source.ExternalGameId)
			.Set(s => s.BetState, source.BetState)
			.Set(s => s.BetType, source.BetType)
			.Set(s => s.FreeGameState, source.FreeGameState)
			.Set(s => s.GambleState, source.GambleState)
			.Set(s => s.CashoutState, source.CashoutState)
			.Set(s => s.ResettleState, source.ResettleState)
			.Set(s => s.JackpotState, source.JackpotState)
			.Set(s => s.CurrencyRatio, source.CurrencyRatio)
			.Set(s => s.Bet, source.Bet)
			.Set(s => s.ValidBet, source.ValidBet)
			.Set(s => s.Settle, source.Settle)
			.Set(s => s.Commission, source.Commission)
			.Set(s => s.Turnover, source.Turnover)
			.Set(s => s.WinLoss, source.WinLoss)
			.Set(s => s.Jackpot, source.Jackpot)
			.Set(s => s.AdditionalData, source.AdditionalData);

		var options = new UpdateOptions
		{
			IsUpsert = true
		};

		var retryCount = _policySettingsOptions.RetryCount;
		var retrySeconds = _policySettingsOptions.RetrySeconds;

		var retryPolicy = Policy
		   .Handle<MongoBulkWriteException>()
		   .WaitAndRetryAsync(
			retryCount: retryCount,
			retryAttempt => TimeSpan.FromSeconds(Math.Pow(retrySeconds, retryAttempt)),
			onRetry: (exception, retryTime, retryCount, context) => logger.LogWarning("LogOn:{logOn} Exception:{exception} RetryTime:{retryTime} RetryCount:{retryCount}",
				DateTime.Now.ToString("o"),
				exception.Message,
				retryTime,
				retryCount));

		var updateResult = await retryPolicy.ExecuteAsync(() => collection.UpdateOneAsync(
				filter: filter,
				update: update,
				options: options,
				cancellationToken: cancellationToken))
			.ConfigureAwait(false);

		return updateResult.UpsertedId is not null || updateResult.MatchedCount != 1 || updateResult.ModifiedCount != 0;
	}

	public async ValueTask InitializationAsync(string gameProviderCode, CancellationToken cancellationToken = default)
	{
		var collection = GetCollection(gameProviderCode);

		var tasks = new Task[]{
			Task.Run(async () =>
			{
				try
				{
					_ = await Index_UniqueAsync( collection: collection,   cancellationToken: cancellationToken).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "LogOn:{logOn} Message:This index creation failed.{index}",  DateTime.Now.ToString("u"),"Index_Unique");
				}
			}, cancellationToken: cancellationToken),
			Task.Run(async () =>
			{
				try
				{
					 _ = await Index_ExpireOnAsync( collection: collection,   cancellationToken: cancellationToken).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "LogOn:{logOn} Message:This index creation failed.{index}",  DateTime.Now.ToString("u"),"Index_ExpireOn");
				}
			}, cancellationToken: cancellationToken),
			Task.Run(async () =>
			{
				try
				{
					_ = await Index_RapidTestOnAsync( collection: collection,   cancellationToken: cancellationToken).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "LogOn:{logOn} Message:This index creation failed.{index}",  DateTime.Now.ToString("u"),"Index_RapidTestOn");
				}
			}, cancellationToken: cancellationToken)
		};

		await Task.WhenAll(tasks).ConfigureAwait(false);
	}

	public async ValueTask<long> SetExpireOnAsync(string gameProviderCode, CancellationToken cancellationToken = default)
	{
		var collection = GetCollection(gameProviderCode);

		var filter = Builders<GameBetRapidTestModel>.Filter.Exists("expire_on", false)
			& Builders<GameBetRapidTestModel>.Filter.Eq("expire_on", BsonNull.Value);

		var update = Builders<GameBetRapidTestModel>.Update
			.Set(s => s.ExpireOn, DateTime.UtcNow.AddMinutes(_expireStringsOptions.ExpireMins));

		var updateResult = await collection.UpdateManyAsync(
			filter: filter,
			update: update,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return updateResult.ModifiedCount;
	}

	private static Task<string> Index_ExpireOnAsync(IMongoCollection<GameBetRapidTestModel> collection, CancellationToken cancellationToken = default)
	{
		var index = Builders<GameBetRapidTestModel>.IndexKeys
			.Ascending(m => m.ExpireOn);

		var createIndexOptions = new CreateIndexOptions
		{
			ExpireAfter = TimeSpan.FromSeconds(1),
			Name = "idx_expire_on"
		};
		var createIndexModel = new CreateIndexModel<GameBetRapidTestModel>(index, createIndexOptions);

		return collection.Indexes.CreateOneAsync(
			model: createIndexModel,
			cancellationToken: cancellationToken);
	}

	private static Task<string> Index_RapidTestOnAsync(IMongoCollection<GameBetRapidTestModel> collection, CancellationToken cancellationToken = default)
	{
		var index = Builders<GameBetRapidTestModel>.IndexKeys
			.Ascending(m => m.RapidTestOn);

		var createIndexOptions = new CreateIndexOptions
		{
			Name = "idx_rapid_test_on"
		};
		var createIndexModel = new CreateIndexModel<GameBetRapidTestModel>(index, createIndexOptions);

		return collection.Indexes.CreateOneAsync(
			model: createIndexModel,
			cancellationToken: cancellationToken);
	}

	private static Task<string> Index_UniqueAsync(IMongoCollection<GameBetRapidTestModel> collection, CancellationToken cancellationToken = default)
	{
		var index = Builders<GameBetRapidTestModel>.IndexKeys
			.Ascending(m => m.GameProviderCode)
			.Ascending(m => m.GameId)
			.Ascending(m => m.WalletId)
			.Ascending(m => m.ExternalBetId);

		var createIndexOptions = new CreateIndexOptions
		{
			Unique = true,
			Name = $"uni_{collection.CollectionNamespace.CollectionName}"
		};
		var createIndexModel = new CreateIndexModel<GameBetRapidTestModel>(index, createIndexOptions);

		return collection.Indexes.CreateOneAsync(
			model: createIndexModel,
			cancellationToken: cancellationToken);
	}

	private IMongoCollection<GameBetRapidTestModel> GetCollection(string gameProviderCode)
						=> client
		.GetDatabase($"game_provider_{gameProviderCode}_master".ToLower())
		.GetCollection<GameBetRapidTestModel>(typeof(GameBetRapidTestModel).GetTableName());
}

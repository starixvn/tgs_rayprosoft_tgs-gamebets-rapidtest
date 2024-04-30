using System.Text.Json.Nodes;

using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameBets.RapidTest.Client;
using Tgs.GameBets.RapidTest.Core;
using Tgs.GameBets.RapidTest.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.IntegrationUnitTest;

public class GameBetsRapidTestUnitTest
{
	[Fact]
	public async Task GameBetRapidTest_Command_Waiting()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Waiting,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Waiting,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Running()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Running,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Running,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Void()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Void,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 0.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Void,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Cancel()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Cancel,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 0.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Cancel,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Refund()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Refund,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 0.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Refund,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Refund_Cashout()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Refund,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.Cashout,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 0.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Refund,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.Cashout,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Loss()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Loss,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 0.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Loss,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Win()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Win,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 8.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Win,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Win_FreeGame()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Win,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.FreeGame,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 8.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Win,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.FreeGame,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Win_Jackpot()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Win,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.Jackpot,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 12.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: 4.0m,
			Jackpot: 4.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Win,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.Jackpot,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_Draw()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.Draw,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 0.0m,
			Settle: 4.0m,
			Commission: 0.0m,
			Turnover: 0.0m,
			WinLoss: 0.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.Draw,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_HalfWin()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.HalfWin,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 2.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -2.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.HalfWin,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Command_HalfLoss()
	{
		var externalRoundId = Guid.NewGuid().ToString();
		var externalBetId = Guid.NewGuid().ToString();
		var externalCreatedOn = DateTimeOffset.UtcNow.AddMinutes(-5);
		var externalClosedOn = DateTimeOffset.UtcNow.AddSeconds(-5);
		var externalPayoffOn = DateTimeOffset.UtcNow;

		var source = new Common.ViewModels.GameBetRapidTestCommandViewModel(
			GameProviderCode: "KY",
			SubGameProviderCode: "KY",
			GameId: 1000017,
			ResellerCode: "TGS",
			LicenseeCode: "TGS",
			BrandCode: "GPI",
			PlayerCode: "4ucmqplxb029",
			Username: "4ucmqplxb029",
			CurrencyCode: "CNY",
			WalletId: Guid.NewGuid(),
			ExternalRoundId: "50-1691469296-370404138-1",
			ExternalBetId: "50-1691469296-370404138-1",
			ExternalGameId: "910",
			BetState: Common.Enums.BetState.HalfLoss,
			BetType: Common.Enums.BetType.Normal,
			FreeGameState: Common.Enums.FreeGameState.None,
			GambleState: Common.Enums.GambleState.None,
			CashoutState: Common.Enums.CashoutState.None,
			ResettleState: Common.Enums.ResettleState.None,
			JackpotState: Common.Enums.JackpotState.None,
			CurrencyRatio: 1.0m,
			Bet: 4.0m,
			ValidBet: 4.0m,
			Settle: 2.0m,
			Commission: 0.0m,
			Turnover: 4.0m,
			WinLoss: -2.0m,
			Jackpot: 0.0m,
			AdditionalData: new JsonObject
			{
				["externalRoundId"] = "50-1691469296-370404138-1"
			}.ToJsonString());

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameBetRapidTestCommand, bool>>();

			var command = new GameBetRapidTestCommand(
				GameProviderCode: source.GameProviderCode,
				SubGameProviderCode: source.SubGameProviderCode,
				GameId: source.GameId,
				ResellerCode: source.ResellerCode,
				LicenseeCode: source.LicenseeCode,
				BrandCode: source.BrandCode,
				PlayerCode: source.PlayerCode,
				Username: source.Username,
				CurrencyCode: source.CurrencyCode,
				WalletId: source.WalletId,
				ExternalRoundId: source.ExternalRoundId,
				ExternalBetId: source.ExternalBetId,
				ExternalGameId: source.ExternalGameId,
				BetState: GameBets.Common.Enums.BetState.HalfLoss,
				BetType: GameBets.Common.Enums.BetType.Normal,
				FreeGameState: GameBets.Common.Enums.FreeGameState.None,
				GambleState: GameBets.Common.Enums.GambleState.None,
				CashoutState: GameBets.Common.Enums.CashoutState.None,
				ResettleState: GameBets.Common.Enums.ResettleState.None,
				JackpotState: GameBets.Common.Enums.JackpotState.None,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData);
			_ = fakeService.GetAsync(
				query: Arg.Is(command),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient<IGetService<GameBetRapidTestCommand, bool>>(_ => fakeService);
		}));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		var actual = await client.GameBetRapidTestCommandAsync(source);

		Assert.True(actual);
	}

	[Fact]
	public async Task GameBetRapidTest_Expire()
	{
		var gameProviderCode = "KY";

		var fakeService = Substitute.For<ICommandService<GameBetRapidTestExpireCommand>>();

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services => _ = services.AddTransient(_ => fakeService)));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		await client.GameBetRapidTestExpireAsync(gameProviderCode);

		_ = fakeService
			.Received(1)
			.ExecuteAsync(
			command: Arg.Is(new GameBetRapidTestExpireCommand(
				GameProviderCode: gameProviderCode)),
			cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task GameBetRapidTest_Initialization()
	{
		var gameProviderCode = "KY";

		var fakeRepository = Substitute.For<IGameBetRapidTestRepository>();

		var web = new GameBetsRapidTestApplicationTest(builder => _ = builder.ConfigureServices(services => _ = services.AddTransient(_ => fakeRepository)));

		var httpClient = web.CreateDefaultClient();
		var client = new GameBetsRapidTestClient(httpClient);

		await client.GameBetRapidTestInitializationAsync(gameProviderCode);

		_ = fakeRepository
			.Received(1)
			.InitializationAsync(
			gameProviderCode: Arg.Is(gameProviderCode),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}

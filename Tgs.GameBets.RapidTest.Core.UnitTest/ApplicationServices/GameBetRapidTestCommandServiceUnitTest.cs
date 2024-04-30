using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameBets.RapidTest.Core.ApplicationServices;
using Tgs.GameBets.RapidTest.Core.Models;

namespace Tgs.GameBets.RapidTest.Core.UnitTest.ApplicationServices;

public class GameBetRapidTestCommandServiceUnitTest
{
	[Fact]
	public async Task Update_Is_True()
	{
		var fakeRepository = Substitute.For<IGameBetRapidTestRepository>();

		var command = new GameBetRapidTestCommand(
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

		var gameBetRapidTestMerge = new GameBetRapidTestMerge(
			GameProviderCode: command.GameProviderCode,
			SubGameProviderCode: command.SubGameProviderCode,
			GameId: command.GameId,
			ResellerCode: command.ResellerCode,
			LicenseeCode: command.LicenseeCode,
			BrandCode: command.BrandCode,
			PlayerCode: command.PlayerCode,
			Username: command.Username,
			CurrencyCode: command.CurrencyCode,
			WalletId: command.WalletId,
			ExternalRoundId: command.ExternalRoundId,
			ExternalBetId: command.ExternalBetId,
			ExternalGameId: command.ExternalGameId,
			BetState: command.BetState,
			BetType: command.BetType,
			FreeGameState: command.FreeGameState,
			GambleState: command.GambleState,
			CashoutState: command.CashoutState,
			ResettleState: command.ResettleState,
			JackpotState: command.JackpotState,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData);
		_ = fakeRepository
			.MergeAsync(
			source: Arg.Is(gameBetRapidTestMerge),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		var sut = new GameBetRapidTestCommandService(
			fakeRepository);

		var actual = await sut.GetAsync(command);

		Assert.True(actual);
	}

	[Fact]
	public async Task Update_Is_False()
	{
		var fakeRepository = Substitute.For<IGameBetRapidTestRepository>();

		var command = new GameBetRapidTestCommand(
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

		var gameBetRapidTestMerge = new GameBetRapidTestMerge(
			GameProviderCode: command.GameProviderCode,
			SubGameProviderCode: command.SubGameProviderCode,
			GameId: command.GameId,
			ResellerCode: command.ResellerCode,
			LicenseeCode: command.LicenseeCode,
			BrandCode: command.BrandCode,
			PlayerCode: command.PlayerCode,
			Username: command.Username,
			CurrencyCode: command.CurrencyCode,
			WalletId: command.WalletId,
			ExternalRoundId: command.ExternalRoundId,
			ExternalBetId: command.ExternalBetId,
			ExternalGameId: command.ExternalGameId,
			BetState: command.BetState,
			BetType: command.BetType,
			FreeGameState: command.FreeGameState,
			GambleState: command.GambleState,
			CashoutState: command.CashoutState,
			ResettleState: command.ResettleState,
			JackpotState: command.JackpotState,
			CurrencyRatio: command.CurrencyRatio,
			Bet: command.Bet,
			ValidBet: command.ValidBet,
			Settle: command.Settle,
			Commission: command.Commission,
			Turnover: command.Turnover,
			WinLoss: command.WinLoss,
			Jackpot: command.Jackpot,
			AdditionalData: command.AdditionalData);
		_ = fakeRepository
			.MergeAsync(
			source: Arg.Is(gameBetRapidTestMerge),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(false);

		var sut = new GameBetRapidTestCommandService(
			fakeRepository);

		var actual = await sut.GetAsync(command);

		Assert.False(actual);
	}
}

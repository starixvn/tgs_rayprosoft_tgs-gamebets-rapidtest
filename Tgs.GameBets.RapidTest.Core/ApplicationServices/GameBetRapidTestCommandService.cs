using System.Text.Json.Nodes;
using Tgs.GameBets.RapidTest.Core.Models;

using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.Core.ApplicationServices;

internal class GameBetRapidTestCommandService(
	IGameBetRapidTestRepository repository) : IGetService<GameBetRapidTestCommand, bool>
{
	public ValueTask<bool> GetAsync(GameBetRapidTestCommand query, CancellationToken cancellationToken = default)
	{
		var gameBetRapidTestMerge = new GameBetRapidTestMerge(
			GameProviderCode: query.GameProviderCode,
			SubGameProviderCode: query.SubGameProviderCode,
			GameId: query.GameId,
			ResellerCode: query.ResellerCode,
			LicenseeCode: query.LicenseeCode,
			BrandCode: query.BrandCode,
			PlayerCode: query.PlayerCode,
			Username: query.Username,
			CurrencyCode: query.CurrencyCode,
			WalletId: query.WalletId,
			ExternalRoundId: query.ExternalRoundId,
			ExternalBetId: query.ExternalBetId,
			ExternalGameId: query.ExternalGameId,
			BetState: query.BetState,
			BetType: query.BetType,
			FreeGameState: query.FreeGameState,
			GambleState: query.GambleState,
			CashoutState: query.CashoutState,
			ResettleState: query.ResettleState,
			JackpotState: query.JackpotState,
			CurrencyRatio: query.CurrencyRatio,
			Bet: query.Bet,
			ValidBet: query.ValidBet,
			Settle: query.Settle,
			Commission: query.Commission,
			Turnover: query.Turnover,
			WinLoss: query.WinLoss,
			Jackpot: query.Jackpot,
			AdditionalData: query.AdditionalData);

		return repository.MergeAsync(
			source: gameBetRapidTestMerge,
			cancellationToken: cancellationToken);
	}
}

using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Tgs.GameBets.RapidTest.Core;
using Tgs.GameBets.RapidTest.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.Controllers;

[Route("rapid-test")]
[ApiController]
public class RapidTestController : ControllerBase
{
	/// <summary>
	/// Games the bet rapid test command asynchronous.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b><br />
	///
	///     Post
	///			{
	///				"gameProviderCode": "KY",
	///				"subGameProviderCode": "KY",
	///				"gameId": 1000017,
	///				"resellerCode": "TGS",
	///				"licenseeCode": "TGS",
	///				"brandCode": "GPI",
	///				"playerCode": "4ucmqplxb029",
	///				"username": "4ucmqplxb029",
	///				"currencyCode": "CNY",
	///				"walletId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
	///				"externalRoundId": "50-1691469296-370404138-1",
	///				"externalBetId": "50-1691469296-370404138-1",
	///				"externalGameId": "910",
	///				"betState": "Win",
	///				"betType": "Normal",
	///				"freeGameState": "None",
	///				"gambleState": "None",
	///				"cashoutState": "None",
	///				"resettleState": "None",
	///				"jackpotState": "None",
	///				"currencyRatio": 1,
	///				"bet": 4.0,
	///				"validBet": 4.0,
	///				"settle": 8.0,
	///				"commission": 0.0,
	///				"turnover": 4.0,
	///				"winLoss": 4.0,
	///				"jackpot": 0,
	///				"additionalData": {
	///					"ExternalRoundId": "50-1691469296-370404138-1",
	///					"ExternalBetId": "50-1691469296-370404138-1"
	///				}
	///			}
	/// </remarks>
	[HttpPost(Name = "GameBet_RapidTest_Command")]
	public ValueTask<bool> GameBetRapidTestCommandAsync(
			[FromServices] IGetService<GameBetRapidTestCommand, bool> service,
			[FromBody] Common.ViewModels.GameBetRapidTestCommandViewModel source)
	{
		var betState = source.BetState switch
		{
			Common.Enums.BetState.Running => GameBets.Common.Enums.BetState.Running,
			Common.Enums.BetState.Void => GameBets.Common.Enums.BetState.Void,
			Common.Enums.BetState.Refund => GameBets.Common.Enums.BetState.Refund,
			Common.Enums.BetState.Cancel => GameBets.Common.Enums.BetState.Cancel,
			Common.Enums.BetState.Loss => GameBets.Common.Enums.BetState.Loss,
			Common.Enums.BetState.Win => GameBets.Common.Enums.BetState.Win,
			Common.Enums.BetState.Draw => GameBets.Common.Enums.BetState.Draw,
			Common.Enums.BetState.HalfWin => GameBets.Common.Enums.BetState.HalfWin,
			Common.Enums.BetState.HalfLoss => GameBets.Common.Enums.BetState.HalfLoss,
			_ => GameBets.Common.Enums.BetState.Waiting
		};

		var betType = source.BetType switch
		{
			Common.Enums.BetType.Normal => GameBets.Common.Enums.BetType.Normal,
			_ => GameBets.Common.Enums.BetType.Normal
		};

		var freeGameState = source.FreeGameState switch
		{
			Common.Enums.FreeGameState.FreeGame => GameBets.Common.Enums.FreeGameState.FreeGame,
			_ => GameBets.Common.Enums.FreeGameState.None
		};

		var gambleState = source.GambleState switch
		{
			Common.Enums.GambleState.Gamble => GameBets.Common.Enums.GambleState.Gamble,
			_ => GameBets.Common.Enums.GambleState.None
		};

		var cashoutState = source.CashoutState switch
		{
			Common.Enums.CashoutState.Cashout => GameBets.Common.Enums.CashoutState.Cashout,
			_ => GameBets.Common.Enums.CashoutState.None
		};

		var resettleState = source.ResettleState switch
		{
			Common.Enums.ResettleState.Resettle => GameBets.Common.Enums.ResettleState.Resettle,
			_ => GameBets.Common.Enums.ResettleState.None
		};

		var jackpotState = source.JackpotState switch
		{
			Common.Enums.JackpotState.Jackpot => GameBets.Common.Enums.JackpotState.Jackpot,
			_ => GameBets.Common.Enums.JackpotState.None
		};
		return service.GetAsync(
			query: new GameBetRapidTestCommand(
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
				BetState: betState,
				BetType: betType,
				FreeGameState: freeGameState,
				GambleState: gambleState,
				CashoutState: cashoutState,
				ResettleState: resettleState,
				JackpotState: jackpotState,
				CurrencyRatio: source.CurrencyRatio,
				Bet: source.Bet,
				ValidBet: source.ValidBet,
				Settle: source.Settle,
				Commission: source.Commission,
				Turnover: source.Turnover,
				WinLoss: source.WinLoss,
				Jackpot: source.Jackpot,
				AdditionalData: source.AdditionalData),
			cancellationToken: HttpContext.RequestAborted);
	}

	/// <summary>
	/// Games the bet rapid test expire asynchronous.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="gameProviderCode">The game provider code.</param>
	/// <returns></returns>
	[HttpPut("{gameProviderCode}/expire", Name = "GameBet_RapidTest_Expire")]
	public ValueTask GameBetRapidTestExpireAsync(
		[FromServices] ICommandService<GameBetRapidTestExpireCommand> service,
		string gameProviderCode)
		=> service.ExecuteAsync(
			command: new GameBetRapidTestExpireCommand(
				GameProviderCode: gameProviderCode),
			cancellationToken: HttpContext.RequestAborted);

	/// <summary>
	/// Games the bet rapid test initialization asynchronous.
	/// </summary>
	/// <param name="repository">The repository.</param>
	/// <param name="gameProviderCode">The game provider code.</param>
	/// <returns></returns>
	[HttpPost("{gameProviderCode}/init", Name = "GameBet-RapidTest-Initialization")]
	public ValueTask GameBetRapidTestInitializationAsync(
		[FromServices] IGameBetRapidTestRepository repository,
		string gameProviderCode)
		=> repository.InitializationAsync(
			gameProviderCode: gameProviderCode,
			cancellationToken: HttpContext.RequestAborted);
}

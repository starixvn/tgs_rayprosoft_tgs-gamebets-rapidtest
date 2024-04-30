using Microsoft.Extensions.Logging;
using Tgs.ServiceUtility;

namespace Tgs.GameBets.RapidTest.Core.ApplicationServices;

internal class GameBetRapidTestExpireCommandService(
	ILogger<GameBetRapidTestExpireCommandService> logger,
	IGameBetRapidTestExpireLockedRepository gameWagersLock,
	IGameBetRapidTestRepository repository) : ICommandService<GameBetRapidTestExpireCommand>
{
	public async ValueTask ExecuteAsync(GameBetRapidTestExpireCommand command, CancellationToken cancellationToken = default)
	{
		if (await gameWagersLock.LockedAsync(
			gameProviderCode: command.GameProviderCode,
			application: typeof(GameBetRapidTestExpireCommandService).Name,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false))
		{
			try
			{
				var momodifiedCount = await repository.SetExpireOnAsync(
					gameProviderCode: command.GameProviderCode,
					cancellationToken: cancellationToken)
					.ConfigureAwait(false);

				if (momodifiedCount > 0)
				{
					logger.LogWarning("LogOn:{logOn} Application:{application} Message:遊戲供應商{gameProviderCode}設定到期時間筆數{count}", DateTime.Now.ToString("u"), typeof(GameBetRapidTestExpireCommandService).Name, command.GameProviderCode, momodifiedCount);
				}
				else
				{
					logger.LogInformation("LogOn:{logOn} Application:{application} Message:遊戲供應商{gameProviderCode}設定到期時間筆數{count}", DateTime.Now.ToString("u"), typeof(GameBetRapidTestExpireCommandService).Name, command.GameProviderCode, momodifiedCount);
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "LogOn:{logOn} Application:{application} Message:遊戲供應商{gameProviderCode}發生例外", DateTime.Now.ToString("u"), typeof(GameBetRapidTestExpireCommandService).Name, command.GameProviderCode);
				throw;
			}
			finally
			{
				await gameWagersLock.UnlockAsync(
					gameProviderCode: command.GameProviderCode,
					application: typeof(GameBetRapidTestExpireCommandService).Name)
					.ConfigureAwait(false);
			}
		}
	}
}

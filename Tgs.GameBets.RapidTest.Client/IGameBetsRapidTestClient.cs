namespace Tgs.GameBets.RapidTest.Client;

public interface IGameBetsRapidTestClient
{
	ValueTask<bool> GameBetRapidTestCommandAsync(Common.ViewModels.GameBetRapidTestCommandViewModel source, CancellationToken cancellationToken = default);

	ValueTask GameBetRapidTestExpireAsync(string gameProviderCode, CancellationToken cancellationToken = default);

	ValueTask GameBetRapidTestInitializationAsync(string gameProviderCode, CancellationToken cancellationToken = default);
}

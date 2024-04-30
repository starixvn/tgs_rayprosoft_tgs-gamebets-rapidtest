using System.Net.Http.Json;
using System.Text.Json;

using System.Text.Json.Serialization;

namespace Tgs.GameBets.RapidTest.Client;

internal class GameBetsRapidTestClient(
	HttpClient httpClient) : IGameBetsRapidTestClient
{
	private static readonly JsonSerializerOptions _SerializerOptions = new()
	{
		WriteIndented = false,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Converters = { new JsonStringEnumConverter() }
	};

	public async ValueTask<bool> GameBetRapidTestCommandAsync(Common.ViewModels.GameBetRapidTestCommandViewModel source, CancellationToken cancellationToken = default)

	{
		var path = new Uri(httpClient.BaseAddress!, $"/rapid-test");
		var builder = new UriBuilder(path);
		var requestContent = JsonContent.Create(inputValue: source, options: _SerializerOptions);
		var response = await httpClient.PostAsync(
			builder.ToString(),
			requestContent,
			cancellationToken).ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<bool>(
			_SerializerOptions,
			cancellationToken).ConfigureAwait(false);
	}

	public async ValueTask GameBetRapidTestExpireAsync(string gameProviderCode, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PutAsync(
			$"/rapid-test/{gameProviderCode}/expire",
			null,
			cancellationToken).ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}

	public async ValueTask GameBetRapidTestInitializationAsync(string gameProviderCode, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PostAsync(
			$"/rapid-test/{gameProviderCode}/init",
			null,
			cancellationToken).ConfigureAwait(false);

		_ = response.EnsureSuccessStatusCode();
	}
}

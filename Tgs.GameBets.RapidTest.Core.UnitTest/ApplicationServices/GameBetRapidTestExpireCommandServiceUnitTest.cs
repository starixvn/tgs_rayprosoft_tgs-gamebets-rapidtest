using Microsoft.Extensions.Logging.Abstractions;

using NSubstitute;

using Tgs.GameBets.RapidTest.Core.ApplicationServices;

namespace Tgs.GameBets.RapidTest.Core.UnitTest.ApplicationServices;

public class GameBetRapidTestExpireCommandServiceUnitTest
{
	[Fact]
	public async Task Locked_Is_True()
	{
		var faceLogger = NullLogger<GameBetRapidTestExpireCommandService>.Instance;
		var fakerGameBetRapidTestExpireLockedRepository = Substitute.For<IGameBetRapidTestExpireLockedRepository>();
		var fakeGameBetRapidTestRepository = Substitute.For<IGameBetRapidTestRepository>();

		var command = new GameBetRapidTestExpireCommand(
			GameProviderCode: "KY");

		var sut = new GameBetRapidTestExpireCommandService(
			faceLogger,
			fakerGameBetRapidTestExpireLockedRepository,
			fakeGameBetRapidTestRepository);

		_ = fakerGameBetRapidTestExpireLockedRepository.LockedAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			application: Arg.Is(typeof(GameBetRapidTestExpireCommandService).Name),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		await sut.ExecuteAsync(command: command);

		_ = fakeGameBetRapidTestRepository
			.Received(1)
			.SetExpireOnAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			cancellationToken: Arg.Any<CancellationToken>());

		_ = fakerGameBetRapidTestExpireLockedRepository
			.Received(1)
			.UnlockAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			application: Arg.Is(typeof(GameBetRapidTestExpireCommandService).Name));
	}

	[Fact]
	public async Task Locked_Is_False()
	{
		var faceLogger = NullLogger<GameBetRapidTestExpireCommandService>.Instance;
		var fakerGameBetRapidTestExpireLockedRepository = Substitute.For<IGameBetRapidTestExpireLockedRepository>();
		var fakeGameBetRapidTestRepository = Substitute.For<IGameBetRapidTestRepository>();

		var command = new GameBetRapidTestExpireCommand(
			GameProviderCode: "KY");

		var sut = new GameBetRapidTestExpireCommandService(
			faceLogger,
			fakerGameBetRapidTestExpireLockedRepository,
			fakeGameBetRapidTestRepository);

		_ = fakerGameBetRapidTestExpireLockedRepository.LockedAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			application: Arg.Is(typeof(GameBetRapidTestExpireCommandService).Name),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(false);

		await sut.ExecuteAsync(command: command);

		_ = fakeGameBetRapidTestRepository
			.Received(0)
			.SetExpireOnAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			cancellationToken: Arg.Any<CancellationToken>());

		_ = fakerGameBetRapidTestExpireLockedRepository
			.Received(0)
			.UnlockAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			application: Arg.Is(typeof(GameBetRapidTestExpireCommandService).Name));
	}

	[Fact]
	public async Task SetExpireOn_Exception()
	{
		var faceLogger = NullLogger<GameBetRapidTestExpireCommandService>.Instance;
		var fakerGameBetRapidTestExpireLockedRepository = Substitute.For<IGameBetRapidTestExpireLockedRepository>();
		var fakeGameBetRapidTestRepository = Substitute.For<IGameBetRapidTestRepository>();

		var command = new GameBetRapidTestExpireCommand(
			GameProviderCode: "KY");

		var sut = new GameBetRapidTestExpireCommandService(
			faceLogger,
			fakerGameBetRapidTestExpireLockedRepository,
			fakeGameBetRapidTestRepository);

		_ = fakerGameBetRapidTestExpireLockedRepository.LockedAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			application: Arg.Is(typeof(GameBetRapidTestExpireCommandService).Name),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

		_ = fakeGameBetRapidTestRepository
			.SetExpireOnAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(ValueTask.FromException<long>(new Exception()));

		_ = await Assert.ThrowsAsync<Exception>(() => sut.ExecuteAsync(command: command).AsTask());

		_ = fakerGameBetRapidTestExpireLockedRepository
			.Received(1)
			.UnlockAsync(
			gameProviderCode: Arg.Is(command.GameProviderCode),
			application: Arg.Is(typeof(GameBetRapidTestExpireCommandService).Name));
	}
}

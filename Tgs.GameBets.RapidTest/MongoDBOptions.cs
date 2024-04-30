namespace Tgs.GameBets.RapidTest;

public record MongoDBOptions
{
	public string ConnectionString { get; set; } = "mongodb://localhost";
}

namespace Nimble.GuestbookApp.Infrastructure.Messaging;

public class RabbitMQSettings
{
  public string Host { get; set; } = "rabbitmq://localhost";
  public string Username { get; set; } = "guest";
  public string Password { get; set; } = "guest";
  public Dictionary<string, string> QueueSettings { get; set; } = new();
}

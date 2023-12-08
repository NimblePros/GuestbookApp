using MassTransit;
using Nimble.GuestbookApp.Core.Services;

namespace RabbitMQConsumer;

public class EmailMessageConsumer : IConsumer<EmailDetails>
{
    private readonly MimeKitEmailSender _emailSender;
    public EmailMessageConsumer(MimeKitEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Consume(ConsumeContext<EmailDetails> context)
    {
        try
        {
            var message = context.Message;
            // Process the message
            await _emailSender.SendEmailAsync(message.To, message.From,
                message.Subject, message.Body);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}

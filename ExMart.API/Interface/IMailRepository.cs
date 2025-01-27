namespace ExMart_Backend.Services.Interface
{
    public interface IMailRepository
    {
        public Task SendEmail(string recepter, string subject, string body);

    }
}

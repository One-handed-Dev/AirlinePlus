namespace InteractionSection.Domain.EmailAgg
{
    public class EmailConfigurationDto
    {
        public int Port { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
    }
}

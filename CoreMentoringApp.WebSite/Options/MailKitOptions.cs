namespace CoreMentoringApp.WebSite.Options
{
    public class MailKitOptions
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
    }
}
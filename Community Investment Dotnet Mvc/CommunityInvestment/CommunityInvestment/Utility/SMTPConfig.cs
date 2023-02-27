namespace CommunityInvestment.Utility;

public class SMTPConfig
{
    public string SenderAddress { get; set; }
    public string SenderDisplayName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string EnableSSL { get; set; }
    public string UseDefaultCredentials { get; set; }
    public string IsBodyHTML { get; set; }
}
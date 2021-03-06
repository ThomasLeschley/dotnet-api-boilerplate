namespace Boilerplate.Api
{
    public class Appsettings
    {
        public string ASPNETCORE_ENVIRONMENT { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public SendGridSettings SendGrid { get; set; }
        public AuthorizationSettings Authorization { get; set; }
        public DefaultAccount[] DefaultAccounts { get; set; } = new DefaultAccount[0];
        public RentalSettings Rental { get; set; }
    }

    public class ConnectionStrings
    {
        public string DbConnection { get; set; }
    }

    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string SendFromName { get; set; }
        public string SendFromEmail { get; set; }
        public string ResetPasswordTemplateId { get; set; }
    }

    public class AuthorizationSettings
    {
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInSeconds { get; set; }
    }

    public class DefaultAccount
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class RentalSettings
    {
        public string DefaultCategoryName { get; set; }
    }

}
namespace Common.Application.Contracts
{
    public sealed record PasswordCheckRequest(string Hash, string Password);
}

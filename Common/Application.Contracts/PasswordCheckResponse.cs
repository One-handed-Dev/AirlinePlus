namespace Common.Application.Contracts
{
    public sealed record PasswordCheckResponse(bool Verified, bool NeedsUpgrade);
}

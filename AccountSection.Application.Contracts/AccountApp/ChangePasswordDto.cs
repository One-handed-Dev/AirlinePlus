namespace AccountSection.Application.Contracts.AccountApp
{
    public sealed record ChangePasswordDto(long AccountId, string Password, string RePassword);
}

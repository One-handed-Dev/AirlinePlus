namespace Common.Application.Contracts
{
    public interface IPasswordService
    {
        string Hash(string password);
        PasswordCheckResponse Check(PasswordCheckRequest command);
    }
}

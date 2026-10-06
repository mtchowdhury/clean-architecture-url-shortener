namespace Api.Extensions;

public interface ICredentialValidator
{
    bool IsValid(string username, string password);
}

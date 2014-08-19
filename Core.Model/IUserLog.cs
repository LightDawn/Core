namespace Core.Model
{
    public interface IUserLog
    {
        string GetIpAddress();
        string GetUserName();
        int? GetOrganizationId();
    }

}

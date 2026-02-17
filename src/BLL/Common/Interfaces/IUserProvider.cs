namespace BLL.Common.Interfaces;

public interface IUserProvider
{
    Task<Guid> GetUserId();
    string GetUserRole();
}
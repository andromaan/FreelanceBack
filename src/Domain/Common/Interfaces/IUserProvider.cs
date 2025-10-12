namespace Domain.Common.Interfaces;

public interface IUserProvider
{
    Task<Guid> GetUserId();
    string GetUserRole();
}
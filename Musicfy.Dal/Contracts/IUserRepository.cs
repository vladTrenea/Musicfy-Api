using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface IUserRepository
    {
        User GetById(string id);

        User GetByCredentials(string username, string password);
    }
}
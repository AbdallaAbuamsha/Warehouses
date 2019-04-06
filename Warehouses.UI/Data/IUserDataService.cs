using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IUserDataService
    {
        User Login(string username, string password);
    }
}
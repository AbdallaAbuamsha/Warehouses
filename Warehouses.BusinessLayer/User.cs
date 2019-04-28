using Exceptions;

namespace Warehouses.BusinessLayer
{
    public class User
    {
        public void Login(string username, string password, string lang)
        {
            BusinessException exception;
            var res = WarehousesManagementEF.User.LogIn(username, password, out exception, lang);

        }

    }
}

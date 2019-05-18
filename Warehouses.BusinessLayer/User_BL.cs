using Exceptions;
using System.Reflection;
using Warehouses.Model;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class User_BL
    {
        public static ResultObject Login(string username, string password, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_USER resultDal = WarehousesManagementEF.User.LogIn(username, password, out exception, language);

                Model.User data = new Model.User();
                data.Id = resultDal.ID;
                data.Username = resultDal.USERNAME;
                data.FirstName = resultDal.FIRST_NAME;
                data.LastName = resultDal.LAST_NAME;
                //data.Email = resultDal.EMAIL;
                data.PhoneNumber = resultDal.MOBILE;
                data.Address = resultDal.ADDRESS;
                data.Age = resultDal.AGE;
                data.VoidReason = resultDal.VOID_REASON;
                data.IsVoid = resultDal.IS_VOID;
                data.TypeId = resultDal.TYPE_ID;

                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
                resultObject.Data = data;
                return resultObject;
            }
            catch
            {
                resultObject.Data = null;
                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
                return resultObject;
            }
        }

        public static ResultObject CreateUser()
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long resultDal = WarehousesManagementEF.User.Create("admin", "admin", "admin", "admin", 1, 23, "admin", "0900000000", out exception, "ar");

                Model.User data = new Model.User();

                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
                resultObject.Data = data;
                return resultObject;
            }
            catch
            {
                resultObject.Data = null;
                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
                return resultObject;
            }
        }
    }
}

using Exceptions;
using System.Collections.Generic;
using Warehouses.Model;
using WarehousesManagementEF.Model;
using System.Reflection;

namespace Warehouses.BusinessLayer
{
    public class Organization_BL
    {
        public static ResultObject GetAll(string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Organization> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                List<WAR_ORGANIZATION> resultDal = WarehousesManagementEF.Organization.GetAll(out exception, language);
                List<Model.Organization> resultBusiness = new List<Model.Organization>();
                foreach (var org in resultDal)
                {
                    Model.Organization temp = new Model.Organization();
                    temp.Id = org.ID;
                    temp.Name = org.NAME;
                    temp.Location = org.ADDRESS;
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Organization>(resultBusiness, resultBusiness.Count);
                resultObject.Data = resultList;
                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
                return resultObject;
            }
            catch
            {
                resultObject.Data = resultList;
                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
                return resultObject;
            }
        }
    }
}

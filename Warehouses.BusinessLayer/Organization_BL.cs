using Exceptions;
using System.Collections.Generic;
using Warehouses.Model;
using WarehousesManagementEF.Model;
using System.Reflection;

namespace Warehouses.BusinessLayer
{
    public class Organization_BL : BusinessBase
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
                    Organization temp = ConvertOrganization(org);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Organization>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject GetById(long organizationId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_ORGANIZATION resultDal = WarehousesManagementEF.Organization.GetById(organizationId, out exception, language);
                Organization data = ConvertOrganization(resultDal);
                return ReturnResultObject(data, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Create(string name, string address, long userId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = WarehousesManagementEF.Organization.Create(name, address, userId, out exception, language);
                Organization resultBusiness = new Model.Organization();

                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject Delete(long organizationId, string voidReason, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool deleteStatus = WarehousesManagementEF.Organization.Delete(organizationId, voidReason, out exception, language);
                return ReturnResultObject(deleteStatus, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

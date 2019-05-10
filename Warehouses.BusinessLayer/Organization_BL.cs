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

        public static ResultObject GetById(long organizationId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_ORGANIZATION resultDal = WarehousesManagementEF.Organization.GetById(organizationId, out exception, language);
                Organization resultBusiness = new Model.Organization();

                Model.Organization data = new Model.Organization();
                data.Id = resultDal.ID;
                data.Name = resultDal.NAME;
                data.Location = resultDal.ADDRESS;
                
                resultObject.Data = data;
                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
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

                resultObject.Data = id;
                resultObject.Code = exception.code;
                resultObject.Message = exception.Message;
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

using Exceptions;
using System.Collections.Generic;
using System.Reflection;
using Warehouses.Model;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class Branch_BL
    {
        public static ResultObject GetAll(string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Branch> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_BRANCH> resultDal = WarehousesManagementEF.Branch.GetAll(out exception, language);
                List<Model.Branch> resultBusiness = new List<Model.Branch>();
                foreach (var branch in resultDal)
                {
                    Model.Branch temp = new Model.Branch();
                    temp.Id = branch.ID;
                    temp.Name = branch.NAME;
                    temp.Location = branch.ADDRESS;
                    temp.ParentId = branch.ORGANIZATION_ID;
                    temp.IsVoid = branch.IS_VOID;
                    temp.VoidReason = branch.VOID_REASON;

                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Branch>(resultBusiness, resultBusiness.Count);
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
        public static ResultObject GetAllByOrganizationId(long organizationId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Branch> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_BRANCH > resultDal = WarehousesManagementEF.Branch.GetAllByOrganizationId(organizationId, out exception, language);
                List<Model.Branch> resultBusiness = new List<Model.Branch>();
                foreach (var branch in resultDal)
                {
                    Model.Branch temp = new Model.Branch();
                    temp.Id = branch.ID;
                    temp.Name = branch.NAME;
                    temp.Location = branch.ADDRESS;
                    temp.ParentId = branch.ORGANIZATION_ID;
                    temp.IsVoid = branch.IS_VOID;
                    temp.VoidReason = branch.VOID_REASON;

                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Branch>(resultBusiness, resultBusiness.Count);
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

        public static ResultObject GetById(long branchId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_BRANCH resultDal = WarehousesManagementEF.Branch.GetById(branchId, out exception, language);
                Branch resultBusiness = new Model.Branch();

                Model.Branch data = new Model.Branch();
                data.Id = resultDal.ID;
                data.Name = resultDal.NAME;
                data.Location = resultDal.ADDRESS;
                data.ParentId = resultDal.ORGANIZATION_ID;

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
    }
}

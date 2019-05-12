using Exceptions;
using System.Collections.Generic;
using System.Reflection;
using Warehouses.Model;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class Branch_BL : BusinessBase
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
                    Branch temp = ConvertBranch(branch);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Branch>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
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
                    Branch temp = ConvertBranch(branch);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Branch>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
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
                Branch data = ConvertBranch(resultDal);
                return ReturnResultObject(data, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject Create(string name, string address, long organizationId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = WarehousesManagementEF.Branch.Create(name, address, organizationId, out exception, language);
                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Delete(long branchId, string voidReason, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool deleteStatus = WarehousesManagementEF.Branch.Delete(branchId, voidReason, out exception, language);
                return ReturnResultObject(deleteStatus, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

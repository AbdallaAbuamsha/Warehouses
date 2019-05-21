using Exceptions;
using System.Collections.Generic;
using System.Reflection;
using Warehouses.Model;
using WarehousesManagementEF;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class MaterialUnit_BL : BusinessBase
    {
        public static ResultObject GetBasicUnitByMaterialId(long materialId, string language)
        {
            BusinessException exception = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_UNIT resultDal = WarehousesManagementEF.MaterialUnit.GetBasicUnitByMaterialId(materialId, out exception, language);

                Model.Unit data = ConvertUnit(resultDal);
                return ReturnResultObject(data, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject GetAllRelatedMaterialUnits(long materialId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Unit> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                List<WAR_UNIT> resultDal = MaterialUnit.GetAllRelatedMaterialUnits(materialId ,out exception, language);
                List<Model.Unit> resultBusiness = new List<Model.Unit>();
                foreach (var org in resultDal)
                {
                    Model.Unit temp = ConvertUnit(org);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Unit>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject GetAllUnRelatedMaterialUnits(long materialId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Unit> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                List<WAR_UNIT> resultDal = MaterialUnit.GetAllUnRelatedMaterialUnits(materialId, out exception, language);
                List<Model.Unit> resultBusiness = new List<Model.Unit>();
                foreach (var org in resultDal)
                {
                    Model.Unit temp = ConvertUnit(org);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Unit>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Create(long materialId, long unitId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = MaterialUnit.Create(materialId, unitId, out exception, language);
                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject Delete(long materialId, long unitId, string voidReason, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool status = MaterialUnit.Delete(materialId, unitId, voidReason, out exception, language);
                return ReturnResultObject(status, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

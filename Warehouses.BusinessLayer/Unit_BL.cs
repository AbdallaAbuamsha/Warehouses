using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class Unit_BL : BusinessBase
    {

        public static ResultObject GetAll(string language)
        {
            BusinessException exception = null;
            ResultList<Model.Unit> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                List<WAR_UNIT> resultDal = WarehousesManagementEF.Unit.GetAll(out exception, language);
                List<Model.Unit> resultBusiness = new List<Model.Unit>();
                foreach (var unit in resultDal)
                {
                    Model.Unit temp = ConvertUnit(unit);
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
        public static ResultObject GetAllByParentId(long unitId, string language)
        {
            BusinessException exception = null;
            ResultList<Model.Unit> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                List<WAR_UNIT> resultDal = WarehousesManagementEF.Unit.GetAll(out exception, language);
                List<Model.Unit> resultBusiness = new List<Model.Unit>();
                foreach (var unit in resultDal)
                {
                    Model.Unit temp = ConvertUnit(unit);
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

        public static ResultObject GetById(long unitId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_UNIT resultDal = WarehousesManagementEF.Unit.GetById(unitId, out exception, language);
                Unit data = ConvertUnit(resultDal);
                return ReturnResultObject(data, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Create(string name, long? parentUnitId, decimal? factor, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = WarehousesManagementEF.Unit.Create(name, parentUnitId, factor, out exception, language);
                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Delete(long unitId, string voidReason, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool deleteStatus = WarehousesManagementEF.Unit.Delete(unitId, voidReason, out exception, language);
                return ReturnResultObject(deleteStatus, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

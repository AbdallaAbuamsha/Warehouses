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
    public class Material_BL : BusinessBase
    {
        public static ResultObject GetAll(string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Material> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                List<WAR_MATERIAL> resultDal = WarehousesManagementEF.Material.GetAll(out exception, language);
                List<Model.Material> resultBusiness = new List<Model.Material>();
                foreach (var org in resultDal)
                {
                    Material temp = ConvertMaterial(org);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Material>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject GetById(long materialId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_MATERIAL resultDal = WarehousesManagementEF.Material.GetById(materialId, out exception, language);
                Material data = ConvertMaterial(resultDal);
                return ReturnResultObject(data, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Create(string name, string latinName, string code, string barcode, bool serializable, long basicUnitId, decimal? minQuantity, decimal? maxQuantity, decimal? freeQuantity, long organizationId, long? parentMaterialId, string lang)
        {           
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {                
                long id = WarehousesManagementEF.Material.Create(name, latinName, code, barcode, serializable, basicUnitId,  minQuantity, maxQuantity, freeQuantity, organizationId, parentMaterialId, out exception, lang);
                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject Delete(long materialId, string voidReason, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool deleteStatus = WarehousesManagementEF.Organization.Delete(materialId, voidReason, out exception, language);
                return ReturnResultObject(deleteStatus, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

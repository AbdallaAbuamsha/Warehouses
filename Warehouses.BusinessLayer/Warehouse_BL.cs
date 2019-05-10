using Exceptions;
using System.Collections.Generic;
using System.Reflection;
using Warehouses.Model;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class Warehouse_BL : BusinessBase
    {
        public static ResultObject GetAllByBranchId(long branchId, string language)
        {
            BusinessException exception = null;
            ResultList<Model.Warehouse> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_WAREHOUSE> resultDal = WarehousesManagementEF.Warehouse.GetAllByBranchId(branchId, out exception, language);
                List<Model.Warehouse> resultBusiness = new List<Model.Warehouse>();
                foreach (var warehouse in resultDal)
                {
                    Model.Warehouse temp = ConvertWarehouse(warehouse);
                    resultBusiness.Add(temp);

                }
                resultList = new ResultList<Model.Warehouse>(resultBusiness, resultBusiness.Count);
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
            ResultList<Model.Warehouse> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_WAREHOUSE> resultDal = WarehousesManagementEF.Warehouse.GetAllByOrganizationId(organizationId, out exception, language);
                List<Model.Warehouse> resultBusiness = new List<Model.Warehouse>();
                foreach (var warehouse in resultDal)
                {
                    Model.Warehouse temp = ConvertWarehouse(warehouse);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Warehouse>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }

        public static ResultObject GetAllByWarehouseId(long warehouseId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Warehouse> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_WAREHOUSE> resultDal = WarehousesManagementEF.Warehouse.GetAllByParentId(warehouseId, out exception, language);
                List<Model.Warehouse> resultBusiness = new List<Model.Warehouse>();
                foreach (var warehouse in resultDal)
                {
                    Model.Warehouse temp = ConvertWarehouse(warehouse);
                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Warehouse>(resultBusiness, resultBusiness.Count);
                return ReturnResultObject(resultList, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject GetById(long warehouseId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_WAREHOUSE warehouse = WarehousesManagementEF.Warehouse.GetById(warehouseId, out exception, language);
                Model.Warehouse data = ConvertWarehouse(warehouse);
                return ReturnResultObject(data, exception.code, exception.Message);

            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject Create(string name, string latinName, string address, string code, byte parentType, long organizationId, long? branchId, long? parentWarehouseId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = WarehousesManagementEF.Warehouse.Create(name, latinName, address, code, parentType, organizationId, branchId, parentWarehouseId, out exception, language);
                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject Delete(long warehouseId, string voidReason, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool deleteStatus = WarehousesManagementEF.Warehouse.Delete(warehouseId, voidReason, out exception, language);
                return ReturnResultObject(deleteStatus, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

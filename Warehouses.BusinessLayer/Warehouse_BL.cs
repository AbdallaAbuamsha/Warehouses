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
    public class Warehouse_BL
    {
        public static ResultObject GetAllByBranchId(long branchId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            ResultList<Model.Warehouse> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_WAREHOUSE> resultDal = WarehousesManagementEF.Warehouse.GetAllByBranchId(branchId, out exception, language);
                List<Model.Warehouse> resultBusiness = new List<Model.Warehouse>();
                foreach (var warehouse in resultDal)
                {
                    Model.Warehouse temp = new Model.Warehouse();
                    temp.Id = warehouse.ID;
                    temp.Name = warehouse.NAME;
                    temp.Location = warehouse.ADDRESS;
                    temp.ParentId = warehouse.ORGANIZATION_ID;
                    temp.IsVoid = warehouse.IS_VOID;
                    temp.VoidReason = warehouse.VOID_REASON;

                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Warehouse>(resultBusiness, resultBusiness.Count);
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
            ResultList<Model.Warehouse> resultList = null;
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                List<WAR_WAREHOUSE> resultDal = WarehousesManagementEF.Warehouse.GetAllByOrganizationId(organizationId, out exception, language);
                List<Model.Warehouse> resultBusiness = new List<Model.Warehouse>();
                foreach (var warehouse in resultDal)
                {
                    Model.Warehouse temp = new Model.Warehouse();
                    temp.Id = warehouse.ID;
                    temp.Name = warehouse.NAME;
                    temp.Location = warehouse.ADDRESS;
                    temp.ParentId = warehouse.ORGANIZATION_ID;
                    temp.IsVoid = warehouse.IS_VOID;
                    temp.VoidReason = warehouse.VOID_REASON;

                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Warehouse>(resultBusiness, resultBusiness.Count);
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
                    Model.Warehouse temp = new Model.Warehouse();
                    temp.Id = warehouse.ID;
                    temp.Name = warehouse.NAME;
                    temp.Location = warehouse.ADDRESS;
                    temp.ParentId = warehouse.ORGANIZATION_ID;
                    temp.IsVoid = warehouse.IS_VOID;
                    temp.VoidReason = warehouse.VOID_REASON;

                    resultBusiness.Add(temp);
                }
                resultList = new ResultList<Model.Warehouse>(resultBusiness, resultBusiness.Count);
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
        public static ResultObject GetById(long warehouseId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                WAR_WAREHOUSE resultDal = WarehousesManagementEF.Warehouse.GetById(warehouseId, out exception, language);
                Warehouse resultBusiness = new Model.Warehouse();

                Model.Warehouse data = new Model.Warehouse();
                data.Id = resultDal.ID;
                data.Name = resultDal.NAME;
                data.Location = resultDal.ADDRESS;
                data.ParentId = resultDal.ORGANIZATION_ID;
                data.Code = resultDal.CODE;                
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
        public static ResultObject Create(string name, string latinName, string address, string code, byte parentType, long organizationId, long? branchId, long? parentWarehouseId, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = WarehousesManagementEF.Warehouse.Create(name, latinName, address, code, parentType, organizationId, branchId, parentWarehouseId, out exception, language);
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

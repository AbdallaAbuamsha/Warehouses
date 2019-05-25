using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using Warehouses.Model;

namespace Warehouses.BusinessLayer
{
    public class Receipt_BL : BusinessBase
    {
        public static ResultObject Create(long userId, long? supplierId, long? customerId, DateTime date, string language)
        {
            BusinessException exception = null;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                long id = WarehousesManagementEF.Voucher.Create(userId, supplierId, customerId, date, out exception, language);
                Organization resultBusiness = new Model.Organization();

                return ReturnResultObject(id, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
        public static ResultObject AddRowToReceipt(long voucherId, long materialId, string serialNumber, DateTime expiryDate, long? originWarehouseId, long? destinationWarehouseId, long basicUnitId, decimal basicUnitQuantity, List<KeyValuePair<long, decimal>> otherUnitsFactors, string lang)
        {
            BusinessException exception = null;
            string materialNotification;
            ResultObject resultObject = new ResultObject();
            MethodBase methodInfo = MethodBase.GetCurrentMethod();
            string functionFullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
            try
            {
                bool addRowStatus = WarehousesManagementEF.Voucher.AddRowToVoucher(voucherId, materialId, serialNumber, expiryDate, originWarehouseId, destinationWarehouseId, basicUnitId, basicUnitQuantity, otherUnitsFactors, out materialNotification, out exception, lang);
                Organization resultBusiness = new Model.Organization();

                return ReturnResultObject(addRowStatus, exception.code, exception.Message);
            }
            catch
            {
                return ReturnResultObject(null, exception.code, exception.Message);
            }
        }
    }
}

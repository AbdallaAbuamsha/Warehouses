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
    }
}

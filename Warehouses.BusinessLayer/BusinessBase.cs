using Warehouses.Model;
using WarehousesManagementEF.Model;

namespace Warehouses.BusinessLayer
{
    public class BusinessBase
    {
        protected static ResultObject ReturnResultObject(object data, int code, string message)
        {
            ResultObject resultObject = new ResultObject();
            resultObject.Data = data;
            resultObject.Code = code;
            resultObject.Message = message;
            return resultObject;
        }

        protected static Model.Warehouse ConvertWarehouse(WAR_WAREHOUSE warehouse)
        {
            Model.Warehouse temp = new Model.Warehouse();
            temp.Id = warehouse.ID;
            temp.Name = warehouse.NAME;
            temp.Location = warehouse.ADDRESS;
            temp.OrganizationID = warehouse.ORGANIZATION_ID;
            temp.BranchId = warehouse.BRANCH_ID;
            temp.ParentWarehouseId = warehouse.PARENT_WAREHOUSE_ID;
            temp.ParentType = warehouse.PARENT_TYPE;
            temp.Code = warehouse.CODE;
            temp.IsVoid = warehouse.IS_VOID;
            temp.VoidReason = warehouse.VOID_REASON;
            return temp;
        }
        protected static Model.Branch ConvertBranch(WAR_BRANCH branch)
        {
            Model.Branch temp = new Model.Branch();
            temp.Id = branch.ID;
            temp.Name = branch.NAME;
            temp.Location = branch.ADDRESS;
            temp.ParentId = branch.ORGANIZATION_ID;
            temp.IsVoid = branch.IS_VOID;
            temp.VoidReason = branch.VOID_REASON;
            return temp;
        }
        protected static Model.Organization ConvertOrganization(WAR_ORGANIZATION organization)
        {
            Model.Organization temp = new Model.Organization();
            temp.Id = organization.ID;
            temp.Name = organization.NAME;
            temp.Location = organization.ADDRESS;
            return temp;
        }
        protected static Model.Unit ConvertUnit(WAR_UNIT unit)
        {
            Unit temp = new Unit();
            temp.Id = unit.ID;
            temp.Name = unit.NAME;
            temp.ParentUnitId = unit.PARENT_UNIT_ID;
            temp.Factor = unit.FACTOR;
            temp.IsVoid = unit.IS_VOID;
            temp.VoidReason = unit.VOID_REASON;
            return temp;
        }
    }
}

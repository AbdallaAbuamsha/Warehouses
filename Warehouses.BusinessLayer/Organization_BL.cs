using Exceptions;
using System.Collections.Generic;
using WarehousesManagementEF;
using Warehouses.Model;
using WarehousesManagementEF.Model;
namespace Warehouses.BusinessLayer
{
    public class Organization_BL
    {
        public static ResultList<Model.Organization> GetAll(string language)
        {
            BusinessException exception = null;
            try
            {
                List<WAR_ORGANIZATION> resultDal = WarehousesManagementEF.Organization.GetAll(out exception, language);
                List<Model.Organization> resultBusiness = new List<Model.Organization>();
                foreach (var org in resultDal)
                {
                    Model.Organization temp = new Model.Organization();
                    temp.Id = org.ID;
                    temp.Name = org.NAME;
                    temp.Location = org.ADDRESS;
                    resultBusiness.Add(temp);
                }
                return new ResultList<Model.Organization>(resultBusiness, resultBusiness.Count);
            }
            catch
            {
                return null;
            }
        }
    }
}

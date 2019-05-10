using System;
using System.Collections.Generic;
using System.Linq;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class WarehouseDataService : IWarehouseDataService
    {
        public bool Delete(Warehouse model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Warehouse> GetAll()
        {
            yield return new Warehouse { Id = 1, Name = "Warehouse 1 "/*, ParentId = 1*/ };
            yield return new Warehouse { Id = 2, Name = "Warehouse 2 "/*, ParentId = 1*/ };
            yield return new Warehouse { Id = 3, Name = "Warehouse 3 "/*, ParentId = 3*/ };
            yield return new Warehouse { Id = 4, Name = "Warehouse 4 "/*, ParentId = 2*/ };
            yield return new Warehouse { Id = 5, Name = "Warehouse 5 "/*, ParentId = 2*/ };
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return new BranchDataService().GetAll();
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
            return new OrganizationDataService().GetAll();
        }

        public Warehouse GetById(long id)
        {
            return GetAll().First(w => w.Id == id) ;
        }

        public IEnumerable<Warehouse> GetByParentId(long id)
        {
            //var warehouses = GetAll();
            //foreach (Warehouse warehouse in warehouses)
            //{
            //    if (warehouse.ParentId == id)
            //    {
            //        yield return warehouse;
            //    }
            //}
            return null;
        }

        public bool HasSiblings()
        {
            return false;
        }

        public bool Save(Warehouse model)
        {
            return true;
        }
    }
}

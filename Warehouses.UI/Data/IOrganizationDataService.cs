﻿using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IOrganizationDataService
    {
        IEnumerable<Organization> GetAll();
        Organization GetById(int id);
        bool Save(Organization organization);
        bool Delete(Organization organization);
    }
}
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IApplicationHelper
    {
        bool AddApplication(AddApplicationModel addApplication);
        bool Delete(int id);
        IEnumerable<Application> GetAll();
        AddApplicationModel GetApplicationData(int id);
    }
}

using DataAccess.Abstraction;
using Model;
using Services.Abstraction;

namespace Services.Helper
{
    public class ApplicationHelper : IApplicationHelper
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationHelper(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public bool AddApplication(AddApplicationModel addApplication)
        {
            return _applicationRepository.AddApplication(addApplication);
        }

        public IEnumerable<Application> GetAll()
        {
            return _applicationRepository.GetAll();
        }

        public bool Delete(int id)
        {
            _applicationRepository.Delete(id);
            return true;
        }

        public AddApplicationModel GetApplicationData(int id)
        {
            return _applicationRepository.GetApplicationData(id);
        }
    }
}

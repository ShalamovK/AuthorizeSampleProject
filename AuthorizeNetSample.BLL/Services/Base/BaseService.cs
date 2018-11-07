using AuthorizeNetSample.Repositories;

namespace AuthorizeNetSample.BLL.Services.Base {
    public class BaseService {
        protected readonly IUnitOfWork _UnitOfWork;
        public BaseService(IUnitOfWork unitOfWork) {
            _UnitOfWork = unitOfWork;
        }
    }
}

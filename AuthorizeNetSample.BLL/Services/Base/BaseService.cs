using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Repositories;
using System;

namespace AuthorizeNetSample.BLL.Services.Base {
    public class BaseService {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IServiceHost _serviceHost;

        public BaseService(IServiceHost serviceHost, IUnitOfWork unitOfWork) {
            _serviceHost = serviceHost ?? throw new ArgumentNullException("serviceHost");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
        }
    }
}

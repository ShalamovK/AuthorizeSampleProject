namespace AuthorizeNetSample.Domain.Interfaces.Services.Base {
    public interface IServiceHost {
        void Register<T>(T service) where T : IService;
        T GetService<T>() where T : IService;
    }
}

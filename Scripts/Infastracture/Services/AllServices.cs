namespace Infastracture.Services
{
  public interface IAllServices
  {
    TService GetSingle<TService>() where TService : IService;
  }

  public class AllServices : IAllServices
  {
    private static AllServices _instance;
    
    public static AllServices Instance => _instance ?? (_instance = new AllServices());

    public void RegisterSingle<TServise>(TServise obj) where TServise : IService
    {
      Implimentation<TServise>.ServiceInstance = obj;
    }

    public TService GetSingle<TService>() where TService : IService
    {
      return Implimentation<TService>.ServiceInstance;
    }

    
    
    
    
    
    
    
    
    private static class Implimentation<TService> where TService : IService
    {
      public static TService ServiceInstance { get; set; }
    }
  }
}
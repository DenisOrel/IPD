
// Type: Intermech.CacheServices.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;


namespace Intermech.CacheServices;

public sealed class Services
{
  public static void Start()
  {
    ICacheServices serviceInstance = (ICacheServices) new Intermech.CacheServices.CacheServices();
    serviceInstance.AddService("ObjectTypeHierarchy", (ICacheService) new ObjectTypeHierarchy());
    ServicesManager.AddService(typeof (ICacheServices), (object) serviceInstance);
  }

  public static void Stop() => ServicesManager.RemoveService(typeof (ICacheServices));
}

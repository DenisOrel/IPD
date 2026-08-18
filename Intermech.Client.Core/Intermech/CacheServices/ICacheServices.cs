
// Type: Intermech.CacheServices.ICacheServices
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.CacheServices;

/// <summary>Предоставляет доступ к списку кэшей.</summary>
public interface ICacheServices
{
  void AddService(string name, ICacheService service);

  void RemoveService(string name);

  ICacheService GetService(string name);

  string[] Names { get; }
}

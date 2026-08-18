
// Type: Intermech.CacheServices.ICacheService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Cache.Performance;


namespace Intermech.CacheServices;

/// <summary>Базовый интерфейс кэш-сервиса.</summary>
public interface ICacheService
{
  /// <summary>Возвращает коллекцию счетчиков производительности.</summary>
  PerformanceCounterCollection PerformanceCounters { get; }
}


// Type: Intermech.Client.Core.ObjectCreator.IStepCompleteManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>Интерфейс для перехода по страницам мастера</summary>
public interface IStepCompleteManager
{
  /// <summary>Переход на другую старницу мастера</summary>
  event StepCompletedHandler StepCompletedEvent;

  bool IsCompletedEventSubscribed { get; }
}

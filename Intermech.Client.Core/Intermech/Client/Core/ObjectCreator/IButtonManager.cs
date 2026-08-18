
// Type: Intermech.Client.Core.ObjectCreator.IButtonManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>
/// Интерфейс реализуют контролы мастера создания объектов и которые могут управлять кнопками на основной форме
/// </summary>
public interface IButtonManager
{
  /// <summary>Установка свойства Enabled кнопке</summary>
  event SetButtonEnabledHandler SetButtonEnabledEvent;

  bool IsButtonEnabledEventSubscribed { get; }
}

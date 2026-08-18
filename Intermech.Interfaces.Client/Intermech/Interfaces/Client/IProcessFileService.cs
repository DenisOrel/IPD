// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IProcessFileService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Служба предварительной обработки значения файлового атрибута при просмотре или редактировании
/// </summary>
public interface IProcessFileService
{
  /// <summary>
  /// Событие назначения аутентичных файлов на версию объекта
  /// </summary>
  event FileProcessEventHandler FileProcessEvent;

  /// <summary>Запустить событие FileProcessEvent</summary>
  /// <param name="eventArgs"></param>
  void FireFileProcessEvent(FileProcessEventArgs eventArgs);
}

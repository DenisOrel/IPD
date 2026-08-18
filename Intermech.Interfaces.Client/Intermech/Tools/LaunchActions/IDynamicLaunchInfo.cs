// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.IDynamicLaunchInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Xml;

#nullable disable
namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Интерфейс контейнера с параметрами для определения динамически подключаемого обработчика запуска приложения.
/// </summary>
public interface IDynamicLaunchInfo
{
  LaunchParams LaunchParams { get; }

  /// <summary>
  /// Возвращает конфигурацию для динамически подключаемого обработчика.
  /// Значение свойства содержит пустой xml-документ, так как у таких обработчиков не может быть
  /// декларативно заданной конфигурации.
  /// </summary>
  XmlDocument HandlerData { get; }

  /// <summary>
  /// Возвращает или задает объект динамически подключаемого обработчика.
  /// Исходное значение свойства равно null.
  /// </summary>
  ILaunchHandler Handler { get; set; }
}

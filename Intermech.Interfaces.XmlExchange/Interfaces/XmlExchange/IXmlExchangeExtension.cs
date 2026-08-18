// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeExtension
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Базовый интерфейс для расширений задач импорта / экспорта
/// </summary>
public interface IXmlExchangeExtension
{
  /// <summary>Приоритет расширения</summary>
  XmlExtensionPriority Priority { get; }

  /// <summary>Глобальный идентификатор плагина</summary>
  /// <remarks>Используется для управления работой плагина (включение / выключение в конфигурациях) в задачах
  /// импорта / экспорта</remarks>
  Guid Guid { get; }

  /// <summary>Признак "системного" плагина</summary>
  /// <remarks>"Включает" плагин вне зависимости от настроек конфигурации, если установлен признак системного</remarks>
  bool IsSystem { get; }

  /// <summary>
  /// Уведомление плагина о начале задачи импорта / экспорта
  /// </summary>
  /// <param name="task">Объект задачи / подзадачи </param>
  void StartTask(object task);

  /// <summary>
  /// Уведомление плагина об окончании задачи импорта / экспорта
  /// </summary>
  /// <param name="task">Объект задачи / подзадачи </param>
  void EndTask(object task);
}

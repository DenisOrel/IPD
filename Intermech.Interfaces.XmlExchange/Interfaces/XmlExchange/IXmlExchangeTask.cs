// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeTask
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.XmlExchange.Services;
using System;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Задача импорта / экспорта</summary>
public interface IXmlExchangeTask : IDisposable
{
  /// <summary>Уникальный идентификатор задачи</summary>
  Guid TaskGuid { get; }

  /// <summary>Глобальный идентификатор пользовательской сессии</summary>
  Guid SessionGuid { get; }

  /// <summary>Контейнер сервисов</summary>
  IServiceContainer Services { get; }

  /// <summary>Статус текущей задачи</summary>
  XmlExchangeTaskStatus TaskStatus { get; }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IResponceObject
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

/// <summary>Входящий запрос</summary>
public interface IResponceObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData, IStatus
{
  /// <summary>Идентификатор входящего запроса</summary>
  string ResponceID { get; }

  /// <summary>
  /// Идентификатор исходящего запроса. Может отсутствовать, т.е. UnknownObjectID
  /// </summary>
  string RequestID { get; }

  /// <summary>Ссылка на конфигурации элемента</summary>
  long[] ConfigElementLink { get; set; }

  /// <summary>Ссылка на объекты-назначение</summary>
  long[] DestinationObjectsLink { get; set; }
}

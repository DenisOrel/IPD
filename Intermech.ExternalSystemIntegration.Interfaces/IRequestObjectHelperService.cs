// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IRequestObjectHelperService
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

/// <summary>
/// Вспомогательный сервис по присовение атрибутов от источника в запрос
/// </summary>
public interface IRequestObjectHelperService
{
  /// <summary>Присовение атрибутов запросу от объекта-источника</summary>
  /// <param name="RequestObjectID"></param>
  /// <param name="SourceObjectID"></param>
  /// <param name="SessionGUID"></param>
  void AssignAttributes(long RequestObjectID, long SourceObjectID, Guid SessionGUID);

  /// <summary>
  /// Присовение атрибутов запросу от объекта-источника по указанной конфигурации
  /// </summary>
  /// <param name="CreatedRequestObjectID"></param>
  /// <param name="SourceObjectID"></param>
  /// <param name="SessionGUID"></param>
  void AssignAttributes(
    long RequestObjectID,
    long SourceObjectID,
    long ConfigObjectID,
    Guid SessionGUID);
}

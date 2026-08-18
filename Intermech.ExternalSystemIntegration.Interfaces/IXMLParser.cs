// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IXMLParser
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

public interface IXMLParser
{
  /// <summary>Текст ошибки во время сравнения xml</summary>
  string CompareErrorMessage { get; }

  /// <summary>Сравнение узлов</summary>
  /// <param name="AEtalonNode"></param>
  /// <param name="ACustomNode"></param>
  /// <param name="AErrorMessage"></param>
  /// <returns></returns>
  bool CompareNodes(string AEtalonNode, string ACustomNode);

  /// <summary>Извлечение атрибутов со схемы</summary>
  /// <param name="AEtalonNode"></param>
  /// <param name="ACustomNode"></param>
  /// <param name="ADictionary"></param>
  Dictionary<int, string> ExtractAttributeFromNodes(
    Guid ASessionGuid,
    string AEtalonNode,
    string ACustomNode);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SpecificationCreationMode
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// В каком режиме открывается форма "Создание новой спецификации"
/// </summary>
[Serializable]
public enum SpecificationCreationMode
{
  /// <summary>По команде "Создать спецификацию по прототипу"</summary>
  CreateBySpcTemplate = 0,
  /// <summary>Значение по умолчанию</summary>
  Default = 0,
  /// <summary>
  /// По команде "Создать спецификацию", без указания прототипа
  /// </summary>
  CreateNew = 1,
  /// <summary>По команде "Создать версию"</summary>
  CreateVersion = 2,
  /// <summary>По команде "Создать в составе"</summary>
  CreateInclude = 3,
}

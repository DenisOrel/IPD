// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCriterionType
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Вид критерия конфигуратора составов</summary>
[Flags]
[Serializable]
public enum PdmCriterionType
{
  /// <summary>
  /// "Заглушка"-контейнер, в вычислениях не участвует, является контейнером дочерних критериев IPdmCriterion
  /// </summary>
  Stub = 0,
  /// <summary>Обычный критерий</summary>
  Criterion = 1,
  /// <summary>Коллекция критериев</summary>
  Collection = 2,
}

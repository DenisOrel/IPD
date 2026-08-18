// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.CompositionVariants
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Варианты составов, в которых может включаться компонент схемы
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[Description("Варианты составов, в которых может включаться компонент схемы")]
[Category("Misc")]
public enum CompositionVariants
{
  /// <summary>Не используется</summary>
  [Description("Не используется")] NoUsed,
  /// <summary>Только в спецификации</summary>
  [Description("Только в спецификации")] Specification,
  /// <summary>Только в перечне элементов</summary>
  [Description("Только в перечне элементов")] ElementsList,
  /// <summary>В спецификации и в ПЭ</summary>
  [Description("В спецификации и в перечне элементов")] SpecificationAndElementsList,
}

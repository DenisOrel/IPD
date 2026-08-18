// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalNamedFlags
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Библиотека именованных флагов для атрибутов конструкторских документов и изделий.
/// </summary>
internal static class MechanicalNamedFlags
{
  /// <summary>
  /// Значение атрибута фиктивно, реальное значение атрибута находится в таблице произвольной формы, находящейся в содержимом конструкторского документа.
  /// Флаг используется для пометки атрибутов ЕСКД, содержащих текст "см.табл", "см.тт", "изделие-заготовка" и т.д.
  /// </summary>
  public static readonly StringKey TableDrivenValue = new StringKey("TableDriven");
}

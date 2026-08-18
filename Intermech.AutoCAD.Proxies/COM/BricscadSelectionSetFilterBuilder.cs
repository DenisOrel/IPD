// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.BricscadSelectionSetFilterBuilder
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Построитель запросов по содержимому документа BricsCAD.
/// Он позволяет абстрагироваться от "жутких" особенностей реализации этого механизма в CAD-системе.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
public sealed class BricscadSelectionSetFilterBuilder : CadSelectionSetFilterBuilder
{
  /// <summary>
  /// Преобразует DXF entity type во внутреннее строковое представление,
  /// принятое в CAD-системе.
  /// </summary>
  /// <param name="entityType">DXF entity type</param>
  /// <returns>Строковое представление, принятое в CAD-системе</returns>
  protected override string ConvertEntityTypeToFilterString(DxfEntityType entityType)
  {
    return entityType == DxfEntityType.PDFUNDERLAY || entityType == DxfEntityType.DWFUNDERLAY || entityType == DxfEntityType.DGNUNDERLAY ? "PDFREFERENCE" : base.ConvertEntityTypeToFilterString(entityType);
  }
}

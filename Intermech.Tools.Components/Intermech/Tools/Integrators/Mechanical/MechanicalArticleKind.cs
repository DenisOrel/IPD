// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalArticleKind
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>Задает способ обработки изделия интегратором</summary>
public enum MechanicalArticleKind
{
  /// <summary>
  /// Способ обработки определяет на основании атрибутов изделия
  /// </summary>
  Autodetect,
  /// <summary>
  /// Интегратор никак не обрабатывает изделие (используется для изделий, сгенерированных сторонними системами - библиотекой стандартных и др.)
  /// </summary>
  ReadOnlyArticle,
  /// <summary>Изделие обрабатывается по общим правилам</summary>
  NormalArticle,
  /// <summary>
  /// Изделие обрабатывается как изделие, описанное в IMBASE. Это может быть как изделие, так и материал
  /// </summary>
  ImbaseObject,
  /// <summary>
  /// Изделие обрабатывается как неосновной материал, не описанный в IMBASE
  /// </summary>
  MinorMaterial,
}

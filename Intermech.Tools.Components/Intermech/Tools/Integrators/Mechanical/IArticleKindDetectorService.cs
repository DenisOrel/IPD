// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleKindDetectorService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Необязательный сервис для определения вида изделия и способа его обработки.
/// </summary>
public interface IArticleKindDetectorService
{
  /// <summary>
  /// Позволяет определить, является ли указанное изделие или материал объектом Imbase. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия или неосновного материала</param>
  /// <returns>true, если это объект Imbase</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  bool IsImbaseObject(SectionEntity articleItem);

  /// <summary>
  /// Позволяет определить, является ли указанный объект неосновным материалом. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент объекта</param>
  /// <returns>true, если это неосновной материал</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  bool IsMinorMaterial(SectionEntity articleItem);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleKindDetectorService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует базовый класс для сервиса определения вида изделия и способа его обработки.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
public class ArticleKindDetectorService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : MechanicalDriverService(driver, driverContext), IArticleKindDetectorService
{
  /// <summary>
  /// Позволяет определить, является ли указанное изделие или материал объектом Imbase. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия или неосновного материала</param>
  /// <returns>true, если это объект Imbase</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public bool IsImbaseObject(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoIsImbaseObject(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Позволяет определить, является ли указанное изделие или материал объектом Imbase. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия или неосновного материала</param>
  /// <returns>true, если это объект Imbase</returns>
  protected virtual bool DoIsImbaseObject(SectionEntity articleItem)
  {
    return !string.IsNullOrEmpty(articleItem.Sections.Get<AttributesSection>().WorkingSet.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, (string) null));
  }

  /// <summary>
  /// Позволяет определить, является ли указанный объект неосновным материалом. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент объекта</param>
  /// <returns>true, если это неосновной материал</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public bool IsMinorMaterial(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoIsMinorMaterial(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Позволяет определить, является ли указанный объект неосновным материалом. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент объекта</param>
  /// <returns>true, если это неосновной материал</returns>
  protected virtual bool DoIsMinorMaterial(SectionEntity articleItem) => false;
}

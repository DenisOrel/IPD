// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.Imbase.ImbaseSelectorParams
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client.Imbase;

/// <summary>Параметры выбора из каталогов / справочников Imbase</summary>
public class ImbaseSelectorParams
{
  /// <summary>Выбирает один объект из указанного Каталога.</summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="catalogObject">Идентификатор Каталога ( Guid, Идентификатор версии(Int64) или имя (string))</param>
  /// <param name="rawObject">Указывает, создавать ли объект нового типа(false) или вернуть сам выбранный объект(true)</param>
  /// <param name="commitCreation">Указывает создавать ли объект в базе (true) или возвращать заготовку (возвращает отрицательный objectId)</param>
  /// <param name="allowedTypes">Список базовых объектов IMBASE, которые могут быть выбраны</param>
  /// <param name="needType">Тип создаваемого объекта или -1 для типа, определяемого по атрибутам</param>
  /// <returns>Идентификатор выбранного объекта или -1 при отмене выбора</returns>
  public ImbaseSelectorParams(
    string caption,
    string description,
    object catalogObject,
    bool rawObject,
    bool commitCreation,
    int[] allowedTypes,
    int needType)
  {
    this.Caption = caption;
    this.Description = description;
    this.CatalogObject = catalogObject;
    this.RawObject = rawObject;
    this.CommitCreation = commitCreation;
    this.AllowedTypes = allowedTypes;
    this.NeedType = needType;
  }

  /// <summary>Заголовок окна при выборе</summary>
  public string Caption { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Description { get; set; }

  /// <summary>
  /// Идентификатор Каталога ( Guid, Идентификатор версии(Int64) или имя (string))
  /// </summary>
  public object CatalogObject { get; set; }

  /// <summary>
  /// Указывает, создавать ли объект нового типа(false) или вернуть сам выбранный объект(true)
  /// </summary>
  public bool RawObject { get; set; }

  /// <summary>
  /// Указывает создавать ли объект в базе (true) или возвращать заготовку (возвращает отрицательный objectId)
  /// </summary>
  public bool CommitCreation { get; set; }

  /// <summary>
  /// Список базовых объектов IMBASE, которые могут быть выбраны
  /// </summary>
  public int[] AllowedTypes { get; set; }

  /// <summary>
  /// Тип создаваемого объекта или -1 для типа, определяемого по атрибутам
  /// </summary>
  public int NeedType { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public long ContextObjectId { get; set; } = -1;

  /// <summary>
  ///  Кастомный анализатор, передается когда работа стандартного анализатора противоречит логике
  /// </summary>
  public object SelectedItemsAnalyzer { get; set; }

  /// <summary>Флаги управления режимом выбора</summary>
  public SelectionOptions SelectionOptions { get; set; } = SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect;
}

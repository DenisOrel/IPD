// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.BoughtArticleItemSettings
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Настройки, связаннные с покупными изделиями.
/// Сохраняется в настройках по идентификатору связи.
/// </summary>
[Serializable]
public sealed class BoughtArticleItemSettings : BaseOrderItemSetting
{
  /// <summary>
  /// Сделать изделие покупным (1 - Собственное, 2 - Покупное, 3 - По кооперации, 4 - Не изготавливать)
  /// 1, 3 - трактовать как Собственное, 2 - Покупное, 4 - не добавлять в состав вообще
  /// </summary>
  public long IsBoughtArticle = 1;
  /// <summary>Количество для покупного изделия</summary>
  public MeasuredValue BoughtQuantity;
  /// <summary>Исходное количество (атрибут связи)</summary>
  public MeasuredValue SourceQuantity;
  /// <summary>
  /// Надо ли создавать новое покупное изделие по данным настройкам
  /// </summary>
  public bool CreateNewInstance;

  /// <summary>Создать пустой экземпляр класса</summary>
  public BoughtArticleItemSettings()
  {
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public BoughtArticleItemSettings(object source) => this.Assign(source);

  /// <summary>Разница между исходным и покупным количеством</summary>
  public MeasuredValue RestQuantity
  {
    get
    {
      if (this.BoughtQuantity == null || this.SourceQuantity == null)
        return (MeasuredValue) null;
      this.CheckSettings();
      return new MeasuredValue(this.SourceQuantity.Value - this.BoughtQuantity.Value, this.SourceQuantity.MeasureID);
    }
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    lock (this.syncRoot)
    {
      this.IsBoughtArticle = 1L;
      this.BoughtQuantity = (MeasuredValue) null;
      this.SourceQuantity = (MeasuredValue) null;
      this.CreateNewInstance = false;
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is BoughtArticleItemSettings articleItemSettings))
      return;
    lock (this.syncRoot)
    {
      this.IsBoughtArticle = articleItemSettings.IsBoughtArticle;
      this.BoughtQuantity = articleItemSettings.BoughtQuantity != null ? new MeasuredValue(articleItemSettings.BoughtQuantity.Value, articleItemSettings.BoughtQuantity.MeasureID) : (MeasuredValue) null;
      this.SourceQuantity = articleItemSettings.SourceQuantity != null ? new MeasuredValue(articleItemSettings.SourceQuantity.Value, articleItemSettings.SourceQuantity.MeasureID) : (MeasuredValue) null;
      this.CreateNewInstance = articleItemSettings.CreateNewInstance;
    }
    this.CheckSettings();
  }

  /// <summary>
  /// Редактируемые данные (возвращаем ссылку на покупное количество)
  /// </summary>
  public override object Data
  {
    [DebuggerStepThrough] get => (object) this;
  }

  /// <summary>Выполнить проверку количеств</summary>
  public void CheckSettings()
  {
    if (this.SourceQuantity == null)
      this.BoughtQuantity = (MeasuredValue) null;
    if (this.IsBoughtArticle == 2L && this.SourceQuantity != null && this.BoughtQuantity == null)
      this.BoughtQuantity = new MeasuredValue(this.SourceQuantity.Value, this.SourceQuantity.MeasureID);
    if (this.BoughtQuantity == null)
      return;
    if (this.SourceQuantity.MeasureID != this.BoughtQuantity.MeasureID)
      this.BoughtQuantity = new MeasuredValue(this.BoughtQuantity.Value, this.SourceQuantity.MeasureID);
    if (this.BoughtQuantity.Value > this.SourceQuantity.Value)
      this.BoughtQuantity = new MeasuredValue(this.SourceQuantity.Value, this.SourceQuantity.MeasureID);
    if (this.BoughtQuantity.Value >= 0.0)
      return;
    this.BoughtQuantity = new MeasuredValue(0.0, this.SourceQuantity.MeasureID);
  }

  /// <summary>
  /// Сделать настройки для признака изготовления "Собственное"
  /// </summary>
  public void MakeOwn()
  {
    this.IsBoughtArticle = 1L;
    this.SourceQuantity = this.RestQuantity;
    this.BoughtQuantity = this.SourceQuantity != null ? new MeasuredValue(0.0, this.SourceQuantity.MeasureID) : (MeasuredValue) null;
    this.CreateNewInstance = false;
    this.CheckSettings();
  }
}

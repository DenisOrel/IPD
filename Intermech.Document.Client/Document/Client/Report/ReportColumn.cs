// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.ReportColumn
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Колонка отчета</summary>
internal class ReportColumn
{
  /// <summary>
  /// Разделитель, необходим для формирования значения атрибута
  /// </summary>
  private char _separator = '|';
  /// <summary>Идентификатор атрибута</summary>
  public int AttributeID;
  /// <summary>Наименование атрибута</summary>
  public string AttributeName = string.Empty;
  /// <summary>Тип данных</summary>
  public FieldTypes AttributeType;
  /// <summary>Порядковый номер в коллекции</summary>
  public int Index = -1;
  /// <summary>Заголовок колонки</summary>
  public string Caption = string.Empty;
  /// <summary>Ширина колонки</summary>
  public int Width = -1;
  /// <summary>Порядок сортировки</summary>
  public SortOrders SortOrder;
  /// <summary>Вычисление итога</summary>
  public MathTotal Result;
  /// <summary>Принадлежность атрибута</summary>
  public AttributeSourceTypes AttributeSource = AttributeSourceTypes.Object;
  /// <summary>Выравнивание в колонке</summary>
  public HorzAlignment Alignment;
  /// <summary>Строка форматирования</summary>
  public string FormatString = string.Empty;

  /// <summary>Конструктор 1</summary>
  public ReportColumn()
  {
  }

  /// <summary>Конструктор 2</summary>
  /// <param name="colStr">Строковое представление атрибута</param>
  /// <param name="session">Сессия</param>
  public ReportColumn(string colStr, IUserSession session)
  {
    string[] strArray = colStr.Split('|');
    if (strArray.Length != 9)
      return;
    IDBAttributeType attributeType = session.GetAttributeType(Convert.ToInt32(strArray[0]));
    this.AttributeID = attributeType.AttributeID;
    this.AttributeName = attributeType.Name;
    this.AttributeType = attributeType.AttributeType != FieldTypes.ftSystem ? attributeType.AttributeType : ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeType.AttributeID);
    this.Index = Convert.ToInt32(strArray[1]);
    this.Caption = Convert.ToString(strArray[2]);
    this.Width = Convert.ToInt32(strArray[3]);
    this.SortOrder = (SortOrders) Convert.ToInt32(strArray[4]);
    this.Result = (MathTotal) Convert.ToInt32(strArray[5]);
    this.AttributeSource = (AttributeSourceTypes) Convert.ToInt32(strArray[6]);
    this.Alignment = (HorzAlignment) Convert.ToInt32(strArray[7]);
    this.FormatString = Convert.ToString(strArray[8]);
  }

  /// <summary>
  /// Формируем из класса строковое представление атрибута
  /// для записи в БД
  /// </summary>
  /// <returns></returns>
  public string ToValue()
  {
    return this.AttributeID.ToString() + this._separator.ToString() + this.Index.ToString() + this._separator.ToString() + this.Caption + this._separator.ToString() + this.Width.ToString() + this._separator.ToString() + ((int) this.SortOrder).ToString() + this._separator.ToString() + ((int) this.Result).ToString() + this._separator.ToString() + ((int) this.AttributeSource).ToString() + this._separator.ToString() + ((int) this.Alignment).ToString() + this._separator.ToString() + this.FormatString;
  }
}

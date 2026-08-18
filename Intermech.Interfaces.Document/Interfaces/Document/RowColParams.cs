// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RowColParams
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Параметры строки или столбца</summary>
[TypeConverter(typeof (RowColumnParamsConverter))]
[Serializable]
public class RowColParams : ICloneable, IWriteReadXml
{
  /// <summary>Значение соответствующее неустановленному идентификатору строки/столбца</summary>
  public static int EmptyIDValue = int.MinValue;
  private string name = "";
  private int id = RowColParams.EmptyIDValue;
  private int templateID = RowColParams.EmptyIDValue;
  private float size = RectangleElement.EmptyFloatValue;
  private CellType cellType;
  private BorderLine borderLine1;
  private BorderLine borderLine2;
  private TableData ownerTable;
  private bool isColumn = true;

  /// <summary>Пустой конструктор для создания через CreateInstance</summary>
  protected RowColParams()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerTable">Таблица владелец столбца/строки</param>
  /// <param name="isColumn">Признак столбца/строки</param>
  /// <param name="id">Идентификатор строки/столбца</param>
  /// <param name="name">Имя строки/столбца</param>
  /// <param name="size">Размер (высота/ширина) строки/столбца</param>
  public RowColParams(TableData ownerTable, bool isColumn, int id, string name, float size)
  {
    this.ownerTable = ownerTable;
    this.isColumn = isColumn;
    this.id = id;
    this.name = name;
    this.size = size;
  }

  /// <summary>Конструктор столбца</summary>
  /// <param name="ownerTable">Таблица владелец столбца</param>
  /// <param name="id">Идентификатор столбца</param>
  /// <param name="name">Имя столбца</param>
  /// <param name="size">Размер ширина столбца</param>
  public RowColParams(TableData ownerTable, int id, string name, float size)
  {
    this.ownerTable = ownerTable;
    this.id = id;
    this.name = name;
    this.size = size;
  }

  /// <summary>Это параметры столбца</summary>
  [Browsable(false)]
  public bool IsColumn
  {
    [DebuggerStepThrough] get => this.isColumn;
  }

  public override string ToString() => $"{this.Index.ToString()}/{this.Size.ToString()}";

  /// <summary>Назначить значение свойству IsColumn. Только для внутреннего использования.</summary>
  /// <param name="value">Значение</param>
  internal void SetIsColumn(bool value) => this.isColumn = value;

  /// <summary>Таблица владеющая столбцом</summary>
  [Browsable(false)]
  public TableData OwnerTable
  {
    [DebuggerStepThrough] get => this.ownerTable;
  }

  /// <summary>Установить новое значение OwnerTable</summary>
  /// <param name="value">Новое значение</param>
  public void SetOwnerTable(TableData value) => this.ownerTable = value;

  /// <summary>Имя строки/столбца</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_420")]
  [CustomDescription("Attribute.Interfaces.Document_421")]
  [CustomCategory("Attribute.Interfaces.Document_422")]
  public string ColRowName
  {
    [DebuggerStepThrough] get => this.name;
    set
    {
      if (!(this.name != value))
        return;
      this.name = value;
      if (this.ownerTable == null)
        return;
      int gridColIndex = this.ownerTable.GridColumnsParams.IndexOf(this);
      List<DocumentTreeNode> columnCells = new List<DocumentTreeNode>();
      this.ownerTable.GetGridColumnCells(gridColIndex, this.ownerTable.GridColumnsParams, (IList<DocumentTreeNode>) columnCells);
      NameChanged_EventArgs e = new NameChanged_EventArgs(this.name);
      for (int index = 0; index < columnCells.Count; ++index)
      {
        if (columnCells[index].Name == null || columnCells[index].Name == "")
          columnCells[index].OnNameChanged(e);
      }
    }
  }

  /// <summary>Размер (высота/ширина) строки/столбца</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_423")]
  [CustomDescription("Attribute.Interfaces.Document_424")]
  [CustomCategory("Attribute.Interfaces.Document_425")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (FloatConverter))]
  public float Size
  {
    [DebuggerStepThrough] get => this.size;
    set => this.AssignSize(value, true, true);
  }

  /// <summary>Назначить значение Size</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить пользовательский интерфейс во владельце</param>
  /// <param name="updateLayout">Обновить разбивку во владельце</param>
  public virtual void AssignSize(float value, bool updateUI, bool updateLayout)
  {
    if ((double) this.size == (double) value)
      return;
    this.size = value;
    if (this.ownerTable == null)
      return;
    this.ownerTable.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
  }

  /// <summary>Тип верхней/левой линии строки/столбца</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_426")]
  [CustomDescription("Attribute.Interfaces.Document_427")]
  [CustomCategory("Attribute.Interfaces.Document_428")]
  [Browsable(false)]
  public BorderLine BorderLine1
  {
    [DebuggerStepThrough] get => this.borderLine1;
    set
    {
      if (this.BorderLine1 == value)
        return;
      this.borderLine1 = value;
      if (this.ownerTable == null)
        return;
      bool suspendedRefreshUiFlag = this.ownerTable.SuspendedRefreshUIFlag;
      if (!suspendedRefreshUiFlag)
        this.ownerTable.SuspendRefreshUI();
      try
      {
        if (!this.isColumn)
          return;
        List<RowColParams> gridColumnsParams = this.ownerTable.GridColumnsParams;
        if (gridColumnsParams == null)
          return;
        int num = gridColumnsParams.IndexOf(this);
        if (num == -1 || num <= 0 || gridColumnsParams[num - 1] == null)
          return;
        gridColumnsParams[num - 1].BorderLine2 = this.borderLine1;
      }
      finally
      {
        if (!suspendedRefreshUiFlag)
          this.ownerTable.ResumeRefreshUI(true);
      }
    }
  }

  /// <summary>Только для внутреннего использования.
  /// Назначить новое значение свойству, без автоматического назначения в ячейках</summary>
  public void AssignBorderLine1(BorderLine value) => this.borderLine1 = value;

  /// <summary>Тип нижней/правой линии строки/столбца</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_429")]
  [CustomDescription("Attribute.Interfaces.Document_430")]
  [CustomCategory("Attribute.Interfaces.Document_431")]
  [Browsable(false)]
  public BorderLine BorderLine2
  {
    [DebuggerStepThrough] get => this.borderLine2;
    set
    {
      if (this.BorderLine2 == value)
        return;
      this.borderLine2 = value;
      if (this.ownerTable == null)
        return;
      bool suspendedRefreshUiFlag = this.ownerTable.SuspendedRefreshUIFlag;
      if (!suspendedRefreshUiFlag)
        this.ownerTable.SuspendRefreshUI();
      try
      {
        if (!this.isColumn)
          return;
        List<RowColParams> gridColumnsParams = this.ownerTable.GridColumnsParams;
        if (gridColumnsParams == null)
          return;
        int num = gridColumnsParams.IndexOf(this);
        if (num == -1 || num >= gridColumnsParams.Count - 1 || gridColumnsParams[num + 1] == null)
          return;
        gridColumnsParams[num + 1].BorderLine1 = this.borderLine2;
      }
      finally
      {
        if (!suspendedRefreshUiFlag)
          this.ownerTable.ResumeRefreshUI(true);
      }
    }
  }

  /// <summary>Только для внутреннего использования.
  /// Назначить новое значение свойству, без автоматического назначения в ячейках</summary>
  public void AssignBorderLine2(BorderLine value) => this.borderLine2 = value;

  /// <summary>Только для столбцов. Исправить левую линию,
  /// если она не соответствует правой линии предыдущего столбца</summary>
  internal void CorrectColumnBorderLine1()
  {
    if (this.borderLine1 != null || !this.isColumn || this.ownerTable == null)
      return;
    List<RowColParams> gridColumnsParams = this.ownerTable.GridColumnsParams;
    if (gridColumnsParams == null)
      return;
    int num = gridColumnsParams.IndexOf(this);
    if (num == -1)
      return;
    if (num > 0)
      this.borderLine1 = gridColumnsParams[num - 1].borderLine2;
    else
      this.borderLine1 = this.borderLine2;
  }

  /// <summary>Индекс в сетке</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_432")]
  [CustomDescription("Attribute.Interfaces.Document_433")]
  [CustomCategory("Attribute.Interfaces.Document_434")]
  public int Index
  {
    [DebuggerStepThrough] get
    {
      if (this.ownerTable != null && this.isColumn)
      {
        List<RowColParams> gridColumnsParams = this.ownerTable.GridColumnsParams;
        if (gridColumnsParams != null)
          return gridColumnsParams.IndexOf(this);
      }
      return -1;
    }
  }

  /// <summary>Тип строки/столбца</summary>
  [ReadOnly(true)]
  [CustomDisplayName("Attribute.Interfaces.Document_435")]
  [CustomDescription("Attribute.Interfaces.Document_436")]
  [CustomCategory("Attribute.Interfaces.Document_437")]
  public CellType CellType
  {
    [DebuggerStepThrough] get => this.cellType;
    set => this.cellType = value;
  }

  /// <summary>Идентификатор строки/столбца</summary>
  [ReadOnly(true)]
  [CustomDisplayName("Attribute.Interfaces.Document_438")]
  [CustomDescription("Attribute.Interfaces.Document_439")]
  [CustomCategory("Attribute.Interfaces.Document_440")]
  [Browsable(false)]
  public int ID
  {
    [DebuggerStepThrough] get => this.id;
    set => this.id = value;
  }

  /// <summary>Ссылка на шаблон</summary>
  [Browsable(false)]
  public int TemplateID
  {
    [DebuggerStepThrough] get => this.templateID;
    set => this.templateID = value;
  }

  /// <summary>Элемент имеет шаблон</summary>
  [Browsable(false)]
  public bool HasTemplate
  {
    [DebuggerStepThrough] get => this.templateID != RowColParams.EmptyIDValue;
  }

  /// <summary>Получить шаблон элемента сетки</summary>
  /// <param name="owner">Владелец сетки</param>
  /// <param name="isColumn">Элемент - столбец</param>
  /// <returns>Шаблон элемента сетки</returns>
  public RowColParams GetTemplate(TableData owner, bool isColumn)
  {
    RowColParams template1 = (RowColParams) null;
    if (this.templateID != RowColParams.EmptyIDValue && owner.Template is TableData template2)
    {
      List<RowColParams> gridParams = !isColumn ? template2.GridRowsParams : template2.GridColumnsParams;
      if (gridParams != null)
        template1 = TableData.GetRowColParams(gridParams, this.templateID);
    }
    return template1;
  }

  /// <summary>Наборы столбцов имеют одинаковые элементы</summary>
  /// <param name="params0"></param>
  /// <param name="params1"></param>
  /// <returns></returns>
  public static bool IsEqual(List<RowColParams> params0, List<RowColParams> params1)
  {
    if (params0 == null && params1 == null)
      return true;
    if (params0 == null || params1 == null || params0.Count != params1.Count)
      return false;
    for (int index = 0; index < params0.Count; ++index)
    {
      if (params0[index] != params1[index])
        return false;
    }
    return true;
  }

  /// <summary>Получить размер элемента сетки с учетом наследования</summary>
  /// <param name="owner">Владелец сетки</param>
  /// <param name="isColumn">Элемент - столбец</param>
  /// <returns>Размер элемента сетки</returns>
  public float GetSize(TableData owner, bool isColumn)
  {
    if ((double) this.size != (double) RectangleElement.EmptyFloatValue)
      return this.size;
    if (owner != null)
    {
      RowColParams template = this.GetTemplate(owner, isColumn);
      if (template != null)
        return template.GetSize(owner.Template as TableData, isColumn);
    }
    return 0.0f;
  }

  /// <summary>Клонировать экземпляр</summary>
  public virtual RowColParams Clone()
  {
    RowColParams rowColParams = new RowColParams();
    rowColParams.name = this.name;
    rowColParams.size = this.size;
    rowColParams.cellType = this.cellType;
    rowColParams.id = this.id;
    rowColParams.templateID = this.templateID;
    rowColParams.isColumn = this.isColumn;
    if (this.borderLine1 != null)
      rowColParams.borderLine1 = this.borderLine1.Clone();
    if (this.borderLine2 != null)
      rowColParams.borderLine2 = this.borderLine2.Clone();
    return rowColParams;
  }

  /// <summary>Клонировать экземпляр</summary>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "BorderLine":
        this.borderLine2 = new BorderLine();
        this.borderLine2.ReadFromXml(readArgs);
        return true;
      case "BorderLine1":
        this.borderLine1 = new BorderLine();
        this.borderLine1.ReadFromXml(readArgs);
        return true;
      case "BorderLine2":
        this.borderLine2 = new BorderLine();
        this.borderLine2.ReadFromXml(readArgs);
        return true;
      case "cellType":
        this.cellType = (CellType) Enum.Parse(typeof (CellType), readArgs.Reader.Value);
        return true;
      case "gridID":
        this.id = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "name":
        this.name = readArgs.Reader.Value;
        return true;
      case "size":
        this.size = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "templateID":
        this.templateID = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      default:
        return false;
    }
  }

  /// <summary>Записать атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.name != null)
      xw.WriteAttributeString("name", this.name);
    if ((double) this.size != (double) RectangleElement.EmptyFloatValue)
      xw.WriteAttributeString("size", this.size.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.id != RowColParams.EmptyIDValue)
      xw.WriteAttributeString("gridID", this.id.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.templateID != RowColParams.EmptyIDValue)
      xw.WriteAttributeString("templateID", this.templateID.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.cellType == CellType.DataCell)
      return;
    xw.WriteAttributeString("cellType", this.cellType.ToString());
  }

  /// <summary>Записать элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    WriteReadXmlHelper.WriteXmlElement("BorderLine1", (IWriteReadXml) this.borderLine1, true, xw, objectRefId);
    WriteReadXmlHelper.WriteXmlElement("BorderLine2", (IWriteReadXml) this.borderLine2, true, xw, objectRefId);
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    this.WriteXmlAttributes(xw, objectRefId);
    this.WriteXmlElements(xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }
}

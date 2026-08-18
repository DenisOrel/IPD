// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eCell
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Класс-описатель ячейки в таблице</summary>
[Serializable]
public class eCell : ICloneable, ISerializable
{
  private eCellDestination _cellDest;
  private eCellType _cellType = eCellType.Text;
  private eCellSymbol _cellSymbol;
  private int _colSpan = 1;
  private int _rowSpan = 1;
  private CommonTypeHolder _commonType;
  private ExpertValue _cellValue;
  private bool _overrideByValue;

  /// <summary>Конструктор</summary>
  public eCell()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellDest">Назначение ячейки</param>
  /// <param name="cellType">Тип ячейки</param>
  public eCell(eCellDestination cellDest, eCellType cellType)
  {
    this._cellDest = cellDest;
    this._cellType = cellType;
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellDest">Назначение ячейки</param>
  /// <param name="commonType">Описатель ячейки</param>
  public eCell(eCellDestination cellDest, CommonTypeHolder commonType)
  {
    this._cellDest = cellDest;
    this._commonType = commonType;
    if (this._commonType == null)
      return;
    this._cellType = this.GetCellType(this._commonType.AttributeType.FieldTypes);
  }

  private eCellType GetCellType(FieldTypes type)
  {
    switch (this._cellDest)
    {
      case eCellDestination.Header:
      case eCellDestination.Result:
        return eCellType.Text;
      default:
        return eCellType.Value;
    }
  }

  /// <summary>Перекрыть значением</summary>
  public bool OverrideByValue
  {
    set => this._overrideByValue = value;
    get => this._overrideByValue;
  }

  /// <summary>Возвращает пустая ли ячейка</summary>
  public bool isEmpty
  {
    get
    {
      if (this._cellSymbol.Equals((object) eCellSymbol.Other))
        return false;
      if (this._cellValue == null || this._cellValue.Value == null)
        return true;
      switch (this._cellValue.ValueType)
      {
        case DataType.String:
          return this._cellValue.Value.Equals((object) string.Empty);
        case DataType.ObjectLink:
        case DataType.ObjectIdLink:
          return this._cellValue.Value.Equals((object) Convert.ToInt64(-1));
        default:
          return false;
      }
    }
  }

  /// <summary>Назначение ячейки</summary>
  public eCellDestination CellDestination
  {
    get => this._cellDest;
    set => this._cellDest = value;
  }

  /// <summary>Тип ячейки</summary>
  public eCellType CellType
  {
    get => this._cellType;
    set => this._cellType = value;
  }

  /// <summary>Символ-условие на аргументы</summary>
  public eCellSymbol CellSymbol
  {
    get => this._cellSymbol;
    set => this._cellSymbol = value;
  }

  /// <summary>Связка "тип объекта":"тип атрибута" для этой ячейки</summary>
  public CommonTypeHolder CommonType
  {
    get => this._commonType;
    set => this._commonType = value;
  }

  /// <summary>Значение ячейки</summary>
  public ExpertValue CellValue
  {
    get
    {
      if (this._cellValue != null)
        return this._cellValue;
      if (this._commonType == null)
        return ExpertValue.Empty(DataType.String);
      FieldTypes attrType = this._commonType.AttributeType.FieldTypes;
      if (attrType == FieldTypes.ftSystem)
        attrType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) this._commonType.AttributeType.SourceAttributeID);
      return ExpertValue.Empty(DataTypeConvertor.AttrType2DataType(attrType));
    }
    set => this._cellValue = value;
  }

  public ExpertValue _RealCellValue => this._cellValue;

  /// <summary>
  /// Пустое ли значение у ячейки (через CellValue хрен получишь...)
  /// </summary>
  public bool IsCellEmpty => this._cellValue == null;

  /// <summary>К-во столбцов, которые будет занимать данная ячейка</summary>
  public int ColSpan
  {
    get => this._colSpan;
    set => this._colSpan = value;
  }

  /// <summary>Кол-во рядов, которые будет занимать данная ячейка</summary>
  public int RowSpan
  {
    get => this._rowSpan;
    set => this._rowSpan = value;
  }

  /// <summary>Строковое представления значения в ячейке</summary>
  /// <returns>строка со значением в ячейке</returns>
  public override string ToString()
  {
    string str = string.Empty;
    switch (this._cellDest)
    {
      case eCellDestination.Data:
        if (this._cellValue != null)
        {
          str = this.CellValue.ValueType != DataType.Boolean ? str + this._cellValue.ToString() : (this._cellValue.Value == null || !this._cellValue.Value.Equals((object) true) ? str + LocalizationHolder.rm.GetString("Expert_238") : str + LocalizationHolder.rm.GetString("Expert_237"));
          break;
        }
        break;
      case eCellDestination.Header:
      case eCellDestination.Result:
        if (this._commonType != null && !this._overrideByValue)
        {
          if (!this._cellSymbol.Equals((object) eCellSymbol.None))
            str += eCellSymbolHelper.GetSymbol(this._cellSymbol);
          str += this._commonType.ToString();
          break;
        }
        if (this._cellValue != null)
        {
          if (!this._cellSymbol.Equals((object) eCellSymbol.None))
            str += eCellSymbolHelper.GetSymbol(this._cellSymbol);
          str = this.CellValue.ValueType != DataType.Boolean ? str + this._cellValue.ToString() : (this._cellValue.Value == null || !this._cellValue.Value.Equals((object) true) ? str + LocalizationHolder.rm.GetString("Expert_238") : str + LocalizationHolder.rm.GetString("Expert_237"));
          break;
        }
        str += LocalizationHolder.rm.GetString("Expert_3");
        break;
      case eCellDestination.HeaderData:
        if (this._cellSymbol.Equals((object) eCellSymbol.Other))
        {
          str += eCellSymbolHelper.GetSymbol(this._cellSymbol);
          break;
        }
        if (this._cellValue != null)
        {
          switch (this._cellSymbol)
          {
            case eCellSymbol.None:
              str = this.CellValue.ValueType != DataType.Boolean ? str + this._cellValue.ToString() : (this._cellValue.Value == null || !this._cellValue.Value.Equals((object) true) ? str + LocalizationHolder.rm.GetString("Expert_238") : str + LocalizationHolder.rm.GetString("Expert_237"));
              break;
            case eCellSymbol.Set:
              str += $"{eCellSymbolHelper.GetSymbol(this._cellSymbol)} ";
              goto case eCellSymbol.None;
            default:
              str += eCellSymbolHelper.GetSymbol(this._cellSymbol);
              goto case eCellSymbol.None;
          }
        }
        else
          break;
        break;
    }
    return str;
  }

  /// <summary>Проверка равенства ячейки</summary>
  /// <param name="obj">Другая ячейка</param>
  /// <returns>True если ячейки имеют равные параметры</returns>
  public override bool Equals(object obj)
  {
    if (obj == null || !obj.GetType().Equals(typeof (eCell)))
      return base.Equals(obj);
    eCell eCell = obj as eCell;
    int num1 = !this._cellDest.Equals((object) eCell._cellDest) || !this._cellType.Equals((object) eCell._cellType) || !this._cellSymbol.Equals((object) eCell._cellSymbol) || !this._colSpan.Equals(eCell._colSpan) ? 0 : (this._rowSpan.Equals(eCell._rowSpan) ? 1 : 0);
    bool flag1 = this._cellValue != null;
    bool flag2 = this._cellValue != null ^ eCell._cellValue != null;
    int num2 = !flag1 || flag2 ? (!flag1 & flag2 ? 1 : 0) : (this._cellValue.Equals((object) eCell._cellValue) ? 1 : 0);
    return (num1 & num2) != 0;
  }

  /// <summary>Проверка на равенство структуры ячейки</summary>
  /// <param name="cell">Другая ячейка</param>
  /// <returns>True если структура ячеек равна (то есть, совпадают тип объекта и тип атрибута)</returns>
  public bool EqualsStructure(eCell cell)
  {
    return cell != null && this._cellType.Equals((object) cell._cellType) && this._commonType != null && this._commonType.Equals((object) cell._commonType);
  }

  /// <summary>Получение hashcode'a для объекта</summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  public virtual bool Assign(eCell other)
  {
    if (other == null)
      return false;
    if (this._cellDest == other._cellDest)
    {
      this._cellType = other._cellType;
      this._cellSymbol = other._cellSymbol;
    }
    if (other._cellValue == null)
      return false;
    if (this._cellValue == null)
      this._cellValue = this.CellValue;
    if (this._cellValue.ValueType == DataType.ObjectLink && other._cellValue.ValueType == DataType.Packet)
      this._cellValue = new ExpertValue(DataType.Packet, other._cellValue.Value);
    else if (this._cellValue.ValueType == other._cellValue.ValueType)
    {
      this._cellValue.Value = other._cellValue.Value;
      return true;
    }
    try
    {
      switch (this._cellValue.ValueType)
      {
        case DataType.Integer:
        case DataType.ObjectLink:
        case DataType.Attribute:
        case DataType.ObjectIdLink:
          this._cellValue.Value = (object) Convert.ToInt64((object) other._cellValue);
          return true;
        case DataType.Float:
          this._cellValue.Value = (object) Convert.ToDouble((object) other._cellValue);
          return true;
        case DataType.Measured:
          if (other._cellValue.ValueType == DataType.String)
            this._cellValue.Value = (object) MeasureHelper.ConvertToMeasuredValue(Convert.ToString((object) other._cellValue));
          if (other._cellValue.ValueType == DataType.Float || other._cellValue.ValueType == DataType.Integer || other._cellValue.ValueType == DataType.String)
          {
            double aValue = Convert.ToDouble((object) other._cellValue);
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._commonType.AttributeType.Guid);
            if (attributeType != null)
            {
              long sizeType = attributeType.SizeType;
              this._cellValue.Value = (object) new MeasuredValue(aValue, MeasureHelper.GetBaseMeasureID(sizeType));
            }
            else
              this._cellValue.Value = (object) new MeasuredValue(aValue, ExpertConsts.Consts.measureShtuk);
          }
          return false;
        case DataType.String:
          this._cellValue.Value = (object) Convert.ToString((object) other._cellValue);
          return true;
        case DataType.Date:
          this._cellValue.Value = (object) Convert.ToDateTime((object) other._cellValue);
          return true;
        case DataType.Boolean:
          this._cellValue.Value = (object) Convert.ToBoolean((object) other._cellValue);
          return true;
        case DataType.Packet:
        case DataType.ObjType:
        case DataType.RelType:
        case DataType.Unknown:
          break;
        case DataType.Diap:
          if (other._cellValue.ValueType != DataType.Integer && other._cellValue.ValueType != DataType.Float && other._cellValue.ValueType != DataType.Date && other._cellValue.ValueType != DataType.String)
            return false;
          this._cellValue.Value = (object) new DiapValue(other._cellValue, other._cellValue);
          return true;
        default:
          return false;
      }
    }
    catch (Exception ex)
    {
      switch (ex)
      {
        case InvalidCastException _:
        case FormatException _:
        case OverflowException _:
          if (this._cellValue.ValueType == DataType.ObjectLink)
          {
            this._cellValue.Value = (object) null;
            break;
          }
          break;
        default:
          throw;
      }
    }
    return false;
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    eCell eCell = this._commonType != null ? new eCell(this._cellDest, this._commonType.Clone() as CommonTypeHolder) : new eCell(this._cellDest, eCellType.Value);
    eCell._cellSymbol = this._cellSymbol;
    eCell._cellValue = this._cellValue != null ? this._cellValue.Clone() as ExpertValue : (ExpertValue) null;
    eCell._colSpan = this._colSpan;
    eCell._rowSpan = this._rowSpan;
    return (object) eCell;
  }

  /// <summary>Десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected eCell(SerializationInfo info, StreamingContext context)
  {
    Dictionary<string, Type> paramsType = SerializationInfoHelper.GetParamsType(info);
    Type type = (Type) null;
    ref Type local = ref type;
    if (paramsType.TryGetValue("C_Dest", out local))
    {
      this._cellDest = (eCellDestination) info.GetInt32("C_Dest");
      this._cellType = (eCellType) info.GetInt32("C_Type");
      this._cellSymbol = (eCellSymbol) info.GetInt32("C_Sym");
    }
    else
    {
      this._cellDest = (eCellDestination) EnumTypeHelper.GetEnumValue(typeof (eCellDestination), info.GetString(nameof (CellDestination)), (object) eCellDestination.Data);
      this._cellType = (eCellType) EnumTypeHelper.GetEnumValue(typeof (eCellType), info.GetString(nameof (CellType)), (object) eCellType.Text);
      this._cellSymbol = eCellSymbolHelper.GetSymbol(info.GetString(nameof (CellSymbol)));
    }
    this._colSpan = info.GetInt32(nameof (ColSpan));
    this._rowSpan = info.GetInt32(nameof (RowSpan));
    this._commonType = info.GetValue("TypeHolder", typeof (CommonTypeHolder)) as CommonTypeHolder;
    if (this._commonType != null)
      this._commonType.UnifyHolders();
    this._cellValue = info.GetValue("Value", typeof (ExpertValue)) as ExpertValue;
    if (this._cellValue == null || this._cellValue.ValueType != DataType.Measured || this._cellValue.Value == null || this._commonType == null || this._commonType.AttributeType.SavedMeasure != null || !(this._cellValue.Value is MeasuredValue) || ((MeasuredValue) this._cellValue.Value).MeasureID == 0L)
      return;
    this._commonType.AttributeType.SavedMeasure = MeasureHelper.FindDescriptor(((MeasuredValue) this._cellValue.Value).MeasureID);
  }

  /// <summary>Сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("C_Dest", (int) this._cellDest);
    info.AddValue("C_Type", (int) this._cellType);
    info.AddValue("C_Sym", (int) this._cellSymbol);
    info.AddValue("ColSpan", this._colSpan);
    info.AddValue("RowSpan", this._rowSpan);
    info.AddValue("TypeHolder", (object) this._commonType);
    info.AddValue("Value", (object) this._cellValue);
  }

  public bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    return this._commonType != null && this._commonType.PerformAttrCombine(fromAttribute, toAttribute, session);
  }
}

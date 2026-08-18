// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.FieldRecord
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Portal;

public class FieldRecord
{
  /// <summary>Идентификатор поля</summary>
  public int Key;
  /// <summary>
  /// Внутреннее имя поля в таблице. Служит для связи дополнительных атрибутов, описанных в этой таблице с соответствующими полями таблицы базы данных
  /// Соответствует данным из поля F_FIELD VARCHAR2(8)
  /// </summary>
  public string Field;
  /// <summary>
  /// Единицы измерения
  /// Соответствует данным из поля F_UNITS VARCHAR2(8)
  /// </summary>
  public string Units;
  /// <summary>
  /// Определяет порядок следования полей при показе таблицы
  /// Соответствует данным из поля F_SORT NUMBER(10, 0)
  /// </summary>
  public int Sort;
  /// <summary>
  /// Битовые значения для флагов, используется при показе таблицы и в прикладных системах
  /// Соответствует данным из поля F_FLAGS NUMBER(10, 0)
  /// </summary>
  public int Flags;
  /// <summary>
  /// Ширина поля при отображении в таблице. Положительное значение указывает на ширину поля в символах. Отрицательное значение - в пикселах
  /// Соответствует данным из поля F_WIDTH NUMBER(10, 0)
  /// </summary>
  public long Width;
  /// <summary>
  /// Вид данных в поле. Значения соответствуют значениям из перечисления ImDataMode
  /// Соответствует данным из поля F_TYPE NUMBER(10, 0)
  /// </summary>
  public ImDataMode DataMode;
  /// <summary>
  /// Если содержит отрицательное значение, то соответствующее  поле не может быть пустым. Дополнительно указывает точность форматирования полей вещественного типа
  /// Соответствует данным из поля F_REQUIRED NUMBER(5, 0)
  /// </summary>
  public int Required;
  /// <summary>
  /// Тип данных поля. Значения соответствуют значениям из перечисления ImDataTypeEx
  /// Соответствует данным из поля F_DATATYPE NUMBER(10, 0)
  /// </summary>
  public FieldTypes FieldType;
  /// <summary>
  /// Определяет режим ввода данных в поле. Возможные значения приведены в перечислении ImEnterMode
  /// Соответствует данным из поля F_ENTERMODE NUMBER(10, 0)
  /// </summary>
  public ImEnterMode EnterMode;
  /// <summary>
  /// Дополнительные данные поля. Для вычисляемых полей содержит текст макроподстановки или формулу. Для обычных полей содержит значение в поле по умолчанию, для логических полей содержть текст для отображения логических значений
  /// Соответствует данным из поля F_DATA VARCHAR2(255)
  /// </summary>
  public string Data;
  /// <summary>
  /// Длинное имя поля
  /// Соответствует данным из поля F_LONGNAME VARCHAR2(64)
  /// </summary>
  public string LongName;
  /// <summary>
  /// Короткое имя поля. Используется для связи поля с параметрами параметрической модели при 2-D и 3-D
  /// Соответствует данным из поля F_SHORTNAME VARCHAR2(8)
  /// </summary>
  public string ShortName;
  /// <summary>GUID соответствующего атрибута в новой базе</summary>
  public Guid GUID;

  public FieldRecord(DataRow row)
  {
    this.Key = Convert.ToInt32(row["F_KEY"]);
    this.Field = Convert.ToString(row["F_FIELD"]);
    this.LongName = Convert.ToString(row["F_LONGNAME"]);
    if (this.LongName.Equals(string.Empty))
      this.LongName = "Атрибут";
    this.ShortName = Convert.ToString(row["F_SHORTNAME"]);
    this.Units = Convert.ToString(row["F_UNITS"]);
    this.Sort = Convert.ToInt32(row["F_SORT"]);
    this.Flags = Convert.ToInt32(row["F_FLAGS"]);
    this.DataMode = (ImDataMode) Convert.ToInt32(row["F_TYPE"]);
    this.Required = Convert.ToInt32(row["F_REQUIRED"]);
    this.Width = (long) Convert.ToInt32(row["F_WIDTH"]);
    this.EnterMode = (ImEnterMode) Convert.ToInt32(row["F_ENTERMODE"]);
    this.Data = Convert.ToString(row["F_DATA"]);
    if (!this.Units.Equals(string.Empty))
    {
      this.FieldType = FieldTypes.ftMeasured;
    }
    else
    {
      switch (Convert.ToInt32(row["F_DATATYPE"]))
      {
        case 0:
          this.FieldType = FieldTypes.ftUnknown;
          break;
        case 1:
          this.FieldType = FieldTypes.ftString;
          break;
        case 2:
          this.FieldType = FieldTypes.ftInteger;
          break;
        case 3:
          this.FieldType = FieldTypes.ftDouble;
          break;
        case 4:
          this.FieldType = FieldTypes.ftBoolean;
          break;
        case 5:
          this.FieldType = FieldTypes.ftObjectLink;
          break;
        default:
          this.FieldType = FieldTypes.ftString;
          break;
      }
    }
    this.GUID = Guid.NewGuid();
  }
}

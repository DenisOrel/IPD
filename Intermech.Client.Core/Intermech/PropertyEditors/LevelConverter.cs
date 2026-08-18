
// Type: Intermech.PropertyEditors.LevelConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>Конвертер уровня продвижения</summary>
public class LevelConverter : DropDownTypeConverter
{
  /// <summary>Доступность пункта "Любой уровень"</summary>
  protected bool levelAnyEnabled;
  /// <summary>Доступность пустого значения в списке</summary>
  protected bool levelEmptyEnabled;

  public LevelConverter()
    : this(true, false)
  {
  }

  public LevelConverter(bool aLevelAnyEnabled, bool aLevelEmptyEnabled)
    : this((EventsHolder.GetListDelegate) null)
  {
    this.levelAnyEnabled = aLevelAnyEnabled;
    this.levelEmptyEnabled = aLevelEmptyEnabled;
  }

  public LevelConverter(EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
    this.sortValues = true;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value.GetType() == typeof (string) ? (object) new LevelPropertyClass(DataHolders.LevelsHolder.GetIDbyName((string) value)) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    DataTable dataTable = DataHolders.LevelsHolder.DataTable;
    ArrayList valuesCustomList = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      valuesCustomList.Add((object) new LevelPropertyClass(Convert.ToInt32(row["F_LEVEL_ID"])));
    if (this.levelAnyEnabled)
      valuesCustomList.Add((object) new LevelPropertyClass(0));
    if (this.levelEmptyEnabled)
      valuesCustomList.Add((object) new LevelPropertyClass(-1));
    return valuesCustomList;
  }
}

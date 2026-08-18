// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.TableAttribute
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class TableAttribute
{
  [Browsable(false)]
  public FieldTypes FieldType;

  [DisplayName("Глобальный идентификатор")]
  public string Guid { get; private set; }

  [DisplayName("Наименование")]
  public string Name { get; private set; }

  [DisplayName("Тип данных")]
  public string FieldTypeDescription => EnumDescConverter.GetEnumDescription((Enum) this.FieldType);

  [DisplayName("Размер")]
  public string Size { get; private set; }

  [Browsable(false)]
  public int Id { get; private set; }

  [DisplayName("Список")]
  public string MultiValueModeDescription
  {
    get => EnumDescConverter.GetEnumDescription((Enum) this.MultiValueMode);
  }

  [Browsable(false)]
  public MultiValueModes MultiValueMode { get; private set; }

  [DisplayName("Допустимые значения")]
  [TypeConverter(typeof (PossibleValuesConverter))]
  public string[] PossibleValues { get; private set; }

  public TableAttribute(
    int id,
    System.Guid guid,
    string name,
    FieldTypes fieldType,
    long size,
    MultiValueModes multiValueMode,
    PossibleValuesCollection possibleValues)
  {
    this.Id = id;
    this.Guid = guid.ToString();
    this.Name = name;
    this.FieldType = fieldType;
    this.Size = fieldType == FieldTypes.ftString ? size.ToString() : string.Empty;
    this.MultiValueMode = multiValueMode;
    if (possibleValues == null || possibleValues.Count <= 0)
      return;
    this.PossibleValues = new string[possibleValues.Count];
    for (int index = 0; index < possibleValues.Count; ++index)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(possibleValues[index].Value);
      if (!string.IsNullOrEmpty(possibleValues[index].Description))
        stringBuilder.AppendFormat(" ({0})", (object) possibleValues[index].Description);
      this.PossibleValues[index] = stringBuilder.ToString();
    }
  }
}

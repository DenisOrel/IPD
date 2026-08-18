// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AddAttrValue
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

[Serializable]
public class AddAttrValue : TypedValueContainer
{
  [NonSerialized]
  private TypeConverter _converter;
  private string _converterType;

  internal AddAttrValue()
  {
  }

  internal AddAttrValue(object value, Type type)
    : base(value, type)
  {
  }

  internal AddAttrValue(object value, Type type, TypeConverter converter)
  {
    this.Converter = converter;
    this.Type = type;
    this.AssignValue(value, type);
  }

  internal AddAttrValue(object value, Type type, bool showInPropertyGrid)
  {
    this.Type = type;
    this.IsShownInPropertyGrid = showInPropertyGrid;
    this.AssignValue(value, type);
  }

  internal AddAttrValue(object value, Type type, TypeConverter converter, bool showInPropertyGrid)
    : this(value, type, converter)
  {
    this.IsShownInPropertyGrid = showInPropertyGrid;
  }

  internal bool IsShownInPropertyGrid { get; set; } = true;

  internal string ConverterType
  {
    get => this._converterType;
    set
    {
      if (!string.IsNullOrWhiteSpace(value))
      {
        Type type = Type.GetType(value, true, true);
        this._converter = Activator.CreateInstance(type) is TypeConverter instance ? instance : throw new ArgumentInvalidCastException(type, $"Тип '{type}' не является типом конвертера значений.");
        this._converterType = type.ToString();
      }
      else
      {
        this._converter = (TypeConverter) null;
        this._converterType = (string) null;
      }
    }
  }

  internal TypeConverter Converter
  {
    get => this._converter;
    private set
    {
      this._converter = value;
      this._converterType = this._converter?.GetType().ToString();
    }
  }

  /// <summary>Создать другой экземпляр - копию текущего</summary>
  /// <returns></returns>
  internal AddAttrValue Clone()
  {
    return new AddAttrValue(this.value, this.Type, this._converter, this.IsShownInPropertyGrid);
  }

  /// <summary>Поместить значение в контейнер</summary>
  /// <param name="newvalue">Само значение</param>
  /// <param name="type">Тип значения</param>
  protected override void AssignValue(object newvalue, Type type)
  {
    switch (newvalue)
    {
      case TypedValueContainer typedValueContainer:
        this.AssignValue(typedValueContainer.Value, typedValueContainer.Type);
        return;
      case string inValue:
        if (type != typeof (string) && type != typeof (object))
        {
          if (this._converter == null)
          {
            this.value = type != (Type) null ? TypedValueContainer.ParseStringValue(type, inValue) : newvalue;
            return;
          }
          this.value = this._converter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) inValue);
          return;
        }
        break;
    }
    this.value = newvalue;
  }

  public override string ToString()
  {
    if (this.value == null)
      return "";
    if (this.Type == typeof (string) || this.Type == typeof (object))
      return this.value.ToString();
    if (this._converter != null)
      return this._converter.ConvertToString(this.value);
    if (!(this.Type != (Type) null))
      return this.value.ToString();
    return TypeDescriptor.GetConverter(this.Type)?.ConvertToString(this.value);
  }
}

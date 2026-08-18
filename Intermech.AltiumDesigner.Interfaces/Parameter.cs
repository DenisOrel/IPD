// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.Parameter
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Параметр</summary>
[Serializable]
public class Parameter
{
  /// <summary>Создать объект</summary>
  /// <param name="name">Наименование параметра</param>
  /// <param name="value">Значение</param>
  /// <param name="isReadOnly">Значение параметра только чтение</param>
  /// <param name="parameterType">Тип данных</param>
  public Parameter(string name, object value, bool isReadOnly, Type parameterType)
    : this(name, value, isReadOnly, parameterType, ModifiedTypes.None)
  {
  }

  /// <summary>Создать объект</summary>
  /// <param name="name">Наименование параметра</param>
  /// <param name="value">Значение</param>
  /// <param name="isReadOnly">Значение параметра только чтение</param>
  /// <param name="parameterType">Тип данных</param>
  /// <param name="modified">Тип изменения значение</param>
  public Parameter(
    string name,
    object value,
    bool isReadOnly,
    Type parameterType,
    ModifiedTypes modified)
  {
    this.Name = name;
    this.Value = value;
    this.IsReadOnly = isReadOnly;
    this.ParameterType = parameterType;
    this.Modified = modified;
  }

  /// <summary>Наименование</summary>
  public string Name { get; private set; }

  /// <summary>Значение</summary>
  public object Value { get; set; }

  /// <summary>Значение параметра только чтение</summary>
  public bool IsReadOnly { get; private set; }

  /// <summary>Тип данных</summary>
  public Type ParameterType { get; private set; }

  /// <summary>Тип изменения значения</summary>
  public ModifiedTypes Modified { get; set; }
}

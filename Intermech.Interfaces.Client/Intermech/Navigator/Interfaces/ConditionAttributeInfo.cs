// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ConditionAttributeInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Описание атрибута</summary>
public sealed class ConditionAttributeInfo
{
  /// <summary>Идентификатор атрибута (ID или GUID)</summary>
  public object Id;
  /// <summary>Наименование</summary>
  public string Name;
  /// <summary>Тип данных</summary>
  public FieldTypes FieldType;

  /// <summary>Конструктор</summary>
  /// <param name="id">Идентификатор атрибута (ID или GUID)</param>
  /// <param name="name">Наименование</param>
  public ConditionAttributeInfo(object id, string name)
    : this(id, name, FieldTypes.ftUnknown)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="id">Идентификатор атрибута (ID или GUID)</param>
  /// <param name="name">Наименование</param>
  /// <param name="fieldType">Тип данных</param>
  public ConditionAttributeInfo(object id, string name, FieldTypes fieldType)
  {
    this.Id = id;
    this.Name = name;
    this.FieldType = fieldType;
  }

  public override bool Equals(object obj)
  {
    return obj is ConditionAttributeInfo ? ((ConditionAttributeInfo) obj).Id.Equals(this.Id) : base.Equals(obj);
  }

  public override int GetHashCode() => this.Id.GetHashCode();

  public override string ToString() => this.Name;
}

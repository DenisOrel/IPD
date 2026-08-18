// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ResultExpertValue
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Expert;

/// <summary>Класс для возвращения значений</summary>
[Serializable]
public class ResultExpertValue
{
  private Guid _attributeGuid = Guid.Empty;
  private Guid _objectGuid = Guid.Empty;
  private ExpertValue _value;

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeGuid">Guid типа объекта</param>
  /// <param name="attributeTypeGuid">Guid типа атрибута</param>
  /// <param name="value">Значение</param>
  public ResultExpertValue(Guid objectTypeGuid, Guid attributeTypeGuid, ExpertValue value)
  {
    this._attributeGuid = attributeTypeGuid;
    this._objectGuid = objectTypeGuid;
    this._value = value;
  }

  /// <summary>Guid типа объекта</summary>
  public Guid ObjectTypeGuid
  {
    [DebuggerStepThrough] get => this._objectGuid;
  }

  /// <summary>Guid типа атрибута</summary>
  public Guid AttributeTypeGuid
  {
    [DebuggerStepThrough] get => this._attributeGuid;
  }

  /// <summary>Значение</summary>
  public ExpertValue Value
  {
    [DebuggerStepThrough] get => this._value;
  }

  /// <summary>Строковое представление объекта</summary>
  /// <returns></returns>
  public override string ToString() => this._value != null ? this._value.ToString() : "null";
}

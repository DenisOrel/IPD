// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectAttributeEntry
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class DBObjectAttributeEntry
{
  private readonly bool isCopyingDisallowed;
  private readonly bool isUniqueValuesRequired;
  private readonly string name;
  private readonly int attributeId;
  private readonly object[] originalValues;
  private object[] newValues;
  private bool isEditableAttribute;

  public DBObjectAttributeEntry(
    int id,
    string name,
    bool isUniqueValuesRequired,
    bool isCopyingDisallowed,
    bool isEditableAttribute,
    object[] originalValues)
  {
    this.attributeId = id;
    this.name = name;
    this.isUniqueValuesRequired = isUniqueValuesRequired;
    this.isCopyingDisallowed = isCopyingDisallowed;
    this.isEditableAttribute = isEditableAttribute;
    this.originalValues = originalValues;
    this.newValues = new object[originalValues.Length];
    this.originalValues.CopyTo((Array) this.newValues, 0);
  }

  public int AttributeId
  {
    [DebuggerStepThrough] get => this.attributeId;
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
  }

  public bool IsUniqueValuesRequired
  {
    [DebuggerStepThrough] get => this.isUniqueValuesRequired;
  }

  public bool IsCopyingDisallowed
  {
    [DebuggerStepThrough] get => this.isCopyingDisallowed;
  }

  public bool IsEditableAttribute
  {
    [DebuggerStepThrough] get => this.isEditableAttribute;
  }

  public IReadOnlyList<object> OriginalValues
  {
    [DebuggerStepThrough] get => (IReadOnlyList<object>) this.originalValues;
  }

  public IReadOnlyList<object> NewValues
  {
    [DebuggerStepThrough] get => (IReadOnlyList<object>) this.newValues;
  }

  public void SetNewValue(int valueIndex, object value)
  {
    if (object.Equals(this.newValues[valueIndex], value))
      return;
    this.newValues[valueIndex] = value;
    EventHandler newValuesChanged = this.NewValuesChanged;
    if (newValuesChanged == null)
      return;
    newValuesChanged((object) this, EventArgs.Empty);
  }

  public event EventHandler NewValuesChanged;
}

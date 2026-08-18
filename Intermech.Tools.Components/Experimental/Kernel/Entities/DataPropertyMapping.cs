// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DataPropertyMapping
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Runtime;
using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Этот тип реализует отображение свойства доменного объекта или объекта-связки к базе данных IPS.
/// Объекты этого типа содержат все необходимые сведения для передачи значений свойства в соответствующий атрибут объекта IPS и
/// обратно.
/// </summary>
/// <remarks>
/// Объекты этого типа поддерживают заморозку, после чего они становятся immutable и thread safe.
/// </remarks>
internal sealed class DataPropertyMapping : FreezableObject
{
  private int id;
  private string name;
  private FieldTypes dbFieldType;
  private bool isContent;
  private bool isCheckoutRequired;
  private bool isManuallyCreated;
  private bool isDeletable;
  private bool allowDBNull;
  private DataPropertyLoadParameters valueLoadParameters;
  private DataPropertySaveParameters valueSaveParameters;

  public DataPropertyMapping(
    DataPropertyDescriptor propertyDescriptor,
    DataPropertyLanguageInfo languageInfo,
    Guid guid)
  {
    this.PropertyDescriptor = propertyDescriptor;
    this.LanguageInfo = languageInfo;
    this.Guid = guid;
    this.Id = 0;
    this.DBFieldType = FieldTypes.ftUnknown;
  }

  public DataPropertyDescriptor PropertyDescriptor { get; private set; }

  public DataPropertyLanguageInfo LanguageInfo { get; private set; }

  public Guid Guid { get; private set; }

  public int Id
  {
    [DebuggerStepThrough] get => this.id;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Id));
      this.id = value;
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Name));
      this.name = value;
    }
  }

  public FieldTypes DBFieldType
  {
    [DebuggerStepThrough] get => this.dbFieldType;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DBFieldType));
      this.dbFieldType = value;
    }
  }

  /// <summary>Возвращает признак обязательного атрибута IPS.</summary>
  public bool IsObligatory
  {
    [DebuggerStepThrough] get => this.id < 0;
  }

  public bool IsFileOrBlob
  {
    [DebuggerStepThrough] get => this.dbFieldType == FieldTypes.ftFile;
  }

  public bool IsContent
  {
    [DebuggerStepThrough] get => this.isContent;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsContent));
      this.isContent = value;
    }
  }

  public bool IsCheckoutRequired
  {
    [DebuggerStepThrough] get => this.isCheckoutRequired;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsCheckoutRequired));
      this.isCheckoutRequired = value;
    }
  }

  public bool IsManuallyCreated
  {
    [DebuggerStepThrough] get => this.isManuallyCreated;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsManuallyCreated));
      this.isManuallyCreated = value;
    }
  }

  public bool IsDeletable
  {
    [DebuggerStepThrough] get => this.isDeletable;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (IsDeletable));
      this.isDeletable = value;
    }
  }

  public bool AllowDBNull
  {
    [DebuggerStepThrough] get => this.allowDBNull;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (AllowDBNull));
      this.allowDBNull = value;
    }
  }

  /// <summary>
  /// Возвращает признак, при чтении из базы данных может встречаться значение DBNull.
  /// Значение свойства будет равно true, если хотя бы одно из свойств AllowDBNull, IsDeletable равно true.
  /// </summary>
  public bool CanBeDBNull
  {
    [DebuggerStepThrough] get => this.AllowDBNull || this.IsDeletable;
  }

  public DataPropertyLoadParameters ValueLoadParameters
  {
    [DebuggerStepThrough] get => this.valueLoadParameters;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (ValueLoadParameters));
      this.valueLoadParameters = value;
    }
  }

  public DataPropertySaveParameters ValueSaveParameters
  {
    [DebuggerStepThrough] get => this.valueSaveParameters;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (ValueSaveParameters));
      this.valueSaveParameters = value;
    }
  }

  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.Id == 0)
      throw PropertyExceptions.PropertyBadValueException((object) this, "Id", (object) this.Id);
    if (string.IsNullOrEmpty(this.Name))
      throw PropertyExceptions.PropertyBadValueException((object) this, "Name", this.Name);
    if (this.DBFieldType == FieldTypes.ftUnknown)
      throw PropertyExceptions.PropertyBadValueException((object) this, "DBFieldType", (object) this.DBFieldType);
    if (this.ValueLoadParameters == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ValueLoadParameters");
    if (this.ValueSaveParameters == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ValueSaveParameters");
  }
}

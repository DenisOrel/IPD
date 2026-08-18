// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DataPropertyLoadParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Runtime;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Описывает правила чтения и трансляции значений атрибута IPS в значения свойства доменного объекта.
/// </summary>
/// <remarks>
/// Объекты этого типа поддерживают заморозку, после чего они становятся immutable и thread safe.
/// </remarks>
internal sealed class DataPropertyLoadParameters : FreezableObject
{
  private DBNullLoadMode dbNullLoadMode;
  private object dbNullEquivalent;
  private Type meaningfulValueType;
  private GetAttributeValuesModes keyLoadMode;
  private ColumnContents batchLoadMode;

  /// <summary>Создает объект.</summary>
  public DataPropertyLoadParameters()
  {
    this.dbNullLoadMode = DBNullLoadMode.NotApplicable;
    this.keyLoadMode = GetAttributeValuesModes.None;
    this.batchLoadMode = ColumnContents.Text;
  }

  /// <summary>
  /// Возвращает или задает режим трансляции DBNull-значений, прочитанных из базы данных,
  /// в значения свойства доменного объекта.
  /// </summary>
  public DBNullLoadMode DBNullLoadMode
  {
    get => this.dbNullLoadMode;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DBNullLoadMode));
      this.dbNullLoadMode = value;
    }
  }

  /// <summary>
  /// Возвращает или задает эквивалент для DBNull-значений, прочитанных из базы данных.
  /// Это значение будет использоваться для инициализации свойства доменного объекта
  /// при чтении DBNull-значения из базы данных.
  /// </summary>
  public object DBNullEquivalent
  {
    get => this.dbNullEquivalent;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DBNullEquivalent));
      this.dbNullEquivalent = value;
    }
  }

  /// <summary>
  /// Возвращает или задает тип для не DBNull-значений, прочитанных из базы данных.
  /// Этот тип будет использоваться для конвертации и инициализации свойства доменного объекта
  /// при чтении не DBNull-значения из базы данных.
  /// </summary>
  public Type MeaningfulValueType
  {
    get => this.meaningfulValueType;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (MeaningfulValueType));
      this.meaningfulValueType = value;
    }
  }

  /// <summary>
  /// Возвращает или задает режим чтения значения атрибута при чтении всех атрибутов одного доменного объекта,
  /// указанного с помощью уникального ключа.
  /// </summary>
  public GetAttributeValuesModes KeyEntityLoadMode
  {
    get => this.keyLoadMode;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (KeyEntityLoadMode));
      this.keyLoadMode = value;
    }
  }

  /// <summary>
  /// Возвращает или задает режим чтения значения атрибута при пакетном чтении атрибутов нескольких доменных объектов.
  /// </summary>
  public ColumnContents BatchLoadMode
  {
    get => this.batchLoadMode;
    set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (BatchLoadMode));
      this.batchLoadMode = value;
    }
  }

  /// <summary>
  /// Позволяет проверить корректность состояния объекта перед заморозкой.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Состояние объекта не корректно и не может быть заморожено</exception>
  protected override void DoValidate()
  {
    base.DoValidate();
    if ((this.DBNullLoadMode == DBNullLoadMode.NotApplicable || this.DBNullLoadMode == DBNullLoadMode.NullValue) && this.DBNullEquivalent != null)
      throw PropertyExceptions.PropertyBadValueException((object) this, "DBNullEquivalent", this.DBNullEquivalent);
    if ((this.DBNullLoadMode == DBNullLoadMode.EmptyValue || this.DBNullLoadMode == DBNullLoadMode.DefaultValue) && this.DBNullEquivalent == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DBNullEquivalent");
    if (this.MeaningfulValueType == (Type) null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "MeaningfulValueType");
  }
}

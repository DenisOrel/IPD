// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCreateBlankObjectAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее создать заготовку объекта</summary>
public class MRPCreateBlankObjectAction : MRPBaseAction
{
  /// <summary>Идентификатор типа создаваемого объекта</summary>
  private int objectTypeID;
  /// <summary>
  /// Ссылка на элемент, который может изменить свой целочисленный идентификатор
  /// </summary>
  private IMRPUpdateableItemRef objRef;

  /// <summary>
  /// Создать действие, позволяющее создать заготовку объекта
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="objRef">Ссылка на элемент, который может изменить свой целочисленный идентификатор</param>
  public MRPCreateBlankObjectAction(
    IServiceProvider services,
    int objectTypeID,
    IMRPUpdateableItemRef objRef)
    : base(services)
  {
    if (objectTypeID == -1)
      throw new ArgumentException();
    if (objRef == null)
      throw new ArgumentNullException(nameof (objRef));
    this.objectTypeID = objectTypeID;
    this.objRef = objRef;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCreateBlankObjectAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.objectTypeID = -1;
    this.objRef = (IMRPUpdateableItemRef) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCreateBlankObjectAction blankObjectAction))
      return;
    this.objectTypeID = blankObjectAction.objectTypeID;
    this.objRef = blankObjectAction.objRef;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
      this.objRef.UpdateItemID((MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetObjectCollection(this.objectTypeID).Create().ObjectID);
  }
}

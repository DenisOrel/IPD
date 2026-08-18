// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCheckInAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее завершить изменение объекта</summary>
public class MRPCheckInAction : MRPBaseAction
{
  /// <summary>Ссылка на объект</summary>
  private IMRPObjectRef objRef;
  /// <summary>
  /// Генерировать исключение при попытке завершить изменение объекта
  /// </summary>
  private bool strictMode;

  /// <summary>
  /// Создать действие, позволяющее завершить изменение объекта
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objRef">Ссылка на объект</param>
  /// <param name="strictMode">Генерировать исключение при попытке завершить изменение объекта</param>
  public MRPCheckInAction(IServiceProvider services, IMRPObjectRef objRef, bool strictMode)
    : base(services)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException(nameof (objRef));
    this.strictMode = strictMode;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCheckInAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.objRef = (IMRPObjectRef) null;
    this.strictMode = true;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCheckInAction mrpCheckInAction))
      return;
    this.objRef = mrpCheckInAction.objRef;
    this.strictMode = mrpCheckInAction.strictMode;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.objRef.ObjectID >= 0L)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IDBObject dbObject = (MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetObject(this.objRef.ObjectID, this.strictMode);
      if (dbObject == null)
        return;
      if (dbObject.CheckoutBy != 0L)
        dbObject.CheckIn();
      this.objRef.UpdateItemID(dbObject.ObjectID);
    }
  }
}

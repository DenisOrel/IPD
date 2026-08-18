// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCheckoutAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее взять объект на изменение</summary>
public class MRPCheckoutAction : MRPBaseAction
{
  /// <summary>Ссылка на объект</summary>
  private IMRPObjectRef objRef;
  /// <summary>
  /// Генерировать исключение при попытке взять на изменение объект, чей шаг этого не допускает
  /// </summary>
  private bool strictMode;

  /// <summary>
  /// Создать действие, позволяющее взять объект на изменение
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objRef">Ссылка на объект</param>
  /// <param name="strictMode">Генерировать исключение при попытке взять на изменение объект, чей шаг этого не допускает</param>
  public MRPCheckoutAction(IServiceProvider services, IMRPObjectRef objRef, bool strictMode)
    : base(services)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException(nameof (objRef));
    this.strictMode = strictMode;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCheckoutAction(object source)
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
    if (!(source is MRPCheckoutAction mrpCheckoutAction))
      return;
    this.objRef = mrpCheckoutAction.objRef;
    this.strictMode = mrpCheckoutAction.strictMode;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.objRef.ObjectID < 0L)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      IMRPCheckInObjectsRef service = this.Services.GetService(typeof (IMRPCheckInObjectsRef)) as IMRPCheckInObjectsRef;
      ObjectCheckedOutVersionsHolder outVersionsHolder = (contextSession.GetCustomService(typeof (IObjectsCheckOutServerService)) as IObjectsCheckOutServerService).CheckOut((object) contextSession.SessionGUID, (IList<long>) new long[1]
      {
        this.objRef.ObjectID
      }, true);
      if (outVersionsHolder == null || outVersionsHolder.Objects == null || outVersionsHolder.Objects.Count == 0)
        return;
      this.objRef.UpdateItemID(outVersionsHolder.Objects[0].F_OBJECT_ID);
      if (this.objRef.ObjectID >= 0L || service == null)
        return;
      service.Add(this.objRef);
    }
  }
}

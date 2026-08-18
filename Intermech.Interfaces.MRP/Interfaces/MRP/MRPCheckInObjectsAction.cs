// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCheckInObjectsAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее завершить изменение коллекции версий объектов
/// </summary>
public class MRPCheckInObjectsAction : MRPBaseAction
{
  /// <summary>Ссылка на контейнер с версиями объектов</summary>
  private IMRPCheckInObjectsRef objRefs;
  /// <summary>
  /// Генерировать исключение при попытке завершить изменение объекта
  /// </summary>
  private bool strictMode;

  /// <summary>
  /// Создать действие, позволяющее завершить изменение объекта
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objRefs">Ссылка на контейнер с версиями объектов</param>
  /// <param name="strictMode">Генерировать исключение при попытке завершить изменение объекта</param>
  public MRPCheckInObjectsAction(
    IServiceProvider services,
    IMRPCheckInObjectsRef objRefs,
    bool strictMode)
    : base(services)
  {
    this.objRefs = objRefs != null ? objRefs : throw new ArgumentNullException(nameof (objRefs));
    this.strictMode = strictMode;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCheckInObjectsAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.objRefs = (IMRPCheckInObjectsRef) null;
    this.strictMode = true;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCheckInObjectsAction checkInObjectsAction))
      return;
    this.objRefs = checkInObjectsAction.objRefs;
    this.strictMode = checkInObjectsAction.strictMode;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.objRefs == null)
      return;
    List<IMRPObjectRef> items = this.objRefs.Items;
    if (items == null || items.Count == 0)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      if (this.Services.GetService(typeof (IMRPProgress)) is IMRPProgress service)
      {
        service.MinProgress = 0;
        service.Progress = 0;
        service.MaxProgress = items.Count;
      }
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      for (int index = 0; index < items.Count; ++index)
      {
        if (service != null)
          service.Progress = index;
        if (items[index].ObjectID < 0L)
        {
          IDBObject dbObject = contextSession.GetObject(items[index].ObjectID, this.strictMode);
          if (dbObject != null)
          {
            if (dbObject.CheckoutBy != 0L)
              dbObject.CheckIn();
            items[index].UpdateItemID(dbObject.ObjectID);
          }
        }
      }
      if (service == null)
        return;
      service.Progress = service.MaxProgress;
    }
  }
}

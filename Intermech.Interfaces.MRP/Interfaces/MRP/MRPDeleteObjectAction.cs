// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPDeleteObjectAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее удалить объект</summary>
public class MRPDeleteObjectAction : MRPBaseAction
{
  /// <summary>Ссылка на объект</summary>
  private IMRPObjectRef objRef;
  /// <summary>Генерировать исключение, если объект не найден</summary>
  private bool throwIfNoObjectFound;

  /// <summary>Создать действие, позволяющее удалить объект</summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objRef">Ссылка на объект</param>
  /// <param name="throwIfNoObjectFound">Генерировать исключение, если объект не найден</param>
  public MRPDeleteObjectAction(
    IServiceProvider services,
    IMRPObjectRef objRef,
    bool throwIfNoObjectFound)
    : base(services)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException(nameof (objRef));
    this.throwIfNoObjectFound = throwIfNoObjectFound;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPDeleteObjectAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.objRef = (IMRPObjectRef) null;
    this.throwIfNoObjectFound = true;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPDeleteObjectAction deleteObjectAction))
      return;
    this.objRef = deleteObjectAction.objRef;
    this.throwIfNoObjectFound = deleteObjectAction.throwIfNoObjectFound;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      MRPNavigatorEventsRef service = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
      IDBObject dbObject = contextSession.GetObject(this.objRef.ObjectID, this.throwIfNoObjectFound);
      if (dbObject == null)
        return;
      service?.AddDeletedObject(dbObject.ObjectID, dbObject.ObjectType);
      dbObject.Delete(0L);
    }
  }
}

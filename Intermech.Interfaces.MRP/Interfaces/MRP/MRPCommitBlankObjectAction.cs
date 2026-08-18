// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCommitBlankObjectAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее преобразовать заготовку в полноценный объект
/// </summary>
public class MRPCommitBlankObjectAction : MRPBaseAction
{
  /// <summary>Ссылка на объект</summary>
  private IMRPObjectRef objRef;

  /// <summary>
  /// Создать действие, позволяющее преобразовать заготовку в полноценный объект
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objRef">Ссылка на объект</param>
  public MRPCommitBlankObjectAction(IServiceProvider services, IMRPObjectRef objRef)
    : base(services)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException(nameof (objRef));
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCommitBlankObjectAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.objRef = (IMRPObjectRef) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCommitBlankObjectAction blankObjectAction))
      return;
    this.objRef = blankObjectAction.objRef;
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
      IDBObject dbObject = contextSession.GetObject(this.objRef.ObjectID, true);
      if (!dbObject.IsCreationMode)
        return;
      dbObject.CommitCreation(true);
      this.objRef.UpdateItemID(dbObject.ObjectID);
      service?.AddCreatedObject(dbObject.ObjectID, dbObject.ObjectType);
    }
  }
}

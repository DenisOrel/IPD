// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPWriteObjectAttributesAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее записать атрибуты в объект</summary>
public class MRPWriteObjectAttributesAction : MRPBaseAction
{
  /// <summary>
  /// Ссылка на элемент, который может изменить свой целочисленный идентификатор
  /// </summary>
  private IMRPObjectRef objRef;
  /// <summary>Список записываемых значений атрибутов</summary>
  private AttributeValues[] attrValues;

  /// <summary>
  /// Создать действие, позволяющее записать атрибуты в объект
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="objRef">Ссылка на элемент, который может изменить свой целочисленный идентификатор</param>
  /// <param name="attrValues">Записываемые атрибуты</param>
  public MRPWriteObjectAttributesAction(
    IServiceProvider services,
    IMRPObjectRef objRef,
    params AttributeValues[] attrValues)
    : base(services)
  {
    if (objRef == null)
      throw new ArgumentNullException(nameof (objRef));
    if (attrValues == null)
      throw new ArgumentNullException(nameof (attrValues));
    this.objRef = objRef;
    this.attrValues = attrValues;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPWriteObjectAttributesAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.objRef = (IMRPObjectRef) null;
    this.attrValues = (AttributeValues[]) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPWriteObjectAttributesAction attributesAction))
      return;
    this.objRef = attributesAction.objRef;
    this.attrValues = attributesAction.attrValues;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.attrValues.Length == 0)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      MRPNavigatorEventsRef service = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
      IDBObject dbObject = contextSession.GetObject(this.objRef.ObjectID, true);
      dbObject.SetAttributesValues(this.attrValues);
      service?.AddChangedObject(dbObject.ObjectID, dbObject.ObjectType);
    }
  }
}

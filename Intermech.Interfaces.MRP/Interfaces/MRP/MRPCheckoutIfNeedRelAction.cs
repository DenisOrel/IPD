// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCheckoutIfNeedRelAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее взять объект на изменение, если это требуется для создания связи
/// </summary>
public class MRPCheckoutIfNeedRelAction : MRPBaseAction
{
  /// <summary>
  /// Ссылка на родительский объект, который, возможно, надо брать на изменение
  /// </summary>
  private IMRPTypedObjectRef projObj;
  /// <summary>
  /// Ссылка на дочерний объект, связь с которым требуется создать
  /// </summary>
  private IMRPTypedObjectRef partObj;
  /// <summary>Тип создаваемой связи</summary>
  private int relTypeID;
  /// <summary>
  /// Генерировать исключение при попытке взять на изменение объект, чей шаг этого не допускает
  /// </summary>
  private bool strictMode;

  /// <summary>
  /// Создать действие, позволяющее взять объект на изменение
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="projObj">Ссылка на родительский объект, который, возможно, надо брать на изменение</param>
  /// <param name="partObj">Ссылка на дочерний объект, связь с которым требуется создать</param>
  /// <param name="relTypeID">Тип создаваемой связи</param>
  /// <param name="strictMode">Генерировать исключение при попытке взять на изменение объект, чей шаг этого не допускает</param>
  public MRPCheckoutIfNeedRelAction(
    IServiceProvider services,
    IMRPTypedObjectRef projObj,
    IMRPTypedObjectRef partObj,
    int relTypeID,
    bool strictMode)
    : base(services)
  {
    if (projObj == null)
      throw new ArgumentNullException(nameof (projObj));
    if (partObj == null)
      throw new ArgumentNullException(nameof (partObj));
    if (relTypeID == -1)
      throw new ArgumentNullException(nameof (relTypeID));
    this.projObj = projObj;
    this.partObj = partObj;
    this.relTypeID = relTypeID;
    this.strictMode = strictMode;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPCheckoutIfNeedRelAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.projObj = (IMRPTypedObjectRef) null;
    this.partObj = (IMRPTypedObjectRef) null;
    this.relTypeID = -1;
    this.strictMode = true;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCheckoutIfNeedRelAction checkoutIfNeedRelAction))
      return;
    this.projObj = checkoutIfNeedRelAction.projObj;
    this.partObj = checkoutIfNeedRelAction.partObj;
    this.relTypeID = checkoutIfNeedRelAction.relTypeID;
    this.strictMode = checkoutIfNeedRelAction.strictMode;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.projObj.ObjectID < 0L)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IDBRelationsApplicability applicability = (MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetRelationsApplicabilityCollection().GetApplicability(this.relTypeID, this.partObj.TypeID, this.projObj.TypeID);
      if (applicability == null || !applicability.IsContent)
        return;
      new MRPCheckoutAction((IServiceProvider) this.services, (IMRPObjectRef) this.projObj, this.strictMode).Execute();
    }
  }
}

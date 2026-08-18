// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPFixRelationPartAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее конкретизировать версию дочернего объекта на указанной связи
/// </summary>
public class MRPFixRelationPartAction : MRPBaseAction
{
  /// <summary>Описание связи</summary>
  private IMRPRelationRef relation;
  /// <summary>Конкретизируемая версия дочернего объекта</summary>
  private IMRPObjectRef concretePart;

  /// <summary>
  /// Создать действие, позволяющее конкретизировать версию дочернего объекта на указанной связи
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="relation">Версия, в которой требуется конкретизация</param>
  /// <param name="concretePart">Конкретизируемая версия дочернего объекта</param>
  public MRPFixRelationPartAction(
    IServiceProvider services,
    IMRPRelationRef relation,
    IMRPObjectRef concretePart)
    : base(services)
  {
    if (relation == null)
      throw new ArgumentNullException(nameof (relation));
    if (concretePart == null)
      throw new ArgumentNullException(nameof (concretePart));
    this.relation = relation;
    this.concretePart = concretePart;
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.relation = (IMRPRelationRef) null;
    this.concretePart = (IMRPObjectRef) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPFixRelationPartAction relationPartAction))
      return;
    this.relation = relationPartAction.relation;
    this.concretePart = relationPartAction.concretePart;
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
      AttributeValues[] valuesList = new AttributeValues[1]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545"), (object) Math.Abs(this.concretePart.ObjectID))
      };
      valuesList[0].IsNew = true;
      valuesList[0].ThrowSetException = true;
      contextSession.GetRelation(this.relation.Guid, this.relation.ProjectID, false)?.SetAttributesValues(valuesList);
    }
  }
}

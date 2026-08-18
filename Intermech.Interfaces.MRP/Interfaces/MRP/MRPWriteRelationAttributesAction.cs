// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPWriteRelationAttributesAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее записать атрибуты в связь</summary>
public class MRPWriteRelationAttributesAction : MRPBaseAction
{
  /// <summary>Ссылка на связь</summary>
  private IMRPRelationRef relRef;
  /// <summary>Список записываемых значений атрибутов</summary>
  private AttributeValues[] attrValues;

  /// <summary>
  /// Создать действие, позволяющее записать атрибуты в связь
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="relRef">Ссылка на связь</param>
  /// <param name="attrValues">Записываемые атрибуты</param>
  public MRPWriteRelationAttributesAction(
    IServiceProvider services,
    IMRPRelationRef relRef,
    params AttributeValues[] attrValues)
    : base(services)
  {
    if (relRef == null)
      throw new ArgumentNullException(nameof (relRef));
    if (attrValues == null)
      throw new ArgumentNullException(nameof (attrValues));
    this.relRef = relRef;
    this.attrValues = attrValues;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPWriteRelationAttributesAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.relRef = (IMRPRelationRef) null;
    this.attrValues = (AttributeValues[]) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPWriteRelationAttributesAction attributesAction))
      return;
    this.relRef = attributesAction.relRef;
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
      (MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetRelation(this.relRef.Guid, this.relRef.ProjectID, true).SetAttributesValues(this.attrValues);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPDelegateAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Базовое действие для задач MRP, вызывающее какой-то внешний обработчик
/// </summary>
public class MRPDelegateAction : MRPBaseAction
{
  /// <summary>Ссылка на внешний обработчик</summary>
  protected MRPActionsHandler handler;

  /// <summary>
  /// Создать экземпляр класса с указанным внешним обработчиком
  /// </summary>
  /// <param name="handler">Обработчик</param>
  public MRPDelegateAction(MRPActionsHandler handler)
    : base((IServiceProvider) null)
  {
    this.handler = handler;
  }

  /// <summary>
  /// Создать экземпляр класса с указанным внешним обработчиком
  /// </summary>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  /// <param name="handler">Обработчик</param>
  public MRPDelegateAction(IServiceProvider services, MRPActionsHandler handler)
    : base(services)
  {
    this.handler = handler;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPDelegateAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.handler = (MRPActionsHandler) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPDelegateAction mrpDelegateAction))
      return;
    this.handler = mrpDelegateAction.handler;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute()
  {
    if (this.handler == null)
      return;
    this.handler(this.Services);
  }

  /// <summary>
  /// Выполнить действие в рамках указанного контекста.
  /// Если контекст не задан, будет использоваться встроенный
  /// </summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие. Если контекст не задан, будет использоваться свой</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.handler == null)
      return;
    this.handler(context ?? this.Services);
  }
}

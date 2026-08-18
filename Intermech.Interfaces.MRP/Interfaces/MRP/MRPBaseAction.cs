// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPBaseAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Базовое действие для задач MRP</summary>
public class MRPBaseAction : MRPContext, IAssignable, ICloneable, IMRPContext, IMRPAction
{
  /// <summary>Создать заполненный контекст MRP</summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  public MRPBaseAction(IServiceProvider services)
    : base(services)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPBaseAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear() => this.services = (AdvancedServiceContainer) null;

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MRPBaseAction mrpBaseAction))
      return;
    this.services.AdvancedProvider = mrpBaseAction.services.AdvancedProvider;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Выполнить действие</summary>
  public virtual void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public virtual void Execute(IServiceProvider context)
  {
  }
}

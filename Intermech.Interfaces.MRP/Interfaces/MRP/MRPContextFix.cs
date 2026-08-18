// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPContextFix
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Вспомогательный класс, позволяющий сменить на время контекст внутри другого контекста
/// </summary>
public sealed class MRPContextFix : IDisposable
{
  /// <summary>Контекстный элемент</summary>
  private IMRPContext contextItem;
  /// <summary>
  /// Сохранённый контекст для указанного контекстного элемента
  /// </summary>
  private IServiceProvider oldContext;

  /// <summary>
  /// Создать экземпляр класса, зафиксировать контекст, если это возможно
  /// </summary>
  /// <param name="contextItem">Контекстный элемент, в котором требуется на время сменить контекст</param>
  /// <param name="newContext">Новый временный контекст для указанного контекстного элемента</param>
  public MRPContextFix(IMRPContext contextItem, IServiceProvider newContext)
  {
    this.contextItem = contextItem;
    this.oldContext = !(contextItem is IMRPAdvancedContext mrpAdvancedContext) || mrpAdvancedContext.Services == null ? (this.contextItem != null ? this.contextItem.Services : (IServiceProvider) null) : mrpAdvancedContext.Services.AdvancedProvider;
    if (this.contextItem == null)
      return;
    this.contextItem.Services = newContext;
  }

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this.contextItem == null)
      return;
    this.contextItem.Services = this.oldContext;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PageControlUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Контейнер для объектов интерфейса пользователя элементов страницы</summary>
public class PageControlUI : PageElementUI
{
  private PageControl pageControl;

  /// <summary>Конструктор</summary>
  /// <param name="pageControl">PageControl владелец</param>
  public PageControlUI(PageControl pageControl) => this.pageControl = pageControl;

  /// <summary>Конструктор</summary>
  protected PageControlUI()
  {
  }

  /// <summary>PageControl владелец</summary>
  public override PageControl PageControl
  {
    [DebuggerStepThrough] get => this.pageControl;
  }
}

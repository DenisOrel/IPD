// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Polyline
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Полилиния</summary>
[Serializable]
public class Polyline : PolylineData, IPageElementWithInterface
{
  [NonSerialized]
  private CancelEventHandler inplaceEditorActivating;
  [NonSerialized]
  private EventHandler inplaceEditorActivated;
  [NonSerialized]
  private CancelEventHandler inplaceEditorDeactivating;
  [NonSerialized]
  private EventHandler inplaceEditorDeactivated;
  /// <summary>Имя типа элемента</summary>
  public new static string ElementTypeName = LocalizationHolder.rm.GetString("Document.Model_501");
  [NonSerialized]
  private PageElementUI pageUI;

  public override bool ShowFocused
  {
    get => this.pageUI != null ? this.pageUI.IsActiveElement : base.ShowFocused;
  }

  public override bool ShowSelected
  {
    get => this.pageUI != null ? this.pageUI.IsSelected : base.ShowSelected;
  }

  /// <summary>Событие перед активацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorActivating
  {
    add => this.inplaceEditorActivating += value;
    remove => this.inplaceEditorActivating -= value;
  }

  /// <summary>Событие после активации редактора по месту</summary>
  public event EventHandler InplaceEditorActivated
  {
    add => this.inplaceEditorActivated += value;
    remove => this.inplaceEditorActivated -= value;
  }

  /// <summary>Событие перед деактивацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorDeactivating
  {
    add => this.inplaceEditorDeactivating += value;
    remove => this.inplaceEditorDeactivating -= value;
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event EventHandler InplaceEditorDeactivated
  {
    add => this.inplaceEditorDeactivated += value;
    remove => this.inplaceEditorDeactivated -= value;
  }

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="pageUI">Элемент управления в контексте которого должен быть редактор</param>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  public void ActivateInPlaceEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
  }

  /// <summary>Контрол редактора по месту</summary>
  [Browsable(false)]
  public Control InPlaceEditorControl
  {
    [DebuggerStepThrough] get => (Control) null;
  }

  /// <summary>Контейнер для управления размерами и положением прямоугольного
  /// элемента управления</summary>
  [Browsable(false)]
  public PageElementUI PageUI
  {
    [DebuggerStepThrough] get => this.pageUI;
    set
    {
      if (this.pageUI == value)
        return;
      int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
      if (num == 0)
        this.SuspendUpdateGeometryRefreshUI();
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) null;
        this.pageUI.Parent = (PageElementUI) null;
      }
      this.pageUI = value;
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) this;
        if (this.Parent is VisualNode parent)
          parent.AddChildUI((DocumentTreeNode) this, false);
      }
      this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (num != 0)
        return;
      this.ResumeUpdateRefreshUI(this.pageUI != null, true);
    }
  }

  /// <summary>Наименование типа</summary>
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => Polyline.ElementTypeName;
  }

  public override void Draw(DrawContext context)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag || this.PathPoints.Length == 0 || context != null && context.Layer != 0)
      return;
    base.Draw(context);
    if (context == null || !context.IsSelected.HasValue || !context.IsFocused.HasValue || (!context.IsPaint || !context.IsSelected.Value ? 0 : (!context.IsFocused.Value ? 1 : 0)) == 0 || this.PageUI == null)
      return;
    this.PageUI.OnPaint(new PaintEventArgs(context.Graphics.InternalGraphics, Rectangle.Empty));
  }

  /// <summary>Создать соответсвующий элемент управления. Должен быть перекрыт</summary>
  public override void CreateUI()
  {
    if (!this.IsVirtualNode && this.needUI && this.pageUI == null)
    {
      if (!(this.parent is Intermech.Document.Model.Page parent2))
      {
        if (!(this.parent is IPageElementWithInterface parent1) || parent1.PageUI == null)
          return;
      }
      else if (parent2.PageUI == null)
        return;
      this.PageUI = (PageElementUI) new PolylineUI();
    }
    base.CreateUI();
  }

  /// <summary>Обновить геометрию интерфейса пользователя</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    bool flag = false;
    if (this.pageUI == null && this.NeedUI)
    {
      this.CreateUI();
      flag = true;
    }
    if (this.pageUI == null)
      return;
    int num = this.SuspendedRefreshUIFlag ? 1 : 0;
    if (num == 0)
      this.SuspendRefreshUI();
    this.InvalidateUI(this.pageUI.Bounds);
    if (this.needUpdateUIGeometry && !flag)
      this.pageUI.UpdateGeometry();
    base.UpdateUIGeometry(false);
    if (num != 0)
      return;
    this.ResumeRefreshUI(refreshUI);
  }

  /// <summary>Обновить мировые координаты элемента преобразовав экранные координаты</summary>
  public override void UpdateWorldCoor()
  {
    if (this.PageUI == null)
      return;
    int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
    if (num == 0)
      this.SuspendUpdateGeometryRefreshUI();
    this.PageUI.UpdateElementGeometry();
    if (num != 0)
      return;
    this.ResumeUpdateRefreshUI(true, true);
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(bool force)
  {
    if (!force && this.SuspendedRefreshUIFlag || this.pageUI == null)
      return;
    if (this.page != null)
      this.page.InvalidateUI(this.pageUI.Bounds);
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(Rectangle clipRectangle, bool force)
  {
    if (this.SuspendedRefreshUIFlag)
      return;
    if (this.page != null)
      this.page.InvalidateUI(clipRectangle);
    if (this.pageUI == null)
      return;
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (this.SuspendedRefreshUIFlag || this.page == null)
      return;
    if (this.pageUI != null)
      this.RefreshUI(this.pageUI.Bounds);
    else
      this.page.RefreshUI();
  }

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new Polyline(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new Polyline();

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected Polyline(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  public Polyline(DocumentTreeNode parent)
  {
    if (parent == null)
      return;
    this.SetParent(parent, false, false);
  }

  /// <summary>Конструктор</summary>
  public Polyline()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public Polyline(bool initFields)
    : base(initFields)
  {
  }
}


// Type: Intermech.Navigator.Controls.PageViewsManagerWrapper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Оболочка над PageViewsManager поддерживающая design-time сериализацию</summary>
public class PageViewsManagerWrapper : UserControlDesignTimeSerializationWrapper
{
  /// <summary>Gets or sets the allowed views</summary>
  [Browsable(true)]
  [DefaultValue(null)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_134")]
  private string[] _defaultAllowedViews;
  /// <summary>Список имён запрещённых к отображению закладок</summary>
  [Browsable(true)]
  [DefaultValue(null)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_135")]
  private string[] _defaultSuppressedViews;
  /// <summary>Выравнивание заголовков закладок</summary>
  [Browsable(true)]
  [DefaultValue(Intermech.Docking.TabAlignment.Top)]
  [Category("Appearance")]
  [CustomDescription("Attribute.Client.Core_136")]
  private Intermech.Docking.TabAlignment _defaultHeaderAlignment;
  /// <summary>Выравнивание закладок</summary>
  [Browsable(true)]
  [DefaultValue(LeftRightAlignment.Left)]
  [Category("Appearance")]
  [CustomDescription("Attribute.Client.Core_137")]
  public LeftRightAlignment _defaultTabsAligment;

  /// <summary>Constructor</summary>
  public PageViewsManagerWrapper(PageViewsManager pageViewsManager)
    : base((UserControl) pageViewsManager)
  {
    this._defaultAllowedViews = pageViewsManager.AllowedViews;
    this._defaultSuppressedViews = pageViewsManager.SuppressedViews;
    this._defaultHeaderAlignment = pageViewsManager.HeaderAlignment;
    this._defaultTabsAligment = pageViewsManager.TabsAligment;
  }

  /// <summary>Оригинальный менеджер закладок</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public PageViewsManager PageViewsManager
  {
    [DebuggerStepThrough] get => (PageViewsManager) this._userControl;
  }

  public string[] AllowedViews
  {
    [DebuggerStepThrough] get => this.PageViewsManager.AllowedViews;
    [DebuggerStepThrough] set => this.PageViewsManager.AllowedViews = value;
  }

  public bool ShouldSerializeAllowedViews()
  {
    if (this.PageViewsManager.AllowedViews == null && this._defaultAllowedViews != null)
      return true;
    return this.PageViewsManager.AllowedViews != null && !this.PageViewsManager.AllowedViews.Equals((object) this._defaultAllowedViews);
  }

  public void ResetAllowedViews() => this.PageViewsManager.AllowedViews = this._defaultAllowedViews;

  public string[] SuppressedViews
  {
    [DebuggerStepThrough] get => this.PageViewsManager.SuppressedViews;
    [DebuggerStepThrough] set => this.PageViewsManager.SuppressedViews = value;
  }

  public bool ShouldSerializeSuppressedViews()
  {
    if (this.PageViewsManager.SuppressedViews == null && this._defaultSuppressedViews != null)
      return true;
    return this.PageViewsManager.SuppressedViews != null && !this.PageViewsManager.SuppressedViews.Equals((object) this._defaultSuppressedViews);
  }

  public void ResetSuppressedViews()
  {
    this.PageViewsManager.SuppressedViews = this._defaultSuppressedViews;
  }

  public Intermech.Docking.TabAlignment HeaderAlignment
  {
    [DebuggerStepThrough] get => this.PageViewsManager.HeaderAlignment;
    [DebuggerStepThrough] set => this.PageViewsManager.HeaderAlignment = value;
  }

  public bool ShouldSerializeHeaderAlignment()
  {
    return !this.PageViewsManager.HeaderAlignment.Equals((object) this._defaultHeaderAlignment);
  }

  public void ResetHeaderAlignment()
  {
    this.PageViewsManager.HeaderAlignment = this._defaultHeaderAlignment;
  }

  public LeftRightAlignment TabsAligment
  {
    [DebuggerStepThrough] get => this.PageViewsManager.TabsAligment;
    [DebuggerStepThrough] set => this.PageViewsManager.TabsAligment = value;
  }

  public bool ShouldSerializeTabsAligment()
  {
    return !this.PageViewsManager.TabsAligment.Equals((object) this._defaultTabsAligment);
  }

  public void ResetTabsAligment() => this.PageViewsManager.TabsAligment = this._defaultTabsAligment;
}

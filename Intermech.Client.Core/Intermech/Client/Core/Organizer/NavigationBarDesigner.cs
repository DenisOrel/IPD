
// Type: Intermech.Client.Core.Organizer.NavigationBarDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.Organizer;

[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Custom)]
internal class NavigationBarDesigner : ParentControlDesigner
{
  private NavigationBar _desControl;
  private IDesignerHost _host;
  private ISelectionService _selectionSrv;
  private IComponentChangeService _componentChangeSrv;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void AddBandVerbClicked(object sender, EventArgs e)
  {
    if (!(this._host.CreateComponent(typeof (NavigationBand)) is NavigationBand component))
      return;
    this._desControl.AddBand(component);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OncomponentChangeSrv_ComponentChanged(object sender, ComponentChangedEventArgs e)
  {
    if (e.Component != this._desControl || e.Member == null || e.Member.Name != "LayoutStyle")
      return;
    this.InitializeLayout();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnselectionSrv_SelectionChanged(object sender, EventArgs e)
  {
    if (!(this._selectionSrv.PrimarySelection is NavigationBand))
      return;
    this._desControl.ActiveBand = this._selectionSrv.PrimarySelection as NavigationBand;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (this._componentChangeSrv != null)
      this._componentChangeSrv.ComponentChanged -= new ComponentChangedEventHandler(this.OncomponentChangeSrv_ComponentChanged);
    if (this._selectionSrv == null)
      return;
    this._selectionSrv.SelectionChanged -= new EventHandler(this.OnselectionSrv_SelectionChanged);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  public override void Initialize(IComponent component)
  {
    base.Initialize(component);
    this._desControl = component as NavigationBar;
    this.InitializeServices();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="defaultValues"></param>
  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    this._desControl.BeginInit();
    base.InitializeNewComponent(defaultValues);
    this.InitializeLayout();
    this._desControl.EndInit();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="properties"></param>
  protected override void PostFilterProperties(IDictionary properties)
  {
    base.PostFilterProperties(properties);
    properties.Remove((object) "BackColor");
    properties.Remove((object) "BackgroundImage");
    properties.Remove((object) "BackgroundImageLayout");
    properties.Remove((object) "ContextMenuStrip");
    properties.Remove((object) "ForeColor");
    properties.Remove((object) "RightToLeft");
    properties.Remove((object) "Text");
  }

  /// <summary>
  /// 
  /// </summary>
  public override DesignerVerbCollection Verbs
  {
    get
    {
      return new DesignerVerbCollection(new DesignerVerb[1]
      {
        new DesignerVerb("Add band..", new EventHandler(this.AddBandVerbClicked))
      });
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="m"></param>
  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 514)
    {
      if (this.HandleClick(NativeMethods.LoWord(m.LParam), NativeMethods.HiWord(m.LParam)))
        return;
      base.WndProc(ref m);
    }
    else
      base.WndProc(ref m);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  private bool HandleClick(int x, int y)
  {
    if (this._desControl == null || this._selectionSrv == null)
      return false;
    foreach (NavigationBand band in (CollectionBase) this._desControl.Bands)
    {
      if (band.Button != null && band.Button.Bounds.Contains(x, y))
      {
        this._selectionSrv.SetSelectedComponents((ICollection) new ArrayList()
        {
          (object) band
        });
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeLayout()
  {
    if (this._desControl.NaviLayout != null)
      this._host.DestroyComponent((IComponent) this._desControl.NaviLayout);
    this._desControl.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeServices()
  {
    this._selectionSrv = this.GetService(typeof (ISelectionService)) as ISelectionService;
    if (this._selectionSrv != null)
      this._selectionSrv.SelectionChanged += new EventHandler(this.OnselectionSrv_SelectionChanged);
    this._componentChangeSrv = this.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
    if (this._componentChangeSrv != null)
      this._componentChangeSrv.ComponentChanged += new ComponentChangedEventHandler(this.OncomponentChangeSrv_ComponentChanged);
    this._host = (IDesignerHost) this.GetService(typeof (IDesignerHost));
  }
}

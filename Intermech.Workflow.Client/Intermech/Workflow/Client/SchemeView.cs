// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.SchemeView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Workflow;
using Intermech.Map;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

[ViewDescriptionProvider(typeof (SchemeView.SchemeViewDescriptionProvider))]
public class SchemeView : UserControl, IView
{
  private long _objectID;
  private IContainer components;
  private GraphView wfView;

  public SchemeView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
  }

  public void Activate(IView previousView)
  {
    this.wfView.AllowDelete = false;
    this.wfView.AllowEdit = false;
    this.wfView.AllowInsert = false;
    this.wfView.AllowDrop = false;
    this.wfView.Doc.ReadOnly = true;
    this.wfView.ProcessID = this._objectID;
    this.wfView.LoadProcess();
    Holder.EditorSettings.SetProperties(this.wfView);
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("Preview");

  public int ImageIndex => Holder.ViewImageIndex;

  public int OrderID => 0;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SchemeView));
    this.wfView = new GraphView();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.wfView, "wfView");
    this.wfView.AllowDrop = true;
    this.wfView.AllowEdit = false;
    this.wfView.AllowInsert = false;
    this.wfView.BackColor = Color.White;
    this.wfView.Border3DStyle = Border3DStyle.Etched;
    this.wfView.BorderStyle = BorderStyle.Fixed3D;
    this.wfView.DragsRealtime = true;
    this.wfView.Form = (wfEditorForm) null;
    this.wfView.GridPenDashStyle = DashStyle.Solid;
    this.wfView.GridSnapDrag = MapViewSnapStyle.None;
    this.wfView.GridSnapResize = MapViewSnapStyle.None;
    this.wfView.GridStyle = MapViewGridStyle.None;
    this.wfView.InterpolationMode = InterpolationMode.High;
    this.wfView.Modified = false;
    this.wfView.Name = "wfView";
    this.wfView.PortHighlightBrush = (Brush) null;
    this.wfView.PrimarySelectionColor = Color.DimGray;
    this.wfView.ShowHorizontalScrollBar = MapViewScrollBarVisibility.IfNeeded;
    this.wfView.ShowVerticalScrollBar = MapViewScrollBarVisibility.IfNeeded;
    this.wfView.SmoothingMode = SmoothingMode.HighQuality;
    this.wfView.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.wfView);
    this.Name = nameof (SchemeView);
    this.ResumeLayout(false);
  }

  private sealed class SchemeViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Preview"),
        ImageIndex = Holder.ViewImageIndex,
        OrderID = 0
      };
    }
  }
}

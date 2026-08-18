// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Views.AutoSelectionView
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Forms;
using Intermech.DataFormats;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Views;

[ViewDescriptionProvider(typeof (AutoSelectionView.AutoSelectionViewDescriptionProvider))]
internal class AutoSelectionView : UserControl, IView
{
  protected long _objectID;
  protected bool _firstRun;
  protected int _imageIndex = -1;
  protected Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule _rule;
  private IContainer components;
  private TreeView tvSelectionRule;
  private ImageList ilSelTree;
  private Button btnChange;
  private Panel pnlBottom;

  private void LoadSelectionTree()
  {
    this.btnChange.Enabled = false;
    this.tvSelectionRule.BeginUpdate();
    try
    {
      this.tvSelectionRule.Nodes.Clear();
      SelectionTreeViewUtils.AddSelectionRule(this.tvSelectionRule, this._rule);
    }
    finally
    {
      this.tvSelectionRule.EndUpdate();
      this.btnChange.Enabled = this._rule != null;
    }
  }

  public AutoSelectionView()
  {
    this.InitializeComponent();
    AutosSelectConsts.Images.LoadImages(ref this.ilSelTree);
    this.tvSelectionRule.ImageList = this.ilSelTree;
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      this._objectID = itemData.Value;
    this._firstRun = true;
  }

  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView || !this._firstRun)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID, false);
      this._rule = dbObject != null ? Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(dbObject) : (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule) null;
    }
    this.LoadSelectionTree();
    this._firstRun = false;
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_66");

  public int ImageIndex
  {
    get
    {
      if (this._imageIndex != -1)
        return this._imageIndex;
      ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        int num = service.IndexOf(4, AutoSelectionConsts.objTypeRuleID);
        if (num >= 0)
          this._imageIndex = num;
      }
      return this._imageIndex;
    }
  }

  public int OrderID => 11;

  private void btnChange_Click(object sender, EventArgs e)
  {
    if (this._objectID == 0L || this._rule == null)
      return;
    AutoSelectionEditForm form = new AutoSelectionEditForm();
    form.ReadOnly = false;
    form.Rule = this._rule;
    if (form.ShowTopDialog() != DialogResult.OK)
      return;
    this._rule = form.Rule;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID, false);
      if (dbObject != null)
        this._rule.Save(dbObject, sessionKeeper.Session);
    }
    this.LoadSelectionTree();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionView));
    this.tvSelectionRule = new TreeView();
    this.ilSelTree = new ImageList(this.components);
    this.btnChange = new Button();
    this.pnlBottom = new Panel();
    this.pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tvSelectionRule, "tvSelectionRule");
    this.tvSelectionRule.ImageList = this.ilSelTree;
    this.tvSelectionRule.Name = "tvSelectionRule";
    this.ilSelTree.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.ilSelTree, "ilSelTree");
    this.ilSelTree.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.btnChange, "btnChange");
    this.btnChange.Name = "btnChange";
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    this.pnlBottom.Controls.Add((Control) this.btnChange);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tvSelectionRule);
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (AutoSelectionView);
    this.Tag = (object) "  ";
    this.pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class AutoSelectionViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
      return new ViewDescription()
      {
        Caption = Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_66"),
        ImageIndex = service.IndexOf(4, AutoSelectionConsts.objTypeRuleID),
        OrderID = 11
      };
    }
  }
}

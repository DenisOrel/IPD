// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.IMCatalogsPropertyPage
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class IMCatalogsPropertyPage : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  private bool _isAdmin;
  private bool _modified;
  private IContainer components;
  private ImageList cmdsIL;
  private Panel panel5;
  private Label label9;
  private ListView View;
  private ToolBar ToolBar;
  private Label CapLabel;
  private ToolBarButton AddButton;
  private ToolBarButton DeleteButton;
  private ToolBarButton delButton;
  private ToolBarButton toolBarButton2;

  public IMCatalogsPropertyPage() => this.InitializeComponent();

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => LocalizationHolder.rm.GetString("ECO.Client_442");

  public void Apply()
  {
    if (!this.Modified)
      return;
    IMCatalogs.All.Clear();
    foreach (ListViewItem listViewItem in this.View.Items)
      IMCatalogs.All.Add((long) listViewItem.Tag);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      IMCatalogs.All.Save(sessionKeeper.Session);
    this.Modified = false;
  }

  public void Cancel() => this.Modified = false;

  public string HelpTopicID => "";

  public string HeaderText => this.PageName;

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void FillView()
  {
    this.View.SmallImageList = ECOPlugin.plugin.IconService.ImageList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._isAdmin = sessionKeeper.Session.IsAdmin;
      if (!IMCatalogs.All.Loaded)
        IMCatalogs.All.Load(sessionKeeper.Session);
      if (!this._isAdmin)
        this.View.ForeColor = SystemColors.GrayText;
      this.View.Items.Clear();
      foreach (long objId in (List<long>) IMCatalogs.All)
        this.AddItem(objId, sessionKeeper.Session);
    }
    if (this.View.Items.Count > 0)
      this.View.Items[0].Selected = true;
    this.UpdateEnabled();
  }

  private ListViewItem SelectedItem
  {
    get => this.View.SelectedItems.Count > 0 ? this.View.SelectedItems[0] : (ListViewItem) null;
  }

  private ListViewItem FindItem(int objTypeId)
  {
    foreach (ListViewItem listViewItem in this.View.Items)
    {
      if (objTypeId.Equals(listViewItem.Tag))
        return listViewItem;
    }
    return (ListViewItem) null;
  }

  private ListViewItem AddItem(long objId, IUserSession ius)
  {
    ListViewItem li = new ListViewItem();
    this.FillItem(li, objId, ius);
    this.View.Items.Add(li);
    li.Selected = true;
    return li;
  }

  private void FillItem(ListViewItem li, long objId, IUserSession ius)
  {
    QuickObjectInfo objectInfo = ius.GetObjectInfo(objId);
    li.Text = objectInfo.Caption;
    li.Tag = (object) objId;
  }

  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      EventHandler changed = this.Changed;
      if (!this._modified || changed == null)
        return;
      changed((object) this, (EventArgs) null);
    }
  }

  private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e == null || sender == null || !e.Button.Enabled)
      return;
    if (Convert.ToInt32(e.Button.Tag) == 1)
    {
      IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
      long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_443"), string.Empty, rootDescriptor, SelectionOptions.Default | SelectionOptions.HideTree);
      if (numArray == null || numArray.Length == 0)
        return;
      bool flag = false;
      this.View.SuspendLayout();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long num in numArray)
        {
          if (!this.View.Items.ContainsKey(sessionKeeper.Session.GetObjectInfo(num).Caption))
          {
            this.AddItem(num, sessionKeeper.Session);
            flag = true;
          }
        }
      }
      this.View.ResumeLayout();
      if (!flag)
        return;
      this.Modified = true;
    }
    else
    {
      this.View.Items.Remove(this.SelectedItem);
      this.Modified = true;
    }
  }

  private void UpdateEnabled()
  {
    this.AddButton.Enabled = this._isAdmin;
    this.DeleteButton.Enabled = this._isAdmin && this.SelectedItem != null;
  }

  private void View_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateEnabled();

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.FillView();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IMCatalogsPropertyPage));
    this.cmdsIL = new ImageList(this.components);
    this.panel5 = new Panel();
    this.View = new ListView();
    this.ToolBar = new ToolBar();
    this.AddButton = new ToolBarButton();
    this.DeleteButton = new ToolBarButton();
    this.label9 = new Label();
    this.CapLabel = new Label();
    this.toolBarButton2 = new ToolBarButton();
    this.panel5.SuspendLayout();
    this.SuspendLayout();
    this.cmdsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("cmdsIL.ImageStream");
    this.cmdsIL.TransparentColor = Color.Fuchsia;
    this.cmdsIL.Images.SetKeyName(0, "add.ico");
    this.cmdsIL.Images.SetKeyName(1, "del.ico");
    this.cmdsIL.Images.SetKeyName(2, "answer.ico");
    this.panel5.BorderStyle = BorderStyle.Fixed3D;
    this.panel5.Controls.Add((System.Windows.Forms.Control) this.View);
    this.panel5.Controls.Add((System.Windows.Forms.Control) this.ToolBar);
    this.panel5.Controls.Add((System.Windows.Forms.Control) this.label9);
    this.panel5.Dock = DockStyle.Fill;
    this.panel5.Location = new Point(15, 55);
    this.panel5.Margin = new Padding(4, 5, 4, 5);
    this.panel5.Name = "panel5";
    this.panel5.Size = new Size(856, 496);
    this.panel5.TabIndex = 2;
    this.View.BorderStyle = BorderStyle.None;
    this.View.Dock = DockStyle.Fill;
    this.View.FullRowSelect = true;
    this.View.HideSelection = false;
    this.View.Location = new Point(0, 0);
    this.View.Margin = new Padding(4, 5, 4, 5);
    this.View.MultiSelect = false;
    this.View.Name = "View";
    this.View.Size = new Size(827, 492);
    this.View.TabIndex = 5;
    this.View.UseCompatibleStateImageBehavior = false;
    this.View.View = System.Windows.Forms.View.List;
    this.View.SelectedIndexChanged += new EventHandler(this.View_SelectedIndexChanged);
    this.ToolBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.AddButton,
      this.DeleteButton
    });
    this.ToolBar.ButtonSize = new Size(22, 22);
    this.ToolBar.Divider = false;
    this.ToolBar.Dock = DockStyle.Right;
    this.ToolBar.DropDownArrows = true;
    this.ToolBar.ImageList = this.cmdsIL;
    this.ToolBar.ImeMode = ImeMode.NoControl;
    this.ToolBar.Location = new Point(827, 0);
    this.ToolBar.Margin = new Padding(4, 5, 4, 5);
    this.ToolBar.Name = "ToolBar";
    this.ToolBar.ShowToolTips = true;
    this.ToolBar.Size = new Size(22, 492);
    this.ToolBar.TabIndex = 6;
    this.ToolBar.TextAlign = ToolBarTextAlign.Right;
    this.ToolBar.ButtonClick += new ToolBarButtonClickEventHandler(this.ToolBar_ButtonClick);
    this.AddButton.ImageIndex = 0;
    this.AddButton.Name = "AddButton";
    this.AddButton.Tag = (object) "1";
    this.AddButton.ToolTipText = "Добавить тип(ы) объектов...";
    this.DeleteButton.ImageIndex = 1;
    this.DeleteButton.Name = "DeleteButton";
    this.DeleteButton.Tag = (object) "2";
    this.DeleteButton.ToolTipText = "Удалить тип объектов";
    this.label9.BorderStyle = BorderStyle.Fixed3D;
    this.label9.Dock = DockStyle.Right;
    this.label9.ImeMode = ImeMode.NoControl;
    this.label9.Location = new Point(849, 0);
    this.label9.Margin = new Padding(4, 0, 4, 0);
    this.label9.Name = "label9";
    this.label9.Size = new Size(3, 492);
    this.label9.TabIndex = 7;
    this.label9.Text = "label9";
    this.CapLabel.Dock = DockStyle.Top;
    this.CapLabel.Location = new Point(15, 15);
    this.CapLabel.Margin = new Padding(4, 0, 4, 0);
    this.CapLabel.Name = "CapLabel";
    this.CapLabel.Padding = new Padding(0, 0, 0, 15);
    this.CapLabel.Size = new Size(856, 40);
    this.CapLabel.TabIndex = 5;
    this.CapLabel.Text = "Каталоги IMBASE, из которых можно вставлять объекты в извещения:";
    this.toolBarButton2.ImageIndex = 1;
    this.toolBarButton2.Name = "toolBarButton2";
    this.toolBarButton2.Tag = (object) "2";
    this.toolBarButton2.ToolTipText = "Удалить строку";
    this.AutoScaleDimensions = new SizeF(9f, 20f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.panel5);
    this.Controls.Add((System.Windows.Forms.Control) this.CapLabel);
    this.Margin = new Padding(4, 5, 4, 5);
    this.Name = nameof (IMCatalogsPropertyPage);
    this.Padding = new Padding(15, 15, 15, 15);
    this.Size = new Size(886, 566);
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.ResumeLayout(false);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.QuantityPhysListSettingsPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class QuantityPhysListSettingsPage : 
  UserControl,
  IPropertyPage,
  IPropertyPageSearchOptionEvents
{
  private System.IServiceProvider _provider;
  private bool _modified;
  private IContainer components;
  private Panel panel1;
  private ToolBar ToolBar;
  private ToolBarButton AddButton;
  private ToolBarButton DeleteButton;
  private ImageList cmdsIL;
  private ListView listView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;

  public QuantityPhysListSettingsPage(System.IServiceProvider provider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this.InitializeComponent();
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_108a"), (IPropertyPage) this);
  }

  public QuantityPhysListSettingsPage() => this.InitializeComponent();

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_108b");

  public void Apply()
  {
    if (!this.Modified)
      return;
    this.SaveData();
    this.Modified = false;
  }

  public void Cancel() => this.Modified = false;

  public string HelpTopicID => "";

  public string HeaderText => this.PageName;

  public List<string> GetOptionNames()
  {
    if (!(this.Control is System.Windows.Forms.Control))
      return new List<string>();
    return new List<string>((IEnumerable<string>) new string[1]
    {
      LocalizationHolder.rm.GetString("DatabaseConfigurator_108b")
    });
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

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.FillView(this.LoadData());
  }

  private List<Guid> LoadData()
  {
    List<Guid> guidList = new List<Guid>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      string str = sessionKeeper.Session.Configurations.ReadString("CLIENT", "COMMON", "QUANTITYPHYSLIST", string.Empty, DBConfigMode.GlobalOnly);
      if (str != string.Empty)
      {
        string[] strArray = str.Split(';');
        if (strArray != null)
        {
          for (int index = 0; index < strArray.Length; ++index)
          {
            try
            {
              guidList.Add(Guid.Parse(strArray[index]));
            }
            catch
            {
              if (!this.Modified)
                this.Modified = true;
            }
          }
        }
      }
    }
    return guidList;
  }

  private bool SaveData()
  {
    List<string> values = new List<string>();
    for (int index = 0; index < this.listView.Items.Count; ++index)
    {
      if (!((IEnumerable<Guid>) SystemGUIDs.objectQuantityPhysListGuids).Contains<Guid>((Guid) this.listView.Items[index].Tag))
        values.Add(((Guid) this.listView.Items[index].Tag).ToString("N"));
    }
    string str = string.Join(";", (IEnumerable<string>) values);
    if (str.Length > Intermech.Consts.MaxStringSize)
      throw new Exception($"Невозможно произвести сохранение списка физических величин для параметра \"{LocalizationHolder.rm.GetString("DatabaseConfigurator_108b")}\":\nдлина сохраняемой строки {str.Length} символов при максимальной допустимой длине {Intermech.Consts.MaxStringSize} символов.");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      sessionKeeper.Session.Configurations.WriteString("CLIENT", "COMMON", "QUANTITYPHYSLIST", str, 0L);
    }
    MeasureHelper.AsQuantityPhysList = (List<Guid>) null;
    return true;
  }

  private void FillView(List<Guid> phListExtended)
  {
    this.listView.BeginUpdate();
    try
    {
      this.listView.Items.Clear();
      using (SessionKeeper sk = new SessionKeeper())
      {
        foreach (Guid quantityPhysListGuid in SystemGUIDs.objectQuantityPhysListGuids)
          this.AddToListView(quantityPhysListGuid, sk.Session);
        phListExtended.ForEach((Action<Guid>) (g => this.AddToListView(g, sk.Session)));
      }
    }
    finally
    {
      this.listView.EndUpdate();
    }
    this.UpdateControls();
  }

  private void AddToListView(Guid g, IUserSession session)
  {
    IDBObject dbObject = session.GetObject(g, false);
    if (dbObject != null)
    {
      this.AddToListView(dbObject.ObjectGUID, dbObject.Caption);
    }
    else
    {
      if (this.Modified)
        return;
      this.Modified = true;
    }
  }

  private void AddToListView(Guid g, string s)
  {
    ListViewItem listViewItem = this.listView.Items.Add(s);
    listViewItem.SubItems.Add(g.ToString());
    listViewItem.Tag = (object) g;
  }

  private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e == null || sender == null || !e.Button.Enabled)
      return;
    if (Convert.ToInt32(e.Button.Tag) == 1)
      this.AddItem();
    if (Convert.ToInt32(e.Button.Tag) != 2)
      return;
    this.DeleteItem();
  }

  private void AddItem()
  {
    List<Guid> list = new List<Guid>();
    for (int index = 0; index < this.listView.Items.Count; ++index)
      list.Add((Guid) this.listView.Items[index].Tag);
    QuantityPhysListSelectForm physListSelectForm = new QuantityPhysListSelectForm();
    if (physListSelectForm.ShowDialog(list) != DialogResult.OK)
      return;
    foreach (KeyValuePair<Guid, string> selected in physListSelectForm.SelectedList)
      this.AddToListView(selected.Key, selected.Value);
    this.Modified = true;
    this.UpdateControls();
  }

  private void DeleteItem()
  {
    if (this.listView.SelectedItems.Count == 0 || IMMessageBox.Show("Подтверждение", $"Удалить \"{this.listView.SelectedItems[0].Text}\"?", MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    int index = this.listView.SelectedItems[0].Index;
    this.listView.Items.Remove(this.listView.SelectedItems[0]);
    if (index < this.listView.Items.Count)
      this.listView.Items[index].Selected = true;
    else if (this.listView.Items.Count > 0)
      this.listView.Items[this.listView.Items.Count - 1].Selected = true;
    this.Modified = true;
    this.UpdateControls();
  }

  private void listView_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateControls();

  private void UpdateControls()
  {
    this.DeleteButton.Enabled = this.listView.SelectedItems.Count > 0 && !((IEnumerable<Guid>) SystemGUIDs.objectQuantityPhysListGuids).Contains<Guid>((Guid) this.listView.SelectedItems[0].Tag);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (QuantityPhysListSettingsPage));
    this.panel1 = new Panel();
    this.listView = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.ToolBar = new ToolBar();
    this.AddButton = new ToolBarButton();
    this.DeleteButton = new ToolBarButton();
    this.cmdsIL = new ImageList(this.components);
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.listView);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.ToolBar);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(768 /*0x0300*/, 507);
    this.panel1.TabIndex = 0;
    this.listView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.listView.Dock = DockStyle.Fill;
    this.listView.FullRowSelect = true;
    this.listView.HideSelection = false;
    this.listView.Location = new Point(0, 0);
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Size = new Size(746, 507);
    this.listView.TabIndex = 8;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.SelectedIndexChanged += new EventHandler(this.listView_SelectedIndexChanged);
    this.columnHeader1.Text = "Наименование";
    this.columnHeader1.Width = 300;
    this.columnHeader2.Text = "Глобальный идентификатор";
    this.columnHeader2.Width = 300;
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
    this.ToolBar.Location = new Point(746, 0);
    this.ToolBar.Name = "ToolBar";
    this.ToolBar.ShowToolTips = true;
    this.ToolBar.Size = new Size(22, 507);
    this.ToolBar.TabIndex = 7;
    this.ToolBar.TextAlign = ToolBarTextAlign.Right;
    this.ToolBar.ButtonClick += new ToolBarButtonClickEventHandler(this.ToolBar_ButtonClick);
    this.AddButton.ImageIndex = 0;
    this.AddButton.Name = "AddButton";
    this.AddButton.Tag = (object) "1";
    this.AddButton.ToolTipText = "Добавить строку";
    this.DeleteButton.ImageIndex = 1;
    this.DeleteButton.Name = "DeleteButton";
    this.DeleteButton.Tag = (object) "2";
    this.DeleteButton.ToolTipText = "Удалить строку";
    this.cmdsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("cmdsIL.ImageStream");
    this.cmdsIL.TransparentColor = Color.Fuchsia;
    this.cmdsIL.Images.SetKeyName(0, "add.ico");
    this.cmdsIL.Images.SetKeyName(1, "del.ico");
    this.cmdsIL.Images.SetKeyName(2, "answer.ico");
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.panel1);
    this.Name = nameof (QuantityPhysListSettingsPage);
    this.Size = new Size(768 /*0x0300*/, 507);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.OwneredAccauntsControl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.Email;

public class OwneredAccauntsControl : UserControl, IPropertyPage
{
  private bool _changed;
  private EmailAccaunt[] _accaunts;
  private IContainer components;
  private SplitContainer splitContainer1;
  private PropertyGrid propertyGrid1;
  private ImageList imageList1;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem tmiRefresh;
  private Panel panel1;
  private ListView listView1;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem biRefresh;

  public OwneredAccauntsControl(EmailAccaunt[] accaunts)
  {
    this.InitializeComponent();
    this._accaunts = accaunts;
    this.RefreshData(false);
    ((INotificationService) ApplicationServices.Container.GetService(typeof (INotificationService))).Subscribe("EmailAccauntChanged", new NotificationEventHandler(this.OnEmailAccauntChanged));
  }

  private void OnEmailAccauntChanged(object sender, NotificationEventArgs e)
  {
    if (sender == this)
      return;
    this.RefreshData(true);
  }

  private void RefreshData(bool fromDataBase)
  {
    this.listView1.SelectedIndexChanged -= new EventHandler(this.listView1_SelectedIndexChanged);
    this.listView1.Items.Clear();
    try
    {
      Guid empty = Guid.Empty;
      if (fromDataBase)
      {
        this._accaunts = (EmailAccaunt[]) null;
        IEmailService customService = (IEmailService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService));
        if (customService != null)
        {
          ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
          this._accaunts = customService.GetAccaunts(service.UserID, false);
        }
      }
      if (this._accaunts != null)
      {
        if (this._accaunts.Length != 0)
        {
          for (int index = 0; index < this._accaunts.Length; ++index)
            this.listView1.Items.Add(new ListViewItem(this._accaunts[index].Email)
            {
              Tag = (object) new OwneredAccauntsControl.AccauntInfo(this._accaunts[index]),
              ImageIndex = 0
            });
        }
      }
    }
    finally
    {
      this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    }
    if (this.listView1.Items.Count <= 0)
      return;
    this.listView1.Items[0].Focused = true;
    this.listView1.Items[0].Selected = true;
    this.propertyGrid1.SelectedObject = this.listView1.Items[0].Tag;
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => LocalizationHolder.rm.GetString("Workflow.Client_74");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (!this._changed)
      return;
    IEmailService customService = (IEmailService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService));
    if (customService != null)
    {
      for (int index = 0; index < this.listView1.Items.Count; ++index)
      {
        if (this.listView1.SelectedItems[0].Tag is OwneredAccauntsControl.AccauntInfo tag)
          customService.UpdateAccaunt(tag.Guid, tag.Login, tag.Password);
      }
    }
    ((INotificationService) ApplicationServices.Container.GetService(typeof (INotificationService))).FireEvent((object) this, new NotificationEventArgs("EmailAccauntChanged"));
    this._changed = false;
  }

  public void Cancel() => this._changed = false;

  public string HelpTopicID => string.Empty;

  private void tmiRefresh_Click(object sender, EventArgs e) => this.RefreshData(true);

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.listView1.SelectedItems.Count == 0 || this.listView1.SelectedItems[0].Tag == null)
      return;
    this.propertyGrid1.SelectedObject = this.listView1.SelectedItems[0].Tag;
  }

  private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this._changed = true;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OwneredAccauntsControl));
    this.splitContainer1 = new SplitContainer();
    this.panel1 = new Panel();
    this.listView1 = new ListView();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.tmiRefresh = new ToolStripMenuItem();
    this.imageList1 = new ImageList(this.components);
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.biRefresh = new ButtonItem();
    this.propertyGrid1 = new PropertyGrid();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.panel1);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.toolBar1);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.propertyGrid1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.listView1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.ContextMenuStrip = this.contextMenuStrip1;
    this.listView1.MultiSelect = false;
    this.listView1.Name = "listView1";
    this.listView1.SmallImageList = this.imageList1;
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.List;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.tmiRefresh
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.tmiRefresh, "tmiRefresh");
    this.tmiRefresh.Name = "tmiRefresh";
    this.tmiRefresh.Click += new EventHandler(this.tmiRefresh_Click);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "id_card.png");
    this.imageList1.Images.SetKeyName(1, "refresh.png");
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("7cd08cb7-7ea6-468d-87d4-7ffb6e406d1b");
    this.toolBar1.Hidden = false;
    this.toolBar1.ImageList = this.imageList1;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.biRefresh
    });
    this.toolBar1.Name = "toolBar1";
    componentResourceManager.ApplyResources((object) this.biRefresh, "biRefresh");
    this.biRefresh.ImageIndex = 1;
    this.biRefresh.Click += new EventHandler(this.tmiRefresh_Click);
    componentResourceManager.ApplyResources((object) this.propertyGrid1, "propertyGrid1");
    this.propertyGrid1.Name = "propertyGrid1";
    this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer1);
    this.Name = nameof (OwneredAccauntsControl);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class AccauntInfo
  {
    private string _login = string.Empty;
    private string _password = string.Empty;
    private Guid _guid;

    public AccauntInfo(EmailAccaunt accaunt)
    {
      this._guid = accaunt.Guid;
      this._login = accaunt.Login;
      this._password = accaunt.Password;
    }

    [Browsable(false)]
    public Guid Guid => this._guid;

    [CustomDisplayName("Attribute.Workflow.Client_1")]
    [CustomDescription("Attribute.Workflow.Client_2")]
    [CustomCategory("Attribute.Workflow.Client_3")]
    public string Login
    {
      get => this._login;
      set => this._login = value;
    }

    [CustomDisplayName("Attribute.Workflow.Client_4")]
    [CustomDescription("Attribute.Workflow.Client_5")]
    [TypeConverter(typeof (PasswordTypeConverter))]
    [Editor(typeof (OwneredAccauntsControl.NewPasswordEditor), typeof (UITypeEditor))]
    [CustomCategory("Attribute.Workflow.Client_3")]
    public string Password
    {
      get => this._password;
      set => this._password = value;
    }
  }

  private class NewPasswordEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider sp,
      object value)
    {
      string password;
      if (UserPasswordForm.Execute(out password, true) == DialogResult.OK)
        value = (object) password;
      return value;
    }
  }
}

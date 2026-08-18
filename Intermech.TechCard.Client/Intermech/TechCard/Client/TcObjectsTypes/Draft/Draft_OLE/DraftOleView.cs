// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE.DraftOleView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;

/// <summary>Закладка просмотра /редактирования OLE эскизов</summary>
public class DraftOleView : TechCardBaseView
{
  /// <summary>Возможность редактирования свойств объекта</summary>
  protected bool _canEdit;
  /// <summary>Название / заголовок объекта</summary>
  protected string _objName = string.Empty;
  /// <summary>OLE контрол</summary>
  private DraftOleControl _oleControl;
  /// <summary>Наименование картинки в списке</summary>
  internal const string IconImageName = "imgDraftOle";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip toolStrip;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton tsbtnEdit;
  private ToolStripDropDownButton tsbtnView;
  private ToolStripMenuItem tsmiViewClip;
  private ToolStripMenuItem tsmiViewStretch;
  private ToolStripMenuItem tsmiViewZoom;
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem ctsmiEdit;
  private Panel pnlClient;

  /// <summary>Вызов редактора</summary>
  /// <returns></returns>
  protected bool CallEditor()
  {
    if (!this.CanModify)
      return false;
    Stream oleStream = this._oleControl.OleStream;
    if (!DraftOleEditDialog.ShowModal(ref oleStream, this._objName, true) || oleStream == null)
      return false;
    oleStream.Position = 0L;
    this._oleControl.OleStream = oleStream;
    this._oleControl.Update();
    this.Modified = true;
    this.UpdateControls();
    return true;
  }

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitResources()
  {
    base.InitResources();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this.toolStrip.ImageList = service.ImageList;
      this.contextMenuStrip.ImageList = service.ImageList;
      this.ctsmiEdit.ImageIndex = service.ImageIndex("imgDesktopObjectType");
      this.tsbtnEdit.Image = service.ImageList.Images[service.ImageIndex("imgDesktopObjectType")];
      this.tsbtnView.Image = service.ImageList.Images[service.ImageIndex("imgView")];
      this.tsmiViewClip.Image = service.ImageList.Images[service.ImageIndex("imgZoom1to1")];
      this.tsmiViewZoom.Image = service.ImageList.Images[service.ImageIndex("imgZoomAll")];
      this._imageIndex = service.ImageIndex("imgDraftOle");
    }
    this.tsmiViewClip.Tag = (object) PictureBoxSizeMode.Normal;
    this.tsmiViewZoom.Tag = (object) PictureBoxSizeMode.Zoom;
    this.tsmiViewStretch.Tag = (object) PictureBoxSizeMode.StretchImage;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ReleaseResources()
  {
    base.ReleaseResources();
    if (this._oleControl == null)
      return;
    this._oleControl.OleStream = (Stream) null;
  }

  /// <summary>Инициализация кастом контролов</summary>
  protected override void InitializeCustomControls()
  {
    this.InitializeComponent();
    base.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this._oleControl = new DraftOleControl();
    this.pnlClient.Controls.Add((Control) this._oleControl);
    this._oleControl.ReadOnly = true;
    this._oleControl.Parent = (Control) this;
    this._oleControl.Dock = DockStyle.Fill;
    this._oleControl.Name = "oleControl";
    this._oleControl.BringToFront();
    this._oleControl.ContextMenuStrip = this.contextMenuStrip;
    this.PerformLayout();
  }

  /// <summary>Инициализация кастом сообщений</summary>
  protected override void InitializeCustomMessages()
  {
    base.InitializeCustomMessages();
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_181");
    this._locMessageTxt = LocalizationHolder.rm.GetString("TechCard.Client_448");
  }

  /// <summary>Загрузить информацию в контрол</summary>
  protected override void LoadData()
  {
    this._canEdit = false;
    this._objName = string.Empty;
    if (this._objID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objID, false);
        if (dbObject != null)
        {
          this._canEdit = dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.CheckoutBy == sessionKeeper.Session.UserID;
          this._objName = dbObject.Caption;
        }
      }
    }
    if (this._objID == 0L)
    {
      this._oleControl.OleStream = (Stream) null;
    }
    else
    {
      DraftOleClass draftOleClass = new DraftOleClass(this._objID);
      if (draftOleClass.LoadData() && draftOleClass.DataStream != null)
      {
        if (draftOleClass.DataStream.Length > 0L)
        {
          try
          {
            this._oleControl.OleStream = draftOleClass.DataStream;
            this._oleControl.Update();
          }
          catch (Exception ex)
          {
            IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
            if (service != null)
            {
              string category = LocalizationHolder.rm.GetString("TechCard.Client_424");
              service.Activate(category);
              service.WriteString(category, string.Format(LocalizationHolder.rm.GetString(sc_19518.ssp_techcard_19519()), (object) ex.Message));
              service.WriteString(category, ex.StackTrace);
              service.ShowView();
            }
          }
        }
      }
    }
    base.LoadData();
  }

  /// <summary>Сохранить информацию из контрола</summary>
  protected override void SaveData(bool sendNotifications = true)
  {
    if (!this.Modified)
      return;
    new DraftOleClass()
    {
      ObjectId = this._objID,
      DataStream = this._oleControl.OleStream
    }.SaveData();
    base.SaveData(sendNotifications);
    if (!sendNotifications)
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._objID));
  }

  /// <summary>Обновить состояние элементов управления закладки</summary>
  protected override void UpdateControls()
  {
    base.UpdateControls();
    this.tsbtnEdit.Enabled = this.ctsmiEdit.Enabled = this.CanModify;
  }

  /// <summary>Загрузка настроек</summary>
  protected override void LoadSettings()
  {
    base.LoadSettings();
    HybridDictionary config = new HybridDictionary(1);
    TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) config);
    if (this._oleControl == null || !config.Contains((object) "OlePictSizeMode"))
      return;
    PictureBoxSizeMode pictureBoxSizeMode = PictureBoxSizeMode.Normal;
    if (config[(object) "OlePictSizeMode"] is PictureBoxSizeMode)
      pictureBoxSizeMode = (PictureBoxSizeMode) config[(object) "OlePictSizeMode"];
    this._oleControl.SizeMode = pictureBoxSizeMode;
  }

  /// <summary>Сохранение настроек</summary>
  protected override void SaveSettings()
  {
    base.SaveSettings();
    HybridDictionary config = new HybridDictionary(1);
    if (this._oleControl != null)
      config.Add((object) "OlePictSizeMode", (object) this._oleControl.SizeMode);
    TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) config);
  }

  /// <summary>Caption</summary>
  public override string Caption => base.Caption;

  /// <summary>OrderID</summary>
  public override int OrderID => 0;

  /// <summary>Can modifying flag</summary>
  public override bool CanModify => base.CanModify && this._canEdit;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiViewSelect_Click(object sender, EventArgs e)
  {
    if (!(sender is ToolStripMenuItem toolStripMenuItem))
      return;
    this._oleControl.SizeMode = (PictureBoxSizeMode) toolStripMenuItem.Tag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsbtnView_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsbtnEdit_Click(object sender, EventArgs e) => this.CallEditor();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ctsmiEdit_Click(object sender, EventArgs e) => this.CallEditor();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.LoadData();
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    this.SaveData(true);
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsbtnView_DropDownOpened(object sender, EventArgs e)
  {
    foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection) this.tsbtnView.DropDownItems)
      dropDownItem.Checked = (PictureBoxSizeMode) dropDownItem.Tag == this._oleControl.SizeMode;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DraftOleView));
    this.toolStrip = new ToolStrip();
    this.tsbtnEdit = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tsbtnView = new ToolStripDropDownButton();
    this.tsmiViewClip = new ToolStripMenuItem();
    this.tsmiViewStretch = new ToolStripMenuItem();
    this.tsmiViewZoom = new ToolStripMenuItem();
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.ctsmiEdit = new ToolStripMenuItem();
    this.pnlClient = new Panel();
    this.pnButtons.SuspendLayout();
    this.toolStrip.SuspendLayout();
    this.contextMenuStrip.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.toolStrip.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsbtnEdit,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tsbtnView
    });
    componentResourceManager.ApplyResources((object) this.toolStrip, "toolStrip");
    this.toolStrip.Name = "toolStrip";
    componentResourceManager.ApplyResources((object) this.tsbtnEdit, "tsbtnEdit");
    this.tsbtnEdit.Name = "tsbtnEdit";
    this.tsbtnEdit.Click += new EventHandler(this.tsbtnEdit_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.tsbtnView.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiViewClip,
      (ToolStripItem) this.tsmiViewStretch,
      (ToolStripItem) this.tsmiViewZoom
    });
    componentResourceManager.ApplyResources((object) this.tsbtnView, "tsbtnView");
    this.tsbtnView.Name = "tsbtnView";
    this.tsbtnView.DropDownOpened += new EventHandler(this.tsbtnView_DropDownOpened);
    this.tsbtnView.Click += new EventHandler(this.tsbtnView_Click);
    this.tsmiViewClip.CheckOnClick = true;
    this.tsmiViewClip.Name = "tsmiViewClip";
    componentResourceManager.ApplyResources((object) this.tsmiViewClip, "tsmiViewClip");
    this.tsmiViewClip.Click += new EventHandler(this.tsmiViewSelect_Click);
    this.tsmiViewStretch.CheckOnClick = true;
    this.tsmiViewStretch.Name = "tsmiViewStretch";
    componentResourceManager.ApplyResources((object) this.tsmiViewStretch, "tsmiViewStretch");
    this.tsmiViewStretch.Click += new EventHandler(this.tsmiViewSelect_Click);
    this.tsmiViewZoom.CheckOnClick = true;
    this.tsmiViewZoom.Name = "tsmiViewZoom";
    componentResourceManager.ApplyResources((object) this.tsmiViewZoom, "tsmiViewZoom");
    this.tsmiViewZoom.Click += new EventHandler(this.tsmiViewSelect_Click);
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.ctsmiEdit
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.ctsmiEdit.Name = "ctsmiEdit";
    componentResourceManager.ApplyResources((object) this.ctsmiEdit, "ctsmiEdit");
    this.ctsmiEdit.Click += new EventHandler(this.ctsmiEdit_Click);
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.toolStrip);
    this.Name = nameof (DraftOleView);
    this.Controls.SetChildIndex((Control) this.toolStrip, 0);
    this.Controls.SetChildIndex((Control) this.pnlClient, 0);
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.pnButtons.ResumeLayout(false);
    this.toolStrip.ResumeLayout(false);
    this.toolStrip.PerformLayout();
    this.contextMenuStrip.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.View.ProcRouteEntryArticleView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Localization;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.View;

/// <summary>
/// Закладка просмотра /редактирования входимости маршрута обработки (для Изделий)
/// </summary>
public class ProcRouteEntryArticleView : TechCardBaseView
{
  /// <summary>Объект "Входимость маршрута обработки"</summary>
  private readonly ProcRouteEntryObject _procRouteEntryObject = new ProcRouteEntryObject(-1L);
  /// <summary>Возможность редактирования свойств объекта</summary>
  private bool _canEdit;
  /// <summary>Наименование картинки в списке</summary>
  internal const string IconImageName = "imgProcRouteEntryArticle";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem ctsmiEdit;
  private Panel pnlClient;
  private ProcRouteEntryForArticleControl procRouteEntryForArticleControl;

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitResources()
  {
    base.InitResources();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    this._imageIndex = service.ImageIndex("imgProcRouteEntryArticle");
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ReleaseResources()
  {
    this._procRouteEntryObject.Changed -= new EventHandler(this.ProcRouteEntryObjectOnChanged);
    base.ReleaseResources();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ReleaseServices()
  {
    this.procRouteEntryForArticleControl.CancelLoadData();
    base.ReleaseServices();
  }

  /// <summary>Инициализация кастом контролов</summary>
  protected override void InitializeCustomControls()
  {
    this.InitializeComponent();
    base.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.procRouteEntryForArticleControl.ProcRouteEntryObject = this._procRouteEntryObject;
    this._procRouteEntryObject.Changed -= new EventHandler(this.ProcRouteEntryObjectOnChanged);
    this._procRouteEntryObject.Changed += new EventHandler(this.ProcRouteEntryObjectOnChanged);
  }

  private void ProcRouteEntryObjectOnChanged(object sender, EventArgs e) => this.Modified = true;

  /// <summary>Инициализация кастом сообщений</summary>
  protected override void InitializeCustomMessages()
  {
    base.InitializeCustomMessages();
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_ProcRouteEntryArticleView_Name");
  }

  /// <summary>Загрузить информацию в контрол</summary>
  protected override void LoadData()
  {
    this._canEdit = false;
    if (this._objID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objID, false);
        if (dbObject != null)
          this._canEdit = dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.CheckoutBy == sessionKeeper.Session.UserID;
      }
    }
    this._procRouteEntryObject.ObjectId = this._objID;
    if (this._objID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.procRouteEntryForArticleControl.StartLoadData(sessionKeeper.Session);
    }
    base.LoadData();
  }

  /// <summary>Сохранить информацию из контрола</summary>
  protected override void SaveData(bool sendNotifications = true)
  {
    if (!this.Modified)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._procRouteEntryObject.SaveData(sessionKeeper.Session);
      this.Modified = false;
    }
    if (sendNotifications)
      ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._objID));
    base.SaveData(sendNotifications);
  }

  /// <summary>Загрузка настроек</summary>
  protected override void LoadSettings()
  {
    base.LoadSettings();
    TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) new HybridDictionary(1));
  }

  /// <summary>Сохранение настроек</summary>
  protected override void SaveSettings()
  {
    base.SaveSettings();
    TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) new HybridDictionary(1));
  }

  /// <summary>OrderID</summary>
  public override int OrderID => 0;

  /// <summary>Can modifying flag</summary>
  public override bool CanModify => base.CanModify && this._canEdit;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcRouteEntryArticleView));
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.ctsmiEdit = new ToolStripMenuItem();
    this.pnlClient = new Panel();
    this.procRouteEntryForArticleControl = new ProcRouteEntryForArticleControl();
    this.pnButtons.SuspendLayout();
    this.contextMenuStrip.SuspendLayout();
    this.pnlClient.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.ctsmiEdit
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.ctsmiEdit.Name = "ctsmiEdit";
    componentResourceManager.ApplyResources((object) this.ctsmiEdit, "ctsmiEdit");
    this.pnlClient.Controls.Add((Control) this.procRouteEntryForArticleControl);
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    this.procRouteEntryForArticleControl.ArticleObjectItems = (List<ObjInfoIDItem>) null;
    componentResourceManager.ApplyResources((object) this.procRouteEntryForArticleControl, "procRouteEntryForArticleControl");
    this.procRouteEntryForArticleControl.Name = "procRouteEntryForArticleControl";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Name = nameof (ProcRouteEntryArticleView);
    this.Controls.SetChildIndex((Control) this.pnlClient, 0);
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.pnButtons.ResumeLayout(false);
    this.contextMenuStrip.ResumeLayout(false);
    this.pnlClient.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}

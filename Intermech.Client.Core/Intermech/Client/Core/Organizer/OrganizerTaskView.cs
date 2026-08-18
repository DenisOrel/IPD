
// Type: Intermech.Client.Core.Organizer.OrganizerTaskView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Контрол для закладки с формой для объекта типа "Задачи органайзера".
/// </summary>
[ToolboxItem(false)]
[ViewDescriptionProvider(typeof (OrganizerTaskView.OrganizerTaskViewDescriptionProvider))]
public class OrganizerTaskView : UserControl, IView, ICanCloseViews, ICanDeactivateView
{
  private string _caption = string.Empty;
  private int _imgIndex = -1;
  private OrganizerTaskCtrl _ctrl;
  private long _objID;
  private long _relID;
  private string _saveDlgMsg = string.Empty;
  private string _saveDlgCaption = string.Empty;
  private INotificationService _notificationService;
  private NotificationEventHandler _notificationHandler;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnOK;

  /// <summary>Конструктор.</summary>
  public OrganizerTaskView()
  {
    this.InitializeComponent();
    this._caption = LocalizationHolder.rm.GetString("Client.Core.ObjectsForm");
    this._saveDlgCaption = LocalizationHolder.rm.GetString("Client.Core_541");
    this._saveDlgMsg = LocalizationHolder.rm.GetString("Client.Core_134");
    this._ctrl = new OrganizerTaskCtrl(0L);
    this._ctrl.Dock = DockStyle.Fill;
    this._ctrl.Visible = false;
    this._ctrl.Modified += new EventHandler(this.On_ctrl_Modified);
    Panel panel = new Panel();
    panel.Dock = DockStyle.Fill;
    panel.Controls.Add((Control) this._ctrl);
    this.Controls.Add((Control) panel);
    if (!(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this._imgIndex = service.ImageIndex("imgCard");
  }

  /// <summary>Отмена изменений.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnCancel_Click(object sender, EventArgs e)
  {
    this._ctrl.Refresh();
    this._pnlBottom.Enabled = false;
  }

  /// <summary>Сохренение изменений.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnOK_Click(object sender, EventArgs e) => this.Save();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_ctrl_Modified(object sender, EventArgs e) => this._pnlBottom.Enabled = true;

  /// <summary>Инициализация закладки.</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    this._objID = itemData.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long id = sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID).ID;
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
      IDBRelation relation = sessionKeeper.Session.GetRelation(this._objID, id, relationTypeId);
      if (relation != null)
        this._relID = relation.RelationID;
    }
    this.InitServices();
  }

  /// <summary>Активация закладки.</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    this._ctrl.Refresh(this._objID);
    this._pnlBottom.Enabled = false;
    this._ctrl.Visible = true;
  }

  /// <summary>Деактивация закладки.</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (!this._ctrl.IsChanged)
      return;
    if (MessageBox.Show(this._saveDlgMsg, this._saveDlgCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      this._ctrl.ResetChanges();
    else
      this.Save();
  }

  /// <summary>Наименование закладки.</summary>
  public string Caption => this._caption;

  /// <summary>Индекс изображения закладки.</summary>
  public int ImageIndex => this._imgIndex;

  /// <summary>Порядковый номер закладки.</summary>
  public int OrderID => 0;

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки.
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public bool CanClose(object sender)
  {
    if (this._ctrl.IsChanged)
    {
      switch (MessageBox.Show(this._saveDlgMsg, this._saveDlgCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
      {
        case DialogResult.Cancel:
          return false;
        case DialogResult.No:
          this._ctrl.ResetChanges();
          return true;
        default:
          this.Save();
          break;
      }
    }
    return true;
  }

  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку.
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>Событие от глобальной службы уведомлений.</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (sender as OrganizerTaskView == this || e == null)
      return;
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    DBRelationsEventArgs relationsEventArgs = e as DBRelationsEventArgs;
    bool flag = false;
    if (objectsEventArgs != null && objectsEventArgs.ObjectIDs != null && objectsEventArgs.ObjectIDs.Count > 0)
    {
      foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
      {
        switch (objectId)
        {
          case -1:
          case 0:
            continue;
          default:
            if (objectId == this._objID)
            {
              flag = true;
              goto label_22;
            }
            continue;
        }
      }
    }
    else if (relationsEventArgs != null && relationsEventArgs.RelationIDs != null && relationsEventArgs.RelationIDs.Count > 0)
    {
      foreach (long relationId in (IEnumerable<long>) relationsEventArgs.RelationIDs)
      {
        if (relationId != 0L && relationId == this._relID)
        {
          flag = true;
          break;
        }
      }
    }
label_22:
    if (!flag)
      return;
    switch (e.EventName)
    {
      case "RelationsChanged":
      case "ObjectsChanged":
        this._ctrl.Refresh(this._objID);
        break;
      case "ObjectsRemoved":
        this._ctrl.Refresh(0L);
        break;
    }
  }

  /// <summary>Сохранение данных.</summary>
  private void Save()
  {
    this._ctrl.Save();
    this._pnlBottom.Enabled = false;
    if (this._notificationService == null)
      return;
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._objID));
  }

  /// <summary>Выполнить инициализацию сервисов закладки.</summary>
  protected virtual void InitServices()
  {
    if (this._notificationService != null)
      return;
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._notificationHandler != null || this._notificationService == null)
      return;
    this._notificationHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
    this._notificationService.Subscribe(this._notificationHandler);
  }

  /// <summary>Выполнить деинициализацию сервисов закладки.</summary>
  protected virtual void ReleaseServices()
  {
    if (this._notificationService == null)
      return;
    if (this._notificationHandler != null && this._notificationService != null)
      this._notificationService.Unsubscribe(this._notificationHandler);
    this._notificationService = (INotificationService) null;
    this._notificationHandler = (NotificationEventHandler) null;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    this.ReleaseServices();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrganizerTaskView));
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Click += new EventHandler(this.On_btnCancel_Click);
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.Name = "_btnOK";
    this._btnOK.Click += new EventHandler(this.On_btnOK_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.MinimumSize = new Size(300, 150);
    this.Name = nameof (OrganizerTaskView);
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class OrganizerTaskViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core.ObjectsForm"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgCard") : -1,
        OrderID = 0
      };
    }
  }
}

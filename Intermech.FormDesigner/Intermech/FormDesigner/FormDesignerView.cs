// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerView
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Вьюшка для редактора форм.</summary>
[ViewDescriptionProvider(typeof (FormDesignerView.FormDesignerViewDescriptionProvider))]
public class FormDesignerView : UserControl, IView, ICanCloseViews, ICanDeactivateView
{
  private bool _reload = true;
  /// <summary>
  /// Контейнер сервисов (контекст) для выделенных элементов пространства навигации
  /// </summary>
  internal System.IServiceProvider _services;
  private FormDesignerControl _control;
  [NonSerialized]
  private DockControl _dockCtrl;
  private bool _checkedChanges;

  /// <summary>
  /// 
  /// </summary>
  private DockControl FindParentDock
  {
    get
    {
      findParentDock = (DockControl) null;
      Control control = (Control) this;
      while (true)
      {
        switch (control)
        {
          case null:
          case DockControl findParentDock:
            goto label_3;
          default:
            control = control.Parent;
            continue;
        }
      }
label_3:
      return findParentDock;
    }
  }

  /// <summary>Конструктор.</summary>
  public FormDesignerView()
  {
    this.InitializeComponent();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this.ImageIndex = service != null ? service.ImageIndex("imgNewWindow") : 0;
    FormDesignerControl formDesignerControl = new FormDesignerControl();
    formDesignerControl.Dock = DockStyle.Fill;
    this._control = formDesignerControl;
    this.Controls.Add((Control) this._control);
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.Subscribe(BeforeFormObjectCheckinEventArgs.BeforeFormObjectCheckinEvent, new NotificationEventHandler(this.BeforeFormObjectCheckin));
  }

  private void BeforeFormObjectCheckin(object sender, NotificationEventArgs e)
  {
    if (!(e is BeforeFormObjectCheckinEventArgs checkinEventArgs) || checkinEventArgs.FormObjectId != this._control.FormID)
      return;
    this.ChackedChanges(false);
  }

  /// <summary>Индекс прицепленной картинки.</summary>
  public int ImageIndex { get; private set; }

  /// <summary>Порядок сортировка закладок.</summary>
  public int OrderID => 7;

  /// <summary>Заголовок вьюшки.</summary>
  public string Caption => LocalizationHolder.rm.GetString("FormDesigner_114");

  /// <summary>Инициализация вьюшки.</summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._services = services;
    long num = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    long formId = this._control.FormID;
    this._reload = true;
    this._control.FormID = num;
  }

  /// <summary>Активация вьюшки.</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    this._checkedChanges = false;
    bool readOnly = this._services.GetService(typeof (IViewState)) is IViewState service && (service.ViewState & ViewStateFlags.ReadOnly) == ViewStateFlags.ReadOnly;
    if (this._reload)
    {
      if (!readOnly)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._control.FormID, false);
          readOnly = objectActualCopy.ReadOnly || objectActualCopy.CheckoutBy != sessionKeeper.Session.UserID;
        }
      }
      this._control.Activate(readOnly);
    }
    else if (readOnly && !this._control.ReadOnly)
      this._control.ViewStateReadOnlyChanged(true);
    this._dockCtrl = this.FindParentDock;
    if (this._dockCtrl != null)
      this._dockCtrl.Closing += new CancelEventHandler(this.OndockCtrl_Closing);
    this._reload = false;
  }

  /// <summary>Деактивация вьюшки.</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (!this._checkedChanges)
      this.ChackedChanges(false);
    this._control.Deactivate();
    if (this._dockCtrl == null)
      return;
    this._dockCtrl.Closing -= new CancelEventHandler(this.OndockCtrl_Closing);
  }

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки.
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладка разрешает закрытие формы, false - закладка запрещает закрытие формы</returns>
  public bool CanClose(object sender) => this.ChackedChanges(true);

  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку.
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладку можно деактивировать, false - закладку нельзя деактивировать</returns>
  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OndockCtrl_Closing(object sender, CancelEventArgs e)
  {
    if (!this.ChackedChanges(true))
      e.Cancel = true;
    else
      this.PrepareControlToClose();
  }

  private void PrepareControlToClose()
  {
    if (this._control == null)
      return;
    this._control.PrepareToClose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="canCancel"></param>
  /// <returns></returns>
  private bool ChackedChanges(bool canCancel)
  {
    bool flag1 = true;
    if (this._control.Modified)
    {
      bool flag2 = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(Math.Abs(this._control.FormID), false);
        if (dbObject == null)
          return true;
        flag2 = dbObject.CheckoutBy == sessionKeeper.Session.UserID;
      }
      if (flag2)
      {
        string str = LocalizationHolder.rm.GetString("FormDesigner_116");
        string text = LocalizationHolder.rm.GetString("FormDesigner_115");
        MessageBoxButtons messageBoxButtons = canCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
        string caption = str;
        int buttons = (int) messageBoxButtons;
        switch (MessageBox.Show(text, caption, (MessageBoxButtons) buttons, MessageBoxIcon.Question))
        {
          case DialogResult.Cancel:
            flag1 = false;
            goto label_15;
          case DialogResult.Yes:
            this._control.Commit();
            break;
          default:
            this._control.Rollback();
            break;
        }
        this._checkedChanges = true;
      }
      else
      {
        this._control.FormID = Math.Abs(this._control.FormID);
        this._control.Rollback();
        this._checkedChanges = true;
      }
    }
label_15:
    return flag1;
  }

  /// <summary>
  /// Используется при вызове формы из Workflow, запрещает команды "Авторазмещение" и "Связать форму с".
  /// </summary>
  public bool AllowLinkingObjects
  {
    set => this._control.IsWorkflowForm = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.Unsubscribe(BeforeFormObjectCheckinEventArgs.BeforeFormObjectCheckinEvent, new NotificationEventHandler(this.BeforeFormObjectCheckin));
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDesignerView));
    this.SuspendLayout();
    this.DoubleBuffered = true;
    this.Name = nameof (FormDesignerView);
    this.ResumeLayout(false);
  }

  private sealed class FormDesignerViewDescriptionProvider : BaseViewDescriptionProvider
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
        Caption = LocalizationHolder.rm.GetString("FormDesigner_114"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgNewWindow") : 0,
        OrderID = 7
      };
    }
  }
}

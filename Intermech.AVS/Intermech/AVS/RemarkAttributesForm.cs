// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RemarkAttributesForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.UI.Winforms;
using Intermech.VirtualTreeView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Редактор списка атрибутов, отображаемых в примечаниях спецификации
/// </summary>
/// <summary>
/// Редактор списка атрибутов, отображаемых в примечаниях спецификации
/// </summary>
public class RemarkAttributesForm : Form
{
  /// <summary>Идентификатор версии объекта, в котором хранятся настройки</summary>
  protected long settingsObjectID;
  /// <summary>Идентификатор атрибута, в котором хранятся настройки</summary>
  protected int settingsAttributeID;
  /// <summary>Корневой узел в дереве всех атрибутов</summary>
  protected List<object> rootItem1 = new List<object>();
  /// <summary>
  /// Корневой узел в дереве редактируемого списка атрибутов
  /// </summary>
  protected List<object> rootItem2 = new List<object>();
  /// <summary>Список допустимых атрибутов для текущего типа связи</summary>
  protected List<RemarkAttribute> allRelAttrs = new List<RemarkAttribute>();
  /// <summary>
  /// Список допустимых атрибутов для допустимых дочерних типов объектов
  /// </summary>
  protected List<RemarkAttribute> allObjAttrs = new List<RemarkAttribute>();
  /// <summary>Список с названиями групп</summary>
  protected List<string> groups = new List<string>();
  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по умолчанию)
  /// 1 - на закладке "Навигатора"
  /// </summary>
  public int ParentMode;
  /// <summary>
  /// Для особых случаев надо запретить и спрятать кнопки "Применить" и "Отмена"
  /// </summary>
  public bool HideApplyCancel;
  /// <summary>Есть ли изменения в редакторе</summary>
  public bool isChanged;
  /// <summary>
  /// Флажок выставляется, когда требуется запретить обработчики событий
  /// </summary>
  protected bool isInEvents;
  /// <summary>Работает ли редактор в режиме "Только просмотр"</summary>
  public bool isReadOnly;
  /// <summary>Параметры формы (исходные данные)</summary>
  public AttributesListFormParams FormParams;
  /// <summary>Результат работы формы</summary>
  public AttributesListFormParams FormResult;
  /// <summary>
  /// Флажок будет установлен в true, если пользователь возьмёт объект на редактирование нажатием кнопки
  /// </summary>
  public bool AutoCheckedOut;
  /// <summary>Коллекция изображений для разных категорий и типов</summary>
  protected ICategoryTypeIconService iconsService;
  /// <summary>Служба уведомлений</summary>
  protected INotificationService notificationSvc;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  protected NotificationEventHandler notifyHandler;
  /// <summary>Обработчик событий от Bars</summary>
  protected EventHandler barEventsHandler;
  /// <summary>Список атрибутов</summary>
  protected string htmlAttributesList = string.Empty;
  /// <summary>Список атрибутов пуст</summary>
  protected string htmlEmptyAttributesList = string.Empty;
  private CheckBox cbBch = new CheckBox();
  private bool readOnlyForm;
  /// <summary>Контейнер компонентов</summary>
  private IContainer components;
  protected Panel panelInfo;
  private PictureBox pictureInfo;
  protected Panel panelBottom;
  private Button btnCancel;
  private Button btnApply;
  private SplitContainer splitContainer;
  protected Panel panelPreview;
  private Label labelPreview;
  protected Panel panelBottomLeft;
  protected Panel panelBottomRight;
  private ImageList imagesToolbars;
  private Intermech.VirtualTreeView.VirtualTreeView treeAttributes;
  protected Column columnAllAttributes;
  private Intermech.Bars.ToolBar toolBarRight;
  private ButtonItem btnAttrAdd;
  private ButtonItem btnAttrDelete;
  private ButtonItem btnRefresh;
  private Intermech.VirtualTreeView.VirtualTreeView treeRemarkAttributes;
  protected Column columnRemarkAttributes;
  private Intermech.Bars.ToolBar toolBarAttributes;
  private ButtonItem btnMoveUp;
  private ButtonItem btnMoveDown;
  private ButtonItem btnMoveTop;
  private ButtonItem btnMoveBottom;
  protected Column columnSeparators;
  private Button btnCheckOut;
  private CheckBox cbShowAllAttributes;
  private CheckBox cbShowMeasureUnits;
  private Button btnDefault;
  private ToolTip toolTips;
  private TextBox textInfo;
  protected CellEditor editSeparator;
  protected ComboBox comboSeparator;
  private MenuBar menuAvailable;
  private ContextMenuBarItem contextMenuAvailable;
  private MenuButtonItem mnpAttrAdd;
  private MenuButtonItem mnpRefresh;
  private MenuBar menuRemarks;
  private ContextMenuBarItem contextMenuRemarks;
  private MenuButtonItem mnpAttrDelete;
  private MenuButtonItem mnpMoveUp;
  private MenuButtonItem mnpMoveDown;
  private MenuButtonItem mnpMoveTop;
  private MenuButtonItem mnpMoveBottom;
  private Timer timerDblClick;
  private Timer timerDblClickAdd;
  private Panel panelHTML;
  private WebBrowser edPreview;
  private ButtonItem btnWithoutDrawing;
  private MenuButtonItem mnpWithoutDrawing;
  private Column columnBch;
  private CellEditor editBch;

  /// <summary>Конструктор</summary>
  public RemarkAttributesForm() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  /// <param name="settingsObjectID">Идентификатор версии объекта, в котором хранятся настройки</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута, в котором хранятся настройки</param>
  /// <param name="formParams">Параметры для вызова формы</param>
  /// <param name="parentMode">Где размещена наша форма:
  /// 0 - самостоятельная форма (по умолчанию),
  /// 1 - на закладке "Навигатора"</param>
  public RemarkAttributesForm(
    long settingsObjectID,
    int settingsAttributeID,
    AttributesListFormParams formParams,
    int parentMode,
    bool readOnly)
  {
    this.settingsObjectID = settingsObjectID;
    this.settingsAttributeID = settingsAttributeID;
    this.InitializeComponent();
    this.cbBch = new CheckBox();
    this.cbBch.Padding = new Padding(10, 0, 0, 0);
    this.editBch.Control = (Control) this.cbBch;
    this.editBch.CellAlignment = ContentAlignment.TopRight;
    this.ReadOnlyForm = readOnly;
    if (this.IsDesignerHosted())
      return;
    this.Init(formParams, parentMode);
  }

  public virtual bool ReadOnlyForm
  {
    get => this.readOnlyForm;
    set => this.readOnlyForm = value;
  }

  /// <summary>
  /// Свойство позволяет узнать, можно ли выполнять редактирование списка атрибутов
  /// </summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Свойство позволяет узнать, можно ли выполнять редактирование списка атрибутов")]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.isReadOnly | this.readOnlyForm;
    [DebuggerStepThrough] set
    {
      this.isReadOnly = value;
      this.UpdateControls();
      this.RaiseOnChanged();
    }
  }

  /// <summary>
  /// Свойство позволяет узнать, были ли изменения в списке атрибутов
  /// </summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Свойство позволяет узнать, были ли изменения в списке атрибутов")]
  public virtual bool IsChanged
  {
    [DebuggerStepThrough] get => this.isChanged;
    [DebuggerStepThrough] set
    {
      this.isChanged = value;
      this.UpdateControls();
      this.RaiseOnChanged();
    }
  }

  /// <summary>
  /// Событие возникает, если в редакторе списка атрибутов происходят изменения
  /// </summary>
  [Description("Событие возникает, если в редакторе списка атрибутов происходят изменения")]
  public event RemarkAttributesChangedEventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  protected virtual void RaiseOnChanged()
  {
    RemarkAttributesChangedEventHandler onChanged = this.OnChanged;
    if (onChanged == null)
      return;
    onChanged((object) this, new RemarkAttributesEventArgs(this.FormResult));
  }

  /// <summary>
  /// Событие возникает, если в редакторе списка атрибутов нажата кнопка "Применить"
  /// </summary>
  [Description("Событие возникает, если в редакторе списка атрибутов нажата кнопка \"Применить\"")]
  public event RemarkAttributesChangedEventHandler OnApplyPressed;

  /// <summary>Сгенерировать событие "OnApplyPressed"</summary>
  protected virtual void RaiseOnApplyPressed()
  {
    RemarkAttributesChangedEventHandler onApplyPressed = this.OnApplyPressed;
    if (onApplyPressed == null)
      return;
    onApplyPressed((object) this, new RemarkAttributesEventArgs(this.FormResult));
  }

  /// <summary>
  /// Событие возникает, если в редакторе списка атрибутов нажата кнопка "Отменить"
  /// </summary>
  [Description("Событие возникает, если в редакторе списка атрибутов нажата кнопка \"Отменить\"")]
  public event RemarkAttributesChangedEventHandler OnCancelPressed;

  /// <summary>Сгенерировать событие "OnCancelPressed"</summary>
  protected virtual void RaiseOnCancelPressed()
  {
    RemarkAttributesChangedEventHandler onCancelPressed = this.OnCancelPressed;
    if (onCancelPressed == null)
      return;
    onCancelPressed((object) this, new RemarkAttributesEventArgs(this.FormResult));
  }

  /// <summary>Инициализировать форму</summary>
  /// <param name="formParams">Параметры для вызова формы</param>
  /// <param name="parentMode">Где размещена наша форма:
  /// 0 - самостоятельная форма (по умолчанию),
  /// 1 - на закладке "Навигатора"</param>
  public void Init(AttributesListFormParams formParams, int parentMode)
  {
    Guid settingsObjectGuid = Guid.Empty;
    if (this.settingsObjectID != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        settingsObjectGuid = sessionKeeper.Session.GetObjectInfo(this.settingsObjectID).VersionGuid;
    }
    this.FormParams = formParams != null ? formParams : new AttributesListFormParams(NoteFieldSettings.GetDefaultAttributes(settingsObjectGuid));
    this.FormResult = this.FormParams is RemarkAttributesFormParams ? (AttributesListFormParams) new RemarkAttributesFormParams(this.FormParams as RemarkAttributesFormParams) : new AttributesListFormParams(this.FormParams);
    if (parentMode == 0 && this.notifyHandler == null)
    {
      Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
      this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
      int width1 = workingArea.Width;
      Size size = this.Size;
      int width2 = size.Width;
      int x = (width1 - width2) / 2;
      int height1 = workingArea.Height;
      size = this.Size;
      int height2 = size.Height;
      int y = (height1 - height2) / 2;
      this.Location = new Point(x, y);
      FormStorage.LoadLayout((Control) this);
    }
    this.ParentMode = parentMode;
    this.btnApply.DialogResult = this.ParentMode == 0 ? DialogResult.OK : DialogResult.None;
    this.btnCancel.DialogResult = this.ParentMode == 0 ? DialogResult.Cancel : DialogResult.None;
    this.CancelButton = this.ParentMode == 0 ? (IButtonControl) this.btnCancel : (IButtonControl) null;
    switch (this.ParentMode)
    {
      case 0:
        this.btnApply.Text = "ОК";
        break;
      case 1:
        this.btnApply.Text = "Применить";
        break;
    }
    this.UpdateControls();
    this.notificationSvc = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this.iconsService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    if (this.notificationSvc != null && this.notifyHandler == null && this.FormParams is RemarkAttributesFormParams)
    {
      this.notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this.notificationSvc.Subscribe(this.notifyHandler);
    }
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service && this.barEventsHandler == null)
    {
      this.barEventsHandler = new EventHandler(this.ToolbarRendererChanged);
      service.RendererChanged += this.barEventsHandler;
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.isReadOnly = false;
    this.IsChanged = false;
    if (this.FormParams is RemarkAttributesFormParams)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.settingsObjectID, true);
        this.settingsObjectID = objectActual.ObjectID;
        IDBAttribute attributeById = objectActual.GetAttributeByID(this.settingsAttributeID);
        this.isReadOnly = objectActual.ObjectModifyMode != ObjectModifyModes.InBase && (objectActual.ObjectModifyMode != ObjectModifyModes.Checkout || objectActual.CheckoutBy != sessionKeeper.Session.UserID) || attributeById == null || attributeById.ReadOnly;
        if (this.isReadOnly)
        {
          if (objectActual.ObjectModifyMode == ObjectModifyModes.Checkout && objectActual.CheckoutBy != sessionKeeper.Session.UserID && objectActual.CheckoutBy != 0L)
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectActual.CheckoutBy);
            this.textInfo.Text = $"Редактировать список атрибутов и изменять параметры нельзя. Объект \"{objectActual.Caption}\" взят на редактирование пользователем \"{objectInfo.Caption}\".";
            this.textInfo.SelectionLength = 0;
          }
          else
          {
            this.textInfo.Text = $"Редактировать список атрибутов и изменять параметры нельзя. Возможно, следует взять объект \"{objectActual.Caption}\" на редактирование.";
            this.textInfo.SelectionLength = 0;
          }
        }
      }
    }
    if (this.FormParams is RemarkAttributesFormParams)
    {
      this.Text = "Редактор списка атрибутов, отображаемых в примечаниях спецификаций";
      try
      {
        this.isInEvents = true;
        this.cbShowMeasureUnits.Checked = ((this.FormParams as RemarkAttributesFormParams).Options & NoteFieldOptions.ShowMeasureUnits) > NoteFieldOptions.None;
      }
      finally
      {
        this.isInEvents = false;
      }
    }
    this.PrepareGroups();
    this.rootItem2.Clear();
    this.rootItem2.Add((object) this.FormResult.Items);
    this.FillTree(this.treeAttributes, (object) this.rootItem1, true);
    this.FillTree(this.treeRemarkAttributes, (object) this.rootItem2, true);
    if (this.readOnlyForm)
      this.isReadOnly = this.readOnlyForm;
    Stream manifestResourceStream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Intermech.AVS.Resources.HTML.AttributesList.html");
    if (manifestResourceStream1 != null)
    {
      byte[] numArray = new byte[manifestResourceStream1.Length];
      manifestResourceStream1.Read(numArray, 0, numArray.Length);
      this.htmlAttributesList = Encoding.UTF8.GetString(numArray);
      manifestResourceStream1.Close();
    }
    Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Intermech.AVS.Resources.HTML.EmptyAttributesList.html");
    if (manifestResourceStream2 != null)
    {
      byte[] numArray = new byte[manifestResourceStream2.Length];
      manifestResourceStream2.Read(numArray, 0, numArray.Length);
      this.htmlEmptyAttributesList = Encoding.UTF8.GetString(numArray);
      manifestResourceStream2.Close();
    }
    this.UpdateControls();
    this.UpdatePreviewString();
  }

  /// <summary>Обновить контролы в форме</summary>
  protected virtual void UpdateControls()
  {
    this.btnApply.Enabled = !this.isReadOnly && this.IsChanged;
    this.btnCancel.Enabled = !this.isReadOnly && this.IsChanged || this.ParentMode == 0;
    this.btnDefault.Enabled = !this.isReadOnly;
    this.btnCheckOut.Enabled = this.isReadOnly && !this.readOnlyForm;
    this.panelBottom.Visible = !this.HideApplyCancel;
    this.panelInfo.Visible = this.isReadOnly && this.FormParams is RemarkAttributesFormParams;
    this.cbShowMeasureUnits.Enabled = !this.isReadOnly && this.FormParams is RemarkAttributesFormParams;
    this.cbShowMeasureUnits.Visible = this.FormParams is RemarkAttributesFormParams;
    this.btnRefresh.Enabled = true;
    this.mnpRefresh.Enabled = this.btnRefresh.Enabled;
    EnabledAttributesActions attributesActions1 = this.EnabledActions_Acceptable();
    EnabledAttributesActions attributesActions2 = this.EnabledActions_Remarks();
    this.btnAttrAdd.Enabled = !this.ReadOnly && (attributesActions1 & EnabledAttributesActions.CanAdd) > EnabledAttributesActions.None;
    this.mnpAttrAdd.Enabled = this.btnAttrAdd.Enabled;
    this.btnAttrDelete.Enabled = !this.ReadOnly && (attributesActions2 & EnabledAttributesActions.CanDelete) > EnabledAttributesActions.None;
    this.mnpAttrDelete.Enabled = this.btnAttrDelete.Enabled;
    this.btnMoveUp.Enabled = !this.ReadOnly && (attributesActions2 & EnabledAttributesActions.MoveUp) > EnabledAttributesActions.None;
    this.mnpMoveUp.Enabled = this.btnMoveUp.Enabled;
    this.btnMoveDown.Enabled = !this.ReadOnly && (attributesActions2 & EnabledAttributesActions.MoveDown) > EnabledAttributesActions.None;
    this.mnpMoveDown.Enabled = this.btnMoveDown.Enabled;
    this.btnMoveTop.Enabled = !this.ReadOnly && (attributesActions2 & EnabledAttributesActions.MoveTop) > EnabledAttributesActions.None;
    this.mnpMoveTop.Enabled = this.btnMoveTop.Enabled;
    this.btnMoveBottom.Enabled = !this.ReadOnly && (attributesActions2 & EnabledAttributesActions.MoveBottom) > EnabledAttributesActions.None;
    this.mnpMoveBottom.Enabled = this.btnMoveBottom.Enabled;
  }

  /// <summary>Подготовить список групп</summary>
  protected virtual void PrepareGroups()
  {
    this.groups.Clear();
    if (this.cbShowAllAttributes.Checked)
    {
      this.groups.Add("Все атрибуты типов связей");
      this.groups.Add("Все атрибуты типов объектов");
    }
    else
    {
      this.groups.Add("Атрибуты текущего типа связи");
      this.groups.Add("Атрибуты допустимых типов объектов");
    }
    this.rootItem1.Clear();
    this.rootItem1.Add((object) this.groups);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.allRelAttrs = NoteFieldSettings.GetRelTypeAttributes(sessionKeeper.Session, MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"), this.cbShowAllAttributes.Checked);
      this.allObjAttrs = this.cbShowAllAttributes.Checked ? NoteFieldSettings.GetAllAttributes(AttributeSourceTypes.Object) : NoteFieldSettings.GetSpecAcceptableAttributes(sessionKeeper.Session);
      for (int index = 0; index < this.FormResult.Items.Count; ++index)
      {
        this.allRelAttrs.Remove(this.FormResult.Items[index]);
        this.allObjAttrs.Remove(this.FormResult.Items[index]);
      }
    }
  }

  /// <summary>Вернуть значок для указанного атрибута</summary>
  /// <param name="attrType">Тип данных атрибута</param>
  /// <returns>Значок для указанного атрибута</returns>
  protected Icon GetAttrTypeIcon(FieldTypes attrType)
  {
    if (this.iconsService == null)
      return (Icon) null;
    int index = this.iconsService.IndexOf(3, -1, (object) attrType);
    return index < 0 ? (Icon) null : ImagesResizeHelper.ResizeIconTo32x16(this.iconsService.GetIndexIcon(index), Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
  }

  /// <summary>Сгенерировать строку с предварительным просмотром</summary>
  /// <returns>Строка с предварительным просмотром</returns>
  protected virtual string CreatePreviewString()
  {
    AttributesListFormParams attributesListFormParams = this.FormResult.Items.Count == 0 ? this.FormParams : this.FormResult;
    if (attributesListFormParams == null || attributesListFormParams.Items == null || attributesListFormParams.Items.Count == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.FormResult.Items.Count; ++index)
    {
      RemarkAttribute remarkAttribute = this.FormResult.Items[index];
      stringBuilder.Append($"[<a>{remarkAttribute.Name}</a>]");
      if (index != this.FormResult.Items.Count - 1)
      {
        if (remarkAttribute.Separator == "?")
          stringBuilder.Append("<br>");
        else if (remarkAttribute.Separator == "~")
        {
          stringBuilder.Append("&nbsp;");
        }
        else
        {
          string str = remarkAttribute.Separator.Replace("<", "&lt;").Replace(">", "&gt;");
          stringBuilder.Append(str);
        }
      }
    }
    return stringBuilder.ToString();
  }

  /// <summary>Обновить строку с предварительным просмотром</summary>
  protected virtual void UpdatePreviewString()
  {
    string previewString = this.CreatePreviewString();
    MemoryStream memoryStream = new MemoryStream();
    if (previewString == string.Empty)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(this.htmlEmptyAttributesList);
      memoryStream.Write(bytes, 0, bytes.Length);
    }
    else
    {
      byte[] bytes1 = Encoding.UTF8.GetBytes(this.htmlAttributesList);
      memoryStream.Write(bytes1, 0, bytes1.Length);
      byte[] bytes2 = Encoding.UTF8.GetBytes($"<body>{previewString}</body>");
      memoryStream.Write(bytes2, 0, bytes2.Length);
    }
    memoryStream.Seek(0L, SeekOrigin.Begin);
    this.edPreview.DocumentStream = (Stream) memoryStream;
  }

  /// <summary>Вызвать редактор для изменения системного списка атрибутов, отображаемых в примечаниях спецификаций</summary>
  /// <param name="settingsObjectID">Идентификатор версии объекта, в котором хранятся настройки</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута, в котором хранятся настройки</param>
  /// <returns>Результаты вызова формы</returns>
  public static DialogResult Execute(long settingsObjectID, int settingsAttributeID, bool readOnly = false)
  {
    NoteFieldSettings noteFieldSettings = new NoteFieldSettings();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      settingsObjectID = sessionKeeper.Session.GetObjectActual(settingsObjectID, true).ObjectID;
      noteFieldSettings.LoadFromDBObjectAttribute(settingsObjectID, settingsAttributeID, sessionKeeper.Session);
    }
    RemarkAttributesFormParams formParams = new RemarkAttributesFormParams(noteFieldSettings.Items, noteFieldSettings.Options);
    using (RemarkAttributesForm sender = new RemarkAttributesForm(settingsObjectID, settingsAttributeID, (AttributesListFormParams) formParams, 0, readOnly))
    {
      DialogResult dialogResult = sender.ShowDialog();
      if (dialogResult != DialogResult.OK)
      {
        if (sender.AutoCheckedOut)
          sender.CancelChangesMainSpecTemplatePressed();
        return dialogResult;
      }
      AttributesListFormParams.CopyTo(sender.FormResult.Items, noteFieldSettings.Items);
      noteFieldSettings.Options = (sender.FormResult as RemarkAttributesFormParams).Options;
      int objectType = -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        noteFieldSettings.SaveToDBObjectAttribute(sender.settingsObjectID, settingsAttributeID, sessionKeeper.Session);
        objectType = sessionKeeper.Session.GetObjectInfo(sender.settingsObjectID).ObjectTypeID;
      }
      if (sender.AutoCheckedOut)
        sender.CheckInSettingsObject();
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        service.FireEvent((object) sender, (NotificationEventArgs) new DBObjectsExtendedEventArgs(sender.settingsObjectID, objectType, new AttributeValues(AvsIDCache.Attr_NoteFieldSettings, (object) null), new AttributeValues(AvsIDCache.Attr_NoteFieldSettings, (object) null)));
      return dialogResult;
    }
  }

  /// <summary>
  /// Вызвать редактор для изменения указанного списка атрибутов
  /// </summary>
  /// <param name="settingsObjectID">Идентификатор версии объекта, в котором хранятся настройки</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута, в котором хранятся настройки</param>
  /// <param name="formParams"></param>
  /// <returns>Результаты вызова формы</returns>
  public static DialogResult Execute(
    long settingsObjectID,
    int settingsAttributeID,
    AttributesListFormParams formParams,
    bool readOnly = false)
  {
    using (RemarkAttributesForm remarkAttributesForm = new RemarkAttributesForm(settingsObjectID, settingsAttributeID, formParams, 0, readOnly))
    {
      DialogResult dialogResult = remarkAttributesForm.ShowDialog();
      if (dialogResult != DialogResult.OK)
        return dialogResult;
      formParams.Assign(remarkAttributesForm.FormResult);
      return dialogResult;
    }
  }

  /// <summary>Корректно назначить контрол-предок для формы</summary>
  /// <param name="aParent">Родительский элемент управления</param>
  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
    this.UpdateControls();
  }

  /// <summary>
  /// Получить список действий, разрешённых над текущей коллекцией выделенных узлов
  /// в дереве допустимых атрибутов
  /// </summary>
  /// <returns>Список действий, разрешённых над текущей коллекцией выделенных узлов
  /// в дереве допустимых атрибутов</returns>
  protected virtual EnabledAttributesActions EnabledActions_Acceptable()
  {
    EnabledAttributesActions enabledAction;
    this.GetSelectedItems(this.treeAttributes, out enabledAction);
    return enabledAction;
  }

  /// <summary>
  /// Получить список действий, разрешённых над текущей коллекцией выделенных узлов
  /// в дереве выбранных атрибутов
  /// </summary>
  /// <returns>Список действий, разрешённых над текущей коллекцией выделенных узлов
  /// в дереве выбранных атрибутов</returns>
  protected virtual EnabledAttributesActions EnabledActions_Remarks()
  {
    EnabledAttributesActions enabledAction;
    this.GetSelectedItems(this.treeRemarkAttributes, out enabledAction);
    return enabledAction;
  }

  /// <summary>Получить список выделенных атрибутов</summary>
  /// <param name="enabledAction">Перечень допустимых операций над указанной коллекцией узлов</param>
  /// <returns>Список выделенных узлов дерева</returns>
  protected virtual List<RemarkAttribute> GetSelectedRemarkAttributes()
  {
    List<RemarkAttribute> remarkAttributes = new List<RemarkAttribute>();
    if (this.treeRemarkAttributes == null || this.treeRemarkAttributes.SelectedRows.Count == 0)
      return remarkAttributes;
    Row parentRow = this.treeRemarkAttributes.SelectedRows[0].ParentRow;
    for (int index = 0; index < this.treeRemarkAttributes.SelectedRows.Count; ++index)
    {
      if (this.treeRemarkAttributes.SelectedRows[index].Item is RemarkAttribute)
        remarkAttributes.Add(this.treeRemarkAttributes.SelectedRows[index].Item as RemarkAttribute);
    }
    return remarkAttributes;
  }

  /// <summary>Получить список выделенных узлов дерева</summary>
  /// <param name="enabledAction">Перечень допустимых операций над указанной коллекцией узлов</param>
  /// <returns>Список выделенных узлов дерева</returns>
  protected virtual List<Row> GetSelectedItems(
    Intermech.VirtualTreeView.VirtualTreeView tree,
    out EnabledAttributesActions enabledAction)
  {
    enabledAction = EnabledAttributesActions.None;
    List<Row> selectedItems = new List<Row>();
    if (tree == null || tree.SelectedRows.Count == 0)
      return selectedItems;
    Row parentRow = tree.SelectedRows[0].ParentRow;
    for (int index = 0; index < tree.SelectedRows.Count; ++index)
    {
      if (tree.SelectedRows[index].Item is RemarkAttribute)
        selectedItems.Add(tree.SelectedRows[index]);
    }
    selectedItems.Sort((IComparer<Row>) new RowsComparer());
    if (this.ReadOnly || selectedItems.Count == 0)
      return selectedItems;
    int childIndex1 = selectedItems[0].ChildIndex;
    int childIndex2 = selectedItems[selectedItems.Count - 1].ChildIndex;
    if (tree == this.treeAttributes)
    {
      enabledAction |= EnabledAttributesActions.CanAdd;
    }
    else
    {
      enabledAction |= EnabledAttributesActions.CanDelete;
      if (parentRow != null && childIndex1 > 0)
        enabledAction = enabledAction | EnabledAttributesActions.MoveUp | EnabledAttributesActions.MoveTop;
      if (parentRow != null && childIndex2 < parentRow.NumChildren - 1)
        enabledAction = enabledAction | EnabledAttributesActions.MoveDown | EnabledAttributesActions.MoveBottom;
    }
    return selectedItems;
  }

  /// <summary>Заполнить дерево</summary>
  /// <param name="tree">Дерево</param>
  /// <param name="dataSource">Источник данных</param>
  /// <param name="resetDatasource">Переназначать источник данных</param>
  protected virtual void FillTree(Intermech.VirtualTreeView.VirtualTreeView tree, object dataSource, bool resetDatasource)
  {
    if (resetDatasource)
      tree.DataSource = dataSource;
    tree.UpdateRows(true);
    this.UpdateControls();
  }

  /// <summary>Двойной клик в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_DoubleClick(object sender, EventArgs e)
  {
    if (this.ReadOnly)
      return;
    this.timerDblClickAdd.Enabled = true;
  }

  /// <summary>
  /// Изменилась сфокусированная строка в дереве допустимых атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_FocusRowChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Получить данные для ячейки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Item is string)
    {
      e.CellData.Value = (object) (string) e.Row.Item;
    }
    else
    {
      if (!(e.Row.Item is RemarkAttribute))
        return;
      RemarkAttribute remarkAttribute = e.Row.Item as RemarkAttribute;
      e.CellData.Value = (object) remarkAttribute.Name;
    }
  }

  /// <summary>Получить дочерние элементы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is List<object>)
    {
      e.Children = (IList) ((e.Row.Item as List<object>)[0] as List<string>);
    }
    else
    {
      if (!(e.Row.Item is string))
        return;
      if (this.groups.IndexOf((string) e.Row.Item) == 0)
        e.Children = (IList) this.allRelAttrs;
      else
        e.Children = (IList) this.allObjAttrs;
    }
  }

  /// <summary>Получить данные для строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Item is string)
    {
      e.RowData.Icon = this.groups.IndexOf((string) e.Row.Item) != 0 ? ImagesResizeHelper.ResizeIconTo32x16(this.imagesToolbars.Images[8], Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue)) : ImagesResizeHelper.ResizeIconTo32x16(this.imagesToolbars.Images[7], Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
      e.RowData.IconSize = e.RowData.Icon != null ? e.RowData.Icon.Width : e.RowData.IconSize;
    }
    else
    {
      if (!(e.Row.Item is RemarkAttribute))
        return;
      RemarkAttribute remarkAttribute = e.Row.Item as RemarkAttribute;
      e.RowData.Icon = this.GetAttrTypeIcon(remarkAttribute.AttrType);
      e.RowData.IconSize = e.RowData.Icon != null ? e.RowData.Icon.Width : e.RowData.IconSize;
    }
  }

  /// <summary>
  /// Изменилась выделенная строка в дереве допустимых атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// Показать контекстное меню в дереве допустимых атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAttributes_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.contextMenuAvailable.Show((Control) this.treeAttributes, e.Location);
  }

  /// <summary>Событие, вызываемое перед показом редактора ячейки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_BeforeShowCellEdit(object sender, BeforeShowCellEditEventArgs e)
  {
    e.Cancel = this.ReadOnly;
  }

  /// <summary>Двойной клик в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_CellDoubleClick(object sender, EventArgs e)
  {
    if (this.ReadOnly)
      return;
    this.timerDblClick.Enabled = true;
  }

  /// <summary>
  /// Изменилась сфокусированная строка в дереве атрибутов примечаний
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_FocusRowChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Получить данные для ячейки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Column == this.columnBch && e.Row.Item is RemarkAttribute)
    {
      RemarkAttribute remarkAttribute = e.Row.Item as RemarkAttribute;
      e.CellData.Value = (object) remarkAttribute.WithoutDrawing;
    }
    if (e.Column == this.columnRemarkAttributes && e.Row.Item is RemarkAttribute)
    {
      RemarkAttribute remarkAttribute = e.Row.Item as RemarkAttribute;
      string name = remarkAttribute.Name;
      string str = remarkAttribute.AttrSource == AttributeSourceTypes.Relation ? " (связь)" : (remarkAttribute.AttrSource == AttributeSourceTypes.Object ? "(объект)" : "(графа)");
      e.CellData.Value = (object) $"{name} {str}";
    }
    else
    {
      if (e.Column != this.columnSeparators || !(e.Row.Item is RemarkAttribute))
        return;
      RemarkAttribute remarkAttribute = e.Row.Item as RemarkAttribute;
      e.CellData.Value = (object) NoteFieldSettings.GetSeparatorDescription(remarkAttribute.Separator);
    }
  }

  /// <summary>Получить дочерние узлы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (!(e.Row.Item is List<object>))
      return;
    e.Children = (IList) ((e.Row.Item as List<object>)[0] as List<RemarkAttribute>);
  }

  /// <summary>Получить данные для строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is RemarkAttribute))
      return;
    RemarkAttribute remarkAttribute = e.Row.Item as RemarkAttribute;
    e.RowData.Icon = this.GetAttrTypeIcon(remarkAttribute.AttrType);
    e.RowData.IconSize = e.RowData.Icon != null ? e.RowData.Icon.Width : e.RowData.IconSize;
  }

  /// <summary>
  /// Изменилась выделенная строка в дереве атрибутов примечаний
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Задать новое значение в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (this.ReadOnly)
    {
      e.Cancel = true;
    }
    else
    {
      if (e.Column == this.columnBch)
      {
        RemarkAttribute remarkAttribute = e.Row != null ? e.Row.Item as RemarkAttribute : (RemarkAttribute) null;
        if (remarkAttribute != null)
          remarkAttribute.WithoutDrawing = (bool) e.NewValue;
        this.IsChanged = true;
      }
      if (e.Column != this.columnSeparators)
        return;
      RemarkAttribute remarkAttribute1 = e.Row != null ? e.Row.Item as RemarkAttribute : (RemarkAttribute) null;
      if (remarkAttribute1 == null)
        return;
      string str = (string) e.NewValue;
      foreach (KeyValuePair<string, string> separatorDescriptor in NoteFieldSettings.SeparatorDescriptors)
      {
        if (separatorDescriptor.Value.Equals(str))
        {
          str = separatorDescriptor.Key;
          break;
        }
      }
      remarkAttribute1.Separator = str;
      this.UpdatePreviewString();
      this.IsChanged = true;
    }
  }

  /// <summary>
  /// Показать контекстное меню в дереве атрибутов примечаний
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeRemarkAttributes_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.contextMenuRemarks.Show((Control) this.treeRemarkAttributes, e.Location);
  }

  /// <summary>Нажата кнопка "Взять на редактирование"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCheckOutMainSpecTemplate(object sender, EventArgs e)
  {
    this.CheckOutMainSpecTemplatePressed();
  }

  /// <summary>Нажата кнопка "По умолчанию"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoSetDefaultValues(object sender, EventArgs e) => this.SetDefaultValuesPressed();

  /// <summary>Нажата кнопка "ОК"/"Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoOKApply(object sender, EventArgs e) => this.OKApplyPressed();

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e) => this.CancelPressed();

  /// <summary>Форма закрывается</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void RemarkAttributesForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Изменился флажок "Показать все атрибуты"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cbShowAllAttributes_CheckedChanged(object sender, EventArgs e)
  {
    this.PrepareGroups();
    this.rootItem2.Clear();
    this.rootItem2.Add((object) this.FormResult.Items);
    this.FillTree(this.treeAttributes, (object) this.rootItem1, true);
    this.FillTree(this.treeRemarkAttributes, (object) this.rootItem2, true);
  }

  /// <summary>Изменился флажок "Показывать единицы измерения"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cbShowMeasureUnits_CheckedChanged(object sender, EventArgs e)
  {
    if (this.ReadOnly || this.isInEvents || !(this.FormResult is RemarkAttributesFormParams formResult))
      return;
    if (this.cbShowMeasureUnits.Checked)
      formResult.Options |= NoteFieldOptions.ShowMeasureUnits;
    else
      formResult.Options &= ~NoteFieldOptions.ShowMeasureUnits;
    this.IsChanged = true;
  }

  /// <summary>
  /// Отображается контекстное меню в каком-либо из деревьев
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void BeforeMenusPopup(object sender, MenuPopupEventArgs e) => this.UpdateControls();

  /// <summary>
  /// Событие по таймеру для добавления атрибута в список по двойному клику мышью
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDelayedAttrAdd(object sender, EventArgs e)
  {
    this.timerDblClickAdd.Enabled = false;
    this.DoAttrAdd(sender, e);
  }

  /// <summary>
  /// Событие по таймеру для удаления атрибута из списка по двойному клику мышью
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDelayedAttrDelete(object sender, EventArgs e)
  {
    this.timerDblClick.Enabled = false;
    this.DoAttrDelete(sender, e);
  }

  /// <summary>
  /// Взять на изменение объект "Основной шаблон спецификаций"
  /// </summary>
  protected virtual void CheckOutMainSpecTemplatePressed()
  {
    this.UpdateControls();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.settingsObjectID, true);
      IDBAttribute attributeById = objectActual.GetAttributeByID(this.settingsAttributeID);
      if ((objectActual.ObjectModifyMode == ObjectModifyModes.InBase || objectActual.ObjectModifyMode == ObjectModifyModes.Checkout && objectActual.CheckoutBy == sessionKeeper.Session.UserID ? (attributeById == null ? 0 : (!attributeById.ReadOnly ? 1 : 0)) : 0) == 0 && objectActual.CheckoutBy != sessionKeeper.Session.UserID && objectActual.CheckoutBy != 0L)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectActual.CheckoutBy);
        int num = (int) MessageBox.Show($"Редактировать список атрибутов и изменять параметры нельзя. Объект \"{objectActual.Caption}\" взят на редактирование пользователем \"{objectInfo.Caption}\".", sc_900.ssp_avs_901(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        long objectId1 = objectActual.ObjectID;
        IDBObject dbObject = objectActual.CheckOut(true);
        this.settingsObjectID = dbObject.ObjectID;
        long objectId2 = dbObject.ObjectID;
        List<long> objectIDs = new List<long>(1);
        List<long> newObjectIDs = new List<long>(1);
        objectIDs.Add(objectId1);
        newObjectIDs.Add(objectId2);
        this.AutoCheckedOut = true;
        this.isReadOnly = false;
        this.UpdateControls();
        if (objectId1 == 0L)
          return;
        this.notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
      }
    }
  }

  /// <summary>
  /// Завершить изменения в объекте "Основной шаблон спецификаций"
  /// </summary>
  protected virtual void CheckInSettingsObject()
  {
    this.UpdateControls();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.settingsObjectID, true);
      long objectID = 0;
      if (objectActual.CheckoutBy == sessionKeeper.Session.UserID)
      {
        objectID = objectActual.ObjectID;
        objectActual.CheckIn();
      }
      this.settingsObjectID = objectActual.ObjectID;
      this.AutoCheckedOut = false;
      this.isReadOnly = true;
      this.UpdateControls();
      if (objectID == 0L)
        return;
      this.notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", objectID));
    }
  }

  /// <summary>
  /// Отменить изменения в объекте "Основной шаблон спецификаций"
  /// </summary>
  protected virtual void CancelChangesMainSpecTemplatePressed()
  {
    this.UpdateControls();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.settingsObjectID, true);
      long objectID = 0;
      if (objectActual.CheckoutBy == sessionKeeper.Session.UserID)
      {
        objectID = objectActual.ObjectID;
        objectActual.CancelChanges();
      }
      this.settingsObjectID = objectActual.ObjectID;
      this.AutoCheckedOut = false;
      this.isReadOnly = true;
      this.UpdateControls();
      if (objectID == 0L)
        return;
      this.notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", objectID));
    }
  }

  /// <summary>Нажата кнопка "По умолчанию"</summary>
  protected virtual void SetDefaultValuesPressed()
  {
    if (this.ReadOnly)
      return;
    Guid settingsObjectGuid = Guid.Empty;
    if (this.settingsObjectID != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        settingsObjectGuid = sessionKeeper.Session.GetObjectInfo(this.settingsObjectID).VersionGuid;
    }
    this.FormResult = this.FormParams is RemarkAttributesFormParams ? (AttributesListFormParams) new RemarkAttributesFormParams(NoteFieldSettings.GetDefaultAttributes(settingsObjectGuid), NoteFieldOptions.ShowMeasureUnits) : new AttributesListFormParams(this.FormParams);
    if (this.FormParams is RemarkAttributesFormParams)
    {
      try
      {
        this.isInEvents = true;
        this.cbShowMeasureUnits.Checked = ((this.FormResult as RemarkAttributesFormParams).Options & NoteFieldOptions.ShowMeasureUnits) > NoteFieldOptions.None;
      }
      finally
      {
        this.isInEvents = false;
      }
    }
    this.PrepareGroups();
    this.rootItem2.Clear();
    this.rootItem2.Add((object) this.FormResult.Items);
    this.FillTree(this.treeAttributes, (object) this.rootItem1, true);
    this.FillTree(this.treeRemarkAttributes, (object) this.rootItem2, true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Нажата кнопка "ОК"/"Применить"</summary>
  protected virtual void OKApplyPressed()
  {
    if (this.ParentMode == 0)
    {
      if (this.ReadOnly)
        return;
      this.DialogResult = DialogResult.OK;
    }
    else
      this.RaiseOnApplyPressed();
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  protected virtual void CancelPressed()
  {
    if (this.ParentMode == 0)
      this.DialogResult = DialogResult.Cancel;
    else
      this.RaiseOnCancelPressed();
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    DBObjectsCheckOutEventArgs checkOutEventArgs = e as DBObjectsCheckOutEventArgs;
    bool flag = false;
    if (objectsEventArgs != null && objectsEventArgs.ObjectIDs != null && (objectsEventArgs.EventName == "ObjectsChanged" || objectsEventArgs.EventName == "ObjectsCheckedIn" || objectsEventArgs.EventName == "ObjectsChangesCancelled"))
      flag = objectsEventArgs.ObjectIDs.Contains(DocumentTypeWeightHelper.objectCommonSpecificationsTemplate) || objectsEventArgs.ObjectIDs.Contains(-DocumentTypeWeightHelper.objectCommonSpecificationsTemplate);
    if (checkOutEventArgs != null && checkOutEventArgs.NewObjectIDs != null && checkOutEventArgs.EventName == "ObjectsCheckedOut")
      flag = checkOutEventArgs.NewObjectIDs.Contains(DocumentTypeWeightHelper.objectCommonSpecificationsTemplate) || checkOutEventArgs.NewObjectIDs.Contains(-DocumentTypeWeightHelper.objectCommonSpecificationsTemplate);
    if (!flag)
      return;
    if (this.ParentMode == 0)
      FormStorage.SaveLayout((Control) this);
    this.Init(this.FormParams, this.ParentMode);
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBarRight.Renderer = renderer;
    this.toolBarAttributes.Renderer = renderer;
    this.menuAvailable.Renderer = renderer;
    this.menuRemarks.Renderer = renderer;
  }

  /// <summary>Нажата кнопка "Добавить атрибут"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoAttrAdd(object sender, EventArgs e)
  {
    EnabledAttributesActions enabledAction;
    List<Row> selectedItems = this.GetSelectedItems(this.treeAttributes, out enabledAction);
    if ((enabledAction & EnabledAttributesActions.CanAdd) == EnabledAttributesActions.None)
      return;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      Row parentRow = selectedItems[index].ParentRow;
      RemarkAttribute remarkAttribute = selectedItems[index].Item as RemarkAttribute;
      if (parentRow != null && remarkAttribute != null)
      {
        int num = this.groups.IndexOf((string) parentRow.Item);
        if (!this.FormResult.Items.Contains(remarkAttribute))
          this.FormResult.Items.Add(remarkAttribute);
        if (num == 0)
          this.allRelAttrs.Remove(remarkAttribute);
        else
          this.allObjAttrs.Remove(remarkAttribute);
      }
    }
    this.treeAttributes.UpdateRows(true);
    this.treeRemarkAttributes.UpdateRows(true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Нажата кнопка "Удалить атрибут"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoAttrDelete(object sender, EventArgs e)
  {
    EnabledAttributesActions enabledAction;
    List<Row> selectedItems = this.GetSelectedItems(this.treeRemarkAttributes, out enabledAction);
    if ((enabledAction & EnabledAttributesActions.CanDelete) == EnabledAttributesActions.None)
      return;
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (selectedItems[index].Item is RemarkAttribute remarkAttribute)
      {
        this.FormResult.Items.Remove(remarkAttribute);
        if (remarkAttribute.AttrSource == AttributeSourceTypes.Relation)
        {
          this.allRelAttrs.Add(remarkAttribute);
          flag1 = true;
        }
        else
        {
          this.allObjAttrs.Add(remarkAttribute);
          flag2 = true;
        }
      }
    }
    if (flag1)
      this.allRelAttrs.Sort();
    if (flag2)
      this.allObjAttrs.Sort();
    this.treeAttributes.UpdateRows(true);
    this.treeRemarkAttributes.UpdateRows(true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Нажата кнопка "Обновить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoRefresh(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
    this.PrepareGroups();
    this.rootItem2.Clear();
    this.rootItem2.Add((object) this.FormResult.Items);
    this.FillTree(this.treeAttributes, (object) this.rootItem1, true);
    this.FillTree(this.treeRemarkAttributes, (object) this.rootItem2, true);
    this.UpdatePreviewString();
    this.IsChanged = !this.ReadOnly;
  }

  /// <summary>Нажата кнопка "Переместить вверх"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoMoveUp(object sender, EventArgs e)
  {
    EnabledAttributesActions enabledAction;
    List<Row> selectedItems = this.GetSelectedItems(this.treeRemarkAttributes, out enabledAction);
    if ((enabledAction & EnabledAttributesActions.MoveUp) == EnabledAttributesActions.None)
      return;
    for (int index = 0; index < selectedItems.Count; ++index)
      NoteFieldSettings.Shift((IList) this.FormResult.Items, selectedItems[index].ChildIndex, -1);
    this.treeRemarkAttributes.UpdateRows(true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Нажата кнопка "Переместить вниз"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoMoveDown(object sender, EventArgs e)
  {
    EnabledAttributesActions enabledAction;
    List<Row> selectedItems = this.GetSelectedItems(this.treeRemarkAttributes, out enabledAction);
    if ((enabledAction & EnabledAttributesActions.MoveDown) == EnabledAttributesActions.None)
      return;
    for (int index = selectedItems.Count - 1; index >= 0; --index)
      NoteFieldSettings.Shift((IList) this.FormResult.Items, selectedItems[index].ChildIndex, 1);
    this.treeRemarkAttributes.UpdateRows(true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Нажата кнопка "Переместить в начало"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoMoveTop(object sender, EventArgs e)
  {
    EnabledAttributesActions enabledAction;
    List<Row> selectedItems = this.GetSelectedItems(this.treeRemarkAttributes, out enabledAction);
    if ((enabledAction & EnabledAttributesActions.MoveTop) == EnabledAttributesActions.None)
      return;
    for (int index = 0; index < selectedItems.Count; ++index)
      NoteFieldSettings.Shift((IList) this.FormResult.Items, selectedItems[index].ChildIndex, -2147483647 /*0x80000001*/);
    this.treeRemarkAttributes.UpdateRows(true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Нажата кнопка "Переместить в конец"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoMoveBottom(object sender, EventArgs e)
  {
    EnabledAttributesActions enabledAction;
    List<Row> selectedItems = this.GetSelectedItems(this.treeRemarkAttributes, out enabledAction);
    if ((enabledAction & EnabledAttributesActions.MoveBottom) == EnabledAttributesActions.None)
      return;
    for (int index = selectedItems.Count - 1; index >= 0; --index)
      NoteFieldSettings.Shift((IList) this.FormResult.Items, selectedItems[index].ChildIndex, 2147483646);
    this.treeRemarkAttributes.UpdateRows(true);
    this.UpdatePreviewString();
    this.IsChanged = true;
  }

  /// <summary>Получить значение у встроенного редактора</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editSeparator_GetControlValue(object sender, CellEditorGetValueEventArgs e)
  {
    if (!(e.Control is ComboBox control) || (e.CellWidget == null || e.CellWidget.Row == null ? (RemarkAttribute) null : e.CellWidget.Row.Item as RemarkAttribute) == null)
      return;
    e.Value = (object) control.Text;
  }

  /// <summary>Инициализировать встроенный редактор</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editSeparator_InitializeControl(object sender, CellEditorInitializeEventArgs e)
  {
    if (!(e.Control is ComboBox control) || (e.CellWidget == null || e.CellWidget.Row == null ? (RemarkAttribute) null : e.CellWidget.Row.Item as RemarkAttribute) == null)
      return;
    control.Items.Clear();
    foreach (KeyValuePair<string, string> separatorDescriptor in NoteFieldSettings.SeparatorDescriptors)
      control.Items.Add((object) separatorDescriptor.Value);
  }

  /// <summary>Установить значение во встроенном редакторе</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editSeparator_SetControlValue(object sender, CellEditorSetValueEventArgs e)
  {
    if (!(e.Control is ComboBox control))
      return;
    RemarkAttribute remarkAttribute = e.CellWidget == null || e.CellWidget.Row == null ? (RemarkAttribute) null : e.CellWidget.Row.Item as RemarkAttribute;
    if (remarkAttribute == null)
      return;
    control.Text = NoteFieldSettings.GetSeparatorDescription(remarkAttribute.Separator);
  }

  /// <summary>
  /// Отыскать в дереве выбранных атрибутов указанный атрибут
  /// </summary>
  /// <param name="attrID">Идентификатор атрибута (меньше нуля - атрибут объектов)</param>
  protected virtual void DoBrowseToAttr(int attrID)
  {
    AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Relation;
    if (attrID < 0)
      attributeSourceTypes = AttributeSourceTypes.Object;
    attrID = Math.Abs(attrID);
    this.treeRemarkAttributes.SelectedRows.Clear();
    this.UpdateControls();
    for (int index = 0; index < this.FormResult.Items.Count; ++index)
    {
      RemarkAttribute remarkAttribute = this.FormResult.Items[index];
      if (remarkAttribute.AttrSource == attributeSourceTypes && remarkAttribute.ID == attrID)
      {
        this.treeRemarkAttributes.SelectedRow = this.treeRemarkAttributes.RootRow.ChildRowByIndex(index);
        break;
      }
    }
  }

  /// <summary>Пользователь кликнул на ссылку с атрибутом</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void edPreview_Navigating(object sender, WebBrowserNavigatingEventArgs e)
  {
    string str = "about:blank";
    string s = e.Url.OriginalString;
    if (e.Url.OriginalString.IndexOf(str) >= 0)
      s = s.Substring(str.Length, e.Url.OriginalString.Length - str.Length);
    int result;
    if (!int.TryParse(s, out result))
      return;
    e.Cancel = true;
    this.DoBrowseToAttr(result);
  }

  private void DoChangeWithoutDrawing(object sender, EventArgs e)
  {
    bool flag = (sender as ButtonItemBase).Checked;
    foreach (RemarkAttribute selectedRemarkAttribute in this.GetSelectedRemarkAttributes())
      selectedRemarkAttribute.WithoutDrawing = !flag;
    this.IsChanged = true;
  }

  private void ShowHelpTopic()
  {
    if (!(this.FormParams is RemarkAttributesFormParams))
      return;
    HelpProvidersClass.ShowHelpTopic(1520);
  }

  private void RemarkAttributesForm_HelpRequested(object sender, HelpEventArgs e)
  {
    this.ShowHelpTopic();
  }

  private void RemarkAttributesForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    this.ShowHelpTopic();
  }

  /// <summary>Освободить управляемые ресурсы</summary>
  /// <param name="disposing">true, если требуется освободить управляемые ресурсы</param>
  protected override void Dispose(bool disposing)
  {
    if (this.notifyHandler != null)
    {
      this.notificationSvc.Unsubscribe(this.notifyHandler);
      this.notifyHandler = (NotificationEventHandler) null;
    }
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBarRight.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarAttributes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuAvailable.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuRemarks.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= this.barEventsHandler;
      this.barEventsHandler = (EventHandler) null;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RemarkAttributesForm));
    this.panelInfo = new Panel();
    this.textInfo = new TextBox();
    this.btnCheckOut = new Button();
    this.pictureInfo = new PictureBox();
    this.panelBottom = new Panel();
    this.btnDefault = new Button();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.splitContainer = new SplitContainer();
    this.treeAttributes = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnAllAttributes = new Column();
    this.toolBarRight = new Intermech.Bars.ToolBar();
    this.imagesToolbars = new ImageList(this.components);
    this.btnAttrAdd = new ButtonItem();
    this.btnAttrDelete = new ButtonItem();
    this.btnRefresh = new ButtonItem();
    this.panelBottomLeft = new Panel();
    this.cbShowAllAttributes = new CheckBox();
    this.menuAvailable = new MenuBar();
    this.contextMenuAvailable = new ContextMenuBarItem();
    this.mnpAttrAdd = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.treeRemarkAttributes = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnBch = new Column();
    this.editBch = new CellEditor();
    this.columnRemarkAttributes = new Column();
    this.columnSeparators = new Column();
    this.editSeparator = new CellEditor();
    this.comboSeparator = new ComboBox();
    this.toolBarAttributes = new Intermech.Bars.ToolBar();
    this.btnMoveUp = new ButtonItem();
    this.btnMoveDown = new ButtonItem();
    this.btnMoveTop = new ButtonItem();
    this.btnMoveBottom = new ButtonItem();
    this.btnWithoutDrawing = new ButtonItem();
    this.panelBottomRight = new Panel();
    this.cbShowMeasureUnits = new CheckBox();
    this.menuRemarks = new MenuBar();
    this.contextMenuRemarks = new ContextMenuBarItem();
    this.mnpAttrDelete = new MenuButtonItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.mnpMoveTop = new MenuButtonItem();
    this.mnpMoveBottom = new MenuButtonItem();
    this.mnpWithoutDrawing = new MenuButtonItem();
    this.panelPreview = new Panel();
    this.panelHTML = new Panel();
    this.edPreview = new WebBrowser();
    this.labelPreview = new Label();
    this.toolTips = new ToolTip(this.components);
    this.timerDblClick = new Timer(this.components);
    this.timerDblClickAdd = new Timer(this.components);
    this.panelInfo.SuspendLayout();
    ((ISupportInitialize) this.pictureInfo).BeginInit();
    this.panelBottom.SuspendLayout();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.treeAttributes.BeginInit();
    this.panelBottomLeft.SuspendLayout();
    this.treeRemarkAttributes.BeginInit();
    this.panelBottomRight.SuspendLayout();
    this.panelPreview.SuspendLayout();
    this.panelHTML.SuspendLayout();
    this.SuspendLayout();
    this.panelInfo.BackColor = SystemColors.Info;
    this.panelInfo.BorderStyle = BorderStyle.Fixed3D;
    this.panelInfo.Controls.Add((Control) this.textInfo);
    this.panelInfo.Controls.Add((Control) this.btnCheckOut);
    this.panelInfo.Controls.Add((Control) this.pictureInfo);
    componentResourceManager.ApplyResources((object) this.panelInfo, "panelInfo");
    this.panelInfo.ForeColor = SystemColors.InfoText;
    this.panelInfo.Name = "panelInfo";
    componentResourceManager.ApplyResources((object) this.textInfo, "textInfo");
    this.textInfo.BackColor = SystemColors.Info;
    this.textInfo.ForeColor = SystemColors.InfoText;
    this.textInfo.Name = "textInfo";
    this.textInfo.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.btnCheckOut, "btnCheckOut");
    this.btnCheckOut.Cursor = Cursors.Default;
    this.btnCheckOut.Name = "btnCheckOut";
    this.toolTips.SetToolTip((Control) this.btnCheckOut, componentResourceManager.GetString("btnCheckOut.ToolTip"));
    this.btnCheckOut.Click += new EventHandler(this.DoCheckOutMainSpecTemplate);
    this.pictureInfo.BackColor = SystemColors.Info;
    componentResourceManager.ApplyResources((object) this.pictureInfo, "pictureInfo");
    this.pictureInfo.Name = "pictureInfo";
    this.pictureInfo.TabStop = false;
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.btnDefault);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    this.btnDefault.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnDefault, "btnDefault");
    this.btnDefault.Name = "btnDefault";
    this.toolTips.SetToolTip((Control) this.btnDefault, componentResourceManager.GetString("btnDefault.ToolTip"));
    this.btnDefault.Click += new EventHandler(this.DoSetDefaultValues);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.DoCancel);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.DoOKApply);
    componentResourceManager.ApplyResources((object) this.splitContainer, "splitContainer");
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.treeAttributes);
    this.splitContainer.Panel1.Controls.Add((Control) this.toolBarRight);
    this.splitContainer.Panel1.Controls.Add((Control) this.panelBottomLeft);
    this.splitContainer.Panel1.Controls.Add((Control) this.menuAvailable);
    this.splitContainer.Panel2.Controls.Add((Control) this.treeRemarkAttributes);
    this.splitContainer.Panel2.Controls.Add((Control) this.toolBarAttributes);
    this.splitContainer.Panel2.Controls.Add((Control) this.panelBottomRight);
    this.splitContainer.Panel2.Controls.Add((Control) this.menuRemarks);
    this.treeAttributes.AllowDrop = true;
    this.treeAttributes.AllowIndividualRowResize = false;
    this.treeAttributes.AllowRowResize = false;
    this.treeAttributes.AllowUserPinnedColumns = false;
    this.treeAttributes.AutoFitColumns = true;
    this.treeAttributes.Columns.Add(this.columnAllAttributes);
    this.treeAttributes.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeAttributes, "treeAttributes");
    this.treeAttributes.ImageList = (ImageList) null;
    this.treeAttributes.LineStyle = LineStyle.Dot;
    this.treeAttributes.MainColumn = this.columnAllAttributes;
    this.treeAttributes.Name = "treeAttributes";
    this.treeAttributes.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeAttributes.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeAttributes.SelectBeforeEdit = true;
    this.treeAttributes.ShowRootRow = false;
    this.treeAttributes.SuppressErrorMessages = true;
    this.treeAttributes.ShowContextMenu += new MouseEventHandler(this.treeAttributes_ShowContextMenu);
    this.treeAttributes.FocusRowChanged += new EventHandler(this.treeAttributes_FocusRowChanged);
    this.treeAttributes.GetCellData += new GetCellDataHandler(this.treeAttributes_GetCellData);
    this.treeAttributes.GetChildren += new GetChildrenHandler(this.treeAttributes_GetChildren);
    this.treeAttributes.GetRowData += new GetRowDataHandler(this.treeAttributes_GetRowData);
    this.treeAttributes.SelectionChanged += new EventHandler(this.treeAttributes_SelectionChanged);
    this.treeAttributes.DoubleClick += new EventHandler(this.treeAttributes_DoubleClick);
    this.columnAllAttributes.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnAllAttributes, "columnAllAttributes");
    this.columnAllAttributes.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnAllAttributes.HeaderStyle.HorzAlignment");
    this.columnAllAttributes.Movable = false;
    this.columnAllAttributes.Name = "columnAllAttributes";
    this.columnAllAttributes.Sortable = false;
    this.toolBarRight.AddRemoveButtonsVisible = false;
    this.toolBarRight.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarRight, "toolBarRight");
    this.toolBarRight.DockLine = 3;
    this.toolBarRight.DrawActionsButton = false;
    this.toolBarRight.Flow = ToolBarLayout.Vertical;
    this.toolBarRight.FullMenus = true;
    this.toolBarRight.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarRight.Hidden = false;
    this.toolBarRight.ImageList = this.imagesToolbars;
    this.toolBarRight.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.btnAttrAdd,
      (ToolbarItemBase) this.btnAttrDelete,
      (ToolbarItemBase) this.btnRefresh
    });
    this.toolBarRight.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRight.Name = "toolBarRight";
    this.toolBarRight.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRight.Stretch = true;
    this.toolBarRight.Tearable = false;
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "Right.ico");
    this.imagesToolbars.Images.SetKeyName(1, "Left.ico");
    this.imagesToolbars.Images.SetKeyName(2, "Up.ico");
    this.imagesToolbars.Images.SetKeyName(3, "Down.ico");
    this.imagesToolbars.Images.SetKeyName(4, "Top.ico");
    this.imagesToolbars.Images.SetKeyName(5, "Bottom.ico");
    this.imagesToolbars.Images.SetKeyName(6, "обновить.png");
    this.imagesToolbars.Images.SetKeyName(7, "связи.ico");
    this.imagesToolbars.Images.SetKeyName(8, "типы_объектов16.ico");
    this.imagesToolbars.Images.SetKeyName(9, "WithoutDrawing.ico");
    componentResourceManager.ApplyResources((object) this.btnAttrAdd, "btnAttrAdd");
    this.btnAttrAdd.ImageIndex = 0;
    this.btnAttrAdd.Click += new EventHandler(this.DoAttrAdd);
    componentResourceManager.ApplyResources((object) this.btnAttrDelete, "btnAttrDelete");
    this.btnAttrDelete.ImageIndex = 1;
    this.btnAttrDelete.Click += new EventHandler(this.DoAttrDelete);
    this.btnRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnRefresh, "btnRefresh");
    this.btnRefresh.ImageIndex = 6;
    this.btnRefresh.Click += new EventHandler(this.DoRefresh);
    this.panelBottomLeft.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottomLeft.Controls.Add((Control) this.cbShowAllAttributes);
    componentResourceManager.ApplyResources((object) this.panelBottomLeft, "panelBottomLeft");
    this.panelBottomLeft.Name = "panelBottomLeft";
    componentResourceManager.ApplyResources((object) this.cbShowAllAttributes, "cbShowAllAttributes");
    this.cbShowAllAttributes.Name = "cbShowAllAttributes";
    this.toolTips.SetToolTip((Control) this.cbShowAllAttributes, componentResourceManager.GetString("cbShowAllAttributes.ToolTip"));
    this.cbShowAllAttributes.UseVisualStyleBackColor = true;
    this.cbShowAllAttributes.CheckedChanged += new EventHandler(this.cbShowAllAttributes_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.menuAvailable, "menuAvailable");
    this.menuAvailable.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuAvailable.Hidden = false;
    this.menuAvailable.ImageList = this.imagesToolbars;
    this.menuAvailable.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuAvailable
    });
    this.menuAvailable.Name = "menuAvailable";
    this.menuAvailable.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuAvailable, "contextMenuAvailable");
    this.contextMenuAvailable.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.mnpAttrAdd,
      (ToolbarItemBase) this.mnpRefresh
    });
    this.contextMenuAvailable.ShowText = true;
    this.contextMenuAvailable.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.BeforeMenusPopup);
    componentResourceManager.ApplyResources((object) this.mnpAttrAdd, "mnpAttrAdd");
    this.mnpAttrAdd.ImageIndex = 0;
    this.mnpAttrAdd.ShowText = true;
    this.mnpAttrAdd.Click += new EventHandler(this.DoAttrAdd);
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ImageIndex = 6;
    this.mnpRefresh.ShowText = true;
    this.mnpRefresh.Click += new EventHandler(this.DoRefresh);
    this.treeRemarkAttributes.AllowDrop = true;
    this.treeRemarkAttributes.AllowIndividualRowResize = false;
    this.treeRemarkAttributes.AllowRowResize = false;
    this.treeRemarkAttributes.AllowUserPinnedColumns = false;
    this.treeRemarkAttributes.AutoFitColumns = true;
    this.treeRemarkAttributes.Columns.Add(this.columnBch);
    this.treeRemarkAttributes.Columns.Add(this.columnRemarkAttributes);
    this.treeRemarkAttributes.Columns.Add(this.columnSeparators);
    this.treeRemarkAttributes.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeRemarkAttributes, "treeRemarkAttributes");
    this.treeRemarkAttributes.Editors.Add(this.editSeparator);
    this.treeRemarkAttributes.Editors.Add(this.editBch);
    this.treeRemarkAttributes.ImageList = (ImageList) null;
    this.treeRemarkAttributes.LineStyle = LineStyle.Dot;
    this.treeRemarkAttributes.MainColumn = this.columnRemarkAttributes;
    this.treeRemarkAttributes.Name = "treeRemarkAttributes";
    this.treeRemarkAttributes.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeRemarkAttributes.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeRemarkAttributes.SelectBeforeEdit = true;
    this.treeRemarkAttributes.ShowRootRow = false;
    this.treeRemarkAttributes.SuppressErrorMessages = true;
    this.treeRemarkAttributes.ShowContextMenu += new MouseEventHandler(this.treeRemarkAttributes_ShowContextMenu);
    this.treeRemarkAttributes.BeforeShowCellEdit += new BeforeShowCellEditHandler(this.treeRemarkAttributes_BeforeShowCellEdit);
    this.treeRemarkAttributes.CellDoubleClick += new EventHandler(this.treeRemarkAttributes_CellDoubleClick);
    this.treeRemarkAttributes.FocusRowChanged += new EventHandler(this.treeRemarkAttributes_FocusRowChanged);
    this.treeRemarkAttributes.GetCellData += new GetCellDataHandler(this.treeRemarkAttributes_GetCellData);
    this.treeRemarkAttributes.GetChildren += new GetChildrenHandler(this.treeRemarkAttributes_GetChildren);
    this.treeRemarkAttributes.GetRowData += new GetRowDataHandler(this.treeRemarkAttributes_GetRowData);
    this.treeRemarkAttributes.SelectionChanged += new EventHandler(this.treeRemarkAttributes_SelectionChanged);
    this.treeRemarkAttributes.SetCellValue += new SetCellValueHandler(this.treeRemarkAttributes_SetCellValue);
    componentResourceManager.ApplyResources((object) this.columnBch, "columnBch");
    this.columnBch.CellEditor = this.editBch;
    this.columnBch.Movable = false;
    this.columnBch.Name = "columnBch";
    this.columnBch.Resizable = false;
    this.columnBch.Sortable = false;
    this.editBch.CellAlignment = ContentAlignment.MiddleCenter;
    this.editBch.Control = (Control) null;
    this.editBch.DisplayMode = CellEditorDisplayMode.Always;
    this.columnRemarkAttributes.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnRemarkAttributes, "columnRemarkAttributes");
    this.columnRemarkAttributes.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnRemarkAttributes.HeaderStyle.HorzAlignment");
    this.columnRemarkAttributes.Movable = false;
    this.columnRemarkAttributes.Name = "columnRemarkAttributes";
    this.columnRemarkAttributes.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnSeparators, "columnSeparators");
    this.columnSeparators.CellEditor = this.editSeparator;
    this.columnSeparators.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnSeparators.HeaderStyle.HorzAlignment");
    this.columnSeparators.Movable = false;
    this.columnSeparators.Name = "columnSeparators";
    this.columnSeparators.Sortable = false;
    this.editSeparator.CellAlignment = ContentAlignment.MiddleRight;
    this.editSeparator.Control = (Control) this.comboSeparator;
    this.editSeparator.GetControlValue += new CellEditorGetValueHandler(this.editSeparator_GetControlValue);
    this.editSeparator.InitializeControl += new CellEditorInitializeHandler(this.editSeparator_InitializeControl);
    this.editSeparator.SetControlValue += new CellEditorSetValueHandler(this.editSeparator_SetControlValue);
    componentResourceManager.ApplyResources((object) this.comboSeparator, "comboSeparator");
    this.comboSeparator.Name = "comboSeparator";
    this.toolTips.SetToolTip((Control) this.comboSeparator, componentResourceManager.GetString("comboSeparator.ToolTip"));
    this.toolBarAttributes.AddRemoveButtonsVisible = false;
    this.toolBarAttributes.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarAttributes, "toolBarAttributes");
    this.toolBarAttributes.DockLine = 3;
    this.toolBarAttributes.DrawActionsButton = false;
    this.toolBarAttributes.Flow = ToolBarLayout.Vertical;
    this.toolBarAttributes.FullMenus = true;
    this.toolBarAttributes.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarAttributes.Hidden = false;
    this.toolBarAttributes.ImageList = this.imagesToolbars;
    this.toolBarAttributes.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.btnMoveUp,
      (ToolbarItemBase) this.btnMoveDown,
      (ToolbarItemBase) this.btnMoveTop,
      (ToolbarItemBase) this.btnMoveBottom,
      (ToolbarItemBase) this.btnWithoutDrawing
    });
    this.toolBarAttributes.MinimumFloatingSize = new Size(250, 30);
    this.toolBarAttributes.Name = "toolBarAttributes";
    this.toolBarAttributes.Overflow = ToolBarOverflow.Wrap;
    this.toolBarAttributes.Stretch = true;
    this.toolBarAttributes.Tearable = false;
    componentResourceManager.ApplyResources((object) this.btnMoveUp, "btnMoveUp");
    this.btnMoveUp.ImageIndex = 2;
    this.btnMoveUp.Click += new EventHandler(this.DoMoveUp);
    componentResourceManager.ApplyResources((object) this.btnMoveDown, "btnMoveDown");
    this.btnMoveDown.ImageIndex = 3;
    this.btnMoveDown.Click += new EventHandler(this.DoMoveDown);
    this.btnMoveTop.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnMoveTop, "btnMoveTop");
    this.btnMoveTop.ImageIndex = 4;
    this.btnMoveTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.btnMoveBottom, "btnMoveBottom");
    this.btnMoveBottom.ImageIndex = 5;
    this.btnMoveBottom.Click += new EventHandler(this.DoMoveBottom);
    this.btnWithoutDrawing.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnWithoutDrawing, "btnWithoutDrawing");
    this.btnWithoutDrawing.ImageIndex = 9;
    this.btnWithoutDrawing.Visible = false;
    this.btnWithoutDrawing.Click += new EventHandler(this.DoChangeWithoutDrawing);
    this.panelBottomRight.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottomRight.Controls.Add((Control) this.cbShowMeasureUnits);
    componentResourceManager.ApplyResources((object) this.panelBottomRight, "panelBottomRight");
    this.panelBottomRight.Name = "panelBottomRight";
    componentResourceManager.ApplyResources((object) this.cbShowMeasureUnits, "cbShowMeasureUnits");
    this.cbShowMeasureUnits.Name = "cbShowMeasureUnits";
    this.toolTips.SetToolTip((Control) this.cbShowMeasureUnits, componentResourceManager.GetString("cbShowMeasureUnits.ToolTip"));
    this.cbShowMeasureUnits.UseVisualStyleBackColor = true;
    this.cbShowMeasureUnits.CheckedChanged += new EventHandler(this.cbShowMeasureUnits_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.menuRemarks, "menuRemarks");
    this.menuRemarks.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuRemarks.Hidden = false;
    this.menuRemarks.ImageList = this.imagesToolbars;
    this.menuRemarks.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuRemarks
    });
    this.menuRemarks.Name = "menuRemarks";
    this.menuRemarks.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuRemarks, "contextMenuRemarks");
    this.contextMenuRemarks.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpAttrDelete,
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown,
      (ToolbarItemBase) this.mnpMoveTop,
      (ToolbarItemBase) this.mnpMoveBottom,
      (ToolbarItemBase) this.mnpWithoutDrawing
    });
    this.contextMenuRemarks.ShowText = true;
    this.contextMenuRemarks.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.BeforeMenusPopup);
    componentResourceManager.ApplyResources((object) this.mnpAttrDelete, "mnpAttrDelete");
    this.mnpAttrDelete.ImageIndex = 1;
    this.mnpAttrDelete.ShowText = true;
    this.mnpAttrDelete.Click += new EventHandler(this.DoAttrDelete);
    this.mnpMoveUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 2;
    this.mnpMoveUp.ShowText = true;
    this.mnpMoveUp.Click += new EventHandler(this.DoMoveUp);
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 3;
    this.mnpMoveDown.ShowText = true;
    this.mnpMoveDown.Click += new EventHandler(this.DoMoveDown);
    this.mnpMoveTop.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveTop, "mnpMoveTop");
    this.mnpMoveTop.ImageIndex = 4;
    this.mnpMoveTop.ShowText = true;
    this.mnpMoveTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.mnpMoveBottom, "mnpMoveBottom");
    this.mnpMoveBottom.ImageIndex = 5;
    this.mnpMoveBottom.ShowText = true;
    this.mnpMoveBottom.Click += new EventHandler(this.DoMoveBottom);
    componentResourceManager.ApplyResources((object) this.mnpWithoutDrawing, "mnpWithoutDrawing");
    this.mnpWithoutDrawing.ImageIndex = 9;
    this.mnpWithoutDrawing.ShowText = true;
    this.mnpWithoutDrawing.Visible = false;
    this.mnpWithoutDrawing.Click += new EventHandler(this.DoChangeWithoutDrawing);
    this.panelPreview.Controls.Add((Control) this.panelHTML);
    this.panelPreview.Controls.Add((Control) this.labelPreview);
    componentResourceManager.ApplyResources((object) this.panelPreview, "panelPreview");
    this.panelPreview.Name = "panelPreview";
    this.panelHTML.BorderStyle = BorderStyle.Fixed3D;
    this.panelHTML.Controls.Add((Control) this.edPreview);
    componentResourceManager.ApplyResources((object) this.panelHTML, "panelHTML");
    this.panelHTML.Name = "panelHTML";
    this.edPreview.AllowWebBrowserDrop = false;
    this.edPreview.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.edPreview, "edPreview");
    this.edPreview.IsWebBrowserContextMenuEnabled = false;
    this.edPreview.Name = "edPreview";
    this.edPreview.ScriptErrorsSuppressed = true;
    this.edPreview.WebBrowserShortcutsEnabled = false;
    this.edPreview.Navigating += new WebBrowserNavigatingEventHandler(this.edPreview_Navigating);
    componentResourceManager.ApplyResources((object) this.labelPreview, "labelPreview");
    this.labelPreview.Name = "labelPreview";
    this.timerDblClick.Interval = 50;
    this.timerDblClick.Tick += new EventHandler(this.DoDelayedAttrDelete);
    this.timerDblClickAdd.Interval = 50;
    this.timerDblClickAdd.Tick += new EventHandler(this.DoDelayedAttrAdd);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.panelPreview);
    this.Controls.Add((Control) this.panelInfo);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (RemarkAttributesForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.HelpButtonClicked += new CancelEventHandler(this.RemarkAttributesForm_HelpButtonClicked);
    this.FormClosed += new FormClosedEventHandler(this.RemarkAttributesForm_FormClosed);
    this.HelpRequested += new HelpEventHandler(this.RemarkAttributesForm_HelpRequested);
    this.panelInfo.ResumeLayout(false);
    this.panelInfo.PerformLayout();
    ((ISupportInitialize) this.pictureInfo).EndInit();
    this.panelBottom.ResumeLayout(false);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.treeAttributes.EndInit();
    this.panelBottomLeft.ResumeLayout(false);
    this.panelBottomLeft.PerformLayout();
    this.treeRemarkAttributes.EndInit();
    this.panelBottomRight.ResumeLayout(false);
    this.panelBottomRight.PerformLayout();
    this.panelPreview.ResumeLayout(false);
    this.panelHTML.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}

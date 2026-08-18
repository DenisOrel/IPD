
// Type: Intermech.Client.Core.FormDesigner.Controls.DesForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Actions;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Client.Core.FormDesigner.Utils;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Dictionary;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Форма для редактирования значений атрибута.</summary>
public class DesForm : Form, IFormDesignerControl
{
  /// <summary>Данные основного объекта/связи</summary>
  private DesForm.ElementInformation _EI = new DesForm.ElementInformation();
  /// <summary>Данные связи, если объект рассматривается в составе</summary>
  private DesForm.ElementInformation _relEI = new DesForm.ElementInformation();
  /// <summary>Контролы Workflow</summary>
  private List<IMultipleAttributeEditor> _multiEditors = new List<IMultipleAttributeEditor>();
  /// <summary>
  /// Кэш TabControl'ов. Нужен для позиционирования на выбранную закладку при новом открытии формы
  /// </summary>
  private TabControlManager _tabMngr;
  /// <summary>Pегистрация дополнительных действий на кнопки</summary>
  private IFormDesignerActionManager _btnMngr;
  /// <summary>Менеджер действий для формы</summary>
  private IFormDesignerEventsManager _eventsMngr;
  /// <summary>
  /// Блокирует от изменения атрибуты, обрабатываемые на клиенте специальным образом. К ним относятся атрибуты, извлекаемые из файла документа при сохранении изменений
  /// </summary>
  private IAttributesLockService _attLockService;
  private IAttributePropertyDescriberService _attrPropDescriberService;
  /// <summary>Сервис для расшифровки значения атрибута</summary>
  private IDictionaryServerService _dictServerSrv;
  /// <summary>
  /// Флаг выставляется в true, только при загрузке или сохранении значений атрибутов
  /// </summary>
  private bool _inSaveLoad;
  private bool _isModified;
  /// <summary>Идентификатор типа объекта/связи</summary>
  private int _newTypeID = -1;
  private long _newProjID;
  private long _oldProjID;
  /// <summary>Владелец объекта</summary>
  /// <remarks>Заказчик - В.Скалозубов (BugBase 1217302)</remarks>
  private long _newOwnerID;
  /// <summary>Режимы получения атрибутов</summary>
  private GetAttributeValuesModes _modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeBlobs | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.RequestedByForm;
  private DockStyle _dock;
  private bool canCheckoutFlag;
  private bool canCheckinFlag;
  /// <summary>Статус зачитан для объекта _StatusLoadedID</summary>
  private bool _StatusLoaded;
  private long _StatusLoadedID;
  private List<Tuple<TabControl, int>> tabControlsInfo = new List<Tuple<TabControl, int>>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ErrorProvider _err;
  private ToolTip _tt;

  /// <summary>Список кнопок, размещенных на форме редактирования.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<AttrButton> AttrButtons { get; private set; }

  /// <summary>Идентификатор типа объекта/связи.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ElementTypeID => this._EI.TypeID;

  /// <summary>Идентификатор формы.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long FormID { get; set; }

  public List<long> IncludedClassificators { get; set; } = new List<long>();

  /// <summary>Идентификатор объекта/связи.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IElementInfo Info
  {
    get => this._EI.Info;
    set
    {
      if (this._EI.Info != null && this._EI.Info.ElementIdentifier == value.ElementIdentifier)
        return;
      this._EI.Info = value;
      this._EI.ap = new AttributeProcessor(this._EI.invokeProcessor);
      this._EI.infoReadonly = true;
    }
  }

  /// <summary>Возможность редактирования элемента.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool InfoReadonly => this._EI.infoReadonly;

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsCreationMode { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsFormActivated { get; private set; }

  /// <summary>Необходимость сохранения.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Modified
  {
    get
    {
      bool modified = false;
      if (!this._inSaveLoad && this._EI.Info != null)
        modified = this._isModified || this._EI.AdditionalValues.Count > 0 || this._relEI.AdditionalValues.Count > 0;
      return modified;
    }
  }

  /// <summary>
  /// Флаг, который показывает, что значение было изменено при загрузке.
  /// </summary>
  /// <remarks>
  /// Вводилось для контрола AttrMeasuredEdit, чтобы была возможность сохранить значение по умолчанию.
  /// (Подробнее см. описание свойства ModifiedInLoad у контрола).
  /// </remarks>
  [Browsable(false)]
  public bool ModifiedInLoad { get; private set; }

  /// <summary>Замены при сохранении.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.Collections.Generic.Dictionary<long, long> PinExchange { get; private set; }

  /// <summary>Процессоры атрибутов.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AttributeProcessor Processor => this._EI.ap;

  /// <summary>Процессоры атрибутов.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AttributeProcessor RelationProcessor => this._relEI.ap;

  /// <summary>
  /// Данные связи, если объект рассматривается в составе.
  /// Если рассматривается обособленно связь, то данные для этой связи находятся в переменной _EI.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IElementInfo RelationInfo
  {
    get => this._relEI.Info;
    set
    {
      this._relEI.Info = value;
      this._relEI.ap = new AttributeProcessor(this._relEI.invokeProcessor);
      this._relEI.infoReadonly = true;
    }
  }

  /// <summary>Идентификатор типа дополнительной связи.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int RelationTypeID => this._relEI.TypeID != -1 ? this._relEI.TypeID : -1;

  /// <summary>Возможность редактирования элемента.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool RelationInfoReadonly => this._relEI.Info == null || this._relEI.infoReadonly;

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.IServiceProvider ServiceProvider { get; internal set; }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ToolTip ToolTip => this._tt;

  /// <summary>Возвращает или задает цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Control")]
  public override Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(typeof (Cursor), "Default")]
  public new Cursor Cursor
  {
    get => base.Cursor;
    set => base.Cursor = value;
  }

  /// <summary>
  /// Возвращает или задает основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "ControlText")]
  public override Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new FormBorderStyle FormBorderStyle
  {
    get => base.FormBorderStyle;
    set => base.FormBorderStyle = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override bool AllowDrop
  {
    get => base.AllowDrop;
    set => base.AllowDrop = value;
  }

  /// <summary>Ссылки на привязанные к форме типы объектов\связей.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public FormLinks Links { get; set; }

  /// <summary>События формы, на которые подписался пользователь.</summary>
  [RefreshProperties(RefreshProperties.All)]
  public FormDesignerAction[] FormDesignerEvents { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(true)]
  public override bool AutoScroll { get; set; }

  /// <summary>Докинг формы.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override DockStyle Dock
  {
    get => this.Site == null || !this.Site.DesignMode ? base.Dock : this._dock;
    set
    {
      if (this.Site != null && this.Site.DesignMode)
        return;
      base.Dock = DockStyle.Fill;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Point Location
  {
    get => base.Location;
    set => base.Location = value;
  }

  /// <summary>
  /// Переопределено для постоянной сериализации. Необходимо для масштабирования.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Margin
  {
    get => base.Margin;
    set => base.Margin = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(typeof (FormStartPosition), "WindowsDefaultLocation")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new FormStartPosition StartPosition
  {
    get => base.StartPosition;
    set => base.StartPosition = value;
  }

  /// <summary>Метка раздела в файле справки.</summary>
  [DefaultValue("")]
  public string HelpPartLabel { get; set; }

  /// <summary>Путь к файлу справки.</summary>
  [DefaultValue("")]
  public string HelpPathToFile { get; set; }

  /// <summary>Список измененных атрибутов основного объекта/связи.</summary>
  public List<AttributeValues> GetBaseElementChangedAttributes => this.GetChangedAttributes(true);

  /// <summary>Список измененных атрибутов дополнительной связи.</summary>
  public List<AttributeValues> GetAdditionalElementChangedAttributes
  {
    get => this.GetChangedAttributes(false);
  }

  /// <summary>Список измененных атрибутов указанной сущности.</summary>
  /// <param name="baseElement">Основной объект/связь или дополнительная связь</param>
  /// <returns>Список измененных атрибутов указанной сущности</returns>
  private List<AttributeValues> GetChangedAttributes(bool baseElement)
  {
    List<AttributeValues> source = new List<AttributeValues>();
    foreach (IAttributeEditor attributeEditor in baseElement ? this._EI.LinkedEditors : this._relEI.LinkedEditors)
    {
      if (attributeEditor.Modified)
      {
        AttributeValues av = attributeEditor.Values;
        if (av.Values != null && source.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID)) == null)
          source.Add(av);
      }
    }
    foreach (AttributeValues attributeValues in baseElement ? this._EI.AdditionalValues : this._relEI.AdditionalValues)
    {
      AttributeValues av = attributeValues;
      if (av.Values != null && source.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID)) == null)
        source.Add(av);
    }
    return source;
  }

  /// <summary>Не использовать кэш номеров страничек.</summary>
  /// <remarks>
  /// BugBase 1293619: Сброс активной вкладки карточки в редакторе карточки
  /// В EditorForm при вызове Surface.Dispose(); происходит уничтожение закладок и вызывается событие смены закладок, в кэш записывается индекс закладки -1
  /// Поэтому нужно запретить запись в кэш
  /// </remarks>
  public bool DontUseCache { get; set; }

  /// <summary>Конструктор.</summary>
  public DesForm()
  {
    this.InitializeComponent();
    this._tabMngr = ServicesManager.GetService(typeof (TabControlManager)) as TabControlManager;
    this._btnMngr = ServicesManager.GetService(typeof (IFormDesignerActionManager)) as IFormDesignerActionManager;
    this._eventsMngr = ServicesManager.GetService(typeof (IFormDesignerEventsManager)) as IFormDesignerEventsManager;
    this._attrPropDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
    this._attLockService = ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true);
    this.AutoScroll = true;
    this.CanContainsChildren = true;
    this.IsCreationMode = false;
    this.AttrButtons = new List<AttrButton>();
    this.PinExchange = new System.Collections.Generic.Dictionary<long, long>();
    this.FormID = 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnEditor_CompletionOfEditingEvent(object sender, EventArgs e)
  {
    if (!(sender is IParent4Control parent4Control))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IDictionaryService)) is IDictionaryServerService customService))
        return;
      if (parent4Control.ParentPoint == AttributeDestinationPoint.Default)
      {
        if (this._EI.ParsedAVs.Count <= 0)
          return;
        AttributeValues values = (sender as IAttributeEditor).Values;
        int index = this._EI.ParsedAVs.IndexOf(values);
        if (index > -1)
          this._EI.ParsedAVs[index].Values = values.Values;
        if (values.AttributeAlias != null && this._EI.ForParsedAVs.ContainsKey(values.AttributeAlias))
          this._EI.ForParsedAVs[values.AttributeAlias].Values = values.Values;
        this.ParseAttributeValues(sessionKeeper.Session, customService, this._EI);
        foreach (AttributeValues parsedAv in this._EI.ParsedAVs)
        {
          if (this._EI.DuplicationEditors.ContainsKey(parsedAv.AttributeID))
          {
            foreach (IAttributeEditor attributeEditor in this._EI.DuplicationEditors[parsedAv.AttributeID])
              attributeEditor.Values = parsedAv;
          }
        }
      }
      else
      {
        if (parent4Control.ParentPoint != AttributeDestinationPoint.Relation || this._relEI.Info == null || this._relEI.ParsedAVs.Count <= 0)
          return;
        this.ParseAttributeValues(sessionKeeper.Session, customService, this._relEI);
        foreach (AttributeValues parsedAv in this._relEI.ParsedAVs)
        {
          if (this._relEI.DuplicationEditors.ContainsKey(parsedAv.AttributeID))
          {
            foreach (IAttributeEditor attributeEditor in this._relEI.DuplicationEditors[parsedAv.AttributeID])
              attributeEditor.Values = parsedAv;
          }
        }
      }
    }
  }

  /// <summary>
  /// При изменении значений, проверяется доступность кнопок.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnControl_Modified(object sender, EventArgs e)
  {
    if (this.DesignMode)
    {
      this._isModified = true;
    }
    else
    {
      this.ModifiedInLoad = false;
      if (!this.Modified)
      {
        this._isModified = true;
        if (this._eventsMngr != null && this.FormDesignerEvents != null)
        {
          foreach (FormDesignerAction formDesignerEvent in this.FormDesignerEvents)
          {
            if (this._eventsMngr.GetEvent(formDesignerEvent.ActionGuid) is IFormDesignerFormEventsHandler formEventsHandler)
              formEventsHandler.Modified((object) this);
          }
        }
      }
      this.CheckButtonsState();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OntabControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._tabMngr == null || this.FormID == 0L || this.DontUseCache || !(sender is TabControl tabControl))
      return;
    this._tabMngr.Cache[this.FormID][tabControl.Name] = tabControl.SelectedIndex;
  }

  /// <summary>Событие, возникающее при деактивации вьюшки.</summary>
  /// <remark>Событие исходит от формы.
  /// Но на событие должны давать возможность подписываться только контролы, которые могут быть контейнерами контролов.
  /// Необходимость возникла из-за случая, когда во время деактивации вьюшки нужно провести деактивацию контрола.
  /// Поэтому, если контрол лежит на форме, то он получает сообщение от самой формы, а если контрол лежит на другом контроле,
  /// то он получает сообщение от родителя, а родитель в итоге от формы.</remark>
  public event EventHandler FormDeactivate;

  /// <summary>Загрузка данных завершена.</summary>
  public event EventHandler LoadDataCompleted;

  /// <summary>Возможность контрола иметь дочерние контролы.</summary>
  public bool CanContainsChildren { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    if (this._eventsMngr == null || this.FormDesignerEvents == null)
      return;
    foreach (FormDesignerAction formDesignerEvent in this.FormDesignerEvents)
    {
      if (this._eventsMngr.GetEvent(formDesignerEvent.ActionGuid) is IFormDesignerFormEventsHandler formEventsHandler)
        formEventsHandler.Deactivate((object) this);
    }
  }

  /// <summary>
  /// При загрузке формы, необходимо определить контролы, связанные с атрибутами.
  /// </summary>
  /// <param name="e"></param>
  public void ControlsLoaded() => this.GetEditorsCollection((Control) this);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ctrl"></param>
  private void GetEditorsCollection(Control ctrl)
  {
    switch (ctrl)
    {
      case AttrButton attrButton:
        if (this.AttrButtons.Contains(attrButton))
          return;
        attrButton.DesForm = this;
        this.AttrButtons.Add(attrButton);
        return;
      case IAttributeEditor attributeEditor:
        attributeEditor.DesForm = this;
        if (attributeEditor.AttributeInfo == null)
        {
          AttrComboBox attrComboBox = attributeEditor as AttrComboBox;
          this.SetEditorEnabled(attributeEditor, attrComboBox != null ? attrComboBox.EnableWithoutAttribute : this.CanControlBeEnabled((object) ctrl));
          attributeEditor.Modified = false;
          return;
        }
        if (!(attributeEditor is IParent4Control parent4Control))
        {
          this.SetEditorEnabled(attributeEditor, false);
          return;
        }
        if (parent4Control.ParentPoint == AttributeDestinationPoint.Default)
        {
          this._EI.AddControl(MetaDataHelper.GetAttributeTypeID(attributeEditor.AttributeInfo.AttributeGuid), attributeEditor);
          attributeEditor.ModifiedEvent += new EventHandler(this.OnControl_Modified);
          return;
        }
        if (this._relEI.Info != null)
        {
          this._relEI.AddControl(MetaDataHelper.GetAttributeTypeID(attributeEditor.AttributeInfo.AttributeGuid), attributeEditor);
          attributeEditor.ModifiedEvent += new EventHandler(this.OnControl_Modified);
          return;
        }
        this.SetEditorEnabled(attributeEditor, false);
        return;
      case IMultipleAttributeEditor multipleAttributeEditor:
        multipleAttributeEditor.DesForm = this;
        this._multiEditors.Add(multipleAttributeEditor);
        multipleAttributeEditor.ModifiedEvent += new EventHandler(this.OnControl_Modified);
        return;
      case TabControl tabControl:
        if (this.FormID != 0L)
        {
          if (!(ServicesManager.GetService(typeof (IFormDesignerStateHolder)) is IFormDesignerStateHolder service) || (service.State & FormDesignerState.OpenObjectCreateWizard) == FormDesignerState.None)
          {
            if (this._tabMngr != null)
            {
              if (this._tabMngr.Cache.ContainsKey(this.FormID))
              {
                if (this._tabMngr.Cache[this.FormID].ContainsKey(ctrl.Name))
                  tabControl.SelectedIndex = this._tabMngr.Cache[this.FormID][ctrl.Name];
              }
              else
                this._tabMngr.Cache.Add(this.FormID, new System.Collections.Generic.Dictionary<string, int>(1));
            }
          }
          else
            this.DontUseCache = true;
          tabControl.SelectedIndexChanged += new EventHandler(this.OntabControl_SelectedIndexChanged);
          break;
        }
        break;
    }
    if (ctrl.Controls.Count <= 0)
      return;
    foreach (Control control in (ArrangedElementCollection) ctrl.Controls)
      this.GetEditorsCollection(control);
  }

  /// <summary>Проверяет доступность контрола по атрибуту</summary>
  /// <param name="ctrl"></param>
  /// <returns></returns>
  private bool CanControlBeEnabled(object ctrl)
  {
    foreach (Attribute customAttribute in ctrl.GetType().GetCustomAttributes(false))
    {
      if (customAttribute is CanAlwaysEnabled)
        return true;
    }
    return false;
  }

  /// <summary>Загрузка значений атрибутов у объекта/связи.</summary>
  /// <param name="mode">Режим обновления данных</param>
  public void LoadAttributes(RefreshMode mode = RefreshMode.Default)
  {
    if (this._EI.Info == null)
      return;
    this._inSaveLoad = true;
    this._EI.ClearLists();
    this._relEI.ClearLists();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        try
        {
          switch (this._EI.Info.ElementKind)
          {
            case AttributableElements.Object:
              IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._EI.Info.ElementIdentifier, false);
              if (objectActualCopy == null)
                return;
              this._newProjID = this._oldProjID = objectActualCopy.ProjectID;
              this.IsCreationMode = objectActualCopy.IsCreationMode;
              this._EI.DBAttributable = (IDBAttributable) objectActualCopy;
              break;
            case AttributableElements.Relation:
              this._EI.DBAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(this._EI.Info.ElementIdentifier, false);
              if (this._EI.DBAttributable == null)
                return;
              break;
            default:
              return;
          }
          this._dictServerSrv = sessionKeeper.Session.GetCustomService(typeof (IDictionaryService)) as IDictionaryServerService;
          this.ModifiedInLoad = false;
          this.LoadProcess(sessionKeeper.Session, this._EI, mode);
          if (this._relEI.Info != null)
          {
            this._relEI.DBAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(this._relEI.Info.ElementIdentifier, false);
            this.LoadProcess(sessionKeeper.Session, this._relEI, mode);
          }
          this._multiEditors.ForEach((Action<IMultipleAttributeEditor>) (x =>
          {
            x.Load();
            x.Modified = false;
          }));
        }
        finally
        {
          this._EI.DBAttributable = this._relEI.DBAttributable = (IDBAttributable) null;
        }
      }
    }
    finally
    {
      this._isModified = this._inSaveLoad = false;
    }
    this.UpdateObjectStatus();
    this.CheckButtonsState();
    this.IsFormActivated = true;
    this.OnLoadDataCompleted();
  }

  /// <summary>Может ли объект быть взят на изменение</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanCheckoutFlag => this.canCheckoutFlag;

  /// <summary>Могут ли быть сохранены изменения объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanCheckinFlag => this.canCheckinFlag;

  /// <summary>
  /// Сбрасывает флаг чтения статуса объекта, чтобы следующий UpdateObjectStatus() его обновил
  /// </summary>
  public void ResetStatus()
  {
    this._StatusLoaded = false;
    this._StatusLoadedID = 0L;
  }

  private void UpdateObjectStatus()
  {
    if (this._StatusLoaded && (!this._StatusLoaded || this._StatusLoadedID == this._EI.Info.ElementIdentifier))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._EI.Info.ElementIdentifier, false);
      bool flag = objectActualCopy != null && objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout;
      long checkoutBy = objectActualCopy == null ? 0L : objectActualCopy.CheckoutBy;
      this.canCheckoutFlag = flag && checkoutBy == 0L;
      this.canCheckinFlag = flag && checkoutBy == sessionKeeper.Session.UserID;
      this._StatusLoaded = true;
      this._StatusLoadedID = this._EI.Info.ElementIdentifier;
    }
  }

  /// <summary>Загрузка значений в контрролы.</summary>
  /// <param name="iSession">Сессия</param>
  /// <param name="ei">Информация об объекте/связи</param>
  /// <param name="mode">Режим загрузки</param>
  private void LoadProcess(IUserSession iSession, DesForm.ElementInformation ei, RefreshMode mode)
  {
    if (ei.HasVirtualAttribute)
      this._modes |= GetAttributeValuesModes.IncludeVirtualAttributes;
    List<AttributeValues> attributeValues = this.GetAttributeValues(ei, mode);
    ei.LockAttributes = this._attLockService != null ? this._attLockService.GetLockedAttributes(ei.Info.ElementKind, ei.Info.ElementIdentifier, ei.TypeID) : (ICollection<int>) new List<int>(0);
    if (this._dictServerSrv != null)
    {
      foreach (AttributeValues av in attributeValues)
      {
        this.CheckParsedAttributeValues(ei, av);
        this.CheckForParsedAttributeValues(ei, av);
      }
      this.ParseAttributeValues(iSession, this._dictServerSrv, ei);
      this.UpdateAttributeValues(attributeValues, ei.ParsedAVs);
    }
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> dictionary = new System.Collections.Generic.Dictionary<int, List<IAttributeEditor>>();
    foreach (KeyValuePair<int, List<IAttributeEditor>> duplicationEditor in ei.DuplicationEditors)
    {
      int attrID = duplicationEditor.Key;
      AttributeValues av = attributeValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
      if (av != null)
      {
        if (ei.LockAttributes.Contains(attrID))
          av.ReadOnly = true;
        if (this.IsCreationMode)
        {
          foreach (IAttributeEditor editor in duplicationEditor.Value)
          {
            if (editor is AttrMeasuredEdit attrMeasuredEdit)
            {
              attrMeasuredEdit.AllowDefaultValue = true;
              this.AddAttributeValues(iSession, ei, av, editor);
              if (attrMeasuredEdit.ModifiedInLoad)
                this.ModifiedInLoad = true;
            }
            else
              this.AddAttributeValues(iSession, ei, av, editor);
          }
        }
        else
          duplicationEditor.Value.ForEach((Action<IAttributeEditor>) (x => this.AddAttributeValues(iSession, ei, av, x)));
      }
      else if (ei.LockAttributes.Contains(attrID))
      {
        duplicationEditor.Value.ForEach((Action<IAttributeEditor>) (x =>
        {
          x.Values = (AttributeValues) null;
          x.Modified = false;
        }));
      }
      else
      {
        AttributeOptions attributeOptions = this.GetAttributeOptions(ei.Info.ElementKind, ei.TypeID, attrID);
        if (ei.infoReadonly && (attributeOptions & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase)
        {
          duplicationEditor.Value.ForEach((Action<IAttributeEditor>) (x =>
          {
            x.Values = (AttributeValues) null;
            x.Modified = false;
          }));
        }
        else
        {
          List<IAttributeEditor> attributeEditorList = new List<IAttributeEditor>(duplicationEditor.Value.Count);
          foreach (IAttributeEditor editor in duplicationEditor.Value)
          {
            AttributeValues av1 = !(editor is IExpertSystemCtrl expertSystemCtrl) || !expertSystemCtrl.UseInExpertSystem ? (AttributeValues) null : new AttributeValues(attrID, (object) DBNull.Value);
            if (editor is AttrLabel)
              this.AddAttributeValues(iSession, ei, av1, editor);
            else if (!editor.CanAddAttribute)
              this.AddAttributeValues(iSession, ei, (AttributeValues) null, editor);
            else if (editor is IIMControlEnabled imControlEnabled && imControlEnabled.DisabledInDesign)
            {
              this.AddAttributeValues(iSession, ei, av1, editor);
            }
            else
            {
              if ((attributeOptions & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit)
              {
                switch (editor)
                {
                  case AttrTextEdit _:
                  case AttrListBoxBase _:
                  case AttrObjectsListBase _:
                    if (this._attrPropDescriberService == null || this._attrPropDescriberService.GetDescriber(attrID) == null)
                      break;
                    goto label_40;
                }
                this.AddAttributeValues(iSession, ei, av1, editor);
                continue;
              }
label_40:
              attributeEditorList.Add(editor);
            }
          }
          if (attributeEditorList.Count != 0)
            dictionary.Add(attrID, attributeEditorList);
        }
      }
    }
    if (dictionary.Count <= 0)
      return;
    AttributeValues[] attributesValues = ei.DBAttributable.GetInitAttributesValues(dictionary.Keys.ToArray<int>());
    foreach (KeyValuePair<int, List<IAttributeEditor>> keyValuePair in dictionary)
    {
      KeyValuePair<int, List<IAttributeEditor>> pair = keyValuePair;
      AttributeValues av = ((IEnumerable<AttributeValues>) attributesValues).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == pair.Key));
      if (av.ReadOnly)
      {
        pair.Value.ForEach((Action<IAttributeEditor>) (x =>
        {
          x.Values = (AttributeValues) null;
          x.Modified = false;
        }));
      }
      else
      {
        object[] values = av.Values;
        bool modifyInLoad = values != null && values.Length == 1 && !string.IsNullOrEmpty(Convert.ToString(values[0]));
        if (this._dictServerSrv != null)
        {
          this.CheckParsedAttributeValues(ei, av);
          this.CheckForParsedAttributeValues(ei, av);
        }
        foreach (IAttributeEditor editor in pair.Value)
        {
          if (editor is AttrMeasuredEdit attrMeasuredEdit)
          {
            attrMeasuredEdit.AllowDefaultValue = true;
            this.AddAttributeValues(iSession, ei, av, editor);
            if (attrMeasuredEdit.ModifiedInLoad)
              this.ModifiedInLoad = true;
          }
          else
            this.AddAttributeValues(iSession, ei, av, editor, modifyInLoad);
        }
        attributeValues.Add(av);
        ei.TempAttrIDs.Add(av.AttributeID);
        if (modifyInLoad && !this.ModifiedInLoad)
          this.ModifiedInLoad = true;
      }
    }
  }

  /// <summary>Закончена загрузка данных в контролы.</summary>
  private void OnLoadDataCompleted()
  {
    if (this.LoadDataCompleted != null)
      this.LoadDataCompleted((object) this, EventArgs.Empty);
    if (ServicesManager.GetService(typeof (IFormDesignerEventsManager)) is FormDesignerEventsManager service)
      service.DataLoaded((object) this, new EventArgs());
    if (this._eventsMngr == null || this.FormDesignerEvents == null)
      return;
    foreach (FormDesignerAction formDesignerEvent in this.FormDesignerEvents)
    {
      if (this._eventsMngr.GetEvent(formDesignerEvent.ActionGuid) is IFormDesignerFormEventsHandler formEventsHandler)
        formEventsHandler.DataLoaded((object) this);
    }
  }

  /// <summary>Занести данные атрибута в контрол.</summary>
  /// <param name="iSession">Сессия пользователя</param>
  /// <param name="ei">Информация об объекте/связи</param>
  /// <param name="av">Данные атрибута</param>
  /// <param name="editor">Элемент управления</param>
  private void AddAttributeValues(
    IUserSession iSession,
    DesForm.ElementInformation ei,
    AttributeValues av,
    IAttributeEditor editor,
    bool modifyInLoad = false)
  {
    ILockModify lockModify = editor as ILockModify;
    if (editor is AttrMeasuredEdit && ((AttrMeasuredEdit) editor).AllowDefaultValue)
      lockModify = (ILockModify) null;
    if (lockModify != null)
      lockModify.LockModify = true;
    try
    {
      if (editor is IExtendedParent4Control extendedParent4Control)
        extendedParent4Control.ParentTypeID = ei.TypeID;
      (editor as IParent4Control).ParentInfo = ei.Info;
      if (av != null && (av.MultipleValued == MultiValueModes.SingleValueFromList || av.MultipleValued == MultiValueModes.MultiValuesFromList))
      {
        IDBAttributeType attributeType = iSession.GetAttributeType(av.AttributeGuid, false);
        DataTable possibleValues = this.GetPossibleValues(attributeType, ei);
        editor.SetPossibleValues(possibleValues, attributeType.PossibleValueFieldName, "F_DESCRIPTION");
      }
      editor.Values = av;
      if (modifyInLoad && lockModify != null)
        lockModify.LockModify = false;
      editor.Modified = modifyInLoad;
    }
    finally
    {
      if (lockModify != null)
        lockModify.LockModify = false;
    }
    if (av == null || !ei.ParsedAVs.Contains(av) && (string.IsNullOrEmpty(av.AttributeAlias) || !ei.ForParsedAVs.ContainsKey(av.AttributeAlias)) || !(editor is ICompletionOfEditing completionOfEditing))
      return;
    completionOfEditing.CompletionOfEditingEvent -= new EventHandler(this.OnEditor_CompletionOfEditingEvent);
    completionOfEditing.CompletionOfEditingEvent += new EventHandler(this.OnEditor_CompletionOfEditingEvent);
  }

  /// <summary>
  /// Проверить атрибут на необходимость расшифровки значения.
  /// </summary>
  /// <param name="ei">Информация об объекте/связи</param>
  /// <param name="av">Атрибут</param>
  private void CheckParsedAttributeValues(DesForm.ElementInformation ei, AttributeValues av)
  {
    if (av.AttributeID <= 0 || av.MultipleValued == MultiValueModes.MultiValues || av.MultipleValued == MultiValueModes.MultiValuesFromList)
      return;
    switch (av.AttributeType)
    {
      case FieldTypes.ftString:
      case FieldTypes.ftMemo:
        if ((this.GetAttributeOptions(ei.Info.ElementKind, ei.TypeID, av.AttributeID) & AttributeOptions.GetDescriptionEvent) != AttributeOptions.GetDescriptionEvent)
          break;
        ei.ParsedAVs.Add(av);
        break;
    }
  }

  /// <summary>
  /// Проверить атрибут на возможность участвовать в расшифровке значения других атрибутов.
  /// </summary>
  /// <param name="ei">Информация об объекте/связи</param>
  /// <param name="av">Атрибут</param>
  private void CheckForParsedAttributeValues(DesForm.ElementInformation ei, AttributeValues av)
  {
    if (av.AttributeID <= 0 || av.MultipleValued == MultiValueModes.MultiValues || av.MultipleValued == MultiValueModes.MultiValuesFromList)
      return;
    FieldTypes attributeType = av.AttributeType;
    if (string.IsNullOrEmpty(av.AttributeAlias) || attributeType != FieldTypes.ftDateTime && attributeType != FieldTypes.ftDouble && attributeType != FieldTypes.ftGuid && attributeType != FieldTypes.ftInteger && attributeType != FieldTypes.ftMeasured && attributeType != FieldTypes.ftObjectLink && attributeType != FieldTypes.ftObjectLinkByID && attributeType != FieldTypes.ftString)
      return;
    ei.ForParsedAVs.Add(av.AttributeAlias, av);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="ei"></param>
  private void ParseAttributeValues(
    IUserSession session,
    IDictionaryServerService dictionaryService,
    DesForm.ElementInformation ei)
  {
    if (dictionaryService == null || ei.ParsedAVs.Count <= 0)
      return;
    System.Collections.Generic.Dictionary<string, AttributeValues> forParseDict = new System.Collections.Generic.Dictionary<string, AttributeValues>();
    foreach (AttributeValues attributeValues in ei.ForParsedAVs.Values)
    {
      if (attributeValues?.Values != null && attributeValues.Values.Length != 0 && !string.IsNullOrEmpty(Convert.ToString(attributeValues.Values[0])))
        forParseDict.Add(attributeValues.AttributeAlias, attributeValues);
    }
    ei.ParsedAVs = dictionaryService.ParseAttributes(session.SessionGUID, ei.ParsedAVs, forParseDict);
  }

  /// <summary>Получение списка атрибутов для объекта/связи.</summary>
  /// <param name="ei">Информация об объекте/связи</param>
  /// <param name="mode">Режим загрузки</param>
  /// <returns>Список атрибутов</returns>
  private List<AttributeValues> GetAttributeValues(DesForm.ElementInformation ei, RefreshMode mode)
  {
    List<AttributeValues> attributeValuesList;
    if (ei.ap.Id != ei.Info.ElementIdentifier || !ei.ap.Loaded || mode == RefreshMode.Forced)
    {
      AttributeValues[] source = ei.ap.Load((object) ei.DBAttributable, ei.Info.ElementKind, this._modes);
      attributeValuesList = source != null ? ((IEnumerable<AttributeValues>) source).ToList<AttributeValues>() : (List<AttributeValues>) null;
    }
    else
      attributeValuesList = (List<AttributeValues>) ei.ap.ActualAttributeValues;
    return attributeValuesList ?? new List<AttributeValues>(0);
  }

  /// <summary>Получение опций атрибута.</summary>
  /// <param name="kind">Тип элемента (объект/связь)</param>
  /// <param name="typeID">Идентификато типа элемента</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>Опции</returns>
  private AttributeOptions GetAttributeOptions(AttributableElements kind, int typeID, int attrID)
  {
    IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
    switch (kind)
    {
      case AttributableElements.Object:
        imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(typeID, attrID);
        break;
      case AttributableElements.Relation:
        imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(typeID, attrID);
        break;
    }
    AttributeOptions attributeOptions;
    if (imsAttribute4 != null)
    {
      attributeOptions = imsAttribute4.Options;
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
      attributeOptions = attributeType != null ? attributeType.Options : AttributeOptions.None;
    }
    return attributeOptions;
  }

  /// <summary>Получить список возможных значений.</summary>
  /// <param name="attrType">Информация о типе атрибута</param>
  /// <param name="ei">Рассматриваемый элемент</param>
  /// <returns>Таблица возможных значений</returns>
  private DataTable GetPossibleValues(IDBAttributeType attrType, DesForm.ElementInformation ei)
  {
    DataTable possibleValues = (DataTable) null;
    if (attrType != null)
    {
      possibleValues = attrType.GetPossibleValues();
      if (possibleValues != null && (this.GetAttributeOptions(ei.Info.ElementKind, ei.TypeID, attrType.AttributeID) & AttributeOptions.DisableNulls) == AttributeOptions.None)
      {
        DataRow row = possibleValues.NewRow();
        row[attrType.PossibleValueFieldName] = (object) DBNull.Value;
        row["F_DESCRIPTION"] = (object) string.Empty;
        possibleValues.Rows.InsertAt(row, 0);
      }
    }
    return possibleValues;
  }

  /// <summary>
  /// Заменить значения в старом списке атрибутов на значения в новом списке.
  /// </summary>
  /// <param name="values">Старый список атрибутов</param>
  /// <param name="newValues">Новый список атрибутов</param>
  private void UpdateAttributeValues(List<AttributeValues> values, List<AttributeValues> newValues)
  {
    if (newValues == null || values == null || values.Count <= 0)
      return;
    foreach (AttributeValues newValue in newValues)
    {
      int index = values.IndexOf(newValue);
      if (index != -1)
        values[index] = newValue;
    }
  }

  /// <summary>
  /// Выполнение у контрола дополнительных действий перед сохранением значения.
  /// </summary>
  /// <remarks>Перед сохранением данных у контрола необходимо выполнить некоторые действия.
  /// В частности, создавалось по просьбе О.Лембиевского для его контрола, в котором перед сохранением необходимо проверять правильность заполнения.</remarks>
  public void ValidateBeforeSave()
  {
    this.OnValidateBeforeSave<IAttributeEditor>((ICollection<IAttributeEditor>) this._EI.LinkedEditors);
    this.OnValidateBeforeSave<IAttributeEditor>((ICollection<IAttributeEditor>) this._relEI.LinkedEditors);
    this.OnValidateBeforeSave<IMultipleAttributeEditor>((ICollection<IMultipleAttributeEditor>) this._multiEditors);
    this.OnValidateBeforeSave<AttrButton>((ICollection<AttrButton>) this.AttrButtons);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  private void OnValidateBeforeSave<T>(ICollection<T> collection)
  {
    foreach (T obj in (IEnumerable<T>) collection)
    {
      if (obj is IValidateBeforeSave validateBeforeSave)
        validateBeforeSave.Validate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void CheckImageFromLibraryAttribute()
  {
    List<object> objectList = new List<object>();
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is IImageFromLibrary imageFromLibrary && !(imageFromLibrary.ImageFromLibrary == Guid.Empty) && !objectList.Contains((object) imageFromLibrary.ImageFromLibrary))
        objectList.Add((object) imageFromLibrary.ImageFromLibraryID);
    }
    Guid guid = new Guid("cad014b6-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this.FormID, false);
      if (objectActualCopy == null)
        return;
      IDBAttributeCollection attributes = objectActualCopy.Attributes;
      IDBAttribute byGuid = attributes.FindByGUID(guid);
      if (byGuid == null && objectList.Count <= 0)
        return;
      if (byGuid == null && objectList.Count > 0)
        attributes.AddAttribute(attributeTypeId, false, objectList.ToArray());
      else if (byGuid != null && objectList.Count == 0)
        byGuid.Delete(0L);
      else
        byGuid.Values = objectList.ToArray();
    }
  }

  /// <summary>
  /// Сброс индекса текущих страниц у таб контрола на 0 / восстановление.
  /// 
  /// Пример вызова:
  ///      ResetTabControlIndicesToZero( true )
  /// try
  /// {
  /// }
  /// finally
  /// {
  ///     ResetTabControlIndicesToZero( false )
  /// }
  /// </summary>
  /// <param name="saveOrRestore">true to save, false to restore</param>
  public void ResetTabControlIndicesToZero(bool _SaveOrRestore)
  {
    if (_SaveOrRestore)
    {
      List<TabControl> _tabControls = new List<TabControl>();
      this.tabControlsInfo.Clear();
      this.CollectTabControls(this.tabControlsInfo, this.Controls);
      this.CollectTabControls(_tabControls, this.Controls);
      this.SetTabControlIndicesToZero(_tabControls);
    }
    else
    {
      this.RestoreTabControlsInfo(this.tabControlsInfo);
      this.tabControlsInfo.Clear();
    }
  }

  /// <summary>Сбор информации по TabControl</summary>
  /// <param name="_tabControlsInfo"></param>
  /// <param name="controls"></param>
  private void CollectTabControls(
    List<Tuple<TabControl, int>> _tabControlsInfo,
    Control.ControlCollection controls)
  {
    foreach (Control control in (ArrangedElementCollection) controls)
    {
      if (control is TabControl)
      {
        TabControl tabControl = (TabControl) control;
        _tabControlsInfo.Add(new Tuple<TabControl, int>(tabControl, tabControl.SelectedIndex));
        if (tabControl.SelectedIndex != -1)
        {
          this.CollectTabControls(_tabControlsInfo, tabControl.SelectedTab.Controls);
          for (int index = 0; index < tabControl.TabPages.Count; ++index)
          {
            if (index != tabControl.SelectedIndex)
              this.CollectTabControls(_tabControlsInfo, tabControl.TabPages[index].Controls);
          }
        }
      }
      else
        this.CollectTabControls(_tabControlsInfo, control.Controls);
    }
  }

  /// <summary>Сбор информации по TabControl</summary>
  /// <param name="_tabControls"></param>
  /// <param name="controls"></param>
  private void CollectTabControls(List<TabControl> _tabControls, Control.ControlCollection controls)
  {
    foreach (Control control in (ArrangedElementCollection) controls)
    {
      if (control is TabControl)
        _tabControls.Add((TabControl) control);
      this.CollectTabControls(_tabControls, control.Controls);
    }
  }

  private void SetTabControlIndicesToZero(List<TabControl> _tabControls)
  {
    for (int index = _tabControls.Count - 1; index >= 0; --index)
    {
      if (_tabControls[index].SelectedIndex != 0)
        _tabControls[index].SelectTab(0);
    }
  }

  private void RestoreTabControlsInfo(List<Tuple<TabControl, int>> _tabControlsInfo)
  {
    for (int index = _tabControlsInfo.Count - 1; index >= 0; --index)
    {
      if (_tabControlsInfo[index].Item1.SelectedIndex != _tabControlsInfo[index].Item2)
        _tabControlsInfo[index].Item1.SelectTab(_tabControlsInfo[index].Item2);
    }
  }

  /// <summary>Сохранение значений атрибутов для объекта/связи.</summary>
  /// <param name="blankMode">Признак того что форма работает с заготовкой</param>
  /// <returns>Результат сохранения. Если true - данные сохранены, false - произошла ошибка</returns>
  public bool SaveAttributes(bool blankMode = false)
  {
    bool flag = true;
    this._inSaveLoad = true;
    try
    {
      List<AttributeValues> changedValues1 = (List<AttributeValues>) null;
      List<AttributeValues> changedValues2 = (List<AttributeValues>) null;
      long objectID = 0;
      long relationID = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        try
        {
          bool bCheckModified1 = !this.CheckPinExchange(sessionKeeper.Session, this._EI);
          if (this._EI.DBAttributable == null)
          {
            if (this._EI.Info.ElementKind == AttributableElements.Object)
              this._EI.DBAttributable = (IDBAttributable) sessionKeeper.Session.GetObjectActualCopy(this._EI.Info.ElementIdentifier, false);
            else if (this._EI.Info.ElementKind == AttributableElements.Relation)
              this._EI.DBAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(this._EI.Info.ElementIdentifier, false);
          }
          AttributeValues[] attributesValues = this._EI.DBAttributable.GetAttributesValues(this._modes & ~GetAttributeValuesModes.CheckVisibility);
          if (this.SaveInfo(this._EI, out changedValues1, bCheckModified1) && !this._EI.invokeProcessor)
          {
            if (this._EI.Info.ElementKind == AttributableElements.Object)
              objectID = this._EI.Info.ElementIdentifier;
            else
              relationID = this._EI.Info.ElementIdentifier;
          }
          AttributeValuesList attributeValuesList1 = (AttributeValuesList) null;
          if (this._relEI.Info != null)
          {
            bool bCheckModified2 = !this.CheckPinExchange(sessionKeeper.Session, this._relEI);
            if (this._relEI.DBAttributable == null)
              this._relEI.DBAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(this._relEI.Info.ElementIdentifier, false);
            attributeValuesList1 = this._relEI.ap.ActualAttributeValues.Clone() as AttributeValuesList;
            relationID = !this.SaveInfo(this._relEI, out changedValues2, bCheckModified2) || this._relEI.invokeProcessor ? 0L : this._relEI.Info.ElementIdentifier;
          }
          foreach (IMultipleAttributeEditor multiEditor in this._multiEditors)
          {
            if (multiEditor.Modified)
              multiEditor.Save();
          }
          if (this._EI.DuplicationEditors.Count > 0)
            this.UpdateDublicationAV(changedValues1, this._EI.DuplicationEditors);
          if (this._relEI.DuplicationEditors.Count > 0)
            this.UpdateDublicationAV(changedValues2, this._relEI.DuplicationEditors);
          this._isModified = false;
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          {
            if (objectID != 0L)
            {
              NotificationEventArgs e = (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", objectID, this._EI.TypeID, ((IEnumerable<AttributeValues>) attributesValues).ToArray<AttributeValues>(), changedValues1.ToArray());
              if (blankMode)
                (e as DBObjectsExtendedEventArgs).VerType = ObjectRecordKind.Blank;
              service.FireEvent((object) this, e);
            }
            if (relationID != 0L)
            {
              if (this._relEI.Info == null)
              {
                service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsExtendedEventArgs("RelationsChanged", relationID, this._EI.TypeID, ((IEnumerable<AttributeValues>) attributesValues).ToArray<AttributeValues>(), changedValues1.ToArray()));
              }
              else
              {
                AttributeValuesList attributeValuesList2 = attributeValuesList1 ?? new AttributeValuesList(0);
                service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsExtendedEventArgs("RelationsChanged", relationID, this._relEI.TypeID, attributeValuesList2.ToArray(), changedValues2.ToArray()));
              }
            }
          }
        }
        finally
        {
          this._EI.DBAttributable = this._relEI.DBAttributable = (IDBAttributable) null;
        }
      }
    }
    catch (AttributeProcessorException ex)
    {
      flag = false;
      throw !string.IsNullOrEmpty(ex.AddiotionelMsg) ? new AttributeProcessorException(ex.AddiotionelMsg) : ex;
    }
    catch (DesForm.DataFormatErrorException ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Msg, ex.Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    catch (Exception ex)
    {
      flag = false;
      throw;
    }
    finally
    {
      this._inSaveLoad = false;
      this.CheckButtonsState();
    }
    return flag;
  }

  /// <summary>
  /// Замена идентификатора старого объекта на идентификатор нового объекта (при наличии замены).
  /// </summary>
  /// <param name="session"></param>
  /// <param name="ei"></param>
  /// <returns></returns>
  private bool CheckPinExchange(IUserSession session, DesForm.ElementInformation ei)
  {
    bool flag = false;
    if (this.PinExchange.ContainsKey(ei.Info.ElementIdentifier))
    {
      long num = this.PinExchange[ei.Info.ElementIdentifier];
      ei.Info = (IElementInfo) new ElementInfo(num, ei.Info.ElementKind);
      if (ei.Info.ElementKind == AttributableElements.Object)
        ei.DBAttributable = (IDBAttributable) session.GetObjectActualCopy(num, false);
      else if (ei.Info.ElementKind == AttributableElements.Relation)
        ei.DBAttributable = (IDBAttributable) session.GetRelation(num, false);
      if (ei.DBAttributable != null)
        ei.ap.Load((object) ei.DBAttributable, ei.Info.ElementKind, this._modes);
      flag = true;
    }
    return flag;
  }

  /// <summary>Сохранение данных.</summary>
  /// <param name="ei">Информация об объекте/связи</param>
  /// <param name="changedValues">Список значений</param>
  /// <param name="bCheckModified"></param>
  /// <returns>Результат сохранения</returns>
  private bool SaveInfo(
    DesForm.ElementInformation ei,
    out List<AttributeValues> changedValues,
    bool bCheckModified)
  {
    bool flag1 = false;
    List<IAttributeEditor> modifiedEditors = new List<IAttributeEditor>();
    changedValues = this.GetModifiedEditorsValues(ei, modifiedEditors, bCheckModified);
    if (this._newTypeID != -1 && this._newTypeID != this._EI.TypeID && ei.DBAttributable != null)
    {
      if (ei.DBAttributable is IDBObject dbAttributable2)
      {
        dbAttributable2.ObjectType = this._newTypeID;
        this._EI.DBAttributable = (IDBAttributable) dbAttributable2;
        flag1 = true;
      }
      else if (ei.DBAttributable is IDBRelation dbAttributable1)
      {
        dbAttributable1.RelationType = this._newTypeID;
        this._EI.DBAttributable = (IDBAttributable) dbAttributable1;
        flag1 = true;
      }
    }
    if (this._newProjID != this._oldProjID && ei.Info.ElementKind == AttributableElements.Object)
    {
      if (ei.DBAttributable is IDBObject dbAttributable3)
        dbAttributable3.ProjectID = this._newProjID;
      this._oldProjID = this._newProjID;
      flag1 = true;
    }
    if (this._newOwnerID != 0L)
    {
      if (ei.DBAttributable is IDBObject dbAttributable4)
        dbAttributable4.OwnerID = this._newOwnerID;
      flag1 = true;
    }
    changedValues.AddRange((IEnumerable<AttributeValues>) ei.AdditionalValues.ToArray());
    if (changedValues.Count > 0)
    {
      ei.ap.SetAttributeValuesArray(new AttributeValuesList((IEnumerable<AttributeValues>) changedValues));
      AttributeValues[] attributeValuesArray = ei.ap.Save();
      ei.AdditionalValues.Clear();
      flag1 = true;
      foreach (AttributeValues attributeValues in changedValues)
      {
        if (attributeValues.Values != null && attributeValues.Values.Length != 0 && attributeValues.Values[0] is DeleteModesEnum)
          attributeValues.Values[0] = (object) DBNull.Value;
      }
      if (attributeValuesArray != null)
      {
        foreach (AttributeValues attributeValues in attributeValuesArray)
        {
          if (!changedValues.Contains(attributeValues))
            changedValues.Add(attributeValues);
        }
      }
      changedValues.ForEach((Action<AttributeValues>) (x => ei.TempAttrIDs.Remove(x.AttributeID)));
    }
    bool flag2 = flag1 || modifiedEditors.Count > 0;
    for (int index = 0; index < modifiedEditors.Count; ++index)
      modifiedEditors[index].Modified = false;
    this._newTypeID = -1;
    return flag2;
  }

  /// <summary>
  /// Сбор значений AttributeValues у конторолов, в которых изменились данные.
  /// </summary>
  /// <param name="ei"></param>
  /// <param name="modifiedEditors">Список контролов в которых изменялась информация</param>
  /// <param name="bCheckModified">Сохранить все значения (не учитывать наличие изменений в контроле)
  /// Нужно для Workflow когда при сохранении один объект подменяется другим</param>
  /// <returns></returns>
  private List<AttributeValues> GetModifiedEditorsValues(
    DesForm.ElementInformation ei,
    List<IAttributeEditor> modifiedEditors,
    bool bCheckModified)
  {
    List<AttributeValues> modifiedEditorsValues = new List<AttributeValues>();
    foreach (IAttributeEditor linkedEditor in ei.LinkedEditors)
    {
      if (!bCheckModified || linkedEditor.Modified)
      {
        AttributeValues values = linkedEditor.Values;
        if (values != null)
        {
          modifiedEditors.Add(linkedEditor);
          if (values.Values.Length == 0 || values.Values.Length == 1 && (values.Values[0] == DBNull.Value || values.Values[0] == null))
          {
            if (!ei.TempAttrIDs.Contains(values.AttributeID))
            {
              if (ei.DBAttributable != null)
              {
                if (ei.DBAttributable.GetAttributeByGuid(values.AttributeGuid) != null)
                {
                  IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
                  if (ei.Info.ElementKind == AttributableElements.Object)
                    imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(ei.TypeID, values.AttributeID);
                  else if (ei.Info.ElementKind == AttributableElements.Relation)
                    imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(ei.TypeID, values.AttributeID);
                  if (imsAttribute4 == null || imsAttribute4.Required != RequiredModes.AutoRequired)
                  {
                    values.Values = new object[1]
                    {
                      (object) DeleteModesEnum.None
                    };
                    modifiedEditorsValues.Add(values);
                    continue;
                  }
                  if (linkedEditor is IDataFormatError dataFormatError && dataFormatError.IsDataFormatError)
                  {
                    string msg = LocalizationHolder.rm.GetString("Client.Core.FormDesigner.DataFormatError");
                    throw new DesForm.DataFormatErrorException(LocalizationHolder.rm.GetString("Client.Core_1149"), msg);
                  }
                }
                else
                  continue;
              }
            }
            else
              continue;
          }
          if (string.IsNullOrEmpty(values.AttributeName))
            values.AttributeName = MetaDataHelper.GetAttributeTypeName(values.AttributeID);
          if (values.AttributeID == -7 && values.Values[0] != DBNull.Value && values.Values[0] != null)
            this._newTypeID = (int) values.Values[0];
          else if (values.AttributeID == -14 && values.Values[0] != DBNull.Value && values.Values[0] != null)
            this._newProjID = Convert.ToInt64(values.Values[0]);
          else if (values.AttributeID == -8 && values.Values[0] != DBNull.Value && values.Values[0] != null)
            this._newOwnerID = Convert.ToInt64(values.Values[0]);
          else
            modifiedEditorsValues.Add(values);
        }
      }
    }
    return modifiedEditorsValues;
  }

  /// <summary>
  /// Обновление контролов, которые связаны с одним атрибутом.
  /// </summary>
  /// <remarks>На форме несколько контролов могут быть связаны с одним атрибутом.
  /// Может возникнуть ситуация, когда значение было изменено в одном контроле и после сохранения изменений в другом контроле останется старое значение.
  /// В частности это проявляется если контролы связаны с атрибутом связи.
  /// Поэтому, после сохранения нужно пройтись по "дублирующимся" контролам и обновить их значения.
  /// Через нотификацию это сделать нельзя, потому что возникает момент когда форма получает уведомления от себя же.
  /// При этом может происходить обращение к удаленным объекта и возникнет ошибка.</remarks>
  /// <param name="changedValues">Измененные значения</param>
  /// <param name="duplicationAV">Список дублирующихся контролов</param>
  private void UpdateDublicationAV(
    List<AttributeValues> changedValues,
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> duplicationAV)
  {
    foreach (AttributeValues changedValue in changedValues)
    {
      AttributeValues av = changedValue;
      if (duplicationAV.ContainsKey(av.AttributeID))
        duplicationAV[av.AttributeID].ForEach((Action<IAttributeEditor>) (x =>
        {
          x.Values = av;
          x.Modified = false;
        }));
    }
  }

  /// <summary>
  /// Проверка кнопок, расположенных на форме, на доступность.
  /// </summary>
  /// <remark>Необходимо вызвать после загрузки данных (например, чтобы отключить кнопку "Применить", т.к. изменений еще не было).
  /// А также, необходимо вызвать после появления изменений и после их сохранения.</remark>
  private void CheckButtonsState()
  {
    if (this._btnMngr == null)
      return;
    foreach (AttrButton attrButton in this.AttrButtons)
    {
      IFormDesignerActionHandler action = this._btnMngr.GetAction((object) attrButton.FormDesignerAction);
      attrButton.Enabled = action != null && action.ButtonEnabled((object) attrButton, (object) this);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editor"></param>
  /// <param name="value"></param>
  private void SetEditorEnabled(IAttributeEditor editor, bool value)
  {
    if (editor is IIMControlEnabled imControlEnabled)
      imControlEnabled.EnabledCtrl = value;
    else
      (editor as Control).Enabled = value;
  }

  /// <summary>
  /// Получение текущих значений атрибутов, связанных с объектом/связью.
  /// </summary>
  /// <param name="ID">Идентификатор объекта/связи</param>
  /// <returns>Список значений атрибутов</returns>
  public List<AttributeValues> GetAttributeValuesFromControls(long ID)
  {
    List<AttributeValues> valuesFromControls = (List<AttributeValues>) null;
    if (this._EI.Info.ElementIdentifier == ID)
      valuesFromControls = this.GetAttributeValuesFromControls(this._EI.DuplicationEditors);
    else if (this._relEI.Info != null && this._relEI.Info.ElementIdentifier == ID)
      valuesFromControls = this.GetAttributeValuesFromControls(this._relEI.DuplicationEditors);
    return valuesFromControls;
  }

  /// <summary>
  /// Получение текущих значений атрибутов, связанных с объектом/связью.
  /// </summary>
  /// <param name="duplicationEditors">Список всех контролов связанных с атрибутами объекта/связи</param>
  /// <returns>Список значений атрибутов</returns>
  private List<AttributeValues> GetAttributeValuesFromControls(
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> duplicationEditors)
  {
    List<AttributeValues> valuesFromControls = new List<AttributeValues>(duplicationEditors.Count);
    foreach (KeyValuePair<int, List<IAttributeEditor>> duplicationEditor in duplicationEditors)
    {
      AttributeValues attributeValues = (AttributeValues) null;
      for (int index = 0; index < duplicationEditor.Value.Count; ++index)
      {
        IAttributeEditor attributeEditor = duplicationEditor.Value[index];
        AttributeValues values = attributeEditor.Values;
        if (values != null)
        {
          attributeValues = values;
          if (attributeEditor.Modified)
            break;
        }
      }
      if (attributeValues != null)
        valuesFromControls.Add(attributeValues);
    }
    return valuesFromControls;
  }

  /// <summary>Получение списка дополнительных значений.</summary>
  /// <param name="ID">Идентификатор объекта/связи</param>
  /// <returns>Список дополнительных значений</returns>
  public List<AttributeValues> GetAdditionalValues(long ID)
  {
    if (ID == this._EI.Info.ElementIdentifier)
      return this._EI.AdditionalValues;
    return ID != this._relEI.Info.ElementIdentifier ? (List<AttributeValues>) null : this._relEI.AdditionalValues;
  }

  /// <summary>
  /// Информация об используемых редакторах IAttributeEditor привязанных к объектам/связям.
  /// </summary>
  /// <remarks>Использовать вместо функции GetPinInformation</remarks>
  /// <param name="ID">Идентификатор объекта/связи</param>
  /// <returns></returns>
  public System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> GetEditors(long ID)
  {
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> dictionary = (System.Collections.Generic.Dictionary<int, List<IAttributeEditor>>) null;
    if (this._EI.Info.ElementIdentifier == ID)
    {
      dictionary = new System.Collections.Generic.Dictionary<int, List<IAttributeEditor>>(this._EI.LinkedEditors.Count);
      foreach (IAttributeEditor linkedEditor in this._EI.LinkedEditors)
      {
        AttributeValues values = linkedEditor.Values;
        if (values != null)
        {
          if (dictionary.ContainsKey(values.AttributeID))
            dictionary[values.AttributeID].Add(linkedEditor);
          else
            dictionary.Add(values.AttributeID, new List<IAttributeEditor>()
            {
              linkedEditor
            });
        }
      }
    }
    else if (this._relEI.Info != null && this._relEI.Info.ElementIdentifier == ID)
    {
      dictionary = new System.Collections.Generic.Dictionary<int, List<IAttributeEditor>>(this._relEI.LinkedEditors.Count);
      foreach (IAttributeEditor linkedEditor in this._relEI.LinkedEditors)
      {
        AttributeValues values = linkedEditor.Values;
        if (values != null)
        {
          if (dictionary.ContainsKey(values.AttributeID))
            dictionary[values.AttributeID].Add(linkedEditor);
          else
            dictionary.Add(values.AttributeID, new List<IAttributeEditor>()
            {
              linkedEditor
            });
        }
      }
    }
    return dictionary ?? new System.Collections.Generic.Dictionary<int, List<IAttributeEditor>>(0);
  }

  /// <summary>Список всех контролов связанных с атрибутами.</summary>
  /// <returns></returns>
  public List<IAttributeEditor> GetLinkedControls()
  {
    return this._EI.LinkedEditors.Concat<IAttributeEditor>((IEnumerable<IAttributeEditor>) this._relEI.LinkedEditors).ToList<IAttributeEditor>();
  }

  /// <summary>Добавить дополнительные значения атрибутов.</summary>
  /// <param name="ID">Идентификатор объекта/связи</param>
  /// <param name="values">Значения</param>
  /// <param name="replace">Заменить старые значения новыми</param>
  public void SetAdditionalValues(long ID, List<AttributeValues> values, bool replace)
  {
    if (values == null || values.Count <= 0)
      return;
    DesForm.ElementInformation elementInformation = ID == this._EI.Info.ElementIdentifier ? this._EI : (ID == this._relEI.Info.ElementIdentifier ? this._relEI : (DesForm.ElementInformation) null);
    if (elementInformation == null)
      return;
    if (replace)
    {
      elementInformation.AdditionalValues.Clear();
      elementInformation.AdditionalValues.AddRange((IEnumerable<AttributeValues>) values);
    }
    else
      elementInformation.MargeAdditionalValues(values);
    this.OnControl_Modified((object) this, EventArgs.Empty);
  }

  /// <summary>Деактивация формы.</summary>
  /// <remark>Вызывается вьюшкой при ее деактивации.</remark>
  public void SetFormDeactivate()
  {
    this.IsFormActivated = false;
    if (this.FormDeactivate != null)
      this.FormDeactivate((object) this, EventArgs.Empty);
    if (this._eventsMngr == null || this.FormDesignerEvents == null)
      return;
    foreach (FormDesignerAction formDesignerEvent in this.FormDesignerEvents)
    {
      if (this._eventsMngr.GetEvent(formDesignerEvent.ActionGuid) is IFormDesignerFormEventsHandler formEventsHandler)
        formEventsHandler.Deactivate((object) this);
    }
  }

  /// <summary>
  /// При выборе в контролах значений для мастер атрибутов возникает необходимость обновить значения связанных с ним атрибутов.
  /// </summary>
  /// <param name="elementInfo"></param>
  /// <param name="attrValueeList"></param>
  public void UpdateSlaveAttribute(IElementInfo elementInfo, AttributeValuesList attrValueeList)
  {
    if (elementInfo == null || attrValueeList == null || attrValueeList.Count <= 0)
      return;
    System.Collections.Generic.Dictionary<Guid, List<IAttributeEditor>> dictionary = new System.Collections.Generic.Dictionary<Guid, List<IAttributeEditor>>();
    if (this._EI.Info.ElementIdentifier == elementInfo.ElementIdentifier)
    {
      foreach (IAttributeEditor linkedEditor in this._EI.LinkedEditors)
      {
        Guid attributeGuid = linkedEditor.AttributeInfo.AttributeGuid;
        if (dictionary.ContainsKey(attributeGuid))
          dictionary[attributeGuid].Add(linkedEditor);
        else
          dictionary.Add(attributeGuid, new List<IAttributeEditor>()
          {
            linkedEditor
          });
      }
    }
    else if (this._relEI.Info != null && this._relEI.Info.ElementIdentifier == elementInfo.ElementIdentifier)
    {
      foreach (IAttributeEditor linkedEditor in this._relEI.LinkedEditors)
      {
        Guid attributeGuid = linkedEditor.AttributeInfo.AttributeGuid;
        if (dictionary.ContainsKey(attributeGuid))
          dictionary[attributeGuid].Add(linkedEditor);
        else
          dictionary.Add(attributeGuid, new List<IAttributeEditor>()
          {
            linkedEditor
          });
      }
    }
    if (dictionary.Count <= 0)
      return;
    foreach (AttributeValues attrValuee in (List<AttributeValues>) attrValueeList)
    {
      AttributeValues entry = attrValuee;
      if (dictionary.ContainsKey(entry.AttributeGuid))
        dictionary[entry.AttributeGuid].ForEach((Action<IAttributeEditor>) (x => x.Values = entry));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public FormDesignerAction[] AttributeChangingEvents { get; set; }

  /// <summary>Для изменения значения в контроле.</summary>
  /// <param name="attrID"></param>
  /// <param name="oldValues"></param>
  /// <param name="newValues"></param>
  /// <param name="isBase"></param>
  public void AttributeChanging(int attrID, object[] oldValues, object[] newValues, bool isBase)
  {
    if (this.Disposing || !this._isModified)
      return;
    DesForm.ElementInformation elementInformation = isBase ? this._EI : this._relEI;
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> duplicationEditors = elementInformation.DuplicationEditors;
    AttributeValues attributeValues = (AttributeValues) null;
    AttributeValues newValue;
    if (duplicationEditors.Count > 0 && duplicationEditors.ContainsKey(attrID))
    {
      AttributeValues values = duplicationEditors[attrID][0].Values;
      values.Values = oldValues;
      newValue = values.Clone() as AttributeValues;
      newValue.Values = newValues;
    }
    else
    {
      if (elementInformation.AdditionalValues != null)
        attributeValues = elementInformation.AdditionalValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
      if (attributeValues != null)
      {
        attributeValues.Values = oldValues;
        newValue = attributeValues.Clone() as AttributeValues;
        newValue.Values = newValues;
      }
      else
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
        newValue = new AttributeValues(attrID, attributeType.FieldType, attributeType.MultiValueMode, newValues);
      }
    }
    AttributeChangingEventArgs eventArgs = this.GetEventArgs(newValue, isBase);
    this.FireEvents(eventArgs);
    this.Update(this._EI, eventArgs.NewObjectAttributes);
    this.Update(this._relEI, eventArgs.NewRelationAttributes);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newValue"></param>
  /// <param name="isBase"></param>
  /// <returns></returns>
  private AttributeChangingEventArgs GetEventArgs(AttributeValues newValue, bool isBase)
  {
    AttributeChangingEventArgs eventArgs = new AttributeChangingEventArgs();
    eventArgs.FormID = this.FormID;
    eventArgs.ObjectID = this._EI.Info.ElementIdentifier;
    eventArgs.ObjectTypeID = this._EI.TypeID;
    IEnumerable<AttributeValues> first1 = this._EI.DuplicationEditors.Where<KeyValuePair<int, List<IAttributeEditor>>>((System.Func<KeyValuePair<int, List<IAttributeEditor>>, bool>) (x => x.Value[0].Values != null)).Select<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>((System.Func<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>) (x => x.Value[0].Values));
    IEnumerable<AttributeValues> additionalValues1 = (IEnumerable<AttributeValues>) this._EI.AdditionalValues;
    eventArgs.OldObjectAttributes = additionalValues1 != null ? first1.Union<AttributeValues>(additionalValues1) : first1;
    if (this._relEI.TypeGuid != Guid.Empty)
    {
      eventArgs.RelationID = this._relEI.Info.ElementIdentifier;
      eventArgs.RelationTypeID = this._relEI.TypeID;
      IEnumerable<AttributeValues> first2 = this._relEI.DuplicationEditors.Select<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>((System.Func<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>) (x => x.Value[0].Values));
      IEnumerable<AttributeValues> additionalValues2 = (IEnumerable<AttributeValues>) this._relEI.AdditionalValues;
      eventArgs.OldRelationAttributes = additionalValues2 != null ? first2.Union<AttributeValues>(additionalValues2) : first2;
    }
    IEnumerable<AttributeValues> attributeValueses = (IEnumerable<AttributeValues>) new List<AttributeValues>()
    {
      newValue
    };
    if (isBase)
      eventArgs.NewObjectAttributes = attributeValueses;
    else
      eventArgs.NewRelationAttributes = attributeValueses;
    return eventArgs;
  }

  /// <summary>Для изменения значений в контроле из вне.</summary>
  /// <param name="newObjectValues"></param>
  /// <param name="newRelationValues"></param>
  public void AttributeChanging(
    IEnumerable<AttributeValues> newObjectValues,
    IEnumerable<AttributeValues> newRelationValues = null)
  {
    if ((newObjectValues == null || newObjectValues.Count<AttributeValues>() <= 0) && (newRelationValues == null || newRelationValues.Count<AttributeValues>() <= 0))
      return;
    if (this._eventsMngr != null && this.AttributeChangingEvents != null)
    {
      AttributeChangingEventArgs eventArgs = this.GetEventArgs(newObjectValues, newRelationValues);
      this.FireEvents(eventArgs);
      newObjectValues = eventArgs.NewObjectAttributes;
      newRelationValues = eventArgs.NewRelationAttributes;
    }
    this.Update(this._EI, newObjectValues);
    this.Update(this._relEI, newRelationValues);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectValues"></param>
  /// <param name="newRelationValues"></param>
  /// <returns></returns>
  private AttributeChangingEventArgs GetEventArgs(
    IEnumerable<AttributeValues> newObjectValues,
    IEnumerable<AttributeValues> newRelationValues = null)
  {
    AttributeChangingEventArgs eventArgs = new AttributeChangingEventArgs();
    eventArgs.FormID = this.FormID;
    eventArgs.ObjectID = this._EI.Info.ElementIdentifier;
    eventArgs.ObjectTypeID = this._EI.TypeID;
    IEnumerable<AttributeValues> first1 = this._EI.DuplicationEditors.Select<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>((System.Func<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>) (x => x.Value[0].Values));
    IEnumerable<AttributeValues> additionalValues1 = (IEnumerable<AttributeValues>) this._EI.AdditionalValues;
    eventArgs.OldObjectAttributes = additionalValues1 != null ? first1.Union<AttributeValues>(additionalValues1) : first1;
    eventArgs.NewObjectAttributes = newObjectValues;
    if (this._relEI.TypeGuid != Guid.Empty)
    {
      eventArgs.RelationID = this._relEI.Info.ElementIdentifier;
      eventArgs.RelationTypeID = this._relEI.TypeID;
      IEnumerable<AttributeValues> first2 = this._relEI.DuplicationEditors.Select<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>((System.Func<KeyValuePair<int, List<IAttributeEditor>>, AttributeValues>) (x => x.Value[0].Values));
      IEnumerable<AttributeValues> additionalValues2 = (IEnumerable<AttributeValues>) this._relEI.AdditionalValues;
      eventArgs.OldRelationAttributes = additionalValues2 != null ? first2.Union<AttributeValues>(additionalValues2) : first2;
      eventArgs.NewRelationAttributes = newRelationValues;
    }
    return eventArgs;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  private void FireEvents(AttributeChangingEventArgs args)
  {
    if (this.AttributeChangingEvents == null || this._eventsMngr == null)
      return;
    foreach (FormDesignerAction attributeChangingEvent in this.AttributeChangingEvents)
    {
      if (this._eventsMngr.GetEvent(attributeChangingEvent.ActionGuid) is IAttributeChangingEventHandler changingEventHandler)
        changingEventHandler.AttributeChanging(args);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ei"></param>
  /// <param name="newValues"></param>
  private void Update(DesForm.ElementInformation ei, IEnumerable<AttributeValues> newValues)
  {
    if (newValues == null || newValues.Count<AttributeValues>() <= 0)
      return;
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> duplicationEditors = ei.DuplicationEditors;
    List<AttributeValues> additionalValues = ei.AdditionalValues;
    this.CheckReadOnly(ei.LockAttributes, newValues);
    if (this._dictServerSrv != null)
    {
      this.CheckParsedAttributes(ei, newValues);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.ParseAttributeValues(sessionKeeper.Session, this._dictServerSrv, ei);
    }
    this.SetChangedAttributes(duplicationEditors, additionalValues, newValues);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="lockAttrIDs"></param>
  /// <param name="values"></param>
  private void CheckReadOnly(ICollection<int> lockAttrIDs, IEnumerable<AttributeValues> values)
  {
    if (lockAttrIDs == null || lockAttrIDs.Count<int>() <= 0)
      return;
    foreach (AttributeValues attributeValues in values)
    {
      if (lockAttrIDs.Contains(attributeValues.AttributeID))
        attributeValues.ReadOnly = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ei"></param>
  /// <param name="values"></param>
  private void CheckParsedAttributes(
    DesForm.ElementInformation ei,
    IEnumerable<AttributeValues> values)
  {
    List<AttributeValues> parsedAvs = ei.ParsedAVs;
    System.Collections.Generic.Dictionary<string, AttributeValues> forParsedAvs = ei.ForParsedAVs;
    for (int index = 0; index < values.Count<AttributeValues>(); ++index)
    {
      AttributeValues newAV = values.ElementAt<AttributeValues>(index);
      AttributeValues attributeValues1 = parsedAvs.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == newAV.AttributeID));
      if (attributeValues1 != null)
      {
        attributeValues1.Values = newAV.Values;
        attributeValues1.ReadOnly = newAV.ReadOnly;
      }
      else
        this.CheckParsedAttributeValues(ei, newAV);
      IEnumerable<AttributeValues> source = forParsedAvs.Where<KeyValuePair<string, AttributeValues>>((System.Func<KeyValuePair<string, AttributeValues>, bool>) (x => x.Value.AttributeID == newAV.AttributeID)).Select<KeyValuePair<string, AttributeValues>, AttributeValues>((System.Func<KeyValuePair<string, AttributeValues>, AttributeValues>) (x => x.Value));
      if (source.Count<AttributeValues>() > 0)
      {
        AttributeValues attributeValues2 = source.ElementAt<AttributeValues>(0);
        attributeValues2.Values = newAV.Values;
        attributeValues2.ReadOnly = newAV.ReadOnly;
      }
      else
        this.CheckForParsedAttributeValues(ei, newAV);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editors"></param>
  /// <param name="additionalValues"></param>
  /// <param name="objectValues"></param>
  private void SetChangedAttributes(
    System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> editors,
    List<AttributeValues> additionalValues,
    IEnumerable<AttributeValues> objectValues)
  {
    foreach (AttributeValues objectValue in objectValues)
    {
      AttributeValues av = objectValue;
      int attributeId = av.AttributeID;
      object[] values = av.Values;
      bool flag = av.ReadOnly;
      if (editors.ContainsKey(attributeId))
      {
        foreach (IAttributeEditor attributeEditor in editors[attributeId])
        {
          AttributeValues attributeValues = attributeEditor.Values ?? av;
          if (attributeEditor is ILockModify lockModify)
            lockModify.LockModify = true;
          try
          {
            attributeValues.Values = values;
            attributeValues.ReadOnly = flag;
            attributeEditor.Values = attributeValues;
          }
          finally
          {
            if (lockModify != null)
              lockModify.LockModify = false;
          }
          attributeEditor.Modified = true;
        }
      }
      else
      {
        AttributeValues attributeValues = additionalValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID));
        if (attributeValues != null)
        {
          attributeValues.Values = av.Values;
          attributeValues.ReadOnly = av.ReadOnly;
        }
        else
          additionalValues.Add(av);
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DesForm));
    this._err = new ErrorProvider(this.components);
    this._tt = new ToolTip(this.components);
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    this._err.ContainerControl = (ContainerControl) this;
    this._tt.ShowAlways = true;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.None;
    this.Name = nameof (DesForm);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private class ElementInformation
  {
    /// <summary>Объект/связь</summary>
    /// <remarks>Заполняется на время загрузки данных на форму, после чего обнуляется</remarks>
    private IDBAttributable _DBAttributable;
    internal AttributeProcessor ap;
    /// <summary>Данные объекта/связи</summary>
    private IElementInfo _info;
    /// <summary>Объект/связь доступен только для чтения</summary>
    internal bool infoReadonly;
    /// <summary>
    /// Флаг, указывающий нужно ли атрибут процессору рассылать уведомления при сохранении объекта/связи
    /// </summary>
    internal bool invokeProcessor;
    /// <summary>Список всех контролов, для которых указан атрибут</summary>
    internal List<IAttributeEditor> LinkedEditors = new List<IAttributeEditor>();
    /// <summary>
    /// Идентификатор атрибута - список контролов, с которыми он связан
    /// </summary>
    internal System.Collections.Generic.Dictionary<int, List<IAttributeEditor>> DuplicationEditors = new System.Collections.Generic.Dictionary<int, List<IAttributeEditor>>();
    /// <summary>
    /// Атрибуты, значения которых, перед отображением, нужно специальным образом распарсить
    /// </summary>
    internal List<AttributeValues> ParsedAVs = new List<AttributeValues>();
    internal System.Collections.Generic.Dictionary<string, AttributeValues> ForParsedAVs = new System.Collections.Generic.Dictionary<string, AttributeValues>();
    /// <summary>
    /// Атрибуты объекта или связи, добавление/редактирование которых, средствами интерфейса пользователя, должно быть недоступно
    /// </summary>
    internal ICollection<int> LockAttributes = (ICollection<int>) new List<int>(0);
    /// <summary>Список идентификаторов временно добавляемых атрибутов</summary>
    internal List<int> TempAttrIDs = new List<int>();

    /// <summary>
    /// Хранятся атрибуты, которые были изменены, но которые не связаны ни с одним контролом.
    /// </summary>
    internal List<AttributeValues> AdditionalValues { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal IElementInfo Info
    {
      get => this._info;
      set
      {
        this._info = value;
        this.TypeID = -1;
        this.TypeGuid = Guid.Empty;
        this.infoReadonly = true;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    internal int TypeID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    internal Guid TypeGuid { get; set; }

    /// <summary>
    /// 
    /// </summary>
    internal IDBAttributable DBAttributable
    {
      get => this._DBAttributable;
      set
      {
        this._DBAttributable = value;
        if (value == null)
          return;
        this.TypeID = value.TypeID;
        this.TypeGuid = this._info.ElementKind != AttributableElements.Object ? MetaDataHelper.GetRelationTypeGuid(this.TypeID) : MetaDataHelper.GetObjectTypeGuid(this.TypeID);
        this.infoReadonly = value.ReadOnly;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    internal bool HasVirtualAttribute
    {
      get
      {
        bool virtualAttribute = false;
        foreach (int key in this.DuplicationEditors.Keys)
        {
          virtualAttribute = ObligatoryObjectAttributesHelper.IsVirtualAttribute(key);
          if (virtualAttribute)
            break;
        }
        return virtualAttribute;
      }
    }

    /// <summary>Конструктор.</summary>
    internal ElementInformation()
    {
      this.AdditionalValues = new List<AttributeValues>();
      this.TypeID = -1;
      this.TypeGuid = Guid.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attrID"></param>
    /// <param name="ctrl"></param>
    internal void AddControl(int attrID, IAttributeEditor ctrl)
    {
      this.LinkedEditors.Add(ctrl);
      if (this.DuplicationEditors.ContainsKey(attrID))
        this.DuplicationEditors[attrID].Add(ctrl);
      else
        this.DuplicationEditors.Add(attrID, new List<IAttributeEditor>()
        {
          ctrl
        });
    }

    /// <summary>Очищение списков.</summary>
    internal void ClearLists()
    {
      this.AdditionalValues.Clear();
      this.ParsedAVs.Clear();
      this.ForParsedAVs.Clear();
      this.TempAttrIDs.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="addValues"></param>
    internal void MargeAdditionalValues(List<AttributeValues> addValues)
    {
      if (addValues == null || addValues.Count <= 0)
        return;
      if (this.AdditionalValues.Count == 0)
      {
        this.AdditionalValues = addValues;
      }
      else
      {
        foreach (AttributeValues addValue in addValues)
        {
          AttributeValues av = addValue;
          AttributeValues attributeValues = this.AdditionalValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID));
          if (attributeValues != null)
            this.AdditionalValues.Remove(attributeValues);
          this.AdditionalValues.Add(av);
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal class DataFormatErrorException : Exception
  {
    internal string Msg = string.Empty;
    internal string Caption = string.Empty;

    /// <summary>Конструктор.</summary>
    /// <param name="caption">Заголовок</param>
    /// <param name="msg">Сообщение</param>
    public DataFormatErrorException(string caption, string msg)
    {
      this.Caption = caption;
      this.Msg = msg;
    }
  }
}


// Type: Intermech.Client.Core.FormDesigner.Navigator.FormDesignerView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Commands;
using Intermech.FormDesigner;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core.FormDesigner.Navigator;

/// <summary>
/// Класс для просмотра формы редактирования атрибутов объекта.
/// </summary>
public class FormDesignerView : 
  UserControl,
  IView,
  ICanCloseViews,
  ICanDeactivateView,
  IObjectCreator
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _panel;
  /// <summary>
  /// Обработчик события "Перед завершением редактирования объекта"
  /// </summary>
  private EventHandler<BeforeObjectCommandArgs> _commandsBeforeCheckInHndl;
  /// <summary>Глобальная служба уведомлений</summary>
  private INotificationService _globalNotificationService;
  /// <summary>Обработчик событий от глобальной службы уведомлений</summary>
  private NotificationEventHandler _globalNotifyHandler;
  private AdvancedServiceContainer _srvProvider = new AdvancedServiceContainer();
  /// <summary>
  /// Данные объекта/связи (если объект/связь рассматривается обособленно)
  /// </summary>
  protected IElementInfo _info;
  /// <summary>
  /// Данные для связи, если объект рассматривается в составе
  /// </summary>
  protected IElementInfo _relInfo;
  private DesForm _form;
  /// <summary>
  /// Идентификаторы объекта и связи нужны для событий.
  /// Когда приходит уведомление о каком-то событии, событие может не относиться к загруженному объекту/связи.
  /// Поэтому, чтобы не выполнять лишних действий, необходимо проверить идентификатор объекта/связи пришедший с уведомлением.
  /// </summary>
  protected long _objID = -1;
  protected long _relID = -1;
  /// <summary>Признак того, что работаем с заготовкой</summary>
  internal bool _blankMode;
  private bool _bNeedCheckChanges = true;
  private bool _bNeedValidateBeforeSave = true;
  private bool _activated;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.ReleaseServices();
      if (this.components != null)
        this.components.Dispose();
      if (this._form != null)
      {
        this._form.Close();
        this._form.Dispose();
        this._form = (DesForm) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDesignerView));
    this._panel = new Panel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._panel, "_panel");
    this._panel.Name = "_panel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._panel);
    this.Name = nameof (FormDesignerView);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Определяет изменились ли значения полей ввода на форме.
  /// </summary>
  public bool FormChanged
  {
    get
    {
      if (this._form == null)
        return false;
      return this._form.Modified || this._form.ModifiedInLoad;
    }
  }

  /// <summary>Идентификатор версии формы редактирования.</summary>
  public virtual long FormID { get; protected set; }

  /// <summary>
  /// 
  /// </summary>
  protected long ObjID
  {
    get => this._objID;
    set
    {
      if (this._objID == value)
        return;
      this._objID = value;
      this._bNeedCheckChanges = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal AdvancedServiceContainer ServiceProvider
  {
    get => this._srvProvider;
    set => this._srvProvider.AdvancedProvider = (System.IServiceProvider) value;
  }

  /// <summary>Конструктор.</summary>
  public FormDesignerView()
  {
    this.InitializeComponent();
    this.InitServices();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this.ImageIndex = service != null ? service.ImageIndex("imgCard") : 0;
    this.FormID = 0L;
    this.Caption = LocalizationHolder.rm.GetString("Client.Core_188");
    this.OrderID = 0;
  }

  /// <summary>Конструктор создания формы для объекта/связи.</summary>
  /// <param name="ID">ID объекта/связи для которого создается форма</param>
  /// <param name="formID">ID формы ввода/вывода</param>
  /// <param name="kind">Тип идентификатора</param>
  public FormDesignerView(long ID, long formID, AttributableElements kind = AttributableElements.Object)
  {
    this.InitializeComponent();
    this._info = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(ID, kind);
    this.FormID = formID;
    this.ImageIndex = 0;
    this.Caption = LocalizationHolder.rm.GetString("Client.Core_188");
  }

  /// <summary>Конструктор создания формы для объекта/связи.</summary>
  /// <param name="objID">ID объекта</param>
  /// <param name="relID">ID связи</param>
  /// <param name="formID">ID формы ввода/вывода</param>
  public FormDesignerView(long objID, long relID, long formID)
    : this(objID, formID)
  {
    this._relID = relID;
    if (this._relID == -1L || this._relID == 0L)
      return;
    this._relInfo = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(this._relID, AttributableElements.Relation);
  }

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки.
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладка разрешает закрытие формы, false - закладка запрещает закрытие формы</returns>
  public bool CanClose(object sender)
  {
    bool flag = true;
    if (this._bNeedCheckChanges)
    {
      flag = this.SaveChanges(true);
      this._bNeedCheckChanges = !flag;
    }
    return flag;
  }

  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку.
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладку можно деактивировать, false - закладку нельзя деактивировать</returns>
  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране.
  /// </summary>
  /// <remarks>Навигатор получает значение этого свойства после того, как закладка будет проинициализирована в методе Initialize</remarks>
  public virtual string Caption { get; private set; }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране, в именованном списке иконок.
  /// </summary>
  /// <remarks>Навигатор получает значение этого свойства после того, как закладка будет проинициализирована в методе Initialize</remarks>
  public int ImageIndex { get; private set; }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок при выводе на экран.
  /// Навигатор сортирует отображаемые закладки в порядке возрастания этого значения.
  /// </summary>
  /// <remarks>Значение этого свойства навигатор получает после того, как закладка будет проинициализирована в методе Initialize</remarks>
  public virtual int OrderID { get; private set; }

  /// <summary>Выполняет инициализацию закладки после ее создания.</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка</param>
  /// <remarks>Реализация этого метода должна работать быстро, т.е. все длительные операции желательно выполнять при первом вызове метода Activate</remarks>
  public virtual void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (this._srvProvider.GetService<ISelectedItems>(false) != null)
      this._srvProvider.RemoveService(typeof (ISelectedItems));
    this._srvProvider.AddService<ISelectedItems>(items);
    this._srvProvider.AdvancedProvider = provider;
    string str = Convert.ToString(this.Tag);
    if (string.IsNullOrEmpty(str))
      return;
    int num = str.IndexOf("]");
    if (num >= 0)
      str = str.Substring(num + 2);
    FormInformation formInformation = FormInformation.Parse(str);
    if (formInformation == null)
      return;
    this.FormID = formInformation.ID;
    this.Caption = formInformation.Caption;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране.
  /// </summary>
  /// <param name="previousView">Закладка, с которой осуществляется переключение. Может быть null для самой первой показываемой на экране закладки</param>
  /// <remarks>Этот метод вызывается при первом показе закладки, а также при переключении на нее с другой закладки</remarks>
  public virtual void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this._activated = true;
    if (this._form == null)
    {
      this._panel.Visible = false;
      this.LoadForm();
      this._panel.Visible = true;
      this.RefreshForm(RefreshMode.Default);
    }
    else
      this.RefreshForm(RefreshMode.Forced);
    this._bNeedCheckChanges = this._activated = true;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране.
  /// </summary>
  /// <param name="nextView">Закладка, на которую осуществляется переключение. Может быть null, если выполяется не переключение, а удаление закладок</param>
  public virtual void Deactivate(IView nextView)
  {
    if (this._form != null)
      this._form.SetFormDeactivate();
    this._activated = false;
    if (this._bNeedCheckChanges)
      this.SaveChanges(false);
    this.SaveSettings();
  }

  /// <summary>
  /// Признак дополнительных действий после завершения создания объекта.
  /// </summary>
  public bool SaveAfterCommitCreation => true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectID"></param>
  /// <returns></returns>
  public bool SaveAfterCommit(IUserSession session, long newObjectID)
  {
    bool flag = true;
    if (this._info.ElementKind == AttributableElements.Object && this._form.IncludedClassificators.Count > 0 && session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
    {
      foreach (long includedClassificator in this._form.IncludedClassificators)
      {
        if (!customService.ExistsObject((object) session.SessionGUID, includedClassificator, this._info.ElementIdentifier))
          customService.IncludeObjects((object) session.SessionGUID, includedClassificator, new long[1]
          {
            this._info.ElementIdentifier
          });
      }
    }
    return flag;
  }

  /// <summary>Получение формы.</summary>
  /// <returns>Форма</returns>
  private DesForm GetForm()
  {
    form2 = (DesForm) null;
    byte[] form1 = ClientFormsCache.GetForm(this.FormID);
    if (form1 != null)
    {
      using (MemoryStream memoryStream = new MemoryStream(form1))
      {
        try
        {
          if (!(ImXmlReader.Read((Stream) memoryStream, (IDesignerHost) null) is DesForm form2))
            throw new Exception();
        }
        catch (Exception ex)
        {
          throw new Exception(LocalizationHolder.rm.GetString("Client.Core_204"), ex);
        }
      }
    }
    return form2;
  }

  /// <summary>Метод для обновления значений атрибутов.</summary>
  /// <param name="mode"></param>
  /// <returns></returns>
  private bool RefreshForm(RefreshMode mode)
  {
    string errorMsg = string.Empty;
    return this.RefreshForm(mode, out errorMsg);
  }

  /// <summary>Сохранение изменений.</summary>
  /// <param name="canCancel">Наличие кнопки "Cancel" в MessageBox'е</param>
  /// <param name="message"></param>
  /// <returns>Результат сохранения</returns>
  private bool SaveChanges(bool canCancel, string message = "")
  {
    if (this._form != null)
      this._form.ValidateBeforeSave();
    bool flag = true;
    if (this.FormChanged)
    {
      DialogResult dialogResult = DialogResult.Yes;
      if (!this._form.ModifiedInLoad)
        dialogResult = MessageBox.Show(string.IsNullOrEmpty(message) ? LocalizationHolder.rm.GetString("Client.Core_178") : message, LocalizationHolder.rm.GetString("Client.Core_135"), canCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      switch (dialogResult)
      {
        case DialogResult.Cancel:
          flag = false;
          break;
        case DialogResult.Yes:
          this._bNeedValidateBeforeSave = false;
          string errorMsg = string.Empty;
          try
          {
            flag = this.SaveForm(out errorMsg);
          }
          catch (KernelException ex)
          {
            if (errorMsg != string.Empty)
            {
              if (MessageBox.Show($"При попытке сохранения данных формы возникла ошибка:\n\n{errorMsg}\n\nИгнорировать ошибку?\n\nПримечание. Игнорируйте данную ошибку, только если её невозможно исправить с правами текущего пользователя: следует иметь в виду, что некоторые данные при этом могут быть не сохранены, и необходимо избегать дальнейшего изменения/сохранения данных объекта до устранения ошибки.", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                flag = true;
              else
                throw;
            }
            else
              throw;
          }
          this._bNeedValidateBeforeSave = true;
          break;
      }
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadSettings()
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration configuration1 = service.Open("Forms");
    if (configuration1 == null)
      return;
    IConfiguration configuration2 = configuration1.Configurations[$"FormID_{Convert.ToString(Math.Abs(this.FormID))}"];
    if (configuration2 == null)
      return;
    string[] strArray = configuration2.GetProperty("Size").Split(';');
    if (strArray.Length == 2)
    {
      this._form.Width = Convert.ToInt32(strArray[0]);
      this._form.Height = Convert.ToInt32(strArray[1]);
    }
    this.LoadSplitterSettings(configuration2);
    this.LoadObjectsListSettings(configuration2);
    this.LoadAttrObjectsListSettings(configuration2);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveSettings()
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration configuration1 = service.Open("Forms") ?? service.Create("Forms");
    string name = $"FormID_{Convert.ToString(Math.Abs(this.FormID))}";
    IConfiguration configuration2 = configuration1.Configurations[name] ?? configuration1.Add(name);
    bool flag1 = this.SaveSplitterSettings(configuration2);
    if (flag1)
      configuration2.SetProperty("Size", $"{this._form.Width.ToString()}; {this._form.Height.ToString()}");
    bool flag2 = this.SaveObjectsListSettings(configuration2) | flag1;
    if (this.SaveAttrObjectsListSettings(configuration2) | flag2)
      return;
    configuration1.Remove(configuration2);
  }

  /// <summary>Загрузить настройки Splitter'ов.</summary>
  /// <param name="formIDConfig">Секция с настройками</param>
  private void LoadSplitterSettings(IConfiguration formIDConfig)
  {
    IConfiguration configuration1 = formIDConfig.Configurations[typeof (Splitter).ToString()];
    if (configuration1 == null)
      return;
    IConfigurationCollection configurations = configuration1.Configurations;
    if (configurations == null || configurations.Count <= 0)
      return;
    List<Splitter> controlCollection = this.GetControlCollection<Splitter>((Control) this._form);
    if (controlCollection == null)
      return;
    string splitterName = string.Empty;
    foreach (IConfiguration configuration2 in (IEnumerable) configurations)
    {
      splitterName = configuration2.Name.Substring("Name_".Length);
      Splitter splitter = controlCollection.FirstOrDefault<Splitter>((Func<Splitter, bool>) (x => x.Name == splitterName));
      if (splitter != null)
        splitter.SplitPosition = Convert.ToInt32(configuration2.GetProperty("SplitPosition"));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formIDConfig"></param>
  /// <returns></returns>
  private bool SaveSplitterSettings(IConfiguration formIDConfig)
  {
    bool flag = false;
    string name = typeof (Splitter).ToString();
    IConfiguration configuration1 = formIDConfig.Configurations[name];
    if (configuration1 != null)
      formIDConfig.Remove(configuration1);
    List<Splitter> controlCollection = this.GetControlCollection<Splitter>((Control) this._form);
    if (controlCollection != null)
    {
      IConfiguration configuration2 = formIDConfig.Add(name);
      foreach (Splitter splitter in controlCollection)
        configuration2.Add($"Name_{splitter.Name}").SetProperty("SplitPosition", splitter.SplitPosition.ToString());
      flag = true;
    }
    return flag;
  }

  /// <summary>Загрузить настройки ObjectsList'ов</summary>
  /// <param name="formIDConfig">Секция с настройками</param>
  private void LoadObjectsListSettings(IConfiguration formIDConfig)
  {
    IConfiguration configuration1 = formIDConfig.Configurations[typeof (ObjectsList).ToString()];
    if (configuration1 == null)
      return;
    IConfigurationCollection configurations1 = configuration1.Configurations;
    if (configurations1 == null || configurations1.Count <= 0)
      return;
    List<ObjectsList> controlCollection = this.GetControlCollection<ObjectsList>((Control) this._form);
    if (controlCollection == null)
      return;
    string objectsListName = string.Empty;
    foreach (IConfiguration configuration2 in (IEnumerable) configurations1)
    {
      objectsListName = configuration2.Name.Substring("Name_".Length);
      ObjectsList objectsList = controlCollection.FirstOrDefault<ObjectsList>((Func<ObjectsList, bool>) (x => x.Name == objectsListName));
      if (objectsList != null)
      {
        IConfiguration configuration3 = configuration2.Configurations["Columns"];
        if (configuration3 != null)
        {
          IConfigurationCollection configurations2 = configuration3.Configurations;
          if (configurations2 != null && configurations2.Count != 0)
          {
            SavedColumnsSettings settings = new SavedColumnsSettings();
            foreach (IConfiguration configuration4 in (IEnumerable) configurations2)
            {
              int int32_1 = Convert.ToInt32(configuration4.Name.Substring("ID_".Length));
              int int32_2 = Convert.ToInt32(configuration4.GetProperty("Width"));
              settings.SetColumnsWidth(int32_1, int32_2);
            }
            objectsList.SetSavedSettings(settings);
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formIDConfig"></param>
  /// <returns></returns>
  private bool SaveObjectsListSettings(IConfiguration formIDConfig)
  {
    bool flag = false;
    string name = typeof (ObjectsList).ToString();
    IConfiguration configuration1 = formIDConfig.Configurations[name];
    if (configuration1 != null)
      formIDConfig.Remove(configuration1);
    List<ObjectsList> controlCollection = this.GetControlCollection<ObjectsList>((Control) this._form);
    if (controlCollection != null)
    {
      IConfiguration configuration2 = formIDConfig.Add(name);
      foreach (ObjectsList objectsList in controlCollection)
      {
        NodeColumnCollection nodeColumns = objectsList.ChildrenView.GetNodeColumns();
        if (nodeColumns != null && nodeColumns.Count != 0)
        {
          IConfiguration configuration3 = configuration2.Add($"Name_{objectsList.Name}").Add("Columns");
          foreach (NodeColumn nodeColumn in (List<NodeColumn>) nodeColumns)
            configuration3.Add($"ID_{nodeColumn.Attribute.AttributeID.ToString()}").SetProperty("Width", nodeColumn.Width.ToString());
          flag = true;
        }
      }
    }
    return flag;
  }

  /// <summary>Загрузить настройки AttrObjectsList'ов</summary>
  /// <param name="formIDConfig">Секция с настройками</param>
  private void LoadAttrObjectsListSettings(IConfiguration formIDConfig)
  {
    IConfiguration configuration1 = formIDConfig.Configurations[typeof (AttrObjectsList).ToString()];
    if (configuration1 == null)
      return;
    IConfigurationCollection configurations1 = configuration1.Configurations;
    if (configurations1 == null || configurations1.Count <= 0)
      return;
    List<AttrObjectsList> controlCollection = this.GetControlCollection<AttrObjectsList>((Control) this._form);
    if (controlCollection == null)
      return;
    string objectsListName = string.Empty;
    foreach (IConfiguration configuration2 in (IEnumerable) configurations1)
    {
      objectsListName = configuration2.Name.Substring("Name_".Length);
      AttrObjectsList attrObjectsList = controlCollection.FirstOrDefault<AttrObjectsList>((Func<AttrObjectsList, bool>) (x => x.Name == objectsListName));
      if (attrObjectsList != null)
      {
        IConfiguration configuration3 = configuration2.Configurations["Columns"];
        if (configuration3 != null)
        {
          IConfigurationCollection configurations2 = configuration3.Configurations;
          if (configurations2 != null && configurations2.Count != 0)
          {
            SavedColumnsSettings settings = new SavedColumnsSettings();
            foreach (IConfiguration configuration4 in (IEnumerable) configurations2)
            {
              int int32_1 = Convert.ToInt32(configuration4.Name.Substring("ID_".Length));
              int int32_2 = Convert.ToInt32(configuration4.GetProperty("Width"));
              settings.SetColumnsWidth(int32_1, int32_2);
            }
            attrObjectsList.SetSavedSettings(settings);
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formIDConfig"></param>
  /// <returns></returns>
  private bool SaveAttrObjectsListSettings(IConfiguration formIDConfig)
  {
    bool flag = false;
    string name = typeof (AttrObjectsList).ToString();
    IConfiguration configuration1 = formIDConfig.Configurations[name];
    if (configuration1 != null)
      formIDConfig.Remove(configuration1);
    List<AttrObjectsList> controlCollection = this.GetControlCollection<AttrObjectsList>((Control) this._form);
    if (controlCollection != null)
    {
      IConfiguration configuration2 = formIDConfig.Add(name);
      foreach (AttrObjectsList attrObjectsList in controlCollection)
      {
        NodeColumnCollection nodeColumns = attrObjectsList.GetNodeColumns();
        if (nodeColumns != null && nodeColumns.Count != 0)
        {
          IConfiguration configuration3 = configuration2.Add($"Name_{attrObjectsList.Name}").Add("Columns");
          foreach (NodeColumn nodeColumn in (List<NodeColumn>) nodeColumns)
            configuration3.Add($"ID_{nodeColumn.Attribute.AttributeID.ToString()}").SetProperty("Width", nodeColumn.Width.ToString());
          flag = true;
        }
      }
    }
    return flag;
  }

  /// <summary>Получить коллекцию контролов указанного типа.</summary>
  /// <param name="ctrl">Родительский контрол</param>
  /// <returns>Список контролов</returns>
  private List<T> GetControlCollection<T>(Control ctrl)
  {
    List<T> objList = new List<T>();
    if (ctrl != null)
    {
      foreach (Control control in (ArrangedElementCollection) ctrl.Controls)
      {
        switch (control)
        {
          case T _:
            objList.Add((T) Convert.ChangeType((object) control, typeof (T)));
            continue;
          case IFormDesignerControl formDesignerControl:
            if (!formDesignerControl.CanContainsChildren)
              continue;
            break;
        }
        List<T> controlCollection = this.GetControlCollection<T>(control);
        if (controlCollection != null)
          objList.AddRange((IEnumerable<T>) controlCollection);
      }
    }
    return objList.Count <= 0 ? (List<T>) null : objList;
  }

  /// <summary>Выполнить инициализацию сервисов закладки.</summary>
  protected virtual void InitServices()
  {
    if (this._commandsBeforeCheckInHndl == null)
    {
      this._commandsBeforeCheckInHndl = new EventHandler<BeforeObjectCommandArgs>(this.CommandsBeforeCheckIn);
      ObjectCommandEvents.Checkin.Before += this._commandsBeforeCheckInHndl;
    }
    if (this._globalNotificationService != null)
      return;
    this._globalNotificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (this._globalNotifyHandler != null || this._globalNotificationService == null)
      return;
    this._globalNotifyHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
    this._globalNotificationService.Subscribe(this._globalNotifyHandler);
  }

  /// <summary>Выполнить деинициализацию сервисов закладки.</summary>
  protected virtual void ReleaseServices()
  {
    if (this._commandsBeforeCheckInHndl != null)
    {
      ObjectCommandEvents.Checkin.Before -= this._commandsBeforeCheckInHndl;
      this._commandsBeforeCheckInHndl = (EventHandler<BeforeObjectCommandArgs>) null;
    }
    if (this._globalNotificationService == null)
      return;
    if (this._globalNotifyHandler != null && this._globalNotificationService != null)
      this._globalNotificationService.Unsubscribe(this._globalNotifyHandler);
    this._globalNotificationService = (INotificationService) null;
    this._globalNotifyHandler = (NotificationEventHandler) null;
  }

  /// <summary>
  /// Событие возникает перед завершением изменений в объекте.
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void CommandsBeforeCheckIn(object sender, BeforeObjectCommandArgs e)
  {
    if (e.ObjectId != this._objID || this._form == null)
      return;
    this.SaveChanges(false);
  }

  /// <summary>Событие от глобальной службы уведомлений.</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e == null)
      return;
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    string eventName = e.EventName;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(eventName))
    {
      case 1430399058:
        if (!(eventName == "RelationsChanged"))
          return;
        break;
      case 1868964354:
        if (!(eventName == "RelationsRemoved") || !(e is DBRelationsEventArgs relationsEventArgs1) || !relationsEventArgs1.RelationIDs.Contains(this._relID))
          return;
        this._relID = 0L;
        this._relInfo = (IElementInfo) null;
        this._bNeedCheckChanges = false;
        goto label_33;
      case 2108022063:
        if (!(eventName == "ObjectsChangesCancelled"))
          return;
        goto label_23;
      case 2621053161:
        if (!(eventName == "ObjectsRemoved"))
          return;
        if (objectsEventArgs != null && objectsEventArgs.ObjectIDs.Contains(this._objID))
        {
          this._bNeedCheckChanges = false;
          goto label_33;
        }
        this.SaveChanges(false, LocalizationHolder.rm.GetString("FormDesigner_NeedRefreshForm_Msg"));
        goto label_33;
      case 2691487867:
        if (!(eventName == "ObjectsCheckedIn"))
          return;
        goto label_23;
      case 3096070312:
        if (!(eventName == "ObjectsCheckedOut") || !(e is DBObjectsCheckOutEventArgs checkOutEventArgs) || !checkOutEventArgs.ObjectIDs.Contains(this._objID))
          return;
        int index = checkOutEventArgs.ObjectIDs.IndexOf(this._objID);
        this._objID = checkOutEventArgs.NewObjectIDs[index];
        this._info = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(this._objID, this._info.ElementKind);
        if (this._form != null)
        {
          this._form.ResetStatus();
          goto label_33;
        }
        goto label_33;
      case 3837095985:
        if (!(eventName == "ObjectsChanged"))
          return;
        break;
      default:
        return;
    }
    if (e.EventName == "ObjectsChanged" && this._form != null)
      this._form.ResetStatus();
    if (sender is DesForm desForm && desForm == this._form)
      return;
    DBRelationsEventArgs relationsEventArgs2 = e as DBRelationsEventArgs;
    if ((objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._objID)) && (relationsEventArgs2 == null || !relationsEventArgs2.RelationIDs.Contains(this._relID)))
      return;
    goto label_33;
label_23:
    if (objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._objID))
      return;
    this._objID = Math.Abs(this._objID);
    this._info = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(this._objID, this._info.ElementKind);
    if (this._form != null)
      this._form.ResetStatus();
label_33:
    if (!this._activated)
      return;
    this.RefreshForm(RefreshMode.Forced);
  }

  /// <summary>
  /// 
  /// </summary>
  protected void RemoveForm()
  {
    this._panel.Controls.Clear();
    this._form = (DesForm) null;
  }

  /// <summary>Метод для загрузки формы и значений атрибутов.</summary>
  /// <returns>Если true, то успешно загружено</returns>
  public bool LoadForm()
  {
    bool flag = false;
    string errorMsg = string.Empty;
    using (new SessionKeeper())
      flag = this.LoadForm((IDBObject) null, out errorMsg);
    if (!string.IsNullOrEmpty(errorMsg))
    {
      int num = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString("Client.Core_204"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    return flag;
  }

  /// <summary>Метод для загрузки формы и значений атрибутов.</summary>
  /// <param name="formObj"></param>
  /// <param name="errorMsg">Если возникла ошибка, содержится сообщение ошибки</param>
  /// <returns>Если true, то успешно загружено</returns>
  public bool LoadForm(IDBObject formObj, out string errorMsg)
  {
    bool flag = false;
    errorMsg = string.Empty;
    try
    {
      if (this._panel.Controls.Count > 0)
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_200"));
      if (this._info == null)
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_201"));
      if (this.FormID == 0L)
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_206"));
      try
      {
        this._form = this.GetForm();
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        throw;
      }
      flag = true;
    }
    catch (Exception ex)
    {
      errorMsg = ex.Message;
    }
    finally
    {
      this._form = this._form ?? new DesForm();
    }
    this.LoadSettings();
    if (!string.IsNullOrEmpty(this._form.HelpPathToFile) && this.Parent != null && this.Parent.Tag is IViewPage tag)
    {
      tag.Info.HelpPath = this._form.HelpPathToFile;
      tag.Info.HelpTopicID = this._form.HelpPartLabel;
    }
    this.MinimumSize = this._form.MinimumSize;
    this.MaximumSize = this._form.MaximumSize;
    this._form.ServiceProvider = (System.IServiceProvider) this._srvProvider;
    this._form.FormID = this.FormID;
    this._form.Info = this._info;
    this._form.RelationInfo = this._relInfo;
    this._form.ControlsLoaded();
    this._form.LoadAttributes();
    this._form.TopLevel = false;
    this._form.Dock = DockStyle.Fill;
    this._form.Visible = true;
    this._panel.Controls.Add((Control) this._form);
    return flag;
  }

  /// <summary>Метод для сохранения значений атрибутов.</summary>
  /// <returns>Если true, то успешно записано</returns>
  public bool SaveForm()
  {
    string errorMsg = string.Empty;
    return this.SaveForm(out errorMsg);
  }

  /// <summary>Метод для сохранения значений атрибутов.</summary>
  /// <param name="errorMsg">Если возникла ошибка, содержится сообщение ошибки</param>
  /// <returns>Если true, то успешно записано</returns>
  public bool SaveForm(out string errorMsg)
  {
    bool flag = false;
    errorMsg = string.Empty;
    if (this._form != null)
    {
      if (this._bNeedValidateBeforeSave)
        this._form.ValidateBeforeSave();
      try
      {
        flag = this._form.SaveAttributes(this._blankMode);
      }
      catch (KernelException ex)
      {
        errorMsg = ex.Message;
        throw;
      }
      catch (DesForm.DataFormatErrorException ex)
      {
        errorMsg = ex.Msg;
      }
    }
    else
      errorMsg = LocalizationHolder.rm.GetString("Client.Core_207");
    return flag;
  }

  /// <summary>Сохранить данные формы в объект с новым ID.</summary>
  /// <param name="saveID">ID объекта для сохранения</param>
  /// <param name="noReload">Не грузить форму после записи атрибутов</param>
  /// <returns>Если true, то успешно записано</returns>
  public bool SaveForm(long saveID, bool noReload)
  {
    bool flag = false;
    if (this._form != null && this._form.Info != null)
    {
      if (this._bNeedValidateBeforeSave)
        this._form.ValidateBeforeSave();
      this._form.PinExchange[this._form.Info.ElementIdentifier] = saveID;
      this._form.SaveAttributes(this._blankMode);
      this._form.PinExchange.Clear();
      flag = true;
    }
    return flag;
  }

  /// <summary>Метод для обновления значений атрибутов.</summary>
  /// <param name="errorMsg">Если возникла ошибка, содержится сообщение ошибки</param>
  /// <returns>Если true, то успешно обновлено</returns>
  public bool RefreshForm(out string errorMsg)
  {
    return this.RefreshForm(RefreshMode.Default, out errorMsg);
  }

  /// <summary>Метод для обновления значений атрибутов.</summary>
  /// <param name="mode"></param>
  /// <param name="errorMsg">Если возникла ошибка, содержится сообщение ошибки</param>
  /// <returns>Если true, то успешно обновлено</returns>
  public bool RefreshForm(RefreshMode mode, out string errorMsg)
  {
    bool flag = false;
    errorMsg = string.Empty;
    if (this._form != null)
    {
      this.LoadSettings();
      this._form.Info = this._info;
      this._form.RelationInfo = this._relInfo;
      this._form.LoadAttributes(mode);
      flag = true;
    }
    errorMsg = LocalizationHolder.rm.GetString("Client.Core_209");
    return flag;
  }

  /// <summary>
  /// Метод для изменения видимости кнопок "Применить" и "Отмена".
  /// </summary>
  /// <param name="visible">Устанавливает видимость кнопок</param>
  public void ButtonsVisible(bool visible)
  {
    if (this._form == null)
      return;
    List<Guid> guidList = new List<Guid>()
    {
      ActionInfo.ApplyAction.ActionGuid,
      ActionInfo.CancelAction.ActionGuid,
      ActionInfo.CheckInAction.ActionGuid,
      ActionInfo.CheckOutAction.ActionGuid
    };
    for (int index = 0; index < this._form.AttrButtons.Count; ++index)
    {
      if (guidList.Contains(this._form.AttrButtons[index].FormDesignerAction.ActionGuid))
        this._form.AttrButtons[index].Visible = visible;
    }
  }
}

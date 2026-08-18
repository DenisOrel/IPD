// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs.ArtsCompositionBaseForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;

/// <summary>
/// Базовая форма для работы с контекстными объектами (сборочными единицами)
/// </summary>
/// <summary>Форма для создания контекстной сборочной единицы</summary>
public class ArtsCompositionBaseForm : Form
{
  /// <summary>
  /// 
  /// </summary>
  private bool _customServicePresent;
  /// <summary>Для регистрации своих категорий</summary>
  private IGuidMapper _guidMapper;
  /// <summary>
  /// Category guid for root descriptor - назначим категорию явно...
  /// </summary>
  private readonly Guid _rootCategoryGuid = Guid.NewGuid();
  /// <summary>Метаданные атрибута "Контекст состава"</summary>
  private MyAttributeMetadata _contextAttr;
  /// <summary>Идентификатор конструкторской сборочной единицы</summary>
  protected long _artObjectId;
  /// <summary>
  /// Идентификатор версии технологического объекта (На данный момент ТП)
  /// </summary>
  protected long _techObjectId;
  /// <summary>Category id for root descriptor</summary>
  protected int _rootCategoryID;
  /// <summary>Контейнер сервисов</summary>
  protected System.IServiceProvider _services;
  /// <summary>Провайдер составов</summary>
  protected readonly ArtsCompositionDataProvider _dataProvider;
  /// <summary>
  /// Коллекция всех настроек формы, которые надо сохранять в настройках пользователя.
  /// Каждый элемент ссылается на экземпляр HybridDictionary.
  /// </summary>
  protected HybridDictionary _formSettings = new HybridDictionary(0, true);
  /// <summary>
  /// Сервис, позволяющий добавлять кое-какие параметры в запросы
  /// </summary>
  internal static IClientPluginsService PluginsService;
  /// <summary>Текущие настройки фильтрации состава</summary>
  internal static IFiltrationService FiltrationService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Регистрация категории</summary>
  private void RegisterCategory()
  {
    if (this._guidMapper == null)
      return;
    this._rootCategoryID = this._guidMapper.Register(this._rootCategoryGuid);
  }

  /// <summary>Раз регистрация категории</summary>
  private void UnregisterCategory()
  {
    if (this._guidMapper == null || this._rootCategoryID == 0)
      return;
    this._guidMapper.Unregister(this._rootCategoryID);
  }

  /// <summary>
  /// 
  /// </summary>
  protected void InitializeData()
  {
    this._guidMapper = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (!this.DesignMode)
    {
      this.RegisterCategory();
      this.InitializeCustomServices();
    }
    this.InitializeFormLayout();
  }

  /// <summary>Инициализация кастом контролов</summary>
  private void InitializeCustomControls()
  {
    if (this._dataProvider == null)
      return;
    this._dataProvider.BeforeLoadData += new EventHandler(this.OnBeforeLoadData);
    this._dataProvider.AfterLoadData += new EventHandler(this.OnAfterLoadData);
    this._dataProvider.ProgressChanged += new ProgressChangedEventHandler(this.OnProcessChanged);
  }

  /// <summary>Инициализация размеров формы / контролов</summary>
  protected virtual void InitializeFormLayout()
  {
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this.LoadSettings(true);
    if (this._formSettings != null)
      return;
    this._formSettings = new HybridDictionary(0, true);
  }

  /// <summary>Инициализация кастом служб</summary>
  protected virtual void InitializeCustomServices() => this._customServicePresent = true;

  /// <summary>Де-инициализация кастом служб</summary>
  protected virtual void ReleaseCustomServices() => this._customServicePresent = false;

  /// <summary>Загрузить данные в форму</summary>
  /// <returns>true, если загрузка прошла успешно</returns>
  protected virtual bool LoadControlData()
  {
    this._dataProvider.CancelLoadData();
    this.ClearControlData();
    this.UpdateControls();
    this._contextAttr = new MyAttributeMetadata("cad00651-306c-11d8-b4e9-00304f19f545");
    return true;
  }

  /// <summary>Очистка внутренних структур</summary>
  protected virtual void ClearControlData()
  {
  }

  /// <summary>Обновить контролы</summary>
  protected virtual void UpdateControls()
  {
  }

  /// <summary>Загрузка списка контекстов</summary>
  protected void LoadContextsList(ComboBox comboBox)
  {
    if (comboBox == null)
      return;
    comboBox.BeginUpdate();
    try
    {
      comboBox.Items.Clear();
      if (this._contextAttr.AttrPossibleValues == null || this._contextAttr.AttrPossibleValues.Count == 0)
        return;
      for (int index = 0; index < this._contextAttr.AttrPossibleValues.Count; ++index)
        comboBox.Items.Add(this._contextAttr.AttrPossibleValues[index]);
    }
    finally
    {
      if (comboBox.Items.Count > 0)
        comboBox.SelectedIndex = 0;
      comboBox.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>For compatibility only</remarks>
  protected void StartLoadData()
  {
    if (this._dataProvider.LoadedDesignData && this._dataProvider.LoadedTechData)
      return;
    this._dataProvider.StartLoadData(new ObjInfoItem(this._artObjectId), new ObjInfoItem(this._techObjectId));
  }

  /// <summary>For compatibility only</summary>
  protected void CancelLoadData() => this._dataProvider.CancelLoadData();

  /// <summary>
  /// 
  /// </summary>
  protected virtual void DoBeforeLoadData(object sender, EventArgs args)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void DoAfterLoadData(object sender, EventArgs args)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  protected virtual void DoProcessChanged(object sender, ProgressChangedEventArgs args)
  {
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected virtual void LoadSettings(bool loadFormPosition)
  {
    if (!loadFormPosition)
      return;
    FormStorage.LoadLayout((Control) this, (IDictionary) this._formSettings);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected virtual void SaveSettings(bool saveFormPosition)
  {
    if (!saveFormPosition)
      return;
    FormStorage.SaveLayout((Control) this, (IDictionary) this._formSettings);
  }

  /// <summary>Создать экземпляр формы</summary>
  /// <remarks>Don't use - for form designer only</remarks>
  public ArtsCompositionBaseForm()
    : this((ArtsCompositionDataProvider) null)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataProvider"></param>
  public ArtsCompositionBaseForm(ArtsCompositionDataProvider dataProvider)
  {
    this._dataProvider = dataProvider;
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    if (!this._customServicePresent)
      return;
    this.ReleaseCustomServices();
  }

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="artObjectId">Идентификатор версии родительской/конструкторской сборочной единицы</param>
  /// <param name="techObjectId">Идентификатор версии технологического объекта (На данный момент ТП)</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  public bool Initialize(long artObjectId, long techObjectId, System.IServiceProvider viewServices)
  {
    this._artObjectId = artObjectId;
    this._techObjectId = techObjectId;
    this._services = viewServices;
    int num = this.LoadControlData() ? 1 : 0;
    if (num == 0)
      this.ClearControlData();
    this.UpdateControls();
    return num != 0;
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnBeforeLoadData(object sender, EventArgs args)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new EventHandler(this.OnBeforeLoadData), sender, (object) args);
    else
      this.DoBeforeLoadData(sender, args);
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnAfterLoadData(object sender, EventArgs args)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new EventHandler(this.OnAfterLoadData), sender, (object) args);
    else
      this.DoAfterLoadData(sender, args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  private void OnProcessChanged(object sender, ProgressChangedEventArgs args)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ProgressChangedEventHandler(this.OnProcessChanged), sender, (object) args);
    else
      this.DoProcessChanged(sender, args);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.UnregisterCategory();
      if (this._customServicePresent)
        this.ReleaseCustomServices();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArtsCompositionBaseForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.KeyPreview = true;
    this.Name = nameof (ArtsCompositionBaseForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.ResumeLayout(false);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectNavListPageControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Tools.Controls;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

/// <summary>
/// Закладка мастера для выбора объектов указанного типа из мастера
/// </summary>
public class SelectObjectNavListPageControl : UserControl, IWizardPage, ISelectedItemsHost
{
  /// <summary>Тип выбираемого объекта</summary>
  private int _objectTypeId;
  /// <summary>Иконка страницы мастера</summary>
  private Image _image;
  /// <summary>Признак наличия загруженных данных</summary>
  private bool _dataLoaded;
  /// <summary>
  /// Коллекция всех настроек, которые надо сохранять в настройках пользователя.
  /// Каждый элемент ссылается на экземпляр HybridDictionary.
  /// </summary>
  private readonly IDictionary _controlSettings = (IDictionary) new HybridDictionary(0, true);
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Загрузка списка объектов</summary>
  private bool LoadControlData(IWizardPage previousPage)
  {
    this.TechNavigatorControl.SelectedItemsChanged -= new EventHandler(this.TechNavControlSelectedItemsChangedEvent);
    try
    {
      return this.DoLoadControlData(previousPage);
    }
    finally
    {
      this.TechNavigatorControl.SelectedItemsChanged += new EventHandler(this.TechNavControlSelectedItemsChangedEvent);
    }
  }

  /// <summary>Инициализация пользовательских контролов</summary>
  private void InitializeCustomControls()
  {
    this.TechNavigatorControl = new TechNavigatorControl();
    this.Controls.Add((Control) this.TechNavigatorControl);
    this.TechNavigatorControl.Dock = DockStyle.Fill;
    this.TechNavigatorControl.BringToFront();
    this.TechNavigatorControl.DoubleClick += new TechNavigatorEventHandler(this.TechNavControlDoubleClickEvent);
    this.TechNavigatorControl.Location = new Point(8, 8);
    this.TechNavigatorControl.Name = "techNavControl";
    this.TechNavigatorControl.ViewsManager.AllowedViews = new string[2]
    {
      "ChildrenView",
      "SelectionViewObject"
    };
    this.TechNavigatorControl.TabIndex = 0;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadSettings()
  {
    FormStorage.LoadLayout((Control) this, this._controlSettings);
    this.TechNavigatorControl?.LoadLayout(this._controlSettings);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveSettings()
  {
    this.TechNavigatorControl?.SaveLayout(this._controlSettings);
    FormStorage.SaveLayout((Control) this, this._controlSettings);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="previousPage"></param>
  /// <returns></returns>
  protected virtual bool DoLoadControlData(IWizardPage previousPage)
  {
    LoadPageControlEventArgs e = new LoadPageControlEventArgs(previousPage);
    LoadPageControlEventHandler loadPageControlData = this.LoadPageControlData;
    if (loadPageControlData != null)
      loadPageControlData((Control) this, e);
    return e.DataLoaded;
  }

  /// <summary>Конструктор</summary>
  public SelectObjectNavListPageControl()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
  }

  /// <summary>Активация закладки</summary>
  /// <param name="prevPage"></param>
  /// <param name="rollback"></param>
  public void Activate(IWizardPage prevPage, bool rollback)
  {
    if (rollback || this._dataLoaded)
      return;
    this._dataLoaded = this.LoadControlData(prevPage);
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete == null)
      return;
    pageComplete((object) this, new PageCompleteEventArgs(this.IsValidSelectedItems));
  }

  /// <summary>Деактивация закладки</summary>
  /// <param name="nextPage"></param>
  /// <param name="rollback"></param>
  public void Deactivate(IWizardPage nextPage, bool rollback) => this.SaveSettings();

  /// <summary>
  /// Признак, если работа пользователя с этой страницей действительно может быть закончена.
  /// Вызывается при нажатии пользователем кнопки "Вперед/Готово".
  /// </summary>
  public bool ReallyComplete
  {
    get
    {
      ISelectedItems selectedItems = this.SelectedItems;
      return selectedItems != null && selectedItems.Any();
    }
  }

  /// <summary>
  /// Позволяет сохранить/обработать результаты работы страницы мастера. Вызывается при нажатии
  /// пользователем кнопки "Вперед/Готово" до смены страниц мастера.
  /// </summary>
  public void DoMagic()
  {
  }

  /// <summary>
  /// Визуальный элемент управления, реализующий страницу мастера.
  /// </summary>
  public Control Control
  {
    get => (Control) this.TechNavigatorControl;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public IWizard Wizard { get; set; }

  /// <summary>Название страницы мастера.</summary>
  public string Caption { get; set; }

  /// <summary>Описание страницы мастера.</summary>
  public string Description { get; set; }

  /// <summary>Иконка страницы мастера.</summary>
  public Image Image
  {
    get
    {
      if (this._image != null)
        return this._image;
      Icon icon1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false)?.GetIcon(4, this.ObjectTypeId);
      if (icon1 != null)
      {
        using (Icon icon2 = ImagesResizeHelper.ResizeIconTo16x16(icon1, Color.Transparent))
          this._image = (Image) icon2.ToBitmap();
      }
      return this._image;
    }
  }

  /// <summary>
  /// Событие, когда пользователь ввел все необходимые данные на этой странице и может
  /// перейти к следующей странице мастера. По этому событию мастер включает и выключает
  /// кнопку "Далее/Готово".
  /// </summary>
  public event EventHandler<PageCompleteEventArgs> PageComplete;

  /// <summary>Описание выбранных элементов</summary>
  public ISelectedItems SelectedItems => this.TechNavigatorControl?.ItemsHost?.SelectedItems;

  /// <summary>
  /// 
  /// </summary>
  internal bool IsValidSelectedItems
  {
    get
    {
      ISelectedItems selectedItems = this.SelectedItems;
      bool validSelectedItems = selectedItems != null && selectedItems.Any();
      if (validSelectedItems && this.ObjectTypeId != 0)
      {
        for (int index = 0; index < this.SelectedItems.Count; ++index)
        {
          IDBObjectTypeID itemData = this.SelectedItems.GetItemData<IDBObjectTypeID>(index, false);
          if (itemData == null || !MetaDataHelper.IsObjectTypeChildOf(itemData.Value, this.ObjectTypeId))
          {
            validSelectedItems = false;
            break;
          }
        }
      }
      return validSelectedItems;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>Тип объекта для отображения</summary>
  public int ObjectTypeId
  {
    get => this._objectTypeId;
    set
    {
      if (this._objectTypeId == value)
        return;
      this._objectTypeId = value;
      this._dataLoaded = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public TechNavigatorControl TechNavigatorControl { get; private set; }

  /// <summary>Событие на загрузку данных закладки</summary>
  public event LoadPageControlEventHandler LoadPageControlData;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechNavControlDoubleClickEvent(object sender, TechNavigatorEventArgs e)
  {
    if (!this.IsValidSelectedItems || !(this.Wizard is Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl wizard))
      return;
    wizard.GotoNextPage();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechNavControlSelectedItemsChangedEvent(object sender, EventArgs e)
  {
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete != null)
      pageComplete((object) this, new PageCompleteEventArgs(this.IsValidSelectedItems));
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged(sender, e);
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
    this.AutoScaleMode = AutoScaleMode.Font;
  }

  [SpecialName]
  string IWizardPage.get_Name() => this.Name;
}

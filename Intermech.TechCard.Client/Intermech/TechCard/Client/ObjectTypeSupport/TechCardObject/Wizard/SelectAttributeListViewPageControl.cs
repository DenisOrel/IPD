// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectAttributeListViewPageControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.PropertyEditors;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

public class SelectAttributeListViewPageControl : UserControl, IWizardPage
{
  /// <summary>Иконка страницы мастера</summary>
  private Image _image;
  /// <summary>Признак наличия загруженных данных</summary>
  private bool _dataLoaded;
  /// <summary>
  /// Информация об элементе, атрибуты которого отображает контрол
  /// </summary>
  private Intermech.Client.Core.FormDesigner.Controls.ElementInfo _elementInfo;
  /// <summary>
  /// Служба регистрации обработчиков атрибутов для ObjectPropertyGrid
  /// </summary>
  private IAttributePropertyDescriberService _propertyDescriberService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация пользовательских контролов</summary>
  private void InitializeCustomControls()
  {
    this.AttrListViewControl = new ListView();
    this.Controls.Add((Control) this.AttrListViewControl);
    this.AttrListViewControl.Dock = DockStyle.Fill;
    this.AttrListViewControl.BringToFront();
    this.AttrListViewControl.Location = new Point(8, 8);
    this.AttrListViewControl.Name = "AttrListViewControl";
    this.AttrListViewControl.TabIndex = 0;
    this.AttrListViewControl.View = View.Details;
    this.AttrListViewControl.Columns.Add("Наименование");
    this.AttrListViewControl.Columns.Add("Значение");
    this.AttrListViewControl.FullRowSelect = true;
    this.AttrListViewControl.GridLines = true;
    this.AttrListViewControl.CheckBoxes = true;
  }

  private void InitializeCustomService()
  {
    this._propertyDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
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
  public SelectAttributeListViewPageControl(Intermech.Client.Core.FormDesigner.Controls.ElementInfo elementInfo)
  {
    this._elementInfo = elementInfo;
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
    this.InitializeCustomService();
  }

  /// <summary>Контрол со значениями атрибутов</summary>
  public ListView AttrListViewControl { get; private set; }

  /// <summary>Словарь отмеченных элементов ListView</summary>
  public Dictionary<Intermech.Client.Core.FormDesigner.Controls.ElementInfo, List<AttributeValues>> SelectedAttributes { get; set; } = new Dictionary<Intermech.Client.Core.FormDesigner.Controls.ElementInfo, List<AttributeValues>>();

  /// <summary>
  /// Информация об элементе, атрибуты которого отображает контрол
  /// </summary>
  public Intermech.Client.Core.FormDesigner.Controls.ElementInfo ElementInfo => this._elementInfo;

  /// <summary>Событие на загрузку данных закладки</summary>
  public event LoadPageControlEventHandler LoadPageControlData;

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
    pageComplete((object) this, new PageCompleteEventArgs(this.SelectedAttributes.Count > 0));
  }

  /// <summary>Деактивация закладки</summary>
  /// <param name="nextPage"></param>
  /// <param name="rollback"></param>
  public void Deactivate(IWizardPage nextPage, bool rollback)
  {
  }

  /// <summary>
  /// Признак, если работа пользователя с этой страницей действительно может быть закончена.
  /// Вызывается при нажатии пользователем кнопки "Вперед/Готово".
  /// </summary>
  public bool ReallyComplete => this.SelectedAttributes.Count != 0;

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
  public Control Control => (Control) this.AttrListViewControl;

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
      Icon icon1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false)?.GetIcon(3, 0);
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

  /// <summary>Загрузка списка атрибутов</summary>
  private bool LoadControlData(IWizardPage previousPage)
  {
    this.AttrListViewControl.ItemChecked -= new ItemCheckedEventHandler(this.AttrListViewControl_ItemChecked);
    try
    {
      return this.DoLoadControlData(previousPage);
    }
    finally
    {
      this.AttrListViewControl.ItemChecked += new ItemCheckedEventHandler(this.AttrListViewControl_ItemChecked);
    }
  }

  /// <summary>Изменения отметки строки атрибута</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void AttrListViewControl_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    if (this.SelectedAttributes == null)
      return;
    if (this.AttrListViewControl.CheckedItems.Count != 0)
    {
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      foreach (ListViewItem checkedItem in this.AttrListViewControl.CheckedItems)
      {
        if (checkedItem.Tag is AttributeValues tag)
          attributeValuesList.Add(tag);
      }
      this.SelectedAttributes[this._elementInfo] = attributeValuesList;
    }
    else
      this.SelectedAttributes.Remove(this._elementInfo);
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete == null)
      return;
    pageComplete((object) this, new PageCompleteEventArgs(this.SelectedAttributes.Count > 0));
  }

  /// <summary>Загрузка значений атрибутов в ListView для элемента</summary>
  /// <param name="attributable"></param>
  public void LoadListViewAttributeValues(AttributeValues[] attributeValues)
  {
    this.AttrListViewControl.Items.Clear();
    if (attributeValues == null || attributeValues.Length == 0)
      return;
    foreach (AttributeValues attributeValue in (IEnumerable<AttributeValues>) ((IEnumerable<AttributeValues>) attributeValues).Where<AttributeValues>((Func<AttributeValues, bool>) (a => a.AttributeID > 0 && a.ComputeMode == ComputeValueModes.NotComputableValue)).OrderBy<AttributeValues, string>((Func<AttributeValues, string>) (a => a.AttributeName)))
      this.AttrListViewControl.Items.Add(new ListViewItem(new string[2]
      {
        attributeValue.AttributeName,
        this.GetAttributeValue(attributeValue, this._elementInfo)
      })
      {
        Tag = (object) attributeValue
      });
    this.RefreshColumnsHeader();
  }

  /// <summary>Получить значение атрибута как для PropertyGrid</summary>
  /// <param name="attributeValue"></param>
  /// <param name="elementInfo"></param>
  /// <param name="propertyDescriberService"></param>
  /// <returns></returns>
  private string GetAttributeValue(AttributeValues attributeValue, Intermech.Client.Core.FormDesigner.Controls.ElementInfo elementInfo)
  {
    if (attributeValue.Values == null || attributeValue.Values[0].IsNullOrDBNull())
      return string.Empty;
    if (this._propertyDescriberService != null)
    {
      IAttributePropertyDescriber describer = this._propertyDescriberService.GetDescriber(attributeValue.AttributeID);
      if (describer != null)
      {
        object propDescriptorValue = describer.GetPropDescriptorValue((IElementInfo) elementInfo, attributeValue.AttributeID, attributeValue.Values[0]);
        return propDescriptorValue == null ? string.Empty : propDescriptorValue.ToString();
      }
    }
    object[] descriptions = attributeValue.Descriptions;
    if ((descriptions != null ? ((IEnumerable<object>) descriptions).First<object>() : (object) null) != null)
      return attributeValue.Descriptions[0].ToString();
    return attributeValue.Value == DBNull.Value ? string.Empty : attributeValue.Value.ToString();
  }

  /// <summary>Обновить ширину столбцов</summary>
  private void RefreshColumnsHeader()
  {
    int count = this.AttrListViewControl.Columns.Count;
    foreach (ColumnHeader column in this.AttrListViewControl.Columns)
      column.Width = (this.AttrListViewControl.Width - 4) / count;
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

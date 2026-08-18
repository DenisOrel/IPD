// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.ApplyGroupAttributesWizard
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Мастер выбора атрибутов группового объекта и единичных объектов для записи этих атрибутов
/// </summary>
public class ApplyGroupAttributesWizard : WizardForm
{
  /// <summary>
  /// Битовый набор, определяющий режимам формирования массива структур AttributeValues
  /// </summary>
  private GetAttributeValuesModes _attributeValuesMode = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption;
  /// <summary>
  /// Идентификатор группового объекта, из которого зачитываем атрибуты
  /// </summary>
  private long _groupObjId;
  /// <summary>
  /// Идентификатор связи с групповым объектом, из которой зачитываем атрибуты
  /// </summary>
  private long _relationId;
  /// <summary>
  /// Словарь объектов для отображения на шаге выбора единичных объектов
  /// </summary>
  private Dictionary<int, List<long>> _unitItems;
  /// <summary>
  /// Коллекция всех настроек, которые надо сохранять в настройках пользователя.
  /// Каждый элемент ссылается на экземпляр HybridDictionary.
  /// </summary>
  private readonly IDictionary _controlSettings = (IDictionary) new HybridDictionary(0, true);
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ApplyGroupAttributesWizard()
  {
    this.InitializeComponent();
    this.LoadSettings();
    this.Closing += new CancelEventHandler(this.ApplyGroupAttributesWizard_Closing);
  }

  private void LoadSettings() => FormStorage.LoadLayout((Control) this, this._controlSettings);

  private void SaveSettings() => FormStorage.SaveLayout((Control) this, this._controlSettings);

  private void ApplyGroupAttributesWizard_Closing(object sender, CancelEventArgs e)
  {
    this.SaveSettings();
  }

  /// <summary>Инициировать страницы мастера</summary>
  /// <param name="unitItems">Коллекция единичных объектов для выбора</param>
  /// <param name="groupObjId">Идентификатор группового объекта</param>
  public void InitializePageCollection(Dictionary<int, List<long>> unitItems, long groupObjId)
  {
    this.InitializePageCollection(unitItems, groupObjId, 0L);
  }

  /// <summary>Инициировать страницы мастера</summary>
  /// <param name="unitItems">Коллекция единичных объектов для выбора</param>
  /// <param name="groupObjId">Идентификатор группового объекта</param>
  /// <param name="relationId">Идентификатор родительской связи группового объекта</param>
  public void InitializePageCollection(
    Dictionary<int, List<long>> unitItems,
    long groupObjId,
    long relationId)
  {
    this._unitItems = unitItems;
    this._groupObjId = groupObjId;
    this._relationId = relationId;
    if (this.DesignMode)
      return;
    this.InitializeCustomComponent();
  }

  public List<long> SelectedUnitItems { get; } = new List<long>();

  public Dictionary<ElementInfo, List<AttributeValues>> SelectedAttributes { get; } = new Dictionary<ElementInfo, List<AttributeValues>>();

  public GetAttributeValuesModes AttributeValuesMode
  {
    get => this._attributeValuesMode;
    set => this._attributeValuesMode = value;
  }

  /// <summary>Инициализация пользовательских контролов</summary>
  private void InitializeCustomComponent()
  {
    SelectObjectTreeViewPageControl treeViewPageControl = new SelectObjectTreeViewPageControl()
    {
      ItemsMode = SelectedItemsMode.CheckedItems
    };
    treeViewPageControl.Caption = treeViewPageControl.Description = "Выберите единичные объекты для записи атрибутов";
    treeViewPageControl.LoadPageControlData += new LoadPageControlEventHandler(this.SelectUnitaryObjectPage_LoadPageControlData);
    treeViewPageControl.TreeViewControl.CheckStateChanged += new EventHandler<NodeEventArgs>(this.SelectUnitaryObjectPage_CheckStateChanged);
    treeViewPageControl.ObjectTypeId = this._unitItems.First<KeyValuePair<int, List<long>>>().Key;
    this.Pages.Add((IWizardPage) treeViewPageControl);
    treeViewPageControl.TreeViewControl.CheckoutMode = TechCheckoutMode.Manual;
    treeViewPageControl.TreeViewControl.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    if (this._groupObjId != 0L)
    {
      SelectAttributeListViewPageControl listViewPageControl = new SelectAttributeListViewPageControl(new ElementInfo(this._groupObjId, AttributableElements.Object));
      listViewPageControl.Caption = listViewPageControl.Description = "Выберите атрибуты объекта для записи в единичные объекты";
      listViewPageControl.LoadPageControlData += new LoadPageControlEventHandler(this.SelectAttributePage_LoadPageControlData);
      listViewPageControl.SelectedAttributes = this.SelectedAttributes;
      this.Pages.Add((IWizardPage) listViewPageControl);
    }
    if (this._relationId == -1L || this._relationId == 0L)
      return;
    SelectAttributeListViewPageControl listViewPageControl1 = new SelectAttributeListViewPageControl(new ElementInfo(this._relationId, AttributableElements.Relation));
    listViewPageControl1.Caption = listViewPageControl1.Description = "Выберите атрибуты связи для записи в единичные объекты";
    listViewPageControl1.LoadPageControlData += new LoadPageControlEventHandler(this.SelectAttributePage_LoadPageControlData);
    listViewPageControl1.SelectedAttributes = this.SelectedAttributes;
    this.Pages.Add((IWizardPage) listViewPageControl1);
  }

  /// <summary>Загрузить значения атрибутов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectAttributePage_LoadPageControlData(Control sender, LoadPageControlEventArgs e)
  {
    if (!(sender is SelectAttributeListViewPageControl listViewPageControl))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable dbAttributable = (IDBAttributable) null;
      switch (listViewPageControl.ElementInfo.ElementKind)
      {
        case AttributableElements.Object:
          dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(listViewPageControl.ElementInfo.ElementIdentifier, false);
          break;
        case AttributableElements.Relation:
          dbAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(listViewPageControl.ElementInfo.ElementIdentifier, false);
          break;
      }
      if (dbAttributable == null)
        return;
      AttributeValues[] attributesValues = dbAttributable.GetAttributesValues(this.AttributeValuesMode);
      listViewPageControl.LoadListViewAttributeValues(attributesValues);
    }
  }

  /// <summary>
  /// Изменена отметка узла в диалоге выбора единичных объектов
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectUnitaryObjectPage_CheckStateChanged(object sender, NodeEventArgs e)
  {
    this.SelectedUnitItems.Clear();
    ISelectedItems selectedItems = sender is NavigatorTreeView navigatorTreeView ? navigatorTreeView.SelectedItems : (ISelectedItems) null;
    if (selectedItems == null || selectedItems.Count == 0)
      return;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      IDBObjectID itemData = selectedItems.GetItemData<IDBObjectID>(index, false);
      if (itemData != null)
        this.SelectedUnitItems.Add(itemData.Value);
    }
  }

  /// <summary>Загрузка данных контрола выбора единичных объектов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectUnitaryObjectPage_LoadPageControlData(
    Control sender,
    LoadPageControlEventArgs e)
  {
    if (this._unitItems == null || this._unitItems.Count == 0)
      return;
    IDescriptor rootDescriptor = (IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this._unitItems.First<KeyValuePair<int, List<long>>>().Key, MetaDataHelper.GetObjectTypeName(this._unitItems.First<KeyValuePair<int, List<long>>>().Key), this._unitItems)
    {
      ExpandNodes = false
    };
    if (sender is SelectObjectTreeViewPageControl treeViewPageControl)
      treeViewPageControl.TreeViewControl?.Build(rootDescriptor);
    if (e == null)
      return;
    e.DataLoaded = false;
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
    this.ClientSize = new Size(800, 450);
    this.Text = nameof (ApplyGroupAttributesWizard);
  }
}

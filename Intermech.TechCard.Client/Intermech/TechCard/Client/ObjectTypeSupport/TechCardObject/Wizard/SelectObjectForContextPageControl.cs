// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectForContextPageControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

public class SelectObjectForContextPageControl : 
  UserControl,
  IWizardPage,
  ISelectedItemsHost,
  IIOSource
{
  /// <summary>Иконка страницы мастера</summary>
  private Image _image;
  /// <summary>Признак наличия загруженных данных</summary>
  private bool _dataLoaded;
  /// <summary>Тип выбираемого объекта</summary>
  private int _objectTypeId;
  /// <summary>
  /// 
  /// </summary>
  private readonly AdvancedServiceContainer _serviceContainer = new AdvancedServiceContainer();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal ObjectsViewBase tcnolcObjectList;

  /// <summary>Инициализация пользовательских контролов</summary>
  private void InitializeCustomControls()
  {
    this.tcnolcObjectList.DisableGroupBox = true;
    this.tcnolcObjectList.DisableToolBar = true;
    this.tcnolcObjectList.DisableStatusBar = true;
    this.tcnolcObjectList.DisableColumnsGrouping = true;
    this.tcnolcObjectList.ViewContentType = ContentType.Folders;
  }

  /// <summary>Загрузка списка объектов</summary>
  private bool LoadControlData([NotNull] ISelectedItems contextSelectedItems)
  {
    List<long> objectIDs = new List<long>();
    List<ObjInfoItem> contextObjInfoItemList = new List<ObjInfoItem>();
    List<IDBTypedObjectID> result;
    contextSelectedItems.TryGetItems<IDBTypedObjectID>(out result);
    if (result != null)
      result.InvokeForAll<IDBTypedObjectID>((Action<IDBTypedObjectID>) (item => contextObjInfoItemList.Add(new ObjInfoItem(item.ObjectID, item.ObjectType))));
    if (contextObjInfoItemList.Count != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
        };
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[3]
        {
          TechCardConsts.ObjectTypes.ProcRoutingID,
          TechCardConsts.ObjectTypes.CehRouteID,
          TechCardConsts.ObjectTypes.TechProcBaseID
        });
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) contextObjInfoItemList, (IEnumerable<int>) new int[1]
        {
          this._objectTypeId
        }, (IEnumerable<int>) childrenIdRecursive, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, true, false, -1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
        DataTable source = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
        if (source != null)
          objectIDs.AddRange((IEnumerable<long>) source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, 0, 0L))));
      }
    }
    this.tcnolcObjectList.Grid.BeginUpdate();
    try
    {
      this.tcnolcObjectList.Initialize((IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this.ObjectTypeId, string.Empty, (IList) objectIDs)
      {
        Mode = TechObjectListMode.UniqueValue
      }, this.Services);
      this.tcnolcObjectList.Activate((IView) null);
    }
    finally
    {
      this.tcnolcObjectList.Grid.EndUpdate();
    }
    return true;
  }

  /// <summary>Конструктор</summary>
  public SelectObjectForContextPageControl()
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
  }

  /// <summary>Активация закладки</summary>
  /// <param name="prevPage"></param>
  /// <param name="rollback"></param>
  public void Activate(IWizardPage prevPage, bool rollback)
  {
    if (rollback)
      return;
    if (prevPage is ISelectedItemsHost selectedItemsHost)
      this._dataLoaded = this.LoadControlData(selectedItemsHost.SelectedItems);
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete == null)
      return;
    ISelectedItems selectedItems = this.SelectedItems;
    pageComplete((object) this, new PageCompleteEventArgs(selectedItems != null && selectedItems.Any()));
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
  public Control Control => (Control) this.tcnolcObjectList;

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
      Icon icon1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false)?.GetIcon(4, this._objectTypeId);
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

  /// <summary>
  /// 
  /// </summary>
  object IIOSource.Control
  {
    get => (object) this.Control;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public System.IServiceProvider Services
  {
    get => (System.IServiceProvider) this._serviceContainer;
    set => this._serviceContainer.AdvancedProvider = value;
  }

  /// <summary>Описание выбранных элементов</summary>
  ISelectedItems IIOSource.SelectedItems
  {
    get => this.tcnolcObjectList.SelectedItems;
    set => ((IIOSource) this.tcnolcObjectList).SelectedItems = value;
  }

  /// <summary>Описание выбранных элементов</summary>
  public ISelectedItems SelectedItems => ((IIOSource) this).SelectedItems;

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>Тип выбираемого объекта</summary>
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
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tcnolcObjectList_SelectedItemsChanged(object sender, EventArgs e)
  {
    EventHandler<PageCompleteEventArgs> pageComplete = this.PageComplete;
    if (pageComplete != null)
    {
      EventHandler<PageCompleteEventArgs> eventHandler = pageComplete;
      ISelectedItems selectedItems = this.SelectedItems;
      PageCompleteEventArgs e1 = new PageCompleteEventArgs(selectedItems != null && selectedItems.Any());
      eventHandler((object) this, e1);
    }
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
    this.tcnolcObjectList = new ObjectsViewBase();
    this.SuspendLayout();
    this.tcnolcObjectList.AllowCustomGroupValues = true;
    this.tcnolcObjectList.Control = (object) this.tcnolcObjectList;
    this.tcnolcObjectList.DisableColumnsGrouping = true;
    this.tcnolcObjectList.DisableColumnsSorting = true;
    this.tcnolcObjectList.DisableGroupBox = true;
    this.tcnolcObjectList.DisableIMContextMenu = true;
    this.tcnolcObjectList.DisableKeyDownEvents = false;
    this.tcnolcObjectList.DisableKeyUpEvents = true;
    this.tcnolcObjectList.DisableStatusBar = true;
    this.tcnolcObjectList.DisableToolBar = true;
    this.tcnolcObjectList.Dock = DockStyle.Fill;
    this.tcnolcObjectList.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.tcnolcObjectList.Font = new Font("Tahoma", 8.25f);
    this.tcnolcObjectList.Location = new Point(0, 0);
    this.tcnolcObjectList.Name = "tcnolcObjectList";
    this.tcnolcObjectList.Size = new Size(344, 291);
    this.tcnolcObjectList.TabIndex = 11;
    this.tcnolcObjectList.ViewContentType = ContentType.NonFolders;
    this.tcnolcObjectList.SelectedItemsChanged += new EventHandler(this.tcnolcObjectList_SelectedItemsChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tcnolcObjectList);
    this.Name = nameof (SelectObjectForContextPageControl);
    this.Size = new Size(344, 291);
    this.ResumeLayout(false);
  }

  [SpecialName]
  string IWizardPage.get_Name() => this.Name;
}

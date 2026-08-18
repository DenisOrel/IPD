// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectFromProductionReportWizardControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

public class SelectObjectFromProductionReportWizardControl : DockWizardView
{
  /// <summary>Тип выбираемого объекта</summary>
  private int _objectTypeId;
  /// <summary>
  /// 
  /// </summary>
  private SelectObjectForContextPageControl _selectObjectPage;
  /// <summary>Guid категория для закладки</summary>
  private static readonly Guid _rootCategoryNodeGuid = new Guid("{ABBA590B-6109-4108-9FEB-501851341E2C}");
  /// <summary>Идентификатор категория для закладки</summary>
  private static int _rootCategoryNodeId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация "прочих" пользовательских контролов</summary>
  private void InitializeCustomComponent()
  {
    SelectObjectNavListPageControl navListPageControl1 = new SelectObjectNavListPageControl()
    {
      ObjectTypeId = MRP2Consts.objtypeIdProductionLists
    };
    navListPageControl1.Caption = navListPageControl1.Description = "Выберите производственную ведомость";
    navListPageControl1.LoadPageControlData += (LoadPageControlEventHandler) ((sender, args) =>
    {
      if (!(sender is SelectObjectNavListPageControl navListPageControl3))
        return;
      IDescriptor descriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(navListPageControl3.ObjectTypeId);
      navListPageControl3.TechNavigatorControl.RootDescriptor = descriptor;
      args.DataLoaded = true;
    });
    this.Pages.Add((IWizardPage) navListPageControl1);
    SelectObjectTreeViewPageControl treeViewPageControl = new SelectObjectTreeViewPageControl()
    {
      ObjectTypeId = MRP2Consts.objtypeIdProductionObjects
    };
    treeViewPageControl.Caption = treeViewPageControl.Description = "Выберите объект из состава производственной ведомости";
    treeViewPageControl.LoadPageControlData += new LoadPageControlEventHandler(this.SelectObjectFromProductionReportComposition_LoadPageControlData);
    this.Pages.Add((IWizardPage) treeViewPageControl);
    this._selectObjectPage = new SelectObjectForContextPageControl();
    this._selectObjectPage.SelectedItemsChanged += new EventHandler(this.SelectObjectPageOnSelectedItemsChanged);
    this._selectObjectPage.Services = (System.IServiceProvider) this._serviceContainer;
    this.Pages.Add((IWizardPage) this._selectObjectPage);
  }

  /// <summary>Конструктор</summary>
  public SelectObjectFromProductionReportWizardControl()
  {
    this.InitializeComponent();
    this.InitializeCustomComponent();
    this.Caption = LocalizationHolder.rm.GetString("TechCard.Client_547");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectTypeId = -1;
    this.Services = provider;
    if (items == null)
      return;
    this._objectTypeId = items.GetItemData<IDescriptor>(0, false) is HiveDescriptor itemData ? itemData.TypeID : -1;
    this._selectObjectPage.ObjectTypeId = this._objectTypeId;
    this._selectObjectPage.Name = this._selectObjectPage.Caption = this._selectObjectPage.Description = "Выберите " + MetaDataHelper.GetObjectName(this._objectTypeId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectObjectPageOnSelectedItemsChanged(object sender, EventArgs e)
  {
    this.DoSelectedItemsChanged(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectObjectFromProductionReportComposition_LoadPageControlData(
    System.Windows.Forms.Control sender,
    LoadPageControlEventArgs e)
  {
    IDescriptor descriptor = (IDescriptor) null;
    if (e?.PreviousPage is ISelectedItemsHost previousPage)
    {
      List<ObjInfoItem> contextObjInfoItemList = new List<ObjInfoItem>();
      List<IDBTypedObjectID> result;
      previousPage.SelectedItems.TryGetItems<IDBTypedObjectID>(out result);
      if (result != null)
        result.InvokeForAll<IDBTypedObjectID>((Action<IDBTypedObjectID>) (item => contextObjInfoItemList.Add(new ObjInfoItem(item.ObjectID, item.ObjectType))));
      if (contextObjInfoItemList.Any<ObjInfoItem>())
        descriptor = (IDescriptor) new TechCompositionDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, contextObjInfoItemList[0].ObjTypeID, contextObjInfoItemList[0].ObjectID, MRP2Consts.objtypeIdProductionObjects, (IEnumerable<int>) new int[1]
        {
          MRP2Consts.reltypeIdProductComposition
        }, string.Empty, RelatedObjectsRole.Composition, (ITechCompositionFilter) null, (IEnumerable<NodeColumnID>) null);
    }
    IDescriptor rootDescriptor = descriptor ?? (IDescriptor) new TechCompositionDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, 0L, TechCardConsts.ObjectTypes.TechBaseObjectID, -1, string.Empty, RelatedObjectsRole.Composition, (ITechCompositionFilter) null);
    if (sender is SelectObjectTreeViewPageControl treeViewPageControl)
      treeViewPageControl.TreeViewControl?.Build(rootDescriptor);
    if (e == null)
      return;
    e.DataLoaded = false;
  }

  /// <summary>Идентификатор категория для закладки</summary>
  public static int RootCategoryNodeId
  {
    get
    {
      if (SelectObjectFromProductionReportWizardControl._rootCategoryNodeId != 0)
        return SelectObjectFromProductionReportWizardControl._rootCategoryNodeId;
      SelectObjectFromProductionReportWizardControl._rootCategoryNodeId = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true).Register(SelectObjectFromProductionReportWizardControl._rootCategoryNodeGuid);
      ICategoryTypeIconService service1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
      if (service1 != null)
      {
        Icon icon = service1.GetIcon(4, MRP2Consts.objtypeIdProductionLists);
        if (icon != null)
          service1.AddIcon(icon, SelectObjectFromProductionReportWizardControl._rootCategoryNodeId);
      }
      IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false);
      if (service2 != null)
        SelectObjectFromProductionReportWizardProvider.RegisterViewProvider(service2);
      return SelectObjectFromProductionReportWizardControl._rootCategoryNodeId;
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
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}

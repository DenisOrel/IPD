// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectFromProductionCopyWizardControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

public class SelectObjectFromProductionCopyWizardControl : DockWizardView
{
  /// <summary>Идентификатор головного объекта - ДСЕ / ПК ДСЕ</summary>
  private long _rootObjectId;
  /// <summary>Тип выбираемого объекта</summary>
  private int _objectTypeId;
  /// <summary>
  /// 
  /// </summary>
  private SelectObjectForContextPageControl _selectObjectPage;
  /// <summary>Guid категория для закладки</summary>
  private static readonly Guid _rootCategoryNodeGuid = new Guid("{DD7543FB-E185-4426-B288-F36CFDD09F7C}");
  /// <summary>Идентификатор категория для закладки</summary>
  private static int _rootCategoryNodeId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация "прочих" пользовательских контролов</summary>
  private void InitializeCustomComponent()
  {
    SelectObjectNavListPageControl navListPageControl = new SelectObjectNavListPageControl()
    {
      ObjectTypeId = MRP2Consts.objtypeIdProductionCopy
    };
    navListPageControl.Caption = navListPageControl.Description = "Выберите производственную копию ДСЕ";
    navListPageControl.LoadPageControlData += new LoadPageControlEventHandler(this.SelectArticlePageOnLoadPageControlData);
    this.Pages.Add((IWizardPage) navListPageControl);
    this._selectObjectPage = new SelectObjectForContextPageControl();
    this._selectObjectPage.SelectedItemsChanged += new EventHandler(this.SelectObjectPageOnSelectedItemsChanged);
    this._selectObjectPage.Services = (System.IServiceProvider) this._serviceContainer;
    this.Pages.Add((IWizardPage) this._selectObjectPage);
  }

  /// <summary>Конструктор</summary>
  public SelectObjectFromProductionCopyWizardControl()
  {
    this.InitializeComponent();
    this.InitializeCustomComponent();
    this.Caption = LocalizationHolder.rm.GetString("TechCard.Client_548");
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
    this._objectTypeId = items.GetItemData<IDescriptor>(0, false) is TypedHiveDescriptor<long> itemData ? itemData.TypeID : -1;
    this._rootObjectId = itemData != null ? itemData.Data : 0L;
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
  private void SelectArticlePageOnLoadPageControlData(System.Windows.Forms.Control sender, LoadPageControlEventArgs e)
  {
    if (!(sender is SelectObjectNavListPageControl navListPageControl))
      return;
    e.DataLoaded = true;
    if (this._rootObjectId == 0L)
      return;
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long conditionValue = 0;
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._rootObjectId);
      if (MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, TechCardConsts.ObjectTypes.ArticleBaseID))
        conditionValue = objectInfo.ObjectID;
      else if (MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, MRP2Consts.objtypeIdProductionCopy))
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(objectInfo.ObjectID, MRP2Consts.attrIdArticleLink);
        conditionValue = objectAttributeById != null ? objectAttributeById.AsInteger : 0L;
      }
      if (conditionValue == 0L)
        return;
      DBRecordSetParams dbRsp = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(MRP2Consts.attrIdArticleLink, RelationalOperators.Equal, (object) conditionValue, (object) null, LogicalOperators.NONE, 0, false)
      }, ObjInfoDbScheme.GetSourceTableColumns().ToArray<ColumnDescriptor>());
      DataTable objectDataEx = DataHelper.GetObjectDataEx((IEnumerable<int>) new int[1]
      {
        MRP2Consts.objtypeIdProductionCopy
      }, sessionKeeper.Session, dbRsp, (IEnumerable<ObjInfoItem>) null);
      new ObjInfoDbScheme().ParseItems(objectDataEx != null ? (IEnumerable<DataRow>) objectDataEx.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objInfoItemList);
    }
    IDescriptor descriptor = (IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MRP2Consts.objtypeIdProductionCopy, "Производственные копии ДСЕ", ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoItemList))
    {
      ExpandNodes = false
    };
    navListPageControl.TechNavigatorControl.RootDescriptor = descriptor;
  }

  /// <summary>Идентификатор категория для закладки</summary>
  public static int RootCategoryNodeId
  {
    get
    {
      if (SelectObjectFromProductionCopyWizardControl._rootCategoryNodeId != 0)
        return SelectObjectFromProductionCopyWizardControl._rootCategoryNodeId;
      SelectObjectFromProductionCopyWizardControl._rootCategoryNodeId = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true).Register(SelectObjectFromProductionCopyWizardControl._rootCategoryNodeGuid);
      ICategoryTypeIconService service1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
      if (service1 != null)
      {
        Icon icon = service1.GetIcon(4, MRP2Consts.objtypeIdProductionCopy);
        if (icon != null)
          service1.AddIcon(icon, SelectObjectFromProductionCopyWizardControl._rootCategoryNodeId);
      }
      IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false);
      if (service2 != null)
        SelectObjectFromProductionCopyWizardProvider.RegisterViewProvider(service2);
      return SelectObjectFromProductionCopyWizardControl._rootCategoryNodeId;
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

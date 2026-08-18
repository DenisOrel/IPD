// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.SelectObjectFromArticleWizardControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.UI.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

/// <summary>
/// Мастер выбора объекта (из состава МО ) для определенного изделия
/// </summary>
public class SelectObjectFromArticleWizardControl : DockWizardView
{
  /// <summary>Тип выбираемого объекта</summary>
  private int _objectTypeId;
  /// <summary>
  /// 
  /// </summary>
  private SelectObjectForContextPageControl _selectObjectPage;
  /// <summary>Guid категория для закладки</summary>
  private static readonly Guid _rootCategoryNodeGuid = new Guid("{0693DE08-97C3-4A78-9368-27B37768B449}");
  /// <summary>Идентификатор категория для закладки</summary>
  private static int _rootCategoryNodeId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация "прочих" пользовательских контролов</summary>
  private void InitializeCustomComponent()
  {
    SelectObjectNavListPageControl navListPageControl1 = new SelectObjectNavListPageControl()
    {
      ObjectTypeId = TechCardConsts.ObjectTypes.ArticleBaseID
    };
    navListPageControl1.Caption = navListPageControl1.Description = "Выберите изделие";
    navListPageControl1.LoadPageControlData += (LoadPageControlEventHandler) ((sender, args) =>
    {
      if (!(sender is SelectObjectNavListPageControl navListPageControl3))
        return;
      IDescriptor descriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(navListPageControl3.ObjectTypeId);
      navListPageControl3.TechNavigatorControl.RootDescriptor = descriptor;
      args.DataLoaded = true;
    });
    this.Pages.Add((IWizardPage) navListPageControl1);
    this._selectObjectPage = new SelectObjectForContextPageControl();
    this._selectObjectPage.SelectedItemsChanged += new EventHandler(this.SelectObjectPageOnSelectedItemsChanged);
    this._selectObjectPage.Services = (System.IServiceProvider) this._serviceContainer;
    this.Pages.Add((IWizardPage) this._selectObjectPage);
  }

  /// <summary>Конструктор</summary>
  public SelectObjectFromArticleWizardControl()
  {
    this.InitializeComponent();
    this.InitializeCustomComponent();
    this.Caption = LocalizationHolder.rm.GetString("TechCard.Client_549");
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

  /// <summary>Идентификатор категория для закладки</summary>
  public static int RootCategoryNodeId
  {
    get
    {
      if (SelectObjectFromArticleWizardControl._rootCategoryNodeId != 0)
        return SelectObjectFromArticleWizardControl._rootCategoryNodeId;
      SelectObjectFromArticleWizardControl._rootCategoryNodeId = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, true).Register(SelectObjectFromArticleWizardControl._rootCategoryNodeGuid);
      ICategoryTypeIconService service1 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
      if (service1 != null)
      {
        Icon icon = service1.GetIcon(4, TechCardConsts.ObjectTypes.ArticleBaseID);
        if (icon != null)
          service1.AddIcon(icon, SelectObjectFromArticleWizardControl._rootCategoryNodeId);
      }
      IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false);
      if (service2 != null)
        SelectObjectFromArticleWizardProvider.RegisterViewProvider(service2);
      return SelectObjectFromArticleWizardControl._rootCategoryNodeId;
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

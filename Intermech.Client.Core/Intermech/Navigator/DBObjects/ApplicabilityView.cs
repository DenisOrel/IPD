
// Type: Intermech.Navigator.DBObjects.ApplicabilityView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Nodes;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка для отображения применяемости</summary>
[ViewDescriptionProvider(typeof (ApplicabilityView.ApplicabilityViewDescriptionProvider))]
public class ApplicabilityView : ChildrenView
{
  private ComboBoxItem _applicabilityTypeComboBoxItem;
  private ApplicabilityView.ApplicabilityType _type;
  /// <summary>Индекс значка закладки</summary>
  private int _imageIndex = -1;
  private ApplicabilityView.ApplicabilityNode _applicabilityNode;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ApplicabilityView));
    this._applicabilityTypeComboBoxItem = new ComboBoxItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._applicabilityTypeComboBoxItem
    });
    this._toolBar.StretchItem = (ToolbarItemBase) this._applicabilityTypeComboBoxItem;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "ViewsManager");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this._filtersComboBoxItem.Visible = false;
    componentResourceManager.ApplyResources((object) this._applicabilityTypeComboBoxItem, "_applicabilityTypeComboBoxItem");
    this._applicabilityTypeComboBoxItem.DropDownStyle = ComboBoxStyle.DropDownList;
    this._applicabilityTypeComboBoxItem.MinimumControlWidth = 50;
    this._applicabilityTypeComboBoxItem.Padding.Bottom = 0;
    this._applicabilityTypeComboBoxItem.Padding.Left = 1;
    this._applicabilityTypeComboBoxItem.Padding.Right = 1;
    this._applicabilityTypeComboBoxItem.Padding.Top = 0;
    this._applicabilityTypeComboBoxItem.Stretch = true;
    this._applicabilityTypeComboBoxItem.SelectedValueChanged += new EventHandler(this.ApplicabilityTypeComboBoxItem_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ApplicabilityView);
    this.Tag = (object) "  ";
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public ApplicabilityView()
  {
    this.InitializeComponent();
    this.InitializeApplicabilityTypeComboBox();
    this._editingModeButtonItem.Visible = true;
  }

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_1339");

  /// <summary>
  /// Можно ли искать унаследованные настройки отображения "Навигатора" для закладки
  /// </summary>
  protected override bool UseInheritedNavViews
  {
    get => false;
    set => base.UseInheritedNavViews = false;
  }

  /// <summary>
  /// Название потока, в котором будут сохранены настройки
  /// Не будем путать настройки для состава и для применяемости
  /// </summary>
  public override string StateStreamPrefix => nameof (ApplicabilityView);

  /// <summary>Порядковый номер закладки</summary>
  public override int OrderID => 27;

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgEntersTo");
      return this._imageIndex;
    }
  }

  public ApplicabilityView.ApplicabilityType Type
  {
    get => this._type;
    set
    {
      if (this._type == value)
        return;
      this._type = value;
      this._applicabilityTypeComboBoxItem.SelectedValueChanged -= new EventHandler(this.ApplicabilityTypeComboBoxItem_SelectedValueChanged);
      try
      {
        this._applicabilityTypeComboBoxItem.ControlText = this._type.GetDescription<ApplicabilityView.ApplicabilityType>();
      }
      finally
      {
        this._applicabilityTypeComboBoxItem.SelectedValueChanged += new EventHandler(this.ApplicabilityTypeComboBoxItem_SelectedValueChanged);
      }
      this._applicabilityNode.Refresh();
      this.ReloadItems();
    }
  }

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    this._applicabilityNode = new ApplicabilityView.ApplicabilityNode(itemData.ObjectType, itemData.ObjectID, this);
    base.Initialize(items, provider);
    this.SetServices((INode) this._applicabilityNode);
  }

  public override void Activate(IView previousView)
  {
    base.Activate(previousView);
    this.ReloadItems();
  }

  protected override INode GetNode() => (INode) this._applicabilityNode;

  public override ContentType ViewContentType
  {
    get => ContentType.NonFolders;
    set
    {
    }
  }

  private void ApplicabilityTypeComboBoxItem_SelectedValueChanged(object sender, EventArgs e)
  {
    this.Type = this.GetApplicabilityType(this._applicabilityTypeComboBoxItem.ControlText);
  }

  private void InitializeApplicabilityTypeComboBox()
  {
    this._toolBar.Items.Remove((ToolbarItemBase) this._filtersComboBoxItem);
    this._applicabilityTypeComboBoxItem.Items.Clear();
    foreach (MemberInfo field in typeof (ApplicabilityView.ApplicabilityType).GetFields(BindingFlags.Static | BindingFlags.Public))
      this._applicabilityTypeComboBoxItem.Items.Add((object) (Attribute.GetCustomAttribute(field, typeof (DescriptionAttribute)) as DescriptionAttribute).Description);
    this._applicabilityTypeComboBoxItem.SelectedValueChanged -= new EventHandler(this.ApplicabilityTypeComboBoxItem_SelectedValueChanged);
    this._applicabilityTypeComboBoxItem.ControlText = this._type.GetDescription<ApplicabilityView.ApplicabilityType>();
    this._applicabilityTypeComboBoxItem.SelectedValueChanged += new EventHandler(this.ApplicabilityTypeComboBoxItem_SelectedValueChanged);
  }

  private ApplicabilityView.ApplicabilityType GetApplicabilityType(string description)
  {
    foreach (FieldInfo field in typeof (ApplicabilityView.ApplicabilityType).GetFields(BindingFlags.Static | BindingFlags.Public))
    {
      if ((Attribute.GetCustomAttribute((MemberInfo) field, typeof (DescriptionAttribute)) as DescriptionAttribute).Description == description)
        return (ApplicabilityView.ApplicabilityType) field.GetValue((object) null);
    }
    return ApplicabilityView.ApplicabilityType.Relations;
  }

  private void SetServices(INode node)
  {
    IContextAware contextAware = node as IContextAware;
    IContextAware parentNode = this._parentNode as IContextAware;
    if (contextAware == null)
      return;
    AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer((System.IServiceProvider) this._services);
    if (parentNode != null)
      serviceContainer.AdvancedProvider = parentNode.Services;
    contextAware.Services = (System.IServiceProvider) serviceContainer;
  }

  public enum ApplicabilityType
  {
    [Description("По связям")] Relations,
    [Description("По ссылкам")] Links,
    [Description("В классификаторах и ручных выборках")] Classifiers,
    [Description("Полная")] Full,
    [Description("Все версии по ссылкам")] AllVersionsByLinks,
  }

  private sealed class ApplicabilityNode : ObjectNode
  {
    private ApplicabilityView _applicabilityView;

    public ApplicabilityNode(
      int objectTypeID,
      long objectVersionID,
      ApplicabilityView applicabilityView)
      : base(objectTypeID, objectVersionID)
    {
      this._applicabilityView = applicabilityView != null ? applicabilityView : throw new ArgumentNullException(nameof (applicabilityView));
    }

    protected override List<PartSlot> CreateNonFolderSlots()
    {
      List<PartSlot> nonFolderSlots = new List<PartSlot>();
      if (this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.Full || this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.Relations)
      {
        ObjectApplicabilityByRelationsNode applicabilityByRelationsNode = new ObjectApplicabilityByRelationsNode(this._objID, this._objTypeID);
        applicabilityByRelationsNode.Services = this.Services;
        nonFolderSlots.AddRange(applicabilityByRelationsNode.FolderSlots.Select<PartSlot, PartSlot>((Func<PartSlot, PartSlot>) (o => new PartSlot(Guid.NewGuid(), o.Object))));
      }
      if (this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.Full || this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.Links)
      {
        ObjectApplicabilityByLinksNode applicabilityByLinksNode = new ObjectApplicabilityByLinksNode(this._objID, this._objTypeID);
        applicabilityByLinksNode.Services = this.Services;
        nonFolderSlots.AddRange(applicabilityByLinksNode.FolderSlots.Select<PartSlot, PartSlot>((Func<PartSlot, PartSlot>) (o => new PartSlot(Guid.NewGuid(), o.Object))));
      }
      if (this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.Full || this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.Classifiers)
      {
        ObjectApplicabilityByClassifiersNode byClassifiersNode = new ObjectApplicabilityByClassifiersNode(this._objID, this._objTypeID);
        byClassifiersNode.Services = this.Services;
        nonFolderSlots.AddRange(byClassifiersNode.FolderSlots.Select<PartSlot, PartSlot>((Func<PartSlot, PartSlot>) (o => new PartSlot(Guid.NewGuid(), o.Object))));
      }
      if (this._applicabilityView.Type == ApplicabilityView.ApplicabilityType.AllVersionsByLinks)
      {
        AllObjectVersionsApplicabilitiesByLinksNode applicabilitiesByLinksNode = new AllObjectVersionsApplicabilitiesByLinksNode(this._objID, this._objTypeID);
        nonFolderSlots.AddRange(applicabilitiesByLinksNode.FolderSlots.Select<PartSlot, PartSlot>((Func<PartSlot, PartSlot>) (o => new PartSlot(Guid.NewGuid(), o.Object))));
      }
      return nonFolderSlots;
    }
  }

  private sealed class ApplicabilityViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_1339"),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgEntersTo"),
        OrderID = 27
      };
    }
  }
}

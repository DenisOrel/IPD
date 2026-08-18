// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivitiesView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Workflow.Design;

public class ActivitiesView : ObjectsViewBase
{
  private ButtonItem ViewProcessButtonItem;
  private ButtonItem PropertiesButtonItem;
  private bool _getII;

  public ActivitiesView()
  {
    this.InitializeComponent();
    this.DisableFiltration = true;
    this.DisableCheckedOutColumn = true;
    this.SelectedItemsChanged += new EventHandler(this.ActivitiesView_SelectedItemsChanged);
    if (BaseHolder.NamedList == null)
      return;
    this.ViewProcessButtonItem.ImageIndex = BaseHolder.NamedList.ImageIndex("wfViewProcess");
    this.PropertiesButtonItem.ImageIndex = BaseHolder.NamedList.ImageIndex("imgProp");
  }

  public override void Activate(IView previousView)
  {
    this.buttonHeightSet.Index = 1000;
    base.Activate(previousView);
  }

  private void ActivitiesView_SelectedItemsChanged(object sender, EventArgs e)
  {
    bool flag = this.SelectedItems.Count > 0;
    this.ViewProcessButtonItem.Enabled = flag;
    this.PropertiesButtonItem.Enabled = flag;
  }

  public void AddActivitiesFilterButtons()
  {
    for (int index1 = 0; index1 < ActivityInfos.Items.Count; ++index1)
    {
      Intermech.Workflow.ActivityInfo activityInfo = ActivityInfos.Items[index1];
      ActivityButtonItem activityButtonItem = new ActivityButtonItem();
      int index2 = BaseHolder.IconService.IndexOf(4, activityInfo.Type);
      if (index2 > -1)
        activityButtonItem.Image = BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[index2]);
      activityButtonItem.ToolTipText = activityInfo.ObjectName;
      if (index1 == 0)
        activityButtonItem.BeginGroup = true;
      activityButtonItem.AutoToggle = AutoToggleType.Single;
      activityButtonItem.Checked = true;
      activityButtonItem.Type = activityInfo.Type;
      activityButtonItem.Click += new EventHandler(this.ActivitiesButtonFilter_Click);
      this._toolBar.Items.Add((ToolbarItemBase) activityButtonItem);
    }
    ButtonItem iiButton = ActivitiesView.CreateIIButton();
    iiButton.Click += new EventHandler(this.ActivitiesButtonFilter_Click);
    this._toolBar.Items.Add((ToolbarItemBase) iiButton);
    CheckUncheckContextMenu.Attach(this._toolBar, this.ActivitiesFilterChanged);
  }

  public void AddProcessesFilterButtons()
  {
    List<ActivityStatus> activityStatusList = new List<ActivityStatus>((IEnumerable<ActivityStatus>) new ActivityStatus[3]
    {
      ActivityStatus.Executed,
      ActivityStatus.Terminated,
      ActivityStatus.Completed
    });
    string[] strArray1 = new string[3]
    {
      "",
      "process_terminated",
      "process_completed"
    };
    string[] strArray2 = new string[3]
    {
      LocalizationHolder.rm.GetString("Workflow.Design_2"),
      LocalizationHolder.rm.GetString("Workflow.Design_3"),
      LocalizationHolder.rm.GetString("Workflow.Design_4")
    };
    for (int index1 = 0; index1 < activityStatusList.Count; ++index1)
    {
      ProcessFilterButtonItem filterButtonItem = new ProcessFilterButtonItem();
      if (strArray1[index1] == "")
      {
        int index2 = BaseHolder.IconService.IndexOf(4, wfConsts.ProcessesTypeID);
        if (index2 > -1)
          filterButtonItem.Image = BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[index2]);
      }
      else
      {
        int index3 = BaseHolder.NamedList.ImageIndex(strArray1[index1]);
        if (index3 > -1)
          filterButtonItem.Image = BaseHolder.NamedList.ImageList.Images[index3];
      }
      filterButtonItem.ToolTipText = strArray2[index1];
      if (index1 == 0)
        filterButtonItem.BeginGroup = true;
      filterButtonItem.AutoToggle = AutoToggleType.Single;
      filterButtonItem.Checked = true;
      filterButtonItem.Status = activityStatusList[index1];
      filterButtonItem.Click += new EventHandler(this.ProcessesFilterButton_Click);
      this._toolBar.Items.Add((ToolbarItemBase) filterButtonItem);
    }
    ButtonItem iiButton = ActivitiesView.CreateIIButton();
    iiButton.Click += new EventHandler(this.ProcessesFilterButton_Click);
    this._toolBar.Items.Add((ToolbarItemBase) iiButton);
  }

  private static ButtonItem CreateIIButton()
  {
    ButtonItem iiButton = new ButtonItem();
    iiButton.Tag = (object) "GetII";
    iiButton.ToolTipText = "Проверка входимости в извещение";
    int index = BaseHolder.IconService.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00348-306c-11d8-b4e9-00304f19f545")));
    if (index > -1)
      iiButton.Image = BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[index]);
    iiButton.BeginGroup = true;
    iiButton.AutoToggle = AutoToggleType.Single;
    iiButton.Checked = false;
    return iiButton;
  }

  public event EventHandler ActivitiesFilterChanged;

  private void ActivitiesButtonFilter_Click(object sender, EventArgs e)
  {
    EventHandler activitiesFilterChanged = this.ActivitiesFilterChanged;
    if (activitiesFilterChanged == null)
      return;
    activitiesFilterChanged((object) this, (EventArgs) null);
  }

  public event EventHandler ProcessesFilterChanged;

  private void ProcessesFilterButton_Click(object sender, EventArgs e)
  {
    EventHandler processesFilterChanged = this.ProcessesFilterChanged;
    if (processesFilterChanged == null)
      return;
    processesFilterChanged((object) this, (EventArgs) null);
  }

  public List<int> GetSelectedActivityTypes()
  {
    List<int> intList = new List<int>();
    bool flag = true;
    this._getII = false;
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._toolBar.Items)
    {
      if (toolbarItemBase is ActivityButtonItem activityButtonItem)
      {
        if (activityButtonItem.Checked)
          intList.Add(activityButtonItem.Type);
        else
          flag = false;
      }
      else if (toolbarItemBase is ButtonItem buttonItem && buttonItem.Tag != null && buttonItem.Tag.Equals((object) "GetII"))
        this._getII = buttonItem.Checked;
    }
    return flag ? (List<int>) null : intList;
  }

  /// <summary>
  /// Указывает на значение нужно ли искать в составе ИИ.
  /// Перед использованием обязательно должен быть вызван метод GetSelectedProcessStatuses или GetSelectedActivityTypes
  /// </summary>
  public bool GetII => this._getII;

  public List<int> GetSelectedProcessStatuses()
  {
    List<int> intList = new List<int>();
    bool flag = true;
    this._getII = false;
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._toolBar.Items)
    {
      if (toolbarItemBase is ProcessFilterButtonItem filterButtonItem)
      {
        if (filterButtonItem.Checked)
          intList.Add((int) filterButtonItem.Status);
        else
          flag = false;
      }
      else if (toolbarItemBase is ButtonItem buttonItem && buttonItem.Tag != null && buttonItem.Tag.Equals((object) "GetII"))
        this._getII = buttonItem.Checked;
    }
    return flag ? (List<int>) null : intList;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActivitiesView));
    this.ViewProcessButtonItem = new ButtonItem();
    this.PropertiesButtonItem = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.ViewProcessButtonItem,
      (ToolbarItemBase) this.PropertiesButtonItem
    });
    this._toggleManualSortingButtonItem.Visible = false;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
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
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "menuHeader");
    this.ViewProcessButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.ViewProcessButtonItem, "ViewProcessButtonItem");
    this.ViewProcessButtonItem.Click += new EventHandler(this.ViewProcessButtonItem_Click);
    componentResourceManager.ApplyResources((object) this.PropertiesButtonItem, "PropertiesButtonItem");
    this.PropertiesButtonItem.Click += new EventHandler(this.PropertiesButtonItem_Click);
    this.Name = nameof (ActivitiesView);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void ViewProcessButtonItem_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems.Count <= 0)
      return;
    long id = 0;
    IDBTypedObjectID itemData = this.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData.ObjectType == wfConsts.ProcessesTypeID)
    {
      id = itemData.ObjectID;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(itemData.ObjectID, wfConsts.AttrProcessID);
        if (objectAttributeById != null)
          id = objectAttributeById.AsInteger;
      }
    }
    if (id == 0L)
      return;
    wfFunx.ViewProcess(id);
  }

  private void PropertiesButtonItem_Click(object sender, EventArgs e)
  {
    if (this.SelectedItems.Count <= 0)
      return;
    wfFunx.ShowActivityProperties((this.SelectedItems.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value);
  }

  public override string StateStreamPrefix => nameof (ActivitiesView);
}

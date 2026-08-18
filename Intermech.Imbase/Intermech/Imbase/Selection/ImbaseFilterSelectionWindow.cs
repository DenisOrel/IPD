// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterSelectionWindow
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Client.Core.Thumbnail;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseFilterSelectionWindow : ImbaseFilterSelectionBaseWindow
{
  private ImbaseCatalogSelectMode _selectMode;
  private HybridDictionary _ctrlsSettings = new HybridDictionary(0, true);
  private List<long> _prevCheckedObjIDs;
  private ImbaseTableView _imbaseTableView;
  private ThumbnailView _thumbnailView;
  private CatalogsView _compositionView;
  private TableLinkPropertiesView _linkPropsView;
  private List<TreeNode> _checkedNodes = new List<TreeNode>();
  private ImbaseFilterSelectionWindow.SelectedTypeNode _selectedTypeNode;
  private bool _loading = true;
  private bool _trvFocused;
  public List<long> CheckedIDs_1726096 = new List<long>();
  private bool multiselectWithSelectfilter;
  private IContainer components;

  public int AttributeID { get; set; }

  public List<long> CheckedIDs
  {
    get
    {
      List<long> checkedIds = (List<long>) null;
      if (this._checkedNodes.Count > 0)
      {
        checkedIds = new List<long>(this._checkedNodes.Count);
        foreach (TreeNode checkedNode in this._checkedNodes)
        {
          if (checkedNode.Tag is NodeInfo tag && !checkedIds.Contains(tag.ObjectId))
            checkedIds.Add(tag.ObjectId);
        }
      }
      return checkedIds;
    }
  }

  public long RecordID { get; set; }

  public long SelectedID { get; private set; }

  public ImbaseFilterSelectionWindow(
    List<long> catalogIDs,
    long ownerObjID,
    long prevSelectedID,
    List<int> needObjTypes = null,
    ImbaseCatalogSelectMode mode = ImbaseCatalogSelectMode.imcmSelectFolder)
    : base(ownerObjID, (IEnumerable<long>) catalogIDs, (IEnumerable<int>) needObjTypes)
  {
    this.InitializeComponent();
    this._selectMode = mode;
    this._prevSelectedObjID = prevSelectedID;
    this.AttributeID = 0;
  }

  public ImbaseFilterSelectionWindow(
    List<long> catalogIDs,
    long ownerObjID,
    List<long> prevSelectedIDs,
    List<int> needObjTypes = null,
    ImbaseCatalogSelectMode mode = ImbaseCatalogSelectMode.imcmSelectFolder)
    : base(ownerObjID, (IEnumerable<long>) catalogIDs, (IEnumerable<int>) needObjTypes)
  {
    this.InitializeComponent();
    this._selectMode = mode;
    this._trv.CheckBoxes = true;
    this.multiselectWithSelectfilter = true;
    this._trv.BeforeCheck += new TreeViewCancelEventHandler(this._trv_BeforeCheck);
    this._prevCheckedObjIDs = prevSelectedIDs;
    this.AttributeID = 0;
  }

  private void _trv_BeforeCheck(object sender, TreeViewCancelEventArgs e)
  {
    e.Cancel = !this.AnalyzeEnableCheck_By_MultiselectWithSelectfilter(e.Node);
  }

  public ImbaseFilterSelectionWindow(
    List<long> catalogIDs,
    long ownerObjID,
    List<long> prevCheckedIDs)
    : base(ownerObjID, (IEnumerable<long>) catalogIDs, (IEnumerable<int>) null)
  {
    this.InitializeComponent();
    this._trv.CheckBoxes = true;
    this._prevCheckedObjIDs = prevCheckedIDs;
    this._btnApply.Enabled = false;
    this.AttributeID = 0;
  }

  private void On_trv_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (e.Node.Checked)
    {
      if (this._checkedNodes.Contains(e.Node))
        return;
      this._checkedNodes.Add(e.Node);
      this._btnApply.Enabled = true;
    }
    else
    {
      this._checkedNodes.Remove(e.Node);
      this._btnApply.Enabled = this._checkedNodes.Count > 0;
    }
  }

  private void On_trv_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._trv.SelectedNode?.Tag == null)
      return;
    ISelectedItems selectedItems = this.GetSelectedItems();
    bool flag = false;
    if (this._thumbnailView != null || this._compositionView != null)
    {
      if (this._selectedTypeNode != ImbaseFilterSelectionWindow.SelectedTypeNode.Folder)
      {
        this.SubscribeViews(ImbaseFilterSelectionWindow.SelectedTypeNode.Folder, false);
        this._thumbnailView = (ThumbnailView) null;
        this._compositionView = (CatalogsView) null;
        flag = true;
      }
    }
    else if (this._imbaseTableView != null && this._selectedTypeNode != ImbaseFilterSelectionWindow.SelectedTypeNode.Link)
    {
      this.SubscribeViews(ImbaseFilterSelectionWindow.SelectedTypeNode.Link, false);
      this._imbaseTableView = (ImbaseTableView) null;
      flag = true;
    }
    if (selectedItems == null)
      return;
    this._viewsMngr.UpdateViews(selectedItems, true);
    if (this._selectedTypeNode == ImbaseFilterSelectionWindow.SelectedTypeNode.Folder && (this._thumbnailView == null || this._compositionView == null))
    {
      for (int index = 0; index < this._viewsMngr.ViewPages.Count; ++index)
      {
        switch (this._viewsMngr.ViewPages[index].View)
        {
          case ThumbnailView thumbnailView:
            this._thumbnailView = thumbnailView;
            break;
          case CatalogsView catalogsView:
            this._compositionView = catalogsView;
            break;
        }
      }
      flag = true;
    }
    else if (this._selectedTypeNode == ImbaseFilterSelectionWindow.SelectedTypeNode.Link && this._imbaseTableView == null)
    {
      for (int index = 0; index < this._viewsMngr.ViewPages.Count; ++index)
      {
        if (this._viewsMngr.ViewPages[index].View is ImbaseTableView view)
        {
          this._imbaseTableView = view;
          break;
        }
      }
      flag = true;
    }
    if (this._loading)
      return;
    if (flag)
      this.SubscribeViews(this._selectedTypeNode, true);
    if (this._trv.CheckBoxes)
      return;
    this.AnalizeEnabledOKButton((IView) null);
  }

  private void On_trv_Enter(object sender, EventArgs e)
  {
    this._trvFocused = true;
    if (this._trv.CheckBoxes)
      return;
    this.AnalizeEnabledOKButton((IView) null);
  }

  private void OnImbaseTable_ItemDoubleClick(object sender, EventArgs e)
  {
    if (this._trv.CheckBoxes || !(sender is TableView tableView) || !this.AnalizeEnabledOKButton((IView) (tableView.Parent as ImbaseTableView)))
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void OnImbaseTable_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (this._trv.CheckBoxes)
      return;
    this.AnalizeEnabledOKButton((IView) (sender as ImbaseTableView));
  }

  private void OnView_Enter(object sender, EventArgs e)
  {
    this._trvFocused = false;
    if (this._trv.CheckBoxes)
      return;
    if (this._viewsMngr.ActiveViewPage.View is CatalogsView || this._viewsMngr.ActiveViewPage.View is ThumbnailView || this._viewsMngr.ActiveViewPage.View is ImbaseTableView)
      this.AnalizeEnabledOKButton(this._viewsMngr.ActiveViewPage.View);
    else
      this.AnalizeEnabledOKButton((IView) null);
  }

  private void OnView_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (this._trv.CheckBoxes)
      return;
    this.AnalizeEnabledOKButton(this._viewsMngr.ActiveViewPage.View);
  }

  protected override void InitializeData()
  {
    base.InitializeData();
    this._viewsMngr.SuppressedViews = new string[6]
    {
      "ApplicabilityView",
      "ObjectsVisibilityView",
      "ContextsSearchView",
      "ImbaseIndexesView",
      "ObjectFiles",
      "ObjectSecurity"
    };
  }

  protected override void LoadSettings()
  {
    FormStorage.LoadLayout((Control) this, (IDictionary) this._ctrlsSettings);
    int result1;
    if (int.TryParse(Convert.ToString(this._ctrlsSettings[(object) "Splitter"]), out result1))
      this._spltContainer.SplitterDistance = result1;
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    string name = this.GetType().ToString();
    IConfiguration configuration = service.Open(name);
    if (configuration != null && this.AttributeID != 0)
      configuration = configuration.Open($"AttrID_{Convert.ToString(this.AttributeID)}");
    if (configuration == null)
      return;
    ImbaseFilterSelectionBaseWindow.ImFilterMode imFilterMode = ImbaseFilterSelectionBaseWindow.ImFilterMode.None;
    try
    {
      imFilterMode = (ImbaseFilterSelectionBaseWindow.ImFilterMode) Convert.ToInt32(configuration.GetProperty("FilterMode"));
    }
    catch
    {
    }
    switch (imFilterMode)
    {
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder:
        string property = configuration.GetProperty("FolderFilterOwnerGuid");
        this.FolderFilterItemChecked = property == this._userFilterGuid ? this._tsmiFolderFilterUser : (property == this._roleFilterGuid ? this._tsmiFolderFilterRole : (property == this._areaFilterGuid ? this._tsmiFolderFilterArea : this._tsmiFolderFilterCommon));
        break;
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Object:
        long objFilterID;
        if (!long.TryParse(configuration.GetProperty(sc_7892.ssp_imbase_7893()), out objFilterID))
          objFilterID = 0L;
        List<ImbaseObjFilterInfo> objFilterList = this._objFilterList;
        if ((objFilterList != null ? objFilterList.FirstOrDefault<ImbaseObjFilterInfo>((System.Func<ImbaseObjFilterInfo, bool>) (x => x.ObjectID == objFilterID)) : (ImbaseObjFilterInfo) null) != null)
        {
          bool flag = true;
          for (int index = 2; index < 6 & flag; ++index)
          {
            if (this._tsBtnObjFilter.DropDownItems[index] is ToolStripMenuItem dropDownItem1 && dropDownItem1.DropDownItems.Count != 0)
            {
              foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) dropDownItem1.DropDownItems)
              {
                if (dropDownItem is ToolStripMenuItem toolStripMenuItem && ((ImbaseObjFilterInfo) toolStripMenuItem.Tag).ObjectID == objFilterID)
                {
                  this._objFilterID = objFilterID;
                  this.ObjectFilterItemChecked = toolStripMenuItem;
                  flag = false;
                  break;
                }
              }
            }
          }
          break;
        }
        break;
    }
    if (this._prevSelectedObjID != 0L)
      return;
    long result2 = -1;
    long result3;
    if (long.TryParse(configuration.GetProperty(sc_7892.ssp_imbase_7894()), out result3))
    {
      this._prevSelectedObjID = result3;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObjectInfo(result3).ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          if (!long.TryParse(configuration.GetProperty("selectedRecordID"), out result2))
            result2 = -1L;
        }
      }
    }
    this.RecordID = result2;
  }

  protected override void SaveSettings()
  {
    base.SaveSettings();
    this._ctrlsSettings[(object) "Splitter"] = (object) this._spltContainer.SplitterDistance;
    FormStorage.SaveLayout((Control) this, (IDictionary) this._ctrlsSettings);
    if (!(this._trv.SelectedNode?.Tag is NodeInfo tag) || tag.ObjectId == 0L || !(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    string name1 = this.GetType().ToString();
    IConfiguration configuration = service.Open(name1) ?? service.Create(name1);
    if (configuration == null)
      return;
    if (this.AttributeID != 0)
    {
      string name2 = $"AttrID_{Convert.ToString(this.AttributeID)}";
      configuration = (configuration.Configurations[name2] ?? configuration.Add(name2)) ?? configuration;
    }
    ImbaseFilterSelectionBaseWindow.ImFilterMode filterMode = this.FilterMode;
    switch (filterMode)
    {
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder:
        configuration.SetProperty("FolderFilterOwnerGuid", this.GetFilterOwnerGuid() ?? string.Empty);
        break;
      case ImbaseFilterSelectionBaseWindow.ImFilterMode.Object:
        configuration.SetProperty("ObjectFilterID", Convert.ToString(this._objFilterID));
        break;
    }
    configuration.SetProperty("FilterMode", Convert.ToString((int) filterMode));
    configuration.SetProperty("selectedObjectID", Convert.ToString(tag.ObjectId));
    long num = -1;
    if (tag.IsTableReference)
      num = this._viewsMngr.ActiveViewPage.View is ImbaseTableView view ? view.RecordId : -1L;
    configuration.SetProperty("selectedRecordID", Convert.ToString(num));
  }

  protected override bool FilterUpdate()
  {
    bool flag = base.FilterUpdate();
    if (flag)
    {
      for (int index = 0; index < this._viewsMngr.ViewPages.Count; ++index)
      {
        IView view = this._viewsMngr.ViewPages[index].View;
        switch (view)
        {
          case PropertiesView _:
          case Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.VisualizerView _:
            (view as Control).Enter += new EventHandler(this.OnView_Enter);
            break;
        }
      }
    }
    return flag;
  }

  protected override void SelectPreviousItemInTree()
  {
    SelectedRecords.Clear();
    this._trv.BeginUpdate();
    try
    {
      if (this._trv.CheckBoxes)
      {
        if (this._prevCheckedObjIDs == null)
          return;
        this._treeBuilder.SetCheckedNodes(this._prevCheckedObjIDs);
      }
      else
      {
        if (this._prevSelectedObjID == 0L)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (MetaDataHelper.IsObjectTypeChildOf(sessionKeeper.Session.GetObjectInfo(this._prevSelectedObjID).ObjectTypeID, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
          {
            this._treeBuilder.SetSelectedNode(this._prevSelectedObjID);
          }
          else
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this._prevSelectedObjID, false);
            IDBAttribute attributeById1 = dbObject?.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
            if (attributeById1 != null)
            {
              if (attributeById1.Value != null)
              {
                if (attributeById1.Value != DBNull.Value)
                {
                  this._treeBuilder.SetSelectedNode((long) attributeById1.Value);
                  if (this._viewsMngr.ActiveViewPage.View is ImbaseTableView view)
                  {
                    IDBAttribute attributeById2 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
                    if (attributeById2 != null)
                    {
                      long result;
                      if (long.TryParse(Convert.ToString(attributeById2.Value), out result))
                      {
                        if (result > -1L)
                          view.RecordId = result;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        if (this.RecordID <= -1L)
          return;
        SelectedRecords.Add(this._prevSelectedObjID, new long[1]
        {
          this.RecordID
        });
      }
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
    finally
    {
      this._trv.EndUpdate();
    }
  }

  protected override void SearchByName(object sender, EventArgs e)
  {
    if (this._trv.SelectedNode == null)
      return;
    FindByNameView.Show((object) this._trv.SelectedNode, true, (LocateNodeEventHandler) null);
  }

  protected override void OnFolderFilterSetup_Click(object sender, EventArgs e)
  {
    if (this._catalogIDs.Count <= 0 || this._ownerObjTypeID == -1)
      return;
    long catalogIdByObjectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, this._imbaseObjectId);
    if (catalogIdByObjectId != 0L)
    {
      int num1 = (int) ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, catalogIdByObjectId, this._catalogIDs[0]);
    }
    else
    {
      int num2 = (int) ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, this._ownerObjTypeID, this._catalogIDs[0]);
    }
    if (this.FilterMode != ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder)
      return;
    this.FilterUpdate();
  }

  protected override void OnObjFilterSetup_Click(object sender, EventArgs e)
  {
    if (ImbaseFilterSetupForm.ShowSetupDialog((IWin32Window) this, this._ownerObjTypeID, this._catalogIDs.Count > 0 ? this._catalogIDs[0] : 0L, false) != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.LoadObjectFiltersInfo(sessionKeeper.Session);
      this.FilterUpdate();
    }
  }

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    this._loading = false;
    this.SubscribeViews(this._selectedTypeNode, true);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    try
    {
      this.RecordID = -1L;
      if (this.DialogResult != DialogResult.Cancel && !this._trv.CheckBoxes)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IView view = this._viewsMngr.ActiveViewPage.View;
          if (!this._trvFocused)
          {
            switch (view)
            {
              case PropertiesView _:
              case Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.VisualizerView _:
                break;
              case ChildrenView _:
              case ThumbnailView _:
                ISelectedItemsHost selectedItemsHost = view as ISelectedItemsHost;
                if (selectedItemsHost.SelectedItems.Count == 0)
                  throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1144"));
                if (!(selectedItemsHost.SelectedItems.GetItemID(0) is NodeID itemId))
                  throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1144"));
                this.SelectedID = itemId.ObjectID;
                goto label_26;
              case ImbaseTableView imbaseTableView:
                if (this._trv.SelectedNode?.Tag == null)
                  throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1146"));
                if (this._trv.SelectedNode.Tag is NodeInfo tag1)
                {
                  if (this._selectMode == ImbaseCatalogSelectMode.imcmAllowSelectRow)
                  {
                    this.SelectedID = tag1.ObjectId;
                    this.RecordID = imbaseTableView.RecordId;
                    goto label_26;
                  }
                  if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
                  {
                    if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
                      throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1147"));
                    this.SelectedID = customService.CreateObject(sessionKeeper.Session.SessionGUID, this.GetCatalogID(this._trv.SelectedNode), tag1.ObjectId, imbaseTableView.RecordId, true, -1);
                    goto label_26;
                  }
                  goto label_26;
                }
                goto label_26;
              default:
                goto label_26;
            }
          }
          if (this._trv.SelectedNode?.Tag == null)
            throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1146"));
          if (this._trv.SelectedNode.Tag is NodeInfo tag2)
          {
            if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
            {
              if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
                throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1147"));
              this.SelectedID = customService.CreateObject(sessionKeeper.Session.SessionGUID, this.GetCatalogID(this._trv.SelectedNode), tag2.ObjectId, -1L, true, -1);
            }
            else
              this.SelectedID = tag2.ObjectId;
          }
label_26:
          if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
          {
            if (!ImbaseUsageHelper.CanUseImbaseObject(new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(this.SelectedID, -1, string.Empty), this.RecordID)))
            {
              e.Cancel = true;
              return;
            }
          }
        }
      }
      if (this.DialogResult != DialogResult.Cancel && this._trv.CheckBoxes && this.multiselectWithSelectfilter)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (this._checkedNodes.Count == 0)
            throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1146"));
          if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
            throw new Exception(LocalizationHolder.rm.GetString("Imbase.Client_1147"));
          IImbaseParamsService service = ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true);
          this.CheckedIDs_1726096.Clear();
          foreach (TreeNode checkedNode in this._checkedNodes)
          {
            if (checkedNode.Tag is NodeInfo tag)
            {
              long objectId = this._selectMode != ImbaseCatalogSelectMode.imcmCreateObject ? tag.ObjectId : customService.CreateObject(sessionKeeper.Session.SessionGUID, this.GetCatalogID(checkedNode), tag.ObjectId, -1L, true, -1);
              if (service.CommonParams.CheckApplicabilityBeforeCreateComposition && !ImbaseUsageHelper.CanUseImbaseObject(new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectId, -1, string.Empty), this.RecordID)))
              {
                e.Cancel = true;
                return;
              }
              this.CheckedIDs_1726096.Add(objectId);
            }
          }
        }
      }
      base.OnClosing(e);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      this._btnApply.Enabled = false;
      e.Cancel = true;
    }
  }

  private bool AnalizeEnabledOKButton(IView view)
  {
    bool flag = false;
    switch (view)
    {
      case ChildrenView _:
      case ThumbnailView _:
        ISelectedItemsHost selectedItemsHost = (ISelectedItemsHost) view;
        if (selectedItemsHost.SelectedItems.Count > 0 && selectedItemsHost.SelectedItems.GetItemID(0) is NodeID itemId)
        {
          if (this._selectMode == ImbaseCatalogSelectMode.imcmSelectFolder || this._selectMode == ImbaseCatalogSelectMode.imcmAllowSelectRow)
          {
            flag = true;
            break;
          }
          if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
          {
            flag = itemId.TypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID;
            break;
          }
          break;
        }
        break;
      case ImbaseTableView imbaseTableView:
        if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject || this._selectMode == ImbaseCatalogSelectMode.imcmAllowSelectRow)
        {
          flag = !imbaseTableView.DisabledRecord;
          break;
        }
        break;
      default:
        if (this._trv.SelectedNode?.Tag is NodeInfo tag && tag.TypeId != Intermech.Imbase.Consts.ImbaseCatalogTypeID)
        {
          if (this._selectMode == ImbaseCatalogSelectMode.imcmSelectFolder || this._selectMode == ImbaseCatalogSelectMode.imcmAllowSelectRow)
          {
            flag = true;
            break;
          }
          if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
          {
            flag = tag.TypeId != Intermech.Imbase.Consts.ImbaseTableRefTypeID && this._trv.SelectedNode.Nodes.Count == 0;
            break;
          }
          break;
        }
        break;
    }
    return this._btnApply.Enabled = flag;
  }

  private bool AnalyzeEnableCheck_By_MultiselectWithSelectfilter(TreeNode tn)
  {
    bool flag = false;
    if (tn.Tag is NodeInfo tag && tag.TypeId != Intermech.Imbase.Consts.ImbaseCatalogTypeID)
    {
      if (this._selectMode == ImbaseCatalogSelectMode.imcmSelectFolder || this._selectMode == ImbaseCatalogSelectMode.imcmAllowSelectRow)
        flag = true;
      else if (this._selectMode == ImbaseCatalogSelectMode.imcmCreateObject)
        flag = tag.TypeId != Intermech.Imbase.Consts.ImbaseTableRefTypeID && tn.Nodes.Count == 0;
    }
    return flag;
  }

  private void SubscribeViews(
    ImbaseFilterSelectionWindow.SelectedTypeNode nodeType,
    bool subscribe)
  {
    switch (nodeType)
    {
      case ImbaseFilterSelectionWindow.SelectedTypeNode.Folder:
        if (this._thumbnailView != null)
        {
          if (subscribe)
          {
            this._thumbnailView.SelectedItemsChanged += new EventHandler(this.OnView_SelectedItemsChanged);
            this._thumbnailView.Enter += new EventHandler(this.OnView_Enter);
          }
          else
          {
            this._thumbnailView.SelectedItemsChanged -= new EventHandler(this.OnView_SelectedItemsChanged);
            this._thumbnailView.Enter -= new EventHandler(this.OnView_Enter);
          }
        }
        if (this._compositionView == null)
          break;
        if (subscribe)
        {
          this._compositionView.SelectedItemsChanged += new EventHandler(this.OnView_SelectedItemsChanged);
          this._compositionView.Enter += new EventHandler(this.OnView_Enter);
          break;
        }
        this._compositionView.SelectedItemsChanged -= new EventHandler(this.OnView_SelectedItemsChanged);
        this._compositionView.Enter -= new EventHandler(this.OnView_Enter);
        break;
      case ImbaseFilterSelectionWindow.SelectedTypeNode.Link:
        if (this._imbaseTableView != null)
        {
          if (subscribe)
          {
            this._imbaseTableView.TblView.ItemDoubleClick += new EventHandler(this.OnImbaseTable_ItemDoubleClick);
            this._imbaseTableView.SelectedItemsChanged += new EventHandler(this.OnImbaseTable_SelectedItemsChanged);
            this._imbaseTableView.Enter += new EventHandler(this.OnView_Enter);
          }
          else
          {
            this._imbaseTableView.TblView.ItemDoubleClick -= new EventHandler(this.OnImbaseTable_ItemDoubleClick);
            this._imbaseTableView.SelectedItemsChanged -= new EventHandler(this.OnImbaseTable_SelectedItemsChanged);
            this._imbaseTableView.Enter -= new EventHandler(this.OnView_Enter);
          }
        }
        if (this._linkPropsView == null)
          break;
        if (subscribe)
        {
          this._linkPropsView.Enter += new EventHandler(this.OnView_Enter);
          break;
        }
        this._linkPropsView.Enter -= new EventHandler(this.OnView_Enter);
        break;
    }
  }

  private long GetCatalogID(TreeNode imTreeNode)
  {
    long catalogId = 0;
    if (imTreeNode != null && this._catalogIDs.Count > 0)
    {
      if (this._catalogIDs.Count == 1)
      {
        catalogId = this._catalogIDs[0];
      }
      else
      {
        TreeNode treeNode = imTreeNode;
        while (treeNode.Parent != null)
          treeNode = treeNode.Parent;
        if (treeNode.Tag is NodeInfo tag)
        {
          if (tag.Path.Length <= 2)
          {
            catalogId = tag.ObjectId;
          }
          else
          {
            DataTable dataTable = (DataTable) null;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              if (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
                dataTable = customService.GetFoldersForObjects(sessionKeeper.Session.SessionGUID, this._catalogIDs.ToArray<long>(), this._catalogIDs.ToArray<long>());
            }
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
              int columnIndex = dataTable.Columns.IndexOf("F_PATH");
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                string str = Convert.ToString(row[columnIndex]);
                if (!string.IsNullOrEmpty(str) && tag.Path.IndexOf(str) == 0)
                {
                  catalogId = Convert.ToInt64(row["F_OBJECT_ID"]);
                  break;
                }
              }
            }
          }
        }
      }
    }
    return catalogId;
  }

  private ISelectedItems GetSelectedItems()
  {
    ISelectedItems selectedItems = (ISelectedItems) null;
    if (this._trv.SelectedNode.Tag is NodeInfo tag)
    {
      this._selectedTypeNode = tag.TypeId == Intermech.Imbase.Consts.ImbaseCatalogTypeID || tag.TypeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? ImbaseFilterSelectionWindow.SelectedTypeNode.Folder : (tag.TypeId != Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID ? (tag.TypeId != Intermech.Imbase.Consts.ImbaseTableRefTypeID ? ImbaseFilterSelectionWindow.SelectedTypeNode.Empty : ImbaseFilterSelectionWindow.SelectedTypeNode.Link) : ImbaseFilterSelectionWindow.SelectedTypeNode.Record);
      IDescriptor descriptor = this._descrs.ContainsKey(tag.ObjectId) ? this._descrs[tag.ObjectId] : (IDescriptor) new ImbaseFilterDescriptor(tag.ObjectId);
      this._descrs[tag.ObjectId] = descriptor;
      this.SetFilterForNode(tag.ObjectId, descriptor);
      NodeIDPath handlerPath = new NodeIDPath(descriptor);
      INode handler = (INode) new EtherealNode(handlerPath.RootDescriptor);
      INodeQuery query = handler.GetQuery(ContentType.Folders);
      query.Execute((object) null, 1);
      NodeIDCollection nodeIDs = new NodeIDCollection()
      {
        query.GetRecordNodeID(0)
      };
      selectedItems = (ISelectedItems) new NodeItems(handlerPath, handler, nodeIDs, (System.IServiceProvider) this._services);
    }
    return selectedItems;
  }

  private void SetFilterForNode(long selectedNodeID, IDescriptor descr)
  {
    if (descr == null)
      return;
    if (descr is ImbaseFilterDescriptor filterDescriptor)
      filterDescriptor.SetFilter((DataTable) null);
    if (this._dtFilter == null || this._dtFilter.Rows.Count <= 0)
      return;
    DataRow dataRow = this._dtFilter.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_OBJECT_ID"]) == selectedNodeID));
    if (dataRow == null)
      return;
    string str = Convert.ToString(dataRow["F_PATH"]);
    if (string.IsNullOrEmpty(str) || this._dtFilter.Select($"{$"{SQLStringHelper.QuoteLikeString($"F_PATH LIKE '{str}")}{"%"}'"} AND F_PATH <> '{str}'").Length == 0)
      return;
    ((ImbaseFilterDescriptor) descr).SetFilter(this._dtFilter);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseFilterSelectionWindow));
    ((ISupportInitialize) this._pbObject).BeginInit();
    this._spltContainer.BeginInit();
    this._spltContainer.Panel1.SuspendLayout();
    this._spltContainer.Panel2.SuspendLayout();
    this._spltContainer.SuspendLayout();
    this._pnlTop.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._viewsMngr, "_viewsMngr");
    this._trv.LineColor = Color.Black;
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.AfterCheck += new TreeViewEventHandler(this.On_trv_AfterCheck);
    this._trv.AfterSelect += new TreeViewEventHandler(this.On_trv_AfterSelect);
    this._trv.Enter += new EventHandler(this.On_trv_Enter);
    componentResourceManager.ApplyResources((object) this._spltContainer, "_spltContainer");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ImbaseFilterSelectionWindow);
    ((ISupportInitialize) this._pbObject).EndInit();
    this._spltContainer.Panel1.ResumeLayout(false);
    this._spltContainer.Panel1.PerformLayout();
    this._spltContainer.Panel2.ResumeLayout(false);
    this._spltContainer.EndInit();
    this._spltContainer.ResumeLayout(false);
    this._pnlTop.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private enum SelectedTypeNode
  {
    Empty,
    Folder,
    Record,
    Link,
  }
}

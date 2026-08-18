// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOEditorForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Archives;
using Intermech.Archives.Copies;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.Sets;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOEditorForm : ECOAncestorForm, IFiltrationClass
{
  public static readonly Guid ECOWindowGuid = new Guid("{BEA01358-7ED6-4ed3-9D1D-B1CC53570D24}");
  private TableElement SelChange;
  private DocumentTreeNode SelElem;
  private ContainerElement ContElem;
  private int revObjType = -1;
  private bool textChanged;
  private bool lockChange;
  private bool lockNumChange;
  public Hashtable addedLinks = new Hashtable();
  public Hashtable deletedLinks = new Hashtable();
  public Dictionary<long, PendingLink> changedLinks = new Dictionary<long, PendingLink>();
  private ImageList IL;
  private IContainer components;
  private readonly HashSet<long> NotDeleted = new HashSet<long>()
  {
    -1L
  };
  private List<PageElementUI> items = new List<PageElementUI>();
  private MenuButtonItem includeMenu;
  private MenuButtonItem removeDocs;
  private MenuButtonItem splitChange;
  private MenuButtonItem showChange;
  private MenuButtonItem hideChange;
  private MenuButtonItem includeElem;
  private MenuButtonItem editElem;
  private MenuButtonItem removeElem;
  private MenuButtonItem usabElem;
  private MenuButtonItem sendToElem;
  private MenuButtonItem selPictFromFile;
  private MenuButtonItem selPictFromBase;
  private MenuButtonItem selPictFromClip;
  private MenuButtonItem createOLEPict;
  private MenuButtonItem originalSize;
  private MenuButtonItem insertTemplate;
  private MenuButtonItem insertImBase;
  private MenuButtonItem changeGoal;
  private MenuButtonItem cutElem;
  private MenuButtonItem copyElem;
  private MenuButtonItem pasteElem;
  private MenuButtonItem refreshElem;
  private MenuButtonItem copyTable;
  private MenuButtonItem sortChange;
  private MenuButtonItem fromNewItem;
  private MenuButtonItem alwaysTableItem;
  private MenuButtonItem copyAllElems;
  private MenuButtonItem pasteAllElems;
  private MenuButtonItem moveElemUp;
  private MenuButtonItem moveElemDown;
  private MenuButtonItem launchScrShooter;
  private TableElement elWorkspace;
  private TableElement _elCurChange;
  private int indexCurChange = -1;
  private TableElement elCurElem;
  private ContainerElement elPicture;
  private int indexCurElem = -1;
  private bool rightPart;
  private TextData textFld;
  private List<int> documentTypes;
  private EcoTreeViewDlg ecoTreeViewDlg;
  private DockControlLayoutSettings ecoTreeViewSettings = new DockControlLayoutSettings();

  protected override void Init()
  {
    base.Init();
    this.Guid = ECOEditorForm.ECOWindowGuid;
    IConfigurationManager configurationManager = DocumentEditorPlugin.Instance.ConfigurationManager;
    bool flag = true;
    if (configurationManager != null)
    {
      IConfiguration configuration = configurationManager.Open(this.GetConfigName());
      if (configuration != null && !string.IsNullOrWhiteSpace(configuration.GetProperty("Docking")))
        flag = false;
    }
    if (!flag)
      return;
    this.ShowECOTreeView(true);
  }

  protected override void InitBarManager()
  {
    bool showDebugInfo = ImDocumentEditorConfig.Instance.ShowDebugInfo;
    this.SetBaseEditCommandsEnabled(showDebugInfo, showDebugInfo);
    base.InitBarManager();
    this.ecoToolBar = (this.MenuHelper as ECOMenuHelper).CreateECOToolBar(DocumentEditorPlugin.imageList, ECOPlugin.plugin.CommandManager);
    this.barManager.AddToolbar(this.ecoToolBar, DockStyle.Top);
    this.ecoToolBar.DockLine = 0;
    this.ecoToolBar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
    this.ecoToolBar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
    this.ecoToolBar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
    IConfigurationManager configurationManager = ECOPlugin.plugin.ConfigurationManager;
    if (configurationManager == null)
      return;
    IConfiguration configuration = configurationManager.Open(this.GetConfigName());
    if (configuration == null)
      return;
    string property = configuration.GetProperty(this.GetToolbarConfigName());
    switch (property)
    {
      case null:
        break;
      case "":
        break;
      default:
        this.barManager.SetLayout(property);
        break;
    }
  }

  private void toolBar_HiddenChanged(object sender, EventArgs e)
  {
    if (this.barManagerInitializing)
      return;
    try
    {
      IConfigurationManager configurationManager = ECOPlugin.plugin.ConfigurationManager;
      if (configurationManager == null)
        return;
      (configurationManager.Open(this.GetConfigName()) ?? configurationManager.Create(this.GetConfigName()))?.SetProperty(this.GetToolbarConfigName(), this.barManager.GetLayout(true));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected override string GetConfigName() => "ECOEditor";

  protected override string GetToolbarConfigName() => "ECO.Toolbar";

  protected override void ControlSelChanged(object sender, SelectionChanged_EventArgs e)
  {
    DocumentTreeNode tableOrContainer = this.FindTableOrContainer();
    if (tableOrContainer != null && tableOrContainer is ContainerElement)
    {
      ContainerElement ce = tableOrContainer as ContainerElement;
      this.ShowScale(ce, ce.Size.Width, ce.Size.Height);
      this.ContElem = ce;
    }
    else
      this.plugin.scalePanel.Text = "";
    this.SetTableEditCommandsEnabled(tableOrContainer != null && tableOrContainer is TableElement && tableOrContainer.Template == null, true);
    this.GetSelecteds();
  }

  protected void GetSelecteds()
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    this.SelChange = (TableElement) null;
    this.SelElem = (DocumentTreeNode) (this.ContElem = (ContainerElement) null);
    DocumentTreeNode parent;
    for (parent = selectedNodes == null || selectedNodes.Length == 0 ? (DocumentTreeNode) null : selectedNodes[0]; parent != null && parent.TemplateId != Intermech.ECO.Client.ECO.fldChange; parent = parent.Parent)
    {
      if (parent is ContainerElement)
        this.ContElem = parent as ContainerElement;
      if (parent.Template != null && parent.Template.Parent != null)
      {
        if (parent.Template.Parent.Id == Intermech.ECO.Client.ECO.fldChange)
          this.SelElem = parent;
      }
      else
        break;
    }
    if (parent == null || parent.Template == null || !(parent.TemplateId == Intermech.ECO.Client.ECO.fldChange))
      return;
    this.SelChange = parent as TableElement;
  }

  internal DocumentTreeNode FindCurChange()
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    DocumentTreeNode parent = selectedNodes == null || selectedNodes.Length == 0 ? (DocumentTreeNode) null : selectedNodes[0];
    while (parent != null && parent.TemplateId != Intermech.ECO.Client.ECO.fldChange)
      parent = parent.Parent;
    return parent;
  }

  internal DocumentTreeNode FindTableOrContainer()
  {
    DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
    DocumentTreeNode parent = selectedNodes == null || selectedNodes.Length == 0 ? (DocumentTreeNode) null : selectedNodes[0];
    while (true)
    {
      switch (parent)
      {
        case null:
          goto label_5;
        case ContainerElement _:
          goto label_2;
        case TableElement _:
          if (parent.Template != null)
            break;
          goto label_2;
      }
      parent = parent.Parent;
    }
label_2:
    return parent;
label_5:
    return (DocumentTreeNode) null;
  }

  internal void UpdateNavMenuItems(DocumentTreeNode change)
  {
    if (this.plugin == null)
      return;
    if (change != null)
    {
      List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = idList.Count - 1; index >= 0; --index)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(idList[index], false);
          if (objectActualCopy == null)
            idList.RemoveAt(index);
          else
            idList[index] = objectActualCopy.ObjectID;
        }
      }
      if (idList == null || idList.Count == 0)
        this.plugin.NavigatorMenuItems = (ISelectedItems) null;
      else
        this.plugin.NavigatorMenuItems = ObjectExtensions.GetItems(idList.ToArray());
    }
    else
      this.plugin.NavigatorMenuItems = (ISelectedItems) null;
    this.plugin.UpdateISimpleSelectedItemsService();
  }

  public ECOEditorForm(IImDocumentManager documentManager, ImDocument document, bool readOnly)
    : base(documentManager, document, readOnly)
  {
  }

  public ECOEditorForm(
    IImDocumentManager documentManager,
    long documentID,
    int fileIndex,
    bool readOnly)
    : base(documentManager, false, readOnly)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject documentObject = sessionKeeper.Session.GetObject(documentID);
      if (!MetaDataHelper.IsObjectTypeChildOf(documentObject.ObjectType, DocIDCache.ObjType_ECO))
        throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_24"));
      this.SetDocumentParams(documentObject);
      this.AssignDocumentControl(new DocumentControl(DocumentEditorPlugin.LoadDocumentFromDBObject(DocumentEditorPlugin.TryCheckOutDocument(documentObject, ref readOnly), fileIndex, false, true, false)));
    }
  }

  public override bool Execute(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      switch (commandState.CommandName)
      {
        case "Copy":
          DocumentTreeNode activeElement1 = this.DocumentControl.ActiveElement;
          if (activeElement1 == null)
            return true;
          string text1 = "";
          if (activeElement1 is TextBoxElement)
          {
            TextBoxElement textBoxElement = (TextBoxElement) activeElement1;
            if (textBoxElement.InPlaceEditorControl != null)
            {
              ImRtfEditor placeEditorControl = (ImRtfEditor) textBoxElement.InPlaceEditorControl;
              if (placeEditorControl != null && placeEditorControl.HilightType != 0)
                placeEditorControl.TerCommand(629);
            }
            else
              text1 = this.textFld.Text;
            if (text1 != "")
            {
              try
              {
                Clipboard.SetText(text1, TextDataFormat.UnicodeText);
              }
              catch (ExternalException ex)
              {
              }
            }
          }
          else if (activeElement1 is ContainerElement && !(activeElement1 as ContainerElement).ReadOnly)
          {
            ContainerElement containerElement = (ContainerElement) activeElement1;
            if (containerElement.Image != null)
              Clipboard.SetImage(containerElement.Image);
          }
          return true;
        case "Cut":
          DocumentTreeNode activeElement2 = this.DocumentControl.ActiveElement;
          if (activeElement2 == null)
            return true;
          string text2 = "";
          if (activeElement2 is TextBoxElement && !(activeElement2 as TextBoxElement).ReadOnly)
          {
            TextBoxElement textBoxElement = (TextBoxElement) activeElement2;
            if (textBoxElement.InPlaceEditorControl != null)
            {
              ImRtfEditor placeEditorControl = (ImRtfEditor) textBoxElement.InPlaceEditorControl;
              if (placeEditorControl != null && placeEditorControl.HilightType != 0)
                placeEditorControl.TerCommand(628);
            }
            else
              text2 = this.textFld.Text;
            if (text2 != "")
            {
              try
              {
                Clipboard.SetText(text2, TextDataFormat.UnicodeText);
              }
              catch (ExternalException ex)
              {
              }
            }
            if (textBoxElement.InPlaceEditorControl == null)
              this.textFld.AssignText("", false, true, true);
          }
          else if (activeElement2 is ContainerElement && !(activeElement2 as ContainerElement).ReadOnly)
          {
            ContainerElement containerElement = (ContainerElement) activeElement2;
            if (containerElement.Image != null)
            {
              Clipboard.SetImage(containerElement.Image);
              containerElement.Image = (Image) null;
              containerElement.InvalidateUI(true);
            }
          }
          return true;
        case "Delete":
          return true;
        case "ECO.AddElemAfter":
          return true;
        case "ECO.AddElemBefore":
          return true;
        case "ECO.AttachIzdel":
          this.IncludeProductsForDocuments();
          return true;
        case "ECO.AttachToECO":
          this.elCurChange = (TableElement) null;
          this.ClearSelection();
          this.AttachAlternative();
          return true;
        case "ECO.AttachToECO_ExternalDoc":
          this.ClearSelection();
          this.AttachToECO_ExternalDoc();
          return true;
        case "ECO.CalculateWhereUsedColumn":
          int num1 = (int) MessageBox.Show("ECO.CalculateWhereUsedColumn", LocalizationHolder.rm.GetString("ECO.Client_26"), MessageBoxButtons.OK);
          return true;
        case "ECO.Card":
          ECOPlugin.FindPlugin().InvokeCommandForObject(this.ECO.EcoObjectID, "ParametersCard");
          this.CommandManager.ActiveTarget = (ICommandTarget) this;
          return true;
        case "ECO.ChangeGoal":
          this.ChangeGoal(this.SelChange);
          return true;
        case "ECO.ChangeReason":
          this.SelReason((DocumentTreeNode) null);
          return true;
        case "ECO.CopyAllElems":
          this.CopyAllElems(this.SelChange);
          return true;
        case "ECO.CopyTable":
          this.CopyTable(this.SelElem);
          return true;
        case "ECO.CreateOLE":
          this.CreateOLEObj(this.ContElem);
          return true;
        case "ECO.DeleteElem":
          this.DeleteElem(this.SelChange, this.SelElem);
          return true;
        case "ECO.DeleteList":
          Page activePage = this.Document.DocumentControl.ActivePage;
          if (activePage != null)
          {
            this.UndoManager.BeginCreateMultyUndo("Удаление листа");
            try
            {
              int num2 = activePage.IsFirstPage ? 1 : 0;
              Page page = !activePage.IsLastPage ? (Page) ImDocumentData.GetNextPage(activePage.Parent, activePage.Index, false) : (Page) ImDocumentData.GetPrevPage(activePage.Parent, activePage.Index, false);
              activePage.RemovePageFromDataFlow(true);
              this.DocumentControl.SetActivePage(page);
              if (num2 != 0)
              {
                if (page.FindFirstMainTable() is TableElement firstMainTable)
                  this.eco.ecoMainTable = firstMainTable;
              }
            }
            finally
            {
              this.UndoManager.EndCreateMultyUndo();
            }
          }
          return true;
        case "ECO.DetachFromECO":
          this.elCurChange = this.SelChange;
          this.cmdRemoveDocs((object) null, (EventArgs) null);
          this.SelChange = (TableElement) null;
          this.CommandManager.QueryStatus();
          return true;
        case "ECO.ImgFromClip":
          this.ImageFromClip(this.ContElem);
          return true;
        case "ECO.ImgFromFile":
          this.ImageFromFile(this.ContElem);
          return true;
        case "ECO.ImgFromObj":
          this.ImageFromObj(this.ContElem);
          return true;
        case "ECO.InsertList":
          int index = new SelTemplateList().Execute(this.Document.Template);
          if (index < 0)
            return true;
          this.UndoManager.BeginCreateMultyUndo("Вставка листа");
          try
          {
            DocumentTreeNode node = this.Document.Template.Nodes[index];
            if (node != null)
              this.DocumentControl.InsertNewPageInDataFlowAfterCurrent(node.Id);
          }
          finally
          {
            this.UndoManager.EndCreateMultyUndo();
          }
          return true;
        case "ECO.LaunchShooter":
          this.LaunchScreenShooter();
          return true;
        case "ECO.MoveElemDown":
          this.MoveElemDown(this.SelChange, this.SelElem);
          return true;
        case "ECO.MoveElemUp":
          this.MoveElemUp(this.SelChange, this.SelElem);
          return true;
        case "ECO.PasteElems":
          this.PasteAllElems(this.SelChange);
          return true;
        case "ECO.PasteObjects":
          this.elCurChange = (TableElement) null;
          this.PasteFromClipboardCommand();
          return true;
        case "ECO.ProcChanges":
          this.SortAndMergeCommand();
          return true;
        case "ECO.ReplaceTemplate":
          this.DoReplaceTemplate();
          return true;
        case "ECO.SetPLForAll":
          this.SetPLForAll();
          return true;
        case "ECO.SetSeriesDates":
          this.SelectSeriesDates((DocumentTreeNode) null);
          return true;
        case "ECO.SortByDes":
          this.SortByDes(this.SelChange);
          return true;
        case "ECO.SpecSymbol":
          this.textFld = this.DocumentControl.ActiveElement as TextData;
          if (this.textFld != null && this.textFld.InPlaceEditorActive)
            this.cmdInsertTemplate((object) this.textFld, (EventArgs) null);
          return true;
        case "ECO.Tree":
          this.ShowECOTreeView(true);
          return true;
        case "Paste":
          DocumentTreeNode activeElement3 = this.DocumentControl.ActiveElement;
          switch (activeElement3)
          {
            case null:
              return true;
            case TextBoxElement _ when !(activeElement3 as TextBoxElement).ReadOnly:
              TextBoxElement textBoxElement1 = (TextBoxElement) activeElement3;
              if (textBoxElement1.InPlaceEditorControl != null)
              {
                ((ImRtfEditor) textBoxElement1.InPlaceEditorControl).TerCommand(630);
                break;
              }
              break;
            case ContainerElement _ when !(activeElement3 as ContainerElement).ReadOnly:
              ContainerElement containerElement1 = (ContainerElement) activeElement3;
              containerElement1.PasteFromClipboard(this.Handle);
              containerElement1.CheckOriginalSizeAndAskUser();
              containerElement1.InvalidateUI(true);
              break;
          }
          return true;
        case "Save":
          if (this.Document != null)
            this.SaveRevision();
          return true;
        default:
          if (base.Execute(commandState))
            return true;
          break;
      }
    }
    catch
    {
      throw;
    }
    return false;
  }

  public override bool QueryStatus(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      switch (commandState.CommandName)
      {
        case "AddRowFromTemplateAbove":
        case "AddRowFromTemplateBelow":
        case "AddTableSection":
        case "ConvertToContainer":
        case "ConvertToHeader":
        case "ConvertToLabel":
        case "ConvertToTextBox":
        case "Format.BgColor":
        case "Format.Borders":
        case "Format.SelectionColor":
        case "Format.SetupBordersAndBackground":
        case "Format.SetupParagraph":
        case "Format.SetupTextDirection":
        case "Format.TextColor":
        case "UpdateTable":
          commandState.Enabled = false;
          return true;
        case "Copy":
        case "Cut":
          DocumentTreeNode activeElement = this.DocumentControl != null ? this.DocumentControl.ActiveElement : (DocumentTreeNode) null;
          if (!this.ReadOnly || activeElement != null)
          {
            switch (activeElement)
            {
              case TextBoxElement _:
                TextBoxElement textBoxElement = (TextBoxElement) activeElement;
                if (textBoxElement.InPlaceEditorControl != null)
                {
                  string textSel = ((ImRtfEditor) textBoxElement.InPlaceEditorControl).TerGetTextSel();
                  if (textSel != null && textSel != "")
                  {
                    commandState.Enabled = true;
                    return true;
                  }
                  break;
                }
                break;
              case ContainerElement _:
                commandState.Enabled = ((ContainerData) activeElement).Image != null;
                return true;
            }
          }
          commandState.Enabled = false;
          return true;
        case "Delete":
          commandState.Enabled = false;
          return true;
        case "ECO":
          commandState.Visible = true;
          commandState.Enabled = true;
          return true;
        case "ECO.AddElemAfter":
          commandState.Enabled = !this.ReadOnly && this.SelElem != null;
          return true;
        case "ECO.AddElemBefore":
          commandState.Enabled = !this.ReadOnly && this.SelElem != null;
          return true;
        case "ECO.AttachIzdel":
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.AttachToECO":
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.AttachToECO_ExternalDoc":
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.CalculateWhereUsedColumn":
          commandState.Visible = true;
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.ChangeGoal":
          commandState.Enabled = !this.ReadOnly && this.SelChange != null;
          return true;
        case "ECO.ChangeReason":
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.CopyAllElems":
          commandState.Enabled = this.SelChange != null;
          return true;
        case "ECO.CopyTable":
          commandState.Enabled = this.SelElem != null && this.SelElem.TemplateId == Intermech.ECO.Client.ECO.fldTable;
          return true;
        case "ECO.CreateOLE":
          commandState.Enabled = !this.ReadOnly && this.ContElem != null;
          return true;
        case "ECO.DeleteElem":
          commandState.Enabled = !this.ReadOnly && this.SelElem != null;
          return true;
        case "ECO.DeleteList":
          if (this.Document != null && this.Document.DocumentControl != null && this.Document.PageCount > 1 && !this.ReadOnly && this.Document.DocumentControl.ActivePage != null)
          {
            commandState.Enabled = true;
            return true;
          }
          commandState.Enabled = false;
          return true;
        case "ECO.DetachFromECO":
          commandState.Enabled = !this.ReadOnly && this.SelChange != null || EcoTreeViewDlg.TreeMenu && this.ecoTreeViewDlg.Selected.Count > 0 && this.ecoTreeViewDlg.Selected[0].Id != 0L;
          return true;
        case "ECO.ImgFromClip":
          commandState.Enabled = !this.ReadOnly && this.ContElem != null;
          return true;
        case "ECO.ImgFromFile":
          commandState.Enabled = !this.ReadOnly && this.ContElem != null;
          return true;
        case "ECO.ImgFromObj":
          commandState.Enabled = !this.ReadOnly && this.ContElem != null;
          return true;
        case "ECO.InsertList":
          commandState.Enabled = this.Document != null && !this.ReadOnly;
          return true;
        case "ECO.LaunchShooter":
          commandState.Enabled = true;
          return true;
        case "ECO.MoveElemDown":
          if (!this.ReadOnly && this.SelElem != null && this.SelElem is TableElement)
          {
            RectangleElement nextDataCell = (this.SelElem as TableElement).FindNextDataCell();
            RectangleElement prevDataCell = (this.SelElem as TableElement).FindPrevDataCell();
            commandState.Enabled = nextDataCell != null && prevDataCell != null;
          }
          else
            commandState.Enabled = false;
          return true;
        case "ECO.MoveElemUp":
          if (!this.ReadOnly && this.SelElem != null && this.SelElem is TableElement)
          {
            RectangleElement prevDataCell = (this.SelElem as TableElement).FindPrevDataCell();
            commandState.Enabled = prevDataCell != null && prevDataCell.FindPrevDataCell() != null;
          }
          else
            commandState.Enabled = false;
          return true;
        case "ECO.PasteElems":
          if (!this.ReadOnly && this.SelChange != null && NodeClipboardHelper.CanPasteFromClipboard((DocumentTreeNode) this.SelChange))
          {
            ClipboardDataAdditionalInfo clipboardInfo = NodeClipboardHelper.GetClipboardInfo();
            commandState.Enabled = clipboardInfo.Tag.Equals((object) "RevEditor");
          }
          else
            commandState.Enabled = false;
          return true;
        case "ECO.PasteObjects":
          IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
          commandState.Enabled = false;
          if (service != null)
          {
            object dataObject = service.GetDataObject();
            if (dataObject != null && dataObject is IDBObjectTypedIDCollection)
              commandState.Enabled = true;
          }
          return true;
        case "ECO.ProcChanges":
          commandState.Visible = true;
          commandState.Enabled = !this.ReadOnly && this.eco != null && this.eco.objLinks != null && this.eco.objLinks.Count > 0;
          return true;
        case "ECO.ReplaceTemplate":
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.SetPLForAll":
          commandState.Enabled = !this.ReadOnly && this.eco.objLinks.Count > 0 && this.plugin.DoSetPLForAll != null;
          return true;
        case "ECO.SortByDes":
          commandState.Enabled = !this.ReadOnly && this.SelChange != null;
          return true;
        case "ECO.SpecSymbol":
          this.textFld = this.DocumentControl.ActiveElement as TextData;
          commandState.Enabled = !this.ReadOnly && this.textFld != null && !this.textFld.ReadOnly && this.textFld.InPlaceEditorActive;
          return true;
        case "ECO.TestMenu":
          commandState.Enabled = true;
          return true;
        case "Paste":
          if (this.ReadOnly)
          {
            commandState.Enabled = false;
            return true;
          }
          DocumentTreeNode documentTreeNode = this.DocumentControl != null ? this.DocumentControl.ActiveElement : (DocumentTreeNode) null;
          if (documentTreeNode != null)
          {
            if (documentTreeNode is TextBoxElement)
            {
              if (!(documentTreeNode as TextBoxElement).ReadOnly)
              {
                try
                {
                  commandState.Enabled = Clipboard.ContainsText();
                  goto label_33;
                }
                catch
                {
                  commandState.Enabled = true;
                  goto label_33;
                }
              }
            }
            if (documentTreeNode is ContainerElement)
            {
              if (!(documentTreeNode as ContainerElement).ReadOnly)
              {
                try
                {
                  commandState.Enabled = Clipboard.ContainsImage();
                }
                catch
                {
                  commandState.Enabled = true;
                }
                return true;
              }
            }
            while (documentTreeNode != null && (!(documentTreeNode is TableElement) || !(documentTreeNode.TemplateId == Intermech.ECO.Client.ECO.fldTable)))
              documentTreeNode = documentTreeNode.Parent;
            commandState.Enabled = documentTreeNode != null && Clipboard.ContainsImage();
          }
label_33:
          return true;
        default:
          if (base.QueryStatus(commandState))
            return true;
          break;
      }
    }
    catch
    {
      throw;
    }
    return false;
  }

  private void AttachAlternative()
  {
    if (!ECOPlugin.ValidateExcessDocuments(this.GetObjectCount()))
      return;
    List<long> objIDs = new List<long>();
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    List<long> noDObjs = new List<long>();
    try
    {
      using (ServiceContainer serviceContainer = new ServiceContainer())
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.ecoID);
          IObjectTypeNodeFilter serviceInstance = (IObjectTypeNodeFilter) new ObjectTypeNodeFilter();
          serviceContainer.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
          ECOPlugin plugin = ECOPlugin.FindPlugin();
          List<int> allowedTypes = plugin.GetAllowedTypes(this.revObjType);
          List<int> intList3 = new List<int>(0);
          foreach (int childTypeID in allowedTypes)
          {
            List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(childTypeID);
            parentsIdReverse.Add(childTypeID);
            for (int index = 0; index < parentsIdReverse.Count; ++index)
            {
              int num = parentsIdReverse[index];
              if (allowedTypes.Contains(num))
              {
                if (!intList3.Contains(num))
                {
                  intList3.Add(num);
                  break;
                }
                break;
              }
            }
          }
          DescriptorCollection descriptors = new DescriptorCollection();
          descriptors.Add((IDescriptor) new ObjectTypesDescriptor(intList3.ToArray(), LocalizationHolder.rm.GetString("ECO.Client_32")));
          Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("ECO.Client_398"), descriptors);
          string nameInMessages = dbObject1.NameInMessages;
          IArchivesDescriptorService service = (IArchivesDescriptorService) ServicesManager.GetService(typeof (IArchivesDescriptorService));
          if (service != null)
            descriptors.Add(service.GetDescriptor());
          IDBTypedObjectID[] dbTypedObjectIdArray = (IDBTypedObjectID[]) SelectionWindow.Select(string.Format(LocalizationHolder.rm.GetString("ECO.Client_29") + LocalizationHolder.rm.GetString("ECO.Client_31"), (object) nameInMessages), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default, allowedTypes.ToArray());
          if (dbTypedObjectIdArray == null)
            return;
          bool designByTemplate = plugin.eps.Current.ReplaceEmptyDesignByTemplate;
          bool flag1 = plugin.eps.Current.InvNumAttr != "";
          foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIdArray)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(dbTypedObjectId.ObjectID, false);
            if (dbObject2 != null)
            {
              bool flag2 = designByTemplate & flag1;
              if (!flag2)
              {
                IDBAttribute attributeByGuid = dbObject2.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
                flag2 = attributeByGuid != null && attributeByGuid.AsString != "";
              }
              if (!flag2)
              {
                noDObjs.Add(dbTypedObjectId.ObjectID);
              }
              else
              {
                objIDs.Add(dbTypedObjectId.ObjectID);
                intList1.Add(dbObject2.ObjectType);
                intList2.Add(dbObject2.VersionID);
              }
            }
          }
        }
        if (objIDs.Count <= 0)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_229"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
          return;
        }
        for (int index = objIDs.Count - 1; index >= 0; --index)
        {
          long objId = objIDs[index];
          if (this.eco.ObjIdIndex(objId) >= 0)
          {
            int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_180"), (object) objId));
            objIDs.RemoveAt(index);
          }
        }
        if (objIDs.Count == 0)
          return;
      }
      IncludeGoal includeGoal = new IncludeGoal();
      ECOGoal goal;
      if (this.elCurChange != null && !Intermech.ECO.Client.ECO.IsExternal(this.elCurChange))
      {
        goal = this.eco.ChangeGoal((DocumentTreeNode) this.elCurChange);
        string attributeValue = this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
        List<long> addObjects = (List<long>) null;
        if (attributeValue != "")
          addObjects = this.eco._GetIdList(attributeValue);
        if (!includeGoal.Execute(goal, objIDs, this.eco.litera, noDObjs, addObjects, this.eco.revType))
          return;
      }
      else
      {
        ECOGoal force = this.eco.HasTerm() ? ECOGoal.Change : ECOGoal.NoGoal;
        if (!includeGoal.Execute(objIDs, this.eco.litera, noDObjs, (List<long>) null, this.eco.revType, force))
          return;
        goal = includeGoal.goal;
      }
      bool flag = this.eco.litera != includeGoal.litera;
      this.eco.litera = includeGoal.litera;
      List<long> finalObjectList = includeGoal.GetFinalObjectList();
      Hashtable synchroTab = new Hashtable();
      if (goal == ECOGoal.Litera)
      {
        List<long> synchroList = new List<long>();
        int index = 0;
        while (index < finalObjectList.Count)
        {
          long num = finalObjectList[index];
          if (ECOPlugin.GetSynchroParents(num, synchroList))
          {
            if (synchroList.Count == 1)
            {
              synchroTab.Add((object) num, (object) synchroList);
            }
            else
            {
              ChooseSynchroDlg chooseSynchroDlg = new ChooseSynchroDlg();
              if (chooseSynchroDlg.Execute(synchroList, num))
              {
                List<long> longList = chooseSynchroDlg.ComposeChosenList();
                if (longList.Count > 0)
                  synchroTab.Add((object) num, (object) longList);
              }
              else
              {
                finalObjectList.RemoveAt(index);
                continue;
              }
            }
          }
          ++index;
        }
      }
      if (synchroTab.Count == 0)
        synchroTab = (Hashtable) null;
      this.NewAttachItems(finalObjectList, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, includeGoal.separateChanges, synchroTab: synchroTab, curTE: this.elCurChange);
      if (!flag)
        return;
      TableData dataOwner;
      for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        if (dataOwner.Nodes[dataPositionInFlow] is TableData node && Intermech.ECO.Client.ECO.IsChange((DocumentTreeNode) node) && this.ECO.ChangeGoal((DocumentTreeNode) node) == ECOGoal.Litera)
          this.UpdateSpecText(node as TableElement);
      }
    }
    finally
    {
      if (this.CanShowRedline())
      {
        this.Document.UpdateLayout(0, true, true);
        Application.DoEvents();
        this.DocumentControl.Refresh();
      }
    }
  }

  public void NewAttachItems(
    List<long> parts,
    ECOGoal goal,
    int schemeId,
    int selLCStepId,
    bool separateChanges,
    List<HidingType> hidingTypes = null,
    Hashtable synchroTab = null,
    TableElement curTE = null,
    string forceChangeNo = null,
    List<long> allVersions = null)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    try
    {
      if (goal == ECOGoal.Annul && !this.ValidateAnnul(parts, this.eco.EcoObjectID))
        return;
      if (this.eco.OrgSet.Count > 0 && ECOPlugin.plugin.eps.Current.AskOnNewOrganizations)
      {
        List<long> wrongParts = new List<long>();
        StringBuilder stringBuilder = new StringBuilder();
        HashSet<long> other1 = new HashSet<long>();
        foreach (long part in parts)
        {
          List<long> abonListIds = Intermech.ECO.Client.ECO.GetAbonListIds(part, RevHelper.idObjOrganization);
          if (abonListIds != null)
          {
            HashSet<long> other2 = new HashSet<long>((IEnumerable<long>) abonListIds);
            if (other2.Count != 0 && !other2.IsSubsetOf((IEnumerable<long>) this.eco.OrgSet))
            {
              wrongParts.Add(part);
              if (stringBuilder.Length != 0)
                stringBuilder.Append(", ");
              stringBuilder.Append(Convert.ToString(part));
              other1.UnionWith((IEnumerable<long>) other2);
            }
          }
        }
        if (wrongParts.Count > 0)
        {
          switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_460"), (object) stringBuilder.ToString()), LocalizationHolder.rm.GetString("ECO.Client_435"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
          {
            case DialogResult.Yes:
              this.eco.OrgSet.UnionWith((IEnumerable<long>) other1);
              break;
            case DialogResult.No:
              parts.RemoveAll((Predicate<long>) (id => wrongParts.Contains(id)));
              if (parts.Count == 0)
                return;
              break;
            default:
              return;
          }
        }
      }
      else
      {
        foreach (long part in parts)
        {
          List<long> abonListIds = Intermech.ECO.Client.ECO.GetAbonListIds(part, RevHelper.idObjOrganization);
          if (abonListIds != null && abonListIds.Any<long>())
            this.eco.OrgSet.UnionWith((IEnumerable<long>) new HashSet<long>((IEnumerable<long>) abonListIds));
        }
      }
      this.UndoManager.Clear();
      this.UndoManager.LockUndo();
      this.DocumentControl.EditorValidating();
      TextData razoslatField = this.GetRazoslatField();
      HashSet<long> objIDs = new HashSet<long>();
      List<List<PendingLink>> changeList = new List<List<PendingLink>>();
      if (!separateChanges)
        changeList.Add(new List<PendingLink>());
      if (curTE != null)
      {
        string attributeValue = curTE.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
        if (attributeValue != "")
        {
          List<long> idList = this.eco._GetIdList(attributeValue);
          if (changeList.Count == 0)
            changeList.Add(new List<PendingLink>());
          List<PendingLink> pendingLinkList = changeList[0];
          foreach (long objId in idList)
          {
            PendingLink pendingLink = this.eco.FindPendingLink(objId);
            if (pendingLink != null)
            {
              pendingLinkList.Add(pendingLink);
              pendingLink.LockMove = true;
              if (!objIDs.Contains(pendingLink.verID))
                objIDs.Add(pendingLink.verID);
            }
          }
        }
      }
      for (int index1 = 0; index1 < parts.Count; ++index1)
      {
        long num1 = parts[index1];
        long num2 = Math.Abs(num1);
        if (this.eco.ObjIdIndex(num2) >= 0)
        {
          int num3 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_180"), (object) num2));
        }
        else if (!this.addedLinks.ContainsKey((object) Math.Abs(num1)))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IUserSession session = sessionKeeper.Session;
            if (this.eco.HasThisObjectVersion(session, num1) || this.eco.HasThisObjectVersion(session, (IEnumerable<PendingLink>) this.GetPendingList(this.addedLinks), num1))
            {
              long objectFId = session.GetObjectF_ID(num1);
              IEnumerator enumerator = this.addedLinks.Values.GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  PendingLink current = (PendingLink) enumerator.Current;
                  if (objectFId == current.ID)
                  {
                    current.hideType = HidingType.Disabled;
                    current.needDelete = true;
                    break;
                  }
                }
                continue;
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
            }
            else if (session.GetObject(num1, false) == null)
              continue;
          }
          try
          {
            bool flag = false;
            HidingType hType = HidingType.Disabled;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IUserSession session = sessionKeeper.Session;
              IDBObject dbObject1 = session.GetObject(num1, false);
              if (this.ValidateAttachToECO(dbObject1, goal, session))
              {
                hType = HidingType.Disabled;
                if (hidingTypes != null)
                  hType = hidingTypes[index1];
                long verForThisContext = this.GetObjectVerForThisContext(session, dbObject1);
                switch (verForThisContext)
                {
                  case -1:
                  case 0:
                    break;
                  default:
                    parts[index1] = verForThisContext;
                    if (goal == ECOGoal.Creation)
                    {
                      hType = HidingType.Disabled;
                    }
                    else
                    {
                      IDBObject dbObject2 = session.GetObject(verForThisContext, false);
                      if (dbObject2 != null)
                        hType = new ReqRevisionInfo(RevReqHelper.GetRevReq(dbObject2.LCStep, dbObject2.ObjectType)).reqType == RequireClass.NoRequire ? HidingType.CanBeHidden : HidingType.Disabled;
                    }
                    flag = Math.Abs(num1) == Math.Abs(verForThisContext);
                    if (!flag)
                    {
                      num1 = verForThisContext;
                      num2 = Math.Abs(verForThisContext);
                      break;
                    }
                    break;
                }
              }
              else
                continue;
            }
            List<PendingLink> pendingLinkList = flag ? this.CopyExistingLink(num2, goal) : this.AddPendingLink(num2, goal, selLCStepId, hType, allVersions, synchroTab);
            if (flag && pendingLinkList.Count == 0)
              pendingLinkList = this.AddPendingLink(num2, goal, selLCStepId, hType, allVersions, synchroTab);
            if (pendingLinkList.Count == 0 && this.eco.ObjIdIndex(num2) == -1)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBObject dbObject = sessionKeeper.Session.GetObject(num1, false);
                int num4 = dbObject.VersionID;
                if (num4 == 0)
                  num4 = 1;
                PendingLink pendingLink = new PendingLink(num2, Convert.ToString(num4), goal, selLCStepId);
                pendingLink.SetDesign(dbObject);
                pendingLinkList.Add(pendingLink);
              }
            }
            for (int index2 = pendingLinkList.Count - 1; index2 >= 0; --index2)
            {
              PendingLink pendingLink = pendingLinkList[index2];
              int index3 = this.eco.ObjIdIndex(pendingLink.verID);
              if (index3 >= 0)
              {
                this.eco.objLinks.RemoveAt(index3);
                this.eco.objLinks.Add(pendingLink);
                pendingLinkList.RemoveAt(index2);
              }
              else
              {
                this.eco.objLinks.Add(pendingLink);
                if (!objIDs.Contains(pendingLink.verID) && !objIDs.Contains(-pendingLink.verID))
                  objIDs.Add(pendingLink.verID);
                else
                  pendingLinkList.RemoveAt(index2);
              }
            }
            if ((index1 == 0 || !separateChanges) && changeList.Count > 0)
            {
              foreach (PendingLink pendingLink in pendingLinkList)
                changeList[0].Add(pendingLink);
            }
            else
              changeList.Add(pendingLinkList);
          }
          catch (Exception ex)
          {
            if (goal == ECOGoal.Annul)
            {
              using (List<long>.Enumerator enumerator = parts.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  long key = Math.Abs(enumerator.Current);
                  if (this.addedLinks.ContainsKey((object) key))
                    this.addedLinks.Remove((object) key);
                }
                break;
              }
            }
            throw;
          }
        }
      }
      if (objIDs.Count == 0)
        return;
      if (goal == ECOGoal.Litera && this.eco.litera != "")
      {
        int maxLiteraIndex = plugin.GetMaxLiteraIndex((IEnumerable<long>) objIDs);
        if (maxLiteraIndex >= plugin.PossibleLiteras.IndexOf(this.eco.litera))
        {
          string possibleLitera = plugin.PossibleLiteras[maxLiteraIndex];
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_413"), (object) possibleLitera, (object) this.eco.litera), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
      }
      if (plugin.eps.Current.HideOnCreation)
      {
        foreach (List<PendingLink> pendingLinkList in changeList)
        {
          foreach (PendingLink pendingLink in pendingLinkList)
          {
            if (pendingLink.ecoGoal == ECOGoal.Creation && pendingLink.hideType == HidingType.Disabled)
              pendingLink.hideType = HidingType.CanBeHidden;
          }
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!HideTypes.All.Loaded)
          HideTypes.All.Load(sessionKeeper.Session);
        if (HideTypes.All.Count > 0)
        {
          HashSet<int> intSet = new HashSet<int>();
          foreach (int parentTypeID in (HashSet<int>) HideTypes.All)
            intSet.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID));
          foreach (List<PendingLink> pendingLinkList in changeList)
          {
            foreach (PendingLink pendingLink in pendingLinkList)
            {
              if (pendingLink.objType == -1)
              {
                IDBObject dbObject = sessionKeeper.Session.GetObject(pendingLink.verID, false) ?? sessionKeeper.Session.GetObject(-pendingLink.verID, false);
                pendingLink.objType = dbObject.ObjectType;
                if (Intermech.ECO.Client.ECO.invNumTemplate == null || !ECOPlugin.plugin.eps.Current.PlaceInvNum)
                  pendingLink.UpdateDesign();
              }
              if (intSet.Contains(pendingLink.objType))
              {
                pendingLink.hideType = HidingType.Hidden;
                long objVerID = pendingLink.verID;
                if (allVersions != null && allVersions.Contains(-objVerID))
                  objVerID = -objVerID;
                long revRelation = RevHelper.GetRevRelation(this.eco.EcoObjectID, objVerID);
                IDBRelation relation = revRelation != 0L ? sessionKeeper.Session.GetRelation(revRelation, false) : (IDBRelation) null;
                if (relation != null)
                {
                  IDBAttribute attributeById = relation.GetAttributeByID(RevHelper.idAttrHiding);
                  if (attributeById != null)
                    attributeById.AsInteger = 2L;
                }
              }
            }
          }
        }
      }
      if (objIDs.Count > 1)
        new AddObjsChanges().Execute(changeList);
      List<long> longList = new List<long>();
      bool flag1 = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index4 = 0; index4 < changeList.Count; ++index4)
        {
          List<PendingLink> newLinks = changeList[index4];
          for (int index5 = newLinks.Count - 1; index5 >= 0; --index5)
          {
            PendingLink pendingLink = newLinks[index5];
            long num = pendingLink.verID;
            if (allVersions != null && allVersions.Contains(-num))
              num = -num;
            long revRelation = RevHelper.GetRevRelation(this.eco.EcoObjectID, num);
            IDBRelation relation = revRelation != 0L ? sessionKeeper.Session.GetRelation(revRelation, false) : (IDBRelation) null;
            IDBObject idbO = sessionKeeper.Session.GetObject(num, false);
            switch (pendingLink.hideType)
            {
              case HidingType.Disabled:
                if (relation != null)
                {
                  relation.GetAttributeByID(RevHelper.idAttrHiding).AsInteger = 0L;
                  goto default;
                }
                goto default;
              case HidingType.CanBeHidden:
                if (relation != null)
                {
                  relation.GetAttributeByID(RevHelper.idAttrHiding).AsInteger = 1L;
                  goto default;
                }
                goto default;
              case HidingType.Hidden:
                this.eco.hiddenLinks.Add(pendingLink);
                this.RemoveChangeNoAttr(sessionKeeper.Session, num);
                newLinks.RemoveAt(index5);
                longList.Add(num);
                break;
              default:
                if (idbO != null)
                {
                  try
                  {
                    string initValue = "";
                    if (forceChangeNo == null)
                    {
                      if (pendingLink.verStr == "")
                      {
                        string cNo = "";
                        bool forceNewNum = goal == ECOGoal.Litera && !plugin.eps.Current.CreateLiteraVersion;
                        idbO = this.AssignChangeNo(sessionKeeper.Session, idbO, goal, out cNo, forceNewNum);
                        pendingLink.verStr = cNo;
                        initValue = cNo;
                      }
                    }
                    else
                    {
                      this.SetNewChangeNo(sessionKeeper.Session, idbO.ObjectID, forceChangeNo, goal);
                      pendingLink.verStr = forceChangeNo;
                      initValue = forceChangeNo;
                    }
                    if (initValue != "")
                    {
                      if (RevHelper.Global.NotifService != null)
                      {
                        long objectId = idbO.ObjectID;
                        if (idbO.ObjectModifyMode == ObjectModifyModes.Checkout && objectId > 0L)
                        {
                          idbO = sessionKeeper.Session.GetObject(objectId);
                          objectId = idbO.ObjectID;
                        }
                        AttributeValues oldAttrValues = new AttributeValues(RevHelper.idAttrChangeNo, (object) "");
                        oldAttrValues.AttributeName = RevHelper.nameAttrChangeNo;
                        AttributeValues newAttrValues = new AttributeValues(RevHelper.idAttrChangeNo, (object) initValue);
                        oldAttrValues.AttributeName = RevHelper.nameAttrChangeNo;
                        RevHelper.Global.NotifService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(objectId, idbO.ObjectType, oldAttrValues, newAttrValues));
                      }
                    }
                  }
                  catch (Exception ex)
                  {
                    ExceptionHelper.ExceptionService.ShowException(ex);
                    pendingLink.verStr = "";
                  }
                  if ((goal != ECOGoal.Change ? 1 : (!plugin.eps.Current.LeaveOTDNumberForChange ? 1 : 0)) != 0 && sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer customService)
                  {
                    IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(idbO.ObjectID, false);
                    long objId = objectActualCopy != null ? objectActualCopy.ObjectID : num;
                    customService.ClearOTDAttrs(objId);
                    break;
                  }
                  break;
                }
                break;
            }
          }
          if (newLinks.Count != 0)
          {
            TableElement tableElement = (index4 == 0 ? curTE : (TableElement) null) ?? (this.indexCurChange >= 0 ? this.eco.InsertNewEcoRow(this.indexCurChange, Intermech.ECO.Client.ECO.fldChange) : this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange));
            if (tableElement != null)
            {
              string str = this._AddObjectsGuids((TableData) tableElement, newLinks);
              if (tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive)
              {
                if (templateRecursive.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) != str)
                  templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, str);
                templateRecursive.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
                templateRecursive.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
                if (this.eco.revType == RevType.PI || this.eco.revType == RevType.PR)
                  templateRecursive.ReadOnly = true;
              }
              tableElement.SetAttributeValue(Intermech.ECO.Client.ECO.schemeIdAttr, Convert.ToString(schemeId));
              List<long> idList = this.ECO._GetIdList(str);
              if (curTE != null)
              {
                TableData firstTable = tableElement.FindFirstTable();
                this.eco.ecoMainTable.UniteTable();
                tableElement = (TableElement) this.eco.ecoMainTable.FindNode(firstTable.Id);
                flag1 = true;
              }
              bool flag2 = this.UpdateMultiChangeHeader((TableData) tableElement, idList, false);
              bool flag3 = this.UpdateSpecText(tableElement) | flag2;
              flag1 |= flag3;
              if (razoslatField != null)
                this.SetRazoslatAttrForChange(tableElement, idList);
            }
          }
        }
      }
      this.UpdateDocDesign();
      if (razoslatField != null)
        this.UpdateSendTo(razoslatField);
      if (synchroTab != null)
      {
        foreach (long key in (IEnumerable) synchroTab.Keys)
        {
          PendingLink pendingLink = this.eco.FindPendingLink(key);
          if (pendingLink != null)
          {
            foreach (long Id in (List<long>) synchroTab[(object) key])
              pendingLink.AddAuxObject(Id);
            long revRelation = RevHelper.GetRevRelation(this.eco.EcoObjectID, pendingLink.verID);
            if (revRelation != 0L)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBRelation relation = sessionKeeper.Session.GetRelation(revRelation, false);
                if (relation != null)
                  this.UpdateAuxLinks(relation, ref pendingLink.auxObjects, false);
              }
            }
          }
        }
      }
      if (flag1)
        this.Document.UpdateLayout(0, true, true);
      if (plugin != null && plugin.eps.Current.AutoCheckOut)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (long num in objIDs)
          {
            long objectID = num;
            if (allVersions != null && allVersions.Contains(-objectID))
              objectID = -objectID;
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
            if (dbObject != null && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
              dbObject.CheckOut();
          }
        }
      }
      this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
      this.CommandManager.QueryStatus();
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  private List<PendingLink> GetPendingList(Hashtable ht)
  {
    List<PendingLink> pendingList = new List<PendingLink>();
    foreach (PendingLink pendingLink in (IEnumerable) ht.Values)
      pendingList.Add(pendingLink);
    return pendingList;
  }

  private TextData GetRazoslatField()
  {
    DocumentTreeNode templateRecursive = this.eco.DocumentECO.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSendTo);
    if (templateRecursive != null && templateRecursive is TextData)
    {
      TextData razoslatField = templateRecursive as TextData;
      if (!razoslatField.ContainsAttribute("ChangedByUser"))
        razoslatField.SetAttributeValue("ChangedByUser", "");
      if (razoslatField.GetAttributeValue("ChangedByUser", true) != "Changed")
        return razoslatField;
    }
    return (TextData) null;
  }

  private void UpdateSendTo(TextData td)
  {
    if (td == null)
      return;
    string str = "";
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      string attributeValue = (dataOwner.Nodes[dataPositionInFlow] as TableData).GetAttributeValue("*razoslat*", true);
      if (str == "")
        str = attributeValue;
      else if (str != attributeValue)
      {
        td.AssignText("", true, true, false);
        return;
      }
    }
    td.AssignText(str, true, true, false);
  }

  private void SetRazoslatAttrForChange(TableElement change, List<long> idList = null)
  {
    if (idList == null)
    {
      string attributeValue = change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
      if (attributeValue != "")
        idList = this.eco._GetIdList(attributeValue);
    }
    if (idList == null || idList.Count == 0)
      return;
    long objId = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long id in idList)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(id);
        if (!objectInfo.Empty)
        {
          long sendList = RevHelper.GetSendList(objectInfo.ID);
          if (objId != 0L && objId != sendList)
            return;
          objId = sendList;
        }
      }
    }
    List<string> abonList = Intermech.ECO.Client.ECO.GetAbonList(objId);
    if (abonList == null)
      return;
    string attributeValue1 = ECOPlugin.FormatAbonents(abonList);
    change.SetAttributeValue("*razoslat*", attributeValue1);
  }

  private void AttachToECO_ExternalDoc()
  {
    this.UndoManager.BeginCreateMultyUndo("Вставка внешнего документа");
    try
    {
      TableElement tableElement = this.indexCurChange >= 0 ? this.eco.InsertNewEcoRow(this.indexCurChange, Intermech.ECO.Client.ECO.fldChange) : this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
      tableElement.Tag = (object) DBNull.Value;
      if (tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive)
      {
        templateRecursive.ReadOnly = false;
        templateRecursive.TextChanged += new TextChanged_EventHandler(this.externalDocDesignChanged);
        IPageElementWithInterface elementWithInterface = templateRecursive as IPageElementWithInterface;
        elementWithInterface.InplaceEditorActivating += new CancelEventHandler(this.ExternalDoc_DesignEditing);
        elementWithInterface.InplaceEditorDeactivating += new CancelEventHandler(this.ExternalDoc_DesignDeactivating);
      }
      tableElement.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, "");
      this.UpdateDocDesign();
      this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
      this.Document.UpdateLayout(0, true, true);
    }
  }

  public void DeleteObjects(List<long> objects)
  {
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      if (dataOwner.Nodes[dataPositionInFlow] is TableData node && node.Template.Name == Intermech.ECO.Client.ECO.fldChange)
      {
        string tagStr = Convert.ToString(node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
        if (tagStr != "")
        {
          List<long> idList = this.eco._GetIdList(tagStr);
          bool flag = false;
          int index = 0;
          while (index < idList.Count)
          {
            if (objects.Contains(idList[index]))
            {
              idList.RemoveAt(index);
              flag = true;
            }
            else
              ++index;
          }
          if (idList.Count == 0)
          {
            node.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, "#");
            documentTreeNodeList.Add((DocumentTreeNode) node);
          }
          else if (flag)
          {
            this.eco._SetIdList((RectangleElement) node, idList);
            documentTreeNodeList.Add((DocumentTreeNode) node);
          }
        }
      }
    }
    int index1 = 0;
    bool flag1 = false;
    while (index1 < documentTreeNodeList.Count)
    {
      TableData tableData = documentTreeNodeList[index1] as TableData;
      if (tableData.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) == "#")
      {
        documentTreeNodeList.RemoveAt(index1);
        tableData.UniteTable();
        tableData.Remove(true, true);
        flag1 = true;
      }
      else
        ++index1;
    }
    foreach (DocumentTreeNode change in documentTreeNodeList)
      this.UpdateMultiChangeHeader(change as TableData, this.eco._GetIdList(Convert.ToString(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true))));
    if (flag1)
      this.Document.UpdateLayout(0, true, true);
    this.CommandManager.QueryStatus();
  }

  private void DetachFromECOCommand()
  {
    List<PendingLink> pendingLinkList1 = new List<PendingLink>();
    HashSet<long> longSet = new HashSet<long>();
    List<long> objects = (List<long>) null;
    using (ObjSelect objSelect = new ObjSelect())
    {
      List<long> objIds = this.eco.ObjIdPrimaryList();
      List<HidingType> mainItems = this.eco.ObjHideStatusList();
      for (int index = objIds.Count - 1; index >= 0; --index)
      {
        if (mainItems[index] == HidingType.Hidden)
        {
          mainItems.RemoveAt(index);
          objIds.RemoveAt(index);
        }
      }
      objects = objSelect.Execute(objIds, true, false, mainItems);
      if (objects.Count == 0)
        return;
    }
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      this.DeleteObjects(objects);
      foreach (long num in objects)
      {
        int index = this.eco.ObjIdIndex(num);
        if (index >= 0)
        {
          PendingLink objLink = this.eco.objLinks[index];
          if (objLink.hideType == HidingType.Disabled)
          {
            this.DeletePendingLink(num, index);
            if (objLink.auxObjects != null)
            {
              foreach (ObjInfo auxObject in objLink.auxObjects)
              {
                if (!longSet.Contains(auxObject.verId))
                  longSet.Add(auxObject.verId);
              }
            }
          }
          else
            pendingLinkList1.Add(objLink);
          this.eco.objLinks.RemoveAt(index);
        }
      }
      bool flag = pendingLinkList1.Count > 0 || longSet.Count > 0;
      if (pendingLinkList1.Count > 0)
      {
        foreach (PendingLink pl in pendingLinkList1)
        {
          if (!longSet.Contains(pl.verID))
            this._DoHiding(pl);
        }
      }
      if (longSet.Count > 0)
      {
        List<PendingLink> pendingLinkList2 = new List<PendingLink>();
        foreach (PendingLink hiddenLink in this.eco.hiddenLinks)
        {
          if (longSet.Contains(hiddenLink.verID))
            pendingLinkList2.Add(hiddenLink);
        }
        foreach (PendingLink pendingLink in pendingLinkList2)
          this.eco.hiddenLinks.Remove(pendingLink);
      }
      if (flag)
        this.ecoTreeViewDlg.UpdateTree();
      this.UpdateDocDesign();
      this.UpdateSpecText();
      this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
      this.CommandManager.QueryStatus();
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  private bool ValidateAttachToECO(IDBObject dbObject, ECOGoal goal, IUserSession ius)
  {
    string text = "";
    ObjectModifyModes objectModifyMode = dbObject.ObjectModifyMode;
    try
    {
      if (this.plugin.GetAllowedTypes(this.revObjType).IndexOf(dbObject.TypeID) < 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.TypeID);
          text = string.Format(LocalizationHolder.rm.GetString("ECO.Client_36"), (object) objectType.ObjectTypeName);
        }
      }
      if (text != "")
        return false;
      if (objectModifyMode != ObjectModifyModes.CreateVersion)
      {
        object[] valuesById = dbObject.GetValuesByID(RevHelper.idAttrChangesGroupNum, false);
        if (valuesById != null && valuesById.Length != 0)
        {
          long int64 = Convert.ToInt64(valuesById[0]);
          if (int64 != 0L && int64 != this.eco.linkedContextNo)
          {
            if (goal == ECOGoal.Litera && this.eco.linkedContextNo == Math.Abs(this.eco.EcoObjectID) && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_412"), (object) dbObject.Caption, (object) dbObject.ObjectID), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
              IDBAttribute dbAttribute = ius.GetObject(this.eco.EcoObjectID).Attributes.AddAttribute(RevHelper.idLinkedContNumber, false);
              if (dbAttribute != null)
              {
                dbAttribute.AsInteger = int64;
                this.eco.linkedContextNo = int64;
                return true;
              }
            }
            text = string.Format(LocalizationHolder.rm.GetString("ECO.Client_37"), (object) dbObject.NameInMessages, (object) int64);
          }
        }
        if (text != "")
          return false;
        long modificationId = dbObject.ModificationID;
        if (modificationId != 0L && Math.Abs(modificationId) != Math.Abs(this.eco.linkedContextNo))
          text = string.Format(LocalizationHolder.rm.GetString("ECO.Client_37"), (object) dbObject.NameInMessages, (object) modificationId);
        if (text != "")
          return false;
      }
      IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_Designation);
      string str = "";
      if (attributeById != null)
        str = attributeById.Description;
      if (str.Trim() == "" && (!this.plugin.eps.Current.ReplaceEmptyDesignByTemplate || this.plugin.eps.Current.InvNumAttr == ""))
        text = LocalizationHolder.rm.GetString("ECO.Client_39") + dbObject.NameInMessages;
      if (text != "")
        return false;
    }
    finally
    {
      if (text != "")
      {
        int num = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("ECO.Client_45"));
      }
    }
    return text == "";
  }

  private bool ValidateAnnul(List<long> newObjs, long thisRevId)
  {
    List<long> longList1 = this.eco.AnnulIdList();
    foreach (long newObj in newObjs)
    {
      if (longList1.IndexOf(newObj) < 0 && longList1.IndexOf(-newObj) < 0)
        longList1.Add(newObj);
    }
    List<long> longList2 = new List<long>();
    List<long> longList3 = new List<long>();
    List<long> longList4 = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
      relationCollection.LocalTypesMode = true;
      foreach (long newObj in newObjs)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -21,
          (object) -20
        });
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, newObj);
        if (dataTable.Rows.Count != 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64_1 = Convert.ToInt64(row[0]);
            long int64_2 = Convert.ToInt64(row[1]);
            int objectTypeId1 = session.GetObjectInfo(int64_1).ObjectTypeID;
            int objectTypeId2 = session.GetObjectInfo(newObj).ObjectTypeID;
            int relationType4PrjLinkId = MetaDataHelper.GetRelationType4PrjLinkID(session, int64_2);
            IMSRelationType relationType = MetaDataHelper.GetRelationType(relationType4PrjLinkId);
            if (relationType != null && (relationType.Options & RelationTypeOptions.EnableCheckAnnulment) != RelationTypeOptions.None && !ECOPlugin.IsSynchroMove(objectTypeId1, objectTypeId2, relationType4PrjLinkId))
              longList4.Add(newObj);
          }
        }
      }
    }
    if (longList4.Count <= 0)
      return true;
    StringBuilder stringBuilder = new StringBuilder(LocalizationHolder.rm.GetString("ECO.Client_232") + "\n\r");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in longList4)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
        if (dbObject != null)
          stringBuilder.Append($"{dbObject.Caption} [{Convert.ToString(objectID)}]\n\r");
      }
    }
    int num = (int) MessageBox.Show(stringBuilder.ToString(), LocalizationHolder.rm.GetString("ECO.Client_117"));
    return false;
  }

  private string SortChangeByDes(TableData change)
  {
    bool flag1 = false;
    bool flag2 = change.Tag == DBNull.Value;
    string[] strList = (string[]) null;
    List<long> longList = (List<long>) null;
    if (flag2)
    {
      if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive1)
        strList = Intermech.ECO.Client.ECO.Str2StrList(templateRecursive1.Text);
    }
    else
    {
      longList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      if (longList.Count == 0)
        return "";
      strList = new string[longList.Count];
      for (int index1 = 0; index1 < this.eco.objLinks.Count; ++index1)
      {
        int index2 = longList.IndexOf(this.eco.objLinks[index1].verID);
        if (index2 >= 0)
          strList[index2] = this.eco.objLinks[index1].design;
      }
    }
    if (strList == null)
      return "";
    for (int index3 = strList.Length - 2; index3 >= 0; --index3)
    {
      for (int index4 = index3; index4 < strList.Length - 1; ++index4)
      {
        if (string.Compare(strList[index4], strList[index4 + 1]) > 0)
        {
          if (!flag2)
          {
            long num = longList[index4];
            longList[index4] = longList[index4 + 1];
            longList[index4 + 1] = num;
          }
          string str = strList[index4];
          strList[index4] = strList[index4 + 1];
          strList[index4 + 1] = str;
          flag1 = true;
        }
      }
    }
    if (flag1)
    {
      if (flag2)
      {
        if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive2)
          templateRecursive2.AssignText(Intermech.ECO.Client.ECO.StrList2Str(strList), false, true, true);
      }
      else
      {
        this.eco._SetIdList((RectangleElement) change, longList);
        this.UpdateMultiChangeHeader(change, longList, false);
      }
      this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
    }
    return strList.Length != 0 ? strList[0] : "";
  }

  public void DeleteLinksToCanceledVersions(List<long> canceledVerList)
  {
    if (this.eco == null || this.eco.objLinks == null || this.Document == null)
      return;
    HashSet<long> longSet = new HashSet<long>();
    foreach (long canceledVer in canceledVerList)
      longSet.Add(Math.Abs(canceledVer));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      for (int index = this.eco.objLinks.Count - 1; index >= 0; --index)
      {
        PendingLink objLink = this.eco.objLinks[index];
        if (longSet.Contains(Math.Abs(objLink.verID)))
        {
          if (objLink.relId != 0L)
          {
            using (new ECOLinkDeleter(session, objLink.relId))
              session.GetRelation(objLink.relId, false)?.Delete((long) Intermech.Consts.PurgeMode);
          }
          this.eco.objLinks.RemoveAt(index);
        }
      }
    }
    for (int index = this.eco.hiddenLinks.Count - 1; index >= 0; --index)
    {
      PendingLink hiddenLink = this.eco.hiddenLinks[index];
      if (longSet.Contains(Math.Abs(hiddenLink.verID)))
        this.eco.hiddenLinks.RemoveAt(index);
    }
    this.RemoveChangesForCanceledVersions();
    foreach (long canceledVer in canceledVerList)
    {
      if (this.addedLinks.ContainsKey((object) canceledVer))
        this.addedLinks.Remove((object) canceledVer);
      if (this.addedLinks.ContainsKey((object) -canceledVer))
        this.addedLinks.Remove((object) -canceledVer);
      if (this.changedLinks.ContainsKey(canceledVer))
        this.changedLinks.Remove(canceledVer);
      if (this.changedLinks.ContainsKey(-canceledVer))
        this.changedLinks.Remove(-canceledVer);
      if (this.deletedLinks.ContainsKey((object) canceledVer))
        this.deletedLinks.Remove((object) canceledVer);
      if (this.deletedLinks.ContainsKey((object) -canceledVer))
        this.deletedLinks.Remove((object) -canceledVer);
    }
  }

  private void RemoveChangesForCanceledVersions()
  {
    List<ECOEditorForm.ItemToDelete> itemToDeleteList = new List<ECOEditorForm.ItemToDelete>();
    int num = this.Document.Modified ? 1 : 0;
    this.eco.ecoMainTable.UniteTable();
    if (num == 0)
      this.Document.Modified = false;
    TableData dataOwner;
    int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner);
    while (dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count)
    {
      if (dataOwner.Nodes[dataPositionInFlow] is TableData node)
      {
        bool flag = false;
        string attributeValue = node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
        if (!(attributeValue == ""))
        {
          List<long> idList = this.eco._GetIdList(attributeValue);
          for (int index = idList.Count - 1; index >= 0; --index)
          {
            if (this.eco.ObjIdIndex(idList[index]) < 0)
            {
              idList.RemoveAt(index);
              flag = true;
            }
          }
          if (idList.Count == 0)
            itemToDeleteList.Add(new ECOEditorForm.ItemToDelete(dataPositionInFlow, dataOwner));
          else if (flag)
            this.eco._SetIdList((RectangleElement) node, idList);
        }
        else
          continue;
      }
      dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner);
    }
    for (int index = itemToDeleteList.Count - 1; index >= 0; --index)
    {
      ECOEditorForm.ItemToDelete itemToDelete = itemToDeleteList[index];
      itemToDelete.tdata.RemoveChildNodeAt(itemToDelete.index, false, false);
    }
    if (itemToDeleteList.Count <= 0)
      return;
    this.Document.UpdateLayout(0, true, true);
  }

  private bool NeedDeleteVersion(long objID)
  {
    string str = "";
    long num1 = 0;
    long num2 = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objID, false) ?? sessionKeeper.Session.GetObject(-objID, false);
      if (dbObject == null)
        return false;
      if (dbObject.VersionID > 0)
      {
        if (dbObject.OwnerID == sessionKeeper.Session.UserID)
        {
          if (dbObject.CheckoutBy != 0L)
          {
            if (dbObject.CheckoutBy != sessionKeeper.Session.UserID)
              goto label_11;
          }
          num2 = (long) dbObject.VersionID;
          num1 = dbObject.ObjectID;
          str = dbObject.Caption;
        }
      }
    }
label_11:
    return num2 != 0L && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_46"), (object) Convert.ToString(num2), (object) str, (object) Convert.ToString(num1)), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.YesNo) == DialogResult.Yes;
  }

  private bool UpdateMultiChangeHeader(
    TableData change,
    List<long> idList,
    bool updateLayout = true,
    bool updateDesignation = true,
    bool forceUpdate = false)
  {
    if (idList == null)
      idList = this.eco._GetIdList(Convert.ToString(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true)));
    bool flag1 = forceUpdate;
    bool flag2 = false;
    ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) change);
    bool flag3 = change.GetAttributeValue("AlwaysTable", true) == "yes";
    List<string> l = this.eco.ChangeNoList(idList);
    if (ecoGoal == ECOGoal.Annul || ecoGoal == ECOGoal.Creation || this.eco.revType == RevType.PI || this.eco.revType == RevType.PR || Intermech.ECO.Client.ECO.SameChangeNums(l) && !flag3)
    {
      ECOEditorForm.SwitchToOneRowHeader(change);
      DocumentTreeNode templateRecursive1 = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader2);
      if (templateRecursive1 != null)
      {
        change.RemoveChildNode(templateRecursive1, false, false);
        if (ecoGoal == ECOGoal.Replace)
          this.UpdateSpecText((TableElement) change);
        flag1 = true;
      }
      if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altPrimaryHeader) == null && change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader) == null)
      {
        if (!(change.Template.FindNode(Intermech.ECO.Client.ECO.fldChangeHeader) is TableElement node))
        {
          string format = LocalizationHolder.rm.GetString("ECO.Client_457");
          string str1 = $"'{change.Id}' [{change.Name}]";
          string str2 = $"'{Intermech.ECO.Client.ECO.fldChangeHeader}'";
          string str3 = str1;
          string str4 = str2;
          throw new Exception(string.Format(format, (object) str3, (object) str4));
        }
        DocumentTreeNode child = (DocumentTreeNode) (node.CloneFromTemplate() as TableElement);
        if (!change.InsertChildNode(0, child, false, true, false, false, false))
          throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_183"));
        flag1 = true;
        flag2 = true;
      }
      if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive2)
      {
        if ((ecoGoal == ECOGoal.Annul || ecoGoal == ECOGoal.Creation) && this.eco.revType != RevType.PI && this.eco.revType != RevType.PR)
        {
          if (templateRecursive2.Text != Intermech.ECO.Client.ECO.noChangeNumber)
          {
            templateRecursive2.AssignText(Intermech.ECO.Client.ECO.noChangeNumber, false, false, false);
            templateRecursive2.ReadOnly = true;
          }
        }
        else if (this.eco.revType != RevType.PI && this.eco.revType != RevType.PR)
        {
          templateRecursive2.ReadOnly = false;
          if (flag1)
          {
            templateRecursive2.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
            templateRecursive2.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
          }
          if (l.Count > 0 && templateRecursive2.Text != l[0])
          {
            bool lockNumChange = this.lockNumChange;
            this.lockNumChange = true;
            try
            {
              templateRecursive2.AssignText(l[0], true, false, false);
            }
            finally
            {
              this.lockNumChange = lockNumChange;
            }
            flag1 = true;
          }
        }
        this.eco._SetIdList((RectangleElement) templateRecursive2, idList);
      }
      if (updateDesignation | flag2 && change.Tag != DBNull.Value)
      {
        string str = this.eco.DesignListStr(idList);
        bool writeDesOnReplace = this.plugin.eps.Current.WriteDesOnReplace;
        if (ecoGoal == ECOGoal.Annul || ecoGoal == ECOGoal.Litera || !writeDesOnReplace && ecoGoal == ECOGoal.Replace)
          str = "";
        if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive3 && templateRecursive3.Text != str)
        {
          templateRecursive3.AssignText(str, true, true, false);
          flag1 = true;
        }
      }
      if (flag1 & updateLayout)
        this.Document.UpdateLayout(0, true, true);
    }
    else
    {
      TableData firstTable = change.FindFirstTable();
      this.eco.ecoMainTable.UniteTable();
      change = (TableData) this.eco.ecoMainTable.FindNode(firstTable.Id);
      flag1 = true;
      ECOEditorForm.SwitchToTableHeader(change);
      DocumentTreeNode templateRecursive4 = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader);
      if (templateRecursive4 != null)
      {
        change.RemoveChildNode(templateRecursive4, false, false);
        flag1 = true;
      }
      DocumentTreeNode child1 = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader2);
      if (child1 == null)
      {
        child1 = (DocumentTreeNode) ((change.Template.FindNode(Intermech.ECO.Client.ECO.fldChangeHeader2) as TableElement).CloneFromTemplate() as TableElement);
        if (!change.InsertChildNode(0, child1, false, true, false, false, false))
          throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_184"));
        flag1 = true;
      }
      if (!(child1.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldDesign) is TableElement templateRecursive5))
        throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_185"));
      templateRecursive5.UniteTable();
      TableElement templateRecursive6 = templateRecursive5.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldString) as TableElement;
      int num = templateRecursive5.Nodes.Count - 3;
      if (num < idList.Count)
      {
        for (int index = 0; index < idList.Count - num; ++index)
        {
          TableElement child2 = templateRecursive6.Template.CloneFromTemplate() as TableElement;
          templateRecursive5.InsertChildNode(templateRecursive6.Index + 1, (DocumentTreeNode) child2, false, true, false, false, false);
        }
        flag1 = true;
      }
      if (num > idList.Count)
      {
        for (int index = 0; index < num - idList.Count; ++index)
          templateRecursive5.RemoveChildNodeAt(templateRecursive6.Index + 1, false, false);
        flag1 = true;
      }
      for (int index1 = 0; index1 < idList.Count; ++index1)
      {
        TableElement node = templateRecursive5.Nodes[templateRecursive6.Index + index1] as TableElement;
        TextData firstNodeByName1 = node.FindFirstNodeByName(Intermech.ECO.Client.ECO.fldChangeNumber) as TextData;
        TextData firstNodeByName2 = node.FindFirstNodeByName(Intermech.ECO.Client.ECO.fldOnlyDesign) as TextData;
        if (firstNodeByName1 != null && !firstNodeByName1.Text.Equals(l[index1]))
        {
          bool lockNumChange = this.lockNumChange;
          this.lockNumChange = true;
          try
          {
            firstNodeByName1.AssignText(l[index1], true, false, false);
            firstNodeByName1.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
            firstNodeByName1.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
            if (this.eco.revType != RevType.PI)
            {
              if (this.eco.revType != RevType.PR)
                goto label_58;
            }
            firstNodeByName1.ReadOnly = true;
          }
          finally
          {
            this.lockNumChange = lockNumChange;
          }
label_58:
          flag1 = true;
        }
        this.eco._SetIdList((RectangleElement) firstNodeByName1, new List<long>()
        {
          idList[index1]
        });
        int index2 = this.eco.ObjIdIndex(idList[index1]);
        if (index2 >= 0)
        {
          string design = this.eco.objLinks[index2].design;
          if (!firstNodeByName2.Text.Equals(design))
          {
            firstNodeByName2.AssignText(design, true, false, false);
            flag1 = true;
          }
        }
      }
      DocumentTreeNode templateRecursive7 = templateRecursive5.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldColCaption);
      if (templateRecursive7 == null || !(templateRecursive7 is TextData))
        throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_186"));
      switch (ecoGoal)
      {
        case ECOGoal.Change:
          ((TextData) templateRecursive7).AssignText(Intermech.ECO.Client.ECO.fldOnlyDesign, false, false, false);
          break;
        case ECOGoal.Litera:
          ((TextData) templateRecursive7).AssignText(Intermech.ECO.Client.ECO.strLitera, true, false, false);
          break;
        case ECOGoal.Replace:
          ((TextData) templateRecursive7).Text = Intermech.ECO.Client.ECO.strReplaceDocs;
          bool flag4 = this.RemoveSpecText((TableElement) change) | flag1;
          flag1 = ECOPlugin.RemoveDefaultText((TableElement) change) | flag4;
          break;
      }
      if (flag1 & updateLayout)
        this.Document.UpdateLayout(0, true, true);
    }
    return flag1;
  }

  public bool UpdateAllMultiHeaders()
  {
    bool flag = false;
    this.eco.ecoMainTable.UniteTable();
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
      string attributeValue = node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
      if (attributeValue != "")
      {
        List<long> idList = this.eco._GetIdList(attributeValue);
        flag = this.UpdateMultiChangeHeader(node, idList, false) | flag;
        if (node.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader2) == null)
        {
          List<string> stringList = this.eco.ChangeNoList(idList);
          if (stringList.Count > 0)
          {
            string str = stringList[0];
            DocumentTreeNode templateRecursive1 = node.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader);
            if (templateRecursive1 != null)
            {
              ECOEditorForm.SwitchToOneRowHeader(node);
              if (templateRecursive1.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive2)
              {
                if (templateRecursive2.Text != str)
                {
                  templateRecursive2.AssignText(str, false, false, false);
                  flag = true;
                }
                templateRecursive2.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
                templateRecursive2.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
                templateRecursive2.ReadOnly = false;
              }
            }
          }
        }
      }
    }
    return flag;
  }

  private void externalDocDesignChanged(object sender, TextChanged_EventArgs e)
  {
    if (this.lockChange)
      return;
    this.UndoManager.BeginCreateMultyUndo("Изменение обозначения");
    try
    {
      if (sender != null && sender is TextData)
        ((DocumentTreeNode) sender).SetAttributeValue(Intermech.ECO.Client.ECO.textAttr, (sender as TextData).Text);
      this.UpdateDocDesign();
      this.textChanged = true;
      this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  private void num_TextValidating(object sender, TextValidating_EventArgs e)
  {
    if (e.Text == Intermech.ECO.Client.ECO.noChangeNumber || e.Text == "-" || this.lockNumChange)
      return;
    string text = e.Text;
    List<long> idList = this.eco._GetIdList((sender as TextData).GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in idList)
      {
        if (!this.IsChangeNumUnique(sessionKeeper.Session, objId, text))
        {
          string message = string.Format(LocalizationHolder.rm.GetString("ECO.Client_235"), (object) objId);
          e.Cancel = true;
          throw new Exception(message);
        }
      }
    }
    if (e.Text.Contains(" "))
      e.Text = e.Text.Replace(" ", "");
    if (e.Text.Contains("\n"))
      e.Text = e.Text.Replace("\n", "");
    if (!e.Text.Contains("\r"))
      return;
    e.Text = e.Text.Replace("\r", "");
  }

  private void num_TextChanged(object sender, TextChanged_EventArgs e)
  {
    if (this.lockNumChange)
      return;
    this.lockNumChange = true;
    try
    {
      TextData textData = sender as TextData;
      foreach (long id in this.eco._GetIdList(textData.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true)))
      {
        this.ChangePendingLink(id, e.NewText);
        int index = this.eco.ObjIdIndex(id);
        if (index >= 0)
          this.eco.objLinks[index].verStr = e.NewText;
      }
      DocumentTreeNode change = (DocumentTreeNode) textData;
      while (!(change.Template.Id == Intermech.ECO.Client.ECO.fldChange))
      {
        change = change.Parent;
        if (change == null)
          break;
      }
      if (change == null)
        return;
      List<long> idList = this.eco._GetIdList((change as TableData).GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      if (this.UndoManager.Actions.Count > 0)
        this.UndoManager.BeginCreateMultyUndo("Смена номера изменения", new List<IUndoAction>()
        {
          this.UndoManager.Actions.Last<IUndoAction>()
        });
      else
        this.UndoManager.BeginCreateMultyUndo("Смена номера изменения");
      try
      {
        if (!this.UpdateMultiChangeHeader(change as TableData, idList, false, false) || this.UpdateDocDesign())
          return;
        this.Document.UpdateLayout(0, true, true);
      }
      finally
      {
        this.UndoManager.EndCreateMultyUndo();
      }
    }
    finally
    {
      this.lockNumChange = false;
    }
  }

  private void AttachToECOCommand()
  {
  }

  private void AttachToECOGroupCommand()
  {
  }

  private List<PendingLink> AddPendingLink(
    long objID,
    ECOGoal goal,
    int lcStepId,
    HidingType hType = HidingType.Disabled,
    List<long> allObjIDs = null,
    Hashtable SynchroTab = null)
  {
    List<PendingLink> newLinks = (List<PendingLink>) null;
    if (this.addedLinks.ContainsKey((object) objID))
      return newLinks;
    long replaceObjId = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService)
        customService.StartTransaction();
      try
      {
        List<long> longList = new List<long>();
        bool flag1 = false;
        try
        {
          if (sessionKeeper.Session.GetObjectActualCopy(objID, false) != null)
          {
            IUserSession session = sessionKeeper.Session;
            IDBObject objectActualCopy1 = session.GetObjectActualCopy(objID, false);
            bool needDelete = false;
            string cNo = "";
            List<long> auxObjects = (List<long>) null;
            if (this.GetVerParmsOfArchiveECO(session, objectActualCopy1.ObjectID, out needDelete, out cNo, out auxObjects) != 0L)
            {
              PendingLink pl = new PendingLink(objectActualCopy1.ObjectID, cNo, goal, lcStepId);
              newLinks = new List<PendingLink>();
              newLinks.Add(pl);
              pl.needDelete = needDelete;
              if (auxObjects != null && auxObjects.Count > 0)
              {
                if (pl.auxObjects == null)
                  pl.auxObjects = new List<ObjInfo>();
                foreach (long vId in auxObjects)
                  pl.auxObjects.Add(new ObjInfo(vId, session));
              }
              this.AddToAddedLinks(pl);
              customService?.Commit();
              return newLinks;
            }
            IDBAttribute attributeById = objectActualCopy1.GetAttributeByID(RevHelper.idAttrDesign);
            if (attributeById != null)
            {
              string asString = attributeById.AsString;
            }
            bool flag2 = objectActualCopy1.ModificationID == this.eco.linkedContextNo && objectActualCopy1.VersionID > 0;
            if (goal == ECOGoal.Annul)
              flag2 = false;
            if (!flag2)
            {
              IDBObject objectActualCopy2 = session.GetObjectActualCopy(objID, false);
              flag2 = this.PerformCreateVersion(session, objID, objectActualCopy2, goal, lcStepId, out newLinks);
              if (!flag2 && allObjIDs != null)
                newLinks.ForEach((Action<PendingLink>) (pl => pl.needDelete = true));
              if (flag2 && SynchroTab != null)
                this.ProcessSynchroTab(session, newLinks, SynchroTab);
              flag1 = flag2;
            }
            IDBObject objectActualCopy3 = sessionKeeper.Session.GetObjectActualCopy(objID, false);
            if (newLinks == null)
              newLinks = new List<PendingLink>();
            if (newLinks.Count == 0)
              newLinks.Add(new PendingLink(goal, lcStepId)
              {
                verID = objectActualCopy3.ObjectID
              });
            for (int index = 0; index < newLinks.Count; ++index)
            {
              PendingLink pl = newLinks[index];
              pl.InitVars(objectActualCopy3);
              if (hType != HidingType.Disabled)
              {
                pl.hideType = hType;
                pl.needDelete = false;
              }
              if (!flag2)
                pl.SetDesign(objectActualCopy3);
              this.AddToAddedLinks(pl);
            }
          }
          else
          {
            newLinks = new List<PendingLink>();
            replaceObjId = -objID;
            IDBObject dbObject = sessionKeeper.Session.GetObject(-objID, false);
            if (dbObject != null)
            {
              string des = "";
              IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
              if (attributeById != null)
                des = attributeById.AsString;
              string aver = "";
              PendingLink pl = new PendingLink(-objID, aver, goal, des);
              pl.stepID = lcStepId;
              pl.hideType = hType;
              pl.needDelete = hType == HidingType.Disabled;
              newLinks.Add(pl);
              this.AddToAddedLinks(pl);
            }
          }
        }
        finally
        {
          if (this.deletedLinks.ContainsKey((object) objID))
            this.deletedLinks.Remove((object) objID);
          if (newLinks != null)
          {
            HashSet<long> longSet = new HashSet<long>();
            List<ObjInfo> objInfoList = new List<ObjInfo>();
            foreach (PendingLink pendingLink in newLinks)
            {
              if (!longSet.Contains(Math.Abs(pendingLink.verID)))
              {
                objInfoList.Add(new ObjInfo(pendingLink.verID, sessionKeeper.Session));
                longSet.Add(Math.Abs(pendingLink.verID));
              }
            }
            if (allObjIDs != null)
            {
              foreach (long allObjId in allObjIDs)
              {
                if (!longSet.Contains(Math.Abs(allObjId)))
                {
                  objInfoList.Add(new ObjInfo(allObjId, sessionKeeper.Session));
                  longSet.Add(Math.Abs(allObjId));
                }
              }
            }
            foreach (PendingLink pl in newLinks)
            {
              if (objInfoList.Count > 0)
              {
                if (pl.auxObjects == null)
                  pl.auxObjects = new List<ObjInfo>();
                longSet.Clear();
                longSet.Add(Math.Abs(pl.verID));
                foreach (ObjInfo auxObject in pl.auxObjects)
                  longSet.Add(Math.Abs(auxObject.verId));
                if (SynchroTab == null)
                {
                  foreach (ObjInfo objInfo in objInfoList)
                  {
                    if (!longSet.Contains(objInfo.verId))
                      pl.auxObjects.Add(new ObjInfo(objInfo.verId, sessionKeeper.Session));
                  }
                }
              }
              if (this._DoCreateRelation(sessionKeeper.Session, pl, replaceObjId))
                longList.Add(pl.relId);
            }
          }
        }
        customService?.Commit();
        if (flag1 && newLinks != null && newLinks.Count > 0 && ServicesManager.GetService(typeof (INotificationService)) is INotificationService service1)
        {
          List<long> objectIDs = new List<long>();
          foreach (PendingLink pendingLink in newLinks)
            objectIDs.Add(pendingLink.verID);
          service1.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objectIDs));
        }
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2)
        {
          foreach (long relationID in longList)
            service2.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", relationID, this.ecoID, Intermech.ECO.Client.ECO.RevTypeToObjType(this.eco.revType), RevHelper.idLinkRevision, NavigatorRelationCommand.CreateIn));
        }
      }
      catch
      {
        customService?.Rollback();
        throw;
      }
    }
    return newLinks;
  }

  private void ProcessSynchroTab(IUserSession ius, List<PendingLink> pList, Hashtable synchroTab)
  {
    HashSet<long> longSet = new HashSet<long>();
    if (synchroTab != null)
    {
      foreach (long key in (IEnumerable) synchroTab.Keys)
      {
        longSet.Add(key);
        List<long> other = (List<long>) synchroTab[(object) key];
        longSet.UnionWith((IEnumerable<long>) other);
      }
    }
    Dictionary<long, long> prevs = new Dictionary<long, long>();
    foreach (PendingLink p in pList)
    {
      long prevVersion1 = this._GetPrevVersion(ius, prevs, p.verID);
      if (!longSet.Contains(prevVersion1))
        p.ecoGoal = ECOGoal.NoGoal;
      if (p.auxObjects != null)
      {
        for (int index = p.auxObjects.Count - 1; index >= 0; --index)
        {
          long prevVersion2 = this._GetPrevVersion(ius, prevs, p.auxObjects[index].verId);
          if (!longSet.Contains(prevVersion2))
            p.auxObjects.RemoveAt(index);
        }
      }
    }
  }

  private long _GetPrevVersion(IUserSession ius, Dictionary<long, long> prevs, long newVerId)
  {
    long prevVersion = 0;
    if (prevs.TryGetValue(newVerId, out prevVersion))
      return prevVersion;
    IDBObject dbObject = ius.GetObject(newVerId, false);
    long parentVersionId = dbObject == null ? 0L : dbObject.ParentVersionID;
    prevs.Add(newVerId, parentVersionId);
    return parentVersionId;
  }

  private void DeletePendingLink(long objID, int index)
  {
    if (this.deletedLinks.ContainsKey((object) objID) || this.deletedLinks.ContainsKey((object) -objID))
      return;
    if (this.addedLinks.ContainsKey((object) objID))
      this.addedLinks.Remove((object) objID);
    if (this.addedLinks.ContainsKey((object) -objID))
      this.addedLinks.Remove((object) -objID);
    if (this.changedLinks.ContainsKey(objID))
      this.changedLinks.Remove(objID);
    if (this.changedLinks.ContainsKey(-objID))
      this.changedLinks.Remove(-objID);
    PendingLink pendingLink1 = (PendingLink) null;
    if (index >= 0)
    {
      pendingLink1 = (PendingLink) this.eco.objLinks[index].Clone();
    }
    else
    {
      PendingLink pendingLink2 = new PendingLink(objID, "", ECOGoal.Change);
    }
    this.deletedLinks.Add((object) objID, (object) pendingLink1);
    if (!this.eco.newVers.Contains(Math.Abs(objID)) || pendingLink1.auxObjects == null || pendingLink1.auxObjects.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation rel = (IDBRelation) null;
      try
      {
        rel = sessionKeeper.Session.GetRelation(this.ecoID, Math.Abs(objID), RevHelper.idLinkRevision, true);
      }
      catch (ObjectNotFoundException ex)
      {
      }
      if (rel == null)
        return;
      this.UpdateAuxLinks(rel, ref pendingLink1.auxObjects);
      using (new ECOLinkDeleter(sessionKeeper.Session, rel.RelationID))
        rel.Delete(0L);
    }
  }

  private void ChangePendingLink(long objID, string newChangeNo)
  {
    if (this.addedLinks.ContainsKey((object) objID))
    {
      (this.addedLinks[(object) objID] as PendingLink).verStr = newChangeNo;
    }
    else
    {
      PendingLink pendingLink = this.eco.FindPendingLink(objID);
      if (pendingLink == null)
        return;
      if (this.changedLinks.ContainsKey(objID))
        this.changedLinks[objID] = pendingLink;
      else
        this.changedLinks.Add(objID, pendingLink);
    }
  }

  private void AddToAddedLinks(PendingLink pl)
  {
    long key = Math.Abs(pl.verID);
    if (!this.addedLinks.ContainsKey((object) key))
      this.addedLinks.Add((object) key, (object) pl);
    if (this.deletedLinks.Contains((object) pl.verID))
      this.deletedLinks.Remove((object) pl.verID);
    if (!this.deletedLinks.Contains((object) -pl.verID))
      return;
    this.deletedLinks.Remove((object) -pl.verID);
  }

  private List<PendingLink> CopyExistingLink(long objId, ECOGoal newGoal)
  {
    List<PendingLink> pendingLinkList = new List<PendingLink>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[8]
      {
        (object) -21,
        (object) -20,
        (object) RevHelper.idAttrChangeNo,
        (object) RevHelper.idAttrIncludeGoal,
        (object) RevHelper.idAttrHiding,
        (object) RevHelper.idAttrDelWhenExcluded,
        (object) RevHelper.idAttrFutureLC,
        (object) RevHelper.idAttrMainObjectGuid
      }), objId);
      if (dataTable.Rows.Count != 0)
      {
        DataRow row = dataTable.Rows[0];
        if (Math.Abs(Convert.ToInt64(row[0])) == Math.Abs(this.eco.EcoObjectID))
          return pendingLinkList;
        long int64_1 = Convert.ToInt64(row[1]);
        string aver = Convert.ToString(row[2]);
        Convert.ToInt32(row[3]);
        HidingType int32_1 = (HidingType) Convert.ToInt32(row[4]);
        bool boolean = Convert.ToBoolean(row[5]);
        int int32_2 = Convert.ToInt32(row[6]);
        string str = Convert.ToString(row[7]);
        Guid guid = GuidHelper.IsGuid(str) ? new Guid(str) : Guid.Empty;
        IDBRelation relation = sessionKeeper.Session.GetRelation(int64_1, false);
        List<ObjInfo> objInfoList = new List<ObjInfo>();
        if (relation != null)
        {
          object[] valuesById = relation.GetValuesByID(RevHelper.idAttrAuxLinks, false);
          if (valuesById != null)
          {
            foreach (object obj in valuesById)
            {
              if (obj != DBNull.Value)
              {
                long int64_2 = Convert.ToInt64(obj);
                objInfoList.Add(new ObjInfo(int64_2));
              }
            }
          }
        }
        IDBObject dbObject = sessionKeeper.Session.GetObject(objId, false);
        Guid vGuid = Guid.Empty;
        string des = "";
        if (dbObject != null)
        {
          vGuid = dbObject.GUID;
          object[] valuesById = dbObject.GetValuesByID(RevHelper.idAttrDesign, false);
          if (valuesById != null && valuesById.Length != 0)
            des = Convert.ToString(valuesById[0]);
        }
        PendingLink pl = new PendingLink(objId, vGuid, aver, newGoal, des);
        pl.hideType = int32_1;
        pl.needDelete = boolean;
        pl.stepID = int32_2;
        pl.mainGuid = guid;
        pl.auxObjects = objInfoList;
        pendingLinkList.Add(pl);
        this._DoCreateRelation(sessionKeeper.Session, pl);
      }
    }
    return pendingLinkList;
  }

  private bool RemoveSpecText(TableElement change)
  {
    bool flag = false;
    DocumentTreeNode templateRecursive;
    do
    {
      templateRecursive = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecText);
      if (templateRecursive != null)
      {
        int index = templateRecursive.Index;
        templateRecursive.Parent.RemoveChildNode(templateRecursive, false, false);
        TableElement child = (change.Template.FindNode(Intermech.ECO.Client.ECO.fldVar1) as TableElement).CloneFromTemplate() as TableElement;
        change.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false, false);
        flag = true;
      }
    }
    while (templateRecursive != null);
    return flag;
  }

  private bool AddSpecText(TableElement change)
  {
    if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecText) != null)
      return false;
    DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator((TableData) change);
    while (dataNodesEnumerator.MoveNext())
    {
      if (dataNodesEnumerator.Current != null && dataNodesEnumerator.Current.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecText) != null)
        return false;
    }
    if (!(change.Template.FindNode(Intermech.ECO.Client.ECO.idSpecText) is TableElement node))
      throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_49"));
    TableElement child = node.CloneFromTemplate() as TableElement;
    int index = change.NodesCount == 0 ? 0 : 1;
    change.InsertChildNode(index, (DocumentTreeNode) child, false, false, false, false, false);
    if (change.Nodes.Count > 2 && change.Nodes[2].Template.Id == Intermech.ECO.Client.ECO.fldVar1 && (change.Nodes[2].Nodes[0] as TextData).Text == "")
      change.RemoveChildNodeAt(2, false, false);
    return true;
  }

  private bool HasAlternativeHeader()
  {
    return this.eco.ecoMainTable.Template.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altPrimaryHeader) != null;
  }

  private bool UpdateAlternativeText(TableElement change)
  {
    bool flag = false;
    ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) change);
    switch (ecoGoal)
    {
      case ECOGoal.NoGoal:
        return false;
      case ECOGoal.Annul:
      case ECOGoal.Replace:
      case ECOGoal.Creation:
        DocumentTreeNode child1 = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altPrimaryHeader);
        if (child1 == null)
        {
          DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator((TableData) change);
          while (dataNodesEnumerator.MoveNext())
          {
            if (dataNodesEnumerator.Current != null)
            {
              child1 = dataNodesEnumerator.Current.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecText);
              if (child1 != null)
                break;
            }
          }
          if (child1 == null && change.Template.FindNode(Intermech.ECO.Client.ECO.altPrimaryHeader) is TableElement node)
          {
            child1 = (DocumentTreeNode) (node.CloneFromTemplate() as TableElement);
            change.InsertChildNode(1, child1, false, true, false, false, false);
            if (change.Nodes.Count > 2 && change.Nodes[2].Template.Id == Intermech.ECO.Client.ECO.fldVar1 && (change.Nodes[2].Nodes[0] as TextData).Text == "")
              change.RemoveChildNodeAt(2, false, false);
            if (change.Nodes.Count > 1 && change.Nodes[0].Template.Id == Intermech.ECO.Client.ECO.fldChangeHeader)
              change.RemoveChildNodeAt(0, false, false);
            flag = true;
          }
        }
        if (child1 == null)
          return false;
        if (!(child1.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altTable) is TableElement templateRecursive1))
          return flag;
        int num = 0;
        DataNodesEnumerator dataNodesEnumerator1 = new DataNodesEnumerator((TableData) templateRecursive1);
        while (dataNodesEnumerator1.MoveNext())
          ++num;
        List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
        if (idList.Count + 3 != num)
          templateRecursive1.UniteTable();
        if (idList.Count + 3 > num)
        {
          TableElement node = child1.Template.FindNode(Intermech.ECO.Client.ECO.altString) as TableElement;
          for (; idList.Count + 3 > num; ++num)
          {
            TableElement child2 = node.CloneFromTemplate() as TableElement;
            templateRecursive1.InsertChildNode(templateRecursive1.NodesCount - 1, (DocumentTreeNode) child2, false, true, false, false, false);
            flag = true;
          }
        }
        for (; idList.Count + 3 < num; --num)
        {
          templateRecursive1.RemoveChildNodeAt(templateRecursive1.NodesCount - 2, false, false);
          flag = true;
        }
        List<string> designList = this.eco.GetDesignList(idList);
        dataNodesEnumerator1.Reset();
        dataNodesEnumerator1.MoveNext();
        dataNodesEnumerator1.MoveNext();
        for (int index = 0; index < designList.Count && dataNodesEnumerator1.MoveNext(); ++index)
        {
          if ((dataNodesEnumerator1.Current as TableElement).FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altDesignation2) is TextData templateRecursive2 && templateRecursive2.Text != designList[index])
          {
            templateRecursive2.AssignText(designList[index], false, false, false);
            flag = true;
          }
        }
        if (templateRecursive1.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altHeaderCaption) is TextData templateRecursive3)
        {
          string altDocuments = Intermech.ECO.Client.ECO.altDocuments;
          switch (ecoGoal)
          {
            case ECOGoal.Annul:
              altDocuments += Intermech.ECO.Client.ECO.altAnnul;
              break;
            case ECOGoal.Replace:
              altDocuments += Intermech.ECO.Client.ECO.altReplace;
              break;
            case ECOGoal.Creation:
              altDocuments += Intermech.ECO.Client.ECO.altCreate;
              break;
          }
          if (templateRecursive3.Text != altDocuments)
          {
            templateRecursive3.AssignText(altDocuments, false, false, false);
            flag = true;
          }
        }
        return flag;
      default:
        return false;
    }
  }

  private bool UpdateSpecText(TableElement change)
  {
    ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) change);
    if (change.Template.FindNode(Intermech.ECO.Client.ECO.altPrimaryHeader) != null)
    {
      switch (ecoGoal)
      {
        case ECOGoal.Annul:
          return this.UpdateAlternativeText(change);
        case ECOGoal.Replace:
          if (ecoGoal != ECOGoal.Creation)
            break;
          goto case ECOGoal.Annul;
      }
    }
    if (ecoGoal == ECOGoal.Change || ecoGoal == ECOGoal.NoGoal || ecoGoal == ECOGoal.Creation)
      return false;
    List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    List<string> l = this.eco.ChangeNoList(idList);
    if (ecoGoal == ECOGoal.Replace && !Intermech.ECO.Client.ECO.SameChangeNums(l))
      return false;
    bool flag = this.AddSpecText(change);
    string str1 = this.eco.DesignListStr(idList);
    switch (ecoGoal)
    {
      case ECOGoal.Annul:
        str1 = idList.Count != 1 ? str1 + LocalizationHolder.rm.GetString("ECO.Client_302") : str1 + LocalizationHolder.rm.GetString("ECO.Client_51");
        if (!(change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive1))
          throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_53"));
        if (templateRecursive1.Text != Intermech.ECO.Client.ECO.noChangeNumber)
        {
          templateRecursive1.AssignText(Intermech.ECO.Client.ECO.noChangeNumber, false, false, false);
          flag = true;
        }
        if (!templateRecursive1.ReadOnly)
        {
          templateRecursive1.ReadOnly = true;
          break;
        }
        break;
      case ECOGoal.Litera:
        List<long> longList = new List<long>();
        List<string> stringList = new List<string>();
        foreach (long objId in idList)
        {
          PendingLink pendingLink = this.eco.FindPendingLink(objId);
          if (pendingLink != null && pendingLink.auxObjects != null)
          {
            foreach (ObjInfo auxObject in pendingLink.auxObjects)
            {
              if (!idList.Contains(auxObject.verId) && !idList.Contains(-auxObject.verId) && !longList.Contains(auxObject.verId) && !longList.Contains(-auxObject.verId))
              {
                longList.Add(auxObject.verId);
                stringList.Add(auxObject.design);
              }
            }
          }
        }
        string str2;
        if (idList.Count == 1 && longList.Count <= 1)
        {
          str2 = LocalizationHolder.rm.GetString("ECO.Client_54") + str1 + LocalizationHolder.rm.GetString("ECO.Client_56");
        }
        else
        {
          str2 = LocalizationHolder.rm.GetString("ECO.Client_54") + str1 + LocalizationHolder.rm.GetString("ECO.Client_57");
          if (stringList.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(stringList[0]);
            for (int index = 1; index < stringList.Count; ++index)
            {
              stringBuilder.Append(", ");
              stringBuilder.Append(stringList[index]);
            }
            str2 += stringBuilder.ToString();
          }
        }
        str1 = $"{str2}{LocalizationHolder.rm.GetString("ECO.Client_58")}{this.eco.litera}\"";
        break;
      case ECOGoal.Replace:
        str1 = idList.Count != 1 ? str1 + LocalizationHolder.rm.GetString("ECO.Client_303") : str1 + LocalizationHolder.rm.GetString("ECO.Client_60");
        break;
    }
    if (!(change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecTextFld) is TextData templateRecursive2))
    {
      DataNodesEnumerator dataNodesEnumerator = new DataNodesEnumerator((TableData) change);
      while (dataNodesEnumerator.MoveNext())
      {
        if (dataNodesEnumerator.Current != null)
        {
          templateRecursive2 = (TextData) dataNodesEnumerator.Current.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecTextFld);
          if (templateRecursive2 != null)
            return false;
        }
      }
    }
    if (templateRecursive2.Text != str1 && templateRecursive2.ReadOnly)
    {
      templateRecursive2.AssignText(str1, false, false, false);
      flag = true;
    }
    return flag;
  }

  private bool UpdateSpecText()
  {
    bool flag = false;
    try
    {
      foreach (DocumentTreeNode documentTreeNode in (TableData) this.eco.ecoMainTable)
      {
        if (Intermech.ECO.Client.ECO.IsChange(documentTreeNode))
          flag = flag || this.UpdateSpecText((TableElement) documentTreeNode);
      }
    }
    finally
    {
      if (flag)
        this.Document.UpdateLayout(0, false, true);
    }
    return flag;
  }

  public void HideObject(long objId, TableElement change)
  {
    List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    if (!idList.Contains(objId))
      return;
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      idList.Remove(objId);
      PendingLink pendingLink = this.eco.FindPendingLink(objId);
      if (pendingLink != null)
      {
        if (pendingLink.hideType == HidingType.Disabled)
          return;
        this._DoHiding(pendingLink);
      }
      string attributeValue = this.eco._SetIdList((RectangleElement) change, idList);
      if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive1)
      {
        if (templateRecursive1.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) != attributeValue)
          templateRecursive1.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
        if (idList.Count == 0)
          templateRecursive1.AssignText("", false, false, false);
      }
      if (idList.Count == 0)
      {
        string id = change.Id;
        this.eco.ecoMainTable.UniteTable();
        change = this.eco.ecoMainTable.FindNode(id, typeof (TableElement)) as TableElement;
        if (change != null)
        {
          if (change.FindFirstNodeFromTemplate_Recursive("IT1") is TextData templateRecursive3 && templateRecursive3.Text == "")
          {
            change.Remove(false, false);
            change = (TableElement) null;
          }
          else
          {
            DocumentTreeNode templateRecursive2 = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecText);
            if (templateRecursive2 != null)
              change.RemoveChildNode(templateRecursive2, false, false);
            if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldVar1) == null && this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar1) is TableData node)
            {
              TableElement child = node.CloneFromTemplate() as TableElement;
              change.AddChildNode((DocumentTreeNode) child, false, false, false, false);
            }
          }
        }
      }
      if (change != null)
      {
        this.UpdateMultiChangeHeader((TableData) change, idList);
        this.UpdateSpecText(change);
      }
      this.UpdateDocDesign();
      this.Document.UpdateLayout(0, true, true);
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  private void _DoHiding(PendingLink pl)
  {
    pl.hideType = HidingType.Hidden;
    this.eco.hiddenLinks.Add(pl);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._SetHidingStatus(sessionKeeper.Session, pl);
      if (!(sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer customService))
        return;
      long revRelation = RevHelper.GetRevRelation(this.eco.EcoObjectID, pl.verID);
      if (revRelation == 0L)
        return;
      customService.RemoveChangeNums(sessionKeeper.Session.SessionGUID, revRelation, pl.verID);
      pl.verStr = "";
    }
  }

  public void UnhideObject(long objId, TableElement change)
  {
    bool flag = change == null;
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      if (change == null)
        change = this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
      List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      if (idList.Contains(objId))
        return;
      idList.Add(objId);
      PendingLink anyLink = this.eco.FindAnyLink(objId);
      if (anyLink != null)
      {
        anyLink.hideType = HidingType.CanBeHidden;
        this.eco.hiddenLinks.Remove(anyLink);
        if (this.eco.ObjIdIndex(anyLink.verID) < 0)
          this.eco.objLinks.Add(anyLink);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          this._SetHidingStatus(session, anyLink);
          IDBObject dbObject = session.GetObject(anyLink.verID, false);
          if (!this.NoChangeNums(anyLink.ecoGoal))
          {
            if (dbObject != null)
            {
              if (this.NeedsChangeNo(dbObject.ObjectType))
              {
                string nchangeNo = this.GetNChangeNo(session, dbObject.ID, objId, anyLink.ecoGoal);
                this.SetChangeNumForRelation(session, objId, nchangeNo);
                this.SetNewChangeNo(session, objId, nchangeNo, anyLink.ecoGoal);
                anyLink.verStr = nchangeNo;
              }
            }
          }
        }
      }
      string attributeValue = this.eco._SetIdList((RectangleElement) change, idList);
      if (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive)
      {
        if (templateRecursive.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) != attributeValue)
          templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
        if (flag)
        {
          templateRecursive.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
          templateRecursive.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
        }
      }
      this.UpdateMultiChangeHeader((TableData) change, idList);
      this.UpdateSpecText(change);
      this.UpdateDocDesign();
      this.Document.UpdateLayout(0, true, true);
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  public void UnhideObjects(HashSet<long> objIds)
  {
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      TableElement tableElement = this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
      List<long> longList = new List<long>();
      foreach (long objId in objIds)
      {
        if (!longList.Contains(objId))
        {
          PendingLink pendingLink = this.eco.FindPendingLink(objId);
          if (pendingLink != null)
          {
            longList.Add(objId);
            pendingLink.hideType = HidingType.CanBeHidden;
            this.eco.hiddenLinks.Remove(pendingLink);
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IUserSession session = sessionKeeper.Session;
              this._SetHidingStatus(session, pendingLink);
              IDBObject dbObject = session.GetObject(pendingLink.verID, false);
              if (!this.NoChangeNums(pendingLink.ecoGoal))
              {
                if (dbObject != null)
                {
                  if (this.NeedsChangeNo(dbObject.ObjectType))
                  {
                    string nchangeNo = this.GetNChangeNo(session, dbObject.ID, objId, pendingLink.ecoGoal);
                    this.SetChangeNumForRelation(session, objId, nchangeNo);
                    this.SetNewChangeNo(session, objId, nchangeNo, pendingLink.ecoGoal);
                    pendingLink.verStr = nchangeNo;
                  }
                }
              }
            }
          }
        }
      }
      string attributeValue = this.eco._SetIdList((RectangleElement) tableElement, longList);
      if (tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive)
      {
        if (templateRecursive.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) != attributeValue)
          templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
        templateRecursive.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
        templateRecursive.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
      }
      this.UpdateMultiChangeHeader((TableData) tableElement, longList);
      this.UpdateSpecText(tableElement);
      this.UpdateDocDesign();
      this.Document.UpdateLayout(0, true, true);
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  public HidingType GetHidingType(long objId)
  {
    PendingLink anyLink = this.eco.FindAnyLink(objId);
    return anyLink == null ? HidingType.Disabled : anyLink.hideType;
  }

  public List<long> GetHidingObjects()
  {
    List<long> hidingObjects = new List<long>();
    foreach (PendingLink hiddenLink in this.eco.hiddenLinks)
      hidingObjects.Add(hiddenLink.verID);
    return hidingObjects;
  }

  internal void SplitChange(TableElement change, long objId)
  {
    string attributeValue1 = change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
    List<long> longList = (List<long>) null;
    if (attributeValue1 != "")
    {
      longList = this.eco._GetIdList(attributeValue1);
      if (longList != null && longList.Count <= 1)
        return;
    }
    string id = change.FindFirstTable().Id;
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) change);
      if (longList != null)
      {
        if (!longList.Remove(objId))
          longList.Remove(-objId);
        this.eco._SetIdList((RectangleElement) change, longList);
        this.UpdateMultiChangeHeader((TableData) change, longList);
        change = (TableElement) this.eco.ecoMainTable.FindNode(id);
        if (ecoGoal == ECOGoal.Annul || ecoGoal == ECOGoal.Litera || ecoGoal == ECOGoal.Replace)
          this.UpdateSpecText(change);
      }
      TableElement tableElement = (this.Document.Template.FindNode(Intermech.ECO.Client.ECO.fldChange) as TableElement).CloneFromTemplate() as TableElement;
      (change.Parent as TableData).InsertChildNode(change.Index + 1, (DocumentTreeNode) tableElement, false, false, false, false, false);
      List<long> IdList = new List<long>() { objId };
      string attributeValue2 = this.eco._SetIdList((RectangleElement) tableElement, IdList);
      PendingLink pendingLink = this.eco.FindPendingLink(objId);
      if (pendingLink != null && tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive1)
        templateRecursive1.AssignText(pendingLink.design, true, true, false);
      this.UpdateDocDesign();
      if (tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive2)
      {
        if (this.eco.revType != RevType.PI && this.eco.revType != RevType.PR && pendingLink != null)
          templateRecursive2.AssignText(pendingLink.verStr, false, false, false);
        templateRecursive2.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
        templateRecursive2.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
        templateRecursive2.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue2);
        if (this.eco.revType == RevType.PI || this.eco.revType == RevType.PR)
          templateRecursive2.ReadOnly = true;
      }
      if (ecoGoal == ECOGoal.Annul || ecoGoal == ECOGoal.Litera || ecoGoal == ECOGoal.Replace)
        this.UpdateSpecText(tableElement);
      this.Document.UpdateLayout(0, false, true);
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  internal int GetObjectCount()
  {
    int objectCount = 0;
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      if (dataOwner.Nodes[dataPositionInFlow] is TableElement node)
      {
        ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) node);
        if (Intermech.ECO.Client.ECO.IsExternal(node))
          ++objectCount;
        else if (ecoGoal != ECOGoal.Litera)
        {
          string attributeValue = node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
          objectCount += attributeValue.Split(',').Length;
        }
      }
    }
    return objectCount;
  }

  public void PasteFromClipboardCommand()
  {
    if (!ECOPlugin.ValidateExcessDocuments(this.GetObjectCount()) || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    List<long> objIDs = new List<long>();
    List<long> noDObjs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long id = 0;
      int objTypeID = -1;
      long owner = 0;
      long version = 0;
      long baseVersion = 0;
      string siteID = string.Empty;
      long modificationID = 0;
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.ecoID, false);
      if (dbObject1 != null)
      {
        id = dbObject1.ID;
        objTypeID = dbObject1.ObjectType;
        owner = dbObject1.OwnerID;
        version = (long) dbObject1.VersionID;
        baseVersion = Convert.ToInt64(dbObject1.IsBaseVersion);
        siteID = dbObject1.SiteID;
        modificationID = dbObject1.ModificationID;
      }
      DBTypedObjectID dbTypedObjectId1 = new DBTypedObjectID(objTypeID, this.ecoID, id, string.Empty, owner, version, baseVersion, siteID, modificationID);
      object dataObject = service.GetDataObject();
      if (dataObject == null || !(dataObject is IDBObjectTypedIDCollection))
        return;
      IDBTypedObjectID[] typedObjects = ((IDBObjectTypedIDCollection) dataObject).GetTypedObjects();
      if (typedObjects == null || typedObjects.Length == 0)
        return;
      foreach (IDBTypedObjectID dbTypedObjectId2 in typedObjects)
      {
        IDBObject dbObject2 = session.GetObject(dbTypedObjectId2.ObjectID, false);
        if (dbObject2 != null)
        {
          IDBAttribute attributeByGuid = dbObject2.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid == null || attributeByGuid.AsString == "")
            noDObjs.Add(dbTypedObjectId2.ObjectID);
          else
            objIDs.Add(dbTypedObjectId2.ObjectID);
        }
      }
    }
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in objIDs)
      {
        if (this.eco.HasThisObjectVersion(sessionKeeper.Session, objId))
          longList.Add(objId);
      }
    }
    if (longList.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (long num in longList)
      {
        if (stringBuilder.Length == 0)
          stringBuilder.Append(Convert.ToString(num));
        else
          stringBuilder.Append(", " + Convert.ToString(num));
        objIDs.Remove(num);
      }
      int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_261"), (object) stringBuilder.ToString()), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      if (objIDs.Count <= 0)
        return;
    }
    if (objIDs.Count <= 0)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_229"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
    }
    else
    {
      IncludeGoal includeGoal = new IncludeGoal();
      if (this.elCurChange != null && !Intermech.ECO.Client.ECO.IsExternal(this.elCurChange))
      {
        ECOGoal goal = this.eco.ChangeGoal((DocumentTreeNode) this.elCurChange);
        if (!includeGoal.Execute(goal, objIDs, this.eco.litera, noDObjs, (List<long>) null, this.eco.revType))
          return;
      }
      else
      {
        ECOGoal force = this.eco.HasTerm() ? ECOGoal.Change : ECOGoal.NoGoal;
        if (!includeGoal.Execute(objIDs, this.eco.litera, noDObjs, (List<long>) null, this.eco.revType, force))
          return;
      }
      List<long> finalObjectList = includeGoal.GetFinalObjectList();
      using (new SessionKeeper())
        this.NewAttachItems(finalObjectList, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, includeGoal.separateChanges);
    }
  }

  public static bool IsEcoContentsDocNode(DocumentTreeNode docNode)
  {
    return docNode != null && docNode.Name == LocalizationHolder.rm.GetString("ECO.Client_62");
  }

  public static DocumentTreeNode FindParentEcoContentsDocNode(DocumentTreeNode docNode)
  {
    for (; docNode != null; docNode = docNode.Parent)
    {
      if (ECOEditorForm.IsEcoContentsDocNode(docNode))
        return docNode;
      switch (docNode)
      {
        case Page _:
        case ImDocument _:
          return (DocumentTreeNode) null;
        default:
          continue;
      }
    }
    return (DocumentTreeNode) null;
  }

  public static bool IsEcoContentsDocNodeChild(DocumentTreeNode docNode)
  {
    return ECOEditorForm.FindParentEcoContentsDocNode(docNode) != null;
  }

  public TableElement CurrentChange => (TableElement) null;

  public void AfterLoadDoc()
  {
    this._filtrationService = this.InitializeFiltrationService();
    DocumentTreeNode templateRecursive1 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idReason);
    if (templateRecursive1 != null && templateRecursive1 is TextData)
      (templateRecursive1 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelReason);
    DocumentTreeNode templateRecursive2 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr);
    if (templateRecursive2 != null && templateRecursive2 is TextData)
      (templateRecursive2 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelReason);
    DocumentTreeNode templateRecursive3 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idUkazVnedrenie);
    if (templateRecursive3 != null && templateRecursive3 is TextData)
    {
      if (ECOPlugin.EnabledSeriesDates())
        (templateRecursive3 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelectSeriesDates);
      else
        (templateRecursive3 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelUkazanie);
    }
    DocumentTreeNode templateRecursive4 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idZadel1);
    if (templateRecursive4 != null && templateRecursive4 is TextData)
      (templateRecursive4 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelZadel);
    DocumentTreeNode templateRecursive5 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idZadel2);
    if (templateRecursive5 != null && templateRecursive5 is TextData)
      (templateRecursive5 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelZadel);
    DocumentTreeNode templateRecursive6 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idCreationDate);
    if (templateRecursive6 != null && templateRecursive6 is TextData)
      (templateRecursive6 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelDate);
    DocumentTreeNode templateRecursive7 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idStartChangeTerm);
    if (templateRecursive7 != null && templateRecursive7 is TextData)
      (templateRecursive7 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelDate);
    DocumentTreeNode templateRecursive8 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idEndChangeTerm);
    if (templateRecursive8 != null && templateRecursive8 is TextData)
      (templateRecursive8 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelDate);
    DocumentTreeNode templateRecursive9 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idPITerm);
    if (templateRecursive9 != null && templateRecursive9 is TextData)
      (templateRecursive9 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelDate);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.revObjType = sessionKeeper.Session.GetObject(this.eco.EcoObjectID).ObjectType;
    this.eco.UpdateRevType(this.revObjType);
    TableData dataOwner;
    int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner);
    while (dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count)
    {
      TableData node1 = dataOwner.Nodes[dataPositionInFlow] as TableData;
      if (!(node1.Template.Id == Intermech.ECO.Client.ECO.fldChange))
      {
        dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner);
      }
      else
      {
        string attributeValue = node1.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, false) ?? "";
        foreach (DocumentTreeNode node2 in node1.Nodes)
        {
          if (node2.Template.Id == Intermech.ECO.Client.ECO.fldChangeHeader)
          {
            if (node2.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive10)
            {
              if (templateRecursive10.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) != attributeValue)
                templateRecursive10.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
              templateRecursive10.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
              templateRecursive10.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
              if (this.eco.revType == RevType.PI || this.eco.revType == RevType.PR)
                templateRecursive10.ReadOnly = true;
            }
            if (node1.Tag == DBNull.Value && node2.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive11)
            {
              templateRecursive11.ReadOnly = false;
              templateRecursive11.TextChanged += new TextChanged_EventHandler(this.externalDocDesignChanged);
              IPageElementWithInterface elementWithInterface = templateRecursive11 as IPageElementWithInterface;
              elementWithInterface.InplaceEditorActivating += new CancelEventHandler(this.ExternalDoc_DesignEditing);
              elementWithInterface.InplaceEditorDeactivating += new CancelEventHandler(this.ExternalDoc_DesignDeactivating);
            }
          }
          if (node2.Template.Id == Intermech.ECO.Client.ECO.fldChangeHeader2)
          {
            DocumentTreeNode firstNodeByName1 = node2.FindFirstNodeByName(Intermech.ECO.Client.ECO.fldDesign);
            if (firstNodeByName1 != null)
            {
              foreach (DocumentTreeNode node3 in firstNodeByName1.Nodes)
              {
                if (node3.TemplateId == Intermech.ECO.Client.ECO.fldString && node3.FindFirstNodeByName(Intermech.ECO.Client.ECO.fldChangeNumber) is TextData firstNodeByName2)
                {
                  firstNodeByName2.TextChanged += new TextChanged_EventHandler(this.num_TextChanged);
                  firstNodeByName2.TextValidating += new TextValidating_EventHandler(this.num_TextValidating);
                  if (this.eco.revType == RevType.PI || this.eco.revType == RevType.PR)
                    firstNodeByName2.ReadOnly = true;
                }
              }
            }
          }
        }
        dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner);
      }
    }
    if (!this.Document.DocumentControl.ReadOnly)
    {
      this.UpdateDocDesign();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.eco.EcoObjectID);
        if (!this.ReadOnly)
        {
          bool flag = false;
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
          if (attributeById1 != null)
          {
            string asString = attributeById1.AsString;
            TextData templateRecursive12 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(this.eco.revType == RevType.II ? Intermech.ECO.Client.ECO.idRevDesignation : Intermech.ECO.Client.ECO.idPIDesignation);
            if (templateRecursive12 == null && this.eco.revType != RevType.II)
            {
              flag = true;
              templateRecursive12 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idRevDesignation);
            }
            if (templateRecursive12 != null && templateRecursive12.Text != asString)
              templateRecursive12.Text = asString;
          }
          if (!flag)
          {
            string nodeTemplateId = this.eco.revType != RevType.II ? Intermech.ECO.Client.ECO.idRevDesignation : Intermech.ECO.Client.ECO.idPIDesignation;
            if (nodeTemplateId != Intermech.ECO.Client.ECO.idPIDesignation)
            {
              TextData templateRecursive13 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(nodeTemplateId);
              if (templateRecursive13 != null && templateRecursive13.Text != "")
                templateRecursive13.Text = "";
            }
          }
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(RevHelper.idAttrRevReason);
          if (attributeById2 != null)
          {
            string str1 = Convert.ToString(attributeById2.Value);
            this.eco.reasonCode = Convert.ToString(str1);
            string str2 = str1 != "-1" ? attributeById2.AsString : Intermech.ECO.Client.ECO.noChangeNumber;
            string description = str1 != "-1" ? attributeById2.Description : "";
            string str3 = LocalizationHolder.rm.GetString("ECO.Client_238");
            TextData templateRecursive14 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr);
            if (templateRecursive14 != null && templateRecursive14.Text != str2 && (this.eco.reasonCode != "-1" || templateRecursive14.Text == ""))
              templateRecursive14.AssignText(str2, false, false, false);
            TextData templateRecursive15 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idReason);
            if (templateRecursive15 != null && templateRecursive15.Text != description && (str1 != "-1" && str1 != "-" || templateRecursive15.Text == str3))
              templateRecursive15.AssignText(description, false, false, false);
          }
          IDBAttribute attributeById3 = dbObject.GetAttributeByID(RevHelper.idAttrLitera);
          if (attributeById3 != null)
            this.eco.litera = attributeById3.AsString;
          IDBAttribute attributeById4 = dbObject.GetAttributeByID(RevHelper.idAttrRevSeriesDates);
          if (attributeById4 != null)
            this.eco.SDAC = attributeById4.AsString;
          IDBAttribute attributeById5 = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateStart);
          if (attributeById5 != null)
          {
            if (attributeById5.Value == null || attributeById5.Value == DBNull.Value)
              this.eco.changeTermStart = DateTime.MinValue;
            else
              this.eco.changeTermStart = Convert.ToDateTime(attributeById5.Value);
            TextData templateRecursive16 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idStartChangeTerm);
            if (templateRecursive16 != null)
            {
              string str = this.eco.changeTermStart == DateTime.MinValue ? string.Empty : this.eco.changeTermStart.ToShortDateString();
              if (templateRecursive16.Text != str)
                templateRecursive16.AssignText(str, false, false, false);
            }
          }
          IDBAttribute attributeById6 = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateEnd);
          if (attributeById6 != null)
          {
            if (attributeById6.Value == null || attributeById6.Value == DBNull.Value)
              this.eco.changeTermEnd = DateTime.MinValue;
            else
              this.eco.changeTermEnd = Convert.ToDateTime(attributeById6.Value);
            TextData templateRecursive17 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idEndChangeTerm);
            if (templateRecursive17 != null)
            {
              string str = this.eco.changeTermEnd == DateTime.MinValue ? string.Empty : this.eco.changeTermEnd.ToShortDateString();
              if (templateRecursive17.Text != str)
                templateRecursive17.AssignText(str, false, false, false);
            }
          }
        }
      }
      this.UpdateSpecText();
    }
    this.UndoManager.Clear();
    foreach (PendingLink objLink in this.eco.objLinks)
    {
      List<long> abonListIds = Intermech.ECO.Client.ECO.GetAbonListIds(objLink.verID, RevHelper.idObjOrganization);
      if (abonListIds != null)
      {
        HashSet<long> other = new HashSet<long>((IEnumerable<long>) abonListIds);
        if (other.Count != 0)
          this.eco.OrgSet.UnionWith((IEnumerable<long>) other);
      }
    }
  }

  private void ExternalDoc_DesignDeactivating(object sender, EventArgs e)
  {
    if (sender == null || !(sender is TextBoxElement))
      return;
    TextBoxElement textBoxElement = (TextBoxElement) sender;
    if (textBoxElement.InPlaceEditorControl == null)
      return;
    ImRtfEditor placeEditorControl = (ImRtfEditor) textBoxElement.InPlaceEditorControl;
    if (this.textChanged)
      return;
    this.lockChange = true;
    try
    {
      int num = this.Document.Modified ? 1 : 0;
      string attributeValue = textBoxElement.GetAttributeValue(Intermech.ECO.Client.ECO.textSaveAttr, false);
      ((TextData) sender).AssignText(attributeValue, false, true, false, false, false);
      if (num != 0)
        return;
      this.Document.Modified = false;
    }
    finally
    {
      this.lockChange = false;
    }
  }

  private void ExternalDoc_DesignEditing(object sender, CancelEventArgs e)
  {
    if (sender == null || !(sender is TextData))
      return;
    TextData textData = (TextData) sender;
    string attributeValue = textData.GetAttributeValue(Intermech.ECO.Client.ECO.textAttr, false);
    this.lockChange = true;
    try
    {
      int num = this.Document.Modified ? 1 : 0;
      textData.SetAttributeValue(Intermech.ECO.Client.ECO.textSaveAttr, textData.Text);
      textData.AssignText(attributeValue, false, true, false, false, false);
      if (num == 0)
        this.Document.Modified = false;
      this.textChanged = false;
    }
    finally
    {
      this.lockChange = false;
    }
  }

  public void ReadECOVersion(IDBObject idbO)
  {
    IDBAttribute attributeById = idbO.GetAttributeByID(RevHelper.idAttrVersion);
    if (attributeById != null)
    {
      this.eco.ecoVersion = attributeById.AsInteger;
    }
    else
    {
      string attributeValue = this.eco.ecoMainTable.GetAttributeValue(Intermech.ECO.Client.ECO.versionIdAttr, true);
      if (attributeValue == "")
        this.eco.ecoVersion = 0L;
      else
        this.eco.ecoVersion = (long) Convert.ToInt32(attributeValue);
    }
  }

  public bool UpdateDocDesign()
  {
    bool writeDesOnReplace = this.plugin.eps.Current.WriteDesOnReplace;
    int num = 0;
    TableData change = (TableData) null;
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
      ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) node);
      if (ecoGoal == ECOGoal.Replace)
        ecoGoal = ECOGoal.Change;
      if (ecoGoal != ECOGoal.NoGoal)
      {
        if (ecoGoal == ECOGoal.Change || ecoGoal == ECOGoal.Creation)
        {
          ++num;
          if (change == null)
            change = node;
        }
        else
          ++num;
      }
    }
    string attributeValue = Intermech.ECO.Client.ECO.seeBelow;
    switch (num)
    {
      case 0:
        attributeValue = "";
        break;
      case 1:
        if (change != null)
        {
          List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
          if (idList.Count == 1)
          {
            PendingLink pendingLink = this.eco.FindPendingLink(idList[0]);
            if (pendingLink != null && (writeDesOnReplace || pendingLink.ecoGoal != ECOGoal.Replace))
            {
              pendingLink.UpdateDesign();
              attributeValue = pendingLink.design;
              break;
            }
            break;
          }
          if (Intermech.ECO.Client.ECO.IsExternal((TableElement) change) && change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive)
          {
            attributeValue = templateRecursive.GetAttributeValue(Intermech.ECO.Client.ECO.textAttr, true);
            if (attributeValue == "" && templateRecursive.Text != "")
            {
              attributeValue = templateRecursive.Text;
              templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.textAttr, attributeValue);
              break;
            }
            break;
          }
          break;
        }
        attributeValue = "";
        break;
      default:
        if (change == null)
        {
          attributeValue = "";
          break;
        }
        break;
    }
    bool flag = false;
    if (this.eco.ecoDocRevision != null && this.eco.ecoDocRevision.Text != attributeValue)
    {
      this.eco.ecoDocRevision.AssignText(attributeValue, true, false, false);
      flag = true;
    }
    if (attributeValue != "" && attributeValue != Intermech.ECO.Client.ECO.seeBelow && change != null)
    {
      TextData templateRecursive = (TextData) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign);
      if (templateRecursive != null)
      {
        if (templateRecursive.Text != "")
        {
          try
          {
            this.lockChange = true;
            templateRecursive.AssignText("", true, false, false);
            flag = true;
          }
          finally
          {
            this.lockChange = false;
          }
        }
      }
    }
    foreach (TableElement dtn in (TableData) this.eco.ecoMainTable)
    {
      if (dtn != change || !(attributeValue != Intermech.ECO.Client.ECO.seeBelow) || !(attributeValue != ""))
      {
        TextData templateRecursive = (TextData) dtn.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign);
        if (templateRecursive != null)
        {
          ECOGoal ecoGoal = this.eco.ChangeGoal((DocumentTreeNode) dtn);
          if (writeDesOnReplace && ecoGoal == ECOGoal.Replace)
            ecoGoal = ECOGoal.Change;
          switch (ecoGoal)
          {
            case ECOGoal.Change:
            case ECOGoal.Creation:
              if (templateRecursive.Text == "")
              {
                List<long> idList = this.eco._GetIdList(dtn.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
                try
                {
                  this.lockChange = true;
                  templateRecursive.Text = idList.Count <= 0 ? templateRecursive.GetAttributeValue(Intermech.ECO.Client.ECO.textAttr, true) : this.eco.DesignListStr(idList);
                  continue;
                }
                finally
                {
                  this.lockChange = false;
                }
              }
              else
                continue;
            case ECOGoal.Annul:
            case ECOGoal.Litera:
            case ECOGoal.Replace:
              if (templateRecursive.Text != "")
              {
                try
                {
                  this.lockChange = true;
                  templateRecursive.AssignText("", true, false, false);
                  flag = true;
                  continue;
                }
                finally
                {
                  this.lockChange = false;
                }
              }
              else
                continue;
            default:
              continue;
          }
        }
      }
    }
    if (flag)
      this.Document.UpdateLayout(0, true, true);
    return flag;
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ECOEditorForm));
    this.IL = new ImageList(this.components);
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Transparent;
    this.AllowDrop = true;
    this.Name = nameof (ECOEditorForm);
    this.Closed += new EventHandler(this.ECOEditorForm_Closed);
    this.ResumeLayout(false);
  }

  public override void OnClosed(EventArgs e)
  {
    this.SaveTreeConfig();
    base.OnClosed(e);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    bool forSaveBeforeClose = this.AskForSaveBeforeClose;
    this.AskForSaveBeforeClose = false;
    try
    {
      base.OnClosing(e);
    }
    finally
    {
      this.AskForSaveBeforeClose = forSaveBeforeClose;
    }
    if (e.Cancel || !this.AskForSaveBeforeClose || this.ReadOnly || !this.ObjectAssigned || this.Document == null || !this.Document.Modified)
      return;
    DialogResult dialogResult = MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_63"), (object) this.DocumentCaption), LocalizationHolder.rm.GetString("ECO.Client_64"), MessageBoxButtons.YesNoCancel);
    switch (dialogResult)
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        try
        {
          if (this.SaveRevision())
          {
            this.Document.Modified = false;
            break;
          }
          e.Cancel = true;
          return;
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
          e.Cancel = true;
          break;
        }
      default:
        if (dialogResult == DialogResult.No && this.eco.newVers.Count > 0)
        {
          dialogResult = MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_65"), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.YesNoCancel);
          switch (dialogResult)
          {
            case DialogResult.Cancel:
              e.Cancel = true;
              break;
            case DialogResult.Yes:
              try
              {
                this.SaveRevision();
                this.Document.Modified = false;
                break;
              }
              catch
              {
                e.Cancel = true;
                break;
              }
          }
        }
        else
          break;
        break;
    }
    if (dialogResult != DialogResult.No || this.eco.newVers.Count <= 0)
      return;
    this.DeleteNewVersions();
  }

  private void LaunchScreenShooter()
  {
    string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scrshooter.exe");
    if (!File.Exists(str))
      return;
    Process.Start(str)?.Dispose();
  }

  private List<long> _AddPendingLink(IUserSession session, PendingLink pl)
  {
    IDBObject dbObject1 = session.GetObjectActualCopy(pl.verID, false);
    if (dbObject1 == null)
    {
      dbObject1 = session.GetObjectActualCopy(-pl.verID, false);
      if (dbObject1 == null)
        return (List<long>) null;
    }
    IDBRelation relation1 = (IDBRelation) null;
    List<long> longList = new List<long>();
    switch (dbObject1.ObjectModifyMode)
    {
      case ObjectModifyModes.Checkout:
        long objectId = dbObject1.ObjectID;
        if (!this.NoChangeNums(pl.ecoGoal))
        {
          if (objectId >= 0L && !this.ChangeNumAllowedWithoutCheckout(dbObject1))
            dbObject1 = dbObject1.CheckOut();
          dbObject1.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false).Value = (object) pl.verStr;
        }
        try
        {
          if ((!this.plugin.eps.Current.AutoCheckOut ? 0 : (this.IsDocObjectType(dbObject1.ObjectType) ? 1 : 0)) != 0)
          {
            if (dbObject1.ObjectID >= 0L)
              dbObject1 = dbObject1.CheckOut();
          }
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
        relation1 = RevHelper.CreateRevRelation(session, this.ecoID, dbObject1.ObjectID, pl.verStr, pl.ecoGoal, pl.hideType);
        break;
      case ObjectModifyModes.CreateVersion:
        if (pl.ecoGoal == ECOGoal.Annul || pl.ecoGoal == ECOGoal.Litera || pl.ecoGoal == ECOGoal.Creation)
        {
          string nchangeNo = this.GetNChangeNo(session, dbObject1.ID, dbObject1.ObjectID, pl.ecoGoal);
          relation1 = RevHelper.CreateRevRelation(session, this.ecoID, dbObject1.ObjectID, nchangeNo, pl.ecoGoal, pl.hideType);
          break;
        }
        List<PendingLink> pendingLinkList = new List<PendingLink>();
        bool version = this._DoCreateVersion(dbObject1.ObjectID, pendingLinkList, pl.ecoGoal, pl.stepID);
        if (pendingLinkList != null)
        {
          foreach (PendingLink pendingLink in pendingLinkList)
          {
            IDBObject dbObject2 = session.GetObject(pendingLink.verID, false);
            pendingLink.InitVars(dbObject2);
            pendingLink.SetDesign(dbObject2);
          }
        }
        if (!version)
        {
          IDBObject dbObject3 = session.GetObject(pl.verID, false);
          if (dbObject3 != null)
          {
            string nchangeNo = this.GetNChangeNo(session, dbObject3.ID, pl.verID, pl.ecoGoal);
            relation1 = RevHelper.CreateRevRelation(session, this.ecoID, pl.verID, nchangeNo, pl.ecoGoal, pl.hideType);
            IDBAttribute dbAttribute = relation1.GetAttributeByID(RevHelper.idAttrDelWhenExcluded);
            if (dbAttribute != null)
              dbAttribute = relation1.Attributes.AddAttribute(RevHelper.idAttrDelWhenExcluded, false);
            if (dbAttribute != null)
              dbAttribute.AsBoolean = pl.needDelete;
            this.AddAuxLinksAttr(relation1, pl);
            break;
          }
          break;
        }
        foreach (PendingLink pendingLink in pendingLinkList)
        {
          long revRelation = RevHelper.GetRevRelation(this.ECO.EcoObjectID, pendingLink.verID);
          if (revRelation != 0L)
          {
            IDBRelation relation2 = session.GetRelation(revRelation, false);
            if (relation2 != null)
              longList.Add(relation2.RelationID);
          }
          if (this.eco.ObjIdIndex(pendingLink.verID) < 0)
            this.eco.objLinks.Add(pendingLink);
        }
        this._AddAdditionalObjects(pl.verID, pendingLinkList);
        this._DoCreateRelation(session, pl);
        return longList;
      default:
        if (!this.NoChangeNums(pl.ecoGoal))
          dbObject1.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false).Value = (object) pl.verStr;
        relation1 = RevHelper.CreateRevRelation(session, this.ecoID, dbObject1.ObjectID, pl.verStr, pl.ecoGoal, pl.hideType);
        break;
    }
    if (pl.auxObjects != null && pl.auxObjects.Count > 0)
      this.AddAuxLinksAttr(relation1, pl);
    if (relation1 != null)
    {
      if (pl.needDelete)
      {
        IDBAttribute attributeById = relation1.GetAttributeByID(RevHelper.idAttrDelWhenExcluded);
        if (attributeById != null)
          attributeById.AsBoolean = pl.needDelete;
      }
      (relation1.GetAttributeByID(RevHelper.idAttrFutureLC) ?? relation1.Attributes.AddAttribute(RevHelper.idAttrFutureLC, false)).AsInteger = (long) pl.stepID;
    }
    longList.Add(relation1.RelationID);
    return longList;
  }

  private void _SetHidingStatus(IUserSession session, PendingLink pl)
  {
    long revRelation = RevHelper.GetRevRelation(this.ECO.EcoObjectID, pl.verID);
    if (revRelation == 0L)
      return;
    IDBRelation relation = session.GetRelation(revRelation, false);
    if (relation == null)
      return;
    IDBAttribute attributeById = relation.GetAttributeByID(RevHelper.idAttrHiding);
    if (attributeById == null)
      return;
    HidingType int32 = (HidingType) Convert.ToInt32(attributeById.Value);
    if (pl.hideType == int32)
      return;
    attributeById.AsInteger = (long) pl.hideType;
  }

  private bool ChangeNumAllowedWithoutCheckout(IDBObject obj)
  {
    if (obj.GetAttributeByID(RevHelper.idAttrChangeNo) == null)
    {
      IDBAttribute dbAttribute;
      try
      {
        dbAttribute = obj.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
      }
      catch (KernelExceptionID ex)
      {
        return false;
      }
      if (dbAttribute == null)
        return false;
    }
    return obj.Session.GetObjectType(obj.ObjectType).Attributes.GetAttributeByID(RevHelper.idAttrChangeNo) is IDBAttributeType4Object attributeById && (attributeById.Attribute4ObjectPropertiesStructure.Options & AttributeOptions.ModifyInBase) != AttributeOptions.None;
  }

  private bool IsDocObjectType(int objType)
  {
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objType);
    if (objectTypeParentsId.IndexOf(objType) < 0)
      objectTypeParentsId.Add(objType);
    return objectTypeParentsId.IndexOf(ECOPlugin.plugin.docTypeId) >= 0;
  }

  private void _AddAdditionalObjects(long mainObjId, List<PendingLink> newLinks)
  {
    TableData ecoRow4Relation = this.eco.FindEcoRow4Relation(mainObjId);
    if (ecoRow4Relation == null)
      return;
    this._AddObjectsGuids(ecoRow4Relation, newLinks);
  }

  private string _AddObjectsGuids(TableData td, List<PendingLink> newLinks)
  {
    IEnumerable<long> newIdList = newLinks.Select<PendingLink, long>((System.Func<PendingLink, long>) (pl => pl.verID));
    return this._AddObjectsGuids(td, newIdList);
  }

  private string _AddObjectsGuids(TableData td, IEnumerable<long> newIdList)
  {
    List<long> idList = this.eco._GetIdList(td.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    bool flag = false;
    foreach (long newId in newIdList)
    {
      if (!idList.Contains(newId))
      {
        idList.Add(newId);
        flag = true;
      }
    }
    string str = "";
    if (flag)
      str = this.eco._SetIdList((RectangleElement) td, idList);
    return str;
  }

  private string _SetObjectsGuids(TableData td, List<PendingLink> newLinks)
  {
    List<long> idList = this.eco._GetIdList(td.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    bool flag = false;
    foreach (PendingLink newLink in newLinks)
    {
      if (!idList.Contains(newLink.verID))
      {
        idList.Add(newLink.verID);
        flag = true;
      }
    }
    string str = "";
    if (flag)
      str = this.eco._SetIdList((RectangleElement) td, idList);
    return str;
  }

  private void _DeletePendingLink(
    IUserSession session,
    PendingLink pl,
    out long relID,
    out long objID)
  {
    relID = RevHelper.GetRevRelation(this.ecoID, pl.verID);
    objID = -1L;
    if (relID == 0L)
      return;
    IDBRelation relation = session.GetRelation(relID);
    if (relation == null || !(session.GetCustomService(typeof (IECOServer)) is IECOServer customService))
      return;
    long relationId = relation.RelationID;
    using (new ECOLinkDeleter(session, relationId))
      relation.Delete(0L);
    HashSet<long> longSet = this.NotDeleted;
    for (int index = 0; longSet != null && longSet.Contains(-1L) && index < 100; ++index)
    {
      longSet = customService.GetDeletedObjects(session.SessionGUID);
      if (longSet != null && longSet.Contains(-1L))
        Thread.Sleep(100);
    }
  }

  private void _ChangeLinkChangeNoAndGoal(
    IUserSession session,
    long objID,
    string changeNo,
    ECOGoal newGoal,
    int newStepId)
  {
    long revRelation = RevHelper.GetRevRelation(this.ecoID, objID);
    if (revRelation == 0L)
      return;
    bool flag = false;
    IDBRelation relation = session.GetRelation(revRelation);
    if (relation != null)
    {
      IDBAttribute dbAttribute = relation.Attributes.FindByID(RevHelper.idAttrChangeNo) ?? relation.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
      if (dbAttribute != null && dbAttribute.AsString != changeNo)
      {
        dbAttribute.AsString = changeNo;
        flag = true;
      }
      IDBAttribute attributeById1 = relation.GetAttributeByID(RevHelper.idAttrIncludeGoal);
      if (attributeById1.AsInteger != (long) newGoal)
        attributeById1.AsInteger = (long) newGoal;
      IDBAttribute attributeById2 = relation.GetAttributeByID(RevHelper.idAttrFutureLC);
      if (attributeById2.AsInteger != (long) newStepId)
        attributeById2.AsInteger = (long) newStepId;
    }
    if (!flag)
      return;
    this.SetNewChangeNo(session, objID, changeNo);
  }

  private bool SaveRevision()
  {
    this.DocumentControl.DeactivateInPlaceEditor();
    if (this.deletedLinks.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (PendingLink pendingLink in (IEnumerable) this.deletedLinks.Values)
      {
        if (pendingLink.needDelete)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.AppendFormat("\"{0}\"", (object) pendingLink.design);
        }
      }
      if (stringBuilder.Length > 0 && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_236"), (object) stringBuilder.ToString()), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return false;
    }
    this._AddNewPendingLinks();
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    List<PendingLink> pendingLinkList = new List<PendingLink>(this.deletedLinks.Count);
    foreach (PendingLink pendingLink in (IEnumerable) this.deletedLinks.Values)
      pendingLinkList.Add(pendingLink);
    List<long> relationIDs = new List<long>();
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    List<long> longList3 = new List<long>();
    Exception exception;
    while (true)
    {
      exception = (Exception) null;
      PendingLink pendingLink = (PendingLink) null;
      longList1.Clear();
      relationIDs.Clear();
      foreach (PendingLink pl in pendingLinkList)
      {
        if (!longList3.Contains(Math.Abs(pl.verID)))
        {
          long relID = -1;
          long objID = -1;
          try
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              sessionKeeper.Session.StartLogHistory();
              try
              {
                this._DeletePendingLink(sessionKeeper.Session, pl, out relID, out objID);
              }
              finally
              {
                sessionKeeper.Session.StopLogHistory();
              }
              List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
              if (modificationsHistoryList != null)
              {
                for (int index = 0; index < modificationsHistoryList.Count; ++index)
                {
                  CategoryValue categoryValue = modificationsHistoryList[index];
                  if (categoryValue.ActionID == ActionType.Purge && categoryValue.CategoryType == 1)
                    longList1.Add(categoryValue.CategoryID);
                }
              }
            }
            relationIDs.Add(relID);
            longList3.Add(pl.verID);
          }
          catch (Exception ex)
          {
            exception = ex;
            pendingLink = pl;
            break;
          }
        }
      }
      if (longList1.Count > 0 || relationIDs.Count > 0)
      {
        if (service != null)
        {
          service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs));
          service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) longList1));
        }
        if (this.ecoTreeViewDlg != null)
          this.ecoTreeViewDlg.UpdateTree();
      }
      if (exception != null)
      {
        if (!longList2.Contains(pendingLink.verID) || relationIDs.Count != 0 || longList1.Count != 0)
        {
          longList2.Add(pendingLink.verID);
          pendingLinkList.Remove(pendingLink);
          pendingLinkList.Add(pendingLink);
        }
        else
          break;
      }
      else
        goto label_51;
    }
    ExceptionHelper.ExceptionService.ShowException(exception);
label_51:
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long key in this.changedLinks.Keys)
      {
        PendingLink changedLink = this.changedLinks[key];
        if (changedLink != null)
        {
          string verStr = changedLink.verStr;
          this._ChangeLinkChangeNoAndGoal(sessionKeeper.Session, key, verStr, changedLink.ecoGoal, changedLink.stepID);
        }
      }
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.eco.EcoObjectID);
      this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr);
      IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrRevReason);
      if (attributeById != null)
      {
        if (attributeById.AsString != this.eco.reasonCode)
          attributeById.AsString = this.eco.reasonCode;
      }
      else
        dbObject.Attributes.AddAttribute(RevHelper.idAttrRevReason, false).AsString = this.eco.reasonCode;
      IECOServer customService = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
      customService.SetLitera(sessionKeeper.Session.SessionGUID, this.eco.EcoObjectID, this.eco.litera);
      IDBAttribute dbAttribute1 = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateStart);
      if (!this.eco.changeTermStart.Equals(DateTime.MinValue))
      {
        if (dbAttribute1 == null)
          dbAttribute1 = dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeDateStart, false);
        if (dbAttribute1 != null)
        {
          if (dbAttribute1.Value == DBNull.Value || !Convert.ToDateTime(dbAttribute1.Value).Equals(this.eco.changeTermStart))
            dbAttribute1.AsDateTime = this.eco.changeTermStart;
          this.UpdateStartDates(this.eco.changeTermStart);
        }
      }
      else if (dbAttribute1 != null)
      {
        if (dbAttribute1.Value != DBNull.Value)
          dbAttribute1.Value = (object) DBNull.Value;
        this.UpdateStartDates(this.eco.changeTermStart);
      }
      IDBAttribute dbAttribute2 = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateEnd);
      if (!this.eco.changeTermEnd.Equals(DateTime.MinValue))
      {
        if (dbAttribute2 == null)
          dbAttribute2 = dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeDateEnd, false);
        if (dbAttribute2 != null)
        {
          if (dbAttribute2.Value == DBNull.Value || !Convert.ToDateTime(dbAttribute2.Value).Equals(this.eco.changeTermEnd))
            dbAttribute2.AsDateTime = this.eco.changeTermEnd;
          this.UpdateEndDates(this.eco.changeTermEnd);
        }
      }
      else if (dbAttribute2 != null)
      {
        if (dbAttribute2.Value != DBNull.Value)
          dbAttribute2.Value = (object) DBNull.Value;
        this.UpdateEndDates(this.eco.changeTermEnd);
      }
      customService.DeleteStartEndAttrs(longList1);
      IDBAttribute dbAttribute3 = dbObject.Attributes.AddAttribute(RevHelper.idAttrVersion, false);
      if (dbAttribute3 != null)
        dbAttribute3.AsInteger = Intermech.ECO.Client.ECO.curVersion;
    }
    if (!this.eco.IdLists())
    {
      TableData dataOwner;
      for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
        string str = Intermech.ECO.Client.ECO.GuidListToStr(this.eco._IdListToGuidList(this.eco._GetIdList(node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true))));
        node.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, str);
      }
    }
    HashSet<long> longSet = new HashSet<long>();
    for (int index = this.eco.objLinks.Count - 1; index >= 0; --index)
    {
      PendingLink objLink = this.eco.objLinks[index];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objLink.verID, false);
        if (dbObject != null)
        {
          if ((dbObject as IDBLifecycleLevel).LevelID != sessionKeeper.Session.IdentHelper.DeletedID)
            continue;
        }
        longSet.Add(objLink.verID);
        this.eco.objLinks.RemoveAt(index);
      }
    }
    if (longSet.Count > 0)
    {
      for (int index = this.eco.hiddenLinks.Count - 1; index >= 0; --index)
      {
        PendingLink hiddenLink = this.eco.hiddenLinks[index];
        if (longSet.Contains(hiddenLink.verID))
          this.eco.hiddenLinks.RemoveAt(index);
      }
      if (this.ecoTreeViewDlg != null)
        this.ecoTreeViewDlg.UpdateTree();
    }
    DocumentEditorPlugin.SaveImDocumentObjectFile(this.DocumentID, this.Document, this.DefaultFileName, 0, true);
    this.addedLinks.Clear();
    this.deletedLinks.Clear();
    this.changedLinks.Clear();
    this.Document.Modified = false;
    return true;
  }

  private void ReAddObjects(IUserSession ius)
  {
    if (this.addedLinks.Count == 0)
      return;
    IDBObject dbObject = ius.GetObject(this.eco.EcoObjectID);
    if (dbObject == null)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDelVersionsList);
    if (attributeById == null || attributeById.ValuesCount == 0)
      return;
    List<string> stringList1 = new List<string>();
    foreach (PendingLink pendingLink in (IEnumerable) this.addedLinks.Values)
    {
      string lower1 = pendingLink.verGuid.ToString().ToLower();
      if (!stringList1.Contains(lower1))
        stringList1.Add(lower1);
      if (pendingLink.auxObjects != null)
      {
        foreach (ObjInfo auxObject in pendingLink.auxObjects)
        {
          string lower2 = auxObject.verGuid.ToString().ToLower();
          if (!stringList1.Contains(lower2))
            stringList1.Add(lower2);
        }
      }
    }
    if (stringList1.Count == 0)
      return;
    List<string> stringList2 = new List<string>();
    foreach (object obj in attributeById.Values)
    {
      string str = Convert.ToString(obj);
      if (!stringList1.Contains(str))
        stringList2.Add(str);
    }
    dbObject.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(RevHelper.idAttrDelVersionsList, FieldTypes.ftString, MultiValueModes.MultiValues, (object[]) stringList2.ToArray())
    });
  }

  public void _AddNewPendingLinks()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> relationIDs = new List<long>();
      try
      {
        foreach (PendingLink pl in (IEnumerable) this.addedLinks.Values)
        {
          List<long> longList = this._AddPendingLink(sessionKeeper.Session, pl);
          if (longList != null)
          {
            foreach (long num in longList)
              relationIDs.Add(num);
          }
        }
      }
      catch (Exception ex)
      {
        IECOServer customService = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
        if (relationIDs.Count > 0)
        {
          foreach (long num in relationIDs)
          {
            customService.StartLinkDeletion(num);
            try
            {
              sessionKeeper.Session.GetRelation(num, false)?.Delete(0L);
            }
            finally
            {
              customService.EndLinkDeletion(num);
            }
          }
        }
        throw;
      }
      if (relationIDs.Count > 0)
        ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs));
      this.ReAddObjects(sessionKeeper.Session);
    }
  }

  private void UpdateStartDates(DateTime newChangeStart)
  {
    IECOServer customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IECOServer)) as IECOServer;
    foreach (PendingLink objLink in this.eco.objLinks)
    {
      switch (objLink.ecoGoal)
      {
        case ECOGoal.Change:
        case ECOGoal.Replace:
        case ECOGoal.Creation:
          customService.SetStartDate(objLink.verID, newChangeStart);
          continue;
        case ECOGoal.Annul:
          customService.SetEndDate(objLink.verID, newChangeStart);
          continue;
        default:
          continue;
      }
    }
  }

  private void UpdateEndDates(DateTime newChangeEnd)
  {
    IECOServer customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IECOServer)) as IECOServer;
    foreach (PendingLink objLink in this.eco.objLinks)
    {
      switch (objLink.ecoGoal)
      {
        case ECOGoal.Change:
        case ECOGoal.Replace:
        case ECOGoal.Creation:
          customService.SetEndDate(objLink.verID, newChangeEnd);
          continue;
        default:
          continue;
      }
    }
  }

  private void DeleteNewVersions()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
      if (!(sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer))
        return;
      for (int index = 0; index < this.eco.newVers.Count; ++index)
      {
        long newVer = this.eco.newVers[index];
        foreach (long parentRevRel in this.eco.GetParentRevRels(sessionKeeper.Session, newVer))
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(parentRevRel, false);
          if (relation != null)
          {
            using (new ECOLinkDeleter(sessionKeeper.Session, parentRevRel))
              relation.Delete(0L);
          }
          service?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", parentRevRel));
        }
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(newVer, false);
        if (objectActualCopy != null)
        {
          if (objectActualCopy.CheckoutBy == sessionKeeper.Session.UserID)
            objectActualCopy.CancelChanges();
          try
          {
            objectActualCopy.Delete(0L);
            service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", newVer));
          }
          catch
          {
          }
        }
      }
    }
  }

  private bool PerformCreateVersion(
    IUserSession ius,
    long objID,
    IDBObject idbO,
    ECOGoal goal,
    int lcStepId,
    out List<PendingLink> newLinks)
  {
    newLinks = new List<PendingLink>();
    ECOGoal ecoGoal = goal;
    if (ecoGoal == ECOGoal.Annul || ecoGoal == ECOGoal.Litera && !this.plugin.eps.Current.CreateLiteraVersion || ecoGoal == ECOGoal.Creation)
      return false;
    if (idbO == null)
      idbO = ius.GetObjectActualCopy(objID, false);
    if (idbO == null)
      return false;
    if (idbO.CheckoutBy != 0L)
    {
      newLinks.Add(new PendingLink(goal, lcStepId)
      {
        verID = idbO.ObjectID,
        needDelete = false,
        hideType = HidingType.Disabled
      });
      return false;
    }
    switch (idbO.ObjectModifyMode)
    {
      case ObjectModifyModes.Checkout:
        newLinks.Add(new PendingLink(goal, lcStepId)
        {
          verID = idbO.ObjectID
        });
        return false;
      case ObjectModifyModes.CreateVersion:
        bool version = this._DoCreateVersion(idbO.ObjectID, newLinks, goal, lcStepId);
        if (newLinks != null)
        {
          foreach (PendingLink pendingLink in newLinks)
          {
            IDBObject dbObject = ius.GetObject(pendingLink.verID, false);
            pendingLink.InitVars(dbObject);
            pendingLink.SetDesign(dbObject);
          }
        }
        return version;
      case ObjectModifyModes.CantModify:
        IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep(idbO.LCStep);
        if (lifecycleStep.ObjectModifyMode == ObjectModifyModes.CantModify)
          throw new ERevision(objID, string.Format(LocalizationHolder.rm.GetString("ECO.Client_251"), (object) objID, (object) lifecycleStep.LCName));
        throw new ERevision(objID, LocalizationHolder.rm.GetString("ECO.Client_188") + Convert.ToString(objID) + LocalizationHolder.rm.GetString("ECO.Client_189"));
      default:
        return false;
    }
  }

  private long GetObjectVerForThisContext(IUserSession session, IDBObject dbObject)
  {
    IDBEditingContextsObject editingContextsObject = (IDBEditingContextsObject) session.GetObject(this.eco.EcoObjectID, false);
    if (editingContextsObject == null)
      return -1;
    if (!editingContextsObject.ExistsObject(dbObject.ID, true))
      return -1;
    try
    {
      return (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetEditingContextsObject((object) session.SessionGUID, editingContextsObject.LinkedContextNumber, false, false).GetObjectVersion(dbObject.ID);
    }
    catch
    {
      return -1;
    }
  }

  private bool AddAuxLinksAttr(IDBRelation relation, PendingLink pl)
  {
    return this.UpdateAuxLinks(relation, ref pl.auxObjects);
  }

  public bool UpdateAuxLinks(IDBRelation rel, ref List<ObjInfo> objIDs, bool addExisting = true)
  {
    if (objIDs != null)
    {
      IECOServer customService = rel.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
      foreach (ObjInfo objInfo in objIDs)
      {
        if (!customService.ObjectHasID(rel.Session.SessionGUID, Math.Abs(objInfo.verId)))
          return false;
      }
    }
    bool flag1 = false;
    IDBAttribute dbAttribute = rel.Attributes.AddAttribute(RevHelper.idAttrAuxLinks, false);
    if (dbAttribute != null)
    {
      HashSet<long> longSet = new HashSet<long>();
      if (dbAttribute.Values != null)
      {
        foreach (object obj in dbAttribute.Values)
        {
          if (!obj.Equals((object) DBNull.Value))
          {
            long int64 = Convert.ToInt64(obj);
            if (!longSet.Contains(int64))
              longSet.Add(int64);
          }
        }
      }
      int count = objIDs != null ? objIDs.Count : 0;
      if (longSet.Count == count)
      {
        for (int index = 0; index < longSet.Count; ++index)
        {
          if (!longSet.Contains(objIDs[index].verId))
          {
            flag1 = true;
            break;
          }
        }
      }
      else
        flag1 = true;
      if (flag1)
      {
        if (objIDs == null)
          objIDs = new List<ObjInfo>();
        if (addExisting)
        {
          foreach (long vId in longSet)
          {
            bool flag2 = false;
            foreach (ObjInfo objInfo in objIDs)
            {
              if (Math.Abs(objInfo.verId) == vId)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              objIDs.Add(new ObjInfo(vId));
          }
        }
        object[] instance = (object[]) Array.CreateInstance(typeof (object), objIDs.Count);
        for (int index = 0; index < objIDs.Count; ++index)
          instance[index] = (object) Math.Abs(objIDs[index].verId);
        try
        {
          dbAttribute.Values = instance;
        }
        catch (ObjectNotFoundException ex)
        {
          flag1 = false;
        }
      }
    }
    return flag1;
  }

  private void AddObjectsToThisContext(IUserSession session, List<long> objects)
  {
    IDBEditingContextsObject editingContextsObject = (IDBEditingContextsObject) session.GetObject(this.eco.EcoObjectID, false);
    if (editingContextsObject == null)
      return;
    try
    {
      IDBEditingContextsService customService = session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService;
      List<long> fIDs = new List<long>();
      foreach (long objectID in objects)
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
        fIDs.Add(objectInfo.ID);
      }
      customService.AddToContext((object) session.SessionGUID, this.eco.EcoObjectID, editingContextsObject.LinkedContextNumber, (IList<long>) fIDs, (IList<long>) objects, true, false);
    }
    catch
    {
    }
  }

  private void DelObjectsFromThisContext(IUserSession session, List<long> objects)
  {
    if ((IDBEditingContextsObject) session.GetObject(this.eco.EcoObjectID, false) == null)
      return;
    try
    {
      (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).DeleteFromContext((object) session.SessionGUID, this.eco.EcoObjectID, (IList<long>) objects, false, true);
    }
    catch
    {
    }
  }

  private bool _DoCreateVersion(long objID, List<PendingLink> plList, ECOGoal goal, int lcStepId)
  {
    bool version = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(objID);
      long verForThisContext = this.GetObjectVerForThisContext(session, dbObject);
      switch (verForThisContext)
      {
        case -1:
        case 0:
          break;
        default:
          long revRelation = RevHelper.GetRevRelation(this.eco.EcoObjectID, verForThisContext);
          string aver = "";
          if (revRelation != 0L)
          {
            IDBAttribute byId = session.GetRelation(revRelation).Attributes.FindByID(RevHelper.idAttrChangeNo);
            if (byId != null)
              aver = Convert.ToString(byId.Value);
            version = true;
          }
          PendingLink pendingLink = new PendingLink(verForThisContext, aver, goal, lcStepId);
          plList.Add(pendingLink);
          return version;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject1 = session.GetObject(objID);
      IDBObjectCollection objectCollection = session.GetObjectCollection(dbObject1.ObjectType);
      if (!(objectCollection is IClientDBObjectCollection))
        throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_381"));
      List<long> longList1 = new List<long>();
      List<bool> boolList = new List<bool>();
      List<long> longList2 = new List<long>();
      long objectID = 0;
      CreateVersionResult versionInternal = (objectCollection as IClientDBObjectCollection).CreateVersionInternal(dbObject1.ObjectID);
      IDBObject dbObject2 = session.GetObject(objID);
      try
      {
        List<ObjInfo> objInfoList = new List<ObjInfo>();
        for (int index = 0; index < versionInternal.SourceVersions.Count; ++index)
        {
          long fObjectId = versionInternal.TargetVersions[index].F_OBJECT_ID;
          objInfoList.Add(new ObjInfo(fObjectId, session));
          if (dbObject2.ObjectID == versionInternal.SourceVersions[index].F_OBJECT_ID)
          {
            objectID = fObjectId;
          }
          else
          {
            ObjectCheckOutVersionDescription targetVersion = versionInternal.TargetVersions[index];
            if (targetVersion.Mode == ObjectCheckedOutVersionMode.NewVersion)
            {
              ObjectCheckOutVersionDescription sourceVersion = versionInternal.SourceVersions[index];
              ReqRevisionInfo reqRevisionInfo = new ReqRevisionInfo(RevReqHelper.GetRevReq(sourceVersion.F_LCSTEP_ID, sourceVersion.F_OBJECT_TYPE));
              if (reqRevisionInfo.reqType != RequireClass.NoRequire)
              {
                longList1.Add(targetVersion.F_OBJECT_ID);
                boolList.Add(reqRevisionInfo.reqType == RequireClass.Require);
              }
            }
          }
        }
        if (objectID != 0L)
        {
          IDBObject dbObject3 = session.GetObject(objectID, false);
          string aver = "";
          PendingLink pendingLink1 = new PendingLink(Math.Abs(objectID), aver, goal, lcStepId);
          pendingLink1.hideType = HidingType.Disabled;
          pendingLink1.needDelete = true;
          if (objInfoList.Count > 1)
            pendingLink1.auxObjects = objInfoList;
          plList.Add(pendingLink1);
          this.eco.newVers.Add(Math.Abs(objectID));
          string g = dbObject3.ObjectGUID.ToString();
          version = true;
          for (int index = 0; index < longList1.Count; ++index)
          {
            long objectId = longList1[index];
            IDBObject dbObject4 = session.GetObject(objectId, false);
            if (dbObject4.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject4.CheckoutBy == 0L)
              objectId = dbObject4.CheckOut().ObjectID;
            if (dbObject3 != null)
            {
              long aobjID = objectId;
              this.eco.newVers.Add(Math.Abs(aobjID));
              PendingLink pendingLink2 = new PendingLink(aobjID, aver, goal, lcStepId);
              pendingLink2.hideType = !boolList[index] ? HidingType.Hidden : HidingType.Disabled;
              pendingLink2.needDelete = boolList[index];
              if (objInfoList.Count > 1)
                pendingLink2.auxObjects = objInfoList;
              plList.Add(pendingLink2);
              pendingLink2.mainGuid = new Guid(g);
            }
          }
        }
        versionInternal.NewObjectVersion.CommitCreation(true);
        versionInternal.Commit(session);
      }
      catch
      {
        versionInternal.Rollback(session);
        throw;
      }
    }
    return version;
  }

  private bool _DoCreateRelation(IUserSession session, PendingLink pl, long replaceObjId = 0)
  {
    long objVerID = replaceObjId != 0L ? replaceObjId : pl.verID;
    if (RevHelper.GetRevRelation(this.ecoID, objVerID) != 0L)
      return false;
    IDBRelation revRelation = RevHelper.CreateRevRelation(session, this.ecoID, objVerID, pl.verStr, pl.ecoGoal, pl.hideType);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    attributeValuesList.Add(new AttributeValues(RevHelper.idAttrDelWhenExcluded, (object) pl.needDelete));
    attributeValuesList.Add(new AttributeValues(RevHelper.idAttrFutureLC, (object) pl.stepID));
    if (pl.mainGuid != Guid.Empty)
      attributeValuesList.Add(new AttributeValues(RevHelper.idAttrMainObjectGuid, (object) pl.mainGuid.ToString()));
    if (pl.auxObjects != null && pl.auxObjects.Count > 0 && pl.mainGuid != Guid.Empty)
    {
      object[] array = pl.auxObjects.Select<ObjInfo, object>((System.Func<ObjInfo, object>) (obj => (object) Math.Abs(obj.verId))).ToArray<object>();
      attributeValuesList.Add(new AttributeValues(RevHelper.idAttrAuxLinks, FieldTypes.ftObjectLink, MultiValueModes.MultiValues, array));
    }
    revRelation.SetAttributesValues(attributeValuesList.ToArray());
    long relationId = revRelation.RelationID;
    IDBRelation relation = session.GetRelation(relationId);
    pl.relId = relation.RelationID;
    return true;
  }

  private string _AssignChangeNo(IUserSession ius, long objId, ECOGoal goal)
  {
    string str = "";
    IDBObject dbObject = ius.GetObject(objId, false);
    if (dbObject == null)
      return str;
    if (!this.NoChangeNums(goal))
    {
      str = this.GetNChangeNo(ius, dbObject.ID, objId, goal);
      IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
      if (dbAttribute != null)
        dbAttribute.Value = (object) str;
    }
    else
      dbObject.GetAttributeByID(RevHelper.idAttrChangeNo)?.Delete(0L);
    return str;
  }

  private long GetVerParmsOfArchiveECO(
    IUserSession ius,
    long verId,
    out bool needDelete,
    out string cNo,
    out List<long> auxObjects)
  {
    needDelete = false;
    auxObjects = (List<long>) null;
    cNo = "";
    long revRelation = RevHelper.GetRevRelation(Math.Abs(this.eco.EcoObjectID), verId);
    if (revRelation != 0L)
    {
      IDBRelation relation = ius.GetRelation(revRelation, false);
      if (relation == null)
        return 0;
      IDBAttribute attributeById1 = relation.GetAttributeByID(RevHelper.idAttrAuxLinks);
      if (attributeById1 != null && attributeById1.Values.Length != 0 && attributeById1.Values[0] != DBNull.Value)
      {
        auxObjects = new List<long>();
        foreach (object obj in attributeById1.Values)
          auxObjects.Add(Convert.ToInt64(obj));
      }
      IDBAttribute attributeById2 = relation.GetAttributeByID(RevHelper.idAttrDelWhenExcluded);
      if (attributeById2 != null)
        needDelete = attributeById2.AsBoolean;
      IDBAttribute attributeById3 = relation.GetAttributeByID(RevHelper.idAttrChangeNo);
      if (attributeById3 != null)
        cNo = Convert.ToString(attributeById3.Value);
    }
    return revRelation;
  }

  public void SynchronizeECODocumentWithDB(bool ReadOnly)
  {
    if (this.eco.EcoObjectID == -1L)
      return;
    this.eco.CheckEcoMainTable();
    int num1 = (int) this.TryActivateContext();
    IDBObject dbObject1 = (IDBObject) null;
    List<ECOPlugin.ECOInfo> ecoInfoList = ECOPlugin.LoadECOStructure(this.eco.EcoObjectID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      dbObject1 = sessionKeeper.Session.GetObject(this.eco.EcoObjectID);
      this.ReadECOVersion(dbObject1);
    }
    bool flag1 = false;
    for (int index = 0; index < ecoInfoList.Count; ++index)
    {
      ECOPlugin.ECOInfo ecoInfo = ecoInfoList[index];
      long objectId = ecoInfo.ObjectID;
      PendingLink pl = (PendingLink) null;
      ECOGoal goal = ECOGoal.NoGoal;
      int num2 = -1;
      bool flag2;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(ecoInfo.RelGuid, ecoInfo.ProjID, false);
        if (relation != null)
        {
          if (ecoInfo.flags.HasValue)
          {
            long? flags = ecoInfo.flags;
            long num3 = 1;
            long? nullable = flags.HasValue ? new long?(flags.GetValueOrDefault() & num3) : new long?();
            long num4 = 0;
            if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
              continue;
          }
          if (ecoInfo.newVerId.HasValue)
            objectId = ecoInfo.newVerId.Value;
          string aver = ecoInfo.changeNo ?? "1";
          goal = (ECOGoal) ((int) ecoInfo.goal ?? 0);
          string des = "";
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectId, false);
          if (objectActualCopy != null)
          {
            IDBAttribute attributeById = objectActualCopy.GetAttributeByID(RevHelper.idAttrDesign);
            if (attributeById != null)
              des = attributeById.AsString;
          }
          num2 = ecoInfo.futureStepId ?? -1;
          pl = new PendingLink(objectId, aver, goal, des);
          pl.needDelete = ecoInfo.needDelete;
          pl.stepID = num2;
          pl.mainGuid = ecoInfo.mainVerGuid != null ? new Guid(ecoInfo.mainVerGuid) : Guid.Empty;
          pl.hideType = (HidingType) ((int) ecoInfo.hideType ?? 0);
          pl.relId = relation.RelationID;
          flag2 = ECOPlugin.HasTerm(dbObject1);
          IDBAttribute attributeById1 = relation.GetAttributeByID(RevHelper.idAttrAuxLinks);
          if (attributeById1 != null)
          {
            if (attributeById1.Values != null)
            {
              foreach (object obj in attributeById1.Values)
              {
                if (obj != DBNull.Value)
                {
                  if (pl.auxObjects == null)
                    pl.auxObjects = new List<ObjInfo>();
                  pl.auxObjects.Add(new ObjInfo(Convert.ToInt64(obj), sessionKeeper.Session));
                }
              }
            }
          }
        }
        else
          continue;
      }
      this.eco.objLinks.Add(pl);
      if (pl.hideType == HidingType.Hidden)
        this.eco.hiddenLinks.Add(pl);
      else if (!ReadOnly && this.eco.FindEcoRow4Relation(pl.verGuid) == null)
      {
        if (pl.mainGuid != Guid.Empty)
        {
          TableData ecoRow4Relation = this.eco.FindEcoRow4Relation(pl.mainGuid);
          if (ecoRow4Relation != null)
          {
            List<long> idList = this.eco._GetIdList(ecoRow4Relation.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
            idList.Add(pl.verID);
            this.eco._SetIdList((RectangleElement) ecoRow4Relation, idList);
          }
        }
        else
        {
          IncludeGoal includeGoal = new IncludeGoal();
          List<long> objIDs = new List<long>();
          objIDs.Add(objectId);
          ECOGoal force = flag2 ? ECOGoal.Change : ECOGoal.NoGoal;
          if (includeGoal.Execute(objIDs, this.eco.litera, (List<long>) null, (List<long>) null, this.eco.revType, force))
          {
            pl.ecoGoal = includeGoal.goal;
            pl.stepID = includeGoal.selLCStepId;
            if (includeGoal.goal != goal || includeGoal.selLCStepId != num2)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBRelation relation = sessionKeeper.Session.GetRelation(ecoInfo.RelGuid, ecoInfo.ProjID, false);
                if (relation != null)
                {
                  IDBAttribute dbAttribute1 = relation.Attributes.AddAttribute(RevHelper.idAttrFutureLC, false);
                  if (dbAttribute1 != null)
                    dbAttribute1.AsInteger = (long) includeGoal.selLCStepId;
                  IDBAttribute dbAttribute2 = relation.Attributes.AddAttribute(RevHelper.idAttrIncludeGoal, false);
                  if (dbAttribute2 != null)
                    dbAttribute2.AsInteger = (long) includeGoal.goal;
                }
              }
            }
            flag1 = this.AddNewChange(objectId, pl) | flag1;
          }
        }
      }
    }
    if (!ReadOnly)
    {
      List<ECOEditorForm.ItemToDelete> itemToDeleteList = new List<ECOEditorForm.ItemToDelete>();
      int num5 = this.Document.Modified ? 1 : 0;
      this.eco.ecoMainTable.UniteTable();
      if (num5 == 0)
        this.Document.Modified = false;
      TableData dataOwner;
      for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
        bool flag3 = false;
        string attributeValue1 = node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
        DocumentTreeNode documentTreeNode = node.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.altPrimaryHeader) ?? node.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldChangeHeader2);
        List<int> intList = new List<int>();
        List<long> longList;
        if (documentTreeNode == null)
        {
          if (attributeValue1 == "")
            node.Tag = (object) DBNull.Value;
          longList = this.eco._GetIdList(attributeValue1);
          ECOEditorForm.SwitchToOneRowHeader(node);
        }
        else
        {
          longList = new List<long>();
          TableElement templateRecursive = documentTreeNode.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldDesign) as TableElement;
          for (int index = 0; index < templateRecursive.NodesCount; ++index)
          {
            if ((templateRecursive.Nodes[index] as TableElement).FindFirstNodeByName(Intermech.ECO.Client.ECO.fldChangeNumber) is TextData firstNodeByName)
            {
              string attributeValue2 = firstNodeByName.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
              if (attributeValue2 != "")
              {
                Guid objectGUID = new Guid(attributeValue2);
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                {
                  QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectGUID);
                  if (!objectInfo.Empty)
                  {
                    longList.Add(objectInfo.ObjectID);
                  }
                  else
                  {
                    flag3 = true;
                    intList.Add(index);
                  }
                }
              }
            }
          }
          ECOEditorForm.SwitchToTableHeader(node);
        }
        for (int index = longList.Count - 1; index >= 0; --index)
        {
          if (this.eco.ObjIdIndex(longList[index]) < 0 || this.eco.HiddenObjIdIndex(longList[index]) >= 0)
          {
            longList.RemoveAt(index);
            flag3 = true;
          }
        }
        if (longList.Count == 0 && attributeValue1 != "")
          itemToDeleteList.Add(new ECOEditorForm.ItemToDelete(dataPositionInFlow, dataOwner));
        else if (flag3)
        {
          this.eco._SetIdList((RectangleElement) node, longList);
          if (intList.Count > 0)
          {
            TableElement templateRecursive = documentTreeNode.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.fldDesign) as TableElement;
            for (int index = intList.Count - 1; index >= 0; --index)
              templateRecursive.RemoveChildNodeAt(intList[index], false, false, false);
          }
          this.UpdateMultiChangeHeader(node, longList, false, false);
        }
      }
      for (int index = itemToDeleteList.Count - 1; index >= 0; --index)
      {
        ECOEditorForm.ItemToDelete itemToDelete = itemToDeleteList[index];
        itemToDelete.tdata.RemoveChildNodeAt(itemToDelete.index, false, false);
        flag1 = true;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(this.eco.EcoObjectID);
        if (dbObject2 is IDBEditingContextsObject)
          this.eco.linkedContextNo = Math.Abs((dbObject2 as IDBEditingContextsObject).LinkedContextNumber);
      }
      if (flag1 || this.Document.NeedUpdateLayoutFlag)
        this.Document.UpdateLayout(0, false, true);
    }
    this.UndoManager.Clear();
  }

  private static void SwitchToTableHeader(TableData headerParent)
  {
    foreach (RectangleElement rectangleElement in headerParent.Template.Nodes.OfType<RectangleElement>().Where<RectangleElement>((System.Func<RectangleElement, bool>) (c => c.TableCellType == CellType.Header)))
    {
      if (rectangleElement.Id.StartsWith(Intermech.ECO.Client.ECO.fldChangeHeader2))
        headerParent.EnableHeader(rectangleElement.Id);
      else
        headerParent.DisableHeader(rectangleElement.Id);
    }
  }

  private static void SwitchToOneRowHeader(TableData headerParent)
  {
    foreach (RectangleElement rectangleElement in headerParent.Template.Nodes.OfType<RectangleElement>().Where<RectangleElement>((System.Func<RectangleElement, bool>) (c => c.TableCellType == CellType.Header)))
    {
      if (rectangleElement.Id.StartsWith(Intermech.ECO.Client.ECO.fldChangeHeader2))
        headerParent.DisableHeader(rectangleElement.Id);
      else
        headerParent.EnableHeader(rectangleElement.Id);
    }
  }

  public bool AddNewChange(long objID, PendingLink pl)
  {
    TableElement change = this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
    string attributeValue = this.eco.IdLists() ? Convert.ToString(objID) : pl.verGuid.ToString();
    if (change == null)
      return false;
    change.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
    TextData templateRecursive = change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) as TextData;
    templateRecursive.AssignText("", false, false, false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      if (objectActualCopy == null)
      {
        objID = -objID;
        objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      }
      if (objectActualCopy != null)
      {
        int attributeId = sessionKeeper.Session.IdentHelper.GetAttributeID("cad00770-306c-11d8-b4e9-00304f19f545");
        if (objectActualCopy.GetAttributeByID(attributeId) != null)
          templateRecursive.Text = Convert.ToString(objectActualCopy.VersionID);
      }
      (change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) as TextData).AssignText(this.eco.GetDocDesignationInECO(objectActualCopy), false, false, false);
    }
    List<long> idList = this.eco._GetIdList(Convert.ToString(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true)));
    bool flag = this.UpdateMultiChangeHeader((TableData) change, idList, false);
    this.UpdateDocDesign();
    return this.UpdateSpecText(change) | flag;
  }

  private bool NeedsChangeNo(int objTypeId)
  {
    return MetaDataHelper.GetAttribute4ObjectType(objTypeId, RevHelper.idAttrChangeNo) != null;
  }

  private IDBObject AssignChangeNo(IUserSession ius, IDBObject idbO, ECOGoal goal)
  {
    if (!this.NoChangeNums(goal) && this.NeedsChangeNo(idbO.ObjectType))
    {
      long objectId = idbO.ObjectID;
      IDBAttribute attributeById = idbO.GetAttributeByID(RevHelper.idAttrChangeNo);
      if (attributeById != null && attributeById.Value != DBNull.Value)
      {
        string sNum = attributeById.AsString.Trim();
        if (sNum != "" && (ius.GetCustomService(typeof (IECOServer)) as IECOServer).IsChangeNumUnique(idbO.ObjectID, sNum))
          return idbO;
      }
      string nchangeNo = this.GetNChangeNo(ius, idbO.ID, objectId, goal);
      return this.SetNewChangeNo(ius, objectId, nchangeNo, goal);
    }
    if (goal == ECOGoal.Creation)
    {
      IDBObject objectActualCopy = ius.GetObjectActualCopy(idbO.ObjectID, false);
      if (objectActualCopy != null)
        this._SetNewChangeNo(objectActualCopy, Intermech.ECO.Client.ECO.noChangeNumber, goal);
    }
    return idbO;
  }

  private IDBObject AssignChangeNo(
    IUserSession ius,
    IDBObject idbO,
    ECOGoal goal,
    out string cNo,
    bool forceNewNum = false)
  {
    if (!this.NoChangeNums(goal) && this.NeedsChangeNo(idbO.ObjectType))
    {
      long objectId = idbO.ObjectID;
      cNo = "";
      IDBAttribute attributeById = idbO.GetAttributeByID(RevHelper.idAttrChangeNo);
      if (attributeById != null && attributeById.Value != DBNull.Value)
      {
        cNo = attributeById.AsString.Trim();
        if (cNo != "" && !forceNewNum && (ius.GetCustomService(typeof (IECOServer)) as IECOServer).IsChangeNumUnique(idbO.ObjectID, cNo))
        {
          this.SetChangeNumForRelation(ius, idbO.ObjectID, cNo);
          return idbO;
        }
      }
      string nchangeNo = this.GetNChangeNo(ius, idbO.ID, objectId, goal);
      if (forceNewNum)
      {
        if (nchangeNo == cNo)
        {
          try
          {
            cNo = (Convert.ToInt32(nchangeNo) + 1).ToString();
            goto label_9;
          }
          catch (FormatException ex)
          {
            goto label_9;
          }
        }
      }
      cNo = nchangeNo;
label_9:
      this.SetChangeNumForRelation(ius, objectId, cNo);
      return this.SetNewChangeNo(ius, objectId, cNo, goal);
    }
    if (goal != ECOGoal.Annul)
      this.RemoveChangeNoAttr(ius, idbO.ObjectID);
    cNo = "";
    return idbO;
  }

  private void SetChangeNumForRelation(IUserSession ius, long objId, string cNo)
  {
    long aRelationID = 0;
    try
    {
      aRelationID = RevHelper.GetRevRelation(this.eco.EcoObjectID, objId);
    }
    catch (ObjectNotFoundException ex)
    {
    }
    if (aRelationID == 0L)
      return;
    IDBRelation relation = ius.GetRelation(aRelationID, false);
    if (relation == null)
      return;
    IDBAttribute byId = relation.Attributes.FindByID(RevHelper.idAttrChangeNo);
    if (byId == null)
      return;
    byId.AsString = cNo;
  }

  private void RemoveChangeNoAttr(IUserSession ius, long objId)
  {
    IDBObject objectActualCopy = ius.GetObjectActualCopy(objId, false);
    if (objectActualCopy != null)
    {
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(RevHelper.idAttrChangeNo);
      if (attributeById != null && !this.ChangeNumAllowedWithoutCheckout(objectActualCopy) && objId > 0L && objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        IDBObject dbObject = objectActualCopy.CheckOut();
        attributeById = dbObject.GetAttributeByID(RevHelper.idAttrChangeNo);
        objId = dbObject.ObjectID;
      }
      if (attributeById != null)
        attributeById.Value = (object) DBNull.Value;
    }
    long aRelationID = 0;
    try
    {
      aRelationID = RevHelper.GetRevRelation(this.eco.EcoObjectID, objId);
    }
    catch (ObjectNotFoundException ex)
    {
    }
    if (aRelationID == 0L)
      return;
    IDBRelation relation = ius.GetRelation(aRelationID, false);
    if (relation == null)
      return;
    IDBAttribute byId = relation.Attributes.FindByID(RevHelper.idAttrChangeNo);
    if (byId == null)
      return;
    byId.Value = (object) DBNull.Value;
  }

  private List<long> GetAllVersions(IUserSession ius, long objId) => ius.GetObjectIDVersions(objId);

  public string GetNChangeNo(IUserSession ius, long ID, long objId, ECOGoal goal)
  {
    return this.NoChangeNums(goal) ? "" : (ius.GetCustomService(typeof (IECOServer)) as IECOServer).GetNewChangeNo(ID, objId).ToString();
  }

  private bool IsChangeNumUnique(IUserSession ius, long objId, string changeNo)
  {
    return (ius.GetCustomService(typeof (IECOServer)) as IECOServer).IsChangeNumUnique(objId, changeNo);
  }

  private IDBObject SetNewChangeNo(IUserSession ius, long objId, string newChangeNo, ECOGoal goal = ECOGoal.Change)
  {
    IDBObject objectActualCopy = ius.GetObjectActualCopy(objId, false);
    if (objectActualCopy == null)
      return (IDBObject) null;
    return this.NoChangeNums(goal) ? objectActualCopy : this._SetNewChangeNo(objectActualCopy, newChangeNo, goal);
  }

  private IDBObject _SetNewChangeNo(IDBObject idbO, string newChangeNo, ECOGoal goal = ECOGoal.Change)
  {
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(idbO.ObjectType);
    if (objectTypeParentsId.IndexOf(idbO.ObjectType) < 0)
      objectTypeParentsId.Add(idbO.ObjectType);
    bool flag = false;
    switch (idbO.ObjectModifyMode)
    {
      case ObjectModifyModes.Checkout:
        if (idbO.ObjectID > 0L && !this.ChangeNumAllowedWithoutCheckout(idbO))
        {
          idbO = idbO.CheckOut(false);
          flag = true;
          break;
        }
        break;
    }
    if (idbO != null)
    {
      try
      {
        IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
        if (dbAttribute != null)
          dbAttribute.AsString = newChangeNo;
      }
      catch (KernelExceptionID ex)
      {
      }
      if (flag)
      {
        try
        {
          idbO.CheckIn();
        }
        catch (AVSCheckInException ex)
        {
          ExceptionHelper.ExceptionService.ShowException((Exception) ex);
        }
      }
    }
    return idbO;
  }

  private bool NoChangeNums(ECOGoal goal = ECOGoal.Change)
  {
    return this.eco.revType == RevType.PI || this.eco.revType == RevType.PR || goal == ECOGoal.Creation;
  }

  public TableElement elCurChange
  {
    get => this._elCurChange;
    set => this._elCurChange = value;
  }

  private bool IsInWorkspace()
  {
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null && pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldWorkspace)
      {
        if (this.elWorkspace == null)
          this.elWorkspace = pageElementUi.Element as TableElement;
        return true;
      }
    }
    return false;
  }

  private bool IsElement(DocumentTreeNode node)
  {
    if (node == null)
      return false;
    if (node.TemplateId == Intermech.ECO.Client.ECO.fldVar1)
      return true;
    return !node.Template.CloneByTemplateWithParent && node.TemplateId != Intermech.ECO.Client.ECO.fldChange;
  }

  private bool IsSpecText(DocumentTreeNode node)
  {
    return node != null && node.Id.StartsWith(Intermech.ECO.Client.ECO.idSpecText);
  }

  private void ClearSelection()
  {
    this.elCurChange = (TableElement) null;
    this.elCurElem = (TableElement) null;
    this.elPicture = (ContainerElement) null;
    this.indexCurChange = -1;
    this.indexCurElem = -1;
  }

  private bool IsWorkspaceChild(DocumentTreeNode node)
  {
    if (node == null)
      return false;
    return node.Name == Intermech.ECO.Client.ECO.fldWorkspace || this.IsWorkspaceChild(node.Parent);
  }

  private void GetCurrents(Point p)
  {
    this.ClearSelection();
    if (this.elWorkspace == null)
      return;
    int num1 = -1;
    bool flag = false;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null && pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldWorkspace)
        flag = true;
    }
    if (flag)
    {
      for (int index = this.items.Count - 1; index >= 0; --index)
      {
        PageElementNode element = this.items[index].Element;
        if (element != null && element.Name != Intermech.ECO.Client.ECO.fldWorkspace && !this.IsWorkspaceChild(element.Parent))
          this.items.RemoveAt(index);
      }
    }
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null)
      {
        if (pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldChange)
        {
          this.elCurChange = pageElementUi.Element as TableElement;
          foreach (DocumentTreeNode node in this.elWorkspace.Nodes)
          {
            ++num1;
            TableElement elCurChange = this.elCurChange;
            if (node == elCurChange)
              this.indexCurChange = num1;
          }
        }
        else if (pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldWorkspace)
          flag = true;
        else if (pageElementUi.Element is ContainerData)
          this.elPicture = pageElementUi.Element as ContainerElement;
      }
    }
    if (this.elCurChange == null)
    {
      if (!flag)
        return;
      this.indexCurElem = int.MinValue;
    }
    else
    {
      int num2 = -1;
      foreach (PageElementUI pageElementUi in this.items)
      {
        if (pageElementUi.Element != null)
        {
          if (pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldChangeHeader)
            break;
          if (this.IsElement((DocumentTreeNode) pageElementUi.Element) || this.IsSpecText((DocumentTreeNode) pageElementUi.Element))
          {
            this.elCurElem = pageElementUi.Element as TableElement;
            Rectangle bounds = this.elCurElem.PageUI.Bounds;
            IEnumerator enumerator = this.elCurElem.Parent.Nodes.GetEnumerator();
            try
            {
              while (enumerator.MoveNext())
              {
                DocumentTreeNode current = (DocumentTreeNode) enumerator.Current;
                ++num2;
                TableElement elCurElem = this.elCurElem;
                if (current == elCurElem)
                {
                  this.indexCurElem = num2;
                  this.rightPart = p.X > bounds.Left + bounds.Width / 2;
                  break;
                }
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable disposable)
                disposable.Dispose();
            }
          }
        }
      }
    }
  }

  private void LoadNavigatorMenuItemImages(MenuBarItem contextMenu)
  {
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) contextMenu.Items)
      this.LoadNavigatorMenuItemImages(menuButtonItem, ECOPlugin.NamedImageList);
  }

  private void LoadNavigatorMenuItemImages(
    MenuButtonItem menuButtonItem,
    INamedImageList iNamedImageList)
  {
    if (menuButtonItem.ImageIndex != -1 && menuButtonItem.Image == null && menuButtonItem.Icon == null)
      menuButtonItem.Image = iNamedImageList.ImageList.Images[menuButtonItem.ImageIndex];
    if (!menuButtonItem.HasChildren)
      return;
    foreach (MenuButtonItem menuButtonItem1 in (CollectionBase) menuButtonItem.Items)
      this.LoadNavigatorMenuItemImages(menuButtonItem1, iNamedImageList);
  }

  private void MakeContMenu(List<MenuButtonItem> al)
  {
    List<long> longList = new List<long>();
    if (this.elCurChange != null || EcoTreeViewDlg.TreeMenu)
    {
      int num1 = 0;
      if (this.ecoTreeViewDlg != null && this.ecoTreeViewDlg.Selected.Count == 1 && this.ecoTreeViewDlg.Selected[0].Node != null && this.ecoTreeViewDlg.Selected[0].Node.Name == Intermech.ECO.Client.ECO.fldChange)
        this.elCurChange = this.ecoTreeViewDlg.Selected[0].Node as TableElement;
      if (this.elCurChange != null)
        longList = this.eco._GetIdList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      if (EcoTreeViewDlg.TreeMenu)
      {
        longList = new List<long>();
        foreach (ECOTreeItem ecoTreeItem in this.ecoTreeViewDlg.Selected)
        {
          if (ecoTreeItem.Node != null)
          {
            string attributeValue = ecoTreeItem.Node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
            longList.AddRange((IEnumerable<long>) this.eco._GetIdList(attributeValue));
          }
          else
            longList.Add(ecoTreeItem.Info.ObjectID);
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = longList.Count - 1; index >= 0; --index)
        {
          long objectID = longList[index];
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
          if (objectActualCopy == null)
          {
            longList.RemoveAt(index);
          }
          else
          {
            longList[index] = objectActualCopy.ObjectID;
            ++num1;
          }
        }
      }
      int num2 = 0;
      if (num1 > 20)
      {
        num2 = (num1 + 20 - 1) / 20;
        int num3 = 0;
        for (int index = 0; index < num2; ++index)
        {
          string str = $"{LocalizationHolder.rm.GetString("ECO.Client_227")} ({Convert.ToString(num3 + 1)}-";
          MenuButtonItem menuButtonItem = new MenuButtonItem(num3 + 20 <= num1 ? $"{str}{Convert.ToString(num3 + 20)})" : $"{str}{Convert.ToString(num1)})");
          al.Add(menuButtonItem);
          num3 += 20;
        }
      }
      IList list = (IList) al;
      if (num2 > 0)
        list = (IList) al[0].Items;
      LogManager.AddLine("============= before adding menu items: " + this.FormCurrTime(), true);
      for (int index1 = 0; index1 < longList.Count; ++index1)
      {
        long objId = longList[index1];
        int index2 = this.ECO.ObjIdIndex(objId);
        if (index2 >= 0)
        {
          PendingLink objLink = this.eco.objLinks[index2];
          if (objLink.design == null)
            objLink.UpdateDesign();
          MenuButtonItem menuButtonItem1 = new MenuButtonItem(objLink.design);
          long num4 = objId;
          ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications);
          AdvancedServiceContainer services = new AdvancedServiceContainer();
          services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
          MenuBarItem menu = Intermech.Navigator.ContextMenu.Services.GetMenu(ObjectExtensions.GetItems(new long[1]
          {
            num4
          }, (System.IServiceProvider) services), (System.IServiceProvider) this._navigatorViewServices);
          this.LoadNavigatorMenuItemImages(menu);
          if (menu.HasChildren)
          {
            foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) menu.Items)
            {
              MenuButtonItem menuButtonItem2 = (MenuButtonItem) toolbarItemBase.CloneItem();
              menuButtonItem2.Click += new EventHandler(this.menuButtonItem_Click);
              menuButtonItem2.Shortcut = Shortcut.None;
              menuButtonItem1.Items.Add((ToolbarItemBase) menuButtonItem2);
            }
          }
          if (num2 > 0 && index1 > 0 && index1 % 20 == 0)
            list = (IList) al[index1 / 20].Items;
          list.Add((object) menuButtonItem1);
        }
      }
      LogManager.AddLine("============= after adding menu items: " + this.FormCurrTime(), true);
    }
    if (longList == null || longList.Count == 0)
      this.plugin.NavigatorMenuItems = (ISelectedItems) null;
    else
      this.plugin.NavigatorMenuItems = ObjectExtensions.GetItems(longList.ToArray());
    this.plugin.UpdateISimpleSelectedItemsService();
    LogManager.AddLine("============= NavMenuItems updated: " + this.FormCurrTime(), true);
    Assembly assembly = this.GetType().Assembly;
    string str1 = "Intermech.ECO.Client.Resources.";
    System.IServiceProvider serviceProvider = ECOPlugin.serviceProvider;
    if (this.elCurChange != null)
    {
      if (this.copyAllElems == null)
      {
        this.copyAllElems = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_405"), new EventHandler(this.cmdCopyAllElems));
        this.copyAllElems.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOCopyAllElems.png");
        this.copyAllElems.BeginGroup = true;
      }
      this.copyAllElems.Enabled = this.elCurChange.NodesCount > 1;
      al.Add(this.copyAllElems);
      if (this.copyTable == null)
      {
        this.copyTable = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_416"), new EventHandler(this.cmdCopyTable));
        this.copyTable.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOCopyTable.png");
      }
      this.copyTable.Enabled = this.elCurElem != null && this.elCurElem.TemplateId == Intermech.ECO.Client.ECO.fldTable;
      al.Add(this.copyTable);
      if (this.pasteAllElems == null)
      {
        this.pasteAllElems = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_406"), new EventHandler(this.cmdPasteAllElems));
        this.pasteAllElems.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOPasteElems.png");
      }
      if (!this.ReadOnly && NodeClipboardHelper.CanPasteFromClipboard((DocumentTreeNode) this.elCurChange))
        this.pasteAllElems.Enabled = NodeClipboardHelper.GetClipboardInfo().Tag.Equals((object) "RevEditor");
      else
        this.pasteAllElems.Enabled = false;
      al.Add(this.pasteAllElems);
      if (this.launchScrShooter == null)
      {
        this.launchScrShooter = new MenuButtonItem("Снимок экрана", new EventHandler(this.cmdLaunchShooter));
        this.launchScrShooter.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ScrCapture.png");
      }
      this.launchScrShooter.Enabled = this.IsLaunchEnabled();
      al.Add(this.launchScrShooter);
      if (!this.ReadOnly)
      {
        if (this.moveElemUp == null)
          this.moveElemUp = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_419"), new EventHandler(this.cmdMoveElemUp));
        this.moveElemUp.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "Up.png");
        this.moveElemUp.Enabled = this.elCurElem != null;
        al.Add(this.moveElemUp);
        if (this.moveElemDown == null)
          this.moveElemDown = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_420"), new EventHandler(this.cmdMoveElemDown));
        this.moveElemDown.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "Down.png");
        this.moveElemDown.Enabled = this.elCurElem != null;
        al.Add(this.moveElemDown);
        if (this.elCurElem != null)
        {
          RectangleElement prevDataCell = this.elCurElem.FindPrevDataCell();
          RectangleElement nextDataCell = this.elCurElem.FindNextDataCell();
          this.moveElemUp.Enabled = prevDataCell != null && prevDataCell.FindPrevDataCell() != null;
          this.moveElemDown.Enabled = nextDataCell != null;
        }
        else
        {
          this.moveElemUp.Enabled = false;
          this.moveElemDown.Enabled = false;
        }
      }
    }
    if (this.ReadOnly)
      return;
    if (this.elPicture != null)
    {
      if (this.selPictFromBase == null)
      {
        this.selPictFromBase = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_191"), new EventHandler(this.InsPictFromObject));
        this.selPictFromBase.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOImgFromObj.png");
        if (this.elCurChange != null)
          this.selPictFromBase.BeginGroup = true;
      }
      al.Add(this.selPictFromBase);
      if (this.selPictFromFile == null)
      {
        this.selPictFromFile = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_192"), new EventHandler(this.InsPictFromFile));
        this.selPictFromFile.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOImgFromFile.png");
      }
      al.Add(this.selPictFromFile);
      if (this.selPictFromClip == null)
      {
        this.selPictFromClip = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_193"), new EventHandler(this.InsPictFromClip));
        this.selPictFromClip.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOImgFromClip.png");
      }
      al.Add(this.selPictFromClip);
      if (this.createOLEPict == null)
      {
        this.createOLEPict = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_194"), new EventHandler(this.CreateOLEPict));
        this.createOLEPict.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOCreateOLE.png");
      }
      al.Add(this.createOLEPict);
      if (this.originalSize == null)
      {
        this.originalSize = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_275"), new EventHandler(this.ToggleOriginalSize));
        this.originalSize.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOOrigSize.png");
      }
      al.Add(this.originalSize);
      this.originalSize.Checked = this.elPicture.ScaleMode != ImageScaleMode.FitWidthHeight;
    }
    if (this.includeMenu == null)
    {
      this.includeMenu = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_195"));
      this.includeMenu.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOInclude.png");
      this.includeMenu.Items.Add(LocalizationHolder.rm.GetString("ECO.Client_196"), new EventHandler(this.cmdIncludeDocs));
      this.includeMenu.Items.Add(LocalizationHolder.rm.GetString("ECO.Client_198"), new EventHandler(this.cmdIncludeExternalDoc));
    }
    this.includeMenu.BeginGroup = this.elPicture != null || this.elCurChange != null;
    al.Add(this.includeMenu);
    if (this.removeDocs == null)
      this.removeDocs = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_199"), new EventHandler(this.cmdRemoveDocs));
    this.removeDocs.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOExclude.png");
    this.removeDocs.Enabled = this.elCurChange != null && this.elWorkspace != null || EcoTreeViewDlg.TreeMenu && this.ecoTreeViewDlg.Selected.Count > 0 && this.ecoTreeViewDlg.Selected[0].Id != 0L;
    if (this.removeDocs.Enabled)
      al.Add(this.removeDocs);
    if (this.splitChange == null)
      this.splitChange = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_360"), new EventHandler(this.cmdSplitChange));
    this.splitChange.Enabled = EcoTreeViewDlg.TreeMenu && this.ecoTreeViewDlg.Selected.Count > 0 && this.ecoTreeViewDlg.Selected[0].Id != 0L && this.ecoTreeViewDlg.Selected[0].ParentItem != null && this.ecoTreeViewDlg.Selected[0].ParentItem.ChildItems.Count > 1 && this.ecoTreeViewDlg.Selected[0].HidingType != HidingType.Hidden;
    if (this.splitChange.Enabled)
      al.Add(this.splitChange);
    if (this.hideChange == null)
      this.hideChange = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_396"), new EventHandler(this.cmdHide));
    this.hideChange.Enabled = false;
    if (EcoTreeViewDlg.TreeMenu && this.ecoTreeViewDlg.Selected.Count > 0 && ECOPlugin.plugin.eps.Current.ShowHidden)
    {
      this.hideChange.Enabled = false;
      foreach (ECOTreeItem ecoTreeItem in this.ecoTreeViewDlg.Selected)
      {
        if (ecoTreeItem.Id != 0L)
        {
          switch (this.GetHidingType(ecoTreeItem.Info.ObjectID))
          {
            case HidingType.Disabled:
              this.hideChange.Enabled = false;
              continue;
            case HidingType.CanBeHidden:
              this.hideChange.Enabled = true;
              continue;
            case HidingType.Hidden:
              this.hideChange.Enabled = false;
              continue;
            default:
              continue;
          }
        }
        else
          this.hideChange.Enabled = false;
      }
    }
    if (this.hideChange.Enabled)
      al.Add(this.hideChange);
    if (this.showChange == null)
      this.showChange = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_397"), new EventHandler(this.cmdShow));
    this.showChange.Enabled = false;
    if (EcoTreeViewDlg.TreeMenu && this.ecoTreeViewDlg.Selected.Count > 0 && ECOPlugin.plugin.eps.Current.ShowHidden)
    {
      this.showChange.Enabled = false;
      foreach (ECOTreeItem ecoTreeItem in this.ecoTreeViewDlg.Selected)
      {
        if (ecoTreeItem.Id != 0L)
        {
          switch (this.GetHidingType(ecoTreeItem.Info.ObjectID))
          {
            case HidingType.Disabled:
              this.showChange.Enabled = false;
              continue;
            case HidingType.CanBeHidden:
              this.showChange.Enabled = false;
              continue;
            case HidingType.Hidden:
              this.showChange.Enabled = true;
              continue;
            default:
              continue;
          }
        }
        else
          this.showChange.Enabled = false;
      }
    }
    if (this.showChange.Enabled)
      al.Add(this.showChange);
    if (this.includeElem == null)
    {
      this.includeElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_200"));
      this.includeElem.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOAddElem.png");
      this.includeElem.BeginGroup = true;
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar1) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar1, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar2) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar2, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar3) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar3, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar4) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar4, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar5) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar5, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar6) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar6, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar7) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar7, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldVar8) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.fldVar8, new EventHandler(this.cmdIncludeElem));
      if (this.eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.fldTable) != null)
        this.includeElem.Items.Add(Intermech.ECO.Client.ECO.cmdInsertTable, new EventHandler(this.cmdIncludeTable));
    }
    if (this.includeElem.Items.Count > 0)
    {
      this.includeElem.Enabled = this.elCurChange != null || this.GetLastChange() != null && !EcoTreeViewDlg.TreeMenu;
      al.Add(this.includeElem);
    }
    if (this.includeElem != null)
    {
      string str2 = LocalizationHolder.rm.GetString("ECO.Client_200");
      Image image = (Image) null;
      if (this.indexCurElem != int.MinValue)
      {
        if (this.rightPart)
        {
          str2 += LocalizationHolder.rm.GetString("ECO.Client_299");
          image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOAddElemAfter.png");
        }
        else
        {
          str2 += LocalizationHolder.rm.GetString("ECO.Client_298");
          image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOAddElemBefore.png");
        }
      }
      this.includeElem.Text = str2;
      this.includeElem.Image = image;
    }
    if (this.removeElem == null)
      this.removeElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_201"), new EventHandler(this.cmdRemoveElem));
    this.removeElem.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECODelElem.png");
    this.removeElem.Enabled = this.elCurChange != null && this.elCurElem != null && this.elCurElem.Id != Intermech.ECO.Client.ECO.idSpecText;
    al.Add(this.removeElem);
    if (this.elCurChange != null)
    {
      if (this.sortChange == null)
      {
        this.sortChange = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_202"), new EventHandler(this.cmdSortChange));
        this.sortChange.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOSortChange.png");
        this.sortChange.BeginGroup = true;
      }
      al.Add(this.sortChange);
    }
    if (this.textFld != null && !this.textFld.ReadOnly && this.elCurChange != null)
    {
      if (this.insertTemplate == null)
      {
        this.insertTemplate = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_262"), new EventHandler(this.cmdInsertTemplate));
        this.insertTemplate.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOInsSpecSymbol.png");
      }
      al.Add(this.insertTemplate);
      if (ECOPlugin.ImbaseSelector != null)
      {
        if (this.insertImBase == null)
        {
          this.insertImBase = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_441"), new EventHandler(this.cmdInsertImBase));
          this.insertImBase.Image = ECOPlugin.NamedImageList.ImageList.Images[ECOPlugin.NamedImageList.ImageIndex("imgImbaseTablesRefType")];
          this.insertImBase.BeginGroup = true;
        }
        al.Add(this.insertImBase);
      }
    }
    if (this.elCurChange != null)
    {
      if (this.fromNewItem == null)
      {
        this.fromNewItem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_342"), new EventHandler(this.cmdFromPageTop));
        this.fromNewItem.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOFromNewPage.png");
        this.fromNewItem.BeginGroup = true;
      }
      TableElement tableElement1 = this.elCurChange;
      if (this.elCurElem != null)
      {
        TableElement tableElement2 = this.elCurElem;
        while (tableElement2.Parent != null && tableElement2.Parent.TemplateId != Intermech.ECO.Client.ECO.fldChange)
          tableElement2 = tableElement2.Parent as TableElement;
        if (tableElement2.Parent != null)
          tableElement1 = tableElement2;
      }
      this.fromNewItem.Checked = tableElement1.FromNewPage;
      al.Add(this.fromNewItem);
      if (this.alwaysTableItem == null)
      {
        this.alwaysTableItem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_456"), new EventHandler(this.cmdAlwaysTable));
        this.alwaysTableItem.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOAlwaysTable.png");
        this.alwaysTableItem.BeginGroup = true;
      }
      this.alwaysTableItem.Checked = this.elCurChange != null && this.elCurChange.GetAttributeValue("AlwaysTable", true) == "yes";
      this.alwaysTableItem.Enabled = this.elCurChange != null;
      al.Add(this.alwaysTableItem);
    }
    if (this.elCurChange != null)
    {
      ECOGoal changeGoal = this.eco.GetChangeGoal(this.elCurChange);
      if (changeGoal != ECOGoal.NoGoal)
      {
        this.changeGoal = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_402"), new EventHandler(this.cmdChangeGoal));
        this.changeGoal.Image = DocumentMenuHelper.LoadImageFromResurces(assembly, str1 + "ECOChangeGoal.png");
        this.changeGoal.Enabled = changeGoal != ECOGoal.Annul;
        al.Add(this.changeGoal);
      }
    }
    if (this.refreshElem == null)
    {
      this.refreshElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_379"), new EventHandler(this.cmdRefresh));
      this.refreshElem.Image = ECOPlugin.NamedImageList.ImageList.Images[ECOPlugin.NamedImageList.ImageIndex("imgRefresh")];
    }
    this.refreshElem.Enabled = EcoTreeViewDlg.TreeMenu;
    if (!this.refreshElem.Enabled)
      return;
    al.Add(this.refreshElem);
  }

  private void menuButtonItem_Click(object sender, EventArgs e)
  {
    int num = (int) this.TryActivateContext();
  }

  internal string FormCurrTime()
  {
    DateTime now = DateTime.Now;
    return $"{now.ToLongTimeString()}.{now.Millisecond.ToString()}";
  }

  protected override void MyGetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    LogManager.AddLine("============= MyGetCustomElementContextMenu started: " + this.FormCurrTime(), true);
    e.ContextMenuItems.Clear();
    Intermech.Document.UI.PageControl pageControl = (sender as DocumentControl).ActivePage.PageControl;
    this.items.Clear();
    Point client = pageControl.PointToClient(Control.MousePosition);
    pageControl.GetPageElementUIAtPoint(client, this.items);
    this.textFld = (TextData) null;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null && pageElementUi.Element is TextData)
      {
        this.textFld = pageElementUi.Element as TextData;
        break;
      }
    }
    if (this.IsInWorkspace() || EcoTreeViewDlg.TreeMenu)
    {
      this.GetCurrents(client);
      LogManager.AddLine("============= before MyGetCustomElementContextMenu: " + this.FormCurrTime(), true);
      this.MakeContMenu(e.ContextMenuItems);
      LogManager.AddLine("============= after MyGetCustomElementContextMenu: " + this.FormCurrTime(), true);
    }
    else if (!this.ReadOnly)
    {
      if (this.textFld != null && (this.textFld.Template.Id == Intermech.ECO.Client.ECO.idShifr || this.textFld.Template.Id == Intermech.ECO.Client.ECO.idReason))
      {
        if (this.editElem == null)
          this.editElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_203"), new EventHandler(this.cmdEdit));
        e.ContextMenuItems.Add(this.editElem);
      }
      if (this.textFld != null && (this.textFld.Template.Id == Intermech.ECO.Client.ECO.idZadel1 || this.textFld.Template.Id == Intermech.ECO.Client.ECO.idZadel2))
      {
        if (this.editElem == null)
          this.editElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_204"), new EventHandler(this.cmdEdit));
        e.ContextMenuItems.Add(this.editElem);
      }
      if (this.textFld != null && this.textFld.Template.Id == Intermech.ECO.Client.ECO.idUsability)
      {
        if (this.usabElem == null)
          this.usabElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_205"), new EventHandler(this.cmdUsability));
        e.ContextMenuItems.Add(this.usabElem);
      }
      if (this.textFld != null && this.textFld.Template.Id == Intermech.ECO.Client.ECO.idSendTo && ArchivesClientStartup.Initialize)
      {
        if (this.sendToElem == null)
          this.sendToElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_331"), new EventHandler(this.cmdWriteSubscribers));
        e.ContextMenuItems.Add(this.sendToElem);
      }
    }
    if (this.textFld != null && this.textFld is TextBoxElement)
    {
      TextBoxElement textFld = (TextBoxElement) this.textFld;
      if (this.copyElem == null)
        this.copyElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_206"), new EventHandler(this.cmdCopy));
      if (this.cutElem == null)
      {
        this.cutElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_207"), new EventHandler(this.cmdCut));
        this.cutElem.BeginGroup = true;
      }
      if (this.pasteElem == null)
        this.pasteElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_208"), new EventHandler(this.cmdPaste));
      e.ContextMenuItems.Add(this.cutElem);
      e.ContextMenuItems.Add(this.copyElem);
      e.ContextMenuItems.Add(this.pasteElem);
      INamedImageList service = (INamedImageList) ECOPlugin.serviceProvider.GetService(typeof (INamedImageList));
      int index1 = service.ImageIndex("imgCopy");
      this.copyElem.Image = service.ImageList.Images[index1];
      int index2 = service.ImageIndex("imgCut");
      this.cutElem.Image = service.ImageList.Images[index2];
      int index3 = service.ImageIndex("imgPaste");
      this.pasteElem.Image = service.ImageList.Images[index3];
      string str = textFld.InPlaceEditorControl == null ? this.textFld.Text : ((ImRtfEditor) textFld.InPlaceEditorControl).TerGetTextSel();
      this.copyElem.Enabled = str != "" && str != null;
      this.cutElem.Enabled = !this.ReadOnly && this.copyElem.Enabled && !this.textFld.ReadOnly;
      bool flag = false;
      try
      {
        flag = Clipboard.ContainsText();
      }
      catch
      {
      }
      this.pasteElem.Enabled = ((this.ReadOnly ? 0 : (!this.textFld.ReadOnly ? 1 : 0)) & (flag ? 1 : 0)) != 0 && ((TextBoxElement) this.textFld).InPlaceEditorControl != null;
    }
    this.EnableContextMenu();
    LogManager.AddLine("============= MyGetCustomElementContextMenu ended: " + this.FormCurrTime(), true);
  }

  private void EnableContextMenu()
  {
    if (this.elPicture == null || this.selPictFromClip == null)
      return;
    this.selPictFromClip.Enabled = this.IsPictInClip();
  }

  private void cmdWriteSubscribers(object sender, EventArgs e)
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      long ecoDeliveryListId = this.GetECODeliveryListID(sk);
      if (ecoDeliveryListId == 0L)
        return;
      using (AddSubscriberForm addSubscriberForm = new AddSubscriberForm(new List<long>()
      {
        ecoDeliveryListId
      }, true))
      {
        if (addSubscriberForm.ShowDialog() != DialogResult.OK)
          return;
        List<string> abonList = Intermech.ECO.Client.ECO.GetAbonList(ecoDeliveryListId);
        string str = "";
        if (abonList != null)
          str = ECOPlugin.FormatAbonents(abonList);
        this.UndoManager.BeginCreateMultyUndo("Изменение абонентов");
        try
        {
          DocumentTreeNode templateRecursive = this.eco.DocumentECO.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSendTo);
          if (!(templateRecursive is TextData))
            return;
          TextData textData = templateRecursive as TextData;
          textData.AssignText(str, false, true, false);
          textData.SetAttributeValue("ChangedByUser", "Changed");
        }
        finally
        {
          this.UndoManager.EndCreateMultyUndo();
        }
      }
    }
  }

  private void cmdUsability(object sender, EventArgs e)
  {
    this.AddUsability((DocumentTreeNode) this.textFld);
  }

  private void cmdSeriesDates(object sender, EventArgs e)
  {
    this.SelectSeriesDates((DocumentTreeNode) this.textFld);
  }

  private void cmdEdit(object sender, EventArgs e)
  {
    if (this.textFld.Id == Intermech.ECO.Client.ECO.idReason || this.textFld.Id == Intermech.ECO.Client.ECO.idShifr)
      this.SelReason((DocumentTreeNode) this.textFld);
    if (!(this.textFld.Id == Intermech.ECO.Client.ECO.idZadel1) && !(this.textFld.Id == Intermech.ECO.Client.ECO.idZadel2))
      return;
    this.SelZadel((DocumentTreeNode) this.textFld);
  }

  private void cmdRefresh(object sender, EventArgs e)
  {
    if ((this.ecoTreeViewDlg == null ? 0 : (this.ecoTreeViewDlg.ContainsFocus ? 1 : 0)) == 0)
      return;
    this.ecoTreeViewDlg.UpdateTree();
  }

  private void cmdCopy(object sender, EventArgs e)
  {
    ICommandState command = ECOPlugin.plugin.CommandManager.FindCommand("Copy");
    ECOPlugin.plugin.CommandManager.Execute(command);
  }

  private void cmdCopyTable(object sender, EventArgs e)
  {
    NodeClipboardHelper.CopyToClipboard(new DocumentTreeNode[1]
    {
      (DocumentTreeNode) this.elCurElem
    }, IntPtr.Zero, (object) "RevEditor");
  }

  private void cmdCut(object sender, EventArgs e)
  {
    ICommandState command = ECOPlugin.plugin.CommandManager.FindCommand("Cut");
    ECOPlugin.plugin.CommandManager.Execute(command);
  }

  private void cmdPaste(object sender, EventArgs e)
  {
    ICommandState command = ECOPlugin.plugin.CommandManager.FindCommand("Paste");
    ECOPlugin.plugin.CommandManager.Execute(command);
  }

  private void cmdTestSpec(object sender, EventArgs e)
  {
  }

  private void cmdSortChange(object sender, EventArgs e) => this.SortByDes(this.elCurChange);

  private void cmdFromPageTop(object sender, EventArgs e)
  {
    if (this.elCurChange == null)
      return;
    this.UndoManager.BeginCreateMultyUndo("Изменить статус 'с нового листа'");
    try
    {
      TableElement tableElement1 = this.elCurChange;
      if (this.elCurElem != null)
      {
        TableElement tableElement2 = this.elCurElem;
        while (tableElement2.Parent != null && tableElement2.Parent.TemplateId != Intermech.ECO.Client.ECO.fldChange)
          tableElement2 = tableElement2.Parent as TableElement;
        if (tableElement2.Parent != null)
          tableElement1 = tableElement2;
      }
      tableElement1.FromNewPage = !tableElement1.FromNewPage;
      this.Document.UpdateLayout(true, true);
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  private void cmdAlwaysTable(object sender, EventArgs e)
  {
    if (this.elCurChange == null)
      return;
    string attributeValue = this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
    if (attributeValue == "" || !attributeValue.Contains(","))
      return;
    this.UndoManager.BeginCreateMultyUndo("Изменить статус 'всегда таблица'");
    try
    {
      this.elCurChange.SetAttributeValue("AlwaysTable", this.elCurChange.GetAttributeValue("AlwaysTable", true) == "yes" ? "no" : "yes", updateUI: false, updateLayout: false);
      this.UpdateMultiChangeHeader((TableData) this.elCurChange, this.eco._GetIdList(attributeValue), updateDesignation: false, forceUpdate: true);
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  private void cmdInsertTemplate(object sender, EventArgs e)
  {
    this.UndoManager.BeginCreateMultyUndo("Вставить спецсимвол");
    try
    {
      if (this.textFld == null || !(this.textFld is TextBoxElement))
        return;
      this.InsertFormula((TextBoxElement) this.textFld);
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  private void cmdInsertImBase(object sender, EventArgs e)
  {
    if (this.textFld == null || !(this.textFld is TextBoxElement))
      return;
    this.UndoManager.BeginCreateMultyUndo("Вставить обозначение из IMBASE");
    try
    {
      List<object> availableCatalogs = this.GetAvailableCatalogs();
      long objectID = ECOPlugin.ImbaseSelector.SelectFromCatalog(LocalizationHolder.rm.GetString("ECO.Client_439"), LocalizationHolder.rm.GetString("ECO.Client_440"), (object) availableCatalogs, false, true, (int[]) null, -1);
      if (objectID == 0L || !((this.textFld as TextBoxElement).InPlaceEditorControl is ImRtfEditor placeEditorControl))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
        if (objectInfo.Empty)
          return;
        placeEditorControl.InsertTerText(objectInfo.Caption, true);
      }
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  protected List<object> GetAvailableCatalogs()
  {
    List<object> availableCatalogs = (List<object>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!IMCatalogs.All.Loaded)
        IMCatalogs.All.Load(sessionKeeper.Session);
      availableCatalogs = IMCatalogs.All != null ? IMCatalogs.All.Select<long, object>((Func<long, int, object>) ((objId, i) => (object) objId)).ToList<object>() : new List<object>();
      if (availableCatalogs.Count == 0)
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(new Guid("{cad008d9-306c-11d8-b4e9-00304f19f545}"), false);
        if (dbObject1 != null)
          availableCatalogs.Add((object) dbObject1.ObjectID);
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(new Guid("{cad008e6-306c-11d8-b4e9-00304f19f545}"), false);
        if (dbObject2 != null)
          availableCatalogs.Add((object) dbObject2.ObjectID);
      }
    }
    return availableCatalogs;
  }

  private void cmdIncludeDocs(object sender, EventArgs e) => this.AttachAlternative();

  private void cmdIncludeExternalDoc(object sender, EventArgs e) => this.AttachToECO_ExternalDoc();

  private void cmdHide(object sender, EventArgs e)
  {
    if ((this.ecoTreeViewDlg == null ? 0 : (this.ecoTreeViewDlg.ContainsFocus ? 1 : 0)) != 0)
    {
      foreach (ECOTreeItem ecoTreeItem in this.ecoTreeViewDlg.Selected)
      {
        if (ecoTreeItem != null && ecoTreeItem.Id != 0L && ecoTreeItem.ParentItem != null && ecoTreeItem.ParentItem.Node != null && this.GetHidingType(ecoTreeItem.Id) == HidingType.CanBeHidden)
          this.HideObject(ecoTreeItem.Info.ObjectID, ecoTreeItem.ParentItem.Node as TableElement);
      }
    }
    this.Document.UpdateLayout(true);
  }

  private void cmdShow(object sender, EventArgs e)
  {
    if ((this.ecoTreeViewDlg == null ? 0 : (this.ecoTreeViewDlg.ContainsFocus ? 1 : 0)) != 0)
    {
      foreach (ECOTreeItem ecoTreeItem in this.ecoTreeViewDlg.Selected)
      {
        if (ecoTreeItem != null && ecoTreeItem.Id != 0L && this.GetHidingType(ecoTreeItem.Id) == HidingType.Hidden)
          this.UnhideObject(ecoTreeItem.Info.ObjectID, (TableElement) null);
      }
    }
    this.Document.UpdateLayout(true);
  }

  private void cmdSplitChange(object sender, EventArgs e)
  {
    int num = this.ecoTreeViewDlg == null ? 0 : (this.ecoTreeViewDlg.ContainsFocus ? 1 : 0);
    ECOTreeItem ecoTreeItem = (ECOTreeItem) null;
    if (num == 0)
      return;
    if (this.ecoTreeViewDlg.Selected.Count > 0)
    {
      ecoTreeItem = this.ecoTreeViewDlg.Selected[0];
      this.elCurChange = ecoTreeItem.Node != null ? ecoTreeItem.Node as TableElement : ecoTreeItem.ParentItem.Node as TableElement;
    }
    if (ecoTreeItem == null || ecoTreeItem.Node != null)
      return;
    this.SplitChange(this.elCurChange, ecoTreeItem.Info.ObjectID);
  }

  private void cmdRemoveDocs(object sender, EventArgs e)
  {
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      int num1 = this.ecoTreeViewDlg == null ? 0 : (this.ecoTreeViewDlg.ContainsFocus ? 1 : 0);
      ECOTreeItem ecoTreeItem = (ECOTreeItem) null;
      if (num1 != 0 && this.ecoTreeViewDlg.Selected.Count > 0)
      {
        ecoTreeItem = this.ecoTreeViewDlg.Selected[0];
        this.elCurChange = ecoTreeItem.Node != null ? ecoTreeItem.Node as TableElement : ecoTreeItem.ParentItem.Node as TableElement;
      }
      List<long> longList1 = (List<long>) null;
      if (this.elCurChange != null)
      {
        if (this.elCurChange.PrevCell != null)
          this.elCurChange = this.elCurChange.FindFirstCell() as TableElement;
        longList1 = this.eco._GetIdList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      }
      bool AskUser = true;
      List<long> idList;
      if (ecoTreeItem != null && ecoTreeItem.Node == null)
      {
        if (ecoTreeItem.Info.ObjectID == 0L)
        {
          int index = this.eco.HiddenObjIdIndex(ecoTreeItem.Id);
          if (index >= 0)
          {
            this.eco.hiddenLinks.RemoveAt(index);
            this.ecoTreeViewDlg.UpdateTree();
            return;
          }
        }
        idList = new List<long>()
        {
          ecoTreeItem.Info.ObjectID
        };
        AskUser = false;
      }
      else
        idList = longList1;
      List<long> hidingList = (List<long>) null;
      List<long> longList2 = this.RemoveCurElemDocs(idList, AskUser, out hidingList, ecoTreeItem != null);
      if (this.elCurChange == null)
      {
        if (ecoTreeItem == null)
          return;
        this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
        this.Document.Modified = true;
      }
      else
      {
        List<long> longList3 = new List<long>();
        foreach (long key in longList2)
        {
          if (this.deletedLinks.ContainsKey((object) key))
          {
            PendingLink deletedLink = (PendingLink) this.deletedLinks[(object) key];
            if (deletedLink != null && deletedLink.auxObjects != null)
            {
              foreach (ObjInfo auxObject in deletedLink.auxObjects)
              {
                if (!longList2.Contains(auxObject.verId) && !longList3.Contains(auxObject.verId))
                  longList3.Add(auxObject.verId);
              }
            }
          }
        }
        for (int index1 = longList3.Count - 1; index1 >= 0; --index1)
        {
          long num2 = longList3[index1];
          if (longList1.Contains(num2))
          {
            longList2.Add(num2);
            int index2 = this.eco.ObjIdIndex(num2);
            if (index2 >= 0)
              this.DeletePendingLink(num2, index2);
            longList3.RemoveAt(index1);
          }
        }
        for (int index = this.eco.hiddenLinks.Count - 1; index >= 0; --index)
        {
          if (longList3.Contains(this.eco.hiddenLinks[index].verID))
            this.eco.hiddenLinks.RemoveAt(index);
        }
        if (longList1.Count > 0 && longList2.Count == 0 && hidingList.Count == 0)
          return;
        if (longList1.Count == longList2.Count + hidingList.Count)
        {
          this.elCurChange.UniteTable();
          this.elCurChange.Remove(true, true);
          this.elCurChange = (TableElement) null;
        }
        else
        {
          foreach (long num3 in longList2)
            longList1.Remove(num3);
          foreach (long num4 in hidingList)
            longList1.Remove(num4);
          this.eco.ecoMainTable.UniteTable();
          this.eco._SetIdList((RectangleElement) this.elCurChange, longList1);
          this.UpdateMultiChangeHeader((TableData) this.elCurChange, longList1);
          this.UpdateDocDesign();
          this.UpdateSpecText(this.elCurChange);
        }
        foreach (long objId in hidingList)
        {
          PendingLink pendingLink = this.eco.FindPendingLink(objId);
          if (pendingLink != null)
            this._DoHiding(pendingLink);
        }
        this.UpdateDocDesign();
        this.UpdateSpecText();
        this.Document.UpdateLayout(0, true, true);
        this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
        this.CommandManager.QueryStatus();
      }
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  private void cmdIncludeElem(object sender, EventArgs e)
  {
    change = this.elCurChange;
    if (change == null && !(this.GetLastChange() is TableElement change))
      return;
    TableElement te;
    if (this.elCurElem == null)
    {
      TableElement lastPageElement = this.GetLastPageElement();
      te = this.indexCurElem != int.MinValue || lastPageElement == null ? this.eco.InsertNewEcoElement((TableElement) null, false, change, (sender as MenuButtonItem).Text, this.UndoManager) : this.eco.InsertNewEcoElement((TableElement) null, true, lastPageElement, (sender as MenuButtonItem).Text, this.UndoManager);
    }
    else
      te = this.eco.InsertNewEcoElement(this.elCurElem, this.rightPart, this.elCurElem.Parent as TableElement, (sender as MenuButtonItem).Text, this.UndoManager);
    if (ECOPlugin.plugin.eps.Current.AutoOrigSize)
    {
      List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
      te.FindNodes(typeof (ContainerData), foundNodes);
      if (foundNodes != null)
      {
        foreach (DocumentTreeNode documentTreeNode in foundNodes)
        {
          if (documentTreeNode is ContainerData containerData)
            containerData.ScaleMode = ImageScaleMode.OriginalAutoSize;
        }
      }
    }
    this.Document.UpdateLayout(0, true, true);
    this.GoToElem(te);
  }

  private void GoToElem(TableElement te)
  {
    this.DocumentControl.ActivePage = (Page) te.FindFirstCell().Page;
  }

  private void cmdIncludeTable(object sender, EventArgs e)
  {
    change = this.elCurChange;
    if (change == null && !(this.GetLastChange() is TableElement change))
      return;
    TableElement node1 = change.Template.FindNode(Intermech.ECO.Client.ECO.fldTable) as TableElement;
    TableElement node2 = node1.FindNode("Внутренняя таблица") as TableElement;
    CreateTableDialog createTableDialog1 = new CreateTableDialog();
    SizeF size;
    if (node2 != null)
    {
      CreateTableDialog createTableDialog2 = createTableDialog1;
      size = node2.Size;
      double height = (double) size.Height;
      createTableDialog2.TableRowHeight = (float) height;
    }
    else
    {
      CreateTableDialog createTableDialog3 = createTableDialog1;
      size = node1.Size;
      double height = (double) size.Height;
      createTableDialog3.TableRowHeight = (float) height;
    }
    if (!createTableDialog1.Execute())
      return;
    TableSize tableSize = createTableDialog1.TableSize;
    float num = createTableDialog1.TableRowHeight * (float) tableSize.Rows;
    this.UndoManager.BeginCreateMultyUndo("Создание таблицы");
    try
    {
      TableElement te;
      if (this.elCurElem == null)
      {
        TableElement lastPageElement = this.GetLastPageElement();
        te = this.indexCurElem != int.MinValue || lastPageElement == null ? this.eco.InsertNewEcoElement((TableElement) null, false, change, Intermech.ECO.Client.ECO.fldTable, this.UndoManager) : this.eco.InsertNewEcoElement((TableElement) null, true, lastPageElement, Intermech.ECO.Client.ECO.fldTable, this.UndoManager);
      }
      else
        te = this.eco.InsertNewEcoElement(this.elCurElem, this.rightPart, this.elCurElem.Parent as TableElement, Intermech.ECO.Client.ECO.fldTable, this.UndoManager);
      if (te == null)
        return;
      if (!(te.FindNode("Внутренняя таблица") is TableElement child))
      {
        child = new TableElement();
        TableElement tableElement = child;
        size = node1.Size;
        double width = (double) size.Width;
        size = node1.Size;
        double height = (double) size.Height;
        RectangleF rectangleF = new RectangleF(0.0f, 0.0f, (float) width, (float) height);
        tableElement.AssignBounds(rectangleF, false, false, false);
        te.AddChildNode((DocumentTreeNode) child, false, false, false, false);
      }
      else
        child.Template = (DocumentTreeNode) null;
      RectangleF bounds = child.Bounds with { Height = num };
      child.AssignBounds(bounds, false, false, false);
      child.SplitCell(tableSize.Rows, tableSize.Columns, true, true, true);
      this.Document.UpdateLayout(0, true, true);
      this.GoToElem(te);
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  private void cmdRemoveElem(object sender, EventArgs e)
  {
    this.DeleteElem(this.elCurChange, (DocumentTreeNode) this.elCurElem);
  }

  private void cmdCopyAllElems(object sender, EventArgs e) => this.CopyAllElems(this.elCurChange);

  private void cmdPasteAllElems(object sender, EventArgs e) => this.PasteAllElems(this.elCurChange);

  internal List<int> DocumentTypes
  {
    get
    {
      if (this.documentTypes == null)
        this.documentTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
      return this.documentTypes;
    }
  }

  private bool IsLaunchEnabled()
  {
    if (this.elCurChange == null)
      return false;
    List<PendingLink> plist = this.eco._GetPList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (PendingLink pendingLink in plist)
      {
        if (pendingLink.objType == -1)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(pendingLink.verID);
          if (!objectInfo.Empty)
            pendingLink.objType = objectInfo.ObjectTypeID;
          else
            continue;
        }
        if (this.DocumentTypes.Contains(pendingLink.objType))
          return true;
      }
    }
    return false;
  }

  private void cmdLaunchShooter(object sender, EventArgs e)
  {
    long objectId = 0;
    string attributeValue = this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
    List<long> objIds = new List<long>();
    foreach (PendingLink pendingLink in this.eco._GetPList(attributeValue))
    {
      if (this.DocumentTypes.Contains(pendingLink.objType))
        objIds.Add(pendingLink.verID);
    }
    switch (objIds.Count)
    {
      case 0:
        return;
      case 1:
        objectId = objIds[0];
        break;
      default:
        using (ObjSelect objSelect = new ObjSelect())
        {
          List<long> longList = objSelect.Execute(objIds, false, false);
          if (longList.Count == 0)
            return;
          objectId = longList[0];
          break;
        }
    }
    if (objectId == 0L)
      return;
    VersionsRulePackage currentWindowRule = VersionsRuleSources.GetCurrentWindowRule();
    ClientContext.LaunchActions.Launch(new LaunchParams(LaunchType.View, objectId, DBHelper.GetObjectType(objectId), currentWindowRule, false));
    string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scrshooter.exe");
    if (!File.Exists(str))
      return;
    Process.Start(str)?.Dispose();
  }

  private void cmdMoveElemUp(object sender, EventArgs e)
  {
    this.MoveElemUp(this.elCurChange, (DocumentTreeNode) this.elCurElem);
  }

  private void cmdMoveElemDown(object sender, EventArgs e)
  {
    this.MoveElemDown(this.elCurChange, (DocumentTreeNode) this.elCurElem);
  }

  private List<long> RemoveCurElemDocs(
    List<long> idList,
    bool AskUser,
    out List<long> hidingList,
    bool forceDelete = false)
  {
    hidingList = new List<long>();
    if (idList == null || idList.Count == 0)
      return idList;
    using (ObjSelect objSelect = new ObjSelect())
    {
      objSelect.capt = idList.Count > 1 ? LocalizationHolder.rm.GetString("ECO.Client_252") : LocalizationHolder.rm.GetString("ECO.Client_317");
      List<HidingType> mainItems = new List<HidingType>();
      for (int index = 0; index < idList.Count; ++index)
      {
        PendingLink pendingLink = this.eco.FindPendingLink(idList[index]);
        mainItems.Add(pendingLink.hideType);
      }
      if (AskUser)
        AskUser = mainItems.Exists((Predicate<HidingType>) (ht => ht == HidingType.Disabled));
      List<long> longList = AskUser ? objSelect.Execute(idList, idList.Count > 1, false, mainItems) : idList;
      if (!forceDelete)
      {
        for (int index1 = longList.Count - 1; index1 >= 0; --index1)
        {
          int index2 = idList.IndexOf(longList[index1]);
          if (index2 >= 0 && mainItems[index2] != HidingType.Disabled)
          {
            long num = longList[index1];
            hidingList.Add(num);
            longList.RemoveAt(index1);
          }
        }
      }
      foreach (long num in longList)
      {
        int index = this.eco.ObjIdIndex(num);
        this.DeletePendingLink(num, index);
        if (index >= 0)
          this.eco.objLinks.RemoveAt(index);
      }
      if (forceDelete)
      {
        for (int index = this.eco.hiddenLinks.Count - 1; index >= 0; --index)
        {
          if (longList.Contains(this.eco.hiddenLinks[index].verID))
            this.eco.hiddenLinks.RemoveAt(index);
        }
      }
      return longList;
    }
  }

  private TableData GetLastChange()
  {
    TableData lastChange = (TableData) null;
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      lastChange = dataOwner.Nodes[dataPositionInFlow] as TableData;
    return lastChange;
  }

  private DocumentTreeNode GetLastElemWithId(DocumentTreeNode root, string TemplateID)
  {
    if (root.TemplateId == null)
      return (DocumentTreeNode) null;
    if (root.TemplateId == TemplateID)
      return root;
    for (int index = root.NodesCount - 1; index >= 0; --index)
    {
      DocumentTreeNode lastElemWithId = this.GetLastElemWithId(root.Nodes[index], TemplateID);
      if (lastElemWithId != null)
        return lastElemWithId;
    }
    return (DocumentTreeNode) null;
  }

  private bool IsWorkspace(DocumentTreeNode node, object conditionValue)
  {
    return conditionValue is string str && node.Template.Name.StartsWith(str);
  }

  private TableElement GetLastPageElement()
  {
    DocumentTreeNode node = this.DocumentControl.ActivePage.FindNode(new FindCondition(this.IsWorkspace), (object) Intermech.ECO.Client.ECO.fldWorkspace);
    if (node == null)
      return (TableElement) null;
    return node.NodesCount > 0 ? node.Nodes[node.NodesCount - 1] as TableElement : (TableElement) null;
  }

  private long GetECODeliveryListID(SessionKeeper sk)
  {
    if (!(sk.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
      return 0;
    QuickObjectInfo objectInfo = sk.Session.GetObjectInfo(this.ecoID);
    long ecoDeliveryListId = customService.GetDeliveryListID(sk.Session.SessionGUID, objectInfo.ID);
    if (ecoDeliveryListId == 0L)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_334"), LocalizationHolder.rm.GetString("ECO.Client_260"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
        return 0;
      ecoDeliveryListId = customService.CreateDeliveryList(sk.Session.SessionGUID, this.ecoID);
    }
    return ecoDeliveryListId;
  }

  internal bool IsChangeAllowed(ECOGoal oldGoal, ECOGoal newGoal)
  {
    switch (oldGoal)
    {
      case ECOGoal.Change:
        return true;
      case ECOGoal.Annul:
        return true;
      case ECOGoal.Litera:
        return true;
      case ECOGoal.Replace:
        return true;
      case ECOGoal.Creation:
        return true;
      default:
        return false;
    }
  }

  private void cmdChangeGoal(object sender, EventArgs e) => this.ChangeGoal(this.elCurChange);

  internal void CopyAllElems(TableElement change)
  {
    if (change == null)
      return;
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    change.UniteTable();
    try
    {
      int num = 0;
      foreach (DocumentTreeNode node in change.Nodes)
      {
        if (num > 0)
          documentTreeNodeList.Add(node);
        ++num;
      }
      NodeClipboardHelper.CopyToClipboard(documentTreeNodeList.ToArray(), IntPtr.Zero, (object) "RevEditor");
    }
    finally
    {
      this.Document.UpdateLayout(0, true, true);
    }
  }

  internal void CopyTable(DocumentTreeNode elem)
  {
    NodeClipboardHelper.CopyToClipboard(new DocumentTreeNode[1]
    {
      elem
    }, IntPtr.Zero, (object) "RevEditor");
  }

  internal void PasteAllElems(TableElement change)
  {
    if (change == null || !NodeClipboardHelper.CanPasteFromClipboard((DocumentTreeNode) change))
      return;
    NodeClipboardHelper.PasteFromClipboard((DocumentTreeNode) change, IntPtr.Zero);
  }

  internal void MoveElemUp(TableElement change, DocumentTreeNode elem)
  {
    RectangleElement rectangleElement = elem as RectangleElement;
    if (change == null || rectangleElement == null)
      return;
    rectangleElement.MoveDataElementUp(true);
  }

  internal void MoveElemDown(TableElement change, DocumentTreeNode elem)
  {
    RectangleElement rectangleElement = elem as RectangleElement;
    if (change == null || rectangleElement == null)
      return;
    rectangleElement.MoveDataElementDown(true);
  }

  internal void AddElemBefore()
  {
  }

  internal void AddElemAfter()
  {
  }

  internal int CalcDataCountWithouRealHeaders(TableData root)
  {
    return ((IEnumerable<RectangleElement>) root.FindFirstCell()).Count<RectangleElement>((System.Func<RectangleElement, bool>) (dc => dc.TemplateId != Intermech.ECO.Client.ECO.fldChangeHeader && dc.TemplateId != Intermech.ECO.Client.ECO.fldChangeHeader2));
  }

  internal void DeleteElem(TableElement change, DocumentTreeNode elem)
  {
    RectangleElement rectangleElement = elem as RectangleElement;
    if (change == null || rectangleElement == null || rectangleElement.Id == Intermech.ECO.Client.ECO.idSpecText)
      return;
    DocumentTreeNode parent = rectangleElement.Template.Parent;
    TableData templateRecursive = (TableData) change.FindFirstNodeFromTemplate_Recursive(parent);
    int num = templateRecursive.Id == "AR2" ? 0 : 1;
    if (this.CalcDataCountWithouRealHeaders(templateRecursive) <= num)
      return;
    this.SelElem = (DocumentTreeNode) null;
    RectangleElement firstCell = rectangleElement.FindFirstCell();
    templateRecursive.UniteTable();
    firstCell.Remove(true, true);
  }

  internal void SortByDes(TableElement change)
  {
    if (change == null)
      return;
    this.UndoManager.BeginCreateMultyUndo("Сортировать изменение");
    try
    {
      this.eco.ecoMainTable.UniteTable();
      try
      {
        this.SortChangeByDes((TableData) change);
      }
      finally
      {
        this.Document.UpdateLayout(0, true, true);
      }
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
  }

  internal void ChangeGoal(TableElement change)
  {
    if (change == null)
      return;
    List<long> idList = this.eco._GetIdList(change.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    List<PendingLink> pendingLinkList = new List<PendingLink>();
    foreach (PendingLink objLink in this.eco.objLinks)
    {
      if (idList.Contains(objLink.verID) || idList.Contains(-objLink.verID))
        pendingLinkList.Add(objLink);
    }
    if (pendingLinkList.Count == 0)
      return;
    ECOGoal ecoGoal = pendingLinkList[0].ecoGoal;
    if (ecoGoal == ECOGoal.NoGoal)
      return;
    List<long> objIds = new List<long>();
    pendingLinkList.ForEach((Action<PendingLink>) (pl => objIds.Add(pl.verID)));
    HashSet<ECOGoal> allowedGoals = new HashSet<ECOGoal>();
    for (int index = 0; index <= 4; ++index)
    {
      ECOGoal newGoal = (ECOGoal) index;
      if (newGoal != ecoGoal && this.IsChangeAllowed(ecoGoal, newGoal))
        allowedGoals.Add(newGoal);
    }
    IncludeGoal ig = new IncludeGoal();
    if (!ig.Execute(objIds, this.eco.litera, allowedGoals))
      return;
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      if (ecoGoal == ECOGoal.Creation && ig.goal == ECOGoal.Change)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (PendingLink pendingLink in pendingLinkList)
          {
            IDBObject idbO = sessionKeeper.Session.GetObject(pendingLink.verID, false);
            if (idbO != null)
            {
              string cNo;
              this.AssignChangeNo(sessionKeeper.Session, idbO, ig.goal, out cNo);
              pendingLink.verStr = cNo;
            }
          }
        }
      }
      pendingLinkList.ForEach((Action<PendingLink>) (pl => pl.ecoGoal = ig.goal));
      pendingLinkList.ForEach((Action<PendingLink>) (pl => pl.stepID = ig.selLCStepId));
      if (ig.goal == ECOGoal.Litera)
        this.eco.litera = ig.litera;
      foreach (PendingLink pendingLink in pendingLinkList)
      {
        if (!this.changedLinks.ContainsKey(pendingLink.verID))
          this.changedLinks.Add(pendingLink.verID, pendingLink);
      }
      this.DropActiveEditor();
      bool flag1 = this.RemoveSpecText(change);
      bool flag2 = this.UpdateSpecText(change) | flag1;
      bool flag3 = this.UpdateMultiChangeHeader((TableData) change, objIds, false) | flag2;
      this.UpdateDocDesign();
      this.Document.UpdateLayout(true, true);
      this.Document.Modified = true;
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  internal void ImageFromClip(ContainerElement cont)
  {
    if (cont == null)
      return;
    cont.PasteFromClipboard(this.Handle);
    if (cont.DataSourceType == DataSourceType.Image)
    {
      Image image = cont.Image;
      SizeF sizeF = new SizeF((float) ((double) image.PhysicalDimension.Width / (double) image.HorizontalResolution * 25.399999618530273), (float) ((double) image.PhysicalDimension.Height / (double) image.VerticalResolution * 25.399999618530273));
      cont.AssignOriginalSize(sizeF, true, true);
      cont.ScaleMode = ImageScaleMode.OriginalAutoSize;
    }
    cont.CheckOriginalSizeAndAskUser();
    cont.InvalidateUI(true);
  }

  internal void ImageFromFile(ContainerElement cont)
  {
    if (cont == null)
      return;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = LocalizationHolder.rm.GetString("ECO.Client_210");
    openFileDialog.CheckFileExists = true;
    openFileDialog.Multiselect = false;
    openFileDialog.Title = LocalizationHolder.rm.GetString("ECO.Client_211");
    openFileDialog.RestoreDirectory = true;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    cont.LoadDataObjectFromFile(openFileDialog.FileName);
    Image image = cont.Image;
    SizeF sizeF = new SizeF((float) ((double) image.PhysicalDimension.Width / (double) image.HorizontalResolution * 25.399999618530273), (float) ((double) image.PhysicalDimension.Height / (double) image.VerticalResolution * 25.399999618530273));
    cont.AssignOriginalSize(sizeF, true, true);
    cont.ScaleMode = ImageScaleMode.OriginalAutoSize;
    cont.CheckOriginalSizeAndAskUser();
  }

  internal void ImageFromObj(ContainerElement cont)
  {
    if (cont == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(-2, CoreConsts.FilterRecords);
      if (objectTypeCollection == null)
        return;
      DataTable usedByAttribute = objectTypeCollection.GetUsedByAttribute(RevHelper.idAttrFile);
      int[] objTypeIdArray = new int[usedByAttribute.Rows.Count];
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) usedByAttribute.Rows)
        objTypeIdArray[num++] = Convert.ToInt32(row[0]);
      IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects(objTypeIdArray);
      if (dbObjectIdArray == null || dbObjectIdArray.Length == 0)
        return;
      long objectID = dbObjectIdArray[0].Value;
      ArcMethods arcMethod = ArcMethods.NotPacked;
      MemoryStream memoryStream = new MemoryStream();
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject == null)
        return;
      if (dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")) is IBlobReader attributeByGuid)
      {
        BlobInformation blobInformation = attributeByGuid.OpenBlob(0);
        arcMethod = blobInformation.ArcMethod;
        try
        {
          byte[] buffer = attributeByGuid.ReadDataBlock((int) blobInformation.RealFileSize);
          memoryStream.Write(buffer, 0, buffer.Length);
        }
        finally
        {
          attributeByGuid.CloseBlob();
        }
      }
      memoryStream.Position = 0L;
      cont.AssignDataStream((Stream) memoryStream, arcMethod, DataSourceType.Unknown, true, true, true, true, true);
      Image image = cont.Image;
      SizeF sizeF = new SizeF((float) ((double) image.PhysicalDimension.Width / (double) image.HorizontalResolution * 25.399999618530273), (float) ((double) image.PhysicalDimension.Height / (double) image.VerticalResolution * 25.399999618530273));
      cont.AssignOriginalSize(sizeF, true, true);
      cont.ScaleMode = ImageScaleMode.OriginalAutoSize;
      cont.CheckOriginalSizeAndAskUser();
    }
  }

  internal void CreateOLEObj(ContainerElement cont) => cont?.CreateOleObject();

  private void PageControl_DragDrop(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!e.Data.GetDataPresent(typeof (IOSource)))
      return;
    IOSource data = e.Data.GetData(typeof (IOSource)) as IOSource;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(data.SelectedItems, data.Services, false);
    if (!commandsTable.Contains("Copy"))
      return;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("Copy", commandsTable, data.Services);
    this.items.Clear();
    Intermech.Document.UI.PageControl pageControl = this.DocumentControl.ActivePage.PageControl;
    pageControl.GetPageElementUIAtPoint(pageControl.PointToClient(Control.MousePosition), this.items);
    this.elCurChange = (TableElement) null;
    if (this.IsInWorkspace())
    {
      foreach (PageElementUI pageElementUi in this.items)
      {
        DocumentTreeNode node = (DocumentTreeNode) pageElementUi.Element;
        if (node != null)
        {
          while (!this.IsElement(node) && !(node.Name == Intermech.ECO.Client.ECO.fldChange))
          {
            node = node.Parent;
            if (node == null)
              break;
          }
          if (node != null)
          {
            if (this.IsElement(node) && this.elCurElem != null)
              this.elCurChange = this.elCurElem.Parent as TableElement;
            if (node.Name == Intermech.ECO.Client.ECO.fldChange)
            {
              this.elCurChange = node as TableElement;
              break;
            }
          }
        }
      }
    }
    this.PasteFromClipboardCommand();
  }

  private void PageControl_DragEnter(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!e.Data.GetDataPresent(typeof (IOSource)) || (e.Data.GetData(typeof (IOSource)) as IOSource).Control == this)
      return;
    e.Effect = DragDropEffects.Copy;
  }

  private void PageControl_DragOver(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!e.Data.GetDataPresent(typeof (IOSource)))
      return;
    e.Data.GetData(typeof (IOSource));
    Intermech.Document.UI.PageControl pageControl = this.DocumentControl.ActivePage.PageControl;
    PageElementUI elementUiAtPoint = pageControl.GetPageElementUIAtPoint(pageControl.PointToClient(new Point(e.X, e.Y)), true);
    if (elementUiAtPoint != null)
    {
      PageElementNode element = elementUiAtPoint.Element;
    }
    if (true)
      e.Effect = DragDropEffects.Copy;
    else
      e.Effect = DragDropEffects.None;
  }

  internal override void DocumentControl_ActivePageChanged(object sender, EventArgs e)
  {
    if (this.DocumentControl.ActivePage == null)
      return;
    Intermech.Document.UI.PageControl pageControl = this.DocumentControl.ActivePage.PageControl;
    pageControl.AllowDrop = true;
    pageControl.DragDrop -= new DragEventHandler(this.PageControl_DragDrop);
    pageControl.DragEnter -= new DragEventHandler(this.PageControl_DragEnter);
    pageControl.DragOver -= new DragEventHandler(this.PageControl_DragOver);
    pageControl.DragDrop += new DragEventHandler(this.PageControl_DragDrop);
    pageControl.DragEnter += new DragEventHandler(this.PageControl_DragEnter);
    pageControl.DragOver += new DragEventHandler(this.PageControl_DragOver);
  }

  public void SetPLForAll()
  {
    if (this.plugin.DoSetPLForAll == null)
      return;
    DocumentTreeNode templateRecursive = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idUkazVnedrenie);
    if (templateRecursive == null || !(templateRecursive is TextData))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_464"), LocalizationHolder.rm.GetString("ECO.Client_108"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
      ((TextData) templateRecursive).AssignText(this.plugin.DoSetPLForAll(this.eco.objLinks), false, true, true);
  }

  public void SortAndMergeCommand()
  {
    this.eco.DocumentECO.SuspendUpdateLayout();
    this.eco.DocumentECO.SuspendUpdateGeometryRefreshUI();
    ProcChanges procChanges = new ProcChanges();
    try
    {
      List<ProcChanges.ChangeInfo> changes = new List<ProcChanges.ChangeInfo>();
      this.eco.ecoMainTable.UniteTable();
      int nodesCount = this.eco.ecoMainTable.NodesCount;
      for (int index = 0; index < nodesCount; ++index)
      {
        if (Intermech.ECO.Client.ECO.IsChange(this.eco.ecoMainTable.Nodes[index]))
        {
          TableElement node = this.eco.ecoMainTable.Nodes[index] as TableElement;
          List<long> idList = this.eco._GetIdList(node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
          ECOGoal goal = ECOGoal.NoGoal;
          if (idList.Count > 0)
          {
            PendingLink pendingLink = this.eco.FindPendingLink(idList[0]);
            if (pendingLink != null)
              goal = pendingLink.ecoGoal;
          }
          changes.Add(new ProcChanges.ChangeInfo(idList, goal, node));
        }
      }
      if (!procChanges.Execute(changes, this.eco.objLinks) || !procChanges.somethingChanged)
        return;
      foreach (ProcChanges.ChangeInfo change1 in procChanges.ChangeList)
      {
        if (change1.MergedList != null && change1.MergedList.Count != 0)
        {
          TableElement change2 = change1.Change;
          for (int index1 = 0; index1 < change1.MergedList.Count; ++index1)
          {
            TableElement merged = change1.MergedList[index1];
            int index2 = 1;
            while (index2 < merged.NodesCount)
            {
              DocumentTreeNode node = merged.Nodes[index2];
              if (node.TemplateId == Intermech.ECO.Client.ECO.idSpecText)
                ++index2;
              else
                change2.AddChildNode(node, false, false);
            }
            this.eco.ecoMainTable.RemoveChildNode((DocumentTreeNode) merged, false, false);
          }
        }
      }
      int num = 0;
      for (int index = 0; index < procChanges.ChangeList.Count; ++index)
      {
        ProcChanges.ChangeInfo change = procChanges.ChangeList[index];
        if (change.State != ProcChanges.ChangeState.Deleted)
        {
          this.eco.ecoMainTable.InsertChildNode(num++, (DocumentTreeNode) change.Change, true, true, false, false, false);
          if (change.State != ProcChanges.ChangeState.NoChange)
          {
            string attributeValue = this.eco._SetIdList((RectangleElement) change.Change, change.ObjIDs);
            this.UpdateSpecText(change.Change);
            if (change.Change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) is TextData templateRecursive)
              templateRecursive.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
            this.UpdateMultiChangeHeader((TableData) change.Change, change.ObjIDs, false);
          }
        }
      }
    }
    finally
    {
      this.eco.DocumentECO.ResumeUpdateLayout(false, false);
      this.eco.DocumentECO.ResumeUpdateRefreshUI(false, false);
      if (procChanges.somethingChanged)
        this.UpdateDocDesign();
      this.eco.DocumentECO.UpdateLayout(false, true);
      this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
    }
  }

  public void SwapSignLinks(IDBObject objPI)
  {
    if (!(this.Document.FindFirstNodeFromTemplate_Recursive("I3 #2") is TextData))
      return;
    this._SwapLink("I3", objPI);
    this._SwapLink("I4", objPI);
    this._SwapLink("I5", objPI);
    this._SwapLink("I6", objPI);
    this._SwapLink("I7", objPI);
  }

  internal void _SwapLink(string Id, IDBObject objPI)
  {
    TextData templateRecursive1 = this.Document.FindFirstNodeFromTemplate_Recursive(Id) as TextData;
    TextData templateRecursive2 = this.Document.FindFirstNodeFromTemplate_Recursive(Id + " #2") as TextData;
    if (templateRecursive1 == null || templateRecursive2 == null)
      return;
    ReferenceToDBObjectBase referenceToTextSource1 = templateRecursive1.ReferenceToTextSource as ReferenceToDBObjectBase;
    ReferenceToDBObjectBase referenceToTextSource2 = templateRecursive2.ReferenceToTextSource as ReferenceToDBObjectBase;
    if (referenceToTextSource1 == null || referenceToTextSource2 == null)
      return;
    templateRecursive1.AssignReferenceToTextSource((ReferenceBase) referenceToTextSource2, true, false, false);
    ReferenceToDBObjectCore.UpdateDBObjectInfo((IDBRelation) null, objPI, referenceToTextSource1);
    templateRecursive2.AssignReferenceToTextSource((ReferenceBase) referenceToTextSource1, true, false, false);
  }

  internal void DoReplaceTemplate()
  {
    ReplaceTemplateForm replaceTemplateForm = new ReplaceTemplateForm();
    if (replaceTemplateForm.ShowDialog() != DialogResult.OK)
      return;
    this.Document.SaveToXml(replaceTemplateForm.BackupFilePath, false);
    this.DoReplaceTemplate(replaceTemplateForm.NewTemplateId, this.Document);
    this.ECO.CheckEcoMainTable();
    this.AfterLoadDoc();
  }

  private void SelReason(DocumentTreeNode dtn)
  {
    if (this.ReadOnly)
      return;
    DocumentTreeNode templateRecursive1 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr);
    DocumentTreeNode templateRecursive2 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idReason);
    string reason = "";
    if (templateRecursive2 != null && templateRecursive2 is TextData)
      reason = (templateRecursive2 as TextData).Text;
    EditReason editReason = new EditReason();
    if (templateRecursive1 != null && templateRecursive1 is TextData)
      editReason.userShifr = (templateRecursive1 as TextData).Text;
    string str = editReason.Execute(ref reason, new Guid(RevHelper.guidAttrRevReason));
    if (str == "-2")
      return;
    this.UndoManager.BeginCreateMultyUndo("Выбор причины");
    try
    {
      if (templateRecursive2 != null && templateRecursive2 is TextData)
      {
        if (ECOPlugin.FindPlugin().eps.Current.ProhibitCustomReason && str == "-1")
          (templateRecursive2 as TextData).Text = "Другое." + reason;
        else
          (templateRecursive2 as TextData).Text = reason;
      }
      if (templateRecursive1 != null)
      {
        if (templateRecursive1 is TextData)
        {
          if (editReason.userShifr != "")
            (templateRecursive1 as TextData).Text = editReason.userShifr;
          else
            (templateRecursive1 as TextData).Text = str;
        }
      }
    }
    finally
    {
      this.UndoManager.EndCreateMultyUndo();
    }
    this.eco.reasonCode = str;
  }

  private void SelZadel(DocumentTreeNode dtn)
  {
    if (this.ReadOnly)
      return;
    EditReason editReason = new EditReason();
    string str = "";
    if (dtn != null && dtn is TextData)
      str = (dtn as TextData).Text;
    ref string local = ref str;
    Guid attrGuid = new Guid(RevHelper.guidAttrZadel);
    if (editReason.Execute(ref local, attrGuid) == "-2" || dtn == null || !(dtn is TextData))
      return;
    (dtn as TextData).Text = str;
  }

  private void SelUkazanie(DocumentTreeNode dtn)
  {
    if (this.ReadOnly)
      return;
    EditReason editReason = new EditReason();
    string str = "";
    if (dtn != null && dtn is TextData)
      str = (dtn as TextData).Text;
    ref string local = ref str;
    Guid attrGuid = new Guid(RevHelper.guidAttrUkazanieOVnedrenii);
    if (editReason.Execute(ref local, attrGuid) == "-2" || dtn == null || !(dtn is TextData))
      return;
    (dtn as TextData).Text = str;
  }

  private void SelectSeriesDates(DocumentTreeNode dtn)
  {
    if (this.ReadOnly || !ECOPlugin.EnabledSeriesDates())
      return;
    if (this.addedLinks.Count > 0 && this.ECO.DocumentECO.Modified)
    {
      switch (MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_339"), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.YesNoCancel))
      {
        case DialogResult.Cancel:
          return;
        case DialogResult.Yes:
          this.SaveRevision();
          break;
      }
    }
    SeriesDatesForm seriesDatesForm = new SeriesDatesForm();
    SeriesDatesApplicabilityCollection sdac = new SeriesDatesApplicabilityCollection();
    sdac.FromString(this.eco.SDAC);
    if (seriesDatesForm.Execute(this.eco.EcoObjectID, ref sdac) != DialogResult.OK)
      return;
    this.UndoManager.Clear();
    this.UndoManager.LockUndo();
    try
    {
      this.UpdateSeriesDatesText(sdac, seriesDatesForm.addComplect);
      this.eco.SDAC = sdac.ToString();
    }
    finally
    {
      this.UndoManager.UnlockUndo();
    }
  }

  private void UpdateSeriesDatesText(SeriesDatesApplicabilityCollection sdac, bool addComplect)
  {
    DocumentTreeNode templateRecursive = this.eco.DocumentECO.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idUkazVnedrenie);
    if (templateRecursive == null || !(templateRecursive is TextData))
      return;
    TextData textData = templateRecursive as TextData;
    if (sdac == null || sdac.Items.Count == 0)
    {
      textData.Text = "";
    }
    else
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index1 = 0; index1 < sdac.Items.Count; ++index1)
        {
          SeriesDatesApplicability datesApplicability = sdac.Items[index1];
          if (datesApplicability.Set != null && datesApplicability.Set.Count > 0)
          {
            if (stringBuilder1.Length == 0)
              stringBuilder1.Append(LocalizationHolder.rm.GetString("ECO.Client_266"));
            if (index1 > 0)
              stringBuilder1.AppendLine();
            stringBuilder1.Append(LocalizationHolder.rm.GetString("ECO.Client_267"));
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(datesApplicability.MainObjectID);
            stringBuilder1.Append(objectInfo.Caption);
            stringBuilder1.Append(" ");
            if (datesApplicability.Applicability == ApplicabilityBy.Series)
            {
              Intermech.Interfaces.Sets.Set<int> set = datesApplicability.Set as Intermech.Interfaces.Sets.Set<int>;
              for (int index2 = 0; index2 < set.Ranges.Count; ++index2)
              {
                Int32Range range = (Int32Range) set.Ranges[index2];
                if (range.MinValue == int.MinValue || range.MaxValue == int.MaxValue)
                {
                  if (range.IsLeftOpen && !range.IsRightOpen)
                  {
                    stringBuilder1.Append(LocalizationHolder.rm.GetString("ECO.Client_271") + Convert.ToString(range.MaxValue));
                    if (addComplect)
                      stringBuilder1.Append(" " + LocalizationHolder.rm.GetString("ECO.Client_269"));
                  }
                  if (!range.IsLeftOpen && range.IsRightOpen)
                  {
                    stringBuilder1.Append(LocalizationHolder.rm.GetString("ECO.Client_268") + Convert.ToString(range.MinValue));
                    if (addComplect)
                      stringBuilder1.Append(" " + LocalizationHolder.rm.GetString("ECO.Client_270"));
                  }
                }
                else
                {
                  stringBuilder1.Append($"{LocalizationHolder.rm.GetString("ECO.Client_268")}{Convert.ToString(range.MinValue)} {LocalizationHolder.rm.GetString("ECO.Client_271")}{Convert.ToString(range.MaxValue)}");
                  if (addComplect)
                    stringBuilder1.Append(" " + LocalizationHolder.rm.GetString("ECO.Client_269"));
                }
                if (index2 < set.Ranges.Count - 1)
                  stringBuilder1.Append(", ");
              }
            }
            else
            {
              Intermech.Interfaces.Sets.Set<DateTime> set = datesApplicability.Set as Intermech.Interfaces.Sets.Set<DateTime>;
              for (int index3 = 0; index3 < set.Ranges.Count; ++index3)
              {
                DateTimeRange range = (DateTimeRange) set.Ranges[index3];
                DateTime dateTime;
                if (range.IsOpen)
                {
                  if (range.IsLeftOpen && !range.IsRightOpen)
                  {
                    StringBuilder stringBuilder2 = stringBuilder1;
                    string str1 = LocalizationHolder.rm.GetString("ECO.Client_271");
                    dateTime = range.MaxValue;
                    string shortDateString = dateTime.ToShortDateString();
                    string str2 = str1 + shortDateString;
                    stringBuilder2.Append(str2);
                  }
                  if (!range.IsLeftOpen && range.IsRightOpen)
                  {
                    StringBuilder stringBuilder3 = stringBuilder1;
                    string str3 = LocalizationHolder.rm.GetString("ECO.Client_268");
                    dateTime = range.MinValue;
                    string shortDateString = dateTime.ToShortDateString();
                    string str4 = str3 + shortDateString;
                    stringBuilder3.Append(str4);
                  }
                }
                else
                {
                  StringBuilder stringBuilder4 = stringBuilder1;
                  string[] strArray = new string[5]
                  {
                    LocalizationHolder.rm.GetString("ECO.Client_268"),
                    null,
                    null,
                    null,
                    null
                  };
                  dateTime = range.MinValue;
                  strArray[1] = dateTime.ToShortDateString();
                  strArray[2] = " ";
                  strArray[3] = LocalizationHolder.rm.GetString("ECO.Client_271");
                  dateTime = range.MaxValue;
                  strArray[4] = dateTime.ToShortDateString();
                  string str = string.Concat(strArray);
                  stringBuilder4.Append(str);
                }
                if (index3 < set.Ranges.Count - 1)
                  stringBuilder1.Append(", ");
              }
            }
          }
        }
      }
      textData.Text = stringBuilder1.ToString();
    }
  }

  internal void ReportSDAC(SeriesDatesApplicabilityCollection sdac, IUserSession session)
  {
    session.EventLog.AddToTrace("========= UpdateSeries report ==========", 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("Count = " + Convert.ToString(sdac.Items.Count), 0, "D:\\SER_TRACE.TXT");
    foreach (SeriesDatesApplicability datesApplicability in sdac.Items)
      session.EventLog.AddToTrace($"mainObjId = {Convert.ToString(datesApplicability.MainObjectID)} toString = {datesApplicability.ToString()}", 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("ToString() = " + Convert.ToString(sdac.ToString()), 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("========= sdac end rep ==========", 0, "D:\\SER_TRACE.TXT");
  }

  internal void ReportSDA(SeriesDatesApplicability sda, int num, IUserSession session)
  {
    session.EventLog.AddToTrace("SDA num = " + Convert.ToString(num), 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("SDA value = " + Convert.ToString(sda.ToString()), 0, "D:\\SER_TRACE.TXT");
  }

  private void AddUsability(DocumentTreeNode dtn)
  {
    if (this.eco.objLinks.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_67"));
    }
    else
    {
      long objectID1 = -1;
      bool flag = false;
      if (this.eco.objLinks.Count == 1)
      {
        objectID1 = this.eco.objLinks[0].verID;
        List<long> products = this.ConvertDocsToProducts(new List<long>()
        {
          objectID1
        });
        if (products != null && products.Count > 0)
          objectID1 = products[0];
      }
      else
      {
        using (ObjSelect objSelect = new ObjSelect())
        {
          List<long> products = this.ConvertDocsToProducts(this.eco.ObjIdList());
          List<long> longList = objSelect.Execute(products, false, true);
          if (longList.Count == 0)
            return;
          objectID1 = longList[0];
          flag = objSelect.addForDoc;
        }
      }
      if (objectID1 == -1L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
        });
        List<long> longList1 = new List<long>();
        List<long> longList2 = new List<long>();
        longList1.Add(objectID1);
        for (int index = 0; index < longList1.Count; ++index)
        {
          long objectID2 = longList1[index];
          relationCollection.LocalTypesMode = true;
          DataTable dataTable = relationCollection.EntersInVersion(paramSet, objectID2);
          if (dataTable.Rows.Count == 0 && objectID2 != objectID1)
          {
            if (longList2.IndexOf(objectID2) < 0)
              longList2.Add(objectID2);
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64 = Convert.ToInt64(row[0]);
              if (longList1.IndexOf(int64) < 0)
                longList1.Add(int64);
            }
          }
        }
        string str = "";
        foreach (long objectID3 in longList2)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID3, false);
          if (objectActualCopy != null && !MetaDataHelper.IsObjectTypeChildOf(objectActualCopy.ObjectType, RevHelper.idObjRevision))
          {
            IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid != null)
            {
              string asString = attributeByGuid.AsString;
              if (asString != "")
                str = !(str == "") ? $"{str}, {asString}" : asString;
            }
          }
        }
        if (!(str != ""))
          return;
        if (flag)
        {
          IDBAttribute attributeByGuid = sessionKeeper.Session.GetObjectActualCopy(objectID1, false).GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
            str = $"{LocalizationHolder.rm.GetString("ECO.Client_209")}{attributeByGuid.AsString}\r\n{str}";
        }
        string text = (dtn as TextData).Text;
        (dtn as TextData).Text = text + (text != "" ? "\r\n" : "") + str;
      }
    }
  }

  private List<long> ConvertDocsToProducts(List<long> objIDs)
  {
    List<long> products = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in objIDs)
      {
        if (this.IsDocId(sessionKeeper.Session, objId))
        {
          DataTable dataTable = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkDocForIzd).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
          {
            (object) -2,
            (object) -7
          }), objId);
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              int int32 = Convert.ToInt32(row[1]);
              if (ECOPlugin.plugin.ProdTypes.Contains(int32))
              {
                long int64 = Convert.ToInt64(row[0]);
                if (!products.Contains(Math.Abs(int64)))
                  products.Add(Math.Abs(int64));
              }
            }
          }
        }
        else if (!products.Contains(Math.Abs(objId)))
          products.Add(Math.Abs(objId));
      }
    }
    return products;
  }

  private void SelDate(DocumentTreeNode dtn)
  {
    if (this.ReadOnly)
      return;
    TextData textData = (TextData) null;
    if (dtn != null && dtn is TextData)
      textData = dtn as TextData;
    if (textData == null)
      return;
    if (textData.Id == Intermech.ECO.Client.ECO.idEndChangeTerm && !this.eco.AllGoalsChange())
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_327"), LocalizationHolder.rm.GetString("ECO.Client_147"), MessageBoxButtons.OK);
    }
    else
    {
      DateTime dt = DateTime.Now;
      try
      {
        dt = Convert.ToDateTime(textData.Text);
      }
      catch (FormatException ex)
      {
      }
      IPageElementWithInterface elementWithInterface = textData as IPageElementWithInterface;
      Rectangle rectangle = Rectangle.Empty;
      if (elementWithInterface != null)
        rectangle = elementWithInterface.PageUI.Bounds;
      Intermech.ECO.Client.SelDate selDate = new Intermech.ECO.Client.SelDate();
      Point screen = elementWithInterface.PageUI.PageControl.PointToScreen(Point.Empty);
      Point loc = new Point(screen.X + rectangle.X, screen.Y + rectangle.Bottom);
      DateTime dateTime = selDate.Execute(dt, loc);
      bool flag = this.eco.revType == RevType.PI;
      switch (selDate.dr)
      {
        case DialogResult.OK:
          textData.Text = dateTime.ToShortDateString();
          if (textData.Id == Intermech.ECO.Client.ECO.idStartChangeTerm)
            this.eco.changeTermStart = dateTime;
          if ((flag || !(textData.Id == Intermech.ECO.Client.ECO.idEndChangeTerm)) && (!flag || !(textData.Id == Intermech.ECO.Client.ECO.idPITerm)))
            break;
          this.eco.changeTermEnd = dateTime;
          break;
        case DialogResult.Yes:
          textData.Text = "";
          if (textData.Id == Intermech.ECO.Client.ECO.idStartChangeTerm)
            this.eco.changeTermStart = DateTime.MinValue;
          if ((flag || !(textData.Id == Intermech.ECO.Client.ECO.idEndChangeTerm)) && (!flag || !(textData.Id == Intermech.ECO.Client.ECO.idPITerm)))
            break;
          this.eco.changeTermEnd = DateTime.MinValue;
          break;
      }
    }
  }

  private void IncludeProductsForDocuments()
  {
    List<string> stringList = new List<string>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = new List<long>();
      HashSet<long> longSet = new HashSet<long>();
      foreach (PendingLink objLink in this.eco.objLinks)
      {
        long num1 = Math.Abs(objLink.verID);
        if (!longList.Contains(num1) && this.IsDocId(sessionKeeper.Session, objLink.verID))
          longList.Add(num1);
        if (!longSet.Contains(num1))
          longSet.Add(num1);
        if (objLink.auxObjects != null && objLink.auxObjects.Count > 0)
        {
          foreach (ObjInfo auxObject in objLink.auxObjects)
          {
            long num2 = Math.Abs(auxObject.verId);
            if (!longList.Contains(num2) && this.IsDocId(sessionKeeper.Session, auxObject.verId))
              longList.Add(num2);
          }
        }
      }
      List<long> versionIDs = new List<long>();
      List<long> fIDs = new List<long>();
      foreach (long objectID in longList)
      {
        DataTable dataTable = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkDocForIzd).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[5]
        {
          (object) -2,
          (object) -7,
          (object) -3,
          (object) -50,
          (object) -15
        }), objectID);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            int int32 = Convert.ToInt32(row[1]);
            if (ECOPlugin.plugin.ProdTypes.Contains(int32))
            {
              long int64 = Convert.ToInt64(row[0]);
              if (Convert.ToInt64(row[4]) == 0L && !longSet.Contains(Math.Abs(int64)))
              {
                longSet.Add(Math.Abs(int64));
                versionIDs.Add(int64);
                fIDs.Add(Convert.ToInt64(row[2]));
                stringList.Add($"{Convert.ToString(row[3])} [{Convert.ToString(int64)}]");
              }
            }
          }
        }
      }
      if (versionIDs.Count > 0)
        (sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).AddToContext((object) sessionKeeper.Session.SessionGUID, this.eco.EcoObjectID, this.eco.linkedContextNo, (IList<long>) fIDs, (IList<long>) versionIDs, true, true);
    }
    if (stringList.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(LocalizationHolder.rm.GetString("ECO.Client_375"));
      foreach (string str in stringList)
        stringBuilder.AppendLine(str);
      int num = (int) MessageBox.Show(stringBuilder.ToString(), LocalizationHolder.rm.GetString("ECO.Client_96"), MessageBoxButtons.OK);
    }
    else
    {
      int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_376"), LocalizationHolder.rm.GetString("ECO.Client_96"), MessageBoxButtons.OK);
    }
  }

  private bool IsDocId(IUserSession ius, long objId)
  {
    QuickObjectInfo objectInfo = ius.GetObjectInfo(objId);
    return ECOPlugin.plugin.DocTypes.Contains(objectInfo.ObjectTypeID);
  }

  private void InsPictFromFile(object sender, EventArgs e) => this.ImageFromFile(this.elPicture);

  private void InsPictFromObject(object sender, EventArgs e) => this.ImageFromObj(this.elPicture);

  private void InsPictFromClip(object sender, EventArgs e) => this.ImageFromClip(this.elPicture);

  private bool IsPictInClip() => this.elPicture != null && this.elPicture.CanPasteFromClipboard();

  public override void SetStatusBar(StatusBar statusBar)
  {
    base.SetStatusBar(statusBar);
    statusBar?.Panels.Insert(2, this.plugin.scalePanel);
  }

  public override void RestoreStatusBar(StatusBar statusBar)
  {
    base.RestoreStatusBar(statusBar);
    if (statusBar == null || this.plugin.scalePanel == null)
      return;
    statusBar.Panels.Remove(this.plugin.scalePanel);
  }

  private void CreateOLEPict(object sender, EventArgs e) => this.CreateOLEObj(this.elPicture);

  private void ToggleOriginalSize(object sender, EventArgs e)
  {
    if (this.elPicture == null)
      return;
    this.elPicture.ScaleMode = !((ButtonItemBase) sender).Checked ? ImageScaleMode.OriginalAutoSize : ImageScaleMode.FitWidthHeight;
  }

  public override void Activated()
  {
    base.Activated();
    if (this._activated)
      return;
    try
    {
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      this.FiltrationInitToolbar();
      this._saveMode = new EditingContextMode?(this.TryActivateContext());
      service.LockEditingContextID = true;
      this._filtrationService.OnFiltrationChanged += new Intermech.Interfaces.Client.FiltrationChanged(((ECOAncestorForm) this).FiltrationChanged);
      ECOPlugin plugin = ECOPlugin.FindPlugin();
      if (plugin == null)
        return;
      plugin.CurRevId = this.ECO.EcoObjectID;
      plugin.UpdateISimpleSelectedItemsService();
    }
    finally
    {
      this._activated = true;
    }
  }

  public override void Deactivated()
  {
    base.Deactivated();
    if (!this._activated)
      return;
    try
    {
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      service.LockEditingContextID = false;
      this._filtrationService.OnFiltrationChanged -= new Intermech.Interfaces.Client.FiltrationChanged(((ECOAncestorForm) this).FiltrationChanged);
      if (this._saveMode.HasValue)
        service.EditingContextMode = this._saveMode.Value;
      this.FiltrationClearToolbar();
      ECOPlugin plugin = ECOPlugin.FindPlugin();
      if (plugin == null)
        return;
      plugin.CurRevId = 0L;
      plugin.NavigatorMenuItems = (ISelectedItems) null;
    }
    finally
    {
      this._activated = false;
    }
  }

  protected override void Do_DeleteFiltrationSettings()
  {
    if (this._FiltrationOwnerID.Length <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).DeleteRuleTuning((object) sessionKeeper.Session.SessionGUID, this._FiltrationOwnerID);
      this._FiltrationOwnerID = string.Empty;
    }
  }

  private void ECOEditorForm_Closed(object sender, EventArgs e)
  {
    if (this.HideOnClose)
      return;
    this.Do_DeleteFiltrationSettings();
  }

  private void DropActiveEditor()
  {
    if (!(this.DocumentControl.ActiveElement is PageElementNode activeElement) || !activeElement.InPlaceEditorActive)
      return;
    this.DocumentControl.SetActiveElement((DocumentTreeNode) null, false, Point.Empty);
    this.DocumentControl.SetSelection(new List<DocumentTreeNode>(), false, false);
  }

  private EcoTreeViewDlg CreateECOTreeView()
  {
    if (this.ecoTreeViewDlg != null)
    {
      this.ecoTreeViewDlg.Close();
      this.ecoTreeViewDlg.Dispose();
      this.ecoTreeViewDlg = (EcoTreeViewDlg) null;
    }
    DockControl dockControl = (DockControl) null;
    if (this.DockManager != null)
      dockControl = this.DockManager.FindDockControl(EcoTreeViewDlg.DockGuid);
    if (dockControl == null || !(dockControl is EcoTreeViewDlg))
    {
      this.ecoTreeViewDlg = new EcoTreeViewDlg();
      this.ecoTreeViewDlg.Form = this;
      this.ecoTreeViewDlg.Text = LocalizationHolder.rm.GetString("ECO.Client_255");
      this.DockManagerStorage.SetControl((DockControl) this.ecoTreeViewDlg);
    }
    return this.ecoTreeViewDlg;
  }

  public EcoTreeViewDlg ECOTreeViewDlg => this.ecoTreeViewDlg;

  private void SaveTreeConfig()
  {
  }

  protected override DockControl GetDockControl(Guid guid, string persistString, string text)
  {
    if (!(guid == EcoTreeViewDlg.DockGuid))
      return base.GetDockControl(guid, persistString, text);
    this.ShowECOTreeView(false);
    return (DockControl) this.ecoTreeViewDlg;
  }

  private void ShowECOTreeView(bool show)
  {
    if (this.DocumentControl == null)
      return;
    if (this.ecoTreeViewDlg == null)
      this.CreateECOTreeView();
    if (this.ecoTreeViewDlg == null)
      return;
    this.ecoTreeViewDlg.DocumentControl = this.DocumentControl;
    this.ecoTreeViewDlg.ECOEditorForm = this;
    this.ecoTreeViewDlg.TreeRoot = new ECOTreeItem(0L, "Документы включенные в извещение", (DocumentTreeNode) this.DocumentControl.Document);
    this.ecoTreeViewDlg.UpdateSelection();
    if (!show)
      return;
    this.ecoTreeViewSettings = this.DockManagerStorage.GetSettings((DockControl) this.ecoTreeViewDlg);
    this.ecoTreeViewSettings.Open((DockControl) this.ecoTreeViewDlg, this.DockManager);
  }

  public override string HelpID => "840";

  private class ItemToDelete
  {
    public int index;
    public TableData tdata;

    public ItemToDelete(int chNodeIndex, TableData td)
    {
      this.index = chNodeIndex;
      this.tdata = td;
    }
  }
}

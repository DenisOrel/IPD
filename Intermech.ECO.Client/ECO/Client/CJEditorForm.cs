// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.CJEditorForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.ECO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.Signs.Client;
using Intermech.Signs.Interfaces;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class CJEditorForm : ECOAncestorForm
{
  public static readonly Guid CJWindowGuid = new Guid("{B97AB0FF-958C-4532-A77E-50FB44DCDCB2}");
  private bool lockChange;
  private string CJ_Design = "";
  private int _maxRecNum;
  private ImageList IL;
  private IContainer components;
  public static readonly string idChangeRecord = "REC_CJ";
  public static readonly string idChangeNum = "J001";
  public static readonly string idChangeDate = "J002";
  public static readonly string idChangeDesign = "J003";
  public static readonly string idChangeContents = "J004";
  public static readonly string idChangeSigns = "J005";
  public static readonly string idChangeOriginals = "J006";
  public static readonly string idChangeCopies = "J007";
  public static readonly string idChangeComments = "J008";
  public static readonly string cjRecordAttr = "_CJ_ID_";
  private SortedList<long, PendingLink> objLinks = new SortedList<long, PendingLink>();
  private OpenFileDialog openPictDlg;
  private SortedList<long, CJEditorForm.RecInfo> objRecIndex = new SortedList<long, CJEditorForm.RecInfo>();
  internal Dictionary<long, CJEditorForm.CJRecordInfo> recTable;
  internal List<CJEditorForm.IdGuid> cjRecList;
  private List<PageElementUI> items = new List<PageElementUI>();
  private MenuButtonItem includeElem;
  private MenuButtonItem removeElem;
  private MenuButtonItem selPictFromBase;
  private MenuButtonItem replaceWithECO;
  private MenuButtonItem signRecord;
  private MenuButtonItem signRecordAs;
  private MenuButtonItem sendByProcess;
  private TableElement elWorkspace;
  private TableElement elCurChange;
  private int indexCurChange = -1;
  private TableElement elCurElem;
  private ContainerElement elPicture;
  private TextData textFld;
  private long recId = -1;
  private List<TableElement> selList;

  public override string DocumentCaption
  {
    get
    {
      string documentCaption = base.DocumentCaption;
      if (documentCaption != null && documentCaption != "")
        return documentCaption;
      string documentName = this.DocumentName;
      string documentDesignation = this.DocumentDesignation;
      if (documentName != null && documentName != "" && documentDesignation != null && documentDesignation != "")
        return $"{documentDesignation}({documentName})";
      if (documentName != null && documentName != "")
        return documentName;
      return documentDesignation != null && documentDesignation != "" ? documentDesignation : LocalizationHolder.rm.GetString("ECO.Client_22");
    }
    set => base.DocumentCaption = value;
  }

  protected override string GetConfigName() => "CJ.Editor";

  protected override string GetToolbarConfigName() => "CJ.Toolbar";

  protected override void Init()
  {
    base.Init();
    this.Guid = CJEditorForm.CJWindowGuid;
    this.InitializeComponent();
  }

  protected override void DocumentControl_BeforeSelectionChanged(
    object sender,
    BeforeSelectionChanged_EventArgs e)
  {
    if (e.Selection == null)
      return;
    if (e.Selection.Count == 1 && e.Selection[0].IsVirtualNode)
    {
      if (!(e.Selection[0] is RectangleElement rectangleElement))
        return;
      int index = 0;
      while (index < rectangleElement.NodesCount)
      {
        if (rectangleElement.Nodes[index].TemplateId != CJEditorForm.idChangeRecord)
          rectangleElement.RemoveChildNodeAt(index, false, false);
        else
          ++index;
      }
    }
    else
    {
      if (e.Selection.Count <= 1)
        return;
      int index = 0;
      while (index < e.Selection.Count)
      {
        if (e.Selection[index].TemplateId != CJEditorForm.idChangeRecord)
          e.Selection.RemoveAt(index);
        else
          ++index;
      }
    }
  }

  protected override void ControlSelChanged(object sender, SelectionChanged_EventArgs e)
  {
    if (this.plugin == null)
      return;
    List<TableElement> selectionList = this.GetSelectionList();
    if (selectionList != null && selectionList.Count > 0)
    {
      List<long> longList = new List<long>();
      if (selectionList.Count > 1)
      {
        foreach (int recNum in this.GetRecNumList(selectionList))
        {
          long id = this.cjRecList[recNum].id;
          longList.Add(id);
        }
      }
      else
      {
        if (this.recId == -1L)
          this.recId = this.cjRecList[this.GetRecNum(selectionList[0])].id;
        longList.Add(this.recId);
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = longList.Count - 1; index >= 0; --index)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(longList[index], false);
          if (objectActualCopy == null)
            longList.RemoveAt(index);
          else
            longList[index] = objectActualCopy.ObjectID;
        }
      }
      this.plugin.NavigatorMenuItems = ObjectExtensions.GetItems(longList.ToArray());
    }
    else
      this.plugin.NavigatorMenuItems = (ISelectedItems) null;
    this.plugin.UpdateISimpleSelectedItemsService();
  }

  public CJEditorForm(IImDocumentManager documentManager, ImDocument document, bool readOnly)
    : base(documentManager, document, readOnly)
  {
  }

  public CJEditorForm(
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
      this.DocumentControl.ReadOnly = readOnly;
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
          if (activeElement1 is TextBoxElement && !(activeElement1 as TextBoxElement).ReadOnly)
          {
            TextBoxElement textBoxElement = (TextBoxElement) activeElement1;
            string text = textBoxElement.InPlaceEditorControl == null ? this.textFld.Text : ((ImRtfEditor) textBoxElement.InPlaceEditorControl).TerGetTextSel();
            if (text != "")
            {
              try
              {
                Clipboard.SetText(text, TextDataFormat.UnicodeText);
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
          if (activeElement2 is TextBoxElement && !(activeElement2 as TextBoxElement).ReadOnly)
          {
            TextBoxElement textBoxElement = (TextBoxElement) activeElement2;
            string text = textBoxElement.InPlaceEditorControl == null ? this.textFld.Text : ((ImRtfEditor) textBoxElement.InPlaceEditorControl).TerGetTextSel();
            if (text != "")
            {
              try
              {
                Clipboard.SetText(text, TextDataFormat.UnicodeText);
              }
              catch (ExternalException ex)
              {
              }
            }
            if (textBoxElement.InPlaceEditorControl != null)
              ((ImRtfEditor) textBoxElement.InPlaceEditorControl).TerDeleteBlock(true);
            else
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
        case "ECO.AttachGroupToECO":
          this.ClearSelection();
          return true;
        case "ECO.AttachToECO":
          this.elCurChange = (TableElement) null;
          this.ClearSelection();
          return true;
        case "ECO.AttachToECO_ExternalDoc":
          this.ClearSelection();
          return true;
        case "ECO.CalculateWhereUsedColumn":
          int num = (int) MessageBox.Show("ECO.CalculateWhereUsedColumn", LocalizationHolder.rm.GetString("ECO.Client_26"), MessageBoxButtons.OK);
          return true;
        case "ECO.Card":
          ECOPlugin.FindPlugin().InvokeCommandForObject(this.ECO.EcoObjectID, "ParametersCard");
          return true;
        case "ECO.ChangeReason":
          this.SelReason((DocumentTreeNode) null);
          return true;
        case "ECO.DetachFromECO":
          return true;
        case "ECO.InsertList":
          int index = new SelTemplateList().Execute(this.Document.Template);
          if (index < 0)
            return true;
          DocumentTreeNode node = this.Document.Template.Nodes[index];
          if (node != null)
            this.DocumentControl.InsertNewPage(node.Id, false);
          return true;
        case "ECO.PasteObjects":
          return true;
        case "ECO.SetSeriesDates":
          return true;
        case "ECO.SortByDes":
          return true;
        case "ECO.Tree":
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
                ImRtfEditor placeEditorControl = (ImRtfEditor) textBoxElement1.InPlaceEditorControl;
                placeEditorControl.TerDeleteBlock(false);
                placeEditorControl.TerInsertText(Clipboard.GetText(), -1, -1, true);
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
            this.SaveChangeJournal();
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
        case "Format.SetupTextDirrection":
        case "Format.TextColor":
        case "UpdateTable":
          commandState.Enabled = false;
          return true;
        case "Copy":
        case "Cut":
          DocumentTreeNode activeElement1 = this.DocumentControl != null ? this.DocumentControl.ActiveElement : (DocumentTreeNode) null;
          if (!this.ReadOnly || activeElement1 != null)
          {
            switch (activeElement1)
            {
              case TextBoxElement _:
                TextBoxElement textBoxElement = (TextBoxElement) activeElement1;
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
                commandState.Enabled = ((ContainerData) activeElement1).Image != null;
                return true;
            }
          }
          commandState.Enabled = false;
          return true;
        case "Delete":
          commandState.Enabled = false;
          return true;
        case "ECO":
          commandState.Visible = false;
          return true;
        case "ECO.AttachGroupToECO":
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
        case "ECO.ChangeReason":
          commandState.Enabled = !this.ReadOnly;
          return true;
        case "ECO.DetachFromECO":
          commandState.Enabled = !this.ReadOnly && this.objLinks.Count > 0;
          return true;
        case "ECO.InsertList":
          commandState.Enabled = false;
          return true;
        case "ECO.PasteObjects":
          commandState.Enabled = false;
          return true;
        case "ECO.SortByDes":
          commandState.Enabled = !this.ReadOnly;
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
          DocumentTreeNode activeElement2 = this.DocumentControl != null ? this.DocumentControl.ActiveElement : (DocumentTreeNode) null;
          if (activeElement2 != null)
          {
            if (activeElement2 is TextBoxElement)
            {
              if (!(activeElement2 as TextBoxElement).ReadOnly)
              {
                try
                {
                  commandState.Enabled = Clipboard.ContainsText();
                  goto label_30;
                }
                catch
                {
                  commandState.Enabled = true;
                  goto label_30;
                }
              }
            }
            if (activeElement2 is ContainerElement)
            {
              if (!(activeElement2 as ContainerElement).ReadOnly)
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
          }
label_30:
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

  private void externalDocDesignChanged(object sender, TextChanged_EventArgs e)
  {
    if (this.lockChange)
      return;
    if (sender != null && sender is TextData)
      ((DocumentTreeNode) sender).SetAttributeValue(Intermech.ECO.Client.ECO.textAttr, "");
    this.UpdateDocDesign();
    this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
  }

  public static bool IsEcoContentsDocNode(DocumentTreeNode docNode)
  {
    return docNode != null && docNode.Name == LocalizationHolder.rm.GetString("ECO.Client_62");
  }

  public static DocumentTreeNode FindParentEcoContentsDocNode(DocumentTreeNode docNode)
  {
    for (; docNode != null; docNode = docNode.Parent)
    {
      if (CJEditorForm.IsEcoContentsDocNode(docNode))
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
    return CJEditorForm.FindParentEcoContentsDocNode(docNode) != null;
  }

  public TableElement CurrentChange => (TableElement) null;

  public void AfterLoadDoc()
  {
    if (!this.Document.DocumentControl.ReadOnly)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.eco.EcoObjectID);
        IDBAttribute attributeById1 = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
        if (attributeById1 != null)
          this.CJ_Design = Convert.ToString(attributeById1.Value);
        if (!this.ReadOnly)
        {
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(RevHelper.idAttrVersion);
          if (attributeById2 != null)
          {
            this.eco.ecoVersion = attributeById2.AsInteger;
          }
          else
          {
            string attributeValue = this.eco.ecoMainTable.GetAttributeValue(Intermech.ECO.Client.ECO.versionIdAttr, true);
            if (attributeValue == "")
              this.eco.ecoVersion = 0L;
            else
              this.eco.ecoVersion = (long) Convert.ToInt32(attributeValue);
          }
          IDBAttribute attributeById3 = dbObject.GetAttributeByID(RevHelper.idAttrMaxCJNum);
          if (attributeById3 != null)
            this._maxRecNum = Convert.ToInt32(attributeById3.Value);
        }
      }
    }
    this.DocumentControl.HyperLinkActivated += new HyperLinkActivated_EventHandler(this.DocumentControl_HyperLinkActivated);
  }

  public void UpdateDocDesign()
  {
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.IL = new ImageList(this.components);
    this.openPictDlg = new OpenFileDialog();
    this.SuspendLayout();
    this.IL.ColorDepth = ColorDepth.Depth8Bit;
    this.IL.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.IL.TransparentColor = Color.Transparent;
    this.openPictDlg.DefaultExt = "PNG";
    this.openPictDlg.Filter = "Все изображения|*.PNG;*.JPG;*.BMP;*.GIF|Изображения .PNG|*.PNG|Изображения .JPG|*.JPG|Изображения .BMP|*.BMP|Все файлы|*.*";
    this.openPictDlg.Title = "Выберите файл вставляемого изображения";
    this.openPictDlg.RestoreDirectory = true;
    this.AllowDrop = true;
    this.Name = nameof (CJEditorForm);
    this.Closed += new EventHandler(this.ECOEditorForm_Closed);
    this.ResumeLayout(false);
  }

  public override void OnClosed(EventArgs e) => base.OnClosed(e);

  protected override void OnClosing(CancelEventArgs e)
  {
    if (e.Cancel || !this.AskForSaveBeforeClose || this.ReadOnly || !this.ObjectAssigned || this.Document == null || !this.Document.Modified)
      return;
    switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_63"), (object) this.DocumentCaption), LocalizationHolder.rm.GetString("ECO.Client_64"), MessageBoxButtons.YesNoCancel))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        try
        {
          if (this.SaveChangeJournal())
          {
            this.Document.Modified = false;
            break;
          }
          e.Cancel = true;
          break;
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
          e.Cancel = true;
          break;
        }
    }
  }

  public List<Guid> _IdListToGuidList(List<long> idList)
  {
    List<Guid> guidList = new List<Guid>();
    foreach (long id in idList)
    {
      if (this.objLinks.ContainsKey(Math.Abs(id)))
        guidList.Add(this.objLinks[Math.Abs(id)].verGuid);
    }
    return guidList;
  }

  public List<long> _GetIdList(string tagStr)
  {
    if (this.eco.IdLists())
      return Intermech.ECO.Client.ECO.Str2IdList(tagStr);
    List<long> idList = new List<long>();
    foreach (Guid str2Guid in Intermech.ECO.Client.ECO.Str2GuidList(tagStr))
    {
      foreach (PendingLink pendingLink in (IEnumerable<PendingLink>) this.objLinks.Values)
      {
        if (pendingLink.verGuid.Equals(str2Guid))
          idList.Add(pendingLink.verID);
      }
    }
    return idList;
  }

  public List<Guid> _GetGuidList(string tagStr)
  {
    if (!this.eco.IdLists())
      return Intermech.ECO.Client.ECO.Str2GuidList(tagStr);
    List<Guid> guidList = new List<Guid>();
    foreach (long str2Id in Intermech.ECO.Client.ECO.Str2IdList(tagStr))
    {
      if (this.objLinks.ContainsKey(Math.Abs(str2Id)))
        guidList.Add(this.objLinks[Math.Abs(str2Id)].verGuid);
    }
    return guidList;
  }

  public string _SetIdList(RectangleElement te, List<long> IdList)
  {
    if (this.eco.IdLists())
    {
      string str = Intermech.ECO.Client.ECO.IdListToStr(IdList);
      te.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, str);
      return str;
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (long id in IdList)
    {
      if (this.objLinks.ContainsKey(Math.Abs(id)))
      {
        PendingLink objLink = this.objLinks[Math.Abs(id)];
        if (objLink.verID == id || objLink.verID == -id)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(objLink.verGuid.ToString());
        }
      }
    }
    string attributeValue = stringBuilder.ToString();
    te.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
    return attributeValue;
  }

  public string DesignListStr(List<long> idList)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (long id in idList)
    {
      if (this.objLinks.ContainsKey(Math.Abs(id)))
      {
        PendingLink objLink = this.objLinks[Math.Abs(id)];
        if (objLink.design != null && objLink.design != "")
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(objLink.design);
        }
      }
    }
    return stringBuilder.ToString();
  }

  internal int GetCurRecNum()
  {
    if (this.elCurChange == null || !(this.elCurChange.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) is TextData templateRecursive))
      return -1;
    int num;
    try
    {
      num = Convert.ToInt32(templateRecursive.Text) - 1;
    }
    catch
    {
      return -1;
    }
    return num < 0 ? -1 : num;
  }

  internal int GetCurRecNum(TableElement change)
  {
    if (change == null || !(change.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) is TextData templateRecursive))
      return -1;
    int num;
    try
    {
      num = Convert.ToInt32(templateRecursive.Text) - 1;
    }
    catch
    {
      return -1;
    }
    return num < 0 ? -1 : num;
  }

  private void CreateNewRecord()
  {
    string ecoObjectName = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.ecoID, false);
      if (dbObject != null)
        ecoObjectName = dbObject.NameInMessages;
    }
    long objId = CJEditorForm.AskForObject(ecoObjectName);
    IncludeGoal includeGoal = new IncludeGoal();
    if (!includeGoal.ExecuteForCJ(objId, this.eco.litera))
      return;
    ECOGoal goal = includeGoal.goal;
    long finalObject = includeGoal.GetFinalObject();
    Hashtable synchroTab = new Hashtable();
    if (goal == ECOGoal.Litera)
    {
      List<long> synchroList = new List<long>();
      if (ECOPlugin.GetSynchroParents(finalObject, synchroList))
      {
        if (synchroList.Count == 1)
        {
          synchroTab.Add((object) finalObject, (object) synchroList);
        }
        else
        {
          ChooseSynchroDlg chooseSynchroDlg = new ChooseSynchroDlg();
          if (!chooseSynchroDlg.Execute(synchroList, finalObject))
            return;
          List<long> longList = chooseSynchroDlg.ComposeChosenList();
          if (longList.Count > 0)
            synchroTab.Add((object) finalObject, (object) longList);
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.AttachItemsToCJ(finalObject, sessionKeeper.Session, includeGoal.goal, includeGoal.schemaId, includeGoal.selLCStepId, synchroTab);
  }

  public static long AskForObject(string ecoObjectName)
  {
    using (ServiceContainer serviceContainer = new ServiceContainer())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IObjectTypeNodeFilter serviceInstance = (IObjectTypeNodeFilter) new ObjectTypeNodeFilter();
        serviceContainer.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
        List<int> allowedTypes = ECOPlugin.FindPlugin().GetAllowedTypes(RevHelper.idObjCJRecord);
        List<int> intList = new List<int>(0);
        foreach (int childTypeID in allowedTypes)
        {
          List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(childTypeID);
          parentsIdReverse.Add(childTypeID);
          for (int index = 0; index < parentsIdReverse.Count; ++index)
          {
            int num = parentsIdReverse[index];
            if (allowedTypes.Contains(num))
            {
              if (!intList.Contains(num))
              {
                intList.Add(num);
                break;
              }
              break;
            }
          }
        }
        DescriptorCollection descriptors = new DescriptorCollection();
        descriptors.Add((IDescriptor) new ObjectTypesDescriptor(intList.ToArray(), LocalizationHolder.rm.GetString("ECO.Client_32")));
        Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("ECO.Client_398"), descriptors);
        IArchivesDescriptorService service = (IArchivesDescriptorService) ServicesManager.GetService(typeof (IArchivesDescriptorService));
        if (service != null)
          descriptors.Add(service.GetDescriptor());
        IDBTypedObjectID[] dbTypedObjectIdArray = (IDBTypedObjectID[]) SelectionWindow.Select(string.Format(LocalizationHolder.rm.GetString("ECO.Client_29") + LocalizationHolder.rm.GetString("ECO.Client_306"), (object) ecoObjectName), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect, allowedTypes.ToArray());
        if (dbTypedObjectIdArray == null)
          return 0;
        IDBObject dbObject = sessionKeeper.Session.GetObject(dbTypedObjectIdArray[0].ObjectID, false);
        if (dbObject == null)
          return 0;
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && !(attributeByGuid.AsString == ""))
          return dbObject.ObjectID;
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_229"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
        return 0;
      }
    }
  }

  public long AttachItemsToCJ(
    long objId,
    IUserSession session,
    ECOGoal goal,
    int schemeId,
    int selLCStepId,
    Hashtable synchroTab)
  {
    TableElement tableElement = (TableElement) null;
    if (session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions dbTransactions)
      dbTransactions.StartTransaction();
    long cj = 0;
    try
    {
      this.SuspendDocumentUpdates();
      List<long> relationIDs = new List<long>();
      try
      {
        List<PendingLink> pList = this.AddPendingLink(session, objId, goal, selLCStepId);
        if (pList == null || pList.Count == 0)
          return 0;
        IDBObject dbObject1 = session.GetObjectCollection(RevHelper.idObjCJRecord).Create();
        IDBAttribute dbAttribute1 = dbObject1.Attributes.FindByID(RevHelper.idAttrJournalLink) ?? dbObject1.Attributes.AddAttribute(RevHelper.idAttrJournalLink, false);
        if (dbAttribute1 != null)
          dbAttribute1.AsInteger = this.ecoID;
        IDBAttribute dbAttribute2 = dbObject1.Attributes.AddAttribute(RevHelper.idAttrDesign, false);
        if (dbAttribute2 != null)
          dbAttribute2.AsString = this.CJ_Design + $".{++this._maxRecNum:D4}";
        dbObject1.CommitCreation(true);
        cj = dbObject1.ObjectID;
        session.GetRelationCollection(RevHelper.idChangeJournalContent).Create(this.ecoID, cj, DateTime.Now);
        IDBObject dbObject2 = session.GetObject(this.eco.EcoObjectID, false);
        if (dbObject2 != null)
        {
          IDBAttribute attributeById = dbObject2.GetAttributeByID(RevHelper.idAttrMaxCJNum);
          if (attributeById != null)
            attributeById.AsInteger = (long) this._maxRecNum;
        }
        if (synchroTab != null)
        {
          foreach (long key in (IEnumerable) synchroTab.Keys)
          {
            if (this.objLinks.ContainsKey(Math.Abs(key)))
            {
              PendingLink objLink = this.objLinks[Math.Abs(key)];
              if (objLink != null)
              {
                foreach (long Id in (List<long>) synchroTab[(object) key])
                  objLink.AddAuxObject(Id);
              }
            }
          }
        }
        foreach (PendingLink pl in pList)
        {
          long verId = pl.verID;
          IDBObject dbObject3 = session.GetObject(verId, false);
          if (dbObject3 != null)
          {
            string nchangeNo = this.GetNChangeNo(session, dbObject3.ID, verId, goal);
            IDBRelation revRelation = RevHelper.CreateRevRelation(session, cj, verId, nchangeNo, goal, HidingType.Disabled);
            long relationId = revRelation.RelationID;
            this.AddAuxLinksAttr(revRelation, pl);
            (revRelation.Attributes.FindByID(RevHelper.idAttrDelWhenExcluded) ?? revRelation.Attributes.AddAttribute(RevHelper.idAttrDelWhenExcluded, false)).AsBoolean = pl.needDelete;
            IDBAttribute dbAttribute3 = revRelation.Attributes.AddAttribute(RevHelper.idAttrFutureLC, false);
            if (dbAttribute3 != null)
              dbAttribute3.AsInteger = (long) pl.stepID;
            if (pl.mainGuid != Guid.Empty)
            {
              IDBAttribute dbAttribute4 = revRelation.Attributes.AddAttribute(RevHelper.idAttrMainObjectGuid, false);
              if (dbAttribute4 != null)
                dbAttribute4.AsString = pl.mainGuid.ToString();
            }
            relationIDs.Add(relationId);
          }
        }
        ISignsService customService = session.GetCustomService(typeof (ISignsService)) as ISignsService;
        List<SignParams> signList = (List<SignParams>) null;
        if (customService != null)
          signList = customService.GetObjectSignsParams(cj, session.SessionGUID);
        tableElement = this.AddNewChange(pList, dbObject1.ObjectGUID, cj, DateTime.Now, signList);
        if (tableElement != null && this.recTable.ContainsKey(cj))
        {
          CJEditorForm.CJRecordInfo cjri = this.recTable[cj];
          this.FillExtraFields((TableData) tableElement, cjri);
        }
        foreach (PendingLink pendingLink in pList)
        {
          CJEditorForm.RecInfo recInfo = new CJEditorForm.RecInfo(this.cjRecList.Count - 1, (TableData) tableElement);
          this.objRecIndex.Add(Math.Abs(pendingLink.verID), recInfo);
        }
      }
      catch
      {
        if (dbTransactions != null)
        {
          dbTransactions.Rollback();
          dbTransactions = (IDBTransactions) null;
        }
        throw;
      }
      finally
      {
        if (dbTransactions != null)
        {
          if (cj != 0L)
          {
            dbTransactions.Commit();
            INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
            if (service != null)
            {
              service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", cj));
              if (relationIDs.Count > 0)
                service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs));
            }
            tableElement?.SetAttributeValue(Intermech.ECO.Client.ECO.schemeIdAttr, Convert.ToString(schemeId));
          }
          else
            dbTransactions.Rollback();
        }
      }
    }
    finally
    {
      this.ResumeDocumentUpdates();
    }
    this.SaveChangeJournal();
    this.CommandManager.QueryStatus();
    return cj;
  }

  private List<PendingLink> AddPendingLink(
    IUserSession ius,
    long objID,
    ECOGoal goal,
    int lcStepId)
  {
    string str1 = "1";
    List<PendingLink> newLinks = (List<PendingLink>) null;
    IDBObject objectActualCopy = ius.GetObjectActualCopy(objID, false);
    if (objectActualCopy != null)
    {
      bool flag = objectActualCopy.ModificationID == this.eco.linkedContextNo && objectActualCopy.VersionID > 0;
      if (!flag)
      {
        EditingContextMode editingContextMode = EditingContextMode.Default;
        if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
          editingContextMode = service.EditingContextMode;
        try
        {
          if (service != null)
            service.EditingContextMode = EditingContextMode.Default;
          flag = this.PerformCreateVersion(ius, objID, objectActualCopy, goal, lcStepId, out newLinks);
        }
        finally
        {
          if (service != null)
            service.EditingContextMode = editingContextMode;
        }
      }
      if (newLinks == null)
        newLinks = new List<PendingLink>();
      if (newLinks.Count == 0)
        newLinks.Add(new PendingLink(goal, lcStepId)
        {
          verID = objectActualCopy.ObjectID
        });
      for (int index = newLinks.Count - 1; index >= 0; --index)
      {
        if (this.objLinks.ContainsKey(Math.Abs(newLinks[index].verID)))
          newLinks.RemoveAt(index);
      }
      for (int index = 0; index < newLinks.Count; ++index)
      {
        PendingLink pendingLink = newLinks[index];
        IDBObject idbO = ius.GetObject(pendingLink.verID, false);
        if (idbO != null)
        {
          IDBAttribute attributeById = idbO.GetAttributeByID(RevHelper.idAttrChangeNo);
          if (attributeById == null || attributeById.Value == DBNull.Value)
          {
            idbO = this.AssignChangeNo(ius, idbO, goal);
            if (attributeById == null)
              attributeById = idbO.GetAttributeByID(RevHelper.idAttrChangeNo);
          }
          if (attributeById != null)
          {
            string str2 = Convert.ToString(attributeById.Value);
            try
            {
              string str3 = LocalizationHolder.rm.GetString("ECO.Client_316");
              if (str2.EndsWith(str3))
                str2 = str2.Substring(0, str2.Length - str3.Length);
              str1 = Convert.ToString(Convert.ToInt32(str2));
            }
            catch
            {
            }
          }
          else
            str1 = idbO.VersionID <= 0 ? "1" : Convert.ToString(idbO.VersionID);
          pendingLink.InitVars(objectActualCopy);
          pendingLink.verStr = str1;
          pendingLink.needDelete = flag;
          this.objLinks.Add(Math.Abs(idbO.ObjectID), pendingLink);
        }
      }
    }
    else
    {
      newLinks = new List<PendingLink>();
      IDBObject dbObject = ius.GetObject(-objID, false);
      if (dbObject != null)
      {
        string des = "";
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
        if (attributeById != null)
          des = attributeById.AsString;
        string nchangeNo = this.GetNChangeNo(ius, dbObject.ID, -objID, goal);
        PendingLink pendingLink = new PendingLink(-objID, nchangeNo, goal, des);
        pendingLink.needDelete = true;
        pendingLink.stepID = lcStepId;
        newLinks.Add(pendingLink);
        this.objLinks.Add(objID, pendingLink);
      }
    }
    return newLinks;
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
    switch (goal)
    {
      case ECOGoal.Annul:
      case ECOGoal.Litera:
        return false;
      default:
        if (idbO == null)
          idbO = ius.GetObjectActualCopy(objID, false);
        if (idbO == null)
          return false;
        if (idbO.CheckoutBy != 0L)
        {
          if (!this.objLinks.ContainsKey(Math.Abs(idbO.ObjectID)))
            newLinks.Add(new PendingLink(goal, lcStepId)
            {
              verID = idbO.ObjectID,
              needDelete = false,
              stepID = lcStepId
            });
          return false;
        }
        switch (idbO.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (this.objLinks.ContainsKey(Math.Abs(idbO.ObjectID)))
              throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_333"));
            newLinks.Add(new PendingLink(goal, lcStepId)
            {
              verID = idbO.ObjectID,
              needDelete = false,
              stepID = lcStepId
            });
            return false;
          case ObjectModifyModes.CreateVersion:
            return this._DoCreateVersion(ius, idbO, newLinks, goal, lcStepId);
          case ObjectModifyModes.CantModify:
            IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep(idbO.LCStep);
            if (lifecycleStep.ObjectModifyMode == ObjectModifyModes.CantModify)
              throw new ERevision(objID, string.Format(LocalizationHolder.rm.GetString("ECO.Client_251"), (object) objID, (object) lifecycleStep.LCName));
            throw new ERevision(objID, LocalizationHolder.rm.GetString("ECO.Client_188") + Convert.ToString(objID) + LocalizationHolder.rm.GetString("ECO.Client_189"));
          default:
            return false;
        }
    }
  }

  private bool _DoCreateVersion(
    IUserSession session,
    IDBObject dbObject,
    List<PendingLink> plList,
    ECOGoal goal,
    int lcStepId)
  {
    bool version = false;
    IDBObjectCollection objectCollection = session.GetObjectCollection(dbObject.ObjectType);
    if (!(objectCollection is IClientDBObjectCollection))
      throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_381"));
    List<long> longList = new List<long>();
    long num1 = 0;
    CreateVersionResult versionInternal = (objectCollection as IClientDBObjectCollection).CreateVersionInternal(dbObject.ObjectID);
    try
    {
      List<ObjInfo> objInfoList = new List<ObjInfo>();
      for (int index = 0; index < versionInternal.SourceVersions.Count; ++index)
      {
        if (dbObject.ObjectID == versionInternal.SourceVersions[index].F_OBJECT_ID)
        {
          num1 = versionInternal.TargetVersions[index].F_OBJECT_ID;
        }
        else
        {
          objInfoList.Add(new ObjInfo(versionInternal.TargetVersions[index].F_OBJECT_ID, session));
          ObjectCheckOutVersionDescription targetVersion = versionInternal.TargetVersions[index];
          if (targetVersion.Mode == ObjectCheckedOutVersionMode.NewVersion)
          {
            ObjectCheckOutVersionDescription sourceVersion = versionInternal.SourceVersions[index];
            if (RevReqHelper.GetRevReq(sourceVersion.F_LCSTEP_ID, sourceVersion.F_OBJECT_TYPE) != ReqRevision.NoRevision)
              longList.Add(targetVersion.F_OBJECT_ID);
          }
        }
      }
      INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
      if (num1 != 0L)
      {
        IDBObject idbO1 = session.GetObject(num1, false);
        if (idbO1 == null)
          return false;
        string nchangeNo1 = this.GetNChangeNo(session, idbO1.ID, num1, goal);
        IDBAttribute dbAttribute1 = idbO1.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
        if (dbAttribute1 != null)
          dbAttribute1.Value = (object) nchangeNo1;
        PendingLink pendingLink1 = new PendingLink(Math.Abs(num1), nchangeNo1, goal, lcStepId);
        pendingLink1.InitVars(idbO1);
        pendingLink1.needDelete = true;
        if (objInfoList.Count >= 1)
          pendingLink1.auxObjects = objInfoList;
        plList.Add(pendingLink1);
        idbO1.ObjectGUID.ToString();
        version = true;
        service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", num1));
        for (int index = 0; index < longList.Count; ++index)
        {
          long objectId = longList[index];
          IDBObject idbO2 = session.GetObject(objectId, false);
          if (idbO2.ObjectModifyMode == ObjectModifyModes.Checkout && idbO2.CheckoutBy == 0L)
          {
            idbO2 = idbO2.CheckOut();
            objectId = idbO2.ObjectID;
          }
          if (idbO2 != null)
          {
            long num2 = objectId;
            this.eco.newVers.Add(Math.Abs(num2));
            string nchangeNo2 = this.GetNChangeNo(session, idbO2.ID, num2, goal);
            IDBAttribute dbAttribute2 = idbO2.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
            if (dbAttribute2 != null)
              dbAttribute2.Value = (object) nchangeNo2;
            PendingLink pendingLink2 = new PendingLink(num2, nchangeNo2, goal, lcStepId);
            pendingLink2.InitVars(idbO2);
            pendingLink2.needDelete = true;
            plList.Add(pendingLink2);
            service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", num2));
          }
        }
      }
      versionInternal.NewObjectVersion.CommitCreation(true);
      versionInternal.Commit(session);
    }
    catch (Exception ex)
    {
      versionInternal.Rollback(session);
      throw ex;
    }
    return version;
  }

  public TableElement AddNewChange(
    List<PendingLink> pList,
    Guid cjGuid,
    long cjId,
    DateTime recDate,
    List<SignParams> signList = null,
    bool readOnly = false)
  {
    TableElement te = this.eco.AddNewEcoRow(CJEditorForm.idChangeRecord, false);
    if (te != null)
    {
      List<long> longList = new List<long>();
      List<Guid> guidList = new List<Guid>();
      foreach (PendingLink p in pList)
      {
        longList.Add(Math.Abs(p.verID));
        guidList.Add(p.verGuid);
      }
      string attributeValue = this.eco.IdLists() ? Intermech.ECO.Client.ECO.IdListToStr(longList) : Intermech.ECO.Client.ECO.GuidListToStr(guidList);
      te.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
      te.SetAttributeValue(CJEditorForm.cjRecordAttr, cjGuid.ToString());
      this.cjRecList.Add(new CJEditorForm.IdGuid(cjId, cjGuid));
      (te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) as TextData).AssignText(Convert.ToString(this.cjRecList.Count), false, false, false);
      if (!this.recTable.ContainsKey(cjId))
        this.recTable.Add(cjId, new CJEditorForm.CJRecordInfo(longList)
        {
          ReadOnly = readOnly,
          SignList = signList
        });
      foreach (PendingLink p in pList)
        p.UpdateDesign();
      string str = this.ECO.MakeDesignString(pList);
      (te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeDesign) as TextData).Text = str;
      (te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeDate) as TextData).Text = recDate.ToShortDateString();
      this.UpdateReadOnlyFields((TableData) te, readOnly);
    }
    return te;
  }

  public void UpdateReadOnlyFields(TableData te, bool readOnly)
  {
    TextData templateRecursive1 = (TextData) te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeOriginals);
    if (templateRecursive1 != null && templateRecursive1.ReadOnly != readOnly)
      templateRecursive1.ReadOnly = readOnly;
    TextData templateRecursive2 = (TextData) te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeCopies);
    if (templateRecursive2 != null && templateRecursive2.ReadOnly != readOnly)
      templateRecursive2.ReadOnly = readOnly;
    TextData templateRecursive3 = (TextData) te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeContents);
    if (templateRecursive3 == null || templateRecursive3.ReadOnly == readOnly)
      return;
    templateRecursive3.ReadOnly = readOnly;
  }

  private void CJEditorForm_InplaceEditorActivating(object sender, CancelEventArgs e)
  {
    e.Cancel = true;
  }

  private bool FillExtraFields(TableData td, CJEditorForm.CJRecordInfo cjri)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long replacingEcoId = cjri.ReplacingECO_Id;
      TextData templateRecursive = (TextData) td.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeComments);
      if (templateRecursive != null)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(replacingEcoId, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
          if (attributeById != null && attributeById.Value != DBNull.Value)
          {
            string str = Convert.ToString(attributeById.Value);
            if (!templateRecursive.Text.Contains(str))
            {
              templateRecursive.AssignText($"{templateRecursive.Text} {str}", false, false, false);
              flag = true;
            }
          }
        }
      }
      if (this.UpdateSigns(td, cjri))
        flag = true;
    }
    return flag;
  }

  private bool UpdateSigns(TableData td, CJEditorForm.CJRecordInfo cjri)
  {
    this.textFld = (TextData) td.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeSigns);
    if (this.textFld != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < cjri.SignList.Count; ++index)
      {
        SignParams sign = cjri.SignList[index];
        stringBuilder.AppendLine(sign.Rank);
        stringBuilder.AppendLine(sign.Surname);
        stringBuilder.Append($"{sign.SignValue} {sign.SignDate.ToShortDateString()}");
        if (index < cjri.SignList.Count - 1)
          stringBuilder.AppendLine();
      }
      string str = stringBuilder.ToString();
      if (!this.textFld.Text.Equals(str))
      {
        this.textFld.AssignText(str, false, false, false);
        return true;
      }
    }
    return false;
  }

  private bool NeedsChangeNo(int objTypeId)
  {
    return MetaDataHelper.GetAttribute4ObjectType(objTypeId, RevHelper.idAttrChangeNo) != null;
  }

  private IDBObject AssignChangeNo(IUserSession ius, IDBObject idbO, ECOGoal goal)
  {
    if (!this.NeedsChangeNo(idbO.ObjectType))
      return idbO;
    long objectId = idbO.ObjectID;
    string nchangeNo = this.GetNChangeNo(ius, idbO.ID, objectId, goal);
    return this.SetNewChangeNo(ius, objectId, nchangeNo);
  }

  private List<long> GetAllVersions(IUserSession ius, long objId) => ius.GetObjectIDVersions(objId);

  public string GetNChangeNo(IUserSession ius, long ID, long objId, ECOGoal goal)
  {
    return goal == ECOGoal.Annul ? Intermech.ECO.Client.ECO.noChangeNumber : (ius.GetCustomService(typeof (IECOServer)) as IECOServer).GetNewChangeNo(ID, objId).ToString() + LocalizationHolder.rm.GetString("ECO.Client_316");
  }

  private bool IsChangeNumUnique(IUserSession ius, long objId, string changeNo)
  {
    return (ius.GetCustomService(typeof (IECOServer)) as IECOServer).IsChangeNumUnique(objId, changeNo);
  }

  private IDBObject SetNewChangeNo(IUserSession ius, long objId, string newChangeNo)
  {
    IDBObject objectActualCopy = ius.GetObjectActualCopy(objId, false);
    if (objectActualCopy == null)
      return (IDBObject) null;
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objectActualCopy.ObjectType);
    if (objectTypeParentsId.IndexOf(objectActualCopy.ObjectType) < 0)
      objectTypeParentsId.Add(objectActualCopy.ObjectType);
    List<long> longList = new List<long>();
    longList.Add(objId);
    IDBObject dbObject1 = objectActualCopy;
    bool flag = false;
    foreach (long objectID in longList)
    {
      IDBObject dbObject2 = ius.GetObjectActualCopy(objectID, true);
      switch (dbObject2.ObjectModifyMode)
      {
        case ObjectModifyModes.Checkout:
          if (objectID > 0L)
          {
            dbObject2 = dbObject2.CheckOut(false);
            flag = true;
          }
          if (dbObject2 == null)
            continue;
          break;
        case ObjectModifyModes.CreateVersion:
        case ObjectModifyModes.CantModify:
          continue;
      }
      if (dbObject2 != null)
      {
        IDBAttribute dbAttribute = dbObject2.Attributes.AddAttribute(RevHelper.idAttrChangeNo, false);
        if (dbAttribute != null)
          dbAttribute.AsString = newChangeNo;
        if (flag)
          dbObject2.CheckIn();
      }
      if (objectID == objId)
        dbObject1 = dbObject2;
    }
    return dbObject1;
  }

  private List<int> GetRecNumList(List<TableElement> selList)
  {
    List<int> recNumList = new List<int>();
    foreach (TableElement sel in selList)
    {
      int recNum = this.GetRecNum(sel);
      if (recNum != -1)
        recNumList.Add(recNum);
    }
    recNumList.Sort();
    return recNumList;
  }

  private int GetRecNum(TableElement te)
  {
    return te.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) is TextData templateRecursive && templateRecursive.Text != "" ? Convert.ToInt32(templateRecursive.Text) - 1 : -1;
  }

  private void DeleteRecord()
  {
    if (this.selList.Count > 1)
    {
      this.DeleteRecords(this.GetRecNumList(this.selList), this.selList);
    }
    else
    {
      int curRecNum = this.GetCurRecNum();
      if (curRecNum < 0)
        return;
      this.DeleteRecord(curRecNum);
    }
  }

  private void DeleteRecord(int recNum, bool confirmDeleting = true)
  {
    long id = this.cjRecList[recNum].id;
    if (confirmDeleting && (!(this.elCurChange.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeDesign) is TextData templateRecursive1) || MessageBox.Show($"{string.Format(LocalizationHolder.rm.GetString("ECO.Client_307"), (object) templateRecursive1.Text)}\n\r\n\r{LocalizationHolder.rm.GetString("ECO.Client_308")}", LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel) != DialogResult.OK))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions dbTransactions)
        dbTransactions.StartTransaction();
      try
      {
        this.DelRels(sessionKeeper.Session, id);
        sessionKeeper.Session.GetObject(id).Delete(0L);
      }
      catch
      {
        if (dbTransactions != null)
        {
          dbTransactions.Rollback();
          dbTransactions = (IDBTransactions) null;
        }
        throw;
      }
      finally
      {
        dbTransactions?.Commit();
      }
    }
    this.elCurChange.Remove(false, false);
    this.eco.ecoMainTable.UniteTable();
    int num = 1;
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      if ((dataOwner.Nodes[dataPositionInFlow] as TableData).FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) is TextData templateRecursive2)
        templateRecursive2.AssignText(Convert.ToString(num), false, false, false);
      ++num;
    }
    this.Document.UpdateLayout(true, true);
    this.cjRecList.RemoveAt(recNum);
    CJEditorForm.CJRecordInfo cjRecordInfo = this.recTable[id];
    this.recTable.Remove(id);
    foreach (long key in cjRecordInfo.ObjList)
    {
      if (this.objLinks.ContainsKey(key))
        this.objLinks.Remove(key);
      if (this.objRecIndex.ContainsKey(key))
        this.objRecIndex.Remove(key);
    }
  }

  private void DelRels(IUserSession ius, long cjRecId)
  {
    IDBRelationCollection relationCollection = ius.GetRelationCollection(RevHelper.idLinkRevision);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID)
    });
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, cjRecId);
    List<long> longList = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      longList.Add(Convert.ToInt64(row[0]));
    if (longList.Count <= 0)
      return;
    IECOServer customService = ius.GetCustomService(typeof (IECOServer)) as IECOServer;
    foreach (long relId in longList)
      customService.StartLinkDeletion(relId);
    try
    {
      relationCollection.Delete(longList.ToArray(), false, 0L);
    }
    finally
    {
      foreach (long relId in longList)
        customService.EndLinkDeletion(relId);
    }
  }

  public void DeleteLinksToCanceledVersions(List<long> canceledVerList)
  {
    List<int> intList = new List<int>();
    foreach (long canceledVer in canceledVerList)
    {
      if (!this.objRecIndex.ContainsKey(Math.Abs(canceledVer)))
      {
        CJEditorForm.RecInfo recInfo = this.objRecIndex[Math.Abs(canceledVer)];
        if (!intList.Contains(recInfo.RecIndex))
          intList.Add(recInfo.RecIndex);
      }
    }
    foreach (int recNum in intList)
      this.DeleteRecord(recNum, false);
  }

  private void DeleteRecords(List<int> recNumList, List<TableElement> tdList, bool confirmDeleting = true)
  {
    if (confirmDeleting)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (DocumentTreeNode td in tdList)
      {
        if (td.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeDesign) is TextData templateRecursive)
        {
          if (stringBuilder.Length != 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(templateRecursive.Text);
        }
      }
      if (MessageBox.Show($"{string.Format(LocalizationHolder.rm.GetString("ECO.Client_307"), (object) stringBuilder.ToString())}\n\r\n\r{LocalizationHolder.rm.GetString("ECO.Client_308")}", LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions dbTransactions)
        dbTransactions.StartTransaction();
      try
      {
        foreach (int recNum in recNumList)
        {
          long id = this.cjRecList[recNum].id;
          this.DelRels(sessionKeeper.Session, id);
          sessionKeeper.Session.GetObject(id, false)?.Delete(0L);
        }
      }
      catch
      {
        if (dbTransactions != null)
        {
          dbTransactions.Rollback();
          dbTransactions = (IDBTransactions) null;
        }
        throw;
      }
      finally
      {
        dbTransactions?.Commit();
      }
    }
    this.eco.ecoMainTable.UniteTable();
    foreach (DocumentTreeNode td in tdList)
      td.Remove(false, false);
    int num = 1;
    TableData dataOwner;
    for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
    {
      if ((dataOwner.Nodes[dataPositionInFlow] as TableData).FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) is TextData templateRecursive)
        templateRecursive.AssignText(Convert.ToString(num), false, false, false);
      ++num;
    }
    this.Document.UpdateLayout(true, true);
    for (int index = recNumList.Count - 1; index >= 0; --index)
    {
      int recNum = recNumList[index];
      long id = this.cjRecList[recNum].id;
      this.cjRecList.RemoveAt(recNum);
      CJEditorForm.CJRecordInfo cjRecordInfo = this.recTable[id];
      this.recTable.Remove(id);
      foreach (long key in cjRecordInfo.ObjList)
      {
        if (this.objLinks.ContainsKey(key))
          this.objLinks.Remove(key);
        if (this.objRecIndex.ContainsKey(key))
          this.objRecIndex.Remove(key);
      }
    }
  }

  public bool SelectRecord(TableData td)
  {
    this.DocumentControl.SetSelection((DocumentTreeNode) td, true, true);
    return false;
  }

  public bool SelectRecById(long recId)
  {
    if (this.recTable.ContainsKey(recId))
    {
      CJEditorForm.CJRecordInfo cjRecordInfo = this.recTable[recId];
      if (cjRecordInfo != null && cjRecordInfo.ObjList != null && cjRecordInfo.ObjList.Count > 0)
      {
        long key = cjRecordInfo.ObjList[0];
        if (this.objRecIndex.ContainsKey(key))
          return this.SelectRecord(this.objRecIndex[key].RecTable);
      }
    }
    return false;
  }

  private bool SaveChangeJournal()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute dbAttribute = sessionKeeper.Session.GetObject(this.eco.EcoObjectID).Attributes.AddAttribute(RevHelper.idAttrVersion, false);
      if (dbAttribute != null)
        dbAttribute.AsInteger = Intermech.ECO.Client.ECO.curVersion;
    }
    if (!this.eco.IdLists())
    {
      TableData dataOwner;
      for (int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
        string str = Intermech.ECO.Client.ECO.GuidListToStr(this._IdListToGuidList(this._GetIdList(node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true))));
        node.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, str);
      }
    }
    this.QuickSaveDoc();
    return true;
  }

  private void QuickSaveDoc()
  {
    DocumentEditorPlugin.SaveImDocumentObjectFile(this.DocumentID, this.Document, this.DefaultFileName, 0, true);
    this.Document.Modified = false;
  }

  private long GetObjectVerForContext(IUserSession session, long contextID, IDBObject dbObject)
  {
    IDBEditingContextsObject editingContextsObject = (IDBEditingContextsObject) session.GetObject(contextID, false);
    if (editingContextsObject == null)
      return -1;
    if (!editingContextsObject.ExistsObject(dbObject.ID, true))
      return 0;
    try
    {
      return (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetEditingContextsObject((object) session.SessionGUID, editingContextsObject.LinkedContextNumber, false, true).GetObjectVersion(dbObject.ID);
    }
    catch
    {
      return 0;
    }
  }

  private long[] AddAuxLinksAttr(IDBRelation relation, PendingLink pl)
  {
    if (pl.auxObjects != null && pl.auxObjects.Count > 0)
    {
      List<long> longList = new List<long>();
      foreach (ObjInfo auxObject in pl.auxObjects)
      {
        if (relation.Session.GetObject(auxObject.verId, false) != null)
          longList.Add(auxObject.verId);
        else if (relation.Session.GetObject(-auxObject.verId, false) != null)
          longList.Add(-auxObject.verId);
      }
      if (longList.Count == 0)
        return (long[]) null;
      long[] array = longList.ToArray();
      IDBAttribute dbAttribute = relation.Attributes.AddAttribute(RevHelper.idAttrAuxLinks, false);
      if (dbAttribute != null)
      {
        object[] instance = (object[]) Array.CreateInstance(typeof (object), longList.Count);
        for (int index = 0; index < longList.Count; ++index)
          instance[index] = (object) array[index];
        dbAttribute.Values = instance;
      }
    }
    return (long[]) null;
  }

  public void CheckEcoMainTable()
  {
    if (this.eco.ecoMainTable == null)
      this.eco.ecoMainTable = this.Document.FindNode(LocalizationHolder.rm.GetString("ECO.Client_19")) as TableElement;
    if (this.eco.ecoMainTable == null)
      throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_20"));
  }

  public void SynchronizeECODocumentWithDB(bool ReadOnly)
  {
    if (this.eco.EcoObjectID == -1L)
      return;
    try
    {
      this.SuspendDocumentUpdates();
      this.CheckEcoMainTable();
      Dictionary<Guid, long> dictionary1 = new Dictionary<Guid, long>();
      Dictionary<long, CJEditorForm.CJRecordInfo> dictionary2 = new Dictionary<long, CJEditorForm.CJRecordInfo>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ISignsService customService = sessionKeeper.Session.GetCustomService(typeof (ISignsService)) as ISignsService;
        DataTable dataTable1 = sessionKeeper.Session.GetObjectCollection(RevHelper.idObjCJRecord).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(RevHelper.idAttrJournalLink, RelationalOperators.Equal, (object) this.ecoID, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
        }, new ColumnDescriptor[5]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
          new ColumnDescriptor((object) RevHelper.idAttrReplacedByECO, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.ASC, 1),
          new ColumnDescriptor((object) RevHelper.idAttrCreationDate, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.ASC, 1)
        }));
        if (dataTable1 != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            Guid key = new Guid(Convert.ToString(row[1]));
            dictionary1.Add(key, int64);
            CJEditorForm.CJRecordInfo cjRecordInfo = new CJEditorForm.CJRecordInfo(new List<long>());
            if (row[2] != null && row[2] != DBNull.Value)
              cjRecordInfo.ReplacingECO_Id = Convert.ToInt64(row[2]);
            if (row[3] != null && row[3] != DBNull.Value)
              cjRecordInfo.RecDate = Convert.ToDateTime(row[3]);
            if (row[4] != null && row[4] != DBNull.Value)
            {
              int int32 = Convert.ToInt32(row[4]);
              IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(int32);
              cjRecordInfo.ReadOnly = lifecycleStep.ObjectModifyMode == ObjectModifyModes.CantModify;
            }
            dictionary2.Add(int64, cjRecordInfo);
          }
          List<long> longList = new List<long>();
          foreach (long num1 in dictionary1.Values)
          {
            CJEditorForm.CJRecordInfo cjRecordInfo = dictionary2[num1];
            ColumnDescriptor[] columns = new ColumnDescriptor[9]
            {
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrFlags, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrVerId, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrChangeNo, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrIncludeGoal, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrFutureLC, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrDelWhenExcluded, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
              new ColumnDescriptor((object) RevHelper.idAttrMainObjectGuid, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
            };
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
            relationCollection.LocalTypesMode = true;
            DataTable dataTable2 = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, columns), num1);
            if (dataTable2 != null)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
              {
                if (row[0] != DBNull.Value)
                {
                  Convert.ToInt64(row[0]);
                  if (row[1] == DBNull.Value || (Convert.ToInt64(row[1]) & 1L) == 0L)
                  {
                    long num2 = Convert.ToInt64(row[5]);
                    long int64 = Convert.ToInt64(row[2]);
                    if (int64 != 0L)
                      num2 = int64;
                    string aver = "1";
                    string str = Convert.ToString(row[3]);
                    if (str != "")
                      aver = str;
                    ECOGoal int32 = (ECOGoal) Convert.ToInt32(row[4]);
                    string des = "";
                    IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(num2, false);
                    if (objectActualCopy != null)
                    {
                      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(RevHelper.idAttrDesign);
                      if (attributeById != null)
                        des = attributeById.AsString;
                    }
                    if (row[6] != DBNull.Value)
                      Convert.ToInt32(row[6]);
                    PendingLink pendingLink = new PendingLink(num2, aver, int32, des);
                    cjRecordInfo.ObjList.Add(num2);
                    pendingLink.needDelete = row[7] != DBNull.Value && Convert.ToBoolean(row[7]);
                    pendingLink.mainGuid = Guid.Empty;
                    if (row[8] != DBNull.Value)
                    {
                      string g = Convert.ToString(row[8]);
                      if (g != "")
                        pendingLink.mainGuid = new Guid(g);
                    }
                    this.objLinks.Add(Math.Abs(pendingLink.verID), pendingLink);
                    longList.Add(Math.Abs(pendingLink.verID));
                    if (customService != null && cjRecordInfo.SignList.Count == 0)
                      cjRecordInfo.SignList = customService.GetObjectSignsParams(num1, sessionKeeper.Session.SessionGUID);
                  }
                }
              }
            }
          }
        }
      }
      if (this.cjRecList == null)
        this.cjRecList = new List<CJEditorForm.IdGuid>();
      if (this.recTable == null)
        this.recTable = new Dictionary<long, CJEditorForm.CJRecordInfo>();
      List<CJEditorForm.ItemToDelete> itemToDeleteList = new List<CJEditorForm.ItemToDelete>();
      int num3 = this.Document.Modified ? 1 : 0;
      this.eco.ecoMainTable.UniteTable();
      if (num3 == 0)
        this.Document.Modified = false;
      bool flag1 = false;
      int recIndex = 0;
      TableData dataOwner;
      int dataPositionInFlow = this.eco.ecoMainTable.FindDataPositionInFlow(0, out dataOwner);
      while (dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count)
      {
        TableData node = dataOwner.Nodes[dataPositionInFlow] as TableData;
        string attributeValue1 = node.GetAttributeValue(CJEditorForm.cjRecordAttr, true);
        Guid empty = Guid.Empty;
        ref Guid local = ref empty;
        if (!Guid.TryParse(attributeValue1, out local) || !dictionary1.ContainsKey(empty))
        {
          itemToDeleteList.Add(new CJEditorForm.ItemToDelete(dataPositionInFlow, dataOwner));
          dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner);
        }
        else
        {
          string attributeValue2 = node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
          List<long> idList = this._GetIdList(attributeValue2);
          bool flag2 = false;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            for (int index = idList.Count - 1; index >= 0; --index)
            {
              if (sessionKeeper.Session.GetObjectInfo(idList[index]).Empty)
              {
                idList.RemoveAt(index);
                flag2 = true;
              }
            }
          }
          if (idList.Count == 0 && attributeValue2 != "")
          {
            itemToDeleteList.Add(new CJEditorForm.ItemToDelete(dataPositionInFlow, dataOwner));
          }
          else
          {
            if (flag2)
              this._SetIdList((RectangleElement) node, idList);
            long num4 = dictionary1[empty];
            this.cjRecList.Add(new CJEditorForm.IdGuid(num4, empty));
            CJEditorForm.CJRecordInfo cjri = dictionary2[num4];
            flag1 = this.FillExtraFields(node, cjri) | flag1;
            this.UpdateReadOnlyFields(node, cjri.ReadOnly);
            this.recTable.Add(num4, cjri);
          }
          dictionary1.Remove(empty);
          CJEditorForm.RecInfo recInfo = new CJEditorForm.RecInfo(recIndex, node);
          foreach (long key in idList)
            this.objRecIndex.Add(key, recInfo);
          dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner);
        }
      }
      bool flag3;
      for (int index = itemToDeleteList.Count - 1; index >= 0; --index)
      {
        CJEditorForm.ItemToDelete itemToDelete = itemToDeleteList[index];
        itemToDelete.tdata.RemoveChildNodeAt(itemToDelete.index, false, false);
        flag3 = true;
      }
      if (dictionary1.Count <= 0)
        return;
      foreach (Guid key1 in dictionary1.Keys)
      {
        long num5 = dictionary1[key1];
        if (dictionary2.ContainsKey(num5))
        {
          CJEditorForm.CJRecordInfo cjri = dictionary2[num5];
          List<PendingLink> pList = new List<PendingLink>();
          foreach (long key2 in cjri.ObjList)
          {
            if (this.objLinks.ContainsKey(key2))
              pList.Add(this.objLinks[key2]);
          }
          this.FillExtraFields((TableData) this.AddNewChange(pList, key1, num5, cjri.RecDate, cjri.SignList, cjri.ReadOnly), cjri);
          flag3 = true;
        }
      }
    }
    finally
    {
      this.ResumeDocumentUpdates();
    }
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
    return node.Name == CJEditorForm.idChangeRecord || node.Name == CJEditorForm.idChangeNum || node.Name == CJEditorForm.idChangeDate || node.Name == CJEditorForm.idChangeDesign || node.Name == CJEditorForm.idChangeContents || node.Name == CJEditorForm.idChangeSigns || node.Name == CJEditorForm.idChangeOriginals || node.Name == CJEditorForm.idChangeCopies;
  }

  private void ClearSelection()
  {
    this.elCurChange = (TableElement) null;
    this.elCurElem = (TableElement) null;
    this.elPicture = (ContainerElement) null;
    this.indexCurChange = -1;
  }

  private void GetCurrents()
  {
    this.ClearSelection();
    if (this.elWorkspace == null)
      return;
    int num1 = -1;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null)
      {
        if (pageElementUi.Element.Template.Id == CJEditorForm.idChangeRecord)
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
        else if (pageElementUi.Element is ContainerData)
          this.elPicture = pageElementUi.Element as ContainerElement;
      }
    }
    if (this.elCurChange == null)
      return;
    int num2 = -1;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null && this.IsElement((DocumentTreeNode) pageElementUi.Element))
      {
        this.elCurElem = pageElementUi.Element as TableElement;
        IEnumerator enumerator = this.elCurChange.Nodes.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            DocumentTreeNode current = (DocumentTreeNode) enumerator.Current;
            ++num2;
            TableElement elCurElem = this.elCurElem;
            if (current == elCurElem)
              break;
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

  private void LoadNavigatorMenuItemImages(MenuBarItem contextMenu)
  {
    INamedImageList service = (INamedImageList) ECOPlugin.serviceProvider.GetService(typeof (INamedImageList));
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) contextMenu.Items)
      this.LoadNavigatorMenuItemImages(menuButtonItem, service);
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
    if (this.elCurChange != null)
    {
      int num1 = 0;
      List<long> idList = this._GetIdList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      for (int index = 0; index < idList.Count; ++index)
      {
        long objectID = idList[index];
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetObjectActualCopy(objectID, false) != null)
            ++num1;
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
      for (int index = 0; index < idList.Count; ++index)
      {
        long objectID = idList[index];
        if (this.objLinks.ContainsKey(Math.Abs(objectID)))
        {
          PendingLink objLink = this.objLinks[Math.Abs(objectID)];
          if (objLink.design == null)
            objLink.UpdateDesign();
          MenuButtonItem menuButtonItem1 = new MenuButtonItem(objLink.design);
          long num4 = objectID;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(objectID, false) ?? sessionKeeper.Session.GetObjectActualCopy(-objectID, false);
            if (dbObject != null)
              num4 = dbObject.ObjectID;
            else
              continue;
          }
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
              menuButtonItem2.Shortcut = Shortcut.None;
              menuButtonItem1.Items.Add((ToolbarItemBase) menuButtonItem2);
            }
          }
          if (num2 > 0 && index > 0 && index % 20 == 0)
            list = (IList) al[index / 20].Items;
          list.Add((object) menuButtonItem1);
        }
      }
    }
    if (this.ReadOnly)
      return;
    int num = this.UpdateRecId();
    bool flag1 = true;
    if (this.recId != -1L)
      flag1 = this.recTable[this.recId].ReadOnly;
    this.selList = this.GetSelectionList();
    if (this.includeElem == null)
    {
      this.includeElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_304"), new EventHandler(this.cmdNewRecord));
      if (this.iNIL != null)
        this.includeElem.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgApplyBall")];
    }
    this.includeElem.BeginGroup = this.elPicture != null || this.elCurChange != null;
    al.Add(this.includeElem);
    if (this.removeElem == null)
    {
      this.removeElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_305"), new EventHandler(this.cmdDelRecord));
      if (this.iNIL != null)
        this.removeElem.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgDelete")];
    }
    this.removeElem.Enabled = this.elCurChange != null;
    al.Add(this.removeElem);
    if (this.selPictFromBase == null)
    {
      this.selPictFromBase = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_309"), new EventHandler(this.cmdAddPicture));
      this.selPictFromBase.BeginGroup = true;
    }
    this.selPictFromBase.Enabled = this.elCurChange != null && !flag1;
    al.Add(this.selPictFromBase);
    if (this.replaceWithECO == null)
    {
      this.replaceWithECO = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_318"), new EventHandler(this.cmdReplaceWithECO));
      this.replaceWithECO.BeginGroup = true;
    }
    bool flag2 = false;
    if (num >= 0)
    {
      long replacingEcoId = this.recTable[this.recId].ReplacingECO_Id;
      flag2 = replacingEcoId == 0L || replacingEcoId == -1L;
    }
    this.replaceWithECO.Enabled = flag2 && !flag1;
    al.Add(this.replaceWithECO);
    if (this.signRecord == null)
    {
      this.signRecord = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_343"), new EventHandler(this.cmdSign));
      this.signRecord.BeginGroup = true;
    }
    this.signRecord.Enabled = flag2 && !flag1;
    al.Add(this.signRecord);
    if (this.signRecordAs == null)
      this.signRecordAs = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_344"), new EventHandler(this.cmdSignAs));
    this.signRecordAs.Enabled = flag2 && !flag1;
    al.Add(this.signRecordAs);
    if (this.sendByProcess == null)
    {
      this.sendByProcess = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_355"), new EventHandler(this.cmdStartProcess));
      this.sendByProcess.BeginGroup = true;
    }
    al.Add(this.sendByProcess);
  }

  private int UpdateRecId()
  {
    int index = this.elCurChange != null ? this.GetCurRecNum() : -1;
    this.recId = index >= 0 ? this.cjRecList[index].id : -1L;
    return index;
  }

  protected override void MyGetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    e.ContextMenuItems.Clear();
    PageControl pageControl = (sender as DocumentControl).ActivePage.PageControl;
    this.items.Clear();
    pageControl.GetPageElementUIAtPoint(pageControl.PointToClient(Control.MousePosition), this.items);
    this.textFld = (TextData) null;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null && pageElementUi.Element is TextData)
      {
        this.textFld = pageElementUi.Element as TextData;
        break;
      }
    }
    if (this.IsInWorkspace())
    {
      this.GetCurrents();
      this.MakeContMenu(e.ContextMenuItems);
    }
    this.EnableContextMenu();
  }

  private List<TableElement> GetSelectionList()
  {
    List<TableElement> selectionList = new List<TableElement>();
    List<DocumentTreeNode> documentTreeNodeList = this.DocumentControl.SelectedNodes;
    if (documentTreeNodeList.Count == 1 && documentTreeNodeList[0].IsVirtualNode)
      documentTreeNodeList = documentTreeNodeList[0].GetNodesFromVirtualNode();
    foreach (DocumentTreeNode documentTreeNode in documentTreeNodeList)
    {
      if (documentTreeNode is TableElement && documentTreeNode.TemplateId == CJEditorForm.idChangeRecord)
        selectionList.Add((TableElement) documentTreeNode);
    }
    return selectionList;
  }

  private void EnableContextMenu()
  {
    if (this.removeElem == null)
      return;
    this.removeElem.Enabled = this.elCurChange != null;
  }

  private void cmdNewRecord(object sender, EventArgs e) => this.CreateNewRecord();

  private void cmdDelRecord(object sender, EventArgs e) => this.DeleteRecord();

  private void cmdAddPicture(object sender, EventArgs e)
  {
    if (this.elCurChange == null || this.openPictDlg.ShowDialog() != DialogResult.OK)
      return;
    string lower = Path.GetFileName(this.openPictDlg.FileName).ToLower();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject idbObject = sessionKeeper.Session.GetObject(this.recId, false);
      if (idbObject == null)
        return;
      if (this.FileExists(idbObject, lower))
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_322"), (object) lower), LocalizationHolder.rm.GetString("ECO.Client_66"));
        return;
      }
      if (!this.AddFile(idbObject, this.openPictDlg.FileName))
        return;
    }
    TextBoxElement templateRecursive = (TextBoxElement) this.elCurChange.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeContents);
    if (templateRecursive == null)
      return;
    if (!templateRecursive.InPlaceEditorActive)
    {
      this.DocumentControl.SetActiveElement((DocumentTreeNode) templateRecursive, true, Point.Empty);
      templateRecursive.TextBox.SetTextPosition(int.MaxValue, false);
    }
    templateRecursive.TextBox.InsertHyperLink(LocalizationHolder.rm.GetString("ECO.Client_321") + lower, lower, false);
    this.Document.UpdateLayout(true);
    this.QuickSaveDoc();
  }

  private bool FileExists(IDBObject idbObject, string shortFileName)
  {
    IDBAttribute attributeById = idbObject.GetAttributeByID(RevHelper.idAttrFile);
    if (attributeById == null)
      return false;
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      if (attributeById is IBlobReader blobReader)
      {
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        try
        {
          if (Path.GetFileName(blobInformation.FileName).ToLower().Equals(shortFileName.ToLower()))
            return true;
        }
        finally
        {
          blobReader.CloseBlob();
        }
      }
    }
    return false;
  }

  private bool AddFile(IDBObject idbObject, string fullFileName)
  {
    int num = 0;
    IDBAttribute aIDBAttribute = idbObject.GetAttributeByID(RevHelper.idAttrFile);
    if (aIDBAttribute == null)
      aIDBAttribute = idbObject.Attributes.AddAttribute(RevHelper.idAttrFile, false);
    else
      num = aIDBAttribute.AddValue((object) null);
    aIDBAttribute.Index = num;
    if (aIDBAttribute is IBlobReader blobReader)
    {
      try
      {
        BlobInformation aBlobInformation = blobReader.OpenBlob(-1) with
        {
          FileType = FileTypes.ftNormal,
          FileName = Path.GetFileName(fullFileName).ToLower(),
          ArcMethod = ArcMethods.ZLibPacked
        };
        using (FileStream aSourceStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read))
          new BlobProcWriter(aIDBAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        return true;
      }
      catch (Exception ex)
      {
        if (num != 0)
          aIDBAttribute.DeleteValue();
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
      finally
      {
        blobReader.CloseBlob();
      }
    }
    return false;
  }

  private void DocumentControl_HyperLinkActivated(object sender, HyperLinkActivated_EventArgs e)
  {
    this.recId = -1L;
    PageControl pageControl = this.DocumentControl.ActivePage.PageControl;
    this.items.Clear();
    pageControl.GetPageElementUIAtPoint(pageControl.PointToClient(Control.MousePosition), this.items);
    if (this.IsInWorkspace())
    {
      this.GetCurrents();
      this.UpdateRecId();
    }
    if (this.recId == -1L)
      return;
    CJEditorForm.CJRecordInfo cjRecordInfo = this.recTable[this.recId];
    if (!e.RightClick)
    {
      VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
      ClientContext.LaunchActions.LaunchByShell(new LaunchParams(cjRecordInfo.ReadOnly ? LaunchType.View : LaunchType.Edit, this.recId, DBHelper.GetObjectType(this.recId), editorRule)
      {
        ObjectFileName = e.LinkId
      });
    }
    else
    {
      if (cjRecordInfo.ReadOnly || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_323"), (object) e.LinkId), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      TextBoxElement templateRecursive = (TextBoxElement) this.elCurChange.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeContents);
      if (templateRecursive == null)
        return;
      templateRecursive.TextBox.DeleteHyperLink(false);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject idbObject = sessionKeeper.Session.GetObject(this.recId, false);
        if (idbObject == null)
          return;
        this.DeleteFile(idbObject, e.LinkId);
      }
      this.Document.UpdateLayout(true);
      this.QuickSaveDoc();
    }
  }

  private void DeleteFile(IDBObject idbObject, string fileName)
  {
    int num = -1;
    IDBAttribute attributeById = idbObject.GetAttributeByID(RevHelper.idAttrFile);
    if (attributeById == null)
      return;
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      if (attributeById is IBlobReader blobReader)
      {
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        try
        {
          if (blobInformation.FileName == fileName)
          {
            num = index;
            break;
          }
        }
        finally
        {
          blobReader.CloseBlob();
        }
      }
    }
    if (num < 0)
      return;
    if (attributeById.ValuesCount == 1)
    {
      attributeById.Delete(0L);
    }
    else
    {
      attributeById.Index = num;
      attributeById.DeleteValue();
    }
  }

  private void cmdReplaceWithECO(object sender, EventArgs e)
  {
    List<long> cjRecList = new List<long>();
    if (this.selList.Count > 1)
    {
      foreach (int recNum in this.GetRecNumList(this.selList))
      {
        long id = this.cjRecList[recNum].id;
        cjRecList.Add(id);
      }
    }
    else
      cjRecList.Add(this.recId);
    string revDesign = "";
    long num = ECOPlugin.ReplaceCJRecord(cjRecList, out revDesign);
    if (num == 0L)
      return;
    foreach (long key in cjRecList)
    {
      CJEditorForm.CJRecordInfo cjRecordInfo = this.recTable[key];
      cjRecordInfo.ReplacingECO_Id = num;
      cjRecordInfo.ReadOnly = true;
    }
    if (this.selList.Count > 1)
    {
      foreach (TableElement sel in this.selList)
        this._MarkReplacedChange(sel, revDesign);
    }
    else
      this._MarkReplacedChange(this.elCurChange, revDesign);
    this.QuickSaveDoc();
  }

  private void _MarkReplacedChange(TableElement change, string revDesign)
  {
    TextData templateRecursive = (TextData) change.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeComments);
    if (templateRecursive == null)
      return;
    templateRecursive.AssignText(revDesign, false, true, true);
    this.UpdateReadOnlyFields((TableData) change, true);
  }

  private void cmdEdit(object sender, EventArgs e)
  {
    if (this.textFld.Id == Intermech.ECO.Client.ECO.idReason || this.textFld.Id == Intermech.ECO.Client.ECO.idShifr)
      this.SelReason((DocumentTreeNode) this.textFld);
    else if (this.textFld.Id == Intermech.ECO.Client.ECO.idZadel1 || this.textFld.Id == Intermech.ECO.Client.ECO.idZadel2)
    {
      this.SelZadel((DocumentTreeNode) this.textFld);
    }
    else
    {
      if (!(this.textFld.Id == Intermech.ECO.Client.ECO.idCreationDate) && !(this.textFld.Id == Intermech.ECO.Client.ECO.idStartChangeTerm) && !(this.textFld.Id == Intermech.ECO.Client.ECO.idEndChangeTerm) && !(this.textFld.Id == Intermech.ECO.Client.ECO.idPITerm))
        return;
      this.SelDate((DocumentTreeNode) this.textFld);
    }
  }

  private void cmdClear(object sender, EventArgs e)
  {
    this.SetFieldText(this.textFld, (string) null);
  }

  private void cmdCopy(object sender, EventArgs e)
  {
    if (this.textFld == null || !(this.textFld is TextBoxElement))
      return;
    TextBoxElement textFld = (TextBoxElement) this.textFld;
    string text = textFld.InPlaceEditorControl == null ? this.textFld.Text : ((ImRtfEditor) textFld.InPlaceEditorControl).TerGetTextSel();
    if (!(text != ""))
      return;
    try
    {
      Clipboard.SetText(text, TextDataFormat.UnicodeText);
    }
    catch (ExternalException ex)
    {
    }
  }

  private void cmdCut(object sender, EventArgs e)
  {
    if (this.textFld == null || !(this.textFld is TextBoxElement))
      return;
    TextBoxElement textFld = (TextBoxElement) this.textFld;
    if (textFld.InPlaceEditorControl != null)
    {
      ImRtfEditor placeEditorControl = (ImRtfEditor) textFld.InPlaceEditorControl;
      string textSel = placeEditorControl.TerGetTextSel();
      if (!(textSel != ""))
        return;
      Clipboard.SetText(textSel);
      placeEditorControl.TerDeleteBlock(true);
    }
    else
    {
      string text = this.textFld.Text;
      if (!(text != ""))
        return;
      Clipboard.SetText(text);
      this.textFld.Text = "";
    }
  }

  private void cmdPaste(object sender, EventArgs e)
  {
    try
    {
      if (!Clipboard.ContainsText())
        return;
    }
    catch
    {
      return;
    }
    if (this.textFld == null || !(this.textFld is TextBoxElement))
      return;
    TextBoxElement textFld = (TextBoxElement) this.textFld;
    if (textFld.InPlaceEditorControl == null)
      return;
    ImRtfEditor placeEditorControl = (ImRtfEditor) textFld.InPlaceEditorControl;
    placeEditorControl.TerDeleteBlock(false);
    placeEditorControl.TerInsertText(Clipboard.GetText(), -1, -1, true);
  }

  private void cmdTestSpec(object sender, EventArgs e)
  {
  }

  private void cmdSortChange(object sender, EventArgs e)
  {
  }

  private void cmdInsertTemplate(object sender, EventArgs e)
  {
    if (this.textFld == null || !(this.textFld is TextBoxElement))
      return;
    this.InsertFormula((TextBoxElement) this.textFld);
  }

  private void cmdIncludeDocs(object sender, EventArgs e)
  {
  }

  private void cmdIncludeDocsGroup(object sender, EventArgs e)
  {
  }

  private void cmdIncludeExternalDoc(object sender, EventArgs e)
  {
  }

  private void cmdRemoveDocs(object sender, EventArgs e)
  {
    if (this.elCurChange == null)
      return;
    List<long> idList = this._GetIdList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
    List<long> longList = new List<long>();
    if (idList.Count > 0)
      return;
    this.elCurChange.UniteTable();
    this.elCurChange.Remove(true, true);
    this.elCurChange = (TableElement) null;
    this.Document.UpdateLayout(0, true, true);
    this.UpdateDocDesign();
    this.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
    this.CommandManager.QueryStatus();
  }

  private void GoToElem(TableElement te)
  {
    this.DocumentControl.ActivePage = (Page) te.FindFirstCell().Page;
  }

  private void cmdRemoveElem(object sender, EventArgs e)
  {
    if (this.elCurChange == null || this.elCurElem == null || this.elCurElem.Id == Intermech.ECO.Client.ECO.idSpecText)
      return;
    TableData templateRecursive = (TableData) this.elCurChange.FindFirstNodeFromTemplate_Recursive(this.elCurElem.Template.Parent);
    TableData firstCell = (TableData) templateRecursive.FindFirstCell();
    int num = templateRecursive.Id == "AR2" ? 0 : 2;
    if (firstCell.CalcDataCellCount() <= num)
      return;
    templateRecursive.RemoveChildNode((DocumentTreeNode) this.elCurElem.FindFirstCell(), true, true);
  }

  private List<long> RemoveCurElemDocs(List<long> idList)
  {
    if (idList == null || idList.Count == 0)
      return idList;
    List<long> longList = idList;
    if (idList.Count > 1)
    {
      using (ObjSelect objSelect = new ObjSelect())
      {
        objSelect.capt = LocalizationHolder.rm.GetString("ECO.Client_252");
        longList = objSelect.Execute(idList, true, false);
      }
    }
    return longList;
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

  private DBTypedObjectID GetTypedObjectID(long objID)
  {
    long id = 0;
    int objTypeID = -1;
    long owner = 0;
    long version = 0;
    long baseVersion = 0;
    string siteID = string.Empty;
    long modificationID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objID, false);
      if (dbObject != null)
      {
        id = dbObject.ID;
        objTypeID = dbObject.ObjectType;
        owner = dbObject.OwnerID;
        version = (long) dbObject.VersionID;
        baseVersion = Convert.ToInt64(dbObject.IsBaseVersion);
        siteID = dbObject.SiteID;
        modificationID = dbObject.ModificationID;
      }
    }
    return new DBTypedObjectID(objTypeID, objID, id, string.Empty, owner, version, baseVersion, siteID, modificationID);
  }

  private void cmdSign(object sender, EventArgs e) => this._Sign(sender, e, false);

  private void cmdSignAs(object sender, EventArgs e) => this._Sign(sender, e, true);

  private void _Sign(object sender, EventArgs e, bool signAs)
  {
    List<IDBTypedObjectID> typedObjectIDs = new List<IDBTypedObjectID>();
    List<long> longList = new List<long>();
    if (this.selList.Count > 1)
    {
      foreach (int recNum in this.GetRecNumList(this.selList))
      {
        long id = this.cjRecList[recNum].id;
        DBTypedObjectID typedObjectId = this.GetTypedObjectID(id);
        typedObjectIDs.Add((IDBTypedObjectID) typedObjectId);
        longList.Add(id);
      }
    }
    else
    {
      DBTypedObjectID typedObjectId = this.GetTypedObjectID(this.recId);
      typedObjectIDs.Add((IDBTypedObjectID) typedObjectId);
      longList.Add(this.recId);
    }
    if (signAs)
      SignsCommands.SignAsCommand(typedObjectIDs);
    else
      SignsCommands.SignUpCommand(typedObjectIDs);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long num in longList)
      {
        CJEditorForm.CJRecordInfo cjRecordInfo = this.recTable[num];
        if (sessionKeeper.Session.GetCustomService(typeof (ISignsService)) is ISignsService customService)
        {
          List<SignParams> objectSignsParams = customService.GetObjectSignsParams(num, sessionKeeper.Session.SessionGUID);
          if (cjRecordInfo.SignList.Count != objectSignsParams.Count)
            cjRecordInfo.SignList = objectSignsParams;
        }
      }
    }
    if (this.selList.Count == 0 && this.elCurChange != null)
      this.selList.Add(this.elCurChange);
    foreach (TableElement sel in this.selList)
    {
      if (sel.FindFirstNodeFromTemplate_Recursive(CJEditorForm.idChangeNum) is TextData templateRecursive && templateRecursive.Text != "")
      {
        CJEditorForm.CJRecordInfo cjri = this.recTable[this.cjRecList[Convert.ToInt32(templateRecursive.Text) - 1].id];
        this.UpdateSigns((TableData) sel, cjri);
      }
    }
    this.Document.UpdateLayout(0, true, true);
  }

  private void cmdStartProcess(object sender, EventArgs e)
  {
    ProcParm procParm = new ProcParm();
    if (!procParm.Execute() || procParm.processId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> objList = new List<long>();
      if (this.selList.Count > 1)
      {
        foreach (int recNum in this.GetRecNumList(this.selList))
        {
          long id = this.cjRecList[recNum].id;
          objList.Add(id);
        }
      }
      else
        objList.Add(this.recId);
      ECOPlugin.SendByProcess(sessionKeeper.Session, procParm.processId, (IEnumerable<long>) objList, procParm.theme, procParm.message);
    }
  }

  private void SelReason(DocumentTreeNode dtn)
  {
    TextData templateRecursive1 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr) as TextData;
    TextData templateRecursive2 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idReason) as TextData;
    string reason = "";
    if (templateRecursive2 != null)
      reason = templateRecursive2.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, true);
    string str = new EditReason().Execute(ref reason, new Guid(RevHelper.guidAttrRevReason));
    if (str == "-2")
      return;
    if (templateRecursive2 != null)
      this.SetFieldText(templateRecursive2, reason);
    if (templateRecursive1 != null)
    {
      string newVal = str != "-1" ? str : (string) null;
      this.SetFieldText(templateRecursive1, newVal);
    }
    this.eco.reasonCode = str;
  }

  private void SelZadel(DocumentTreeNode dtn)
  {
    EditReason editReason = new EditReason();
    string newVal = "";
    if (dtn is TextData td)
      newVal = td.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, true);
    ref string local = ref newVal;
    Guid attrGuid = new Guid(RevHelper.guidAttrZadel);
    if (editReason.Execute(ref local, attrGuid) == "-2" || td == null)
      return;
    this.SetFieldText(td, newVal);
  }

  private void SelDate(DocumentTreeNode dtn)
  {
    TextData td = (TextData) null;
    if (dtn != null && dtn is TextData)
      td = dtn as TextData;
    if (td == null)
      return;
    DateTime dt = DateTime.Now;
    try
    {
      string attributeValue = td.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, false);
      if (attributeValue != null)
        dt = Convert.ToDateTime(attributeValue);
    }
    catch (FormatException ex)
    {
    }
    IPageElementWithInterface elementWithInterface = td as IPageElementWithInterface;
    Rectangle rectangle = Rectangle.Empty;
    if (elementWithInterface != null)
      rectangle = elementWithInterface.PageUI.Bounds;
    Intermech.ECO.Client.SelDate selDate = new Intermech.ECO.Client.SelDate();
    Point screen = elementWithInterface.PageUI.PageControl.PointToScreen(Point.Empty);
    Point loc = new Point(screen.X + rectangle.X, screen.Y + rectangle.Bottom);
    DateTime dateTime = selDate.Execute(dt, loc);
    if (selDate.dr == DialogResult.Cancel)
      return;
    string shortDateString = selDate.dr == DialogResult.Yes ? (string) null : dateTime.ToShortDateString();
    this.SetFieldText(td, shortDateString);
    if (td.Id == Intermech.ECO.Client.ECO.idStartChangeTerm)
      this.eco.changeTermStart = dateTime;
    if (!(td.Id == Intermech.ECO.Client.ECO.idEndChangeTerm))
      return;
    this.eco.changeTermEnd = dateTime;
  }

  private void SelOther(DocumentTreeNode dtn)
  {
    TextData td = (TextData) null;
    if (dtn != null && dtn is TextData)
      td = dtn as TextData;
    if (td == null)
      return;
    string attributeValue = td.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, true);
    if (!new InputForm().Execute(td.Name, ref attributeValue))
      return;
    this.SetFieldText(td, attributeValue);
  }

  private void UpdateValue(DocumentTreeNode dtn, TextData td, string newVal)
  {
    dtn.SetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, newVal);
    string str = string.Format(LocalizationHolder.rm.GetString("ECO.Client_285"), (object) td.Name) + (newVal == null ? LocalizationHolder.rm.GetString("ECO.Client_286") : newVal);
    TextData node = dtn.Nodes[0] as TextData;
    node.Text = str;
    node.ReadOnly = true;
    td.SetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, newVal);
  }

  private void SetFieldText(TextData td, string newVal)
  {
    TableElement tableElement1;
    if (this.eco.ecoMainTable.NodesCount == 0)
    {
      tableElement1 = this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
      tableElement1.RemoveChildNodeAt(1, true, true);
    }
    else
      tableElement1 = (TableElement) this.eco.ecoMainTable.Nodes[0];
    foreach (DocumentTreeNode node in tableElement1.Nodes)
    {
      if (node.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenId, false) == td.TemplateId && node.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, false) != newVal)
      {
        this.UpdateValue(node, td, newVal);
        return;
      }
    }
    TableElement tableElement2 = (tableElement1.Template.FindNode(Intermech.ECO.Client.ECO.fldVar1) as TableElement).CloneFromTemplate() as TableElement;
    tableElement2.SetAttributeValue(Intermech.ECO.Client.ECO.hiddenId, td.TemplateId);
    tableElement1.AddChildNode((DocumentTreeNode) tableElement2, false, false);
    this.UpdateValue((DocumentTreeNode) tableElement2, td, newVal);
  }

  public override void Activated()
  {
    base.Activated();
    if (this._activated)
      return;
    try
    {
      ECOPlugin plugin = ECOPlugin.FindPlugin();
      if (plugin == null)
        return;
      plugin.CurRevId = this.ecoID;
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

  private void ECOEditorForm_Closed(object sender, EventArgs e)
  {
  }

  public override string HelpID => "840";

  public class RecInfo
  {
    private int _recIndex;
    private TableData _recTable;

    public int RecIndex
    {
      get => this._recIndex;
      set => this._recIndex = value;
    }

    public TableData RecTable
    {
      get => this._recTable;
      set => this._recTable = value;
    }

    public RecInfo(int recIndex, TableData tElem)
    {
      this._recIndex = recIndex;
      this._recTable = tElem;
    }
  }

  internal class CJRecordInfo
  {
    private List<long> _objList;
    private long _replacingECO_Id;
    private List<SignParams> _signList;
    private DateTime _recDate;
    private bool _readOnly;

    public List<long> ObjList => this._objList;

    public long ReplacingECO_Id
    {
      get => this._replacingECO_Id;
      set => this._replacingECO_Id = value;
    }

    public List<SignParams> SignList
    {
      get => this._signList;
      set => this._signList = value;
    }

    public DateTime RecDate
    {
      get => this._recDate;
      set => this._recDate = value;
    }

    public bool ReadOnly
    {
      get => this._readOnly;
      set => this._readOnly = value;
    }

    public CJRecordInfo(List<long> oList)
    {
      this._objList = oList;
      this._replacingECO_Id = 0L;
      this._signList = new List<SignParams>();
    }

    public CJRecordInfo(List<long> oList, long replacingId, List<SignParams> signs)
    {
      this._objList = oList;
      this._replacingECO_Id = replacingId;
      this._signList = signs ?? new List<SignParams>();
    }
  }

  internal class IdGuid
  {
    public long id;
    public Guid guid;

    public IdGuid(long i, Guid g)
    {
      this.id = i;
      this.guid = g;
    }
  }

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

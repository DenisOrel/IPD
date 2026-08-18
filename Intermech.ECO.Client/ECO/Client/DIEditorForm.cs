// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.DIEditorForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Bars;
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
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class DIEditorForm : ECOAncestorForm, IFiltrationClass
{
  public static readonly Guid DIWindowGuid = new Guid("{CEA01358-7ED6-4ed3-9D1D-B1CC53570D24}");
  private List<string> allowedStrings = new List<string>()
  {
    "I120",
    "I223",
    "I3",
    "J3",
    "I4",
    "J4",
    "I5",
    "J5",
    "I6",
    "J6",
    "I7",
    "J7",
    "I8",
    "J8"
  };
  private bool lockChange;
  public Hashtable addedLinks = new Hashtable();
  public Hashtable deletedLinks = new Hashtable();
  public Hashtable changedLinks = new Hashtable();
  private ImageList IL;
  private IContainer components;
  private List<PageElementUI> items = new List<PageElementUI>();
  private MenuButtonItem includeMenu;
  private MenuButtonItem includeElem;
  private MenuButtonItem editElem;
  private MenuButtonItem clearElem;
  private MenuButtonItem removeElem;
  private MenuButtonItem usabElem;
  private MenuButtonItem leaveElem;
  private MenuButtonItem insertTemplate;
  private MenuButtonItem sortChange;
  private TableElement elWorkspace;
  private TableElement elCurChange;
  private int indexCurChange = -1;
  private TableElement elCurElem;
  private ContainerElement elPicture;
  private TextData textFld;

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

  protected override string GetConfigName() => "DI.Editor";

  protected override string GetToolbarConfigName() => "DI.Toolbar";

  protected override void Init()
  {
    base.Init();
    this.Guid = DIEditorForm.DIWindowGuid;
  }

  protected override void InitBarManager()
  {
    bool showDebugInfo = ImDocumentEditorConfig.Instance.ShowDebugInfo;
    this.SetBaseEditCommandsEnabled(showDebugInfo, showDebugInfo);
    base.InitBarManager();
  }

  public DIEditorForm(IImDocumentManager documentManager, ImDocument document, bool readOnly)
    : base(documentManager, document, readOnly)
  {
  }

  public DIEditorForm(
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
          commandState.Enabled = !this.ReadOnly && this.eco.objLinks.Count > 0;
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
      if (DIEditorForm.IsEcoContentsDocNode(docNode))
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
    return DIEditorForm.FindParentEcoContentsDocNode(docNode) != null;
  }

  public TableElement CurrentChange => (TableElement) null;

  public void AfterLoadDoc()
  {
    this._filtrationService = this.InitializeFiltrationService();
    if (!this.Document.DocumentControl.ReadOnly)
    {
      DocumentTreeNode templateRecursive1 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idReason);
      if (templateRecursive1 != null && templateRecursive1 is TextData)
        (templateRecursive1 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelReason);
      DocumentTreeNode templateRecursive2 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr);
      if (templateRecursive2 != null && templateRecursive2 is TextData)
        (templateRecursive2 as TextData).CallExternalEditor = new CallDocNodeEditorDelegate(this.SelReason);
      DocumentTreeNode templateRecursive3 = this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idUkazVnedrenie);
      if (templateRecursive3 != null)
      {
        TextData textData = templateRecursive3 as TextData;
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
      List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
      this.Document.FindNodes(typeof (TextBoxElement), foundNodes);
      foreach (TextBoxElement textBoxElement in foundNodes)
      {
        if (!this.IsEditingAllowed(textBoxElement.Id))
        {
          textBoxElement.ReadOnly = true;
          if (textBoxElement.Id.StartsWith("I") && textBoxElement.CallExternalEditor == null && textBoxElement.Id != Intermech.ECO.Client.ECO.idRevDesignation && textBoxElement.Id != Intermech.ECO.Client.ECO.idPIDesignation)
            textBoxElement.CallExternalEditor = new CallDocNodeEditorDelegate(this.SelOther);
        }
        else
          textBoxElement.ReadOnly = false;
      }
    }
    if (this.Document.DocumentControl.ReadOnly)
      return;
    this.UpdateDocDesign();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.eco.EcoObjectID);
      if (this.ReadOnly)
        return;
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(RevHelper.idAttrRevReason);
      if (attributeById1 != null)
      {
        string str1 = Convert.ToString(attributeById1.Value);
        this.eco.reasonCode = Convert.ToString(str1);
        string asString = str1 != "-1" ? attributeById1.AsString : "";
        string description = str1 != "-1" ? attributeById1.Description : "";
        string str2 = LocalizationHolder.rm.GetString("ECO.Client_238");
        TextData templateRecursive10 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idShifr);
        if (templateRecursive10 != null && templateRecursive10.Text != asString)
          templateRecursive10.Text = asString;
        TextData templateRecursive11 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idReason);
        if (templateRecursive11 != null && templateRecursive11.Text != description && (str1 != "-1" && str1 != "-" || templateRecursive11.Text == str2))
          templateRecursive11.Text = description;
      }
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(RevHelper.idAttrLitera);
      if (attributeById2 != null)
        this.eco.litera = attributeById2.AsString;
      IDBAttribute attributeById3 = dbObject.GetAttributeByID(RevHelper.idAttrRevSeriesDates);
      if (attributeById3 != null)
        this.eco.SDAC = attributeById3.AsString;
      IDBAttribute attributeById4 = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateStart);
      if (attributeById4 != null)
      {
        if (attributeById4.Value == null || attributeById4.Value == DBNull.Value)
          this.eco.changeTermStart = DateTime.MinValue;
        else
          this.eco.changeTermStart = Convert.ToDateTime(attributeById4.Value);
        TextData templateRecursive = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idStartChangeTerm);
        if (templateRecursive != null && templateRecursive.Text != "")
          templateRecursive.Text = "";
      }
      IDBAttribute attributeById5 = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateEnd);
      if (attributeById5 != null)
      {
        if (attributeById5.Value == null || attributeById5.Value == DBNull.Value)
          this.eco.changeTermEnd = DateTime.MinValue;
        else
          this.eco.changeTermEnd = Convert.ToDateTime(attributeById5.Value);
        TextData templateRecursive = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idEndChangeTerm);
        if (templateRecursive != null && templateRecursive.Text != "")
          templateRecursive.Text = "";
      }
      IDBAttribute attributeById6 = dbObject.GetAttributeByID(RevHelper.idAttrVersion);
      if (attributeById6 != null)
      {
        this.eco.ecoVersion = attributeById6.AsInteger;
      }
      else
      {
        string attributeValue = this.eco.ecoMainTable.GetAttributeValue(Intermech.ECO.Client.ECO.versionIdAttr, true);
        if (attributeValue == "")
          this.eco.ecoVersion = 0L;
        else
          this.eco.ecoVersion = (long) Convert.ToInt32(attributeValue);
      }
      IDBAttribute attributeById7 = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
      if (attributeById7 != null)
      {
        string asString = attributeById7.AsString;
        TextData templateRecursive = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(this.eco.revType == RevType.DPI ? Intermech.ECO.Client.ECO.idPIDesignation : Intermech.ECO.Client.ECO.idRevDesignation);
        if (templateRecursive != null && templateRecursive.Text != asString)
          templateRecursive.Text = asString;
      }
      TextData templateRecursive12 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(this.eco.revType != RevType.DPI ? Intermech.ECO.Client.ECO.idPIDesignation : Intermech.ECO.Client.ECO.idRevDesignation);
      if (templateRecursive12 == null || !(templateRecursive12.Text != ""))
        return;
      templateRecursive12.Text = "";
    }
  }

  internal bool IsEditingAllowed(string Id) => this.allowedStrings.Contains(Id);

  public void UpdateDocDesign()
  {
    int num = 0;
    TableData change = (TableData) null;
    foreach (DocumentTreeNode dtn in (TableData) this.eco.ecoMainTable)
    {
      switch (this.eco.ChangeGoal(dtn))
      {
        case ECOGoal.NoGoal:
          continue;
        case ECOGoal.Change:
          ++num;
          if (change == null)
          {
            change = (TableData) dtn;
            continue;
          }
          continue;
        default:
          ++num;
          continue;
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
            if (pendingLink != null)
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
    if (this.eco.ecoDocRevision != null && this.eco.ecoDocRevision.Text != attributeValue)
      this.eco.ecoDocRevision.Text = attributeValue;
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
            templateRecursive.Text = "";
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
          switch (this.eco.ChangeGoal((DocumentTreeNode) dtn))
          {
            case ECOGoal.Change:
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
                  templateRecursive.Text = "";
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
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ECOEditorForm));
    this.IL = new ImageList(this.components);
    this.topBarDock.SuspendLayout();
    this.SuspendLayout();
    this.leftBarDock.Location = new Point(0, 52);
    this.leftBarDock.Size = new Size(0, 235);
    this.rightBarDock.Location = new Point(362, 52);
    this.rightBarDock.Size = new Size(0, 235);
    this.topBarDock.Size = new Size(362, 52);
    this.formatToolBar.Location = new Point(2, 0);
    this.formatToolBar.Size = new Size(772, 26);
    this.tableToolBar.Location = new Point(2, 26);
    this.tableToolBar.Size = new Size(516, 26);
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Transparent;
    this.IL.Images.SetKeyName(0, "apply.bmp");
    this.AllowDrop = true;
    this.Name = "ECOEditorForm";
    this.Closed += new EventHandler(this.ECOEditorForm_Closed);
    this.topBarDock.ResumeLayout(false);
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
          if (this.SaveRevision())
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
      case DialogResult.No:
        if (this.eco.newVers.Count <= 0)
          break;
        switch (MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_65"), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.YesNoCancel))
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            return;
          case DialogResult.Yes:
            try
            {
              this.SaveRevision();
              this.Document.Modified = false;
              return;
            }
            catch
            {
              e.Cancel = true;
              return;
            }
          default:
            return;
        }
    }
  }

  private bool SaveRevision()
  {
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
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
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
      (sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer).SetLitera(sessionKeeper.Session.SessionGUID, this.eco.EcoObjectID, this.eco.litera);
      DateTime dateTime;
      if (!this.eco.changeTermStart.Equals(DateTime.MinValue))
      {
        IDBAttribute dbAttribute = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateStart) ?? dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeDateStart, false);
        if (dbAttribute != null)
        {
          if (dbAttribute.Value != DBNull.Value)
          {
            dateTime = Convert.ToDateTime(dbAttribute.Value);
            if (dateTime.Equals(this.eco.changeTermStart))
              goto label_23;
          }
          dbAttribute.AsDateTime = this.eco.changeTermStart;
        }
      }
label_23:
      if (!this.eco.changeTermEnd.Equals(DateTime.MinValue))
      {
        IDBAttribute dbAttribute = dbObject.GetAttributeByID(RevHelper.idAttrChangeDateEnd) ?? dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeDateEnd, false);
        if (dbAttribute != null)
        {
          if (dbAttribute.Value != DBNull.Value)
          {
            dateTime = Convert.ToDateTime(dbAttribute.Value);
            if (dateTime.Equals(this.eco.changeTermEnd))
              goto label_28;
          }
          dbAttribute.AsDateTime = this.eco.changeTermEnd;
        }
      }
label_28:
      IDBAttribute dbAttribute1 = dbObject.Attributes.AddAttribute(RevHelper.idAttrVersion, false);
      if (dbAttribute1 != null)
        dbAttribute1.AsInteger = Intermech.ECO.Client.ECO.curVersion;
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
    DocumentEditorPlugin.SaveImDocumentObjectFile(this.DocumentID, this.Document, this.DefaultFileName, 0, true);
    this.addedLinks.Clear();
    this.deletedLinks.Clear();
    this.changedLinks.Clear();
    this.Document.Modified = false;
    return true;
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
      return (session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetEditingContextsObject((object) session.SessionGUID, editingContextsObject.LinkedContextNumber, false, true).GetObjectVersion(dbObject.ID);
    }
    catch
    {
      return -1;
    }
  }

  private long[] AddAuxLinksAttr(IDBRelation relation, PendingLink pl)
  {
    if (pl.auxObjects == null || pl.auxObjects.Count <= 0)
      return (long[]) null;
    long[] instance1 = (long[]) Array.CreateInstance(typeof (long), pl.auxObjects.Count);
    IDBAttribute dbAttribute = relation.Attributes.AddAttribute(RevHelper.idAttrAuxLinks, false);
    if (dbAttribute != null)
    {
      object[] instance2 = (object[]) Array.CreateInstance(typeof (object), pl.auxObjects.Count);
      for (int index = 0; index < pl.auxObjects.Count; ++index)
      {
        instance1[index] = pl.auxObjects[index].verId;
        instance2[index] = (object) instance1[index];
      }
      dbAttribute.Values = instance2;
    }
    return instance1;
  }

  public void SynchronizeECODocumentWithDB(bool ReadOnly)
  {
    if (this.eco.EcoObjectID == -1L)
      return;
    this.eco.CheckEcoMainTable();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable1 = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkFromDI).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[4]
      {
        (object) -26,
        (object) -22,
        (object) -2,
        (object) -21
      }), this.eco.EcoObjectID);
      if (dataTable1 != null)
      {
        if (dataTable1.Rows.Count > 0)
        {
          long int64 = Convert.ToInt64(dataTable1.Rows[0][2]);
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
          relationCollection.LocalTypesMode = true;
          DataTable dataTable2 = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[5]
          {
            (object) -26,
            (object) -22,
            (object) -2,
            (object) -21,
            (object) RevHelper.idAttrIncludeGoal
          }), int64);
          if (dataTable2 != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
              this.eco.objLinks.Add(new PendingLink(Convert.ToInt64(row[2]), "", (ECOGoal) Convert.ToInt32(row[4])));
          }
        }
      }
    }
    TextData templateRecursive1 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idStartChangeTerm);
    if (templateRecursive1 != null && templateRecursive1.Text != "")
      templateRecursive1.Text = "";
    TextData templateRecursive2 = (TextData) this.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idEndChangeTerm);
    if (templateRecursive2 == null || !(templateRecursive2.Text != ""))
      return;
    templateRecursive2.Text = "";
  }

  public void AddNewChange(long objID, PendingLink pl)
  {
    TableElement tableElement = this.eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
    string attributeValue = this.eco.IdLists() ? Convert.ToString(objID) : pl.verGuid.ToString();
    if (tableElement == null)
      return;
    tableElement.SetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, attributeValue);
    TextData templateRecursive = tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo) as TextData;
    IDBObject dbObject = (IDBObject) null;
    templateRecursive.Text = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      dbObject = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      if (dbObject == null)
      {
        objID = -objID;
        dbObject = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      }
      if (dbObject != null)
      {
        int attributeId = sessionKeeper.Session.IdentHelper.GetAttributeID("cad00770-306c-11d8-b4e9-00304f19f545");
        if (dbObject.GetAttributeByID(attributeId) != null)
          templateRecursive.Text = Convert.ToString(dbObject.VersionID);
      }
    }
    (tableElement.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) as TextData).Text = this.eco.GetDocDesignationInECO(dbObject);
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
    return node.Name == Intermech.ECO.Client.ECO.fldVar1 || node.Name == Intermech.ECO.Client.ECO.fldVar2 || node.Name == Intermech.ECO.Client.ECO.fldVar3 || node.Name == Intermech.ECO.Client.ECO.fldVar4 || node.Name == Intermech.ECO.Client.ECO.fldVar5 || node.Name == Intermech.ECO.Client.ECO.fldVar6;
  }

  private void ClearSelection()
  {
    this.elCurChange = (TableElement) null;
    this.elCurElem = (TableElement) null;
    this.elPicture = (ContainerElement) null;
    this.indexCurChange = -1;
  }

  private void GetSelection()
  {
    this.ClearSelection();
    foreach (DocumentTreeNode parent in this.GetCommandContext())
    {
      while (parent != null && !this.IsElement(parent) && !(parent.Name == Intermech.ECO.Client.ECO.fldChange))
      {
        parent = parent.Parent;
        if (parent == null)
          break;
      }
      if (this.IsElement(parent))
      {
        this.elCurElem = parent as TableElement;
        this.elCurChange = this.elCurElem.Parent as TableElement;
        if (this.elWorkspace == null)
          this.elWorkspace = this.elCurChange.Parent as TableElement;
        if (this.elWorkspace == null)
          break;
        for (int index = 0; index < this.elWorkspace.Nodes.Count; ++index)
        {
          if (this.elWorkspace.Nodes[index] == this.elCurChange)
          {
            this.indexCurChange = index;
            break;
          }
        }
        break;
      }
      if (parent != null && parent.Name == Intermech.ECO.Client.ECO.fldChange)
      {
        this.elCurChange = parent as TableElement;
        if (this.elWorkspace == null)
          this.elWorkspace = this.elCurChange.Parent as TableElement;
        if (this.elWorkspace == null)
          break;
        for (int index = 0; index < this.elWorkspace.Nodes.Count; ++index)
        {
          if (this.elWorkspace.Nodes[index] == this.elCurChange)
          {
            this.indexCurChange = index;
            break;
          }
        }
      }
    }
  }

  private void GetCurrents()
  {
    this.ClearSelection();
    if (this.elWorkspace == null)
      return;
    int num = -1;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null)
      {
        if (pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldChange)
        {
          this.elCurChange = pageElementUi.Element as TableElement;
          foreach (DocumentTreeNode node in this.elWorkspace.Nodes)
          {
            ++num;
            TableElement elCurChange = this.elCurChange;
            if (node == elCurChange)
              this.indexCurChange = num;
          }
        }
        else if (pageElementUi.Element is ContainerData)
          this.elPicture = pageElementUi.Element as ContainerElement;
      }
    }
    if (this.elCurChange == null)
      return;
    foreach (PageElementUI pageElementUi in this.items)
    {
      if (pageElementUi.Element != null)
      {
        if (pageElementUi.Element.Name == Intermech.ECO.Client.ECO.fldChangeHeader)
          break;
        if (this.IsElement((DocumentTreeNode) pageElementUi.Element))
        {
          this.elCurElem = pageElementUi.Element as TableElement;
          break;
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

  private void MakeContMenu(ArrayList al)
  {
    if (this.elCurChange != null || EcoTreeViewDlg.TreeMenu)
    {
      int num1 = 0;
      List<long> longList = new List<long>();
      if (this.elCurChange != null)
        longList = this.eco._GetIdList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
      for (int index = 0; index < longList.Count; ++index)
      {
        long objectID = longList[index];
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
          al.Add((object) menuButtonItem);
          num3 += 20;
        }
      }
      IList list = (IList) al;
      if (num2 > 0)
        list = (IList) (al[0] as MenuButtonItem).Items;
      for (int index1 = 0; index1 < longList.Count; ++index1)
      {
        long num4 = longList[index1];
        int index2 = this.ECO.ObjIdIndex(num4);
        if (index2 >= 0)
        {
          PendingLink objLink = this.eco.objLinks[index2];
          if (objLink.design == null)
            objLink.UpdateDesign();
          MenuButtonItem menuButtonItem1 = new MenuButtonItem(objLink.design);
          long num5 = num4;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(num4, false) ?? sessionKeeper.Session.GetObjectActualCopy(-num4, false);
            if (dbObject != null)
              num5 = dbObject.ObjectID;
            else
              continue;
          }
          ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications);
          AdvancedServiceContainer services = new AdvancedServiceContainer();
          services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
          MenuBarItem menu = Services.GetMenu(ObjectExtensions.GetItems(new long[1]
          {
            num5
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
            list = (IList) (al[index1 / 20] as MenuButtonItem).Items;
          list.Add((object) menuButtonItem1);
        }
      }
    }
    if (this.ReadOnly)
      return;
    if (this.includeMenu == null)
    {
      this.includeMenu = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_195"));
      this.includeMenu.Items.Add(LocalizationHolder.rm.GetString("ECO.Client_196"), new EventHandler(this.cmdIncludeDocs));
      this.includeMenu.Items.Add(LocalizationHolder.rm.GetString("ECO.Client_197"), new EventHandler(this.cmdIncludeDocsGroup));
      this.includeMenu.Items.Add(LocalizationHolder.rm.GetString("ECO.Client_198"), new EventHandler(this.cmdIncludeExternalDoc));
    }
    this.includeMenu.BeginGroup = this.elPicture != null || this.elCurChange != null;
    if (this.iNIL != null)
      this.includeMenu.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgApplyBall")];
    al.Add((object) this.includeMenu);
    if (this.includeElem == null)
    {
      this.includeElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_200"));
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
    }
    if (this.includeElem.Items.Count > 0)
    {
      if (this.iNIL != null)
        this.includeElem.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgOk")];
      this.includeElem.Enabled = this.elCurChange != null || this.GetLastElemWithId((DocumentTreeNode) this.eco.ecoMainTable, Intermech.ECO.Client.ECO.fldChange) != null;
      al.Add((object) this.includeElem);
    }
    if (this.removeElem == null)
      this.removeElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_201"), new EventHandler(this.cmdRemoveElem));
    if (this.iNIL != null)
      this.removeElem.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgDelete")];
    this.removeElem.Enabled = this.elCurChange != null && this.elCurElem != null && this.elCurElem.Id != Intermech.ECO.Client.ECO.idSpecText;
    al.Add((object) this.removeElem);
    if (this.elCurChange != null)
    {
      if (this.sortChange == null)
        this.sortChange = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_202"), new EventHandler(this.cmdSortChange));
      this.sortChange.BeginGroup = true;
      al.Add((object) this.sortChange);
    }
    if (this.textFld == null || this.elCurChange == null)
      return;
    this.insertTemplate = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_262"), new EventHandler(this.cmdInsertTemplate));
    this.insertTemplate.BeginGroup = true;
    al.Add((object) this.insertTemplate);
  }

  private void menuButtonItem_Click(object sender, EventArgs e)
  {
    int num = (int) this.TryActivateContext();
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
    if (this.textFld != null && (this.textFld.Id == Intermech.ECO.Client.ECO.idRevDesignation || this.textFld.Id == Intermech.ECO.Client.ECO.idPIDesignation))
      return;
    if (!this.ReadOnly && !this.IsInWorkspace() && this.textFld.Id.StartsWith("I"))
    {
      if (this.editElem == null)
        this.editElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_203"), new EventHandler(this.cmdEdit));
      e.ContextMenuItems.Add(this.editElem);
      if (this.textFld != null && this.textFld.Template.Id == Intermech.ECO.Client.ECO.idUsability)
      {
        if (this.usabElem == null)
          this.usabElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_205"), new EventHandler(this.cmdUsability));
        e.ContextMenuItems.Add(this.usabElem);
      }
      if (this.clearElem == null)
        this.clearElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_284"), new EventHandler(this.cmdClear));
      this.clearElem.BeginGroup = true;
      e.ContextMenuItems.Add(this.clearElem);
      if (this.leaveElem == null)
        this.leaveElem = new MenuButtonItem(LocalizationHolder.rm.GetString("ECO.Client_335"), new EventHandler(this.cmdLeave));
      this.leaveElem.BeginGroup = true;
      e.ContextMenuItems.Add(this.leaveElem);
    }
    this.EnableContextMenu();
  }

  private void EnableContextMenu()
  {
  }

  private void cmdUsability(object sender, EventArgs e)
  {
    this.AddUsability((DocumentTreeNode) this.textFld);
  }

  private void cmdSeriesDates(object sender, EventArgs e)
  {
  }

  private void cmdEdit(object sender, EventArgs e)
  {
    if (this.textFld.Id == Intermech.ECO.Client.ECO.idReason || this.textFld.Id == Intermech.ECO.Client.ECO.idShifr)
      this.SelReason((DocumentTreeNode) this.textFld);
    else if (this.textFld.Id == Intermech.ECO.Client.ECO.idZadel1 || this.textFld.Id == Intermech.ECO.Client.ECO.idZadel2)
      this.SelZadel((DocumentTreeNode) this.textFld);
    else if (this.textFld.Id == Intermech.ECO.Client.ECO.idCreationDate || this.textFld.Id == Intermech.ECO.Client.ECO.idStartChangeTerm || this.textFld.Id == Intermech.ECO.Client.ECO.idEndChangeTerm || this.textFld.Id == Intermech.ECO.Client.ECO.idPITerm)
      this.SelDate((DocumentTreeNode) this.textFld);
    else
      this.SelOther((DocumentTreeNode) this.textFld);
  }

  private void cmdClear(object sender, EventArgs e)
  {
    if (this.textFld.Id != Intermech.ECO.Client.ECO.idCreationDate)
      this.SetFieldText(this.textFld, (string) null);
    else
      this.textFld.AssignText("", false, false, false);
  }

  private void cmdLeave(object sender, EventArgs e)
  {
    if (this.textFld.Id != Intermech.ECO.Client.ECO.idCreationDate)
    {
      TableElement tableElement = (TableElement) null;
      if (this.eco.ecoMainTable.NodesCount > 0)
        tableElement = (TableElement) this.eco.ecoMainTable.Nodes[0];
      DocumentTreeNode node1 = (DocumentTreeNode) null;
      foreach (DocumentTreeNode node2 in tableElement.Nodes)
      {
        if (node2.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenId, false) == this.textFld.Id)
        {
          node1 = node2;
          break;
        }
      }
      if (node1 == null)
        return;
      tableElement.RemoveChildNode(node1, true, true);
    }
    else
      this.textFld.AssignText("", false, false, false);
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
    List<long> idList = this.eco._GetIdList(this.elCurChange.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true));
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

  private void cmdIncludeElem(object sender, EventArgs e)
  {
    change = this.elCurChange;
    if (change == null && !(this.GetLastElemWithId((DocumentTreeNode) this.eco.ecoMainTable, Intermech.ECO.Client.ECO.fldChange) is TableElement change))
      return;
    TableElement te = this.eco.InsertNewEcoElement(this.elCurElem, true, change, (sender as MenuButtonItem).Text, this.UndoManager);
    this.Document.UpdateLayout(0, true, true);
    this.GoToElem(te);
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

  private void InitDragEvents()
  {
    this.DocumentControl.ActivePageChanged += new ActivePageChanged_EventHandler(((ECOAncestorForm) this).DocumentControl_ActivePageChanged);
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
    this.UndoManager.BeginCreateMultyUndo("Смена причины");
    try
    {
      if (templateRecursive2 != null)
        this.SetFieldText(templateRecursive2, reason);
      if (templateRecursive1 != null)
      {
        string newVal = str != "-1" ? str : "-";
        this.SetFieldText(templateRecursive1, newVal);
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
    if (td.Id == Intermech.ECO.Client.ECO.idEndChangeTerm && !this.eco.AllGoalsChange())
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_327"), LocalizationHolder.rm.GetString("ECO.Client_147"), MessageBoxButtons.OK);
    }
    else
    {
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
      if (td.Id != Intermech.ECO.Client.ECO.idCreationDate)
        this.SetFieldText(td, shortDateString);
      else
        td.AssignText(shortDateString, false, false, false);
      if (td.Id == Intermech.ECO.Client.ECO.idStartChangeTerm)
        this.eco.changeTermStart = dateTime;
      bool flag = this.eco.revType == RevType.PI || this.eco.revType == RevType.DPI;
      if ((flag || !(td.Id == Intermech.ECO.Client.ECO.idEndChangeTerm)) && (!flag || !(td.Id == Intermech.ECO.Client.ECO.idPITerm)))
        return;
      this.eco.changeTermEnd = dateTime;
    }
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
      if (node.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenId, false) == td.TemplateId)
      {
        if (!(node.GetAttributeValue(Intermech.ECO.Client.ECO.hiddenValue, false) != newVal))
          return;
        this.UpdateValue(node, td, newVal);
        return;
      }
    }
    TableElement tableElement2 = (tableElement1.Template.FindNode(Intermech.ECO.Client.ECO.fldVar1) as TableElement).CloneFromTemplate() as TableElement;
    tableElement2.SetAttributeValue(Intermech.ECO.Client.ECO.hiddenId, td.TemplateId);
    tableElement1.AddChildNode((DocumentTreeNode) tableElement2, false, false);
    this.UpdateValue((DocumentTreeNode) tableElement2, td, newVal);
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
      }
      else
      {
        using (ObjSelect objSelect = new ObjSelect())
        {
          List<long> longList = objSelect.Execute(this.eco.ObjIdList(), false, true);
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
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID1, false);
        long id = objectActualCopy.ID;
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
        });
        List<long> longList1 = new List<long>();
        List<long> longList2 = new List<long>();
        longList1.Add(id);
        for (int index = 0; index < longList1.Count; ++index)
        {
          long partID = longList1[index];
          relationCollection.LocalTypesMode = true;
          DataTable dataTable = relationCollection.EntersIn(paramSet, partID);
          if (dataTable.Rows.Count == 0)
          {
            if (longList2.IndexOf(partID) < 0)
              longList2.Add(partID);
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
        foreach (long objectID2 in longList2)
        {
          objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID2, false);
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
          IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
            str = $"{LocalizationHolder.rm.GetString("ECO.Client_209")}{attributeByGuid.AsString}\r\n{str}";
        }
        string text = (dtn as TextData).Text;
        string newVal = text + (text != "" ? "\r\n" : "") + str;
        this.SetFieldText(dtn as TextData, newVal);
      }
    }
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
      int num = (int) this.TryActivateContext();
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
      (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).LockEditingContextID = false;
      this._filtrationService.OnFiltrationChanged -= new Intermech.Interfaces.Client.FiltrationChanged(((ECOAncestorForm) this).FiltrationChanged);
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

  public override string HelpID => "840";
}

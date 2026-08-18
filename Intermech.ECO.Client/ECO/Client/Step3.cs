// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.Step3
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core.History;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class Step3 : ObjectCreatorControl
{
  private List<string> reasonIds;
  private List<string> reasons;
  private List<long> templIds;
  private string reason = "-1";
  private int reason_index = -1;
  private long template = -1;
  private string design = "";
  private int revTypeID;
  private List<int> _allIIs;
  private static string noFile = LocalizationHolder.rm.GetString("ECO.Client_328");
  private long revObjId = -1;
  private long lastTemplateId;
  private bool wasRefresh;
  private IContainer components;
  private System.Windows.Forms.ComboBox comboReason;
  private Label lbl33;
  private ButtonEdit beDesign;
  private Label label2;
  private GroupBox gb;
  private Panel panel2;
  private Panel panel3;
  private ListBox listBox1;
  private ButtonEdit beScannedFile;
  private CheckBox cbScannedDoc;
  private OpenFileDialog ofd;

  public int RevTypeID
  {
    set => this.revTypeID = value;
  }

  public Step3(int typeID)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.Visible = true;
    this.revTypeID = typeID;
    this.LoadLastTemplateId();
    this._StepIsReadyCheckRequired = true;
    this._StepIsReady = false;
    this.DisableCalculatedDesignation();
    this._allIIs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_II);
  }

  public Step3(CreatedObjectItem coi)
    : base(coi)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.Visible = true;
    this.revTypeID = coi.ObjectTypeID;
    this.revObjId = coi.ObjectID;
    this._SaveInTransaction = false;
    this.LoadLastTemplateId();
    this._StepIsReadyCheckRequired = true;
    this._StepIsReady = false;
    this.DisableCalculatedDesignation();
    this._allIIs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_II);
  }

  private bool IsII(int objTypeId) => this._allIIs.Contains(objTypeId);

  private void LoadLastTemplateId()
  {
    this.lastTemplateId = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadInteger("ECOClient", "UserParms", "LastRevTempl" + Convert.ToString(this.revTypeID), 0L, DBConfigMode.UserOnly);
  }

  private void DisableCalculatedDesignation()
  {
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this.revTypeID, RevHelper.idAttrDesign);
    if (attribute4ObjectType == null || attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue)
      return;
    this.beDesign.Properties.ReadOnly = true;
  }

  internal void SetupControls(bool showTemplate)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataRow[] possibleValuesRows = sessionKeeper.Session.GetAttributeType(new Guid(RevHelper.guidAttrRevReason)).GetPossibleValuesRows();
      this.reasonIds = new List<string>(possibleValuesRows.Length);
      this.reasons = new List<string>(possibleValuesRows.Length);
      foreach (DataRow dataRow in possibleValuesRows)
      {
        string str1 = Convert.ToString(dataRow["F_STRING_VALUE"]);
        string str2 = Convert.ToString(dataRow["F_DESCRIPTION"]);
        this.reasonIds.Add(str1);
        this.reasons.Add(str2);
        this.comboReason.Items.Add((object) str2);
      }
      this.comboReason.SelectedIndex = 0;
      this.gb.Visible = showTemplate;
      this.cbScannedDoc.Visible = showTemplate;
      HybridDictionary hybridDictionary = new HybridDictionary();
      if (!hybridDictionary.Contains((object) "{7FB30639-2F65-4407-B78E-523547B1B133}"))
        hybridDictionary.Add((object) "{7FB30639-2F65-4407-B78E-523547B1B133}", (object) true);
      else
        hybridDictionary[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(RevHelper.idObjTypeRevTemplate).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_GUID,
        (object) ObligatoryObjectAttributes.CAPTION
      })
      {
        Tags = hybridDictionary
      });
      this.templIds = new List<long>(dataTable.Rows.Count);
      if (showTemplate)
        this.listBox1.Items.Clear();
      DataRow dataRow1 = (DataRow) null;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToString(row[1]) == RevHelper.guidDefRevTemplate)
        {
          this.templIds.Add(Convert.ToInt64(row[0]));
          if (showTemplate)
            this.listBox1.Items.Add((object) LocalizationHolder.rm.GetString("ECO.Client_143"));
          dataRow1 = row;
          break;
        }
      }
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row != dataRow1)
        {
          long int64 = Convert.ToInt64(row[0]);
          this.templIds.Add(int64);
          if (showTemplate)
            this.listBox1.Items.Add((object) Convert.ToString(row[2]));
          if (int64 == this.lastTemplateId)
            num = this.listBox1.Items.Count - 1;
        }
      }
      if (showTemplate)
      {
        if (this.listBox1.Items.Count > 0)
          this.listBox1.SelectedIndex = num;
      }
    }
    this._NeedSaveWhenNotVisible = true;
    this.beScannedFile.EditValue = (object) Step3.noFile;
  }

  public void ClassifyAttributes(long newECOObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(newECOObjectID, true);
      IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid1 != null)
        this.beDesign.Text = attributeByGuid1.Value.ToString();
      IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad0077d-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid2 == null || attributeByGuid2.IsNull)
        return;
      this.comboReason.SelectedIndex = this.comboReason.Items.IndexOf((object) this.reasons[int.Parse(attributeByGuid2.Value.ToString())]);
    }
  }

  public bool BeforeCreateObject()
  {
    if (this.beDesign.Text == "")
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_146"), LocalizationHolder.rm.GetString("ECO.Client_147"));
      return false;
    }
    this.design = this.beDesign.Text;
    this.reason = this.reasonIds[this.comboReason.SelectedIndex];
    this.reason_index = this.comboReason.SelectedIndex;
    this.template = this.templIds[this.listBox1.SelectedIndex];
    return true;
  }

  public IDBObject CreateObject(
    out ImDocument documentECO,
    MemoryStream ms,
    long newECOObjectID,
    List<long> ClassifiersToAdd)
  {
    ImDocument template = DocumentEditorPlugin.LoadDocumentFromDBObject(this.template, 0);
    if (!(template.FindNode(LocalizationHolder.rm.GetString("ECO.Client_144")) is TableElement))
      throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_145"));
    IDBObject dbObject = (IDBObject) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      dbObject = sessionKeeper.Session.GetObject(newECOObjectID, true);
      AttributeValues[] valuesList = new AttributeValues[2]
      {
        new AttributeValues(RevHelper.idAttrDesign, (object) this.design),
        new AttributeValues(RevHelper.idAttrRevReason, (object) this.reason)
      };
      dbObject.SetAttributesValues(valuesList);
      if (ms == null)
      {
        documentECO = new ImDocument(template, true, true);
        ms = new MemoryStream();
        documentECO.SaveToXml((Stream) ms);
      }
      else
      {
        MemoryStream memoryStream = new MemoryStream();
        ms.WriteTo((Stream) memoryStream);
        memoryStream.Position = 0L;
        documentECO = ImDocument.LoadFromStream((Stream) memoryStream, true, false, true);
        ms.Position = 0L;
      }
      new BlobProcWriter(dbObject.Attributes.FindByID(RevHelper.idAttrFile) ?? dbObject.Attributes.AddAttribute(RevHelper.idAttrFile, false), 0, new BlobInformation(ms.Length, 0L, DateTime.Now, this.design + ".revx", ArcMethods.ZLibPacked, ""), (Stream) ms, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      if (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
      {
        foreach (long num in ClassifiersToAdd)
        {
          // ISSUE: variable of a boxed type
          __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
          long selectionID = num;
          long[] objectIDs = new long[1]{ newECOObjectID };
          customService.IncludeObjects((object) sessionGuid, selectionID, objectIDs);
        }
      }
      dbObject.CommitCreation(false);
      documentECO.Reference = (ReferenceBase) new ReferenceToDBObject((DocumentTreeNode) documentECO, dbObject, false);
      TextData textData = (TextData) null;
      if (!this.IsII(this.revTypeID))
        textData = documentECO.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idPIDesignation) as TextData;
      if (textData == null)
        textData = documentECO.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idRevDesignation) as TextData;
      if (textData != null)
        textData.Text = this.design;
      if (documentECO.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idReason) is TextData nodeFromTemplate1)
        nodeFromTemplate1.Text = this.comboReason.Items[this.reason_index] as string;
      if (documentECO.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idShifr) is TextData nodeFromTemplate2)
        nodeFromTemplate2.Text = Convert.ToString(this.reason);
    }
    return dbObject;
  }

  public void SaveAttr(long newECOObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(newECOObjectID, true);
      this.design = this.beDesign.Text;
      this.reason = this.reasonIds[this.comboReason.SelectedIndex];
      AttributeValues[] valuesList = new AttributeValues[2]
      {
        new AttributeValues(RevHelper.idAttrDesign, (object) this.design),
        new AttributeValues(RevHelper.idAttrRevReason, (object) this.reason)
      };
      dbObject.SetAttributesValues(valuesList);
    }
  }

  private void beDesign_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.beDesign.Properties.ReadOnly)
      return;
    using (ObjectsHistory objectsHistory = new ObjectsHistory((object) this.revTypeID, AttributableElements.Object, (object) MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545")))
    {
      objectsHistory.SelectedValue = (object) this.beDesign.Text.Trim();
      if (objectsHistory.ShowDialog() != DialogResult.OK)
        return;
      this.beDesign.Text = (string) objectsHistory.SelectedValue;
    }
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this.revObjId != -1L)
        dbObject = sessionKeeper.Session.GetObject(this.revObjId);
      if (dbObject != null)
      {
        object[] valuesByGuid1 = dbObject.GetValuesByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
        if (valuesByGuid1 != null && valuesByGuid1[0] != DBNull.Value)
          this.beDesign.Text = Convert.ToString(valuesByGuid1[0]);
        object[] valuesById = dbObject.GetValuesByID(RevHelper.idAttrRevReason, false);
        if (valuesById != null && valuesById[0] != DBNull.Value)
        {
          object obj = valuesById[0];
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(RevHelper.idAttrRevReason);
          for (int index = 0; index < attributeType.PossibleValues.Count; ++index)
          {
            if (attributeType.PossibleValues[index].Equals(obj))
            {
              this.comboReason.SelectedIndex = index;
              break;
            }
          }
        }
        if (this.listBox1.Visible)
        {
          object[] valuesByGuid2 = dbObject.GetValuesByGuid(new Guid("cad01558-306c-11d8-b4e9-00304f19f545"), false);
          if (valuesByGuid2 != null)
          {
            if (valuesByGuid2[0] != DBNull.Value)
            {
              long int64 = Convert.ToInt64(valuesByGuid2[0]);
              for (int index = 0; index < this.templIds.Count; ++index)
              {
                if (this.templIds[index] == int64)
                {
                  this.listBox1.SelectedIndex = index;
                  break;
                }
              }
            }
          }
        }
      }
    }
    args.Error = (Exception) null;
    this.wasRefresh = true;
    this._StepIsReady = true;
    return true;
  }

  public override bool Save(PageSaveArgs args)
  {
    PageRefreshArgs args1 = new PageRefreshArgs();
    if (args.NextPageIndex == -1 && (!this.wasRefresh || args.currControl != this))
      this.Refresh(args1);
    if (args.NextPageIndex >= 0 && this.PageIndex > args.NextPageIndex)
      return true;
    if (args1.Error != null)
    {
      args.Error = args1.Error;
      return false;
    }
    if (this.beDesign.Text == "")
    {
      if (this.revObjId != -1L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.revObjId);
          if (dbObject != null)
          {
            IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(RevHelper.idAttrDesign, false);
            if (dbAttribute != null)
              this.beDesign.Text = dbAttribute.AsString;
          }
        }
      }
      if (this.beDesign.Text == "")
      {
        args.Error = new Exception(LocalizationHolder.rm.GetString("ECO.Client_146"));
        return false;
      }
    }
    this.design = this.beDesign.Text;
    bool flag = this.cbScannedDoc.Checked;
    if (flag && Convert.ToString(this.beScannedFile.EditValue) == Step3.noFile)
    {
      args.Error = new Exception(LocalizationHolder.rm.GetString("ECO.Client_329"));
      return false;
    }
    this.reason_index = this.comboReason.SelectedIndex;
    if (this.reason_index == -1)
      this.reason_index = this.comboReason.Items.Count - 1;
    this.reason = this.reasonIds[this.reason_index];
    if (this.templIds.Count == 0)
    {
      args.Error = new Exception(LocalizationHolder.rm.GetString("ECO.Client_452"));
      return false;
    }
    this.template = this.listBox1.Visible ? this.templIds[this.listBox1.SelectedIndex] : this.templIds[0];
    if (this.template != this.lastTemplateId)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.Configurations.WriteInteger("ECOClient", "UserParms", "LastRevTempl" + Convert.ToString(this.revTypeID), this.template, sessionKeeper.Session.UserID);
        this.lastTemplateId = this.template;
      }
    }
    ImDocument template = (ImDocument) null;
    if (this.gb.Visible)
    {
      template = DocumentEditorPlugin.LoadDocumentFromDBObject(this.template, 0);
      if (!(template.FindNode(LocalizationHolder.rm.GetString("ECO.Client_144")) is TableElement))
      {
        args.Error = new Exception(LocalizationHolder.rm.GetString("ECO.Client_145"));
        return false;
      }
    }
    if (this.revObjId != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.revObjId);
        if (dbObject != null)
        {
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(RevHelper.idAttrDesign, false);
          if (dbAttribute != null)
            dbAttribute.AsString = this.design;
          dbObject.Attributes.AddAttribute(RevHelper.idAttrRevReason, false).AsString = this.reason;
          dbObject.Attributes.AddAttribute(RevHelper.idAttrTemplate, false).AsInteger = this.template;
          if (this.revTypeID == RevHelper.idObjIPV)
          {
            IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idLinkedContNumber);
            if (attributeById != null)
            {
              long int64 = Convert.ToInt64(attributeById.Value);
              dbObject.Attributes.AddAttribute(RevHelper.idAttrChangeReason, false).AsInteger = int64;
            }
          }
        }
        if (this.gb.Visible)
        {
          ImDocument ownerNode = new ImDocument(template, true, true);
          ownerNode.Reference = (ReferenceBase) new ReferenceToDBObject((DocumentTreeNode) ownerNode, dbObject, false);
          TextData textData = (TextData) null;
          if (!this.IsII(this.revTypeID))
            textData = ownerNode.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idPIDesignation) as TextData;
          if (textData == null)
            textData = ownerNode.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idRevDesignation) as TextData;
          if (textData != null)
            textData.Text = this.design;
          if (ownerNode.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idReason) is TextData nodeFromTemplate1)
            nodeFromTemplate1.Text = this.comboReason.Items[this.reason_index] as string;
          if (ownerNode.FindFirstNodeFromTemplate(Intermech.ECO.Client.ECO.idShifr) is TextData nodeFromTemplate2)
            nodeFromTemplate2.Text = Convert.ToString(this.reason);
          MemoryStream aSourceStream = new MemoryStream();
          ownerNode.SaveToXml((Stream) aSourceStream);
          if (dbObject == null)
            return false;
          int num1 = flag ? RevHelper.idAttrDocFile4Scanned : RevHelper.idAttrFile;
          IDBAttribute aIDBAttribute1 = dbObject.Attributes.FindByID(num1) ?? dbObject.Attributes.AddAttribute(num1, false);
          BlobInformation aBlobInformation = new BlobInformation(aSourceStream.Length, 0L, DateTime.Now, FileNameHelper.ReplaceInvalidFileNameChars(this.design) + (flag ? "" : ".revx"), ArcMethods.ZLibPacked, "");
          BlobProcWriter blobProcWriter1 = new BlobProcWriter(aIDBAttribute1, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          try
          {
            blobProcWriter1.WriteData();
          }
          catch (Exception ex)
          {
            args.Error = ex;
            args.errorType = ErrorType.CheckNotCompleted;
            return false;
          }
          if (flag)
          {
            FileStream fileStream = new FileStream(this.ofd.FileName, FileMode.Open);
            aSourceStream.SetLength(fileStream.Length);
            try
            {
              byte[] buffer = aSourceStream.GetBuffer();
              int length = (int) fileStream.Length;
              int offset = 0;
              int num2;
              while ((num2 = fileStream.Read(buffer, offset, length - offset)) > 0)
                offset += num2;
            }
            catch (Exception ex)
            {
              args.Error = ex;
              args.errorType = ErrorType.CheckNotCompleted;
              return false;
            }
            finally
            {
              fileStream.Close();
            }
            IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(RevHelper.idAttrScannedDoc, false);
            if (dbAttribute == null)
              return false;
            dbAttribute.AsBoolean = true;
            IDBAttribute aIDBAttribute2 = dbObject.Attributes.AddAttribute(RevHelper.idAttrFile, false);
            if (aIDBAttribute2 == null)
              return false;
            string fileName = Path.GetFileName(this.ofd.FileName);
            aBlobInformation = new BlobInformation(aSourceStream.Length, 0L, DateTime.Now, fileName, ArcMethods.ZLibPacked, "");
            BlobProcWriter blobProcWriter2 = new BlobProcWriter(aIDBAttribute2, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
            try
            {
              blobProcWriter2.WriteData();
            }
            catch (Exception ex)
            {
              args.Error = ex;
              args.errorType = ErrorType.CheckNotCompleted;
              return false;
            }
          }
        }
      }
    }
    return true;
  }

  public override int HelpTopicID => 686;

  private void beDesign_EditValueChanged(object sender, EventArgs e)
  {
  }

  private void cbScannedDoc_CheckedChanged(object sender, EventArgs e)
  {
    this.beScannedFile.Visible = this.cbScannedDoc.Checked;
  }

  private void beScannedFile_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.ofd.ShowDialog() != DialogResult.OK)
      return;
    this.beScannedFile.EditValue = (object) this.ofd.FileName;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.comboReason = new System.Windows.Forms.ComboBox();
    this.lbl33 = new Label();
    this.beDesign = new ButtonEdit();
    this.label2 = new Label();
    this.gb = new GroupBox();
    this.listBox1 = new ListBox();
    this.panel2 = new Panel();
    this.beScannedFile = new ButtonEdit();
    this.cbScannedDoc = new CheckBox();
    this.panel3 = new Panel();
    this.ofd = new OpenFileDialog();
    this.beDesign.Properties.BeginInit();
    this.gb.SuspendLayout();
    this.panel2.SuspendLayout();
    this.beScannedFile.Properties.BeginInit();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.comboReason.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboReason.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboReason.FormattingEnabled = true;
    this.comboReason.Location = new Point(15, 68);
    this.comboReason.Name = "comboReason";
    this.comboReason.Size = new Size(565, 21);
    this.comboReason.TabIndex = 12;
    this.lbl33.AutoSize = true;
    this.lbl33.ImeMode = ImeMode.NoControl;
    this.lbl33.Location = new Point(12, 52);
    this.lbl33.Name = "lbl33";
    this.lbl33.Size = new Size(156, 13);
    this.lbl33.TabIndex = 11;
    this.lbl33.Text = "Причина выпуска извещения";
    this.beDesign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beDesign.EditValue = (object) "";
    this.beDesign.Location = new Point(15, 26);
    this.beDesign.Name = "beDesign";
    this.beDesign.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "История значений атрибута")
    });
    this.beDesign.Size = new Size(565, 20);
    this.beDesign.TabIndex = 10;
    this.beDesign.ButtonClick += new ButtonPressedEventHandler(this.beDesign_ButtonClick);
    this.beDesign.EditValueChanged += new EventHandler(this.beDesign_EditValueChanged);
    this.label2.AutoSize = true;
    this.label2.ImeMode = ImeMode.NoControl;
    this.label2.Location = new Point(12, 10);
    this.label2.Name = "label2";
    this.label2.Size = new Size(134, 13);
    this.label2.TabIndex = 9;
    this.label2.Text = "Обозначение извещения";
    this.gb.Controls.Add((Control) this.listBox1);
    this.gb.Dock = DockStyle.Fill;
    this.gb.Location = new Point(0, 0);
    this.gb.Name = "gb";
    this.gb.Size = new Size(603, 324);
    this.gb.TabIndex = 13;
    this.gb.TabStop = false;
    this.gb.Text = "Шаблон для извещения";
    this.listBox1.Dock = DockStyle.Fill;
    this.listBox1.FormattingEnabled = true;
    this.listBox1.Location = new Point(3, 16 /*0x10*/);
    this.listBox1.Name = "listBox1";
    this.listBox1.Size = new Size(597, 305);
    this.listBox1.TabIndex = 13;
    this.panel2.Controls.Add((Control) this.beScannedFile);
    this.panel2.Controls.Add((Control) this.cbScannedDoc);
    this.panel2.Controls.Add((Control) this.beDesign);
    this.panel2.Controls.Add((Control) this.label2);
    this.panel2.Controls.Add((Control) this.comboReason);
    this.panel2.Controls.Add((Control) this.lbl33);
    this.panel2.Dock = DockStyle.Top;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(603, 124);
    this.panel2.TabIndex = 15;
    this.beScannedFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beScannedFile.EditValue = (object) "Файл документа...";
    this.beScannedFile.Location = new Point(188, 96 /*0x60*/);
    this.beScannedFile.Name = "beScannedFile";
    this.beScannedFile.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beScannedFile.Properties.ReadOnly = true;
    this.beScannedFile.Size = new Size(392, 20);
    this.beScannedFile.TabIndex = 14;
    this.beScannedFile.Visible = false;
    this.beScannedFile.ButtonClick += new ButtonPressedEventHandler(this.beScannedFile_ButtonClick);
    this.cbScannedDoc.AutoSize = true;
    this.cbScannedDoc.Location = new Point(17, 98);
    this.cbScannedDoc.Name = "cbScannedDoc";
    this.cbScannedDoc.Size = new Size(165, 17);
    this.cbScannedDoc.TabIndex = 13;
    this.cbScannedDoc.Text = "Сканированное извещение";
    this.cbScannedDoc.UseVisualStyleBackColor = true;
    this.cbScannedDoc.CheckedChanged += new EventHandler(this.cbScannedDoc_CheckedChanged);
    this.panel3.Controls.Add((Control) this.gb);
    this.panel3.Dock = DockStyle.Fill;
    this.panel3.Location = new Point(0, 124);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(603, 324);
    this.panel3.TabIndex = 16 /*0x10*/;
    this.ofd.DefaultExt = "JPG";
    this.ofd.Filter = "Файлы изображений|*.PNG;*.BMP;*.JPG;*.TIF|Файлы *.JPG|*.JPG|Файлы PNG|*.PNG|Файлы BMP|*.BMP|Файлы TIF|*.TIF|Все файлы|*.*";
    this.ofd.RestoreDirectory = true;
    this.ofd.Title = "Выберите файл сканированного извещения";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (Step3);
    this.Size = new Size(603, 448);
    this.beDesign.Properties.EndInit();
    this.gb.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.beScannedFile.Properties.EndInit();
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}

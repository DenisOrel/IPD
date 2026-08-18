// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Vedomost_VB
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Victor;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс создания ведомостей программным методом Железняков В. </summary>
public class Vedomost_VB : IComparer<Vedomost_VB.RecordForVed_New>
{
  private ImDocument docTemplate;
  public PageData LRI_Page;
  public Guid _guidTemplateDoc = Guid.Empty;
  public Guid _guidTypeDoc = Guid.Empty;
  public string _format = "";
  public string _designationIspVed = "";
  public int _i_vybranogo_Ispolnenia;
  public AVSDocument _specificationMain;
  private long _objectIdMainSP;
  private int _objectTypeMainSp;
  private Guid _articleGroupIDMainSp = Guid.Empty;
  private long _objectIdMainArt;
  private Guid _articleGroupID = Guid.Empty;
  private bool _isGroupSp;
  public IMSObjectType _imsObjectType_RazrabatyvaemoiVed;
  public One_Ved_Nastr _one_Ved_Nastr_RazrabatyvaemoiVed;
  public int _urovenN = 1;
  public string _designationArticle;
  public string _designationDoc;
  public string _kodDoc;
  public string _derzPodl;
  public string _nameArticle;
  public string _nameTypeDoc;
  private bool _is_Golovnaia_Sp_Komplekt;
  private bool _isTherezKomplekt;
  private bool _isTherezDopZam;
  public bool _isGroupVed = true;
  public Vedomost_VB.FormaGroup _groupForm;
  private bool isPeremDannye;
  public ProductInfo _prodInfo;
  public long _iDSP;
  public string _metodCreate;
  public string _metodFrom;
  private List<Vedomost_VB.OneSpecification> _listSpecifications = new List<Vedomost_VB.OneSpecification>();
  private List<Vedomost_VB.RecordForMainVed> _listRecordsForMainVed;
  private List<Vedomost_VB.RecordForMainVed> _listRecordsForMainVed_DopZam;
  private bool _etap_Sbora_DopZam;
  public List<Vedomost_VB.RecordForVed_New> _listRecordsVed_New;
  public List<Vedomost_VB.RecordForVed_New> _listRecordsVed_New_DopZam;
  private List<Vedomost_VB.RecordForMainVed> _listSvoiaVedomost;
  public List<Vedomost_VB.RecordForVed_New> _listOglavlenie;
  public List<Vedomost_VB.One_Attribute> List_For_Rebuilding_From_Attributes;
  private List<Vedomost_VB.RecordForMainVed> _listRecordsForMainVed_VSI;
  private List<Vedomost_VB.RecordForMainVed> _listSvoiaVedomost_VSI;
  public List<ProductInfo> _listAll_IspolneniySp_prodInfo;
  public Vedomost_VB.Variables_Coordination _variables_Coordination;
  public List<Vedomost_VB.Izdelie_Doc> _listIzdelie_Doc;
  private Vedomost_VB.CompareRecordsForMainVed_byDesignation<Vedomost_VB.RecordForMainVed> _compareRecordsForMainVed_byDesignation = new Vedomost_VB.CompareRecordsForMainVed_byDesignation<Vedomost_VB.RecordForMainVed>();
  private Vedomost_VB.CompareRecordsForMainVed_byDesignation4<Vedomost_VB.RecordForMainVed> _compareRecordsForMainVed_byDesignation4 = new Vedomost_VB.CompareRecordsForMainVed_byDesignation4<Vedomost_VB.RecordForMainVed>();
  private Vedomost_VB.CompareRecordsVed_stepDopZam<Vedomost_VB.RecordForVed_New> _compareRecordsVed_stepDopZam = new Vedomost_VB.CompareRecordsVed_stepDopZam<Vedomost_VB.RecordForVed_New>();
  private Vedomost_VB.CompareRecordsVed_step0<Vedomost_VB.RecordForVed_New> _compareRecordsVed_step0 = new Vedomost_VB.CompareRecordsVed_step0<Vedomost_VB.RecordForVed_New>();
  private Vedomost_VB.Compare_objType_Ved1 _compare_objTypeVed1 = new Vedomost_VB.Compare_objType_Ved1();
  private Vedomost_VB.Compare_objType_Ved2 _compare_objTypeVed2 = new Vedomost_VB.Compare_objType_Ved2();
  private Vedomost_VB.Compare_RecordForVed_Vtor _compare_RecordForVed_Vtor = new Vedomost_VB.Compare_RecordForVed_Vtor();
  private List<Guid> _listSpGuids;
  private List<IMSObjectType> _listSpTyps;
  public ListCommonId listCommonId = new ListCommonId();
  public List<Vedomost_VB.RecordForVed_For_Isp> List_For_Isp;
  public ListError_OneError _listError_OneError = new ListError_OneError();
  public XmlDocument _xmlProtocol;
  public StreamWriter _txtProtocol;
  public XmlElement _xmlElementCurr;
  public XmlDocument xml_SborVed_Dump;
  public XmlDocument xml_SborMainVed_Dump;
  public SortSchema _sortSchema;
  private static int[] UrovniI = new int[20];
  public bool is_Zacikleno;
  public SborVedTask sborVedTask;
  public bool _isReDraw;
  public DocumentTreeNode _titListPage;
  public List<DocumentTreeNode> _listLizmPages;
  public bool _islistZagolovki;

  /// <summary>Создать и открыть новый документ</summary>
  public void CreateAndOpenNewDocument(bool isRedraw, bool quietMode, bool isConvert)
  {
    if (this.sborVedTask != null)
      this.sborVedTask.Dispose();
    this.sborVedTask = new SborVedTask("Вывод документа");
    this.sborVedTask.Show();
    ImDocument imDocument = this.GenerateImDocument(isRedraw, isConvert);
    if (imDocument == null)
    {
      if (this.sborVedTask == null)
        return;
      this.sborVedTask.Dispose();
    }
    else
    {
      this.txtProtocol_Add("Отображение ДОКУМЕНТА на экран");
      long num1 = -1;
      string documentObjectCaption = "";
      DBRelationsEventArgs e = (DBRelationsEventArgs) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IPDMSpecificationsService service = ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService;
        IDBObject dbObject1 = (IDBObject) null;
        int objTypeDocument = AvsIDCache.ObjType_Document;
        string designationDoc = this._designationDoc;
        long num2 = service.GetObjectWithDesignation(objTypeDocument, designationDoc);
        if (!num2.IsUndefinedId())
        {
          dbObject1 = sessionKeeper.Session.GetObject(num2, false);
          if (dbObject1 != null)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(num2, false);
            if (dbObject2 != null && dbObject2.ObjectModifyMode == ObjectModifyModes.CreateVersion)
            {
              IDBObject version = sessionKeeper.Session.GetObjectCollection(-1).CreateVersion(num2);
              if (version.IsCreationMode)
                version.CommitCreation(true, true);
              long objectId = version.ObjectID;
              dbObject1 = version;
              num2 = objectId;
            }
          }
        }
        if (dbObject1 == null)
        {
          dbObject1 = sessionKeeper.Session.GetObjectCollection(this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.Guid).Create();
          dbObject1.SetAttributesValues(DBObjectHelper.Filter(dbObject1, new AttributeValues[2]
          {
            new AttributeValues(AvsIDCache.Attr_Designation, (object) this._designationDoc),
            new AttributeValues(AvsIDCache.Attr_Name, (object) this._nameArticle)
          }), false, true);
          dbObject1.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_Format, (object) this._format)
          });
        }
        else if (dbObject1.ObjectType != this._one_Ved_Nastr_RazrabatyvaemoiVed._idTypeVed)
        {
          dbObject1.CheckIn();
          IDBObject dbObject3 = sessionKeeper.Session.GetObject(Math.Abs(num2));
          dbObject3.ObjectType = this._one_Ved_Nastr_RazrabatyvaemoiVed._idTypeVed;
          dbObject1 = dbObject3.CheckOut();
        }
        dbObject1.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_FirstApplicability, (object) this._designationArticle)
        });
        num1 = dbObject1.ObjectID;
        if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedAddToSp && this._prodInfo != null)
        {
          if (this._prodInfo.Id < 0L)
          {
            e = this.AddVedToSp(dbObject1);
          }
          else
          {
            long id = this._prodInfo.Id;
            sessionKeeper.Session.GetObject(id, false);
            if (sessionKeeper.Session.GetRelation(id, dbObject1.ObjectID, true) == null)
            {
              OneError oneError = new OneError();
              oneError._message_kurc = "Ведомость не смогли включить в спецификацию т.к. спецификация не взята на редактирование";
              oneError.Message();
              if (this._listError_OneError == null)
                this._listError_OneError = new ListError_OneError();
              this._listError_OneError._list.Add(oneError);
            }
          }
        }
        int objectType = dbObject1.ObjectType;
        if (dbObject1.IsCreationMode)
          dbObject1.CommitCreation(true, true);
        int typeId = dbObject1.TypeID;
        documentObjectCaption = dbObject1.Caption;
      }
      this.txtProtocol_Add("document.UpdateLayout 1");
      imDocument.UpdateLayout(false);
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._protection_From_Editing._isFullProhibition)
        Vedomost_VB_Static.MainTables_ReadOnly((DocumentTreeNode) imDocument, true, false);
      this.txtProtocol_Add("FillProductHeadersOnPages");
      this.FillProductHeadersOnPages(imDocument);
      Vedomost_VB_Static.FilledNumbersIspolneniyFormaB(imDocument);
      this.txtProtocol_Add("Вставка пустого LIZM");
      if (this._listLizmPages == null && imDocument.Nodes.Count > this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._iLIZM - 1)
      {
        this.LRI_Page = (PageData) this.docTemplate.FindFirstNodeByName("Лист регистрации изменений").CloneFromTemplate(true, true);
        imDocument.AddChildNode((DocumentTreeNode) this.LRI_Page, true, true);
      }
      Vedomost_VB.AlgorithmToPrint algorithmToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint;
      if (this._groupForm == Vedomost_VB.FormaGroup.B)
        algorithmToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint_B;
      if (algorithmToPrint._isDeleteIdenticalTexts && !isRedraw)
      {
        this.txtProtocol_Add("DeleteIdenticalTexts");
        Processing_Ved_Static.DeleteIdenticalTexts(imDocument);
        this.txtProtocol_Add("document.UpdateLayout 2");
        imDocument.UpdateLayout(true);
      }
      string defaultFileNameForDb = DocumentEditorPlugin.GenerateDefaultFileNameForDB((ImDocumentData) imDocument, num1, documentObjectCaption);
      this.txtProtocol_Add("SaveImDocumentObjectFile");
      DocumentEditorPlugin.SaveImDocumentObjectFile(num1, imDocument, defaultFileNameForDb, -1, true);
      if (e != null)
      {
        INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
        if (service != null && e.RelationIDs.Count > 0)
          service.FireEvent((object) this, (NotificationEventArgs) e);
      }
      DockControl openedDocument = DocumentEditorPlugin.Instance.FindOpenedDocument(DBHelper.GetObjGuidByID(num1));
      if (openedDocument != null)
      {
        if (openedDocument is ImDocumentEditorForm documentEditorForm)
          documentEditorForm.AskForSaveBeforeClose = false;
        openedDocument.Close();
      }
      if (this.sborVedTask != null)
        this.sborVedTask.Dispose();
      this.txtProtocol_Add("ОТКРЫТЬ ОКНО с документом");
      ImDocumentEditorForm documentEditorForm1 = DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(num1, false, true, new DocumentWindowCreatorDelegate(VedomostEditorWindow.VedomostEditorWindowCreator));
      if (!isRedraw)
      {
        if (this._listError_OneError != null)
        {
          if (this._listError_OneError._list.Count == 0)
          {
            int num3 = (int) MessageBox.Show("Ведомость создана\n\nЗамечаний нет", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
          else
          {
            int num4 = (int) MessageBox.Show("Ведомость создана\n\nЕсть замечания", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this._listError_OneError.Sort();
            this._listError_OneError.Union();
            documentEditorForm1.ErrorsUserControl.Show(this._listError_OneError.CreateErrorMessage());
          }
        }
      }
      else if (!quietMode)
      {
        string text = "Документ отображен заново";
        if (this._isReDraw && this._listLizmPages != null && this._listLizmPages.Count > 0)
          text += "\r\n\r\nПосле перерисовки документа проверьте содержимое Листа регистрации изменений";
        int num5 = (int) MessageBox.Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      if (e != null)
      {
        INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
        if (service != null && e.RelationIDs.Count > 0)
          service.FireEvent((object) this, (NotificationEventArgs) e);
      }
      if (!Vedomost_VB_Static.isCreateDump_Tmp)
        return;
      this.txtProtocol_Add("Создание Dump");
      ImDocumentEditorForm documentEditorForm2 = documentEditorForm1;
      string textIn1 = $"{Vedomost_VB_Static.DirectoryDump}\\{this.DesignationDoc}.pdf";
      string textIn2 = $"{Vedomost_VB_Static.DirectoryDump}\\{this.DesignationDoc}.imdx";
      string fileName1 = Vedomost_VB_Static.Replace_Invalid_Char(textIn1, true);
      string fileName2 = Vedomost_VB_Static.Replace_Invalid_Char(textIn2, true);
      documentEditorForm2.Document.SaveToPdf(fileName1, false);
      documentEditorForm2.Document.SaveToXml(fileName2, false);
      Vedomost_VB_Static.xmlProtocol_Last = this._xmlProtocol;
      Vedomost_VB_Static.xml_SborMainVed_Dump_Last = this.xml_SborMainVed_Dump;
      Vedomost_VB_Static.xml_SborVed_Dump_Last = this.xml_SborVed_Dump;
      Vedomost_VB_Static.imDocument = documentEditorForm2.Document;
      if (MessageBox.Show($"По результатам созданы протоколы и файлы DUMP.\r\n\r\nПапка\r\n{Vedomost_VB_Static.DirectoryDump}\r\n\r\nОткрыть эту папку?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      Process.Start(Vedomost_VB_Static.DirectoryDump);
    }
  }

  /// <summary>Сгенерировать (ЗАПОЛНИТЬ информацией) документ</summary>
  /// <param name="templateID">Идентификатор шаблона в БД</param>
  /// <returns></returns>
  public ImDocument GenerateImDocument(bool isRedraw, bool isConvert)
  {
    TableData tableData1 = (TableData) null;
    int num1 = 1;
    int num2 = 0;
    int n2 = 0;
    string nextPageTemplateID = "Следующая страница";
    this.txtProtocol_Add("Создание пока пустого документа");
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null)
      this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.Read_One_Ved_Nastr_byDocTemplateGuid(this._guidTemplateDoc, this._guidTypeDoc, Vedomost_VB.TypeDoc.Ved);
    string nodeId1 = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._tableName;
    if (this._groupForm != Vedomost_VB.FormaGroup.Ed && this._variables_Coordination == null)
      this._variables_Coordination = new Vedomost_VB.Variables_Coordination();
    Guid objectGUID;
    Vedomost_VB.AlgorithmToPrint algorithmToPrint;
    if (this._groupForm != Vedomost_VB.FormaGroup.B || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint_B == null)
    {
      objectGUID = this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid;
      algorithmToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint;
    }
    else
    {
      objectGUID = this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid_B;
      algorithmToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint_B;
      num1 = this._variables_Coordination.list_Captions.Count / algorithmToPrint._kolGraf;
      if (this._variables_Coordination.list_Captions.Count % algorithmToPrint._kolGraf > 0)
        ++num1;
      num2 = 0;
      n2 = this._variables_Coordination.list_Captions.Count;
      if (algorithmToPrint != null && algorithmToPrint._kolGraf < this._variables_Coordination.list_Captions.Count)
        n2 = algorithmToPrint._kolGraf;
      nodeId1 = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._tableName;
      this.Generate_GuidRecB();
    }
    long docObjectID = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
      if (dbObject == null)
      {
        int num3 = (int) MessageBox.Show("Файл шаблона (бланка) не найден", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (ImDocument) null;
      }
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_Format);
      if (attributeById != null)
        this._format = attributeById.AsString;
      docObjectID = dbObject.ObjectID;
    }
    ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(docObjectID);
    if (algorithmToPrint._isCheck)
      Vedomost_VB_Static.Checking_Template_And_Nastr(this._listError_OneError, algorithmToPrint, imDocument);
    ImDocument document = new ImDocument(imDocument, true, true);
    document.Name = this._one_Ved_Nastr_RazrabatyvaemoiVed._nameVed;
    this.docTemplate = document.DocumentTemplate as ImDocument;
    if (this.docTemplate.Nodes.Count > 2)
    {
      PageData firstNodeByName1 = (PageData) this.docTemplate.FindFirstNodeByName("Лист регистрации изменений");
    }
    this.txtProtocol_Add("Заполнение паспортных данных");
    if (!string.IsNullOrEmpty(this._designationDoc))
      document.SetAttributeValue("_designationDoc", this._designationDoc);
    if (!string.IsNullOrEmpty(this._nameArticle))
      document.SetAttributeValue("_nameArticle", this._nameArticle);
    if (!string.IsNullOrEmpty(this._designationArticle))
      document.SetAttributeValue("_designationArticle", this._designationArticle);
    if (!string.IsNullOrEmpty(this._kodDoc))
      document.SetAttributeValue("_kodDoc", this._kodDoc);
    if (!string.IsNullOrEmpty(this._nameTypeDoc))
      document.SetAttributeValue("_nameTypeDoc", this._nameTypeDoc);
    document.SetAttributeValue("_guidTypeDoc", this._one_Ved_Nastr_RazrabatyvaemoiVed._guidTypeVed.ToString());
    document.SetAttributeValue("guidTemplate", objectGUID.ToString());
    document.SetAttributeValue("_typeCreate", this._one_Ved_Nastr_RazrabatyvaemoiVed._typeCreate.ToString());
    document.SetAttributeValue("_typeVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed.ToString());
    document.SetAttributeValue("metodCreate", this._metodCreate);
    document.SetAttributeValue("metodFrom", this._metodFrom);
    document.SetAttributeValue("iDSP", this._iDSP.ToString());
    document.SetAttributeValue("_i_vybranogo_Ispolnenia", this._i_vybranogo_Ispolnenia.ToString());
    if (this._listAll_IspolneniySp_prodInfo != null && this._listAll_IspolneniySp_prodInfo.Count > this._i_vybranogo_Ispolnenia)
    {
      ProductInfo productInfo = this._listAll_IspolneniySp_prodInfo[this._i_vybranogo_Ispolnenia];
      if (productInfo != null)
      {
        document.SetAttributeValue("prodInfo_Id", productInfo.Id.ToString());
        document.SetAttributeValue("prodInfo_ObjectType", productInfo.ObjectType.ToString());
        document.SetAttributeValue("prodInfo_Designation", productInfo.Designation.ToString());
      }
    }
    document.SetAttributeValue("GroupForm", this._groupForm.ToString());
    if (this.List_For_Rebuilding_From_Attributes != null)
    {
      for (int index = 0; index < this.List_For_Rebuilding_From_Attributes.Count; ++index)
      {
        Vedomost_VB.One_Attribute rebuildingFromAttribute = this.List_For_Rebuilding_From_Attributes[index];
        if (this._metodCreate == "ChangeType")
        {
          if (rebuildingFromAttribute.name_Attribute == "metodCreate")
            rebuildingFromAttribute.text_Attribute = this._metodCreate;
          if (rebuildingFromAttribute.name_Attribute == "metodFrom")
            rebuildingFromAttribute.text_Attribute = this._metodFrom;
        }
        if (rebuildingFromAttribute.name_Attribute != "GroupForm" && rebuildingFromAttribute.name_Attribute != "guidTemplate")
          document.SetAttributeValue(rebuildingFromAttribute.name_Attribute, rebuildingFromAttribute.text_Attribute);
      }
    }
    if (this._isGroupVed && this._variables_Coordination != null && this._variables_Coordination.list_Variables.Count > 0)
      document.SetAttributeValue("VariablesCount", this._variables_Coordination.list_Variables.Count.ToString());
    if (this._isGroupVed && this._variables_Coordination != null && this._variables_Coordination.list_Variables != null)
    {
      for (int index = 0; index < this._variables_Coordination.list_Variables.Count; ++index)
      {
        string listVariable = this._variables_Coordination.list_Variables[index];
        string attributeName1 = "Variable_" + index.ToString();
        string attributeName2 = "Caption_" + index.ToString();
        string listCaption = this._variables_Coordination.list_Captions[index];
        document.SetAttributeValue(attributeName1, listVariable);
        document.SetAttributeValue(attributeName2, listCaption);
        if (this._listAll_IspolneniySp_prodInfo != null && this._listAll_IspolneniySp_prodInfo.Count == this._variables_Coordination.list_Variables.Count)
        {
          ProductInfo productInfo = this._listAll_IspolneniySp_prodInfo[index];
          if (productInfo != null)
          {
            document.SetAttributeValue("prodInfo_Id_" + index.ToString(), productInfo.Id.ToString());
            document.SetAttributeValue("prodInfo_ObjectType_" + index.ToString(), productInfo.ObjectType.ToString());
            document.SetAttributeValue("prodInfo_Designation_" + index.ToString(), productInfo.Designation.ToString());
          }
        }
      }
    }
    int index1 = 0;
    DocumentTreeNode node1 = this.docTemplate.Nodes[0];
    PageData firstChildNodeByName1 = this.docTemplate.FindFirstChildNodeByName("Следующая страница") as PageData;
    if ((node1.Name == "Титульная страница" || node1.Name == "Титульный лист") && this.docTemplate.NodesCount > 1)
    {
      node1 = this.docTemplate.Nodes[1];
      index1 = 1;
    }
    PageData currentPage_Template = (PageData) node1;
    TableData tableData2 = (TableData) null;
    if (this._listRecordsVed_New != null && this._listRecordsVed_New.Count > 0)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[0];
      string nodeName = "";
      if (recordForVedNew.Razdel_Ved > 0L)
        nodeName = Vedomost_VB_Static.Name_Page_for_Razdel(this._one_Ved_Nastr_RazrabatyvaemoiVed._list_RazdelsVed, recordForVedNew.Razdel_Ved);
      if (nodeName == "")
        nodeName = recordForVedNew.NamePage;
      if (nodeName != "" && nodeName != "Заглавный лист" && nodeName != currentPage_Template.Name && this.docTemplate.FindFirstChildNodeByName(nodeName) is PageData firstChildNodeByName2)
      {
        document.Nodes.RemoveAt(index1);
        currentPage_Template = firstChildNodeByName2;
        PageData pageData = (PageData) currentPage_Template.CloneFromTemplate(true, true);
        document.Nodes.Insert(index1, (DocumentTreeNode) pageData);
      }
      tableData2 = currentPage_Template.FindFirstNodeByName(algorithmToPrint._oneRecordToPrintEmpty._tableRowId) as TableData;
    }
    Vedomost_VB.OneRecordToPrint recordToPrintPasport = algorithmToPrint._oneRecordToPrintPasport;
    if (recordToPrintPasport != null)
    {
      for (int index2 = 0; index2 < recordToPrintPasport._listOneGrafaToPrint.Count; ++index2)
      {
        Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = recordToPrintPasport._listOneGrafaToPrint[index2];
        string cellId = oneGrafaToPrint._cell_ID;
        if (document.FindNode(cellId) is TextData node3)
        {
          string str1 = "";
          for (int index3 = 0; index3 < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index3)
          {
            Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index3];
            if (this._groupForm == Vedomost_VB.FormaGroup.B && dataFieldToPrint._typeFieldVedPasport == Vedomost_VB.TypeFieldVedPasport.GeneratedNumber && this._variables_Coordination.list_Captions != null)
            {
              for (int index4 = num2; index4 < n2; ++index4)
              {
                string listCaption = this._variables_Coordination.list_Captions[index4];
                string nodeId2 = "Исполнение " + index4.ToString();
                if (document.FindNode(nodeId2) is TextData node2)
                {
                  string str2 = listCaption;
                  if (str2 != "")
                    node2.AssignText(str2, false, true, true);
                }
              }
              str1 = "";
            }
            else
            {
              string typeFieldVedPasport = this.Get_Data_String_for_TypeFieldVedPasport(dataFieldToPrint._typeFieldVedPasport);
              if (typeFieldVedPasport != "")
              {
                if (str1 != "")
                  str1 += dataFieldToPrint._symbolRazd;
                str1 += typeFieldVedPasport;
              }
            }
          }
          if (str1 != "")
          {
            if (node3.Text != "")
              str1 = $"{node3.Text} {str1} -";
            node3.AssignText(str1, false, false, false);
          }
        }
      }
    }
    int num4 = 0;
    for (int index5 = 0; index5 < num1; ++index5)
    {
      PageData pageData1;
      if (index5 > 0)
      {
        num2 += algorithmToPrint._kolGraf;
        n2 += algorithmToPrint._kolGraf;
        if (n2 > this._variables_Coordination.list_Captions.Count)
          n2 = this._variables_Coordination.list_Captions.Count;
        currentPage_Template = firstChildNodeByName1;
        pageData1 = Vedomost_VB_Static.AddNewPageForFormB(document, nextPageTemplateID, num2);
        if (pageData1 != null)
          nodeId1 = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._tableName + " 2";
      }
      else
      {
        pageData1 = document.Nodes[0] as PageData;
        if ((pageData1.Name == "Титульная страница" || pageData1.Name == "Титульный лист") && this.docTemplate.NodesCount > 1)
        {
          currentPage_Template = (PageData) this.docTemplate.Nodes[1];
          PageData pageData2 = (PageData) currentPage_Template.CloneFromTemplate(true, true);
          document.Nodes.Insert(index1, (DocumentTreeNode) pageData2);
          pageData1 = document.Nodes[0] as PageData;
        }
      }
      if (string.IsNullOrEmpty(nodeId1))
      {
        int num5 = (int) MessageBox.Show("В настройке отсутствует имя Главной таблицы", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (ImDocument) null;
      }
      string attributeValue1 = index5.ToString();
      pageData1.SetAttributeValue("iCikl", attributeValue1);
      if (!(pageData1.FindFirstNodeByName("Главная таблица") is TableData mainTable))
        mainTable = pageData1.FindNode(nodeId1) as TableData;
      if (mainTable == null && string.IsNullOrEmpty(nodeId1))
      {
        int num6 = (int) MessageBox.Show($"На странице {currentPage_Template.Name} отсутствует имя Главной таблицы", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (ImDocument) null;
      }
      if (!(currentPage_Template.FindFirstNodeByName(algorithmToPrint._oneRecordToPrintEmpty._tableRowId) is TableData stroka_IzShablona_Empty))
        stroka_IzShablona_Empty = tableData2;
      this.txtProtocol_Add("Вывод записей");
      if (this._listRecordsVed_New != null)
      {
        bool flag = false;
        for (int index6 = num4; index6 < this._listRecordsVed_New.Count; ++index6)
        {
          Vedomost_VB.OneRecordToPrint oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
          tableData1 = (TableData) null;
          Vedomost_VB.RecordForVed_New recordForVed_New1 = this._listRecordsVed_New[index6];
          if (this._groupForm == Vedomost_VB.FormaGroup.B && this._isReDraw | isConvert)
          {
            if ((index6 != 0 || index5 != 0 || recordForVed_New1.TypeRec != Vedomost_VB.TypeRec.NewPage) && (index5 <= 0 || recordForVed_New1.TypeRec != Vedomost_VB.TypeRec.NewPage || recordForVed_New1._iCikl == index5))
            {
              if (index6 > 0 || index5 > 0)
              {
                if (recordForVed_New1.TypeRec == Vedomost_VB.TypeRec.NewPage && recordForVed_New1._iCikl != index5)
                {
                  num4 = index6 + 1;
                  break;
                }
                if (recordForVed_New1._iCikl != index5)
                  continue;
              }
            }
            else
              continue;
          }
          if (index6 > 0)
          {
            string nodeName = "";
            if (recordForVed_New1.Razdel_Ved > 0L)
              nodeName = Vedomost_VB_Static.Name_Page_for_Razdel(this._one_Ved_Nastr_RazrabatyvaemoiVed._list_RazdelsVed, recordForVed_New1.Razdel_Ved);
            if (nodeName == "")
              nodeName = recordForVed_New1.NamePage;
            if (nodeName != "" && nodeName != "Заглавный лист" && nodeName != currentPage_Template.Name && this.docTemplate.FindFirstChildNodeByName(nodeName) is PageData firstChildNodeByName3)
            {
              PageData pageData3 = firstChildNodeByName3;
              PageData pageData4 = (PageData) pageData3.CloneFromTemplate(true, true);
              if (pageData4.FindFirstNodeByName("Главная таблица") is TableData firstNodeByName2)
              {
                currentPage_Template = pageData3;
                mainTable = firstNodeByName2;
                if (!isRedraw || firstChildNodeByName3.Name != "Следующая страница")
                  document.Nodes.Add((DocumentTreeNode) pageData4);
                flag = true;
                stroka_IzShablona_Empty = currentPage_Template.FindFirstNodeByName(algorithmToPrint._oneRecordToPrintEmpty._tableRowId) as TableData;
              }
              else if (string.IsNullOrEmpty(nodeId1))
              {
                int num7 = (int) MessageBox.Show($"На странице {pageData3.Name} отсутствует имя Главной таблицы {nodeId1}", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              }
            }
            if (((!(nodeName == "") ? 0 : (currentPage_Template.Name != "" ? 1 : 0)) & (flag ? 1 : 0)) != 0)
            {
              if (!(this.docTemplate.FindFirstChildNodeByName("Заглавный лист") is PageData pageData5))
                pageData5 = firstChildNodeByName1;
              if (firstChildNodeByName1 != null)
              {
                PageData pageData6 = pageData5;
                PageData pageData7 = (PageData) firstChildNodeByName1.CloneFromTemplate(true, true);
                if (pageData7.FindFirstNodeByName("Главная таблица") is TableData firstNodeByName3)
                {
                  currentPage_Template = pageData6;
                  mainTable = firstNodeByName3;
                  document.Nodes.Add((DocumentTreeNode) pageData7);
                  flag = false;
                  stroka_IzShablona_Empty = currentPage_Template.FindFirstNodeByName(algorithmToPrint._oneRecordToPrintEmpty._tableRowId) as TableData;
                }
                else if (string.IsNullOrEmpty(nodeId1))
                {
                  int num8 = (int) MessageBox.Show($"На странице {pageData6.Name} отсутствует имя Главной таблицы {nodeId1}", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
              }
            }
          }
          TableData tableData3 = this.DocRow_Templation(recordForVed_New1, algorithmToPrint, currentPage_Template, out oneRecordToPrint);
          if (tableData3 != null && this.Filled_Record_DocRow_Ved(tableData3, recordForVed_New1, oneRecordToPrint, num2, n2, document, currentPage_Template))
          {
            if (this._groupForm == Vedomost_VB.FormaGroup.B && num1 > 1 && recordForVed_New1.TypeRec == Vedomost_VB.TypeRec.Title)
            {
              if (index6 < this._listRecordsVed_New.Count - 1)
              {
                Vedomost_VB.RecordForVed_New recordForVed_New2 = this._listRecordsVed_New[index6 + 1];
                if (recordForVed_New2.TypeRec == Vedomost_VB.TypeRec.Info)
                {
                  TableData docRow = this.DocRow_Templation(recordForVed_New2, algorithmToPrint, currentPage_Template, out oneRecordToPrint);
                  if (docRow == null || !this.Filled_Record_DocRow_Ved(docRow, recordForVed_New2, oneRecordToPrint, num2, n2, document, currentPage_Template))
                    continue;
                }
                else
                  continue;
              }
              else
                continue;
            }
            string from = "Stream";
            if (isRedraw)
              from = "Redraw";
            this.Filled_Record_DocRow_Ved_Attributes(tableData3, recordForVed_New1, from);
            if (recordForVed_New1.Guid_RecB != Guid.Empty)
              tableData3.SetAttributeValue("Guid_RecB", recordForVed_New1.Guid_RecB.ToString());
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._protection_From_Editing._isProhibition_DocRowWithObj)
            {
              string attributeValue2 = tableData3.GetAttributeValue("ObjectIdIzd", false);
              if (!string.IsNullOrEmpty(attributeValue2) && attributeValue2 != "0")
                Vedomost_VB_Static.DocRow_ReadOnly((DocumentTreeNode) tableData3, true, true);
            }
            mainTable.AddChildNode((DocumentTreeNode) tableData3, false, false);
            if (index6 < this._listRecordsVed_New.Count - 1)
            {
              Vedomost_VB.RecordForVed_New recordForVed_Next = this._listRecordsVed_New[index6 + 1];
              this.EmptyAdd(mainTable, recordForVed_New1, recordForVed_Next, algorithmToPrint, stroka_IzShablona_Empty);
            }
          }
        }
      }
    }
    if (isRedraw && this._isReDraw)
    {
      if (this._titListPage != null)
        document.InsertChildNode(0, this._titListPage, true, true, true, true, false);
      if (this._listLizmPages != null && this._listLizmPages.Count > 0)
      {
        for (int index7 = 0; index7 < this._listLizmPages.Count; ++index7)
        {
          DocumentTreeNode listLizmPage = this._listLizmPages[index7];
          document.AddChildNode(listLizmPage, true, true);
        }
      }
    }
    return document;
  }

  /// <summary> Создание ПОКА ПУСТОЙ записи по шаблону </summary>
  /// <param name="recordForVed_New"></param>
  /// <param name="algorithmToPrint"></param>
  /// <param name="currentPage_Template"></param>
  /// <returns></returns>
  public TableData DocRow_Templation(
    Vedomost_VB.RecordForVed_New recordForVed_New,
    Vedomost_VB.AlgorithmToPrint algorithmToPrint,
    PageData currentPage_Template,
    out Vedomost_VB.OneRecordToPrint oneRecordToPrint)
  {
    oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    if (recordForVed_New == null)
      return (TableData) null;
    TableData docRow = (TableData) null;
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Info || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Main)
    {
      Vedomost_VB.OneRecordToPrint printByRecordNew = this.OneRecordToPrint_By_RecordNew(algorithmToPrint, recordForVed_New);
      if (printByRecordNew != null && printByRecordNew._listOneGrafaToPrint != null && printByRecordNew._listOneGrafaToPrint.Count > 0)
      {
        oneRecordToPrint = printByRecordNew;
        if (!(currentPage_Template.FindFirstNodeByName(oneRecordToPrint._tableRowId.ToString()) is TableData tableData))
          tableData = currentPage_Template.FindNode(oneRecordToPrint._tableRowId.ToString()) as TableData;
        if (tableData == null)
        {
          Vedomost_VB_Static.ListError_Add(this._listError_OneError, " В шаблоне не найден тип строки: " + oneRecordToPrint._tableRowId.ToString());
          return (TableData) null;
        }
        docRow = tableData.CloneFromTemplate() as TableData;
        docRow.SetAttributeValue("TypeRow", recordForVed_New.TypeRec.ToString());
      }
    }
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Included && algorithmToPrint._oneRecordToPrintIncluded._listOneGrafaToPrint != null && algorithmToPrint._oneRecordToPrintIncluded._listOneGrafaToPrint.Count > 0)
    {
      oneRecordToPrint = algorithmToPrint._oneRecordToPrintIncluded;
      TableData tableData = currentPage_Template.FindFirstNodeByName(oneRecordToPrint._tableRowId.ToString()) as TableData;
      docRow = tableData.CloneFromTemplate() as TableData;
      if (tableData == null)
        tableData = currentPage_Template.FindNode(oneRecordToPrint._tableRowId.ToString()) as TableData;
      if (tableData == null)
      {
        Vedomost_VB_Static.ListError_Add(this._listError_OneError, " В шаблоне не найден тип строки: " + oneRecordToPrint._tableRowId.ToString());
        return (TableData) null;
      }
      this.Add_AttributeTypeRec_To_DocRow(docRow, "Included", "Stream");
      Vedomost_VB.OneRecordToPrint recordToPrintVtor = algorithmToPrint._oneRecordToPrintIncluded._oneRecordToPrint_Vtor;
      Vedomost_VB.OneRecordToPrint recordToPrintItogo = algorithmToPrint._oneRecordToPrintIncluded._oneRecordToPrint_Itogo;
      docRow.SetAttributeValue("TypeRow", recordForVed_New.TypeRec.ToString());
    }
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Title || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Title2 || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleIsp || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleIncluded || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleVar)
    {
      if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleIsp)
        oneRecordToPrint = algorithmToPrint._oneRecordToPrintTitleIsp;
      if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Title)
        oneRecordToPrint = algorithmToPrint._oneRecordToPrintTitle;
      if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Title2)
        oneRecordToPrint = algorithmToPrint._oneRecordToPrintTitlePodSection;
      if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleIncluded)
        oneRecordToPrint = algorithmToPrint._oneRecordToPrintTitleIncluded;
      if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.TitleVar)
        oneRecordToPrint = algorithmToPrint._oneRecordToPrintTitleVar;
      if (oneRecordToPrint == null)
        return (TableData) null;
      if (oneRecordToPrint._listOneGrafaToPrint == null || oneRecordToPrint._listOneGrafaToPrint.Count < 1)
        return (TableData) null;
      if (!(currentPage_Template.FindFirstNodeByName(oneRecordToPrint._tableRowId.ToString()) is TableData tableData))
        tableData = currentPage_Template.FindNode(oneRecordToPrint._tableRowId.ToString()) as TableData;
      if (tableData == null)
      {
        Vedomost_VB_Static.ListError_Add(this._listError_OneError, " В шаблоне не найден тип строки: " + oneRecordToPrint._tableRowId.ToString());
        return (TableData) null;
      }
      docRow = tableData.CloneFromTemplate() as TableData;
      docRow.SetAttributeValue("TypeRow", recordForVed_New.TypeRec.ToString());
    }
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Remark)
    {
      oneRecordToPrint = algorithmToPrint._oneRecordToPrintRemark;
      if (oneRecordToPrint == null)
        return (TableData) null;
      if (oneRecordToPrint._listOneGrafaToPrint == null || oneRecordToPrint._listOneGrafaToPrint.Count < 1)
        return (TableData) null;
      if (!(currentPage_Template.FindFirstNodeByName(oneRecordToPrint._tableRowId.ToString()) is TableData tableData))
        tableData = currentPage_Template.FindNode(oneRecordToPrint._tableRowId.ToString()) as TableData;
      if (tableData == null)
      {
        Vedomost_VB_Static.ListError_Add(this._listError_OneError, " В шаблоне не найден тип строки: " + oneRecordToPrint._tableRowId.ToString());
        return (TableData) null;
      }
      docRow = tableData.CloneFromTemplate() as TableData;
      docRow.SetAttributeValue("TypeRow", recordForVed_New.TypeRec.ToString());
    }
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.RemarkShort)
    {
      oneRecordToPrint = algorithmToPrint._oneRecordToPrintRemarkShort;
      if (oneRecordToPrint == null)
        return (TableData) null;
      if (oneRecordToPrint._listOneGrafaToPrint == null || oneRecordToPrint._listOneGrafaToPrint.Count < 1)
        return (TableData) null;
      if (!(currentPage_Template.FindFirstNodeByName(oneRecordToPrint._tableRowId.ToString()) is TableData tableData))
        tableData = currentPage_Template.FindNode(oneRecordToPrint._tableRowId.ToString()) as TableData;
      if (tableData == null)
      {
        Vedomost_VB_Static.ListError_Add(this._listError_OneError, " В шаблоне не найден тип строки: " + oneRecordToPrint._tableRowId.ToString());
        return (TableData) null;
      }
      docRow = tableData.CloneFromTemplate() as TableData;
      docRow.SetAttributeValue("TypeRow", recordForVed_New.TypeRec.ToString());
    }
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Empty)
    {
      oneRecordToPrint = algorithmToPrint._oneRecordToPrintEmpty;
      if (oneRecordToPrint == null)
        return (TableData) null;
      if (!(currentPage_Template.FindFirstNodeByName(oneRecordToPrint._tableRowId.ToString()) is TableData tableData))
        tableData = currentPage_Template.FindNode(oneRecordToPrint._tableRowId.ToString()) as TableData;
      if (tableData == null)
      {
        Vedomost_VB_Static.ListError_Add(this._listError_OneError, " В шаблоне не найден тип строки: " + oneRecordToPrint._tableRowId.ToString());
        return (TableData) null;
      }
      docRow = tableData.CloneFromTemplate() as TableData;
      docRow.SetAttributeValue("TypeRow", recordForVed_New.TypeRec.ToString());
    }
    return docRow;
  }

  /// <summary> Вставка пустых промежуточных строк </summary>
  /// <param name="mainTable"></param>
  /// <param name="recordForVed_New"></param>
  /// <param name="recordForVed_Next"></param>
  /// <param name="algorithmToPrint"></param>
  /// <param name="docTemplate"></param>
  public void EmptyAdd(
    TableData mainTable,
    Vedomost_VB.RecordForVed_New recordForVed_New,
    Vedomost_VB.RecordForVed_New recordForVed_Next,
    Vedomost_VB.AlgorithmToPrint algorithmToPrint,
    TableData stroka_IzShablona_Empty)
  {
    if (mainTable == null || recordForVed_New == null || stroka_IzShablona_Empty == null)
      return;
    if (algorithmToPrint._afterInfo == 0 && recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Info && (recordForVed_Next.TypeRec == Vedomost_VB.TypeRec.Title || recordForVed_Next.TypeRec == Vedomost_VB.TypeRec.Title2 || recordForVed_Next.TypeRec == Vedomost_VB.TypeRec.TitleIncluded || recordForVed_Next.TypeRec == Vedomost_VB.TypeRec.TitleIsp || recordForVed_Next.TypeRec == Vedomost_VB.TypeRec.TitlePart || recordForVed_Next.TypeRec == Vedomost_VB.TypeRec.TitleVar) && stroka_IzShablona_Empty.CloneFromTemplate() is TableData tableData1)
    {
      tableData1.SetAttributeValue("TypeRow", "Empty");
      if (!string.IsNullOrEmpty(recordForVed_New.Ispolnenie))
        tableData1.SetAttributeValue("Variable", recordForVed_New.Ispolnenie);
      this.Add_AttributeTypeRec_To_DocRow(tableData1, "Empty", "Stream");
      Guid guid = Guid.NewGuid();
      tableData1.SetAttributeValue("Guid_RecB", guid.ToString());
      mainTable.AddChildNode((DocumentTreeNode) tableData1, false, false);
    }
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Info || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Main || recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Included)
    {
      for (int index = 0; index < algorithmToPrint._afterInfo; ++index)
      {
        if (stroka_IzShablona_Empty.CloneFromTemplate() is TableData tableData2)
        {
          tableData2.SetAttributeValue("TypeRow", "Empty");
          if (!string.IsNullOrEmpty(recordForVed_New.Ispolnenie))
            tableData2.SetAttributeValue("Variable", recordForVed_New.Ispolnenie);
          this.Add_AttributeTypeRec_To_DocRow(tableData2, "Empty", "Stream");
          Guid guid = Guid.NewGuid();
          tableData2.SetAttributeValue("Guid_RecB", guid.ToString());
          mainTable.AddChildNode((DocumentTreeNode) tableData2, false, false);
        }
      }
    }
    else
    {
      if (recordForVed_New.TypeRec != Vedomost_VB.TypeRec.Remark)
        return;
      for (int index = 0; index < algorithmToPrint._afterRemark; ++index)
      {
        if (stroka_IzShablona_Empty.CloneFromTemplate() is TableData tableData3)
        {
          tableData3.SetAttributeValue("TypeRow", "Empty");
          if (!string.IsNullOrEmpty(recordForVed_New.Ispolnenie))
            tableData3.SetAttributeValue("Variable", recordForVed_New.Ispolnenie);
          this.Add_AttributeTypeRec_To_DocRow(tableData3, "Empty", "Stream");
          Guid guid = Guid.NewGuid();
          tableData3.SetAttributeValue("Guid_RecB", guid.ToString());
          mainTable.AddChildNode((DocumentTreeNode) tableData3, false, false);
        }
      }
    }
  }

  /// <summary> Привязать ведомость (ВЕРСИЮ) к изделию (и добавить её в СП) </summary>
  /// <param name="dbDocumentObjectVed"></param>
  public DBRelationsEventArgs AddVedToSp(IDBObject dbDocumentObjectVed)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> relationIDs = new List<long>();
      List<long> projIDs = new List<long>();
      List<int> intList1 = new List<int>();
      List<int> relTypeIDs = new List<int>();
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      List<int> intList2 = new List<int>();
      List<int> intList3 = new List<int>();
      if (this._isGroupVed && this._listAll_IspolneniySp_prodInfo != null && this._listAll_IspolneniySp_prodInfo.Count > 1)
      {
        for (int index = 0; index < this._listAll_IspolneniySp_prodInfo.Count; ++index)
        {
          long id = this._listAll_IspolneniySp_prodInfo[index].Id;
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
          if (sessionKeeper.Session.GetRelation(id, dbDocumentObjectVed.ObjectID, true) == null)
          {
            IDBRelation dbRelation = relationCollection.Create(id, dbDocumentObjectVed.ObjectID);
            relationIDs.Add(dbRelation.RelationID);
            projIDs.Add(dbRelation.ProjID);
            intList1.Add(sessionKeeper.Session.GetObjectInfo(dbRelation.ProjID).ObjectTypeID);
            relTypeIDs.Add(dbRelation.RelationType);
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedAddToRazdel == 1)
            {
              long sectionId = SpecificationSectionInfo.FindSectionById(SpecificationSectionInfo.ComplectSectionGuid).SectionID;
              dbRelation.SetAttributesValues(new AttributeValues[1]
              {
                new AttributeValues(AvsIDCache.Attr_SpecificationSection, (object) sectionId)
              });
            }
          }
        }
      }
      else
      {
        long id = this._prodInfo.Id;
        IDBObject dbObject = sessionKeeper.Session.GetObject(id, false);
        if (dbObject != null)
        {
          string[] descriptionsById = dbObject.GetDescriptionsByID(-7, false);
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
          IDBRelation dbRelation1 = sessionKeeper.Session.GetRelation(id, dbDocumentObjectVed.ObjectID, true);
          if (dbRelation1 == null && descriptionsById[0] != "Комплект" && relationCollection != null)
          {
            IDBRelation dbRelation2 = relationCollection.Create(id, dbDocumentObjectVed.ObjectID);
            relationIDs.Add(dbRelation2.RelationID);
            projIDs.Add(dbRelation2.ProjID);
            intList1.Add(sessionKeeper.Session.GetObjectInfo(dbRelation2.ProjID).ObjectTypeID);
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedAddToRazdel == 1)
            {
              long sectionId = SpecificationSectionInfo.FindSectionById(SpecificationSectionInfo.ComplectSectionGuid).SectionID;
              dbRelation2.SetAttributesValues(new AttributeValues[1]
              {
                new AttributeValues(AvsIDCache.Attr_SpecificationSection, (object) sectionId)
              });
            }
            relTypeIDs.Add(dbRelation2.RelationType);
            dbRelation1 = dbRelation2;
          }
          if (this._listAll_IspolneniySp_prodInfo != null && this._listAll_IspolneniySp_prodInfo.Count > 1 && dbRelation1 != null)
          {
            foreach (ProductInfo productInfo in this._listAll_IspolneniySp_prodInfo)
            {
              if (id != productInfo.Id)
              {
                IDBRelation relation = sessionKeeper.Session.GetRelation(productInfo.Id, dbDocumentObjectVed.ObjectID, true);
                if (relation != null)
                {
                  longList1.Add(relation.RelationID);
                  longList2.Add(relation.ProjID);
                  intList2.Add(sessionKeeper.Session.GetObjectInfo(relation.ProjID).ObjectTypeID);
                  intList3.Add(relation.RelationType);
                  relation.Delete(0L);
                }
              }
            }
          }
        }
      }
      return new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs);
    }
  }

  /// <summary> Заполнение записи в ДОКУМЕНТЕ </summary>
  /// <param name="docRow"></param>
  /// <param name="recordForVed_New"></param>
  /// <param name="oneRecordToPrint"></param>
  /// <param name="n1"></param>
  /// <param name="n2"></param>
  /// <param name="document"></param>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public bool Filled_Record_DocRow_Ved(
    TableData docRow,
    Vedomost_VB.RecordForVed_New recordForVed_New,
    Vedomost_VB.OneRecordToPrint oneRecordToPrint,
    int n1,
    int n2,
    ImDocument document,
    PageData currentPage_Template)
  {
    bool flag1 = false;
    string str1 = "";
    string str2 = "";
    string str3 = "";
    if (oneRecordToPrint == null || recordForVed_New == null || docRow == null)
      return false;
    if (recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Info)
    {
      str1 = recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_Designation);
      str2 = recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_Name);
      str3 = recordForVed_New.Get_Data_String_for_TypeFieldVedRec(Vedomost_VB.TypeFieldVedRec.DocumentTypeName);
    }
    recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_Format);
    recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_DerzPodl);
    recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_Listov);
    string stringForObjType1 = recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_Name);
    string stringForObjType2 = recordForVed_New.Get_Data_String_for_objType(AvsIDCache.Attr_Designation);
    stroka_KudaVhodit_Shablon = (TableData) null;
    TableData podTable_KudaVhodit_Shablon = (TableData) null;
    stroka_KudaVhodit_Itogo_Shablon = (TableData) null;
    TableData podTable_KudaVhodit_Itogo_Shablon = (TableData) null;
    Vedomost_VB.OneRecordToPrint oneRecordToPrint1 = (Vedomost_VB.OneRecordToPrint) null;
    Vedomost_VB.OneRecordToPrint oneRecordToPrint2 = (Vedomost_VB.OneRecordToPrint) null;
    if (oneRecordToPrint._listOneGrafaToPrint != null)
    {
      for (int index1 = 0; index1 < docRow.Nodes.Count; ++index1)
      {
        node = docRow.Nodes[index1] as TextData;
        int num1 = 0;
        if (node != null)
        {
          string str4 = node.ToString();
          Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
          bool flag2 = false;
          for (int index2 = 0; index2 < oneRecordToPrint._listOneGrafaToPrint.Count; ++index2)
          {
            oneGrafaToPrint = oneRecordToPrint._listOneGrafaToPrint[index2];
            string str5 = oneGrafaToPrint._cell_ID.ToString();
            if (str4.IndexOf(str5) > -1)
            {
              flag2 = true;
              break;
            }
          }
          if (flag2 && oneGrafaToPrint != null)
          {
            string str6 = "";
            string str7 = "";
            for (int index3 = 0; index3 < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index3)
            {
              Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index3];
              if (dataFieldToPrint._typeField == Vedomost_VB.TypeField.ObjectType)
              {
                str7 = recordForVed_New.Get_Data_String_for_objType(dataFieldToPrint._objectType);
                if (index3 == 0 && dataFieldToPrint._objectType == AvsIDCache.Attr_Name)
                  num1 = 1;
              }
              if (dataFieldToPrint._typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
              {
                if (dataFieldToPrint._typeFieldVedRec != Vedomost_VB.TypeFieldVedRec.Count_Summ || !oneRecordToPrint._isVtorOblast)
                {
                  str7 = recordForVed_New.Get_Data_String_for_TypeFieldVedRec(dataFieldToPrint._typeFieldVedRec);
                  if (str7 == "шт" && dataFieldToPrint._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.EdIzmKol)
                    str7 = "";
                  if (str7 == "Спецификация" && dataFieldToPrint._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.DocumentTypeName)
                  {
                    str7 = "";
                    str3 = "";
                  }
                  if (index3 == 1 && dataFieldToPrint._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.DocumentTypeName && num1 == 1)
                    num1 = 2;
                }
                if (this._groupForm == Vedomost_VB.FormaGroup.B && dataFieldToPrint._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Count_Summ && recordForVed_New.List_recordForVed_For_Isp != null)
                {
                  int num2 = 0;
                  bool flag3 = false;
                  int num3 = !this._isReDraw ? 0 : n1;
                  for (int index4 = n1; index4 < n2 && index4 - num3 < recordForVed_New.List_recordForVed_For_Isp.Count; ++index4)
                  {
                    str6 = "";
                    if (docRow.Nodes[index1 + num2] is TextData node)
                    {
                      Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp = recordForVed_New.List_recordForVed_For_Isp[index4 - num3];
                      str6 = !(recordForVedForIsp.Count_SummS == "") ? recordForVedForIsp.Count_SummS : recordForVedForIsp.Count_Summ.ToString();
                      if (str6 == "0")
                        str6 = "";
                      node.AssignText(str6, false, false, false);
                      if (str6 != "")
                        flag3 = true;
                    }
                    ++num2;
                  }
                  if (!flag3)
                    return false;
                  continue;
                }
              }
              if (!string.IsNullOrEmpty(str7))
                str6 = !(str6 != "") ? str7 : str6 + dataFieldToPrint._symbolRazd + str7;
              if (dataFieldToPrint._objectType == AvsIDCache.Attr_Razmery_I_Parametry && str6 == "")
                str6 = stringForObjType1;
              if (dataFieldToPrint._objectType == AvsIDCache.Attr_Gost && str6 == "")
                str6 = stringForObjType2;
            }
            if (num1 == 2 && oneGrafaToPrint._listOneDataFieldToPrint.Count == 2 && recordForVed_New.TypeRec == Vedomost_VB.TypeRec.Info && str1 != "" && str3 != null && str3 != "")
            {
              if (document != null)
                this._designationArticle = document.GetAttributeValue("_nameArticle", false);
              str6 = this._designationArticle != null && str1.StartsWith(this._designationArticle) || str2 == "" ? str3 : $"{str2}. {str3}";
            }
            if (str6 != "")
            {
              node.AssignText(str6, false, false, false);
              flag1 = true;
            }
          }
        }
      }
    }
    if (currentPage_Template != null && oneRecordToPrint._isVtorOblast && oneRecordToPrint._tableVtorOblastId != "0" && !string.IsNullOrWhiteSpace(oneRecordToPrint._tableVtorOblastId.ToString()) && oneRecordToPrint._listOneGrafaToPrint != null && oneRecordToPrint._listOneGrafaToPrint.Count > 0)
    {
      if (!(currentPage_Template.FindFirstNodeByName(oneRecordToPrint._oneRecordToPrint_Vtor._tableRowId.ToString()) is TableData stroka_KudaVhodit_Shablon))
        stroka_KudaVhodit_Shablon = currentPage_Template.FindNode(oneRecordToPrint._oneRecordToPrint_Vtor._tableRowId.ToString()) as TableData;
      if (stroka_KudaVhodit_Shablon != null)
      {
        podTable_KudaVhodit_Shablon = stroka_KudaVhodit_Shablon.ParentCell;
        oneRecordToPrint1 = oneRecordToPrint._oneRecordToPrint_Vtor;
      }
    }
    if (currentPage_Template != null && oneRecordToPrint._isVtorOblast && oneRecordToPrint._oneRecordToPrint_Itogo != null && oneRecordToPrint._oneRecordToPrint_Itogo._listOneGrafaToPrint != null && oneRecordToPrint._oneRecordToPrint_Itogo._listOneGrafaToPrint.Count > 0)
    {
      string tableRowId = oneRecordToPrint._oneRecordToPrint_Itogo._tableRowId;
      if (tableRowId != "" && !string.IsNullOrWhiteSpace(tableRowId))
      {
        if (!(currentPage_Template.FindFirstNodeByName(tableRowId) is TableData stroka_KudaVhodit_Itogo_Shablon))
          stroka_KudaVhodit_Itogo_Shablon = currentPage_Template.FindNode(tableRowId) as TableData;
        if (stroka_KudaVhodit_Shablon != null && stroka_KudaVhodit_Itogo_Shablon != null && stroka_KudaVhodit_Itogo_Shablon.ParentCell != null)
        {
          podTable_KudaVhodit_Itogo_Shablon = stroka_KudaVhodit_Itogo_Shablon.ParentCell;
          if (podTable_KudaVhodit_Itogo_Shablon != null)
            oneRecordToPrint2 = oneRecordToPrint._oneRecordToPrint_Itogo;
        }
      }
    }
    if (stroka_KudaVhodit_Shablon != null && oneRecordToPrint1 != null)
    {
      this.Filled_VtorRecords_Ved(docRow, podTable_KudaVhodit_Shablon, stroka_KudaVhodit_Shablon, recordForVed_New, oneRecordToPrint1);
      if (recordForVed_New.List_recordForVed_Vtor != null && recordForVed_New.List_recordForVed_Vtor.Count > 1 && (double) recordForVed_New.Count_Summ != 0.0)
        this.Filled_ItogoRecord_Ved(docRow, podTable_KudaVhodit_Itogo_Shablon, stroka_KudaVhodit_Itogo_Shablon, recordForVed_New, oneRecordToPrint2);
    }
    if (recordForVed_New.List_For_Rebuilding_From_Graf != null)
    {
      for (int index5 = 0; index5 < recordForVed_New.List_For_Rebuilding_From_Graf.Count; ++index5)
      {
        Vedomost_VB.One_Grafa oneGrafa = recordForVed_New.List_For_Rebuilding_From_Graf[index5];
        for (int index6 = 0; index6 < docRow.NodesCount; ++index6)
        {
          DocumentTreeNode node = docRow.Nodes[index6];
          if (node.NodeClass == "TextBoxElement")
          {
            TextData textData = (TextData) node;
            if (textData != null && textData.TemplateId == oneGrafa.templateId)
            {
              textData.AssignText(oneGrafa.text, false, false, false);
              flag1 = true;
              break;
            }
          }
        }
      }
    }
    if (recordForVed_New.List_For_Rebuilding_From_Attributes != null)
    {
      for (int index = 0; index < recordForVed_New.List_For_Rebuilding_From_Attributes.Count; ++index)
      {
        Vedomost_VB.One_Attribute rebuildingFromAttribute = recordForVed_New.List_For_Rebuilding_From_Attributes[index];
        docRow.SetAttributeValue(rebuildingFromAttribute.name_Attribute, rebuildingFromAttribute.text_Attribute);
        flag1 = true;
      }
    }
    if (recordForVed_New.FromNewPage)
      docRow.FromNewPage = true;
    return flag1;
  }

  /// <summary> Вывод ВТОРИЧНЫХ записей </summary>
  /// <param name="docRow"></param>
  /// <param name="podTable_KudaVhodit_Shablon"></param>
  /// <param name="stroka_KudaVhodit_Shablon"></param>
  /// <param name="recordForVed_New"></param>
  /// <param name="oneRecordToPrint"></param>
  public void Filled_VtorRecords_Ved(
    TableData docRow,
    TableData podTable_KudaVhodit_Shablon,
    TableData stroka_KudaVhodit_Shablon,
    Vedomost_VB.RecordForVed_New recordForVed_New,
    Vedomost_VB.OneRecordToPrint oneRecordToPrint)
  {
    if (oneRecordToPrint == null || recordForVed_New == null || docRow == null || stroka_KudaVhodit_Shablon == null || podTable_KudaVhodit_Shablon == null || recordForVed_New.List_recordForVed_Vtor == null || recordForVed_New.List_recordForVed_Vtor.Count < 1 || !(docRow.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) podTable_KudaVhodit_Shablon) is TableData templateRecursive))
      return;
    this.Add_AttributeTypeRec_To_DocRow(templateRecursive, "SubTable", "Stream");
    for (int index1 = 0; index1 < recordForVed_New.List_recordForVed_Vtor.Count; ++index1)
    {
      Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor = recordForVed_New.List_recordForVed_Vtor[index1];
      if (!(stroka_KudaVhodit_Shablon.CloneFromTemplate() is TableData tableData))
        break;
      foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(tableData))
      {
        if (textData != null)
        {
          string str1 = textData.ToString();
          bool flag = false;
          Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
          for (int index2 = 0; index2 < oneRecordToPrint._listOneGrafaToPrint.Count; ++index2)
          {
            oneGrafaToPrint = oneRecordToPrint._listOneGrafaToPrint[index2];
            string str2 = oneGrafaToPrint._cell_ID.ToString();
            if (str1.IndexOf(str2) > -1)
            {
              flag = true;
              break;
            }
          }
          if (flag && oneGrafaToPrint != null)
          {
            string str3 = "";
            string str4 = "";
            for (int index3 = 0; index3 < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index3)
            {
              Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index3];
              if (dataFieldToPrint._typeField != Vedomost_VB.TypeField.ObjectType)
                str4 = recordForVed_Vtor.Get_Data_String_for_TypeFieldVedRec(dataFieldToPrint._typeFieldVedRec);
              if (str4 != "")
                str3 = !(str3 != "") ? str4 : str3 + dataFieldToPrint._symbolRazd + str4;
            }
            if (str3 != "")
              textData.AssignText(str3, false, false, false);
          }
        }
      }
      if (recordForVed_Vtor.List_For_Rebuilding_From_Graf != null)
      {
        for (int index4 = 0; index4 < recordForVed_Vtor.List_For_Rebuilding_From_Graf.Count; ++index4)
        {
          Vedomost_VB.One_Grafa oneGrafa = recordForVed_Vtor.List_For_Rebuilding_From_Graf[index4];
          for (int index5 = 0; index5 < tableData.NodesCount; ++index5)
          {
            DocumentTreeNode node = tableData.Nodes[index5];
            if (node.NodeClass == "TextBoxElement")
            {
              TextData textData = (TextData) node;
              if (textData != null && textData.TemplateId == oneGrafa.templateId)
              {
                textData.AssignText(oneGrafa.text, false, false, false);
                break;
              }
            }
          }
        }
      }
      if (recordForVed_Vtor.List_For_Rebuilding_From_Attributes != null)
      {
        for (int index6 = 0; index6 < recordForVed_Vtor.List_For_Rebuilding_From_Attributes.Count; ++index6)
        {
          Vedomost_VB.One_Attribute rebuildingFromAttribute = recordForVed_Vtor.List_For_Rebuilding_From_Attributes[index6];
          tableData.SetAttributeValue(rebuildingFromAttribute.name_Attribute, rebuildingFromAttribute.text_Attribute);
        }
      }
      templateRecursive?.AddChildNode((DocumentTreeNode) tableData, false, false);
      this.Filled_Record_DocRow_VedVtor_Attributes(tableData, recordForVed_Vtor);
    }
  }

  /// <summary> Вывод записи ИТОГО </summary>
  /// <param name="docRow"> ЗАПИСЬ </param>
  /// <param name="internalRowParentTemplate"> Подтаблица Куда входит </param>
  /// <param name="internalRowTemplate"> Строка Кол итого </param>
  /// <param name="recordForVed_New"></param>
  /// <param name="oneRecordToPrint"></param>
  /// podTable_KudaVhodit_Itogo_Shablon, stroka_KudaVhodit_Itogo_Shablon
  public void Filled_ItogoRecord_Ved(
    TableData docRow,
    TableData podTable_KudaVhodit_Itogo_Shablon,
    TableData stroka_KudaVhodit_Itogo_Shablon,
    Vedomost_VB.RecordForVed_New recordForVed_New,
    Vedomost_VB.OneRecordToPrint oneRecordToPrint)
  {
    if (oneRecordToPrint == null || recordForVed_New == null || docRow == null || stroka_KudaVhodit_Itogo_Shablon == null || podTable_KudaVhodit_Itogo_Shablon == null || recordForVed_New.List_recordForVed_Vtor == null || recordForVed_New.List_recordForVed_Vtor.Count < 2)
      return;
    TableData templateRecursive = docRow.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) podTable_KudaVhodit_Itogo_Shablon) as TableData;
    if (!(stroka_KudaVhodit_Itogo_Shablon.CloneFromTemplate() is TableData tableData))
      return;
    foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(tableData))
    {
      if (textData != null)
      {
        string str1 = textData.ToString();
        bool flag = false;
        Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
        for (int index = 0; index < oneRecordToPrint._listOneGrafaToPrint.Count; ++index)
        {
          oneGrafaToPrint = oneRecordToPrint._listOneGrafaToPrint[index];
          string str2 = oneGrafaToPrint._cell_ID.ToString();
          if (str1.IndexOf(str2) > -1)
          {
            flag = true;
            break;
          }
        }
        if (flag && oneGrafaToPrint != null)
        {
          string str3 = "";
          string str4 = "";
          for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
          {
            Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index];
            if (dataFieldToPrint._typeField != Vedomost_VB.TypeField.ObjectType)
              str4 = recordForVed_New.Get_Data_String_for_TypeFieldVedRec(dataFieldToPrint._typeFieldVedRec);
            if (str4 != "")
              str3 = !(str3 != "") ? str4 : str3 + dataFieldToPrint._symbolRazd + str4;
          }
          if (str3 != "")
            textData.AssignText(str3, false, false, false);
        }
      }
    }
    if (templateRecursive != null)
    {
      this.Add_AttributeTypeRec_To_DocRow(tableData, "SubRow", "Stream");
      templateRecursive.AddChildNode((DocumentTreeNode) tableData, false, false);
    }
    if (recordForVed_New.Record_For_Rebuilding_Itogo != null && recordForVed_New.Record_For_Rebuilding_Itogo.List_For_Rebuilding_From_Graf != null)
    {
      for (int index = 0; index < recordForVed_New.Record_For_Rebuilding_Itogo.List_For_Rebuilding_From_Graf.Count; ++index)
      {
        Vedomost_VB.One_Grafa oneGrafa = recordForVed_New.Record_For_Rebuilding_Itogo.List_For_Rebuilding_From_Graf[index];
        ((TextData) tableData.FindFirstNodeByName(oneGrafa.templateId))?.AssignText(oneGrafa.text, false, false, false);
      }
    }
    this.Filled_Record_DocRow_VedItogo_Attributes(tableData, recordForVed_New);
  }

  /// <summary> Сохранение ВСЕХ ПЕРВИЧНЫХ данных из recordForVed_New (и привязывается к строке таблицы)</summary>
  /// <param name="docRow"></param>
  /// <param name="recordForVed_New"></param>
  /// <param from="recordForVed_New">Какой командой заполнялась запись</param>
  /// <returns></returns>
  public bool Filled_Record_DocRow_Ved_Attributes(
    TableData docRow,
    Vedomost_VB.RecordForVed_New recordForVed_New,
    string from)
  {
    if (docRow == null || recordForVed_New == null)
      return false;
    string attributeValue1 = recordForVed_New.TypeRec.ToString();
    docRow.SetAttributeValue("TypeRec", attributeValue1);
    if (from != null && from != "")
      docRow.SetAttributeValue("From", from);
    long objectId = recordForVed_New.Get_ObjectID();
    docRow.SetAttributeValue("ObjectIdIzd", objectId.ToString());
    if (!string.IsNullOrEmpty(recordForVed_New.Count_in_Sp_S))
      docRow.SetAttributeValue("Count_in_Sp_S", recordForVed_New.Count_in_Sp_S);
    if ((double) Math.Abs(recordForVed_New.Count_in_Sp) > 0.0)
      docRow.SetAttributeValue("Count_in_Sp", recordForVed_New.Count_in_Sp.ToString());
    if ((double) Math.Abs(recordForVed_New.Count_in_Izdelie) > 0.0)
      docRow.SetAttributeValue("Count_in_Izdelie", recordForVed_New.Count_in_Izdelie.ToString());
    if (!string.IsNullOrEmpty(recordForVed_New.Count_in_SpKompl_S))
      docRow.SetAttributeValue("Count_in_SpKompl_S", recordForVed_New.Count_in_SpKompl_S);
    if ((double) Math.Abs(recordForVed_New.Count_in_SpKompl) > 0.0)
      docRow.SetAttributeValue("Count_in_SpKompl", recordForVed_New.Count_in_SpKompl.ToString());
    if (!string.IsNullOrEmpty(recordForVed_New.Count_in_SpRegulir_S))
      docRow.SetAttributeValue("Count_in_SpRegulir_S", recordForVed_New.Count_in_SpRegulir_S);
    if ((double) Math.Abs(recordForVed_New.Count_in_SpRegulir) > 0.0)
      docRow.SetAttributeValue("Count_in_SpRegulir", recordForVed_New.Count_in_SpRegulir.ToString());
    if ((double) Math.Abs(recordForVed_New.CountF_samOi_sp) > 0.0)
      docRow.SetAttributeValue("CountF_samOi_sp", recordForVed_New.CountF_samOi_sp.ToString());
    if ((double) Math.Abs(recordForVed_New.Count_Vsego) > 0.0)
      docRow.SetAttributeValue("Count_Vsego", recordForVed_New.Count_Vsego.ToString());
    if ((double) Math.Abs(recordForVed_New.Count_Summ) > 0.0)
      docRow.SetAttributeValue("Count_Summ", recordForVed_New.Count_Summ.ToString());
    if (!string.IsNullOrEmpty(recordForVed_New.EdIzmKol))
      docRow.SetAttributeValue("EdIzmKol", recordForVed_New.EdIzmKol);
    if (!string.IsNullOrEmpty(recordForVed_New.Remark))
      docRow.SetAttributeValue("Remark", recordForVed_New.Remark);
    if (recordForVed_New.Razdel_Ved != 0L)
      docRow.SetAttributeValue("Razdel_Ved", recordForVed_New.Razdel_Ved.ToString());
    if (!string.IsNullOrEmpty(recordForVed_New.UrovenS))
      docRow.SetAttributeValue("UrovenS", recordForVed_New.UrovenS);
    if (!string.IsNullOrEmpty(recordForVed_New.Ispolnenie))
      docRow.SetAttributeValue("Variable", recordForVed_New.Ispolnenie);
    for (int index = 0; index < recordForVed_New.List_OneDataVed.Count; ++index)
    {
      Vedomost_VB.OneDataVed oneDataVed = recordForVed_New.List_OneDataVed[index];
      if (oneDataVed.AttributeSourceTypes == AttributeSourceTypes.Object)
      {
        string stringForObjType = recordForVed_New.Get_Data_String_for_objType(oneDataVed.ObjectType);
        if (!string.IsNullOrEmpty(stringForObjType))
        {
          string attributeName = "OneDataVed" + index.ToString();
          string attributeValue2 = $"{oneDataVed.ObjectType.ToString()}={stringForObjType}";
          docRow.SetAttributeValue(attributeName, attributeValue2);
        }
      }
    }
    if (recordForVed_New.List_recordForVed_Vtor != null && recordForVed_New.List_recordForVed_Vtor.Count > 0)
    {
      string attributeValue3 = recordForVed_New.List_recordForVed_Vtor.Count.ToString();
      docRow.SetAttributeValue("nVtorS", attributeValue3);
    }
    return true;
  }

  /// <summary> Сохранение ВСЕХ вторичных данных из RecordForVed_Vtor (и привязывается к строке таблицы) </summary>
  /// <param name="stroka_KudaVhodit"></param>
  /// <param name="recordForVed_Vtor"></param>
  /// <returns></returns>
  public bool Filled_Record_DocRow_VedVtor_Attributes(
    TableData stroka_KudaVhodit,
    Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor)
  {
    if (stroka_KudaVhodit == null || recordForVed_Vtor == null)
      return false;
    if (recordForVed_Vtor.KudaDesignation != "")
      stroka_KudaVhodit.SetAttributeValue("KudaDesignation", recordForVed_Vtor.KudaDesignation);
    long kudaObjectId = recordForVed_Vtor.KudaObjectId;
    stroka_KudaVhodit.SetAttributeValue("ObjectIdIzd", kudaObjectId.ToString());
    if (recordForVed_Vtor.Count_in_Sp_S != "")
      stroka_KudaVhodit.SetAttributeValue("Count_in_Sp_S", recordForVed_Vtor.Count_in_Sp_S);
    if ((double) Math.Abs(recordForVed_Vtor.Count_in_Sp) > 0.0)
      stroka_KudaVhodit.SetAttributeValue("Count_in_Sp", recordForVed_Vtor.Count_in_Sp.ToString());
    if ((double) Math.Abs(recordForVed_Vtor.Count_in_Izdelie) > 0.0)
      stroka_KudaVhodit.SetAttributeValue("Count_in_Izdelie", recordForVed_Vtor.Count_in_Izdelie.ToString());
    if (recordForVed_Vtor.Count_in_SpKompl_S != "")
      stroka_KudaVhodit.SetAttributeValue("Count_in_SpKompl_S", recordForVed_Vtor.Count_in_SpKompl_S);
    if ((double) Math.Abs(recordForVed_Vtor.Count_in_SpKompl) > 0.0)
      stroka_KudaVhodit.SetAttributeValue("Count_in_SpKompl", recordForVed_Vtor.Count_in_SpKompl.ToString());
    if (recordForVed_Vtor.Count_in_SpRegulir_S != "")
      stroka_KudaVhodit.SetAttributeValue("Count_in_SpRegulir_S", recordForVed_Vtor.Count_in_SpRegulir_S);
    if ((double) Math.Abs(recordForVed_Vtor.Count_in_SpRegulir) > 0.0)
      stroka_KudaVhodit.SetAttributeValue("Count_in_SpRegulir", recordForVed_Vtor.Count_in_SpRegulir.ToString());
    if ((double) Math.Abs(recordForVed_Vtor.CountF_samOi_sp) > 0.0)
      stroka_KudaVhodit.SetAttributeValue("CountF_samOi_sp", recordForVed_Vtor.CountF_samOi_sp.ToString());
    if ((double) Math.Abs(recordForVed_Vtor.Count_Vsego) > 0.0)
      stroka_KudaVhodit.SetAttributeValue("Count_Vsego", recordForVed_Vtor.Count_Vsego.ToString());
    for (int index = 0; index < recordForVed_Vtor.List_OneDataVed.Count; ++index)
    {
      Vedomost_VB.OneDataVed oneDataVed = recordForVed_Vtor.List_OneDataVed[index];
      if (oneDataVed.AttributeSourceTypes == AttributeSourceTypes.Object)
      {
        string stringForObjType = recordForVed_Vtor.Get_Data_String_for_objType(oneDataVed.ObjectType);
        if (string.IsNullOrEmpty(stringForObjType))
        {
          string attributeName = "OneDataVed" + index.ToString();
          string attributeValue = $"{oneDataVed.ObjectType.ToString()}={stringForObjType}";
          stroka_KudaVhodit.SetAttributeValue(attributeName, attributeValue);
        }
      }
    }
    return true;
  }

  /// <summary> Сохранение данных ИТОГО из RecordForVed_Vtor (и привязывается к строке таблицы) </summary>
  /// <param name="docRow"></param>
  /// <param name="recordForVed_New"></param>
  /// <returns></returns>
  public bool Filled_Record_DocRow_VedItogo_Attributes(
    TableData docRow,
    Vedomost_VB.RecordForVed_New recordForVed_New)
  {
    if (docRow == null || recordForVed_New == null)
      return false;
    this.Add_AttributeTypeRec_To_DocRow(docRow, "Total", "Stream");
    return true;
  }

  /// <summary> Заполнение атрибута типа записи при ручном добавлении записи /// </summary>
  /// <param name="docRow"></param>
  /// <param name="typeRec"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  public bool Add_AttributeTypeRec_To_DocRow(TableData docRow, string typeRec, string from)
  {
    if (docRow == null)
      return false;
    if (!string.IsNullOrEmpty(typeRec))
      docRow.SetAttributeValue("TypeRec", typeRec);
    if (!string.IsNullOrEmpty(typeRec))
      docRow.SetAttributeValue("From", from);
    return true;
  }

  public void ReDrawing()
  {
  }

  /// <summary> Настройки вывода для определенного раздела </summary>
  /// <param name="algorithmToPrint"></param>
  /// <param name="razdel_Ved"></param>
  /// <returns></returns>
  public Vedomost_VB.OneRecordToPrint OneRecordToPrint_By_RecordNew(
    Vedomost_VB.AlgorithmToPrint algorithmToPrint,
    int razdel_Ved_Int)
  {
    if (algorithmToPrint == null)
      return (Vedomost_VB.OneRecordToPrint) null;
    Vedomost_VB.OneRecordToPrint printByRecordNew = (Vedomost_VB.OneRecordToPrint) null;
    if (razdel_Ved_Int < 1)
      return this.OneRecordToPrint_By_RecordNew0(algorithmToPrint);
    if (algorithmToPrint._oneRecordToPrint_Info != null)
      printByRecordNew = algorithmToPrint._oneRecordToPrint_Info;
    else if (algorithmToPrint._list_OneRazdelToPrint != null && algorithmToPrint._list_OneRazdelToPrint.Count > 0)
    {
      Vedomost_VB.OneRecordToPrint recordToPrintInfo = algorithmToPrint._list_OneRazdelToPrint[0]._oneRecordToPrint_Info;
      for (int index = 0; index < algorithmToPrint._list_OneRazdelToPrint.Count; ++index)
      {
        Vedomost_VB.OneRazdelToPrint oneRazdelToPrint = algorithmToPrint._list_OneRazdelToPrint[index];
        if (oneRazdelToPrint._razdelVed == razdel_Ved_Int)
        {
          printByRecordNew = oneRazdelToPrint._oneRecordToPrint_Info;
          break;
        }
      }
      if (printByRecordNew == null)
        printByRecordNew = recordToPrintInfo;
    }
    return printByRecordNew;
  }

  /// <summary> Настройки вывода для НЕОПРЕДЕЛЕННОГО раздела  </summary>
  /// <param name="algorithmToPrint"></param>
  /// <returns></returns>
  public Vedomost_VB.OneRecordToPrint OneRecordToPrint_By_RecordNew0(
    Vedomost_VB.AlgorithmToPrint algorithmToPrint)
  {
    if (algorithmToPrint == null)
      return (Vedomost_VB.OneRecordToPrint) null;
    Vedomost_VB.OneRecordToPrint printByRecordNew0 = (Vedomost_VB.OneRecordToPrint) null;
    if (algorithmToPrint._oneRecordToPrint_Info != null)
      printByRecordNew0 = algorithmToPrint._oneRecordToPrint_Info;
    else if (algorithmToPrint._list_OneRazdelToPrint != null && algorithmToPrint._list_OneRazdelToPrint.Count > 0)
      printByRecordNew0 = algorithmToPrint._list_OneRazdelToPrint[0]._oneRecordToPrint_Info;
    return printByRecordNew0;
  }

  /// <summary> Настройки вывода для определенного раздела </summary>
  /// <param name="algorithmToPrint"></param>
  /// <param name="razdel_Ved_Str"></param>
  /// <returns></returns>
  public Vedomost_VB.OneRecordToPrint OneRecordToPrint_By_RecordNew(
    Vedomost_VB.AlgorithmToPrint algorithmToPrint,
    string razdel_Ved_Str)
  {
    if (algorithmToPrint == null)
      return (Vedomost_VB.OneRecordToPrint) null;
    if (string.IsNullOrEmpty(razdel_Ved_Str))
      return this.OneRecordToPrint_By_RecordNew0(algorithmToPrint);
    int int32 = Convert.ToInt32(razdel_Ved_Str);
    return this.OneRecordToPrint_By_RecordNew(algorithmToPrint, int32);
  }

  /// <summary> Настройки вывода для определенного раздела </summary>
  /// <param name="algorithmToPrint"></param>
  /// <param name="recordForVed_New"></param>
  /// <returns></returns>
  public Vedomost_VB.OneRecordToPrint OneRecordToPrint_By_RecordNew(
    Vedomost_VB.AlgorithmToPrint algorithmToPrint,
    Vedomost_VB.RecordForVed_New recordForVed_New)
  {
    if (algorithmToPrint == null)
      return (Vedomost_VB.OneRecordToPrint) null;
    return recordForVed_New == null || recordForVed_New.Razdel_Ved < 1L ? this.OneRecordToPrint_By_RecordNew0(algorithmToPrint) : this.OneRecordToPrint_By_RecordNew(algorithmToPrint, (int) recordForVed_New.Razdel_Ved);
  }

  /// <summary>Создание GUIDов записей (RecordNew) (для формы Б)</summary>
  public void Generate_GuidRecB()
  {
    if (this._listRecordsVed_New == null)
      return;
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
      if (recordForVedNew.Guid_RecB == Guid.Empty)
        recordForVedNew.Guid_RecB = Guid.NewGuid();
    }
  }

  /// <summary>/// Создание ведомости шаг 1. Из навигатора
  /// objectIdMainSP это objectId Головной СПЕЦИФИКАЦИИ (документа)
  /// </summary>
  /// <param name="ObjectIdMainSP"></param>
  public bool CreateVedomost(long ObjectIdMainSP, bool isReCreate = false)
  {
    if (ObjectIdMainSP == 0L)
    {
      int num = (int) MessageBox.Show("Головная спецификация не доступна\r\nРекомендуем создать ведомость непосредственно из спецификации", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    this._objectIdMainSP = ObjectIdMainSP;
    return this.For_CreateVedomost() && this.CreateVedomostCommon(isReCreate);
  }

  public bool Create_General_Ved(long ObjectIdMainSP)
  {
    if (ObjectIdMainSP == 0L)
      return false;
    this._objectIdMainSP = ObjectIdMainSP;
    this.txtProtocol_Add("General Входим в For_CreateVedomost");
    if (!this.For_CreateVedomost())
    {
      this.txtProtocol_Add("General Выполнена НЕ успешно For_CreateVedomost");
      return false;
    }
    this.txtProtocol_Add("General Выполнена успешно For_CreateVedomost");
    this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.General_Nastr_Init();
    this.ListCommonId_Filled(this.listCommonId);
    this._imsObjectType_RazrabatyvaemoiVed = Vedomost_VB_Static.Get_IMSObjectType_ByTemplateGuid(Vedomost_VB_Static.GuidTemplateRS, Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr);
    if (this._listAll_IspolneniySp_prodInfo.Count == 1)
      this._isGroupVed = false;
    this.txtProtocol_Add("General Перед ProcessingMainVed_Step1_PostroenieDerevat");
    if (!this.ProcessingMainVed_Step1_PostroenieDereva(false))
    {
      this.txtProtocol_Add("General ProcessingMainVed_Step1_PostroenieDerevat НЕ УСПЕШНО");
      return false;
    }
    this.txtProtocol_Add("ProcessingMainVed_Step1_PostroenieDerevat УСПЕШНО");
    this.txtProtocol_Add("General Перед _listRecordsForMainVed.Sort");
    this._listRecordsForMainVed.Sort((IComparer<Vedomost_VB.RecordForMainVed>) this._compareRecordsForMainVed_byDesignation);
    this.txtProtocol_Add("General Перед ProcessingMainVed_Step2_SummOdinakovyh");
    this.ProcessingMainVed_Step2_SummOdinakovyh(this._listRecordsForMainVed);
    this.txtProtocol_Add("General Перед ProcessingMainVed_Step3_MainIspToKuda");
    this.ProcessingMainVed_Step3_MainIspToKuda(this._listRecordsForMainVed);
    this.txtProtocol_Add("General Перед _listRecordsForMainVed.Sort");
    this._listRecordsForMainVed.Sort((IComparer<Vedomost_VB.RecordForMainVed>) this._compareRecordsForMainVed_byDesignation4);
    this.txtProtocol_Add("General Перед ProcessingMainVed_Step5_CreateVtorRecords");
    this.ProcessingMainVed_Step5_CreateVtorRecords(this._listRecordsForMainVed);
    this.txtProtocol_Add("General Перед ProcessingMainVed_Step6_Summ");
    this.ProcessingMainVed_Step6_Summ(this._listRecordsForMainVed);
    this._listRecordsVed_New = new List<Vedomost_VB.RecordForVed_New>();
    this.txtProtocol_Add("General Перед CreateVed_Step1_Sbor");
    this.CreateVed_Step1_Sbor(this._listRecordsForMainVed);
    if (this._isGroupVed && this._variables_Coordination.list_Variables.Count > 1)
    {
      this.txtProtocol_Add("General Перед _listRecordsVed_New.Sort");
      this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this._compareRecordsVed_step0);
      this.txtProtocol_Add("General Перед Merger_Ved_ispolneniy");
      this.Merger_Ved_ispolneniy();
    }
    this.txtProtocol_Add("General Перед Addition_FuncGroup");
    this.Addition_FuncGroup();
    this.txtProtocol_Add("General Перед _listRecordsVed_New.Sort");
    this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this);
    this.txtProtocol_Add("General Перед Union_Records_Ved_1");
    this.Union_Records_Ved_1();
    this.txtProtocol_Add("General Перед Extrection_Ved_Vtor1");
    this.Extrection_Ved_Vtor1();
    this.txtProtocol_Add("General Перед Merger_Ved_Vtor");
    this.Merger_Ved_Vtor();
    this.txtProtocol_Add("General Перед Sort_Ved_Vtor");
    this.Sort_Ved_Vtor();
    this.txtProtocol_Add("General Перед Summ_VedVtor");
    this.Summ_VedVtor();
    this.txtProtocol_Add("General После Summ_VedVtor");
    return true;
  }

  public bool For_CreateVedomost()
  {
    this._listAll_IspolneniySp_prodInfo = new List<ProductInfo>();
    bool flag = false;
    ProductInfo productInfo = (ProductInfo) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
      relationCollection.ObjectTypeID = AvsIDCache.ObjType_Product;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) AvsIDCache.Attr_ArticleGroupID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0),
        new ColumnDescriptor((object) AvsIDCache.Attr_Name, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      paramSet.LastKeyValue = 0L;
      paramSet.LastOrderValue = (object) null;
      try
      {
        if (sessionKeeper.Session.GetObject(this._objectIdMainSP, false) == null)
        {
          int num = (int) MessageBox.Show("Головная спецификация не доступна\r\nРекомендуем создать ведомость непосредственно из спецификации", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Головная спецификация не доступна\r\nРекомендуем создать ведомость непосредственно из спецификации", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      DataTable dataTable = (DataTable) null;
      while (!flag)
      {
        dataTable = relationCollection.EntersInVersion(paramSet, this._objectIdMainSP);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          productInfo = new ProductInfo();
          productInfo.Id = Convert.ToInt64(row[0]);
          if (row[1] != DBNull.Value)
          {
            productInfo.ArticleGroupID = new Guid(Convert.ToString(row[1]));
            this._articleGroupID = new Guid(Convert.ToString(row[1]));
          }
          productInfo.ObjectType = Convert.ToInt32(row[2]);
          productInfo.Designation = Convert.ToString(row[3]);
          productInfo.Name = Convert.ToString(row[4]);
          this._listAll_IspolneniySp_prodInfo.Add(productInfo);
        }
        flag = Convert.ToBoolean(dataTable.ExtendedProperties[(object) "Eof"]);
        if (!flag && dataTable.Rows.Count > 0 && productInfo != null)
        {
          paramSet.LastKeyValue = productInfo.Id;
          paramSet.LastOrderValue = (object) productInfo.Designation;
        }
      }
      if (flag)
      {
        if (dataTable.Rows.Count == 0)
        {
          int num1 = (int) MessageBox.Show("Головная спецификация не доступна\r\nРекомендуем создать ведомость непосредственно из спецификации", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }
    if (this._listAll_IspolneniySp_prodInfo != null && this._listAll_IspolneniySp_prodInfo.Count >= 1)
      return true;
    this.txtProtocol_Add("В For_CreateVedomost _listAll_IspolneniySp_prodInfo пустая");
    return false;
  }

  /// <summary>/// Создание ведомости шаг 1. Из ТЕКУЩЕЙ спецификации</summary>
  /// <param name="specificationMainCurr"></param>
  public bool CreateVedomost(AVSDocument specificationMainCurr)
  {
    if (specificationMainCurr == null)
      return false;
    if (specificationMainCurr.AVSDocType != AVSDocumentType.Specification)
    {
      int num = (int) MessageBox.Show("Текущий документ не спецификация\r\nСоздание ведомости невозможно", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    this._specificationMain = specificationMainCurr;
    this._listAll_IspolneniySp_prodInfo = new List<ProductInfo>();
    for (int index = 0; index < this._specificationMain.productsInfo.Count; ++index)
    {
      ProductInfo productInfo = this._specificationMain.productsInfo[index];
      this._listAll_IspolneniySp_prodInfo.Add(productInfo);
      if (index == 0)
        this._articleGroupID = productInfo.ArticleGroupID;
    }
    if (this._listAll_IspolneniySp_prodInfo.Count == 0)
    {
      int num = (int) MessageBox.Show("Спецификация не связана с изделиями\r\nСоздание ведомости невозможно", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    return this.CreateVedomostCommon();
  }

  /// <summary>Создание ведомости. Шаг 2. Общий. Для спецификации или изделия</summary>
  /// <param name="_listAll_IspolneniySp"> Список типа ProductInfo </param>
  public bool CreateVedomostCommon(bool isReCreate = false)
  {
    if (!isReCreate)
    {
      if (this.DesignationArticle == null)
      {
        if (this._listAll_IspolneniySp_prodInfo == null || this._listAll_IspolneniySp_prodInfo.Count < 1)
        {
          int num = (int) MessageBox.Show("Спецификация не связана с изделиями\r\nСоздание ведомости невозможно", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
        this.DesignationArticle = this._listAll_IspolneniySp_prodInfo[0].Designation;
      }
      if (!this.VyborVedomosti())
        return false;
    }
    else if (!this.VyborVedomosti_For_Recreate())
      return false;
    if (!isReCreate && this._listAll_IspolneniySp_prodInfo.Count == 1)
      this._isGroupVed = false;
    if (this.DesignationArticle == null)
      this.DesignationArticle = this._listAll_IspolneniySp_prodInfo[0].Designation;
    if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
    {
      this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.Read_One_Ved_Nastr_byDocTypeGuid_From_Conformity(this._imsObjectType_RazrabatyvaemoiVed.Guid);
      this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType = this._imsObjectType_RazrabatyvaemoiVed;
    }
    else
      this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.Read_One_Ved_Nastr(this._imsObjectType_RazrabatyvaemoiVed, Vedomost_VB.TypeDoc.Ved);
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null)
      this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.Ved_Nastr_Init(this._imsObjectType_RazrabatyvaemoiVed.Guid, Guid.Empty, true);
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null)
    {
      int num = (int) MessageBox.Show("Настроек для данной ведомости не найдено", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType == null)
    {
      int num = (int) MessageBox.Show("В настройках не определен ImsObjectType", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._idTypeVed < 2)
    {
      int num = (int) MessageBox.Show("В настройках не определен ID", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._guidTypeVed == Guid.Empty)
    {
      int num = (int) MessageBox.Show("В настройках не определен Guid", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    this.ListCommonId_Filled(this.listCommonId);
    XmlElement xmlElement_Kuda = (XmlElement) null;
    if (Vedomost_VB_Static.isCreateDump_Tmp)
    {
      if (Directory.Exists(Vedomost_VB_Static.DirectoryDump))
      {
        Vedomost_VB_Static.CleaningDirectoryDumpVed();
        Vedomost_VB_Static.OneVedNastrToDump(this._one_Ved_Nastr_RazrabatyvaemoiVed);
        Vedomost_VB_Static.ShablonToDump(this._one_Ved_Nastr_RazrabatyvaemoiVed);
      }
      List<string> stringList = Vedomost_VB_Static.GelVersion_AVS();
      StreamWriter streamWriter = new StreamWriter(Vedomost_VB_Static.DirectoryDump + "\\VERSION.TXT");
      for (int index = 0; index < stringList.Count; ++index)
      {
        string str = stringList[index];
        streamWriter.WriteLine(str);
      }
      streamWriter.Close();
      this.txtProtocol_create(Vedomost_VB_Static.DirectoryDump + "\\PROTOСOL все этапы.TXT");
      this.XmlProtocol_create();
      (this.xml_SborMainVed_Dump, xmlElement_Kuda) = this.Ved_Dump_Create(Vedomost_VB_Static.DirectoryDump, "SborMainVed_Dump.xml", this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ObjectName);
    }
    try
    {
      this.sborVedTask = new SborVedTask("Предварительный сбор");
      this.sborVedTask.Show();
      if (!this.ProcessingMainVed_Step1_PostroenieDereva(isReCreate))
        return false;
      if (this._xmlProtocol != null && (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain))
        this._xmlProtocol.Save(Vedomost_VB_Static.DirectoryDump + "\\PROTOCOL_предварительный сбор дерева.xml");
      if (this._txtProtocol != null)
      {
        this.txtProtocol_Add("ЗАКОНЧЕН  ПРЕДВАРИТЕЛЬНЫЙ СБОР СПЕЦИФИКАЦИЙ");
        this.txtProtocol_Add("===============================================================");
        this.txtProtocol_Add("");
        this.txtProtocol_Add("---------------------------------------------------------------");
        this.txtProtocol_Add("ЭТАПЫ ОБРАБОТКИ СПИСКА СП");
      }
      if (this.xml_SborMainVed_Dump != null)
      {
        this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step001_Сразу после сбора", this._listRecordsForMainVed);
        if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 0)
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step001DopZam_Сразу после сбора", this._listRecordsForMainVed_DopZam);
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isMainSort1)
      {
        this.txtProtocol_Add("");
        this.txtProtocol_Add("Первая сортировка списка СП");
        this._listRecordsForMainVed.Sort((IComparer<Vedomost_VB.RecordForMainVed>) this._compareRecordsForMainVed_byDesignation);
        if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
          this._listRecordsForMainVed_DopZam.Sort((IComparer<Vedomost_VB.RecordForMainVed>) this._compareRecordsForMainVed_byDesignation);
        if (this.xml_SborMainVed_Dump != null)
        {
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step002_После первой сортировки", this._listRecordsForMainVed);
          if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
            this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step002DopZam_После первой сортировки", this._listRecordsForMainVed_DopZam);
        }
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isMainSummOdinakovyh)
      {
        this.txtProtocol_Add("");
        this.txtProtocol_Add("Первое объединение");
        this.ProcessingMainVed_Step2_SummOdinakovyh(this._listRecordsForMainVed);
        this.ProcessingMainVed_Step3_MainIspToKuda(this._listRecordsForMainVed);
        if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
        {
          this.ProcessingMainVed_Step2_SummOdinakovyh(this._listRecordsForMainVed_DopZam);
          this.ProcessingMainVed_Step3_MainIspToKuda(this._listRecordsForMainVed_DopZam);
        }
        if (this.xml_SborMainVed_Dump != null)
        {
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step003_После первого объединения", this._listRecordsForMainVed);
          if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
            this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step003DopZam_После первого объединения", this._listRecordsForMainVed_DopZam);
        }
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isMainSort2)
      {
        this.txtProtocol_Add("");
        this.txtProtocol_Add("Сортировка списка СП по исполнениям");
        this._listRecordsForMainVed.Sort((IComparer<Vedomost_VB.RecordForMainVed>) this._compareRecordsForMainVed_byDesignation4);
        if (this.xml_SborMainVed_Dump != null)
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step005_После второй сортировки", this._listRecordsForMainVed);
        if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
        {
          this._listRecordsForMainVed_DopZam.Sort((IComparer<Vedomost_VB.RecordForMainVed>) this._compareRecordsForMainVed_byDesignation4);
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step005DopZam_После второй сортировки", this._listRecordsForMainVed_DopZam);
        }
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isMainCreateVtorRecords)
      {
        this.txtProtocol_Add("");
        this.txtProtocol_Add("Выделение вторичных записей");
        this.ProcessingMainVed_Step5_CreateVtorRecords(this._listRecordsForMainVed);
        if (this.xml_SborMainVed_Dump != null)
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step006_Созданы вторичные записи", this._listRecordsForMainVed);
        if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
        {
          this.ProcessingMainVed_Step5_CreateVtorRecords(this._listRecordsForMainVed_DopZam);
          if (this.xml_SborMainVed_Dump != null)
            this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step006DopZam_Созданы вторичные записи", this._listRecordsForMainVed_DopZam);
        }
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isMainSumm)
      {
        this.txtProtocol_Add("");
        this.txtProtocol_Add("Суммирование СП");
        this.ProcessingMainVed_Step6_Summ(this._listRecordsForMainVed);
        if (this.xml_SborMainVed_Dump != null)
          this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step007_Произведено суммирование", this._listRecordsForMainVed);
        if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 1)
        {
          this.ProcessingMainVed_Step6_Summ(this._listRecordsForMainVed_DopZam);
          if (this.xml_SborMainVed_Dump != null)
            this.Main_Dump_Add_Step(this.xml_SborMainVed_Dump, xmlElement_Kuda, "Step007DopZam_Произведено суммирование", this._listRecordsForMainVed_DopZam);
        }
      }
      if (this.xml_SborMainVed_Dump != null)
      {
        this.xml_SborMainVed_Dump.Save(Vedomost_VB_Static.DirectoryDump + "\\SborMainVed_Dump.xml");
        this.xml_SborMainVed_Dump = (XmlDocument) null;
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI)
      {
        for (int index = this._listRecordsForMainVed.Count - 1; index >= 1; --index)
        {
          Vedomost_VB.RecordForMainVed recordForMainVed = this._listRecordsForMainVed[index];
          if (!recordForMainVed.IsTherezKomplekt && !recordForMainVed.EtaSp_Komplekt)
            this._listRecordsForMainVed.Remove(recordForMainVed);
        }
      }
      this.txtProtocol_Add("Закончена обработка списка СП");
      if (this._listRecordsForMainVed.Count == 0)
      {
        this.txtProtocol_Add("ДЕРЕВО СОСТАВА ПУСТОЕ !!!!!!!!!!!!!!!!!!!");
        int num = (int) MessageBox.Show("ДЕРЕВО СОСТАВА ПУСТОЕ", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      if (this._listRecordsForMainVed.Count == 1)
        this.txtProtocol_Add("ДЕРЕВО СОСТАВА СОДЕРЖИТ ТОЛЬКО ГОЛОВНУЮ СПЕЦИФИКАЦИЮ !!!!!!!!!!!!!!!!!!!");
      this.txtProtocol_Add("");
      this.txtProtocol_Add("---------------------------------------------------------------");
      this.txtProtocol_Add("СБОР ВЕДОМОСТИ НА ОСНОВАНИИ СОБРАННОГО СПИСКА СП");
      this.Create_Ved();
      this.txtProtocol_Add("СБОР ВЕДОМОСТИ ЗАКОНЧЕН");
      this.txtProtocol_Add("===============================================================");
      this.txtProtocol_Add("");
      if (this._txtProtocol != null)
      {
        this._txtProtocol.Close();
        Vedomost_VB_Static.AboutToFile();
      }
    }
    catch (Exception ex)
    {
      if (this.sborVedTask != null)
        this.sborVedTask.Dispose();
      string str = "Ошибка создания ведомости";
      int num = (int) MessageBox.Show(Vedomost_VB_Static.isCreateDump_Tmp ? str + "\r\nПросим запаковать и выслать в адрес Интермех все файлы из данной папки" : str + "\r\nРекомендуем повторно выполнить команду создания ведомости\r\n" + "При этом автоматически будут созданы файлы протокола для анализа ошибок программы", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      if (Vedomost_VB_Static.isCreateDump_Tmp)
      {
        if (Directory.Exists(Vedomost_VB_Static.DirectoryDump))
        {
          if (this._one_Ved_Nastr_RazrabatyvaemoiVed._isCreateDumpAuto == 1)
          {
            if (this._txtProtocol != null)
              this._txtProtocol.Close();
            if (this.xml_SborVed_Dump != null)
              this.xml_SborVed_Dump.Save(Vedomost_VB_Static.DirectoryDump + "\\SborVed_Dump.xml");
            this.Ved_XML_File_Create_From_ListRecordsVed_New();
            if (this.xml_SborMainVed_Dump != null)
              this.xml_SborMainVed_Dump.Save(Vedomost_VB_Static.DirectoryDump + "\\SborMainVed_Dump.xml");
            Process.Start(Vedomost_VB_Static.DirectoryDump);
          }
        }
      }
      else
        Vedomost_VB_Static.isCreateDump_Tmp = true;
    }
    return true;
  }

  /// <summary> Выбор разрабатываемой ведомости  и НОМЕРА исполнения </summary>
  /// <returns> если False, то прерываем</returns>
  public bool VyborVedomosti()
  {
    Vedomost_VB_Static.Begin_For_Ved();
    if (Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr.Count == 0)
    {
      int num = (int) MessageBox.Show($"В Конфигураторе базы данных  не назначен Редактор ведомостей\r\n\r\nСмотри:\r\n\r\nНастройка/Настройка инструментов\r\n  Объекты\r\n    Документы\r\n      Конструкторские документы\r\n        Ведомости\r\n\r\nДля КАЖДОГО документа должны быть настроены\r\n\r\n\"Редактор команд\" и \"Команды по умолчанию\" - AVS" + "\r\n\r\nИ перегрузить программу (клиента) заново", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    using (GroupSelectionForm groupSelectionForm = new GroupSelectionForm())
    {
      groupSelectionForm._allProducts = this._listAll_IspolneniySp_prodInfo;
      if (this._specificationMain != null)
        groupSelectionForm._aVSDocumentForm = this._specificationMain.AvsDocumentForm;
      groupSelectionForm._designation_Article = this.DesignationArticle;
      if (groupSelectionForm.ShowDialog() != DialogResult.OK || groupSelectionForm.isError)
        return false;
      this._imsObjectType_RazrabatyvaemoiVed = groupSelectionForm._one_ImsObjectType_With_One_Ved_Nastr_Result.imsObjectType;
      Guid vedomosyByGuidTypeVed = Vedomost_VB_Static.Get_GuidTemplateVedomosy_ByGuidTypeVed(this._imsObjectType_RazrabatyvaemoiVed.Guid);
      if (vedomosyByGuidTypeVed == Guid.Empty)
      {
        int num = (int) MessageBox.Show("Для такого типа документа нет назначенного шаблона" + "\r\n\r\nНастройка ведомости невозможна" + "\r\n\r\nНеобходимо настроить систему в" + "\r\n\r\nНастройка\r\n  Настройка инструментов\r\n    Интеграторы с приложениями\r\n      Интегратор с редактором документов" + "\r\n\r\nИ перегрузить программу (клиента) заново", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(vedomosyByGuidTypeVed, false) == null)
        {
          int num = (int) MessageBox.Show("Файл шаблона (бланка) не найден", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
      }
      this._one_Ved_Nastr_RazrabatyvaemoiVed = groupSelectionForm._one_Conformity_Template_Nastr_Result == null || groupSelectionForm._one_Conformity_Template_Nastr_Result._one_Ved_Nastr == null ? groupSelectionForm._one_ImsObjectType_With_One_Ved_Nastr_Result.one_Ved_Nastr : groupSelectionForm._one_Conformity_Template_Nastr_Result._one_Ved_Nastr;
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._list_Ved_ID.Count == 0 || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeCreateNastr == TypeCreateNastr.Empty)
      {
        int num = (int) MessageBox.Show("Создание ведомости пока невозможно\r\n" + "т.к. настройка для данного типа ведомости не производилась" + "\r\n\r\nСмотри\r\nНастройка/Настройка конструкторских ведомостей", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this.DesignationArticle = groupSelectionForm._designationVed_Result;
      if (this._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID != 0 && this._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID != -1)
      {
        this.KodDoc = Vedomost_VB_Static.Get_DocumentTypeSuffix_ForObjectTypeId(this._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID);
        if (string.IsNullOrEmpty(this.KodDoc))
        {
          int num = (int) MessageBox.Show($"В конфигураторе базы данных для документа типа\r\n\r\n{this._imsObjectType_RazrabatyvaemoiVed.ObjectName}\r\n\r\n" + "не настроен \"Код типа документа\"" + "\r\nКод, соответствующий ЕСКД, должен входить в обозначение" + "\r\n\r\nНеобходимо предварительно настроить", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
      }
      string str = "";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        str = DocumentsHelper.GetSeparatorInDesignation(sessionKeeper.Session);
      this.DesignationDoc = this.DesignationArticle + str + this.KodDoc;
      this.NameTypeDoc = this._imsObjectType_RazrabatyvaemoiVed.ObjectName;
      this.NameArticle = groupSelectionForm._productInfo_Result.Name;
      this._prodInfo = groupSelectionForm._productInfo_Result;
      this._isGroupSp = groupSelectionForm._isGroupSp;
      this._isGroupVed = this._isGroupSp && groupSelectionForm._isGroupVed;
      this._groupForm = !this._isGroupSp || !this._isGroupVed ? Vedomost_VB.FormaGroup.Ed : groupSelectionForm._formaGroup;
      this._designationIspVed = groupSelectionForm._designationVed_Result;
      this._i_vybranogo_Ispolnenia = groupSelectionForm._iIsp_Result;
    }
    if (!(ServicesManager.GetService(typeof (IPDMSpecificationsService)) is IPDMSpecificationsService service))
    {
      int num = (int) MessageBox.Show("Не найдена служба PDM", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    if (MetaDataHelper.GetObjectType(this._imsObjectType_RazrabatyvaemoiVed.Guid) == null)
    {
      int num = (int) MessageBox.Show("Не найден GUID типа объекта\r\nGetObjectType(_imsObjectType_RazrabatyvaemoiVed.Guid)\r\n" + this._imsObjectType_RazrabatyvaemoiVed.Guid.ToString(), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    long objectWithDesignation = service.GetObjectWithDesignation(AvsIDCache.ObjType_Document, this._designationDoc);
    switch (objectWithDesignation)
    {
      case -1:
      case 0:
        if (!objectWithDesignation.IsUndefinedId())
        {
          if (MessageBox.Show($"Документ\r\n\r\n{this._designationDoc}\r\n\r\nуже существует!\r\nСоздать заново?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return false;
          ObjectModifyModes objectModifyMode;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectWithDesignation, false);
            if (dbObject == null)
              return false;
            objectModifyMode = dbObject.ObjectModifyMode;
          }
          if (objectWithDesignation > 0L && objectModifyMode == ObjectModifyModes.Checkout)
          {
            int num = (int) MessageBox.Show($"Документ\r\n\r\n{this._designationDoc}\r\n\r\nЗакрыт для редактирования.\r\nЕго необходимо предварительно взять на редактирование", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
          }
          if (objectModifyMode == ObjectModifyModes.CreateVersion && MessageBox.Show("Будет создана новая версия ведомости\r\n\r\nСоздать?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return false;
        }
        this._variables_Coordination = new Vedomost_VB.Variables_Coordination();
        if (this._isGroupVed)
        {
          if (this.CheckSystemCaptions1() == 2)
          {
            for (int index = 0; index < this._listAll_IspolneniySp_prodInfo.Count; ++index)
            {
              int num = index + 1;
              this._variables_Coordination.list_Variables.Add(this._listAll_IspolneniySp_prodInfo[index].Designation);
              this._variables_Coordination.list_Captions.Add(num.ToString());
            }
          }
          else
          {
            for (int index = 0; index < this._listAll_IspolneniySp_prodInfo.Count; ++index)
              this._variables_Coordination.Add_Variable_AutoCaption(this._listAll_IspolneniySp_prodInfo[index].Designation, this._prodInfo.Designation);
          }
        }
        else
          this._variables_Coordination.Add_Variable_AutoCaption(this._prodInfo.Designation, this._prodInfo.Designation);
        return true;
      default:
        if (string.IsNullOrEmpty(Vedomost_VB_Static.Get_DocumentTypeSuffix_ForObjectTypeId(objectWithDesignation)))
        {
          int num = (int) MessageBox.Show($"В конфигураторе базы данных для документа типа\r\n\r\n{this._imsObjectType_RazrabatyvaemoiVed.ObjectName}\r\n\r\n" + "не настроен \"Код типа документа\", например \"ДП\"" + "\r\nи Код должен входить в обозначение", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
        goto case -1;
    }
  }

  /// <summary> Проверка. Какая состема обозначений. </summary>
  /// <returns></returns>
  public int CheckSystemCaptions1()
  {
    int num = 0;
    int length1 = this._designationArticle.Length;
    if (this._prodInfo == null || string.IsNullOrEmpty(this._prodInfo.Designation))
      return 0;
    if (this._isGroupVed)
    {
      for (int index = 0; index < this._listAll_IspolneniySp_prodInfo.Count; ++index)
      {
        ProductInfo productInfo = this._listAll_IspolneniySp_prodInfo[index];
        if (index == 0)
        {
          if (this._designationArticle != productInfo.Designation)
            return 2;
        }
        else
        {
          string designation = productInfo.Designation;
          if (index == 1)
            num = designation.Length;
          if (!designation.StartsWith(this._designationArticle))
            return 2;
          int length2 = designation.Length;
          if (num != length2)
            return 2;
        }
      }
    }
    return 1;
  }

  public bool VyborVedomosti_For_Recreate()
  {
    if (!(ServicesManager.GetService(typeof (IPDMSpecificationsService)) is IPDMSpecificationsService))
    {
      int num = (int) MessageBox.Show("Не найдена служба PDM", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.Read_One_Ved_Nastr(this._imsObjectType_RazrabatyvaemoiVed, Vedomost_VB.TypeDoc.Ved);
    One_Ved_Nastr razrabatyvaemoiVed = this._one_Ved_Nastr_RazrabatyvaemoiVed;
    this.KodDoc = Vedomost_VB_Static.Get_DocumentTypeSuffix_ForObjectTypeId(this._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID);
    string str = " ";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      str = DocumentsHelper.GetSeparatorInDesignation(sessionKeeper.Session);
    this.DesignationDoc = this.DesignationArticle + str + this.KodDoc;
    this.NameTypeDoc = this._imsObjectType_RazrabatyvaemoiVed.ObjectName;
    if (string.IsNullOrEmpty(this.NameArticle))
      this.NameArticle = this._listAll_IspolneniySp_prodInfo[0].Name;
    if (this._prodInfo == null)
      this._prodInfo = this._listAll_IspolneniySp_prodInfo[0];
    if (this._listAll_IspolneniySp_prodInfo.Count == 1)
    {
      this._isGroupSp = false;
      this._isGroupVed = false;
      this._groupForm = Vedomost_VB.FormaGroup.Ed;
    }
    else
    {
      this._isGroupSp = true;
      this._isGroupVed = this._groupForm != Vedomost_VB.FormaGroup.Ed;
    }
    if (this._isGroupVed)
    {
      this._variables_Coordination = new Vedomost_VB.Variables_Coordination();
      for (int index = 0; index < this._listAll_IspolneniySp_prodInfo.Count; ++index)
        this._variables_Coordination.Add_Variable_AutoCaption(this._listAll_IspolneniySp_prodInfo[index].Designation, this._prodInfo.Designation);
    }
    return true;
  }

  public void Read_One_Ved_Nastr2(IMSObjectType imsObjectType_RazrabatyvaemoiVed)
  {
    if (imsObjectType_RazrabatyvaemoiVed == null || imsObjectType_RazrabatyvaemoiVed.ObjectTypeID < 1 || imsObjectType_RazrabatyvaemoiVed.Guid == Guid.Empty)
      return;
    this._guidTemplateDoc = DocumentEditorPlugin.GetDocumentTemplateIDFromIMDocSettings(imsObjectType_RazrabatyvaemoiVed.Guid);
    if (this._guidTemplateDoc == Guid.Empty)
      return;
    XmlDocument xmlDocument = Vedomost_VB_Static.ReadXmlNastrFromBase(this._guidTemplateDoc);
    if (xmlDocument == null)
    {
      if (imsObjectType_RazrabatyvaemoiVed.Guid != Vedomost_VB_Static.GuidVS && imsObjectType_RazrabatyvaemoiVed.Guid != Vedomost_VB_Static.GuidVP && imsObjectType_RazrabatyvaemoiVed.Guid != Vedomost_VB_Static.GuidRS)
        return;
      this._one_Ved_Nastr_RazrabatyvaemoiVed = Vedomost_VB_Static.Ved_Nastr_Init(imsObjectType_RazrabatyvaemoiVed.Guid, Guid.Empty, true);
    }
    else
    {
      this._one_Ved_Nastr_RazrabatyvaemoiVed = new One_Ved_Nastr();
      this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType = imsObjectType_RazrabatyvaemoiVed;
      this._one_Ved_Nastr_RazrabatyvaemoiVed.Filled_One_Ved_Nastr_FromXml(xmlDocument);
      this._one_Ved_Nastr_RazrabatyvaemoiVed._guidTypeVed = imsObjectType_RazrabatyvaemoiVed.Guid;
      this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid = this._guidTemplateDoc;
    }
  }

  /// <summary> Построение ДЕРЕВА проекта (это может быть групповой СП) </summary>
  /// <param name="_listAll_IspolneniySp"></param>
  /// <returns></returns>
  public bool ProcessingMainVed_Step1_PostroenieDereva(bool isRecreate)
  {
    if (this._prodInfo != null)
      this._objectTypeMainSp = this._prodInfo.ObjectType;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._is_Vydeliat_Therez_Komplekty && this._objectTypeMainSp == AvsIDCache.ObjType_Complect)
    {
      this._is_Golovnaia_Sp_Komplekt = true;
      this.txtProtocol_Add("Головная спецификация = Комплект");
    }
    int num1;
    int num2;
    if (this._isGroupSp)
    {
      if (this._isGroupVed)
      {
        num1 = 0;
        num2 = this._variables_Coordination.list_Variables.Count;
        this.txtProtocol_Add("Групповая ведомость");
      }
      else
      {
        num1 = this._i_vybranogo_Ispolnenia;
        num2 = num1 + 1;
        this.txtProtocol_Add("Ведомость на одно исполнение");
      }
    }
    else
    {
      num1 = 0;
      num2 = 1;
      this._isGroupVed = false;
    }
    this._listRecordsForMainVed = new List<Vedomost_VB.RecordForMainVed>();
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isAllocateDopZam == 1)
      this._listRecordsForMainVed_DopZam = new List<Vedomost_VB.RecordForMainVed>();
    this._listSvoiaVedomost = new List<Vedomost_VB.RecordForMainVed>();
    this._listIzdelie_Doc = new List<Vedomost_VB.Izdelie_Doc>();
    for (int index = num1; index < num2; ++index)
    {
      string Ispolnenie = "";
      ProductInfo productInfo = this._listAll_IspolneniySp_prodInfo[index];
      if (productInfo == null || productInfo.Id == 0L || productInfo.Id == -1L)
        return false;
      DataTable dataTable = this.ReadOneSpecification(productInfo.Id, this.listCommonId._listCommonId, 3);
      if (dataTable == null)
        return false;
      this._listSpecifications.Add(new Vedomost_VB.OneSpecification(productInfo.Id, productInfo.ObjectType, productInfo.Designation, dataTable));
      Vedomost_VB.RecordForMainVed RecordForMainVedPrevision = new Vedomost_VB.RecordForMainVed((DataRow) null, productInfo.Designation, productInfo.Id, false, false, "1", this);
      RecordForMainVedPrevision.PartsDoc = dataTable;
      RecordForMainVedPrevision.UrovenN = this._urovenN;
      RecordForMainVedPrevision.Name = this._nameArticle;
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
      {
        string stringForObjType = RecordForMainVedPrevision.Get_Data_String_for_objType(AvsIDCache.Attr_DerzPodl);
        RecordForMainVedPrevision.DerzPodl = stringForObjType;
      }
      if (this._isGroupVed)
      {
        RecordForMainVedPrevision.Ispolnenie = productInfo.Designation;
        Ispolnenie = productInfo.Designation;
      }
      if (RecordForMainVedPrevision.Ispolnenie == null)
        RecordForMainVedPrevision.Ispolnenie = "";
      this._listRecordsForMainVed.Add(RecordForMainVedPrevision);
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isAllocateDopZam == 1 && this._listRecordsForMainVed_DopZam != null)
        this._listRecordsForMainVed_DopZam.Add(RecordForMainVedPrevision);
      string designation = this._prodInfo.Designation;
      long id = this._prodInfo.Id;
      bool is_VSI = this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VSI;
      if (!this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isOnlyUroven1)
        this.ProcessingOneSpecification(dataTable, this.listCommonId._listCommonId, designation, true, id, "1", Ispolnenie, RecordForMainVedPrevision, is_VSI);
    }
    return true;
  }

  /// <summary> Обработка одной спецификации одного изделия, исполнения </summary>
  /// <param name="DataTableSp"> Таблица СП ИЗДЕЛИЯ</param>
  /// <param name="listIdFields"> список полей для чтения </param>
  /// <param name="KudaDesignation"></param>
  /// <param name="IsGolovnaya"></param>
  /// <param name="ObjectId_Kuda"></param>
  /// <param name="Uroven"></param>
  /// <param name="Ispolnenie"></param>
  /// <param name="RecordForMainVedPrevision"></param>
  /// <param name="is_VSI"> признак ВСИ </param>
  public void ProcessingOneSpecification(
    DataTable DataTableSp,
    List<Vedomost_VB.OneFieldSpForRead> ListIdFields,
    string KudaDesignation,
    bool IsGolovnaya,
    long ObjectId_Kuda,
    string Uroven,
    string Ispolnenie,
    Vedomost_VB.RecordForMainVed RecordForMainVedPrevision,
    bool is_VSI)
  {
    if (DataTableSp == null)
      return;
    bool flag1 = false;
    bool flag2 = false;
    int num1 = 1;
    string str1 = "";
    int columnIndex1 = this.ItemIndexForID(ListIdFields, -2);
    int columnIndex2 = this.ItemIndexForID(ListIdFields, -7);
    int columnIndex3 = this.ItemIndexForID(ListIdFields, AvsIDCache.Attr_Designation);
    int columnIndex4 = this.ItemIndexForID(ListIdFields, AvsIDCache.Attr_Name);
    int columnIndex5 = this.ItemIndexForID(ListIdFields, AvsIDCache.Attr_SpecificationSection);
    int columnIndex6 = this.ItemIndexForID(ListIdFields, AvsIDCache.Attr_PartName);
    if (this._urovenN > 19)
    {
      int num2 = (int) MessageBox.Show("Состав изделия зациклен. Проверьте", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.is_Zacikleno = true;
    }
    else
    {
      ++this._urovenN;
      int iRec = 0;
      bool flag3 = false;
      if (!string.IsNullOrEmpty(Ispolnenie) && Ispolnenie.StartsWith(KudaDesignation))
        this.txtProtocol_Add("Обработка :" + Ispolnenie, this._urovenN);
      else
        this.txtProtocol_Add("Обработка :" + KudaDesignation, this._urovenN);
      using (SessionKeeper sk = new SessionKeeper())
      {
        int num3 = -1;
        foreach (DataRow row in (InternalDataCollectionBase) DataTableSp.Rows)
        {
          ++num3;
          long num4 = -1;
          int num5 = -1;
          string str2 = "";
          string str3 = "";
          string str4 = "";
          string str5 = "";
          if (IsGolovnaya)
          {
            this._isTherezDopZam = false;
            flag2 = false;
          }
          if (columnIndex2 > -1 && row[columnIndex2] != DBNull.Value)
            num5 = Convert.ToInt32(row[columnIndex2]);
          if (columnIndex3 > -1 && row[columnIndex3] != DBNull.Value)
            str2 = Convert.ToString(row[columnIndex3]);
          if (columnIndex4 > -1 && row[columnIndex4] != DBNull.Value)
            str3 = Convert.ToString(row[columnIndex4]);
          if (columnIndex5 > -1 && row[columnIndex5] != DBNull.Value)
            str4 = Convert.ToString(row[columnIndex5]);
          if (columnIndex6 > -1 && row[columnIndex6] != DBNull.Value)
            str5 = Convert.ToString(row[columnIndex6]);
          if (columnIndex1 > -1 && row[columnIndex1] != DBNull.Value)
            num4 = Convert.ToInt64(row[columnIndex1]);
          if (num5 == AvsIDCache.ObjType_Specification || num5 == AvsIDCache.ObjType_Complect)
          {
            Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(ObjectId_Kuda, sk);
            this._listIzdelie_Doc.Add(new Vedomost_VB.Izdelie_Doc()
            {
              _objectId_KudaVhodit = ObjectId_Kuda,
              _objectIdDoc = num4,
              _designation = str2,
              _name = str3
            });
          }
          if (IsGolovnaya && num3 == 0)
          {
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
              this._derzPodl = this.Field_From_RecordSP_Extended(row, AvsIDCache.Attr_DerzPodl);
          }
          if (is_VSI || sk.Session.GetRelationsApplicabilityCollection().GetApplicability(AvsIDCache.Relation_Document, AvsIDCache.ObjType_Specification, num5) != null)
          {
            Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(ObjectId_Kuda, sk);
            this._listIzdelie_Doc.Add(new Vedomost_VB.Izdelie_Doc()
            {
              _objectId_KudaVhodit = ObjectId_Kuda,
              _objectIdDoc = num4,
              _designation = str2,
              _name = str3
            });
            if ((string.IsNullOrEmpty(str5) || !(str5 == "Снятые составные части")) && this.CheckRecordSp_for_Create_recordForMainVed(row) != 2)
            {
              if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._opеning_Sections != null && this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._opеning_Sections.Count > 0 && !string.IsNullOrEmpty(str4))
              {
                bool flag4 = false;
                for (int index = 0; index < this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._opеning_Sections.Count; ++index)
                {
                  if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._opеning_Sections[index] == str4)
                  {
                    flag4 = true;
                    break;
                  }
                }
                if (!flag4)
                  continue;
              }
              if (row[columnIndex1] != DBNull.Value)
                num4 = Convert.ToInt64(row[columnIndex1]);
              if (row[columnIndex3] != DBNull.Value)
                str2 = Convert.ToString(row[columnIndex3]);
              if (row[columnIndex4] != DBNull.Value)
                str1 = Convert.ToString(row[columnIndex4]);
              int recordForMainVed1 = this.CheckSp_for_Create_recordForMainVed(row);
              if (recordForMainVed1 != 2)
              {
                if (!this._isTherezKomplekt && !this._is_Golovnaia_Sp_Komplekt && this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._is_Vydeliat_Therez_Komplekty && num5 == AvsIDCache.ObjType_Complect)
                  this._isTherezKomplekt = true;
                if (num5 == AvsIDCache.ObjType_Complect)
                {
                  if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isFor_ZIP_COMPL_Add || this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isFor_ZIP_COMPL_Raskr)
                    flag1 = true;
                  else
                    continue;
                }
                string str6 = this.Field_From_RecordSP(row, AvsIDCache.Attr_DopZamenNumInGroup, out bool _);
                if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isDopZam == 1 && this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isAllocateDopZam == 1 && str6 != "" && str6 != "0")
                {
                  this._isTherezDopZam = true;
                  flag2 = true;
                }
                string str7 = $"{Uroven}.{Convert.ToString(num1)}";
                ++num1;
                Vedomost_VB.RecordForMainVed recordForMainVed2 = new Vedomost_VB.RecordForMainVed(row, KudaDesignation, ObjectId_Kuda, this._isTherezKomplekt, this._isTherezDopZam, str7, this);
                recordForMainVed2.EtaSp_Komplekt = flag1;
                if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI)
                  recordForMainVed2.IsTherezKomplekt = RecordForMainVedPrevision.EtaSp_Komplekt;
                recordForMainVed2.Ispolnenie = Ispolnenie;
                recordForMainVed2.UrovenN = this._urovenN;
                recordForMainVed2.EtaSp_Komplekt = flag1;
                recordForMainVed2.EtaSp_DopZam = flag2;
                recordForMainVed2.recordForMainVedPrevision = RecordForMainVedPrevision;
                if (!recordForMainVed2.EtaSp_DopZam && !recordForMainVed2.IsTherezDopZam)
                {
                  this._listRecordsForMainVed.Add(recordForMainVed2);
                  recordForMainVed2.EtaSp_DopZam = false;
                  if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isAllocateDopZam == 1 && this._listRecordsForMainVed_DopZam != null)
                    this._listRecordsForMainVed_DopZam.Add(recordForMainVed2);
                }
                else if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isAllocateDopZam == 1 && this._listRecordsForMainVed_DopZam != null)
                {
                  recordForMainVed2.EtaSp_DopZam = true;
                  this._listRecordsForMainVed_DopZam.Add(recordForMainVed2);
                }
                if (recordForMainVed1 > 0)
                {
                  recordForMainVed2.NeRaskryvat = true;
                }
                else
                {
                  if (is_VSI)
                  {
                    if (this._listRecordsVed_New == null)
                      this._listRecordsVed_New = new List<Vedomost_VB.RecordForVed_New>();
                    ++iRec;
                    Vedomost_VB.RecordForVed_New recordForVedNew = this.Create_recordForVed_New(row, RecordForMainVedPrevision, Uroven, iRec, sk);
                    if (recordForVedNew != null)
                      this._listRecordsVed_New.Add(recordForVedNew);
                  }
                  if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
                  {
                    string str8 = this.Field_From_RecordSP_Extended(row, AvsIDCache.Attr_DerzPodl);
                    recordForMainVed2.DerzPodl = str8;
                  }
                  if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isRaskrSP_s_takoi_Ved == 0)
                  {
                    DataRow recordSp = this.CheckingCurrentVedInSp(num4);
                    if (recordSp != null)
                    {
                      recordForMainVed2.EstSvoiaVedomost = true;
                      recordForMainVed2.RecordSpIncudeVed = recordSp;
                      recordForMainVed2.IsTherezKomplekt = false;
                      recordForMainVed2.EtaSp_Komplekt = false;
                      recordForMainVed2.EtaSp_DopZam = false;
                      recordForMainVed2.IsTherezDopZam = false;
                      recordForMainVed2.ObjectIdIzd = num4;
                      Vedomost_VB_Static.Get_DocumentTypeName_ForObjectId(this.Get_ObjectId_From_RecordSP_Extended(recordSp));
                      this._listSvoiaVedomost.Add(recordForMainVed2);
                      this.txtProtocol_Add(recordForMainVed2.Designation.ToString() + " : Не раскрывали. Т.к. есть своя ведомость", this._urovenN);
                      if (this._xmlProtocol != null)
                      {
                        this.XmlProtocol_Add(this._xmlProtocol, recordForMainVed2, "Не раскрывали. Есть своя ведомость");
                        continue;
                      }
                      continue;
                    }
                    if (this._derzPodl != recordForMainVed2.DerzPodl)
                      recordForMainVed2.NeRaskryvat = true;
                  }
                  DataTable dataTable = this.FindObjId_In_listSpecifications(num4);
                  if (dataTable == null)
                  {
                    dataTable = this.ReadOneSpecification(num4, this.listCommonId._listCommonId, 1);
                    if (dataTable != null && this._designationArticle != str2)
                      this._listSpecifications.Add(new Vedomost_VB.OneSpecification(num4, num5, str2, dataTable));
                  }
                  if (dataTable != null && !this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isOnlyUroven1)
                  {
                    XmlElement xmlElement1 = (XmlElement) null;
                    XmlElement xmlElement2 = (XmlElement) null;
                    if (this._xmlProtocol != null)
                    {
                      xmlElement1 = this.XmlProtocol_Add(this._xmlProtocol, recordForMainVed2, "");
                      xmlElement2 = (XmlElement) null;
                      if (xmlElement1 != null)
                      {
                        xmlElement2 = this._xmlElementCurr;
                        this._xmlElementCurr = xmlElement1;
                      }
                    }
                    this.ProcessingOneSpecification(dataTable, this.listCommonId._listCommonId, str2, false, num4, str7, Ispolnenie, recordForMainVed2, is_VSI);
                    if (!this.is_Zacikleno)
                    {
                      flag3 = true;
                      if (this._xmlProtocol != null && xmlElement1 != null)
                        this._xmlElementCurr = xmlElement2;
                    }
                    else
                      break;
                  }
                  recordForMainVed2.PartsDoc = dataTable;
                }
              }
            }
          }
        }
      }
      if (this.is_Zacikleno)
        return;
      if (flag1)
        this._isTherezKomplekt = false;
      if (flag2)
        this._isTherezDopZam = false;
      if (flag3)
        this.txtProtocol_Add("Окончание :" + KudaDesignation, this._urovenN);
      else
        this.txtProtocol_Add("Входящих СП нет :" + KudaDesignation, this._urovenN);
      this.txtProtocol_Add("", this._urovenN);
      --this._urovenN;
    }
  }

  /// <summary> Проверка, брать ли эту СП (только по данным записи! т.е. по параметрам связи и только в ЭТОМ применении) </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public int CheckRecordSp_for_Create_recordForMainVed(DataRow recordSp)
  {
    this.Get_Designation_From_RecordSP_Extended(recordSp);
    this.Get_Name_From_RecordSP_Extended(recordSp);
    string str = this.Field_From_RecordSP(recordSp, AvsIDCache.Attr_DopZamenNumInGroup, out bool _);
    return str != "" && str != "0" && this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isDopZam == 0 ? 2 : 0;
  }

  /// <summary> Проверка, брать ли эту СП (только по данным базы! т.е. для всех применений) </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public int CheckSp_for_Create_recordForMainVed(DataRow recordSp) => 0;

  /// <summary> Проверка. А может в этой спецификации есть такая ведомость </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  public DataRow CheckingCurrentVedInSp(long ObjectId)
  {
    DataTable dataTable = this.ReadOneSpecification(ObjectId, this.listCommonId._listCommonId, 2);
    if (dataTable == null)
      return (DataRow) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int num = -1;
      if (row[1] != DBNull.Value)
        num = Convert.ToInt32(row[1]);
      if (this._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID == num)
        return row;
    }
    return (DataRow) null;
  }

  /// <summary> Прочитать запись о ВХОДЯЩЕЙ ВЕДОМОСТИ. А может в этой спецификации есть такая ведомость </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  public DataRow ReadRecOfCurrentVedInSp(long objectId)
  {
    DataTable dataTable = this.ReadOneSpecification(objectId, this.listCommonId._listCommonId, 2);
    if (dataTable == null)
      return (DataRow) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int num = -1;
      if (row[1] != DBNull.Value)
        num = Convert.ToInt32(row[1]);
      if (this._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID == num)
        return row;
    }
    return (DataRow) null;
  }

  /// <summary> Суммирование одинаковых записей. Если одинаковые "Позиция" и "Кол" - постое объединение. Иначе - суммирование.  </summary>
  /// <param name="_listAll_IspolneniySp"></param>
  /// <returns></returns>
  public void ProcessingMainVed_Step2_SummOdinakovyh(
    List<Vedomost_VB.RecordForMainVed> listRecordsForMainVed)
  {
    int index = listRecordsForMainVed.Count - 1;
    while (index > 0)
    {
      Vedomost_VB.RecordForMainVed recordForMainVed1 = listRecordsForMainVed[index];
      Vedomost_VB.RecordForMainVed recordForMainVed2 = listRecordsForMainVed[index - 1];
      if (!this.CompareRecordsForMainVed_For_Processing_Step2(recordForMainVed1, recordForMainVed2))
      {
        --index;
      }
      else
      {
        if (!this.CompareRecordsForMainVed_For_Processing_Step2Dop(recordForMainVed1, recordForMainVed2))
          this.SummRrecordsForMainVed(recordForMainVed1, recordForMainVed2);
        listRecordsForMainVed.Remove(recordForMainVed2);
        --index;
      }
    }
  }

  /// <summary> Если это входит в головную СП, то КудаВходит делаем равным ИСПОЛНЕНИЕ </summary>
  /// <param name="_listAll_IspolneniySp"></param>
  public void ProcessingMainVed_Step3_MainIspToKuda(
    List<Vedomost_VB.RecordForMainVed> listRecordsForMainVed)
  {
    string designation = listRecordsForMainVed[0].Designation;
    for (int index = 0; index < listRecordsForMainVed.Count; ++index)
    {
      Vedomost_VB.RecordForMainVed recordForMainVed = listRecordsForMainVed[index];
      if (recordForMainVed.KudaDesignation == designation && recordForMainVed.Ispolnenie != "" && recordForMainVed.KudaDesignation != recordForMainVed.Ispolnenie)
        recordForMainVed.KudaDesignation = recordForMainVed.Ispolnenie;
    }
  }

  /// <summary> Создание "вторичных" записей (по входимости), Объединение одинаковых записей </summary>
  /// <param name="_listAll_IspolneniySp"></param>
  /// <returns></returns>
  public void ProcessingMainVed_Step5_CreateVtorRecords(
    List<Vedomost_VB.RecordForMainVed> listRecordsForMainVed)
  {
    int index1 = 0;
    for (int index2 = index1 + 1; index1 < listRecordsForMainVed.Count && index2 < listRecordsForMainVed.Count; index1 = index2)
    {
      Vedomost_VB.RecordForMainVed recordForMainVed1 = listRecordsForMainVed[index1];
      if (recordForMainVed1.List_recordForMainVedVtor == null)
      {
        recordForMainVed1.List_recordForMainVedVtor = new List<Vedomost_VB.RecordForMainVedVtor>();
        Vedomost_VB.RecordForMainVedVtor recordForMainVedVtor = new Vedomost_VB.RecordForMainVedVtor();
        recordForMainVedVtor.KudaDesignation = recordForMainVed1.KudaDesignation;
        recordForMainVed1.KudaDesignation = "";
        recordForMainVedVtor.CountS1 = recordForMainVed1.CountS;
        recordForMainVed1.CountS = "";
        recordForMainVedVtor.CountF1 = recordForMainVed1.CountF;
        recordForMainVed1.CountF = 0.0f;
        recordForMainVed1.List_recordForMainVedVtor.Add(recordForMainVedVtor);
      }
      index2 = index1 + 1;
      while (index2 < listRecordsForMainVed.Count)
      {
        Vedomost_VB.RecordForMainVed recordForMainVed2 = listRecordsForMainVed[index2];
        if (this.CompareRecordsForMainVed_For_Processing_Step5(recordForMainVed1, recordForMainVed2))
        {
          recordForMainVed1.List_recordForMainVedVtor.Add(new Vedomost_VB.RecordForMainVedVtor()
          {
            KudaDesignation = recordForMainVed2.KudaDesignation,
            CountS1 = recordForMainVed2.CountS,
            CountF1 = recordForMainVed2.CountF
          });
          listRecordsForMainVed.Remove(recordForMainVed2);
        }
        else
          break;
      }
    }
  }

  /// <summary> Суммирование </summary>
  /// <param name="_listAll_IspolneniySp"></param>
  /// <returns></returns>
  public void ProcessingMainVed_Step6_Summ(
    List<Vedomost_VB.RecordForMainVed> listRecordsForMainVed)
  {
    bool flag1 = true;
    bool flag2 = false;
    float num1 = 0.0f;
    while (flag1)
    {
      flag1 = false;
      int index1 = 0;
      while (index1 < listRecordsForMainVed.Count)
      {
        Vedomost_VB.RecordForMainVed recordForMainVed1 = listRecordsForMainVed[index1];
        string ispolnenie = recordForMainVed1.Ispolnenie;
        if (recordForMainVed1.Uroven == "1")
        {
          recordForMainVed1.CountSummF = 1f;
          recordForMainVed1.CountF = 1f;
          recordForMainVed1.CountS = "1";
          ++index1;
        }
        else if ((double) recordForMainVed1.CountSummF > 0.0)
        {
          ++index1;
        }
        else
        {
          if (recordForMainVed1.List_recordForMainVedVtor != null)
          {
            for (int index2 = 0; index2 < recordForMainVed1.List_recordForMainVedVtor.Count; ++index2)
            {
              Vedomost_VB.RecordForMainVedVtor recordForMainVedVtor = recordForMainVed1.List_recordForMainVedVtor[index2];
              if ((double) recordForMainVedVtor.CountFn <= 0.0)
              {
                float num2 = 0.0f;
                for (int index3 = 0; index3 < listRecordsForMainVed.Count; ++index3)
                {
                  Vedomost_VB.RecordForMainVed recordForMainVed2 = listRecordsForMainVed[index3];
                  if (recordForMainVedVtor.KudaDesignation == recordForMainVed2.Designation && (recordForMainVed1.IsTherezKomplekt == recordForMainVed2.IsTherezKomplekt || recordForMainVed1.EtaSp_Komplekt || recordForMainVed1.EstSvoiaVedomost) && recordForMainVed1.Ispolnenie == recordForMainVed2.Ispolnenie)
                  {
                    if ((double) recordForMainVed2.CountSummF > 0.0)
                    {
                      num2 += recordForMainVed2.CountSummF;
                    }
                    else
                    {
                      num2 = 0.0f;
                      break;
                    }
                  }
                }
                if ((double) num2 > 0.0 && (double) recordForMainVedVtor.CountF1 > 0.0)
                {
                  recordForMainVedVtor.CountFn = recordForMainVedVtor.CountF1 * num2;
                  flag1 = true;
                }
              }
            }
            flag2 = true;
            num1 = 0.0f;
            for (int index4 = 0; index4 < recordForMainVed1.List_recordForMainVedVtor.Count; ++index4)
            {
              Vedomost_VB.RecordForMainVedVtor recordForMainVedVtor = recordForMainVed1.List_recordForMainVedVtor[index4];
              if ((double) recordForMainVedVtor.CountFn > 0.0)
              {
                num1 += recordForMainVedVtor.CountFn;
                flag2 = true;
              }
              else
              {
                flag2 = false;
                break;
              }
            }
          }
          if (flag2)
          {
            recordForMainVed1.CountSummF = num1;
            flag1 = true;
          }
          ++index1;
        }
      }
    }
  }

  /// <summary> Для объекта objectId получить список ССЫЛОК </summary>
  /// 
  ///             Список ССЫЛОК, согласно пояснению Барабанщикова, привязан непосредственно к ОБЪЕКТУ, бе учета ГДЕ он применяется (21.08.2018)
  ///             <param name="objectId"></param>
  /// <param name="ListIdFields"></param>
  /// <returns></returns>
  public DataTable GetObjectReferences(
    long objectId,
    List<Vedomost_VB.OneFieldSpForRead> ListIdFields)
  {
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    for (int index = 0; index < ListIdFields.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead listIdField = ListIdFields[index];
      ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) (ObligatoryObjectAttributes) listIdField._id, listIdField._attributeSourceTypes, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0);
      columnDescriptorList.Add(columnDescriptor);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columnDescriptorList.ToArray());
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Reference);
      relationCollection.ObjectTypeID = -1;
      return relationCollection.ConsistFrom(paramSet, objectId);
    }
  }

  /// <summary> Чтение одной спецификации (изделия, исполнения) </summary>
  /// <param name="objectId"> objectId Изделия</param>
  /// <param name="ListIdFields"> Список читаемых полей</param>
  /// <param name="IsObjOrDoc"> ЧТО читать 1-Obj 2-Doc 3-DOC+Obj</param>
  /// <returns> DataTable спецификации</returns>
  public DataTable ReadOneSpecification(
    long ObjectId,
    List<Vedomost_VB.OneFieldSpForRead> ListIdFields,
    int IsObjOrDoc)
  {
    DataTable dataTable = (DataTable) null;
    DataTable table1 = (DataTable) null;
    DataTable table2 = (DataTable) null;
    DataTable table3 = (DataTable) null;
    if (IsObjOrDoc < 1 || IsObjOrDoc > 3)
      return (DataTable) null;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    for (int index = 0; index < ListIdFields.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead listIdField = ListIdFields[index];
      ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) (ObligatoryObjectAttributes) listIdField._id, listIdField._attributeSourceTypes, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0);
      columnDescriptorList.Add(columnDescriptor);
    }
    using (SessionKeeper sk = new SessionKeeper())
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columnDescriptorList.ToArray());
      if (IsObjOrDoc == 1 || IsObjOrDoc == 3)
      {
        IDBRelationCollection relationCollection1 = sk.Session.GetRelationCollection(AvsIDCache.Relation_Project);
        relationCollection1.ObjectTypeID = -1;
        table1 = relationCollection1.ConsistFrom(paramSet, ObjectId);
        IDBRelationCollection relationCollection2 = sk.Session.GetRelationCollection(AvsIDCache.Relation_Podbor);
        relationCollection2.ObjectTypeID = -1;
        table2 = relationCollection2.ConsistFrom(paramSet, ObjectId);
        IDBRelationCollection relationCollection3 = sk.Session.GetRelationCollection(AvsIDCache.Relation_Zagotovka);
        relationCollection3.ObjectTypeID = -1;
        table3 = relationCollection3.ConsistFrom(paramSet, ObjectId);
      }
      if (IsObjOrDoc == 2 || IsObjOrDoc == 3)
      {
        IDBRelationCollection relationCollection = sk.Session.GetRelationCollection(AvsIDCache.Relation_Document);
        relationCollection.ObjectTypeID = AvsIDCache.ObjType_Document;
        dataTable = relationCollection.ConsistFrom(paramSet, ObjectId);
      }
      if (IsObjOrDoc == 3)
      {
        dataTable.Merge(table1);
        dataTable.Merge(table2);
        dataTable.Merge(table3);
      }
      if (IsObjOrDoc == 3)
        this.Add_Dates_from_Doc(dataTable, sk);
    }
    return IsObjOrDoc == 2 || IsObjOrDoc == 3 ? dataTable : table1;
  }

  public void Add_Dates_from_Doc(DataTable dataTable, SessionKeeper sk)
  {
    if (dataTable == null)
      return;
    long num = -1;
    int columnIndex1 = this.ItemIndexForID(this.listCommonId._listCommonId, -2);
    int columnIndex2 = this.ItemIndexForID(this.listCommonId._listCommonId, -7);
    int columnIndex3 = this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_Designation);
    int columnIndex4 = this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_Name);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long objectIdIzd = -1;
      num = -1L;
      if (row[columnIndex3] != DBNull.Value)
        Convert.ToString(row[columnIndex3]);
      if (row[columnIndex4] != DBNull.Value)
        Convert.ToString(row[columnIndex4]);
      if (row[columnIndex2] != DBNull.Value)
        Convert.ToInt32(row[columnIndex2]);
      for (int index = 0; index < this.listCommonId._listCommonId.Count; ++index)
      {
        object obj = row[index];
        if (obj != null && string.IsNullOrEmpty(obj.ToString()))
        {
          if (row[columnIndex1] != DBNull.Value)
            objectIdIzd = Convert.ToInt64(row[columnIndex1]);
          if (objectIdIzd != 0L && objectIdIzd != -1L)
          {
            long idDocByObjectIzd = Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(objectIdIzd, sk);
            if (idDocByObjectIzd != 0L)
            {
              Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.listCommonId._listCommonId[index];
              string fieldForObjectId = this.Get_OneField_ForObjectId(idDocByObjectIzd, oneFieldSpForRead._id, sk);
              if (!string.IsNullOrEmpty(fieldForObjectId))
                row[index] = (object) fieldForObjectId;
            }
          }
        }
      }
    }
  }

  /// <summary> Сравнение для объединения записей в одну запись </summary>
  /// <param name="recordForMainVed1"></param>
  /// <param name="recordForMainVed2"></param>
  /// <returns></returns>
  public bool CompareRecordsForMainVed_For_Processing_Step2(
    Vedomost_VB.RecordForMainVed recordForMainVed1,
    Vedomost_VB.RecordForMainVed recordForMainVed2)
  {
    return recordForMainVed1 != null && recordForMainVed2 != null && recordForMainVed1.IsTherezKomplekt == recordForMainVed2.IsTherezKomplekt && recordForMainVed1.IsTherezDopZam == recordForMainVed2.IsTherezDopZam && string.Compare(recordForMainVed1.Designation, recordForMainVed2.Designation, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.KudaDesignation, recordForMainVed2.KudaDesignation, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.Ispolnenie, recordForMainVed2.Ispolnenie, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.Remark, recordForMainVed2.Remark, StringComparison.Ordinal) == 0;
  }

  /// <summary> Сравнение для объединения записей в одну запись </summary>
  /// <param name="recordForMainVed1"></param>
  /// <param name="recordForMainVed2"></param>
  /// <returns></returns>
  public bool CompareRecordsForMainVed_For_Processing_Step2Dop(
    Vedomost_VB.RecordForMainVed recordForMainVed1,
    Vedomost_VB.RecordForMainVed recordForMainVed2)
  {
    return recordForMainVed1 != null && recordForMainVed2 != null && string.Compare(recordForMainVed1.Position, recordForMainVed2.Position, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.CountS, recordForMainVed2.CountS, StringComparison.Ordinal) == 0;
  }

  /// <summary> Сравнение для формирования вторичных записей </summary>
  /// <param name="recordForMainVed1"></param>
  /// <param name="recordForMainVed2"></param>
  /// <returns></returns>
  public bool CompareRecordsForMainVed_For_Processing_Step5(
    Vedomost_VB.RecordForMainVed recordForMainVed1,
    Vedomost_VB.RecordForMainVed recordForMainVed2)
  {
    return recordForMainVed1 != null && recordForMainVed2 != null && string.Compare(recordForMainVed1.Designation, recordForMainVed2.Designation, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.Ispolnenie, recordForMainVed2.Ispolnenie, StringComparison.Ordinal) == 0 && recordForMainVed1.IsTherezKomplekt == recordForMainVed2.IsTherezKomplekt && recordForMainVed1.IsTherezDopZam == recordForMainVed2.IsTherezDopZam && string.Compare(recordForMainVed1.Remark, recordForMainVed2.Remark, StringComparison.Ordinal) == 0;
  }

  /// <summary> Объединение (СУММИРОВАНИЕ) двух записей в первую запись </summary>
  /// <param name="recordForMainVed1"></param>
  /// <param name="recordForMainVed2"></param>
  /// <returns></returns>
  public bool SummRrecordsForMainVed(
    Vedomost_VB.RecordForMainVed recordForMainVed1,
    Vedomost_VB.RecordForMainVed recordForMainVed2)
  {
    if (recordForMainVed1 == null || recordForMainVed2 == null)
      return false;
    float num = recordForMainVed1.CountF + recordForMainVed2.CountF;
    recordForMainVed1.CountF = num;
    recordForMainVed1.CountS = $"{num.ToString()} {recordForMainVed1.EdIzmKol}";
    return true;
  }

  /// <summary> Сравнение для объединения ВСЕХ исполнений в одну ОБЩУЮ запись </summary>
  /// <param name="recordForMainVed1"></param>
  /// <param name="recordForMainVed2"></param>
  /// <returns></returns>
  public bool CompareRecordsForMainVed3(
    Vedomost_VB.RecordForMainVed recordForMainVed1,
    Vedomost_VB.RecordForMainVed recordForMainVed2)
  {
    return recordForMainVed1 != null && recordForMainVed2 != null && string.Compare(recordForMainVed1.Designation, recordForMainVed2.Designation, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.KudaDesignation, recordForMainVed2.KudaDesignation, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.CountS, recordForMainVed2.CountS, StringComparison.Ordinal) == 0 && string.Compare(recordForMainVed1.Remark, recordForMainVed2.Remark, StringComparison.Ordinal) == 0;
  }

  /// <summary> Сбор Ved в целом</summary>
  public void Create_Ved()
  {
    XmlElement xmlElement_Kuda = (XmlElement) null;
    if (Vedomost_VB_Static.isCreateDump_Tmp)
      (this.xml_SborVed_Dump, xmlElement_Kuda) = this.Ved_Dump_Create(Vedomost_VB_Static.DirectoryDump, "SborVed_Dump.xml", this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ObjectName);
    if (this.sborVedTask != null)
      this.sborVedTask.Dispose();
    this.sborVedTask = new SborVedTask("Сбор данных из спецификаций");
    this.sborVedTask.Show();
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.VSI)
    {
      if (this._listRecordsVed_New == null)
        this._listRecordsVed_New = new List<Vedomost_VB.RecordForVed_New>();
      else
        this._listRecordsVed_New.Clear();
      this.CreateVed_Step1_Sbor(this._listRecordsForMainVed);
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VD || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VDE)
        this.CreateVed_Step2_SborRecurive_VD(this._listRecordsVed_New);
    }
    this.txtProtocol_Add("---------------------------------------------------------------");
    this.txtProtocol_Add("ЭТАПЫ ОБРАБОТКИ ВЕДОМОСТИ");
    if (this.xml_SborVed_Dump != null)
      this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step001_Сразу после сбора");
    Vedomost_VB vedomost_VB_General = (Vedomost_VB) null;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI)
    {
      this.txtProtocol_Add("");
      this.txtProtocol_Add("---------------------------------------------------------------");
      this.txtProtocol_Add("ЭТАПЫ ОБРАБОТКИ vedomost_VB_General");
      this.txtProtocol_Add("");
      this.txtProtocol_Add("General Заходим в Create_vedomost_VB_General");
      vedomost_VB_General = this.Create_vedomost_VB_General();
      this.txtProtocol_Add("");
      this.txtProtocol_Add("General Вышли из Create_vedomost_VB_General");
      this.txtProtocol_Add("---------------------------------------------------------------");
      this.txtProtocol_Add("");
    }
    this.isPeremDannye = false;
    if (this._isGroupVed && this._variables_Coordination.list_Variables.Count > 1)
    {
      this.txtProtocol_Add("Ведомость групповая");
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSortGroup)
      {
        this.txtProtocol_Add("Сортировка 1");
        this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this._compareRecordsVed_step0);
        if (this.xml_SborVed_Dump != null)
          this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step002_После сортировки для выделения общей части");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedMergerIsp)
      {
        this.txtProtocol_Add("Выделение ОБЩЕЙ части");
        this.Merger_Ved_ispolneniy();
        if (this.xml_SborVed_Dump != null)
          this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step003_После выделения записей общей части");
      }
      this.txtProtocol_Add("Групповая обработка завершена");
    }
    this.Check_FuncGroup();
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedAddFuncGroup)
    {
      this.txtProtocol_Add("Дополнение функциональных групп");
      this.Addition_FuncGroup();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step004 Дополнение поля Функциональная группа исходя из раздела ведомости");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSort1)
    {
      this.txtProtocol_Add("Сортировка 2. По настройке");
      this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this);
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step005_После первой сортировки Исполнение Функцгруппа Обозначение Наименование");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedUnion)
    {
      this.txtProtocol_Add("Объединение однородных записей");
      this.Union_Records_Ved_1();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step006_После объединения однородных записей в тч основных и ЧЕРЕЗ комплекты");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
    {
      this.txtProtocol_Add("Ведомость держателй подлинников. Суммирование по основному держателю подлинников");
      this.Summ_for_DP();
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedExtrectionVtor)
    {
      this.txtProtocol_Add("Выделение по одной вторичной записи из основной (Пока только по одной вторичной записи)");
      this.Extrection_Ved_Vtor1();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step007_После выделения вторичных записей");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedMergerVtor)
    {
      this.txtProtocol_Add("Объединение записей с одинаковой первичной");
      this.Merger_Ved_Vtor();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step008_После Объединения записей с одинаковой первичной");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSortVtor)
    {
      this.txtProtocol_Add("Сортировка вторичных записей");
      this.Sort_Ved_Vtor();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step009_После сортировки вторичных записей");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSummVtor)
    {
      this.txtProtocol_Add("Суммирование ВСЕГО и СУММА");
      this.Summ_VedVtor();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step010_После расчета Всего и Сумма");
    }
    if (this._groupForm == Vedomost_VB.FormaGroup.B)
    {
      this.txtProtocol_Add("Преобразование в форму Б");
      this.ConvertGrAToGrB(true);
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step011_Конвертировано в форму Б");
      this.txtProtocol_Add("Сортировка формы Б");
      this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this);
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step012_Форма Б Сортировано");
    }
    else if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedCreateZagolIspoln && this._isGroupVed && this._variables_Coordination.list_Variables.Count > 1)
    {
      this.txtProtocol_Add("Создание заголовков исполнений");
      this.Create_Ved_Zagol_Ispoln();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step013 Форма А Созданы заголовки исполнений");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedCreateZagolSvoiaVed)
    {
      this.txtProtocol_Add("Создание заголовка ВХОДЯЩИЕ ВЕДОМОСТИ");
      this.Create_Ved_Zagol_SvoiaVed();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step014_Создан заголовок Ведомости составных частей");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedCreateZagolPoPriznaku)
    {
      this.txtProtocol_Add("Создание заголовков ПО ПРИЗНАКУ");
      this.Create_Ved_Zagol_PoPriznaku();
      if (this.xml_SborVed_Dump != null)
        this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step015_Созданы заголовки разделов");
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI && vedomost_VB_General != null)
    {
      this.txtProtocol_Add("Заносим Кол в изделии из vedomost_VB_General");
      Vedomost_VB_Static.ZI_Processing_Count(this._listRecordsVed_New, this._one_Ved_Nastr_RazrabatyvaemoiVed, vedomost_VB_General);
    }
    if (this._groupForm == Vedomost_VB.FormaGroup.A)
    {
      if (this.isPeremDannye)
      {
        this.txtProtocol_Add(" Если нет каких то исполнений, то вставляются записи Отсутствуют");
        this.Ispolneniye_Otsutstvuet();
        if (this.xml_SborVed_Dump != null)
          this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step016_Созданы заголовки пустых исполнений и записи ОТСУТСТВУЮТ");
      }
      if (!this.isPeremDannye)
      {
        this.txtProtocol_Add("Запись Различие исполнений по чертежу (Если необходимо))");
        this.Razlithie_ispolneniy();
        if (this.xml_SborVed_Dump != null)
          this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step016_Создана Различие исполнений по чертежу (Если необходимо))");
      }
    }
    if (this.xml_SborVed_Dump != null)
      this.Ved_Dump_Add_Step(this.xml_SborVed_Dump, xmlElement_Kuda, "Step018_____ВСЕ___ВЕДОМОСТЬ ГОТОВА");
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
    }
    if (this._listRecordsForMainVed_DopZam != null && this._listRecordsForMainVed_DopZam.Count > 0)
    {
      this.txtProtocol_Add("Сбор данных из спецификаций для допустимых замен");
      this._etap_Sbora_DopZam = true;
      this.Create_Ved_DopZam();
    }
    if (Vedomost_VB_Static.isCreateDump_Tmp)
    {
      if (this.xml_SborVed_Dump != null)
      {
        this.xml_SborVed_Dump.Save(Vedomost_VB_Static.DirectoryDump + "\\SborVed_Dump.xml");
        this.xml_SborVed_Dump = (XmlDocument) null;
      }
      this.txtProtocol_Add("Ved_XML_File_Create_From_ListRecordsVed_New");
      this.Ved_XML_File_Create_From_ListRecordsVed_New();
    }
    this.txtProtocol_Add("Разбиение МНОГОСТРОЧНЫХ записей");
    this.Splitting_Multilines();
    this.txtProtocol_Add("_listError_OneError.Sort");
    this._listError_OneError.Sort();
    this.txtProtocol_Add("_listError_OneError.Union");
    this._listError_OneError.Union();
    this.txtProtocol_Add("Создание ДОКУМЕНТА");
    if (this._listRecordsVed_New.Count == 0)
    {
      this.txtProtocol_Add("ВЕДОМОСТЬ ПУСТАЯ. ЗАПИСЕЙ НЕТ !!!!!!!!!!!!!!!!!!!");
      int num = (int) MessageBox.Show("Ведомость пустая\r\nДанных, соответствующих настройке, не найдено", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    this.CreateAndOpenNewDocument(false, false, false);
  }

  /// <summary> Сбор данных из спецификаций для допустимых замен </summary>
  public void Create_Ved_DopZam()
  {
    XmlElement xmlElement_Kuda = (XmlElement) null;
    this._listRecordsForMainVed = this._listRecordsForMainVed_DopZam;
    List<Vedomost_VB.RecordForVed_New> recordForVedNewList = new List<Vedomost_VB.RecordForVed_New>();
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
      recordForVedNewList.Add(recordForVedNew);
    }
    XmlDocument xmlDocument = (XmlDocument) null;
    if (this.sborVedTask != null)
      this.sborVedTask.Dispose();
    this.sborVedTask = new SborVedTask("Сбор данных из спецификаций для допустимых замен");
    this.sborVedTask.Show();
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.VSI)
    {
      if (this._listRecordsVed_New == null)
        this._listRecordsVed_New = new List<Vedomost_VB.RecordForVed_New>();
      else
        this._listRecordsVed_New.Clear();
      this.CreateVed_Step1_Sbor(this._listRecordsForMainVed);
    }
    if (this._listRecordsVed_New == null || this._listRecordsVed_New.Count < 1)
    {
      this._listRecordsVed_New = recordForVedNewList;
    }
    else
    {
      this.txtProtocol_Add("");
      this.txtProtocol_Add("--- ДОПУСТИМЫЕ ЗАМЕНЫ --------------------------------------------------");
      this.txtProtocol_Add("ЭТАПЫ ОБРАБОТКИ ВЕДОМОСТИ ДОПУСТИМЫЕ ЗАМЕНЫ");
      if (Vedomost_VB_Static.isCreateDump_Tmp)
        (xmlDocument, xmlElement_Kuda) = this.Ved_Dump_Create(Vedomost_VB_Static.DirectoryDump, "SborVed_Dump_DopZam.xml", this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ObjectName);
      Vedomost_VB vedomost_VB_General = (Vedomost_VB) null;
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI)
        vedomost_VB_General = this.Create_vedomost_VB_General();
      this.isPeremDannye = false;
      if (this._isGroupVed && this._variables_Coordination.list_Variables.Count > 1)
      {
        this.txtProtocol_Add("Ведомость групповая");
        if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSortGroup)
        {
          this.txtProtocol_Add("Сортировка 1");
          this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this._compareRecordsVed_step0);
          if (xmlDocument != null)
            this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step002DopZam_После сортировки для выделения общей части");
        }
        if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedMergerIsp)
        {
          this.txtProtocol_Add("Выделение ОБЩЕЙ части");
          this.Merger_Ved_ispolneniy();
          if (xmlDocument != null)
            this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step003DopZam_После выделения записей общей части");
        }
        this.txtProtocol_Add("Групповая обработка завершена");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedAddFuncGroup)
      {
        this.txtProtocol_Add("Дополнение функциональных групп");
        this.Addition_FuncGroup();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step004DopZam_Дополнение поля Функциональная группа исходя из раздела ведомости");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSort1)
      {
        this.txtProtocol_Add("Сортировка 2. По настройке");
        this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this);
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step005DopZam_После первой сортировки Исполнение Функцгруппа Обозначение Наименование");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedUnion)
      {
        this.txtProtocol_Add("Объединение однородных записей");
        this.Union_Records_Ved_1();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step006DopZam_После объединения однородных записей в тч основных и ЧЕРЕЗ комплекты");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
      {
        this.txtProtocol_Add("Ведомость держателй подлинников. Суммирование по основному держателю подлинников");
        this.Summ_for_DP();
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedExtrectionVtor)
      {
        this.txtProtocol_Add("Выделение по одной вторичной записи из основной (Пока только по одной вторичной записи)");
        this.Extrection_Ved_Vtor1();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step007DopZam_После выделения вторичных записей");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedMergerVtor)
      {
        this.txtProtocol_Add("Объединение записей с одинаковой первичной");
        this.Merger_Ved_Vtor();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step008DopZam_После Объединения записей с одинаковой первичной");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSortVtor)
      {
        this.txtProtocol_Add("Сортировка вторичных записей");
        this.Sort_Ved_Vtor();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step009DopZam_После сортировки вторичных записей");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedSummVtor)
      {
        this.txtProtocol_Add("Суммирование ВСЕГО и СУММА");
        this.Summ_VedVtor();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step010DopZam_После расчета Всего и Сумма");
      }
      if (this._groupForm == Vedomost_VB.FormaGroup.B)
      {
        this.txtProtocol_Add("Преобразование в форму Б");
        this.ConvertGrAToGrB(true);
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step011DopZam_Конвертировано в форму Б");
        this.txtProtocol_Add("Сортировка формы Б");
        this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this);
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step012DopZam_Форма Б Сортировано");
      }
      else if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedCreateZagolIspoln && this._isGroupVed && this._variables_Coordination.list_Variables.Count > 1)
      {
        this.txtProtocol_Add("Создание заголовков исполнений");
        this.Create_Ved_Zagol_Ispoln();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step013DopZam Форма А Созданы заголовки исполнений");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedCreateZagolSvoiaVed)
      {
        this.txtProtocol_Add("Создание заголовка ВХОДЯЩИЕ ВЕДОМОСТИ");
        this.Create_Ved_Zagol_SvoiaVed();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step014DopZam_Создан заголовок Ведомости составных частей");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isVedCreateZagolPoPriznaku)
      {
        this.txtProtocol_Add("Создание заголовков ПО ПРИЗНАКУ");
        this.Create_Ved_Zagol_PoPriznaku();
        if (xmlDocument != null)
          this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step015DopZam_Созданы заголовки разделов");
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI && vedomost_VB_General != null)
        Vedomost_VB_Static.ZI_Processing_Count(this._listRecordsVed_New, this._one_Ved_Nastr_RazrabatyvaemoiVed, vedomost_VB_General);
      if (this._groupForm == Vedomost_VB.FormaGroup.A)
      {
        if (this.isPeremDannye)
        {
          this.txtProtocol_Add(" Если нет каких то исполнений, то вставляются записи Отсутствуют");
          this.Ispolneniye_Otsutstvuet();
          if (xmlDocument != null)
            this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step016DopZam_Созданы заголовки пустых исполнений и записи ОТСУТСТВУЮТ");
        }
        if (!this.isPeremDannye)
        {
          this.txtProtocol_Add("Запись Различие исполнений по чертежу (Если необходимо))");
          this.Razlithie_ispolneniy();
          if (xmlDocument != null)
            this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step016DopZam_Создана Различие исполнений по чертежу (Если необходимо))");
        }
      }
      if (xmlDocument != null)
        this.Ved_Dump_Add_Step(xmlDocument, xmlElement_Kuda, "Step018DopZam_____ВСЕ___ВЕДОМОСТЬ ГОТОВА");
      if (Vedomost_VB_Static.isCreateDump_Tmp && xmlDocument != null)
        xmlDocument.Save(Vedomost_VB_Static.DirectoryDump + "\\SborVed_Dump_DopZam.xml");
      this.txtProtocol_Add("--- ДОПУСТИМЫЕ ЗАМЕНЫ ЗАВЕРШЕНЫ ---------------------------");
      this.txtProtocol_Add("");
      if (this._listRecordsVed_New.Count > 0)
      {
        Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
        recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Title;
        recordForVedNew.FromNewPage = true;
        string name = "Допустимые замены";
        recordForVedNew.Set_Name(name);
        recordForVedNewList.Add(recordForVedNew);
      }
      for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
      {
        Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
        recordForVedNewList.Add(recordForVedNew);
      }
      this._listRecordsVed_New = recordForVedNewList;
    }
  }

  /// <summary> Создание vedomost_VB_General ведомость для подсчета "Кол в изделии"</summary>
  /// <returns></returns>
  private Vedomost_VB Create_vedomost_VB_General()
  {
    this.txtProtocol_Add("General Создание vedomost_VB_General ведомость для подсчета Кол в изделии");
    Vedomost_VB vedomost_VB_General = (Vedomost_VB) null;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI)
    {
      vedomost_VB_General = Vedomost_VB_Static.Create_General_Ved(this._designationArticle, this._nameArticle, this._iDSP);
      vedomost_VB_General._txtProtocol = this._txtProtocol;
      vedomost_VB_General._prodInfo = this._prodInfo;
      this.txtProtocol_Add("General Входим в vedomost_VB_General(_iDSP)");
      vedomost_VB_General.Create_General_Ved(this._iDSP);
      this.txtProtocol_Add("General Вышли из vedomost_VB_General(_iDSP)");
      if (vedomost_VB_General != null)
      {
        this.txtProtocol_Add("General vedomost_VB_General не null");
        this.txtProtocol_Add("General Заходим в ZI_Processing_Razd");
        Vedomost_VB_Static.ZI_Processing_Razd(this._listRecordsVed_New, this._one_Ved_Nastr_RazrabatyvaemoiVed, vedomost_VB_General);
        this.txtProtocol_Add("General Вышли из ZI_Processing_Razd");
      }
      else
        this.txtProtocol_Add("General vedomost_VB_General null");
    }
    this.txtProtocol_Add("General Заканчивается Create_vedomost_VB_General");
    return vedomost_VB_General;
  }

  /// <summary> Сбор конкретной ведомости. шаг 1 </summary>
  /// <returns></returns>
  public bool CreateVed_Step1_Sbor(
    List<Vedomost_VB.RecordForMainVed> listRecordsForMainVed)
  {
    int num1 = 1;
    if (listRecordsForMainVed == null || listRecordsForMainVed.Count < 1)
      return false;
    Vedomost_VB.RecordForMainVed recordForMainVed1 = listRecordsForMainVed[0];
    Vedomost_VB.UrovniI[0] = 0;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isSamuSP_ne_iz_spiska_zanosit && !this._etap_Sbora_DopZam)
    {
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
        this.Create_RecordForVed_New_Main_By_DP(recordForMainVed1);
      else
        this.CreateRecordVed_FromMainSpecification(recordForMainVed1);
    }
    this.txtProtocol_Add("");
    this.txtProtocol_Add(" ----------------------------------- ЦИКЛ ЧТЕНИЯ listRecordsForMainVed -------------");
    this.txtProtocol_Add("");
    for (int index1 = 0; index1 < listRecordsForMainVed.Count; ++index1)
    {
      this.txtProtocol_Add("Чтение recordForVed_New_Old i=" + index1.ToString());
      Vedomost_VB.RecordForMainVed recordForMainVed2 = listRecordsForMainVed[index1];
      if (recordForMainVed2 == null)
      {
        this.txtProtocol_Add("ОШИБКА. recordForVed_New_Old==null. i=" + index1.ToString());
      }
      else
      {
        this.txtProtocol_Add("Прочитано recordForVed_New_Old i=" + index1.ToString());
        this.txtProtocol_Add("recordForVed_New_Old.Designation: " + recordForMainVed2.Designation);
        this.txtProtocol_Add("UrovenCurr=" + recordForMainVed2.UrovenN.ToString());
        if (!recordForMainVed2.NeRaskryvat)
        {
          int urovenN = recordForMainVed2.UrovenN;
          this._urovenN = recordForMainVed2.UrovenN;
          if (urovenN == num1)
          {
            int num2 = Vedomost_VB.UrovniI[urovenN - 1] + 1;
            Vedomost_VB.UrovniI[urovenN - 1] = num2;
          }
          if (urovenN > num1)
            Vedomost_VB.UrovniI[urovenN - 1] = 1;
          if (urovenN < num1)
          {
            for (int index2 = 4; index2 > urovenN - 1; --index2)
            {
              Vedomost_VB.UrovniI[index2] = 0;
              int num3 = Vedomost_VB.UrovniI[urovenN - 1] + 1;
              Vedomost_VB.UrovniI[urovenN - 1] = num3;
            }
          }
          num1 = urovenN;
          if (!recordForMainVed2.EstSvoiaVedomost)
          {
            if (recordForMainVed2.PartsDoc == null)
            {
              this.txtProtocol_Add($"ОШИБКА. recordForVed_New_Old.PartsDoc==null. i={index1.ToString()}  {recordForMainVed2.Designation}");
            }
            else
            {
              if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI && !this._etap_Sbora_DopZam)
              {
                if (recordForMainVed2.IsTherezKomplekt)
                {
                  if (recordForMainVed2.EtaSp_Komplekt)
                  {
                    if (!this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isFor_ZIP_COMPL_Raskr)
                      continue;
                  }
                  else if (!this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isFor_ZIP_SB_Raskr)
                    continue;
                }
                else if (!recordForMainVed2.EtaSp_Komplekt)
                  continue;
              }
              this.txtProtocol_Add("Обращаемся к CreateRecordsVed_FromOneSpecification(recordForVed_New_Old)");
              this.CreateRecordsVed_FromOneSpecification(recordForMainVed2);
            }
          }
          else if (!this._etap_Sbora_DopZam)
          {
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
            {
              if (this._derzPodl == recordForMainVed2.DerzPodl)
                this.CreateRecord_SvoiaVedomost_FromOneSpecification(recordForMainVed2);
            }
            else
            {
              this.txtProtocol_Add("CreateRecord_SvoiaVedomost_FromOneSpecification");
              this.CreateRecord_SvoiaVedomost_FromOneSpecification(recordForMainVed2);
            }
          }
        }
      }
    }
    return true;
  }

  /// <summary> Поиск ссылок у самИх ссылочных документов ТОЛЬКО ДЛЯ ВЕДОМОСТИ ССЫЛОЧНЫХ ДОКУМЕНТОВ </summary>
  /// <param name="listRecordsVed_New"></param>
  /// <returns></returns>
  public bool CreateVed_Step2_SborRecurive_VD(
    List<Vedomost_VB.RecordForVed_New> listRecordsVed_New)
  {
    if (listRecordsVed_New == null || listRecordsVed_New.Count < 1)
      return true;
    List<Vedomost_VB.RecordForVed_New> recordForVedNewList = new List<Vedomost_VB.RecordForVed_New>();
    for (int index = 0; index < listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = listRecordsVed_New[index].RecordForVed_New_Copy(0, 0, false);
      recordForVedNewList.Add(recordForVedNew);
    }
    using (SessionKeeper sk = new SessionKeeper())
    {
      for (int index1 = 0; index1 < recordForVedNewList.Count; ++index1)
      {
        long objectId = recordForVedNewList[index1].Get_ObjectID();
        List<Vedomost_VB.Reference> referenceList = this.List_References_Create(objectId, objectId, sk);
        if (referenceList != null && referenceList.Count > 0)
        {
          for (int index2 = 0; index2 < referenceList.Count; ++index2)
          {
            Vedomost_VB.Reference reference = referenceList[index2];
            Vedomost_VB.RecordForVed_New referenceForDpAndVd = this.Create_record_Reference_For_DP_and_VD((Vedomost_VB.RecordForMainVed) null, reference._objectIdParent, reference._objectId_For_Reference, sk);
            if (referenceForDpAndVd != null)
            {
              referenceForDpAndVd.Removal_of_duplicates();
              this._listRecordsVed_New.Add(referenceForDpAndVd);
            }
          }
        }
      }
    }
    return true;
  }

  /// <summary> Сбор Ved. шаг 1 </summary>
  /// <returns></returns>
  public bool CreateVed_Step1_Sbor_VSI()
  {
    Vedomost_VB.UrovniI[0] = 0;
    this._objectTypeMainSp = this._prodInfo.ObjectType;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._is_Vydeliat_Therez_Komplekty && this._objectTypeMainSp == AvsIDCache.ObjType_Complect)
      this._is_Golovnaia_Sp_Komplekt = true;
    int num1;
    int num2;
    if (this._isGroupSp)
    {
      if (this._isGroupVed)
      {
        num1 = 0;
        num2 = this._variables_Coordination.list_Variables.Count;
      }
      else
      {
        num1 = this._i_vybranogo_Ispolnenia;
        num2 = num1 + 1;
      }
    }
    else
    {
      num1 = 0;
      num2 = 1;
      this._isGroupVed = false;
    }
    this._listRecordsForMainVed_VSI = new List<Vedomost_VB.RecordForMainVed>();
    this._listSvoiaVedomost_VSI = new List<Vedomost_VB.RecordForMainVed>();
    this.txtProtocol_Add("");
    this.txtProtocol_Add("-----------------------------------------------------");
    this.txtProtocol_Add("СБОР ДАННЫХ ИЗ СПЕЦИФИКАЦИЙ");
    for (int index = num1; index < num2; ++index)
    {
      string Ispolnenie = "";
      ProductInfo productInfo = this._listAll_IspolneniySp_prodInfo[index];
      DataTable dataTable = this.ReadOneSpecification(productInfo.Id, this.listCommonId._listCommonId, 3);
      if (dataTable == null)
        return false;
      this._listSpecifications.Add(new Vedomost_VB.OneSpecification(productInfo.Id, productInfo.ObjectType, productInfo.Designation, dataTable));
      Vedomost_VB.RecordForMainVed RecordForMainVedPrevision = new Vedomost_VB.RecordForMainVed((DataRow) null, productInfo.Designation, productInfo.Id, false, false, "1", this);
      RecordForMainVedPrevision.PartsDoc = dataTable;
      RecordForMainVedPrevision.UrovenN = this._urovenN;
      RecordForMainVedPrevision.Name = this._nameArticle;
      if (this._isGroupVed)
      {
        RecordForMainVedPrevision.Ispolnenie = productInfo.Designation;
        Ispolnenie = productInfo.Designation;
      }
      if (RecordForMainVedPrevision.Ispolnenie == null)
        RecordForMainVedPrevision.Ispolnenie = "";
      this._listRecordsForMainVed.Add(RecordForMainVedPrevision);
      string designation = this._prodInfo.Designation;
      long id = this._prodInfo.Id;
      this.ProcessingOneSpecification(dataTable, this.listCommonId._listCommonId, designation, false, id, "1", Ispolnenie, RecordForMainVedPrevision, true);
    }
    this.txtProtocol_Add("Закончен сбор данных из спецификаций");
    this.txtProtocol_Add("=====================================================");
    this.txtProtocol_Add("");
    return true;
  }

  /// <summary> Создание записи о ГОЛОВНОЙ спецификации </summary>
  public bool CreateRecordVed_FromMainSpecification(Vedomost_VB.RecordForMainVed recordForMainVed)
  {
    Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New((DataRow) null, (Vedomost_VB.RecordForMainVed) null, recordForMainVed.Uroven, this);
    recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Main;
    recordForVedNew.Set_ObjectID(recordForMainVed.ObjectIdIzd);
    recordForVedNew.Set_Designation(this._designationArticle);
    recordForVedNew.Set_Name(this._nameArticle);
    recordForVedNew.KudaDesignation = "";
    recordForVedNew.KudaObjectId = 0L;
    recordForVedNew.CountF_samOi_sp = 1f;
    recordForVedNew.Count_in_Izdelie = 0.0f;
    recordForVedNew.Ispolnenie = "";
    recordForVedNew.IsTherezKomplekt = false;
    recordForVedNew.IsTherezDopZam = false;
    recordForVedNew.Count_in_Sp = 1f;
    recordForVedNew.Count_in_Sp_S = "1";
    recordForVedNew.Count_in_Izdelie = 1f;
    recordForVedNew.Razdel_Ved = 1L;
    recordForVedNew.Removal_of_duplicates();
    this._listRecordsVed_New.Add(recordForVedNew);
    this.txtProtocol_Add("Занесена запись о самОй головной спецификации");
    this.txtProtocol_Add($"{this._designationArticle} : {this._nameArticle}");
    return true;
  }

  /// <summary> Головная запись ведомости держателей подлинников </summary>
  /// <param name="recordForMainVed"></param>
  /// <returns></returns>
  public bool Create_RecordForVed_New_Main_By_DP(Vedomost_VB.RecordForMainVed recordForMainVed)
  {
    if (recordForMainVed == null)
      return false;
    Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
    recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Main;
    recordForVedNew.KudaObjectId = recordForMainVed.ObjectIdIzd;
    recordForVedNew.CountF_samOi_sp = recordForMainVed.CountSummF;
    recordForVedNew.UrovenS = "0";
    if (recordForMainVed.Ispolnenie != null)
      recordForVedNew.Ispolnenie = recordForMainVed.Ispolnenie;
    recordForVedNew.Set_Designation(recordForMainVed.Designation);
    long num1 = recordForMainVed.ObjectIdIzd;
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE || this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isInputDoc && !this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isInputIzd)
      {
        long idDocByObjectIzd = Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(num1, sk);
        if (idDocByObjectIzd != -1L)
          num1 = idDocByObjectIzd;
      }
      IDBObject dbObject = sk.Session.GetObject(num1, false);
      if (dbObject == null)
        return false;
      AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
      long num2 = -1;
      for (int index1 = 0; index1 < this.listCommonId._listCommonId.Count; ++index1)
      {
        string data1 = "";
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.listCommonId._listCommonId[index1];
        string name = oneFieldSpForRead._name;
        MetaDataHelper.GetAttributeTypeName(oneFieldSpForRead._id);
        if (oneFieldSpForRead._attributeSourceTypes == AttributeSourceTypes.Object)
        {
          for (int index2 = 0; index2 < attributesValues.Length; ++index2)
          {
            MetaDataHelper.GetAttributeTypeName(attributesValues[index2].AttributeID);
            if (attributesValues[index2].AttributeID == oneFieldSpForRead._id && attributesValues[index2].Values[0] != DBNull.Value)
            {
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Int)
              {
                int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int32);
                recordForVedNew.List_OneDataVed.Add(oneDataVed);
                if (attributesValues[index2].AttributeID == -7 && MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Document))
                {
                  string documentTypeName = Vedomost_VB.GetDocumentTypeName(int32);
                  if (documentTypeName != "")
                  {
                    recordForVedNew.DocumentTypeName = documentTypeName;
                    break;
                  }
                  break;
                }
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Long)
              {
                long int64 = Convert.ToInt64(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int64);
                recordForVedNew.List_OneDataVed.Add(oneDataVed);
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Float)
              {
                float single = Convert.ToSingle(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Float, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) single);
                recordForVedNew.List_OneDataVed.Add(oneDataVed);
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.String)
              {
                IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributesValues[index2].AttributeID);
                if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
                {
                  int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                  QuickObjectInfo objectInfo = sk.Session.GetObjectInfo((long) int32);
                  if (!objectInfo.Empty)
                    data1 = objectInfo.Caption;
                }
                else
                  data1 = Convert.ToString(attributesValues[index2].Values[0]);
                if (data1 != "")
                {
                  Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data1);
                  recordForVedNew.List_OneDataVed.Add(oneDataVed);
                  break;
                }
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Guid)
              {
                string data2 = Convert.ToString(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data2);
                recordForVedNew.List_OneDataVed.Add(oneDataVed);
                break;
              }
            }
          }
        }
      }
      if (recordForVedNew.Count_in_Sp_S == null)
      {
        if (recordForVedNew.Count_in_SpRegulir_S == null)
        {
          string stringForObjType = recordForVedNew.Get_Data_String_for_objType(AvsIDCache.Attr_SpecificationSection);
          if (stringForObjType != "Документация" && stringForObjType != "")
          {
            OneError oneError = new OneError();
            oneError._objectIdSP_KudaVhodit = recordForVedNew.KudaObjectId;
            oneError._designationSp_KudaVhodit = recordForVedNew.KudaDesignation;
            oneError._objectId_Izdelie = recordForVedNew.Get_ObjectID();
            oneError._designation_Izdelie = recordForVedNew.Get_Designation();
            oneError._name_Izdelie = recordForVedNew.Get_Name();
            oneError._f_PRJLINK_ID = num2;
            oneError._message_kurc = "Не указано количество";
            oneError.Message();
            this._listError_OneError._list.Add(oneError);
          }
          if (string.Compare(recordForVedNew.Get_Designation(), recordForMainVed.Designation, false) != 0)
            recordForVedNew.KudaDesignation = recordForMainVed.Designation;
        }
      }
    }
    recordForVedNew.Set_ObjectID(recordForMainVed.ObjectIdIzd);
    recordForVedNew.Set_Designation(this._designationArticle);
    recordForVedNew.Get_Name();
    string sText_razdelitel = ". ";
    string text1 = this._one_Ved_Nastr_RazrabatyvaemoiVed._dopoln_Options_Ved._text1;
    recordForVedNew.Add_Data_String(AvsIDCache.Attr_Name, sText_razdelitel, text1);
    recordForVedNew.Get_Name();
    recordForVedNew.KudaDesignation = "";
    recordForVedNew.KudaObjectId = 0L;
    recordForVedNew.CountF_samOi_sp = 1f;
    recordForVedNew.Count_in_Izdelie = 0.0f;
    recordForVedNew.IsTherezKomplekt = false;
    recordForVedNew.IsTherezDopZam = false;
    recordForVedNew.Count_in_Sp = 1f;
    recordForVedNew.Count_in_Sp_S = "1";
    recordForVedNew.Count_in_Izdelie = 1f;
    recordForVedNew.Razdel_Ved = 1L;
    recordForVedNew.Removal_of_duplicates();
    this._listRecordsVed_New.Add(recordForVedNew);
    this.txtProtocol_Add("Создана Головная запись ведомости держателей подлинников");
    this.txtProtocol_Add($"{this._designationArticle} : {this._nameArticle}");
    return true;
  }

  /// <summary> Создание записей конкретной ведомости по одной СП </summary>
  /// <param name="recordForMainVed"></param>
  /// <returns></returns>
  public bool CreateRecordsVed_FromOneSpecification(Vedomost_VB.RecordForMainVed recordForMainVed)
  {
    int columnIndex1 = this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_SpecificationSection);
    int iRec = 1;
    string str1 = "";
    this.txtProtocol_Add("");
    this.txtProtocol_Add("Обработка ----------------------------------- " + recordForMainVed.Designation);
    int columnIndex2 = this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_PartName);
    using (SessionKeeper sk = new SessionKeeper())
    {
      string UrovenCurr = "";
      for (int index = 0; index < 5; ++index)
      {
        int num = Vedomost_VB.UrovniI[index];
        if (num != 0)
        {
          string str2 = num.ToString();
          if (index > 0)
            UrovenCurr += ".";
          UrovenCurr += str2;
        }
        else
          break;
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VD || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VDE)
        this.Sbor_Reference_Docs_For_DP_and_VD(recordForMainVed, sk);
      bool flag1 = false;
      foreach (DataRow row in (InternalDataCollectionBase) recordForMainVed.PartsDoc.Rows)
      {
        if (row != null)
        {
          if (row[columnIndex1] != DBNull.Value)
            str1 = Convert.ToString(row[columnIndex1]);
          string recordSpExtended1 = this.Get_Designation_From_RecordSP_Extended(row);
          if ((this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE) && !this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isOnlyUroven1)
          {
            bool flag2 = false;
            for (int index = 0; index < this._listSvoiaVedomost.Count; ++index)
            {
              Vedomost_VB.RecordForMainVed recordForMainVed1 = this._listSvoiaVedomost[index];
              if (recordSpExtended1 == recordForMainVed1.Designation || recordForMainVed1.Designation.StartsWith(recordSpExtended1) || recordSpExtended1.StartsWith(recordForMainVed1.Designation))
              {
                flag2 = recordForMainVed1.DerzPodl == this._derzPodl;
                break;
              }
            }
            if (flag2)
              continue;
          }
          else
          {
            bool flag3 = false;
            for (int index = 0; index < this._listSvoiaVedomost.Count; ++index)
            {
              Vedomost_VB.RecordForMainVed recordForMainVed2 = this._listSvoiaVedomost[index];
              if (recordSpExtended1 == recordForMainVed2.Designation || recordForMainVed2.Designation.StartsWith(recordSpExtended1) || recordSpExtended1.StartsWith(recordForMainVed2.Designation))
              {
                flag3 = true;
                break;
              }
            }
            if (flag3)
              continue;
          }
          this.Get_Name_From_RecordSP_Extended(row);
          this.Get_ObjectType_From_RecordSP_Extended(row);
          string objectTypeName = Vedomost_VB_Static.Get_ObjectTypeName(this.Get_ObjectId_From_RecordSP_Extended(row), sk);
          string str3 = columnIndex2 <= -1 || row[columnIndex2] == DBNull.Value ? "" : Convert.ToString(row[columnIndex2]);
          if ((string.IsNullOrEmpty(str3) || !(str3 == "Снятые составные части")) && (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.ZI || (!(objectTypeName == "Сборочная единица") || recordForMainVed == null || !recordForMainVed.EtaSp_Komplekt || this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isFor_ZIP_SB_Add) && (!(objectTypeName == "Комплект") || recordForMainVed == null || !recordForMainVed.EtaSp_Komplekt || this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isFor_ZIP_COMPL_Add)) && (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.VS || !(objectTypeName != "Сборочная единица") || !(objectTypeName != "Комплект") || !(objectTypeName != "Спецификация")) && this.CheckRecordSp_for_Create_Ved(row, objectTypeName, recordForMainVed.EtaSp_DopZam))
          {
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VP && str1 == "Комплекты" && !flag1)
            {
              OneError oneError = new OneError();
              oneError._objectIdSP_KudaVhodit = recordForMainVed.ObjectIdIzd;
              oneError._designationSp_KudaVhodit = recordForMainVed.Designation;
              string recordSpExtended2 = this.Get_Designation_From_RecordSP_Extended(row);
              long recordSpExtended3 = this.Get_ObjectId_From_RecordSP_Extended(row);
              oneError._objectId_Izdelie = recordSpExtended3;
              oneError._designation_Izdelie = recordSpExtended2;
              string recordSpExtended4 = this.Get_Name_From_RecordSP_Extended(row);
              oneError._name_Izdelie = recordSpExtended4;
              oneError._message_kurc = "Изделие найдено в разделе Комплекты. Заносить изделия в раздел Комплекты не рекомендуется. (См. Руководство п.4.4.3.3)";
              oneError.Message();
              this._listError_OneError._list.Add(oneError);
              flag1 = true;
            }
            Vedomost_VB.RecordForVed_New recordForVedNew = this.Create_recordForVed_New(row, recordForMainVed, UrovenCurr, iRec, sk);
            if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.ZI)
            {
              string stringForObjType = recordForVedNew.Get_Data_String_for_objType(AvsIDCache.Attr_RazdVedZip);
              if (stringForObjType != "")
              {
                int int32 = Convert.ToInt32(stringForObjType);
                recordForVedNew.Razdel_Ved = (long) int32;
              }
              else
              {
                switch (objectTypeName)
                {
                  case "Сборочная единица":
                    recordForVedNew.Razdel_Ved = 2L;
                    break;
                  case "Деталь":
                    recordForVedNew.Razdel_Ved = 2L;
                    break;
                  case "Стандартное изделие":
                    recordForVedNew.Razdel_Ved = 2L;
                    break;
                  case "Прочее изделие":
                    recordForVedNew.Razdel_Ved = 2L;
                    break;
                  case "Материал":
                    recordForVedNew.Razdel_Ved = 4L;
                    break;
                  case "Комплект":
                    recordForVedNew.Razdel_Ved = 2L;
                    break;
                  default:
                    recordForVedNew.Razdel_Ved = 1L;
                    break;
                }
              }
            }
            ++iRec;
            if (recordForVedNew != null)
            {
              recordForVedNew.Removal_of_duplicates();
              this.txtProtocol_Add($"{recordForVedNew.Get_Designation()}:{recordForVedNew.Get_Name()}:{recordForVedNew.Count_in_Sp_S}");
              this._listRecordsVed_New.Add(recordForVedNew);
            }
          }
        }
      }
    }
    this.txtProtocol_Add("Закончена =================================== " + recordForMainVed.Designation);
    this.txtProtocol_Add("");
    return true;
  }

  /// <summary> Создание записи в ВЕДОМОСТИ </summary>
  /// <param name="recordSp"></param>
  /// <param name="recordForMainVed"></param>
  /// <param name="UrovenCurr"></param>
  /// <param name="iRec"></param>
  /// <param name="sk"></param>
  /// <returns></returns>
  public Vedomost_VB.RecordForVed_New Create_recordForVed_New(
    DataRow recordSp,
    Vedomost_VB.RecordForMainVed recordForMainVed,
    string UrovenCurr,
    int iRec,
    SessionKeeper sk)
  {
    if (recordSp == null || recordForMainVed == null)
      return (Vedomost_VB.RecordForVed_New) null;
    Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New(recordSp, recordForMainVed, recordForMainVed.Uroven, this);
    recordForVedNew.Razdel_Ved = 0L;
    recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Info;
    recordForVedNew.KudaObjectId = recordForMainVed.ObjectIdIzd;
    recordForVedNew.CountF_samOi_sp = recordForMainVed.CountSummF;
    recordForVedNew.IsTherezKomplekt = recordForMainVed.IsTherezKomplekt;
    recordForVedNew.IsTherezDopZam = recordForMainVed.IsTherezDopZam;
    if (iRec > 0)
      recordForVedNew.UrovenS = $"{UrovenCurr}.{iRec.ToString()}";
    if (this.Get_ObjectType_From_RecordSP_Extended(recordSp) == AvsIDCache.ObjType_Complect)
      recordForVedNew.EtaSp_Komplekt = true;
    recordForVedNew.Razdel_Ved = 1L;
    if (this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidVS)
      recordForVedNew.Razdel_Ved = !recordForVedNew.EtaSp_Komplekt || !this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._is_Vydeliat_Sami_Komplekty ? 2L : 3L;
    if (this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidVP)
      recordForVedNew.Razdel_Ved = 1L;
    if (this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidDP || this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidDPE)
      recordForVedNew.Razdel_Ved = 2L;
    if (this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidZI)
      recordForVedNew.Razdel_Ved = 1L;
    if (this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidED)
      recordForVedNew.Razdel_Ved = 1L;
    if (!recordForVedNew.IsTherezKomplekt)
      recordForVedNew.Count_in_Izdelie = recordForVedNew.CountF_samOi_sp * recordForVedNew.Count_in_Sp;
    else
      recordForVedNew.Count_in_SpKompl = recordForVedNew.CountF_samOi_sp * recordForVedNew.Count_in_Sp;
    recordForVedNew.Count_in_SpRegulir = recordForVedNew.CountF_samOi_sp * recordForVedNew.Count_in_SpRegulir;
    if (recordForMainVed.Ispolnenie != null)
      recordForVedNew.Ispolnenie = recordForMainVed.Ispolnenie;
    if (recordForVedNew.Get_Designation() == null)
      recordForVedNew.Set_Designation("");
    if (recordForVedNew.Get_Name() == null)
      recordForVedNew.Set_Name("");
    if (recordForVedNew.Get_FuncGroup() == null)
      recordForVedNew.Set_FuncGroup("");
    long num1 = 0;
    if (recordForVedNew.Get_ObjectID() > 0L)
      num1 = recordForVedNew.Get_ObjectID();
    else if (recordSp[0] != DBNull.Value)
      num1 = Convert.ToInt64(recordSp[0]);
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE || this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isInputDoc && !this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isInputIzd)
    {
      long idDocByObjectIzd = Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(num1, sk);
      switch (idDocByObjectIzd)
      {
        case -1:
        case 0:
          break;
        default:
          num1 = idDocByObjectIzd;
          break;
      }
    }
    IDBObject dbObject = sk.Session.GetObject(num1, false);
    if (dbObject == null)
      return (Vedomost_VB.RecordForVed_New) null;
    AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
    long num2 = -1;
    for (int index1 = 0; index1 < this.listCommonId._listCommonId.Count; ++index1)
    {
      string str = "";
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.listCommonId._listCommonId[index1];
      MetaDataHelper.GetAttributeTypeName(oneFieldSpForRead._id);
      if (oneFieldSpForRead._attributeSourceTypes == AttributeSourceTypes.Relation && recordSp[index1] != DBNull.Value)
      {
        if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Int)
        {
          int int32 = Convert.ToInt32(recordSp[index1]);
          Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int32);
          recordForVedNew.List_OneDataVed.Add(oneDataVed);
          continue;
        }
        if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Long)
        {
          long int64 = Convert.ToInt64(recordSp[index1]);
          Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int64);
          if (-20 == oneFieldSpForRead._id)
            num2 = (long) oneDataVed.Data;
          recordForVedNew.List_OneDataVed.Add(oneDataVed);
          continue;
        }
        if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Float)
        {
          float single = Convert.ToSingle(recordSp[index1]);
          Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Float, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) single);
          recordForVedNew.List_OneDataVed.Add(oneDataVed);
          continue;
        }
        if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.String)
        {
          string data = Convert.ToString(recordSp[index1]);
          Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data);
          recordForVedNew.List_OneDataVed.Add(oneDataVed);
          continue;
        }
        if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Guid)
        {
          Guid guid = (Guid) recordSp[index1];
          Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) str);
          recordForVedNew.List_OneDataVed.Add(oneDataVed);
          continue;
        }
      }
      if (oneFieldSpForRead._attributeSourceTypes == AttributeSourceTypes.Object)
      {
        for (int index2 = 0; index2 < attributesValues.Length; ++index2)
        {
          string name = oneFieldSpForRead._name;
          MetaDataHelper.GetAttributeTypeName(attributesValues[index2].AttributeID);
          if (attributesValues[index2].AttributeID == oneFieldSpForRead._id && attributesValues[index2].Values[0] != DBNull.Value)
          {
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Int)
            {
              int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int32);
              recordForVedNew.List_OneDataVed.Add(oneDataVed);
              int columnIndex = this.ItemIndexForID(this.listCommonId._listCommonId, -7);
              if (recordSp[columnIndex] != DBNull.Value)
                Convert.ToInt32(recordSp[columnIndex]);
              if (attributesValues[index2].AttributeID == -7 && MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Document))
              {
                string documentTypeName = Vedomost_VB.GetDocumentTypeName(int32);
                if (documentTypeName != "")
                {
                  recordForVedNew.DocumentTypeName = documentTypeName;
                  break;
                }
                break;
              }
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Long)
            {
              long int64 = Convert.ToInt64(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int64);
              recordForVedNew.List_OneDataVed.Add(oneDataVed);
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Float)
            {
              float single = Convert.ToSingle(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Float, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) single);
              recordForVedNew.List_OneDataVed.Add(oneDataVed);
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.String)
            {
              IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributesValues[index2].AttributeID);
              if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
              {
                int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                QuickObjectInfo objectInfo = sk.Session.GetObjectInfo((long) int32);
                if (!objectInfo.Empty)
                  str = objectInfo.Caption;
              }
              else
              {
                str = Convert.ToString(attributesValues[index2].Values[0]);
                if (str.StartsWith("IK") && str.Length > 37)
                {
                  Convert.ToInt32(Vedomost_VB.StrCopy(str, 38, 3));
                  str = Vedomost_VB.StrCopy(str, 2, 36);
                  Guid objectGUID = new Guid(str);
                  QuickObjectInfo objectInfo = sk.Session.GetObjectInfo(objectGUID);
                  if (!objectInfo.Empty)
                    str = objectInfo.Caption;
                }
              }
              if (!string.IsNullOrEmpty(str))
              {
                string data = str.Trim();
                if (!string.IsNullOrEmpty(data))
                {
                  Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data);
                  recordForVedNew.List_OneDataVed.Add(oneDataVed);
                  break;
                }
                break;
              }
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Guid)
            {
              string data = Convert.ToString(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data);
              recordForVedNew.List_OneDataVed.Add(oneDataVed);
              break;
            }
          }
        }
      }
    }
    if (this._imsObjectType_RazrabatyvaemoiVed.Guid == Vedomost_VB_Static.GuidRS)
    {
      string caption = this.Field_From_RecordSP_Extended(recordSp, AvsIDCache.Attr_SpecificationSection);
      if (!string.IsNullOrEmpty(caption))
      {
        SpecificationSectionInfo sectionByCaption = SpecificationSectionInfo.FindSectionByCaption(caption);
        recordForVedNew.Razdel_Ved = Convert.ToInt64(sectionByCaption.RazdelSP);
      }
    }
    if (recordForVedNew.Count_in_Sp_S == null && recordForVedNew.Count_in_SpRegulir_S == null)
    {
      string stringForObjType = recordForVedNew.Get_Data_String_for_objType(AvsIDCache.Attr_SpecificationSection);
      if (stringForObjType != "Документация" && stringForObjType != "")
      {
        OneError oneError = new OneError();
        oneError._objectIdSP_KudaVhodit = recordForVedNew.KudaObjectId;
        oneError._designationSp_KudaVhodit = recordForVedNew.KudaDesignation;
        oneError._objectId_Izdelie = recordForVedNew.Get_ObjectID();
        oneError._designation_Izdelie = recordForVedNew.Get_Designation();
        oneError._name_Izdelie = recordForVedNew.Get_Name();
        oneError._f_PRJLINK_ID = num2;
        oneError._message_kurc = "Не указано количество";
        oneError.Message();
        this._listError_OneError._list.Add(oneError);
      }
      if (string.Compare(recordForVedNew.Get_Designation(), recordForMainVed.Designation, false) != 0)
        recordForVedNew.KudaDesignation = recordForMainVed.Designation;
    }
    recordForVedNew.Removal_of_duplicates();
    return recordForVedNew;
  }

  /// <summary> Создание записи о ВХОДЯЩЕЙ ВЕДОМОСТИ </summary>
  /// <param name="recordForMainVed"></param>
  private void CreateRecord_SvoiaVedomost_FromOneSpecification(
    Vedomost_VB.RecordForMainVed recordForMainVed)
  {
    DataRow recordSpIncudeVed = recordForMainVed.RecordSpIncudeVed;
    Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
    recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Included;
    this.Get_ObjectId_From_RecordSP_Extended(recordSpIncudeVed);
    recordForVedNew.Set_Designation(this.Get_Designation_From_RecordSP_Extended(recordSpIncudeVed));
    recordForVedNew.Set_Name(this.Get_Name_From_RecordSP_Extended(recordSpIncudeVed));
    long recordSpExtended = this.Get_ObjectId_From_RecordSP_Extended(recordSpIncudeVed);
    recordForVedNew.DocumentTypeName = Vedomost_VB_Static.Get_DocumentTypeName_ForObjectId(recordSpExtended);
    recordForVedNew.KudaDesignation = recordForMainVed.Designation;
    recordForVedNew.KudaObjectId = recordForMainVed.ObjectIdIzd;
    recordForVedNew.Count_in_Sp_S = Convert.ToString(recordForMainVed.CountSummF);
    recordForVedNew.Count_in_Sp = recordForMainVed.CountSummF;
    recordForVedNew.CountF_samOi_sp = recordForMainVed.CountSummF;
    recordForVedNew.Count_in_Izdelie = recordForVedNew.CountF_samOi_sp;
    recordForVedNew.Razdel_Ved = 1000L;
    if (recordForMainVed.Ispolnenie != null)
      recordForVedNew.Ispolnenie = recordForMainVed.Ispolnenie;
    this._listRecordsVed_New.Add(recordForVedNew);
  }

  /// <summary> Ведомость держателй подлинников. Ведомость ссылочных документов. Сбор ссылочных документов  для самОй recordForMainVed</summary>
  /// 
  ///             Собираются данные по ВСЕМ записям данной СП
  ///             <param name="recordForMainVed"></param>
  public void Sbor_Reference_Docs_For_DP_and_VD(
    Vedomost_VB.RecordForMainVed recordForMainVed,
    SessionKeeper sk)
  {
    int columnIndex = this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_SpecificationSection);
    int num = 1;
    string razdelRec = "";
    List<Vedomost_VB.Reference> referenceList1 = this.List_References_Create(recordForMainVed.ObjectIdIzd, recordForMainVed.ObjectIdIzd, sk);
    if (referenceList1 != null && referenceList1.Count > 0)
    {
      for (int index = 0; index < referenceList1.Count; ++index)
      {
        Vedomost_VB.Reference reference = referenceList1[index];
        if (this.CheckReference_for_Create_Ved(reference._objectIdParent, reference._objectId_For_Reference, sk, "Сборочные единицы"))
        {
          Vedomost_VB.RecordForVed_New referenceForDpAndVd = this.Create_record_Reference_For_DP_and_VD(recordForMainVed, reference._objectIdParent, reference._objectId_For_Reference, sk);
          if (referenceForDpAndVd != null)
          {
            referenceForDpAndVd.Removal_of_duplicates();
            this._listRecordsVed_New.Add(referenceForDpAndVd);
          }
        }
      }
    }
    if (recordForMainVed.NeRaskryvat)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) recordForMainVed.PartsDoc.Rows)
    {
      if (row != null)
      {
        if (row[columnIndex] != DBNull.Value)
          razdelRec = Convert.ToString(row[columnIndex]);
        string recordSpExtended = this.Get_Designation_From_RecordSP_Extended(row);
        if ((this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE) && !this._one_Ved_Nastr_RazrabatyvaemoiVed._bases_Options_Ved._isOnlyUroven1)
        {
          bool flag = false;
          for (int index = 0; index < this._listSvoiaVedomost.Count; ++index)
          {
            Vedomost_VB.RecordForMainVed recordForMainVed1 = this._listSvoiaVedomost[index];
            if (recordSpExtended == recordForMainVed1.Designation || recordForMainVed1.Designation.StartsWith(recordSpExtended) || recordSpExtended.StartsWith(recordForMainVed1.Designation))
            {
              flag = true;
              break;
            }
          }
          if (!flag && this.Get_DerzPodl_For_Designation(recordSpExtended) != this._derzPodl)
            flag = true;
          if (flag)
            continue;
        }
        this.Get_Name_From_RecordSP_Extended(row);
        this.Get_ObjectType_From_RecordSP_Extended(row);
        Convert.ToInt64(row[0]);
        List<Vedomost_VB.Reference> referenceList2 = this.List_References_Create(this.Get_ObjectId_From_RecordSP_Extended(row), recordForMainVed.ObjectIdIzd, sk);
        if (referenceList2 != null && referenceList2.Count > 0)
        {
          for (int index = 0; index < referenceList2.Count; ++index)
          {
            Vedomost_VB.Reference reference = referenceList2[index];
            if (this.CheckReference_for_Create_Ved(reference._objectIdParent, reference._objectId_For_Reference, sk, razdelRec))
            {
              Vedomost_VB.RecordForVed_New referenceForDpAndVd = this.Create_record_Reference_For_DP_and_VD(recordForMainVed, reference._objectIdParent, reference._objectId_For_Reference, sk);
              if (referenceForDpAndVd != null)
              {
                referenceForDpAndVd.Removal_of_duplicates();
                this._listRecordsVed_New.Add(referenceForDpAndVd);
              }
            }
          }
        }
        ++num;
      }
    }
  }

  public void Sbor_Reference_Docs_VD(
    Vedomost_VB.RecordForVed_New recordForVed_New_Old,
    SessionKeeper sk)
  {
    this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_SpecificationSection);
    this.List_References_Create(recordForVed_New_Old.Get_ObjectID(), recordForVed_New_Old.Get_ObjectID(), sk);
  }

  public string Get_DerzPodl_For_Designation(string designation)
  {
    if (string.IsNullOrEmpty(designation) || this._listRecordsForMainVed == null || this._listRecordsForMainVed.Count == 0)
      return "";
    string derzPodl = this._derzPodl;
    for (int index = 0; index < this._listRecordsForMainVed.Count; ++index)
    {
      Vedomost_VB.RecordForMainVed recordForMainVed = this._listRecordsForMainVed[index];
      if (recordForMainVed.Designation.StartsWith(designation) && !string.IsNullOrEmpty(recordForMainVed.DerzPodl))
      {
        derzPodl = recordForMainVed.DerzPodl;
        break;
      }
    }
    return derzPodl;
  }

  /// <summary> Получение списка ссылок для объекта objectId_From_RecordSp </summary>
  /// <param name="objectId_From_RecordSp"></param>
  /// <returns></returns>
  public List<Vedomost_VB.Reference> List_References_Create(
    long objectId_From_RecordSp,
    long objectIdParent,
    SessionKeeper sk)
  {
    if (objectId_From_RecordSp == 0L || objectId_From_RecordSp == -1L)
      return (List<Vedomost_VB.Reference>) null;
    if (objectIdParent == 0L || objectIdParent == -1L)
      return (List<Vedomost_VB.Reference>) null;
    DataTable objectReferences = this.GetObjectReferences(objectId_From_RecordSp, this._one_Ved_Nastr_RazrabatyvaemoiVed._list_Ved_ID);
    List<Vedomost_VB.Reference> referenceList = (List<Vedomost_VB.Reference>) null;
    foreach (DataRow row in (InternalDataCollectionBase) objectReferences.Rows)
    {
      object obj = row[0];
      if (obj != null)
      {
        long int64 = Convert.ToInt64(obj);
        if (referenceList == null)
          referenceList = new List<Vedomost_VB.Reference>();
        referenceList.Add(new Vedomost_VB.Reference()
        {
          _objectIdParent = objectId_From_RecordSp,
          _objectId_For_Reference = int64
        });
      }
    }
    return referenceList;
  }

  /// <summary> Ведомость Держателей подлинников. Присвоение номера раздела </summary>
  /// <param name="recordForVed_New"></param>
  /// <param name="parent"> Здесь "Тип"</param>
  public void RazdelDP_By_ObjectType(Vedomost_VB.RecordForVed_New recordForVed_New, string parent)
  {
    if (recordForVed_New == null)
      return;
    recordForVed_New.Razdel_Ved = 3L;
    switch (parent)
    {
      case "Стандартное изделие":
      case "Прочее изделие":
        recordForVed_New.PodRazdel_Ved = 1;
        break;
      default:
        recordForVed_New.PodRazdel_Ved = 2;
        break;
    }
  }

  /// <summary> Ведомость ссылочных документов. Присвоение номера раздела </summary>
  /// <param name="recordForVed_New"></param>
  public void RazdelVD_By_ObjectType(Vedomost_VB.RecordForVed_New recordForVed_New)
  {
    if (recordForVed_New == null)
      return;
    switch (recordForVed_New.Get_Data_String_for_objType(-7))
    {
      case "Государственные документы":
        recordForVed_New.Razdel_Ved = 4L;
        break;
      case "Документ предприятия":
      case "Документы предприятий":
      case "Документы предприятия":
        recordForVed_New.Razdel_Ved = 1L;
        break;
      case "Межгосударственные документы":
        recordForVed_New.Razdel_Ved = 5L;
        break;
      case "Отраслевые документы":
        recordForVed_New.Razdel_Ved = 2L;
        break;
      case "Республиканские документы":
        recordForVed_New.Razdel_Ved = 3L;
        break;
      default:
        recordForVed_New.Razdel_Ved = 1L;
        break;
    }
  }

  /// <summary> Создание записи по ссылке на документы </summary>
  /// <param name="recordForMainVed"> Используется только для чтения ИСПОЛНЕНИЯ</param>
  /// <param name="objectId_Parent"> идентификатор от какого элемента ссылка</param>
  /// <param name="objectId_Reference"> идентификатор ссылки</param>
  /// <param name="sk"></param>
  /// <returns></returns>
  public Vedomost_VB.RecordForVed_New Create_record_Reference_For_DP_and_VD(
    Vedomost_VB.RecordForMainVed recordForMainVed,
    long objectId_Parent,
    long objectId_Reference,
    SessionKeeper sk)
  {
    if (objectId_Reference == 0L)
      return (Vedomost_VB.RecordForVed_New) null;
    if (sk == null)
      return (Vedomost_VB.RecordForVed_New) null;
    Vedomost_VB.RecordForVed_New recordForVed_New = new Vedomost_VB.RecordForVed_New();
    recordForVed_New.Razdel_Ved = 0L;
    recordForVed_New.TypeRec = Vedomost_VB.TypeRec.Info;
    recordForVed_New.Set_ObjectID(objectId_Reference);
    recordForVed_New.Set_Designation(this.Get_Designation_ForObjectId(objectId_Reference));
    recordForVed_New.Set_Name(this.Get_Name_ForObjectId(objectId_Reference));
    recordForVed_New.UrovenS = "0";
    if (recordForMainVed != null && recordForMainVed.Ispolnenie != null)
      recordForVed_New.Ispolnenie = recordForMainVed.Ispolnenie;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
    {
      long idDocByObjectIzd = Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(objectId_Reference, sk);
      switch (idDocByObjectIzd)
      {
        case -1:
        case 0:
          break;
        default:
          objectId_Reference = idDocByObjectIzd;
          break;
      }
    }
    string objectTypeName = Vedomost_VB_Static.Get_ObjectTypeName(objectId_Parent, sk);
    IDBObject dbObject = sk.Session.GetObject(objectId_Reference, false);
    if (dbObject == null)
      return (Vedomost_VB.RecordForVed_New) null;
    AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
    for (int index1 = 0; index1 < this.listCommonId._listCommonId.Count; ++index1)
    {
      string data1 = "";
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.listCommonId._listCommonId[index1];
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(oneFieldSpForRead._id);
      if (oneFieldSpForRead._attributeSourceTypes == AttributeSourceTypes.Object)
      {
        for (int index2 = 0; index2 < attributesValues.Length; ++index2)
        {
          string name = oneFieldSpForRead._name;
          MetaDataHelper.GetAttributeTypeName(attributesValues[index2].AttributeID);
          if (attributesValues[index2].AttributeID == oneFieldSpForRead._id && attributesValues[index2].Values[0] != DBNull.Value)
          {
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Int)
            {
              if (attributesValues[index2].MultipleValued == MultiValueModes.SingleValue)
              {
                string data2 = Convert.ToString(attributesValues[index2].Descriptions[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data2);
                recordForVed_New.List_OneDataVed.Add(oneDataVed);
                break;
              }
              int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int32);
              recordForVed_New.List_OneDataVed.Add(oneDataVed1);
              if (attributesValues[index2].AttributeID == -7 && MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Document))
              {
                string documentTypeName = Vedomost_VB.GetDocumentTypeName(int32);
                if (documentTypeName != "")
                {
                  recordForVed_New.DocumentTypeName = documentTypeName;
                  break;
                }
                break;
              }
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Long)
            {
              long int64 = Convert.ToInt64(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int64);
              recordForVed_New.List_OneDataVed.Add(oneDataVed);
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Float)
            {
              if (attributeTypeName == "Тип НТД")
              {
                string data3 = Convert.ToString(attributesValues[index2].Descriptions[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data3);
                recordForVed_New.List_OneDataVed.Add(oneDataVed);
                break;
              }
              float single = Convert.ToSingle(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed2 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Float, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) single);
              recordForVed_New.List_OneDataVed.Add(oneDataVed2);
              break;
            }
            if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.String)
            {
              IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributesValues[index2].AttributeID);
              if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
              {
                int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                QuickObjectInfo objectInfo = sk.Session.GetObjectInfo((long) int32);
                if (!objectInfo.Empty)
                  data1 = objectInfo.Caption;
              }
              else
                data1 = Convert.ToString(attributesValues[index2].Values[0]);
              if (!(attributeTypeName == "Листов") || !(data1 == "0"))
              {
                if (attributeTypeName == "Тип НТД")
                {
                  string data4 = Convert.ToString(attributesValues[index2].Descriptions[0]);
                  Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data4);
                  recordForVed_New.List_OneDataVed.Add(oneDataVed);
                  break;
                }
                Vedomost_VB.OneDataVed oneDataVed3 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data1);
                recordForVed_New.List_OneDataVed.Add(oneDataVed3);
                break;
              }
            }
            else if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Guid)
            {
              string data5 = Convert.ToString(attributesValues[index2].Values[0]);
              Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data5);
              recordForVed_New.List_OneDataVed.Add(oneDataVed);
              break;
            }
          }
        }
      }
    }
    recordForVed_New.Removal_of_duplicates();
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
      this.RazdelDP_By_ObjectType(recordForVed_New, objectTypeName);
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VD || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.VDE)
      this.RazdelVD_By_ObjectType(recordForVed_New);
    return recordForVed_New;
  }

  /// <summary> Создание записи конкретной ведомости по objectID </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  public Vedomost_VB.RecordForVed_New Create_recordForVed_New_From_ObjectID(long objectId)
  {
    if (objectId == 0L)
      return (Vedomost_VB.RecordForVed_New) null;
    this.ItemIndexForID(this.listCommonId._listCommonId, AvsIDCache.Attr_SpecificationSection);
    Vedomost_VB.RecordForVed_New vedNewFromObjectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      vedNewFromObjectId = new Vedomost_VB.RecordForVed_New((DataRow) null, (Vedomost_VB.RecordForMainVed) null, "", this);
      vedNewFromObjectId.TypeRec = Vedomost_VB.TypeRec.Info;
      vedNewFromObjectId.Razdel_Ved = 0L;
      AttributeValues[] attributesValues = sessionKeeper.Session.GetObject(objectId, false).GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
      for (int index = 0; index < attributesValues.Length; ++index)
      {
        int attributeId = attributesValues[index].AttributeID;
        MetaDataHelper.GetAttributeTypeName(attributesValues[index].AttributeID);
      }
      for (int index1 = 0; index1 < this.listCommonId._listCommonId.Count; ++index1)
      {
        Vedomost_VB.OneDataVed oneDataVed1 = (Vedomost_VB.OneDataVed) null;
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.listCommonId._listCommonId[index1];
        string attributeTypeName = MetaDataHelper.GetAttributeTypeName(oneFieldSpForRead._id);
        if (oneFieldSpForRead._attributeSourceTypes == AttributeSourceTypes.Object)
        {
          for (int index2 = 0; index2 < attributesValues.Length; ++index2)
          {
            string name = oneFieldSpForRead._name;
            MetaDataHelper.GetAttributeTypeName(attributesValues[index2].AttributeID);
            if (attributesValues[index2].AttributeID == oneFieldSpForRead._id && attributesValues[index2].Values[0] != DBNull.Value)
            {
              if (attributesValues[index2].MultipleValued == MultiValueModes.SingleValueFromList)
              {
                string data = Convert.ToString(attributesValues[index2].Descriptions[0]);
                Vedomost_VB.OneDataVed oneDataVed2 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data);
                vedNewFromObjectId.List_OneDataVed.Add(oneDataVed2);
                oneDataVed1 = (Vedomost_VB.OneDataVed) null;
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Int)
              {
                int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int32);
                int id = oneFieldSpForRead._id;
                if (attributesValues[index2].AttributeID == -7 && MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Document))
                {
                  string documentTypeName = Vedomost_VB.GetDocumentTypeName(int32);
                  if (documentTypeName != "")
                  {
                    vedNewFromObjectId.DocumentTypeName = documentTypeName;
                    break;
                  }
                  break;
                }
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Long)
              {
                long int64 = Convert.ToInt64(attributesValues[index2].Values[0]);
                oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int64);
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Float)
              {
                float single = Convert.ToSingle(attributesValues[index2].Values[0]);
                oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Float, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) single);
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.String)
              {
                string data = Convert.ToString(attributesValues[index2].Values[0]);
                if (!(attributeTypeName == "Листов") || !(data == "0"))
                {
                  oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data);
                  break;
                }
              }
              else if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Guid)
              {
                string data = Convert.ToString(attributesValues[index2].Values[0]);
                oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data);
                break;
              }
            }
          }
          if (oneDataVed1 != null)
          {
            MetaDataHelper.GetAttributeTypeName(oneFieldSpForRead._id);
            vedNewFromObjectId.List_OneDataVed.Add(oneDataVed1);
          }
        }
      }
    }
    return vedNewFromObjectId;
  }

  public Vedomost_VB.RecordForVed_New Create_recordForVed_New_From_ObjectID_DP_VD_Dialog(
    long objectId_Reference)
  {
    if (objectId_Reference == 0L)
      return (Vedomost_VB.RecordForVed_New) null;
    Vedomost_VB.RecordForVed_New recordForVed_New = (Vedomost_VB.RecordForVed_New) null;
    using (SessionKeeper sk = new SessionKeeper())
    {
      recordForVed_New = new Vedomost_VB.RecordForVed_New();
      recordForVed_New.Razdel_Ved = 0L;
      recordForVed_New.TypeRec = Vedomost_VB.TypeRec.Info;
      recordForVed_New.Set_ObjectID(objectId_Reference);
      recordForVed_New.Set_Designation(this.Get_Designation_ForObjectId(objectId_Reference));
      recordForVed_New.Set_Name(this.Get_Name_ForObjectId(objectId_Reference));
      recordForVed_New.UrovenS = "0";
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
      {
        long idDocByObjectIzd = Vedomost_VB_Static.Get_ObjectIdDoc_by_ObjectIzd(objectId_Reference, sk);
        switch (idDocByObjectIzd)
        {
          case -1:
          case 0:
            break;
          default:
            objectId_Reference = idDocByObjectIzd;
            break;
        }
      }
      IDBObject dbObject = sk.Session.GetObject(objectId_Reference, false);
      if (dbObject == null)
        return (Vedomost_VB.RecordForVed_New) null;
      AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
      for (int index1 = 0; index1 < this.listCommonId._listCommonId.Count; ++index1)
      {
        string data1 = "";
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.listCommonId._listCommonId[index1];
        string attributeTypeName = MetaDataHelper.GetAttributeTypeName(oneFieldSpForRead._id);
        if (oneFieldSpForRead._attributeSourceTypes == AttributeSourceTypes.Object)
        {
          for (int index2 = 0; index2 < attributesValues.Length; ++index2)
          {
            string name = oneFieldSpForRead._name;
            MetaDataHelper.GetAttributeTypeName(attributesValues[index2].AttributeID);
            if (attributesValues[index2].AttributeID == oneFieldSpForRead._id && attributesValues[index2].Values[0] != DBNull.Value)
            {
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Int)
              {
                if (attributesValues[index2].MultipleValued == MultiValueModes.SingleValue)
                {
                  string data2 = Convert.ToString(attributesValues[index2].Descriptions[0]);
                  Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data2);
                  recordForVed_New.List_OneDataVed.Add(oneDataVed);
                  break;
                }
                int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed1 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int32);
                recordForVed_New.List_OneDataVed.Add(oneDataVed1);
                if (attributesValues[index2].AttributeID == -7 && MetaDataHelper.IsObjectTypeChildOf(int32, AvsIDCache.ObjType_Document))
                {
                  string documentTypeName = Vedomost_VB.GetDocumentTypeName(int32);
                  if (documentTypeName != "")
                  {
                    recordForVed_New.DocumentTypeName = documentTypeName;
                    break;
                  }
                  break;
                }
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Long)
              {
                long int64 = Convert.ToInt64(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) int64);
                recordForVed_New.List_OneDataVed.Add(oneDataVed);
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Float)
              {
                if (attributesValues[index2].MultipleValued == MultiValueModes.SingleValueFromList)
                {
                  string data3 = Convert.ToString(attributesValues[index2].Descriptions[0]);
                  Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data3);
                  recordForVed_New.List_OneDataVed.Add(oneDataVed);
                  break;
                }
                float single = Convert.ToSingle(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed2 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Float, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) single);
                recordForVed_New.List_OneDataVed.Add(oneDataVed2);
                break;
              }
              if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.String)
              {
                IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributesValues[index2].AttributeID);
                if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
                {
                  int int32 = Convert.ToInt32(attributesValues[index2].Values[0]);
                  QuickObjectInfo objectInfo = sk.Session.GetObjectInfo((long) int32);
                  if (!objectInfo.Empty)
                    data1 = objectInfo.Caption;
                }
                else
                  data1 = Convert.ToString(attributesValues[index2].Values[0]);
                if (!(attributeTypeName == "Листов") || !(data1 == "0"))
                {
                  if (attributesValues[index2].MultipleValued == MultiValueModes.SingleValueFromList)
                  {
                    string data4 = Convert.ToString(attributesValues[index2].Descriptions[0]);
                    Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data4);
                    recordForVed_New.List_OneDataVed.Add(oneDataVed);
                    break;
                  }
                  Vedomost_VB.OneDataVed oneDataVed3 = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data1);
                  recordForVed_New.List_OneDataVed.Add(oneDataVed3);
                  break;
                }
              }
              else if (oneFieldSpForRead._type == Vedomost_VB.TypeDataSel.Guid)
              {
                string data5 = Convert.ToString(attributesValues[index2].Values[0]);
                Vedomost_VB.OneDataVed oneDataVed = new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, oneFieldSpForRead._attributeSourceTypes, oneFieldSpForRead._id, (object) data5);
                recordForVed_New.List_OneDataVed.Add(oneDataVed);
                break;
              }
            }
          }
        }
      }
      recordForVed_New.Removal_of_duplicates();
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.DP)
      {
        int typeVed = (int) this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed;
      }
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.VD)
      {
        if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed != Vedomost_VB.TypeVed.VDE)
          goto label_47;
      }
      this.RazdelVD_By_ObjectType(recordForVed_New);
    }
label_47:
    return recordForVed_New;
  }

  /// <summary> Создание записи Куда входит по objectID </summary>
  /// <param name="recordForMainVed"></param>
  /// <returns></returns>
  public Vedomost_VB.RecordForVed_Vtor Create_recordForVed_Vtor_From_ObjectID(long objectId)
  {
    if (objectId == 0L)
      return (Vedomost_VB.RecordForVed_Vtor) null;
    Vedomost_VB.RecordForVed_Vtor vtorFromObjectId = (Vedomost_VB.RecordForVed_Vtor) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      vtorFromObjectId = new Vedomost_VB.RecordForVed_Vtor();
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
      {
        string[] descriptionsById = dbObject.GetDescriptionsByID(AvsIDCache.Attr_Designation, false);
        if (descriptionsById.Length != 0)
        {
          string str = descriptionsById[0];
          vtorFromObjectId.KudaDesignation = str;
          vtorFromObjectId.KudaObjectId = objectId;
        }
      }
    }
    return vtorFromObjectId;
  }

  /// <summary> Проверка, брать ли ЭТУ запись в ВЕДОМОСТЬ (согласно условиям сбора) </summary>
  /// <param name="recordSP"></param>
  /// <returns></returns>
  public bool CheckRecordSp_for_Create_Ved(
    DataRow recordSp,
    string objectTypeName,
    bool etaSp_DopZam)
  {
    string str1 = this.Field_From_RecordSP_Extended(recordSp, AvsIDCache.Attr_SpecificationSection);
    if (str1 == "" && !string.IsNullOrEmpty(objectTypeName))
    {
      switch (objectTypeName)
      {
        case "Деталь":
          str1 = "Детали";
          break;
        case "Документ":
          str1 = "Документация";
          break;
        case "Комплекс":
          str1 = "Комплексы";
          break;
        case "Комплект":
          str1 = "Комплекты";
          break;
        case "Материал":
          str1 = "Материалы";
          break;
        case "Прочее изделие":
          str1 = "Прочие изделия";
          break;
        case "Сборочная единица":
          str1 = "Сборочные единицы";
          break;
        case "Стандартное изделие":
          str1 = "Стандартные изделия";
          break;
      }
    }
    if (str1 == "")
      return false;
    Vedomost_VB.Usl_Read_From_SP uslReadFromSp1 = (Vedomost_VB.Usl_Read_From_SP) null;
    for (int index = 0; index < this._one_Ved_Nastr_RazrabatyvaemoiVed._list_Usl_Read_From_SP.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP uslReadFromSp2 = this._one_Ved_Nastr_RazrabatyvaemoiVed._list_Usl_Read_From_SP[index];
      if (uslReadFromSp2._section_SP == str1)
      {
        uslReadFromSp1 = uslReadFromSp2;
        break;
      }
    }
    if (uslReadFromSp1 == null)
      return false;
    this.Get_Designation_From_RecordSP_Extended(recordSp);
    this.Get_Name_From_RecordSP_Extended(recordSp);
    bool isItemIndex;
    string str2 = this.Field_From_RecordSP(recordSp, AvsIDCache.Attr_DopZamenNumInGroup, out isItemIndex);
    long recordSpExtended = this.Get_ObjectId_From_RecordSP_Extended(recordSp);
    if (!this._etap_Sbora_DopZam)
    {
      if (str2 != "" && str2 != "0" && (this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isDopZam == 0 || this._one_Ved_Nastr_RazrabatyvaemoiVed._sbor_Options._isAllocateDopZam != 0))
        return false;
    }
    else if (this._urovenN == 1)
    {
      if (str2 == "" || str2 == "0")
        return false;
    }
    else if (!etaSp_DopZam && (str2 == "" || str2 == "0"))
      return false;
    if (uslReadFromSp1._list_Usl_Read_From_SP_One == null || uslReadFromSp1._list_Usl_Read_From_SP_One.Count == 0)
      return true;
    for (int index = 0; index < uslReadFromSp1._list_Usl_Read_From_SP_One.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne = uslReadFromSp1._list_Usl_Read_From_SP_One[index];
      string str3 = this.Field_From_RecordSP(recordSp, uslReadFromSpOne._oneFieldSpForRead._id, out isItemIndex);
      if (string.IsNullOrEmpty(str3))
        str3 = Vedomost_VB_Static.Get_Value_By_ObjectId_AttrId(recordSpExtended, uslReadFromSpOne._oneFieldSpForRead._id);
      bool flag = false;
      switch (uslReadFromSpOne._uslovie)
      {
        case "=":
          if (str3 == uslReadFromSpOne._text)
          {
            flag = true;
            break;
          }
          break;
        case "!=":
          if (str3 == "" && uslReadFromSpOne._text != "" || str3 != "" && uslReadFromSpOne._text == "" || str3 != uslReadFromSpOne._text)
          {
            flag = true;
            break;
          }
          break;
        case "?":
          if (str3 != "" && uslReadFromSpOne._text != "" && str3.IndexOf(uslReadFromSpOne._text) > -1)
          {
            flag = true;
            break;
          }
          break;
        case "!?":
          if (str3.IndexOf(uslReadFromSpOne._text) < 0)
          {
            flag = true;
            break;
          }
          break;
        case "&":
          if (str3 != "" && uslReadFromSpOne._text != "" && str3.IndexOf(uslReadFromSpOne._text) == 0)
          {
            flag = true;
            break;
          }
          break;
      }
      if (flag)
      {
        if (index == uslReadFromSp1._list_Usl_Read_From_SP_One.Count - 1 || !uslReadFromSpOne._or_and)
          return true;
      }
      else if (index == uslReadFromSp1._list_Usl_Read_From_SP_One.Count - 1 || uslReadFromSpOne._or_and)
        return false;
    }
    return false;
  }

  public bool CheckReference_for_Create_Ved(
    long objectId_Parent,
    long objectId_Reference,
    SessionKeeper sk,
    string razdelRec)
  {
    if (razdelRec == "")
      return false;
    Vedomost_VB.Usl_Read_From_SP uslReadFromSp1 = (Vedomost_VB.Usl_Read_From_SP) null;
    for (int index = 0; index < this._one_Ved_Nastr_RazrabatyvaemoiVed._list_Usl_Read_From_SP_Reference.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP uslReadFromSp2 = this._one_Ved_Nastr_RazrabatyvaemoiVed._list_Usl_Read_From_SP_Reference[index];
      if (uslReadFromSp2._section_SP == razdelRec)
      {
        uslReadFromSp1 = uslReadFromSp2;
        break;
      }
    }
    if (uslReadFromSp1 == null)
      return false;
    if (uslReadFromSp1._list_Usl_Read_From_SP_One == null || uslReadFromSp1._list_Usl_Read_From_SP_One.Count == 0)
      return true;
    string str = "";
    IDBObject dbObject = sk.Session.GetObject(objectId_Reference, false);
    if (dbObject != null)
    {
      string[] descriptionsById = dbObject.GetDescriptionsByID(-7, false);
      if (descriptionsById.Length != 0)
        str = descriptionsById[0];
    }
    sk.Session.GetObject(objectId_Parent, false);
    dbObject?.GetDescriptionsByID(-7, false);
    for (int index = 0; index < uslReadFromSp1._list_Usl_Read_From_SP_One.Count; ++index)
    {
      Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne = uslReadFromSp1._list_Usl_Read_From_SP_One[index];
      bool flag = false;
      switch (uslReadFromSpOne._uslovie)
      {
        case "=":
          if (str == uslReadFromSpOne._text)
          {
            flag = true;
            break;
          }
          break;
        case "!=":
          if (str == "" && uslReadFromSpOne._text != "" || str != "" && uslReadFromSpOne._text == "" || str != uslReadFromSpOne._text)
          {
            flag = true;
            break;
          }
          break;
        case "?":
          if (str != "" && uslReadFromSpOne._text != "" && str.IndexOf(uslReadFromSpOne._text) > -1)
          {
            flag = true;
            break;
          }
          break;
        case "!?":
          if (str.IndexOf(uslReadFromSpOne._text) < 0)
          {
            flag = true;
            break;
          }
          break;
        case "&":
          if (str != "" && uslReadFromSpOne._text != "" && str.IndexOf(uslReadFromSpOne._text) == 0)
          {
            flag = true;
            break;
          }
          break;
      }
      if (flag && (index == 0 || !uslReadFromSpOne._or_and))
        return true;
    }
    return false;
  }

  /// <summary> Обозначение ищется в списке _listSvoiaVedomost </summary>
  /// <param name="designation"></param>
  /// <returns></returns>
  public bool CheckRecordSp_by_listSvoiaVedomost(string designation)
  {
    if (designation == "" || this._listSvoiaVedomost == null || this._listSvoiaVedomost.Count == 0)
      return false;
    for (int index = 0; index < this._listSvoiaVedomost.Count; ++index)
    {
      Vedomost_VB.RecordForMainVed recordForMainVed = this._listSvoiaVedomost[index];
      if (designation == recordForMainVed.Designation)
        return true;
    }
    return false;
  }

  /// <summary> Объединение однородных записей (в т.ч. основных и ЧЕРЕЗ комплекты) </summary>
  /// <returns></returns>
  public bool Union_Records_Ved_1()
  {
    for (int index = this._listRecordsVed_New.Count - 1; index > 0; --index)
    {
      Vedomost_VB.RecordForVed_New recordForVed_New1 = this._listRecordsVed_New[index - 1];
      Vedomost_VB.RecordForVed_New recordForVed_New2 = this._listRecordsVed_New[index];
      if (this.Compare_Union_Records_Ved_1(recordForVed_New1, recordForVed_New2))
      {
        this.Summ_for_Union_Records_Ved_1(recordForVed_New1, recordForVed_New2);
        this._listRecordsVed_New.RemoveAt(index);
      }
    }
    return true;
  }

  /// <summary> Сравнение для объединения однородных записей (в т.ч. основных и ЧЕРЕЗ комплекты)</summary>
  /// <param name="recordForVed_New1"></param>
  /// <param name="recordForVed_New2"></param>
  /// <returns></returns>
  public bool Compare_Union_Records_Ved_1(
    Vedomost_VB.RecordForVed_New recordForVed_New1,
    Vedomost_VB.RecordForVed_New recordForVed_New2)
  {
    if (recordForVed_New1.Get_Designation() != recordForVed_New2.Get_Designation() || recordForVed_New1.Get_Name() != recordForVed_New2.Get_Name())
      return false;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DP || this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed == Vedomost_VB.TypeVed.DPE)
      return true;
    if (recordForVed_New1.KudaDesignation != recordForVed_New2.KudaDesignation || recordForVed_New1.Ispolnenie != recordForVed_New2.Ispolnenie)
      return false;
    string note1 = recordForVed_New1.Get_Note();
    string note2 = recordForVed_New2.Get_Note();
    return !(note1.Trim() != note2.Trim());
  }

  /// <summary> Суммирование при Объединении двух записей </summary>
  /// <param name="recordForVed_New1"></param>
  /// <param name="recordForVed_New2"></param>
  public void Summ_for_Union_Records_Ved_1(
    Vedomost_VB.RecordForVed_New recordForVed_New1,
    Vedomost_VB.RecordForVed_New recordForVed_New2)
  {
    string position1 = recordForVed_New1.Get_Position();
    string position2 = recordForVed_New2.Get_Position();
    string note1 = recordForVed_New1.Get_Note();
    string note2 = recordForVed_New2.Get_Note();
    string str1 = note1.Trim();
    string str2 = note2.Trim();
    string str3 = position2;
    if ((position1 != str3 || (double) Math.Abs(recordForVed_New1.Count_in_Sp - recordForVed_New2.Count_in_Sp) > 0.0) && str1 == str2)
    {
      recordForVed_New1.Count_in_Sp += recordForVed_New2.Count_in_Sp;
      recordForVed_New1.Count_in_Sp_S = recordForVed_New1.Count_in_Sp.ToString();
    }
    recordForVed_New1.Count_in_Izdelie += recordForVed_New2.Count_in_Izdelie;
    recordForVed_New1.Count_in_SpKompl += recordForVed_New2.Count_in_SpKompl;
    recordForVed_New1.Count_in_SpRegulir += recordForVed_New2.Count_in_SpRegulir;
    recordForVed_New1.IsTherezKomplekt = false;
    recordForVed_New1.IsTherezDopZam = false;
  }

  /// <summary> Первая сортировка ведомости, Сортировка по настройке </summary>
  /// <param name="recordForVed1"></param>
  /// <param name="recordForVed2"></param>
  /// <returns></returns>
  public virtual int Compare(
    Vedomost_VB.RecordForVed_New recordForVed1,
    Vedomost_VB.RecordForVed_New recordForVed2)
  {
    if (recordForVed1 == null || recordForVed2 == null)
      return 0;
    if (recordForVed2.TypeRec == Vedomost_VB.TypeRec.Main)
      return 1;
    if (recordForVed1.TypeRec == Vedomost_VB.TypeRec.Main)
      return -1;
    int num = string.Compare(recordForVed1.Ispolnenie, recordForVed2.Ispolnenie, StringComparison.Ordinal);
    if (num != 0)
      return num;
    if (recordForVed1.TypeRec != recordForVed2.TypeRec)
    {
      if (recordForVed2.TypeRec == Vedomost_VB.TypeRec.Included)
        return -1;
      if (recordForVed1.TypeRec == Vedomost_VB.TypeRec.Included)
        return 1;
    }
    recordForVed1.Designation();
    recordForVed2.Designation();
    return this.StringCompareForVed2(recordForVed1, recordForVed2, this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl.Sorting_Usl_VedOsn);
  }

  /// <summary> Выделение вторичных записей </summary>
  /// <returns></returns>
  public void Extrection_Ved_Vtor1()
  {
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
      Vedomost_VB.RecordForVed_Vtor recordForVedVtor = new Vedomost_VB.RecordForVed_Vtor();
      recordForVedNew.List_recordForVed_Vtor = new List<Vedomost_VB.RecordForVed_Vtor>();
      recordForVedVtor.KudaDesignation = recordForVedNew.KudaDesignation;
      recordForVedNew.KudaDesignation = "";
      recordForVedVtor.KudaObjectId = recordForVedNew.KudaObjectId;
      recordForVedNew.KudaObjectId = 0L;
      recordForVedVtor.Count_in_Izdelie = recordForVedNew.Count_in_Izdelie;
      recordForVedNew.Count_in_Izdelie = 0.0f;
      recordForVedVtor.Count_in_Sp = recordForVedNew.Count_in_Sp;
      recordForVedNew.Count_in_Sp = 0.0f;
      recordForVedVtor.Count_in_Sp_S = recordForVedNew.Count_in_Sp_S;
      recordForVedNew.Count_in_Sp_S = "";
      recordForVedVtor.Count_in_SpKompl = recordForVedNew.Count_in_SpKompl;
      recordForVedNew.Count_in_SpKompl = 0.0f;
      recordForVedVtor.Count_in_SpKompl_S = recordForVedNew.Count_in_SpKompl_S;
      recordForVedNew.Count_in_SpKompl_S = "";
      recordForVedVtor.Count_in_SpRegulir = recordForVedNew.Count_in_SpRegulir;
      recordForVedNew.Count_in_SpRegulir = 0.0f;
      recordForVedVtor.Count_in_SpRegulir_S = recordForVedNew.Count_in_SpRegulir_S;
      recordForVedNew.Count_in_SpRegulir_S = "";
      recordForVedVtor.Count_Vsego = recordForVedNew.Count_Vsego;
      recordForVedNew.Count_Vsego = 0.0f;
      recordForVedVtor.CountF_samOi_sp = recordForVedNew.CountF_samOi_sp;
      recordForVedNew.CountF_samOi_sp = 0.0f;
      recordForVedNew.List_recordForVed_Vtor.Add(recordForVedVtor);
    }
  }

  public void Tmp_ControlRecordForVed_New()
  {
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
    }
  }

  public void Tmp_Peremeshaem()
  {
    for (int index1 = 0; index1 < this._listRecordsVed_New.Count; ++index1)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index1];
      int count = recordForVedNew.List_recordForVed_Vtor.Count;
      for (int index2 = 0; index2 < count; ++index2)
      {
        Vedomost_VB.RecordForVed_Vtor recordForVedVtor = recordForVedNew.List_recordForVed_Vtor[index2];
        recordForVedNew.List_recordForVed_Vtor.Add(recordForVedVtor);
      }
    }
  }

  /// <summary> Сравниваем и если одинаковое во ВСЕХ исполнениях ДЕЛАЕМ ОДНУ запись с пустым исполнением </summary>
  /// 
  ///             Т.е. Выделение ОБЩЕЙ части
  public void Merger_Ved_ispolneniy()
  {
    List<string> stringList = new List<string>();
    List<int> intList = new List<int>();
    int index1 = 0;
    if (this._variables_Coordination.list_Variables.Count > this._listRecordsVed_New.Count)
      return;
    while (index1 < this._listRecordsVed_New.Count)
    {
      Vedomost_VB.RecordForVed_New RecordForVed1 = this._listRecordsVed_New[index1];
      if (RecordForVed1.TypeRec != Vedomost_VB.TypeRec.Info && RecordForVed1.TypeRec != Vedomost_VB.TypeRec.Included)
      {
        ++index1;
      }
      else
      {
        intList.Clear();
        stringList.Clear();
        for (int index2 = 0; index2 < this._variables_Coordination.list_Variables.Count; ++index2)
        {
          string listVariable = this._variables_Coordination.list_Variables[index2];
          stringList.Add(listVariable);
        }
        string str = stringList[0];
        string ispolnenie1 = RecordForVed1.Ispolnenie;
        if (string.IsNullOrEmpty(ispolnenie1))
          stringList.Remove(str);
        else
          stringList.Remove(ispolnenie1);
        for (int index3 = index1 + 1; index3 < this._listRecordsVed_New.Count; ++index3)
        {
          Vedomost_VB.RecordForVed_New RecordForVed2 = this._listRecordsVed_New[index3];
          if ((RecordForVed2.TypeRec == Vedomost_VB.TypeRec.Info || RecordForVed2.TypeRec == Vedomost_VB.TypeRec.Included) && this.CompareRecordForVed_For_Merger_Ved_ispolneniy(RecordForVed1, RecordForVed2))
          {
            if (RecordForVed1.TypeRec != Vedomost_VB.TypeRec.Included || !(RecordForVed1.Ispolnenie == RecordForVed2.Ispolnenie))
            {
              intList.Add(index3);
              string ispolnenie2 = RecordForVed2.Ispolnenie;
              stringList.Remove(ispolnenie2);
            }
            else
              break;
          }
        }
        if (stringList.Count == 0 && intList.Count > 0)
        {
          RecordForVed1.Ispolnenie = "";
          for (int index4 = intList.Count - 1; index4 >= 0; --index4)
            this._listRecordsVed_New.RemoveAt(intList[index4]);
        }
        ++index1;
      }
    }
  }

  /// <summary> Различие исполнений по сборочному чертежу </summary>
  public void Razlithie_ispolneniy()
  {
    if (this.isPeremDannye)
      return;
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      if (this._listRecordsVed_New[index].TypeRec == Vedomost_VB.TypeRec.TitleVar)
      {
        this.isPeremDannye = true;
        break;
      }
    }
    if (this.isPeremDannye)
      return;
    Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
    recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Remark;
    string str = "Различие исполнений" + " ";
    for (int index = 0; index < this._listAll_IspolneniySp_prodInfo.Count; ++index)
    {
      string designation = this._listAll_IspolneniySp_prodInfo[index].Designation;
      str = (index != 0 ? (index != this._listAll_IspolneniySp_prodInfo.Count - 1 ? str + ", " : str + " и ") : str + " ") + designation;
    }
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._isUnbrokenDefis)
    {
      string newValue = "" + '–'.ToString();
      str = str.Replace("-", newValue);
    }
    string name = str + " по сборочному чертежу";
    recordForVedNew.Set_Name(name);
    this._listRecordsVed_New.Add(recordForVedNew);
  }

  /// <summary> Если есть запись "Переменные данные ..." проверим, а все ли исполнения есть </summary>
  /// 
  ///             Если надо - вставляем записи "Отсутствуют"
  public void Ispolneniye_Otsutstvuet()
  {
    if (!this.isPeremDannye)
      return;
    string str = "";
    int index1 = 0;
    int index2 = 0;
    while (index2 < this._variables_Coordination.list_Variables.Count)
    {
      string listVariable = this._variables_Coordination.list_Variables[index2];
      while (index1 < this._listRecordsVed_New.Count)
      {
        string ispolnenie = this._listRecordsVed_New[index1].Ispolnenie;
        if (ispolnenie == "")
          ++index1;
        else if (ispolnenie == str)
        {
          ++index1;
        }
        else
        {
          str = ispolnenie;
          if (ispolnenie != listVariable)
          {
            for (; ispolnenie != listVariable; listVariable = this._variables_Coordination.list_Variables[index2])
            {
              Vedomost_VB.RecordForVed_New recordForVedNew1 = new Vedomost_VB.RecordForVed_New();
              recordForVedNew1.TypeRec = Vedomost_VB.TypeRec.TitleIsp;
              recordForVedNew1.Set_Name(listVariable);
              this._listRecordsVed_New.Insert(index1, recordForVedNew1);
              int index3 = index1 + 1;
              Vedomost_VB.RecordForVed_New recordForVedNew2 = new Vedomost_VB.RecordForVed_New();
              recordForVedNew2.TypeRec = Vedomost_VB.TypeRec.RemarkShort;
              recordForVedNew2.Set_Name("Отсутствуют");
              this._listRecordsVed_New.Insert(index3, recordForVedNew2);
              recordForVedNew2.Ispolnenie = listVariable;
              index1 = index3 + 1;
              ++index2;
              if (index2 == this._variables_Coordination.list_Variables.Count)
                break;
            }
            ++index2;
            if (index2 != this._variables_Coordination.list_Variables.Count)
            {
              listVariable = this._variables_Coordination.list_Variables[index2];
              ++index1;
            }
            else
              break;
          }
          else
          {
            ++index2;
            if (index2 != this._variables_Coordination.list_Variables.Count)
            {
              listVariable = this._variables_Coordination.list_Variables[index2];
              ++index1;
            }
            else
              break;
          }
        }
      }
      if (index1 >= this._listRecordsVed_New.Count)
        break;
    }
    for (int index4 = index2; index4 < this._variables_Coordination.list_Variables.Count; ++index4)
    {
      string listVariable = this._variables_Coordination.list_Variables[index4];
      Vedomost_VB.RecordForVed_New recordForVedNew3 = new Vedomost_VB.RecordForVed_New();
      recordForVedNew3.TypeRec = Vedomost_VB.TypeRec.TitleIsp;
      recordForVedNew3.Set_Name(listVariable);
      recordForVedNew3.Ispolnenie = listVariable;
      this._listRecordsVed_New.Add(recordForVedNew3);
      Vedomost_VB.RecordForVed_New recordForVedNew4 = new Vedomost_VB.RecordForVed_New();
      recordForVedNew4.TypeRec = Vedomost_VB.TypeRec.RemarkShort;
      recordForVedNew4.Set_Name("Отсутствуют");
      recordForVedNew4.Ispolnenie = listVariable;
      this._listRecordsVed_New.Add(recordForVedNew4);
    }
  }

  /// <summary> Объединение записей с одинаковой первичной т.е. ОБЪЕДИНЯЕМ И СОЗДАЕМ ГРУППЫ ВТОРИЧНЫХ записей </summary>
  public void Merger_Ved_Vtor()
  {
    int index1 = 0;
    for (int index2 = index1 + 1; index1 < this._listRecordsVed_New.Count && index2 < this._listRecordsVed_New.Count; index1 = index2)
    {
      Vedomost_VB.RecordForVed_New recordForVed1 = this._listRecordsVed_New[index1];
      index2 = index1 + 1;
      while (index2 < this._listRecordsVed_New.Count)
      {
        Vedomost_VB.RecordForVed_New recordForVed2 = this._listRecordsVed_New[index2];
        if (this.CompareRecordForVed_For_Merger_Ved_Vtor(recordForVed1, recordForVed2, this._one_Ved_Nastr_RazrabatyvaemoiVed._merge_Usl2))
        {
          if (recordForVed2.List_recordForVed_Vtor != null)
          {
            for (int index3 = 0; index3 < recordForVed2.List_recordForVed_Vtor.Count; ++index3)
            {
              Vedomost_VB.RecordForVed_Vtor recordForVedVtor = recordForVed2.List_recordForVed_Vtor[index3];
              recordForVed1.List_recordForVed_Vtor.Add(recordForVedVtor);
            }
            this._listRecordsVed_New.Remove(recordForVed2);
          }
        }
        else
          break;
      }
    }
  }

  /// <summary> Сортировка вторичных записей ВЕДОМОСТИ </summary>
  public void Sort_Ved_Vtor()
  {
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
      if (recordForVedNew.List_recordForVed_Vtor != null)
        recordForVedNew.List_recordForVed_Vtor.Sort((IComparer<Vedomost_VB.RecordForVed_Vtor>) this._compare_RecordForVed_Vtor);
    }
  }

  /// <summary> Расчет "Всего" и "Сумма" </summary>
  public void Summ_VedVtor()
  {
    for (int index1 = 0; index1 < this._listRecordsVed_New.Count; ++index1)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index1];
      float num1 = 0.0f;
      if (recordForVedNew.List_recordForVed_Vtor != null)
      {
        for (int index2 = 0; index2 < recordForVedNew.List_recordForVed_Vtor.Count; ++index2)
        {
          Vedomost_VB.RecordForVed_Vtor recordForVedVtor = recordForVedNew.List_recordForVed_Vtor[index2];
          float num2 = recordForVedVtor.Count_in_Izdelie + recordForVedVtor.Count_in_SpKompl + recordForVedVtor.Count_in_SpRegulir;
          recordForVedVtor.Count_Vsego = num2;
          num1 += recordForVedVtor.Count_Vsego;
        }
        recordForVedNew.Count_Summ = num1;
      }
    }
  }

  /// <summary> Сравнение для объединения с создание группы ВТОРИЧНЫХ ЗАПИСЕЙ по услоовию </summary>
  /// <param name="recordForVed1"></param>
  /// <param name="recordForVed2"></param>
  /// <param name="merge_Usl2"></param>
  /// <returns></returns>
  public bool CompareRecordForVed_For_Merger_Ved_Vtor(
    Vedomost_VB.RecordForVed_New recordForVed1,
    Vedomost_VB.RecordForVed_New recordForVed2,
    Vedomost_VB.Merge_Usl2 merge_Usl2)
  {
    if (recordForVed1 == null || recordForVed2 == null || recordForVed1.List_recordForVed_Vtor == null || recordForVed2.List_recordForVed_Vtor == null || recordForVed2.List_recordForVed_Vtor.Count == 0 || recordForVed1.TypeRec != recordForVed2.TypeRec || recordForVed2.TypeRec != Vedomost_VB.TypeRec.Info && recordForVed2.TypeRec != Vedomost_VB.TypeRec.Included)
      return false;
    if (merge_Usl2 == null || merge_Usl2._list_Merge_Usl2 == null || merge_Usl2._list_Merge_Usl2.Count == 0)
      return this.CompareRecordForVed_For_Merger_Ved_Vtor(recordForVed1, recordForVed2);
    for (int index = 0; index < merge_Usl2._list_Merge_Usl2.Count; ++index)
    {
      Vedomost_VB.Merge_Usl_One mergeUslOne = merge_Usl2._list_Merge_Usl2[index];
      string str1;
      string str2;
      if (mergeUslOne._typeField == Vedomost_VB.TypeField.ObjectType)
      {
        str1 = recordForVed1.Get_Data_String_for_objType(mergeUslOne._objectType);
        str2 = recordForVed2.Get_Data_String_for_objType(mergeUslOne._objectType);
      }
      else
      {
        str1 = recordForVed1.Get_Data_String_for_TypeFieldVedRec(mergeUslOne._typeFieldVedRec);
        str2 = recordForVed2.Get_Data_String_for_TypeFieldVedRec(mergeUslOne._typeFieldVedRec);
      }
      if (str1 != str2)
        return false;
    }
    return true;
  }

  /// <summary> Сравнение для объединения с создание группы ВТОРИЧНЫХ ЗАПИСЕЙ фиксированное </summary>
  /// <param name="recordForVed1"></param>
  /// <param name="recordForVed2"></param>
  /// <returns></returns>
  public bool CompareRecordForVed_For_Merger_Ved_Vtor(
    Vedomost_VB.RecordForVed_New recordForVed1,
    Vedomost_VB.RecordForVed_New recordForVed2)
  {
    return recordForVed1 != null && recordForVed2 != null && recordForVed1.List_recordForVed_Vtor != null && recordForVed2.List_recordForVed_Vtor != null && recordForVed2.List_recordForVed_Vtor.Count != 0 && recordForVed1.TypeRec == recordForVed2.TypeRec && (recordForVed2.TypeRec == Vedomost_VB.TypeRec.Info || recordForVed2.TypeRec == Vedomost_VB.TypeRec.Included) && !(recordForVed1.Get_Name() != recordForVed2.Get_Name()) && !(recordForVed1.Get_Designation() != recordForVed2.Get_Designation()) && !(recordForVed1.Remark != recordForVed2.Remark) && !(recordForVed1.EdIzmKol != recordForVed2.EdIzmKol) && !(recordForVed1.Get_FuncGroup() != recordForVed2.Get_FuncGroup()) && recordForVed1.Get_ObjectID() == recordForVed2.Get_ObjectID() && !(recordForVed1.Ispolnenie != recordForVed2.Ispolnenie);
  }

  /// <summary> Сравниваем и если одинаковое во ВСЕХ исполнениях ДЕЛАЕМ ЗАТЕМ ОДНУ запись с пустым исполнением </summary>
  /// <param name="RecordForVed1"></param>
  /// <param name="RecordForVed2"></param>
  /// <returns></returns>
  public bool CompareRecordForVed_For_Merger_Ved_ispolneniy(
    Vedomost_VB.RecordForVed_New RecordForVed1,
    Vedomost_VB.RecordForVed_New RecordForVed2)
  {
    if (RecordForVed1 == null || RecordForVed2 == null || RecordForVed1.TypeRec != Vedomost_VB.TypeRec.Info && RecordForVed1.TypeRec != Vedomost_VB.TypeRec.Included || RecordForVed2.TypeRec != Vedomost_VB.TypeRec.Info && RecordForVed2.TypeRec != Vedomost_VB.TypeRec.Included || RecordForVed1.TypeRec != RecordForVed2.TypeRec || RecordForVed1.Get_Name() != RecordForVed2.Get_Name() || RecordForVed1.Get_Designation() != RecordForVed2.Get_Designation())
      return false;
    string remark1 = RecordForVed1.Remark;
    string remark2 = RecordForVed1.Remark;
    return (string.IsNullOrEmpty(remark1) || string.IsNullOrEmpty(remark2) || !(remark1.Trim() != remark2.Trim())) && (double) Math.Abs(RecordForVed1.Count_in_Sp - RecordForVed2.Count_in_Sp) <= 0.0 && (double) Math.Abs(RecordForVed1.Count_in_SpKompl - RecordForVed2.Count_in_SpKompl) <= 0.0 && (double) Math.Abs(RecordForVed1.Count_in_Izdelie - RecordForVed2.Count_in_Izdelie) <= 0.0 && (double) Math.Abs(RecordForVed1.CountF_samOi_sp - RecordForVed2.CountF_samOi_sp) <= 0.0 && !(RecordForVed1.EdIzmKol != RecordForVed2.EdIzmKol) && !(RecordForVed1.Get_FuncGroup() != RecordForVed2.Get_FuncGroup()) && RecordForVed1.Get_ObjectID() == RecordForVed2.Get_ObjectID() && !(RecordForVed1.Get_Position() != RecordForVed2.Get_Position());
  }

  /// <summary> Создание заголовков исполнений </summary>
  public void Create_Ved_Zagol_Ispoln()
  {
    string str = "";
    int index = 0;
    while (index < this._listRecordsVed_New.Count)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew1 = this._listRecordsVed_New[index];
      if (recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.Info)
      {
        string ispolnenie = recordForVedNew1.Ispolnenie;
        if (ispolnenie == "")
        {
          ++index;
          continue;
        }
        if (!this.isPeremDannye)
        {
          Vedomost_VB.RecordForVed_New recordForVedNew2 = new Vedomost_VB.RecordForVed_New();
          recordForVedNew2.TypeRec = Vedomost_VB.TypeRec.TitleVar;
          recordForVedNew2.Set_Name(Vedomost_VB_Static._text_For_TitleVar);
          this._listRecordsVed_New.Insert(index, recordForVedNew2);
          this.isPeremDannye = true;
          ++index;
          continue;
        }
        if (ispolnenie != str)
        {
          Vedomost_VB.RecordForVed_New recordForVedNew3 = new Vedomost_VB.RecordForVed_New();
          recordForVedNew3.TypeRec = Vedomost_VB.TypeRec.TitleIsp;
          recordForVedNew3.Set_Name(ispolnenie);
          recordForVedNew3.Ispolnenie = ispolnenie;
          this._listRecordsVed_New.Insert(index, recordForVedNew3);
          str = ispolnenie;
          ++index;
          continue;
        }
      }
      ++index;
    }
  }

  /// <summary> Создание заголовка "Ведомости составных частей" </summary>
  public void Create_Ved_Zagol_SvoiaVed()
  {
    bool flag = false;
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew1 = this._listRecordsVed_New[index];
      if (recordForVedNew1.TypeRec != Vedomost_VB.TypeRec.Included && recordForVedNew1.TypeRec != Vedomost_VB.TypeRec.TitleIncluded)
        flag = false;
      if (recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.Included && !flag)
      {
        Vedomost_VB.RecordForVed_New recordForVedNew2 = new Vedomost_VB.RecordForVed_New();
        recordForVedNew2.TypeRec = Vedomost_VB.TypeRec.TitleIncluded;
        string includeName = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._include_Name;
        recordForVedNew2.Set_Name(includeName);
        this._listRecordsVed_New.Insert(index, recordForVedNew2);
        flag = true;
        ++index;
      }
    }
  }

  /// <summary> Дополнение поля Функциональная группа исходя из раздела ведомости </summary>
  public void Addition_FuncGroup()
  {
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
      if (recordForVedNew.TypeRec == Vedomost_VB.TypeRec.Info)
      {
        string name = recordForVedNew.Get_FuncGroup();
        if (!(name != "") && recordForVedNew.Razdel_Ved != 0L)
        {
          switch (recordForVedNew.Razdel_Ved - 1L)
          {
            case 0:
              name = "0001";
              break;
            case 1:
              name = "0002";
              break;
            case 2:
              name = "0003";
              break;
            case 3:
              name = "0004";
              break;
            case 4:
              name = "9710995";
              break;
            case 5:
              name = "9710996";
              break;
            case 6:
              name = "980099";
              break;
            case 7:
              name = "9997";
              break;
          }
          recordForVedNew.Set_FuncGroup(name);
        }
      }
    }
  }

  /// <summary> Контроль наличия функциональных групп </summary>
  public void Check_FuncGroup()
  {
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl.Sorting_Usl_VedOsn == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Count == 0)
      return;
    bool flag = false;
    for (int index1 = 0; index1 < this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Count; ++index1)
    {
      Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel = this._one_Ved_Nastr_RazrabatyvaemoiVed._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel[index1];
      if (sortingUslOneRazdel._list_sorting_Usl_One != null && sortingUslOneRazdel._list_sorting_Usl_One.Count != 0)
      {
        for (int index2 = 0; index2 < sortingUslOneRazdel._list_sorting_Usl_One.Count; ++index2)
        {
          Vedomost_VB.Sorting_Usl_One sortingUslOne = sortingUslOneRazdel._list_sorting_Usl_One[index2];
          if (sortingUslOne._typeField == Vedomost_VB.TypeField.ObjectType && sortingUslOne._objectType == AvsIDCache.Attr_FuncGroup)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!flag)
      return;
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index];
      if (recordForVedNew.TypeRec == Vedomost_VB.TypeRec.Info && recordForVedNew.Razdel_Ved != 0L && !(recordForVedNew.Get_FuncGroup() != ""))
      {
        string stringForObjType = recordForVedNew.Get_Data_String_for_objType(AvsIDCache.Attr_SpecificationSection);
        if (!(stringForObjType != "") || !(stringForObjType != "Стандартные изделия") || !(stringForObjType != "Прочие изделия") || !(stringForObjType != "Материалы"))
        {
          OneError oneError = new OneError();
          oneError._objectIdSP_KudaVhodit = recordForVedNew.KudaObjectId;
          oneError._designationSp_KudaVhodit = recordForVedNew.KudaDesignation;
          oneError._objectId_Izdelie = recordForVedNew.Get_ObjectID();
          oneError._designation_Izdelie = recordForVedNew.Get_Designation();
          oneError._name_Izdelie = recordForVedNew.Get_Name();
          oneError._message_kurc = "Нет функциональной группы";
          oneError.Message();
          this._listError_OneError._list.Add(oneError);
        }
      }
    }
  }

  /// <summary> Создание заголовков по ПРИЗНАКУ </summary>
  public void Create_Ved_Zagol_PoPriznaku()
  {
    string strB1 = "";
    string strA = "";
    string strB2 = "";
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    string str = "";
    Vedomost_VB.One_Zagolovok oneZagolovok1 = (Vedomost_VB.One_Zagolovok) null;
    Vedomost_VB.One_Zagolovok oneZagolovok2 = (Vedomost_VB.One_Zagolovok) null;
    bool flag1 = true;
    int index1 = 0;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._list_One_Zagolovok == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._list_One_Zagolovok.Count < 2)
      return;
    Vedomost_VB.TypeField typeField = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._typeField;
    int objectType = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._objectType;
    Vedomost_VB.TypeFieldVedRec typeFieldVedRec = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._typeFieldVedRec;
    Vedomost_VB.TypeCompare typeCompare = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._typeCompare;
    while (index1 < this._listRecordsVed_New.Count)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew1 = this._listRecordsVed_New[index1];
      recordForVedNew1.Get_Name();
      recordForVedNew1.Get_Designation();
      if (recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.TitleVar || recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.TitleIsp)
      {
        strB1 = "";
        strB2 = "";
        str = "";
        num1 = 0;
        num3 = 0;
        oneZagolovok1 = (Vedomost_VB.One_Zagolovok) null;
        oneZagolovok2 = (Vedomost_VB.One_Zagolovok) null;
        flag1 = true;
        ++index1;
      }
      else if (recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.Main)
        ++index1;
      else if (recordForVedNew1.TypeRec != Vedomost_VB.TypeRec.Info)
      {
        ++index1;
      }
      else
      {
        if (typeField == Vedomost_VB.TypeField.ObjectType)
        {
          Vedomost_VB.OneDataVed data = recordForVedNew1.Get_Data(objectType);
          if (data != null && data.Data != DBNull.Value)
          {
            if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
              strA = Convert.ToString(data.Data);
            else
              num2 = !Convert.ToString(data.Data).All<char>(new System.Func<char, bool>(char.IsDigit)) ? 1 : Convert.ToInt32(data.Data);
          }
          else if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
            strA = "";
          else
            num2 = 0;
        }
        if (typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
        {
          if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
          {
            strA = recordForVedNew1.Get_Data_String_for_TypeFieldVedRec(typeFieldVedRec);
          }
          else
          {
            string forTypeFieldVedRec = recordForVedNew1.Get_Data_String_for_TypeFieldVedRec(typeFieldVedRec);
            num2 = string.IsNullOrEmpty(forTypeFieldVedRec) ? 0 : Convert.ToInt32(forTypeFieldVedRec);
          }
        }
        recordForVedNew1.Designation();
        for (int index2 = 0; index2 < this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._list_One_Zagolovok.Count - 1; ++index2)
        {
          Vedomost_VB.One_Zagolovok oneZagolovok3 = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._list_One_Zagolovok[index2];
          Vedomost_VB.One_Zagolovok oneZagolovok4 = this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._list_One_Zagolovok[index2 + 1];
          int num4;
          int num5;
          if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
          {
            num4 = string.Compare(strA, oneZagolovok3._granicaPriznaka, StringComparison.Ordinal);
            num5 = string.Compare(strA, oneZagolovok4._granicaPriznaka, StringComparison.Ordinal);
          }
          else
          {
            num4 = num2 - Convert.ToInt32(oneZagolovok3._granicaPriznaka);
            num5 = num2 - Convert.ToInt32(oneZagolovok4._granicaPriznaka);
          }
          if (num4 == 0 || num4 > 0 && num5 < 0)
          {
            if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
              strB2 = oneZagolovok3._granicaPriznaka;
            else
              num3 = Convert.ToInt32(oneZagolovok3._granicaPriznaka);
            string name = oneZagolovok3._name;
            if (flag1 || (typeCompare != Vedomost_VB.TypeCompare.Symbol ? Convert.ToInt32(oneZagolovok3._granicaPriznaka) - num1 : string.Compare(oneZagolovok3._granicaPriznaka, strB1, StringComparison.Ordinal)) != 0)
            {
              if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
                strB1 = strB2;
              else
                num1 = num3;
              Vedomost_VB.RecordForVed_New recordForVedNew2 = new Vedomost_VB.RecordForVed_New();
              recordForVedNew2.TypeRec = Vedomost_VB.TypeRec.Title;
              recordForVedNew2.Set_Name(name);
              recordForVedNew2.Razdel_Ved = typeCompare != Vedomost_VB.TypeCompare.Symbol ? (long) num3 : (long) Convert.ToInt32(strB2);
              if (recordForVedNew1.Ispolnenie != null && recordForVedNew1.Ispolnenie != "")
                recordForVedNew2.Ispolnenie = recordForVedNew1.Ispolnenie;
              this._listRecordsVed_New.Insert(index1, recordForVedNew2);
              flag1 = false;
              ++index1;
              break;
            }
            break;
          }
          if (index2 == this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._list_One_Zagolovok.Count - 2)
          {
            int num6 = typeCompare != Vedomost_VB.TypeCompare.Symbol ? Convert.ToInt32(oneZagolovok4._granicaPriznaka) - num3 : string.Compare(oneZagolovok4._granicaPriznaka, strB2, StringComparison.Ordinal);
            if (num5 >= 0 && num6 != 0)
            {
              if (typeCompare == Vedomost_VB.TypeCompare.Symbol)
              {
                strB2 = oneZagolovok4._granicaPriznaka;
                strB1 = strB2;
              }
              else
              {
                num3 = Convert.ToInt32(oneZagolovok4._granicaPriznaka);
                num1 = num3;
              }
              string name = oneZagolovok4._name;
              Vedomost_VB.RecordForVed_New recordForVedNew3 = new Vedomost_VB.RecordForVed_New();
              recordForVedNew3.TypeRec = Vedomost_VB.TypeRec.Title;
              recordForVedNew3.Set_Name(name);
              recordForVedNew3.Razdel_Ved = typeCompare != Vedomost_VB.TypeCompare.Symbol ? (long) num3 : (long) Convert.ToInt32(strB2);
              if (recordForVedNew1.Ispolnenie != null && recordForVedNew1.Ispolnenie != "")
                recordForVedNew3.Ispolnenie = recordForVedNew1.Ispolnenie;
              this._listRecordsVed_New.Insert(index1, recordForVedNew3);
              flag1 = false;
              ++index1;
              break;
            }
          }
        }
        ++index1;
      }
    }
    bool flag2 = false;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._zagolovki_Ved._vyvodit_PodZagolovki && typeField == Vedomost_VB.TypeField.TypeFieldVedRec && typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Razdel_Ved)
    {
      for (int index3 = 0; index3 < this._one_Ved_Nastr_RazrabatyvaemoiVed._list_RazdelsVed.Count; ++index3)
      {
        Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_RazrabatyvaemoiVed._list_RazdelsVed[index3];
        if (oneRazdelVed._list_onePodRazdels != null && oneRazdelVed._list_onePodRazdels.Count > 0)
        {
          flag2 = true;
          break;
        }
      }
    }
    if (!flag2)
      return;
    Vedomost_VB.OnePodRazdelVed onePodRazdelVed1 = (Vedomost_VB.OnePodRazdelVed) null;
    Vedomost_VB.OnePodRazdelVed onePodRazdelVed2 = (Vedomost_VB.OnePodRazdelVed) null;
    int num7 = 0;
    int num8 = 0;
    str = "";
    int index4 = 0;
    List<Vedomost_VB.OnePodRazdelVed> onePodRazdelVedList = (List<Vedomost_VB.OnePodRazdelVed>) null;
    int num9 = 0;
    while (index4 < this._listRecordsVed_New.Count)
    {
      Vedomost_VB.RecordForVed_New recordForVed = this._listRecordsVed_New[index4];
      recordForVed.Get_Name();
      recordForVed.Get_Designation();
      if (recordForVed.TypeRec == Vedomost_VB.TypeRec.TitleVar || recordForVed.TypeRec == Vedomost_VB.TypeRec.TitleIsp)
        num9 = 0;
      if (recordForVed.TypeRec == Vedomost_VB.TypeRec.TitleVar || recordForVed.TypeRec == Vedomost_VB.TypeRec.TitleIsp || recordForVed.TypeRec == Vedomost_VB.TypeRec.Title || recordForVed.TypeRec == Vedomost_VB.TypeRec.Title2)
      {
        num7 = 0;
        num8 = 0;
        str = "";
        onePodRazdelVed1 = (Vedomost_VB.OnePodRazdelVed) null;
        onePodRazdelVed2 = (Vedomost_VB.OnePodRazdelVed) null;
        flag1 = true;
        onePodRazdelVedList = (List<Vedomost_VB.OnePodRazdelVed>) null;
        ++index4;
      }
      else if (recordForVed.TypeRec == Vedomost_VB.TypeRec.Main)
        ++index4;
      else if (recordForVed.TypeRec != Vedomost_VB.TypeRec.Info)
      {
        ++index4;
      }
      else
      {
        int razdelVed = (int) recordForVed.Razdel_Ved;
        if (razdelVed < 1)
        {
          ++index4;
        }
        else
        {
          if (razdelVed != num9)
          {
            onePodRazdelVedList = this.Get_ListPodrazdels(recordForVed, this._one_Ved_Nastr_RazrabatyvaemoiVed._list_RazdelsVed);
            num9 = razdelVed;
          }
          if (onePodRazdelVedList == null)
          {
            ++index4;
          }
          else
          {
            int podRazdelVed = recordForVed.PodRazdel_Ved;
            for (int index5 = 0; index5 < onePodRazdelVedList.Count - 1; ++index5)
            {
              Vedomost_VB.OnePodRazdelVed onePodRazdelVed3 = onePodRazdelVedList[index5];
              Vedomost_VB.OnePodRazdelVed onePodRazdelVed4 = onePodRazdelVedList[index5 + 1];
              int num10 = podRazdelVed - onePodRazdelVed3._podRazdelVed;
              int num11 = podRazdelVed - onePodRazdelVed4._podRazdelVed;
              if (num10 == 0 || num10 > 0 && num11 < 0)
              {
                num8 = onePodRazdelVed3._podRazdelVed;
                string name = onePodRazdelVed3._name;
                if (flag1 || onePodRazdelVed3._podRazdelVed - num7 != 0)
                {
                  num7 = num8;
                  Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
                  recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Title2;
                  recordForVedNew.Set_Name(name);
                  recordForVedNew.Razdel_Ved = (long) recordForVed.PodRazdel_Ved;
                  recordForVedNew.PodRazdel_Ved = num8;
                  if (recordForVed.Ispolnenie != null && recordForVed.Ispolnenie != "")
                    recordForVedNew.Ispolnenie = recordForVed.Ispolnenie;
                  this._listRecordsVed_New.Insert(index4, recordForVedNew);
                  flag1 = false;
                  ++index4;
                  break;
                }
                break;
              }
              if (index5 == onePodRazdelVedList.Count - 2)
              {
                int num12 = onePodRazdelVed4._podRazdelVed - num8;
                if (num11 >= 0 && num12 != 0)
                {
                  num8 = onePodRazdelVed4._podRazdelVed;
                  string name = onePodRazdelVed4._name;
                  num7 = num8;
                  Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
                  recordForVedNew.TypeRec = Vedomost_VB.TypeRec.Title2;
                  recordForVedNew.Set_Name(name);
                  this._listRecordsVed_New.Insert(index4, recordForVedNew);
                  flag1 = false;
                  ++index4;
                  break;
                }
              }
            }
            ++index4;
          }
        }
      }
    }
  }

  public void Splitting_Multilines()
  {
    int index = this._listRecordsVed_New.Count - 1;
    (int, int) valueTuple1 = (20, 20);
    ImDocument templateObjectGuid = Vedomost_VB_Static.Get_Template_By_TemplateObjectGuid(this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid);
    if (templateObjectGuid == null || templateObjectGuid.NodesCount < 1 || this._listRecordsVed_New.Count == 0)
      return;
    (int, int) valueTuple2 = Vedomost_VB_Static.GetLines_InPages(templateObjectGuid);
    (int, int) valueTuple3 = valueTuple2;
    if (valueTuple3.Item1 == 0 && valueTuple3.Item2 == 0)
      valueTuple2 = (20, 20);
    int num1 = valueTuple2.Item1;
    int num2 = valueTuple2.Item2;
    int num3 = num1;
    while (index >= 0)
    {
      if (index > 0)
        num3 = num2;
      int num4 = 0;
      if (index > 0)
      {
        Vedomost_VB.RecordForVed_New recordForVedNew1 = this._listRecordsVed_New[index - 1];
        if (recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.Title || recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.Title || recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.TitleIsp || recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.TitleIncluded || recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.Title2 || recordForVedNew1.TypeRec == Vedomost_VB.TypeRec.TitleVar)
          num4 += 2;
        if (index > 1)
        {
          Vedomost_VB.RecordForVed_New recordForVedNew2 = this._listRecordsVed_New[index - 2];
          if (recordForVedNew2.TypeRec == Vedomost_VB.TypeRec.Title || recordForVedNew2.TypeRec == Vedomost_VB.TypeRec.Title || recordForVedNew2.TypeRec == Vedomost_VB.TypeRec.TitleIsp || recordForVedNew2.TypeRec == Vedomost_VB.TypeRec.TitleIncluded || recordForVedNew2.TypeRec == Vedomost_VB.TypeRec.Title2 || recordForVedNew2.TypeRec == Vedomost_VB.TypeRec.TitleVar)
            num4 += 2;
        }
      }
      Vedomost_VB.RecordForVed_New recordForVedNew3 = this._listRecordsVed_New[index];
      if (recordForVedNew3.TypeRec != Vedomost_VB.TypeRec.Info && recordForVedNew3.TypeRec != Vedomost_VB.TypeRec.Included)
        --index;
      else if (recordForVedNew3.List_recordForVed_Vtor == null || recordForVedNew3.List_recordForVed_Vtor.Count < num3)
      {
        --index;
      }
      else
      {
        int count = recordForVedNew3.List_recordForVed_Vtor.Count;
        int num5 = count - 1;
        int num6 = num3;
        int num7 = count / num3;
        int num8 = count - num7 * num3;
        bool isItogo = true;
        int k_do1;
        int k_from1;
        if (num8 == 0 || num8 == 1 || num8 == 2)
        {
          int k_from2 = count - 2 - num4;
          int k_do2 = count - 1;
          Vedomost_VB.RecordForVed_New recordForVedNew4 = recordForVedNew3.RecordForVed_New_Copy(k_from2, k_do2, isItogo);
          this._listRecordsVed_New.Insert(index + 1, recordForVedNew4);
          k_do1 = k_from2 - 1;
          k_from1 = k_do1 - num6 + 3 - num8;
          if (k_from1 < 0)
            k_from1 = 0;
          isItogo = false;
        }
        else
        {
          k_do1 = count - 1;
          k_from1 = k_do1 - num8 + 1;
        }
        while (k_from1 >= 0)
        {
          Vedomost_VB.RecordForVed_New recordForVedNew5 = recordForVedNew3.RecordForVed_New_Copy(k_from1, k_do1, isItogo);
          this._listRecordsVed_New.Insert(index + 1, recordForVedNew5);
          k_do1 = k_from1 - 1;
          k_from1 = k_do1 - num6 + 1;
          isItogo = false;
        }
        this._listRecordsVed_New.RemoveAt(index);
        --index;
      }
    }
  }

  private List<Vedomost_VB.OnePodRazdelVed> Get_ListPodrazdels(
    Vedomost_VB.RecordForVed_New recordForVed,
    List<Vedomost_VB.OneRazdelVed> list_RazdelVeds)
  {
    if (recordForVed == null || recordForVed.Razdel_Ved < 1L)
      return (List<Vedomost_VB.OnePodRazdelVed>) null;
    if (list_RazdelVeds == null || list_RazdelVeds.Count < 1)
      return (List<Vedomost_VB.OnePodRazdelVed>) null;
    int razdelVed1 = (int) recordForVed.Razdel_Ved;
    if (razdelVed1 < 1)
      return (List<Vedomost_VB.OnePodRazdelVed>) null;
    List<Vedomost_VB.OnePodRazdelVed> listPodrazdels = (List<Vedomost_VB.OnePodRazdelVed>) null;
    for (int index = 0; index < list_RazdelVeds.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed listRazdelVed = list_RazdelVeds[index];
      int razdelVed2 = listRazdelVed._razdelVed;
      if (razdelVed1 == razdelVed2)
        return listRazdelVed._list_onePodRazdels;
    }
    return listPodrazdels;
  }

  public void LoadSpecificationSortSchemaVed()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._sortSchema = (SortSchema) this.DocumentSettingsStructureVed.CreateSettingsLevelFromObject(sessionKeeper.Session, -1L, 1274, -1L, AvsIDCache.Attr_SortSchema, typeof (SortSchema));
  }

  /// <summary>Структура наследования настроек типа конструкторского документа</summary>
  /// <returns></returns>
  public SettingsStructure DocumentSettingsStructureVed
  {
    get
    {
      return AVSDocumentsSettings.Instance.GetAVSDocumentTypeSettings(Vedomost_VB_Static.GuidVP)?.SettingsInheritanceStructure;
    }
  }

  /// <summary>Сравнить текстовые строки ЦЕЛИКОМ</summary>
  /// <param name="strX">Строка 1</param>
  /// <param name="strY">Строка 2</param>
  /// <param name="numberCompare">Сравнивать числа по значению, а не как текст</param>
  /// <returns> меньше 0 - strX меньше чем strY;
  /// 0 strX равен strY;
  /// &gt; 0 strX больше чем strY.
  /// Цифры Меньше чем буквы
  /// </returns>
  public static int StringCompareForVed(
    string strX,
    string strY,
    bool numberCompare,
    int attributeId)
  {
    if (!numberCompare)
      return string.Compare(strX, strY);
    if (string.IsNullOrEmpty(strX) && string.IsNullOrEmpty(strY))
      return 0;
    if (!string.IsNullOrEmpty(strX) && string.IsNullOrEmpty(strY))
      return 1;
    if (string.IsNullOrEmpty(strX) && !string.IsNullOrEmpty(strY))
      return -1;
    if (strX == strY)
      return 0;
    int startIndex1 = 0;
    int startIndex2 = 0;
    int numberLength1 = 0;
    int numberLength2 = 0;
    int numberBegin1 = 0;
    int numberBegin2 = 0;
    double num1 = 0.0;
    double num2 = 0.0;
    NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
    ParserOptions options = ParserOptions.LEADINGWHITE | ParserOptions.TRAILINGWHITE | ParserOptions.DECIMAL | ParserOptions.THOUSANDS | ParserOptions.SCIENTIFIC | ParserOptions.PERCENT | ParserOptions.IgnoreTrailingText | ParserOptions.SkipLeadingText;
    ParsedNumberData number1;
    ParsedNumberData number2;
    string strA;
    string strB;
    int num3;
    int num4;
    int num5;
    while (true)
    {
      number1 = new ParsedNumberData();
      int num6 = NumberParserAdvanced.ParseNumber(strX, startIndex1, options, number1, currentInfo, out numberBegin1, out numberLength1) ? 1 : 0;
      number2 = new ParsedNumberData();
      int num7 = NumberParserAdvanced.ParseNumber(strY, startIndex2, options, number2, currentInfo, out numberBegin2, out numberLength2) ? 1 : 0;
      if ((num6 & num7) != 0)
      {
        if (numberBegin1 != 0 || numberBegin2 <= 0)
        {
          if (numberBegin2 != 0 || numberBegin1 <= 0)
          {
            int length = Math.Min(Math.Min(Math.Max(numberBegin1 - startIndex1, numberBegin2 - startIndex2), strX.Length - startIndex1), strY.Length - startIndex2);
            strA = strX.Substring(startIndex1, length);
            strB = strY.Substring(startIndex2, length);
            num3 = string.Compare(strA, strB);
            if (num3 == 0)
            {
              NumberParserAdvanced.NumberToDouble(number1, out num1);
              NumberParserAdvanced.NumberToDouble(number2, out num2);
              num4 = num1.CompareTo(num2);
              if (num4 == 0)
              {
                num5 = string.Compare(strX.Substring(startIndex1, numberBegin1 - startIndex1 + numberLength1), strY.Substring(startIndex2, numberBegin2 - startIndex2 + numberLength2));
                if (num5 == 0)
                {
                  startIndex1 = numberBegin1 + numberLength1;
                  startIndex2 = numberBegin2 + numberLength2;
                }
                else
                  goto label_24;
              }
              else
                goto label_22;
            }
            else
              goto label_17;
          }
          else
            goto label_15;
        }
        else
          break;
      }
      else
        goto label_26;
    }
    return -1;
label_15:
    return 1;
label_17:
    if (attributeId == AvsIDCache.Attr_Designation && (strA.EndsWith("-") || strB.EndsWith("-")) && strA.Length > 0 && strB.Length > 0 && !(strA.Remove(strA.Length - 1) != strB.Remove(strA.Length - 1)))
    {
      double num8 = 0.0;
      double num9 = 0.0;
      if (!strA.EndsWith("-") ? NumberParserAdvanced.NumberToDouble(number2, out num9) : NumberParserAdvanced.NumberToDouble(number1, out num8))
        return num8.CompareTo(num9);
    }
    return num3;
label_22:
    return num4;
label_24:
    return num5;
label_26:
    return string.Compare(strX.Substring(startIndex1), strY.Substring(startIndex2));
  }

  /// <summary> Сравнение двух записей ведомости по УСЛОВИЯМ </summary>
  /// <param name="recordForVed1"></param>
  /// <param name="recordForVed2"></param>
  /// <param name="sorting_Usl_VedOsn"></param>
  /// <returns></returns>
  public int StringCompareForVed2(
    Vedomost_VB.RecordForVed_New recordForVed1,
    Vedomost_VB.RecordForVed_New recordForVed2,
    Vedomost_VB.Sorting_Usl_One_From4 sorting_Usl_VedOsn)
  {
    if (recordForVed1 == null && recordForVed2 == null)
      return 0;
    if (recordForVed2 == null)
      return 1;
    if (recordForVed1 == null)
      return -1;
    if (sorting_Usl_VedOsn == null || sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel == null || sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Count == 0)
      return 0;
    int num = 0;
    long razdelVed1 = recordForVed1.Razdel_Ved;
    long razdelVed2 = recordForVed2.Razdel_Ved;
    if (razdelVed1 != razdelVed2)
      return razdelVed1 < razdelVed2 ? -1 : 1;
    Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel1 = (Vedomost_VB.Sorting_Usl_OneRazdel) null;
    for (int index = 0; index < sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel2 = sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel[index];
      if (sortingUslOneRazdel2._razdelNum == 0L || sortingUslOneRazdel2._razdelNum == razdelVed1)
      {
        sortingUslOneRazdel1 = sortingUslOneRazdel2;
        break;
      }
    }
    if (sortingUslOneRazdel1 == null || sortingUslOneRazdel1._list_sorting_Usl_One == null || sortingUslOneRazdel1._list_sorting_Usl_One.Count == 0)
      return 0;
    for (int index = 0; index < sortingUslOneRazdel1._list_sorting_Usl_One.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_One sorting_Usl_OneUsl = sortingUslOneRazdel1._list_sorting_Usl_One[index];
      num = Vedomost_VB.RecCompareForVed_OneUsl(recordForVed1, recordForVed2, sorting_Usl_OneUsl);
      if (num != 0)
        break;
    }
    return num;
  }

  public bool Sorting_list_RecordVed_New(
    List<Vedomost_VB.RecordForVed_New> listRecordsVed_New,
    Vedomost_VB.Sorting_Usl_One_From4 sorting_Usl)
  {
    bool flag1 = false;
    if (listRecordsVed_New == null || listRecordsVed_New.Count < 2 || sorting_Usl == null)
      return flag1;
    bool flag2 = true;
    while (flag2)
    {
      flag2 = false;
      for (int index = 0; index < listRecordsVed_New.Count - 1; ++index)
      {
        Vedomost_VB.RecordForVed_New recordForVed1 = listRecordsVed_New[index];
        Vedomost_VB.RecordForVed_New recordForVed2 = listRecordsVed_New[index + 1];
        recordForVed1.Designation();
        recordForVed2.Designation();
        if (this.StringCompareForVed2(recordForVed1, recordForVed2, sorting_Usl) > 0)
        {
          listRecordsVed_New.RemoveAt(index + 1);
          listRecordsVed_New.Insert(index, recordForVed2);
          flag2 = true;
        }
      }
    }
    return flag1;
  }

  public static int RecCompareForVed_OneUsl(
    Vedomost_VB.RecordForVed_New recordForVed1,
    Vedomost_VB.RecordForVed_New recordForVed2,
    Vedomost_VB.Sorting_Usl_One sorting_Usl_OneUsl)
  {
    if (recordForVed1 == null && recordForVed2 == null)
      return 0;
    if (recordForVed2 == null)
      return 1;
    if (recordForVed1 == null)
      return -1;
    if (sorting_Usl_OneUsl == null)
      return 0;
    string text1;
    string text2;
    if (sorting_Usl_OneUsl._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      text1 = recordForVed1.Get_Data_String_for_objType(sorting_Usl_OneUsl._objectType);
      text2 = recordForVed2.Get_Data_String_for_objType(sorting_Usl_OneUsl._objectType);
    }
    else
    {
      text1 = recordForVed1.Get_Data_String_for_TypeFieldVedRec(sorting_Usl_OneUsl._typeFieldVedRec);
      text2 = recordForVed2.Get_Data_String_for_TypeFieldVedRec(sorting_Usl_OneUsl._typeFieldVedRec);
    }
    return Vedomost_VB.StringCompareForVed_OneUsl(text1, text2, sorting_Usl_OneUsl);
  }

  public static int StringCompareForVed_OneUsl(
    string text1,
    string text2,
    Vedomost_VB.Sorting_Usl_One sorting_Usl_OneUsl)
  {
    if (text1 == "" && text2 == "")
      return 0;
    if (text1 == "" && text2 != "")
      return sorting_Usl_OneUsl._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce ? 1 : -1;
    if (text1 != "" && text2 == "")
      return sorting_Usl_OneUsl._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce ? -1 : 1;
    int iOtcuda1 = 0;
    int iSkolko1 = 500;
    int iOtcuda2 = 0;
    int iSkolko2 = 500;
    switch (sorting_Usl_OneUsl._beginSravn)
    {
      case Vedomost_VB.BeginSravn.S_begin:
        iOtcuda1 = 0;
        iOtcuda2 = 0;
        break;
      case Vedomost_VB.BeginSravn.S_pozicii:
        iOtcuda1 = sorting_Usl_OneUsl._num_symb_ot;
        iOtcuda2 = sorting_Usl_OneUsl._num_symb_ot;
        break;
      case Vedomost_VB.BeginSravn.Ot_symbola:
        if (sorting_Usl_OneUsl._symb_ot != null && sorting_Usl_OneUsl._symb_ot != "")
        {
          iOtcuda1 = text1.IndexOf(sorting_Usl_OneUsl._symb_ot);
          iOtcuda2 = text2.IndexOf(sorting_Usl_OneUsl._symb_ot);
          break;
        }
        break;
      case Vedomost_VB.BeginSravn.Ot_symbola_s_konca:
        if (sorting_Usl_OneUsl._symb_ot != null && sorting_Usl_OneUsl._symb_ot != "")
        {
          iOtcuda1 = text1.LastIndexOf(sorting_Usl_OneUsl._symb_ot);
          iOtcuda2 = text2.LastIndexOf(sorting_Usl_OneUsl._symb_ot);
          break;
        }
        break;
    }
    switch (sorting_Usl_OneUsl._endSravn)
    {
      case Vedomost_VB.EndSravn.Do_end:
        iSkolko1 = 500;
        iSkolko2 = 500;
        break;
      case Vedomost_VB.EndSravn.Skolko:
        iSkolko1 = sorting_Usl_OneUsl._num_symb_do;
        iSkolko2 = sorting_Usl_OneUsl._num_symb_do;
        break;
      case Vedomost_VB.EndSravn.Do_symbola:
        if (!string.IsNullOrEmpty(sorting_Usl_OneUsl._symb_do))
        {
          iSkolko1 = text1.IndexOf(sorting_Usl_OneUsl._symb_do) - iOtcuda1;
          iSkolko2 = text2.IndexOf(sorting_Usl_OneUsl._symb_do) - iOtcuda2;
          break;
        }
        break;
      case Vedomost_VB.EndSravn.Do_symbola_s_konca:
        if (!string.IsNullOrEmpty(sorting_Usl_OneUsl._symb_do))
        {
          iSkolko1 = text1.LastIndexOf(sorting_Usl_OneUsl._symb_do) - iOtcuda1;
          iSkolko2 = text2.LastIndexOf(sorting_Usl_OneUsl._symb_do) - iOtcuda2;
          break;
        }
        break;
    }
    if (iSkolko1 < 0)
      iSkolko1 = 0;
    if (iSkolko2 < 0)
      iSkolko2 = 0;
    string strX = Vedomost_VB.StrCopy(text1, iOtcuda1, iSkolko1);
    string str = Vedomost_VB.StrCopy(text2, iOtcuda2, iSkolko2);
    bool flag = sorting_Usl_OneUsl._sravnenie == Vedomost_VB.Sravnenie.Number;
    string strY = str;
    int num1 = flag ? 1 : 0;
    int objectType = sorting_Usl_OneUsl._objectType;
    int num2 = Vedomost_VB.StringCompareForVed(strX, strY, num1 != 0, objectType);
    if (sorting_Usl_OneUsl._poriadokSortirovki == Vedomost_VB.PoriadokSortirovki.Ubyvanie)
      num2 = -num2;
    return num2;
  }

  public void Ispytanie()
  {
    Vedomost_VB.Sorting_Usl_One sorting_Usl_OneUsl = new Vedomost_VB.Sorting_Usl_One();
    sorting_Usl_OneUsl._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola;
    sorting_Usl_OneUsl._symb_ot = "-";
    sorting_Usl_OneUsl._num_symb_ot = 1;
    sorting_Usl_OneUsl._endSravn = Vedomost_VB.EndSravn.Do_symbola;
    sorting_Usl_OneUsl._symb_do = "-";
    sorting_Usl_OneUsl._num_symb_do = 3;
    sorting_Usl_OneUsl._sravnenie = Vedomost_VB.Sravnenie.Symbol;
    Vedomost_VB.StringCompareForVed_OneUsl("AAA-001002-004-005", "AAA-001002-004-007", sorting_Usl_OneUsl);
    Vedomost_VB.StringCompareForVed_OneUsl("AAA-001002-004-005", "AAA-001002-005-007", sorting_Usl_OneUsl);
    Vedomost_VB.StringCompareForVed_OneUsl("AAA-001002-005-005", "AAA-001002-004-007", sorting_Usl_OneUsl);
    Vedomost_VB.StringCompareForVed_OneUsl("Винт М2.3456-90", "Винт М12.3456-90", sorting_Usl_OneUsl);
    Vedomost_VB.StringCompareForVed_OneUsl("Винт М2.3456-90", "Винт М1,2.3456-90", sorting_Usl_OneUsl);
    Vedomost_VB.StringCompareForVed_OneUsl("Винт М12.3456-90", "Винт М2.3456-90", sorting_Usl_OneUsl);
    Vedomost_VB.StringCompareForVed_OneUsl("Винт М1,2.3456-90", "Винт М2.3456-90", sorting_Usl_OneUsl);
  }

  public static string StrCopy(string text1, int iOtcuda, int iSkolko)
  {
    if (text1 == null)
      return "";
    if (iOtcuda < 0)
      return text1;
    int length = text1.Length;
    if (iOtcuda > length - 1)
      return "";
    if (iSkolko > length - iOtcuda)
      iSkolko = length - iOtcuda;
    return text1.Substring(iOtcuda, iSkolko);
  }

  /// <summary> Преобразование групповой А в Б </summary>
  public void ConvertGrAToGrB(bool isNew)
  {
    if (!this._isGroupVed)
      return;
    int index1 = this._listRecordsVed_New.Count - 1;
    while (index1 > -1)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index1];
      if (recordForVedNew.TypeRec == Vedomost_VB.TypeRec.TitleVar)
      {
        this._listRecordsVed_New.RemoveAt(index1);
        --index1;
      }
      else if (recordForVedNew.TypeRec == Vedomost_VB.TypeRec.TitleIsp)
      {
        this._listRecordsVed_New.RemoveAt(index1);
        --index1;
      }
      else if (recordForVedNew.TypeRec == Vedomost_VB.TypeRec.Title || recordForVedNew.TypeRec == Vedomost_VB.TypeRec.Title2 || recordForVedNew.TypeRec == Vedomost_VB.TypeRec.TitleIncluded || recordForVedNew.TypeRec == Vedomost_VB.TypeRec.TitlePart || recordForVedNew.TypeRec == Vedomost_VB.TypeRec.Empty || recordForVedNew.TypeRec == Vedomost_VB.TypeRec.NewPage)
      {
        this._listRecordsVed_New.RemoveAt(index1);
        --index1;
      }
      else
      {
        if (recordForVedNew.TypeRec == Vedomost_VB.TypeRec.RemarkShort)
        {
          bool flag = false;
          if (recordForVedNew.List_For_Rebuilding_From_Graf != null)
          {
            for (int index2 = 0; index2 < recordForVedNew.List_For_Rebuilding_From_Graf.Count; ++index2)
            {
              if (recordForVedNew.List_For_Rebuilding_From_Graf[index2].text == "Отсутствуют")
              {
                flag = true;
                break;
              }
            }
            if (flag)
            {
              this._listRecordsVed_New.RemoveAt(index1);
              --index1;
              continue;
            }
          }
        }
        --index1;
      }
    }
    if (this._listAll_IspolneniySp_prodInfo.Count > 1)
    {
      this.List_For_Isp = new List<Vedomost_VB.RecordForVed_For_Isp>();
      for (int index3 = 0; index3 < this._listAll_IspolneniySp_prodInfo.Count; ++index3)
      {
        ProductInfo productInfo = this._listAll_IspolneniySp_prodInfo[index3];
        this.List_For_Isp.Add(new Vedomost_VB.RecordForVed_For_Isp()
        {
          Ispolnenie = productInfo.Designation,
          Ispolnenie_Zagol = this._variables_Coordination.list_Captions[index3]
        });
      }
    }
    for (int index4 = 0; index4 < this._listRecordsVed_New.Count; ++index4)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New[index4];
      recordForVedNew.Get_Name();
      recordForVedNew.Get_Designation();
      recordForVedNew.Get_Gost();
    }
    int index5 = 0;
    while (index5 < this._listRecordsVed_New.Count)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew1 = this._listRecordsVed_New[index5];
      if (recordForVedNew1.TypeRec != Vedomost_VB.TypeRec.Info && recordForVedNew1.TypeRec != Vedomost_VB.TypeRec.Included && recordForVedNew1.TypeRec != Vedomost_VB.TypeRec.Main)
      {
        ++index5;
      }
      else
      {
        recordForVedNew1.List_recordForVed_For_Isp = new List<Vedomost_VB.RecordForVed_For_Isp>();
        for (int index6 = 0; index6 < this.List_For_Isp.Count; ++index6)
        {
          Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp1 = new Vedomost_VB.RecordForVed_For_Isp();
          Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp2 = this.List_For_Isp[index6];
          recordForVedForIsp1.Ispolnenie = recordForVedForIsp2.Ispolnenie;
          recordForVedForIsp1.Ispolnenie_Zagol = recordForVedForIsp2.Ispolnenie_Zagol;
          recordForVedNew1.List_recordForVed_For_Isp.Add(recordForVedForIsp1);
        }
        for (int index7 = 0; index7 < this.List_For_Isp.Count; ++index7)
        {
          Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp3 = this.List_For_Isp[index7];
          if (!(recordForVedNew1.Ispolnenie != "") || !(recordForVedForIsp3.Ispolnenie != recordForVedNew1.Ispolnenie))
          {
            Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp4 = recordForVedNew1.List_recordForVed_For_Isp[index7];
            if ((double) Math.Abs(recordForVedNew1.Count_Summ) <= 0.0)
              recordForVedForIsp4.Count_SummS = "X";
            else
              recordForVedForIsp4.Count_Summ = recordForVedNew1.Count_Summ;
          }
        }
        recordForVedNew1.Count_Summ = 0.0f;
        recordForVedNew1.Count_in_SpKompl = 0.0f;
        recordForVedNew1.Count_in_SpKompl_S = "";
        recordForVedNew1.Count_in_SpRegulir = 0.0f;
        recordForVedNew1.Count_in_SpRegulir_S = "";
        recordForVedNew1.Count_Vsego = 0.0f;
        recordForVedNew1.List_recordForVed_Vtor = (List<Vedomost_VB.RecordForVed_Vtor>) null;
        recordForVedNew1.Ispolnenie = "";
        string designation1 = recordForVedNew1.Get_Designation();
        string name1 = recordForVedNew1.Get_Name();
        string gost1 = recordForVedNew1.Get_Gost();
        string ispolnenie1 = recordForVedNew1.Ispolnenie;
        int index8 = this._listRecordsVed_New.Count - 1;
        while (index8 > index5)
        {
          Vedomost_VB.RecordForVed_New recordForVedNew2 = this._listRecordsVed_New[index8];
          string designation2 = recordForVedNew2.Get_Designation();
          string name2 = recordForVedNew2.Get_Name();
          string gost2 = recordForVedNew2.Get_Gost();
          string ispolnenie2 = recordForVedNew2.Ispolnenie;
          if (name1 != name2 || designation1 != designation2 || gost1 != gost2)
          {
            --index8;
          }
          else
          {
            for (int index9 = 0; index9 < this.List_For_Isp.Count; ++index9)
            {
              Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp = this.List_For_Isp[index9];
              if (!(ispolnenie2 != "") || !(recordForVedForIsp.Ispolnenie != ispolnenie2))
                recordForVedNew1.List_recordForVed_For_Isp[index9].Count_Summ += recordForVedNew2.Count_Summ;
            }
            for (int index10 = 0; index10 < this._listRecordsVed_New.Count; ++index10)
              this._listRecordsVed_New[index10].Get_Name();
            this._listRecordsVed_New.RemoveAt(index8);
            --index8;
            for (int index11 = 0; index11 < this._listRecordsVed_New.Count; ++index11)
              this._listRecordsVed_New[index11].Get_Name();
          }
        }
        ++index5;
      }
    }
    if (isNew)
      return;
    this._listRecordsVed_New.Sort((IComparer<Vedomost_VB.RecordForVed_New>) this);
  }

  /// <summary> Для визуального контроля при отладке </summary>
  public void checkRec()
  {
    Vedomost_VB.RecordForVed_New recordForVedNew = this._listRecordsVed_New.Count <= 1 ? this._listRecordsVed_New[0] : this._listRecordsVed_New[1];
    for (int index = 0; index < recordForVedNew.List_OneDataVed.Count; ++index)
    {
      Vedomost_VB.OneDataVed oneDataVed = recordForVedNew.List_OneDataVed[index];
    }
  }

  /// <summary> Для визуального контроля при отладке </summary>
  /// <param name="recordForVed_New"></param>
  public void checkRec1(Vedomost_VB.RecordForVed_New recordForVed_New)
  {
    for (int index = 0; index < recordForVed_New.List_OneDataVed.Count; ++index)
    {
      Vedomost_VB.OneDataVed oneDataVed = recordForVed_New.List_OneDataVed[index];
      if (oneDataVed.AttributeSourceTypes == AttributeSourceTypes.Object)
        MetaDataHelper.GetAttributeTypeName(oneDataVed.ObjectType);
    }
  }

  private void Summ_for_DP()
  {
    if (this._listRecordsVed_New.Count < 2)
      return;
    Vedomost_VB.RecordForVed_New recordForVedNew1 = this._listRecordsVed_New[0];
    string stringForObjType1 = recordForVedNew1.Get_Data_String_for_objType(AvsIDCache.Attr_DerzPodl);
    if (string.IsNullOrEmpty(stringForObjType1))
      return;
    int index = this._listRecordsVed_New.Count - 1;
    while (index > 0)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew2 = this._listRecordsVed_New[index];
      string stringForObjType2 = recordForVedNew2.Get_Data_String_for_objType(AvsIDCache.Attr_DerzPodl);
      if (string.IsNullOrEmpty(stringForObjType2))
        --index;
      else if (stringForObjType1 != stringForObjType2)
      {
        --index;
      }
      else
      {
        int listov = recordForVedNew1.Get_Listov() + recordForVedNew2.Get_Listov();
        recordForVedNew1.Set_Listov(listov);
        this._listRecordsVed_New.RemoveAt(index);
        --index;
      }
    }
  }

  /// <summary> Головная спецификация </summary>
  public AVSDocument SpecificationMain
  {
    [DebuggerStepThrough] get => this._specificationMain;
    [DebuggerStepThrough] set => this._specificationMain = value;
  }

  /// <summary> objectId ГОЛОВНОЙ СПЕЦИФИКАЦИИ </summary>
  public long ObjectIdMainSP
  {
    [DebuggerStepThrough] get => this._objectIdMainSP;
    [DebuggerStepThrough] set => this._objectIdMainSP = value;
  }

  /// <summary> imsObjectType ГОЛОВНОЙ СПЕЦИФИКАЦИИ </summary>
  public int ObjectTypeMainSp
  {
    [DebuggerStepThrough] get => this._objectTypeMainSp;
    [DebuggerStepThrough] set => this._objectTypeMainSp = value;
  }

  /// <summary> objectId ГОЛОВНОГО ИЗДЕЛИЯ </summary>
  public long ObjectIdMainArt
  {
    [DebuggerStepThrough] get => this._objectIdMainArt;
    [DebuggerStepThrough] set => this._objectIdMainArt = value;
  }

  public string DesignationArticle
  {
    get => this._designationArticle;
    set => this._designationArticle = value;
  }

  public string DesignationDoc
  {
    get => this._designationDoc;
    set => this._designationDoc = value;
  }

  /// Наименование Ведомости (изделия)
  public string NameArticle
  {
    get => this._nameArticle;
    set => this._nameArticle = value;
  }

  public string NameTypeDoc
  {
    get => this._nameTypeDoc;
    set => this._nameTypeDoc = value;
  }

  /// Код документа (например "ВП")
  public string KodDoc
  {
    get => this._kodDoc;
    set => this._kodDoc = value;
  }

  /// <summary> Конструктор класса Vedomost_VB </summary>
  public Vedomost_VB()
  {
    this._metodCreate = "Empty";
    this._metodFrom = "Empty";
    Vedomost_VB_Static.List_Ved_imsObjectType_Filled(false);
    Vedomost_VB_Static.Begin_For_Ved();
    Vedomost_VB_Static.List_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr_Filled(false);
    Vedomost_VB_Static.ListOneAttribVedRec_Init();
    Vedomost_VB_Static.ListOneAttribVedPasport_Init();
    this._listError_OneError.Clear();
  }

  public Vedomost_VB(Vedomost_VB.TypeDoc typeDoc)
  {
    if (typeDoc == Vedomost_VB.TypeDoc.Ved)
    {
      Vedomost_VB_Static.List_Ved_imsObjectType_Filled(false);
      Vedomost_VB_Static.List_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr_Filled(false);
      Vedomost_VB_Static.ListOneAttribVedRec_Init();
      Vedomost_VB_Static.ListOneAttribVedPasport_Init();
    }
    this._listError_OneError.Clear();
  }

  public StreamWriter txtProtocol_create(string fileName)
  {
    this._txtProtocol = new StreamWriter(fileName);
    this.txtProtocol_Add("ПРОТОКОЛ СБОРА ВЕДОМОСТИ");
    this._txtProtocol.WriteLine("");
    this._txtProtocol.WriteLine("Ведомость=" + this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ObjectName);
    this._txtProtocol.WriteLine("Обозначение изделия=" + this._designationArticle);
    this._txtProtocol.WriteLine("Обозначение документа=" + this.DesignationDoc);
    this._txtProtocol.WriteLine("Код документа=" + this.KodDoc);
    this._txtProtocol.WriteLine("Наименование изделия=" + this.NameArticle);
    this._txtProtocol.WriteLine("Тип документа=" + this.NameTypeDoc);
    this._txtProtocol.WriteLine("Групповая форма=" + this._groupForm.ToString());
    this._txtProtocol.WriteLine("");
    this._txtProtocol.WriteLine("------------------------------------------------");
    this._txtProtocol.WriteLine("Шаг = ПРЕДВАРИТЕЛЬНЫЙ СБОР СПЕЦИФИКАЦИЙ");
    this._txtProtocol.WriteLine("Порядок чтения спецификаций");
    this._txtProtocol.WriteLine("");
    return this._txtProtocol;
  }

  public void txtProtocol_Add(string text, int urovenN)
  {
    if (this._txtProtocol == null)
      return;
    --urovenN;
    --urovenN;
    string str = "";
    for (int index = 0; index < urovenN; ++index)
      str = "|  " + str;
    this._txtProtocol.WriteLine(str + text);
  }

  public void txtProtocol_Add(string text)
  {
    if (this._txtProtocol == null)
      return;
    this._txtProtocol.WriteLine(text);
  }

  public XmlDocument XmlProtocol_create()
  {
    this._xmlProtocol = new XmlDocument();
    this._xmlProtocol.InsertBefore((XmlNode) this._xmlProtocol.CreateXmlDeclaration("1.0", "windows-1251", "yes"), (XmlNode) this._xmlProtocol.DocumentElement);
    string localName = Vedomost_VB_Static.Replace_Invalid_Char(this._designationArticle, false);
    XmlElement element = this._xmlProtocol.CreateElement(string.Empty, localName, string.Empty);
    this._xmlProtocol.AppendChild((XmlNode) element);
    XmlAttribute attribute1 = this._xmlProtocol.CreateAttribute("Comment");
    attribute1.Value = "Последовательность чтения спецификаций";
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = this._xmlProtocol.CreateAttribute("Name");
    attribute2.Value = this._nameArticle;
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = this._xmlProtocol.CreateAttribute("Type");
    attribute3.Value = this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ObjectName;
    element.Attributes.Append(attribute3);
    XmlAttribute attribute4 = this._xmlProtocol.CreateAttribute("DocumentDesignation");
    attribute4.Value = this.DesignationArticle;
    element.Attributes.Append(attribute4);
    XmlAttribute attribute5 = this._xmlProtocol.CreateAttribute("DesignationDoc");
    attribute5.Value = this.DesignationDoc;
    element.Attributes.Append(attribute5);
    XmlAttribute attribute6 = this._xmlProtocol.CreateAttribute("DocumentName");
    attribute6.Value = this.NameArticle;
    element.Attributes.Append(attribute6);
    XmlAttribute attribute7 = this._xmlProtocol.CreateAttribute("NameTypeDoc");
    attribute7.Value = this.NameTypeDoc;
    element.Attributes.Append(attribute7);
    XmlAttribute attribute8 = this._xmlProtocol.CreateAttribute("KodDoc");
    attribute8.Value = this.KodDoc;
    element.Attributes.Append(attribute8);
    XmlAttribute attribute9 = this._xmlProtocol.CreateAttribute("FormaGroup");
    attribute9.Value = this._groupForm.ToString();
    element.Attributes.Append(attribute9);
    this._xmlElementCurr = element;
    return this._xmlProtocol;
  }

  public XmlElement XmlProtocol_Add(
    XmlDocument xmlProtocol,
    Vedomost_VB.RecordForMainVed recordForMainVed,
    string comment)
  {
    if (string.IsNullOrEmpty(recordForMainVed.Designation))
      return (XmlElement) null;
    string localName = Vedomost_VB_Static.Replace_Invalid_Char(recordForMainVed.Designation, false);
    XmlElement element = xmlProtocol.CreateElement(string.Empty, localName, string.Empty);
    if (element == null)
      return (XmlElement) null;
    if (comment != "")
    {
      XmlAttribute attribute = xmlProtocol.CreateAttribute("Комментарий");
      attribute.Value = comment;
      element.Attributes.Append(attribute);
    }
    XmlAttribute attribute1 = xmlProtocol.CreateAttribute("Кол.");
    attribute1.Value = recordForMainVed.CountS;
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlProtocol.CreateAttribute("Наименование");
    attribute2.Value = recordForMainVed.Name;
    element.Attributes.Append(attribute2);
    this._xmlElementCurr.AppendChild((XmlNode) element);
    return element;
  }

  /// <summary> Получение ItemIndex в списке ListFields по id т.к. в recordsSP будем читать поля по номеру </summary>
  /// <param _nameTypeRec="listIdFields"></param>
  /// <param _nameTypeRec="id"></param>
  /// <returns></returns>
  public int ItemIndexForID(List<Vedomost_VB.OneFieldSpForRead> listIdFields, int id)
  {
    for (int index = 0; index < listIdFields.Count; ++index)
    {
      if (listIdFields[index]._id == id)
        return index;
    }
    return -1;
  }

  /// <summary> Получение содержимого поля по id поля (если ЭТО поле есть в списке прочитанных полей) </summary>
  /// <param name="recordPasport"></param>
  /// <param name="id"></param>
  /// <param name="isItemIndex"></param>
  /// <returns></returns>
  public string Field_From_RecordSP(DataRow recordSp, int id, out bool isItemIndex)
  {
    isItemIndex = false;
    if (recordSp == null || id == 0)
      return "";
    int columnIndex = this.ItemIndexForID(this.listCommonId._listCommonId, id);
    if (columnIndex < 0)
      return "";
    isItemIndex = true;
    return recordSp[columnIndex] == DBNull.Value ? "" : Convert.ToString(recordSp[columnIndex]);
  }

  /// <summary> Получение содержимого поля по id поля (даже если поля нет в списке прочитанных полей) </summary>
  /// <param name="recordPasport"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public string Field_From_RecordSP_Extended(DataRow recordSp, int id)
  {
    if (recordSp == null || id == 0)
      return "";
    bool isItemIndex;
    string str = this.Field_From_RecordSP(recordSp, id, out isItemIndex);
    if (str == "" && !isItemIndex)
    {
      long int64 = Convert.ToInt64(this.Field_From_RecordSP(recordSp, -2, out isItemIndex));
      if (int64 != 0L)
        str = this.Get_OneField_ForObjectId(int64, id);
    }
    return str;
  }

  /// <summary> Получить objectId из записи СП </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public long Get_ObjectId_From_RecordSP_Extended(DataRow recordSp)
  {
    return Convert.ToInt64(this.Field_From_RecordSP_Extended(recordSp, -2));
  }

  /// <summary> Получить ObjectType из записи СП </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public int Get_ObjectType_From_RecordSP_Extended(DataRow recordSp)
  {
    return Convert.ToInt32(this.Field_From_RecordSP_Extended(recordSp, -7));
  }

  /// <summary> Получить Format из записи СП </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public string Get_Format_From_RecordSP_Extended(DataRow recordSp)
  {
    return this.Field_From_RecordSP_Extended(recordSp, AvsIDCache.Attr_Format);
  }

  /// <summary> Получить designation из записи СП </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public string Get_Designation_From_RecordSP_Extended(DataRow recordSp)
  {
    return this.Field_From_RecordSP_Extended(recordSp, AvsIDCache.Attr_Designation);
  }

  /// <summary> Получить Name из записи СП </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public string Get_Name_From_RecordSP_Extended(DataRow recordSp)
  {
    return this.Field_From_RecordSP_Extended(recordSp, AvsIDCache.Attr_Name);
  }

  /// <summary> Получить Note ОБЪЕКТА из записи СП </summary>
  /// <param name="recordSp"></param>
  /// <returns></returns>
  public string Get_NoteObj_From_RecordSP_Extended(DataRow recordSp)
  {
    return this.Field_From_RecordSP_Extended(recordSp, AvsIDCache.Attr_Note);
  }

  /// <summary> Получить тип документа для записи. Например "Ведомость покупных" </summary>
  /// <param name="recordPasport"></param>
  /// <returns></returns>
  public string Get_DocumentTypeName_ForObjectId(DataRow recordSp)
  {
    long recordSpExtended = this.Get_ObjectId_From_RecordSP_Extended(recordSp);
    string typeNameForObjectId = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(recordSpExtended);
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      int objectTypeId = objectInfo.ObjectTypeID;
      DocumentTypeSettings settings = customService.GetSettings(sessionGuid, objectTypeId);
      if (settings.DocumentNameInStamp)
        typeNameForObjectId = settings.DocumentTypeName;
    }
    return typeNameForObjectId;
  }

  public string Get_Format_ForObjectId(long objectId)
  {
    return this.Get_OneField_ForObjectId(objectId, AvsIDCache.Attr_Format);
  }

  public string Get_Designation_ForObjectId(long objectId)
  {
    return this.Get_OneField_ForObjectId(objectId, AvsIDCache.Attr_Designation);
  }

  public string Get_Name_ForObjectId(long objectId)
  {
    return this.Get_OneField_ForObjectId(objectId, AvsIDCache.Attr_Name);
  }

  public string Get_Note_ForObjectId(long objectId)
  {
    return this.Get_OneField_ForObjectId(objectId, AvsIDCache.Attr_Note);
  }

  /// <summary> Получение отдельного аттрибута для объекта </summary>
  /// <param name="objectId"></param>
  /// <param name="attrId"></param>
  /// <returns></returns>
  public string Get_OneField_ForObjectId(long objectId, int attrId)
  {
    string fieldForObjectId = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(attrId);
        if (attributeById != null)
        {
          if (attributeById.Value != DBNull.Value)
            fieldForObjectId = Convert.ToString(attributeById.Value);
        }
      }
    }
    return fieldForObjectId;
  }

  /// <summary> Получение отдельного аттрибута для объекта </summary>
  /// <param name="objectId"></param>
  /// <param name="attrId"></param>
  /// <returns></returns>
  public string Get_OneField_ForObjectId(long objectId, int attrId, SessionKeeper sk)
  {
    if (sk == null)
      return "";
    string fieldForObjectId = "";
    IDBObject dbObject = sk.Session.GetObject(objectId, false);
    if (dbObject != null)
    {
      IDBAttribute attributeById = dbObject.GetAttributeByID(attrId);
      if (attributeById != null && attributeById.Value != DBNull.Value)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
        if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
        {
          int int32 = Convert.ToInt32(attributeById.Value);
          QuickObjectInfo objectInfo = sk.Session.GetObjectInfo((long) int32);
          if (!objectInfo.Empty)
            fieldForObjectId = objectInfo.Caption;
        }
        else
          fieldForObjectId = Convert.ToString(attributeById.Value);
      }
    }
    return fieldForObjectId;
  }

  /// <summary> Поиск objectId в списке _listSpecifications (т.е. поиск спецификации в списке уже ЗАГРУЖЕННЫХ спецификаций, чтобы не грузить их заново) </summary>
  /// <param name="objectId"></param>
  /// <returns> Возвращает САМУ спецификацию</returns>
  public DataTable FindObjId_In_listSpecifications(long objectId)
  {
    if (objectId == 0L)
      return (DataTable) null;
    for (int index = 0; index < this._listSpecifications.Count; ++index)
    {
      Vedomost_VB.OneSpecification listSpecification = this._listSpecifications[index];
      if (objectId == listSpecification.ObjectId)
        return listSpecification.PartsDoc;
    }
    return (DataTable) null;
  }

  /// <summary> Получение списка всех типов существующих СП </summary>
  public void ReadListSpTyps()
  {
    this._listSpGuids = MetaDataHelper.GetObjectTypeChildrenGuidRecursive(Vedomost_VB_Static.GuidConctructorskyAll);
    this._listSpTyps = new List<IMSObjectType>();
    for (int index = 0; index < this._listSpGuids.Count; ++index)
      this._listSpTyps.Add(MetaDataHelper.GetObjectType(this._listSpGuids[index]));
  }

  /// <summary> Наименование раздела по его наименованию </summary>
  /// <param name="razdelCaption"></param>
  /// <returns></returns>
  public long FindRazdelNumByCaption(string razdelCaption)
  {
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.UpdateCacheSpecSections(sessionKeeper.Session, (IList<long>) null);
    }
    SpecificationSectionInfo sectionByCaption = SpecificationSectionInfo.FindSectionByCaption(razdelCaption);
    long razdelNumByCaption = -1;
    if (sectionByCaption != null)
      razdelNumByCaption = Convert.ToInt64(sectionByCaption.RazdelSP);
    return razdelNumByCaption;
  }

  /// <summary>Получить имя типа документа</summary>
  /// <param name="docType">Тип документа</param>
  /// <returns></returns>
  internal static string GetDocumentTypeName(int docType)
  {
    string documentTypeName = (string) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
      if (customService != null)
      {
        DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, docType);
        documentTypeName = !settings.DocumentNameInStamp ? "" : settings.DocumentTypeName;
      }
      return documentTypeName;
    }
  }

  /// <summary> Запись в файл протокола ОСНОВНОГО сбора одного шага с именем step </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="step"></param>
  private void Main_Dump_Add_Step(
    XmlDocument xmlDocument,
    XmlElement xmlElement_Kuda,
    string step,
    List<Vedomost_VB.RecordForMainVed> listRecordsForMainVed)
  {
    if (xmlDocument == null)
      return;
    switch (step)
    {
      case null:
        break;
      case "":
        break;
      default:
        string localName = Vedomost_VB_Static.Replace_Invalid_Char(step, false);
        XmlElement element = xmlDocument.CreateElement(string.Empty, localName, string.Empty);
        Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountRecs", listRecordsForMainVed.Count.ToString());
        for (int index = 0; index < listRecordsForMainVed.Count; ++index)
        {
          Vedomost_VB.RecordForMainVed recordForMainVed = listRecordsForMainVed[index];
          XmlElement newChild = this.Xml_recordForMainVed_Create(xmlDocument, element, recordForMainVed, index);
          if (newChild != null)
            element.AppendChild((XmlNode) newChild);
        }
        xmlElement_Kuda.AppendChild((XmlNode) element);
        break;
    }
  }

  /// <summary> Обработка одного recordForMainVed для XML </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="xmlElement_Kuda"></param>
  /// <param name="recordForMainVed"></param>
  /// <param name="i"></param>
  /// <returns></returns>
  private XmlElement Xml_recordForMainVed_Create(
    XmlDocument xmlDocument,
    XmlElement xmlElement_Kuda,
    Vedomost_VB.RecordForMainVed recordForMainVed,
    int i)
  {
    if (recordForMainVed == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "RecordForMainVed", string.Empty);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Nзаписи", (i + 1).ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Designation", recordForMainVed.Designation);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Name", recordForMainVed.Name);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Ispolnenie", recordForMainVed.Ispolnenie);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "KudaDesignation", recordForMainVed.KudaDesignation);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "KudaObjectId", recordForMainVed.KudaObjectId.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Format", recordForMainVed.Format);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Zone", recordForMainVed.Zone);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Uroven", recordForMainVed.Uroven);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "UrovenN", recordForMainVed.UrovenN.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Position", recordForMainVed.Position);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "ObjectIdIzd", recordForMainVed.ObjectIdIzd.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "ObjectIdDoc", recordForMainVed.ObjectIdDoc.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "ObjectType", recordForMainVed.ObjectType.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountS", recordForMainVed.CountS);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountF", recordForMainVed.CountF.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountSummF", recordForMainVed.CountSummF.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EdIzmKol", recordForMainVed.EdIzmKol);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "DerzPodl", recordForMainVed.DerzPodl);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Remark", recordForMainVed.Remark);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "IsTherezKomplekt", recordForMainVed.IsTherezKomplekt.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EtaSp_Komplekt", recordForMainVed.EtaSp_Komplekt.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "IsTherezDopZam", recordForMainVed.IsTherezDopZam.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EtaSp_DopZam", recordForMainVed.EtaSp_DopZam.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EstSvoiaVedomost", recordForMainVed.EstSvoiaVedomost.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "NeRaskryvat", recordForMainVed.NeRaskryvat.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "_articleGroupID", recordForMainVed._articleGroupID.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "_F_PRJLINK_ID", recordForMainVed._F_PRJLINK_ID.ToString());
    if (recordForMainVed.List_recordForMainVedVtor == null)
    {
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "List_recordForMainVedVtor", "null");
    }
    else
    {
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountVtorRecs", recordForMainVed.List_recordForMainVedVtor.Count.ToString());
      XmlElement element2 = xmlDocument.CreateElement(string.Empty, "vedVtor", string.Empty);
      for (int index = 0; index < recordForMainVed.List_recordForMainVedVtor.Count; ++index)
      {
        Vedomost_VB.RecordForMainVedVtor recordForMainVedVtor = recordForMainVed.List_recordForMainVedVtor[index];
        XmlElement newChild = this.Xml_RecordForMainVedVtor_Create(xmlDocument, element2, recordForMainVedVtor, index);
        if (newChild != null)
          element2.AppendChild((XmlNode) newChild);
      }
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  /// <summary> Обработка одного ВТОРИЧНОГО recordForMainVedVtor для XML </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="xmlElement_Kuda"></param>
  /// <param name="recordForMainVedVtor"></param>
  /// <param name="i"></param>
  /// <returns></returns>
  private XmlElement Xml_RecordForMainVedVtor_Create(
    XmlDocument xmlDocument,
    XmlElement xmlElement_Kuda,
    Vedomost_VB.RecordForMainVedVtor recordForMainVedVtor,
    int i)
  {
    if (recordForMainVedVtor == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (recordForMainVedVtor), string.Empty);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "N_втор_записи", (i + 1).ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "KudaDesignation", recordForMainVedVtor.KudaDesignation);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountS1", recordForMainVedVtor.CountS1);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountF1", recordForMainVedVtor.CountF1.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountSn", recordForMainVedVtor.CountSn);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountFn", recordForMainVedVtor.CountFn.ToString());
    return element;
  }

  /// <summary> СОЗДАНИЕ файла протокола сбора ВЕДОМОСТИ </summary>
  /// <param name="dumpDirectory"></param>
  /// <param name="fileNameDump"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  private (XmlDocument, XmlElement) Ved_Dump_Create(
    string dumpDirectory,
    string fileNameDump,
    string name)
  {
    if (dumpDirectory == null || dumpDirectory == "")
      return ((XmlDocument) null, (XmlElement) null);
    if (fileNameDump == null || fileNameDump == "")
      return ((XmlDocument) null, (XmlElement) null);
    string localName = Vedomost_VB_Static.Replace_Invalid_Char(name, false);
    XmlDocument xmlDocument = new XmlDocument();
    XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "windows-1251", "yes");
    XmlElement documentElement = xmlDocument.DocumentElement;
    xmlDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) documentElement);
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, localName, string.Empty);
    xmlDocument.AppendChild((XmlNode) element1);
    XmlElement element2 = xmlDocument.CreateElement(string.Empty, "PASSPORT", string.Empty);
    element1.AppendChild((XmlNode) element2);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "DesignationArticle", this.DesignationArticle);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "DesignationDoc", this.DesignationDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "NameArticle", this.NameArticle);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "NameTypeDoc", this.NameTypeDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "KodDoc", this.KodDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_groupForm", this._groupForm.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_is_Golovnaia_Sp_Komplekt", this._is_Golovnaia_Sp_Komplekt.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_imsObjectType_RazrabatyvaemoiVed", this._imsObjectType_RazrabatyvaemoiVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_one_Ved_Nastr_RazrabatyvaemoiVed", this._one_Ved_Nastr_RazrabatyvaemoiVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_guidTypeVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._guidTypeVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_guidParent", this._one_Ved_Nastr_RazrabatyvaemoiVed._guidParent.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_typeVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_nameVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._nameVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_vedomostTemplateObjectGuid", this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_vedomostTemplateObjectGuid_B", this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid_B.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_typeCreate", this._one_Ved_Nastr_RazrabatyvaemoiVed._typeCreate.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_imsObjectType", this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ToString());
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._dateIni != null)
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_dateIni", this._one_Ved_Nastr_RazrabatyvaemoiVed._dateIni.ToString());
    XmlElement element3 = xmlDocument.CreateElement(string.Empty, "DATA", string.Empty);
    element1.AppendChild((XmlNode) element3);
    return (xmlDocument, element3);
  }

  /// <summary> СОЗДАНИЕ XML файла ВЕДОМОСТИ </summary>
  private void Ved_XML_File_Create_From_ListRecordsVed_New()
  {
    if (!Vedomost_VB_Static.isComputerName_Victor && !Vedomost_VB_Static.isHozain)
      return;
    string textIn = $"{Vedomost_VB_Static.DirectoryDump}\\{this.DesignationDoc}.xml";
    XmlDocument xmlDocument = new XmlDocument();
    XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "windows-1251", "yes");
    XmlElement documentElement = xmlDocument.DocumentElement;
    xmlDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) documentElement);
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "Vedomost", string.Empty);
    xmlDocument.AppendChild((XmlNode) element1);
    XmlElement element2 = xmlDocument.CreateElement(string.Empty, "PASSPORT", string.Empty);
    element1.AppendChild((XmlNode) element2);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "DesignationArticle", this.DesignationArticle);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "DesignationDoc", this.DesignationDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "NameArticle", this.NameArticle);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "NameTypeDoc", this.NameTypeDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "KodDoc", this.KodDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_groupForm", this._groupForm.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_is_Golovnaia_Sp_Komplekt", this._is_Golovnaia_Sp_Komplekt.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_imsObjectType_RazrabatyvaemoiVed", this._imsObjectType_RazrabatyvaemoiVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_one_Ved_Nastr_RazrabatyvaemoiVed", this._one_Ved_Nastr_RazrabatyvaemoiVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_guidTypeVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._guidTypeVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_guidParent", this._one_Ved_Nastr_RazrabatyvaemoiVed._guidParent.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_typeVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._typeVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_nameVed", this._one_Ved_Nastr_RazrabatyvaemoiVed._nameVed.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_vedomostTemplateObjectGuid", this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_vedomostTemplateObjectGuid_B", this._one_Ved_Nastr_RazrabatyvaemoiVed._vedomostTemplateObjectGuid_B.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_typeCreate", this._one_Ved_Nastr_RazrabatyvaemoiVed._typeCreate.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_imsObjectType", this._one_Ved_Nastr_RazrabatyvaemoiVed._imsObjectType.ToString());
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed._dateIni != null)
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element2, "_dateIni", this._one_Ved_Nastr_RazrabatyvaemoiVed._dateIni.ToString());
    for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
    {
      Vedomost_VB.RecordForVed_New recordForVed_New = this._listRecordsVed_New[index];
      XmlElement newChild = this.Xml_recordForVed_New_Create(xmlDocument, element1, recordForVed_New, index);
      if (newChild != null)
        element1.AppendChild((XmlNode) newChild);
    }
    string filename = Vedomost_VB_Static.Replace_Invalid_Char(textIn, true);
    xmlDocument.Save(filename);
  }

  /// <summary> Запись в файл протокола сбора ВЕДОМОСТИ одного шага с именем step </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="step"></param>
  private void Ved_Dump_Add_Step(XmlDocument xmlDocument, XmlElement xmlElement_Kuda, string step)
  {
    if (xmlDocument == null)
      return;
    switch (step)
    {
      case null:
        break;
      case "":
        break;
      default:
        string localName = Vedomost_VB_Static.Replace_Invalid_Char(step, false);
        XmlElement element = xmlDocument.CreateElement(string.Empty, localName, string.Empty);
        Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountRecs", this._listRecordsVed_New.Count.ToString());
        for (int index = 0; index < this._listRecordsVed_New.Count; ++index)
        {
          Vedomost_VB.RecordForVed_New recordForVed_New = this._listRecordsVed_New[index];
          XmlElement newChild = this.Xml_recordForVed_New_Create(xmlDocument, element, recordForVed_New, index);
          if (newChild != null)
            element.AppendChild((XmlNode) newChild);
        }
        xmlElement_Kuda.AppendChild((XmlNode) element);
        break;
    }
  }

  /// <summary> Обработка одного recordForVed_New для XML </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="xmlElement_Kuda"></param>
  /// <param name="recordForVed_New"></param>
  /// <param name="i"></param>
  /// <returns></returns>
  private XmlElement Xml_recordForVed_New_Create(
    XmlDocument xmlDocument,
    XmlElement xmlElement_Kuda,
    Vedomost_VB.RecordForVed_New recordForVed_New,
    int i)
  {
    if (recordForVed_New == null)
      return (XmlElement) null;
    XmlElement element1 = xmlDocument.CreateElement(string.Empty, "RecordForVed_New", string.Empty);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "N_записи", (i + 1).ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "TypeRec", recordForVed_New.TypeRec.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "KudaDesignation", recordForVed_New.KudaDesignation);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "KudaObjectId", recordForVed_New.KudaObjectId.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Ispolnenie", recordForVed_New.Ispolnenie);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "DocumentTypeName", recordForVed_New.DocumentTypeName);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Razdel_Ved", recordForVed_New.Razdel_Ved.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "PodRazdel_Ved", recordForVed_New.PodRazdel_Ved.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_Sp_S", recordForVed_New.Count_in_Sp_S);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_Sp", recordForVed_New.Count_in_Sp.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_Izdelie", recordForVed_New.Count_in_Izdelie.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_SpKompl_S", recordForVed_New.Count_in_SpKompl_S);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_SpKompl", recordForVed_New.Count_in_SpKompl.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_SpRegulir_S", recordForVed_New.Count_in_SpRegulir_S);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_in_SpRegulir", recordForVed_New.Count_in_SpRegulir.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountF_samOi_sp", recordForVed_New.CountF_samOi_sp.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_Vsego", recordForVed_New.Count_Vsego.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Count_Summ", recordForVed_New.Count_Summ.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EdIzmKol", recordForVed_New.EdIzmKol);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "Remark", recordForVed_New.Remark);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "DerzPodlDoc", recordForVed_New.DerzPodlDoc);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "UrovenS", recordForVed_New.UrovenS);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "UrovenN", recordForVed_New.UrovenN.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "IsTherezKomplekt", recordForVed_New.IsTherezKomplekt.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EtaSp_Komplekt", recordForVed_New.EtaSp_Komplekt.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "IsTherezDopZam", recordForVed_New.IsTherezDopZam.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "EtaSp_DopZam", recordForVed_New.EtaSp_DopZam.ToString());
    if (recordForVed_New.List_OneDataVed != null && recordForVed_New.List_OneDataVed.Count > 0)
    {
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "OneDataVed_Count", recordForVed_New.List_OneDataVed.Count.ToString());
      XmlElement xmlElement = this.Xml_List_OneDataVed_Create(xmlDocument, recordForVed_New);
      for (int index = 0; index < recordForVed_New.List_OneDataVed.Count; ++index)
      {
        Vedomost_VB.OneDataVed oneDataVed = recordForVed_New.List_OneDataVed[index];
        Vedomost_VB_Static.InsertToXml_Text(xmlDocument, xmlElement, "AttributeSourceTypes", oneDataVed.AttributeSourceTypes.ToString());
        Vedomost_VB_Static.InsertToXml_Text(xmlDocument, xmlElement, "ObjectType", oneDataVed.ObjectType.ToString());
        string name = Vedomost_VB_Static.Replace_Invalid_Char(MetaDataHelper.GetAttributeTypeName(oneDataVed.ObjectType), false);
        string stringForObjType = recordForVed_New.Get_Data_String_for_objType(oneDataVed.ObjectType);
        Vedomost_VB_Static.InsertToXml_Text(xmlDocument, xmlElement, name, stringForObjType);
      }
      if (xmlElement != null)
        element1.AppendChild((XmlNode) xmlElement);
    }
    if (recordForVed_New.List_recordForVed_Vtor == null)
    {
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "List_recordForVed_Vtor", "null");
    }
    else
    {
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountVtorRecs", recordForVed_New.List_recordForVed_Vtor.Count.ToString());
      XmlElement element2 = xmlDocument.CreateElement(string.Empty, "vedVtor", string.Empty);
      for (int index = 0; index < recordForVed_New.List_recordForVed_Vtor.Count; ++index)
      {
        Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor = recordForVed_New.List_recordForVed_Vtor[index];
        XmlElement newChild = this.Xml_RecordForVedVtor_Create(xmlDocument, element2, recordForVed_Vtor, index);
        if (newChild != null)
          element2.AppendChild((XmlNode) newChild);
      }
      element1.AppendChild((XmlNode) element2);
    }
    if (recordForVed_New.List_recordForVed_For_Isp != null)
    {
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "CountIspRecs", recordForVed_New.List_recordForVed_For_Isp.Count.ToString());
      XmlElement element3 = xmlDocument.CreateElement(string.Empty, "Ved_For_Isp", string.Empty);
      for (int index = 0; index < recordForVed_New.List_recordForVed_For_Isp.Count; ++index)
      {
        Vedomost_VB.RecordForVed_For_Isp recordForVed_For_Isp = recordForVed_New.List_recordForVed_For_Isp[index];
        XmlElement newChild = this.Xml_RecordForVed_For_Isp_Create(xmlDocument, element3, recordForVed_For_Isp, index);
        if (newChild != null)
          element3.AppendChild((XmlNode) newChild);
      }
      element1.AppendChild((XmlNode) element3);
    }
    return element1;
  }

  /// <summary> Создание секции List_OneDataVed </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="recordForVed_New"></param>
  /// <returns></returns>
  private XmlElement Xml_List_OneDataVed_Create(
    XmlDocument xmlDocument,
    Vedomost_VB.RecordForVed_New recordForVed_New)
  {
    if (xmlDocument == null)
      return (XmlElement) null;
    return recordForVed_New == null ? (XmlElement) null : xmlDocument.CreateElement(string.Empty, "List_OneDataVed", string.Empty);
  }

  /// <summary> Обработка одного ВТОРИЧНОГО recordForVed_Vtor для XML </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="xmlElement_Kuda"></param>
  /// <param name="recordForVed_Vtor"></param>
  /// <param name="i"></param>
  /// <returns></returns>
  private XmlElement Xml_RecordForVedVtor_Create(
    XmlDocument xmlDocument,
    XmlElement xmlElement_Kuda,
    Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor,
    int i)
  {
    if (recordForVed_Vtor == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (recordForVed_Vtor), string.Empty);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "N_втор_записи", (i + 1).ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "KudaDesignation", recordForVed_Vtor.KudaDesignation);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "KudaObjectId", recordForVed_Vtor.KudaObjectId.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_Sp_S", recordForVed_Vtor.Count_in_Sp_S);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_Sp", recordForVed_Vtor.Count_in_Sp.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_Izdelie", recordForVed_Vtor.Count_in_Izdelie.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_SpKompl_S", recordForVed_Vtor.Count_in_SpKompl_S);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_SpKompl", recordForVed_Vtor.Count_in_SpKompl.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_SpRegulir_S", recordForVed_Vtor.Count_in_SpRegulir_S);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_in_SpRegulir", recordForVed_Vtor.Count_in_SpRegulir.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "CountF_samOi_sp", recordForVed_Vtor.CountF_samOi_sp.ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_Vsego", recordForVed_Vtor.Count_Vsego.ToString());
    if (recordForVed_Vtor.List_OneDataVed == null)
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "List_OneDataVed", "null");
    return element;
  }

  /// <summary> Обработка одного ВТОРИЧНОГО recordForVed_For_Isp для XML </summary>
  /// <param name="xmlDocument"></param>
  /// <param name="xmlElement_Kuda"></param>
  /// <param name="recordForVed_For_Isp"></param>
  /// <param name="i"></param>
  /// <returns></returns>
  private XmlElement Xml_RecordForVed_For_Isp_Create(
    XmlDocument xmlDocument,
    XmlElement xmlElement_Kuda,
    Vedomost_VB.RecordForVed_For_Isp recordForVed_For_Isp,
    int i)
  {
    if (recordForVed_For_Isp == null)
      return (XmlElement) null;
    XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (recordForVed_For_Isp), string.Empty);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "N_втор_записи", (i + 1).ToString());
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Ispolnenie", recordForVed_For_Isp.Ispolnenie);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Ispolnenie_Zagol", recordForVed_For_Isp.Ispolnenie_Zagol);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_SummS", recordForVed_For_Isp.Count_SummS);
    Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "Count_Summ", recordForVed_For_Isp.Count_Summ.ToString());
    return element;
  }

  public string Get_Data_String_for_TypeFieldVedPasport(
    Vedomost_VB.TypeFieldVedPasport typeFieldVedPasport)
  {
    string typeFieldVedPasport1 = "";
    switch (typeFieldVedPasport)
    {
      case Vedomost_VB.TypeFieldVedPasport.DesignationIzd:
        typeFieldVedPasport1 = this._designationArticle;
        break;
      case Vedomost_VB.TypeFieldVedPasport.DesignationDoc:
        typeFieldVedPasport1 = this._designationDoc;
        break;
      case Vedomost_VB.TypeFieldVedPasport.KodDoc:
        typeFieldVedPasport1 = this._kodDoc;
        break;
      case Vedomost_VB.TypeFieldVedPasport.NameArticle:
        typeFieldVedPasport1 = this._nameArticle;
        break;
      case Vedomost_VB.TypeFieldVedPasport.NameTypeDoc:
        typeFieldVedPasport1 = this._nameTypeDoc;
        break;
    }
    return typeFieldVedPasport1;
  }

  /// <summary> Создание ведомости шаг 1 Экспериментальный. В дальнейшем удалим. </summary>
  public void CreateVedomost5()
  {
    bool flag = false;
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
      relationCollection.ObjectTypeID = AvsIDCache.ObjType_Product;
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) AvsIDCache.Attr_ArticleGroupID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) AvsIDCache.Attr_Designation, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0),
        new ColumnDescriptor((object) AvsIDCache.Attr_Name, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      ProductInfo productInfo = (ProductInfo) null;
      paramSet.LastKeyValue = 0L;
      paramSet.LastOrderValue = (object) null;
      while (!flag)
      {
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, this._objectIdMainSP);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          productInfo = new ProductInfo();
          productInfo.Id = Convert.ToInt64(row[0]);
          if (row[1] != DBNull.Value)
            productInfo.ArticleGroupID = new Guid(Convert.ToString(row[1]));
          productInfo.ObjectType = Convert.ToInt32(row[2]);
          productInfo.Designation = Convert.ToString(row[3]);
          productInfo.Name = Convert.ToString(row[4]);
          productInfoList.Add(productInfo);
        }
        flag = Convert.ToBoolean(dataTable.ExtendedProperties[(object) "Eof"]);
        if (!flag && dataTable.Rows.Count > 0 && productInfo != null)
        {
          paramSet.LastKeyValue = productInfo.Id;
          paramSet.LastOrderValue = (object) productInfo.Designation;
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectIdMainSP, false);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID);
      if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
        this._articleGroupIDMainSp = new Guid(attributeById.Value.ToString());
      AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
      for (int index = 0; index < attributesValues.Length; ++index)
      {
        if (attributesValues[index].AttributeID == AvsIDCache.Attr_ArticleGroupID && attributesValues[index].Values[0] != DBNull.Value)
        {
          Guid guid = new Guid(Convert.ToString(attributesValues[index].Values[0]));
        }
      }
    }
  }

  /// <summary> Чисто экспериментальная функция</summary>
  /// <param name="avsDocument"></param>
  public void CheckRecords(AVSDocument avsDocument)
  {
    List<AVSRow> allRows = avsDocument.GetAllRows(true, true);
    int count = allRows.Count;
    for (int index = 0; index < count; ++index)
    {
      AVSRow avsRow = allRows[index];
    }
  }

  /// <summary>
  /// Обработчик события после завершения разбивки документа
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void document_BackgroundThreadsFinished(object sender, BackgroundThreadsFinishedArgs e)
  {
    if (sender == null || !(sender is ImDocument document))
      return;
    int pageCount = document.PageCount;
    if (document == null || e.ThreadTypes != DocumentBackgroundThreadType.DistributeThread)
      return;
    this.FillProductHeadersOnPages(document);
  }

  /// <summary> Для формы Б заполнение Заголовка ОБОЗНАЧЕНИЯ номеров исполнений для ДОКУМЕНТА </summary>
  /// <param name="document"></param>
  public void FillProductHeadersOnPages(ImDocument document)
  {
    int index1 = 0;
    int num1 = 0;
    foreach (PageData pageData in (ImDocumentData) document)
    {
      string attributeValue = (document.Nodes[index1] as PageData).GetAttributeValue("iCikl", true);
      if (!string.IsNullOrEmpty(attributeValue))
        num1 = int.Parse(attributeValue) * 10;
      ++index1;
      if (pageData.FindFirstNodeByName("Заголовок таблицы исполнений") is TextData firstNodeByName1)
      {
        string str = firstNodeByName1.Text;
        if (string.IsNullOrEmpty(str))
          str = Vedomost_VB_Static.Kol_na_ispoln_Template;
        firstNodeByName1.AssignText(string.Format(str + " {0} -", (object) this._designationArticle), false, true, false, false, false);
      }
      if (pageData.FindFirstNodeByName("Номера исполнений") is TableData firstNodeByName2)
      {
        int count = this._variables_Coordination.list_Variables.Count;
        int num2 = num1 + 10;
        if (this._variables_Coordination != null && this._variables_Coordination.list_Variables.Count <= count)
        {
          int index2 = -1;
          int index3 = num1;
          while (true)
          {
            if (index3 < num2 && index3 < this._variables_Coordination.list_Variables.Count)
            {
              ++index2;
              if (firstNodeByName2.Nodes[index2] is TextData node)
              {
                string listVariable = this._variables_Coordination.list_Variables[index3];
                string listCaption = this._variables_Coordination.list_Captions[index3];
                node.AssignText(listCaption, false, true, false, false, false);
              }
              ++index3;
            }
            else
              goto label_21;
          }
        }
        else
        {
          int index4 = 0;
          while (true)
          {
            if (index4 < this._variables_Coordination.list_Captions.Count && index4 < firstNodeByName2.Nodes.Count)
            {
              if (firstNodeByName2.Nodes[index4] is TextData node)
              {
                string listCaption = this._variables_Coordination.list_Captions[index4];
                if (!string.IsNullOrEmpty(listCaption))
                  node.AssignText(listCaption, false, true, false, false, false);
                else
                  goto label_21;
              }
              ++index4;
            }
            else
              goto label_21;
          }
        }
      }
      else
        continue;
label_21:;
    }
  }

  /// <summary> Создание ОДНОЙ записи на основе RecordForVed_New. Вторичные создаются, но не заполняются. </summary>
  /// <param name="recordForVed_New"></param>
  /// <param name="docTemplate"></param>
  /// <param from="recordForVed_New">Какой командой заполнялась запись</param>
  /// <returns></returns>
  public TableData Create_DocRow_Info(
    Vedomost_VB.RecordForVed_New recordForVed_New,
    ImDocument docTemplate,
    ImDocument document,
    string from,
    bool quiet,
    string name_PageTemplate)
  {
    bool flag = false;
    string nameRow = "Основная строка";
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint printByRecordNew = this.OneRecordToPrint_By_RecordNew(this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint, recordForVed_New);
    if (printByRecordNew != null && !string.IsNullOrEmpty(printByRecordNew._tableRowId))
      nameRow = printByRecordNew._tableRowId.ToString();
    TableData tableData1 = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, nameRow);
    if (tableData1 == null)
      return (TableData) null;
    TableData docRow = tableData1.CloneFromTemplate() as TableData;
    if (recordForVed_New != null && printByRecordNew != null)
      flag = this.Filled_Record_DocRow_Ved(docRow, recordForVed_New, printByRecordNew, 0, 0, document, (PageData) null);
    if (flag)
      this.Filled_Record_DocRow_Ved_Attributes(docRow, recordForVed_New, from);
    if (flag && printByRecordNew._isVtorOblast && printByRecordNew._tableVtorOblastId != "0" && !string.IsNullOrWhiteSpace(printByRecordNew._tableVtorOblastId.ToString()))
    {
      if (docTemplate.FindNode(printByRecordNew._oneRecordToPrint_Vtor._tableRowId.ToString()) is TableData tableData2)
        tableData2 = docTemplate.FindFirstChildNodeByName(printByRecordNew._oneRecordToPrint_Vtor._tableRowId.ToString()) as TableData;
      if (tableData2 != null)
      {
        TableData parentCell = tableData2.ParentCell;
        if (parentCell != null)
        {
          TableData templateRecursive = docRow.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) parentCell) as TableData;
          TableData child = tableData2.CloneFromTemplate() as TableData;
          if (templateRecursive != null && child != null)
            templateRecursive.AddChildNode((DocumentTreeNode) child, false, false);
        }
      }
    }
    if (flag | quiet)
    {
      if (this._one_Ved_Nastr_RazrabatyvaemoiVed._protection_From_Editing._isProhibition_DocRowWithObj && docRow != null)
        Vedomost_VB_Static.DocRow_ReadOnly((DocumentTreeNode) docRow, true, true);
      return docRow;
    }
    int num = (int) MessageBox.Show("Нет данных для внесения в запись\r\n\r\nЗапись не создана", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    return (TableData) null;
  }

  /// <summary> Получить строку из шаблона с учетом имени СТРАНИЦЫ шаблона </summary>
  /// <param name="docTemplate"></param>
  /// <param name="name_PageTemplate"></param>
  /// <param name="nameRow"></param>
  /// <returns></returns>
  public TableData Select_Stroka_IzShablona(
    ImDocument docTemplate,
    string name_PageTemplate,
    string nameRow)
  {
    string nodeName = "Главная таблица";
    if (string.IsNullOrEmpty(name_PageTemplate))
    {
      if (!(docTemplate.FindNode(nameRow) is TableData tableData2))
        tableData2 = docTemplate.FindFirstChildNodeByName(nameRow) as TableData;
    }
    else if (!(((docTemplate.FindFirstChildNodeByName(name_PageTemplate) as PageData).FindFirstChildNodeByName(nodeName) as TableData).FindFirstChildNodeByName(nameRow) is TableData tableData2))
      tableData2 = docTemplate.FindNode(nameRow) as TableData;
    return tableData2;
  }

  /// <summary> Создание ДОПОЛНИТЕЛЬНОЙ </summary>
  /// <param name="recordForVed_New"></param>
  /// <param name="docTemplate"></param>
  /// <param from="recordForVed_New">Какой командой заполнялась запись</param>
  /// <returns></returns>
  public TableData Create_DocRow_Additional(
    ImDocument docTemplate,
    ImDocument document,
    string from,
    int numberAdditional,
    string name_PageTemplate)
  {
    Vedomost_VB.OneRecordToPrint oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    string nameRow = "Дополнительная";
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null)
      return (TableData) null;
    switch (numberAdditional)
    {
      case 1:
        nameRow += " 1";
        oneRecordToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintAdditional1;
        break;
      case 2:
        nameRow += " 2";
        oneRecordToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintAdditional2;
        break;
      case 3:
        nameRow += " 3";
        oneRecordToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintAdditional3;
        break;
      case 4:
        nameRow += " 4";
        oneRecordToPrint = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintAdditional4;
        break;
    }
    if (oneRecordToPrint != null && !string.IsNullOrEmpty(oneRecordToPrint._tableRowId))
      nameRow = oneRecordToPrint._tableRowId.ToString();
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, nameRow);
    if (tableData != null)
      return tableData.CloneFromTemplate() as TableData;
    int num = (int) MessageBox.Show($"В шаблоне не найден тип строки\r\n\r\n\"{nameRow}\"", "Ошибка!");
    return (TableData) null;
  }

  /// <summary> Заполнение существующей записи </summary>
  /// <param name="recordForVed_New"></param>
  /// <param name="docTemplate"></param>
  /// <param name="document"></param>
  /// <param name="docRowInfo"></param>
  /// <returns></returns>
  public TableData FillingExisting_DocRow_Info(
    Vedomost_VB.RecordForVed_New recordForVed_New,
    ImDocument docTemplate,
    ImDocument document,
    TableData docRowInfo)
  {
    if (recordForVed_New == null)
      return (TableData) null;
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrint_Info == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint recordToPrintInfo = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrint_Info;
    docTemplate.FindNode(recordToPrintInfo._tableRowId.ToString());
    this.Filled_Record_DocRow_Ved(docRowInfo, recordForVed_New, recordToPrintInfo, 0, 0, document, (PageData) null);
    this.Filled_Record_DocRow_Ved_Attributes(docRowInfo, recordForVed_New, "Stream");
    return docRowInfo;
  }

  /// <summary> Заполнение вторичной записи на основе RecordForVed_Vtor </summary>
  /// <param name="recordForVed_Vtor"></param>
  /// <param name="docTemplate"></param>
  /// <param name="stroka_KudaVhodit"></param>
  /// <returns></returns>
  public bool Filled_DocRowVtor_Info(
    Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor,
    ImDocument docTemplate,
    TableData stroka_KudaVhodit)
  {
    if (recordForVed_Vtor == null || docTemplate == null || stroka_KudaVhodit == null)
      return false;
    Vedomost_VB.OneRecordToPrint recordToPrintVtor = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrint_Info._oneRecordToPrint_Vtor;
    foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(stroka_KudaVhodit))
    {
      if (textData != null)
      {
        string str1 = textData.ToString();
        bool flag = false;
        Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
        for (int index = 0; index < recordToPrintVtor._listOneGrafaToPrint.Count; ++index)
        {
          oneGrafaToPrint = recordToPrintVtor._listOneGrafaToPrint[index];
          string str2 = oneGrafaToPrint._cell_ID.ToString();
          if (str1.IndexOf(str2) > -1)
          {
            flag = true;
            break;
          }
        }
        if (flag && oneGrafaToPrint != null)
        {
          string str3 = "";
          string str4 = "";
          for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
          {
            Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index];
            if (dataFieldToPrint._typeField != Vedomost_VB.TypeField.ObjectType)
              str4 = recordForVed_Vtor.Get_Data_String_for_TypeFieldVedRec(dataFieldToPrint._typeFieldVedRec);
            if (str4 != "")
              str3 = !(str3 != "") ? str4 : str3 + dataFieldToPrint._symbolRazd + str4;
          }
          if (str3 != "")
            textData.AssignText(str3, false, false, false);
        }
      }
    }
    this.Filled_Record_DocRow_VedVtor_Attributes(stroka_KudaVhodit, recordForVed_Vtor);
    return true;
  }

  /// <summary> Создание ПУСТОЙ записи </summary>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public TableData Create_DocRow_Empty(
    ImDocument docTemplate,
    string variable,
    string name_PageTemplate)
  {
    string nameRow = "Пустая строка";
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint recordToPrintEmpty = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintEmpty;
    if (recordToPrintEmpty != null && !string.IsNullOrEmpty(recordToPrintEmpty._tableRowId))
      nameRow = recordToPrintEmpty._tableRowId.ToString();
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, nameRow);
    if (tableData == null)
      return (TableData) null;
    TableData docRowEmpty = tableData.CloneFromTemplate() as TableData;
    docRowEmpty.SetAttributeValue("TypeRow", "Empty");
    if (!string.IsNullOrEmpty(variable))
      docRowEmpty.SetAttributeValue("Variable", variable);
    return docRowEmpty;
  }

  /// <summary> Создание записи ПРИМЕЧАНИЕ</summary>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public TableData Create_DocRow_Remark(
    ImDocument docTemplate,
    string variable,
    string name_PageTemplate)
  {
    string nameRow = "Длинная строка";
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint recordToPrintRemark = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintRemark;
    if (recordToPrintRemark != null && !string.IsNullOrEmpty(recordToPrintRemark._tableRowId))
      nameRow = recordToPrintRemark._tableRowId.ToString();
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, nameRow);
    if (tableData == null)
      return (TableData) null;
    TableData docRowRemark = tableData.CloneFromTemplate() as TableData;
    docRowRemark.SetAttributeValue("TypeRow", "Remark");
    if (!string.IsNullOrEmpty(variable))
      docRowRemark.SetAttributeValue("Variable", variable);
    return docRowRemark;
  }

  /// <summary> Создание записи Примечание короткое</summary>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public TableData Create_DocRow_RemarkShort(
    ImDocument docTemplate,
    string variable,
    string name_PageTemplate)
  {
    string nameRow = "Примечание короткое";
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint recordToPrintEmpty = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintEmpty;
    if (recordToPrintEmpty != null && !string.IsNullOrEmpty(recordToPrintEmpty._tableRowId))
      nameRow = recordToPrintEmpty._tableRowId.ToString();
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, nameRow);
    if (tableData == null)
      return (TableData) null;
    if (tableData == null)
    {
      int num = (int) MessageBox.Show("В шаблоне нет строки с именем \"Примечание короткое\"", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (TableData) null;
    }
    TableData docRowRemarkShort = tableData.CloneFromTemplate() as TableData;
    docRowRemarkShort.SetAttributeValue("TypeRow", "RemarkShort");
    if (!string.IsNullOrEmpty(variable))
      docRowRemarkShort.SetAttributeValue("Variable", variable);
    return docRowRemarkShort;
  }

  public TableData Create_DocRow_TitlePart(
    ImDocument docTemplate,
    string variable,
    string name_PageTemplate)
  {
    Vedomost_VB.OneRecordToPrint toPrintTitlePart = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintTitlePart;
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, toPrintTitlePart._tableRowId);
    if (tableData == null)
      return (TableData) null;
    TableData docRowTitlePart = tableData.CloneFromTemplate() as TableData;
    docRowTitlePart.SetAttributeValue("TypeRow", "RemarkShort");
    if (!string.IsNullOrEmpty(variable))
      docRowTitlePart.SetAttributeValue("Variable", variable);
    return docRowTitlePart;
  }

  /// <summary> Создание записи в ЛРИ</summary>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public TableData Create_LriRow(ImDocument docTemplate)
  {
    return (TableData) (docTemplate.FindNode("Таблица изменений") as TableData).Nodes[0].CloneFromTemplate(true, true);
  }

  /// <summary> Создание записи для Заголовка </summary>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public TableData Create_DocRow_Zagolovok(
    ImDocument docTemplate,
    Vedomost_VB.RecordForVed_New recordForVed,
    string variable,
    string name_PageTemplate)
  {
    if (docTemplate == null || recordForVed == null)
      return (TableData) null;
    TableData table = (TableData) null;
    string nameRow = "Заголовок";
    if (this._one_Ved_Nastr_RazrabatyvaemoiVed == null || this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint recordToPrintTitle = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintTitle;
    if (recordToPrintTitle != null && !string.IsNullOrEmpty(recordToPrintTitle._tableRowId))
      nameRow = recordToPrintTitle._tableRowId.ToString();
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_PageTemplate, nameRow);
    if (tableData == null)
      return (TableData) null;
    if (tableData != null)
      table = tableData.CloneFromTemplate() as TableData;
    string name = recordForVed.Get_Name();
    if (name != "" && recordToPrintTitle != null && table != null)
    {
      foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(table))
      {
        if (textData != null)
        {
          string str1 = textData.ToString();
          bool flag = false;
          Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
          for (int index = 0; index < recordToPrintTitle._listOneGrafaToPrint.Count; ++index)
          {
            oneGrafaToPrint = recordToPrintTitle._listOneGrafaToPrint[index];
            string str2 = oneGrafaToPrint._cell_ID.ToString();
            if (str1.IndexOf(str2) > -1)
            {
              flag = true;
              break;
            }
          }
          if (flag && oneGrafaToPrint != null)
          {
            string str3 = "";
            for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
            {
              Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index];
              string str4 = dataFieldToPrint._typeField != Vedomost_VB.TypeField.ObjectType ? recordForVed.Get_Data_String_for_TypeFieldVedRec(dataFieldToPrint._typeFieldVedRec) : name;
              if (str4 != "")
                str3 = !(str3 != "") ? str4 : str3 + dataFieldToPrint._symbolRazd + str4;
            }
            if (str3 != "")
              textData.AssignText(str3, false, false, false);
          }
        }
      }
    }
    if (table != null)
    {
      table.SetAttributeValue("TypeRow", "Title");
      if (!string.IsNullOrEmpty(variable))
        table.SetAttributeValue("Variable", variable);
    }
    return table;
  }

  /// <summary> Создание записи для Заголовка </summary>
  /// <param name="docTemplate"></param>
  /// <returns></returns>
  public TableData Create_DocRow_PodZagolovok(
    ImDocument docTemplate,
    Vedomost_VB.RecordForVed_New recordForVed,
    string variable,
    string name_Page_Current)
  {
    if (docTemplate == null || recordForVed == null)
      return (TableData) null;
    Vedomost_VB.OneRecordToPrint printTitlePodSection = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrintTitlePodSection;
    if (printTitlePodSection == null)
      return (TableData) null;
    TableData tableData = this.Select_Stroka_IzShablona(docTemplate, name_Page_Current, printTitlePodSection._tableRowId);
    if (tableData == null)
      return (TableData) null;
    TableData table = tableData.CloneFromTemplate() as TableData;
    string name = recordForVed.Get_Name();
    if (name != "" && printTitlePodSection != null && table != null)
    {
      foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(table))
      {
        if (textData != null)
        {
          string str1 = textData.ToString();
          bool flag = false;
          Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
          for (int index = 0; index < printTitlePodSection._listOneGrafaToPrint.Count; ++index)
          {
            oneGrafaToPrint = printTitlePodSection._listOneGrafaToPrint[index];
            string str2 = oneGrafaToPrint._cell_ID.ToString();
            if (str1.IndexOf(str2) > -1)
            {
              flag = true;
              break;
            }
          }
          if (flag && oneGrafaToPrint != null)
          {
            string str3 = "";
            for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
            {
              Vedomost_VB.OneDataFieldToPrint dataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index];
              string str4 = dataFieldToPrint._typeField != Vedomost_VB.TypeField.ObjectType ? recordForVed.Get_Data_String_for_TypeFieldVedRec(dataFieldToPrint._typeFieldVedRec) : name;
              if (str4 != "")
                str3 = !(str3 != "") ? str4 : str3 + dataFieldToPrint._symbolRazd + str4;
            }
            if (str3 != "")
              textData.AssignText(str3, false, false, false);
          }
        }
      }
    }
    if (table != null)
    {
      table.SetAttributeValue("TypeRow", "Title2");
      if (!string.IsNullOrEmpty(variable))
        table.SetAttributeValue("Variable", variable);
    }
    return table;
  }

  public TableData Create_DocRow_KudaVhodit(ImDocument docTemplate)
  {
    Vedomost_VB.OneRecordToPrint recordToPrintVtor = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrint_Info._oneRecordToPrint_Vtor;
    return (docTemplate.FindNode(recordToPrintVtor._tableRowId.ToString()) as TableData).CloneFromTemplate() as TableData;
  }

  public TableData Create_DocRow_Itogo(ImDocument docTemplate)
  {
    Vedomost_VB.OneRecordToPrint recordToPrintItogo = this._one_Ved_Nastr_RazrabatyvaemoiVed._algorithmToPrint._oneRecordToPrint_Info._oneRecordToPrint_Itogo;
    return (docTemplate.FindNode(recordToPrintItogo._tableRowId.ToString()) as TableData).CloneFromTemplate() as TableData;
  }

  /// <summary> Удаление объекта из окон и документов </summary>
  /// <param name="oldObjectID">  </param>
  /// <param name="sender"> Объект, рассылающий событие обновления </param>
  public void Delete_ObjectFromBase_By_oldObjectID(long oldObjectID, object sender)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(Math.Abs(oldObjectID), false);
      if (dbObject == null)
        return;
      dbObject.Delete(0L);
      ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent(sender, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", oldObjectID));
    }
  }

  /// <summary> Заполнение списков получаемых полей головное </summary>
  public void ListCommonId_Filled(ListCommonId listCommonId)
  {
    if (listCommonId == null)
      return;
    if (listCommonId._listCommonId == null)
      listCommonId._listCommonId = new List<Vedomost_VB.OneFieldSpForRead>();
    else
      listCommonId._listCommonId.Clear();
    if (listCommonId._one_Ved_Nastr_RazrabatyvaemoiVed == null)
      listCommonId._one_Ved_Nastr_RazrabatyvaemoiVed = this._one_Ved_Nastr_RazrabatyvaemoiVed;
    Vedomost_VB_Static.ListObligatoryId_Filled();
    for (int index = 0; index < Vedomost_VB_Static._listObligatoryId.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = Vedomost_VB_Static._listObligatoryId[index];
      listCommonId._listCommonId.Add(oneFieldSpForRead);
    }
    for (int index1 = 0; index1 < listCommonId._one_Ved_Nastr_RazrabatyvaemoiVed._list_Ved_ID.Count; ++index1)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = listCommonId._one_Ved_Nastr_RazrabatyvaemoiVed._list_Ved_ID[index1];
      bool flag = false;
      for (int index2 = 0; index2 < listCommonId._listCommonId.Count; ++index2)
      {
        if (listCommonId._listCommonId[index2]._id == oneFieldSpForRead._id)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        listCommonId._listCommonId.Add(oneFieldSpForRead);
    }
    for (int index = 0; index < listCommonId._listCommonId.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = listCommonId._listCommonId[index];
      string name = oneFieldSpForRead._name;
      int id = oneFieldSpForRead._id;
    }
  }

  /// <summary> Первая сортиовка listRecordsForMainVed. Сортировка ОСНОВЫ (дерева) всех ведомостей </summary>
  /// 
  ///             Основная СП на первом месте
  ///             По Обозначению, Куда входит, Исполнение, Количество, Примечание
  ///             <typeparam name="T"></typeparam>
  public class CompareRecordsForMainVed_byDesignation<T> : IComparer<T> where T : Vedomost_VB.RecordForMainVed
  {
    public int Compare(T recordForMainVed1, T recordForMainVed2)
    {
      if ((object) recordForMainVed1 == null || (object) recordForMainVed2 == null)
        return 0;
      if (recordForMainVed1.Uroven == "1" && recordForMainVed2.Uroven != "1")
        return -1000;
      if (recordForMainVed2.Uroven == "1" && recordForMainVed1.Uroven != "1" || recordForMainVed1.IsTherezKomplekt && !recordForMainVed2.IsTherezKomplekt)
        return 1000;
      if (recordForMainVed2.IsTherezKomplekt && !recordForMainVed1.IsTherezKomplekt)
        return -1000;
      if (recordForMainVed1.IsTherezDopZam && !recordForMainVed2.IsTherezDopZam)
        return 1000;
      if (recordForMainVed2.IsTherezDopZam && !recordForMainVed1.IsTherezDopZam)
        return -1000;
      int num1 = string.Compare(recordForMainVed1.Designation, recordForMainVed2.Designation, StringComparison.Ordinal);
      if (num1 != 0)
        return num1;
      int num2 = string.Compare(recordForMainVed1.KudaDesignation, recordForMainVed2.KudaDesignation, StringComparison.Ordinal);
      if (num2 != 0)
        return num2;
      int num3 = string.Compare(recordForMainVed1.Ispolnenie, recordForMainVed2.Ispolnenie, StringComparison.Ordinal);
      if (num3 != 0)
        return num3;
      int num4 = string.Compare(recordForMainVed1.CountS, recordForMainVed2.CountS, StringComparison.Ordinal);
      if (num4 != 0)
        return num4;
      int num5 = string.Compare(recordForMainVed1.Remark, recordForMainVed2.Remark, StringComparison.Ordinal);
      return num5 != 0 ? num5 : 0;
    }
  }

  /// <summary> Сортировка полученного с учетом исполнений </summary>
  /// 
  ///             По ОБОЗНАЧЕНИЮ, по ИСПОЛНЕНИЮ, Комплекты в конец, через Комплекты, Куда водит, Количество, Примечание
  ///             <typeparam name="T"></typeparam>
  public class CompareRecordsForMainVed_byDesignation4<T> : IComparer<T> where T : Vedomost_VB.RecordForMainVed
  {
    public int Compare(T recordForMainVed1, T recordForMainVed2)
    {
      if ((object) recordForMainVed1 == null || (object) recordForMainVed2 == null)
        return 0;
      if (recordForMainVed1.Uroven == "1" && recordForMainVed2.Uroven != "1")
        return -1000;
      if (recordForMainVed2.Uroven == "1" && recordForMainVed1.Uroven != "1")
        return 1000;
      int num1 = string.Compare(recordForMainVed1.Designation, recordForMainVed2.Designation, StringComparison.Ordinal);
      if (num1 != 0)
        return num1;
      int num2 = string.Compare(recordForMainVed1.Ispolnenie, recordForMainVed2.Ispolnenie, StringComparison.Ordinal);
      if (num2 != 0)
        return num2;
      if (recordForMainVed1.EtaSp_Komplekt && !recordForMainVed2.EtaSp_Komplekt)
        return 1000;
      if (recordForMainVed2.EtaSp_Komplekt && !recordForMainVed1.EtaSp_Komplekt)
        return -1000;
      if (recordForMainVed1.EtaSp_DopZam && !recordForMainVed2.EtaSp_DopZam)
        return 1000;
      if (recordForMainVed2.EtaSp_DopZam && !recordForMainVed1.EtaSp_DopZam)
        return -1000;
      if (recordForMainVed1.IsTherezKomplekt != recordForMainVed2.IsTherezKomplekt)
      {
        if (recordForMainVed1.IsTherezKomplekt)
          return 1;
        if (recordForMainVed2.IsTherezKomplekt)
          return -1;
      }
      if (recordForMainVed1.IsTherezDopZam != recordForMainVed2.IsTherezDopZam)
      {
        if (recordForMainVed1.IsTherezDopZam)
          return 1;
        if (recordForMainVed2.IsTherezDopZam)
          return -1;
      }
      int num3 = string.Compare(recordForMainVed1.KudaDesignation, recordForMainVed2.KudaDesignation, StringComparison.Ordinal);
      if (num3 != 0)
        return num3;
      int num4 = string.Compare(recordForMainVed1.CountS, recordForMainVed2.CountS, StringComparison.Ordinal);
      if (num4 != 0)
        return num4;
      int num5 = string.Compare(recordForMainVed1.Remark, recordForMainVed2.Remark, StringComparison.Ordinal);
      return num5 != 0 ? num5 : 0;
    }
  }

  /// <summary> Сортировка для выделения общей части в групповой ведомости </summary>
  /// <typeparam name="T"></typeparam>
  public class CompareRecordsVed_step0<T> : IComparer<Vedomost_VB.RecordForVed_New>
  {
    public int Compare(
      Vedomost_VB.RecordForVed_New recordForVed1,
      Vedomost_VB.RecordForVed_New recordForVed2)
    {
      if (recordForVed1 == null || recordForVed2 == null)
        return 0;
      if (recordForVed2.TypeRec == Vedomost_VB.TypeRec.Main)
        return 1;
      if (recordForVed1.TypeRec == Vedomost_VB.TypeRec.Main)
        return -1;
      int num1 = string.Compare(recordForVed1.Get_Designation(), recordForVed2.Get_Designation(), StringComparison.Ordinal);
      if (num1 != 0)
        return num1;
      int num2 = string.Compare(recordForVed1.Get_Name(), recordForVed2.Get_Name(), StringComparison.Ordinal);
      if (num2 != 0)
        return num2;
      int num3 = string.Compare(recordForVed1.KudaDesignation, recordForVed2.KudaDesignation, StringComparison.Ordinal);
      if (num3 != 0)
        return num3;
      if (recordForVed1.IsTherezKomplekt && !recordForVed2.IsTherezKomplekt)
        return 1000;
      if (!recordForVed1.IsTherezKomplekt && recordForVed2.IsTherezKomplekt)
        return -1000;
      int num4 = string.Compare(recordForVed1.Ispolnenie, recordForVed2.Ispolnenie, StringComparison.Ordinal);
      if (num4 != 0)
        return num4;
      if (recordForVed1.TypeRec != recordForVed2.TypeRec)
      {
        if (recordForVed2.TypeRec == Vedomost_VB.TypeRec.Included)
          return -4;
        if (recordForVed1.TypeRec == Vedomost_VB.TypeRec.Included)
          return 4;
      }
      return 0;
    }
  }

  public class CompareRecordsVed_stepDopZam<T> : IComparer<Vedomost_VB.RecordForVed_New>
  {
    public int Compare(
      Vedomost_VB.RecordForVed_New recordForVed1,
      Vedomost_VB.RecordForVed_New recordForVed2)
    {
      if (recordForVed1 == null || recordForVed2 == null)
        return 0;
      if (recordForVed2.TypeRec == Vedomost_VB.TypeRec.Main)
        return 1;
      if (recordForVed1.TypeRec == Vedomost_VB.TypeRec.Main)
        return -1;
      if (recordForVed1.IsTherezDopZam && !recordForVed2.IsTherezDopZam)
        return 1000;
      return !recordForVed1.IsTherezDopZam && recordForVed2.IsTherezDopZam ? -1000 : 0;
    }
  }

  public class Compare_objType_Ved1 : IComparer<IMSObjectType>
  {
    public int Compare(IMSObjectType objType1, IMSObjectType objType2)
    {
      if (objType1 == null || objType2 == null || objType1.ShortName == objType2.ShortName)
        return 0;
      if (objType1.ShortName == "ВС" && objType2.ShortName != "ВС")
        return -1;
      if (objType1.ShortName == "ВП" && objType2.ShortName != "ВП")
        return -2;
      if (objType1.ShortName == "РС" && objType2.ShortName != "РС")
        return -3;
      return objType1.ShortName == "РСП" && objType2.ShortName != "РСП" ? -4 : 0;
    }
  }

  public class Compare_objType_Ved2 : IComparer<IMSObjectType>
  {
    public int Compare(IMSObjectType objType1, IMSObjectType objType2)
    {
      if (objType1 == null || objType2 == null || objType1.ShortName == objType2.ShortName)
        return 0;
      if (objType1.ShortName == "ВС" && objType2.ShortName != "ВС")
        return 1;
      if (objType1.ShortName == "ВП" && objType2.ShortName != "ВП")
        return 2;
      if (objType1.ShortName == "РС" && objType2.ShortName != "РС")
        return 3;
      return objType1.ShortName == "РСП" && objType2.ShortName != "РСП" ? 4 : 0;
    }
  }

  public class Compare_RecordForVed_Vtor : IComparer<Vedomost_VB.RecordForVed_Vtor>
  {
    public int Compare(
      Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor1,
      Vedomost_VB.RecordForVed_Vtor recordForVed_Vtor2)
    {
      return string.Compare(recordForVed_Vtor1.KudaDesignation, recordForVed_Vtor2.KudaDesignation, StringComparison.Ordinal);
    }
  }

  public enum TypeRec
  {
    Undefined,
    Info,
    Included,
    Title,
    Title2,
    TitleVar,
    TitleIsp,
    TitleIncluded,
    TitlePart,
    Remark,
    RemarkShort,
    Additional1,
    Additional2,
    Additional3,
    Additional4,
    Main,
    Oglavlenie,
    Empty,
    NewPage,
  }

  /// <summary> Типы полей в записи ведомости </summary>
  public enum TypeFieldVedRec
  {
    Undefined,
    Razdel_Ved,
    PodRazdel_Ved,
    KudaDesignation,
    Ispolnenie,
    DocumentTypeName,
    Uroven,
    Count_in_Sp_S,
    Count_in_Sp,
    Count_in_Izdelie,
    Count_in_SpKompl,
    Count_in_SpRegulir,
    CountF_samOi_sp,
    Count_Vsego,
    Count_Summ,
    EdIzmKol,
    Remark,
    DerzPodl,
  }

  /// <summary> Типы полей в записи ведомости </summary>
  public enum TypeFieldVedPasport
  {
    Undefined,
    DesignationIzd,
    DesignationDoc,
    KodDoc,
    NameArticle,
    NameTypeDoc,
    Remark,
    GeneratedNumber,
  }

  public enum TypeField
  {
    Undefined,
    ObjectType,
    TypeFieldVedRec,
    TypeFieldVedPasport,
  }

  public enum TypeDataToXml
  {
    Field,
    Attribute,
  }

  public enum TypeDataSel
  {
    Undefined,
    Int,
    Long,
    Float,
    String,
    Guid,
    Measured,
    Boolean,
    DateTime,
  }

  public enum UslovieSravnenia
  {
    Ravno,
    NeRavno,
    Soderzit,
    NeSoderzit,
    Nathinaetsia,
  }

  public enum TypeCompare
  {
    Int,
    Symbol,
  }

  public enum TypeVed
  {
    Undefined,
    VS,
    VP,
    RS,
    VSI,
    VD,
    VDE,
    DP,
    DPE,
    VM,
    VR,
    VDZ,
    TABL,
    TABLSOED,
    TABLSOEDSZ,
    Others,
    ED,
    ZI,
    GENERAL,
    ESPD,
  }

  public enum TypeCreate
  {
    Undefined,
    System,
    User,
  }

  public enum FormaGroup
  {
    Ed,
    A,
    B,
  }

  public enum TypeDoc
  {
    Undefined,
    Ved,
    Tabl,
    Sp,
    Pe,
    Espd,
    EspdLU,
  }

  /// <summary> Список соответствия Исполнение - Заголовок графы </summary>
  public class Variables_Coordination
  {
    public List<string> list_Variables = new List<string>();
    public List<string> list_Captions = new List<string>();
    public string errorText = "";

    /// <summary> Проверка на синхронность размера списков исполнений и заголовков </summary>
    /// <returns></returns>
    public bool IsCorrect()
    {
      if (this.list_Captions.Count == this.list_Variables.Count)
      {
        this.errorText = "";
        return true;
      }
      this.errorText = "list_Captions.Count != list_Variables.Count";
      return false;
    }

    /// <summary> Проверка VARIABLE </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    public bool Variable_check(string variable)
    {
      if (string.IsNullOrEmpty(variable))
      {
        this.errorText = "Исполнение пустое";
        return false;
      }
      for (int index = 0; index < this.list_Variables.Count; ++index)
      {
        if (variable == this.list_Variables[index])
        {
          this.errorText = $"Исполнение\r\n{variable}\r\nуже существует";
          return false;
        }
      }
      return true;
    }

    /// <summary> Проверка CAPTION  </summary>
    /// <param name="caption"></param>
    /// <returns></returns>
    public bool Caption_check(string caption)
    {
      if (string.IsNullOrEmpty(caption))
      {
        this.errorText = "Заголовок исполнения пустой";
        return false;
      }
      for (int index = 0; index < this.list_Captions.Count; ++index)
      {
        if (caption == this.list_Captions[index])
        {
          this.errorText = $"Заголовок исполнения\r\n{caption}\r\nуже существует";
          return false;
        }
      }
      return true;
    }

    /// <summary> Добавление исполнения с одновременным  генерированием Caption </summary>
    /// <param name="variable"></param>
    /// <param name="designatinArt"></param>
    /// <returns></returns>
    public bool Add_Variable_AutoCaption(string variable, string designatinArt)
    {
      if (!this.IsCorrect() || !this.Variable_check(variable))
        return false;
      string str = Vedomost_VB_Static.Generated_Captions(variable, this.list_Variables.Count + 1, designatinArt, this.list_Variables.Count + 1);
      if (string.IsNullOrEmpty(str))
        return false;
      this.list_Variables.Add(variable);
      this.list_Captions.Add(str);
      return true;
    }

    /// <summary> Добавление исполнения и заголовка </summary>
    /// <param name="variable"></param>
    /// <param name="caption"></param>
    /// <returns></returns>
    public bool Add_Variable_WithCaption(string variable, string caption)
    {
      if (!this.IsCorrect())
      {
        int num = (int) MessageBox.Show(this.errorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      if (!this.Variable_check(variable))
      {
        int num = (int) MessageBox.Show(this.errorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      if (!this.Caption_check(caption))
      {
        int num = (int) MessageBox.Show(this.errorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this.list_Variables.Add(variable);
      this.list_Captions.Add(caption);
      return true;
    }

    /// <summary> Порядковый номер исполнения по ИМЕНИ ГРАФЫ </summary>
    /// <param name="caption"></param>
    /// <returns></returns>
    public int I_Variable_By_Caption(string caption)
    {
      if (!this.IsCorrect() || string.IsNullOrEmpty(caption))
        return -1;
      int num = -1;
      for (int index = 0; index < this.list_Captions.Count; ++index)
      {
        if (caption == this.list_Captions[index])
        {
          num = index;
          break;
        }
      }
      return num;
    }

    /// <summary> Порядковый номер исполнения по обозначению исполнения </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    public int I_Variable_By_Variable(string variable)
    {
      if (!this.IsCorrect() || string.IsNullOrEmpty(variable))
        return -1;
      int num = -1;
      for (int index = 0; index < this.list_Variables.Count; ++index)
      {
        if (variable == this.list_Variables[index])
        {
          num = index;
          break;
        }
      }
      return num;
    }

    /// <summary> Обозначение исполнения по ИМЕНИ ГРАФЫ </summary>
    /// <param name="caption"></param>
    /// <returns></returns>
    public string Variable_By_Caption(string caption)
    {
      if (!this.IsCorrect() || string.IsNullOrEmpty(caption))
        return "";
      int index = this.I_Variable_By_Caption(caption);
      return index < 0 ? "" : this.list_Variables[index];
    }

    /// <summary> ИМЯ ГРАФЫ по обозначению исполнения </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    public string Caption_By_Variable(string variable)
    {
      if (!this.IsCorrect() || string.IsNullOrEmpty(variable))
        return "";
      int index = this.I_Variable_By_Variable(variable);
      return index < 0 ? "" : this.list_Captions[index];
    }
  }

  public class One_Variable_for_VariableDialog
  {
    public string _variable = "";
    public string _caption = "";
    public bool _isNew;
    public bool _isDeleted;
    public bool _isRename;
    public bool _isTemplate;
    public string _variable_old = "";
    public string _caption_old = "";
    public Vedomost_VB.One_Variable_for_VariableDialog one_Variable_For_VariableDialog_Template;
    public int i_variable_Template = -1;
  }

  public class Variables_for_VariableDialog
  {
    private Vedomost_VB.One_Variable_for_VariableDialog one_Variable_For_VariableDialog;
    public string _errorText = "";
    public List<Vedomost_VB.One_Variable_for_VariableDialog> list_Variables = new List<Vedomost_VB.One_Variable_for_VariableDialog>();

    /// <summary> Проверка VARIABLE </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    public bool Variable_check(string variable, string variable_Curr)
    {
      if (string.IsNullOrEmpty(variable))
      {
        this._errorText = "Исполнение пустое";
        return false;
      }
      for (int index = 0; index < this.list_Variables.Count; ++index)
      {
        this.one_Variable_For_VariableDialog = this.list_Variables[index];
        if (variable == this.one_Variable_For_VariableDialog._variable && variable != variable_Curr)
        {
          this._errorText = $"Исполнение\r\n{variable}\r\nуже существует";
          return false;
        }
      }
      return true;
    }

    /// <summary> Проверка CAPTION </summary>
    /// <param name="caption"></param>
    /// <returns></returns>
    public bool Caption_check(string caption, string caption_Curr)
    {
      if (string.IsNullOrEmpty(caption))
      {
        this._errorText = "Заголовок пустой";
        return false;
      }
      for (int index = 0; index < this.list_Variables.Count; ++index)
      {
        this.one_Variable_For_VariableDialog = this.list_Variables[index];
        if (caption == this.one_Variable_For_VariableDialog._caption && caption != caption_Curr)
        {
          this._errorText = $"Заголовок\r\n{caption}\r\nуже существует";
          return false;
        }
      }
      return true;
    }
  }

  /// <summary> Сведения об одной найденной спецификации </summary>
  public class OneSpecification
  {
    private long _objectId;
    private int _objectType;
    private string _designation;
    private string _name;
    private DataTable _dataTableSp;

    public long ObjectId
    {
      get => this._objectId;
      set => this._objectId = value;
    }

    public int ObjectType
    {
      get => this._objectType;
      set => this._objectType = value;
    }

    public string Designation
    {
      get => this._designation;
      set => this._designation = value;
    }

    public string Name
    {
      get => this._name;
      set => this._name = value;
    }

    public DataTable PartsDoc
    {
      get => this._dataTableSp;
      set => this._dataTableSp = value;
    }

    /// <summary> Конструктор Сведения об одной найденной спецификации </summary>
    /// <param name="objectId"></param>
    /// <param name="designation"></param>
    /// <param name="dataTableSp"></param>
    public OneSpecification(
      long objectId,
      int objectType,
      string designation,
      DataTable dataTableSp)
    {
      this._objectId = objectId;
      this._objectType = objectType;
      this._designation = designation;
      this._dataTableSp = dataTableSp;
    }
  }

  /// <summary> Запись при сборе ОСНОВЫ ведомостей </summary>
  public class RecordForMainVed
  {
    private Vedomost_VB _vedomost_VB;
    public long ObjectIdIzd;
    public long ObjectIdDoc;
    public int ObjectType;
    public string Designation;
    public string Name;
    public string CountS;
    public float CountF;
    public float CountSummF;
    public string EdIzmKol;
    public string Remark;
    public string DerzPodl;
    public string KudaDesignation;
    public string Ispolnenie;
    public string Uroven;
    public int UrovenN;
    public bool NeRaskryvat;
    public bool EstSvoiaVedomost;
    public bool IsTherezKomplekt;
    public bool EtaSp_Komplekt;
    public bool IsTherezDopZam;
    public bool EtaSp_DopZam;
    public Guid _articleGroupID;
    public long _F_PRJLINK_ID;
    private DataTable _partsDoc;
    public string Format;
    public string Zone;
    public string Position;
    public long KudaObjectId;
    public List<Vedomost_VB.RecordForMainVedVtor> List_recordForMainVedVtor;
    public DataRow RecordSpIncudeVed;
    public Vedomost_VB.RecordForMainVed recordForMainVedPrevision;

    public DataTable PartsDoc
    {
      get => this._partsDoc;
      set => this._partsDoc = value;
    }

    /// <summary> Сведения об одной найденной спецификации </summary>
    /// <param name="recordSp"></param>
    /// <param name="kudaDesignation"></param>
    /// <param name="kudaObjectId"></param>
    /// <param name="isTherezKomplekt"></param>
    /// <param name="isTherezDopZam"></param>
    /// <param name="uroven"></param>
    /// <param name="vedomost_VB"></param>
    public RecordForMainVed(
      DataRow recordSp,
      string kudaDesignation,
      long kudaObjectId,
      bool isTherezKomplekt,
      bool isTherezDopZam,
      string uroven,
      Vedomost_VB vedomost_VB)
    {
      this._vedomost_VB = vedomost_VB;
      this.ObjectIdIzd = -1L;
      this.ObjectType = -1;
      this._articleGroupID = Guid.Empty;
      this._F_PRJLINK_ID = -1L;
      this.Designation = "";
      this.Name = "";
      this.Format = "";
      this.CountS = "";
      this.Position = "";
      this.Zone = "";
      this.Remark = "";
      this.KudaDesignation = kudaDesignation;
      this.KudaObjectId = kudaObjectId;
      this.IsTherezKomplekt = isTherezKomplekt;
      this.IsTherezDopZam = isTherezDopZam;
      this.Uroven = uroven;
      int num;
      if (recordSp != null)
      {
        if (recordSp[0] != DBNull.Value)
          this.ObjectIdIzd = Convert.ToInt64(recordSp[0]);
        if (recordSp[1] != DBNull.Value)
          this.ObjectType = Convert.ToInt32(recordSp[1]);
        if (recordSp[AvsIDCache.Attr_ArticleGroupID.ToString()] != DBNull.Value)
        {
          DataRow dataRow = recordSp;
          num = AvsIDCache.Attr_ArticleGroupID;
          string columnName = num.ToString();
          this._articleGroupID = new Guid(dataRow[columnName].ToString());
        }
        if (recordSp[3] != DBNull.Value)
          this._F_PRJLINK_ID = Convert.ToInt64(recordSp[3]);
        DataRow dataRow1 = recordSp;
        num = AvsIDCache.Attr_Designation;
        string columnName1 = num.ToString();
        if (dataRow1[columnName1] != DBNull.Value)
        {
          DataRow dataRow2 = recordSp;
          num = AvsIDCache.Attr_Designation;
          string columnName2 = num.ToString();
          this.Designation = Convert.ToString(dataRow2[columnName2]);
        }
        DataRow dataRow3 = recordSp;
        num = AvsIDCache.Attr_Name;
        string columnName3 = num.ToString();
        if (dataRow3[columnName3] != DBNull.Value)
        {
          DataRow dataRow4 = recordSp;
          num = AvsIDCache.Attr_Name;
          string columnName4 = num.ToString();
          this.Name = Convert.ToString(dataRow4[columnName4]);
        }
        DataRow dataRow5 = recordSp;
        num = AvsIDCache.Attr_Format;
        string columnName5 = num.ToString();
        if (dataRow5[columnName5] != DBNull.Value)
        {
          DataRow dataRow6 = recordSp;
          num = AvsIDCache.Attr_Format;
          string columnName6 = num.ToString();
          this.Format = Convert.ToString(dataRow6[columnName6]);
        }
        DataRow dataRow7 = recordSp;
        num = AvsIDCache.Attr_Count;
        string columnName7 = num.ToString();
        if (dataRow7[columnName7] != DBNull.Value)
        {
          DataRow dataRow8 = recordSp;
          num = AvsIDCache.Attr_Count;
          string columnName8 = num.ToString();
          this.CountS = Convert.ToString(dataRow8[columnName8]);
        }
        string sMessage;
        if (Vedomost_VB_Static.CheckKol(this.CountS, this.Designation, this.Name, this.ObjectIdIzd, out sMessage))
        {
          this.CountF = Vedomost_VB_Static.SeparationKol(this.CountS);
          if ((double) this.CountF > 0.0)
            this.EdIzmKol = Vedomost_VB_Static.SeparationEdIzm(this.CountS);
        }
        else
          Vedomost_VB_Static._listError_Strings.Add(sMessage);
        DataRow dataRow9 = recordSp;
        num = AvsIDCache.Attr_Zone;
        string columnName9 = num.ToString();
        if (dataRow9[columnName9] != DBNull.Value)
        {
          DataRow dataRow10 = recordSp;
          num = AvsIDCache.Attr_Zone;
          string columnName10 = num.ToString();
          this.Zone = Convert.ToString(dataRow10[columnName10]);
        }
        DataRow dataRow11 = recordSp;
        num = AvsIDCache.Attr_Position;
        string columnName11 = num.ToString();
        if (dataRow11[columnName11] != DBNull.Value)
        {
          DataRow dataRow12 = recordSp;
          num = AvsIDCache.Attr_Position;
          string columnName12 = num.ToString();
          this.Position = Convert.ToString(dataRow12[columnName12]);
        }
      }
      if (recordSp == null && uroven == "1")
      {
        this.ObjectIdIzd = kudaObjectId;
        this.ObjectType = vedomost_VB._imsObjectType_RazrabatyvaemoiVed.ObjectTypeID;
        this.Designation = kudaDesignation;
      }
      if (recordSp == null || !(this.Designation != kudaDesignation) || !(this.CountS == "") && (double) Math.Abs(this.CountF) > 0.0)
        return;
      string str = "";
      DataRow dataRow13 = recordSp;
      num = AvsIDCache.Attr_SpecificationSection;
      string columnName13 = num.ToString();
      if (dataRow13[columnName13] != DBNull.Value)
      {
        DataRow dataRow14 = recordSp;
        num = AvsIDCache.Attr_SpecificationSection;
        string columnName14 = num.ToString();
        str = Convert.ToString(dataRow14[columnName14]);
      }
      if (!(str != "Документация") || !(str != ""))
        return;
      OneError oneError = new OneError();
      oneError._objectIdSP_KudaVhodit = kudaObjectId;
      oneError._designationSp_KudaVhodit = kudaDesignation;
      oneError._objectId_Izdelie = this.ObjectIdIzd;
      oneError._designation_Izdelie = this.Designation;
      oneError._name_Izdelie = this.Name;
      oneError._f_PRJLINK_ID = this._F_PRJLINK_ID;
      oneError._message_kurc = "Не указано количество";
      oneError.Message();
      this._vedomost_VB._listError_OneError._list.Add(oneError);
    }

    public string Get_Data_String_for_objType(int objType)
    {
      if (this.PartsDoc == null)
        return "";
      string stringForObjType = "";
      DataRow row = this.PartsDoc.Rows[0];
      if (row[AvsIDCache.Attr_DerzPodl.ToString()] != DBNull.Value)
        stringForObjType = Convert.ToString(row[AvsIDCache.Attr_DerzPodl.ToString()]);
      return stringForObjType;
    }
  }

  /// <summary> Вторичная запись </summary>
  public class RecordForMainVedVtor
  {
    public string KudaDesignation;
    public string CountS1;
    public float CountF1;
    public string CountSn = "";
    public float CountFn;
  }

  /// <summary> Запись при сборе VEd New</summary>
  public class RecordForVed_New
  {
    private Vedomost_VB _vedomost_VB;
    private DataTable _partsDoc;
    public Vedomost_VB.TypeRec TypeRec;
    public long Razdel_Ved;
    public int PodRazdel_Ved;
    public string Count_in_Sp_S;
    public float Count_in_Sp;
    public float Count_in_Izdelie;
    public string Count_in_SpKompl_S;
    public float Count_in_SpKompl;
    public string Count_in_SpRegulir_S;
    public float Count_in_SpRegulir;
    public float CountF_samOi_sp;
    public float Count_Vsego;
    public float Count_Summ;
    public string EdIzmKol;
    public string Remark;
    public string DerzPodlDoc;
    public string KudaDesignation;
    public long KudaObjectId;
    public string Ispolnenie;
    public string DocumentTypeName;
    public string Others1;
    public int _iCikl;
    public string UrovenS;
    public int UrovenN;
    public bool IsTherezKomplekt;
    public bool EtaSp_Komplekt;
    public bool IsTherezDopZam;
    public bool EtaSp_DopZam;
    public bool FromNewPage;
    public Guid Guid_RecB = Guid.Empty;
    public List<Vedomost_VB.OneDataVed> List_OneDataVed = new List<Vedomost_VB.OneDataVed>();
    public List<Vedomost_VB.RecordForVed_Vtor> List_recordForVed_Vtor;
    public List<Vedomost_VB.RecordForVed_For_Isp> List_recordForVed_For_Isp;
    public List<Vedomost_VB.One_Grafa> List_For_Rebuilding_From_Graf;
    public List<Vedomost_VB.One_Attribute> List_For_Rebuilding_From_Attributes;
    public Vedomost_VB.RecordForVed_Vtor Record_For_Rebuilding_Itogo;
    public string NamePage = "";
    public string Text_tableName = "";

    public DataTable PartsDoc
    {
      get => this._partsDoc;
      set => this._partsDoc = value;
    }

    public RecordForVed_New()
    {
      if (this.Get_Designation() == null)
        this.Set_Designation("");
      if (this.Get_Name() == null)
        this.Set_Name("");
      if (this.Ispolnenie != null)
        return;
      this.Ispolnenie = "";
    }

    public RecordForVed_New(
      DataRow recordSp,
      Vedomost_VB.RecordForMainVed recordForMainVed,
      string urovenS,
      Vedomost_VB vedomost_VB)
    {
      this._vedomost_VB = vedomost_VB;
      this.Ispolnenie = "";
      if (recordForMainVed != null)
      {
        this.KudaDesignation = recordForMainVed.Designation;
        this.KudaObjectId = recordForMainVed.ObjectIdIzd;
        this.IsTherezKomplekt = recordForMainVed.IsTherezKomplekt;
        this.IsTherezDopZam = recordForMainVed.IsTherezDopZam;
      }
      else
      {
        this.KudaDesignation = "";
        this.KudaObjectId = 0L;
        this.IsTherezKomplekt = false;
        this.IsTherezDopZam = false;
      }
      this.UrovenS = urovenS;
      if (recordSp != null)
      {
        if (recordSp[AvsIDCache.Attr_Count.ToString()] != DBNull.Value)
        {
          this.Count_in_Sp_S = Convert.ToString(recordSp[AvsIDCache.Attr_Count.ToString()]);
          string sMessage;
          if (Vedomost_VB_Static.CheckKol(this.Count_in_Sp_S, this.Get_Designation(), this.Get_Name(), this.Get_ObjectID(), out sMessage))
          {
            this.Count_in_Sp = Vedomost_VB_Static.SeparationKol(this.Count_in_Sp_S);
            if ((double) this.Count_in_Sp > 0.0)
              this.EdIzmKol = Vedomost_VB_Static.SeparationEdIzm(this.Count_in_Sp_S);
          }
          else
            Vedomost_VB_Static._listError_Strings.Add(sMessage);
        }
        if (recordSp[AvsIDCache.Attr_CountForAdjustment.ToString()] != DBNull.Value)
        {
          this.Count_in_SpRegulir_S = Convert.ToString(recordSp[AvsIDCache.Attr_CountForAdjustment.ToString()]);
          this.Count_in_SpRegulir = Vedomost_VB_Static.SeparationKol(this.Count_in_SpRegulir_S);
        }
      }
      if (recordSp == null && urovenS == "1")
        this.Set_ObjectID(this.KudaObjectId);
      if (this.Get_Designation() == null)
        this.Set_Designation("");
      if (this.Get_Name() == null)
        this.Set_Name("");
      if (this.Ispolnenie != null)
        return;
      this.Ispolnenie = "";
    }

    /// <summary> Копия записи </summary>
    /// <returns></returns>
    public Vedomost_VB.RecordForVed_New RecordForVed_New_Copy(int k_from, int k_do, bool isItogo)
    {
      Vedomost_VB.RecordForVed_New recordForVedNew = new Vedomost_VB.RecordForVed_New();
      recordForVedNew.TypeRec = this.TypeRec;
      recordForVedNew.Razdel_Ved = this.Razdel_Ved;
      recordForVedNew.PodRazdel_Ved = this.PodRazdel_Ved;
      recordForVedNew.Count_in_Sp_S = this.Count_in_Sp_S;
      recordForVedNew.Count_in_Sp = this.Count_in_Sp;
      recordForVedNew.Count_in_Izdelie = this.Count_in_Izdelie;
      recordForVedNew.Count_in_SpKompl_S = this.Count_in_SpKompl_S;
      recordForVedNew.Count_in_SpKompl = this.Count_in_SpKompl;
      recordForVedNew.Count_in_SpRegulir_S = this.Count_in_SpRegulir_S;
      recordForVedNew.Count_in_SpRegulir = this.Count_in_SpRegulir;
      recordForVedNew.CountF_samOi_sp = this.CountF_samOi_sp;
      recordForVedNew.Count_Vsego = this.Count_Vsego;
      if (isItogo)
        recordForVedNew.Count_Summ = this.Count_Summ;
      recordForVedNew.EdIzmKol = this.EdIzmKol;
      recordForVedNew.Remark = this.Remark;
      recordForVedNew.DerzPodlDoc = this.DerzPodlDoc;
      recordForVedNew.KudaDesignation = this.KudaDesignation;
      recordForVedNew.KudaObjectId = this.KudaObjectId;
      recordForVedNew.Ispolnenie = this.Ispolnenie;
      recordForVedNew.DocumentTypeName = this.DocumentTypeName;
      recordForVedNew.Others1 = this.Others1;
      recordForVedNew.UrovenS = this.UrovenS;
      recordForVedNew.UrovenN = this.UrovenN;
      recordForVedNew.IsTherezKomplekt = this.IsTherezKomplekt;
      recordForVedNew.EtaSp_Komplekt = this.EtaSp_Komplekt;
      recordForVedNew.IsTherezDopZam = this.IsTherezDopZam;
      recordForVedNew.EtaSp_DopZam = this.EtaSp_DopZam;
      recordForVedNew.Guid_RecB = this.Guid_RecB;
      for (int index = 0; index < this.List_OneDataVed.Count; ++index)
      {
        Vedomost_VB.OneDataVed oneDataVed1 = this.List_OneDataVed[index];
        Vedomost_VB.OneDataVed oneDataVed2 = new Vedomost_VB.OneDataVed(oneDataVed1.TypeDataSel, oneDataVed1.AttributeSourceTypes, oneDataVed1.ObjectType, oneDataVed1.Data);
        recordForVedNew.List_OneDataVed.Add(oneDataVed2);
      }
      if (this.List_recordForVed_Vtor != null)
      {
        if (k_from < 0 || k_do < 0)
        {
          k_from = 0;
          k_do = this.List_recordForVed_Vtor.Count;
        }
        else
          ++k_do;
        recordForVedNew.List_recordForVed_Vtor = new List<Vedomost_VB.RecordForVed_Vtor>();
        for (int index = k_from; index < k_do; ++index)
        {
          Vedomost_VB.RecordForVed_Vtor recordForVedVtor = this.List_recordForVed_Vtor[index].Copy();
          recordForVedNew.List_recordForVed_Vtor.Add(recordForVedVtor);
        }
      }
      if (this.List_recordForVed_For_Isp != null)
      {
        recordForVedNew.List_recordForVed_For_Isp = new List<Vedomost_VB.RecordForVed_For_Isp>();
        for (int index = 0; index < this.List_recordForVed_For_Isp.Count; ++index)
        {
          Vedomost_VB.RecordForVed_For_Isp recordForVedForIsp = this.List_recordForVed_For_Isp[index].Copy();
          recordForVedNew.List_recordForVed_For_Isp.Add(recordForVedForIsp);
        }
      }
      return recordForVedNew;
    }

    public string Designation() => this.Get_Designation();

    public string Name() => this.Get_Name();

    public string Gost() => this.Get_Gost();

    /// <summary> Получение данных по objType </summary>
    /// <param name="objType"></param>
    /// <returns></returns>
    public Vedomost_VB.OneDataVed Get_Data(int objType)
    {
      for (int index = 0; index < this.List_OneDataVed.Count; ++index)
      {
        Vedomost_VB.OneDataVed data = this.List_OneDataVed[index];
        if (data.ObjectType == objType)
          return data;
      }
      return (Vedomost_VB.OneDataVed) null;
    }

    /// <summary> Дополнение строчных данных </summary>
    /// <param name="objType"></param>
    /// <param name="sText_razdelitel"></param>
    /// <param name="sText2"></param>
    public void Add_Data_String(int objType, string sText_razdelitel, string sText2)
    {
      switch (sText2)
      {
        case null:
          break;
        case "":
          break;
        default:
          if (objType == 0)
            break;
          Vedomost_VB.OneDataVed oneDataVed1 = (Vedomost_VB.OneDataVed) null;
          for (int index = 0; index < this.List_OneDataVed.Count; ++index)
          {
            oneDataVed1 = this.List_OneDataVed[index];
            if (oneDataVed1.ObjectType == objType)
            {
              if (oneDataVed1.TypeDataSel != Vedomost_VB.TypeDataSel.String)
                return;
              string str = oneDataVed1.Data.ToString();
              if (sText_razdelitel != null && sText_razdelitel != "")
                str += sText_razdelitel;
              string data = str + sText2;
              Vedomost_VB.OneDataVed oneDataVed2 = new Vedomost_VB.OneDataVed(oneDataVed1.TypeDataSel, oneDataVed1.AttributeSourceTypes, objType, (object) data);
              this.List_OneDataVed.RemoveAt(index);
              this.List_OneDataVed.Add(oneDataVed2);
              return;
            }
          }
          this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(oneDataVed1.TypeDataSel, AttributeSourceTypes.Object, objType, (object) sText2));
          break;
      }
    }

    /// <summary> Удаление дублирующихся данных </summary>
    public void Removal_of_duplicates()
    {
      for (int index1 = this.List_OneDataVed.Count - 1; index1 > 0; --index1)
      {
        Vedomost_VB.OneDataVed oneDataVed1 = this.List_OneDataVed[index1];
        for (int index2 = index1 - 1; index2 > -1; --index2)
        {
          Vedomost_VB.OneDataVed oneDataVed2 = this.List_OneDataVed[index2];
          if (oneDataVed1.ObjectType == oneDataVed2.ObjectType)
            this.List_OneDataVed.RemoveAt(index1);
        }
      }
    }

    /// <summary> Удаление данных данного типа </summary>
    /// <param name="objType"></param>
    /// <returns></returns>
    public bool Removal_Date(int objType)
    {
      if (objType == 0)
        return false;
      bool flag = false;
      for (int index = this.List_OneDataVed.Count - 1; index > -1; --index)
      {
        if (this.List_OneDataVed[index].ObjectType == objType)
        {
          this.List_OneDataVed.RemoveAt(index);
          flag = true;
        }
      }
      return flag;
    }

    /// <summary> Проверка есть ли уже данные этого типа </summary>
    /// <param name="objType"></param>
    /// <returns></returns>
    public bool Check_Date(int objType)
    {
      if (objType == 0)
        return false;
      for (int index = 0; index < this.List_OneDataVed.Count; ++index)
      {
        if (this.List_OneDataVed[index].ObjectType == objType)
          return true;
      }
      return false;
    }

    /// <summary> Получение данных именно в виде строки </summary>
    /// <param name="objType"></param>
    /// <returns></returns>
    public string Get_Data_String_for_objType(int objType)
    {
      string stringForObjType = "";
      for (int index = 0; index < this.List_OneDataVed.Count; ++index)
      {
        Vedomost_VB.OneDataVed oneDataVed = this.List_OneDataVed[index];
        if (oneDataVed == null)
        {
          stringForObjType = "";
          break;
        }
        if (oneDataVed.ObjectType == objType || -oneDataVed.ObjectType == objType)
        {
          if (oneDataVed.TypeDataSel == Vedomost_VB.TypeDataSel.Float)
          {
            if ((double) Math.Abs((float) oneDataVed.Data) > 0.0)
            {
              stringForObjType = Convert.ToString(oneDataVed.Data);
              break;
            }
          }
          else if (oneDataVed.TypeDataSel == Vedomost_VB.TypeDataSel.Int)
          {
            if ((int) oneDataVed.Data != 0)
            {
              stringForObjType = Convert.ToString(oneDataVed.Data);
              break;
            }
          }
          else if (oneDataVed.TypeDataSel == Vedomost_VB.TypeDataSel.Long)
          {
            if ((long) oneDataVed.Data != 0L)
            {
              stringForObjType = Convert.ToString(oneDataVed.Data);
              break;
            }
          }
          else
          {
            stringForObjType = Convert.ToString(oneDataVed.Data);
            break;
          }
        }
      }
      return stringForObjType;
    }

    public string Get_Data_String_for_TypeFieldVedRec(Vedomost_VB.TypeFieldVedRec typeFieldVedRec)
    {
      string forTypeFieldVedRec = "";
      switch (typeFieldVedRec)
      {
        case Vedomost_VB.TypeFieldVedRec.Razdel_Ved:
          if (this.Razdel_Ved != 0L)
          {
            forTypeFieldVedRec = this.Razdel_Ved.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.PodRazdel_Ved:
          if (this.PodRazdel_Ved != 0)
          {
            forTypeFieldVedRec = this.PodRazdel_Ved.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.KudaDesignation:
          forTypeFieldVedRec = this.KudaDesignation;
          break;
        case Vedomost_VB.TypeFieldVedRec.Ispolnenie:
          forTypeFieldVedRec = this.Ispolnenie;
          break;
        case Vedomost_VB.TypeFieldVedRec.DocumentTypeName:
          forTypeFieldVedRec = this.DocumentTypeName;
          break;
        case Vedomost_VB.TypeFieldVedRec.Uroven:
          forTypeFieldVedRec = this.UrovenS;
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_Sp:
          if ((double) Math.Abs(this.Count_in_Sp) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_Sp.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_Izdelie:
          if ((double) Math.Abs(this.Count_in_Izdelie) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_Izdelie.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_SpKompl:
          if ((double) Math.Abs(this.Count_in_SpKompl) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_SpKompl.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_SpRegulir:
          if ((double) Math.Abs(this.Count_in_SpRegulir) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_SpRegulir.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.CountF_samOi_sp:
          if ((double) Math.Abs(this.CountF_samOi_sp) > 0.0)
          {
            forTypeFieldVedRec = this.CountF_samOi_sp.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_Vsego:
          if ((double) Math.Abs(this.Count_Vsego) > 0.0)
          {
            forTypeFieldVedRec = this.Count_Vsego.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_Summ:
          if ((double) Math.Abs(this.Count_Summ) > 0.0)
          {
            forTypeFieldVedRec = this.Count_Summ.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.EdIzmKol:
          forTypeFieldVedRec = this.EdIzmKol;
          break;
        case Vedomost_VB.TypeFieldVedRec.Remark:
          forTypeFieldVedRec = this.Remark;
          break;
        case Vedomost_VB.TypeFieldVedRec.DerzPodl:
          forTypeFieldVedRec = this.DerzPodlDoc;
          break;
      }
      return forTypeFieldVedRec;
    }

    /// <summary> Чтение из RecordNew ObjectIdIzd </summary>
    /// <returns></returns>
    public long Get_ObjectID()
    {
      long objectId = 0;
      Vedomost_VB.OneDataVed data = this.Get_Data(-2);
      if (data != null)
        objectId = Convert.ToInt64(data.Data);
      return objectId;
    }

    public void Set_ObjectID(long objectID)
    {
      this.Removal_Date(-2);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, AttributeSourceTypes.Object, -2, (object) objectID));
    }

    /// <summary> Чтение из RecordNew ObjectType </summary>
    /// <returns></returns>
    public int Get_ObjectType()
    {
      int objectType = 0;
      Vedomost_VB.OneDataVed data = this.Get_Data(-7);
      if (data != null)
        objectType = (int) data.Data;
      return objectType;
    }

    public void Set_ObjectType(int objectType)
    {
      this.Removal_Date(-7);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Long, AttributeSourceTypes.Object, -7, (object) objectType));
    }

    /// <summary> Чтение из RecordNew designation </summary>
    /// <returns></returns>
    public string Get_Designation()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Designation);
      string designation = data == null ? "" : (string) data.Data;
      if (designation == "" && this.List_For_Rebuilding_From_Graf != null)
      {
        designation = Vedomost_VB_Static.Text_From_List_For_Rebuilding_From_Graf(this.List_For_Rebuilding_From_Graf, "Обозначение");
        if (designation == "")
          designation = Vedomost_VB_Static.Text_From_List_For_Rebuilding_From_Graf(this.List_For_Rebuilding_From_Graf, "Обозначение документа на поставку");
      }
      return designation;
    }

    /// <summary> Запись в RecordNew designation </summary>
    /// <param name="designation"></param>
    public void Set_Designation(string designation)
    {
      this.Removal_Date(AvsIDCache.Attr_Designation);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Designation, (object) designation));
    }

    public string Get_Name()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Name);
      string name = data == null ? "" : (string) data.Data;
      if (name == "" && this.List_For_Rebuilding_From_Graf != null)
        name = Vedomost_VB_Static.Text_From_List_For_Rebuilding_From_Graf(this.List_For_Rebuilding_From_Graf, "Наименование");
      return name;
    }

    public void Set_Name(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_Name);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Name, (object) name));
    }

    /// <summary> Примечание </summary>
    /// <param name="note"></param>
    public void Set_Note(string note)
    {
      this.Removal_Date(AvsIDCache.Attr_Note);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Note, (object) note));
    }

    public string Get_FuncGroup()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_FuncGroup);
      return data == null ? "" : (string) data.Data;
    }

    public string Get_Position()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Position);
      return data == null ? "" : (string) data.Data;
    }

    public string Get_Note()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Note);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_FuncGroup(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_FuncGroup);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_FuncGroup, (object) name));
    }

    public string Get_Class()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Class);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_Class(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_Class);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Class, (object) name));
    }

    public string Get_Razmery_I_Parametry()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Razmery_I_Parametry);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_Razmery_I_Parametry(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_Razmery_I_Parametry);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Razmery_I_Parametry, (object) name));
    }

    public string Get_Gost()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Gost);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_Gost(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_Gost);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Gost, (object) name));
    }

    public string Get_Postavthik()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Postavthik);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_Postavthik(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_Postavthik);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_Postavthik, (object) name));
    }

    public string Get_DerzPodl()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_DerzPodl);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_DerzPodl(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_DerzPodl);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_DerzPodl, (object) name));
    }

    public string Get_OKPCode()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_OKPCode);
      return data == null ? "" : (string) data.Data;
    }

    public void Set_OKPCode(string name)
    {
      this.Removal_Date(AvsIDCache.Attr_OKPCode);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.String, AttributeSourceTypes.Object, AvsIDCache.Attr_OKPCode, (object) name));
    }

    public int Get_Listov()
    {
      Vedomost_VB.OneDataVed data = this.Get_Data(AvsIDCache.Attr_Listov);
      return data == null ? 0 : Convert.ToInt32(data.Data);
    }

    public void Set_Listov(int listov)
    {
      this.Removal_Date(AvsIDCache.Attr_Listov);
      this.List_OneDataVed.Add(new Vedomost_VB.OneDataVed(Vedomost_VB.TypeDataSel.Int, AttributeSourceTypes.Object, AvsIDCache.Attr_Listov, (object) listov));
    }
  }

  /// <summary> Вторичная запись </summary>
  public class RecordForVed_Vtor
  {
    public string KudaDesignation;
    public long KudaObjectId;
    public string Count_in_Sp_S;
    public float Count_in_Sp;
    public float Count_in_Izdelie;
    public string Count_in_SpKompl_S;
    public float Count_in_SpKompl;
    public string Count_in_SpRegulir_S;
    public float Count_in_SpRegulir;
    public float CountF_samOi_sp;
    public float Count_Vsego;
    public List<Vedomost_VB.OneDataVed> List_OneDataVed = new List<Vedomost_VB.OneDataVed>();
    public List<Vedomost_VB.One_Grafa> List_For_Rebuilding_From_Graf;
    public List<Vedomost_VB.One_Attribute> List_For_Rebuilding_From_Attributes;

    /// <summary> Получение данных по objType </summary>
    /// <param name="objType"></param>
    /// <returns></returns>
    public Vedomost_VB.OneDataVed Get_Data(int objType)
    {
      for (int index = 0; index < this.List_OneDataVed.Count; ++index)
      {
        Vedomost_VB.OneDataVed data = this.List_OneDataVed[index];
        if (data.ObjectType == objType)
          return data;
      }
      return (Vedomost_VB.OneDataVed) null;
    }

    /// <summary> Получение данных именно в виде строки </summary>
    /// <param name="objType"></param>
    /// <returns></returns>
    public string Get_Data_String_for_objType(int objType)
    {
      string stringForObjType = "";
      for (int index = 0; index < this.List_OneDataVed.Count; ++index)
      {
        Vedomost_VB.OneDataVed oneDataVed = this.List_OneDataVed[index];
        if (oneDataVed == null)
        {
          stringForObjType = "";
          break;
        }
        if (oneDataVed.ObjectType == objType)
        {
          if (oneDataVed.TypeDataSel == Vedomost_VB.TypeDataSel.Float)
          {
            if ((double) Math.Abs((float) oneDataVed.Data) <= 0.0)
              stringForObjType = "";
          }
          else if (oneDataVed.TypeDataSel == Vedomost_VB.TypeDataSel.Int)
          {
            if ((int) oneDataVed.Data == 0)
              stringForObjType = "";
          }
          else if (oneDataVed.TypeDataSel == Vedomost_VB.TypeDataSel.Long)
          {
            if ((long) oneDataVed.Data == 0L)
              stringForObjType = "";
          }
          else
            stringForObjType = Convert.ToString(oneDataVed.Data);
        }
      }
      return stringForObjType;
    }

    public string Get_Data_String_for_TypeFieldVedRec(Vedomost_VB.TypeFieldVedRec typeFieldVedRec)
    {
      string forTypeFieldVedRec = "";
      switch (typeFieldVedRec)
      {
        case Vedomost_VB.TypeFieldVedRec.KudaDesignation:
          forTypeFieldVedRec = this.KudaDesignation;
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_Sp:
          if ((double) Math.Abs(this.Count_in_Sp) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_Sp.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_Izdelie:
          if ((double) Math.Abs(this.Count_in_Izdelie) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_Izdelie.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_SpKompl:
          if ((double) Math.Abs(this.Count_in_SpKompl) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_SpKompl.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_in_SpRegulir:
          if ((double) Math.Abs(this.Count_in_SpRegulir) > 0.0)
          {
            forTypeFieldVedRec = this.Count_in_SpRegulir.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.CountF_samOi_sp:
          if ((double) Math.Abs(this.CountF_samOi_sp) > 0.0)
          {
            forTypeFieldVedRec = this.CountF_samOi_sp.ToString();
            break;
          }
          break;
        case Vedomost_VB.TypeFieldVedRec.Count_Vsego:
          if ((double) Math.Abs(this.Count_Vsego) > 0.0)
          {
            forTypeFieldVedRec = this.Count_Vsego.ToString();
            break;
          }
          break;
      }
      return forTypeFieldVedRec;
    }

    public Vedomost_VB.RecordForVed_Vtor Copy()
    {
      return new Vedomost_VB.RecordForVed_Vtor()
      {
        KudaDesignation = this.KudaDesignation,
        KudaObjectId = this.KudaObjectId,
        Count_in_Sp_S = this.Count_in_Sp_S,
        Count_in_Sp = this.Count_in_Sp,
        Count_in_Izdelie = this.Count_in_Izdelie,
        Count_in_SpKompl_S = this.Count_in_SpKompl_S,
        Count_in_SpKompl = this.Count_in_SpKompl,
        Count_in_SpRegulir_S = this.Count_in_SpRegulir_S,
        Count_in_SpRegulir = this.Count_in_SpRegulir,
        CountF_samOi_sp = this.CountF_samOi_sp,
        Count_Vsego = this.Count_Vsego
      };
    }
  }

  /// <summary> Вторичная запись для ИСПОЛНЕНИЯ </summary>
  public class RecordForVed_For_Isp
  {
    public string Ispolnenie = "";
    public string Ispolnenie_Zagol = "";
    public string Count_SummS = "";
    public float Count_Summ;

    public Vedomost_VB.RecordForVed_For_Isp Copy()
    {
      return new Vedomost_VB.RecordForVed_For_Isp()
      {
        Ispolnenie = this.Ispolnenie,
        Ispolnenie_Zagol = this.Ispolnenie_Zagol,
        Count_SummS = this.Count_SummS,
        Count_Summ = this.Count_Summ
      };
    }
  }

  /// <summary> Отдельное поле данных ВЕДОМОСТИ</summary>
  /// 
  ///             Сюда помещаются данные СОВМЕСТИМЫЕ с данными СП
  public class OneDataVed
  {
    private Vedomost_VB.TypeDataSel _typeDataSel;
    private AttributeSourceTypes _attributeSourceTypes;
    private int _objectType;
    private object _data;

    public Vedomost_VB.TypeDataSel TypeDataSel
    {
      get => this._typeDataSel;
      set => this._typeDataSel = value;
    }

    public AttributeSourceTypes AttributeSourceTypes
    {
      get => this._attributeSourceTypes;
      set => this._attributeSourceTypes = value;
    }

    public int ObjectType
    {
      get => this._objectType;
      set => this._objectType = value;
    }

    public object Data
    {
      get
      {
        switch (this._typeDataSel)
        {
          case Vedomost_VB.TypeDataSel.Int:
            return (object) (int) this._data;
          case Vedomost_VB.TypeDataSel.Long:
            return (object) (long) this._data;
          case Vedomost_VB.TypeDataSel.Float:
            return (object) (float) this._data;
          case Vedomost_VB.TypeDataSel.String:
            return (object) (string) this._data;
          case Vedomost_VB.TypeDataSel.Guid:
            return (object) (Guid) this._data;
          default:
            return (object) null;
        }
      }
      set => this._data = value;
    }

    /// <summary> Конструктор отдельного поля данных ВЕДОМОСТИ </summary>
    /// <param name="typeDataSel"> Тип данных (Int, Long, Flot, String, Guid</param>
    /// <param name="attributeSourceTypes">атрибут связи или объекта</param>
    /// <param name="imsObjectType"> Тип (номер) атрибута</param>
    /// <param name="data"> Сами данные</param>
    public OneDataVed(
      Vedomost_VB.TypeDataSel typeDataSel,
      AttributeSourceTypes attributeSourceTypes,
      int objType,
      object data)
    {
      this._attributeSourceTypes = attributeSourceTypes;
      this._typeDataSel = typeDataSel;
      this._objectType = objType;
      this.Data = data;
    }
  }

  public class One_Grafa
  {
    public string templateId;
    public string text;
  }

  public class One_Attribute
  {
    public string name_Attribute;
    public string text_Attribute;
  }

  /// <summary> Описание поля для чтения из спецификации </summary>
  public class OneFieldSpForRead
  {
    public int _id;
    public Guid _guid;
    public string _name;
    public AttributeSourceTypes _attributeSourceTypes;
    public Vedomost_VB.TypeDataSel _type;
    public int _perv_Vtor;

    public OneFieldSpForRead(
      int id,
      AttributeSourceTypes attr,
      Vedomost_VB.TypeDataSel attrType,
      int perv_Vtor = 1)
    {
      this._id = id;
      this._name = MetaDataHelper.GetAttributeTypeName(id);
      this._guid = MetaDataHelper.GetObjectTypeGuid(id);
      this._attributeSourceTypes = attr;
      this._type = attrType;
      if (perv_Vtor != 1 && perv_Vtor != 2)
        perv_Vtor = 1;
      this._perv_Vtor = perv_Vtor;
    }

    public OneFieldSpForRead(
      Guid guid,
      AttributeSourceTypes attr,
      Vedomost_VB.TypeDataSel attrType,
      int perv_Vtor = 1)
    {
      this._guid = guid;
      this._id = MetaDataHelper.GetObjectTypeID(guid);
      this._name = MetaDataHelper.GetAttributeTypeName(guid);
      this._attributeSourceTypes = attr;
      this._type = attrType;
      if (perv_Vtor != 1 && perv_Vtor != 2)
        perv_Vtor = 1;
      this._perv_Vtor = perv_Vtor;
    }

    public OneFieldSpForRead(
      string name,
      AttributeSourceTypes attr,
      Vedomost_VB.TypeDataSel attrType,
      int perv_Vtor = 1)
    {
      this._name = name;
      this._id = MetaDataHelper.GetObjectTypeID(name);
      this._guid = MetaDataHelper.GetObjectTypeGuid(this._id);
      this._attributeSourceTypes = attr;
      this._type = attrType;
      if (perv_Vtor != 1 && perv_Vtor != 2)
        perv_Vtor = 1;
      this._perv_Vtor = perv_Vtor;
    }

    public OneFieldSpForRead(
      int id,
      Guid guid,
      string name,
      AttributeSourceTypes attr,
      Vedomost_VB.TypeDataSel attrType,
      int perv_Vtor = 1)
    {
      this._name = name;
      this._id = id;
      this._guid = guid;
      this._attributeSourceTypes = attr;
      this._type = attrType;
      if (perv_Vtor != 1 && perv_Vtor != 2)
        perv_Vtor = 1;
      this._perv_Vtor = perv_Vtor;
    }
  }

  /// <summary> ОДНО условие ввода из СП </summary>
  public class Usl_Read_From_SP_One
  {
    public Vedomost_VB.OneFieldSpForRead _oneFieldSpForRead;
    public string _uslovie;
    public string _text;
    public bool _or_and;

    public Usl_Read_From_SP_One()
    {
      this._oneFieldSpForRead = (Vedomost_VB.OneFieldSpForRead) null;
      this._uslovie = "";
      this._text = "";
      this._or_and = false;
    }

    /// <summary> Конструктор </summary>
    /// <param name="oneFieldSpForRead"></param>
    /// <param name="uslovie"></param>
    /// <param name="text"></param>
    /// <param name="or_and"></param>
    public Usl_Read_From_SP_One(
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead,
      string uslovie,
      string text,
      bool or_and)
    {
      this._oneFieldSpForRead = oneFieldSpForRead;
      this._uslovie = uslovie;
      this._text = text;
      this._or_and = or_and;
    }
  }

  /// <summary> Список условий ввода ЗАПИСИ из СП </summary>
  public class Usl_Read_From_SP
  {
    public string _section_SP;
    public List<Vedomost_VB.Usl_Read_From_SP_One> _list_Usl_Read_From_SP_One;

    public Usl_Read_From_SP()
    {
      this._section_SP = "";
      this._list_Usl_Read_From_SP_One = (List<Vedomost_VB.Usl_Read_From_SP_One>) null;
    }

    public Usl_Read_From_SP(string section_SP)
    {
      this._section_SP = section_SP;
      this._list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>();
    }
  }

  /// <summary> Раздел ВЕДОМОСТИ </summary>
  public class OneRazdelVed
  {
    public int _razdelVed;
    public string _caption;
    public string _name;
    public List<Vedomost_VB.OnePodRazdelVed> _list_onePodRazdels;
    public string _namePage;
  }

  /// <summary> ПОДраздел ВЕДОМОСТИ </summary>
  public class OnePodRazdelVed
  {
    public int _podRazdelVed;
    public string _name;
  }

  /// <summary> Базовые опции ведомости </summary>
  public class Bases_Options_Ved
  {
    public bool _isReadOrInit_isMain;
    public bool _isMainSort1;
    public bool _isMainSummOdinakovyh;
    public bool _isMainSort2;
    public bool _isMainCreateVtorRecords;
    public bool _isMainSumm;
    public bool _isOnlyUroven1;
    public bool _is_Specification_Instrument;
    public List<string> _opеning_Sections;
    public bool _isVedSortGroup;
    public bool _isVedMergerIsp;
    public bool _isVedAddFuncGroup;
    public bool _isVedSort1;
    public bool _isVedUnion;
    public bool _isVedExtrectionVtor;
    public bool _isVedMergerVtor;
    public bool _isVedSortVtor;
    public bool _isVedSummVtor;
    public bool _isVedCreateZagolIspoln;
    public bool _isVedCreateZagolSvoiaVed;
    public bool _isVedCreateZagolPoPriznaku;
    public bool _isVedAddToSp;
    public int _isVedAddToRazdel;
    public bool _isFor_ZIP_SB_Raskr;
    public bool _isFor_ZIP_SB_Add;
    public bool _isFor_ZIP_COMPL_Raskr;
    public bool _isFor_ZIP_COMPL_Add;
    public bool _isInputDoc;
    public bool _isInputIzd;
    public bool _isInputMat;
    public List<QuickObjectInfo> _list_quickObjectInfo;
    public bool _is_Extended_List_Names;
  }

  /// <summary> Защита от редактирования </summary>
  public class Protection_From_Editing
  {
    public bool _isFullProhibition;
    public bool _isProhibition_DocRowWithObj;
    public bool _isProtectionCommand;
  }

  /// <summary>  Дополнительные данные, характерные для конкретной ведомости </summary>
  /// 
  ///             На всякий случай. Пока не используется
  public class Dopoln_Options_Ved
  {
    public string _text1;
    public string _text2;
    public int _int1;
    public int _int2;
    public double _double1;
    public double _double2;
    public bool _bool1;
    public bool _bool2;
  }

  public class Sbor_Options
  {
    public bool _is_Vydeliat_Sami_Komplekty;
    public bool _is_Vydeliat_Therez_Komplekty;
    public bool _isSamuSP_ne_iz_spiska_zanosit;
    public bool _isReference_Show;
    public int _isRaskrSP_s_takoi_Ved;
    public int _isDopZam;
    public int _isAllocateDopZam;
  }

  public class ESPD
  {
    public bool _isAddLU = true;
    public bool _isCreateLU = true;
    public bool _isOpenLU = true;
    public bool _isAddToSpLU = true;
    public bool _isAddRemark = true;
    public string _textRemark = "Размножать по указанию";
  }

  /// <summary> Один элемент Что и КУда входит </summary>
  public class Izdelie_Doc
  {
    public long _objectId_KudaVhodit;
    public long _objectIdDoc;
    public string _designation;
    public string _name;
  }

  /// <summary> Один элемент для описания ссылок </summary>
  public class Reference
  {
    public long _objectIdParent;
    public long _objectId_For_Reference;
  }

  /// <summary> ОДИН заголовок ведомости </summary>
  public class One_Zagolovok
  {
    public string _granicaPriznaka;
    public string _name;
  }

  /// <summary> Список заголовков ведомости </summary>
  public class Zagolovki_Ved
  {
    public Vedomost_VB.TypeField _typeField;
    public int _objectType;
    public bool _vyvodit_PodZagolovki;
    public bool _userZagolovki;
    public bool _locationZagolovki = true;
    public Vedomost_VB.TypeFieldVedRec _typeFieldVedRec;
    public Vedomost_VB.TypeCompare _typeCompare;
    public List<Vedomost_VB.One_Zagolovok> _list_One_Zagolovok = new List<Vedomost_VB.One_Zagolovok>();
    public string _include_Name = "Ведомости составных частей";

    public XmlElement Xml_OneDataFieldToZagol(XmlDocument xmlDocument)
    {
      XmlElement element = xmlDocument.CreateElement(string.Empty, "oneDataFieldToZagol", string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("typeField");
      attribute1.Value = this._typeField.ToString();
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("objectType");
      attribute2.Value = this._objectType.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("vyvodit_PodZagolovki");
      attribute3.Value = this._vyvodit_PodZagolovki.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("userZagolovki");
      attribute4.Value = this._userZagolovki.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("locationZagolovki");
      attribute5.Value = this._locationZagolovki.ToString();
      element.Attributes.Append(attribute5);
      XmlAttribute attribute6 = xmlDocument.CreateAttribute("typeCompare");
      attribute6.Value = this._typeCompare.ToString();
      element.Attributes.Append(attribute6);
      XmlAttribute attribute7 = xmlDocument.CreateAttribute("include_Name");
      attribute7.Value = this._include_Name;
      element.Attributes.Append(attribute7);
      XmlAttribute attribute8 = xmlDocument.CreateAttribute("typeFieldVedRec");
      int typeFieldVedRec = (int) this._typeFieldVedRec;
      attribute8.Value = typeFieldVedRec.ToString();
      element.Attributes.Append(attribute8);
      return element;
    }

    public void Clear()
    {
      this._list_One_Zagolovok.Clear();
      this._typeField = Vedomost_VB.TypeField.Undefined;
      this._objectType = -1;
      this._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
    }
  }

  /// <summary> Получение копии Zagolovki_Ved </summary>
  /// <param name="zagolovki_Ved1"></param>
  /// <returns></returns>
  public enum PoriadokSortirovki
  {
    Vozrastanie,
    Ubyvanie,
  }

  public enum PustyeStroki
  {
    Vnathale,
    Vkonce,
  }

  public enum Sravnenie
  {
    Symbol,
    Number,
  }

  public enum BeginSravn
  {
    S_begin,
    S_pozicii,
    Ot_symbola,
    Ot_symbola_s_konca,
  }

  public enum EndSravn
  {
    Do_end,
    Skolko,
    Do_symbola,
    Do_symbola_s_konca,
  }

  /// <summary> Все правила сортировки ВЕДОМОСТИ </summary>
  public class Sorting_Usl
  {
    public Vedomost_VB.Sorting_Usl_One_From4 Sorting_Usl_MainOsn;
    public Vedomost_VB.Sorting_Usl_One_From4 Sorting_Usl_MainVtor;
    public Vedomost_VB.Sorting_Usl_One_From4 Sorting_Usl_VedOsn;
    public Vedomost_VB.Sorting_Usl_One_From4 Sorting_Usl_VedVtor;
  }

  /// <summary> Настройки сортировки ведомости </summary>
  public class Sorting_Usl_One_From4
  {
    public string _name;
    public List<Vedomost_VB.Sorting_Usl_OneRazdel> _list_sorting_Usl_OneRazdel;
  }

  /// <summary> Список условий для одного раздела </summary>
  public class Sorting_Usl_OneRazdel
  {
    public long _razdelNum;
    public List<Vedomost_VB.Sorting_Usl_One> _list_sorting_Usl_One;
  }

  /// <summary> Одно условие сравнения </summary>
  public class Sorting_Usl_One
  {
    public Vedomost_VB.TypeField _typeField;
    public int _objectType;
    public Vedomost_VB.TypeFieldVedRec _typeFieldVedRec;
    public Vedomost_VB.BeginSravn _beginSravn;
    public string _symb_ot;
    public int _num_symb_ot;
    public Vedomost_VB.EndSravn _endSravn;
    public string _symb_do;
    public int _num_symb_do;
    public Vedomost_VB.Sravnenie _sravnenie;
    public Vedomost_VB.PoriadokSortirovki _poriadokSortirovki;
    public Vedomost_VB.PustyeStroki _pustyeStroki;
  }

  /// <summary> Сортировка ГОТОВОГО ДОКУМЕНТА </summary>
  public class Sorting_Usl_Doc
  {
    public List<Vedomost_VB.Sorting_Usl_Doc_OneRazdel> _list_sorting_Usl_Doc = new List<Vedomost_VB.Sorting_Usl_Doc_OneRazdel>();
  }

  /// <summary> Список условий сортировки ДОКУМЕНТА для одного раздела </summary>
  public class Sorting_Usl_Doc_OneRazdel
  {
    public long _razdelNum;
    public List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa> _list_sorting_Usl_Doc_OneRazdel = new List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa>();
  }

  /// <summary> Сравнение двух СТРОК в одной графе </summary>
  public class Sorting_Usl_Doc_OneGrafa
  {
    public string _grafa;
    public Vedomost_VB.BeginSravn _beginSravn;
    public string _symb_ot;
    public int _num_symb_ot;
    public Vedomost_VB.EndSravn _endSravn;
    public string _symb_do;
    public int _num_symb_do;
    public Vedomost_VB.Sravnenie _sravnenie;
    public Vedomost_VB.PoriadokSortirovki _poriadokSortirovki;
    public Vedomost_VB.PustyeStroki _pustyeStroki;
  }

  /// <summary> Одно сравнение при объединении строк  </summary>
  public class Merge_Usl_One
  {
    public Vedomost_VB.TypeField _typeField;
    public int _objectType;
    public Vedomost_VB.TypeFieldVedRec _typeFieldVedRec;

    public Merge_Usl_One()
    {
      this._typeField = Vedomost_VB.TypeField.ObjectType;
      this._objectType = -1;
      this._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
    }

    public Merge_Usl_One(
      Vedomost_VB.TypeField typeField,
      int objectType,
      Vedomost_VB.TypeFieldVedRec typeFieldVedRec)
    {
      this._typeField = typeField;
      this._objectType = objectType;
      this._typeFieldVedRec = typeFieldVedRec;
    }

    public string Name_Attribut()
    {
      string str = "";
      if (this._typeField == Vedomost_VB.TypeField.ObjectType)
        str = MetaDataHelper.GetAttributeTypeName(this._objectType);
      if (this._typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
      {
        int index = -1;
        str = Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(this._typeFieldVedRec, out index)._name;
      }
      return str;
    }
  }

  public class Merge_Usl2
  {
    public List<Vedomost_VB.Merge_Usl_One> _list_Merge_Usl2 = new List<Vedomost_VB.Merge_Usl_One>();

    public int Find_Merge_Usl_One(Vedomost_VB.Merge_Usl_One merge_Usl_One)
    {
      if (merge_Usl_One == null || this._list_Merge_Usl2 == null || this._list_Merge_Usl2.Count == 0)
        return -1;
      for (int index = 0; index < this._list_Merge_Usl2.Count; ++index)
      {
        Vedomost_VB.Merge_Usl_One mergeUslOne = this._list_Merge_Usl2[index];
        if (merge_Usl_One._objectType == mergeUslOne._objectType && merge_Usl_One._typeField == mergeUslOne._typeField && merge_Usl_One._typeFieldVedRec == mergeUslOne._typeFieldVedRec)
          return index;
      }
      return -1;
    }
  }

  /// <summary> Настройки вывода ведомости в шаблон </summary>
  public class AlgorithmToPrint
  {
    public int _kolGraf;
    public string _tableName;
    public List<Vedomost_VB.OneRazdelToPrint> _list_OneRazdelToPrint;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Info;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintIncluded;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintTitleIncluded;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintTitleVar;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintTitleIsp;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintTitle;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintTitlePodSection;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintRemark;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintRemarkShort;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintPasport;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintEmpty;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintTitlePart;
    public List<Vedomost_VB.OneRazdelToPrintAdditional> _list_OneRazdelToPrintAdditional;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintAdditional1;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintAdditional2;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintAdditional3;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrintAdditional4;
    public XmlElement _nameNastr;
    public int _iLIZM = 5;
    public int _includedLizmInDoc;
    public int _afterInfo = 1;
    public int _afterRemark = 1;
    public int _additional1;
    public int _additional2;
    public int _additional3;
    public int _additional4;
    public bool _isDeleteIdenticalTexts;
    public bool _isCheck = true;
    public bool _isUnbrokenDefis;

    public XmlElement Xml_OneRecordToPrint(
      XmlDocument xmlDocument,
      Vedomost_VB.OneRecordToPrint oneRecordToPrint,
      string number = "")
    {
      if (oneRecordToPrint == null)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, "ONERECORDTOPRINT", string.Empty);
      if (!string.IsNullOrEmpty(number))
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("ADDITIONAL");
        attribute.Value = number;
        element1.Attributes.Append(attribute);
      }
      if (oneRecordToPrint._nameTypeRec != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("NAME");
        attribute.Value = oneRecordToPrint._nameTypeRec.ToString();
        element1.Attributes.Append(attribute);
      }
      if (!string.IsNullOrEmpty(oneRecordToPrint._parentId))
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("PARENTID");
        attribute.Value = oneRecordToPrint._parentId.ToString();
        element1.Attributes.Append(attribute);
      }
      if (oneRecordToPrint._tableRowId != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("TABLEROWID");
        attribute.Value = oneRecordToPrint._tableRowId.ToString();
        element1.Attributes.Append(attribute);
      }
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("ISVTOROBLAST");
      attribute1.Value = oneRecordToPrint._isVtorOblast.ToString();
      element1.Attributes.Append(attribute1);
      if (!string.IsNullOrEmpty(oneRecordToPrint._tableVtorOblastId))
      {
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("TABLEVTOROBLASTID");
        attribute2.Value = oneRecordToPrint._tableVtorOblastId.ToString();
        element1.Attributes.Append(attribute2);
      }
      if (oneRecordToPrint._listOneGrafaToPrint != null)
      {
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "LISTONEGRAFATOPRINT", string.Empty);
        for (int index = 0; index < oneRecordToPrint._listOneGrafaToPrint.Count; ++index)
        {
          Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = oneRecordToPrint._listOneGrafaToPrint[index];
          XmlElement print = this.Xml_OneGrafaToPrint(xmlDocument, oneGrafaToPrint);
          if (print != null)
            element2.AppendChild((XmlNode) print);
        }
        element1.AppendChild((XmlNode) element2);
      }
      if (oneRecordToPrint._isVtorOblast)
      {
        if (oneRecordToPrint._oneRecordToPrint_Vtor != null)
        {
          XmlElement print = this.Xml_OneRecordToPrint(xmlDocument, oneRecordToPrint._oneRecordToPrint_Vtor);
          if (print != null)
            element1.AppendChild((XmlNode) print);
        }
        if (oneRecordToPrint._oneRecordToPrint_Itogo != null)
        {
          XmlElement print = this.Xml_OneRecordToPrint(xmlDocument, oneRecordToPrint._oneRecordToPrint_Itogo);
          if (print != null)
            element1.AppendChild((XmlNode) print);
        }
      }
      return element1;
    }

    public XmlElement Xml_OneGrafaToPrint(
      XmlDocument xmlDocument,
      Vedomost_VB.OneGrafaToPrint oneGrafaToPrint)
    {
      if (oneGrafaToPrint == null)
        return (XmlElement) null;
      if (oneGrafaToPrint._listOneDataFieldToPrint == null)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, nameof (oneGrafaToPrint), string.Empty);
      XmlAttribute attribute = xmlDocument.CreateAttribute("cellNumber");
      attribute.Value = oneGrafaToPrint._cell_ID.ToString();
      element1.Attributes.Append(attribute);
      XmlElement element2 = xmlDocument.CreateElement(string.Empty, "listOneDataFieldToPrint", string.Empty);
      for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
      {
        Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint = oneGrafaToPrint._listOneDataFieldToPrint[index];
        XmlElement print = this.Xml_OneDataFieldToPrint(xmlDocument, oneDataFieldToPrint);
        element2.AppendChild((XmlNode) print);
      }
      element1.AppendChild((XmlNode) element2);
      return element1;
    }

    public XmlElement Xml_OneDataFieldToPrint(
      XmlDocument xmlDocument,
      Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint)
    {
      if (oneDataFieldToPrint == null)
        return (XmlElement) null;
      XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (oneDataFieldToPrint), string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("symbolRazd");
      attribute1.Value = oneDataFieldToPrint._symbolRazd.ToString();
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("typeField");
      attribute2.Value = oneDataFieldToPrint._typeField.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("objectType");
      attribute3.Value = oneDataFieldToPrint._objectType.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("typeFieldVedRec");
      int typeFieldVedRec = (int) oneDataFieldToPrint._typeFieldVedRec;
      attribute4.Value = typeFieldVedRec.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("typeFieldVedPasport");
      int typeFieldVedPasport = (int) oneDataFieldToPrint._typeFieldVedPasport;
      attribute5.Value = typeFieldVedPasport.ToString();
      element.Attributes.Append(attribute5);
      return element;
    }

    public XmlElement Xml_AlgorithmToPrint(XmlDocument xmlDocument, string name)
    {
      if (xmlDocument == null)
        return (XmlElement) null;
      XmlElement element = xmlDocument.CreateElement(string.Empty, name, string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("TABLENAME");
      attribute1.Value = this._tableName;
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("kolGraf");
      attribute2.Value = this._kolGraf.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("iLIZM");
      attribute3.Value = this._iLIZM.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("includedLizmInDoc");
      attribute4.Value = this._includedLizmInDoc.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("afterInfo");
      attribute5.Value = this._afterInfo.ToString();
      element.Attributes.Append(attribute5);
      XmlAttribute attribute6 = xmlDocument.CreateAttribute("afterRemark");
      attribute6.Value = this._afterRemark.ToString();
      element.Attributes.Append(attribute6);
      XmlAttribute attribute7 = xmlDocument.CreateAttribute("additional1");
      attribute7.Value = this._additional1.ToString();
      element.Attributes.Append(attribute7);
      XmlAttribute attribute8 = xmlDocument.CreateAttribute("additional2");
      attribute8.Value = this._additional2.ToString();
      element.Attributes.Append(attribute8);
      XmlAttribute attribute9 = xmlDocument.CreateAttribute("additional3");
      attribute9.Value = this._additional3.ToString();
      element.Attributes.Append(attribute9);
      XmlAttribute attribute10 = xmlDocument.CreateAttribute("additional4");
      attribute10.Value = this._additional4.ToString();
      element.Attributes.Append(attribute10);
      XmlAttribute attribute11 = xmlDocument.CreateAttribute("isDeleteIdenticalTexts");
      attribute11.Value = this._isDeleteIdenticalTexts.ToString();
      element.Attributes.Append(attribute11);
      XmlAttribute attribute12 = xmlDocument.CreateAttribute("isCheck");
      attribute12.Value = this._isCheck.ToString();
      element.Attributes.Append(attribute12);
      XmlAttribute attribute13 = xmlDocument.CreateAttribute("isUnbrokenDefis");
      attribute13.Value = this._isUnbrokenDefis.ToString();
      element.Attributes.Append(attribute13);
      if (this._oneRecordToPrint_Info != null)
      {
        XmlElement print = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrint_Info);
        if (print != null)
          element.AppendChild((XmlNode) print);
      }
      if (this._list_OneRazdelToPrint != null && this._list_OneRazdelToPrint.Count > 0)
      {
        XmlElement printCreate = this.Xml_List_OneRazdelToPrint_Create(xmlDocument, this._list_OneRazdelToPrint);
        if (printCreate != null)
          element.AppendChild((XmlNode) printCreate);
      }
      XmlElement print1 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintTitleIncluded);
      if (print1 != null)
        element.AppendChild((XmlNode) print1);
      XmlElement print2 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintIncluded);
      if (print2 != null)
        element.AppendChild((XmlNode) print2);
      XmlElement print3 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintTitleVar);
      if (print3 != null)
        element.AppendChild((XmlNode) print3);
      XmlElement print4 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintTitleIsp);
      if (print4 != null)
        element.AppendChild((XmlNode) print4);
      XmlElement print5 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintTitle);
      if (print5 != null)
        element.AppendChild((XmlNode) print5);
      XmlElement print6 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintTitlePodSection);
      if (print6 != null)
        element.AppendChild((XmlNode) print6);
      if (this._oneRecordToPrintRemark != null)
      {
        XmlElement print7 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintRemark);
        if (print7 != null)
          element.AppendChild((XmlNode) print7);
      }
      if (this._oneRecordToPrintRemarkShort != null)
      {
        XmlElement print8 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintRemarkShort);
        if (print8 != null)
          element.AppendChild((XmlNode) print8);
      }
      if (this._oneRecordToPrintPasport != null)
      {
        XmlElement print9 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintPasport);
        if (print9 != null)
          element.AppendChild((XmlNode) print9);
      }
      if (this._oneRecordToPrintEmpty != null)
      {
        XmlElement print10 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintEmpty);
        if (print10 != null)
          element.AppendChild((XmlNode) print10);
      }
      if (this._list_OneRazdelToPrintAdditional != null && this._list_OneRazdelToPrintAdditional.Count > 0)
      {
        XmlElement additionalCreate = this.Xml_List_OneRazdelToPrintAdditional_Create(xmlDocument, this._list_OneRazdelToPrintAdditional);
        if (additionalCreate != null)
          element.AppendChild((XmlNode) additionalCreate);
      }
      else
      {
        if (this._oneRecordToPrintAdditional1 != null)
        {
          XmlElement print11 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintAdditional1);
          if (print11 != null)
            element.AppendChild((XmlNode) print11);
        }
        if (this._oneRecordToPrintAdditional2 != null)
        {
          XmlElement print12 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintAdditional2);
          if (print12 != null)
            element.AppendChild((XmlNode) print12);
        }
        if (this._oneRecordToPrintAdditional3 != null)
        {
          XmlElement print13 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintAdditional3);
          if (print13 != null)
            element.AppendChild((XmlNode) print13);
        }
        if (this._oneRecordToPrintAdditional4 != null)
        {
          XmlElement print14 = this.Xml_OneRecordToPrint(xmlDocument, this._oneRecordToPrintAdditional4);
          if (print14 != null)
            element.AppendChild((XmlNode) print14);
        }
      }
      return element;
    }

    private XmlElement Xml_List_OneRazdelToPrint_Create(
      XmlDocument xmlDocument,
      List<Vedomost_VB.OneRazdelToPrint> list_oneRazdelToPrints)
    {
      if (xmlDocument == null)
        return (XmlElement) null;
      if (list_oneRazdelToPrints == null || list_oneRazdelToPrints.Count < 1)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, "List_OneRazdelToPrint", string.Empty);
      for (int index = 0; index < list_oneRazdelToPrints.Count; ++index)
      {
        Vedomost_VB.OneRazdelToPrint oneRazdelToPrint = list_oneRazdelToPrints[index];
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "OneRazdelToPrint", string.Empty);
        XmlAttribute attribute1 = xmlDocument.CreateAttribute("RazdelVed");
        attribute1.Value = oneRazdelToPrint._razdelVed.ToString();
        element2.Attributes.Append(attribute1);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("NamePage_First");
        attribute2.Value = oneRazdelToPrint._namePage_First;
        element2.Attributes.Append(attribute2);
        XmlAttribute attribute3 = xmlDocument.CreateAttribute("NamePage_Next");
        attribute3.Value = oneRazdelToPrint._namePage_Next;
        element2.Attributes.Append(attribute3);
        XmlElement print = this.Xml_OneRecordToPrint(xmlDocument, oneRazdelToPrint._oneRecordToPrint_Info);
        if (print != null)
          element2.AppendChild((XmlNode) print);
        element1.AppendChild((XmlNode) element2);
      }
      return element1;
    }

    private XmlElement Xml_List_OneRazdelToPrintAdditional_Create(
      XmlDocument xmlDocument,
      List<Vedomost_VB.OneRazdelToPrintAdditional> list_oneRazdelToPrintsAdditional)
    {
      if (xmlDocument == null)
        return (XmlElement) null;
      if (list_oneRazdelToPrintsAdditional == null || list_oneRazdelToPrintsAdditional.Count < 1)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, "List_OneRazdelToPrintAdditional", string.Empty);
      for (int index = 0; index < list_oneRazdelToPrintsAdditional.Count; ++index)
      {
        Vedomost_VB.OneRazdelToPrintAdditional toPrintAdditional = list_oneRazdelToPrintsAdditional[index];
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "OneRazdelToPrint", string.Empty);
        XmlAttribute attribute = xmlDocument.CreateAttribute("RazdelVed");
        attribute.Value = toPrintAdditional._razdelVed.ToString();
        element2.Attributes.Append(attribute);
        XmlElement print1 = this.Xml_OneRecordToPrint(xmlDocument, toPrintAdditional._oneRecordToPrint_Additional1, "1");
        if (print1 != null)
          element2.AppendChild((XmlNode) print1);
        XmlElement print2 = this.Xml_OneRecordToPrint(xmlDocument, toPrintAdditional._oneRecordToPrint_Additional2, "2");
        if (print2 != null)
          element2.AppendChild((XmlNode) print2);
        XmlElement print3 = this.Xml_OneRecordToPrint(xmlDocument, toPrintAdditional._oneRecordToPrint_Additional3, "3");
        if (print3 != null)
          element2.AppendChild((XmlNode) print3);
        XmlElement print4 = this.Xml_OneRecordToPrint(xmlDocument, toPrintAdditional._oneRecordToPrint_Additional4, "4");
        if (print4 != null)
          element2.AppendChild((XmlNode) print4);
        element1.AppendChild((XmlNode) element2);
      }
      return element1;
    }
  }

  public class OneRazdelToPrint
  {
    public int _razdelVed;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Info;
    public string _namePage_First;
    public string _namePage_Next;

    public OneRazdelToPrint()
    {
      this._razdelVed = 1;
      this._oneRecordToPrint_Info = (Vedomost_VB.OneRecordToPrint) null;
      this._namePage_First = "";
      this._namePage_Next = "";
    }

    public OneRazdelToPrint(string namePage_First)
    {
      this._razdelVed = 1;
      this._oneRecordToPrint_Info = (Vedomost_VB.OneRecordToPrint) null;
      this._namePage_First = namePage_First;
      this._namePage_Next = "";
    }

    public OneRazdelToPrint(string namePage_First, string namePage_Next)
    {
      this._razdelVed = 1;
      this._oneRecordToPrint_Info = (Vedomost_VB.OneRecordToPrint) null;
      this._namePage_First = namePage_First;
      this._namePage_Next = namePage_Next;
    }
  }

  public class OneRecordToPrint
  {
    public string _nameTypeRec;
    public string _parentId;
    public string _tableRowId;
    public bool _isVtorOblast;
    public string _tableVtorOblastId;
    public List<Vedomost_VB.OneGrafaToPrint> _listOneGrafaToPrint;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Vtor;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Itogo;
  }

  public class OneRazdelToPrintAdditional
  {
    public int _razdelVed;
    public string _namePage_First;
    public string _namePage_Next;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Additional1;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Additional2;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Additional3;
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint_Additional4;

    public OneRazdelToPrintAdditional()
    {
      this._razdelVed = 1;
      this._oneRecordToPrint_Additional1 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional2 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional3 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional4 = (Vedomost_VB.OneRecordToPrint) null;
      this._namePage_First = "";
      this._namePage_Next = "";
    }

    public OneRazdelToPrintAdditional(string namePage_First)
    {
      this._razdelVed = 1;
      this._oneRecordToPrint_Additional1 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional2 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional3 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional4 = (Vedomost_VB.OneRecordToPrint) null;
      this._namePage_First = namePage_First;
      this._namePage_Next = "";
    }

    public OneRazdelToPrintAdditional(string namePage_First, string namePage_Next)
    {
      this._razdelVed = 1;
      this._oneRecordToPrint_Additional1 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional2 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional3 = (Vedomost_VB.OneRecordToPrint) null;
      this._oneRecordToPrint_Additional4 = (Vedomost_VB.OneRecordToPrint) null;
      this._namePage_First = namePage_First;
      this._namePage_Next = namePage_Next;
    }
  }

  public class OneRazdelToXml
  {
    public int _razdelVed;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Info;
  }

  public class OneRazdelToXmlAdditional
  {
    public int _razdelVed;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Additional1;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Additional2;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Additional3;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Additional4;
  }

  /// <summary> Соответсвие имен для связи с XML </summary>
  public class AlgorithmXml
  {
    public Vedomost_VB.OneRecordXml _oneRecordXmlPasport;
    public List<Vedomost_VB.OneRazdelToXml> _list_OneRazdelToXml;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Info;
    public Vedomost_VB.OneRecordXml _oneRecordXmlIncluded;
    public Vedomost_VB.OneRecordXml _oneRecordXmlTitleIncluded;
    public Vedomost_VB.OneRecordXml _oneRecordXmlTitleVar;
    public Vedomost_VB.OneRecordXml _oneRecordXmlTitleIsp;
    public Vedomost_VB.OneRecordXml _oneRecordXmlTitle;
    public Vedomost_VB.OneRecordXml _oneRecordXmlTitlePodSection;
    public Vedomost_VB.OneRecordXml _oneRecordXmlRemark;
    public Vedomost_VB.OneRecordXml _oneRecordXmlRemarkShort;
    public Vedomost_VB.OneRecordXml _oneRecordXmlTitlePart;
    public List<Vedomost_VB.OneRazdelToXmlAdditional> _list_OneRazdelToXmlAdditionals;
    public Vedomost_VB.OneRecordXml _oneRecordXmlAdditional1;
    public Vedomost_VB.OneRecordXml _oneRecordXmlAdditional2;
    public Vedomost_VB.OneRecordXml _oneRecordXmlAdditional3;
    public Vedomost_VB.OneRecordXml _oneRecordXmlAdditional4;
    public Vedomost_VB.OneRecordXml _oneRecordXmlEmpty;
    public XmlElement _nameNastr;
    public int _afterInfo = 1;
    public int _afterRemark;
    public int _passportOut;
    public int _passportIn;
    public string _folderXmlIn = "";

    public XmlElement Xml_AlgorithmXml(XmlDocument xmlDocument, string name)
    {
      if (xmlDocument == null)
        return (XmlElement) null;
      XmlElement element = xmlDocument.CreateElement(string.Empty, name, string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("afterInfo");
      attribute1.Value = this._afterInfo.ToString();
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("afterRemark");
      attribute2.Value = this._afterRemark.ToString();
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("passportOut");
      attribute3.Value = this._passportOut.ToString();
      element.Attributes.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("passportIn");
      attribute4.Value = this._passportIn.ToString();
      element.Attributes.Append(attribute4);
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("folderXmlIn");
      attribute5.Value = this._folderXmlIn;
      element.Attributes.Append(attribute5);
      if (this._oneRecordXmlPasport != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlPasport, "oneRecordXmlPasport");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXml_Info != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXml_Info, "oneRecordXml_Info");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlIncluded != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlIncluded, "oneRecordXmlIncluded");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlTitleIncluded != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlTitleIncluded, "oneRecordXmlTitleIncluded");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlTitleVar != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlTitleVar, "oneRecordXmlTitleVar");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlTitleIsp != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlTitleIsp, "oneRecordXmlTitleIsp");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlTitle != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlTitle, "oneRecordXmlTitle");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlTitlePodSection != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlTitlePodSection, "oneRecordXmlTitlePodSection");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlRemark != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlRemark, "oneRecordXmlRemark");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlRemarkShort != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlRemarkShort, "oneRecordXmlRemarkShort");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlTitlePart != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlTitlePart, "oneRecordXmlTitlePart");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlAdditional1 != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlAdditional1, "oneRecordXmlAdditional1");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlAdditional2 != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlAdditional2, "oneRecordXmlAdditional2");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlAdditional3 != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlAdditional3, "oneRecordXmlAdditional3");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlAdditional4 != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlAdditional4, "oneRecordXmlAdditional4");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      if (this._oneRecordXmlEmpty != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, this._oneRecordXmlEmpty, "oneRecordXmlEmpty");
        if (newChild != null)
          element.AppendChild((XmlNode) newChild);
      }
      return element;
    }

    public XmlElement Xml_OneRecordXml(
      XmlDocument xmlDocument,
      Vedomost_VB.OneRecordXml oneRecordXml,
      string name_oneRecordXml)
    {
      if (oneRecordXml == null)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, "ONERECORDXML", string.Empty);
      if (oneRecordXml._nameTypeRec != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("nameTypeRec");
        attribute.Value = oneRecordXml._nameTypeRec.ToString();
        element1.Attributes.Append(attribute);
      }
      if (oneRecordXml._tableRowId != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("tableRowId");
        attribute.Value = oneRecordXml._tableRowId.ToString();
        element1.Attributes.Append(attribute);
      }
      if (oneRecordXml._listOneFieldXml != null)
      {
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "LISTONEFIELDXML", string.Empty);
        for (int index = 0; index < oneRecordXml._listOneFieldXml.Count; ++index)
        {
          Vedomost_VB.OneFieldXml oneFielddXml = oneRecordXml._listOneFieldXml[index];
          XmlElement newChild = this.Xml_OneFieldXml(xmlDocument, oneFielddXml);
          if (newChild != null)
            element2.AppendChild((XmlNode) newChild);
        }
        element1.AppendChild((XmlNode) element2);
      }
      if (oneRecordXml._oneRecordXml_Vtor != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, oneRecordXml._oneRecordXml_Vtor, "oneRecordXml_Vtor");
        if (newChild != null)
          element1.AppendChild((XmlNode) newChild);
      }
      if (oneRecordXml._oneRecordXml_Itogo != null)
      {
        XmlElement newChild = this.Xml_OneRecordXml(xmlDocument, oneRecordXml._oneRecordXml_Itogo, "oneRecordXml_Itogo");
        if (newChild != null)
          element1.AppendChild((XmlNode) newChild);
      }
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element1, "NAME", name_oneRecordXml);
      return element1;
    }

    public XmlElement Xml_OneFieldXml(XmlDocument xmlDocument, Vedomost_VB.OneFieldXml oneFielddXml)
    {
      if (xmlDocument == null || oneFielddXml == null || string.IsNullOrEmpty(oneFielddXml._nameToXml) || string.IsNullOrEmpty(oneFielddXml._nameToFile))
        return (XmlElement) null;
      XmlElement element = xmlDocument.CreateElement(string.Empty, "OneFieldXml", string.Empty);
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "nameToXml", oneFielddXml._nameToXml);
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "nameToFile", oneFielddXml._nameToFile);
      Vedomost_VB_Static.InsertToXml_Text(xmlDocument, element, "typeDataToXml", oneFielddXml._typeDataToXml.ToString());
      return element;
    }
  }

  /// <summary> Соответсвие имен для связи с XML одного типа записей </summary>
  public class OneRecordXml
  {
    public string _nameTypeRec;
    public string _tableRowId;
    public List<Vedomost_VB.OneFieldXml> _listOneFieldXml;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Vtor;
    public Vedomost_VB.OneRecordXml _oneRecordXml_Itogo;
  }

  /// <summary> Соответсвие имен для связи с XML одного поля </summary>
  public class OneFieldXml
  {
    public Vedomost_VB.TypeDataToXml _typeDataToXml;
    public string _nameToFile;
    public string _nameToXml;
  }

  /// <summary> Вывод в ОДНУ ячейку таблицы </summary>
  public class OneGrafaToPrint
  {
    public string _cell_ID;
    public List<Vedomost_VB.OneDataFieldToPrint> _listOneDataFieldToPrint;
  }

  /// <summary> Описание ОДНОГО поля для вывода </summary>
  public class OneDataFieldToPrint
  {
    public string _symbolRazd;
    public Vedomost_VB.TypeField _typeField;
    public int _objectType;
    public Vedomost_VB.TypeFieldVedRec _typeFieldVedRec;
    public Vedomost_VB.TypeFieldVedPasport _typeFieldVedPasport;
  }

  /// <summary> Описание одного специфичного атрибута ведомостей </summary>
  public class OneAttribVedRec
  {
    public Vedomost_VB.TypeFieldVedRec _typeFieldVedRec;
    public string _name;
  }

  /// <summary> Описание одного специфичного атрибута ведомостей </summary>
  public class OneAttribVedPasport
  {
    public Vedomost_VB.TypeFieldVedPasport _typeFieldVedPasport;
    public string _name;
  }

  /// <summary> Описание ОДНОГО поля Avs6 для вывода </summary>
  public class OneDataField_Avs6_To_Ips
  {
    public string _symbolRazd;
    public int _objectType;
  }

  /// <summary> Вывод в ОДНУ ячейку шаблона </summary>
  public class OneGrafa_Avs6_To_Ips
  {
    public string _cell_ID;
    public List<Vedomost_VB.OneDataField_Avs6_To_Ips> _listOneDataField_Avs6_To_Ips;
  }

  /// <summary> Вывод одной записи AVS6 </summary>
  public class OneRecord_Avs6_To_Ips
  {
    public string _nameTypeRec;
    public char _recordType_Avs6;
    public string _parentId;
    public string _tableRowId;
    public bool _isVtorOblast;
    public string _tableVtorOblastId;
    public List<Vedomost_VB.OneGrafa_Avs6_To_Ips> _listOneGrafa_Avs6_To_Ips;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Vtor;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Itogo;
  }

  public class OneRazdel_Avs6_To_Ips
  {
    public int _razdelVed;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Info;
  }

  /// <summary> Настройки вывода AVS6 ведомости в шаблон </summary>
  public class Algorithm_Avs6_To_Ips
  {
    public string name;
    public string _tableName = "Главная таблица";
    public List<Vedomost_VB.OneRazdel_Avs6_To_Ips> _list_OneRazdel_Avs6_To_Ips;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Info;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Included;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_TitleIncluded;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_TitleVar;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_TitleIsp;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Title;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_TitlePodSection;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Remark;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_RemarkShort;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Pasport;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Empty;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_TitlePart;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Additional1;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Additional2;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Additional3;
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs6_To_Ips_Additional4;

    public XmlElement Xml_OneRecord_Avs6_To_Ips(
      XmlDocument xmlDocument,
      Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs6_To_Ips)
    {
      if (oneRecord_Avs6_To_Ips == null)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, "ONERECORD_Avs6_To_Ips", string.Empty);
      if (oneRecord_Avs6_To_Ips._nameTypeRec != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("NAME");
        attribute.Value = oneRecord_Avs6_To_Ips._nameTypeRec.ToString();
        element1.Attributes.Append(attribute);
      }
      if (oneRecord_Avs6_To_Ips._recordType_Avs6 != char.MinValue)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("RECORDTYPE_AVS6");
        attribute.Value = oneRecord_Avs6_To_Ips._recordType_Avs6.ToString();
        element1.Attributes.Append(attribute);
      }
      if (!string.IsNullOrEmpty(oneRecord_Avs6_To_Ips._parentId))
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("PARENTID");
        attribute.Value = oneRecord_Avs6_To_Ips._parentId.ToString();
        element1.Attributes.Append(attribute);
      }
      if (oneRecord_Avs6_To_Ips._tableRowId != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("TABLEROWID");
        attribute.Value = oneRecord_Avs6_To_Ips._tableRowId.ToString();
        element1.Attributes.Append(attribute);
      }
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("ISVTOROBLAST");
      attribute1.Value = oneRecord_Avs6_To_Ips._isVtorOblast.ToString();
      element1.Attributes.Append(attribute1);
      if (!string.IsNullOrEmpty(oneRecord_Avs6_To_Ips._tableVtorOblastId))
      {
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("TABLEVTOROBLASTID");
        attribute2.Value = oneRecord_Avs6_To_Ips._tableVtorOblastId.ToString();
        element1.Attributes.Append(attribute2);
      }
      if (oneRecord_Avs6_To_Ips._listOneGrafa_Avs6_To_Ips != null)
      {
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "LISTONEGRAFA_Avs6_To_Ips", string.Empty);
        for (int index = 0; index < oneRecord_Avs6_To_Ips._listOneGrafa_Avs6_To_Ips.Count; ++index)
        {
          Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIp = oneRecord_Avs6_To_Ips._listOneGrafa_Avs6_To_Ips[index];
          XmlElement ips = this.Xml_OneGrafa_Avs6_To_Ips(xmlDocument, oneGrafaAvs6ToIp);
          if (ips != null)
            element2.AppendChild((XmlNode) ips);
        }
        element1.AppendChild((XmlNode) element2);
      }
      if (oneRecord_Avs6_To_Ips._isVtorOblast)
      {
        if (oneRecord_Avs6_To_Ips._oneRecord_Avs6_To_Ips_Vtor != null)
        {
          XmlElement ips = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, oneRecord_Avs6_To_Ips._oneRecord_Avs6_To_Ips_Vtor);
          if (ips != null)
            element1.AppendChild((XmlNode) ips);
        }
        if (oneRecord_Avs6_To_Ips._oneRecord_Avs6_To_Ips_Itogo != null)
        {
          XmlElement ips = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, oneRecord_Avs6_To_Ips._oneRecord_Avs6_To_Ips_Itogo);
          if (ips != null)
            element1.AppendChild((XmlNode) ips);
        }
      }
      return element1;
    }

    public XmlElement Xml_OneGrafa_Avs6_To_Ips(
      XmlDocument xmlDocument,
      Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafa_Avs6_To_Ips)
    {
      if (oneGrafa_Avs6_To_Ips == null)
        return (XmlElement) null;
      if (oneGrafa_Avs6_To_Ips._listOneDataField_Avs6_To_Ips == null)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, nameof (oneGrafa_Avs6_To_Ips), string.Empty);
      XmlAttribute attribute = xmlDocument.CreateAttribute("cellNumber");
      attribute.Value = oneGrafa_Avs6_To_Ips._cell_ID.ToString();
      element1.Attributes.Append(attribute);
      XmlElement element2 = xmlDocument.CreateElement(string.Empty, "listOneDataField_Avs6_To_Ips", string.Empty);
      for (int index = 0; index < oneGrafa_Avs6_To_Ips._listOneDataField_Avs6_To_Ips.Count; ++index)
      {
        Vedomost_VB.OneDataField_Avs6_To_Ips dataFieldAvs6ToIp = oneGrafa_Avs6_To_Ips._listOneDataField_Avs6_To_Ips[index];
        XmlElement ips = this.Xml_OneDataField_Avs6_To_Ips(xmlDocument, dataFieldAvs6ToIp);
        element2.AppendChild((XmlNode) ips);
      }
      element1.AppendChild((XmlNode) element2);
      return element1;
    }

    public XmlElement Xml_OneDataField_Avs6_To_Ips(
      XmlDocument xmlDocument,
      Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs6_To_Ips)
    {
      if (oneDataField_Avs6_To_Ips == null)
        return (XmlElement) null;
      XmlElement element = xmlDocument.CreateElement(string.Empty, nameof (oneDataField_Avs6_To_Ips), string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("symbolRazd");
      attribute1.Value = oneDataField_Avs6_To_Ips._symbolRazd.ToString();
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("objectType");
      attribute2.Value = oneDataField_Avs6_To_Ips._objectType.ToString();
      element.Attributes.Append(attribute2);
      return element;
    }

    public XmlElement Xml_Algorithm_Avs6_To_Ips(XmlDocument xmlDocument, string name)
    {
      if (xmlDocument == null)
        return (XmlElement) null;
      XmlElement element = xmlDocument.CreateElement(string.Empty, name, string.Empty);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("TABLENAME");
      attribute1.Value = this._tableName;
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("kolGraf");
      element.Attributes.Append(attribute2);
      if (this._oneRecord_Avs6_To_Ips_Info != null)
      {
        XmlElement ips = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Info);
        if (ips != null)
          element.AppendChild((XmlNode) ips);
      }
      if (this._list_OneRazdel_Avs6_To_Ips != null && this._list_OneRazdel_Avs6_To_Ips.Count > 0)
      {
        XmlElement ipsCreate = this.Xml_List_OneRazdel_Avs6_To_Ips_Create(xmlDocument, this._list_OneRazdel_Avs6_To_Ips);
        if (ipsCreate != null)
          element.AppendChild((XmlNode) ipsCreate);
      }
      XmlElement ips1 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_TitleIncluded);
      if (ips1 != null)
        element.AppendChild((XmlNode) ips1);
      XmlElement ips2 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Included);
      if (ips2 != null)
        element.AppendChild((XmlNode) ips2);
      XmlElement ips3 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_TitleVar);
      if (ips3 != null)
        element.AppendChild((XmlNode) ips3);
      XmlElement ips4 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_TitleIsp);
      if (ips4 != null)
        element.AppendChild((XmlNode) ips4);
      XmlElement ips5 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Title);
      if (ips5 != null)
        element.AppendChild((XmlNode) ips5);
      XmlElement ips6 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_TitlePodSection);
      if (ips6 != null)
        element.AppendChild((XmlNode) ips6);
      if (this._oneRecord_Avs6_To_Ips_Remark != null)
      {
        XmlElement ips7 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Remark);
        if (ips7 != null)
          element.AppendChild((XmlNode) ips7);
      }
      if (this._oneRecord_Avs6_To_Ips_RemarkShort != null)
      {
        XmlElement ips8 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_RemarkShort);
        if (ips8 != null)
          element.AppendChild((XmlNode) ips8);
      }
      if (this._oneRecord_Avs6_To_Ips_Pasport != null)
      {
        XmlElement ips9 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Pasport);
        if (ips9 != null)
          element.AppendChild((XmlNode) ips9);
      }
      if (this._oneRecord_Avs6_To_Ips_Empty != null)
      {
        XmlElement ips10 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Empty);
        if (ips10 != null)
          element.AppendChild((XmlNode) ips10);
      }
      if (this._oneRecord_Avs6_To_Ips_Additional1 != null)
      {
        XmlElement ips11 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Additional1);
        if (ips11 != null)
          element.AppendChild((XmlNode) ips11);
      }
      if (this._oneRecord_Avs6_To_Ips_Additional2 != null)
      {
        XmlElement ips12 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Additional2);
        if (ips12 != null)
          element.AppendChild((XmlNode) ips12);
      }
      if (this._oneRecord_Avs6_To_Ips_Additional3 != null)
      {
        XmlElement ips13 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Additional3);
        if (ips13 != null)
          element.AppendChild((XmlNode) ips13);
      }
      if (this._oneRecord_Avs6_To_Ips_Additional4 != null)
      {
        XmlElement ips14 = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, this._oneRecord_Avs6_To_Ips_Additional4);
        if (ips14 != null)
          element.AppendChild((XmlNode) ips14);
      }
      return element;
    }

    private XmlElement Xml_List_OneRazdel_Avs6_To_Ips_Create(
      XmlDocument xmlDocument,
      List<Vedomost_VB.OneRazdel_Avs6_To_Ips> list_oneRazdel_Avs6_To_Ipss)
    {
      if (xmlDocument == null)
        return (XmlElement) null;
      if (list_oneRazdel_Avs6_To_Ipss == null || list_oneRazdel_Avs6_To_Ipss.Count < 1)
        return (XmlElement) null;
      XmlElement element1 = xmlDocument.CreateElement(string.Empty, "List_OneRazdel_Avs6_To_Ips", string.Empty);
      for (int index = 0; index < list_oneRazdel_Avs6_To_Ipss.Count; ++index)
      {
        Vedomost_VB.OneRazdel_Avs6_To_Ips oneRazdelAvs6ToIps = list_oneRazdel_Avs6_To_Ipss[index];
        XmlElement element2 = xmlDocument.CreateElement(string.Empty, "OneRazdel_Avs6_To_Ips", string.Empty);
        XmlAttribute attribute = xmlDocument.CreateAttribute("RazdelVed");
        attribute.Value = oneRazdelAvs6ToIps._razdelVed.ToString();
        element2.Attributes.Append(attribute);
        XmlElement ips = this.Xml_OneRecord_Avs6_To_Ips(xmlDocument, oneRazdelAvs6ToIps._oneRecord_Avs6_To_Ips_Info);
        if (ips != null)
          element2.AppendChild((XmlNode) ips);
        element1.AppendChild((XmlNode) element2);
      }
      return element1;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NewSpecificationFormAdv
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Archives;
using Intermech.Client.Core;
using Intermech.Client.Core.History;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Форма "Создание новой спецификации"</summary>
/// <summary>
/// Форма, позволяющая выполнять создание новых спецификаций
/// </summary>
public class NewSpecificationFormAdv : Form
{
  /// <summary>Открывать ли в редакторе созданную спецификацию</summary>
  internal static bool openInEditor = true;
  /// <summary>
  /// Категория "Навигатора" для корня дерева классификаторов (Guid)
  /// </summary>
  internal static readonly Guid RootCategoryGuid = new Guid("{7B49A8BD-2293-41BD-9F66-0BFD41148D7A}");
  /// <summary>
  /// Категория "Навигатора" для корня дерева классификаторов
  /// </summary>
  internal static int RootCategoryID = -1;
  /// <summary>Параметры, с которыми работает форма</summary>
  protected SpecificationCreationParams _formParams;
  /// <summary>Требуется ли подавление обработки событий</summary>
  protected bool _supressEvents;
  /// <summary>Сервис значков для типов и категорий</summary>
  protected ICategoryTypeIconService _objtypesIcons;
  /// <summary>Кэш графических элементов "Навигатора"</summary>
  protected INavGraphicsCache _navGraphicsCache;
  /// <summary>Служба по работе с исполнениями</summary>
  protected IArticleService _artService;
  /// <summary>Служба по работе со спецификациями (со стороны PDM)</summary>
  protected IPDMSpecificationsService _specServices;
  /// <summary>
  /// Корневой элемент иерархии типов специфицируемых объектов
  /// </summary>
  internal SpecArticleType _rootItem;
  /// <summary>Тип специфицируемого объекта</summary>
  internal SpecArticleType _productItem;
  /// <summary>Первый проход по дереву типов</summary>
  protected bool _firstPass;
  private List<AVSDocumentTypeSettings> availableDocTypes;
  public static Guid defaultDocType = Guid.Empty;
  /// <summary>Контейнер компонентов</summary>
  private IContainer components;
  private Panel panelBottom;
  private Panel panelControls;
  private CheckBox cb_OpenInEditor;
  private ButtonEdit edit_Name;
  private Label labelName;
  private ButtonEdit edit_Designation;
  private Label labelDesignation;
  private Panel panelTree;
  private Intermech.VirtualTreeView.VirtualTreeView treeArticleTypes;
  private Column columnObjectType;
  private Button btnCancel;
  private Button btnApply;
  private Bevel bevel;
  private ErrorProvider errorProvider;
  private Label label1;
  private System.Windows.Forms.ComboBox avsDocType;
  private Label label_FileScan;
  private CheckBox cb_isScanSpecification;
  private ButtonEdit edit_fileScan;
  private Label label3;
  private ButtonEdit edit_Archive;

  /// <summary>Создать пустой экземпляр формы</summary>
  public NewSpecificationFormAdv()
    : this((SpecificationCreationParams) null)
  {
  }

  /// <summary>Создать экземпляр формы, задать параметры</summary>
  /// <param name="formParams">Параметры, с которыми работает форма</param>
  public NewSpecificationFormAdv(SpecificationCreationParams formParams)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1504);
    AVSPlugin.AllocateAVSLicense();
    NewSpecificationFormAdv.InitCategory();
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    FormStorage.LoadLayout((Control) this);
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._artService = ServicesManager.GetService(typeof (IArticleService)) as IArticleService;
    this._specServices = ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService;
    if (this._specServices == null || this._artService == null)
      throw new Exception("Не загружен модуль PDM, необходимый для работы AVS");
    this._formParams = formParams;
    this.cb_OpenInEditor.Checked = NewSpecificationFormAdv.openInEditor;
    this.Icon = this.GetObjTypeIconOriginalSize(MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545"));
    if (this._formParams == null || this._formParams.SpecPrototypeID == -1L || this._formParams.SpecPrototypeID == 0L)
      this.Text = "Создание новой спецификации";
    else if (this._formParams.Mode == SpecificationCreationMode.CreateVersion)
      this.Text = "Создание новой версии спецификации";
    else
      this.Text = "Создание новой спецификации по прототипу";
    this.availableDocTypes = new List<AVSDocumentTypeSettings>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Guid templateGuid;
      if (AVSDocumentsSettings.Instance.GetTemplate(AVSDocumentType.Specification, new AVSDocumentForm?(AVSDocumentForm.Single), out templateGuid, sessionKeeper.Session, false) != -1L)
        this.availableDocTypes.Add(AVSDocumentsSettings.Instance.FindAVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_Specification));
      if (AVSDocumentsSettings.Instance.GetTemplate(AVSDocumentType.AutoIndustrySpecification, new AVSDocumentForm?(AVSDocumentForm.Single), out templateGuid, sessionKeeper.Session, false) != -1L)
        this.availableDocTypes.Add(AVSDocumentsSettings.Instance.FindAVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification));
      foreach (AVSDocumentTypeSettings avsDocumentType in AVSDocumentsSettings.Instance.AvsDocumentTypes)
      {
        if (avsDocumentType.AVSDocType == AVSDocumentType.UserSpecification && AVSDocumentsSettings.Instance.GetTemplate(avsDocumentType.TypeGuid, new AVSDocumentForm?(AVSDocumentForm.Single), out templateGuid, sessionKeeper.Session, false) != -1L)
          this.availableDocTypes.Add(avsDocumentType);
      }
    }
    this.avsDocType.Items.Clear();
    foreach (object availableDocType in this.availableDocTypes)
      this.avsDocType.Items.Add(availableDocType);
    Guid dbObjectType = formParams.ObjectTypeGuid;
    if (dbObjectType == Guid.Empty)
      dbObjectType = NewSpecificationFormAdv.defaultDocType;
    AVSDocumentTypeSettings documentTypeSettings = (AVSDocumentTypeSettings) null;
    if (dbObjectType != Guid.Empty)
      documentTypeSettings = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(dbObjectType, (AVSDocumentType) AvsConfig.General.DefaultSpecificationType);
    if (documentTypeSettings == null)
      documentTypeSettings = AVSDocumentsSettings.Instance.FindAVSDocumentTypeSettings((AVSDocumentType) AvsConfig.General.DefaultSpecificationType);
    this.avsDocType.SelectedItem = (object) documentTypeSettings;
    if (!this.IsDesignerHosted())
    {
      this.LoadData();
      this.UpdateControls();
    }
    else
      this.UpdateControls();
    this.CreateSpecificationBlanckObject();
  }

  protected override void OnHandleCreated(EventArgs e) => base.OnHandleCreated(e);

  /// <summary>можно отображать сканированные спецификации</summary>
  private bool CanShowScanSpecifications => AvsConfig.General.ShowScan;

  /// <summary>Статический метод</summary>
  internal static void InitCategory()
  {
    if (NewSpecificationFormAdv.RootCategoryID != -1)
      return;
    NewSpecificationFormAdv.RootCategoryID = (ServicesManager.GetService(typeof (IGuidMapper)) as IGuidMapper).Register(NewSpecificationFormAdv.RootCategoryGuid);
    (ServicesManager.GetService(typeof (IFactory)) as IFactory).AddNodeType(NewSpecificationFormAdv.RootCategoryID, typeof (ObjectsListNode));
  }

  /// <summary>Вызвать форму "Создание новой спецификации"</summary>
  /// <param name="formParams">Параметры, с которыми работает форма</param>
  /// <returns>Результат вызова формы</returns>
  public static DialogResult Execute(SpecificationCreationParams formParams)
  {
    using (NewSpecificationFormAdv specificationFormAdv = new NewSpecificationFormAdv(formParams))
      return specificationFormAdv.DialogResult == DialogResult.Cancel || formParams.NewSpecID == 0L ? DialogResult.Cancel : specificationFormAdv.ShowDialog();
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Icon GetObjTypeIcon(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    return this._objtypesIcons.IndexOf(4, objTypeID) < 0 ? (Icon) null : ImagesResizeHelper.ResizeIconTo32x16(this._objtypesIcons.GetIcon(4, objTypeID), this.treeArticleTypes.BackColor);
  }

  /// <summary>
  /// Вернуть значок для указанного типа объекта без изменения его размеров
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected Icon GetObjTypeIconOriginalSize(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    return this._objtypesIcons.IndexOf(4, objTypeID) < 0 ? (Icon) null : this._objtypesIcons.GetIcon(4, objTypeID);
  }

  /// <summary>Обновить статус контролов в окне</summary>
  protected void UpdateControls()
  {
    this.treeArticleTypes.Enabled = this._formParams != null && this._formParams.Mode != SpecificationCreationMode.CreateVersion && this._formParams.Mode != SpecificationCreationMode.CreateInclude;
    Row selectedRow = this.treeArticleTypes.SelectedRow;
    if (this.treeArticleTypes.Enabled)
      this._productItem = selectedRow?.Item as SpecArticleType;
    this.edit_Designation.Properties.Buttons[0].Visible = this.treeArticleTypes.Enabled;
    this.edit_Designation.Properties.Buttons[0].Enabled = this._productItem != null && this._productItem.Enabled;
    this.edit_Designation.Properties.Buttons[1].Enabled = true;
    this.edit_Designation.Properties.Buttons[2].Visible = true;
    this.edit_Designation.Properties.Buttons[2].Enabled = true;
    if (this._formParams != null && this._formParams.ClassifyType == ObjectsClassifyType.Obligatory)
    {
      bool flag = this.edit_Designation.Text != "";
      this.edit_Designation.Properties.ReadOnly = !flag;
      this.edit_Designation.Properties.Buttons[0].Enabled &= flag;
      this.edit_Designation.Properties.Buttons[1].Enabled &= flag;
    }
    this.btnApply.Enabled = this._formParams != null && this._productItem != null && this._productItem.Enabled;
    this.btnCancel.Enabled = true;
    this.cb_isScanSpecification.Checked = this._formParams?.ScanFile != null;
    this.edit_fileScan.Visible = this.cb_isScanSpecification.Visible;
    this.edit_fileScan.Enabled = this.cb_isScanSpecification.Checked;
    this.label_FileScan.Visible = this.cb_isScanSpecification.Visible;
    if (this._formParams == null || this._formParams.Mode != SpecificationCreationMode.CreateBySpcTemplate)
      return;
    this.avsDocType.DropDownStyle = ComboBoxStyle.Simple;
    this.avsDocType.FlatStyle = FlatStyle.Flat;
    this.avsDocType.Enabled = false;
  }

  protected override void OnLoad(EventArgs e)
  {
    if (this.CanShowScanSpecifications)
    {
      this.cb_isScanSpecification.Visible = true;
      this.edit_fileScan.Visible = this.cb_isScanSpecification.Visible;
      this.edit_fileScan.Enabled = this.cb_isScanSpecification.Checked;
      this.label_FileScan.Visible = this.cb_isScanSpecification.Visible;
    }
    else if (this.cb_isScanSpecification.Visible)
    {
      this.cb_isScanSpecification.Visible = false;
      this.edit_fileScan.Visible = this.cb_isScanSpecification.Visible;
      this.label_FileScan.Visible = this.cb_isScanSpecification.Visible;
      this.panelControls.Height -= 68;
    }
    base.OnLoad(e);
  }

  /// <summary>Форма закрывается, сохраняем её размер и положение</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void NewSpecificationForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      this.DeleteSpecificationTemplate();
    FormStorage.SaveLayout((Control) this);
    AVSPlugin.ReleaseAVSLicense();
  }

  /// <summary>Изменился флажок "Открывать в редакторе"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cb_OpenInEditor_CheckedChanged(object sender, EventArgs e)
  {
    NewSpecificationFormAdv.openInEditor = this.cb_OpenInEditor.Checked;
  }

  /// <summary>Метод создаёт заготовку спецификации</summary>
  protected void CreateSpecificationBlanckObject()
  {
    if (this._formParams.NewSpecID != 0L)
      return;
    string initValue1 = this.edit_Designation.Text.Trim();
    string initValue2 = this.edit_Name.Text.Trim();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject prototype = this._formParams.SpecPrototypeID == 0L || this._formParams.SpecPrototypeID == -1L ? (IDBObject) null : sessionKeeper.Session.GetObject(this._formParams.SpecPrototypeID, false);
      int objectTypeId1 = this._formParams.ObjectTypeId;
      if (objectTypeId1 == -1)
        objectTypeId1 = MetaDataHelper.GetObjectTypeID(!(this.avsDocType.SelectedItem is AVSDocumentTypeSettings selectedItem1) || selectedItem1.DBObjectTypeList.Count <= 0 ? "cad00133-306c-11d8-b4e9-00304f19f545" : selectedItem1.DBObjectTypeList[0].ToString());
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId1);
      IDBObject dbObject1 = (IDBObject) null;
      if (this._formParams.Mode == SpecificationCreationMode.CreateVersion)
      {
        long[] versionEx = objectCollection.CreateVersionEx(prototype != null ? prototype.ObjectID : 0L);
        if (versionEx != null && versionEx.Length != 0)
        {
          dbObject1 = sessionKeeper.Session.GetObject(versionEx[0]);
          for (int index = 1; index < versionEx.Length; ++index)
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(versionEx[index]);
            if (objectInfo.Empty)
              objectInfo = sessionKeeper.Session.GetObjectInfo(Math.Abs(versionEx[index]));
            int objectTypeId2 = objectInfo.ObjectTypeID;
            if (!objectInfo.Empty)
            {
              if (MetaDataHelper.IsObjectTypeChildOf(objectTypeId2, this._formParams.PrototypeProductType))
                this._formParams.NewSpecArticleIDs.Add(objectInfo.ObjectID);
              else
                this._formParams.NewObjectIDs.Add(objectInfo.ObjectID);
            }
          }
        }
      }
      else
        dbObject1 = objectCollection.Create(prototype);
      this._formParams.SetNewSpecObjectInfo(dbObject1);
      if (this._formParams.NewSpecID == 0L)
        return;
      if (ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service)
        (service as Intermech.Client.Core.ObjectCreator.ObjectCreator).FireObjectCreatorDraftCreatedEvent(dbObject1.ObjectType, this._formParams.NewSpecID, this._formParams.SpecPrototypeID);
      this._formParams.IsBlank = true;
      IDBObject dbObject2 = sessionKeeper.Session.GetObject(this._formParams.NewSpecID);
      object obj = (object) "";
      long objectId = dbObject2.ObjectID;
      IDBAttribute attributeById1 = dbObject2.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeArchive));
      if (attributeById1 != null)
      {
        obj = (object) attributeById1.AsString;
        string name = attributeById1.Name;
      }
      this.edit_Archive.Text = obj.ToString();
      DBObjectHelper.SetDBAttributeValues(dbObject2, new AttributeValues[2]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) initValue1),
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) initValue2)
      });
      if (prototype != null)
        return;
      ImDocument document = (ImDocument) null;
      AVSDocumentTypeSettings selectedItem2 = this.avsDocType.SelectedItem as AVSDocumentTypeSettings;
      Guid templateGuid = Guid.Empty;
      Guid typeGuid = selectedItem2.TypeGuid;
      long template1 = AVSDocumentsSettings.Instance.GetTemplate(typeGuid, new AVSDocumentForm?(AVSDocumentForm.Single), out templateGuid, sessionKeeper.Session, false);
      if (template1 != -1L)
      {
        ImDocument template2 = DocumentEditorPlugin.LoadDocumentFromDBObject(template1);
        if (template2 != null)
          document = new ImDocument(template2, true, true);
      }
      if (this._formParams.ScanFile != null)
      {
        dbObject2.Attributes.AddAttribute(DocIDCache.Attr_DocumentFile, false);
        if (File.Exists(this._formParams.ScanFile))
        {
          FileStream fileStream = new FileStream(this._formParams.ScanFile, FileMode.Open);
          DBObjectHelper.SaveStreamToFileAttribute(dbObject2, Path.GetFileName(this._formParams.ScanFile), DocIDCache.Attr_File, -1, (Stream) fileStream);
          fileStream.Close();
        }
        else
          dbObject2.Attributes.FindByID(DocIDCache.Attr_File)?.ClearValues();
      }
      if (document != null)
      {
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) document, dbObject2);
        document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, selectedItem2.AVSDocType.ToString(), false, false, false);
        document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, selectedItem2.TypeGuid.ToString(), false, false, false);
        string fileName = DocumentEditorPlugin.GenerateDefaultFileNameForDB((ImDocumentData) document) + ".spx";
        DocumentEditorPlugin.SaveImDocumentObjectFile(this._formParams.NewSpecID, document, fileName, -1, true);
      }
      else
      {
        int attributeID = DocIDCache.Attr_File;
        if (this._formParams.ScanFile != null)
          attributeID = DocIDCache.Attr_DocumentFile;
        IBlobWriter attributeById2 = (IBlobWriter) dbObject2.GetAttributeByID(attributeID);
        if (attributeById2 == null)
          return;
        string fileName = initValue1;
        string str = !string.IsNullOrEmpty(fileName) ? ImDocumentData.ReplaceForbiddenSymbols(fileName) : "Document";
        attributeById2.OpenBlob(new BlobInformation(0L, 0L, DateTime.Now, str + ".spx", ArcMethods.ZLibPacked, ""), true);
      }
    }
  }

  /// <summary>Метод удаляет заготовку спецификации</summary>
  protected virtual void DeleteSpecificationTemplate()
  {
    if (this._formParams.NewSpecID == 0L || !this._formParams.IsBlank)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.GetObject(this._formParams.NewSpecID, false)?.Delete(0L);
      if (this._formParams.NewSpecArticleIDs != null && this._formParams.NewSpecArticleIDs.Count > 0)
      {
        foreach (long newSpecArticleId in this._formParams.NewSpecArticleIDs)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(newSpecArticleId, false);
          if (dbObject != null && dbObject.IsCreationMode)
            dbObject.Delete(0L);
        }
      }
      if (this._formParams.NewObjectIDs != null && this._formParams.NewObjectIDs.Count > 0)
      {
        foreach (long newObjectId in this._formParams.NewObjectIDs)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(newObjectId, false);
          if (dbObject != null && dbObject.IsCreationMode)
            dbObject.Delete(0L);
        }
      }
      this._formParams.IsBlank = true;
    }
  }

  /// <summary>Метод завершает создание спецификации</summary>
  protected virtual void BeforeCommitSpecificationBlank()
  {
    if (this._formParams.NewSpecID == 0L || !this._formParams.IsBlank)
      return;
    string initValue1 = this.edit_Designation.Text.Trim();
    string initValue2 = this.edit_Name.Text.Trim();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._formParams.NewSpecID);
      DBObjectHelper.SetDBAttributeValues(dbObject, new AttributeValues[2]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) initValue1),
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) initValue2)
      });
      if (this._formParams.OtherClassificationAttrValues != null && this._formParams.OtherClassificationAttrValues.Count > 0 && dbObject is IDBAVSDocumentObject dbavsDocumentObject)
        dbavsDocumentObject.SetAttributesValues(this._formParams.OtherClassificationAttrValues.ToArray(), false);
      AVSDocumentTypeSettings selectedItem = this.avsDocType.SelectedItem as AVSDocumentTypeSettings;
      Guid templateGuid = Guid.Empty;
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_SpecificationForm);
      AVSDocumentForm? nullable = new AVSDocumentForm?();
      if (attributeById != null)
        nullable = AVSDocument.DecodeSpecificationFormAttrValue(attributeById.AsString);
      if (!nullable.HasValue)
      {
        List<ProductInfo> products = AVSDocument.LoadProductsByGroupID(this._formParams.OldObjectID, sessionKeeper.Session);
        nullable = products == null || products.Count <= 1 ? new AVSDocumentForm?(AVSDocumentForm.Single) : new AVSDocumentForm?(AVSDocument.GetDefaultGroupDocumentForm(selectedItem.AVSDocType, products));
      }
      if (this._formParams.SpecPrototypeID == -1L)
      {
        Guid typeGuid = selectedItem.TypeGuid;
        long template1 = AVSDocumentsSettings.Instance.GetTemplate(typeGuid, new AVSDocumentForm?(nullable.Value), out templateGuid, sessionKeeper.Session, false);
        ImDocument document = (ImDocument) null;
        if (template1 != -1L)
        {
          ImDocument template2 = DocumentEditorPlugin.LoadDocumentFromDBObject(template1);
          if (template2 != null)
            document = new ImDocument(template2, true, true);
        }
        if (this._formParams.ScanFile != null)
        {
          dbObject.Attributes.AddAttribute(DocIDCache.Attr_DocumentFile, false);
          if (File.Exists(this._formParams.ScanFile))
          {
            FileStream fileStream = new FileStream(this._formParams.ScanFile, FileMode.Open);
            DBObjectHelper.SaveStreamToFileAttribute(dbObject, Path.GetFileName(this._formParams.ScanFile), DocIDCache.Attr_File, -1, (Stream) fileStream);
            fileStream.Close();
          }
          else
            dbObject.Attributes.FindByID(DocIDCache.Attr_File)?.ClearValues();
          (dbObject as IDBAVSDocumentObject).SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_ScanDocument, (object) true)
          }, false);
        }
        if (document == null)
          return;
        DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) document, dbObject);
        document.SetAttributeValue(AVSDocument.AVSDocType_DocAttribute, selectedItem.AVSDocType.ToString(), false, false, false);
        document.SetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, selectedItem.TypeGuid.ToString(), false, false, false);
        document.SetAttributeValue(AVSDocument.SpecTemplateGuid_DocAttribute, templateGuid.ToString(), false, false, false);
        string fileName = DocumentEditorPlugin.GenerateDefaultFileNameForDB((ImDocumentData) document) + ".spx";
        DocumentEditorPlugin.SaveImDocumentObjectFile(this._formParams.NewSpecID, document, fileName, -1, true);
      }
      else
      {
        IDBAttribute byId1 = dbObject.Attributes.FindByID(DocIDCache.Attr_DocumentFile);
        if (this._formParams.ScanFile == null)
        {
          if (byId1 == null)
            return;
          if (this._formParams.Mode == SpecificationCreationMode.CreateVersion)
            dbObject.Attributes.FindByID(DocIDCache.Attr_File)?.ClearValues();
          ((IDBAVSDocumentObject) dbObject).SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_ScanDocument, (object) false)
          }, false);
        }
        else
        {
          IDBAttribute byId2 = dbObject.Attributes.FindByID(DocIDCache.Attr_File);
          dbObject.Attributes.AddAttribute(DocIDCache.Attr_DocumentFile, false);
          if (File.Exists(this._formParams.ScanFile))
          {
            FileStream fileStream = new FileStream(this._formParams.ScanFile, FileMode.Open);
            DBObjectHelper.SaveStreamToFileAttribute(dbObject, Path.GetFileName(this._formParams.ScanFile), DocIDCache.Attr_File, -1, (Stream) fileStream);
            fileStream.Close();
          }
          else
            dbObject.Attributes.FindByID(DocIDCache.Attr_File)?.ClearValues();
          if (byId1 != null || byId2 == null)
            return;
          (dbObject as IDBAVSDocumentObject).SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_ScanDocument, (object) true)
          }, false);
        }
      }
    }
  }

  /// <summary>Метод завершает создание спецификации</summary>
  protected virtual void BeforeCommitSpecificationBlank_2()
  {
    if (this._formParams.NewSpecID == 0L || !this._formParams.IsBlank)
      return;
    string str1 = this.edit_Designation.Text.Trim();
    string str2 = this.edit_Name.Text.Trim();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._formParams.NewSpecID);
      dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545")).Value = (object) str1;
      dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")).Value = (object) str2;
      if (this._formParams.RelationTypeIDs == null || this._formParams.RelatedObjectIDs == null || this._formParams.RelationTypeIDs.Length == 0 || this._formParams.RelationTypeIDs.Length != this._formParams.RelatedObjectIDs.Length)
        return;
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this._formParams.RelationTypeIDs[0]);
      for (int index = 0; index < this._formParams.RelationTypeIDs.Length; ++index)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
        });
        relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, this._formParams.NewSpecID);
        long num = -1;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt64(row[0]) == this._formParams.RelatedObjectIDs[index])
          {
            num = Convert.ToInt64(row[1]);
            break;
          }
        }
        if (num == -1L)
        {
          if (relationCollection.RelationTypeID != this._formParams.RelationTypeIDs[index])
            relationCollection.RelationTypeID = this._formParams.RelationTypeIDs[index];
          num = (sessionKeeper.Session.GetRelation(this._formParams.RelatedObjectIDs[index], this._formParams.NewSpecID, true) ?? relationCollection.Create(this._formParams.RelatedObjectIDs[index], this._formParams.NewSpecID, DateTime.Now)).RelationID;
        }
        this._formParams.NewRelations.Add(num);
        this._formParams.NewRelationsProjIDs.Add(this._formParams.RelatedObjectIDs[index]);
        this._formParams.NewRelationsTypeIDs.Add(this._formParams.RelationTypeIDs[index]);
      }
    }
  }

  /// <summary>Заполнить редакторы "Обозначение" и "Наименование" из объекта с указанным идентификатором</summary>
  /// <param name="objectID">Идентификатор версии изделия (исполнения)</param>
  /// <returns>true, если объект был найден и успешно загружен</returns>
  internal virtual bool FillEditors(long objectID)
  {
    this.edit_Designation.Text = string.Empty;
    this.edit_Name.Text = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject == null)
        return false;
      object obj1 = (object) "";
      IDBAttribute attributeById1 = dbObject.GetAttributeByID(AvsIDCache.Attr_Designation);
      if (attributeById1 != null)
        obj1 = (object) attributeById1.AsString;
      this.edit_Designation.Text = obj1.ToString();
      object obj2 = (object) string.Empty;
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(AvsIDCache.Attr_Name);
      if (attributeById2 != null)
        obj2 = (object) attributeById2.AsString;
      this.edit_Name.Text = obj2.ToString();
    }
    return true;
  }

  /// <summary>Создать дерево допустимых родительских типов объектов</summary>
  /// <param name="session"></param>
  /// <param name="objTypeID">Идентификатор родительского типа объекта</param>
  /// <param name="parentItem"></param>
  /// <returns></returns>
  internal virtual SpecArticleType CreateObjectTypesTree(
    IUserSession session,
    int objTypeID,
    SpecArticleType parentItem)
  {
    SpecArticleType parentItem1 = new SpecArticleType(objTypeID, MetaDataHelper.GetObjectTypeName(objTypeID), false, this.GetObjTypeIcon(objTypeID));
    parentItem?.Items.Add(parentItem1);
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objTypeID);
    for (int index = 0; index < objectTypeChildrenId.Count; ++index)
      this.CreateObjectTypesTree(session, objectTypeChildrenId[index], parentItem1);
    IDBRelationsApplicability applicability = session.GetRelationsApplicabilityCollection().GetApplicability(relationTypeId, objectTypeId, objTypeID);
    parentItem1.Enabled = applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled;
    return parentItem1;
  }

  /// <summary>Сгенерировать дерево типов объектов, которые можно связать со спецификацией</summary>
  /// <returns>Дерево типов объектов, которые можно связать со спецификацией</returns>
  internal virtual SpecArticleType CreateAppTypesTree()
  {
    if (this._supressEvents)
      return (SpecArticleType) null;
    try
    {
      this._supressEvents = true;
      SpecArticleType parentItem = new SpecArticleType(-1, string.Empty, false, (Icon) null);
      using (SessionKeeper sessionKeeper1 = new SessionKeeper())
      {
        IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper1.Session.GetRelationsApplicabilityCollection();
        int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
        int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
        int relationType = relationTypeId;
        int objectType = objectTypeId1;
        DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(relationType, objectType, -1);
        if (this._formParams != null)
        {
          if (this._formParams.SpecPrototypeID != 0L && this._formParams.SpecPrototypeID != -1L)
          {
            List<ProductInfo> productInfoList = AVSDocument.LoadProductsForAVSDocument(this._formParams.SpecPrototypeID, (List<int>) null, false, "", sessionKeeper1.Session);
            if (productInfoList.Count > 0)
            {
              this._formParams.PrototypeProductType = productInfoList[0].ObjectType;
              this._productItem = new SpecArticleType(this._formParams.PrototypeProductType, MetaDataHelper.GetObjectTypeName(this._formParams.PrototypeProductType), true, this.GetObjTypeIcon(this._formParams.PrototypeProductType));
            }
            if (productInfoList.Count == 0)
            {
              string str = "";
              using (SessionKeeper sessionKeeper2 = new SessionKeeper())
                str = sessionKeeper2.Session.GetObjectInfo(this._formParams.SpecPrototypeID).Caption;
              throw new Exception($"Для спецификации \"{str}\" не найдено специфицируемое изделие! Невозможно для неё выпустить версию или спецификацию по прототипу.");
            }
            this.FillEditors(this._formParams.SpecPrototypeID);
            for (int index1 = 0; index1 < productInfoList.Count; ++index1)
            {
              int index2 = this._formParams.ProductsIDs.IndexOf(productInfoList[index1].Id);
              if (index2 == -1)
              {
                this._formParams.ProductsIDs.Add(productInfoList[index1].Id);
                index2 = this._formParams.ProductsIDs.Count - 1;
              }
              if (productInfoList[index1].Designation == this.edit_Designation.Text && index2 != 0)
              {
                this._formParams.ProductsIDs.Insert(0, this._formParams.ProductsIDs[index2]);
                this._formParams.ProductsIDs.RemoveAt(index2 + 1);
              }
            }
          }
          else if (this._formParams.ProductsIDs.Count > 0)
          {
            if (this._productItem == null)
            {
              int objectTypeId2 = sessionKeeper1.Session.GetObjectInfo(this._formParams.ProductsIDs[0]).ObjectTypeID;
              this._productItem = new SpecArticleType(objectTypeId2, MetaDataHelper.GetObjectTypeName(objectTypeId2), true, this.GetObjTypeIcon(objectTypeId2));
            }
            this.FillEditors(this._formParams.ProductsIDs[0]);
          }
          else if (this._formParams.RelatedObjectIDs != null && this._formParams.RelatedObjectIDs.Length != 0)
            this.FillEditors(this._formParams.RelatedObjectIDs[0]);
        }
        if (applicabilitiesList == null)
          return parentItem;
        int objectTypeId3 = MetaDataHelper.GetObjectTypeID("cad00583-306c-11d8-b4e9-00304f19f545");
        for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
        {
          int int32 = Convert.ToInt32(applicabilitiesList.Rows[index]["F_INOBJECT_TYPE"]);
          if (!MetaDataHelper.IsObjectTypeChildOf(int32, objectTypeId3))
          {
            SpecArticleType objectTypesTree = this.CreateObjectTypesTree(sessionKeeper1.Session, int32, parentItem);
            if (this._productItem != null && this._productItem.ObjectType == objectTypesTree.ObjectType)
              this._productItem = objectTypesTree;
          }
        }
      }
      parentItem.Items.Sort();
      return parentItem;
    }
    finally
    {
      this._supressEvents = false;
      this.UpdateControls();
    }
  }

  /// <summary>Заполнить контролы формы информацией, полученной в параметрах</summary>
  protected void LoadData()
  {
    this._firstPass = true;
    this._rootItem = this.CreateAppTypesTree();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._formParams.ClassifyType = ObjectsClassifyHelper.GetClassifierType(sessionKeeper.Session, AvsIDCache.ObjType_Specification);
      this.treeArticleTypes.DataSource = (object) this._rootItem;
      if (this._formParams.SpecPrototypeID != 0L && this._formParams.SpecPrototypeID != -1L)
      {
        IDBObject docObject = sessionKeeper.Session.GetObject(this._formParams.SpecPrototypeID, false);
        if (docObject != null)
        {
          IDBAttributeCollection attributes = docObject.Attributes;
          if (attributes.FindByID(DocIDCache.Attr_DocumentFile) != null)
          {
            this._formParams.ScanFile = "";
            this.cb_isScanSpecification.Checked = true;
          }
          IDBAttribute byId1 = attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"));
          this.edit_Designation.Text = byId1 != null ? byId1.AsString : string.Empty;
          IDBAttribute byId2 = attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"));
          this.edit_Name.Text = byId2 != null ? byId2.AsString : string.Empty;
          List<ProductInfo> productInfoList = AVSDocument.LoadProductsForAVSDocument(this._formParams.SpecPrototypeID, (List<int>) null, false, (string) null, sessionKeeper.Session);
          if (productInfoList.Count > 0)
            this._formParams.PrototypeProductType = productInfoList[0].ObjectType;
          string attributeValue = DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, -1, false, false, false)?.GetAttributeValue(AVSDocument.AVSDocTypeGuid_DocAttribute, true);
          if (!string.IsNullOrEmpty(attributeValue))
          {
            Guid avsDocumentTypeGuid = Guid.Parse(attributeValue);
            this.avsDocType.SelectedItem = (object) AVSDocumentsSettings.Instance.FindAVSDocumentTypeSettings(avsDocumentTypeGuid);
          }
        }
      }
      else if (this._formParams.Mode == SpecificationCreationMode.CreateInclude)
      {
        if (this._formParams.RelatedObjectIDs.Length != 0)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._formParams.RelatedObjectIDs[0]);
          this._formParams.PrototypeProductType = dbObject.ObjectType;
          IDBAttributeCollection attributes = dbObject.Attributes;
          IDBAttribute byId3 = attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"));
          this.edit_Designation.Text = byId3 != null ? byId3.AsString : string.Empty;
          IDBAttribute byId4 = attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"));
          this.edit_Name.Text = byId4 != null ? byId4.AsString : string.Empty;
        }
      }
    }
    int objectType = this._formParams.PrototypeProductType;
    if (objectType == -1)
      objectType = MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545");
    this._productItem = this._rootItem.FindItem(objectType);
    if (this._productItem != null)
    {
      this.treeArticleTypes.SelectedRow = this.treeArticleTypes.FindRow((object) this._productItem);
      if (this.treeArticleTypes.SelectedRow != null)
        this.treeArticleTypes.SelectedRow.Expand();
    }
    this._firstPass = false;
  }

  /// <summary>Требуется информация о дочерних узлах</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeArticleTypes_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (this._supressEvents || !(e.Row.Item is SpecArticleType))
      return;
    e.Children = (IList) (e.Row.Item as SpecArticleType).Items;
  }

  /// <summary>Требуется информация о строке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeArticleTypes_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (this._supressEvents || !(e.Row.Item is SpecArticleType))
      return;
    SpecArticleType specArticleType = e.Row.Item as SpecArticleType;
    e.RowData.Icon = specArticleType.Icon;
    e.RowData.IconSize = specArticleType.Icon != null ? specArticleType.Icon.Width : 32 /*0x20*/;
    if (!this._firstPass || this._productItem == null || this._productItem.ObjectType != specArticleType.ObjectType)
      return;
    e.Row.Selected = true;
  }

  /// <summary>Требуется информация о ячейке строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeArticleTypes_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (this._supressEvents || !(e.Row.Item is SpecArticleType))
      return;
    SpecArticleType specArticleType = e.Row.Item as SpecArticleType;
    if (e.Column == this.columnObjectType)
      e.CellData.Value = (object) specArticleType.ObjTypeName;
    if (specArticleType.Enabled)
      return;
    e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, new StyleDelta()
    {
      ForeColor = SystemColors.GrayText
    });
    e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, new StyleDelta()
    {
      ForeColor = SystemColors.GrayText
    });
  }

  /// <summary>Изменилась выделенная строка в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeArticleTypes_SelectionChanged(object sender, EventArgs e)
  {
    if (this._supressEvents || sender is ButtonEdit buttonEdit && (buttonEdit.Name.Equals("edit_Name") || buttonEdit.Name.Equals("edit_Designation")) && this._formParams.Mode == SpecificationCreationMode.CreateVersion)
      return;
    this.errorProvider.Clear();
    this._formParams.OldObjectID = -1L;
    this._formParams.NewSpecArticleIDs.Clear();
    this.UpdateControls();
  }

  /// <summary>Двойной клик в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeArticleTypes_DoubleClick(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (!this.edit_Designation.Properties.Buttons[0].Enabled)
      return;
    this.edit_Designation_ButtonClick((object) this, new ButtonPressedEventArgs(this.edit_Designation.Properties.Buttons[0]));
  }

  /// <summary>Получить список специфицируемых объектов для указанной спецификации</summary>
  /// <param name="specID">Идентификатор версии спецификации</param>
  /// <returns>Список специфицируемых объектов для указанной спецификации</returns>
  public void GetSpecifyingObjects_ID(
    long specID,
    out List<long> objectIdList,
    out List<long> idList)
  {
    objectIdList = new List<long>();
    idList = new List<long>();
    if (specID.IsUndefinedId())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1)
      });
      AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
      relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
      DataTable dataTable1 = relationCollection.EntersInVersion(paramSet, specID);
      if (dataTable1 == null)
        return;
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
      {
        long int64_1 = Convert.ToInt64(dataTable1.Rows[index][0]);
        if (!idList.Contains(int64_1))
          idList.Add(int64_1);
        long int64_2 = Convert.ToInt64(dataTable1.Rows[index][1]);
        if (!objectIdList.Contains(int64_2))
          objectIdList.Add(int64_2);
      }
      dataTable1.Dispose();
      string conditionValue = (string) null;
      IDBObject dbObject = (IDBObject) null;
      if (objectIdList.Count > 0)
      {
        long objectID = objectIdList[0];
        dbObject = sessionKeeper.Session.GetObject(objectID);
        IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_ArticleGroupID);
        if (attributeById != null && attributeById.Value != null && !(attributeById.Value is DBNull))
          conditionValue = attributeById.AsString;
      }
      if (string.IsNullOrEmpty(conditionValue))
        return;
      ColumnDescriptor[] columns = new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(dbObject.ObjectType);
      objectCollection.ShowAllModifications = true;
      paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(AvsIDCache.Attr_ArticleGroupID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, true),
        new ConditionStructure(-9, RelationalOperators.NotEqual, (object) sessionKeeper.Session.IdentHelper.DeletedID, LogicalOperators.AND, 0, false)
      }, columns);
      AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
      DataTable dataTable2 = objectCollection.Select(paramSet);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
      {
        long int64_3 = Convert.ToInt64(row[0]);
        if (!idList.Contains(int64_3))
          idList.Add(int64_3);
        long int64_4 = Convert.ToInt64(row[1]);
        if (!objectIdList.Contains(int64_4))
          objectIdList.Add(int64_4);
      }
      dataTable2.Dispose();
    }
  }

  /// <summary>Назначение обозначения документа по классификатору</summary>
  /// <returns></returns>
  private bool Classify()
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    string objectTypeName = MetaDataHelper.GetObjectTypeName(objectTypeId);
    if (this._productItem == null || this._formParams == null)
      return false;
    long[] classifierForObjType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      ObjectsClassifyType classifierType = ObjectsClassifyHelper.GetClassifierType(sessionKeeper.Session, objectTypeId);
      // ISSUE: variable of a boxed type
      __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
      int objType = objectTypeId;
      classifierForObjType = customService.GetClassifierForObjType((object) sessionGuid, objType);
      if (classifierForObjType != null)
      {
        if (classifierForObjType.Length != 0)
          goto label_9;
      }
      if (classifierType == ObjectsClassifyType.Obligatory)
        throw new Exception($"Не найдено ни одного классификатора для объекта типа \"{objectTypeName}\"");
    }
label_9:
    using (ClassifySelectionForm classifySelectionForm = new ClassifySelectionForm(classifierForObjType))
    {
      if (classifySelectionForm.ShowDialog() != DialogResult.OK)
        return false;
      this._productItem.ClassifierID = (classifySelectionForm.SelectedItems.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    }
    AttributeValues[] classificationAttributes = ClassificationHelper.GetClassificationAttributes(this._productItem.ClassifierID, this._formParams.NewSpecID);
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
    if (classificationAttributes != null)
    {
      this._formParams.OtherClassificationAttrValues = this._formParams.OtherClassificationAttrValues ?? new List<AttributeValues>();
      this._formParams.OtherClassificationAttrValues.Clear();
      for (int index = 0; index < classificationAttributes.Length; ++index)
      {
        AttributeValues attributeValues = classificationAttributes[index];
        if (attributeValues.Values != null && attributeValues.Values.Length != 0 && attributeValues.Values[0] != DBNull.Value)
        {
          if (attributeValues.AttributeID == attributeTypeId1)
            this.edit_Designation.Text = attributeValues.Values[0].ToString();
          else if (attributeValues.AttributeID == attributeTypeId2)
            this.edit_Name.Text = attributeValues.Values[0].ToString();
          else
            this._formParams.OtherClassificationAttrValues.Add(attributeValues);
        }
      }
    }
    this.UpdateControls();
    return true;
  }

  /// <summary>Нажата одна из кнопок в редакторе "Обозначение"</summary>
  /// <param name="sender">Редактор "Обозначение"</param>
  /// <param name="e">Аргументы события</param>
  private void edit_Designation_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    MetaDataHelper.GetObjectTypeName(objectTypeId1);
    int num1 = objectTypeId1;
    if (this._productItem != null)
      num1 = this._productItem.ObjectType;
    if (e.Button == this.edit_Designation.Properties.Buttons[1])
    {
      object ID = (object) num1;
      if (this._formParams.SpecPrototypeID != 0L && this._formParams.SpecPrototypeID != -1L)
        ID = (object) this._formParams.SpecPrototypeID;
      using (ObjectsHistory objectsHistory = new ObjectsHistory(ID, AttributableElements.Object, (object) MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545")))
      {
        objectsHistory.SelectedValue = (object) this.edit_Designation.Text.Trim();
        if (objectsHistory.ShowDialog() == DialogResult.OK)
          this.edit_Designation.Text = (string) objectsHistory.SelectedValue;
      }
      this.UpdateControls();
    }
    if (e.Button == this.edit_Designation.Properties.Buttons[0] && this._productItem != null && this._formParams != null)
    {
      if (this._formParams.Mode == SpecificationCreationMode.CreateVersion)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          List<long> idList;
          this.GetSpecifyingObjects_ID(this._formParams.SpecPrototypeID, out this._formParams.ProductsIDs, out idList);
          if (idList.Count > 0)
          {
            int objectTypeId2 = sessionKeeper.Session.GetObjectInfo(this._formParams.ProductsIDs[0]).ObjectTypeID;
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId2);
            objectCollection.ShowAllModifications = true;
            List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
            for (int index = 0; index < idList.Count; ++index)
            {
              int groupID = idList.Count != 1 ? (index != 0 ? (index != idList.Count - 1 ? 0 : -1) : 1) : 0;
              conditionStructureList.Add(new ConditionStructure(-3, RelationalOperators.Equal, (object) idList[index], idList.Count > 1 ? LogicalOperators.OR : LogicalOperators.AND, groupID, true));
            }
            ColumnDescriptor[] columns = new ColumnDescriptor[2]
            {
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1)
            };
            DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns);
            DataTable dataTable = objectCollection.Select(paramSet);
            List<long> objectIDs = new List<long>();
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64 = Convert.ToInt64(row[0]);
              if (!this._formParams.ProductsIDs.Contains(int64))
                objectIDs.Add(int64);
            }
            if (objectIDs.Count > 0)
            {
              object[] objArray = SelectionWindow.Select("Выбор специфицируемого объекта", "Выберите объект, для которого требуется создать спецификацию", (IDescriptor) new SelectObjectsDescriptor("Изделия", objectIDs), typeof (IDBTypedObjectID), SelectionOptions.HideTree | SelectionOptions.HideViewsToolbar | SelectionOptions.HideViewsGroupingBox | SelectionOptions.SelectObjects | SelectionOptions.DisableObjectListFilter | SelectionOptions.DisableMultiselect | SelectionOptions.ForceRebuildNavTree);
              if (objArray != null)
              {
                if (objArray.Length != 0)
                {
                  this._formParams.OldObjectID = ((IDBTypedObjectID) objArray[0]).ObjectID;
                  this.FillEditors(this._formParams.OldObjectID);
                }
              }
            }
            else
            {
              int num2 = (int) MessageBox.Show("Нет подходящих объектов для выбора");
            }
          }
        }
      }
      else
      {
        long[] numArray = SelectionWindow.SelectObjects("Выбор специфицируемого объекта", "Выберите объект, для которого требуется создать спецификацию", (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(this._productItem.ObjectType), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
        if (numArray != null && numArray.Length != 0 && numArray[0] != -1L)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[0], false);
            if (dbObject != null)
            {
              int objectType = dbObject.ObjectType;
              IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
              relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
              DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
              {
                new ColumnDescriptor((object) -2)
              });
              DataTable dataTable = relationCollection.ConsistFrom(paramSet, numArray[0]);
              bool flag = false;
              if (dataTable.Rows.Count > 0)
              {
                int num3 = (int) MessageBox.Show(sc_886.ssp_avs_887(), "Создание спецификации", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                flag = true;
              }
              if (!flag)
              {
                long checkoutBy = dbObject.CheckoutBy;
                if (checkoutBy != sessionKeeper.Session.UserID && checkoutBy != 0L)
                {
                  int num4 = (int) MessageBox.Show($"Выбранный объект взят на изменение пользователем \"{sessionKeeper.Session.GetObject(checkoutBy).Caption}\".\r\nСоздание спецификации для него невозможно до тех пор, пока для данного объекта не будут завершены изменения.", "Создание спецификации", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
              }
              this._formParams.OldObjectID = dbObject.ObjectID;
              this.FillEditors(this._formParams.OldObjectID);
            }
          }
        }
      }
      this.UpdateControls();
    }
    if (e.Button != this.edit_Designation.Properties.Buttons[2] || this._productItem == null || this._formParams == null)
      return;
    this.Classify();
  }

  /// <summary>Нажата одна из кнопок в редакторе "Наименование"</summary>
  /// <param name="sender">Редактор "Наименование"</param>
  /// <param name="e">Аргументы события</param>
  private void edit_Name_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (e.Button != this.edit_Name.Properties.Buttons[0])
      return;
    using (ObjectsHistory objectsHistory = new ObjectsHistory((object) this._formParams.NewSpecID, AttributableElements.Object, (object) MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")))
    {
      objectsHistory.SelectedValue = (object) this.edit_Name.Text.Trim();
      if (objectsHistory.ShowDialog() == DialogResult.OK)
        this.edit_Name.Text = (string) objectsHistory.SelectedValue;
    }
    this.UpdateControls();
  }

  /// <summary>Попытаться создать спецификацию по параметрам в форме</summary>
  /// <returns>true, если всё успешно создано</returns>
  protected virtual bool TryCreateSpec()
  {
    this.errorProvider.Clear();
    if (this._formParams == null || this._productItem == null)
      return false;
    if (this.cb_isScanSpecification.Checked)
    {
      this._formParams.ScanFile = this.edit_fileScan.Text;
      if (!File.Exists(this._formParams.ScanFile) && IMMessageBox.Show("Вопрос", "Не выбран файл сканированного документа. Продолжить?", MessageBoxButtons.YesNo) == DialogResult.No)
        return false;
    }
    else
      this._formParams.ScanFile = (string) null;
    this._formParams.openInEditor = this.cb_OpenInEditor.Checked;
    if (this._formParams.OldObjectID == 0L)
      this._formParams.OldObjectID = -1L;
    this._formParams.NewObjects.Clear();
    this._formParams.NewRelations.Clear();
    this._formParams.NewRelationsProjIDs.Clear();
    this._formParams.NewRelationsTypeIDs.Clear();
    string str1 = this.edit_Designation.Text.Trim();
    string str2 = this.edit_Name.Text.Trim();
    if (str1 == string.Empty)
    {
      this.errorProvider.SetError((Control) this.labelDesignation, "Не указано обозначение");
      return false;
    }
    long objectID = -1;
    long[] numArray = (long[]) null;
    if (this._formParams.Mode != SpecificationCreationMode.CreateVersion)
    {
      if (this._formParams.OldObjectID == -1L)
      {
        this._formParams.OldObjectID = this._specServices.GetObjectWithDesignation(this._productItem.ObjectType, str1);
        if (this._formParams.OldObjectID == 0L)
          this._formParams.OldObjectID = -1L;
      }
      IDBObject dbObject1 = (IDBObject) null;
      if (this._formParams.OldObjectID != -1L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          dbObject1 = sessionKeeper.Session.GetObject(this._formParams.OldObjectID, false);
          bool flag = true;
          if (dbObject1 != null)
            flag = dbObject1.IsCreationMode;
          if (flag)
          {
            this._formParams.OldObjectID = -1L;
            dbObject1 = (IDBObject) null;
          }
        }
      }
      long num1 = this._specServices.GetObjectSpecification(this._formParams.OldObjectID);
      if (num1 == 0L)
        num1 = -1L;
      switch (this._specServices.GetObjectWithDesignation(AvsIDCache.ObjType_Specification, str1))
      {
        case -1:
        case 0:
          if (this._formParams.OldObjectID != -1L && num1 != -1L)
          {
            this.errorProvider.SetError((Control) this.labelDesignation, $"В базе данных уже существует объект с обозначением \"{str1}\",\nдля которого создана спецификация.");
            int num2 = (int) MessageBox.Show(string.Format(sc_886.ssp_avs_889(), (object) str1), "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
          }
          if (!this._formParams.OldObjectID.IsUndefinedId())
          {
            if (this._formParams.Mode != SpecificationCreationMode.CreateInclude && MessageBox.Show(string.Format(sc_886.ssp_avs_890(), (object) str1), "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
              return false;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              objectID = dbObject1.ObjectID;
              this._formParams.NewSpecArticleIDs.Add(objectID);
              long checkoutBy = dbObject1.CheckoutBy;
              if (checkoutBy == 0L)
              {
                if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout)
                {
                  long objectId = dbObject1.ObjectID;
                  IDBObject dbObject2 = dbObject1.CheckOut();
                  (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
                  {
                    objectId
                  }, (IList<long>) new long[1]
                  {
                    dbObject2.ObjectID
                  }));
                  objectID = dbObject2.ObjectID;
                }
                else
                {
                  if (dbObject1.ObjectModifyMode == ObjectModifyModes.CreateVersion)
                  {
                    this.errorProvider.SetError((Control) this.labelDesignation, $"Для изменения существующего объекта с обозначением \"{str1}\" требуется выпускать новую версию.");
                    int num3 = (int) MessageBox.Show($"Для изменения существующего объекта с обозначением \"{str1}\" требуется выпускать новую версию.", "Создание спецификации", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                  }
                  if (dbObject1.ObjectModifyMode == ObjectModifyModes.CantModify)
                  {
                    this.errorProvider.SetError((Control) this.labelDesignation, $"Существующий объект с обозначением \"{str1}\" нельзя модифицировать.");
                    int num4 = (int) MessageBox.Show($"Существующий объект с обозначением \"{str1}\" нельзя модифицировать.", "Создание спецификации", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                  }
                }
              }
              else if (checkoutBy != sessionKeeper.Session.UserID)
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(checkoutBy);
                this.errorProvider.SetError((Control) this.labelDesignation, $"Объект с обозначением \"{str1}\" уже присутствует в базе данных и он взят на редактирование пользователем \"{objectInfo.Caption}\".");
                int num5 = (int) MessageBox.Show($"Объект с обозначением \"{str1}\" уже присутствует в базе данных и он взят на редактирование пользователем \"{objectInfo.Caption}\".", "Создание спецификации", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
              }
            }
          }
          if (objectID == -1L && this._formParams.ProductsIDs.Count > 1)
          {
            switch (this._specServices.CreateArticlesForm(this._formParams.ProductsIDs[0], this._formParams.NewSpecArticleIDs, str1, str2))
            {
              case DialogResult.OK:
              case DialogResult.Abort:
                break;
              default:
                return false;
            }
          }
          else
            break;
          break;
        default:
          this.errorProvider.SetError((Control) this.labelDesignation, $"В базе данных уже существует спецификация с обозначением \"{str1}\"");
          int num6 = (int) MessageBox.Show(string.Format(sc_886.ssp_avs_888(), (object) str1), "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return false;
      }
    }
    else
    {
      List<long> specifyingObjects = this._specServices.GetSpecifyingObjects(this._formParams.SpecPrototypeID);
      for (int index = 0; index < specifyingObjects.Count; ++index)
      {
        if (!this._formParams.ProductsIDs.Contains(specifyingObjects[index]))
          this._formParams.ProductsIDs.Add(specifyingObjects[index]);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.CreateSpecificationBlanckObject();
      this.BeforeCommitSpecificationBlank();
      this.BeforeCommitSpecificationBlank_2();
      IDBObject dbObj = (IDBObject) null;
      string str3 = (string) null;
      if (this._formParams.SpecPrototypeID != -1L)
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this._formParams.SpecPrototypeID).GetAttributeByID(AvsIDCache.Attr_Designation);
        if (attributeById != null)
          str3 = attributeById.AsString;
      }
      if (objectID == -1L && this._formParams.NewSpecArticleIDs.Count > 0)
        objectID = this._formParams.NewSpecArticleIDs[0];
      if (objectID == -1L && this._formParams.NewSpecArticleIDs.Count == 0)
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this._productItem.ObjectType);
        if (this._formParams.Mode != SpecificationCreationMode.CreateVersion)
        {
          dbObj = this._formParams.ProductsIDs.Count > 0 ? objectCollection.Create(this._formParams.ProductsIDs[0]) : objectCollection.Create();
          if (dbObj != null && dbObj.ObjectModifyMode == ObjectModifyModes.Checkout && dbObj.CheckoutBy == 0L)
          {
            long objectId = dbObj.ObjectID;
            dbObj = dbObj.CheckOut();
          }
        }
        else
        {
          if (this._formParams.ProductsIDs.Count > 0)
          {
            numArray = objectCollection.CreateVersionEx(this._formParams.ProductsIDs[0]);
            if (numArray != null && numArray.Length != 0)
              dbObj = sessionKeeper.Session.GetObject(numArray[0]);
          }
          else
            dbObj = objectCollection.Create();
          if (dbObj != null && dbObj.ObjectModifyMode == ObjectModifyModes.Checkout && dbObj.CheckoutBy == 0L)
          {
            long objectId = dbObj.ObjectID;
            dbObj = dbObj.CheckOut();
          }
        }
        if (dbObj != null)
        {
          DBObjectHelper.SetDBAttributeValues(dbObj, new AttributeValues[2]
          {
            new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) str1),
            new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) str2)
          });
          objectID = dbObj.ObjectID;
          this._formParams.NewSpecArticleIDs.Insert(0, objectID);
          if (numArray != null)
          {
            int objectTypeId = sessionKeeper.Session.GetObjectInfo(objectID).ObjectTypeID;
            for (int index = 0; index < numArray.Length; ++index)
            {
              if (numArray[index] != objectID && sessionKeeper.Session.GetObjectInfo(numArray[index]).ObjectTypeID == objectTypeId)
                this._formParams.NewSpecArticleIDs.Add(numArray[index]);
              else
                this._formParams.NewObjectIDs.Add(numArray[index]);
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < this._formParams.NewSpecArticleIDs.Count; ++index)
        {
          dbObj = sessionKeeper.Session.GetObject(this._formParams.NewSpecArticleIDs[index]);
          if (dbObj != null)
          {
            if (dbObj.ObjectModifyMode == ObjectModifyModes.Checkout && dbObj.CheckoutBy == 0L)
            {
              long objectId = dbObj.ObjectID;
              dbObj = dbObj.CheckOut();
              (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
              {
                objectId
              }, (IList<long>) new long[1]{ dbObj.ObjectID }));
            }
            string initValue = "";
            IDBAttribute attributeById = dbObj.GetAttributeByID(AvsIDCache.Attr_Designation);
            if (attributeById != null)
              initValue = attributeById.AsString;
            if (!initValue.Contains(str1))
            {
              if (!string.IsNullOrEmpty(str3) && initValue.IndexOf(str3) == 0)
                initValue = str1 + initValue.Remove(0, str3.Length);
              else if (str1 != str3)
                initValue = str1;
            }
            try
            {
              DBObjectHelper.SetDBAttributeValues(dbObj, new AttributeValues[2]
              {
                new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) initValue),
                new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) str2)
              });
            }
            catch (Exception ex)
            {
              if (this._formParams.OldObjectID != -1L)
              {
                this._formParams.NewSpecArticleIDs.Remove(this._formParams.OldObjectID);
                this._formParams.OldObjectID = -1L;
                throw;
              }
            }
          }
        }
      }
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
      relationCollection.ObjectTypeID = AvsIDCache.ObjType_Specification;
      for (int index = 0; index < this._formParams.NewSpecArticleIDs.Count; ++index)
      {
        long id = sessionKeeper.Session.GetObjectInfo(this._formParams.NewSpecID).ID;
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._formParams.NewSpecArticleIDs[index], id, relationCollection.RelationTypeID, false);
        if (relation == null)
        {
          long projectID = this._formParams.NewSpecArticleIDs[index];
          if (projectID > 0L)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this._formParams.NewSpecArticleIDs[index]);
            if (dbObject.CheckoutBy != 0L && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
              projectID = -projectID;
          }
          IDBRelation dbRelation = relationCollection.Create(projectID, this._formParams.NewSpecID, new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(this._formParams.NewSpecID))
          });
          this._formParams.NewRelations.Add(dbRelation.RelationID);
          this._formParams.NewRelationsProjIDs.Add(dbRelation.ProjID);
          this._formParams.NewRelationsTypeIDs.Add(dbRelation.RelationType);
        }
        else
          relation.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(this._formParams.NewSpecID))
          });
      }
      IDBObject dbObject3 = sessionKeeper.Session.GetObject(this._formParams.NewSpecID);
      if (dbObject3.IsCreationMode)
        ServicesManager.GetService<IAVSClientService>(false)?.OnBeforeCommitCreationAVSDocument(new BeforeCommitCreationAVSDocumentEventArgs(dbObject3, this._formParams.SpecPrototypeID, new List<long>((IEnumerable<long>) this._formParams.NewSpecArticleIDs), new List<long>((IEnumerable<long>) this._formParams.NewObjects)));
      if (dbObj != null)
      {
        objectID = dbObj.ObjectID;
        if (dbObj.IsCreationMode)
        {
          if (dbObj is IDBArticle dbArticle)
            dbArticle.KeepRelationWithSpecification = true;
          dbObj.CommitCreation(false, true);
          this._formParams.IsBlank = false;
          this._formParams.NewObjects.Add(dbObj.ObjectID);
          if (!this._formParams.NewSpecArticleIDs.Contains(objectID))
            this._formParams.NewSpecArticleIDs.Insert(0, objectID);
        }
      }
      if (this._formParams.NewSpecArticleIDs.Count > 0)
      {
        for (int index = 0; index < this._formParams.NewSpecArticleIDs.Count; ++index)
        {
          if (objectID != this._formParams.NewSpecArticleIDs[index])
          {
            dbObj = sessionKeeper.Session.GetObject(this._formParams.NewSpecArticleIDs[index], false);
            if (dbObj != null && dbObj.IsCreationMode)
            {
              if (dbObj is IDBArticle dbArticle)
                dbArticle.KeepRelationWithSpecification = true;
              dbObj.CommitCreation(false, true);
              this._formParams.NewObjects.Add(dbObj.ObjectID);
            }
          }
        }
      }
      if (dbObject3.IsCreationMode)
      {
        dbObject3.CommitCreation(false, true);
        this._formParams.SetNewSpecObjectInfo(dbObject3);
      }
      ClassificationHelper.Classification(this._productItem.ClassifierID, dbObject3.ObjectID);
      if (this._formParams.NewObjectIDs != null)
      {
        for (int index = 0; index < this._formParams.NewObjectIDs.Count; ++index)
        {
          if (objectID != this._formParams.NewObjectIDs[index])
          {
            dbObj = sessionKeeper.Session.GetObjectActual(this._formParams.NewObjectIDs[index], true);
            if (dbObj.IsCreationMode)
            {
              if (dbObj is IDBArticle dbArticle)
                dbArticle.KeepRelationWithSpecification = true;
              dbObj.CommitCreation(false, true);
              this._formParams.NewObjects.Add(dbObj.ObjectID);
              if (!this._formParams.NewSpecArticleIDs.Contains(dbObj.ObjectID))
                this._formParams.NewSpecArticleIDs.Insert(0, dbObj.ObjectID);
            }
          }
        }
      }
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) this._formParams.NewRelations, (IList<long>) this._formParams.NewRelationsProjIDs, (IList<int>) null, (IList<int>) this._formParams.NewRelationsTypeIDs));
      if (this._formParams.NewSpecArticleIDs.Count > 0)
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) this._formParams.NewSpecArticleIDs));
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", this._formParams.NewSpecID));
      if (dbObj != null)
        RecentObjectsNode.MRUObjects.Add(dbObj.ObjectID, ObjectAction.Create, DateTime.UtcNow);
    }
    return true;
  }

  /// <summary>Нажата клавиша "ОК</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    if (!this.TryCreateSpec())
      return;
    if (this.avsDocType.SelectedIndex != -1)
      NewSpecificationFormAdv.defaultDocType = (this.avsDocType.SelectedItem as AVSDocumentTypeSettings).TypeGuid;
    this.DialogResult = DialogResult.OK;
  }

  private void edit_fileScan_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Multiselect = false;
    if (openFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    this.edit_fileScan.Text = openFileDialog.FileName;
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cb_isScanSpecification.Checked && this._formParams.ScanFile == null)
      this._formParams.ScanFile = string.Empty;
    if (!this.cb_isScanSpecification.Checked && this._formParams.ScanFile != null)
      this._formParams.ScanFile = (string) null;
    this.UpdateControls();
  }

  private void edit_Archive_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeArchive);
    if (e.Button.Kind == ButtonPredefines.Ellipsis)
    {
      ServiceContainer nodesContext = new ServiceContainer();
      nodesContext.AddService(typeof (ViewArchives), (object) new ViewArchives());
      IDescriptor rootDescriptor = (IDescriptor) new HiveDescriptor("Архивы");
      long[] numArray = SelectionWindow.SelectObjects("Выберите архив", string.Empty, rootDescriptor, (System.IServiceProvider) nodesContext, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
      if (numArray == null || numArray.Length == 0)
        return;
      long num = numArray[0];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(this._formParams.NewSpecID)?.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(attributeTypeId, (object) num)
        });
        this.edit_Archive.Text = sessionKeeper.Session.GetObjectInfo(num).Caption;
      }
    }
    else
    {
      if (e.Button.Kind != ButtonPredefines.Delete)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(this._formParams.NewSpecID)?.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(attributeTypeId, (object) null)
        });
        this.edit_Archive.Text = "";
      }
    }
  }

  private void avsDocType_Format(object sender, ListControlConvertEventArgs e)
  {
    e.Value = (object) (e.ListItem as AVSDocumentTypeSettings).TypeName;
  }

  /// <summary>Освободить ресурсы</summary>
  /// <param name="disposing">true, если требуется освободить управляемые ресурсы</param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewSpecificationFormAdv));
    this.panelBottom = new Panel();
    this.bevel = new Bevel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panelControls = new Panel();
    this.label3 = new Label();
    this.edit_Archive = new ButtonEdit();
    this.label_FileScan = new Label();
    this.label1 = new Label();
    this.avsDocType = new System.Windows.Forms.ComboBox();
    this.cb_isScanSpecification = new CheckBox();
    this.cb_OpenInEditor = new CheckBox();
    this.edit_fileScan = new ButtonEdit();
    this.edit_Name = new ButtonEdit();
    this.labelName = new Label();
    this.edit_Designation = new ButtonEdit();
    this.labelDesignation = new Label();
    this.panelTree = new Panel();
    this.treeArticleTypes = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnObjectType = new Column();
    this.errorProvider = new ErrorProvider(this.components);
    this.panelBottom.SuspendLayout();
    this.panelControls.SuspendLayout();
    this.edit_Archive.Properties.BeginInit();
    this.edit_fileScan.Properties.BeginInit();
    this.edit_Name.Properties.BeginInit();
    this.edit_Designation.Properties.BeginInit();
    this.panelTree.SuspendLayout();
    this.treeArticleTypes.BeginInit();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.bevel);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.bevel, "bevel");
    this.bevel.Name = "bevel";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.panelControls.Controls.Add((Control) this.label3);
    this.panelControls.Controls.Add((Control) this.edit_Archive);
    this.panelControls.Controls.Add((Control) this.label_FileScan);
    this.panelControls.Controls.Add((Control) this.label1);
    this.panelControls.Controls.Add((Control) this.avsDocType);
    this.panelControls.Controls.Add((Control) this.cb_isScanSpecification);
    this.panelControls.Controls.Add((Control) this.cb_OpenInEditor);
    this.panelControls.Controls.Add((Control) this.edit_fileScan);
    this.panelControls.Controls.Add((Control) this.edit_Name);
    this.panelControls.Controls.Add((Control) this.labelName);
    this.panelControls.Controls.Add((Control) this.edit_Designation);
    this.panelControls.Controls.Add((Control) this.labelDesignation);
    componentResourceManager.ApplyResources((object) this.panelControls, "panelControls");
    this.panelControls.Name = "panelControls";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.edit_Archive, "edit_Archive");
    this.edit_Archive.Name = "edit_Archive";
    this.edit_Archive.Properties.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "Выбрать архив", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выбрать архив"),
      new EditorButton(ButtonPredefines.Delete, "Очистить", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) null, new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, Color.Brown), "Очистить значение")
    });
    this.edit_Archive.Properties.ReadOnly = true;
    this.edit_Archive.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.edit_Archive.ButtonClick += new ButtonPressedEventHandler(this.edit_Archive_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label_FileScan, "label_FileScan");
    this.label_FileScan.Name = "label_FileScan";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.avsDocType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.avsDocType.FormattingEnabled = true;
    this.avsDocType.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("avsDocType.Items"),
      (object) componentResourceManager.GetString("avsDocType.Items1"),
      (object) componentResourceManager.GetString("avsDocType.Items2")
    });
    componentResourceManager.ApplyResources((object) this.avsDocType, "avsDocType");
    this.avsDocType.Name = "avsDocType";
    this.avsDocType.Format += new ListControlConvertEventHandler(this.avsDocType_Format);
    componentResourceManager.ApplyResources((object) this.cb_isScanSpecification, "cb_isScanSpecification");
    this.cb_isScanSpecification.Name = "cb_isScanSpecification";
    this.cb_isScanSpecification.UseVisualStyleBackColor = true;
    this.cb_isScanSpecification.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cb_OpenInEditor, "cb_OpenInEditor");
    this.cb_OpenInEditor.Checked = true;
    this.cb_OpenInEditor.CheckState = CheckState.Checked;
    this.cb_OpenInEditor.Name = "cb_OpenInEditor";
    this.cb_OpenInEditor.UseVisualStyleBackColor = true;
    this.cb_OpenInEditor.CheckedChanged += new EventHandler(this.cb_OpenInEditor_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.edit_fileScan, "edit_fileScan");
    this.edit_fileScan.Name = "edit_fileScan";
    this.edit_fileScan.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "Выбрать файл", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_fileScan.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выбрать файл")
    });
    this.edit_fileScan.ButtonClick += new ButtonPressedEventHandler(this.edit_fileScan_ButtonClick);
    componentResourceManager.ApplyResources((object) this.edit_Name, "edit_Name");
    this.edit_Name.Name = "edit_Name";
    this.edit_Name.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Name.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений")
    });
    this.edit_Name.ButtonClick += new ButtonPressedEventHandler(this.edit_Name_ButtonClick);
    this.edit_Name.EditValueChanged += new EventHandler(this.treeArticleTypes_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.labelName, "labelName");
    this.labelName.Name = "labelName";
    componentResourceManager.ApplyResources((object) this.edit_Designation, "edit_Designation");
    this.edit_Designation.Name = "edit_Designation";
    this.edit_Designation.Properties.Buttons.AddRange(new EditorButton[3]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Designation.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Выбрать специфицируемый объект"),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Designation.Properties.Buttons1"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений"),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.IM.Utils.HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Designation.Properties.Buttons2"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Классификация")
    });
    this.edit_Designation.ButtonClick += new ButtonPressedEventHandler(this.edit_Designation_ButtonClick);
    this.edit_Designation.EditValueChanged += new EventHandler(this.treeArticleTypes_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.labelDesignation, "labelDesignation");
    this.labelDesignation.Name = "labelDesignation";
    this.panelTree.Controls.Add((Control) this.treeArticleTypes);
    componentResourceManager.ApplyResources((object) this.panelTree, "panelTree");
    this.panelTree.Name = "panelTree";
    this.treeArticleTypes.AllowDrop = true;
    this.treeArticleTypes.AllowIndividualRowResize = false;
    this.treeArticleTypes.AllowMultiSelect = false;
    this.treeArticleTypes.AllowRowResize = false;
    this.treeArticleTypes.AllowUserPinnedColumns = false;
    componentResourceManager.ApplyResources((object) this.treeArticleTypes, "treeArticleTypes");
    this.treeArticleTypes.AutoFitColumns = true;
    this.treeArticleTypes.Columns.Add(this.columnObjectType);
    this.treeArticleTypes.DisableHeaderContextMenu = false;
    this.treeArticleTypes.ImageList = (ImageList) null;
    this.treeArticleTypes.LineStyle = LineStyle.Dot;
    this.treeArticleTypes.MainColumn = this.columnObjectType;
    this.treeArticleTypes.Name = "treeArticleTypes";
    this.treeArticleTypes.SelectBeforeEdit = true;
    this.treeArticleTypes.ShowRootRow = false;
    this.treeArticleTypes.SuppressErrorMessages = true;
    this.treeArticleTypes.GetCellData += new GetCellDataHandler(this.treeArticleTypes_GetCellData);
    this.treeArticleTypes.GetChildren += new GetChildrenHandler(this.treeArticleTypes_GetChildren);
    this.treeArticleTypes.GetRowData += new GetRowDataHandler(this.treeArticleTypes_GetRowData);
    this.treeArticleTypes.SelectionChanged += new EventHandler(this.treeArticleTypes_SelectionChanged);
    this.treeArticleTypes.DoubleClick += new EventHandler(this.treeArticleTypes_DoubleClick);
    this.columnObjectType.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnObjectType, "columnObjectType");
    this.columnObjectType.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnObjectType.HeaderStyle.HorzAlignment");
    this.columnObjectType.Movable = false;
    this.columnObjectType.Name = "columnObjectType";
    this.columnObjectType.Sortable = false;
    this.errorProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.errorProvider, "errorProvider");
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelTree);
    this.Controls.Add((Control) this.panelControls);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NewSpecificationFormAdv);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.NewSpecificationForm_FormClosed);
    this.panelBottom.ResumeLayout(false);
    this.panelControls.ResumeLayout(false);
    this.panelControls.PerformLayout();
    this.edit_Archive.Properties.EndInit();
    this.edit_fileScan.Properties.EndInit();
    this.edit_Name.Properties.EndInit();
    this.edit_Designation.Properties.EndInit();
    this.panelTree.ResumeLayout(false);
    this.treeArticleTypes.EndInit();
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }
}

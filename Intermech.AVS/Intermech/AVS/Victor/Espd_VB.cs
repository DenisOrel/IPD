// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.Espd_VB
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

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
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class Espd_VB
{
  public string _vedomst_Vedomstvo = "";
  public string _nameDoc = "";
  public string _nameProg = "";
  public string _designationDocLU = "";
  public string _nositel_Info = "";
  public long _iDSP;
  public ImDocument documentLU;

  /// <summary> Создать и открыть Лист утверждения на текущую спецификацию ЕСПД </summary>
  /// <param name="open"> ОТКРЫТЬ окно редактора</param>
  /// <returns></returns>
  public long CreateAndOpenLU(bool open)
  {
    if (string.IsNullOrEmpty(this._designationDocLU))
    {
      int num = (int) MessageBox.Show("Нет обозначения программной спецификации", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return 0;
    }
    if (string.IsNullOrEmpty(this._nameDoc))
    {
      int num = (int) MessageBox.Show("Нет наименовния программной спецификации", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return 0;
    }
    this.documentLU = this.GenerateImDocumentLU();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IPDMSpecificationsService service = ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService;
      IDBObject dbObject1 = (IDBObject) null;
      int objTypeDocument = AvsIDCache.ObjType_Document;
      string designationDocLu = this._designationDocLU;
      long objectWithDesignation = service.GetObjectWithDesignation(objTypeDocument, designationDocLu);
      if (!objectWithDesignation.IsUndefinedId())
      {
        dbObject1 = sessionKeeper.Session.GetObject(objectWithDesignation, false);
        if (dbObject1 != null)
        {
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(objectWithDesignation, false);
          if (dbObject2 != null && dbObject2.ObjectModifyMode == ObjectModifyModes.CreateVersion)
          {
            IDBObject version = sessionKeeper.Session.GetObjectCollection(-1).CreateVersion(objectWithDesignation);
            if (version.IsCreationMode)
              version.CommitCreation(true, true);
            long objectId = version.ObjectID;
            dbObject1 = version;
          }
        }
      }
      long objectId1 = dbObject1.ObjectID;
      string caption = dbObject1.Caption;
      this.documentLU.UpdateLayout(false);
      DocumentEditorPlugin.SaveImDocumentObjectFile(objectId1, this.documentLU, caption, -1, true);
      Guid objGuidById = DBHelper.GetObjGuidByID(objectId1);
      if (open)
      {
        DockControl openedDocument = DocumentEditorPlugin.Instance.FindOpenedDocument(objGuidById);
        if (openedDocument != null)
        {
          if (openedDocument is ImDocumentEditorForm documentEditorForm)
            documentEditorForm.AskForSaveBeforeClose = false;
          openedDocument.Close();
        }
        DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(objectId1, false, true, new DocumentWindowCreatorDelegate(VedomostEditorWindow.VedomostEditorWindowCreator));
      }
      return objectId1;
    }
  }

  /// <summary> Создание ЛИСТА УТВЕРЖДЕНИЯ </summary>
  /// <returns></returns>
  public ImDocument GenerateImDocumentLU()
  {
    long num1 = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(Vedomost_VB_Static.GuidTemplateESPDLU, false);
      if (dbObject == null)
      {
        int num2 = (int) MessageBox.Show("Файл шаблона (бланка) не найден", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (ImDocument) null;
      }
      num1 = dbObject.ObjectID;
      if (num1.IsUndefinedId())
      {
        int num3 = (int) MessageBox.Show("Не найден ID шаблона", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (ImDocument) null;
      }
    }
    ImDocument imDocumentLu = new ImDocument(DocumentEditorPlugin.LoadDocumentFromDBObject(num1), true, true);
    imDocumentLu.Name = "Лист утверждения";
    ImDocumentData documentTemplate = imDocumentLu.DocumentTemplate;
    if (!string.IsNullOrEmpty(this._vedomst_Vedomstvo))
      imDocumentLu.SetAttributeValue("_vedomst_Vedomstvo", this._vedomst_Vedomstvo);
    if (!string.IsNullOrEmpty(this._nameDoc))
      imDocumentLu.SetAttributeValue("_nameDoc", this._nameDoc);
    if (!string.IsNullOrEmpty(this._nameProg))
      imDocumentLu.SetAttributeValue("_nameProg", this._nameProg);
    if (!string.IsNullOrEmpty(this._designationDocLU))
      imDocumentLu.SetAttributeValue("_designationDocLU", this._designationDocLU);
    if (!string.IsNullOrEmpty(this._nositel_Info))
      imDocumentLu.SetAttributeValue("_nositel_Info", this._nositel_Info);
    if (!string.IsNullOrEmpty(this._nameProg))
      imDocumentLu.SetAttributeValue("_nameArticle", this._nameProg);
    if (!this._iDSP.IsUndefinedId())
      imDocumentLu.SetAttributeValue("_iDSP", this._iDSP.ToString());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IPDMSpecificationsService service = ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService;
      IDBObject dbObj = (IDBObject) null;
      int objTypeDocument = AvsIDCache.ObjType_Document;
      string designationDocLu = this._designationDocLU;
      long num4 = service.GetObjectWithDesignation(objTypeDocument, designationDocLu);
      if (!num4.IsUndefinedId())
      {
        dbObj = sessionKeeper.Session.GetObject(num4, false);
        if (dbObj != null)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(num4, false);
          if (dbObject != null && dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
          {
            IDBObject version = sessionKeeper.Session.GetObjectCollection(-1).CreateVersion(num4);
            if (version.IsCreationMode)
              version.CommitCreation(true, true);
            long objectId = version.ObjectID;
            dbObj = version;
            num4 = objectId;
          }
        }
      }
      if (dbObj == null)
      {
        dbObj = sessionKeeper.Session.GetObjectCollection(Vedomost_VB_Static.GuidLUESPD).Create();
        dbObj.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_Format, (object) "A4")
        });
      }
      else if (dbObj.ObjectType != AvsIDCache.ObjType_EspdLU)
      {
        dbObj.CheckIn();
        IDBObject dbObject = sessionKeeper.Session.GetObject(Math.Abs(num4));
        dbObject.ObjectType = AvsIDCache.ObjType_EspdLU;
        dbObj = dbObject.CheckOut();
      }
      dbObj.SetAttributesValues(DBObjectHelper.Filter(dbObj, new AttributeValues[4]
      {
        new AttributeValues(AvsIDCache.Attr_NameProg, (object) this._nameProg),
        new AttributeValues(AvsIDCache.Attr_NameDoc, (object) this._nameDoc),
        new AttributeValues(AvsIDCache.Attr_Name, (object) this._nameProg),
        new AttributeValues(AvsIDCache.Attr_Designation, (object) this._designationDocLU)
      }), false, true);
      long objectId1 = dbObj.ObjectID;
      if (this._iDSP >= -1L && !this._iDSP.IsUndefinedId() && sessionKeeper.Session.GetObject(this._iDSP, false) != null && sessionKeeper.Session.GetRelation(this._iDSP, dbObj.ObjectID, true) == null)
        new OneError()
        {
          _message_kurc = "Лист утверждения не смогли включить в спецификацию т.к. спецификация не взята на редактирование"
        }.Message();
      if (dbObj.IsCreationMode)
        dbObj.CommitCreation(true, true);
    }
    return imDocumentLu;
  }
}

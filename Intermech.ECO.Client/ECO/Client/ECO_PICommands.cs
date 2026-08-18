// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECO_PICommands
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.ECO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class ECO_PICommands
{
  public static void ReplaceCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    List<long> longList = new List<long>();
    long num1 = 0;
    string objCapt = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        long num2 = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
        IDBObject piObj = sessionKeeper.Session.GetObject(num2);
        IDBAttribute attributeById1 = piObj.GetAttributeByID(RevHelper.idAttrScannedDoc);
        if (attributeById1 != null && attributeById1.AsBoolean)
        {
          int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_330"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
          return;
        }
        IDBAttribute attributeById2 = piObj.GetAttributeByID(RevHelper.idLinkedContNumber);
        long num4 = attributeById2 == null || attributeById2.Value == DBNull.Value ? num2 : Convert.ToInt64(attributeById2.Value);
        if (num4 != Math.Abs(num2))
        {
          if (num1 == 0L)
            num1 = num4;
          else if (num1 != num4)
          {
            int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_340"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
            return;
          }
        }
        IDBAttribute attributeById3 = piObj.GetAttributeByID(RevHelper.idAttrStampedByII);
        if (attributeById3 != null && attributeById3.Value != DBNull.Value)
        {
          IDBAttribute attributeById4 = piObj.GetAttributeByID(RevHelper.idAttrDesign);
          string str1 = attributeById4 == null || attributeById4.Value == DBNull.Value ? $"[{Convert.ToString(num2)}]" : $"{Convert.ToString(attributeById4.Value)} [{Convert.ToString(num2)}]";
          long int64 = Convert.ToInt64(attributeById3.Value);
          IDBObject dbObject = sessionKeeper.Session.GetObject(int64, false);
          string str2 = $"[{Convert.ToString(int64)}]";
          if (dbObject != null)
          {
            IDBAttribute attributeById5 = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
            if (attributeById5 != null && attributeById5.Value != DBNull.Value)
              str2 = $"{Convert.ToString(attributeById5.Value)} {str2}";
          }
          switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_354"), (object) str1, (object) str2), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.YesNoCancel))
          {
            case DialogResult.Cancel:
              return;
            case DialogResult.No:
              continue;
          }
        }
        long annulingRevision = ECO_PICommands.GetAnnulingRevision(num2);
        if (annulingRevision != 0L)
        {
          switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_366"), (object) num2) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_367"), (object) annulingRevision), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
          {
            case DialogResult.OK:
              continue;
            case DialogResult.Cancel:
              return;
          }
        }
        if (ECO_PICommands.IsLevelForbidden(piObj))
        {
          switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_366"), (object) num2) + LocalizationHolder.rm.GetString("ECO.Client_369"), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
          {
            case DialogResult.OK:
              continue;
            case DialogResult.Cancel:
              return;
          }
        }
        IDBAttribute attributeById6 = piObj.GetAttributeByID(RevHelper.idLinkToAnnuledPI);
        if (attributeById6 != null && attributeById6.Value != null)
        {
          long int64 = Convert.ToInt64(attributeById6.Value);
          if (int64 != 0L)
          {
            switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_366"), (object) num2) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_368"), (object) int64), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
            {
              case DialogResult.OK:
                continue;
              case DialogResult.Cancel:
                return;
            }
          }
        }
        longList.Add(num2);
      }
      if (longList.Count == 0)
        return;
      if (num1 != 0L)
        ECOPlugin.RevObjectCreator.linkedNumber = num1;
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(longList[0]);
      if (!objectInfo.Empty)
        objCapt = objectInfo.Caption;
    }
    ECOPlugin.RevObjectCreator.BlockLinking = true;
    bool Existing;
    Intermech.ECO.Client.ECO eco1;
    try
    {
      eco1 = plugin.CreateECO(RevHelper.idObj_II, ECOGoal.Stamp, objCapt, out Existing);
    }
    finally
    {
      ECOPlugin.RevObjectCreator.BlockLinking = false;
    }
    if (eco1 == null)
      return;
    if (Existing)
    {
      TableData dataOwner;
      for (int dataPositionInFlow = eco1.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
      {
        string attributeValue = (dataOwner.Nodes[dataPositionInFlow] as TableData).GetAttributeValue(Intermech.ECO.Client.ECO.replacedPIAttr, true);
        if (attributeValue != "")
        {
          long int64 = Convert.ToInt64(attributeValue);
          if (longList.Contains(int64) || longList.Contains(-int64))
          {
            int num6 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_341"), (object) int64), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
            return;
          }
        }
      }
    }
    if (num1 == 0L)
      num1 = Math.Abs(eco1.EcoObjectID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService)
        customService.StartTransaction();
      try
      {
        for (int index = 0; index < longList.Count; ++index)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(longList[index]);
          IDBAttribute dbAttribute1 = dbObject.GetAttributeByID(RevHelper.idLinkedContNumber);
          if ((dbAttribute1 == null || dbAttribute1.Value == DBNull.Value ? longList[index] : Convert.ToInt64(dbAttribute1.Value)) != num1)
          {
            if (dbAttribute1 == null)
              dbAttribute1 = dbObject.Attributes.AddAttribute(RevHelper.idLinkedContNumber, true);
            dbAttribute1.AsInteger = num1;
          }
          IDBAttribute dbAttribute2 = dbObject.Attributes.AddAttribute(RevHelper.idAttrStampedByII, false);
          if (dbAttribute2 != null)
            dbAttribute2.AsInteger = Math.Abs(eco1.EcoObjectID);
        }
        customService?.Commit();
      }
      catch
      {
        customService?.Rollback();
        throw;
      }
    }
    ECOEditorForm ecoEditorForm = plugin.CreateECOEditorForm(eco1, false, true, true, false);
    Intermech.ECO.Client.ECO eco2 = ecoEditorForm.ECO;
    eco2.CopyPIAttribs(longList[sc_6342.ssp_eco_6348(2006739355)]);
    for (int index = 0; index < longList.Count; ++index)
    {
      long num7 = longList[index];
      List<IdLinkPair> objRevList = eco2.CopyLinksFrom(num7, true);
      TableElement change = eco2.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
      change.SetAttributeValue(Intermech.ECO.Client.ECO.replacedPIAttr, Convert.ToString(Math.Abs(num7)));
      TableElement child = (change.Template.FindNode(Intermech.ECO.Client.ECO.idSpecText) as TableElement).CloneFromTemplate() as TableElement;
      if (change.AddChildNode((DocumentTreeNode) child, false, false) >= 0)
      {
        TextData templateRecursive = (TextData) child.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecTextFld);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(num7);
          string designationInEco = eco2.GetDocDesignationInECO(dbObject);
          templateRecursive.Text = LocalizationHolder.rm.GetString("ECO.Client_100") + designationInEco + LocalizationHolder.rm.GetString("ECO.Client_101");
        }
        ECOPlugin.RemoveDefaultText(change);
        ((PageElementNode) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo)).ReadOnly = true;
        ((PageElementNode) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign)).ReadOnly = true;
      }
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IECOServer)) is IECOServer customService)
        customService.AssignChangeNumbers(objRevList);
    }
    ecoEditorForm.Document.UpdateLayout(0, true, true);
  }

  public static void ReplaceContentsCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long num1 = (items.GetItemData(sc_6342.ssp_eco_6349(799945983), typeof (IDBObjectID)) as IDBObjectID).Value;
    string objCaption = "";
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject piObj = sessionKeeper.Session.GetObject(num1);
        IDBAttribute attributeById1 = piObj.GetAttributeByID(RevHelper.idAttrScannedDoc);
        if (attributeById1 != null && attributeById1.AsBoolean)
        {
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_330"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
          return;
        }
        long annulingRevision = ECO_PICommands.GetAnnulingRevision(num1);
        if (annulingRevision != 0L)
        {
          int num3 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_366"), (object) num1) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_367"), (object) annulingRevision), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
          return;
        }
        if (ECO_PICommands.IsLevelForbidden(piObj))
        {
          int num4 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_366"), (object) num1) + LocalizationHolder.rm.GetString("ECO.Client_369"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
          return;
        }
        IDBAttribute attributeById2 = piObj.GetAttributeByID(RevHelper.idLinkToAnnuledPI);
        if (attributeById2 != null && attributeById2.Value != null)
        {
          long int64 = Convert.ToInt64(attributeById2.Value);
          if (int64 != 0L)
          {
            int num5 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_366"), (object) num1) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_368"), (object) int64), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
            return;
          }
        }
        IDBAttribute attributeById3 = piObj.GetAttributeByID(RevHelper.idAttrStampedByII);
        if (attributeById3 != null && attributeById3.Value != DBNull.Value)
        {
          IDBAttribute attributeById4 = piObj.GetAttributeByID(RevHelper.idAttrDesign);
          string str1 = attributeById4 == null || attributeById4.Value == DBNull.Value ? $"[{Convert.ToString(num1)}]" : $"{Convert.ToString(attributeById4.Value)} [{Convert.ToString(num1)}]";
          long int64 = Convert.ToInt64(attributeById3.Value);
          IDBObject dbObject = sessionKeeper.Session.GetObject(int64, false);
          string str2 = $"[{Convert.ToString(int64)}]";
          if (dbObject != null)
          {
            IDBAttribute attributeById5 = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
            if (attributeById5 != null && attributeById5.Value != DBNull.Value)
              str2 = $"{Convert.ToString(attributeById5.Value)} {str2}";
          }
          switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_354"), (object) str1, (object) str2), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
          {
            case DialogResult.Cancel:
              return;
          }
        }
        try
        {
          if (piObj.GetAttributeByID(DocIDCache.Attr_File) != null)
            new BlobProcReader(piObj.ObjectID, AttributableElements.Object, DocIDCache.Attr_File, 0, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
          memoryStream.Position = 0L;
          if (memoryStream.Length == 0L)
            throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_102"));
        }
        catch (Exception ex)
        {
          int num6 = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        IDBAttribute attributeById6 = piObj.GetAttributeByID(RevHelper.idLinkedContNumber);
        ECOPlugin.RevObjectCreator.linkedNumber = attributeById6 == null || attributeById6.Value == DBNull.Value ? num1 : Convert.ToInt64(attributeById6.Value);
        objCaption = piObj.GetAttributeByID(DocIDCache.Attr_Designation).Description;
      }
      string str = "";
      using (RevisionWizardForm revisionWizardForm = new RevisionWizardForm(RevHelper.idObj_II, RequireClass.NoRequire, false, (List<long>) null, ECOGoal.Stamp, objCaption, true, false))
      {
        ECOPlugin.BlockECOOpening = true;
        try
        {
          if (revisionWizardForm.ShowDialog() != DialogResult.OK)
            return;
          Intermech.ECO.Client.ECO eco = (Intermech.ECO.Client.ECO) null;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject docObject = sessionKeeper.Session.GetObject(revisionWizardForm.ECOObjectID);
            IDBAttribute byId = docObject.Attributes.FindByID(RevHelper.idAttrDesign);
            if (byId != null)
              str = Convert.ToString(byId.Value);
            long objectId = docObject.ObjectID;
            INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
            service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectId));
            if (docObject.ObjectModifyMode == ObjectModifyModes.Checkout)
            {
              docObject = docObject.CheckOut();
              service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
              {
                objectId
              }, (IList<long>) new long[1]
              {
                docObject.ObjectID
              }));
            }
            new BlobProcWriter(docObject.Attributes.FindByID(RevHelper.idAttrFile), 0, new BlobInformation(memoryStream.Length, 0L, DateTime.Now, str + ".revx", ArcMethods.ZLibPacked, ""), (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
            revisionWizardForm.DocumentECO = DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, 0, false, true, false);
            eco = new Intermech.ECO.Client.ECO(revisionWizardForm.DocumentECO, docObject.ObjectID, docObject.ObjectGUID, revisionWizardForm.RT);
          }
          if (eco == null)
            return;
          eco.CopyPIAttribs(num1);
          List<IdLinkPair> objRevList = eco.CopyLinksFrom(num1, false);
          ECOEditorForm ecoEditorForm = plugin.CreateECOEditorForm(eco, false, true, true, true);
          try
          {
            if (eco.ecoMainTable.Template.FindNode(Intermech.ECO.Client.ECO.specTextTable) is TableElement node)
            {
              TableElement child = node.CloneFromTemplate() as TableElement;
              eco.ecoMainTable.InsertChildNode(0, (DocumentTreeNode) child, false, false, false, false, true);
              TextData templateRecursive = (TextData) child.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.specTextFld);
              if (templateRecursive != null)
                templateRecursive.Text = string.Format(LocalizationHolder.rm.GetString("ECO.Client_104"), (object) objCaption);
            }
            ((TextData) ecoEditorForm.Document.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idPITerm))?.AssignText("", false, false, false);
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objPI = sessionKeeper.Session.GetObject(num1);
              IDBAttribute dbAttribute = objPI.Attributes.AddAttribute(RevHelper.idAttrStampedByII, false);
              if (dbAttribute != null)
                dbAttribute.AsInteger = Math.Abs(eco.EcoObjectID);
              if (sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer customService)
              {
                List<string> stringList = customService.AssignChangeNumbers(objRevList);
                for (int index = 0; index < objRevList.Count; ++index)
                {
                  PendingLink pendingLink = eco.FindPendingLink(objRevList[index].ObjID);
                  if (pendingLink != null)
                    pendingLink.verStr = stringList[index];
                }
              }
              ecoEditorForm.SwapSignLinks(objPI);
            }
          }
          finally
          {
            ecoEditorForm.UndoManager.LockUndo();
            try
            {
              ecoEditorForm.UpdateAllMultiHeaders();
            }
            finally
            {
              ecoEditorForm.UndoManager.UnlockUndo();
            }
            ecoEditorForm.OnStructureChanged(new StructureChanged_EventArgs((DocumentTreeNode) null));
            ecoEditorForm.Document.UpdateLayout(0, true, true);
          }
        }
        finally
        {
          ECOPlugin.BlockECOOpening = false;
        }
      }
    }
  }

  internal static long GetAnnulingRevision(long piObjId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(RevHelper.idObj_II);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(RevHelper.idLinkToAnnuledPI, RelationalOperators.Equal, (object) Math.Abs(piObjId), LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable1 = objectCollection1.Select(paramSet);
      if (dataTable1 != null && dataTable1.Rows.Count > 0)
        return Convert.ToInt64(dataTable1.Rows[0][0]);
      IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(RevHelper.idObj_PI);
      paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(RevHelper.idLinkToAnnuledPI, RelationalOperators.Equal, (object) Math.Abs(piObjId), LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable2 = objectCollection2.Select(paramSet);
      if (dataTable2 != null)
      {
        if (dataTable2.Rows.Count > 0)
          return Convert.ToInt64(dataTable2.Rows[0][0]);
      }
    }
    return 0;
  }

  internal static bool IsLevelForbidden(IDBObject piObj)
  {
    IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(piObj.LCStep);
    return lcStep.LevelID == RevHelper.idLevelKeeping || lcStep.LevelID == RevHelper.idLevelAnnuled;
  }

  public static void AnnulPICommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long num1 = (items.GetItemData(sc_6342.ssp_eco_6350(92980231), typeof (IDBObjectID)) as IDBObjectID).Value;
    long num2 = -1;
    string objCapt = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject piObj = sessionKeeper.Session.GetObject(num1, false);
      if (piObj == null)
        return;
      IDBAttribute attributeById1 = piObj.GetAttributeByID(RevHelper.idAttrStampedByII);
      if (attributeById1 != null && attributeById1.Value != null && attributeById1.Value != DBNull.Value)
      {
        long int64 = Convert.ToInt64(attributeById1.Value);
        if (int64 != 0L)
        {
          int num3 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_362"), (object) num1) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_370"), (object) int64), LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
      }
      long annulingRevision = ECO_PICommands.GetAnnulingRevision(num1);
      if (annulingRevision != 0L)
      {
        string str = "";
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(annulingRevision);
        if (!objectInfo.Empty)
          str = objectInfo.Caption;
        int num4 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_362"), (object) num1) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_367"), (object) str, (object) annulingRevision), LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK);
        return;
      }
      if (ECO_PICommands.IsLevelForbidden(piObj))
      {
        int num5 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_362"), (object) num1) + LocalizationHolder.rm.GetString("ECO.Client_369"), LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK);
        return;
      }
      IDBAttribute attributeById2 = piObj.GetAttributeByID(RevHelper.idLinkToAnnuledPI);
      if (attributeById2 != null && attributeById2.Value != null)
      {
        long int64 = Convert.ToInt64(attributeById2.Value);
        if (int64 != 0L)
        {
          int num6 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_362"), (object) num1) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_368"), (object) int64), LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK);
          return;
        }
      }
      IDBAttribute attributeById3 = piObj.GetAttributeByID(RevHelper.idLinkedContNumber);
      num2 = attributeById3 == null || attributeById3.Value == null ? Math.Abs(num1) : Convert.ToInt64(attributeById3.Value);
      objCapt = piObj.Caption;
    }
    List<int> objTypes = new List<int>()
    {
      RevHelper.idObj_II,
      RevHelper.idObj_PI
    };
    bool Existing;
    Intermech.ECO.Client.ECO eco = plugin.CreateECO(objTypes, ECOGoal.Annul, objCapt, out Existing);
    if (eco == null)
      return;
    if (Existing)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(eco.EcoObjectID, false);
        if (dbObject == null)
          return;
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idLinkToAnnuledPI);
        if (attributeById != null)
        {
          if (attributeById.Value != null)
          {
            long int64 = Convert.ToInt64(attributeById.Value);
            string str = "";
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
            if (!objectInfo.Empty)
              str = objectInfo.Caption;
            int num7 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_363"), (object) str, (object) int64), LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(eco.EcoObjectID, false);
      if (Existing)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idLinkedContNumber);
        if (attributeById != null && attributeById.Value != null)
        {
          long int64 = Convert.ToInt64(attributeById.Value);
          if (int64 != num2 && int64 != Math.Abs(dbObject.ObjectID))
          {
            int num8 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_365"), LocalizationHolder.rm.GetString("ECO.Client_103"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
        }
      }
      dbObject.Attributes.AddAttribute(RevHelper.idLinkedContNumber, false).AsInteger = num2;
    }
    plugin.CreateECOEditorForm(eco, false, true, true, false);
    eco.CopyLinksFrom(num1, true, 1);
    TableElement change = eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
    change.SetAttributeValue(Intermech.ECO.Client.ECO.replacedPIAttr, Convert.ToString(Math.Abs(num1)));
    TableElement child = (change.Template.FindNode(Intermech.ECO.Client.ECO.idSpecText) as TableElement).CloneFromTemplate() as TableElement;
    if (change.AddChildNode((DocumentTreeNode) child, false, false) >= 0)
    {
      TextData templateRecursive = (TextData) child.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecTextFld);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(num1);
        string designationInEco = eco.GetDocDesignationInECO(dbObject);
        templateRecursive.Text = string.Format(LocalizationHolder.rm.GetString("ECO.Client_364"), (object) designationInEco);
      }
      ECOPlugin.RemoveDefaultText(change);
      ((TextData) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo))?.AssignText(Intermech.ECO.Client.ECO.noChangeNumber, false, false, false);
      ((PageElementNode) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo)).ReadOnly = true;
      ((PageElementNode) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign)).ReadOnly = true;
      eco.DocumentECO.UpdateLayout(true, true);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute dbAttribute = sessionKeeper.Session.GetObject(eco.EcoObjectID, false).Attributes.AddAttribute(RevHelper.idLinkToAnnuledPI, false);
      if (dbAttribute == null)
        return;
      dbAttribute.AsInteger = num1;
    }
  }

  public static void UnreplaceCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long num = (items.GetItemData(sc_6342.ssp_eco_6351(394862397), typeof (IDBObjectID)) as IDBObjectID).Value;
    long objectID = 0;
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(num, false);
      if (dbObject1 == null)
        return;
      string asString = dbObject1.GetAttributeByID(RevHelper.idAttrDesign).AsString;
      IDBAttribute attributeById = dbObject1.GetAttributeByID(RevHelper.idAttrStampedByII);
      if (attributeById != null)
      {
        if (attributeById.Value != null)
        {
          if (attributeById.Value != DBNull.Value)
          {
            objectID = Convert.ToInt64(attributeById.Value);
            if (objectID == 0L)
              return;
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(objectID, false);
            if (dbObject2 == null)
              return;
            str = dbObject2.GetAttributeByID(RevHelper.idAttrDesign).AsString;
          }
        }
      }
    }
    if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_433"), (object) str, (object) objectID), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(num, false);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        dbObject = dbObject.CheckOut();
      IDBAttribute attributeById = dbObject.GetAttributeByID(RevHelper.idAttrStampedByII);
      if (attributeById != null)
        attributeById.Value = (object) DBNull.Value;
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IECOServer)) is IECOServer customService)
      {
        List<IdLinkPair> linksFrom = ECO_PICommands.GetLinksFrom(num);
        customService.ClearChangeNumbers(linksFrom);
        foreach (IdLinkPair idLinkPair in linksFrom)
        {
          IDBAttribute byId = sessionKeeper.Session.GetObject(idLinkPair.ObjID, false).Attributes.FindByID(RevHelper.idAttrRevision);
          if (byId != null)
            byId.AsInteger = num;
        }
      }
    }
    if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_434"), (object) str, (object) objectID), LocalizationHolder.rm.GetString("ECO.Client_435"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(objectID, false)?.Delete(0L);
    }
    else
    {
      ECOEditorForm ecoEditorForm = plugin.OpenECOEditorForObject(objectID, false, true, true, true);
      TableData dataOwner;
      for (int dataPositionInFlow = ecoEditorForm.ECO.ecoMainTable.FindDataPositionInFlow(0, out dataOwner); dataPositionInFlow != -1 && dataOwner != null && dataPositionInFlow < dataOwner.Nodes.Count; dataPositionInFlow = dataOwner.FindNextDataPositionInFlow(dataPositionInFlow, out dataOwner))
        (dataOwner.Nodes[dataPositionInFlow] as TableData).RemoveAttribute(Intermech.ECO.Client.ECO.replacedPIAttr, false, false);
      ecoEditorForm.SaveDocument();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
        if (dbObject == null)
          return;
        dbObject.Attributes.FindByID(RevHelper.idLinkToAnnuledPI)?.Delete(0L);
        IDBAttribute byId = dbObject.Attributes.FindByID(RevHelper.idAttrObjectLink);
        if (byId == null)
          return;
        byId.Value = (object) DBNull.Value;
      }
    }
  }

  public static List<IdLinkPair> GetLinksFrom(long otherEcoId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(RevHelper.idLinkRevision);
      relationCollection.LocalTypesMode = true;
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[4]
      {
        (object) -26,
        (object) -22,
        (object) -2,
        (object) -21
      }), otherEcoId);
      List<IdLinkPair> linksFrom = new List<IdLinkPair>();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string g = dataTable.Rows[index][0].ToString();
        long int64_1 = Convert.ToInt64(dataTable.Rows[index][2]);
        long int64_2 = Convert.ToInt64(dataTable.Rows[index][3]);
        IDBRelation relation = sessionKeeper.Session.GetRelation(new Guid(g), int64_2, false);
        if (relation != null)
          linksFrom.Add(new IdLinkPair(int64_1, relation.RelationID));
      }
      return linksFrom;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECO_PRCommands
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
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class ECO_PRCommands
{
  public static void AcceptCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long num1 = (items.GetItemData(sc_6342.ssp_eco_6352(509601558), typeof (IDBObjectID)) as IDBObjectID).Value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long num2 = num1;
      IDBObject piObj = sessionKeeper.Session.GetObject(num2);
      IDBAttribute attributeById1 = piObj.GetAttributeByID(RevHelper.idAttrScannedDoc);
      if (attributeById1 != null && attributeById1.AsBoolean)
      {
        int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_377"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
        return;
      }
      IDBAttribute attributeById2 = piObj.GetAttributeByID(RevHelper.idAttrStampedByII);
      if (attributeById2 != null && attributeById2.Value != DBNull.Value)
      {
        IDBAttribute attributeById3 = piObj.GetAttributeByID(RevHelper.idAttrDesign);
        string str1 = attributeById3 == null || attributeById3.Value == DBNull.Value ? $"[{Convert.ToString(num2)}]" : $"{Convert.ToString(attributeById3.Value)} [{Convert.ToString(num2)}]";
        long int64 = Convert.ToInt64(attributeById2.Value);
        IDBObject dbObject = sessionKeeper.Session.GetObject(int64, false);
        string str2 = $"[{Convert.ToString(int64)}]";
        if (dbObject != null)
        {
          IDBAttribute attributeById4 = dbObject.GetAttributeByID(RevHelper.idAttrDesign);
          if (attributeById4 != null && attributeById4.Value != DBNull.Value)
            str2 = $"{Convert.ToString(attributeById4.Value)} {str2}";
        }
        switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_354"), (object) str1, (object) str2), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.YesNoCancel))
        {
          case DialogResult.Cancel:
            return;
          case DialogResult.No:
            return;
        }
      }
      long annulingRevision = ECO_PICommands.GetAnnulingRevision(num2);
      if (annulingRevision != 0L)
      {
        switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_378"), (object) num2) + string.Format(LocalizationHolder.rm.GetString("ECO.Client_367"), (object) annulingRevision), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
        {
          case DialogResult.OK:
            return;
          case DialogResult.Cancel:
            return;
        }
      }
      if (ECO_PICommands.IsLevelForbidden(piObj))
      {
        switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_378"), (object) num2) + LocalizationHolder.rm.GetString("ECO.Client_369"), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
        {
          case DialogResult.OK:
            return;
          case DialogResult.Cancel:
            return;
        }
      }
    }
    Intermech.ECO.Client.ECO eco = (Intermech.ECO.Client.ECO) null;
    ECOPlugin.RevObjectCreator.BlockLinking = true;
    try
    {
      eco = plugin.CreateECO(RevHelper.idObj_II, ECOGoal.NoGoal, "");
    }
    finally
    {
      ECOPlugin.RevObjectCreator.BlockLinking = false;
    }
    if (eco == null)
      return;
    eco.CopyPIAttribs(num1);
    eco.CopyLinksFrom(num1, true);
    ECOEditorForm ecoEditorForm = plugin.CreateECOEditorForm(eco, false, true, true, false);
    TableElement change = eco.AddNewEcoRow(Intermech.ECO.Client.ECO.fldChange);
    TableElement child = (change.Template.FindNode(Intermech.ECO.Client.ECO.idSpecText) as TableElement).CloneFromTemplate() as TableElement;
    if (change.AddChildNode((DocumentTreeNode) child, false, false) >= 0)
    {
      try
      {
        TextData templateRecursive1 = (TextData) child.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idSpecTextFld);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(num1);
          string designationInEco = eco.GetDocDesignationInECO(dbObject);
          templateRecursive1.Text = LocalizationHolder.rm.GetString("ECO.Client_105") + designationInEco + LocalizationHolder.rm.GetString("ECO.Client_106");
        }
        ECOPlugin.RemoveDefaultText(change);
        TextData templateRecursive2 = (TextData) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldChangeNo);
        if (templateRecursive2 != null)
          templateRecursive2.ReadOnly = true;
        TextData templateRecursive3 = (TextData) change.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign);
        if (templateRecursive3 != null)
          templateRecursive3.ReadOnly = true;
      }
      finally
      {
        ecoEditorForm.Document.UpdateLayout(0, true, true);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute dbAttribute = sessionKeeper.Session.GetObject(num1).Attributes.AddAttribute(RevHelper.idAttrStampedByII, false);
      if (dbAttribute == null)
        return;
      dbAttribute.AsInteger = Math.Abs(eco.EcoObjectID);
    }
  }

  public static void AcceptContentsCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    long num1 = (items.GetItemData(sc_6342.ssp_eco_6353(1261320306), typeof (IDBObjectID)) as IDBObjectID).Value;
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
          switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_411"), (object) str1, (object) str2), LocalizationHolder.rm.GetString("ECO.Client_66"), MessageBoxButtons.OKCancel))
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
            throw new Exception(LocalizationHolder.rm.GetString("ECO.Client_107"));
        }
        catch (Exception ex)
        {
          int num6 = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("ECO.Client_108"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                templateRecursive.Text = LocalizationHolder.rm.GetString("ECO.Client_105") + objCaption + LocalizationHolder.rm.GetString("ECO.Client_106");
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
            ecoEditorForm.UpdateAllMultiHeaders();
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
}

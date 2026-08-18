// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowBriefcase
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Briefcase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class WorkflowBriefcase
{
  private static bool _objectFoundShown;

  public static void Export(long objectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObject(objectID, false) is IScheme scheme1))
        return;
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "*.iwf|*.iwf";
      saveFileDialog.DefaultExt = "iwf";
      saveFileDialog.FileName = scheme1.Name + ".iwf";
      saveFileDialog.RestoreDirectory = true;
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string fileName = saveFileDialog.FileName;
      SimpleBriefcase simpleBriefcase = new SimpleBriefcase(sessionKeeper.Session);
      SimpleBriefcase.ObjectExportingDelegate exportingDelegate = (SimpleBriefcase.ObjectExportingDelegate) ((brief, obj) =>
      {
        if (obj == null || obj.ObjectType != wfConsts.SchemesTypeID && obj.ObjectType != wfConsts.ProcessesTypeID)
          return;
        IScheme scheme2 = obj as IScheme;
        IActivity[] activities = scheme2.Activities;
        List<long> longList = new List<long>();
        foreach (IActivity activity in activities)
        {
          long formId = activity.FormID;
          if (formId != 0L)
            brief.AddObject(formId);
          brief.AddObject(activity.ObjectID);
          longList.Add(activity.ObjectID);
        }
        foreach (IDBObject allLink in scheme2.AllLinks)
          brief.AddObject(allLink.ObjectID);
      });
      simpleBriefcase.ObjectExporting += exportingDelegate;
      simpleBriefcase.RegisterSpecialRule(wfConsts.AttrFromActivityID, SpecialRule.ObjectLinkAttribute);
      simpleBriefcase.RegisterSpecialRule(wfConsts.AttrToActivityID, SpecialRule.ObjectLinkAttribute);
      simpleBriefcase.RegisterExportedRelation(wfConsts.ActivitiesTypeID, wfConsts.ScriptRelationTypeID, SelectFunction.ConsistFrom, wfConsts.ScriptsTypeID);
      if (scheme1.ObjectType == wfConsts.ProcessesTypeID)
      {
        IDBAttribute attributeById1 = scheme1.GetAttributeByID(wfConsts.AttrCreateActivitiesOnDemandID);
        if (attributeById1 != null && attributeById1.AsBoolean)
        {
          IDBAttribute attributeById2 = scheme1.GetAttributeByID(wfConsts.AttrPrototypeID);
          if (attributeById2 != null)
            simpleBriefcase.AddObject(attributeById2.AsInteger);
        }
      }
      simpleBriefcase.AddObject(objectID, true);
      simpleBriefcase.AttributeExporting += new SimpleBriefcase.AttributeExportingDelegate(WorkflowBriefcase.Briefcase_AttributeExporting);
      simpleBriefcase.Export(fileName);
      if (!(simpleBriefcase.Errors != ""))
        return;
      int num = (int) MessageBox.Show(simpleBriefcase.Errors, (string) null, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private static bool Briefcase_AttributeExporting(
    SimpleBriefcase brief,
    IDBAttributable obj,
    SimpleBriefcase.BriefcaseAttribute attr,
    MemoryStream stream)
  {
    WorkflowBriefcase.StrValueFunc strValueFunc = (WorkflowBriefcase.StrValueFunc) (() => attr.Value == null ? "" : attr.Value.ToString());
    Action action = (Action) (() =>
    {
      stream.Position = (long) sc_21930.ssp_workflow_21931(1402829942);
      attr.Blob.Binary = Convert.ToBase64String(stream.ToArray());
      if (attr.Blob.RealFileSize >= stream.Length)
        return;
      attr.Blob.RealFileSize = stream.Length;
    });
    if (attr.ID == (long) wfConsts.AttrParticipantsID)
    {
      ParticipantList participantList = new ParticipantList(brief.Session);
      participantList.AsString = strValueFunc();
      participantList.WriteGuids = true;
      attr.Value = (object) participantList.AsString;
      brief.AddMapping(Domain.Objects, (IEnumerable<long>) participantList.ObjectIDs);
    }
    else if (attr.ID == (long) wfConsts.AttrVariablesID)
    {
      VarList varList = new VarList(brief.Session, false, false);
      varList.LoadFromStream((Stream) stream);
      foreach (Variable variable in varList)
        brief.AddMapping(Domain.Variables, (long) variable.AttrTypeID);
      varList.WriteGuids = true;
      stream = new MemoryStream();
      varList.SaveToStream((Stream) stream);
      action();
      brief.AddMapping(Domain.Objects, (IEnumerable<long>) varList.ObjectIDs);
    }
    else if (attr.ID == (long) wfConsts.AttrNotificationsID)
    {
      Notifications notifications = new Notifications(brief.Session);
      notifications.XMLOnlyMode = true;
      notifications.LoadFromStream((Stream) stream);
      notifications.WriteGuids = true;
      stream = new MemoryStream();
      notifications.SaveToStream((Stream) stream);
      action();
      brief.AddMapping(Domain.Objects, (IEnumerable<long>) notifications.ObjectIDs);
    }
    else if (attr.ID == (long) wfConsts.AttrTermsID)
    {
      Terms terms = new Terms(brief.Session);
      terms.XMLOnlyMode = true;
      terms.LoadFromStream((Stream) stream);
      terms.WriteGuids = true;
      stream = new MemoryStream();
      terms.SaveToStream((Stream) stream);
      action();
    }
    else if (attr.ID == (long) wfConsts.AttrConditionID && obj is IDBObject && (obj as IDBObject).TypeID == wfConsts.CaseTypeID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ConditionList conditionList = new ConditionList(sessionKeeper.Session);
        conditionList.LoadFromStream((Stream) stream);
        conditionList.WriteGuids = true;
        stream = new MemoryStream();
        conditionList.SaveToStream((Stream) stream);
        action();
      }
    }
    else if (attr.ID == (long) wfConsts.AttrConditionFormulaID && obj is IDBObject && (obj as IDBObject).TypeID == wfConsts.CaseTypeID)
      action();
    else if (attr.ID == (long) wfConsts.AttrAddInfoID)
    {
      XmlIni xmlIni = new XmlIni();
      xmlIni.Load((Stream) stream);
      bool flag = false;
      string str = xmlIni.ReadString("Props", "TimerPeriod");
      if (str == "" && xmlIni.Root.Name == "Period")
        str = xmlIni.AsString;
      if (str != "")
      {
        xmlIni.WriteString("Props", "TimerPeriod", new PeriodInformation(brief.Session)
        {
          AsString = str,
          WriteGuids = true
        }.AsString);
        flag = true;
      }
      if (flag)
      {
        stream = new MemoryStream();
        xmlIni.Save((Stream) stream);
        action();
      }
    }
    else if (attr.ID == (long) wfConsts.AttrLCConfigAttrID)
    {
      LCInfoList lcInfoList = new LCInfoList();
      lcInfoList.AsString = strValueFunc();
      lcInfoList.WriteGuids = true;
      attr.Value = (object) lcInfoList.AsString;
      foreach (KeyValuePair<Domain, List<long>> objectId in lcInfoList.ObjectIDs)
        brief.AddMapping(objectId.Key, (IEnumerable<long>) objectId.Value);
    }
    else if (attr.ID == (long) wfConsts.AttrObjectTypesID)
    {
      foreach (object obj1 in attr.Values)
      {
        if (obj1.ToString() != "")
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(obj1.ToString()));
          if (objectTypeId > 0)
            brief.AddMapping(Domain.ObjectTypes, (long) objectTypeId);
        }
      }
    }
    return true;
  }

  public static void Import()
  {
    OpenFileDialog openFileDialog1 = new OpenFileDialog();
    openFileDialog1.Filter = "*.iwf|*.iwf";
    openFileDialog1.RestoreDirectory = true;
    OpenFileDialog openFileDialog2 = openFileDialog1;
    if (openFileDialog2.ShowDialog() != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      SimpleBriefcase simpleBriefcase = new SimpleBriefcase(sessionKeeper.Session);
      simpleBriefcase.RegisterSpecialRule(wfConsts.AttrFromActivityID, SpecialRule.ObjectLinkAttribute);
      simpleBriefcase.RegisterSpecialRule(wfConsts.AttrToActivityID, SpecialRule.ObjectLinkAttribute);
      simpleBriefcase.RegisterSpecialRule(wfConsts.AttrRecipID, SpecialRule.CreateSurrogateObjects);
      simpleBriefcase.RegisterSpecialRule(wfConsts.AttrSenderID, SpecialRule.CreateSurrogateObjects);
      SimpleBriefcase.BriefcaseImportedDelegate importedDelegate = (SimpleBriefcase.BriefcaseImportedDelegate) ((brief, _objs) =>
      {
        foreach (KeyValuePair<long, IDBObject> keyValuePair in _objs)
        {
          if (keyValuePair.Value is IScheme scheme2)
          {
            string description = scheme2.Description;
            string str = $"/* {LocalizationHolder.GetString("Imported")} {DateTime.Now} */";
            if (description != "")
              str = "\r\n" + str;
            scheme2.Description = description + str;
            SchemeStatus schemeStatus = string.IsNullOrEmpty(scheme2.Validate()) ? SchemeStatus.Valid : SchemeStatus.Invalid;
            scheme2.GetAttributeByID(wfConsts.AttrActivityStatusID).AsInteger = (long) schemeStatus;
            if (schemeStatus == SchemeStatus.Invalid)
            {
              IDBAttribute aIDBAttribute = scheme2.Attributes.AddAttribute(wfConsts.AttrBriefcaseID, false);
              try
              {
                using (FileStream aSourceStream = new FileStream(brief.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                  BlobInformation aBlobInformation = new BlobInformation(aSourceStream.Length, 0L, DateTime.Now, "", ArcMethods.NotPacked, "");
                  new BlobProcWriter(aIDBAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
                }
              }
              catch (Exception ex)
              {
                ExceptionHelper.ExceptionService.ShowException(ex);
                aIDBAttribute.Delete(0L);
              }
            }
          }
        }
      });
      simpleBriefcase.BriefcaseImported += importedDelegate;
      string fileName = openFileDialog2.FileName;
      simpleBriefcase.ImportPrompt += new Func<string, SimpleBriefcase, bool>(WorkflowBriefcase.ImportPrompt);
      simpleBriefcase.CreateVariablesPrompt += new Func<SimpleBriefcase, bool>(WorkflowBriefcase.CreateVariablesPrompt);
      WorkflowBriefcase._objectFoundShown = false;
      simpleBriefcase.ObjectFound += new SimpleBriefcase.BriefcaseObjectFound(WorkflowBriefcase.BriefcaseObjectFound);
      List<SimpleBriefcase.ImportedObjectInfo> importedObjectInfoList = simpleBriefcase.Import(fileName);
      if (importedObjectInfoList == null)
        return;
      List<long> longList = new List<long>();
      foreach (SimpleBriefcase.ImportedObjectInfo importedObjectInfo in importedObjectInfoList)
        longList.Add(importedObjectInfo.ObjectID);
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", (IList<long>) longList.ToArray());
      BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
      foreach (SimpleBriefcase.ImportedObjectInfo importedObjectInfo in importedObjectInfoList)
      {
        if (importedObjectInfo.IsRoot)
        {
          wfFunx.OpenProcess(importedObjectInfo.ObjectID, importedObjectInfo.ObjectTypeID == wfConsts.SchemesTypeID);
          break;
        }
      }
      if (!(simpleBriefcase.Errors != ""))
        return;
      int num = (int) MessageBox.Show($"{LocalizationHolder.GetString("BriefcaseImportErr")}\r\n{simpleBriefcase.Errors}", (string) null, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private static void BriefcaseObjectFound(SimpleBriefcase brief, ref IDBObject obj)
  {
    if (WorkflowBriefcase._objectFoundShown)
      return;
    WorkflowBriefcase._objectFoundShown = true;
    if (MessageBox.Show(string.Format(LocalizationHolder.GetString("ImportedObjectExists"), (object) obj.NameInMessages), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
      throw new AbortException();
  }

  private static bool ImportPrompt(string filename, SimpleBriefcase brief)
  {
    string str = "";
    if (brief.RootObject != null)
      str = brief.RootObject.Caption;
    return MessageBox.Show(string.Format(LocalizationHolder.GetString(nameof (ImportPrompt)), (object) brief.RootObjectTypeName.ToLowerInvariant(), (object) str, (object) filename), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
  }

  private static bool CreateVariablesPrompt(SimpleBriefcase brief)
  {
    return MessageBox.Show($"{brief.RootObjectTypeName} {LocalizationHolder.GetString("CreateVarsPrompt")}", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
  }

  private delegate string StrValueFunc();
}

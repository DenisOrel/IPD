// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpObjectCreator
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using ImSSP;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.SelectionService;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.SelectionView;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Universal Object Creator for all expert system objects
/// </summary>
public class ExpObjectCreator : IObjectCreatorCustomService
{
  internal string GetNewName(string s1, string s2)
  {
    return new UserPrompt().Execute(LocalizationHolder.rm.GetString(s1), LocalizationHolder.rm.GetString(s2), false);
  }

  private void CreateRelations(
    int[] linkTypesID,
    long[] relatedObjIDs,
    long objID,
    DateTime startRelationTime)
  {
    if (linkTypesID.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < linkTypesID.Length; ++index)
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(linkTypesID[index]);
        try
        {
          relationCollection.Create(relatedObjIDs[index], objID, startRelationTime);
        }
        catch (KernelException ex)
        {
        }
      }
    }
  }

  public long CreateObjectDialog(
    int aObjectTypeID,
    long protoObjID,
    int[] linkTypesID,
    long[] relatedObjIDs,
    DateTime startRelationTime,
    bool IsVersion)
  {
    if (IsVersion)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_588"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
      return 0;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (protoObjID != 0L && protoObjID != -1L)
        dbObject = sessionKeeper.Session.GetObject(protoObjID, false);
      if (dbObject != null & IsVersion)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.ObjectType, false);
        if (objectType == null || objectType.Versionable != ObjectVersionModes.MultiVersion)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_587"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
          return 0;
        }
      }
      if (aObjectTypeID == ExpertConsts.Consts.objFormula || aObjectTypeID == ExpertConsts.Consts.objCond || aObjectTypeID == ExpertConsts.Consts.objSimpleFormula || aObjectTypeID == ExpertConsts.Consts.objESFolder)
      {
        if (dbObject != null && !IsVersion)
        {
          string initValue = new UserPrompt().Execute(LocalizationHolder.rm.GetString("Expert.Editor_585"), LocalizationHolder.rm.GetString("Expert.Editor_210"), false);
          if (initValue == "" || !(dbObject is IExpertFormulable))
            return -1;
          IExpertFormulable prototype = dbObject as IExpertFormulable;
          IExpertFormulable expertFormulable = sessionKeeper.Session.GetObjectCollection(aObjectTypeID).Create((IDBObject) prototype) as IExpertFormulable;
          AttributeValues[] valuesList = new AttributeValues[2]
          {
            new AttributeValues(ExpertConsts.Consts.attrObjectName, (object) initValue),
            new AttributeValues(ExpertConsts.Consts._attrObjName, (object) initValue)
          };
          expertFormulable.SetAttributesValues(valuesList, false, false);
          expertFormulable.CommitCreation(true);
          IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
          byte[] traceInfo = (byte[]) null;
          bool flag = false;
          if (customService != null)
            flag = customService.ReflectObjUpdate(sessionKeeper.Session.SessionGUID, expertFormulable.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
          if (flag)
          {
            using (RuleUpdateReport ruleUpdateReport = new RuleUpdateReport())
              ruleUpdateReport.Execute(traceInfo);
          }
          return expertFormulable.ObjectID;
        }
        ExpertFormulaType efType = aObjectTypeID != ExpertConsts.Consts.objFormula ? (aObjectTypeID != ExpertConsts.Consts.objCond ? (aObjectTypeID != ExpertConsts.Consts.objESFolder ? ExpertFormulaType.SimpleFormula : ExpertFormulaType.ESFolder) : ExpertFormulaType.Cond) : ExpertFormulaType.CommonFormula;
        FormulaCreator formulaCreator = new FormulaCreator();
        if (!formulaCreator.Execute(efType))
          return -1;
        long objectDialog = formulaCreator.createObject(protoObjID, IsVersion);
        switch (objectDialog)
        {
          case -1:
          case 0:
            return objectDialog;
          default:
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectDialog));
            this.CreateRelations(linkTypesID, relatedObjIDs, objectDialog, startRelationTime);
            goto case -1;
        }
      }
      else
      {
        if (aObjectTypeID == ExpertConsts.Consts.objScript || aObjectTypeID == ExpertConsts.Consts.objFunction || aObjectTypeID == ExpertConsts.Consts.objCommandScript)
        {
          int objFunction = ExpertConsts.Consts.objFunction;
          string newName = this.GetNewName("Expert.Editor_369", "Expert.Editor_370");
          if (newName == "")
            return -1;
          DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
          ScriptEditCon con = new ScriptEditCon(aObjectTypeID == ExpertConsts.Consts.objScript ? ExpertScriptType.CommonCalc : (aObjectTypeID == ExpertConsts.Consts.objFunction ? ExpertScriptType.FunctionScript : ExpertScriptType.CommandScript), -1L, newName);
          con.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con);
          this.scriptEditor_Created((object) con.scriptEditor, con.scriptEditor.create_Args);
          con.scriptEditor.needCloseQuery = true;
          long scriptId = con.scriptEditor.scriptID;
          ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId));
          this.CreateRelations(linkTypesID, relatedObjIDs, scriptId, startRelationTime);
          con.Show(service, DockState.Document);
          con.Select();
          FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service);
        }
        if (aObjectTypeID == ExpertConsts.Consts.objAttrRules)
        {
          object aSender = (object) null;
          if (SelObjAttrControl.ShowDialog(ref aSender, LocalizationHolder.rm.GetString("Expert.Editor_203"), true, false, true))
          {
            InputObjectAttribute inputObjectAttribute = (InputObjectAttribute) aSender;
            DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
            ScriptEditCon con = new ScriptEditCon(ExpertScriptType.AttribRule, -1L, "");
            con.scriptEditor.CreateObjName(inputObjectAttribute.AttributeGUID, inputObjectAttribute.ObjectGUID);
            con.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con);
            this.scriptEditor_Created((object) con.scriptEditor, con.scriptEditor.create_Args);
            con.scriptEditor.needCloseQuery = true;
            long scriptId = con.scriptEditor.scriptID;
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId));
            this.CreateRelations(linkTypesID, relatedObjIDs, scriptId, startRelationTime);
            con.Show(service, DockState.Document);
            con.Select();
            FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service);
          }
        }
        if (aObjectTypeID == ExpertConsts.Consts.objRecalcScript)
        {
          object aSender = (object) null;
          if (SelObjAttrControl.ShowDialog(ref aSender, LocalizationHolder.rm.GetString("Expert.Editor_204"), true, false, true))
          {
            InputObjectAttribute inputObjectAttribute = (InputObjectAttribute) aSender;
            DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
            ScriptEditCon con = new ScriptEditCon(ExpertScriptType.RecalcScript, -1L, "");
            con.scriptEditor.CreateObjName(inputObjectAttribute.AttributeGUID, inputObjectAttribute.ObjectGUID);
            con.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con);
            this.scriptEditor_Created((object) con.scriptEditor, con.scriptEditor.create_Args);
            con.scriptEditor.needCloseQuery = true;
            long scriptId = con.scriptEditor.scriptID;
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId));
            this.CreateRelations(linkTypesID, relatedObjIDs, scriptId, startRelationTime);
            con.Show(service, DockState.Document);
            con.Select();
            FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service);
            con.scriptEditor.FocusFirstNode();
          }
        }
        if (aObjectTypeID == ExpertConsts.Consts.objObjRules)
        {
          object aSender = (object) null;
          if (SelObjAttrControl.ShowDialog(ref aSender, LocalizationHolder.rm.GetString("Expert.Editor_205"), true, true, false))
          {
            InputObjectAttribute inputObjectAttribute = (InputObjectAttribute) aSender;
            if (inputObjectAttribute.AttributeGUID == Guid.Empty && inputObjectAttribute.ObjectGUID == Guid.Empty)
              return -1;
            DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
            ScriptEditCon con = new ScriptEditCon(ExpertScriptType.ObjectRule, -1L, "");
            con.scriptEditor.CreateObjName(inputObjectAttribute.AttributeGUID, inputObjectAttribute.ObjectGUID);
            con.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con);
            this.scriptEditor_Created((object) con.scriptEditor, con.scriptEditor.create_Args);
            con.scriptEditor.needCloseQuery = true;
            long scriptId = con.scriptEditor.scriptID;
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId));
            this.CreateRelations(linkTypesID, relatedObjIDs, scriptId, startRelationTime);
            con.Show(service, DockState.Document);
            con.Select();
            FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service);
            con.scriptEditor.FocusFirstNode();
          }
        }
        if (aObjectTypeID == ExpertConsts.Consts.objComplectTemplate)
        {
          ComplectTemplateDlg complectTemplateDlg = new ComplectTemplateDlg();
          if (complectTemplateDlg.Execute())
          {
            DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
            ScriptEditCon con = new ScriptEditCon(ExpertScriptType.ComplectTemplate, -1L, complectTemplateDlg.name)
            {
              scriptEditor = {
                ObjectGUID = MetaDataHelper.GetObjectTypeGuid(complectTemplateDlg.objTypeId),
                AttributeGUID = Guid.Empty
              }
            };
            con.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con);
            this.scriptEditor_Created((object) con.scriptEditor, con.scriptEditor.create_Args);
            con.scriptEditor.needCloseQuery = true;
            long scriptId = con.scriptEditor.scriptID;
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId));
            this.CreateRelations(linkTypesID, relatedObjIDs, scriptId, startRelationTime);
            con.Show(service, DockState.Document);
            con.Select();
            FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service);
            con.scriptEditor.FocusFirstNode();
          }
        }
        if (aObjectTypeID == ExpertConsts.Consts.objDocScript)
        {
          long[] numArray;
          if (protoObjID == -1L)
            numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_206"), LocalizationHolder.rm.GetString("Expert.Editor_207"), ExpertConsts.Consts.objTemplate, SelectionOptions.Default);
          else
            numArray = new long[1]{ -1L };
          if (numArray != null && numArray.Length != 0)
          {
            long templId = numArray[sc_6469.ssp_expert_6472(1164888949)];
            string newObjName = "";
            if (!IsVersion)
            {
              newObjName = this.GetNewName(protoObjID != -1L ? "Expert.Editor_585" : "Expert.Editor_367", "Expert.Editor_368");
              if (newObjName == "")
                return -1;
            }
            DockManager service = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
            ScriptEditCon con = new ScriptEditCon(ExpertScriptType.DocScript, templId, newObjName);
            con.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con);
            this.scriptEditor_Created((object) con.scriptEditor, con.scriptEditor.create_Args);
            con.scriptEditor.needCloseQuery = true;
            long scriptId = con.scriptEditor.scriptID;
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId));
            this.CreateRelations(linkTypesID, relatedObjIDs, scriptId, startRelationTime);
            con.Show(service, DockState.Document);
            con.Select();
            FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service);
            con.scriptEditor.FocusFirstNode();
          }
          return -1;
        }
        if (aObjectTypeID != ExpertConsts.Consts.objVisScheme)
        {
          if (aObjectTypeID != ExpertConsts.Consts.objVisStyles)
            goto label_78;
        }
        bool flag = aObjectTypeID == ExpertConsts.Consts.objVisScheme;
        string newObjName1 = flag ? this.GetNewName("Expert.Editor_683", "Expert.Editor_684") : this.GetNewName("Expert.Editor_685", "Expert.Editor_686");
        if (newObjName1 == "")
          return -1;
        DockManager service1 = (DockManager) FormulaEditPlugin._serviceProvider.GetService(typeof (DockManager));
        ScriptEditCon con1 = new ScriptEditCon(flag ? ExpertScriptType.VisDataScheme : ExpertScriptType.VisStyles, -1L, newObjName1);
        con1.scriptEditor.create_Args = new CreateEventArgs(protoObjID, linkTypesID, relatedObjIDs, startRelationTime, IsVersion, (DockControl) con1);
        this.scriptEditor_Created((object) con1.scriptEditor, con1.scriptEditor.create_Args);
        con1.scriptEditor.needCloseQuery = true;
        long scriptId1 = con1.scriptEditor.scriptID;
        ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", scriptId1));
        this.CreateRelations(linkTypesID, relatedObjIDs, scriptId1, startRelationTime);
        con1.Show(service1, DockState.Document);
        con1.Select();
        FormulaEditPlugin.FindPlugin().UpdateDocumentCaption(service1);
      }
    }
label_78:
    return -1;
  }

  private void scriptEditor_Created(object sender, CreateEventArgs e)
  {
    long objID = ((ScriptEdit2) sender).createObject(e.protoObjID, e.IsVersion);
    if (objID == -1L)
      return;
    this.CreateRelations(e.linkTypesID, e.relatedObjIDs, objID, e.startRelationTime);
    ((ScriptEdit2) sender).scriptID = objID;
  }
}

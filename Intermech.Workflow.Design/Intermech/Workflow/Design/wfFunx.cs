// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.wfFunx
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core.Organizer;
using Intermech.Commands;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Expert;
using Intermech.Expert.Editor;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.SelectionView;
using Intermech.Remoting;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for wfFunx.</summary>
public class wfFunx
{
  private static readonly IUserNamesCache _cache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
  private static long _processID = 0;
  public static string LastBrowsedSchemeName = "";

  public static long CreateProcess() => wfFunx.CreateProcess(0L);

  public static long CreateProcess(long schemeID)
  {
    return wfFunx.CreateProcess(schemeID, (AttachmentList) null);
  }

  public static long CreateProcess(long schemeID, ISimpleSelectedItems items)
  {
    return wfFunx.CreateProcess(schemeID, items, 0L);
  }

  public static long CreateProcess(long schemeID, ISimpleSelectedItems items, long rootWFGroup)
  {
    IDBTypedObjectID[] objs = new IDBTypedObjectID[0];
    bool flag = true;
    if (items != null)
    {
      objs = new IDBTypedObjectID[items.Count];
      for (int index = 0; index < items.Count; ++index)
      {
        objs[index] = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        if (objs[index] != null && objs[index].ObjectType != wfConsts.SchemesTypeID)
          flag = false;
      }
    }
    string filtered = "";
    List<int> applicableAttachmentTypes = wfFunx.GetApplicableAttachmentTypes(wfConsts.ActivitiesTypeID, wfConsts.AttachmentRelationTypeID);
    if (schemeID != 0L)
      new AllowedTypes(schemeID).Filter(applicableAttachmentTypes);
    IDBTypedObjectID[] applicableAttachments = wfFunx.GetApplicableAttachments(objs, applicableAttachmentTypes, ref filtered);
    if (!flag && filtered != "")
    {
      filtered = LocalizationHolder.rm.GetString(sc_21923.ssp_workflow_21924()) + filtered;
      filtered += LocalizationHolder.rm.GetString("Workflow.Design_102");
      if (MessageBox.Show(filtered, LocalizationHolder.rm.GetString("Workflow.Design_103"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
        return 0;
    }
    AttachmentList attachmentList = new AttachmentList();
    wfFunx.CopyIDBTypedToAttachments(applicableAttachments, attachmentList);
    return wfFunx.CreateProcess(schemeID, attachmentList, rootWFGroup);
  }

  public static long CreateProcess(long schemeID, AttachmentList attachs)
  {
    return wfFunx.CreateProcess(schemeID, attachs, 0L);
  }

  public static long CreateProcess(long schemeID, AttachmentList attachs, long rootWFGroup)
  {
    return wfFunx.CreateProcess(schemeID, attachs, string.Empty, string.Empty, rootWFGroup);
  }

  public static long CreateProcess(
    long schemeID,
    AttachmentList attachs,
    string caption,
    string message)
  {
    return wfFunx.CreateProcess(schemeID, attachs, caption, message, 0L);
  }

  public static long CreateProcess(
    long schemeID,
    AttachmentList attachs,
    string caption,
    string message,
    long rootWFGroup)
  {
    using (new SessionKeeper())
    {
      using (NewProcessForm newProcessForm = new NewProcessForm())
      {
        newProcessForm.SchemeID = schemeID;
        newProcessForm.SchemeRootGroupID = rootWFGroup;
        newProcessForm.FillAdditionalInfos(caption, message);
        if (attachs != null)
          newProcessForm.Attachments.Assign(attachs);
        if (newProcessForm.ShowDialog() == DialogResult.OK)
        {
          try
          {
            Holder.RecentLaunched.AddRecent(Math.Abs(newProcessForm.SchemeID));
            NotificationEventArgs e = new NotificationEventArgs("MailRefresh");
            BaseHolder.NotificationService.FireEvent((object) null, e);
            return newProcessForm.ProcessID;
          }
          catch
          {
            newProcessForm.DeleteProcess();
            throw;
          }
        }
        else
          newProcessForm.DeleteProcess();
      }
    }
    return 0;
  }

  public static void OpenProcess(ISelectedItems items, bool edit, bool showModal)
  {
    for (int index = 0; index < items.Count; ++index)
      wfFunx.OpenProcess((items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID, edit, showModal);
  }

  public static void OpenProcess(long id, bool isEditMode)
  {
    wfFunx.OpenProcess(id, isEditMode, false);
  }

  public static void OpenProcess(long id, bool isEditMode, bool showModal)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObject(id, false) is IDBSecurity dbSecurity)
      {
        int rightID = isEditMode ? 2 : 8;
        dbSecurity.CheckAccess((ActionType) rightID);
      }
    }
    DockControl dc = (DockControl) null;
    wfEditorForm wfEditorForm = (wfEditorForm) null;
    if (!showModal)
      wfEditorForm = Holder.Editors.FindEditor(id, isEditMode) as wfEditorForm;
    if (wfEditorForm?.Parent is DockControl)
    {
      dc = (DockControl) wfEditorForm.Parent;
    }
    else
    {
      if (!showModal)
        dc = new DockControl();
      wfEditorForm = new wfEditorForm(dc, id, isEditMode);
      if (dc != null)
      {
        dc.Text = wfEditorForm.Text;
        dc.Tag = (object) wfEditorForm;
        dc.TabImageIndex = Holder.SchemeNamedImageIndex;
        dc.ShowImageInDocumentTab = true;
        dc.Closing += new CancelEventHandler(wfEditorForm.FormClosingHandler);
        dc.PersistState = false;
        wfEditorForm.Visible = true;
        dc.Show((DockManager) ApplicationServices.Container.GetService(typeof (DockManager)));
      }
    }
    dc?.Activate();
    wfEditorForm.UpdateCommands();
    if (!showModal)
      return;
    int num = (int) wfEditorForm.ShowDialog();
    wfEditorForm.Dispose();
  }

  public static void EditProcess(long id) => wfFunx.OpenProcess(id, true);

  public static void ViewProcess(long id) => wfFunx.OpenProcess(id, false);

  public static void AbortProcess(long id)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetObject(id) as IProcess).StopProcess((IActivity) null, true);
  }

  public static void ShowProcessHistory(long id)
  {
    using (ProcessHistoryForm processHistoryForm = new ProcessHistoryForm())
    {
      processHistoryForm.FillProcessInfo(id);
      int num = (int) processHistoryForm.ShowDialog();
    }
  }

  public static string GetUserName(long id) => wfFunx._cache.GetUserName(id);

  public static string GetVarName(int id)
  {
    string attributeTypeName = MetaDataHelper.GetAttributeTypeName(id);
    return string.IsNullOrEmpty(attributeTypeName) ? "??" : attributeTypeName;
  }

  public static string GetParticipantName(Participant p)
  {
    return p.Kind != ParticipantKind.Variable ? wfFunx.GetUserName(p.ID) : wfFunx.GetVarName((int) p.ID);
  }

  public static string PromptForFileName(string DefExt, bool SaveDialog)
  {
    FileDialog fileDialog = !SaveDialog ? (FileDialog) new OpenFileDialog() : (FileDialog) new SaveFileDialog();
    try
    {
      fileDialog.Filter = string.Format(sc_21923.ssp_workflow_21925(), (object) DefExt);
      fileDialog.DefaultExt = DefExt;
      fileDialog.RestoreDirectory = true;
      return fileDialog.ShowDialog() == DialogResult.OK ? fileDialog.FileName : string.Empty;
    }
    finally
    {
      fileDialog.Dispose();
    }
  }

  public static void RegisterLoadSaveCommands(System.Windows.Forms.ToolBar tb, TextBox box)
  {
    LoadSaveTextHelper loadSaveTextHelper = new LoadSaveTextHelper(tb, box);
  }

  public static void StringToFile(string s, string fn)
  {
    StreamWriter streamWriter = new StreamWriter(fn, false);
    try
    {
      streamWriter.Write(s);
    }
    finally
    {
      streamWriter.Close();
    }
  }

  public static string FileToString(string fn)
  {
    StreamReader streamReader = new StreamReader(fn, Encoding.UTF8, true);
    try
    {
      return streamReader.ReadToEnd();
    }
    finally
    {
      streamReader.Close();
    }
  }

  public static bool ShowActivityProperties(long id)
  {
    if (Control.ModifierKeys == (Keys.Shift | Keys.Control))
    {
      using (ActivPropForm activPropForm = new ActivPropForm())
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject Activity = sessionKeeper.Session.GetObject(id);
          activPropForm.GetProperties(Activity, (WorkflowNode) null);
        }
        activPropForm.ReadOnly = true;
        return activPropForm.ShowDialog() == DialogResult.OK;
      }
    }
    using (ActivityProperty activityProperty = new ActivityProperty())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject activityObject = sessionKeeper.Session.GetObject(id);
        activityProperty.LoadProperty(activityObject, (WorkflowNode) null);
      }
      activityProperty.ReadOnly = true;
      return activityProperty.ShowDialog() == DialogResult.OK;
    }
  }

  public static ParticipantList SelectedItemsToParticipants(ISelectedItems si)
  {
    ParticipantList participants = (ParticipantList) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (si.Count > 0)
        participants = new ParticipantList();
      for (int index = 0; index < si.Count; ++index)
      {
        if (si.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(itemData.ObjectID);
          if (objectInfo.Empty)
            return participants;
          ParticipantKind Kind = objectInfo.ObjectTypeID == wfConsts.GroupTypeID ? ParticipantKind.Group : ParticipantKind.User;
          participants.AddParticipant(Kind, objectInfo.ObjectID);
        }
      }
    }
    return participants;
  }

  public static ParticipantList BrowseForUsers(string caption, long processID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (ChooseUsersForm chooseUsersForm = new ChooseUsersForm(processID, sessionKeeper.Session))
        return chooseUsersForm.ShowDialog() == DialogResult.OK ? chooseUsersForm.Participants : (ParticipantList) null;
    }
  }

  public static bool BrowseForUsers(ParticipantList users, long processID)
  {
    using (AddUsersForm addUsersForm = new AddUsersForm())
    {
      ParticipantList src = new ParticipantList();
      src.Assign(users);
      addUsersForm.Participants = src;
      addUsersForm.ProcessID = processID;
      int num = addUsersForm.ShowDialog() != DialogResult.OK ? 0 : (addUsersForm.Modified ? 1 : 0);
      if (num != 0)
        users.Assign(src);
      return num != 0;
    }
  }

  public static bool EditExpression(TempFormula tf) => wfFunx.EditExpression(ref tf, 0L);

  public static int SelectVariable(long processID, List<VarType> filterKinds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (VariablesForm variablesForm = new VariablesForm(processID, filterKinds, sessionKeeper.Session))
      {
        variablesForm.ReadOnly = true;
        variablesForm.SelectionMode = true;
        if (variablesForm.ShowDialog() == DialogResult.OK)
        {
          Intermech.Workflow.Variable selectedVar = variablesForm.SelectedVar;
          if (selectedVar != null)
            return selectedVar.AttrTypeID;
        }
      }
    }
    return 0;
  }

  public static int SelectVariable(long processID)
  {
    return wfFunx.SelectVariable(processID, (List<VarType>) null);
  }

  public static SelFormResult SelectVariableForExpert(object sender, SelTypeEventArgs e)
  {
    int attrTypeID = wfFunx.SelectVariable(wfFunx._processID);
    if (attrTypeID != 0)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
      if (attributeType != null)
        return new SelFormResult(attributeType.ShortName, attributeType.Name, attributeType.AttributeID, attributeType.AttributeGuid.ToString());
    }
    return (SelFormResult) null;
  }

  public static SelFormResult SelectVariableForExpertEx(object sender, SelTypeEventArgs e)
  {
    using (ChooseAttributeForm chooseAttributeForm = new ChooseAttributeForm(wfFunx._processID))
    {
      if (chooseAttributeForm.ShowDialog() == DialogResult.OK)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(chooseAttributeForm.SelectedAttributeType);
        if (attributeType != null)
          return new SelFormResult(attributeType.ShortName, attributeType.Name, attributeType.AttributeID, attributeType.AttributeGuid.ToString());
      }
    }
    return (SelFormResult) null;
  }

  public static bool EditExpression(
    ref string exp,
    List<Intermech.Expressions.Variable> variables,
    List<Intermech.Expressions.Variable> activityVariables)
  {
    if (variables == null)
      throw new ArgumentNullException(nameof (variables));
    return ExpressionEditor.EditExpression(ref exp, variables, -1, (ParseEventHandler) null, activityVariables);
  }

  public static bool EditExpression(ref TempFormula tf, long ProcessID)
  {
    return wfFunx.EditExpression(ref tf, ProcessID, false);
  }

  public static bool EditExpression(
    ref TempFormula tf,
    long ProcessID,
    bool UseExtendedAttributeSelector)
  {
    using (FormEditor formEditor = new FormEditor())
    {
      if (ProcessID != (long) sc_21923.ssp_workflow_21926(180310992))
      {
        wfFunx._processID = ProcessID;
        if (UseExtendedAttributeSelector)
          formEditor.SelAttrType += new FormEditor.SelTypeEventHandler(wfFunx.SelectVariableForExpertEx);
        else
          formEditor.SelAttrType += new FormEditor.SelTypeEventHandler(wfFunx.SelectVariableForExpert);
      }
      return formEditor.Execute(ref tf, " ");
    }
  }

  [Obsolete("Устарел и заменён на использование стандартных окон")]
  public static void PerformCommand(long objID, string commandName)
  {
    RemotingCallContext.SetData("X-IPS-NoFilterQuery", "true");
    ISelectedItems items;
    try
    {
      items = Intermech.Navigator.ContextMenu.Services.GetItems(objID);
    }
    finally
    {
      RemotingCallContext.FreeNamedDataSlot("X-IPS-NoFilterQuery");
    }
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices);
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, (System.IServiceProvider) viewServices);
  }

  public static void PerformCommand(ISelectedItems items, string commandName)
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    wfFunx.PerformCommand(items, (System.IServiceProvider) viewServices, commandName);
  }

  public static void PerformCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    string commandName)
  {
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, viewServices);
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, viewServices);
  }

  public static long BrowseForScheme()
  {
    IDescriptor rootDescriptor = (IDescriptor) new TopObjectsDescriptor(Holder.CategorySchemesID, 0, LocalizationHolder.rm.GetString("Workflow.Design_146"), wfConsts.SchemeCategoriesID);
    ServiceContainer nodesContext = new ServiceContainer();
    nodesContext.AddService(typeof (IViewState), (object) new ViewStateService());
    nodesContext.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    nodesContext.AddService(typeof (VersionsRule), (object) Holder.AllVersionsRule);
    nodesContext.AddService(typeof (ICommandManager), (object) BaseHolder.CommandManager);
    object[] objArray = Intermech.Navigator.SelectionWindow.Select(sc_21923.ssp_workflow_21927(), LocalizationHolder.rm.GetString("Workflow.Design_106"), rootDescriptor, typeof (IDBObjectID), (System.IServiceProvider) nodesContext, SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return -1;
    wfFunx.LastBrowsedSchemeName = (objArray[0] as IDBObjectID).Caption;
    return (objArray[0] as IDBObjectID).Value;
  }

  public static List<int> GetApplicableAttachmentTypes()
  {
    return wfFunx.GetApplicableAttachmentTypes(wfConsts.ActivitiesTypeID, wfConsts.AttachmentRelationTypeID);
  }

  public static List<int> GetApplicableAttachmentTypes(int ObjectType, int RelationType)
  {
    List<int> applicableAttachmentTypes = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(RelationType, -1, ObjectType).Rows)
        applicableAttachmentTypes.Add(Convert.ToInt32(row["F_OBJECT_TYPE"]));
    }
    return applicableAttachmentTypes;
  }

  public static IDBTypedObjectID[] GetApplicableAttachments(
    IDBTypedObjectID[] objs,
    List<int> attTypes,
    ref string filtered)
  {
    List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>();
    HashSet<int> intSet = new HashSet<int>();
    foreach (IDBTypedObjectID dbTypedObjectId in objs)
    {
      if (dbTypedObjectId != null)
      {
        int objectType = dbTypedObjectId.ObjectType;
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objectType);
        objectTypeParentsId.Insert(0, objectType);
        bool flag = true;
        foreach (int num in objectTypeParentsId)
        {
          if (attTypes.Contains(num))
          {
            flag = false;
            break;
          }
        }
        if (flag)
          intSet.Add(objectType);
        else
          dbTypedObjectIdList.Add(dbTypedObjectId);
      }
    }
    if (filtered != null)
    {
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      foreach (int objectTypeID in intSet)
      {
        if (filtered != "")
          filtered += ", ";
        IDBObjectTypeInfo objectType = service.GetObjectType(objectTypeID);
        filtered = objectType == null ? filtered + "???" : filtered + objectType.ObjectTypeName;
      }
    }
    return dbTypedObjectIdList.ToArray();
  }

  public static IDBTypedObjectID[] GetApplicableAttachments(
    IDBTypedObjectID[] objs,
    int inObjectType,
    int RelationType,
    ref string filtered)
  {
    List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>();
    Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      foreach (IDBTypedObjectID dbTypedObjectId in objs)
      {
        if (dbTypedObjectId != null)
        {
          if (!dictionary.ContainsKey(dbTypedObjectId.ObjectType))
            dictionary[dbTypedObjectId.ObjectType] = applicabilityCollection.GetApplicability(RelationType, dbTypedObjectId.ObjectType, inObjectType) != null;
          if (dictionary[dbTypedObjectId.ObjectType])
            dbTypedObjectIdList.Add(dbTypedObjectId);
        }
      }
      if (filtered != null)
      {
        foreach (KeyValuePair<int, bool> keyValuePair in dictionary)
        {
          if (!keyValuePair.Value)
          {
            if (filtered != "")
              filtered += ", ";
            string objectTypeName = MetaDataHelper.GetObjectTypeName(keyValuePair.Key);
            filtered += string.IsNullOrEmpty(objectTypeName) ? "???" : objectTypeName;
          }
        }
      }
    }
    return dbTypedObjectIdList.ToArray();
  }

  public static IDBTypedObjectID[] BrowseForObjects() => wfFunx.BrowseForObjects((List<int>) null);

  public static IDBTypedObjectID[] BrowseForObjects(List<int> objectTypes)
  {
    return wfFunx.BrowseForObjects(objectTypes, true);
  }

  public static IDBTypedObjectID[] BrowseForObjects(List<int> objectTypes, bool showArchives)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    if (objectTypes == null)
    {
      descriptors.Add((IDescriptor) new ObjectTypesNodeDescriptor());
    }
    else
    {
      foreach (int objectType in objectTypes)
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectType));
    }
    if (showArchives && ApplicationServices.Container.GetService(typeof (IArchivesDescriptorService)) is IArchivesDescriptorService service)
      descriptors.Add(service.GetDescriptor());
    IDescriptor descriptor = (IDescriptor) new DesktopNodeDescriptor(DesktopObjectNode.DesktopObjectID);
    descriptors.Add(descriptor);
    return (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Design_108"), (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Workflow.Design_109"), descriptors), typeof (IDBTypedObjectID), SelectionOptions.Default);
  }

  public static AttachmentList BrowseForAttachments(
    int inObjectType,
    int RelationTypeID,
    AllowedTypes allowedTypes = null)
  {
    List<int> applicableAttachmentTypes = wfFunx.GetApplicableAttachmentTypes(inObjectType, RelationTypeID);
    allowedTypes?.Filter(applicableAttachmentTypes);
    IDBTypedObjectID[] objs = wfFunx.BrowseForObjects(applicableAttachmentTypes, true);
    if (objs != null)
    {
      string filtered = "";
      objs = wfFunx.GetApplicableAttachments(objs, applicableAttachmentTypes, ref filtered);
      if (filtered != "")
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Design_110") + filtered, LocalizationHolder.rm.GetString("Workflow.Design_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    AttachmentList list = (AttachmentList) null;
    if (objs != null && objs.Length != 0)
    {
      list = new AttachmentList();
      wfFunx.CopyIDBTypedToAttachments(objs, list);
    }
    return list;
  }

  public static void CopyIDBTypedToAttachments(IDBTypedObjectID[] objs, AttachmentList list)
  {
    if (objs == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IDBTypedObjectID dbTypedObjectId in objs)
      {
        Attachment attachment = list.NewAttachment();
        attachment.ObjectID = dbTypedObjectId.ObjectID;
        attachment.ID = dbTypedObjectId.ID;
        attachment.TypeID = dbTypedObjectId.ObjectType;
        IDBObject dbObject = sessionKeeper.Session.GetObject(attachment.ObjectID, false);
        if (dbObject != null)
          attachment.CheckOutBy = dbObject.CheckoutBy;
        list.Add(attachment);
      }
    }
  }

  public static void ShowActivityMessage(long id)
  {
    string text = string.Empty;
    string caption = "Просмотр сообщения";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(id);
      if (!objectInfo.Empty)
      {
        caption = $"Просмотр сообщения '{objectInfo.Caption}' (ID: {objectInfo.ObjectID})";
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(id, wfConsts.AttrActivityMessageID);
        if (objectAttributeById != null)
          text = HtmlUtils.nl2br(objectAttributeById.Value.ToString());
      }
    }
    MessageForm.Show(text, caption);
  }

  public static string VerifyFormulaStr(
    long objID,
    TempFormula tf,
    bool AllowNonExistentAttributes)
  {
    if (tf == null)
      return LocalizationHolder.rm.GetString("Workflow.Design_112");
    Guid empty = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      int num = customService.StartTask(sessionGuid);
      try
      {
        object obj = (object) null;
        ExpertResult expertResult = customService.CalcFormulaSimpleMode(num, (object) tf, objID, out obj);
        string str1;
        if (expertResult == ExpertResult.OK)
        {
          if (obj is bool)
          {
            string str2 = !Convert.ToBoolean(obj) ? LocalizationHolder.rm.GetString("Workflow.Design_114") : LocalizationHolder.rm.GetString("Workflow.Design_113");
            str1 = LocalizationHolder.rm.GetString("Workflow.Design_115") + str2;
          }
          else
          {
            str1 = LocalizationHolder.rm.GetString("Workflow.Design_116");
            if (obj != null)
              str1 += $" ({obj.ToString()})";
          }
        }
        else
          str1 = !AllowNonExistentAttributes || expertResult != ExpertResult.RuleNotFound ? LocalizationHolder.rm.GetString("Workflow.Design_118") + expertResult.ToString() : LocalizationHolder.rm.GetString("Workflow.Design_117");
        return str1;
      }
      finally
      {
        if (customService.GetTrace(num))
          MiscFunx.GenerateExpertTrace(customService, num, sessionKeeper.Session);
        customService.EndTask(num);
      }
    }
  }

  public static string VerifyFormulaStr(long objID, TempFormula tf)
  {
    return wfFunx.VerifyFormulaStr(objID, tf, false);
  }

  public static void ValidateFormulaDialog(
    long objectID,
    TempFormula tf,
    bool AllowNonExistentAttributes)
  {
    int num = (int) MessageBox.Show(wfFunx.VerifyFormulaStr(objectID, tf, AllowNonExistentAttributes), LocalizationHolder.rm.GetString("Workflow.Design_119"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  public static void ValidateFormulaDialog(long objectID, TempFormula tf)
  {
    wfFunx.ValidateFormulaDialog(objectID, tf, false);
  }

  public static void SayError(string s)
  {
    int num = (int) MessageBox.Show(s, LocalizationHolder.rm.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  public static bool InputQuery(string Caption, string Label, ref string Text)
  {
    using (InputQueryForm inputQueryForm = new InputQueryForm())
    {
      inputQueryForm.Text = Caption;
      inputQueryForm.l.Text = Label;
      inputQueryForm.tb.Text = Text;
      int num = inputQueryForm.ShowDialog() == DialogResult.OK ? 1 : 0;
      if (num != 0)
        Text = inputQueryForm.tb.Text;
      return num != 0;
    }
  }

  public static void ExecClientScript(ScriptKind kind, IActivity activity)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string scriptCode = MiscFunx.GetScriptCode(sessionKeeper.Session, activity.ObjectID, kind, ScriptExecSide.Client);
      if (scriptCode == null || !(scriptCode.Trim() != ""))
        return;
      bool oldStateOfTransaction = MiscFunx.CheckForActiveTransaction(sessionKeeper.Session);
      using (RemoteLock remoteLock = new RemoteLock())
      {
        remoteLock.Add((object) activity);
        ActivityFlags flags = activity.Flags;
        ActivityFlags activityFlags = flags;
        switch (kind)
        {
          case ScriptKind.BeforeExec:
            flags |= ActivityFlags.BeforeExec;
            break;
          case ScriptKind.AfterExec:
            flags |= ActivityFlags.AfterExec;
            break;
        }
        activity.Flags = flags;
        try
        {
          string str;
          try
          {
            str = MiscFunx.IsolatedExecScript(scriptCode, activity, CSharpScriptInvocationOptions.WithOptimizations);
          }
          finally
          {
            MiscFunx.CheckForActiveTransaction(sessionKeeper.Session, activity, $"[ECS] (Script ID={MiscFunx.LastScriptID})", oldStateOfTransaction);
            activity.Changed(ActivityChanged.SaveGlobalVariables);
            activity.Changed(ActivityChanged.SaveVariables);
          }
          if (!string.IsNullOrEmpty(str))
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_121"), (object) (int) kind) + str);
        }
        finally
        {
          activity.Flags = activityFlags;
        }
      }
    }
  }

  public static DockControl FindParentDock(Control c)
  {
    while (!(c is DockControl))
    {
      if (c != null)
        c = c.Parent;
      if (c == null)
        return (DockControl) null;
    }
    return (DockControl) c;
  }

  public static Icon BitmapToIcon(Bitmap bmp) => ImageHelper.BitmapToIcon(bmp);

  public static void ShowProcesses(ISimpleSelectedItems items)
  {
    dbObjectId = (IDBObjectID) null;
    if (items != null && items.Count > 0 && items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID dbObjectId)
      ObjectRevisionHistoryView.ShowProcesses(dbObjectId.Value);
    if (dbObjectId != null)
      return;
    wfFunx.SayError(LocalizationHolder.rm.GetString("NoItemsSelected"));
  }

  public static void ShowRevisionHistory(ISimpleSelectedItems items)
  {
    dbObjectId = (IDBObjectID) null;
    if (items != null && items.Count > 0 && items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID dbObjectId)
      ObjectRevisionHistoryView.ShowRevisionHistory(dbObjectId.Value);
    if (dbObjectId != null)
      return;
    wfFunx.SayError(LocalizationHolder.rm.GetString("NoItemsSelected"));
  }

  public static void SaveTreePath(NavigatorTreeView tree)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      NavigatorTreeNode[] selectedNodes = tree.SelectedNodes;
      if (selectedNodes.Length < 1)
        return;
      PersistentState[] persistentStateArray = Intermech.Navigator.Utils.SerializePath(tree.GetNodeIDPath(selectedNodes[0]), tree.Services);
      if (persistentStateArray == null)
        return;
      IStateFormatter stateFormatter = (IStateFormatter) new BinaryStateFormatter();
      memoryStream.WriteByte(Convert.ToByte(persistentStateArray.Length));
      for (int index = 0; index < persistentStateArray.Length; ++index)
        stateFormatter.Serialize((Stream) memoryStream, persistentStateArray[index]);
      IDBConfigurations service = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      byte[] array = memoryStream.ToArray();
      BlobInformation config_info = new BlobInformation((long) array.Length, (long) array.Length, DateTime.Now, "SchemesTreePath", ArcMethods.NotPacked, "b");
      service?.WriteConfigData(config_info, array);
    }
  }

  public static bool RestoreTreePath(NavigatorTreeView tree)
  {
    IDBConfigurations service = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    byte[] config_file = new byte[0];
    service?.LoadConfigData("SchemesTreePath", out BlobInformation _, out config_file);
    if (config_file.Length != 0)
    {
      using (MemoryStream memoryStream = new MemoryStream(config_file))
      {
        int length = memoryStream.ReadByte();
        if (length > 0)
        {
          PersistentState[] persistPath = new PersistentState[length];
          IStateFormatter stateFormatter = (IStateFormatter) new BinaryStateFormatter();
          for (int index = 0; index < length; ++index)
          {
            PersistentState persistentState = stateFormatter.Deserialize((Stream) memoryStream);
            persistPath[index] = persistentState;
          }
          NodeIDPath path = Intermech.Navigator.Utils.DeserializePath(persistPath, tree.Services);
          tree.Build(path);
          return true;
        }
      }
    }
    return false;
  }

  public static bool CheckInAttachments(
    AttachmentList attachs,
    wfFunx.AttachCheckInProcessDelegate processEvent)
  {
    bool flag = true;
    string errorText = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (Attachment workCopy in (List<Attachment>) attachs.WorkCopies)
      {
        string objectCaption = MiscFunx.GetObjectCaption(sessionKeeper.Session, workCopy.ObjectID);
        processEvent(workCopy, objectCaption, true, false);
        bool result = true;
        try
        {
          ObjectCopyCommand checkinCommand = ObjectCommandFactory.CreateCheckinCommand(true);
          checkinCommand.ObjectId = workCopy.ObjectID;
          checkinCommand.Execute();
          workCopy.ObjectID = checkinCommand.NewObjectId;
        }
        catch (Exception ex)
        {
          result = false;
          flag = false;
          errorText = ex.Message;
        }
        processEvent(workCopy, objectCaption, false, result, errorText);
      }
    }
    return flag;
  }

  public static int FindActivitiesLinkedToForm(long formid, long ExcludeActivityID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(wfConsts.ParticipantActivitiesTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(wfConsts.AttrFormID, RelationalOperators.Equal, (object) formid, (object) null, LogicalOperators.AND, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(-2, RelationalOperators.NotEqual, (object) ExcludeActivityID, LogicalOperators.AND, 0, true)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }, 0L, (object) null, 0));
      if (dataTable.Rows.Count > 0)
      {
        object obj = dataTable.Rows[0][0];
        if (!obj.Equals((object) DBNull.Value))
          return Convert.ToInt32(obj);
      }
      return 0;
    }
  }

  public static bool ShowVariables(long activityID) => wfFunx.ShowVariables(activityID, true);

  public static bool ShowVariables(long activityID, bool readOnly)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (VariablesForm variablesForm = new VariablesForm(activityID, sessionKeeper.Session))
      {
        variablesForm.ReadOnly = readOnly;
        if (variablesForm.ShowDialog() == DialogResult.OK)
        {
          if (variablesForm.Modified)
            return true;
        }
      }
    }
    return false;
  }

  public static bool ShowVariables(long processID, long activityID, bool tryEdit)
  {
    bool readOnly = true;
    if (tryEdit)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(processID, false) is IDBSecurity dbSecurity)
          readOnly = !dbSecurity.CheckAccess(ActionType.Edit, false, false);
      }
    }
    return wfFunx.ShowVariables(activityID, readOnly);
  }

  public static OrganizerCalendarView OrganizerContext(System.IServiceProvider services)
  {
    return services?.GetService(typeof (OrganizerCalendarView)) as OrganizerCalendarView;
  }

  /// <summary>
  /// Записывает файл с диска в файловый атрибут объекта. Если objectID == 0, то создает новый объект (в случае выбора нескольких файлов - объекты).
  /// Возвращает список из ObjectID объектов, к которым крепились файлы
  /// </summary>
  /// <param name="typeID"></param>
  /// <param name="objectID"></param>
  public static List<long> AddFileToObject(int typeID, long objectID, bool allowMultiSelect = true)
  {
    List<long> longList = new List<long>();
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.Title = LocalizationHolder.rm.GetString("ChooseFile");
      openFileDialog.RestoreDirectory = true;
      bool flag = objectID == 0L;
      if (allowMultiSelect)
        openFileDialog.Multiselect = flag;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        foreach (string fileName1 in openFileDialog.FileNames)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            using (FileStream aSourceStream = new FileStream(fileName1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
              string fileName2 = Path.GetFileName(fileName1);
              IDBObject dbObject;
              if (objectID != 0L)
              {
                dbObject = sessionKeeper.Session.GetObject(objectID);
              }
              else
              {
                dbObject = sessionKeeper.Session.GetObjectCollection(typeID).Create();
                objectID = Math.Abs(dbObject.ObjectID);
                dbObject.CommitCreation(true);
              }
              longList.Add(objectID);
              dbObject.Caption = fileName2;
              FileInfo fileInfo = new FileInfo(fileName1);
              BlobInformation aBlobInformation = new BlobInformation(fileInfo.Length, 0L, fileInfo.LastWriteTime, $"{objectID.ToString()}\\{fileName2}", ArcMethods.ZLibPacked, "");
              new BlobProcWriter(objectID, AttributableElements.Object, wfConsts.AttrFileID, 0, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
              if (flag)
              {
                objectID = 0L;
              }
              else
              {
                DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", objectID);
                BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
              }
            }
          }
        }
      }
    }
    return longList;
  }

  internal static void TryViewByNavigator(long objectId)
  {
    wfFunx.TryInvokeNavigatorCommand(objectId, "ViewDocument");
  }

  internal static void TryEditByNavigator(long objectId)
  {
    wfFunx.TryInvokeNavigatorCommand(objectId, "EditDocument");
  }

  private static void TryInvokeNavigatorCommand(long objectId, string commandName)
  {
    ServiceContainer viewServices = new ServiceContainer();
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(Intermech.Navigator.ContextMenu.Services.GetItems(objectId), (System.IServiceProvider) viewServices);
    if (!commandsTable.Contains(commandName))
      return;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, (System.IServiceProvider) viewServices);
  }

  /// <summary>Получить все ИИ куда входит заданная версия объекта</summary>
  /// <param name="userSession"></param>
  /// <param name="verId"></param>
  /// <returns></returns>
  internal static List<long> GetParentIIs(IUserSession userSession, long verId)
  {
    List<long> parentIis = new List<long>();
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00349-306c-11d8-b4e9-00304f19f545");
    IDBRelationCollection relationCollection = userSession.GetRelationCollection(wfConsts.DocsInECORelationTypeID);
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) objectTypeId, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }), verId);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long num = Math.Abs(Convert.ToInt64(row[0]));
        parentIis.Add(num);
      }
    }
    return parentIis;
  }

  public delegate void AttachCheckInProcessDelegate(
    Attachment att,
    string caption,
    bool beforeProcess,
    bool result,
    string errorText = "");
}

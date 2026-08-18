// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.IMProject
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Metadata;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls;
using Intermech.Security;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public abstract class IMProject : Intermech.Project.Controls.IMProject
{
  public const string KeyMenuBarName = "View";
  public const string RootNodeTypeGuidStr = "E40E0222-8A4F-48DA-B12E-6DD1813AE9FD";

  public static Guid RootNodeTypeGuid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Navigator.Consts.IMProjectRootNodeGuid;
  }

  public static int RootNodeTypeID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Navigator.Consts.OrganizerRootNodeTypeID;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] internal set
    {
      Intermech.Navigator.Consts.OrganizerRootNodeTypeID = value;
    }
  }

  [CanBeNull]
  public static ClientProject LoadProject(long id, bool editingMode, bool expandSubProjects = false)
  {
    ClientProject clientProject = new ClientProject(Intermech.Project.Localization.GetString("Project"));
    clientProject.ProgressNotifier = (IProgressNotifier) ProgressNotifier.Notifier;
    clientProject.OnRequestEdit += new Intermech.Project.Project.RequestEditHandler(IMProject.Project_OnRequestEdit);
    clientProject.AutoLoadSubProjects = expandSubProjects;
    if (id != 0L)
    {
      try
      {
        clientProject.Load(id, new bool?(editingMode));
      }
      catch (Exception ex)
      {
        if (ex is KernelExceptionID kernelExceptionId && (kernelExceptionId.ErrorID == 63 /*0x3F*/ || kernelExceptionId.ErrorID == 346) || ex is AbortException)
        {
          if (MessageFuncs.SayError(Intermech.Project.Localization.GetString("CantEditProject", (object) clientProject.Name, (object) ex.Message), MessageBoxButtons.OKCancel) != DialogResult.OK)
            return (ClientProject) null;
          clientProject.Load(id, new bool?(false));
        }
        else
          throw;
      }
    }
    return clientProject;
  }

  public static bool? Project_OnRequestEdit([NotNull] Task t)
  {
    string str = t.Project?.Name ?? "?";
    string s;
    if (!(t is Intermech.Project.Project))
      s = Intermech.Project.Localization.GetString("CheckoutTaskPrompt", (object) t.Name, (object) str);
    else
      s = Intermech.Project.Localization.GetString("CheckoutPrompt", (object) t.Name);
    DialogResult dialogResult = MessageFuncs.Ask(s, MessageBoxButtons.YesNoCancel);
    return dialogResult == DialogResult.Cancel ? new bool?() : new bool?(dialogResult == DialogResult.Yes);
  }

  public static void OpenProject([NotNull] ISelectedItems items, bool edit)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(index);
      if (edit)
        IMProject.EditProject(itemData.ObjectID);
      else
        IMProject.ViewProject(itemData.ObjectID);
    }
  }

  [CanBeNull]
  public static ProjectEditorForm OpenProject(long id, bool isEditMode)
  {
    return IMProject.OpenProject(id, isEditMode, (AddToCompositionInfo) null);
  }

  [NotNull]
  private static ProjectEditorForm OpenProjectForm([NotNull] ClientProject project, ProjectEditorMode mode = ProjectEditorMode.Project)
  {
    ProjectEditorForm projectEditorForm = new ProjectEditorForm();
    projectEditorForm.TabImageIndex = mode == ProjectEditorMode.Project ? Intermech.Project.Controls.Images.ProjectImageIndex : Intermech.Project.Controls.Images.ResourcesImageIndex;
    projectEditorForm.ShowImageInDocumentTab = true;
    projectEditorForm.Mode = mode;
    projectEditorForm.Visible = true;
    DockManager service1 = ApplicationServices.Container.GetService<DockManager>();
    projectEditorForm.Show(service1);
    BarManager service2 = ApplicationServices.Container.GetService<BarManager>(false);
    if (service2 != null)
      projectEditorForm.Renderer = service2.Renderer;
    projectEditorForm.Project = project;
    projectEditorForm.Activate();
    Intermech.Client.Services.CommandManager.QueryStatus();
    return projectEditorForm;
  }

  [CanBeNull]
  public static ProjectEditorForm OpenProject(
    long id,
    bool isEditMode,
    [CanBeNull] AddToCompositionInfo addToComposition)
  {
    if (id != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetObject(id, false) is IDBSecurity dbSecurity) && id < 0L)
        {
          id = -id;
          dbSecurity = sessionKeeper.Session.GetObject(id, false) as IDBSecurity;
        }
        dbSecurity?.CheckAccess(isEditMode ? ActionType.Edit : ActionType.View);
      }
    }
    if (Editors.FindEditor(id, isEditMode) is ProjectEditorForm projectEditorForm && projectEditorForm.Parent is DockControl parent)
    {
      parent.Activate();
      projectEditorForm.UpdateCommands();
    }
    else
    {
      ClientProject project = IMProject.LoadProject(id, isEditMode);
      if (project == null)
        return (ProjectEditorForm) null;
      project.AddToComposition = addToComposition;
      projectEditorForm = IMProject.OpenProjectForm(project);
      project.HandleSuspendedErrors();
      if (id != 0L)
        RecentObjectsNode.MRUObjects.Add(id, isEditMode ? ObjectAction.View : ObjectAction.Edit, DateTime.UtcNow);
    }
    return projectEditorForm;
  }

  public static void EditProject(long id, [CanBeNull] AddToCompositionInfo addToComposition)
  {
    IMProject.OpenProject(id, true, addToComposition);
  }

  public static void EditProject(long id) => IMProject.EditProject(id, (AddToCompositionInfo) null);

  public static void ViewProject(long id) => IMProject.OpenProject(id, false);

  public static void StartProject(long id)
  {
    int num1 = IMProject.CheckInProject(id) ? 1 : 0;
    id = Math.Abs(id);
    ClientProject clientProject = IMProject.LoadProject(id, false);
    if (num1 != 0)
      clientProject.DoNotification(Task.EventKind.CheckIn, -id);
    clientProject.Execute();
    int num2 = (int) MessageFuncs.SayOK(Intermech.Project.Localization.GetString("ProjectStarted", (object) clientProject.Name));
  }

  public static void StartProject([NotNull] ISelectedItems items)
  {
    if (MessageFuncs.Ask(Intermech.Project.Localization.GetString("ProjectStartPrompt")) != DialogResult.Yes)
      return;
    for (int index = 0; index < items.Count; ++index)
      IMProject.StartProject(items.GetItemData<IDBTypedObjectID>(index).ObjectID);
  }

  public static void AbortProject(long id)
  {
    ClientProject clientProject = IMProject.LoadProject(id, false);
    clientProject.Abort();
    int num = (int) MessageFuncs.SayOK(Intermech.Project.Localization.GetString("ProjectAborted", (object) clientProject.Name));
  }

  public static void AbortProject([NotNull] ISelectedItems items)
  {
    if (MessageFuncs.Ask(Intermech.Project.Localization.GetString("ProjectAbortPrompt")) != DialogResult.Yes)
      return;
    for (int index = 0; index < items.Count; ++index)
      IMProject.AbortProject(items.GetItemData<IDBTypedObjectID>(index).ObjectID);
  }

  public static bool CheckInProject(long objID)
  {
    IMProject.CloseEditor(objID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(objID, false);
      if (dbObject != null)
      {
        if (dbObject.CheckoutBy == session.UserID)
        {
          dbObject.CheckIn();
          return true;
        }
      }
    }
    return false;
  }

  public static bool CloseEditor(long project)
  {
    if (!(Editors.FindEditor(project, true) is ProjectEditorForm editor) || !(editor.Parent is DockControl parent))
      return false;
    parent.Close();
    return true;
  }

  [CanBeNull]
  public static List<long> BrowseForUsers(bool allowGroups = false)
  {
    IDescriptor rootDescriptor = (IDescriptor) new UsersGroupsDescriptor();
    if (!allowGroups)
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(ListFactory.Create<int>((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User), true), true);
    object[] objArray = Intermech.Navigator.SelectionWindow.Select(Intermech.Project.Localization.GetString("ChooseUsers"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default);
    List<long> longList = (List<long>) null;
    if (objArray != null)
    {
      longList = new List<long>(objArray.Length);
      foreach (IDBObjectID dbObjectId in objArray)
        longList.Add(dbObjectId.Value);
    }
    return longList;
  }

  [NotNull]
  public static ProjectEditorForm ShowResourceAssignments([NotNull] List<long> userIDs)
  {
    ResourceAssignmentsProject assignmentsProject = new ResourceAssignmentsProject(userIDs);
    assignmentsProject._SessionProvider = ClientSessionProvider2.Provider;
    assignmentsProject.ProgressNotifier = (IProgressNotifier) ProgressNotifier.Notifier;
    ProjectEditorForm projectEditorForm = IMProject.OpenProjectForm((ClientProject) assignmentsProject, ProjectEditorMode.Resources);
    ResourcesSummaryProject resourcesSummaryProject = new ResourcesSummaryProject(assignmentsProject);
    assignmentsProject.Load();
    projectEditorForm.ResourcesSummaryView.Project = resourcesSummaryProject;
    projectEditorForm.ProjectView.ExpandAll();
    projectEditorForm.ProjectView.Project = (ClientProject) assignmentsProject;
    return projectEditorForm;
  }

  [CanBeNull]
  public static ProjectEditorForm ShowResourceAssignments()
  {
    List<long> userIDs = IMProject.BrowseForUsers(true);
    return userIDs == null ? (ProjectEditorForm) null : IMProject.ShowResourceAssignments(userIDs);
  }
}

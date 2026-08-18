// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectFuncs
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Client;

[Obsolete("Moved to Intermech.Project.Client.IMProject")]
internal class ProjectFuncs
{
  [CanBeNull]
  [Obsolete("Call Intermech.Project.Client.IMProject.LoadProject()")]
  public static ClientProject LoadProject([CanBeEmpty] long id, bool editingMode, bool expandSubProjects = false)
  {
    return IMProject.LoadProject(id, editingMode, expandSubProjects);
  }

  [Obsolete("Call Intermech.Project.Client.IMProject.Project_OnRequestEdit()")]
  public static bool? Project_OnRequestEdit([NotNull] Task t) => IMProject.Project_OnRequestEdit(t);

  [Obsolete("Call Intermech.Project.Client.IMProject.OpenProject()")]
  public static void OpenProject([NotNull] ISelectedItems items, bool edit)
  {
    IMProject.OpenProject(items, edit);
  }

  [CanBeNull]
  [Obsolete("Call Intermech.Project.Client.IMProject.OpenProject()")]
  public static ProjectEditorForm OpenProject(long id, bool isEditMode)
  {
    return IMProject.OpenProject(id, isEditMode, (AddToCompositionInfo) null);
  }

  [CanBeNull]
  [Obsolete("Call Intermech.Project.Client.IMProject.OpenProject()")]
  public static ProjectEditorForm OpenProject(
    long id,
    bool isEditMode,
    [CanBeNull] AddToCompositionInfo addToComposition)
  {
    return IMProject.OpenProject(id, isEditMode, addToComposition);
  }

  [Obsolete("Call Intermech.Project.Client.IMProject.EditProject()")]
  public static void EditProject(long id, [CanBeNull] AddToCompositionInfo addToComposition)
  {
    IMProject.OpenProject(id, true, addToComposition);
  }

  [Obsolete("Call Intermech.Project.Client.IMProject.EditProject()")]
  public static void EditProject(long id) => IMProject.EditProject(id, (AddToCompositionInfo) null);

  [Obsolete("Call Intermech.Project.Client.IMProject.ViewProject()")]
  public static void ViewProject(long id) => IMProject.OpenProject(id, false);

  [Obsolete("Call Intermech.Project.Client.IMProject.StartProject()")]
  public static void StartProject(long id) => IMProject.StartProject(id);

  [Obsolete("Call Intermech.Project.Client.IMProject.StartProject()")]
  public static void StartProject([NotNull] ISelectedItems items) => IMProject.StartProject(items);

  [Obsolete("Call Intermech.Project.Client.IMProject.AbortProject()")]
  public static void AbortProject(long id) => IMProject.AbortProject(id);

  [Obsolete("Call Intermech.Project.Client.IMProject.AbortProject()")]
  public static void AbortProject([NotNull] ISelectedItems items) => IMProject.AbortProject(items);

  [Obsolete("Call Intermech.Project.Client.IMProject.CheckInProject()")]
  public static bool CheckInProject(long objID) => IMProject.CheckInProject(objID);

  [Obsolete("Call Intermech.Project.Client.IMProject.CloseEditor()")]
  public static bool CloseEditor(long project) => IMProject.CloseEditor(project);

  [CanBeNull]
  [Obsolete("Call Intermech.Project.Client.IMProject.BrowseForUsers()")]
  public static List<long> BrowseForUsers(bool allowGroups = false)
  {
    return IMProject.BrowseForUsers(allowGroups);
  }

  [NotNull]
  [Obsolete("Call Intermech.Project.Client.IMProject.ShowResourceAssignments()")]
  public static ProjectEditorForm ShowResourceAssignments([NotNull] List<long> userIDs)
  {
    return IMProject.ShowResourceAssignments(userIDs);
  }

  [CanBeNull]
  [Obsolete("Call Intermech.Project.Client.IMProject.ShowResourceAssignments()")]
  public static ProjectEditorForm ShowResourceAssignments() => IMProject.ShowResourceAssignments();
}

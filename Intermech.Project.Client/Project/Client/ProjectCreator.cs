// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectCreator
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

internal class ProjectCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool AcceptDialog(
    int objectType,
    long templateObject,
    [NotNull] int[] relationTypeIDs,
    [NotNull] long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    return templateObject == -1L;
  }

  public bool AfterCreate(long newObjectID) => true;

  [CanBeNull]
  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get => (IDictionary<ObjectCreatePages, bool>) null;
  }

  public bool OnCommitAction(
    [NotNull] IUserSession session,
    long newObjectID,
    [NotNull] List<NotificationEventArgs> nea)
  {
    return false;
  }

  public bool OnCancelAction(
    [NotNull] IUserSession session,
    long newObjectID,
    [NotNull] List<NotificationEventArgs> nea)
  {
    return false;
  }

  [CanBeNull]
  public Dictionary<UserControl, int> AddPages([CanBeNull] object createdObject)
  {
    return (Dictionary<UserControl, int>) null;
  }

  [CanBeNull]
  public Dictionary<UserControl, int> AddPages([CanBeNull] object createdObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }

  public long CreateObjectDialog(
    int objectType,
    long templateObject,
    [NotNull] int[] relationTypeIDs,
    [NotNull] long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    AddToCompositionInfo addToComposition = (AddToCompositionInfo) null;
    if (relationTypeIDs.Length != 0 && relatedObjectIDs.Length != 0)
      addToComposition = new AddToCompositionInfo(relationTypeIDs[0], relatedObjectIDs[0]);
    IMProject.EditProject(0L, addToComposition);
    return 0;
  }
}

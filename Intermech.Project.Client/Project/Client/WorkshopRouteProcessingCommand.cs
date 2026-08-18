// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.WorkshopRouteProcessingCommand
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Project.Controls;
using Intermech.Techcard;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

internal class WorkshopRouteProcessingCommand
{
  [NotNull]
  private static readonly string StrObjectTypeNotFound = Intermech.Project.Localization.GetString("ObjTypeNotFound");
  [NotNull]
  public static readonly string StrRouteElementObjTypeName = Intermech.Project.Localization.GetString("TechRouteElement");
  [NotNull]
  public static readonly string StrBasicTechprocessObjTypeName = Intermech.Project.Localization.GetString("BaseTechprocess");
  [NotNull]
  public static readonly string StrTechRouteObjTypeName = Intermech.Project.Localization.GetString("BaseTechprocess");
  [NotNull]
  private readonly ProjectEditorForm _projectEditorForm;

  public WorkshopRouteProcessingCommand([NotNull] ProjectEditorForm projectEditorForm)
  {
    this._projectEditorForm = projectEditorForm;
  }

  [CanBeNull]
  private Intermech.Project.Project Project
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (Intermech.Project.Project) this._projectEditorForm.Project;
    }
  }

  [NotNull]
  private System.IServiceProvider Services
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Intermech.Diagnostics.Check.Result.NotNull<System.IServiceProvider>(this._projectEditorForm.Services);
    }
  }

  public bool Visible
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return SpecialCommands.ShowWorkshopRouteProcessingCommand;
    }
  }

  public bool Enabled
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Visible && this.Project != null && this.Project.Tasks.Count > 0 && this.Project.ImportedObjects.Count > 0;
    }
  }

  internal static void ValidateTechcardMetadata()
  {
    if ((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.RouteElement == -1)
      throw new Exception(string.Format(WorkshopRouteProcessingCommand.StrObjectTypeNotFound, (object) WorkshopRouteProcessingCommand.StrRouteElementObjTypeName));
    if ((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.BasicTechprocess == -1)
      throw new Exception(string.Format(WorkshopRouteProcessingCommand.StrObjectTypeNotFound, (object) WorkshopRouteProcessingCommand.StrBasicTechprocessObjTypeName));
    if ((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.TechRoute == -1)
      throw new Exception(string.Format(WorkshopRouteProcessingCommand.StrObjectTypeNotFound, (object) WorkshopRouteProcessingCommand.StrTechRouteObjTypeName));
  }

  public void Execute()
  {
    if (this.Enabled)
    {
      using (SelectImportedObjectForm importedObjectForm = new SelectImportedObjectForm(this.Services, "WorkshopRouteProcessing", true))
      {
        importedObjectForm.OperationHint = Intermech.Project.Localization.GetString("SelectImportedObjectForProcessing");
        importedObjectForm.IsReadOnly = true;
        if (importedObjectForm.ShowDialog() != DialogResult.OK)
          return;
        IReadOnlyList<long> objectVersionIds = importedObjectForm.SelectedObjectVersionIDs;
        if (objectVersionIds.Count <= 0)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (ImportedObject importedObject in objectVersionIds.SelectNotNull<long, ImportedObject>((Func<long, ImportedObject>) (objVerID => this.Project.GetImportedObjectDescriptor(objVerID))))
            this.ExecuteOnObject(importedObject, sessionKeeper.Session);
        }
      }
    }
    else
      Intermech.Client.Services.CommandManager.QueryStatus();
  }

  public void ExecuteOnObject([NotNull] ImportedObject importedObject, [NotNull] IUserSession session)
  {
    List<TechComposition.TechRouteElement> techRouteElements = TechComposition.GetObjectDefaultTechRouteElements(importedObject.ObjectVersionID, session, resultObjectIdType: ObjectIDType.ObjectVersionID);
    if (techRouteElements.Count <= 0)
      return;
    HashSet<long> techRouteElementIDs = new HashSet<long>((IEnumerable<long>) new HashSet<long>(techRouteElements.Select<TechComposition.TechRouteElement, long>((Func<TechComposition.TechRouteElement, long>) (techRouteElement => Math.Abs(techRouteElement.TechRouteElementID))).Distinct<long>()));
    this.Project.RemoveTasks((IEnumerable<Task>) this.Project.Tasks.Where<Task>((Func<Task, bool>) (task => task.ImportedRootObjectVersionGuid.Equals(importedObject.ObjectVersionGuid) && WorkshopRouteProcessingCommand.TaskMustBeDeleted(task, techRouteElementIDs))).ToList<Task>());
  }

  private static bool TaskMustBeDeleted([NotNull] Task task, [NotNull] HashSet<long> techRouteElementIDs)
  {
    if (task.Attachments.Count == 0)
      return false;
    long num1 = 0;
    List<long> source = (List<long>) null;
    int num2 = 0;
    foreach (Attachment attachment in (List<Attachment>) task.Attachments)
    {
      if (TechConsts.TypeIsTechRouteElement(attachment.TypeID))
      {
        if (num1 == 0L)
        {
          num1 = Math.Abs(attachment.ObjectID);
        }
        else
        {
          if (source == null)
          {
            source = new List<long>(task.Attachments.Count - num2 + 1);
            source.Add(num1);
          }
          source.Add(Math.Abs(attachment.ObjectID));
        }
      }
      ++num2;
    }
    if (source != null && source.Count > 0)
      return source.All<long>((Func<long, bool>) (elementID => !techRouteElementIDs.Contains(elementID)));
    return num1 != 0L && !techRouteElementIDs.Contains(num1);
  }
}

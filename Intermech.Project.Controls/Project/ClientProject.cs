// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ClientProject
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Helpers;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class ClientProject : Intermech.Project.Project
{
  [NotNull]
  [NonSerialized]
  public ProjectDisplayOptions DisplayOptions;
  /// <summary>
  /// Здесь хранится информация о связи, которую надо будет создать после сохранения проекта
  /// т.е. проект создавался командой "Создать в составе"
  /// </summary>
  [CanBeNull]
  [NonSerialized]
  public AddToCompositionInfo AddToComposition;
  [CanBeNull]
  private Dictionary<Task, int> _visibleTaskIndexes;

  public ClientProject()
  {
    this.DisplayOptions = new ProjectDisplayOptions((Intermech.Project.Project) this);
    this._SessionProvider = ClientSessionProvider2.Provider;
  }

  public ClientProject([NotNull] string name)
    : base(Intermech.Diagnostics.Check.Optional.ArgumentNotNull<string>(name, nameof (name)))
  {
    this.DisplayOptions = new ProjectDisplayOptions((Intermech.Project.Project) this);
    this._SessionProvider = ClientSessionProvider2.Provider;
  }

  protected override void SaveData(XmlIni ini)
  {
    base.SaveData(ini);
    this.DisplayOptions.Save(ini);
  }

  protected override void AfterSave(IUserSession session, IDBObject obj)
  {
    if (this.AddToComposition == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.AddToComposition.RelationTypeID);
        try
        {
          relationCollection.Create(this.AddToComposition.ObjectID, this.ObjectID);
        }
        catch (Exception ex)
        {
          if ((ex.InnerException is KernelExceptionID innerException ? (innerException.ErrorID != 47 ? 1 : 0) : 1) == 0)
            return;
          throw;
        }
      }
      finally
      {
        this.AddToComposition = (AddToCompositionInfo) null;
      }
    }
  }

  protected override void Loaded()
  {
    base.Loaded();
    if (this.HasState(TaskState.Loading) || this.DisplayOptions.Loaded)
      return;
    this.DisplayOptions.Load(this.ProjectData);
  }

  [CanBeNull]
  [ItemNotNull]
  internal static IDBTypedObjectID[] BrowseForResources([CanBeNull] Intermech.Project.Project project, bool usersOnly = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> applicableResourceTypes = Intermech.Project.Project.GetApplicableResourceTypes(sessionKeeper.Session);
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (int objTypeID in applicableResourceTypes)
      {
        if (objTypeID == (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User && project != null)
        {
          long objectID = 0;
          AddToCompositionInfo toCompositionInfo = (AddToCompositionInfo) null;
          if (project is ClientProject clientProject)
            toCompositionInfo = clientProject.AddToComposition;
          if (toCompositionInfo != null && toCompositionInfo.ObjectTypeID == (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project)
            objectID = toCompositionInfo.ObjectID;
          if (objectID == 0L)
          {
            DataTable dataTable = sessionKeeper.Session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) ObjectTypes.Project).Select(new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) project.RootProject.ID, LogicalOperators.NONE, 0, false)
            }, new object[1]{ (object) -2 }, 0L, (object) null, 1));
            if (dataTable.Rows.Count > 0)
              objectID = Convert.ToInt64(dataTable.Rows[0][0]);
          }
          if (objectID != 0L)
          {
            IProject project1 = Intermech.Diagnostics.Check.Is<IProject>((object) sessionKeeper.Session.GetObject(objectID, true));
            ParcipiantInfo[] parcipiants = project1.Parcipiants;
            List<long> list = ((IEnumerable<ParcipiantInfo>) parcipiants).Select<ParcipiantInfo, long>((System.Func<ParcipiantInfo, long>) (pi => pi.ObjectVerID)).ToList<long>(parcipiants.Length);
            UniversalDescriptor universalDescriptor = new UniversalDescriptor(new Guid("{41EB2798-9533-4b6e-BAC3-39ECA3F1446F}"), (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User, string.Format(Localization.GetString("ProjectMembers"), (object) project1.Caption), (IList) list);
            descriptors.Add((IDescriptor) universalDescriptor);
            continue;
          }
        }
        if (objTypeID == (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User)
          descriptors.Add((IDescriptor) new UsersGroupsDescriptor());
        else if (!usersOnly)
          descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objTypeID));
      }
      if (applicableResourceTypes.Count <= 0)
        return (IDBTypedObjectID[]) null;
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(applicableResourceTypes, true), true);
      return (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select(Localization.GetString("ChooseResources"), (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(Localization.GetString("ResourcesTitle"), descriptors), typeof (IDBTypedObjectID), SelectionOptions.Default);
    }
  }

  public override string DateFormat => this.DisplayOptions.DateFormat;

  public override bool StopProgress()
  {
    int num = base.StopProgress() ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (this.DisplayOptions.View == null)
      return num != 0;
    this.DisplayOptions.View.Invalidate(true);
    return num != 0;
  }

  public override void UpdateControls()
  {
    base.UpdateControls();
    this.DisplayOptions.UpdateControls();
  }

  public override void BeforeSetTaskProperty([NotNull] Task task, [NotNull] string property, [CanBeNull] object value)
  {
    base.BeforeSetTaskProperty(task, property, value);
    if (this.HasState(TaskState.Loading))
      return;
    switch (property)
    {
      case "Start":
        DateTime dateTime1 = (DateTime) value;
        if (this.PlanningType != PlanningType.FromStart || !(dateTime1 < this.Start))
          break;
        ControlFuncs.ReleaseCapture();
        if (MessageBox.Show(string.Format(Intermech.Project.Controls.Properties.Resources.MoveProjectStartPrompt, (object) this.Start, (object) dateTime1), string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
          break;
        this.Start = dateTime1;
        break;
      case "Finish":
        DateTime dateTime2 = (DateTime) value;
        if (this.PlanningType != PlanningType.FromEnd || !(dateTime2 > this.Finish))
          break;
        ControlFuncs.ReleaseCapture();
        if (MessageBox.Show(string.Format(Intermech.Project.Controls.Properties.Resources.MoveProjectFinishPrompt, (object) this.Finish, (object) dateTime2), string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
          break;
        this.Finish = dateTime2;
        break;
    }
  }

  [NotNull]
  internal Dictionary<Task, int> VisibleTaskIndexes
  {
    get
    {
      if (this._visibleTaskIndexes == null)
      {
        this._visibleTaskIndexes = new Dictionary<Task, int>();
        int num = 0;
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Tasks)
        {
          if (!task.IsHidden)
          {
            this._visibleTaskIndexes[task] = num;
            ++num;
          }
        }
      }
      return this._visibleTaskIndexes;
    }
  }

  internal void ClearVisibleTaskIndexes()
  {
    this._visibleTaskIndexes = (Dictionary<Task, int>) null;
  }

  internal void ExtendRowHeightForCaptions(ref int h, int minimalHeight)
  {
    int num = minimalHeight + this.DisplayOptions.TaskCaptions.Padding.Top + this.DisplayOptions.TaskCaptions.Padding.Height;
    if (h >= num)
      return;
    h = num;
  }

  internal void PropertiesChanged() => this.PropertiesChanged(Task.CalcProps.All, true);
}

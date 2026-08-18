// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ResourceAssignmentsProject
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Project.Controls;

public class ResourceAssignmentsProject : ClientProject
{
  [NotNull]
  protected List<long> _ObjectIDs;
  [NotNull]
  [ItemNotNull]
  protected readonly List<Task> _ResourceAssignmentsTasks = new List<Task>();
  protected bool _GroupByProject = true;
  [NotNull]
  [ItemNotNull]
  protected List<ResourceAssignmentsProject.UserInfo> OrigUserInfos = new List<ResourceAssignmentsProject.UserInfo>();
  [CanBeNull]
  internal ResourcesSummaryProject SummaryProject;

  [NotNull]
  [ItemNotNull]
  public List<Task> ResourceAssignmentsTasks => this._ResourceAssignmentsTasks;

  /// <summary>Идентификаторы объектов, загрузка которых вычисляется</summary>
  [NotNull]
  public List<long> ObjectIDs => this._ObjectIDs;

  public ResourceAssignmentsProject([NotNull, ItemNotEmpty] List<long> userIDs)
  {
    this.EditingMode = EditingMode.None;
    this._AutoLoadSubTasks = true;
    this._ObjectIDs = userIDs;
  }

  public bool GroupByProject => this._GroupByProject;

  /// <summary>развернутый список ресурсов (группы пользователей раскрыты)</summary>
  [NotNull]
  [ItemNotNull]
  public List<ResourceAssignmentsProject.UserInfo> UserInfos { get; protected set; } = new List<ResourceAssignmentsProject.UserInfo>();

  [NotNull]
  public string UserNames
  {
    get
    {
      List<string> list = this.OrigUserInfos.Where<ResourceAssignmentsProject.UserInfo>((System.Func<ResourceAssignmentsProject.UserInfo, bool>) (ui => ui.Name != string.Empty)).Select<ResourceAssignmentsProject.UserInfo, string>((System.Func<ResourceAssignmentsProject.UserInfo, string>) (ui => ui.Name)).ToList<string>();
      list.Sort();
      return string.Join(", ", list.ToArray());
    }
  }

  private static int CompareUserInfoByName(
    [NotNull] ResourceAssignmentsProject.UserInfo ui1,
    [NotNull] ResourceAssignmentsProject.UserInfo ui2)
  {
    return string.Compare(ui1.Name, ui2.Name, StringComparison.Ordinal);
  }

  public virtual void Load()
  {
    IUserSession session = this.GetSession();
    try
    {
      this.StartProgress(1, string.Empty);
      try
      {
        this._Start = DateTime.MinValue;
        this.OrigUserInfos = new List<ResourceAssignmentsProject.UserInfo>();
        this.UserInfos = new List<ResourceAssignmentsProject.UserInfo>();
        List<long> ds = new List<long>((IEnumerable<long>) this._ObjectIDs);
        int count = ds.Count;
        for (int index = 0; index < ds.Count; ++index)
        {
          long num1 = ds[index];
          IDBObject dbObject = session.GetObject(num1, false);
          if (dbObject != null)
          {
            ResourceAssignmentsProject.UserInfo userInfo = new ResourceAssignmentsProject.UserInfo(num1, dbObject.ID, dbObject.Caption);
            if (index < count)
              this.OrigUserInfos.Add(userInfo);
            if (dbObject.TypeID == (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.UserGroup)
            {
              foreach (long num2 in MiscFunx.ExpandGroup(session, num1).Where<long>((System.Func<long, bool>) (id => !ds.Contains(id))))
                ds.Add(num2);
            }
            else
              this.UserInfos.Add(userInfo);
          }
        }
        this.OrigUserInfos.Sort(new Comparison<ResourceAssignmentsProject.UserInfo>(ResourceAssignmentsProject.CompareUserInfoByName));
        this.UserInfos.Sort(new Comparison<ResourceAssignmentsProject.UserInfo>(ResourceAssignmentsProject.CompareUserInfoByName));
        foreach (ResourceAssignmentsProject.UserInfo userInfo in this.UserInfos)
        {
          DataTable dataTable = session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task).Select(new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) userInfo.ID, LogicalOperators.AND, 0, true)
          }, new ColumnDescriptor[4]
          {
            new ColumnDescriptor((object) -2),
            new ColumnDescriptor((object) Intermech.Project.Attributes.Project.ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
            new ColumnDescriptor((object) Intermech.Project.Attributes.Project.ID, AttributeSourceTypes.Auto, ColumnContents.String, ColumnNameMapping.Default, SortOrders.NONE, 0),
            new ColumnDescriptor((object) Intermech.Project.Attributes.PlanStart.ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 0)
          }));
          Dictionary<long, ResourceAssignmentsTask> dictionary = new Dictionary<long, ResourceAssignmentsTask>();
          List<Task> subtasks = new List<Task>();
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              ResourceAssignmentsTask resourceAssignmentsTask1 = new ResourceAssignmentsTask(Convert.ToInt64(row[0]));
              if (this.GroupByProject)
              {
                long num = row[1] == DBNull.Value ? resourceAssignmentsTask1.ObjectID : Convert.ToInt64(row[1]);
                ResourceAssignmentsTask resourceAssignmentsTask2;
                if (!dictionary.TryGetValue(num, out resourceAssignmentsTask2))
                {
                  resourceAssignmentsTask2 = (ResourceAssignmentsTask) new ResourceAssignmentsSubProject(num);
                  dictionary.Add(num, resourceAssignmentsTask2);
                  subtasks.Add((Task) resourceAssignmentsTask2);
                }
                resourceAssignmentsTask2.ResourceAssignmentsTasks.Add((Task) resourceAssignmentsTask1);
              }
              else
                subtasks.Add((Task) resourceAssignmentsTask1);
            }
          }
          UserSummaryTask userSummaryTask = new UserSummaryTask(userInfo.ObjectID, userInfo.Name ?? string.Empty, subtasks);
          this._ResourceAssignmentsTasks.Add((Task) userSummaryTask);
          userInfo.Task = userSummaryTask;
        }
        try
        {
          this.Loading();
          this.Name = $"{Localization.GetString("ResourceAssignments")} \"{this.UserNames.Truncate(120)}\"";
          this.StartProgress(this.AllSubTasksCount, this.Name);
          try
          {
            this.HasNotLoadedSubTasks = true;
            this.LoadSubTasks((Intermech.Project.Project) this);
            Intermech.Project.Project.ProjectCache cache = this._Cache;
            if ((cache != null ? (cache.Start.HasValue ? 1 : 0) : 0) != 0)
              this._Cache.Start = new DateTime?();
            if (!(this.Start == DateTime.MinValue))
              return;
            this.Start = DateTime.Now.Date;
          }
          finally
          {
            this.StopProgress();
          }
        }
        finally
        {
          this.Loaded();
          this.Modified = false;
        }
      }
      finally
      {
        this.StopProgress();
      }
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  protected internal override bool CheckOut(ref IDBObject obj) => false;

  protected override void LoadSubTasksInternal(IUserSession session, Intermech.Project.Project project)
  {
    foreach (Task task in this._ResourceAssignmentsTasks.OfType<ResourceAssignmentsTask>())
      task.LoadAsSubTask((Task) this, project);
  }

  protected override IReadOnlyList<Task> GetSubTasks()
  {
    return (IReadOnlyList<Task>) this._ResourceAssignmentsTasks;
  }

  public override void OnTaskLoaded([NotNull] Task task)
  {
    base.OnTaskLoaded(task);
    this.MoveProjectStart(task);
  }

  protected void MoveProjectStart([NotNull] Task task)
  {
    DateTime start = task.Start;
    if (!this._Start.Equals(DateTime.MinValue) && !(this._Start > start) || start.Year <= 1)
      return;
    this._Start = task.Start;
  }

  public int AllSubTasksCount
  {
    get
    {
      return this._ResourceAssignmentsTasks.OfType<ResourceAssignmentsTask>().Sum<ResourceAssignmentsTask>((System.Func<ResourceAssignmentsTask, int>) (rt => 1 + rt.AllSubTasksCount));
    }
  }

  protected override void Loaded()
  {
    base.Loaded();
    if (this.HasState(TaskState.Loading) || this.SummaryProject == null)
      return;
    this.SummaryProject.Assign(this);
  }

  public override void Clear()
  {
    bool raiseItemEvents = this.Tasks.RaiseItemEvents;
    try
    {
      this.Tasks.RaiseItemEvents = false;
      base.Clear();
      this._ResourceAssignmentsTasks.Clear();
    }
    finally
    {
      this.Tasks.RaiseItemEvents = raiseItemEvents;
    }
  }

  public void Reload()
  {
    this.Clear();
    this.Load();
  }

  [CanBeNull]
  protected override string CheckForDuplicates(Task task, int newIndex) => (string) null;

  public override void BeforeSetTaskProperty(Task task, string property, object value)
  {
  }

  public override bool CanSetProperty(string name, object value, bool silent) => true;

  public class UserInfo
  {
    [NotEmpty]
    public readonly long ObjectID;
    [NotEmpty]
    public readonly long ID;
    [NotNull]
    public readonly string Name;
    public UserSummaryTask Task;

    public UserInfo([NotEmpty] long objectID, [NotEmpty] long ID, [NotNull] string name)
    {
      this.ObjectID = objectID;
      this.ID = ID;
      this.Name = name;
    }
  }
}

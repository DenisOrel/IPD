
// Type: Intermech.Navigator.DBObjects.VersionsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дискриптор окна навигатора для отображения версий объектов в виде дерева или списка
/// </summary>
internal abstract class VersionsDescriptor : HiveDescriptor, IVersionsDescriptor
{
  private const string _propObjID = "ObjectID";
  private const string _propID = "ID";
  private const string _propMode = "Mode";
  private const string _propOnDate = "OnDate";
  private const string _propCaption = "ObjectCaption";

  public long ObjectID { get; private set; }

  public long ID { get; private set; }

  public VersionsWindowVisualModes VisualMode { get; private set; }

  public DateTime CurrentDate { get; set; }

  public string ObjectCaption { get; private set; }

  public abstract string Path { get; }

  public NodeColumnCollection TreeColumns
  {
    get => Utils.VersionColumns(NodeColumnSortOrder.None, this.IsList);
  }

  protected virtual bool IsList { get; }

  public int ObjectTypeID { get; private set; }

  public VersionsDescriptor(
    long objectID,
    int objectTypeID,
    VersionsWindowVisualModes mode,
    DateTime onDate)
    : this(objectID, 0L, objectTypeID, string.Empty, mode, onDate)
  {
  }

  public VersionsDescriptor(
    long objectID,
    long id,
    int objectTypeID,
    string caption,
    VersionsWindowVisualModes mode,
    DateTime onDate)
    : base(Intermech.Navigator.Consts.CategoryVersionsObjectNode, objectTypeID, string.Empty)
  {
    this.VisualMode = mode;
    this.CurrentDate = onDate;
    this.ObjectID = objectID;
    this.ObjectTypeID = objectTypeID;
    if (id == 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
        this.ObjectCaption = objectInfo.Caption;
        this.ID = objectInfo.ID;
      }
    }
    else
    {
      this.ID = id;
      this.ObjectCaption = caption;
    }
    this.Initialize();
  }

  /// <summary>
  /// Вернуть описание корневого узла для текущего дескриптора
  /// </summary>
  /// <returns></returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new VersionsHiveNodeID(this._categoryID, this._typeID, this.VisualMode);
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state">Строка с сохранённым состоянием дескриптора</param>
  public VersionsDescriptor(PersistentState state)
    : base(state)
  {
    this.InitializeFromPersistentState(state);
  }

  protected void InitializeFromPersistentState(PersistentState state)
  {
    this.VisualMode = (VersionsWindowVisualModes) Enum.Parse(typeof (VersionsWindowVisualModes), (string) state.GetValue("Mode"));
    this.ObjectID = (long) state.GetValue("ObjectID");
    this.ID = (long) state.GetValue("ID");
    this.CurrentDate = DateTime.MaxValue;
    this.ObjectCaption = (string) state.GetValue("ObjectCaption");
    if (state.Contains("OnDate"))
    {
      object obj = state.GetValue("OnDate");
      this.CurrentDate = obj != null ? Convert.ToDateTime((string) obj) : DateTime.MaxValue;
    }
    this.Initialize();
  }

  public override object MapColumnToField(NodeColumn column)
  {
    object field = base.MapColumnToField(column);
    if (field != null)
      return field;
    return (column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) ? (object) "F_CAPTION" : (object) null;
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new VersionsNode(-1L, this.ID, nodeID.TypeID, this.VisualMode, this.CurrentDate);
  }

  protected bool Equals(VersionsDescriptor descriptor)
  {
    return this._categoryID == descriptor._categoryID && this._typeID == descriptor._typeID && Math.Abs(this.ObjectID) == Math.Abs(descriptor.ObjectID) && this.ID == descriptor.ID && this.VisualMode == descriptor.VisualMode && this.CurrentDate == descriptor.CurrentDate;
  }

  public override int GetHashCode() => this.ObjectID.GetHashCode() ^ this.VisualMode.GetHashCode();

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("ObjectID", (object) this.ObjectID);
    state.AddValue("ID", (object) this.ID);
    state.AddValue("ObjectCaption", (object) this.ObjectCaption);
    state.AddValue("Mode", (object) Enum.GetName(typeof (VersionsWindowVisualModes), (object) this.VisualMode));
    if (!(this.CurrentDate != DateTime.MaxValue))
      return;
    state.AddValue("OnDate", (object) this.CurrentDate.ToString());
  }

  public override object GetData(INodeID nodeID, Type dataFormat) => (object) null;

  protected abstract void Initialize();
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivitiesListNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

internal class ActivitiesListNode : ObjectsListNode
{
  private new IList objectIDs;
  private long _attachedObjectID;
  private ActivitiesDescriptor _parent;
  private ActivitiesListPart _part;

  public ActivitiesListNode(ActivitiesDescriptor parent, IList objectIDs)
    : base(objectIDs)
  {
    this.objectIDs = objectIDs;
    this._parent = parent;
  }

  public ActivitiesListNode(ActivitiesDescriptor parent, long AttachedObjectID)
    : base((IList) null)
  {
    this._attachedObjectID = AttachedObjectID;
    this._parent = parent;
  }

  internal ActivitiesListPart Part
  {
    get
    {
      if (this._part == null)
        this._part = new ActivitiesListPart(this._parent, this._attachedObjectID, this.Services);
      return this._part;
    }
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    if (this._attachedObjectID > 0L)
      return this.SlotsFromSinglePart((INodePart) this.Part);
    if (this.objectIDs == null || this.objectIDs.Count <= 0)
      return (List<PartSlot>) null;
    List<PartSlot> folderSlots = new List<PartSlot>();
    foreach (object objectId in (IEnumerable) this.objectIDs)
    {
      long int64 = Convert.ToInt64(objectId);
      folderSlots.AddRange((IEnumerable<PartSlot>) this.SlotsFromSinglePart((INodePart) new ActivitiesListPart(this._parent, int64, this.Services)));
    }
    return folderSlots;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    if (this._attachedObjectID > 0L)
      return this.SlotsFromSinglePart((INodePart) this.Part);
    if (this.objectIDs == null || this.objectIDs.Count <= 0)
      return (List<PartSlot>) null;
    List<PartSlot> nonFolderSlots = new List<PartSlot>();
    foreach (object objectId in (IEnumerable) this.objectIDs)
    {
      long int64 = Convert.ToInt64(objectId);
      nonFolderSlots.AddRange((IEnumerable<PartSlot>) this.SlotsFromSinglePart((INodePart) new ActivitiesListPart(this._parent, int64, this.Services)));
    }
    return nonFolderSlots;
  }

  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return this.Part.GetDefaultColumns();
  }

  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return this.Part.GetSupportedColumns(ColumnSetName);
  }
}

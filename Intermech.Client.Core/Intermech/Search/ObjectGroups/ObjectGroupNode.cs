
// Type: Intermech.Search.ObjectGroups.ObjectGroupNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupNode : CompositeNode, IContextAware
{
  private int _projectTypeID = -1;
  private int _relationTypeID = -1;
  private int _partTypeID = -1;
  private long _projectVersionID;

  public ObjectGroupNode(
    int projectTypeID,
    int relationTypeID,
    int partTypeID,
    long projectVersionID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(projectTypeID))
      throw new ArgumentException();
    if (RelationTypeHelper.IsUnknownRelationTypeID(relationTypeID))
      throw new ArgumentException();
    if (ObjectTypeHelper.IsUnknownObjectTypeID(partTypeID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(projectVersionID))
      throw new ArgumentException();
    this._projectTypeID = projectTypeID;
    this._relationTypeID = relationTypeID;
    this._partTypeID = partTypeID;
    this._projectVersionID = projectVersionID;
  }

  protected override List<PartSlot> CreateFolderSlots() => this.CreateSlots();

  protected override List<PartSlot> CreateNonFolderSlots() => this.CreateSlots();

  public IServiceProvider Services { get; set; }

  private List<PartSlot> CreateSlots()
  {
    return new List<PartSlot>()
    {
      new PartSlot(MetaDataHelper.GetRelationType(this._relationTypeID).Guid, (INodePart) new RelatedObjectsPart(this._projectTypeID, this._projectVersionID, RelatedObjectsRole.Composition, this._relationTypeID, this._partTypeID, (ConditionStructure[]) null, this.Services))
    };
  }
}

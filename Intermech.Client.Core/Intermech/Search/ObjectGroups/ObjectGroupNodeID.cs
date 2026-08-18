
// Type: Intermech.Search.ObjectGroups.ObjectGroupNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupNodeID : INodeID
{
  public ObjectGroupNodeID(
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
    this.ProjectTypeID = projectTypeID;
    this.RelationTypeID = relationTypeID;
    this.PartTypeID = partTypeID;
    this.ProjectVersionID = projectVersionID;
  }

  public int ProjectTypeID { get; private set; }

  public int RelationTypeID { get; private set; }

  public int PartTypeID { get; private set; }

  public long ProjectVersionID { get; private set; }

  public int CategoryID => 4;

  public int TypeID => this.PartTypeID;

  public object Cookie { get; set; }

  public override bool Equals(object obj)
  {
    if (this == obj)
      return true;
    return obj is ObjectGroupNodeID objectGroupNodeId && this.ProjectTypeID == objectGroupNodeId.ProjectTypeID && this.RelationTypeID == objectGroupNodeId.RelationTypeID && this.PartTypeID == objectGroupNodeId.PartTypeID && this.ProjectVersionID == objectGroupNodeId.ProjectVersionID;
  }

  public override int GetHashCode()
  {
    return this.ProjectTypeID ^ this.RelationTypeID ^ this.PartTypeID ^ (int) this.ProjectVersionID;
  }
}

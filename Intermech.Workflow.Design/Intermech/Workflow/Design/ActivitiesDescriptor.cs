// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivitiesDescriptor
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

public class ActivitiesDescriptor : ListDescriptor
{
  private long _attachedObjectID;
  public List<int> ActivityTypesFilter;

  public ActivitiesDescriptor(int categoryID, int typeID, string caption, IList objectIDs)
    : base(categoryID, typeID, caption, objectIDs)
  {
  }

  public ActivitiesDescriptor(int categoryID, int typeID, string caption, long attachedObjectID)
    : base(categoryID, typeID, caption, (IList) null)
  {
    this._attachedObjectID = attachedObjectID;
  }

  public override INode GetChild(INodeID nodeID)
  {
    return this._attachedObjectID > 0L ? (INode) new ActivitiesListNode(this, this._attachedObjectID) : (INode) new ActivitiesListNode(this, this._objectIDs);
  }
}

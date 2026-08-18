// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Helpers.UniversalNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Helpers;

public class UniversalNode : ObjectsListNode
{
  private new IList objectIDs;
  private UniversalDescriptor _parent;
  private UniversalPart _part;

  public UniversalNode(UniversalDescriptor parent, IList objectIDs)
    : base(objectIDs)
  {
    this.objectIDs = objectIDs;
    this._parent = parent;
  }

  internal UniversalPart Part
  {
    get
    {
      if (this._part == null)
        this._part = new UniversalPart(this._parent, this.objectIDs, this.Services);
      return this._part;
    }
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) this.Part);
  }

  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;

  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return this.Part.GetSupportedColumns(ColumnSetName);
  }

  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return this.Part.GetDefaultColumns();
  }
}

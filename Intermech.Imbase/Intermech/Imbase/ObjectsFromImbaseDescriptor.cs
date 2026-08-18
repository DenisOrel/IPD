// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ObjectsFromImbaseDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

internal class ObjectsFromImbaseDescriptor : HiveDescriptor
{
  private DescriptorCollection _descriptors;
  private long _objID;
  private List<long> _objIDs;

  public override string Caption => this._caption;

  public static string VirtualNodeCaption => LocalizationHolder.rm.GetString("ObjectTypes");

  public ObjectsFromImbaseDescriptor(DescriptorCollection descriptors)
    : base(Consts.ObjectsFromImbaseNodeCategoryID, -1, ObjectsFromImbaseDescriptor.VirtualNodeCaption)
  {
    this._descriptors = descriptors;
  }

  public ObjectsFromImbaseDescriptor(int typeID, long objID, string caption)
    : base(4, typeID, caption)
  {
    this._objID = objID;
  }

  public ObjectsFromImbaseDescriptor(int typeID, List<long> objIDs, string caption)
    : base(4, typeID, caption)
  {
    this._objIDs = objIDs;
  }

  protected ObjectsFromImbaseDescriptor(PersistentState state)
    : base(state)
  {
  }

  public override INode GetChild(INodeID nodeID)
  {
    INode node = (INode) null;
    if (this._descriptors != null)
      node = (INode) new ObjectsFromImbaseRootNode(this._descriptors);
    else if (this._objIDs != null)
      node = (INode) new ObjectsFromImbaseNode(this._typeID, this._objIDs, AccessRights.Enabled);
    return node ?? (INode) new ObjectsFromImbaseNode(this._typeID, this._objID, AccessRights.Enabled);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    object obj = (object) null;
    if (dataFormat == typeof (IDBObjectTypeID))
      obj = (object) new DBObjectTypeID(nodeID.TypeID);
    return obj ?? base.GetData(nodeID, dataFormat);
  }
}

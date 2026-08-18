
// Type: Intermech.Navigator.Descriptos.ObjectTypesDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Nodes;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.Descriptos;

public sealed class ObjectTypesDescriptor : HiveDescriptor
{
  private int[] _objectTypeIds;

  public ObjectTypesDescriptor(int[] objectTypeIds, string caption)
    : base(Intermech.Navigator.Consts.CategoryObjectTypes, 0, caption)
  {
    if (objectTypeIds == null)
      throw new ArgumentNullException(nameof (objectTypeIds));
    this._objectTypeIds = !ObjectTypeHelper.IsAnyUnknownObjectTypeID((IEnumerable<int>) objectTypeIds) ? ((IEnumerable<int>) objectTypeIds).Distinct<int>().OrderBy<int, string>((Func<int, string>) (o => MetaDataHelper.GetObjectTypeName(o))).ToArray<int>() : throw new ArgumentException();
  }

  public ObjectTypesDescriptor(PersistentState persistentState)
    : base(persistentState)
  {
    this._objectTypeIds = ((IEnumerable<string>) ((string) persistentState.GetValue("ObjectTypeIds")).Split('|')).Select<string, int>((Func<string, int>) (o => Convert.ToInt32(o))).ToArray<int>();
  }

  public override bool Equals(object obj)
  {
    if (obj == this)
      return true;
    ObjectTypesDescriptor other = obj as ObjectTypesDescriptor;
    return other != null && this._objectTypeIds.Length == other._objectTypeIds.Length && ((IEnumerable<int>) this._objectTypeIds).Where<int>((Func<int, int, bool>) ((o, index) => other._objectTypeIds[index] == o)).Count<int>() == this._objectTypeIds.Length;
  }

  public override INode GetChild(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    return (INode) new ObjectTypesNode(this._objectTypeIds);
  }

  public override int GetHashCode()
  {
    return (this._caption != null ? this._caption.GetHashCode() : 0) ^ this._objectTypeIds.Length;
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("ObjectTypeIds", (object) string.Join<int>("|", (IEnumerable<int>) this._objectTypeIds));
  }
}

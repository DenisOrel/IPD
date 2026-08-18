
// Type: Intermech.Navigator.VirtualNodes.HiveNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>Описание виртуального узла</summary>
public class HiveNodeID : INodeID
{
  private int _categoryID;
  private int _typeID;
  private object _cookie;

  public HiveNodeID(int categoryID, int typeID)
  {
    this._categoryID = categoryID;
    this._typeID = typeID;
    this._cookie = (object) null;
  }

  public int CategoryID => this._categoryID;

  public int TypeID => this._typeID;

  public object Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }

  public override bool Equals(object obj)
  {
    return obj is HiveNodeID hiveNodeId && hiveNodeId._categoryID == this._categoryID && hiveNodeId.TypeID == this._typeID;
  }

  public override int GetHashCode()
  {
    return this._categoryID.GetHashCode() << 16 /*0x10*/ ^ this._typeID.GetHashCode();
  }
}

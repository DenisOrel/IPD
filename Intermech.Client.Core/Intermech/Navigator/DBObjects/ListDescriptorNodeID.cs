
// Type: Intermech.Navigator.DBObjects.ListDescriptorNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// 
/// </summary>
/// <remarks>Для корректной работы нескольких дескрипторов с одной и той же категорией в навигаторе,
/// вынуждены добавлять заголовок в NodeID</remarks>
internal class ListDescriptorNodeID : HiveNodeID
{
  /// <summary>
  /// 
  /// </summary>
  private readonly string _caption;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="categoryID"></param>
  /// <param name="typeID"></param>
  /// <param name="caption"></param>
  public ListDescriptorNodeID(int categoryID, int typeID, string caption = null)
    : base(categoryID, typeID)
  {
    this._caption = caption;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return base.Equals(obj) && obj is ListDescriptorNodeID descriptorNodeId && descriptorNodeId._caption == this._caption;
  }
}

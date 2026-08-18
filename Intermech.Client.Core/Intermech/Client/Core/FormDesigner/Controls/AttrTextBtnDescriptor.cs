
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextBtnDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class AttrTextBtnDescriptor : Descriptor
{
  protected long _objID;
  protected ConditionStructure[] _conditions;

  /// <summary>Конструктор.</summary>
  /// <param name="objTypeID"></param>
  /// <param name="objID"></param>
  public AttrTextBtnDescriptor(int objTypeID, long objID)
    : this(objTypeID, objID, (ConditionStructure[]) null)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objTypeID"></param>
  /// <param name="objID"></param>
  /// <param name="selectionGuid">контекстная выборка, при отсутствии Guid.Empty. </param>
  private AttrTextBtnDescriptor(int objTypeID, long objID, ConditionStructure[] conditions)
    : base(objTypeID)
  {
    this._objID = objID;
    this._conditions = conditions;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID)
  {
    return !(nodeID is NodeID nodeId) ? base.GetChild(nodeID) : (INode) new AttrTextBtnNode(nodeID.TypeID, nodeId.AccessRights, this._objID, this._conditions);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj is AttrTextBtnDescriptor textBtnDescriptor && this._objID == textBtnDescriptor._objID && this._objTypeID == textBtnDescriptor.ObjectTypeID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return this._objID.GetHashCode() << 2 ^ this._objTypeID.GetHashCode();
  }
}

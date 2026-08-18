
// Type: Intermech.Navigator.DBObjects.RelationsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System.Collections;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс, предназначенный для описания элементов "Объект базы данных с составом" из
/// пространства навигации, включаемых в коллекцию дескрипторов элементов.
/// Такие коллекции используются для создания всевозможных виртуальных
/// элементов.
/// </summary>
public class RelationsDescriptor : Descriptor
{
  /// <summary>Коллекция идентификаторов связей</summary>
  private IList _prjlinkIDs;

  /// <summary>Создать дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="prjlinkIDs">Коллекция идентификаторов связей</param>
  public RelationsDescriptor(long objID, IList prjlinkIDs)
    : base(objID, ObjectFiltrationState.fsCorrespondingSingle)
  {
    this._prjlinkIDs = prjlinkIDs;
  }

  /// <summary>Создать дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="prjlinkIDs">Коллекция идентификаторов связей</param>
  /// <param name="notCheckObject">Не выполнять обращение к серверу приложений, дескриптор получается частично заполненным</param>
  public RelationsDescriptor(long objID, IList prjlinkIDs, bool notCheckObject)
    : base(objID, ObjectFiltrationState.fsCorrespondingSingle, true)
  {
    this._prjlinkIDs = prjlinkIDs;
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new RelationsListNode(this._realObjID, this._prjlinkIDs);
  }

  public override bool Equals(object obj)
  {
    if (!(obj is RelationsDescriptor relationsDescriptor))
      return base.Equals(obj);
    if (this._objID != relationsDescriptor._objID || relationsDescriptor._prjlinkIDs == null && this._prjlinkIDs != null || relationsDescriptor._prjlinkIDs.Count != this._prjlinkIDs.Count)
      return false;
    bool flag = true;
    for (int index = 0; index < this._prjlinkIDs.Count; ++index)
    {
      flag = this._prjlinkIDs[index].Equals(relationsDescriptor._prjlinkIDs[index]) & flag;
      if (!flag)
        return false;
    }
    return flag;
  }

  public override int GetHashCode() => base.GetHashCode();
}

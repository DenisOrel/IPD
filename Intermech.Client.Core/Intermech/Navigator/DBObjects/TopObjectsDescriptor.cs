
// Type: Intermech.Navigator.DBObjects.TopObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjects;

public class TopObjectsDescriptor : HiveDescriptor
{
  protected int _objTypeID;
  private const string PropObjTypeId = "ObjTypeId";

  /// <summary>Создает дескриптор.</summary>
  /// <param name="categoryID"></param>
  /// <param name="caption"></param>
  /// <param name="objTypeID"></param>
  public TopObjectsDescriptor(int categoryID, string caption, int objTypeID)
    : base(categoryID, 0, caption)
  {
    this._objTypeID = objTypeID;
  }

  /// <summary>Создае дескриптор.</summary>
  /// <param name="categoryID"></param>
  /// <param name="typeID"></param>
  /// <param name="caption"></param>
  /// <param name="objTypeID"></param>
  public TopObjectsDescriptor(int categoryID, int typeID, string caption, int objTypeID)
    : base(categoryID, typeID, caption)
  {
    this._objTypeID = objTypeID;
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected TopObjectsDescriptor(PersistentState state)
    : base(state)
  {
    this._objTypeID = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType((Guid) state.GetValue("ObjTypeId"), true).ObjectType;
  }

  /// <summary>Выполняет сериализаций дескриптора.</summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this._objTypeID);
      state.AddValue("ObjTypeId", (object) ((IDBGuid) objectType).GUID);
    }
  }

  public override INode GetChild(INodeID nodeID)
  {
    return ((INodesFactory) ServicesManager.GetService(typeof (IFactory))).GetNode(nodeID, (object) this._objTypeID);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new TopObjectsDescriptor(this._categoryID, this._typeID, this._caption, this._objTypeID);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  public override bool Equals(object obj)
  {
    if (!(obj is TopObjectsDescriptor objectsDescriptor))
      return base.Equals(obj);
    return this._categoryID == objectsDescriptor._categoryID && this._typeID == objectsDescriptor._typeID && this._objTypeID == objectsDescriptor._objTypeID;
  }

  public override int GetHashCode() => this._categoryID ^ this._typeID ^ this._objTypeID;
}

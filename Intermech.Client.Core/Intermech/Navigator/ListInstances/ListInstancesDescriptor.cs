
// Type: Intermech.Navigator.ListInstances.ListInstancesDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.ListInstances;

/// <summary>Дескриптор для узла, описывающего список исполнений</summary>
public class ListInstancesDescriptor : HiveDescriptor
{
  /// <summary>Значение атрибута "Идентификатор группового изделия"</summary>
  private IListInstancesInfo _info;
  /// <summary>
  /// Название параметра для сериализации/десериализации окна
  /// </summary>
  private const string NumGroupInst = "NumGroupInst";
  /// <summary>
  /// Название параметра для сериализации/десериализации окна
  /// </summary>
  private const string FirstInstGUID = "FirstInstGUID";

  public ListInstancesDescriptor(IListInstancesInfo info)
    : base(PDMPluginConsts.CategoryInstance, 0, "")
  {
    this._info = info;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(info.InitInstanceGUID);
      this._caption = CaptionTransform.GetCaption(dbObject.Caption, (long) dbObject.VersionID);
    }
  }

  public ListInstancesDescriptor(Guid numGroupInstance, Guid instanceGUID)
    : this((IListInstancesInfo) new ListInstancesInfo(numGroupInstance, instanceGUID))
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  protected ListInstancesDescriptor(PersistentState state)
    : base(state)
  {
    this._info = (IListInstancesInfo) new ListInstancesInfo((Guid) state.GetValue(nameof (NumGroupInst)), (Guid) state.GetValue(nameof (FirstInstGUID)));
  }

  public Guid Guid => PDMPluginGuids.CategoryInstanceGuid;

  public override INode GetChild(INodeID nodeID) => (INode) new ListInstancesNode(this._info);

  public override bool Equals(object obj)
  {
    if (obj == null || obj.GetType() != typeof (ListInstancesDescriptor))
      return base.Equals(obj);
    ListInstancesDescriptor instancesDescriptor = (ListInstancesDescriptor) obj;
    return this._categoryID == instancesDescriptor._categoryID && this._typeID == instancesDescriptor._typeID && this._info.NumGroupInstance == instancesDescriptor._info.NumGroupInstance && this._info.InitInstanceGUID == instancesDescriptor._info.InitInstanceGUID;
  }

  public override int GetHashCode()
  {
    return this._categoryID ^ this._typeID ^ this._info.NumGroupInstance.GetHashCode() ^ this._info.InitInstanceGUID.GetHashCode();
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("NumGroupInst", (object) this._info.NumGroupInstance);
    state.AddValue("FirstInstGUID", (object) this._info.InitInstanceGUID);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new ListInstancesDescriptor(this._info.NumGroupInstance, this._info.InitInstanceGUID);
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    return dataFormat == typeof (IListInstancesInfo) ? (object) this._info : base.GetData(nodeID, dataFormat);
  }
}


// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectsListNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class ObjectsListNodeDescriptor : HiveDescriptor
{
  private ObjectsListService _srv;

  /// <summary>Конструктор.</summary>
  /// <param name="categoryID">Категория узла, после регистрации в IGuidMapper</param>
  /// <param name="services">Сервисы</param>
  public ObjectsListNodeDescriptor(int categoryID, ObjectsListService services = null)
    : base(categoryID, -1, string.Empty)
  {
    this._srv = services;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="state"></param>
  protected ObjectsListNodeDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID)
  {
    INode child = base.GetChild(nodeID);
    if (child != null && child is IContextAware contextAware && contextAware.Services is AdvancedServiceContainer services)
    {
      if (services.GetService(typeof (ObjectsListService)) is ObjectsListService)
      {
        ObjectsListService srv = this._srv;
      }
      else
        services.AddService(typeof (ObjectsListService), (object) this._srv);
    }
    return child;
  }
}

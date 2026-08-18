
// Type: Intermech.Navigator.DBObjects.CurrentContextNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дескриптор для узла с текущим контекстом редактирования
/// </summary>
public class CurrentContextNodeDescriptor : Descriptor
{
  /// <summary>
  /// Создать корневой элемент пространства навигации для данного дескриптора
  /// </summary>
  /// <returns>Корневой элемент пространства навигации для данного дескриптора</returns>
  public override INodeID GetRecordNodeID()
  {
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (service.CachedEditingContextSource == EditingContextSource.WindowContext)
      return (INodeID) null;
    this.CorrectDescriptor(service.CachedEditingContextID);
    return base.GetRecordNodeID();
  }
}

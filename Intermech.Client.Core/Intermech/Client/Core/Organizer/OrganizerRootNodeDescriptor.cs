
// Type: Intermech.Client.Core.Organizer.OrganizerRootNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>Дескриптор для узла "Органайзер".</summary>
public class OrganizerRootNodeDescriptor : HiveDescriptor
{
  /// <summary>Заголовок.</summary>
  public new static string Caption => LocalizationHolder.rm.GetString("Organaizer_RootNodeCaption");

  /// <summary>
  /// Конструктор.
  /// Создает дескриптор корня дерева папок органайзер.
  /// </summary>
  public OrganizerRootNodeDescriptor()
    : base(Intermech.Navigator.Consts.OrganizerRootNodeTypeID, -1, OrganizerRootNodeDescriptor.Caption)
  {
  }

  /// <summary>
  /// Конструктор.
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected OrganizerRootNodeDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new OrganizerRootNodeDescriptor();
    if (dataFormat == typeof (IOrganizerNode))
      return (object) new OrganizerNode();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }
}

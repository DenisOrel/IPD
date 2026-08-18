
// Type: Intermech.Client.Core.ImageLibraryRootNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Client.Core;

public class ImageLibraryRootNodeDescriptor : HiveDescriptor
{
  /// <summary>
  /// Создает дескриптор корня дерева папок библиотеки изображений.
  /// </summary>
  public ImageLibraryRootNodeDescriptor()
    : base(Intermech.Navigator.Consts.ImageLibraryNodeTypeID, 0, LocalizationHolder.rm.GetString("Client.Core_233"))
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected ImageLibraryRootNodeDescriptor(PersistentState state)
    : base(state)
  {
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new ImageLibraryRootNodeDescriptor();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }
}

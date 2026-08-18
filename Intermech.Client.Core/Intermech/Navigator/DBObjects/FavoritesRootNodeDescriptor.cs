
// Type: Intermech.Navigator.DBObjects.FavoritesRootNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор корневого нода Избранное</summary>
public class FavoritesRootNodeDescriptor : HiveDescriptor
{
  /// <summary>Заголовок.</summary>
  public new static string Caption => LocalizationHolder.rm.GetString("Favorites_RootNodeCaption");

  /// <summary>
  /// Конструктор.
  /// Создает дескриптор корня дерева папок избранного.
  /// </summary>
  public FavoritesRootNodeDescriptor()
    : base(Intermech.Navigator.Consts.CategoryFavoritesNode, -1, FavoritesRootNodeDescriptor.Caption)
  {
  }

  /// <summary>
  /// Конструктор.
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected FavoritesRootNodeDescriptor(PersistentState state)
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
      return (object) new FavoritesRootNodeDescriptor();
    if (dataFormat == typeof (IFavoritesNode))
      return (object) new FavoritesNode();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }
}

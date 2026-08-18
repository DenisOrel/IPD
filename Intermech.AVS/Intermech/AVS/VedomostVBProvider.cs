// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.VedomostVBProvider
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using Intermech.Interfaces.AVS;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.AVS;

/// <summary> Провайдер команд контекстного меню т.е. включение команды в локальное меню Навигатора </summary>
internal class VedomostVBProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("CreaveVedomostVB", new CommandInfo(0, new ClickEventHandler(VedomostVBProvider.CreaveVedomostVBCommand)));
    return groupCommands;
  }

  /// <summary> Вызов создания ведомости из Навигатора </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CreaveVedomostVBCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData.ObjectType == AvsIDCache.ObjType_AssemblyUnit || itemData.ObjectType == AvsIDCache.ObjType_ProcessComposition)
    {
      Vedomost_VB vedomostVb = new Vedomost_VB();
      vedomostVb.ObjectTypeMainSp = AvsIDCache.ObjType_Specification;
      long objectSpecification = AVSPlugin.PDMSpecificationsService.GetObjectSpecification(itemData.ObjectID);
      vedomostVb._metodCreate = "Auto";
      vedomostVb._metodFrom = "NavigatorAssemblyUnit";
      vedomostVb._iDSP = objectSpecification;
      if (objectSpecification != 0L)
        vedomostVb.CreateVedomost(objectSpecification);
      else
        vedomostVb.CreateVedomost(itemData.ObjectID);
    }
    else
      new Vedomost_VB()
      {
        ObjectTypeMainSp = itemData.ObjectType,
        _metodCreate = "Auto",
        _metodFrom = "NavigatorSP",
        _iDSP = itemData.ObjectID
      }.CreateVedomost(itemData.ObjectID);
  }
}

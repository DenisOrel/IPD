// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Extensions.CommandsProviderExtension
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.TechCard;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Extensions;

internal static class CommandsProviderExtension
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandsProvider"></param>
  /// <param name="factory"></param>
  public static void RegisterForAllBaseTypes(
    this ICommandsProvider commandsProvider,
    IFactory factory)
  {
    foreach (int techAllBaseObjType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechAllBaseObjTypes)
      factory.AddCommandsProvider(1, techAllBaseObjType, commandsProvider);
  }
}

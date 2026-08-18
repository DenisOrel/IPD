// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.TechCardCommandUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// 
/// </summary>
internal static class TechCardCommandUtils
{
  /// <summary>
  /// Получение фокусeд элементов для checked режима навигатора
  /// </summary>
  /// <param name="items"></param>
  /// <param name="contextServices"></param>
  /// <returns></returns>
  public static ISelectedItems GetFocusedItems(
    ISelectedItems items,
    System.IServiceProvider contextServices)
  {
    bool flag = true;
    for (int index = 0; index < items.Count; ++index)
    {
      NavigatorTreeNode itemData = items.GetItemData<NavigatorTreeNode>(index, false);
      if (itemData == null || itemData.CheckState != CheckState.Checked)
      {
        flag = false;
        break;
      }
    }
    return !flag || !(ServiceUtils.GetService<ISimpleSelectedItems>((object) ServicesManager.ServiceContainer, false) is ISelectedItems service) ? items : service;
  }
}

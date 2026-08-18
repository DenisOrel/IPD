// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionTreeViewCellWidgetProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Controls;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

internal class ArtsCompositionTreeViewCellWidgetProvider : INavigatorTreeViewCellWidgetProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="treeView"></param>
  /// <param name="rowWidget"></param>
  /// <param name="column"></param>
  /// <returns></returns>
  public CellWidget GetCellWidget(NavigatorTreeView treeView, RowWidget rowWidget, Column column)
  {
    if (!(column is NavigatorTreeColumn navigatorTreeColumn))
      return (CellWidget) null;
    return navigatorTreeColumn.NavigatorColumn.ID.Equals((object) ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS) && navigatorTreeColumn.NavigatorColumn.SchemeGuid == ArtsCompositionColumnScheme.Consts.SchemeGuid ? (CellWidget) new ArtsCompositionCellWidgetItemStatus(rowWidget, column) : (CellWidget) null;
  }
}

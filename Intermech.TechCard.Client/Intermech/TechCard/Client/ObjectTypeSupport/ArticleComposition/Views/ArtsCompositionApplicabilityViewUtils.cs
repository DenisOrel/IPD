// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views.ArtsCompositionApplicabilityViewUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Tools.Controls.Navigator;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;

/// <summary>
/// 
/// </summary>
public static class ArtsCompositionApplicabilityViewUtils
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public static NodeColumnCollection OnGetSupportedColumnsEventHandler(object sender)
  {
    return TechCardNavTreeViewUtils.GetObjAndRelSupportedColumns(TechCardConsts.ObjectTypes.EdinicaSostavaID, TechCardConsts.RelTypes.TechRelationID);
  }
}

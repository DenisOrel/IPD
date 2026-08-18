// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.ArtsCompositionItemStatusHelper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>
/// 
/// </summary>
internal static class ArtsCompositionItemStatusHelper
{
  /// <summary>
  /// Рассчитать статус для позиции по количеству в конструкторском и технологическом составах
  /// </summary>
  /// <param name="versionQty"></param>
  /// <param name="objectQty"></param>
  /// <returns></returns>
  public static ArtsCompositionItemStatus CalcStatus(
    ElementQuantity versionQty,
    ElementQuantity objectQty)
  {
    if (versionQty?.RemainQuantity == null)
      return ArtsCompositionItemStatus.None;
    if (versionQty.TechQuantity == null || MathUtils.AlmostZero(versionQty.TechQuantity.Value))
    {
      if (objectQty?.TechQuantity == null)
        return ArtsCompositionItemStatus.NotUsed;
      ElementQuantity elementQuantity = new ElementQuantity(versionQty.TypedInfoItem, versionQty.DesignQuantity, objectQty.TechQuantity);
      return elementQuantity.RemainQuantity.Value >= 0.0 && !MathUtils.AlmostZero(elementQuantity.RemainQuantity.Value) ? ArtsCompositionItemStatus.VersionNotAllUsed : ArtsCompositionItemStatus.VersionAllUsed;
    }
    if (MathUtils.AlmostZero(versionQty.RemainQuantity.Value))
      return ArtsCompositionItemStatus.AllUsed;
    return versionQty.RemainQuantity.Value >= 0.0 ? ArtsCompositionItemStatus.NotAllUsed : ArtsCompositionItemStatus.UsedOverLimit;
  }
}

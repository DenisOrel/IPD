// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.IArtsCompositionParamsService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>
/// 
/// </summary>
internal interface IArtsCompositionParamsService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="settings"></param>
  /// <returns></returns>
  bool SaveSettings(IArtsCompositionParams settings);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="settings"></param>
  /// <returns></returns>
  bool LoadSettings(out IArtsCompositionParams settings);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings.ArtsCompositionStatusParamWrapper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings;

/// <summary>
/// 
/// </summary>
[TypeConverter(typeof (ArtsCompositionStatusParamWrapperConverter))]
internal class ArtsCompositionStatusParamWrapper
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IArtsCompositionStatusParams _statusParams;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="statusParams"></param>
  public ArtsCompositionStatusParamWrapper(IArtsCompositionStatusParams statusParams)
  {
    this._statusParams = statusParams;
  }

  [CustomDisplayName("Attribute.TechCard.Client_59")]
  [CustomDescription("Attribute.TechCard.Client_60")]
  public Color BackColor
  {
    get => this._statusParams.Color;
    set => this._statusParams.Color = value;
  }
}

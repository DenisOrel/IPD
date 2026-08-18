// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.ArtsCompositionStatusParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.Drawing;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>Настройка для статуса позиции</summary>
internal class ArtsCompositionStatusParams : IArtsCompositionStatusParams
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="status"></param>
  public ArtsCompositionStatusParams(ArtsCompositionItemStatus status) => this.Status = status;

  /// <summary>
  /// 
  /// </summary>
  public ArtsCompositionItemStatus Status { get; }

  /// <summary>
  /// 
  /// </summary>
  public Color Color { get; set; }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.IArtsCompositionImageService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

internal interface IArtsCompositionImageService
{
  /// <summary>Возвращает индекс иконки по значению</summary>
  /// <param name="status">Значение</param>
  /// <returns>Индекс в списке или -1.</returns>
  int ImageIndex(ArtsCompositionItemStatus status);

  /// <summary>Объект ImageList</summary>
  ImageList ImageList { get; }
}

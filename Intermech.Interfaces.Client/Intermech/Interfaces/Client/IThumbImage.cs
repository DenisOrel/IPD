// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IThumbImage
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс представления абстрактного изображения
/// для рисования в эскизах страниц
/// </summary>
public interface IThumbImage
{
  /// <summary>Рисует изображение в указанных границах</summary>
  /// <param name="g">Graphics для рисования</param>
  /// <param name="bounds">Границы для рисования</param>
  /// <param name="stretchBounds">Границы, пересчитанные с учетом пропорционального масштабирования</param>
  void PaintTo(Graphics g, Rectangle bounds, Rectangle stretchBounds);

  int Height { get; }

  int Width { get; }
}

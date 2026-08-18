// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImRtfViewService
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#nullable disable
namespace Intermech.Document.Client;

/// <summary></summary>
public class ImRtfViewService : IImRtfViewService
{
  private readonly object _dictionaryLock = new object();

  /// <summary>получить коллекцию формула -- изображение формулы</summary>
  /// <param name="font">используемый фонт</param>
  /// <param name="textColor">цвет формулы</param>
  /// <param name="backgroundColor">цвет подложки</param>
  /// <param name="dictImage">коллекция формула--&gt; изображение формулы </param>
  /// <param name="dictImageSize">коллекция формула--&gt; реальный размер изображения</param>
  /// <returns>есть ли формулы</returns>
  public bool CreateFormulaImages(
    Font font,
    Color textColor,
    Color backgroundColor,
    ref Dictionary<string, Image> dictImage,
    ref Dictionary<string, SizeF> dictImageSize)
  {
    lock (this._dictionaryLock)
    {
      using (EditSymbolForm editSymbolForm = new EditSymbolForm())
      {
        editSymbolForm.BackColor = backgroundColor;
        editSymbolForm.ForeColor = textColor;
        dictImageSize.Clear();
        foreach (string str in dictImage.Keys.ToArray<string>())
        {
          SizeF totalSize;
          Image formulaImages = editSymbolForm.CreateFormulaImages(str, font, textColor, backgroundColor, out totalSize);
          dictImage[str] = formulaImages;
          dictImageSize[str] = totalSize;
        }
      }
    }
    return false;
  }
}

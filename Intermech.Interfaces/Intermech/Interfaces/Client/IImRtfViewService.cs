
// Type: Intermech.Interfaces.Client.IImRtfViewService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Interfaces.Client
{
    /// <summary>интерфейс для просмотра формул</summary>
    public interface IImRtfViewService
    {
      /// <summary>получить коллекцию формула -- изображение формулы</summary>
      /// <param name="font">используемый фонт</param>
      /// <param name="textColor">цвет формулы</param>
      /// <param name="backgroundColor">цвет подложки</param>
      /// <param name="dictImage">коллекция формула--&gt; изображение формулы </param>
      /// <param name="dictImageSize">коллекция формула--&gt; реальный размер изображения</param>
      /// <returns>есть ли формулы</returns>
      bool CreateFormulaImages(
        Font font,
        Color textColor,
        Color backgroundColor,
        ref Dictionary<string, Image> dictImage,
        ref Dictionary<string, SizeF> dictImageSize);
    }
}

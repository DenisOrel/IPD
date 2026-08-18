
// Type: Intermech.UI.Winforms.ICustomMessageBoxData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    internal interface ICustomMessageBoxData
    {
      /// <summary>Возвращает заголовок сообщения.</summary>
      string Caption { get; }

      /// <summary>Возвращает текст сообщения.</summary>
      string Text { get; }

      /// <summary>
      /// Возвращает стандартную иконку для окна сообщения.
      /// Значение свойства используется только в том случае, если свойство <see cref="P:Intermech.UI.Winforms.ICustomMessageBoxData.CustomIcon" /> не задано.
      /// </summary>
      MessageBoxIcon Icon { get; }

      /// <summary>
      /// Возвращает нестандартную иконку для окна сообщения.
      /// Значение этого свойства имеет приоритет перед свойством <see cref="P:Intermech.UI.Winforms.ICustomMessageBoxData.Icon" />.
      /// Если значение задано, то используется это свойство, а не <see cref="P:Intermech.UI.Winforms.ICustomMessageBoxData.Icon" />.
      /// </summary>
      Image CustomIcon { get; }

      /// <summary>Возвращает коллекцию кнопок для окна сообщения.</summary>
      ObservableCollection<CustomMessageBoxButton> Buttons { get; }
    }
}

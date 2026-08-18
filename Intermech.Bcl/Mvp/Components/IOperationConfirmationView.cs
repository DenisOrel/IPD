
// Type: Intermech.Mvp.Components.IOperationConfirmationView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components
{
    /// <summary>
    /// Интерфейс вида MVP, в котором пользователь имеет возможность подтвердить сделанные изменения или свой выбор перед
    /// завершение работы вида. Как правило, этот интерфейс реализуется диалоговыми окнами выбора или редакторами с
    /// кнопкой "OK", по которой происходит сохранение изменений и завершение работы вида.
    /// </summary>
    public interface IOperationConfirmationView
    {
      /// <summary>
      /// Событие успешного подтвержения сделанных изменений или своего выбора пользователем.
      /// После этого события взаимодействие пользователя с видом заканчивается.
      /// </summary>
      event EventHandler OperationConfirmed;
    }
}

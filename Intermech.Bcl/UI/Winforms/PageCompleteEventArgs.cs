
// Type: Intermech.UI.Winforms.PageCompleteEventArgs
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Описывает аргументы события о том, что пользователь ввел все необходимые данные на странице мастера
    /// и может перейти к следующей странице.
    /// </summary>
    public class PageCompleteEventArgs : EventArgs
    {
      private bool isComplete;

      /// <summary>Создает объект.</summary>
      /// <param name="isComplete">Признак законченности страницы</param>
      public PageCompleteEventArgs(bool isComplete) => this.isComplete = isComplete;

      /// <summary>
      /// Возвращает true, если страница действительно закончена и ее можно сменить.
      /// </summary>
      public bool IsComplete => this.isComplete;
    }
}

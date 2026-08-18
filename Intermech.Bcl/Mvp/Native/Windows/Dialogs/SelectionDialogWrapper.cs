
// Type: Intermech.Mvp.Native.Windows.Dialogs.SelectionDialogWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using System;


namespace Intermech.Mvp.Native.Windows.Dialogs
{
    public abstract class SelectionDialogWrapper : SystemDialogWrapper, IOperationConfirmationView
    {
      private EventHandler operationConfirmed;

      protected override void DoProcessSuccess()
      {
        base.DoProcessSuccess();
        if (this.operationConfirmed == null)
          return;
        this.operationConfirmed((object) this, EventArgs.Empty);
      }

      /// <summary>
      /// Событие успешного подтвержения сделанных изменений или своего выбора пользователем.
      /// После этого события взаимодействие пользователя с видом заканчивается.
      /// </summary>
      event EventHandler IOperationConfirmationView.OperationConfirmed
      {
        add => this.operationConfirmed += value;
        remove => this.operationConfirmed -= value;
      }
    }
}


// Type: Intermech.Mvp.Components.Dialogs.SelectionDialogPresenter`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components.Dialogs
{
    public abstract class SelectionDialogPresenter<TView> : Presenter<TView> where TView : class, IView, IOperationConfirmationView
    {
      private bool isSuccessful;

      public bool IsSuccessful => this.isSuccessful;

      protected override void OnAttachView()
      {
        base.OnAttachView();
        this.View.OperationConfirmed += new EventHandler(this.OnAcceptDialogResult);
        this.isSuccessful = false;
      }

      protected override void OnDetachView()
      {
        base.OnDetachView();
        this.View.OperationConfirmed -= new EventHandler(this.OnAcceptDialogResult);
      }

      protected virtual void OnAcceptDialogResult(object sender, EventArgs e)
      {
        this.isSuccessful = true;
      }
    }
}

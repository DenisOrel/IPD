
// Type: Intermech.Mvp.Winforms.FormDisplayState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    /// <summary>
    /// Реализует объект состояния вида MVP (view), используемый при создании видов на основе класса Form.
    /// </summary>
    public sealed class FormDisplayState : AbstractViewDisplayState
    {
      private Form form;

      /// <summary>Создает объект.</summary>
      /// <param name="form">Форма Windows Forms</param>
      /// <exception cref="T:ArgumentNullException">form</exception>
      public FormDisplayState(Form form)
      {
        this.form = form != null ? form : throw new ArgumentNullException(nameof (form));
        this.SetInitialState(form.Visible);
        this.form.Shown += new EventHandler(this.OnFormShown);
        this.form.FormClosed += new FormClosedEventHandler(this.OnFormClosed);
      }

      private void OnFormShown(object sender, EventArgs e) => this.RaiseViewShown();

      private void OnFormClosed(object sender, FormClosedEventArgs e) => this.RaiseViewClosed();
    }
}


// Type: Intermech.Mvp.Winforms.ControlDisplayState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    /// <summary>
    /// Реализует объект состояния вида MVP (view), используемый при создании видов на основе класса Control.
    /// </summary>
    public sealed class ControlDisplayState : AbstractViewDisplayState
    {
      private Control control;
      private Form parentForm;

      /// <summary>Создает объект.</summary>
      /// <param name="control">Визуальный элемент Windows Forms</param>
      /// <exception cref="T:ArgumentNullException">control</exception>
      public ControlDisplayState(Control control)
      {
        this.control = control != null ? control : throw new ArgumentNullException(nameof (control));
        this.SetInitialState(ControlDisplayState.IsControlShown(control));
        this.control.ParentChanged += new EventHandler(this.OnParentChanged);
        this.parentForm = this.control.FindForm();
        if (this.parentForm == null)
          return;
        this.AttachToParentForm(false);
      }

      private static bool IsControlShown(Control control)
      {
        if (control == null)
          throw new ArgumentNullException(nameof (control));
        return control.Visible && control.Parent != null;
      }

      private void OnParentChanged(object sender, EventArgs e)
      {
        Form form = this.control.FindForm();
        if (this.parentForm == form)
          return;
        if (this.parentForm != null)
        {
          this.DetachFromParentForm(form);
          this.parentForm = (Form) null;
        }
        this.parentForm = form;
        if (this.parentForm == null)
          return;
        this.AttachToParentForm(true);
      }

      private void AttachToParentForm(bool raiseEvents)
      {
        this.parentForm.Shown += new EventHandler(this.parentForm_Shown);
        this.parentForm.FormClosed += new FormClosedEventHandler(this.parentForm_FormClosed);
        if (!raiseEvents || !this.parentForm.Visible)
          return;
        this.RaiseViewShown();
      }

      private void DetachFromParentForm(Form newParentForm)
      {
        this.parentForm.Shown -= new EventHandler(this.parentForm_Shown);
        this.parentForm.FormClosed -= new FormClosedEventHandler(this.parentForm_FormClosed);
        this.RaiseViewClosed();
      }

      private void parentForm_FormClosed(object sender, FormClosedEventArgs e)
      {
        this.RaiseViewClosed();
      }

      private void parentForm_Shown(object sender, EventArgs e) => this.RaiseViewShown();
    }
}

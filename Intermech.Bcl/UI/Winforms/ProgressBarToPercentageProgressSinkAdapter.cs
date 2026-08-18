
// Type: Intermech.UI.Winforms.ProgressBarToPercentageProgressSinkAdapter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    internal sealed class ProgressBarToPercentageProgressSinkAdapter : PercentageProgressSinkBase
    {
      private Control viewControl;
      private Label stateLabel;
      private ProgressBar progressBar;
      private Func<bool> queryCancelledState;

      public ProgressBarToPercentageProgressSinkAdapter(
        Control viewControl,
        Label stateLabel,
        ProgressBar progressBar,
        Func<bool> queryCancelledState)
      {
        if (viewControl == null)
          throw new ArgumentNullException(nameof (viewControl));
        if (stateLabel == null)
          throw new ArgumentNullException(nameof (stateLabel));
        if (progressBar == null)
          throw new ArgumentNullException(nameof (progressBar));
        if (queryCancelledState == null)
          throw new ArgumentNullException(nameof (queryCancelledState));
        this.viewControl = viewControl;
        this.stateLabel = stateLabel;
        this.progressBar = progressBar;
        this.queryCancelledState = queryCancelledState;
      }

      /// <summary>
      /// Возвращает признак прерывания выполнения текущего процесса. Процесс должен периодически проверять значение этого свойства.
      /// Если значение свойства стало равно true, то процесс должен прервать свое выполнение.
      /// </summary>
      public override bool IsCancelled
      {
        [DebuggerStepThrough] get => this.queryCancelledState();
      }

      /// <summary>Сообщает текущее состояние процесса.</summary>
      /// <param name="text">Описание текущего состояния процесса или выполняемой операции</param>
      protected override void DoSetState(string text)
      {
        if (!this.CanUpdateView())
          return;
        this.stateLabel.Text = text;
        Application.DoEvents();
      }

      /// <summary>
      /// Сообщает процент готовности процесса. Новое новое значение процента должно быть больше текущего значения.
      /// </summary>
      /// <param name="percentValue">Процент готовности процесса в диапазоне от 0 до 100</param>
      protected override void DoSetProgress(double percentValue)
      {
        if (!this.CanUpdateView())
          return;
        this.progressBar.Value = (int) Math.Round(percentValue);
        Application.DoEvents();
      }

      private bool CanUpdateView()
      {
        return !this.IsCancelled && !this.viewControl.IsDisposed && this.viewControl.Visible;
      }
    }
}

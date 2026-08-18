
// Type: Intermech.Mvp.Winforms.MvpUserControl
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    /// <summary>
    /// Базовый класс для создания видов MVP (view) на основе класса UserControl.
    /// </summary>
    public class MvpUserControl : UserControl, IView
    {
      private ControlDisplayState viewDisplayState;
      /// <summary>Required designer variable.</summary>
      private IContainer components;

      public MvpUserControl()
      {
        this.InitializeComponent();
        if (this.DesignMode)
          return;
        this.viewDisplayState = new ControlDisplayState((Control) this);
      }

      /// <summary>
      /// Возвращает состояние вида MVP (view). Объект состояния вида используется посредником MVP (presenter) для подключения к виду и отключения от него.
      /// </summary>
      [Browsable(false)]
      public IViewDisplayState DisplayState
      {
        [DebuggerStepThrough] get => (IViewDisplayState) this.viewDisplayState;
      }

      /// <summary>Clean up any resources being used.</summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
        this.components = (IContainer) new Container();
        this.AutoScaleMode = AutoScaleMode.Font;
      }
    }
}

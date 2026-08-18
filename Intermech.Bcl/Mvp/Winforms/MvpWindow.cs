
// Type: Intermech.Mvp.Winforms.MvpWindow
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    /// <summary>
    /// Базовый класс для создания видов MVP (view) на основе класса Form.
    /// </summary>
    public class MvpWindow : Form, IView
    {
      private FormDisplayState viewDisplayState;
      /// <summary>Required designer variable.</summary>
      private IContainer components;

      public MvpWindow()
      {
        this.InitializeComponent();
        if (this.DesignMode)
          return;
        this.viewDisplayState = new FormDisplayState((Form) this);
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
        this.SuspendLayout();
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(284, 262);
        this.Name = nameof (MvpWindow);
        this.Text = nameof (MvpWindow);
        this.ResumeLayout(false);
      }
    }
}


// Type: Intermech.UI.Winforms.CustomMessageBoxWindow
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Окно для пользовательского MessageBox с полностью настраиваемым набором кнопок.
    /// Вид окна максимально приближен к системному, размер и положение элементов окна
    /// автоматически подстраивается под текст и количество кнопок.
    /// Содержимое окна создается динамически с помощью объекта типа <see cref="T:Intermech.UI.Winforms.CustomMessageBox" />.
    /// </summary>
    /// <remarks>
    /// Для автоматической подстройки под динамическое содержимое используется макет окна на
    /// основе компонентов FlowLayoutPanel и TableLayoutPanel.
    /// </remarks>
    internal sealed class CustomMessageBoxWindow : Form
    {
      private object customDialogResult;
      private bool isInitialized;
      private List<Button> initializedButtons;
      private bool disposeIconImage;
      /// <summary>Required designer variable.</summary>
      private IContainer components;
      private FlowLayoutPanel flpMainPanel;
      private FlowLayoutPanel flpImageAndMessagePanel;
      private PictureBox pbIconImage;
      private FlowLayoutPanel flpButtonPanel;
      private Label lbMessage;
      private Button btButton;

      /// <summary>Создает объект.</summary>
      public CustomMessageBoxWindow()
      {
        this.InitializeComponent();
        this.initializedButtons = new List<Button>();
      }

      internal void InitializeDialog(ICustomMessageBoxData data)
      {
        if (data == null)
          throw new ArgumentNullException(nameof (data));
        this.ClearDialog();
        this.InitializeDialogCore(data);
        this.isInitialized = true;
      }

      internal void ClearDialog()
      {
        if (!this.isInitialized)
          return;
        this.ClearDialogCore();
        this.isInitialized = false;
      }

      private void ClearDialogCore()
      {
        if (this.AcceptButton != null)
          this.AcceptButton = (IButtonControl) null;
        if (this.CancelButton != null)
          this.CancelButton = (IButtonControl) null;
        this.Text = string.Empty;
        this.lbMessage.Text = string.Empty;
        this.ClearIconImage();
        this.ClearButtons();
      }

      private void ClearIconImage()
      {
        if (this.pbIconImage.Image == null)
          return;
        if (this.disposeIconImage)
        {
          Image image = this.pbIconImage.Image;
          this.pbIconImage.Image = (Image) null;
          image.Dispose();
          this.disposeIconImage = false;
        }
        else
          this.pbIconImage.Image = (Image) null;
      }

      private void ClearButtons()
      {
        if (this.initializedButtons.Count == 0)
          return;
        foreach (Button initializedButton in this.initializedButtons)
        {
          initializedButton.Tag = (object) null;
          initializedButton.Text = (string) null;
          initializedButton.DialogResult = DialogResult.None;
          initializedButton.Click -= new EventHandler(this.OnButtonClick);
          if (initializedButton != this.btButton)
          {
            initializedButton.Parent = (Control) null;
            initializedButton.Dispose();
          }
        }
        this.initializedButtons.Clear();
      }

      private void InitializeDialogCore(ICustomMessageBoxData data)
      {
        this.Text = data.Caption;
        this.lbMessage.Text = data.Text;
        if (data.CustomIcon != null)
        {
          this.pbIconImage.Image = data.CustomIcon;
          this.disposeIconImage = false;
        }
        else
        {
          this.pbIconImage.Image = this.TryGetMessageBoxIconImage(data.Icon);
          this.disposeIconImage = this.pbIconImage.Image != null;
        }
        for (int index = data.Buttons.Count - 1; index >= 0; --index)
        {
          CustomMessageBoxButton button1 = data.Buttons[index];
          Button button2 = this.AddButtonControl();
          button2.Tag = (object) button1;
          button2.Text = button1.Text;
          button2.DialogResult = button1.DialogResult;
          button2.Click += new EventHandler(this.OnButtonClick);
          if (button1.IsDefaultButton)
          {
            this.AcceptButton = (IButtonControl) button2;
            button2.Select();
          }
          if (button1.IsCancelButton)
            this.CancelButton = (IButtonControl) button2;
          this.initializedButtons.Add(button2);
        }
      }

      private Image TryGetMessageBoxIconImage(MessageBoxIcon icon)
      {
        switch (icon)
        {
          case MessageBoxIcon.Hand:
            return (Image) SystemIcons.Error.ToBitmap();
          case MessageBoxIcon.Question:
            return (Image) SystemIcons.Question.ToBitmap();
          case MessageBoxIcon.Exclamation:
            return (Image) SystemIcons.Warning.ToBitmap();
          case MessageBoxIcon.Asterisk:
            return (Image) SystemIcons.Information.ToBitmap();
          default:
            return (Image) null;
        }
      }

      private Button AddButtonControl()
      {
        Button button;
        if (this.initializedButtons.Count == 0)
        {
          button = this.btButton;
        }
        else
        {
          button = new Button();
          button.Size = this.btButton.Size;
          button.Margin = this.btButton.Margin;
          button.Padding = this.btButton.Padding;
          button.MinimumSize = this.btButton.MinimumSize;
          button.MaximumSize = this.btButton.MaximumSize;
          button.AutoSizeMode = this.btButton.AutoSizeMode;
          button.AutoSize = this.btButton.AutoSize;
          this.flpButtonPanel.Controls.Add((Control) button);
        }
        return button;
      }

      internal object CustomDialogResult => this.customDialogResult;

      private void OnButtonClick(object sender, EventArgs e)
      {
        Button button = (Button) sender;
        this.customDialogResult = ((CustomMessageBoxButton) button.Tag).CustomDialogResult;
        if (button.DialogResult != DialogResult.None)
          return;
        this.Close();
      }

      /// <summary>Clean up any resources being used.</summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
        if (disposing)
          this.ClearDialog();
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
        this.flpMainPanel = new FlowLayoutPanel();
        this.flpImageAndMessagePanel = new FlowLayoutPanel();
        this.pbIconImage = new PictureBox();
        this.lbMessage = new Label();
        this.flpButtonPanel = new FlowLayoutPanel();
        this.btButton = new Button();
        this.flpMainPanel.SuspendLayout();
        this.flpImageAndMessagePanel.SuspendLayout();
        ((ISupportInitialize) this.pbIconImage).BeginInit();
        this.flpButtonPanel.SuspendLayout();
        this.SuspendLayout();
        this.flpMainPanel.AutoSize = true;
        this.flpMainPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.flpMainPanel.Controls.Add((Control) this.flpImageAndMessagePanel);
        this.flpMainPanel.Controls.Add((Control) this.flpButtonPanel);
        this.flpMainPanel.Dock = DockStyle.Fill;
        this.flpMainPanel.FlowDirection = FlowDirection.TopDown;
        this.flpMainPanel.Location = new Point(0, 0);
        this.flpMainPanel.Margin = new Padding(0);
        this.flpMainPanel.Name = "flpMainPanel";
        this.flpMainPanel.Size = new Size(551, 146);
        this.flpMainPanel.TabIndex = 0;
        this.flpMainPanel.WrapContents = false;
        this.flpImageAndMessagePanel.AutoSize = true;
        this.flpImageAndMessagePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.flpImageAndMessagePanel.BackColor = SystemColors.Window;
        this.flpImageAndMessagePanel.Controls.Add((Control) this.pbIconImage);
        this.flpImageAndMessagePanel.Controls.Add((Control) this.lbMessage);
        this.flpImageAndMessagePanel.Dock = DockStyle.Fill;
        this.flpImageAndMessagePanel.Location = new Point(0, 0);
        this.flpImageAndMessagePanel.Margin = new Padding(0);
        this.flpImageAndMessagePanel.Name = "flpImageAndMessagePanel";
        this.flpImageAndMessagePanel.Padding = new Padding(24);
        this.flpImageAndMessagePanel.Size = new Size(448, 84);
        this.flpImageAndMessagePanel.TabIndex = 0;
        this.flpImageAndMessagePanel.WrapContents = false;
        this.pbIconImage.BackColor = SystemColors.Window;
        this.pbIconImage.Location = new Point(24, 26);
        this.pbIconImage.Margin = new Padding(0, 2, 2, 2);
        this.pbIconImage.Name = "pbIconImage";
        this.pbIconImage.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
        this.pbIconImage.SizeMode = PictureBoxSizeMode.CenterImage;
        this.pbIconImage.TabIndex = 0;
        this.pbIconImage.TabStop = false;
        this.lbMessage.AutoSize = true;
        this.lbMessage.BackColor = SystemColors.Window;
        this.lbMessage.Location = new Point(62, 26);
        this.lbMessage.Margin = new Padding(4, 2, 12, 2);
        this.lbMessage.MaximumSize = new Size(450, 0);
        this.lbMessage.MinimumSize = new Size(350, 0);
        this.lbMessage.Name = "lbMessage";
        this.lbMessage.Size = new Size(350, 15);
        this.lbMessage.TabIndex = 1;
        this.flpButtonPanel.AutoSize = true;
        this.flpButtonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.flpButtonPanel.Controls.Add((Control) this.btButton);
        this.flpButtonPanel.Dock = DockStyle.Bottom;
        this.flpButtonPanel.FlowDirection = FlowDirection.RightToLeft;
        this.flpButtonPanel.Location = new Point(0, 84);
        this.flpButtonPanel.Margin = new Padding(0);
        this.flpButtonPanel.MaximumSize = new Size(550, 0);
        this.flpButtonPanel.Name = "flpButtonPanel";
        this.flpButtonPanel.Padding = new Padding(3, 8, 3, 8);
        this.flpButtonPanel.Size = new Size(448, 49);
        this.flpButtonPanel.TabIndex = 1;
        this.btButton.AutoSize = true;
        this.btButton.Location = new Point(351, 12);
        this.btButton.Margin = new Padding(8, 4, 3, 3);
        this.btButton.MaximumSize = new Size(176 /*0xB0*/, 0);
        this.btButton.MinimumSize = new Size(88, 26);
        this.btButton.Name = "btButton";
        this.btButton.Size = new Size(88, 26);
        this.btButton.TabIndex = 0;
        this.btButton.Text = "button";
        this.btButton.UseVisualStyleBackColor = true;
        this.AutoScaleDimensions = new SizeF(7f, 15f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.ClientSize = new Size(551, 146);
        this.Controls.Add((Control) this.flpMainPanel);
        this.Font = new Font("Segoe UI", 9f);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (CustomMessageBoxWindow);
        this.StartPosition = FormStartPosition.CenterParent;
        this.flpMainPanel.ResumeLayout(false);
        this.flpMainPanel.PerformLayout();
        this.flpImageAndMessagePanel.ResumeLayout(false);
        this.flpImageAndMessagePanel.PerformLayout();
        ((ISupportInitialize) this.pbIconImage).EndInit();
        this.flpButtonPanel.ResumeLayout(false);
        this.flpButtonPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}

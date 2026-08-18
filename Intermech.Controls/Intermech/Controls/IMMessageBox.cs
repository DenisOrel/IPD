
// Type: Intermech.Controls.IMMessageBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>Окно для выдачи сообщения пользователю</summary>
public class IMMessageBox
{
  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Tag нажатой кнопки</returns>
  public static object ShowEx(string FormCaption, string Message, IMMessageBoxButton[] Buttons)
  {
    return IMMessageBox.ShowMessageBoxEx(FormCaption, Message, Buttons, (Image) null);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Tag нажатой кнопки</returns>
  public static object ShowEx(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    Image image)
  {
    return IMMessageBox.ShowMessageBoxEx(FormCaption, Message, Buttons, image);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Tag нажатой кнопки</returns>
  public static object ShowEx(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    IMMessageBoxImage image)
  {
    return IMMessageBox.ShowMessageBoxEx(FormCaption, Message, Buttons, IMMessageBox.GetImage(image));
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Tag нажатой кнопки</returns>
  public static object Show(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    IMMessageBoxImage image,
    Form parent,
    IList<string> messageDetails = null)
  {
    return (object) IMMessageBox.ShowMessageBox(FormCaption, Message, Buttons, IMMessageBox.GetImage(image), parent, messageDetails);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResult Show(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    IList<string> messageDetails = null)
  {
    return IMMessageBox.ShowMessageBox(FormCaption, Message, Buttons, (Image) null, (Form) null, messageDetails);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResult Show(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    IMMessageBoxImage image,
    IList<string> messageDetails = null)
  {
    return IMMessageBox.Show(FormCaption, Message, Buttons, IMMessageBox.GetImage(image), messageDetails);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResult Show(
    string FormCaption,
    string Message,
    MessageBoxButtons messageBoxButtons,
    IMMessageBoxImage image)
  {
    return IMMessageBox.Show(FormCaption, Message, messageBoxButtons, IMMessageBox.GetImage(image));
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="messageBoxButtonsAdv">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResultAdv Show(
    string FormCaption,
    string Message,
    MessageBoxButtonsAdv messageBoxButtonsAdv,
    IMMessageBoxImage image)
  {
    return IMMessageBox.Show(FormCaption, Message, messageBoxButtonsAdv, IMMessageBox.GetImage(image));
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="messageBoxButtons">Кнопки</param>
  /// <param name="messageDetails">Список строк с детальной информацией</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResult Show(
    string FormCaption,
    string Message,
    MessageBoxButtons messageBoxButtons,
    IList<string> messageDetails = null)
  {
    return IMMessageBox.Show(FormCaption, Message, messageBoxButtons, (Image) null, messageDetails);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="Buttons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <param name="messageDetails">Список строк с детальной информацией</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResult Show(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    Image image,
    IList<string> messageDetails = null)
  {
    return IMMessageBox.ShowMessageBox(FormCaption, Message, Buttons, image, (Form) null, messageDetails);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="messageBoxButtons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResult Show(
    string FormCaption,
    string Message,
    MessageBoxButtons messageBoxButtons,
    Image image,
    IList<string> messageDetails = null)
  {
    IMMessageBoxButton[] Buttons = (IMMessageBoxButton[]) null;
    switch (messageBoxButtons)
    {
      case MessageBoxButtons.OK:
        Buttons = new IMMessageBoxButton[1]
        {
          new IMMessageBoxButton("OK", DialogResult.OK)
        };
        break;
      case MessageBoxButtons.OKCancel:
        Buttons = new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("OK", DialogResult.OK),
          new IMMessageBoxButton("Отмена", DialogResult.Cancel)
        };
        break;
      case MessageBoxButtons.AbortRetryIgnore:
        Buttons = new IMMessageBoxButton[3]
        {
          new IMMessageBoxButton("Прервать", DialogResult.Abort),
          new IMMessageBoxButton("Повтор", DialogResult.Retry),
          new IMMessageBoxButton("Пропустить", DialogResult.Ignore)
        };
        break;
      case MessageBoxButtons.YesNoCancel:
        Buttons = new IMMessageBoxButton[3]
        {
          new IMMessageBoxButton("Да", DialogResult.Yes),
          new IMMessageBoxButton("Нет", DialogResult.No),
          new IMMessageBoxButton("Отмена", DialogResult.Cancel)
        };
        break;
      case MessageBoxButtons.YesNo:
        Buttons = new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Да", DialogResult.Yes),
          new IMMessageBoxButton("Нет", DialogResult.No)
        };
        break;
      case MessageBoxButtons.RetryCancel:
        Buttons = new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Повтор", DialogResult.Retry),
          new IMMessageBoxButton("Отмена", DialogResult.Cancel)
        };
        break;
    }
    return IMMessageBox.ShowMessageBox(FormCaption, Message, Buttons, image, (Form) null, messageDetails);
  }

  /// <summary>Отобразить окно с сообщением</summary>
  /// <param name="FormCaption">Заголовок окна</param>
  /// <param name="Message">Сообщение</param>
  /// <param name="messageBoxButtons">Кнопки</param>
  /// <param name="image">Картинка</param>
  /// <returns>Тип нажатой кнопки</returns>
  public static DialogResultAdv Show(
    string FormCaption,
    string Message,
    MessageBoxButtonsAdv messageBoxButtons,
    Image image)
  {
    IMMessageBoxButton[] Buttons = (IMMessageBoxButton[]) null;
    switch (messageBoxButtons)
    {
      case MessageBoxButtonsAdv.OK:
        Buttons = new IMMessageBoxButton[1]
        {
          new IMMessageBoxButton("OK", DialogResultAdv.OK)
        };
        break;
      case MessageBoxButtonsAdv.OKCancel:
        Buttons = new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("OK", DialogResultAdv.OK),
          new IMMessageBoxButton("Отмена", DialogResultAdv.Cancel)
        };
        break;
      case MessageBoxButtonsAdv.AbortRetryIgnore:
        Buttons = new IMMessageBoxButton[3]
        {
          new IMMessageBoxButton("Прервать", DialogResultAdv.Abort),
          new IMMessageBoxButton("Повтор", DialogResultAdv.Retry),
          new IMMessageBoxButton("Пропустить", DialogResultAdv.Ignore)
        };
        break;
      case MessageBoxButtonsAdv.YesNoCancel:
        Buttons = new IMMessageBoxButton[3]
        {
          new IMMessageBoxButton("Да", DialogResultAdv.Yes),
          new IMMessageBoxButton("Нет", DialogResultAdv.No),
          new IMMessageBoxButton("Отмена", DialogResultAdv.Cancel)
        };
        break;
      case MessageBoxButtonsAdv.YesNo:
        Buttons = new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Да", DialogResultAdv.Yes),
          new IMMessageBoxButton("Нет", DialogResultAdv.No)
        };
        break;
      case MessageBoxButtonsAdv.RetryCancel:
        Buttons = new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Повтор", DialogResultAdv.Retry),
          new IMMessageBoxButton("Отмена", DialogResultAdv.Cancel)
        };
        break;
      case MessageBoxButtonsAdv.Ignore_IgnoreAll_Abort:
        Buttons = new IMMessageBoxButton[3]
        {
          new IMMessageBoxButton("Игнорировать", DialogResultAdv.Ignore),
          new IMMessageBoxButton("Игнорировать все", DialogResultAdv.IgnoreAll),
          new IMMessageBoxButton("Прервать", DialogResultAdv.Abort)
        };
        break;
    }
    return IMMessageBox.ShowMessageBoxAdv(FormCaption, Message, Buttons, image);
  }

  /// <summary>
  /// Преобразовать тип нажатой кнопки DialogResultAdv в DialogResult
  /// </summary>
  /// <param name="dialogResultAdv">Тип нажатой кнопки DialogResultAdv</param>
  /// <returns>Тип нажатой кнопки DialogResult</returns>
  private static DialogResult DialogResultAdv_To_DialogResult(DialogResultAdv dialogResultAdv)
  {
    switch (dialogResultAdv)
    {
      case DialogResultAdv.None:
        return DialogResult.None;
      case DialogResultAdv.OK:
        return DialogResult.OK;
      case DialogResultAdv.Cancel:
        return DialogResult.Cancel;
      case DialogResultAdv.Abort:
        return DialogResult.Abort;
      case DialogResultAdv.Retry:
        return DialogResult.Retry;
      case DialogResultAdv.Ignore:
        return DialogResult.Ignore;
      case DialogResultAdv.Yes:
        return DialogResult.Yes;
      case DialogResultAdv.No:
        return DialogResult.No;
      default:
        return DialogResult.None;
    }
  }

  /// <summary>
  /// Преобразовать тип нажатой кнопки DialogResult в DialogResultAdv
  /// </summary>
  /// <param name="dialogResult">Тип нажатой кнопки DialogResult</param>
  /// <returns>Тип нажатой кнопки DialogResultAdv</returns>
  private static DialogResultAdv DialogResult_To_DialogResultAdv(DialogResult dialogResult)
  {
    switch (dialogResult)
    {
      case DialogResult.None:
        return DialogResultAdv.None;
      case DialogResult.OK:
        return DialogResultAdv.OK;
      case DialogResult.Cancel:
        return DialogResultAdv.Cancel;
      case DialogResult.Abort:
        return DialogResultAdv.Abort;
      case DialogResult.Retry:
        return DialogResultAdv.Retry;
      case DialogResult.Ignore:
        return DialogResultAdv.Ignore;
      case DialogResult.Yes:
        return DialogResultAdv.Yes;
      case DialogResult.No:
        return DialogResultAdv.No;
      default:
        return DialogResultAdv.None;
    }
  }

  private static Form InitializeForm()
  {
    Form form = new Form();
    form.AutoScaleDimensions = new SizeF(6f, 13f);
    form.AutoScaleMode = AutoScaleMode.Font;
    form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    form.MaximizeBox = false;
    form.Name = "MessageBoxEx";
    form.StartPosition = FormStartPosition.CenterParent;
    form.SuspendLayout();
    return form;
  }

  private static Label InitializeLabel(string Text)
  {
    Label label = new Label();
    label.Location = new Point(0, 0);
    label.Name = "labelMessage";
    label.Text = Text;
    label.AutoSize = true;
    label.TextAlign = ContentAlignment.MiddleLeft;
    label.TabStop = false;
    return label;
  }

  private static PictureBox InitializePicture(Image image)
  {
    PictureBox pictureBox = new PictureBox();
    ((ISupportInitialize) pictureBox).BeginInit();
    pictureBox.Image = image;
    pictureBox.Name = "picture";
    pictureBox.Size = new Size(Consts.ImageSize, Consts.ImageSize);
    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox.TabStop = false;
    ((ISupportInitialize) pictureBox).EndInit();
    return pictureBox;
  }

  private static Button InitializeButton(IMMessageBoxButton button)
  {
    Button button1 = new Button();
    button1.Text = button.Caption;
    button1.Tag = button.Tag;
    SizeF text = IMMessageBox.CalculateText((Control) button1, button.Caption);
    button1.Size = new Size(Consts.ButtonMinWidth > Convert.ToInt32(text.Width) + Consts.ButtonWidthAdd ? Consts.ButtonMinWidth : Convert.ToInt32(text.Width) + Consts.ButtonWidthAdd, Consts.ButtonHeigth);
    button1.DialogResult = button.MessageResult;
    return button1;
  }

  private static DialogResult ShowMessageBox(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    Image image,
    Form parent,
    IList<string> messageDetails = null)
  {
    using (Form form = IMMessageBox.InitializeForm())
    {
      if (parent != null)
        form.Owner = parent;
      IMMessageBox.SetForm(form, FormCaption, Message, Buttons, image, messageDetails);
      return form.ShowDialog();
    }
  }

  private static DialogResultAdv ShowMessageBoxAdv(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    Image image,
    IList<string> messageDetails = null)
  {
    using (Form form = IMMessageBox.InitializeForm())
    {
      IMMessageBox.SetForm(form, FormCaption, Message, Buttons, image, messageDetails);
      form.ShowInTaskbar = false;
      form.FormBorderStyle = FormBorderStyle.FixedDialog;
      form.ShowIcon = false;
      form.StartPosition = FormStartPosition.CenterScreen;
      int num = (int) form.ShowDialog();
      return form.Tag == null || !(form.Tag is IMMessageBoxButton) ? DialogResultAdv.None : ((IMMessageBoxButton) form.Tag).MessageResultAdv;
    }
  }

  private static object ShowMessageBoxEx(
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    Image image,
    IList<string> messageDetails = null)
  {
    using (Form form = IMMessageBox.InitializeForm())
    {
      IMMessageBox.SetForm(form, FormCaption, Message, Buttons, image, messageDetails);
      int num = (int) form.ShowDialog();
      return form.Tag;
    }
  }

  private static void SetForm(
    Form form,
    string FormCaption,
    string Message,
    IMMessageBoxButton[] Buttons,
    Image image,
    IList<string> messageDetails = null)
  {
    int num1 = 0;
    int num2 = 0;
    form.Text = FormCaption;
    Label label = IMMessageBox.InitializeLabel(Message);
    form.Controls.Add((Control) label);
    int num3 = num1 + label.Width;
    int borderSize = Consts.BorderSize;
    int num4;
    if (image != null)
    {
      PictureBox pictureBox = IMMessageBox.InitializePicture(image);
      form.Controls.Add((Control) pictureBox);
      num3 += Consts.ImageToLabelSize + pictureBox.Width;
      num4 = num2 + (label.Height > pictureBox.Height ? label.Height : pictureBox.Height);
      pictureBox.Left = Consts.BorderSize;
      pictureBox.Top = Consts.BorderSize + (num4 - pictureBox.Height) / 2;
      borderSize += pictureBox.Width + Consts.ImageToLabelSize;
    }
    else
      num4 = num2 + label.Height;
    label.Top = Consts.BorderSize + (num4 - label.Height) / 2;
    label.Left = borderSize;
    if (messageDetails != null && messageDetails.Count > 0 && messageDetails.Max<string>((Func<string, int>) (s => s.Length)) > 0)
    {
      int num5 = num3;
      num4 += Consts.ButtonHeigth * 4 + Consts.BorderSize + Consts.ImageToLabelSize;
      TextBox textBox = new TextBox();
      textBox.Multiline = true;
      textBox.ScrollBars = ScrollBars.Both;
      textBox.TextAlign = HorizontalAlignment.Left;
      textBox.ReadOnly = true;
      form.Controls.Add((Control) textBox);
      textBox.Width = num5;
      textBox.Height = Consts.ButtonHeigth * 4;
      textBox.Top = label.Top + label.Height + Consts.ImageToLabelSize;
      textBox.Left = Consts.BorderSize;
      textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBox.Margin = new Padding(Consts.BorderSize, 0, Consts.BorderSize, 0);
      textBox.Text = string.Join(Environment.NewLine, (IEnumerable<string>) messageDetails);
    }
    ArrayList arrayList = new ArrayList();
    int num6 = 0;
    foreach (IMMessageBoxButton button1 in Buttons)
    {
      Button button2 = IMMessageBox.InitializeButton(button1);
      arrayList.Add((object) button2);
      num6 += Convert.ToInt32(button2.Width) + Consts.ButtonToButtonSize;
    }
    if (num6 > num3)
      num3 = num6;
    int num7 = Consts.BorderSize + (num3 - num6) / 2 + Consts.BorderSize;
    for (int index = 0; index < arrayList.Count; ++index)
    {
      Button button = arrayList[index] as Button;
      if (button.Tag != null)
        button.Click += new EventHandler(IMMessageBox.button_Click);
      form.Controls.Add((Control) button);
      if (Buttons[index].IsDefaultButton && form.AcceptButton == null)
      {
        form.AcceptButton = (IButtonControl) button;
        form.ActiveControl = (Control) button;
      }
      button.Top = num4 + Consts.BorderSize * 2;
      button.Left = num7;
      num7 += button.Width + Consts.ButtonToButtonSize;
      button.TabIndex = index;
    }
    form.ClientSize = new Size(num3 + Consts.BorderSize * 2, num4 + Consts.BorderSize * 3 + Consts.ButtonHeigth);
    form.ResumeLayout(false);
    form.PerformLayout();
  }

  private static void button_Click(object sender, EventArgs e)
  {
    ((Control) sender).Parent.Tag = ((Control) sender).Tag;
    if (((Control) sender).Tag == null || !(((Control) sender).Tag is IMMessageBoxButton) || !(((Control) sender).Parent is Form) || ((IMMessageBoxButton) ((Control) sender).Tag).MessageResultAdv == DialogResultAdv.No)
      return;
    ((Form) ((Control) sender).Parent).Close();
  }

  private static SizeF CalculateText(Control control, string text)
  {
    using (Graphics graphics = control.CreateGraphics())
    {
      int width = Screen.PrimaryScreen.WorkingArea.Width / 100 * 50;
      return graphics.MeasureString(text, control.Font, width, StringFormat.GenericDefault);
    }
  }

  public static Image GetImage(IMMessageBoxImage image)
  {
    if (ServicesManager.GetService(typeof (IBigImageList)) is IBigImageList service)
    {
      string name = "";
      switch (image)
      {
        case IMMessageBoxImage.Error:
          name = "imgError";
          break;
        case IMMessageBoxImage.Warning:
          name = "imgWarning";
          break;
        case IMMessageBoxImage.Information:
          name = "imgInfo";
          break;
        case IMMessageBoxImage.Question:
          name = "imgHelp";
          break;
      }
      if (name != "")
      {
        int index = service.ImageIndex(name);
        if (index != -1)
          return service.ImageList.Images[index];
      }
    }
    return (Image) null;
  }
}

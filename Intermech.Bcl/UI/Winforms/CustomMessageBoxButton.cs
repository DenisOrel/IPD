
// Type: Intermech.UI.Winforms.CustomMessageBoxButton
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>Описывает кнопку настраиваемого MessageBox.</summary>
    public sealed class CustomMessageBoxButton : INotifyPropertyChanged
    {
      private string text;
      private bool isDefaultButton;
      private bool isCancelButton;
      private DialogResult dialogResult;
      private object customDialogResult;

      /// <summary>Создает объект.</summary>
      public CustomMessageBoxButton()
      {
        this.text = "Button";
        this.dialogResult = DialogResult.None;
      }

      /// <summary>Возвращает или задает текст кнопки.</summary>
      /// <exception cref="T:System.ArgumentNullException">Значение свойства равно null</exception>
      public string Text
      {
        get => this.text;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (value));
          if (!(this.text != value))
            return;
          this.text = value;
          this.RaisePropertyChanged(nameof (Text));
        }
      }

      /// <summary>
      /// Возвращает или задает флаг кнопки по умолчанию.
      /// Такая кнопка используется при нажатии клавиши ENTER в диалоговом окне.
      /// </summary>
      public bool IsDefaultButton
      {
        get => this.isDefaultButton;
        set
        {
          if (this.isDefaultButton == value)
            return;
          this.isDefaultButton = value;
          this.RaisePropertyChanged(nameof (IsDefaultButton));
        }
      }

      /// <summary>
      /// Возвращает или задает флаг кнопки отмены по умолчанию.
      /// Такая кнопка используется при нажатии клавиши ESCAPE в диалоговом окне.
      /// </summary>
      public bool IsCancelButton
      {
        get => this.isCancelButton;
        set
        {
          if (this.isCancelButton == value)
            return;
          this.isCancelButton = value;
          this.RaisePropertyChanged(nameof (IsCancelButton));
        }
      }

      /// <summary>
      /// Возвращает или задает результат работы диалогового окна.
      /// Значение свойства используется только в том случае, если свойство <see cref="P:Intermech.UI.Winforms.CustomMessageBoxButton.CustomDialogResult" /> не задано.
      /// </summary>
      public DialogResult DialogResult
      {
        get => this.dialogResult;
        set
        {
          if (this.dialogResult == value)
            return;
          this.dialogResult = value;
          this.RaisePropertyChanged(nameof (DialogResult));
        }
      }

      /// <summary>
      /// Возвращает или задает нестандартный результат работы диалогового окна.
      /// Значение этого свойства имеет приоритет перед свойством <see cref="P:Intermech.UI.Winforms.CustomMessageBoxButton.DialogResult" />.
      /// Если значение задано, то используется это свойство, а не <see cref="P:Intermech.UI.Winforms.CustomMessageBoxButton.DialogResult" />.
      /// </summary>
      public object CustomDialogResult
      {
        get => this.customDialogResult;
        set
        {
          if (this.customDialogResult == value)
            return;
          this.customDialogResult = value;
          this.RaisePropertyChanged(nameof (CustomDialogResult));
        }
      }

      private void RaisePropertyChanged(string propertyName)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }

      public event PropertyChangedEventHandler PropertyChanged;
    }
}

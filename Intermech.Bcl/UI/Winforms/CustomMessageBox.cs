
// Type: Intermech.UI.Winforms.CustomMessageBox
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Отображает MessageBox с полностью настраиваемым набором кнопок.
    /// Вид окна максимально приближен к системному, размер и положение элементов окна
    /// автоматически подстраивается под текст и количество кнопок.
    /// </summary>
    public sealed class CustomMessageBox : INotifyPropertyChanged, ICustomMessageBoxData
    {
      private string caption;
      private string text;
      private MessageBoxIcon icon;
      private Image customIcon;
      private ObservableCollection<CustomMessageBoxButton> buttons;

      /// <summary>Создает объект.</summary>
      public CustomMessageBox()
      {
        this.caption = string.Empty;
        this.text = string.Empty;
        this.icon = MessageBoxIcon.None;
        this.buttons = new ObservableCollection<CustomMessageBoxButton>();
      }

      /// <summary>Возвращает или задает заголовок сообщения.</summary>
      /// <exception cref="T:System.ArgumentNullException">Значение свойства равно null</exception>
      public string Caption
      {
        get => this.caption;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (value));
          if (!(this.caption != value))
            return;
          this.caption = value;
          this.RaisePropertyChanged(nameof (Caption));
        }
      }

      /// <summary>Возвращает или задает текст сообщения.</summary>
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
      /// Возвращает или задает стандартную иконку для окна сообщения.
      /// Значение свойства используется только в том случае, если свойство <see cref="P:Intermech.UI.Winforms.CustomMessageBox.CustomIcon" /> не задано.
      /// </summary>
      public MessageBoxIcon Icon
      {
        get => this.icon;
        set
        {
          if (this.icon == value)
            return;
          this.icon = value;
          this.RaisePropertyChanged(nameof (Icon));
        }
      }

      /// <summary>
      /// Возвращает или задает нестандартную иконку для окна сообщения.
      /// Значение этого свойства имеет приоритет перед свойством <see cref="P:Intermech.UI.Winforms.CustomMessageBox.Icon" />.
      /// Если значение задано, то используется это свойство, а не <see cref="P:Intermech.UI.Winforms.CustomMessageBox.Icon" />.
      /// </summary>
      public Image CustomIcon
      {
        get => this.customIcon;
        set
        {
          if (this.customIcon == value)
            return;
          this.customIcon = value;
          this.RaisePropertyChanged(nameof (CustomIcon));
        }
      }

      /// <summary>Возвращает коллекцию кнопок для окна сообщения.</summary>
      public ObservableCollection<CustomMessageBoxButton> Buttons => this.buttons;

      /// <summary>Показывает сообщение и возвращает выбор пользователя.</summary>
      /// <param name="owner">Родительское окно. Значение параметра может быть не задано и равно null</param>
      /// <returns>Выбор пользователя. В качестве возвращаемого значения используется значение свойства
      /// <see cref="P:Intermech.UI.Winforms.CustomMessageBoxButton.CustomDialogResult" /> или <see cref="P:Intermech.UI.Winforms.CustomMessageBoxButton.DialogResult" />
      /// </returns>
      public object ShowDialog(IWin32Window owner = null)
      {
        this.ValidateAndPrepare();
        using (CustomMessageBoxWindow messageBoxWindow = new CustomMessageBoxWindow())
        {
          messageBoxWindow.InitializeDialog((ICustomMessageBoxData) this);
          DialogResult dialogResult = messageBoxWindow.ShowDialog(owner);
          return messageBoxWindow.CustomDialogResult ?? (object) dialogResult;
        }
      }

      private void ValidateAndPrepare()
      {
        if (this.buttons.Count == 0)
          this.buttons.Add(this.CreateOKButton());
        this.EnsureDefaultButtonIsPresent();
        this.EnsureCancelButtonIsSingle();
      }

      private CustomMessageBoxButton CreateOKButton()
      {
        return new CustomMessageBoxButton()
        {
          Text = "OK",
          IsDefaultButton = true,
          CustomDialogResult = (object) DialogResult.OK
        };
      }

      private void EnsureDefaultButtonIsPresent()
      {
        CustomMessageBoxButton messageBoxButton = this.buttons.FirstOrDefault((Func<CustomMessageBoxButton, bool>) (x => x.IsDefaultButton));
        if (messageBoxButton == null)
        {
          this.buttons[0].IsDefaultButton = true;
        }
        else
        {
          foreach (CustomMessageBoxButton button in (Collection<CustomMessageBoxButton>) this.buttons)
          {
            if (button.IsDefaultButton && button != messageBoxButton)
              button.IsDefaultButton = false;
          }
        }
      }

      private void EnsureCancelButtonIsSingle()
      {
        CustomMessageBoxButton messageBoxButton = this.buttons.FirstOrDefault((Func<CustomMessageBoxButton, bool>) (x => x.IsCancelButton));
        if (messageBoxButton == null)
          return;
        foreach (CustomMessageBoxButton button in (Collection<CustomMessageBoxButton>) this.buttons)
        {
          if (button.IsCancelButton && button != messageBoxButton)
            button.IsCancelButton = false;
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

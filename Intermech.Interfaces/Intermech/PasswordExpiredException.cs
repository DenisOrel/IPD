
// Type: Intermech.PasswordExpiredException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>Срок действия Вашего пароля истек</summary>
    [Serializable]
    public class PasswordExpiredException : KernelException
    {
      private bool _showDialog = true;

      public PasswordExpiredException(string message)
        : base(message)
      {
      }

      public PasswordExpiredException(string message, bool showDlg)
        : base(message)
      {
        this._showDialog = showDlg;
      }

      public PasswordExpiredException()
        : base(LocalizationHolder.rm.GetString("Interfaces_201"))
      {
      }

      protected PasswordExpiredException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      public override string Message
      {
        get
        {
          return base.Message != null && base.Message != string.Empty ? base.Message : LocalizationHolder.rm.GetString("Interfaces_201");
        }
      }

      /// <summary>
      /// Свойство означает нужно ли показывать диалог с информацией о том, что истек срок действия пароля, или можно сразу приступить к его смене
      /// </summary>
      public bool ShowDialog => this._showDialog;
    }
}


// Type: Intermech.Diagnostics.WindowsApiException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.WindowsDll;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация связанная с работой с Windows API</summary>
    [Serializable]
    public class WindowsApiException : Win32Exception, ISerializable
    {
      private int _preventRecursionIndex;
      [CanBeNull]
      private static FieldInfo _innerExceptionField;
      private static bool _innerExceptionFieldLoaded;

      /// <summary>Тест сообщения</summary>
      [CanBeNull]
      [CanBeEmpty]
      public string CustomMessage { get; }

      /// <summary>Функция Windows, вызов которой вызвал ошибку</summary>
      [NotNull]
      [NotWhitespace]
      public string CalledFunction { get; }

      /// <summary>Значения аргументов, с которыми была вызвана функция Win32</summary>
      [NotNull]
      [CanBeEmpty]
      [ItemNotEmpty]
      public ArgumentDescriptor[] Arguments { get; }

      /// <summary>Принудительно создаёт (не выбрасывает!) исключительную ситуацию типа <see cref="T:Intermech.Diagnostics.WindowsApiException" /> вне зависимости от значения <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /></summary>
      /// <param name="calledFunction">Функция Windows, вызов которой вызвал ошибку</param>
      /// <param name="arguments">Значения аргументов, с которыми была вызвана функция Win32</param>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static WindowsApiException GetLastForce(
        [NotNull, NotWhitespace] string calledFunction,
        [NotNull] params ArgumentDescriptor[] arguments)
      {
        return new WindowsApiException(Marshal.GetLastWin32Error(), calledFunction, arguments);
      }

      /// <summary>Создаёт (не выбрасывает!) исключительную ситуацию типа <see cref="T:Intermech.Diagnostics.WindowsApiException" /> если <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /> вернёт значение, отличное от 0</summary>
      /// <param name="calledFunction">Функция Windows, вызов которой вызвал ошибку</param>
      /// <param name="arguments">Значения аргументов, с которыми была вызвана функция Win32</param>
      /// <returns>Исключительная ситуация типа <see cref="T:Intermech.Diagnostics.WindowsApiException" /> если <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /> вернёт значение, отличное от 0, иначе null</returns>
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static WindowsApiException GetLastOrNull(
        [NotNull, NotWhitespace] string calledFunction,
        [NotNull] params ArgumentDescriptor[] arguments)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        return lastWin32Error == 0 ? (WindowsApiException) null : new WindowsApiException(lastWin32Error, calledFunction, arguments);
      }

      [ContractAnnotation("=> true, exception: notnull; => false, exception: null")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetLast(
        [NotNull, NotWhitespace] string calledFunction,
        out WindowsApiException exception,
        [NotNull] params ArgumentDescriptor[] arguments)
      {
        return (exception = WindowsApiException.GetLastOrNull(calledFunction, arguments)) != null;
      }

      public WindowsApiException(
        [CanBeEmpty] int errorCode,
        [NotNull, NotWhitespace] string calledFunction,
        [NotNull] params ArgumentDescriptor[] arguments)
        : this(errorCode, (string) null, 0, (Exception) null, calledFunction, arguments)
      {
      }

      public WindowsApiException(
        [CanBeEmpty] int errorCode,
        [CanBeNull] Exception innerException,
        [NotNull, NotWhitespace] string calledFunction,
        [NotNull] params ArgumentDescriptor[] arguments)
        : this(errorCode, (string) null, 0, innerException, calledFunction, arguments)
      {
      }

      internal WindowsApiException(
        [CanBeEmpty] int errorCode,
        [CanBeNull] string customMessage,
        int preventRecursionIndex,
        [CanBeNull] Exception innerException,
        [NotNull, NotWhitespace] string calledFunction,
        [NotNull] params ArgumentDescriptor[] arguments)
        : base(errorCode, customMessage)
      {
        this.CustomMessage = customMessage;
        if (innerException != null)
          this.InnerException = innerException;
        this.CalledFunction = calledFunction;
        this.Arguments = arguments;
      }

      protected WindowsApiException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._preventRecursionIndex = info.GetInt32(nameof (_preventRecursionIndex));
        this.CustomMessage = info.GetString(nameof (CustomMessage));
        this.CalledFunction = info.GetString(nameof (CalledFunction));
        this.Arguments = (ArgumentDescriptor[]) info.GetValue(nameof (Arguments), typeof (ArgumentDescriptor[]));
      }

      [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_preventRecursionIndex", this._preventRecursionIndex);
        info.AddValue("CustomMessage", (object) this.CustomMessage);
        info.AddValue("CalledFunction", (object) this.CalledFunction);
        info.AddValue("Arguments", (object) this.Arguments);
      }

      [CanBeNull]
      private static FieldInfo InnerExceptionField
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          if (!WindowsApiException._innerExceptionFieldLoaded)
            return WindowsApiException._innerExceptionField;
          WindowsApiException._innerExceptionField = typeof (WindowsApiException).GetField("_innerException", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.SetField);
          WindowsApiException._innerExceptionFieldLoaded = true;
          return WindowsApiException._innerExceptionField;
        }
      }

      [CanBeNull]
      public new Exception InnerException
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => base.InnerException;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private set
        {
          FieldInfo innerExceptionField = WindowsApiException.InnerExceptionField;
          if (!(innerExceptionField != (FieldInfo) null) || value == base.InnerException)
            return;
          innerExceptionField.SetValue((object) this, (object) value);
        }
      }

      [CanBeNull]
      protected string OriginalMessage => base.Message;

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.CustomMessage))
            return this.CustomMessage;
          StringBuilder stringBuilder = new StringBuilder();
          if (this.ErrorCode != 0)
            stringBuilder.AppendLine("Windows API Error 0x" + Convert.ToString(this.ErrorCode, 16 /*0x10*/));
          else if (this.InnerException != null)
            stringBuilder.AppendLine(this.InnerException.Message);
          else
            stringBuilder.AppendLine("Unknown windows API Error (no error code)");
          if (!string.IsNullOrWhiteSpace(this.CalledFunction))
          {
            stringBuilder.Append(this.CalledFunction);
            stringBuilder.Append('(');
            int length = this.Arguments.Length;
            int num = 0;
            foreach (ArgumentDescriptor argumentDescriptor in this.Arguments)
            {
              stringBuilder.Append(argumentDescriptor.ToString());
              if (++num < length)
                stringBuilder.Append(" ,");
            }
            stringBuilder.AppendLine(")");
          }
          if (this._preventRecursionIndex < 2)
          {
            string winApiErrorText = WindowsApiException.GetWinApiErrorText(this.ErrorCode, this._preventRecursionIndex, this);
            if (this._preventRecursionIndex < 2 && !string.IsNullOrWhiteSpace(winApiErrorText))
            {
              stringBuilder.AppendLine();
              stringBuilder.AppendLine(winApiErrorText);
            }
          }
          if (this.InnerException != null && this.ErrorCode != 0)
          {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Inner exception:");
            stringBuilder.AppendLine(this.InnerException.Message);
          }
          return stringBuilder.ToString();
        }
      }

      [NotNull]
      [NotWhitespace]
      public string ShortText
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return WindowsApiException.GetWinApiErrorText(this.ErrorCode, 0, (WindowsApiException) null);
        }
      }

      [NotNull]
      [NotWhitespace]
      public string CodeAndShortText
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return this.ErrorCode == 0 ? this.InnerException?.Message ?? "Unknown windows API error (no error code)" : $"Error 0x{Convert.ToString(this.ErrorCode, 16 /*0x10*/)} - {this.ShortText}";
        }
      }

      [NotNull]
      [NotWhitespace]
      public static string GetWinApiErrorText([NotEmpty] int errorCode)
      {
        return WindowsApiException.GetWinApiErrorText(errorCode, 0, (WindowsApiException) null);
      }

      [NotNull]
      [NotWhitespace]
      private static string GetWinApiErrorText(
        [NotEmpty] int errorCode,
        int preventRecursion,
        [CanBeNull] WindowsApiException exceptionToDecode)
      {
        if (errorCode == 0)
          return "Unknown error (no error code)";
        IntPtr zero = IntPtr.Zero;
        if (Kernel32.FormatMessage(FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem | FormatMessageFlags.IgnoreInserts, IntPtr.Zero, (uint) errorCode, 0U, ref zero, 0U, IntPtr.Zero) != 0U)
        {
          if (!(zero == IntPtr.Zero))
          {
            string winApiErrorText;
            try
            {
              winApiErrorText = Marshal.PtrToStringAnsi(zero)?.Trim();
            }
            finally
            {
              IntPtr num = Kernel32.LocalFree(zero);
              if (num != IntPtr.Zero)
              {
                int lastWin32Error = Marshal.GetLastWin32Error();
                if (lastWin32Error != 0)
                {
                  if (exceptionToDecode != null)
                    exceptionToDecode._preventRecursionIndex = 2;
                  // ISSUE: explicit reference operation
                  throw new WindowsApiException(lastWin32Error, (string) null, preventRecursion + 1, (Exception) exceptionToDecode, "Kernel32.dll::LocalFree", new ArgumentDescriptor[1]
                  {
                    (ArgumentDescriptor) @(typeof (IntPtr), (object) num)
                  });
                }
              }
            }
            if (string.IsNullOrWhiteSpace(winApiErrorText))
              winApiErrorText = "WindowsApiException.GetWinApiErrorText() error!";
            return winApiErrorText;
          }
        }
        int lastWin32Error1 = Marshal.GetLastWin32Error();
        if (lastWin32Error1 != 0)
        {
          if (exceptionToDecode != null)
            exceptionToDecode._preventRecursionIndex = 2;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          throw new WindowsApiException(lastWin32Error1, (string) null, preventRecursion + 1, (Exception) exceptionToDecode, "Kernel32.dll::FormatMessage", new ArgumentDescriptor[7]
          {
            (ArgumentDescriptor) @(typeof (FormatMessageFlags), (object) (FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem | FormatMessageFlags.IgnoreInserts)),
            (ArgumentDescriptor) @(typeof (IntPtr), (object) IntPtr.Zero),
            (ArgumentDescriptor) @(typeof (uint), (object) errorCode),
            (ArgumentDescriptor) @(typeof (uint), (object) 0),
            (ArgumentDescriptor) @(typeof (IntPtr), (object) IntPtr.Zero),
            (ArgumentDescriptor) @(typeof (uint), (object) 0),
            (ArgumentDescriptor) @(typeof (IntPtr), (object) IntPtr.Zero)
          });
        }
        return "Kernel32.FormatMessage() unknown error!";
      }
    }
}

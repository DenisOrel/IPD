
// Type: Intermech.Diagnostics.ArgumentInvalidCastException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "Ошибка конвертации аргумента".</summary>
    [Serializable]
    public class ArgumentInvalidCastException : ArgumentException, ISerializable
    {
      [CanBeNull]
      [NotWhitespace]
      public string TypeName { get; }

      [CanBeNull]
      [NotWhitespace]
      public string TypeFullName { get; }

      protected ArgumentInvalidCastException()
      {
      }

      protected ArgumentInvalidCastException([CanBeNull] string message)
        : this((Type) null, message, (string) null, (Exception) null)
      {
      }

      protected ArgumentInvalidCastException([CanBeNull] string message, [CanBeNull] Exception innerException)
        : this((Type) null, message, (string) null, innerException)
      {
      }

      public ArgumentInvalidCastException([CanBeNull] Type type, [CanBeNull] string message)
        : this(type, message, (string) null, (Exception) null)
      {
      }

      public ArgumentInvalidCastException([CanBeNull] Type type, [CanBeNull] string message, [CanBeNull] Exception innerException)
        : this(type, message, (string) null, innerException)
      {
      }

      public ArgumentInvalidCastException([CanBeNull] Type type, [CanBeNull] string message, [CanBeNull] string paramName)
        : this(type, message, paramName, (Exception) null)
      {
      }

      public ArgumentInvalidCastException(
        [CanBeNull] Type type,
        [CanBeNull] string message,
        [CanBeNull] string paramName,
        [CanBeNull] Exception innerException)
        : base(message, paramName, innerException)
      {
        if (!(type != (Type) null))
          return;
        this.TypeName = type.Name;
        this.TypeFullName = type.FullName;
      }

      protected ArgumentInvalidCastException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.TypeName = info.GetString(nameof (TypeName));
        this.TypeFullName = info.GetString(nameof (TypeFullName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("TypeName", (object) this.TypeName);
        info.AddValue("TypeFullName", (object) this.TypeFullName);
      }

      [CanBeNull]
      protected string OriginalMessage => base.Message;

      [NotNull]
      public override string Message
      {
        get
        {
          if (string.IsNullOrWhiteSpace(this.TypeName))
            return "Invalid cast of argument.";
          if (string.IsNullOrWhiteSpace(this.ParamName))
            return $"Invalid cast of argument to {this.TypeName} type.";
          return $"Invalid cast of {this.ParamName} argument to {this.TypeName} type.";
        }
      }
    }
}

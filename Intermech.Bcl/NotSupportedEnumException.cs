
// Type: Intermech.NotSupportedEnumException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class NotSupportedEnumException : NotSupportedException
    {
      private const string NotSupportedValueProp = "_notSupportedValue";
      private readonly Enum notSupportedValue;

      public NotSupportedEnumException(Enum notSupportedValue)
        : base(NotSupportedEnumException.MakeErrorMessage(notSupportedValue))
      {
        this.notSupportedValue = notSupportedValue;
      }

      public NotSupportedEnumException(Enum notSupportedValue, string message)
        : base(message)
      {
        this.notSupportedValue = notSupportedValue;
      }

      private static string MakeErrorMessage(Enum notSupportedValue)
      {
        if (notSupportedValue == null)
          throw new ArgumentNullException(nameof (notSupportedValue));
        return $"Значение '{notSupportedValue}' типа '{notSupportedValue.GetType()}' не поддерживается.";
      }

      protected NotSupportedEnumException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.notSupportedValue = (Enum) info.GetValue("_notSupportedValue", typeof (Enum));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_notSupportedValue", (object) this.notSupportedValue, typeof (Enum));
      }
    }
}


// Type: Intermech.Mvp.PresenterPropertyException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Mvp
{
    [Serializable]
    public class PresenterPropertyException : MvpException
    {
      private static readonly string propertyNameField = "_propertyName";
      private string propertyName;

      public PresenterPropertyException(string propertyName)
        : this(propertyName, "Недопустимое значение свойства посредника MVP (presenter).")
      {
      }

      public PresenterPropertyException(string propertyName, string message)
        : base(message)
      {
        this.propertyName = propertyName != null ? propertyName : throw new ArgumentNullException(nameof (propertyName));
      }

      protected PresenterPropertyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.propertyName = info.GetString(PresenterPropertyException.propertyNameField);
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue(PresenterPropertyException.propertyNameField, (object) this.propertyName);
      }

      public string PropertyName => this.propertyName;
    }
}

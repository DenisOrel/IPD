
// Type: Intermech.Diagnostics.NotNullAfterAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>Признак того, что свойство или поле становится отличным от Null после вызова метода, имя которого передаётся в качестве параметра</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class NotNullAfterAttribute : Attribute
    {
      public NotNullAfterAttribute([NotNull, NotWhitespace] string methodName)
      {
        this.MethodName = methodName;
      }

      [NotNull]
      [NotWhitespace]
      public string MethodName { get; }
    }
}

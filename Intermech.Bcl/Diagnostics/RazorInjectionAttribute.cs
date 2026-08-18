
// Type: Intermech.Diagnostics.RazorInjectionAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorInjectionAttribute : Attribute
    {
      public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
      {
        this.Type = type;
        this.FieldName = fieldName;
      }

      [NotNull]
      public string Type { get; }

      [NotNull]
      public string FieldName { get; }
    }
}

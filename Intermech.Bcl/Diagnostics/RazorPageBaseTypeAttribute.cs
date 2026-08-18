
// Type: Intermech.Diagnostics.RazorPageBaseTypeAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorPageBaseTypeAttribute : Attribute
    {
      public RazorPageBaseTypeAttribute([NotNull] string baseType) => this.BaseType = baseType;

      public RazorPageBaseTypeAttribute([NotNull] string baseType, [NotNull] string pageName)
      {
        this.BaseType = baseType;
        this.PageName = pageName;
      }

      [NotNull]
      public string BaseType { get; }

      [CanBeNull]
      public string PageName { get; }
    }
}

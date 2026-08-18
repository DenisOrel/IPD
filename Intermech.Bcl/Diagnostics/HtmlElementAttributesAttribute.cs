
// Type: Intermech.Diagnostics.HtmlElementAttributesAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class HtmlElementAttributesAttribute : Attribute
    {
      public HtmlElementAttributesAttribute()
      {
      }

      public HtmlElementAttributesAttribute([NotNull] string name) => this.Name = name;

      [CanBeNull]
      public string Name { get; }
    }
}


// Type: Intermech.Diagnostics.AspChildControlTypeAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspChildControlTypeAttribute : Attribute
    {
      public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
      {
        this.TagName = tagName;
        this.ControlType = controlType;
      }

      [NotNull]
      public string TagName { get; }

      [NotNull]
      public Type ControlType { get; }
    }
}

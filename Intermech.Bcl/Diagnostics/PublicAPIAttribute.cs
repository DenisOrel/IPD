
// Type: Intermech.Diagnostics.PublicAPIAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// This attribute is intended to mark publicly available API
    /// which should not be removed and so is treated as used.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class PublicAPIAttribute : Attribute
    {
      public PublicAPIAttribute()
      {
      }

      public PublicAPIAttribute([NotNull] string comment) => this.Comment = comment;

      [CanBeNull]
      public string Comment { get; }
    }
}

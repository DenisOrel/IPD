
// Type: Intermech.Diagnostics.UsedImplicitlyAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
    /// so this symbol will not be reported as unused (as well as by other usage inspections).
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class UsedImplicitlyAttribute : Attribute
    {
      public UsedImplicitlyAttribute()
        : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
      {
      }

      public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
        : this(useKindFlags, ImplicitUseTargetFlags.Default)
      {
      }

      public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
        : this(ImplicitUseKindFlags.Default, targetFlags)
      {
      }

      public UsedImplicitlyAttribute(
        ImplicitUseKindFlags useKindFlags,
        ImplicitUseTargetFlags targetFlags)
      {
        this.UseKindFlags = useKindFlags;
        this.TargetFlags = targetFlags;
      }

      public ImplicitUseKindFlags UseKindFlags { get; }

      public ImplicitUseTargetFlags TargetFlags { get; }
    }
}

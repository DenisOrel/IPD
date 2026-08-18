
// Type: Intermech.Diagnostics.MeansImplicitUseAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Can be applied to attributes, type parameters, and parameters of a type assignable from <see cref="T:System.Type" /> .
    /// When applied to an attribute, the decorated attribute behaves the same as <see cref="T:Intermech.Diagnostics.UsedImplicitlyAttribute" />.
    /// When applied to a type parameter or to a parameter of type <see cref="T:System.Type" />,  indicates that the corresponding type
    /// is used implicitly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
    public sealed class MeansImplicitUseAttribute : Attribute
    {
      public MeansImplicitUseAttribute()
        : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
      {
      }

      public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
        : this(useKindFlags, ImplicitUseTargetFlags.Default)
      {
      }

      public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
        : this(ImplicitUseKindFlags.Default, targetFlags)
      {
      }

      public MeansImplicitUseAttribute(
        ImplicitUseKindFlags useKindFlags,
        ImplicitUseTargetFlags targetFlags)
      {
        this.UseKindFlags = useKindFlags;
        this.TargetFlags = targetFlags;
      }

      [UsedImplicitly]
      public ImplicitUseKindFlags UseKindFlags { get; }

      [UsedImplicitly]
      public ImplicitUseTargetFlags TargetFlags { get; }
    }
}

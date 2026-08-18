
// Type: Intermech.Diagnostics.ImplicitUseKindFlags
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Specify the details of implicitly used symbol when it is marked
    /// with <see cref="T:Intermech.Diagnostics.MeansImplicitUseAttribute" /> or <see cref="T:Intermech.Diagnostics.UsedImplicitlyAttribute" />.
    /// </summary>
    [Flags]
    public enum ImplicitUseKindFlags
    {
      Default = 7,
      /// <summary>Only entity marked with attribute considered used.</summary>
      Access = 1,
      /// <summary>Indicates implicit assignment to a member.</summary>
      Assign = 2,
      /// <summary>
      /// Indicates implicit instantiation of a type with fixed constructor signature.
      /// That means any unused constructor parameters won't be reported as such.
      /// </summary>
      InstantiatedWithFixedConstructorSignature = 4,
      /// <summary>Indicates implicit instantiation of a type.</summary>
      InstantiatedNoFixedConstructorSignature = 8,
    }
}

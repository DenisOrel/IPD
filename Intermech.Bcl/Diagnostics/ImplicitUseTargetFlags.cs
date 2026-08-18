
// Type: Intermech.Diagnostics.ImplicitUseTargetFlags
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Specify what is considered to be used implicitly when marked
    /// with <see cref="T:Intermech.Diagnostics.MeansImplicitUseAttribute" /> or <see cref="T:Intermech.Diagnostics.UsedImplicitlyAttribute" />.
    /// </summary>
    [Flags]
    public enum ImplicitUseTargetFlags
    {
      Default = 1,
      Itself = Default, // 0x00000001
      /// <summary>Members of entity marked with attribute are considered used.</summary>
      Members = 2,
      /// <summary> Inherited entities are considered used. </summary>
      WithInheritors = 4,
      /// <summary>Entity marked with attribute and all its members considered used.</summary>
      WithMembers = Members | Itself, // 0x00000003
    }
}

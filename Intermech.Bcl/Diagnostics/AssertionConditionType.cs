
// Type: Intermech.Diagnostics.AssertionConditionType
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Specifies assertion type. If the assertion method argument satisfies the condition,
    /// then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
      /// <summary>Marked parameter should be evaluated to true.</summary>
      IS_TRUE,
      /// <summary>Marked parameter should be evaluated to false.</summary>
      IS_FALSE,
      /// <summary>Marked parameter should be evaluated to null value.</summary>
      IS_NULL,
      /// <summary>Marked parameter should be evaluated to not null value.</summary>
      IS_NOT_NULL,
    }
}

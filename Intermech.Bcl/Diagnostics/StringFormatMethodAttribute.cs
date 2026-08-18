
// Type: Intermech.Diagnostics.StringFormatMethodAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Indicates that the marked method builds string by the format pattern and (optional) arguments.
    /// The parameter, which contains the format string, should be given in constructor. The format string
    /// should be in <see cref="M:System.String.Format(System.IFormatProvider,System.String,System.Object[])" />-like form.
    /// </summary>
    /// <example><code>
    /// [StringFormatMethod("message")]
    /// void ShowError(string message, params object[] args) { /* do something */ }
    /// 
    /// void Foo() {
    ///   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
    public sealed class StringFormatMethodAttribute : Attribute
    {
      /// <param name="formatParameterName">
      /// Specifies which parameter of an annotated method should be treated as the format string
      /// </param>
      public StringFormatMethodAttribute([NotNull] string formatParameterName)
      {
        this.FormatParameterName = formatParameterName;
      }

      [NotNull]
      public string FormatParameterName { get; }
    }
}

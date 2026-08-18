
// Type: Intermech.Diagnostics.ValueRangeAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Indicates that the integral value falls into the specified interval.
    /// It's allowed to specify multiple non-intersecting intervals.
    /// Values of interval boundaries are inclusive.
    /// </summary>
    /// <example><code>
    /// void Foo([ValueRange(0, 100)] int value) {
    ///   if (value == -1) { // Warning: Expression is always 'false'
    ///     ...
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true)]
    public sealed class ValueRangeAttribute : Attribute
    {
      public object From { get; }

      public object To { get; }

      public ValueRangeAttribute(long from, long to)
      {
        this.From = (object) from;
        this.To = (object) to;
      }

      public ValueRangeAttribute(long value) => this.From = this.To = (object) value;
    }
}

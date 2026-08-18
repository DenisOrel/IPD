
// Type: Intermech.Diagnostics.SortedAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>Признак того, что описанное перечисление отсортировано </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.GenericParameter)]
    public sealed class SortedAttribute : Attribute
    {
      public SortDirection SortDirection { get; }

      public SortedAttribute(SortDirection sortDirection = SortDirection.Ascending)
      {
        this.SortDirection = sortDirection;
      }
    }
}

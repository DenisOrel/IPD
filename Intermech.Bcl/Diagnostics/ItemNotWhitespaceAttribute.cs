
// Type: Intermech.Diagnostics.ItemNotWhitespaceAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>Признак того, что строки в описываемой коллекции/перечислении/списка/массива должны иметь хотя бы один символ, отличный от пробела</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ItemNotWhitespaceAttribute : Attribute
    {
    }
}

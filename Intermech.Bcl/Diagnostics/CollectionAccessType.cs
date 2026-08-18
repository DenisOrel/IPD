
// Type: Intermech.Diagnostics.CollectionAccessType
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Provides a value for the <see cref="T:Intermech.Diagnostics.CollectionAccessAttribute" /> to define
    /// how the collection method invocation affects the contents of the collection.
    /// </summary>
    [Flags]
    public enum CollectionAccessType
    {
      /// <summary>Method does not use or modify content of the collection.</summary>
      None = 0,
      /// <summary>Method only reads content of the collection but does not modify it.</summary>
      Read = 1,
      /// <summary>Method can change content of the collection but does not add new elements.</summary>
      ModifyExistingContent = 2,
      /// <summary>Method can add new elements to the collection.</summary>
      UpdatedContent = 6,
    }
}

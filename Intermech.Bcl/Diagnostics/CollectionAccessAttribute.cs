
// Type: Intermech.Diagnostics.CollectionAccessAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Indicates how method, constructor invocation, or property access
    /// over collection type affects the contents of the collection.
    /// Use <see cref="P:Intermech.Diagnostics.CollectionAccessAttribute.CollectionAccessType" /> to specify the access type.
    /// </summary>
    /// <remarks>
    /// Using this attribute only makes sense if all collection methods are marked with this attribute.
    /// </remarks>
    /// <example><code>
    /// public class MyStringCollection : List&lt;string&gt;
    /// {
    ///   [CollectionAccess(CollectionAccessType.Read)]
    ///   public string GetFirstString()
    ///   {
    ///     return this.ElementAt(0);
    ///   }
    /// }
    /// class Test
    /// {
    ///   public void Foo()
    ///   {
    ///     // Warning: Contents of the collection is never updated
    ///     var col = new MyStringCollection();
    ///     string x = col.GetFirstString();
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class CollectionAccessAttribute : Attribute
    {
      public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
      {
        this.CollectionAccessType = collectionAccessType;
      }

      public CollectionAccessType CollectionAccessType { get; }
    }
}

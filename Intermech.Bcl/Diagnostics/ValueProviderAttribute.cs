
// Type: Intermech.Diagnostics.ValueProviderAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Use this annotation to specify a type that contains static or const fields
    /// with values for the annotated property/field/parameter.
    /// The specified type will be used to improve completion suggestions.
    /// </summary>
    /// <example><code>
    /// namespace TestNamespace
    /// {
    ///   public class Constants
    ///   {
    ///     public static int INT_CONST = 1;
    ///     public const string STRING_CONST = "1";
    ///   }
    /// 
    ///   public class Class1
    ///   {
    ///     [ValueProvider("TestNamespace.Constants")] public int myField;
    ///     public void Foo([ValueProvider("TestNamespace.Constants")] string str) { }
    /// 
    ///     public void Test()
    ///     {
    ///       Foo(/*try completion here*/);//
    ///       myField = /*try completion here*/
    ///     }
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class ValueProviderAttribute : Attribute
    {
      public ValueProviderAttribute([NotNull] string name) => this.Name = name;

      [NotNull]
      public string Name { get; }
    }
}

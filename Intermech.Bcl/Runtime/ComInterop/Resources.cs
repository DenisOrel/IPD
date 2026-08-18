
// Type: Intermech.Runtime.ComInterop.Resources
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
      private static ResourceManager resourceMan;
      private static CultureInfo resourceCulture;

      internal Resources()
      {
      }

      /// <summary>
      ///   Returns the cached ResourceManager instance used by this class.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (Intermech.Runtime.ComInterop.Resources.resourceMan == null)
            Intermech.Runtime.ComInterop.Resources.resourceMan = new ResourceManager("Intermech.Runtime.ComInterop.Resources", typeof (Intermech.Runtime.ComInterop.Resources).Assembly);
          return Intermech.Runtime.ComInterop.Resources.resourceMan;
        }
      }

      /// <summary>
      ///   Overrides the current thread's CurrentUICulture property for all
      ///   resource lookups using this strongly typed resource class.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => Intermech.Runtime.ComInterop.Resources.resourceCulture;
        set => Intermech.Runtime.ComInterop.Resources.resourceCulture = value;
      }

      /// <summary>
      ///   Looks up a localized string similar to Имя файла '{0}' должно быть задано с абсолютным путем..
      /// </summary>
      internal static string Arg_AbsolutePathRequired
      {
        get
        {
          return Intermech.Runtime.ComInterop.Resources.ResourceManager.GetString(nameof (Arg_AbsolutePathRequired), Intermech.Runtime.ComInterop.Resources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не задано имя файла..
      /// </summary>
      internal static string Arg_NullOrEmptyFileName
      {
        get
        {
          return Intermech.Runtime.ComInterop.Resources.ResourceManager.GetString(nameof (Arg_NullOrEmptyFileName), Intermech.Runtime.ComInterop.Resources.resourceCulture);
        }
      }
    }
}

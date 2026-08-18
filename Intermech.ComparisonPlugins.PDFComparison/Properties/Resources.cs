
// Type: Intermech.ComparisonPlugins.PDFComparison.Properties.Resources




using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.ComparisonPlugins.PDFComparison.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
      private static ResourceManager resourceMan;
      private static CultureInfo resourceCulture;

      internal Resources()
      {
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceMan == null)
            Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceMan = new ResourceManager("Intermech.ComparisonPlugins.PDFComparison.Properties.Resources", typeof (Intermech.ComparisonPlugins.PDFComparison.Properties.Resources).Assembly);
          return Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceMan;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceCulture;
        set => Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceCulture = value;
      }

      internal static Bitmap negative
      {
        get
        {
          return (Bitmap) Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.ResourceManager.GetObject(nameof (negative), Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceCulture);
        }
      }

      internal static Bitmap positive
      {
        get
        {
          return (Bitmap) Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.ResourceManager.GetObject(nameof (positive), Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceCulture);
        }
      }

      internal static Bitmap reset
      {
        get => (Bitmap) Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.ResourceManager.GetObject(nameof (reset), Intermech.ComparisonPlugins.PDFComparison.Properties.Resources.resourceCulture);
      }
    }
}

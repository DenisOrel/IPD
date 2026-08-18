
// Type: Properties.Resources
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Properties
{
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

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (Properties.Resources.resourceMan == null)
            Properties.Resources.resourceMan = new ResourceManager("Properties.Resources", typeof (Properties.Resources).Assembly);
          return Properties.Resources.resourceMan;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => Properties.Resources.resourceCulture;
        set => Properties.Resources.resourceCulture = value;
      }

      internal static Bitmap ImagePlaceHolder
      {
        get
        {
          return (Bitmap) Properties.Resources.ResourceManager.GetObject(nameof (ImagePlaceHolder), Properties.Resources.resourceCulture);
        }
      }

      internal static Bitmap ImagePlaceHolder16x16
      {
        get
        {
          return (Bitmap) Properties.Resources.ResourceManager.GetObject(nameof (ImagePlaceHolder16x16), Properties.Resources.resourceCulture);
        }
      }
    }
}

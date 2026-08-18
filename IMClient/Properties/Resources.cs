
// Type: IMClient.Properties.Resources




using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace IMClient.Properties
{
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
          if (IMClient.Properties.Resources.resourceMan == null)
            IMClient.Properties.Resources.resourceMan = new ResourceManager("IMClient.Properties.Resources", typeof (IMClient.Properties.Resources).Assembly);
          return IMClient.Properties.Resources.resourceMan;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => IMClient.Properties.Resources.resourceCulture;
        set => IMClient.Properties.Resources.resourceCulture = value;
      }

      internal static Bitmap auth
      {
        get => (Bitmap) IMClient.Properties.Resources.ResourceManager.GetObject(nameof (auth), IMClient.Properties.Resources.resourceCulture);
      }

      internal static Bitmap CloseButton_20x20
      {
        get
        {
          return (Bitmap) IMClient.Properties.Resources.ResourceManager.GetObject(nameof (CloseButton_20x20), IMClient.Properties.Resources.resourceCulture);
        }
      }

      internal static Bitmap CopyHS
      {
        get => (Bitmap) IMClient.Properties.Resources.ResourceManager.GetObject(nameof (CopyHS), IMClient.Properties.Resources.resourceCulture);
      }

      internal static Bitmap Hard_concretised
      {
        get
        {
          return (Bitmap) IMClient.Properties.Resources.ResourceManager.GetObject("Hard concretised", IMClient.Properties.Resources.resourceCulture);
        }
      }

      internal static Bitmap PasteHS
      {
        get
        {
          return (Bitmap) IMClient.Properties.Resources.ResourceManager.GetObject(nameof (PasteHS), IMClient.Properties.Resources.resourceCulture);
        }
      }
    }
}

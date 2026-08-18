// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.Properties.Resources
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Document.Editor.Properties;

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
      if (Intermech.Document.Editor.Properties.Resources.resourceMan == null)
        Intermech.Document.Editor.Properties.Resources.resourceMan = new ResourceManager("Intermech.Document.Editor.Properties.Resources", typeof (Intermech.Document.Editor.Properties.Resources).Assembly);
      return Intermech.Document.Editor.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Document.Editor.Properties.Resources.resourceCulture;
    set => Intermech.Document.Editor.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap auth
  {
    get => (Bitmap) Intermech.Document.Editor.Properties.Resources.ResourceManager.GetObject(nameof (auth), Intermech.Document.Editor.Properties.Resources.resourceCulture);
  }

  internal static Bitmap CopyHS
  {
    get => (Bitmap) Intermech.Document.Editor.Properties.Resources.ResourceManager.GetObject(nameof (CopyHS), Intermech.Document.Editor.Properties.Resources.resourceCulture);
  }

  internal static Bitmap PasteHS
  {
    get
    {
      return (Bitmap) Intermech.Document.Editor.Properties.Resources.ResourceManager.GetObject(nameof (PasteHS), Intermech.Document.Editor.Properties.Resources.resourceCulture);
    }
  }
}

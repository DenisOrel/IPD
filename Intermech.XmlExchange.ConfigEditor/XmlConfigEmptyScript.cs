// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.XmlConfigEmptyScript
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class XmlConfigEmptyScript
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal XmlConfigEmptyScript()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (XmlConfigEmptyScript.resourceMan == null)
        XmlConfigEmptyScript.resourceMan = new ResourceManager("Intermech.XmlExchange.ConfigEditor.XmlConfigEmptyScript", typeof (XmlConfigEmptyScript).Assembly);
      return XmlConfigEmptyScript.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => XmlConfigEmptyScript.resourceCulture;
    set => XmlConfigEmptyScript.resourceCulture = value;
  }

  internal static string xmlExportEmptyScript
  {
    get
    {
      return XmlConfigEmptyScript.ResourceManager.GetString(nameof (xmlExportEmptyScript), XmlConfigEmptyScript.resourceCulture);
    }
  }

  internal static string xmlImportEmptyScript
  {
    get
    {
      return XmlConfigEmptyScript.ResourceManager.GetString(nameof (xmlImportEmptyScript), XmlConfigEmptyScript.resourceCulture);
    }
  }
}

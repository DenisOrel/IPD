// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Properties.Resources
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Workflow.Client.Properties;

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
      if (Intermech.Workflow.Client.Properties.Resources.resourceMan == null)
        Intermech.Workflow.Client.Properties.Resources.resourceMan = new ResourceManager("Intermech.Workflow.Client.Properties.Resources", typeof (Intermech.Workflow.Client.Properties.Resources).Assembly);
      return Intermech.Workflow.Client.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Workflow.Client.Properties.Resources.resourceCulture;
    set => Intermech.Workflow.Client.Properties.Resources.resourceCulture = value;
  }

  internal static Icon MailBoxIcon
  {
    get
    {
      return (Icon) Intermech.Workflow.Client.Properties.Resources.ResourceManager.GetObject(nameof (MailBoxIcon), Intermech.Workflow.Client.Properties.Resources.resourceCulture);
    }
  }
}

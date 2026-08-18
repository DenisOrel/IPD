// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class CustomDescription : DescriptionAttribute
{
  public static ResourceManager rma = new ResourceManager("Intermech.AutoSelection.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  public CustomDescription(string description)
  {
    object obj = (object) CustomDescription.rma.GetString(description);
    this.DescriptionValue = obj != null ? (string) obj : string.Empty;
  }
}

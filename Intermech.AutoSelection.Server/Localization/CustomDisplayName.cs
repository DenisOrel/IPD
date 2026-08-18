// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDisplayName
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class CustomDisplayName : DisplayNameAttribute
{
  public static ResourceManager rma = new ResourceManager("Intermech.AutoSelection.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  public CustomDisplayName(string displayName)
  {
    object obj = (object) CustomDisplayName.rma.GetString(displayName);
    this.DisplayNameValue = obj != null ? (string) obj : string.Empty;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CustomDisplayName
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CustomDisplayName : DisplayNameAttribute
{
  public static ResourceManager rma = new ResourceManager("Intermech.CompositionTracking.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  public CustomDisplayName(string displayName)
  {
    object obj = (object) CustomDisplayName.rma.GetString(displayName);
    this.DisplayNameValue = obj != null ? (string) obj : string.Empty;
  }
}

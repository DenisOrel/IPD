// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Expert.Server.Resources.ExpertServerResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Expert.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}

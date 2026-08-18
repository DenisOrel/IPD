// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.MRP.Server.Resources.MRPServerResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.MRP.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}

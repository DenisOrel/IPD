// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.Localization
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class Localization
{
  public static ResourceManager rm = new ResourceManager("Intermech.Cadmech.Integrator.Resources.CadmechIntegratorResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Cadmech.Integrator.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}

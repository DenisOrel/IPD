// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomCategory
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  public static ResourceManager rma = new ResourceManager("Intermech.AutoSelection.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  protected override string GetLocalizedString(string value)
  {
    return CustomCategory.rma.GetString(value) == null ? string.Empty : CustomCategory.rma.GetString(value);
  }
}

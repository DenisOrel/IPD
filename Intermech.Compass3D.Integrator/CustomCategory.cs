// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.CustomCategory
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  protected override string GetLocalizedString(string value)
  {
    return Localization.rma.GetString(value) == null ? string.Empty : Localization.rma.GetString(value);
  }
}

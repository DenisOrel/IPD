// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.CustomDescription
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal class CustomDescription : DescriptionAttribute
{
  public CustomDescription(string description)
  {
    object obj = (object) Localization.rma.GetString(description);
    this.DescriptionValue = obj != null ? (string) obj : string.Empty;
  }
}

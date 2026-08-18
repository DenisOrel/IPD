// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.CustomDescription
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.AutoSelection.Client;

internal class CustomDescription : DescriptionAttribute
{
  public static ResourceManager rma = new ResourceManager("Intermech.AutoSelection.Client.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  public CustomDescription(string description)
  {
    object obj = (object) CustomDescription.rma.GetString(description);
    this.DescriptionValue = obj != null ? (string) obj : string.Empty;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.CustomDisplayName
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.AutoSelection.Client;

internal class CustomDisplayName : DisplayNameAttribute
{
  public static ResourceManager rma = new ResourceManager("Intermech.AutoSelection.Client.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  public CustomDisplayName(string displayName)
  {
    object obj = (object) CustomDisplayName.rma.GetString(displayName);
    this.DisplayNameValue = obj != null ? (string) obj : string.Empty;
  }
}

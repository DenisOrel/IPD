// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Document.Client.Resources.DocumentClientResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Document.Client.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Expert.Test, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 494A2DB2-0ED6-480D-BF40-DFD41733278B
// Assembly location: D:\IPS\Client\Intermech.Expert.Test.dll

using Intermech.Expert.Editor;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Expert.Editor.Resources.ExpertEditorResources", Assembly.GetAssembly(typeof (FormEditor)));
  public static ResourceManager rma = new ResourceManager("Intermech.Expert.Editor.Resources.CustomAttributesResources", Assembly.GetAssembly(typeof (FormEditor)));
}

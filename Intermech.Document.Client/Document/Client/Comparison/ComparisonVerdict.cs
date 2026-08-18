// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Comparison.ComparisonVerdict
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client.Comparison;

internal enum ComparisonVerdict
{
  [Description("")] Identical,
  [Description("Есть только в документе 2")] AbsentInDocOne,
  [Description("Есть только в документе 1")] AbsentInDoc2,
  [Description("Есть различия в содержимом/геометрии")] HasDifferentContentOrGeometry,
}

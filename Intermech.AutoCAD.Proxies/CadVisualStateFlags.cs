// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.CadVisualStateFlags
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System;

#nullable disable
namespace Intermech.AutoCAD.Proxies;

[Flags]
public enum CadVisualStateFlags
{
  None = 0,
  ActiveDocument = 1,
  OpenDocuments = 2,
  All = OpenDocuments | ActiveDocument, // 0x00000003
}

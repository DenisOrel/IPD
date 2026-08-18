// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADDocumentType
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

public enum CADDocumentType
{
  Undefined = -1, // 0xFFFFFFFF
  DefinedByTemplate = 0,
  Part = 1,
  Assembly = 2,
  Drawing = 3,
  Skeleton = 4,
  Layout = 5,
  AssemblyInterchange = 6,
  Manufacturing = 7,
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ILinkObjTypes
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

public interface ILinkObjTypes
{
  List<int> LinkTypeIDs { get; }

  List<int> ObjTypeIDs { get; }

  List<int> LinkTypesForObjTypes { get; }

  bool UseConfigOptions { get; }

  HiddenContentsMode hcMode { get; }
}

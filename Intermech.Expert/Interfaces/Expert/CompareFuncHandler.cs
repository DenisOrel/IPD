// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.CompareFuncHandler
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Generic delegate for user compare function</summary>
/// <param name="ti"></param>
/// <param name="objId1">Object ID of the first object</param>
/// <param name="objId2">Object ID of the second objects</param>
/// <param name="dr1">DataRow with params of the first object</param>
/// <param name="dr2">DataRow with params of the second object</param>
/// <returns>-1 если меньше, 0 если равно, 1 если больше</returns>
public delegate int CompareFuncHandler(
  object ti,
  long objId1,
  long objId2,
  HybridRowExp dr1,
  HybridRowExp dr2);

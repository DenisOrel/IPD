// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GraphOptions
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[Flags]
public enum GraphOptions
{
  None = 0,
  LoadLinks = 1,
  LoadBackLinks = 2,
  LoadGraphData = 4,
  LoadConditions = 8,
  LoadAll = 65535, // 0x0000FFFF
  /// <summary>
  /// Не грузить унаследованные действия из родительского шаблона
  /// </summary>
  SkipParent = 65536, // 0x00010000
}

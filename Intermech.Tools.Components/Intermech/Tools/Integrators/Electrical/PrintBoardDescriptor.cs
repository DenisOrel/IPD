// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.PrintBoardDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Печатная плата из проекта ECAD</summary>
[Serializable]
public sealed class PrintBoardDescriptor : IdentifiedEntity
{
  /// <summary>
  /// Признак искуственно созданной сборки для группировки сборок в составе при 2 варианте
  /// </summary>
  public bool IsVirtual;

  /// <summary>Признак платы главной схемы.</summary>
  public bool Root { get; private set; }

  public long AssemblyID { get; set; }

  public Guid Guid { get; set; }

  public PrintBoardDescriptor(string designation, string name, bool root)
    : base(designation, name)
  {
    this.Root = root;
    this.AssemblyID = 0L;
  }
}

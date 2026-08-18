// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.CompositionObjectSortingNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services.Compositions.Sorting;

internal class CompositionObjectSortingNode([NotNull] CompositionSortingProjInfo source) : 
  CompositionSortingProjInfo(source),
  IComparable<CompositionObjectSortingNode>,
  IEquatable<CompositionObjectSortingNode>
{
  public override int GetHashCode() => this.ProjObjID.GetHashCode();

  public override int CompareTo(object obj) => this.CompareTo(obj as CompositionObjectSortingNode);

  public int CompareTo(CompositionObjectSortingNode other)
  {
    if (other == null)
      return 1;
    int num1 = this.ProjObjID.CompareTo(other.ProjObjID);
    if (num1 != 0)
      return num1;
    int num2 = this.RelTypeID;
    int num3 = num2.CompareTo(other.RelTypeID);
    if (num3 != 0)
      return num3;
    num2 = this.PartObjType;
    return num2.CompareTo(other.PartObjType);
  }

  public bool Equals(CompositionObjectSortingNode other) => this.CompareTo(other) == 0;
}

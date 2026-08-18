// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImClassiff
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImClassiff : IComparable, IEquatable<ImClassiff>
{
  public ImClassiff(long objId)
    : this(objId, string.Empty)
  {
  }

  public ImClassiff(long objId, string value)
  {
    this.ObjectId = objId;
    this.Value = value;
  }

  public ImClassiffDiffMode CompareToClassiff(object obj)
  {
    ImClassiff imClassiff = (ImClassiff) null;
    if (obj != null)
    {
      if (obj is string str)
        imClassiff = new ImClassiff(0L, str);
      else if (obj.GetType() == typeof (ImClassiff))
        imClassiff = (ImClassiff) obj;
    }
    if (imClassiff == null)
      return ImClassiffDiffMode.icdmDifferent;
    if (string.Compare(this.Value, imClassiff.Value, StringComparison.Ordinal) == 0)
      return ImClassiffDiffMode.icdmEqual;
    if (this.Value.StartsWith(imClassiff.Value))
      return ImClassiffDiffMode.icdmEnterIn;
    return imClassiff.Value.StartsWith(this.Value) ? ImClassiffDiffMode.icdmContains : ImClassiffDiffMode.icdmDifferent;
  }

  public long ObjectId { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

  public string Value { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

  public int CompareTo(object obj)
  {
    ImClassiff imClassiff = (ImClassiff) null;
    if (obj != null)
    {
      if (obj is string str)
        imClassiff = new ImClassiff(0L, str);
      else if (obj.GetType() == typeof (ImClassiff))
        imClassiff = (ImClassiff) obj;
    }
    return imClassiff != null ? string.Compare(this.Value, imClassiff.Value, StringComparison.Ordinal) : -1;
  }

  public bool Equals(ImClassiff other) => this.CompareTo((object) other) == 0;

  public override string ToString() => this.Value;

  public override int GetHashCode() => this.Value.GetHashCode();
}

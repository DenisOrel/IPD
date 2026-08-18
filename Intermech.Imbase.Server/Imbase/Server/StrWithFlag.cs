// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.StrWithFlag
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;

#nullable disable
namespace Intermech.Imbase.Server;

internal class StrWithFlag : IComparable
{
  private readonly string _str = string.Empty;
  private readonly bool _flag;

  internal string Str => this._str;

  internal bool Flag => this._flag;

  internal StrWithFlag(string str, bool flag)
  {
    this._str = str;
    this._flag = flag;
  }

  int IComparable.CompareTo(object obj)
  {
    return !(obj is StrWithFlag strWithFlag) ? -1 : string.CompareOrdinal(this._str, strWithFlag.Str);
  }
}

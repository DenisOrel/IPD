// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ILinkedToOne`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

public interface ILinkedToOne<TOther> where TOther : class
{
  [CanBeNull]
  TOther LinkedTo { get; }
}

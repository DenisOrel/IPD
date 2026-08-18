// Decompiled with JetBrains decompiler
// Type: Intermech.Common.CanBeReadOnlyCommon
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Common;

public static class CanBeReadOnlyCommon
{
  public delegate bool IsReadOnlyCanBeChangedEventHandler([NotNull] object sender, bool newReadOnlyValue);
}

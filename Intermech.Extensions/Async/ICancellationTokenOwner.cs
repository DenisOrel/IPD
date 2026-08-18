// Decompiled with JetBrains decompiler
// Type: Intermech.Async.ICancellationTokenOwner
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

#nullable disable
namespace Intermech.Async;

public interface ICancellationTokenOwner
{
  System.Threading.CancellationToken? CancellationToken { get; }
}

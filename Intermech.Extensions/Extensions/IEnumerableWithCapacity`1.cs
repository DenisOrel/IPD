// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IEnumerableWithCapacity`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Extensions;

public interface IEnumerableWithCapacity<T> : IEnumerable<T>, IEnumerable, ICapacity
{
}

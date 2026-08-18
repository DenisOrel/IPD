// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.IInitWithSession
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Metadata;

internal interface IInitWithSession
{
  void Init([NotNull] IUserSession session);
}

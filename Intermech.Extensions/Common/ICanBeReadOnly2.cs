// Decompiled with JetBrains decompiler
// Type: Intermech.Common.ICanBeReadOnly2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using System;

#nullable disable
namespace Intermech.Common;

public interface ICanBeReadOnly2 : ICanBeReadOnly
{
  bool ForceIsReadOnly { get; set; }

  event CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler ReadOnlyCanBeChangedEvent;

  bool UpdateReadOnly();

  event Action<object> ReadOnlyWasChanged;
}

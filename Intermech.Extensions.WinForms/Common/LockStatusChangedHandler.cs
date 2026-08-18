// Decompiled with JetBrains decompiler
// Type: Intermech.Common.LockStatusChangedHandler
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Common;

public delegate void LockStatusChangedHandler(
  [NotNull] LocksManager sender,
  [CanBeNull] object locksCounterOwner,
  bool isLocked);

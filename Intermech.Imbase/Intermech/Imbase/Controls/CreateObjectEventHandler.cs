// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.CreateObjectEventHandler
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Controls;

public delegate void CreateObjectEventHandler(
  long linkId,
  long recordId,
  IServiceProvider services);

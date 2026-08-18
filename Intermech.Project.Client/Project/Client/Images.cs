// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.Images
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Project.Client;

internal abstract class Images : Intermech.Project.Controls.Images
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  internal new static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Images._initOnce.Invoke((Action) (() => Intermech.Project.Controls.Images.Init(serviceProvider, session)));
  }
}

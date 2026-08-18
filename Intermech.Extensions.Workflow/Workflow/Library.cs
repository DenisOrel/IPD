// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Library
// Assembly: Intermech.Extensions.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3168E407-FCE5-437D-846E-527B99048CAF
// Assembly location: D:\IPS\Client\Intermech.Extensions.Workflow.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow;

public abstract class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke(ref session, (Action) (() =>
    {
      Intermech.Diagnostics.Check.NotNull<IUserSession>(session, nameof (session));
      Intermech.Extensions.Interfaces.Library.Init(serviceProvider, session);
      MetadataLoader.Init(session);
    }));
  }
}

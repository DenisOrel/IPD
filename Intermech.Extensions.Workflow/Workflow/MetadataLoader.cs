// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.MetadataLoader
// Assembly: Intermech.Extensions.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3168E407-FCE5-437D-846E-527B99048CAF
// Assembly location: D:\IPS\Client\Intermech.Extensions.Workflow.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow;

public class MetadataLoader : Intermech.Metadata.MetadataLoader
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  protected internal new static void Init([NotNull] IUserSession session)
  {
    MetadataLoader._initOnce.Invoke((Action) (() =>
    {
      Intermech.Metadata.MetadataLoader.Init(session);
      Intermech.Metadata.MetadataLoader.InitMetadata<MetadataLoader>(session);
    }));
  }
}

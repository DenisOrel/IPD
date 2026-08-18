// Decompiled with JetBrains decompiler
// Type: Intermech.Project.MetadataLoader
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Project;

/// <summary>Загрузчик метаданных IPS.Project</summary>
public abstract class MetadataLoader : Intermech.Metadata.MetadataLoader
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  protected internal new static void Init([NotNull] IUserSession session)
  {
    MetadataLoader._initOnce.Invoke((Action) (() =>
    {
      Intermech.Metadata.MetadataLoader.Init(session);
      Intermech.Metadata.MetadataLoader.Init(session);
      Intermech.Metadata.MetadataLoader.InitMetadata<MetadataLoader>(session);
      Attributes.InitProtectedIDs(session);
    }));
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Interfaces.ConfigViewSettings
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;

internal class ConfigViewSettings : IConfigViewSettings
{
  [CanBeNull]
  public IDocumentConfigElement ConfigElement { get; set; }

  [CanBeNull]
  public DocumentConfigElementType ConfigElementType { get; set; }

  public bool ReadOnly { get; set; }

  [CanBeNull]
  public Action<IConfigViewController, bool> OnDataChanged { get; set; }

  public IServiceProvider Services { get; }

  public ConfigViewSettings(IServiceProvider services) => this.Services = services;
}

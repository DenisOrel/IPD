// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Interfaces.IConfigViewSettings
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;

public interface IConfigViewSettings
{
  [CanBeNull]
  IDocumentConfigElement ConfigElement { get; set; }

  DocumentConfigElementType ConfigElementType { get; set; }

  bool ReadOnly { get; set; }

  [CanBeNull]
  Action<IConfigViewController, bool> OnDataChanged { get; set; }

  IServiceProvider Services { get; }
}

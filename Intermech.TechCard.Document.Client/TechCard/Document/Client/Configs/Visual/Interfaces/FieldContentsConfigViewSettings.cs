// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Interfaces.FieldContentsConfigViewSettings
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Expert;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;

internal class FieldContentsConfigViewSettings(IServiceProvider services) : ConfigViewSettings(services)
{
  public string Caption { get; set; }

  public DataType DataType { get; set; } = DataType.Boolean;

  public FieldContentsType DefaultFieldContentsType { get; set; }
}

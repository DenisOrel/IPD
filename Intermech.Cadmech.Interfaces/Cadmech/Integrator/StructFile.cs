// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructFile
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// 
/// </summary>
public class StructFile
{
  private List<PartData> parts;
  private List<RowData> rows;

  /// <summary>
  /// 
  /// </summary>
  public StructFile()
  {
    this.parts = new List<PartData>();
    this.rows = new List<RowData>();
  }

  /// <summary>
  /// 
  /// </summary>
  public List<PartData> Parts => this.parts;

  /// <summary>
  /// 
  /// </summary>
  public List<RowData> Rows => this.rows;
}

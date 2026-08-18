// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructData
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Описывает изделие, сформированное по сборочному чертежу CADMECH 2D.
/// </summary>
public class StructData
{
  private string dwgPath;
  private long baseProjectId;
  private List<long> projectIds;
  private StructFile structFile;
  private SpecDummy spec;

  /// <summary>Создает объект.</summary>
  public StructData()
  {
    this.dwgPath = string.Empty;
    this.baseProjectId = 0L;
    this.projectIds = new List<long>(32 /*0x20*/);
  }

  /// <summary>
  /// Возвращает или задает полный путь к сборочному чертежу.
  /// </summary>
  public string DwgPath
  {
    get => this.dwgPath;
    set => this.dwgPath = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор версии основного исполнения сборочной единицы, выпускаемой
  /// по сборочному чертежу.
  /// </summary>
  public long BaseProjectId
  {
    get => this.baseProjectId;
    set => this.baseProjectId = value;
  }

  /// <summary>
  /// Возвращает список идентификаторов исполнений сборочной единицы, выпускаемой по
  /// сборочному чертежу.
  /// </summary>
  public List<long> ProjectIds => this.projectIds;

  /// <summary>
  /// 
  /// </summary>
  public StructFile StructFile
  {
    get => this.structFile;
    set => this.structFile = value;
  }

  /// <summary>Возвращает или задает макет спецификации.</summary>
  public SpecDummy Spec
  {
    get => this.spec;
    set => this.spec = value;
  }
}

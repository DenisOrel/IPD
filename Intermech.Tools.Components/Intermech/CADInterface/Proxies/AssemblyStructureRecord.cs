// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.AssemblyStructureRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class AssemblyStructureRecord
{
  private string projConfiguration;
  private Guid occurenceGuid;
  private ModelConfigurationProxy compConfiguration;
  private string compMasterFile;
  private ValueBag attributes;

  internal AssemblyStructureRecord() => this.attributes = new ValueBag();

  /// <summary>
  /// Возвращает имя конфигурации изделия, в которое входит компонент. Если значение равно null, то компонент входит
  /// во все конфигурации изделия.
  /// </summary>
  public string ProjectConfiguration
  {
    get => this.projConfiguration;
    internal set => this.projConfiguration = value;
  }

  public Guid OccurenceGuid
  {
    get => this.occurenceGuid;
    internal set => this.occurenceGuid = value;
  }

  /// <summary>Возвращает имя конфигурации компонента.</summary>
  public ModelConfigurationProxy ComponentConfiguration
  {
    get => this.compConfiguration;
    internal set => this.compConfiguration = value;
  }

  /// <summary>Возвращает путь к мастер-модели компонента.</summary>
  public string ComponentMasterFile
  {
    get => this.compMasterFile;
    internal set => this.compMasterFile = value;
  }

  public ValueBag Attributes => this.attributes;
}

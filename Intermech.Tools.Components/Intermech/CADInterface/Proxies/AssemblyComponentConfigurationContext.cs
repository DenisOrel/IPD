// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.AssemblyComponentConfigurationContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Описывает контекст получения конфигурации документа CAD-системы при работе со структурой сборочной модели.
/// </summary>
public sealed class AssemblyComponentConfigurationContext : IModelConfigurationCreationContext
{
  private CADDocumentProxy assemblyDocument;

  /// <summary>Создает объект.</summary>
  /// <param name="assemblyDocument">Сборочный документ, служащий источником информации о структуре изделия. Это может быть сборочная модель или сборочный чертеж</param>
  public AssemblyComponentConfigurationContext(CADDocumentProxy assemblyDocument)
  {
    this.assemblyDocument = assemblyDocument != null ? assemblyDocument : throw new ArgumentNullException(nameof (assemblyDocument));
  }

  /// <summary>
  /// Возвращает сборочный документ, служащий источником информации о структуре изделия. Это может быть сборочная модель или сборочный чертеж.
  /// </summary>
  public CADDocumentProxy AssemblyDocument => this.assemblyDocument;
}

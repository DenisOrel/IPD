// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.OccurenceFormat
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Описывает способы записи информации об исполнениях сборочной единицы в обменный файл (поля RELEASE и NUMBER).
/// </summary>
public enum OccurenceFormat
{
  /// <summary>
  /// Компонент входит во все исполнения сборочной единицы. Поле RELEASE пусто, в поле NUMBER содержится количество компонентов
  /// </summary>
  AllProjects,
  /// <summary>
  /// Одно исполнение, чей идентификатор указывается в поле RELEASE, в поле NUMBER содержится количество компонентов
  /// </summary>
  OneProject,
  /// <summary>
  /// Несколько исполнений, перечисленных с количествами через запятую в поле RELEASE, поле NUMBER игнорируется
  /// </summary>
  VariousProjects,
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.UnresolvedFilesSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Секция для хранения информации о файловых зависимостях документа, которые не удалось найти на диске в процессе анализа документа.
/// Имена всех таких файлов будут помещены в атрибут документа 'Требует уточнения ссылок на файлы'.
/// </summary>
internal sealed class UnresolvedFilesSection
{
  private readonly PathCollection files;

  public UnresolvedFilesSection() => this.files = new PathCollection(8);

  public PathCollection Files => this.files;
}

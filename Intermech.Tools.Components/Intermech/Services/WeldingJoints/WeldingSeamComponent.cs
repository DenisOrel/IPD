// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamComponent
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Ссылка и параметры компонента сварочного шва, полученные из внешней системы.
/// </summary>
internal sealed class WeldingSeamComponent
{
  private string filePath;
  private string compareKey;
  private string articleExternalKey;
  private int groupId;

  /// <summary>Создает объект.</summary>
  /// <param name="compareKey">Ключ для сравнения компонентов между собой</param>
  /// <param name="filePath">Путь к файлу модели компонента</param>
  /// <param name="articleExternalKey">Внешний ключ изделия для компонента модели</param>
  /// <param name="groupId">Номер группы компонентов, в которую входит модель (1 или 2)</param>
  public WeldingSeamComponent(
    string compareKey,
    string filePath,
    string articleExternalKey,
    int groupId)
  {
    if (compareKey == null)
      throw new ArgumentNullException(nameof (compareKey));
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (articleExternalKey == null)
      throw new ArgumentNullException(nameof (articleExternalKey));
    if (groupId != 1 && groupId != 2)
      throw new ArgumentOutOfRangeException(nameof (groupId));
    this.compareKey = compareKey;
    this.filePath = filePath;
    this.articleExternalKey = articleExternalKey;
    this.groupId = groupId;
  }

  /// <summary>
  /// Возвращает ключ для сравнения компонентов между собой.
  /// </summary>
  public string CompareKey => this.compareKey;

  /// <summary>Возвращает путь к файлу модели компонента.</summary>
  public string FilePath => this.filePath;

  /// <summary>
  /// Возвращает внешний ключ изделия для компонента модели.
  /// Значение свойства может быть пусто.
  /// </summary>
  public string ArticleExternalKey => this.articleExternalKey;

  /// <summary>
  /// Возвращает номер группы компонентов, в которую входит модель (1 или 2).
  /// </summary>
  public int GroupId => this.groupId;
}

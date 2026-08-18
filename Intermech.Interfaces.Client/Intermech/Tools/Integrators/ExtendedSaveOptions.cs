// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ExtendedSaveOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Settings;
using Intermech.UI;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Содержит опции выполнения расширенного сохранения документа.
/// </summary>
public sealed class ExtendedSaveOptions
{
  private SaveChangesMode mode;
  private bool createNewArticlesOnly;
  private bool updateExistingArticlesOnly;
  private bool recalculateMass;
  private IReplaceFilePolicy workAreaPolicy;

  /// <summary>Создает объект.</summary>
  /// <param name="context">Режим выполнения сохранения изменений</param>
  public ExtendedSaveOptions(SaveChangesMode mode)
  {
    this.mode = mode;
    if (mode != SaveChangesMode.Default && mode == SaveChangesMode.Checkin)
    {
      this.createNewArticlesOnly = (bool) (ValueCell<bool>) ExtendedSaveOnCheckinSettings.Instance.CreateArticles;
      this.updateExistingArticlesOnly = (bool) (ValueCell<bool>) ExtendedSaveOnCheckinSettings.Instance.UpdateArticles;
      this.recalculateMass = (bool) (ValueCell<bool>) ExtendedSaveOnCheckinSettings.Instance.RecalculateMass;
    }
    else
    {
      this.createNewArticlesOnly = true;
      this.updateExistingArticlesOnly = true;
      this.recalculateMass = false;
    }
  }

  /// <summary>Возвращает режим выполнения сохранения изменений.</summary>
  public SaveChangesMode Mode => this.mode;

  /// <summary>
  /// Включает и выключает режим, при котором создание изделий выполняется только в том случае, если у документа вообще нет изделий в базе IPS.
  /// </summary>
  public bool CreateNewArticlesOnly
  {
    get => this.createNewArticlesOnly;
    set => this.createNewArticlesOnly = value;
  }

  /// <summary>
  /// Включает и выключает режим, при котором обновление изделий выполняется только в том случае, если у документа они уже есть в базе IPS.
  /// </summary>
  public bool UpdateExistingArticlesOnly
  {
    get => this.updateExistingArticlesOnly;
    set => this.updateExistingArticlesOnly = value;
  }

  /// <summary>
  /// Включает и выключает режим пересчета массы изделий на основе физических свойств документа.
  /// </summary>
  public bool RecalculateMass
  {
    get => this.recalculateMass;
    set => this.recalculateMass = value;
  }

  /// <summary>
  /// Возвращает или задает политику замены файлов в рабочей области пользователя.
  /// </summary>
  public IReplaceFilePolicy WorkAreaPolicy
  {
    get => this.workAreaPolicy;
    set => this.workAreaPolicy = value;
  }

  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IPercentageProgressSink ProgressSink { get; set; }
}

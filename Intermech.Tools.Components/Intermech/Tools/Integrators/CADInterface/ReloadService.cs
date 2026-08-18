// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ReloadService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис интегратора для закрытия и последующего переоткрытия документов, открытых в CAD-системе.
/// Реализация является thread safe.
/// </summary>
internal sealed class ReloadService(IIntegrator owner) : IntegratorService(owner)
{
  private IReloadDriver reloadDriver;

  /// <summary>
  /// Возвращает или задает стратегию закрытия и переоткрытия документов.
  /// Значение свойства должно быть задано.
  /// </summary>
  public IReloadDriver ReloadDriver
  {
    get => this.reloadDriver;
    set => this.reloadDriver = value;
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.ReloadDriver == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_258"));
  }

  /// <summary>Выгружает все файлы, открытые в CAD-системе.</summary>
  /// <returns>Объект состояния, позволяющие переоткрыть закрытые файлы. Может быть null, если переоткрывать нечего</returns>
  public object UnloadAll()
  {
    this.RequireReadyState();
    return this.UnloadCore(new Predicate<IReloadItem>(this.UnloadAllPredicate));
  }

  private bool UnloadAllPredicate(IReloadItem item) => true;

  /// <summary>
  /// Выгружает указанные файлы, если они открыты в CAD-системе.
  /// </summary>
  /// <param name="doomedFiles">Коллекция абсолютных путей к файлам, которые должны быть выгружены</param>
  /// <returns>Объект состояния, позволяющие переоткрыть закрытые файлы. Может быть null, если переоткрывать нечего</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="doomedFiles" /> содержит null</exception>
  public object Unload(ICollection<string> doomedFiles)
  {
    if (doomedFiles == null)
      throw new ArgumentNullException(nameof (doomedFiles), LocalizationHolder.rm.GetString("Tools.Components_257"));
    this.RequireReadyState();
    Predicate<IReloadItem> unloadPredicate = (Predicate<IReloadItem>) (item =>
    {
      foreach (string doomedFile in (IEnumerable<string>) doomedFiles)
      {
        if (item.ContainsFile(doomedFile))
          return true;
      }
      return false;
    });
    return doomedFiles.Count <= 0 ? (object) null : this.UnloadCore(unloadPredicate);
  }

  private object UnloadCore(Predicate<IReloadItem> unloadPredicate)
  {
    List<IReloadItem> all = this.reloadDriver.GetReloadItems().FindAll(unloadPredicate);
    if (all.Count <= 0)
      return (object) null;
    for (int index = 0; index < all.Count; ++index)
      all[index].PrepareForClose();
    object obj = this.reloadDriver.SaveAppState();
    for (int index = 0; index < all.Count; ++index)
      all[index].Close(false);
    return obj;
  }

  /// <summary>Загружает обратно в CAD-систему закрытые ранее файлы.</summary>
  /// <param name="reloadState">Объект состояния, позволяющие переоткрыть закрытые файлы</param>
  public void Reload(object reloadState)
  {
    this.RequireReadyState();
    if (reloadState == null)
      return;
    this.reloadDriver.RestoreAppState(reloadState);
  }
}

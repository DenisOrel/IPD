
// Type: Intermech.Tools.Integrators.CompositeApiResourceManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Базовый класс для реализации составных менеджеров ресурсов приложения.
/// </summary>
public abstract class CompositeApiResourceManager : ApplicationApiResourceManager
{
  private ICollection<ApplicationApiResourceManager> subManagers;

  /// <summary>
  /// Активирует сохранение информации о ресурсах приложения (COM-объекты и др.), открытых интегратором.
  /// </summary>
  protected override void DoStart()
  {
    base.DoStart();
    if (this.subManagers == null)
    {
      this.subManagers = this.GetSubManagers();
      if (this.subManagers == null)
        throw new InvalidOperationException("Method CompositeApiResourceManager.GetSubManagers must not return null.");
    }
    List<ApplicationApiResourceManager> managerCollection = new List<ApplicationApiResourceManager>(this.subManagers.Count);
    try
    {
      foreach (ApplicationApiResourceManager subManager in (IEnumerable<ApplicationApiResourceManager>) this.subManagers)
      {
        subManager.Start();
        managerCollection.Add(subManager);
      }
    }
    catch
    {
      managerCollection.Reverse();
      this.SafelyReleaseResourcesAndStop((IEnumerable<ApplicationApiResourceManager>) managerCollection, false);
      throw;
    }
  }

  /// <summary>
  /// Освобождает ресурсы приложения, открытые интегратором, а также деактивирует сохранение информации об открытых ресурсах приложения.
  /// Метод не должен сбрасывать исключения. Все ошибки освобождения ресурсов приложения должны сохраняться в коллекции Errors.
  /// </summary>
  protected override void DoReleaseResourcesAndStop()
  {
    this.SafelyReleaseResourcesAndStop((IEnumerable<ApplicationApiResourceManager>) this.subManagers, true);
    base.DoReleaseResourcesAndStop();
  }

  private void SafelyReleaseResourcesAndStop(
    IEnumerable<ApplicationApiResourceManager> managerCollection,
    bool saveErrors)
  {
    foreach (ApplicationApiResourceManager manager in managerCollection)
    {
      manager.ReleaseResourcesAndStop();
      if (saveErrors && manager.Errors.Count != 0)
      {
        this.Errors.AddRange((IEnumerable<ErrorInfo>) manager.Errors);
        manager.Errors.Clear();
      }
    }
  }

  /// <summary>
  /// Возвращает коллекцию подчиненных менеджеров ресурсов приложения.
  /// </summary>
  /// <returns>Коллекция подчиненных менеджеров ресурсов приложения</returns>
  protected abstract ICollection<ApplicationApiResourceManager> GetSubManagers();
}

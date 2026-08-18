
// Type: Intermech.Tools.Integrators.IntegratorServiceCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.Tools.Integrators;

public class IntegratorServiceCollection : Collection<IIntegratorService>
{
  private bool isChangesLocked;

  public IntegratorServiceCollection()
    : base((IList<IIntegratorService>) new List<IIntegratorService>(32 /*0x20*/))
  {
  }

  public void LockChanges()
  {
    if (this.isChangesLocked)
      return;
    this.isChangesLocked = true;
  }

  private void RequireChangesUnlocked()
  {
    if (this.isChangesLocked)
      throw new InvalidOperationException("Unable to modify the integrator service collection beacuse it is already initialized and locked.");
  }

  protected override void ClearItems()
  {
    this.RequireChangesUnlocked();
    base.ClearItems();
  }

  protected override void InsertItem(int index, IIntegratorService item)
  {
    this.RequireChangesUnlocked();
    base.InsertItem(index, item);
  }

  protected override void RemoveItem(int index)
  {
    this.RequireChangesUnlocked();
    base.RemoveItem(index);
  }

  protected override void SetItem(int index, IIntegratorService item)
  {
    this.RequireChangesUnlocked();
    base.SetItem(index, item);
  }

  /// <summary>Возвращает сервис интегратора указанного типа.</summary>
  /// <param name="serviceType">Тип сервиса</param>
  /// <returns>Найденный сервис интегратора или null, если сервис не поддерживается интегратором</returns>
  /// <exception cref="T:System.ArgumentNullException">serviceType</exception>
  public object TryGetService(Type serviceType)
  {
    return !(serviceType == (Type) null) ? (object) CollectionUtils.Find<IIntegratorService>((IEnumerable<IIntegratorService>) this, (Predicate<IIntegratorService>) (item => serviceType.IsAssignableFrom(item.GetType()))) : throw new ArgumentNullException(nameof (serviceType));
  }
}


// Type: Intermech.Tools.Integrators.IntegratorRegistry
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Tools.Integrators;

/// <summary>
/// <para>
/// Реализует клиентскую службу, обслуживающую интеграторы с приложениями.  Предоставляет
/// сервисы регистрации клиентских частей интеграторов и доступа к ним.
/// </para>
/// <para>
/// Сервис является thread-safe.
/// </para>
/// </summary>
internal sealed class IntegratorRegistry : IIntegratorRegistry
{
  private readonly List<IIntegrator> integrators;
  private readonly ReaderWriterLock guard;

  /// <summary>Создает объект.</summary>
  public IntegratorRegistry()
  {
    this.integrators = new List<IIntegrator>(32 /*0x20*/);
    this.guard = new ReaderWriterLock();
  }

  /// <summary>Регистрирует клиентскую часть интегратора.</summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Объект интегратора уже зарегистрирован</exception>
  public void RegisterIntegrator(IIntegrator integrator)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator), LocalizationHolder.rm.GetString("Client.Core_1312"));
    this.guard.AcquireWriterLock(-1);
    try
    {
      if (this.IsAlreadyRegistered(integrator))
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_1313"));
      this.integrators.Add(integrator);
    }
    finally
    {
      this.guard.ReleaseWriterLock();
    }
  }

  /// <summary>
  /// Отменяет регистрацию клиентской части интегратора. Метод допускает, что объект интегратора может быть
  /// не зарегистрирован.
  /// </summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
  public void UnregisterIntgerator(IIntegrator integrator)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator), LocalizationHolder.rm.GetString("Client.Core_1312"));
    this.guard.AcquireWriterLock(-1);
    try
    {
      this.integrators.Remove(integrator);
    }
    finally
    {
      this.guard.ReleaseWriterLock();
    }
  }

  /// <summary>
  /// Возвращает список загруженных и зарегистрированных клиентских частей интеграторов. Содержимое этого списка может
  /// не соответствовать списку объектов интеграторов в базе IPS.
  /// </summary>
  /// <returns>Список клиентских частей интеграторов</returns>
  public List<IIntegrator> GetIntegrators()
  {
    this.guard.AcquireReaderLock(-1);
    try
    {
      return this.integrators.GetRange(0, this.integrators.Count);
    }
    finally
    {
      this.guard.ReleaseReaderLock();
    }
  }

  /// <summary>
  /// Возвращает клиентскую часть интегратора с указанным приложением.
  /// </summary>
  /// <param name="iobj">Объект, идентифицирующий интегратор с приложением</param>
  /// <param name="throwIfNotFound">Признак генерации исключения в случае, если клиентская часть интегратора не загружена</param>
  /// <returns>Объект интегратора или null, если клиентская часть интегратора не загружена</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на идентификатор интегратора не может быть null</exception>
  /// <exception cref="T:Intermech.FaultException">Клиентская часть интегратора не загружена</exception>
  public IIntegrator GetIntegrator(IntegratorObject iobj, bool throwIfNotFound)
  {
    if (iobj == null)
      throw new ArgumentNullException(nameof (iobj), LocalizationHolder.rm.GetString("Client.Core_1314"));
    this.guard.AcquireReaderLock(-1);
    try
    {
      IIntegrator byId = this.FindById(iobj.Id);
      return !(byId == null & throwIfNotFound) ? byId : throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1596"), (object) iobj.DisplayName));
    }
    finally
    {
      this.guard.ReleaseReaderLock();
    }
  }

  private IIntegrator FindById(Guid id)
  {
    return this.integrators.Find((Predicate<IIntegrator>) (integrator => integrator.Id == id));
  }

  private bool IsAlreadyRegistered(IIntegrator integrator)
  {
    return this.integrators.Contains(integrator) || this.FindById(integrator.Id) != null;
  }
}

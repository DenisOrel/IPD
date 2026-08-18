// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IIntegratorRegistry
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Клиентская служба, обслуживающая интеграторы с CAD-системами и другими приложениями. Предоставляет
/// сервисы регистрации клиентских частей интеграторов и доступа к ним.
/// </summary>
public interface IIntegratorRegistry
{
  /// <summary>Регистрирует клиентскую часть интегратора.</summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Объект интегратора уже зарегистрирован</exception>
  void RegisterIntegrator(IIntegrator integrator);

  /// <summary>
  /// Отменяет регистрацию клиентской части интегратора. Метод допускает, что объект интегратора может быть
  /// не зарегистрирован.
  /// </summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
  void UnregisterIntgerator(IIntegrator integrator);

  /// <summary>
  /// Возвращает список загруженных и зарегистрированных клиентских частей интеграторов. Содержимое этого списка может
  /// не соответствовать списку объектов интеграторов в базе IPS.
  /// </summary>
  /// <returns>Список клиентских частей интеграторов</returns>
  List<IIntegrator> GetIntegrators();

  /// <summary>
  /// Возвращает клиентскую часть интегратора с указанным приложением.
  /// </summary>
  /// <param name="iobj">Объект, идентифицирующий интегратор с приложением</param>
  /// <param name="throwIfNotFound">Признак генерации исключения в случае, если клиентская часть интегратора не загружена</param>
  /// <returns>Объект интегратора или null, если клиентская часть интегратора не загружена</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на идентификатор интегратора не может быть null</exception>
  /// <exception cref="T:Intermech.FaultException">Клиентская часть интегратора не загружена</exception>
  IIntegrator GetIntegrator(IntegratorObject iobj, bool throwIfNotFound);
}

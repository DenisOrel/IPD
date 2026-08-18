// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.EmbedAttributesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, отвечающего за передачу изменений из карточки объекта в его файлы.
/// </summary>
public abstract class EmbedAttributesService : IntegratorService, IEmbedAttributesService
{
  private readonly EmbedAttributesManager manager;
  private EmbedAttributesActionOptions emptyEmbedAttributesOptions;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  protected EmbedAttributesService(IIntegrator owner)
    : base(owner)
  {
    this.manager = new EmbedAttributesManager();
    this.emptyEmbedAttributesOptions = new EmbedAttributesActionOptions();
  }

  /// <summary>
  /// Записывает в файловую копию объекта указанные значения атрибутов объекта.
  /// Как правило, этот метод вызывается из карточки документа для сохранения изменных атрибутов в файле документа.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="attributeValues">Коллекция значений атрибутов</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="attributeValues" /> не должен быть равен null</exception>
  public void EmbedAttributeValues(long objectId, IList<AttributeValues> attributeValues)
  {
    this.EmbedAttributeValues(objectId, attributeValues, this.emptyEmbedAttributesOptions);
  }

  /// <summary>
  /// Записывает в файловую копию объекта указанные значения атрибутов объекта.
  /// Как правило, этот метод вызывается из карточки документа для сохранения изменных атрибутов в файле документа.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="attributeValues">Коллекция значений атрибутов</param>
  /// <param name="options">Опции выполнения операции</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="attributeValues" /> не должен быть равен null. Параметр <paramref name="options" /> не должен быть равен null.</exception>
  public void EmbedAttributeValues(
    long objectId,
    IList<AttributeValues> attributeValues,
    EmbedAttributesActionOptions options)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта", nameof (objectId));
    if (attributeValues == null)
      throw new ArgumentNullException(nameof (attributeValues));
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      this.CheckDriver();
      if (this.manager.Driver == null)
        this.manager.Driver = this.Driver;
      this.LicenseService.Check();
      this.OnBeforeEmbedAttributes(objectId, attributeValues);
      try
      {
        this.ConfigureDriverParameters();
        this.manager.EmbedAttributes(new EmbedAttributesActionParameters()
        {
          ObjectId = objectId,
          AttributeValues = attributeValues,
          ProgressSink = options.ProgressSink
        });
      }
      finally
      {
        this.ResetDriverParameters();
      }
      this.OnAfterEmbedAttributes(objectId, attributeValues);
    }
  }

  private void CheckDriver()
  {
    if (this.Driver == null)
      throw new InvalidOperationException("Property 'Driver' must not be null.");
  }

  /// <summary>
  /// Возвращает драйвер для работы с атрибутами документов.
  /// </summary>
  protected abstract IEmbedAttributesDriver Driver { get; }

  /// <summary>
  /// Устанавливает свойства драйвера, управляющие его поведением. Метод вызывается перед каждым использованием драйвера.
  /// </summary>
  protected virtual void ConfigureDriverParameters()
  {
  }

  /// <summary>
  /// Очищает свойства драйвера, управляющие его поведением. Метод вызывается после каждого использования драйвера.
  /// </summary>
  protected virtual void ResetDriverParameters()
  {
  }

  /// <summary>
  /// Вызывается в самом начале процесса записи атрибутов в файл объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии документа</param>
  /// <param name="attributeValues">Коллекция значений атрибутов</param>
  protected virtual void OnBeforeEmbedAttributes(
    long objectId,
    IList<AttributeValues> attributeValues)
  {
  }

  /// <summary>
  /// Вызывается в самом конце процесса после успешной записи атрибутов в файл объекта.
  /// Этот метод не будет вызван, если при записи атрибутов в файл объекта будет сброшено исключение.
  /// </summary>
  /// <param name="objectId">Идентификатор версии документа</param>
  /// <param name="attributeValues">Коллекция значений атрибутов</param>
  protected virtual void OnAfterEmbedAttributes(
    long objectId,
    IList<AttributeValues> attributeValues)
  {
  }
}

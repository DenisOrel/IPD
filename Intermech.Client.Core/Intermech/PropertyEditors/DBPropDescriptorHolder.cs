
// Type: Intermech.PropertyEditors.DBPropDescriptorHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// Базовый тип для Holder'ов, назначаемых в PropertyGrid и служащих для обработки элементов в базе,
/// классифицирующихся по категориям
/// </summary>
public class DBPropDescriptorHolder : PropDescriptorHolder
{
  /// <summary>Идентификатор в рамках категории</summary>
  protected object idValue;

  /// <summary>
  /// Добавляем зарегестрированные кастом дескрипторы в список
  /// </summary>
  /// <remarks>Для LoadData. Добавляем только новые, с проверкой на уникальность</remarks>
  /// <returns>Список всех кастом дескрипторов для тек. категории и ид.</returns>
  protected PropDescriptor[] AddRegisteredPropertyDescriptors()
  {
    PropDescriptor[] propDescriptors = CategoryPropsHolder.GetPropDescriptors((PropDescriptorHolder) this, this.Category, this.Id);
    if (propDescriptors != null)
    {
      for (int index = 0; index < propDescriptors.Length; ++index)
      {
        if (PropDescriptorHolder.IndexOfPDCItem(this.PropDescriptorCollection, (PropertyDescriptor) propDescriptors[index]) == -1)
          this.PropDescriptorCollection.Add((PropertyDescriptor) propDescriptors[index]);
      }
    }
    return propDescriptors;
  }

  /// <summary>Применение изменений для кастом дескр-ров</summary>
  /// <remarks>Для Apply на форме</remarks>
  /// <param name="idOld"></param>
  /// <returns></returns>
  protected bool ApplyToRegisteredPropertyDescriptors(object idOld)
  {
    CategoryPropsHolder.Apply((PropDescriptorHolder) this, this.Category, this.Id, idOld);
    return true;
  }

  /// <summary>Отмена изменений для кастом дескр-ров</summary>
  /// <remarks>Для Cancel на форме</remarks>
  protected void CancelToRegisteredPropertyDescriptors()
  {
    CategoryPropsHolder.Cancel((PropDescriptorHolder) this, this.Category, this.Id);
  }

  /// <summary>Обработка изменений данных</summary>
  /// <param name="e">Аргументы события</param>
  protected void ChangeEventDataToRegisteredPropertyDescriptors(EventArgs e)
  {
    CategoryPropsHolder.ChangeEventData((PropDescriptorHolder) this, this.Category, this.Id, e);
  }

  /// <summary>Конструктор</summary>
  /// <param name="idValue">Идентификатор в рамках категории </param>
  public DBPropDescriptorHolder(object idValue) => this.idValue = idValue;

  /// <summary>Идентификатор в рамках категории</summary>
  /// <remarks>Не совсем понятно зачем стоило разбивать на два метода?!</remarks>
  public virtual object Id => this.idValue;

  /// <summary>Идентификатор в рамках категории</summary>
  /// <param name="aId"></param>
  public virtual void SetId(object aId) => this.idValue = aId;

  /// <summary>Категория, которую обрабатывает Holder</summary>
  /// <remarks>Перекрыть в дочернем классе</remarks>
  public virtual int Category => 0;
}

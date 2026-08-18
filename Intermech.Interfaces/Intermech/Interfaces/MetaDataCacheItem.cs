
// Type: Intermech.Interfaces.MetaDataCacheItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует базовый класс для всех элементов кэша метаданных. Реализация thread safety основана на заморозке состояния элементов кэша. Если требуется
    /// обновить элемент кэша, то его следует клонировать, внести изменения, заморозить состояние, а затем поместить новый элемент в кэш на место старого.
    /// </summary>
    [Serializable]
    public abstract class MetaDataCacheItem : FreezableObject, IAssignable, ICloneable
    {
      /// <summary>Создает объект.</summary>
      protected MetaDataCacheItem() => this.Clear();

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public virtual void Clear() => this.RequireNotFrozen();

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public virtual void Assign(object source)
      {
        this.RequireNotFrozen();
        if (this == source)
          return;
        this.Clear();
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      object ICloneable.Clone() => this.Clone();

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public object Clone()
      {
        IAssignable instance = (IAssignable) Activator.CreateInstance(this.GetType());
        instance.Assign((object) this);
        return (object) instance;
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public virtual void Load(DataRow row)
      {
        if (row == null)
          throw new ArgumentNullException(nameof (row));
        this.RequireNotFrozen();
        this.Clear();
      }
    }
}

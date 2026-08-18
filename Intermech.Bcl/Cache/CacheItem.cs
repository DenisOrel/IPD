
// Type: Intermech.Cache.CacheItem
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache
{
    /// <summary>
    /// Реализует контейнер метаданных, относящихся к элементу, помещенному в кэш.
    /// </summary>
    public class CacheItem : ICloneable
    {
      /// <summary>Уникальный ключ элемента в кэше.</summary>
      private object key;
      /// <summary>
      /// Делегат метода, вызываемого перед удалением элемента из кэша.
      /// </summary>
      private BeforeRemoveEventHandler beforeRemove;
      /// <summary>
      /// Делегат метода, вызываемого после удаления элемента из кэша.
      /// </summary>
      private AfterRemoveEventHandler afterRemove;
      /// <summary>Массив объектов, контролирующих устаревание элемента.</summary>
      private IExpiration[] expirations;

      /// <summary>Создает новый контейнер метаданных.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="beforeRemove">Делегат метода, вызываемого перед удалением элемента из кэша</param>
      /// <param name="afterRemove">Делегат метода, вызываемого после удалениея элемента из кэша</param>
      /// <param name="expirations">Массив объектов, контролирующих устаревание элемента</param>
      public CacheItem(
        object key,
        BeforeRemoveEventHandler beforeRemove,
        AfterRemoveEventHandler afterRemove,
        IExpiration[] expirations)
      {
        Validator.CheckKey(key);
        Validator.CheckExpirations(expirations);
        this.key = key;
        this.beforeRemove = beforeRemove;
        this.afterRemove = afterRemove;
        this.expirations = expirations;
      }

      /// <summary>Создает клон контейнера метаданных.</summary>
      /// <param name="clonedItem">Клонируемый контейнер</param>
      public CacheItem(CacheItem clonedItem)
      {
        this.key = clonedItem.Key;
        this.beforeRemove = clonedItem.BeforeRemove;
        this.afterRemove = clonedItem.AfterRemove;
        this.expirations = clonedItem.Expirations;
      }

      /// <summary>Возвращает уникальный ключ элемента в кэше.</summary>
      public object Key => this.key;

      /// <summary>
      /// Возвращает делегат, вызываемый при удалении элемента из кэша.
      /// </summary>
      public BeforeRemoveEventHandler BeforeRemove => this.beforeRemove;

      /// <summary>
      /// Возвращает делегат, вызываемый при удалении элемента из кэша.
      /// </summary>
      public AfterRemoveEventHandler AfterRemove => this.afterRemove;

      /// <summary>
      /// Возвращает массив объектов, контролирующих устаревание элемента.
      /// </summary>
      public IExpiration[] Expirations => this.expirations;

      /// <summary>
      /// Создает и возвращает клон этого контейнера метаданных.
      /// </summary>
      /// <returns>Клон контейнера</returns>
      public object Clone() => (object) new CacheItem(this);
    }
}

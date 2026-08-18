
// Type: Intermech.Cache.BeforeRemoveEventHandler
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Представляет метод, который вызывается перед удалением элемента из кэша.
    /// </summary>
    /// <param name="key">Уникальный ключ элемента в кэше</param>
    /// <param name="data">"Элемент кэша</param>
    /// <param name="cause">Причина удаления элемента из кэша</param>
    public delegate void BeforeRemoveEventHandler(object key, object data, RemoveCause cause);
}

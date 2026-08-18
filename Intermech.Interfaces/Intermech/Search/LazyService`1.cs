
// Type: Intermech.Search.LazyService`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search
{
    public sealed class LazyService<T> where T : class
    {
      private volatile object _service;

      public T Value
      {
        get
        {
          if (this._service == null)
            this._service = (object) ServiceLocator.Get<T>();
          return this._service as T;
        }
      }
    }
}

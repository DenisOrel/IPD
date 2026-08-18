
// Type: Intermech.Protection.ILicenser
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Protection
{
    /// <summary>
    /// распределение лицензий через основной коннект к менеджеру лицензий
    /// </summary>
    public interface ILicenser
    {
      /// <summary>Распределить лицензию для приложения appId</summary>
      /// <param name="appId">идентификатор приложения</param>
      /// <returns>true, если новая лицензия распределена,
      /// false если лицензия уже была распределена ранее.</returns>
      bool AllocateLicense(int appId);

      /// <summary>Освобождает ранее распределенную лицензию для appId</summary>
      /// <param name="appId">идентификатор приложения</param>
      /// <returns>true, если лицензия освобожденя, false если лицензия не освобождена
      /// а у нее уменьшено количество ссылок или лицензия не была распределена.</returns>
      bool ReleaseLicense(int appId);
    }
}

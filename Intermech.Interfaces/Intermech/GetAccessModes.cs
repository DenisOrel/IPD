
// Type: Intermech.GetAccessModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech
{
    /// <summary>
    /// Управление полученим отчета о проверках прав доступа для пользовательской сессии
    /// </summary>
    /// <remarks>ServerMode - возвращает данные в "серверном" формате</remarks>
    public enum GetAccessModes
    {
      AllRecords,
      LastCheck,
      LastNRecs,
      ServerMode,
    }
}

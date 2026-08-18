
// Type: Intermech.Interfaces.Briefcase.CheckOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Briefcase
{
    public enum CheckOptions
    {
      None = 0,
      /// <summary>Необходима синхронизация метаданных</summary>
      IsSynhronizing = 1,
      /// <summary>Режим "Только добавление"</summary>
      CreateOnly = 2,
      /// <summary>
      /// Записывать в лог ошибки всегда, несмотря на синхронизацию
      /// </summary>
      IsErrorAlways = 4,
    }
}

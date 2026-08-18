
// Type: Intermech.DBConfigMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech
{
    /// <summary>Методы чтения (записи) конфигураций</summary>
    public enum DBConfigMode
    {
      /// <summary>Читать (писать) только пользовательские настройки</summary>
      UserOnly,
      /// <summary>Читать (писать) только глобальные настройки</summary>
      GlobalOnly,
      /// <summary>
      /// Читать сначала пользовательские настройки, а если их нет, то глобальные
      /// </summary>
      UserAndGlobal,
      /// <summary>
      /// Читать сначала глобальные настройки, а если их нет, то пользовательские
      /// </summary>
      GlobalAndUser,
    }
}

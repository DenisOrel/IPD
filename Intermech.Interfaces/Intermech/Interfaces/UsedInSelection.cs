
// Type: Intermech.Interfaces.UsedInSelection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Использование в выборках</summary>
    public enum UsedInSelection
    {
      /// <summary>Не используется в выборках</summary>
      None,
      /// <summary>Используется в выборках в пределах базы данных</summary>
      Base,
      /// <summary>Используется для запросов на портал</summary>
      Portal,
      /// <summary>Используется везде</summary>
      All,
    }
}


// Type: Intermech.Interfaces.SignFlags
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Битовый флаг к версии подписи.
    /// Флаги версии подписи располагаются в старшем байте версии подписи
    /// </summary>
    public enum SignFlags
    {
      /// <summary>
      /// Подпись, совместимая с внешними системами (Search) - подписывается только атрибут Файл, внутренние атрибуты не учитываются.
      /// 31 бит
      /// </summary>
      CompatibleSignFlag = 1073741824, // 0x40000000
    }
}

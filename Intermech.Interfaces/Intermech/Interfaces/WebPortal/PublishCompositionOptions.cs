
// Type: Intermech.Interfaces.WebPortal.PublishCompositionOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Опции получения публикуемого состава</summary>
    [Flags]
    public enum PublishCompositionOptions
    {
      /// <summary>Нео опций</summary>
      None = 0,
      /// <summary>Включить в список связанные объекты</summary>
      WithLinkedObjects = 1,
      /// <summary>
      /// В состав всегда добавляются объекты у которых есть атрибуты не влияющие на дату модификации объекта
      /// </summary>
      IncludeFreeChangeAttributes = 2,
      /// <summary>
      /// Необходима инфрормация о причине попадания в список публикуемых объектов
      /// </summary>
      InfoRequired = 4,
      /// <summary>
      /// Всегда включать в пакет объект, а не контейнер атрибутов на него
      /// </summary>
      IncludeObjectsAlways = 8,
      /// <summary>Принудительная публикация</summary>
      ForcedPublication = 16, // 0x00000010
    }
}

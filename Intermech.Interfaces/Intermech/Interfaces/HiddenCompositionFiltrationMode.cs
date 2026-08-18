
// Type: Intermech.Interfaces.HiddenCompositionFiltrationMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Режим фильтрации объектов со скрытым составом</summary>
    [Serializable]
    public enum HiddenCompositionFiltrationMode
    {
      /// <summary>Не выполнять никакой фильтрации</summary>
      None,
      /// <summary>
      /// Не показывать скрытый состав, но оставлять в составе объекты, имеющие скрытый состав
      /// </summary>
      HideChilds,
      /// <summary>
      /// Не показывать скрытый состав и объекты, имеющие скрытый состав
      /// </summary>
      HideAll,
    }
}

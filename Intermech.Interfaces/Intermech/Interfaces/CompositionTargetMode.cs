
// Type: Intermech.Interfaces.CompositionTargetMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Режимы обработки / назначения сортировки</summary>
    public enum CompositionTargetMode
    {
      /// <summary>Режим вставки связи в конец</summary>
      [Obsolete("Use InsertLast instead. Will be removed in IPS 9.0")] Add = 0,
      /// <summary>Режим вставки связи в конец</summary>
      InsertLast = 0,
      InsertBefore = 1,
      /// <summary>Режим вставки после указанной связи</summary>
      InsertAfter = 2,
      /// <summary>Режим вставки связи в начало</summary>
      InsertFirst = 3,
    }
}

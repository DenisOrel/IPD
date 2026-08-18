
// Type: Intermech.Interfaces.MapiRecipDesc
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Содержит информацию о получателе или отправителе сообщения.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MapiRecipDesc
    {
      /// <summary>зарезервировано, всегда 0</summary>
      public int reserved;
      /// <summary>тип получателя</summary>
      public int recipClass;
      /// <summary>имя получателя или отправителя</summary>
      public string name;
      /// <summary>адресс получателя или отправителя</summary>
      public string address;
      /// <summary>размер в байтах указателя lpEntryID</summary>
      public int eIDSize;
      /// <summary>
      /// указатель на идентификатор, используемый системой сообщений,
      /// чтобы установить получателя сообщения
      /// </summary>
      public IntPtr entryID;
    }
}

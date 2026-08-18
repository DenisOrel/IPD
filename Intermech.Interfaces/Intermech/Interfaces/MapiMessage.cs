
// Type: Intermech.Interfaces.MapiMessage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Interfaces
{
    /// <summary>Информация о посылаемом/принимаемом сообщении</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MapiMessage
    {
      /// <summary>зарезервировано</summary>
      public int reserved;
      /// <summary>тема сообщения</summary>
      public string subject;
      /// <summary>тест сообщения</summary>
      public string noteText;
      /// <summary>тип сообщения</summary>
      public string messageType;
      /// <summary>дату получения в формате YYYY/MM/DD HH(0-24):MM</summary>
      public string dateReceived;
      /// <summary>идентификатор  потока, в который поступило сообщение</summary>
      public string conversationID;
      /// <summary>маска флагов сообщений</summary>
      public int flags;
      /// <summary>информация об отправителе сообщения</summary>
      public IntPtr originator;
      /// <summary>количество получателей, записанных в массиве recips</summary>
      public int recipCount;
      /// <summary>
      /// указатель на структуры MapiRecipDesc,  содержащие информацию о получателях сообщения
      /// </summary>
      public IntPtr recips;
      /// <summary>количество файлов-приложений в массиве lpFiles.</summary>
      public int fileCount;
      /// <summary>
      /// указатель на структуру, содержащую информацию о всех файлах, приложенных к сообщению
      /// </summary>
      public IntPtr files;
    }
}


// Type: Intermech.Search.RecentObjects.RecentObjectAction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Search.RecentObjects
{
    [Flags]
    public enum RecentObjectAction
    {
      None = 0,
      [Description("Создание нового объекта")] Create = 1,
      [Description("Взятие объекта на изменение")] CheckOut = 16, // 0x00000010
      [Description("Завершение изменений в объекте")] CheckIn = 32, // 0x00000020
      [Description("Отмена изменений в объекте")] CancelChanges = 64, // 0x00000040
      [Description("Сохранение измений в объекте")] SaveChanges = 128, // 0x00000080
      [Description("Открытие объекта в новом окне")] OpenInNewWindow = 256, // 0x00000100
      [Description("Открытие объекта (команда \"Открыть\")")] Open = 4096, // 0x00001000
      [Description("Редактирование объекта (команда \"Редактировать\")")] Edit = 8192, // 0x00002000
      [Description("Просмотр объекта (команда \"Просмотр\")")] View = 16384, // 0x00004000
      [Description("Печать объекта (команда \"Печать\")")] Print = 32768, // 0x00008000
      All = Print | View | Edit | Open | OpenInNewWindow | SaveChanges | CancelChanges | CheckIn | CheckOut | Create, // 0x0000F1F1
    }
}

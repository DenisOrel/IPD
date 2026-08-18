// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImFileAtt
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>
/// Набор атрибутов, которые могут быть назначены Каталогу, папке или таблице. Каждое значение описывает один бит, начиная с младшего
/// </summary>
[Flags]
internal enum ImFileAtt
{
  [Description("Зарезервирован системой и в настоящее время не используется")] ITF_CATALOG = 1,
  [Description("Объект \"только чтение\"")] ITF_READONLY = 2,
  [Description("Системный объект")] ITF_SYSTEM = 4,
  [Description("Скрытый объект")] ITF_HIDDEN = 8,
  [Description("Конструкторский")] ITF_DESIGN = 16, // 0x00000010
  [Description("Технологический")] ITF_TECHNO = 32, // 0x00000020
  [Description("Личный")] ITF_PRIVATE = 64, // 0x00000040
  [Description("Не удаляемый")] ITF_UNDELETABLE = 128, // 0x00000080
  [Description("Не участвует в индексации")] ITF_NOINDEXING = 256, // 0x00000100
  [Description("Не участвует в проверке индекса")] ITF_NOINDEXCHECK = 512, // 0x00000200
  [Description("Связан с таблицей")] ITF_TABLINKED = 65536, // 0x00010000
}

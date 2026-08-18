// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImFileAtt
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

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

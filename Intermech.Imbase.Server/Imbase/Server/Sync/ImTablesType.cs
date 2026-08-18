// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImTablesType
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal enum ImTablesType
{
  [Description("Неизвестный тип")] IMTT_UNKNOWN,
  [Description("Таблица Каталога")] IMTT_CATALOG,
  [Description("Справочник")] IMTT_CTLREF,
  [Description("Технологический справочник")] IMTT_TECHREF,
  [Description("Пользовательская таблица")] IMTT_TABLE,
  [Description("Таблица индекса Каталога")] IMTT_INDEX,
  [Description("Таблица записей каталога")] IMTT_CTLREC,
}

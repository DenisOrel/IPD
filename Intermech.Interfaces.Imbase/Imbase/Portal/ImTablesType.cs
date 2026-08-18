// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImTablesType
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>Тип записи в таблице IM_TABLES</summary>
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

// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImDataMode
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>Вид данных в поле таблицы IMBASE</summary>
public enum ImDataMode
{
  /// <summary>Обычные данные</summary>
  [Description("Обычные данные")] IDM_DATA,
  /// <summary>Имя другой таблицы</summary>
  [Description("Имя другой таблицы")] IDM_TABLE,
  /// <summary>Ссылка на рисунок</summary>
  [Description("Ссылка на рисунок")] IDM_IMAGE,
  /// <summary>Ссылка на описание</summary>
  [Description("Ссылка на описание")] IDM_TEXT,
}

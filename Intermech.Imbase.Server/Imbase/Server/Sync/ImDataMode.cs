// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImDataMode
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal enum ImDataMode
{
  [Description("Обычные данные")] IDM_DATA,
  [Description("Имя другой таблицы")] IDM_TABLE,
  [Description("Ссылка на рисунок")] IDM_IMAGE,
  [Description("Ссылка на описание")] IDM_TEXT,
}

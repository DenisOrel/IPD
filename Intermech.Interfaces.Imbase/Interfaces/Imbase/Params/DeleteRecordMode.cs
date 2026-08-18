// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.DeleteRecordMode
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>Режим удаления записи</summary>
public enum DeleteRecordMode
{
  [Description("Запретить удаление")] Disable,
  [Description("Запрос перед удалением")] Ask,
  [Description("Разрешить удаление")] Enable,
}

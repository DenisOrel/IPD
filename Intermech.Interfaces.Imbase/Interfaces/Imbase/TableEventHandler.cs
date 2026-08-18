// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.TableEventHandler
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Делегат описывает действия над таблицей IMBASE</summary>
/// <param name="tableId"></param>
public delegate void TableEventHandler(object sender, ImbaseTableEventArgs e);

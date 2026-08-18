// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ITablesCache
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

public interface ITablesCache
{
  DataSet Load(Guid session, long tableId);

  DataSet Load(IUserSession session, long tableId);

  void Remove(long tableId);

  void Clear();

  bool Enabled { get; set; }
}

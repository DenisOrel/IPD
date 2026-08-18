// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseRestrictiveCache
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Кэш ограничительного перечня Imbase</summary>
public interface IImbaseRestrictiveCache
{
  void Add(long userId, string imbaseInternalKey);

  HashSet<string> GetList(long userId);

  bool Check(long userId, string imbaseInternalKey);

  void Remove(long userId, string imbaseInternalKey);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.IFileListView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal interface IFileListView
{
  void AppendItem(
    string key,
    string name,
    string type,
    long length,
    DateTime lastWriteTime,
    string state);

  void UpdateItem(string key, string state);

  bool ContainsItem(string key);

  void ClearItems();

  string GetSelectedItem();

  void AutoSizeColumns();

  void ReapplySort();

  int ItemsCount { get; }
}

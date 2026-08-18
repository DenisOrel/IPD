// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectFilesRenameTable
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

public sealed class ObjectFilesRenameTable : ICloneable
{
  private List<string> whatToReplaceList;
  private List<string> toReplaceWithList;
  private Tuple<string[], string[]> arrayPair;

  public ObjectFilesRenameTable()
    : this(4)
  {
  }

  public ObjectFilesRenameTable(int capacity)
  {
    this.whatToReplaceList = new List<string>(capacity);
    this.toReplaceWithList = new List<string>(capacity);
  }

  public void Clear()
  {
    this.whatToReplaceList.Clear();
    this.toReplaceWithList.Clear();
    this.ResetCache();
  }

  public void Add(string whatToReplacePath, string toReplaceWithPath)
  {
    if (string.IsNullOrEmpty(whatToReplacePath))
      throw new ArgumentException("Путь к файлу не задан.", nameof (whatToReplacePath));
    if (string.IsNullOrEmpty(toReplaceWithPath))
      throw new ArgumentException("Путь к файлу не задан.", nameof (toReplaceWithPath));
    this.whatToReplaceList.Add(whatToReplacePath);
    this.toReplaceWithList.Add(toReplaceWithPath);
    this.ResetCache();
  }

  public void Add(ObjectFilesRenameTable otherTable)
  {
    if (otherTable == null)
      throw new ArgumentNullException(nameof (otherTable));
    for (int index = 0; index < otherTable.whatToReplaceList.Count; ++index)
      this.Add(otherTable.whatToReplaceList[index], otherTable.toReplaceWithList[index]);
    this.ResetCache();
  }

  private void ResetCache()
  {
    if (this.arrayPair == null)
      return;
    this.arrayPair = (Tuple<string[], string[]>) null;
  }

  public Tuple<string[], string[]> AsArrayPair()
  {
    if (this.arrayPair == null)
      this.arrayPair = new Tuple<string[], string[]>(this.whatToReplaceList.ToArray(), this.toReplaceWithList.ToArray());
    return this.arrayPair;
  }

  public bool IsEmpty
  {
    [DebuggerStepThrough] get => this.whatToReplaceList.Count == 0;
  }

  public ObjectFilesRenameTable Clone()
  {
    ObjectFilesRenameTable filesRenameTable = new ObjectFilesRenameTable(this.whatToReplaceList.Count);
    for (int index = 0; index < this.whatToReplaceList.Count; ++index)
      filesRenameTable.Add(this.whatToReplaceList[index], this.toReplaceWithList[index]);
    return filesRenameTable;
  }

  object ICloneable.Clone() => (object) this.Clone();
}

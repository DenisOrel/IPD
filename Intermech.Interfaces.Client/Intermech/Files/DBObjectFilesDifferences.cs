// Decompiled with JetBrains decompiler
// Type: Intermech.Files.DBObjectFilesDifferences
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

public sealed class DBObjectFilesDifferences : ICloneable
{
  private DBObjectState objectState;
  private List<FileDifferencePair> differencePairs;

  public DBObjectFilesDifferences(DBObjectState objectState, int capacity)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    if (capacity < 0)
      throw new ArgumentOutOfRangeException(nameof (capacity));
    this.objectState = objectState;
    this.differencePairs = new List<FileDifferencePair>(capacity);
  }

  public DBObjectFilesDifferences(DBObjectState objectState)
    : this(objectState, 4)
  {
  }

  public DBObjectFilesDifferences Clone()
  {
    DBObjectFilesDifferences filesDifferences = new DBObjectFilesDifferences(this.objectState, this.differencePairs.Count);
    foreach (FileDifferencePair differencePair in this.DifferencePairs)
      filesDifferences.DifferencePairs.Add(differencePair.Clone());
    return filesDifferences;
  }

  object ICloneable.Clone() => (object) this.Clone();

  public DBObjectState ObjectState
  {
    [DebuggerStepThrough] get => this.objectState;
  }

  public List<FileDifferencePair> DifferencePairs
  {
    [DebuggerStepThrough] get => this.differencePairs;
  }
}

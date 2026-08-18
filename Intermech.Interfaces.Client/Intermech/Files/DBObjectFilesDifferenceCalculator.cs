// Decompiled with JetBrains decompiler
// Type: Intermech.Files.DBObjectFilesDifferenceCalculator
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Files;

public sealed class DBObjectFilesDifferenceCalculator
{
  private readonly IFileArea fileArea;
  private readonly IDBObjectFilesDifferenceRules diffRules;
  private readonly IDBFilesInformationService dbFilesInformation;
  private readonly FileDifferenceCalculator fileDiffCalculator;
  private readonly List<DBObjectFilesDifferenceCalculator.ObjectInputData> unprocessed;
  private readonly List<DBObjectFilesDifferenceCalculator.ObjectInputData> processed;
  private readonly Dictionary<long, DBObjectFilesDifferenceCalculator.GuardType> guardTable;
  private readonly List<DBObjectFilesDifferences> result;

  public DBObjectFilesDifferenceCalculator(
    IFileArea fileArea,
    IDBObjectFilesDifferenceRules differenceRules,
    IDBFilesInformationService dbFilesInformation,
    int capacity)
  {
    if (fileArea == null)
      throw new ArgumentNullException(nameof (fileArea));
    if (differenceRules == null)
      throw new ArgumentNullException(nameof (differenceRules));
    if (dbFilesInformation == null)
      throw new ArgumentNullException(nameof (dbFilesInformation));
    if (capacity < 0)
      throw new ArgumentOutOfRangeException(nameof (capacity));
    this.fileArea = fileArea;
    this.diffRules = differenceRules;
    this.dbFilesInformation = dbFilesInformation;
    this.fileDiffCalculator = new FileDifferenceCalculator();
    this.unprocessed = new List<DBObjectFilesDifferenceCalculator.ObjectInputData>(capacity);
    this.processed = new List<DBObjectFilesDifferenceCalculator.ObjectInputData>(capacity);
    this.guardTable = new Dictionary<long, DBObjectFilesDifferenceCalculator.GuardType>(capacity);
    this.result = new List<DBObjectFilesDifferences>(capacity);
  }

  public void Add(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.AddCore(objectState, (List<FileState>) null);
  }

  public void Add(DBObjectState objectState, List<FileState> areaFiles)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    if (areaFiles == null)
      throw new ArgumentNullException(nameof (areaFiles));
    this.AddCore(objectState, areaFiles);
  }

  public void AddRange(ICollection<DBObjectState> objectStates)
  {
    if (objectStates == null)
      throw new ArgumentNullException();
    foreach (DBObjectState objectState in (IEnumerable<DBObjectState>) objectStates)
      this.Add(objectState);
  }

  private void AddCore(DBObjectState objectState, List<FileState> areaFiles)
  {
    if (this.guardTable.ContainsKey(objectState.ObjectId))
      return;
    if (this.guardTable.ContainsKey(objectState.Id))
      throw new InvalidOperationException($"Попытка повторного включения объекта IPS (идентификатор версии = {objectState.ObjectId}, идентификатор объекта = {objectState.Id}) в обработчик типа '{this.GetType()}'.");
    this.unprocessed.Add(new DBObjectFilesDifferenceCalculator.ObjectInputData(objectState, areaFiles));
    this.guardTable.Add(objectState.ObjectId, DBObjectFilesDifferenceCalculator.GuardType.ObjectID);
    this.guardTable.Add(objectState.Id, DBObjectFilesDifferenceCalculator.GuardType.ID);
  }

  public void Clear()
  {
    this.unprocessed.Clear();
    this.processed.Clear();
    this.guardTable.Clear();
    this.result.Clear();
  }

  public void Invalidate()
  {
    this.result.Clear();
    this.unprocessed.AddRange((IEnumerable<DBObjectFilesDifferenceCalculator.ObjectInputData>) this.processed);
    this.processed.Clear();
  }

  public void Calculate()
  {
    if (this.unprocessed.Count <= 0)
      return;
    this.CalculateCore();
  }

  private void CalculateCore()
  {
    List<DBObjectStateWithFiles> fileStates = this.dbFilesInformation.GetFileStates((IList<DBObjectState>) this.unprocessed.ConvertAll<DBObjectState>((Converter<DBObjectFilesDifferenceCalculator.ObjectInputData, DBObjectState>) (item => item.ObjectState)));
    DateTime utcNow = DateTime.UtcNow;
    List<DBObjectFilesDifferences> collection = new List<DBObjectFilesDifferences>(this.unprocessed.Count);
    for (int index = 0; index < this.unprocessed.Count; ++index)
    {
      DBObjectFilesDifferenceCalculator.ObjectInputData inputItem = this.unprocessed[index];
      DBObjectStateWithFiles objectStateWithFiles = fileStates[index];
      DBObjectFilesDifferences resultItem = new DBObjectFilesDifferences(objectStateWithFiles.Owner);
      foreach (FileState file in objectStateWithFiles.Files)
      {
        FileState areaFileState = this.CalculateAreaFileState(inputItem, file);
        resultItem.DifferencePairs.Add(this.diffRules.CalculateDifference(utcNow, resultItem.ObjectState, areaFileState, file));
      }
      if (inputItem.ArearFiles != null)
      {
        foreach (FileState localState in inputItem.ArearFiles.FindAll((Predicate<FileState>) (inputAreaFile => !resultItem.DifferencePairs.Exists((Predicate<FileDifferencePair>) (diffPair => diffPair.RemoteState != null && PathUtils.IsSamePath(diffPair.RemoteState.FileName, inputAreaFile.FileName))))))
          resultItem.DifferencePairs.Add(this.fileDiffCalculator.Calculate(localState, (FileState) null));
      }
      collection.Add(resultItem);
    }
    this.result.AddRange((IEnumerable<DBObjectFilesDifferences>) collection);
    this.processed.AddRange((IEnumerable<DBObjectFilesDifferenceCalculator.ObjectInputData>) this.unprocessed);
    this.unprocessed.Clear();
  }

  private FileState CalculateAreaFileState(
    DBObjectFilesDifferenceCalculator.ObjectInputData inputItem,
    FileState dbFile)
  {
    if (inputItem.ArearFiles != null)
      return inputItem.ArearFiles.Find((Predicate<FileState>) (item => PathUtils.IsSamePath(item.FileName, dbFile.FileName)));
    string path = Path.Combine(this.fileArea.AreaPath, dbFile.FileName);
    return File.Exists(path) ? FileState.FromFile(path, dbFile.FileName) : (FileState) null;
  }

  public List<DBObjectFilesDifferences> Results => this.result;

  private sealed class ObjectInputData
  {
    public readonly DBObjectState ObjectState;
    public readonly List<FileState> ArearFiles;

    public ObjectInputData(DBObjectState objectState, List<FileState> areaFiles)
    {
      this.ObjectState = objectState;
      this.ArearFiles = areaFiles;
    }
  }

  private enum GuardType
  {
    ID,
    ObjectID,
  }
}

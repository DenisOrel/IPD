// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Indexes.IndexesHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Indexes;

internal class IndexesHelper
{
  public IndexesHelper(long sourceID, IndexesStatus status = IndexesStatus.None)
  {
    this.SourceID = sourceID;
    this.ImageIndex = ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service ? service.ImageIndex("imgIndexes") : -1;
    this.AddedIndexes = new Dictionary<int, bool>(0);
    this.ChangedIndexes = new Dictionary<int, bool>(0);
    this.RemovedIndexes = new List<int>(0);
    this.Actions = status;
  }

  public IndexesStatus Actions { get; set; }

  public Dictionary<int, bool> AddedIndexes { get; set; }

  public Dictionary<int, bool> ChangedIndexes { get; set; }

  public int ImageIndex { get; set; }

  public List<int> RemovedIndexes { get; set; }

  public long SourceID { get; private set; }

  public List<long> DeletedRowNums { get; set; }

  public List<int> DeletedColumns { get; set; }

  public long PrevCatalogID { get; set; }

  public List<long> PastedObjIDs { get; set; }
}

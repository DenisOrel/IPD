// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.ClipboardFiles
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.Collections;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class ClipboardFiles
{
  public ArrayList FileIDs;
  public long StorageID;

  public ClipboardFiles(long storageID)
  {
    this.StorageID = storageID;
    this.FileIDs = new ArrayList();
  }

  public void Add(long file) => this.FileIDs.Add((object) file);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileStorageNode
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileStorageNode : ObjectNode
{
  private ConditionStructure[] _conditionStructures;

  public ConditionStructure[] ConditionStructures
  {
    get
    {
      return FileStorageView.FilterStructures == null ? this._conditionStructures : ConditionStructure.Join(FileStorageView.FilterStructures, this._conditionStructures);
    }
    set => this._conditionStructures = value;
  }

  public FileStorageNode(int objTypeID, long objID)
    : base(objTypeID, objID)
  {
    this.Options = NodeOptions.None;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new FilesNodePart(this.ConditionStructures, this._objID));
  }
}

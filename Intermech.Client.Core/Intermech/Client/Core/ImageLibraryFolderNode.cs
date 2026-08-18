
// Type: Intermech.Client.Core.ImageLibraryFolderNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;


namespace Intermech.Client.Core;

/// <summary>Summary description for ImageLibraryFolder.</summary>
public class ImageLibraryFolderNode : ObjectNode
{
  private ConditionStructure foldersCondition;
  private ConditionStructure nonFoldersCondition;

  public ImageLibraryFolderNode(int folderTypeID, long folderObjID)
    : base(folderTypeID, folderObjID)
  {
    this.foldersCondition = new ConditionStructure(-7, RelationalOperators.Equal, (object) Intermech.Client.Core.Thumbnail.Consts.ImageLibraryFolderTypeID, LogicalOperators.AND, 0, false);
    this.nonFoldersCondition = new ConditionStructure(-7, RelationalOperators.Equal, (object) Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID, LogicalOperators.AND, 0, false);
  }

  protected override INodePart CreateFolderPart(int relTypeId)
  {
    return (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relTypeId, this.foldersCondition, this.Services);
  }

  protected override INodePart CreateNonFolderPart(int relTypeId)
  {
    return (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, relTypeId, this.nonFoldersCondition, this.Services);
  }
}

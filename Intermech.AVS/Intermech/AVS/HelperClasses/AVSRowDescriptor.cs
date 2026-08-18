// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.AVSRowDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.AVS.HelperClasses;

internal class AVSRowDescriptor : Intermech.Navigator.DBObjects.Descriptor
{
  private AVSRow row;

  public AVSRowDescriptor(AVSRow row)
  {
    this.row = row;
    this._objID = row.ObjectId;
    this._objGuid = row.ObjGuid;
    this._state = ObjectFiltrationState.fsNotRequired;
  }

  public override INodeID GetRecordNodeID()
  {
    CreateObjectNodeParams createObjectNodeParams = new CreateObjectNodeParams()
    {
      Caption = this.row.Caption,
      CheckedOutBy = AVSSelectedItemsHelper.ToInt64(this.row.GetFieldValue(new AvsRowAttributeInfo(false, -6), 0, 0, false, false), 0L),
      ID = this.row.Object_F_ID,
      LCStepID = AVSSelectedItemsHelper.ToInt32(this.row.GetFieldValue(new AvsRowAttributeInfo(false, -4), 0, 0, false, false), -1),
      ObjectID = this.row.ObjectId,
      ObjectTypeID = this.row.ObjType,
      Owner = AVSSelectedItemsHelper.ToInt64(this.row.GetFieldValue(new AvsRowAttributeInfo(false, -8), 0, 0, false, false), 0L),
      PrjLinkID = 0,
      RelationTypeID = -1,
      Sorting = this.row.SortIndex,
      State = ObjectFiltrationState.fsCorresponding,
      Version = AVSSelectedItemsHelper.ToInt64(this.row.GetFieldValue(new AvsRowAttributeInfo(false, -5), 0, 0, false, false), 0L)
    };
    createObjectNodeParams.PrjLinkID = 0L;
    createObjectNodeParams.RelationTypeID = -1;
    createObjectNodeParams.BaseVersion = 0L;
    return this.CreateObjectNodeIdFromParams(createObjectNodeParams);
  }
}

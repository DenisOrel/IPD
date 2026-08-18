
// Type: Intermech.Interface.Attributes4RelationReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Interface
{
    public sealed class Attributes4RelationReader(
      int typeID,
      bool includeManual = false,
      int[] forbiddenAttributeIDs = null) : Attributes4TypeReader<IMSAttribute4RelationType>(typeID, includeManual, forbiddenAttributeIDs)
    {
      protected override List<IMSAttribute4RelationType> CollectionItems
      {
        get => MetaDataHelper.GetAttribute4RelationTypeList(this.typeID);
      }
    }
}

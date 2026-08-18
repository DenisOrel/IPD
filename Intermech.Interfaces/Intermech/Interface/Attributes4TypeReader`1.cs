
// Type: Intermech.Interface.Attributes4TypeReader`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Interface
{
    public abstract class Attributes4TypeReader<TCollectionItem> where TCollectionItem : IMSAttribute4
    {
      protected int typeID;
      protected bool includeManual;
      protected int[] forbiddenAttributeIDs;

      public Attributes4TypeReader(int typeID, bool includeManual = false, int[] forbiddenAttributeIDs = null)
      {
        this.typeID = typeID;
        this.includeManual = includeManual;
        this.forbiddenAttributeIDs = forbiddenAttributeIDs;
      }

      protected abstract List<TCollectionItem> CollectionItems { get; }

      public List<TypeAttribute> Read()
      {
        List<TypeAttribute> typeAttributeList = new List<TypeAttribute>();
        foreach (TCollectionItem collectionItem in this.CollectionItems)
        {
          if ((this.includeManual || collectionItem.Required != RequiredModes.Manual) && (this.forbiddenAttributeIDs == null || Array.IndexOf<int>(this.forbiddenAttributeIDs, collectionItem.AttributeID) == -1))
            typeAttributeList.Add(new TypeAttribute((IMSAttribute4) collectionItem));
        }
        return typeAttributeList;
      }
    }
}

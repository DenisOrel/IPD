
// Type: Intermech.Search.AutoConcretization.AutoConcretizationHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.AutoConcretization
{
    public static class AutoConcretizationHelper
    {
      public static bool IsCompositionAutoConcretizationAttributeExists(int objectTypeID)
      {
        if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
          throw new ArgumentException();
        return MetaDataHelper.GetAttribute4ObjectType(objectTypeID, AutoConcretizationConstants.CompositionAutoConcretizationAttributeTypeID) != null;
      }
    }
}

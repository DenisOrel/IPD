
// Type: Intermech.Search.Utilities.ObjectTypeApplicabilityHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Utilities
{
    public static class ObjectTypeApplicabilityHelper
    {
      public static bool IsSoftConcretizationMode(
        int projectTypeID,
        int relationTypeID,
        int partTypeID)
      {
        if (projectTypeID == -1)
          throw new ArgumentException();
        if (relationTypeID == -1)
          throw new ArgumentException();
        if (partTypeID == -1)
          throw new ArgumentException();
        IMSApplicability applicability = MetaDataHelper.GetApplicability(projectTypeID, partTypeID, relationTypeID);
        return applicability != null && applicability.Options.HasFlag((Enum) ApplicabilityOptions.SoftInstantiation);
      }
    }
}

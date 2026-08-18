
// Type: Intermech.Search.VersionSelectionRules.VersionSelectionRulesConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.VersionSelectionRules
{
    public static class VersionSelectionRulesConstants
    {
      public static readonly Guid VersionSelectionRuleObjectTypeGuid = new Guid("cad001b3-306c-11d8-b4e9-00304f19f545");

      public static int VersionSelectionRuleObjectTypeID
      {
        get
        {
          return MetaDataHelper.GetObjectTypeID(VersionSelectionRulesConstants.VersionSelectionRuleObjectTypeGuid);
        }
      }

      public static int[] AllVersionSelectionRuluObjectTypeIds
      {
        get
        {
          return MetaDataHelper.GetObjectTypeChildrenIDRecursive(VersionSelectionRulesConstants.VersionSelectionRuleObjectTypeID).ToArray();
        }
      }
    }
}

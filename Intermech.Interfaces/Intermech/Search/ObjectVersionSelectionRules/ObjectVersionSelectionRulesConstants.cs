
// Type: Intermech.Search.ObjectVersionSelectionRules.ObjectVersionSelectionRulesConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.ObjectVersionSelectionRules
{
    public static class ObjectVersionSelectionRulesConstants
    {
      public static readonly Guid ObjectVersionSelectionRuleObjectTypeGuid = new Guid("cad001b3-306c-11d8-b4e9-00304f19f545");

      public static int ObjectVersionSelectionRuleObjectTypeID
      {
        get
        {
          return MetaDataHelper.GetObjectTypeID(ObjectVersionSelectionRulesConstants.ObjectVersionSelectionRuleObjectTypeGuid);
        }
      }
    }
}

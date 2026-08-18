
// Type: Intermech.Interfaces.XMLRuleConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Константы XML для правил подбора версий</summary>
    internal abstract class XMLRuleConsts
    {
      /// <summary>xml version=\"1.0\" ?</summary>
      internal const string xmlHeader = "<?xml version=\"1.0\" ?>";
      /// <summary>xml version=\"1.0\" encoding=\"utf-8\" ?</summary>
      internal const string xmlHeaderUTF8 = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
      /// <summary>xmlHeader + xmlRootNode;</summary>
      internal const string xmlEmptyDoc = "<?xml version=\"1.0\" ?>\n<Intermech.NET />\n";
      /// <summary>Intermech.NET</summary>
      internal const string xmlRootNode = "Intermech.NET";
      /// <summary>criterion</summary>
      internal const string xmlCriterionNode = "criterion";
      /// <summary>value</summary>
      internal const string xmlValueNode = "value";
      /// <summary>actualDate</summary>
      internal const string xmlActualDate = "actualDate";
      /// <summary>function</summary>
      internal const string xmlattrFunction = "function";
      /// <summary>guid</summary>
      internal const string xmlattrGUID = "guid";
      /// <summary>not</summary>
      internal const string xmlattrNot = "not";
      /// <summary>type</summary>
      internal const string xmlattrType = "type";
      /// <summary>bool</summary>
      internal const string xmlattrBool = "bool";
      /// <summary>rule_type</summary>
      internal const string xmlattrRuleType = "rule_type";
      /// <summary>default_rule</summary>
      internal const string xmlattrDefaultRule = "default_rule";
    }
}

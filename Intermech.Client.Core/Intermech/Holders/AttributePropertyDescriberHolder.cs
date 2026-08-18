
// Type: Intermech.Holders.AttributePropertyDescriberHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.PropertyEditors;
using System.Collections.Specialized;


namespace Intermech.Holders;

/// <summary>
/// Хранитель зарегистрированных обработчиков на атрибуты ObjectPropertyGrid
/// </summary>
public class AttributePropertyDescriberHolder
{
  private static HybridDictionary describerHashtable = new HybridDictionary();

  public static void AddDescriber(int attributeId, IAttributePropertyDescriber describer)
  {
    if (AttributePropertyDescriberHolder.describerHashtable[(object) attributeId] is IAttributePropertyDescriber)
      AbortException.Abort(string.Format(LocalizationHolder.rm.GetString("Client.Core_228"), (object) attributeId.ToString()));
    AttributePropertyDescriberHolder.describerHashtable.Add((object) attributeId, (object) describer);
  }

  public static void RemoveDescriber(int attributeId)
  {
    AttributePropertyDescriberHolder.describerHashtable.Remove((object) attributeId);
  }

  public static IAttributePropertyDescriber GetDescriber(int attributeId)
  {
    return AttributePropertyDescriberHolder.describerHashtable[(object) attributeId] as IAttributePropertyDescriber;
  }
}

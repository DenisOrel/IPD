
// Type: Intermech.Client.Core.FormDesigner.Controls.ExtendedParent4Control
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public static class ExtendedParent4Control
{
  /// <summary>
  /// Метод для получения опций атрибута у типа объекта/связи или у типа атрибута (при отсутствии атрибута у типа объекта/связи).
  /// </summary>
  /// <param name="parentInfo"></param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="session">Сессия</param>
  /// <returns>Опции</returns>
  public static AttributeOptions GetAttributeOptions(
    this IExtendedParent4Control parentInfo,
    int attrID,
    IUserSession session = null)
  {
    IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
    int num = parentInfo.ParentTypeID;
    if (parentInfo.ParentInfo != null)
    {
      if (parentInfo.ParentInfo.ElementKind == AttributableElements.Object)
      {
        if (num == -1 && session != null)
        {
          IDBObject objectActualCopy = session.GetObjectActualCopy(parentInfo.ParentInfo.ElementIdentifier, false);
          num = objectActualCopy != null ? objectActualCopy.TypeID : -1;
        }
        if (num != -1)
          imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(num, attrID);
      }
      else if (parentInfo.ParentInfo.ElementKind == AttributableElements.Relation)
      {
        if (num == -1 && session != null)
        {
          IDBRelation relation = session.GetRelation(parentInfo.ParentInfo.ElementIdentifier, false);
          num = relation != null ? relation.TypeID : -1;
        }
        if (num != -1)
          imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(num, attrID);
      }
    }
    AttributeOptions attributeOptions;
    if (imsAttribute4 != null)
    {
      attributeOptions = imsAttribute4.Options;
      if (imsAttribute4.Required == RequiredModes.Manual)
        attributeOptions &= ~AttributeOptions.DisableNulls;
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
      attributeOptions = (AttributeOptions) ((attributeType != null ? (int) attributeType.Options : 0) & -9);
    }
    return attributeOptions;
  }
}

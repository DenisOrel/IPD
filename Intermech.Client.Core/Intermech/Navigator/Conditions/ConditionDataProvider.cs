
// Type: Intermech.Navigator.Conditions.ConditionDataProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Базовый класс, провайдер данных для условия выборки с атрибутом.
/// </summary>
public abstract class ConditionDataProvider : IConditionDataProvider
{
  protected List<SelectionParameterTypes> enabledParameterTypes;

  public virtual List<SelectionParameterTypes> EnabledParameterTypes
  {
    get
    {
      if (this.enabledParameterTypes == null)
        this.ReloadEnabledParameterTypes();
      return this.enabledParameterTypes;
    }
  }

  protected void ReloadEnabledParameterTypes()
  {
    this.enabledParameterTypes = new List<SelectionParameterTypes>();
    foreach (SelectionParameterTypes selectionParameterTypes in Enum.GetValues(typeof (SelectionParameterTypes)))
      this.enabledParameterTypes.Add(selectionParameterTypes);
  }

  public virtual int UserTypeID => -1;

  public virtual int[] UserGroupTypeIDs => new int[0];

  public Guid GetAttributeGuid(object attributeID)
  {
    switch (attributeID)
    {
      case Guid attributeGuid:
        return attributeGuid;
      case ObligatoryObjectAttributes _:
      case int _:
        return this.GetAttributeGuidFromId((int) attributeID);
      default:
        return Guid.Empty;
    }
  }

  public int GetAttributeID(object attributeID)
  {
    switch (attributeID)
    {
      case ObligatoryObjectAttributes _:
      case int _:
        return (int) attributeID;
      case Guid attributeGuid:
        return this.GetAttributeIdFromGuid(attributeGuid);
      default:
        return 0;
    }
  }

  protected abstract int GetAttributeIdFromGuid(Guid attributeGuid);

  protected abstract Guid GetAttributeGuidFromId(int attributeID);

  public abstract bool AnyAttributes(AttributeSourceTypes sourceType, int[] objectTypeIDs);

  public abstract string GetAttributeName(object attributeID);

  public abstract FieldTypes GetFieldType(object attributeID);

  public abstract List<ConditionAttributeInfo> GetListAttributes(
    AttributeSourceTypes sourceType,
    int[] objectTypeIDs);

  public abstract Dictionary<object, string> GetPossibleValues(object attributeID);

  public abstract bool ChoiseObjectType(ref object objectType, SelectionType selectionType);

  public string ConvertToString(
    object attributeID,
    RelationalOperators relationalOperator,
    SelectionParameterTypes selParType,
    object objValue,
    Dictionary<object, string> possibleValues,
    object tag)
  {
    return ServicesManager.GetService<IConditionDisplayService>().ConvertConditionValueToString((IConditionDataProvider) this, relationalOperator, attributeID, selParType, objValue, possibleValues, tag);
  }

  public string GetUserCaption(object userID)
  {
    if (Convert.ToString(userID) == Intermech.Consts.CurrentUserFunction)
      return Intermech.Consts.CurrentUserFunction;
    return userID is 0L ? "Не определен" : this.GetObjectCaption(userID);
  }

  /// <summary>Наименование типа объектов</summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public virtual string GetObjectTypeCaption(object value) => Convert.ToString(value);

  public virtual string GetObjectCaption(object value) => Convert.ToString(value);

  public virtual string GetLifecycleStepCaption(object value) => Convert.ToString(value);

  public virtual string GetLifecycleLevelCaption(object value) => Convert.ToString(value);

  public virtual string GetRelationTypeCaption(object value) => Convert.ToString(value);

  public virtual string GetSubjectAreaCaption(object value) => Convert.ToString(value);

  protected virtual string GetInputObjectAttributeCaption(InputObjectAttribute attribute)
  {
    return string.Empty;
  }

  public virtual bool ChoiseRelationType(ref object relationType) => false;

  public abstract List<ConditionAttributeInfo> GetAttributesForObjectTypes(int[] objTypes);

  public abstract List<ConditionAttributeInfo> GetObligatoryAttributes(
    AttributeSourceTypes sourceType);

  public abstract bool SelectDialog(
    ref object value,
    SelectionParameterTypes type,
    object addInfo,
    int attrID,
    int[] selection4Types);

  public abstract int GetObjectType4ObjectLink(int attributeID);

  public abstract string GenerateConditionCaption(
    ConditionStructure conditionStructure,
    string value1,
    string value2);

  protected bool SelectSitesDialog(ref object value)
  {
    string str = Convert.ToString(value);
    ISitesCacheService customService = (ISitesCacheService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISitesCacheService));
    object aObject = (object) 0L;
    if (str != string.Empty)
    {
      SiteInfo site = customService.GetSite(str[0]);
      if (site != null)
        aObject = (object) site.ID;
    }
    if (ValueRelationSelector.SelectObject(ref aObject, -17, (int[]) null, (object) MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeSites), false))
    {
      SiteInfo site = customService.GetSite((long) aObject);
      if (site != null)
      {
        value = (object) site.Code;
        return true;
      }
    }
    return false;
  }

  public abstract int GetObjectTypeID(Guid objectTypeGuid);

  public virtual void GetDateAttributeFormat(
    int attributeID,
    int[] objectTypeIDs,
    out DateTimePickerFormat format,
    out string formatString)
  {
    format = DateTimePickerFormat.Custom;
    formatString = "dd.MM.yyyy HH:mm:ss";
  }

  public virtual bool IsUserObjectID(long objectID) => false;

  public virtual MultiValueModes GetAttributeMultiValueMode(object attributeID)
  {
    return MultiValueModes.SingleValue;
  }

  public abstract RelationalOperators[] GetEnableRelationalOperators(
    FieldTypes fieldType,
    int attributeID);
}

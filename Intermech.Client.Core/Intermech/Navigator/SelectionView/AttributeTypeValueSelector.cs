
// Type: Intermech.Navigator.SelectionView.AttributeTypeValueSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.PropertyEditors;
using System;


namespace Intermech.Navigator.SelectionView;

public class AttributeTypeValueSelector
{
  public static IDBAttributeType GetAttributeType(IUserSession session, object value)
  {
    IDBAttributeType attributeType = (IDBAttributeType) null;
    switch (value)
    {
      case int _:
        attributeType = session.GetAttributeType(Convert.ToInt32(value));
        break;
      case Guid guid when !guid.Equals(Guid.Empty):
        attributeType = session.GetAttributeType((Guid) value);
        break;
      case string _ when (string) value != string.Empty:
        attributeType = session.GetAttributeType(Convert.ToString(value));
        break;
    }
    return attributeType;
  }

  /// <summary>
  /// Получение типа данных параметра условия выборки по типу атрибута и назначение
  /// для данного параметра соответствующего обработчика изменения значения
  /// </summary>
  /// <param name="idbAttrType">Тип атрибута для которого задается условие выборки</param>
  /// <param name="sysAttrSel">Переменная в которую будет передан обработчик значения</param>
  /// <returns>Тип данных параметра условия выборки </returns>
  public static SelectionParameterTypes GetAttType(
    IDBAttributeType idbAttrType,
    out SystemAttributeSelect sysAttrSel)
  {
    sysAttrSel = (SystemAttributeSelect) null;
    if ((ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(idbAttrType.AttributeID) != null)
    {
      sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectHandler);
      return SelectionParameterTypes.sptHandler;
    }
    SelectionParameterTypes nodeValueType = SelectionParameter.GetNodeValueType(idbAttrType);
    switch (nodeValueType)
    {
      case SelectionParameterTypes.sptObject:
      case SelectionParameterTypes.sptCheckOutBy:
        if (idbAttrType.AttributeType == FieldTypes.ftSystem)
        {
          if (idbAttrType.AttributeID == -14)
            sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectProject);
          if (idbAttrType.AttributeID == -8 || idbAttrType.AttributeID == -6)
          {
            sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectUser);
            break;
          }
          break;
        }
        break;
      case SelectionParameterTypes.sptObjectType:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectObjectType);
        break;
      case SelectionParameterTypes.sptLifecycleLevel:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectLifecycleLevel);
        break;
      case SelectionParameterTypes.sptSubjectArea:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectSubjectArea);
        break;
      case SelectionParameterTypes.sptLinkType:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectLinkType);
        break;
      case SelectionParameterTypes.sptLifecycleStep:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectLifeCycleStep);
        break;
      case SelectionParameterTypes.sptGlobalID:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectVersionsGuid);
        break;
      case SelectionParameterTypes.sptMeasured:
        sysAttrSel = new SystemAttributeSelect(ValueRelationSelector.SelectMeasure);
        break;
    }
    return nodeValueType;
  }
}

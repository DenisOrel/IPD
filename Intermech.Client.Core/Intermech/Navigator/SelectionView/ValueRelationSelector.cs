
// Type: Intermech.Navigator.SelectionView.ValueRelationSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.LifeCycle;
using Intermech.PropertyEditors;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для организации вызова диалогов выбора через статические методы
/// </summary>
public static class ValueRelationSelector
{
  /// <summary>Выбор объекта</summary>
  public static bool SelectObject(
    ref object aObject,
    int attrID,
    int[] selection4Types,
    object AddInfo,
    bool multiSelect)
  {
    ISelectObjectDialogService service1 = (ISelectObjectDialogService) ServicesManager.GetService(typeof (ISelectObjectDialogService));
    int num1 = -1;
    if (AddInfo != null && (AddInfo.GetType() == typeof (long) || AddInfo.GetType() == typeof (int)) && Convert.ToInt32(AddInfo) > 0)
      num1 = Convert.ToInt32(AddInfo);
    List<int> intList = (List<int>) null;
    if (AddInfo != null && AddInfo is IList<int>)
      intList = new List<int>((IEnumerable<int>) AddInfo);
    if (selection4Types != null && selection4Types.Length == 1 && ServicesManager.GetService(typeof (IImbaseFilterSelector)) is IImbaseFilterSelector service2)
    {
      long num2 = aObject != null ? (long) aObject : 0L;
      ImbaseFilterEditor imbaseFilterEditor = AttributeValuesEditor.ImbaseAttributesHandle((IUserSession) null, attrID, selection4Types[0], num2);
      if (imbaseFilterEditor != null)
      {
        ObjectPropertyClass objectPropertyClass = (ObjectPropertyClass) imbaseFilterEditor.EditValue(service2, attrID, num2);
        if (objectPropertyClass.ObjectID == 0L)
          return false;
        aObject = (object) objectPropertyClass.ObjectID;
        return true;
      }
    }
    IDescriptor rootDescriptor = num1 == -1 ? (intList == null ? (IDescriptor) new ObjectTypesNodeDescriptor() : (IDescriptor) new ObjectTypesDescriptor(intList.ToArray(), "Допустимые типы объектов")) : service1.GetDescriptor(num1);
    SelectionOptions options = SelectionOptions.Default;
    if (!multiSelect)
      options |= SelectionOptions.DisableMultiselect;
    int[] enableTypes = (int[]) null;
    if (num1 != -1)
      enableTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(num1).ToArray();
    if (SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_22"), rootDescriptor, typeof (IDBObjectID), options, enableTypes) is IDBObjectID[] array)
      aObject = array.Length != 1 ? (object) Array.ConvertAll<IDBObjectID, long>(array, (Converter<IDBObjectID, long>) (x => x.Value)) : (object) array[0].Value;
    return array != null;
  }

  public static bool SelectHandler(ref object aObject, object AddInfo)
  {
    int int32 = Convert.ToInt32(AddInfo);
    IAttributePropertyDescriber describer = (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(int32);
    if (describer != null)
    {
      UITypeEditor descriptorEditor = describer.GetPropDescriptorEditor(int32) as UITypeEditor;
      if (descriptorEditor.GetEditStyle() == UITypeEditorEditStyle.Modal)
      {
        using (ServiceContainer provider = new ServiceContainer())
        {
          object propertyValue = descriptorEditor.EditValue((System.IServiceProvider) provider, aObject);
          aObject = describer.GetAttributeValue((IElementInfo) null, int32, propertyValue);
          return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Выбор шага жизненного цикла (выбирается в контексте типа объекта
  /// сначала выбрать тип объекта, а для типа показываются возможные шаги)
  /// </summary>
  public static bool SelectLifeCycleStep(ref object aObject, object AddInfo)
  {
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1522"), LocalizationHolder.rm.GetString("Client.Core_1523"), (IDescriptor) new LCSchemesObjTypesDescriptor(), typeof (IDBLCStepID), SelectionOptions.SelectOtherNodes | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length == 0 || !(objArray[0] is IDBLCStepID dblcStepId))
      return false;
    aObject = (object) dblcStepId.LCStepID;
    return true;
  }

  public static bool SelectLifeCycleStep(ref object aObject)
  {
    return ValueRelationSelector.SelectLifeCycleStep(ref aObject, (object) null);
  }

  /// <summary>
  /// Выбор идентификатора пользователя (который взял на изменение объект или владелец объекта)
  /// </summary>
  public static bool SelectUser(ref object aObject, object AddInfo)
  {
    bool flag1 = false;
    bool flag2 = AddInfo is bool flag3 && flag3;
    if ((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"), false) != null)
    {
      SelectionOptions options = SelectionOptions.SelectObjects;
      if (!flag2)
        options |= SelectionOptions.DisableMultiselect;
      if (SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_398"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBObjectID), options) is IDBObjectID[] dbObjectIdArray)
      {
        if (!flag2)
        {
          aObject = (object) dbObjectIdArray[0].Value;
        }
        else
        {
          aObject = (object) new ArrayList();
          foreach (IDBObjectID dbObjectId in dbObjectIdArray)
            ((ArrayList) aObject).Add((object) dbObjectId.Value);
        }
      }
      flag1 = true;
    }
    return flag1;
  }

  /// <summary>Выбор идентификатора проекта</summary>
  public static bool SelectProject(ref object aObject, object AddInfo)
  {
    bool flag = false;
    aObject = (object) 0;
    DescriptorCollection descriptors = new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545"))
    };
    if (SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_704"), (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_283"), descriptors), typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect) is IDBObjectID[] dbObjectIdArray && dbObjectIdArray.Length != 0)
    {
      aObject = (object) dbObjectIdArray[0].Value;
      flag = true;
    }
    return flag;
  }

  public static bool SelectUser(ref object aObject)
  {
    return ValueRelationSelector.SelectUser(ref aObject, (object) null);
  }

  /// <summary>Выбор типа объектов</summary>
  public static bool SelectObjectType(ref object aObject, object AddInfo)
  {
    bool aMultiSelect = false;
    if (AddInfo != null && AddInfo is bool)
      aMultiSelect = Convert.ToBoolean(AddInfo);
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_392"), typeof (ObjectTypeFolder), aMultiSelect);
    if (aObject is ArrayList)
      selectorForm.InitSelectionAsType(aObject as ArrayList, (ArrayList) null);
    else if (aObject is int)
    {
      ArrayList idList = new ArrayList()
      {
        (object) Convert.ToInt32(aObject)
      };
      selectorForm.InitSelectionAsType(idList, (ArrayList) null);
    }
    int num = selectorForm == null ? 0 : (selectorForm.ShowDialog() == DialogResult.OK ? 1 : 0);
    if (num != 0)
    {
      if (aMultiSelect)
      {
        aObject = (object) selectorForm.IDList;
        return num != 0;
      }
      if (selectorForm.IDList.Count > 0)
      {
        aObject = selectorForm.IDList[0];
        return num != 0;
      }
      aObject = (object) null;
      return num != 0;
    }
    aObject = (object) 0;
    return num != 0;
  }

  public static bool SelectObjectType(ref object aObject)
  {
    return ValueRelationSelector.SelectObjectType(ref aObject, (object) null);
  }

  /// <summary>Выбор уровня продвижения объекта</summary>
  public static bool SelectLifecycleLevel(ref object aObject, object AddInfo)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (LevelsFolder), LocalizationHolder.rm.GetString("Client.Core_399"), typeof (LevelFolder), false);
    bool flag = selectorForm != null && selectorForm.ShowDialog() == DialogResult.OK;
    aObject = flag ? (selectorForm.IDList.Count > 0 ? selectorForm.IDList[0] : (object) null) : (object) 0;
    return flag;
  }

  public static bool SelectLifecycleLevel(ref object aObject)
  {
    return ValueRelationSelector.SelectLifecycleLevel(ref aObject, (object) null);
  }

  /// <summary>Выбор предметной области</summary>
  public static bool SelectSubjectArea(ref object aObject, object AddInfo)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (AreasFolder), LocalizationHolder.rm.GetString("Client.Core_400"), typeof (AreaFolder), false);
    bool flag = selectorForm != null && selectorForm.ShowDialog() == DialogResult.OK;
    aObject = flag ? (selectorForm.IDList.Count > 0 ? selectorForm.IDList[0] : (object) null) : (object) 0;
    return flag;
  }

  public static bool SelectSubjectArea(ref object aObject)
  {
    return ValueRelationSelector.SelectSubjectArea(ref aObject, (object) null);
  }

  /// <summary>Выбор глобального идентификатора версии объекта</summary>
  public static bool SelectVersionsGuid(ref object aObject, object AddInfo)
  {
    GuidSelector guidSelector = aObject == null || !(aObject.GetType() == typeof (Guid)) ? new GuidSelector(Guid.Empty) : new GuidSelector((Guid) aObject);
    bool flag = guidSelector != null && guidSelector.ShowDialog() == DialogResult.OK;
    aObject = (object) (flag ? guidSelector.ResultGuid : Guid.Empty);
    return flag;
  }

  public static bool SelectVersionsGuid(ref object aObject)
  {
    return ValueRelationSelector.SelectVersionsGuid(ref aObject, (object) null);
  }

  /// <summary>Выбор типа связи</summary>
  public static bool SelectLinkType(ref object aObject, object AddInfo)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("Client.Core_401"), typeof (RelationTypeFolder), false);
    bool flag = selectorForm != null && selectorForm.ShowDialog() == DialogResult.OK;
    aObject = flag ? (selectorForm.IDList.Count > 0 ? selectorForm.IDList[0] : (object) null) : (object) 0;
    return flag;
  }

  public static bool SelectLinkType(ref object aObject)
  {
    return ValueRelationSelector.SelectLinkType(ref aObject, (object) null);
  }

  /// <summary>Выбор единиц измерения</summary>
  public static bool SelectMeasure(ref object aObject, object AddInfo)
  {
    long num1 = AddInfo != null ? Convert.ToInt64(AddInfo) : -1L;
    if (aObject == null || aObject.GetType() != typeof (MeasuredValue))
    {
      foreach (MeasureDescriptor measure in MeasureHelper.Measures)
      {
        if (measure.PhysicalQuantityID == num1 || num1 < 0L)
        {
          aObject = (object) new MeasuredValue(0.0, MeasureHelper.FindBaseValue(measure).MeasureID);
          break;
        }
      }
    }
    if (aObject == null)
      return false;
    MeasuredValue aMeasureValue = (MeasuredValue) aObject;
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(aMeasureValue.MeasureID);
    ArrayList arrayList = new ArrayList();
    foreach (MeasureDescriptor measure in MeasureHelper.Measures)
    {
      if (measure.PhysicalQuantityID == descriptor.PhysicalQuantityID || num1 < 0L)
        arrayList.Add((object) measure);
    }
    MeasureDescriptor[] aMeasureDescriptorList = new MeasureDescriptor[arrayList.Count];
    for (int index = 0; index < arrayList.Count; ++index)
      aMeasureDescriptorList[index] = (MeasureDescriptor) arrayList[index];
    int num2 = new MeasureForm().ExecuteDialog(ref aMeasureValue, aMeasureDescriptorList) == DialogResult.OK ? 1 : 0;
    aObject = (object) aMeasureValue;
    return num2 != 0;
  }
}

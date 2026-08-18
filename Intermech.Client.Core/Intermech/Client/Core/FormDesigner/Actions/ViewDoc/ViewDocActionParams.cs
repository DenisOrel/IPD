
// Type: Intermech.Client.Core.FormDesigner.Actions.ViewDoc.ViewDocActionParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.FormDesigner.External.Classes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.Serialization;


namespace Intermech.Client.Core.FormDesigner.Actions.ViewDoc;

/// <summary>Параметры действия "Просмотр документа"</summary>
[TypeConverter(typeof (ActionTypeConverter))]
[Serializable]
internal class ViewDocActionParams : IFormDesignerActionParams, ISerializable
{
  private const string ParamNameDocumentGuid = "DocumentGuid";

  /// <summary>Guid версии объекта (документа)</summary>
  [DefaultValue(typeof (Guid), "00000000-0000-0000-0000-000000000000")]
  [CustomDisplayName("ClientCore_Document")]
  [CustomDescription("ClientCore_DocSelectedForView")]
  [TypeConverter(typeof (Guid2ObjectCaptionConverter))]
  [Editor(typeof (ViewDocActionParams.DocumentGuidSelectEditor), typeof (UITypeEditor))]
  public Guid DocumentGuid { get; set; }

  /// <summary>simplest constructor</summary>
  public ViewDocActionParams() => this.DocumentGuid = Guid.Empty;

  /// <summary>Populates a SerializationInfo with the data needed to serialize the target object.</summary>
  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("DocumentGuid", (object) this.DocumentGuid);
  }

  /// <summary>
  /// The special constructor is used to deserialize values.
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public ViewDocActionParams(SerializationInfo info, StreamingContext context)
  {
    this.DocumentGuid = (Guid) info.GetValue(nameof (DocumentGuid), typeof (Guid));
  }

  /// <summary>Action component (control).</summary>
  [Browsable(false)]
  public object Component { get; set; }

  /// <summary>Редактор выбора значения DocumentGuid</summary>
  public class DocumentGuidSelectEditor : UITypeEditor
  {
    /// <summary>
    /// Формирование и возврат списка (в фильтре допустимых типов nodesFilter) всех допустимых типов объектов, которые могут отображаться
    /// в иерархии дерева типов объектов, при выборе пользователем объекта в качестве документа для просмотра (для просмотра его файла, если это возможно).
    /// Критерии отбора: тип объекта или какой-то из его дочерних подтипов на любом уровне вложенности имеет атрибут "Файл"
    /// или может иметь атрибуты любого типа.
    /// Так же формирует и возвращает список допустимых корневых типов верхнего уровня (topTypesList) на основе вышеописанных для nodesFilter условий,
    /// который необходим для создания дескриптора отображаемых узлов в дереве типов.
    /// </summary>
    private static (IEnumerable<int> topTypesList, IObjectTypeNodeFilter nodesFilter) GetSelectableObjTypesData()
    {
      int fileAttributeId = MetaDataHelper.GetAttributeID((object) new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      List<int> intList = new List<int>();
      IObjectTypeNodeFilter nodesFilter = (IObjectTypeNodeFilter) new ObjectTypeNodeFilter();
      foreach (IMSObjectType imsObjectType in MetaDataHelper.GetObjectTypesList().Where<IMSObjectType>((Func<IMSObjectType, bool>) (objTypeInfo => (objTypeInfo.AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objTypeInfo.ObjectTypeID, fileAttributeId) != null) && !nodesFilter.EnabledObjectTypes.Contains(objTypeInfo.ObjectTypeID))))
      {
        int childTypeID = imsObjectType.ObjectTypeID;
        nodesFilter.EnabledObjectTypes.Add(childTypeID);
        while (true)
        {
          int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(childTypeID);
          if (objectTypeParentId != -1)
          {
            if (!nodesFilter.EnabledObjectTypes.Contains(objectTypeParentId))
              nodesFilter.EnabledObjectTypes.Add(childTypeID = objectTypeParentId);
            else
              break;
          }
          else
            goto label_7;
        }
        childTypeID = -1;
label_7:
        if (childTypeID != -1)
          intList.Add(childTypeID);
      }
      return ((IEnumerable<int>) intList, nodesFilter);
    }

    /// <summary>
    /// Создание дескриптора дерева узлов допустимых типов объектов.
    /// </summary>
    /// <param name="topTypesList"></param>
    /// <returns>Дескриптор дерева узлов допустимых типов объектов</returns>
    private static IDescriptor GetSelectionDescriptor(IEnumerable<int> topTypesList)
    {
      return Intermech.Navigator.DBObjectTypes.Descriptor.CreateComposition(topTypesList, LocalizationHolder.rm.GetString("Client.Core_283"));
    }

    /// <summary>
    /// Создание кастомного контейнера сервисов содержащего сервис IObjectTypeNodeFilter,
    /// который предоставляет информацию о допустимых типах объектов.
    /// </summary>
    /// <param name="nodesFilter">Фильтр с информацию о допустимых и/или запрещённых типах объектов.</param>
    /// <returns>Контейнер сервисов с сервисом фильтра допустимых типов объектов</returns>
    private static IServiceContainer GetNodesContext(IObjectTypeNodeFilter nodesFilter)
    {
      ServiceContainer nodesContext = new ServiceContainer();
      nodesContext.AddService(typeof (IObjectTypeNodeFilter), (object) nodesFilter);
      return (IServiceContainer) nodesContext;
    }

    /// <summary>The editor style is always modal.</summary>
    /// <param name="context"> Not used </param>
    /// <returns> Always == Modal Window style </returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    /// <summary>Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
    /// сервиса (при аргументе throwExceptionIfNotFound == true)</exception>
    /// <exception cref="T:System.ApplicationException">Не удалось получить информацию о выбранном пользователем объекте</exception>
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      (IEnumerable<int> topTypesList, IObjectTypeNodeFilter nodesFilter) = ViewDocActionParams.DocumentGuidSelectEditor.GetSelectableObjTypesData();
      long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.GetString("Client.Core_22"), string.Empty, ViewDocActionParams.DocumentGuidSelectEditor.GetSelectionDescriptor(topTypesList), (IServiceProvider) ViewDocActionParams.DocumentGuidSelectEditor.GetNodesContext(nodesFilter), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect);
      if (numArray != null && numArray.Length != 0)
      {
        QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(numArray[0]);
        value = !objectInfo.Empty ? (object) objectInfo.VersionGuid : throw new ApplicationException(LocalizationHolder.GetString("FormDesigner_Fail_Get_Obj_Info"));
      }
      return value;
    }
  }
}

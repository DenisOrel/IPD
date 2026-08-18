
// Type: Intermech.PropertyEditors.MaterialEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.PropertyEditors;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Обработчик изменения значений атрибута Материал.</summary>
public class MaterialEditor : ObjectEditor
{
  /// <summary>Ид. типа атрибута</summary>
  private readonly int _attributeId;

  public MaterialEditor(int lAttributeId)
    : base(lAttributeId)
  {
    this._attributeId = lAttributeId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    IElementInfo elementInfo = value is AttributablePropertyClass attributablePropertyClass ? attributablePropertyClass.ElementInfo : (IElementInfo) null;
    object obj = value;
    long result = 0;
    if (attributablePropertyClass != null)
      result = attributablePropertyClass.ObjectID;
    else if (!long.TryParse(Convert.ToString(value), out result))
      result = 0L;
    DescriptorCollection descriptorCollection = new DescriptorCollection();
    DescriptorCollection typesDescriptors = GetPossibleDescriptors.PossibleTypesDescriptors;
    if (typesDescriptors != null)
      descriptorCollection.Add((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_283"), typesDescriptors));
    if (ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service1)
    {
      int objectTypeId = -1;
      if (elementInfo != null && elementInfo.ElementKind == AttributableElements.Object)
      {
        if (elementInfo is IDBObjectTypeID dbObjectTypeId)
        {
          objectTypeId = dbObjectTypeId.Value;
        }
        else
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            objectTypeId = sessionKeeper.Session.GetObjectInfo(elementInfo.ElementIdentifier).ObjectTypeID;
        }
      }
      descriptorCollection.Add(service1.GetImbaseDescriptor(objectTypeId, attributablePropertyClass != null ? attributablePropertyClass.AttributeId : MetaDataHelper.GetAttributeID((object) "cad0038c-306c-11d8-b4e9-00304f19f545")));
    }
    long num1;
    if (ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service2)
    {
      num1 = service2.SelectMaterial(LocalizationHolder.rm.GetString("Client.Core_1229"), string.Empty, (object) descriptorCollection, -1, result);
    }
    else
    {
      if (service1 == null)
        return base.EditValue(context, provider, value);
      long num2 = service1.SelectFromCatalog(LocalizationHolder.rm.GetString("Client.Core_1229"), string.Empty, (object) descriptorCollection, -1, result);
      num1 = num2 != -1L ? num2 : result;
    }
    if (num1 != result)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num1 == 0L ? result : num1);
        obj = (object) new AttributablePropertyClass(attributablePropertyClass?.ElementInfo, attributablePropertyClass != null ? attributablePropertyClass.AttributeId : this._attributeId, objectInfo.ObjectID, objectInfo.Caption);
      }
    }
    return obj;
  }
}


// Type: Intermech.PropertyEditors.ObjectsDropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
internal class ObjectsDropDownListEditor : DropDownListEditor
{
  private ObjectsTypeConverter objTypeConverter;
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getObjList;
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  public ObjectsDropDownListEditor(
    IPossibleValuesHolder aIPossibleValuesHolder,
    bool _objectVersionProcessed = true)
  {
    this.objectVersionProcessed = _objectVersionProcessed;
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.objTypeConverter = new ObjectsTypeConverter(aIPossibleValuesHolder, true, this.objectVersionProcessed);
  }

  public ObjectsDropDownListEditor(
    IPossibleValuesHolder aIPossibleValuesHolder,
    bool valCanNull,
    bool _objectVersionProcessed = true)
  {
    this.objectVersionProcessed = _objectVersionProcessed;
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
    this.objTypeConverter = new ObjectsTypeConverter(aIPossibleValuesHolder, valCanNull, this.objectVersionProcessed);
  }

  public ObjectsDropDownListEditor(
    EventsHolder.GetListDelegate aGetObjList,
    bool _objectVersionProcessed = true)
  {
    this.objectVersionProcessed = _objectVersionProcessed;
    this.getObjList = aGetObjList;
    this.objTypeConverter = new ObjectsTypeConverter(aGetObjList, true, this.objectVersionProcessed);
  }

  public ObjectsDropDownListEditor(
    EventsHolder.GetListDelegate aGetObjList,
    bool valCanNull,
    bool _objectVersionProcessed = true)
  {
    this.objectVersionProcessed = _objectVersionProcessed;
    this.getObjList = aGetObjList;
    this.objTypeConverter = new ObjectsTypeConverter(aGetObjList, valCanNull, this.objectVersionProcessed);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this.objTypeConverter.GetStandardValuesCustomList(context, args);
  }
}

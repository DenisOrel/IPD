
// Type: Intermech.PropertyEditors.MeasuredIdReceiver
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class MeasuredIdReceiver
{
  private int attributeId;
  private long elementId;
  private AttributableElements attributableElements;
  private int typeId = -1;

  /// <summary>Конструктор.</summary>
  /// <param name="info"></param>
  /// <param name="aAttributeId"></param>
  public MeasuredIdReceiver(IElementInfo info, int aAttributeId)
  {
    this.attributeId = aAttributeId;
    this.elementId = info.ElementIdentifier;
    this.attributableElements = info.ElementKind;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  public long GetDefaultMeasureID(object sender, params object[] args)
  {
    long defaultMeasureId = -1;
    if (this.attributeId != 0)
    {
      if (this.typeId == -1)
        this.typeId = ClientCommons.GetElementType(this.elementId, this.attributableElements);
      if (this.typeId != -1)
      {
        IDBAttributableTypeInfo attributableType = ClientCommons.GetAttributableType(this.typeId, this.attributableElements);
        if (attributableType != null)
        {
          IDBAttributeTypeInfo4 attributeById = attributableType.Attributes.GetAttributeByID(this.attributeId);
          if (attributeById != null)
          {
            if (attributeById is IDBMeasureAttributeType measureAttributeType)
              defaultMeasureId = measureAttributeType.DefaultMeasureID;
          }
          else
          {
            IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId);
            if (attributeType != null && attributeType.AttributeType == FieldTypes.ftMeasured && attributeType.SizeType != -1L)
              defaultMeasureId = MeasureHelper.GetBaseMeasureID(attributeType.SizeType);
          }
        }
      }
    }
    return defaultMeasureId;
  }
}

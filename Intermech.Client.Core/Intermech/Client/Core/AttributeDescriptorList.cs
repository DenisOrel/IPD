
// Type: Intermech.Client.Core.AttributeDescriptorList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Client.Core;

/// <summary> Список дескрипторов атрибутов в списке выбора атрибутов </summary>
public class AttributeDescriptorList : ArrayList
{
  public AttributeDescriptorList()
  {
  }

  public AttributeDescriptorList(int capacity)
    : base(capacity)
  {
  }

  /// <summary> Получить дескриптор атрибута по его идентификатору </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <returns> Дескриптор атрибута </returns>
  public AttributeDescriptor GetByID(int attributeID)
  {
    foreach (AttributeDescriptor byId in (ArrayList) this)
    {
      if (byId.AttributeID == attributeID)
        return byId;
    }
    return (AttributeDescriptor) null;
  }

  /// <summary> Получить индекс дескриптора атрибута по его идентификатору </summary>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <returns> Дескриптор атрибута </returns>
  public int IndexOfID(int attributeID)
  {
    int num = 0;
    foreach (AttributeDescriptor attributeDescriptor in (ArrayList) this)
    {
      if (attributeDescriptor.AttributeID == attributeID)
        return num;
      ++num;
    }
    return -1;
  }

  public AttributeDescriptor this[int index]
  {
    get => (AttributeDescriptor) base[index];
    set => this[index] = (object) value;
  }
}

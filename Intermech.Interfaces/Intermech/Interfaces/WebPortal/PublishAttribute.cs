
// Type: Intermech.Interfaces.WebPortal.PublishAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Сруктура хранящая информацию об атрибуте опубликованного объекта и его значениях
    /// </summary>
    [Serializable]
    public class PublishAttribute
    {
      /// <summary>Информация об атрибуте</summary>
      public AttributeInfo Info;
      /// <summary>Значения</summary>
      public AttributeValue[] Values;
      /// <summary>Категория опубликованного атрибута</summary>
      public PublishAttributeCategory Category;

      public PublishAttribute()
      {
      }

      public PublishAttribute(
        AttributeInfo info,
        AttributeValue[] values,
        PublishAttributeCategory category)
      {
        this.Info = info;
        this.Values = values;
        this.Category = category;
      }
    }
}

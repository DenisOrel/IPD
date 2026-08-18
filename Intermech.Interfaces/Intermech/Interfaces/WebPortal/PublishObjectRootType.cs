
// Type: Intermech.Interfaces.WebPortal.PublishObjectRootType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Корневые типы опубликованных объектов</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Interfaces_Portal_8")]
    [Serializable]
    public enum PublishObjectRootType
    {
      /// <summary>
      /// 
      /// </summary>
      [CustomDescription("Interfaces_Portal_9")] rtUnknown,
      /// <summary>
      /// 
      /// </summary>
      [CustomDescription("Interfaces_Portal_10")] rtArticle,
      /// <summary>
      /// 
      /// </summary>
      [CustomDescription("Interfaces_Portal_11")] rtDocument,
    }
}

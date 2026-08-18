
// Type: Intermech.Interfaces.WebPortal.PublishOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Набор статических методов для работы с опциями публикации
    /// </summary>
    public static class PublishOptionsHelper
    {
      public static bool NormalPublish(IncludeTypes includeType)
      {
        return PublishOptionsHelper.IsEnumAttributeEqual(includeType, UnitPublishType.Publish);
      }

      public static bool DummyPublish(IncludeTypes includeType)
      {
        return PublishOptionsHelper.IsEnumAttributeEqual(includeType, UnitPublishType.Dummy);
      }

      public static bool ForbiddenForPublish(IncludeTypes includeType)
      {
        return PublishOptionsHelper.IsEnumAttributeEqual(includeType, UnitPublishType.Forbidden);
      }

      private static bool IsEnumAttributeEqual(IncludeTypes includeType, UnitPublishType publishType)
      {
        return ((EnablePublishAttribute[]) includeType.GetType().GetField(includeType.ToString()).GetCustomAttributes<EnablePublishAttribute>())[0].PublishType == publishType;
      }

      public static ExtendedPublishOptions Deserialize(IDBObject obj)
      {
        IDBAttribute attributeByGuid = obj.GetAttributeByGuid(PortalConsts.attributePublishOptions);
        if (attributeByGuid == null || attributeByGuid.IsNull)
          return (ExtendedPublishOptions) null;
        using (ImChunkedStream imChunkedStream = new ImChunkedStream())
        {
          new BlobProcReader(attributeByGuid, 0, (Stream) imChunkedStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
          return PublishOptionsHelper.DeserializeOptions(new BinaryFormatter().Deserialize((Stream) imChunkedStream));
        }
      }

      public static ExtendedPublishOptions Deserialize(byte[] data)
      {
        using (ImChunkedStream serializationStream = new ImChunkedStream())
        {
          serializationStream.Write(data, 0, data.Length);
          serializationStream.Position = 0L;
          return PublishOptionsHelper.DeserializeOptions(new BinaryFormatter().Deserialize((Stream) serializationStream));
        }
      }

      private static ExtendedPublishOptions DeserializeOptions(object value)
      {
        switch (value)
        {
          case ExtendedPublishOptions extendedPublishOptions:
            return extendedPublishOptions;
          case PublishOptions options:
            return ExtendedPublishOptions.Create(options);
          default:
            return (ExtendedPublishOptions) null;
        }
      }

      public static byte[] Serialize(ExtendedPublishOptions options)
      {
        using (ImChunkedStream serializationStream = new ImChunkedStream())
        {
          new BinaryFormatter().Serialize((Stream) serializationStream, (object) options);
          return serializationStream.ToArray();
        }
      }
    }
}

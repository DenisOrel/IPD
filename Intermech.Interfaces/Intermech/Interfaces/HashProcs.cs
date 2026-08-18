
// Type: Intermech.Interfaces.HashProcs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Interfaces
{
    public class HashProcs
    {
      /// <summary>Возвращает версию хэша без учета спецфлагов</summary>
      /// <param name="hashVersion"></param>
      /// <returns></returns>
      public static int SimpleVersion(int hashVersion) => hashVersion & 16777215 /*0xFFFFFF*/;

      /// <summary>Проверка флага совместимой подписи (Search)</summary>
      /// <param name="hashVersion"></param>
      /// <returns></returns>
      public static bool IsCompatibleSign(int hashVersion)
      {
        return (hashVersion & 1073741824 /*0x40000000*/) != 0;
      }

      public static int ClearCompatibleSign(int hashVersion)
      {
        return hashVersion & -1073741825 /*0xBFFFFFFF*/;
      }

      /// <summary>
      /// Формирует в поток информацию для подписывания/проверки объекта
      /// </summary>
      /// <param name="siStream">поток, куда ведется запись;
      /// для криптоподписей IPS здесь будет информация по метаданным объекта, в том числе тела файлов. порядок метаданных - в hashContent;
      /// для подписей, совместимых с Search, здесь будут только тела файлов атрибута Файл. порядок файлов - в fileAttrOrder</param>
      /// <param name="customObject">объект</param>
      /// <param name="hashVersion">версия подписи; при взведенном флаге SignFlags.CompatibleSignFlag - совместимая подпись (с Search)</param>
      /// <param name="setContent">при подписывании true - тогда заполняется hashContent и fileAttrOrder; при проверке false - используется содержимое hashContent, при этом fileAttrOrder не имеет значения</param>
      /// <param name="hashContent">порядок метаданных в хэше: при setContent=true заполняется порядок учета метаданных; при setContent=false учитывается порядок при формировании выходного потока</param>
      /// <returns>версия хэша может быть скорректирована, например, для объектов без документов, которые нужно подписать совместимой с Search подписью</returns>
      public static int ExtractSignInfo(
        Stream siStream,
        IDBObject idbObject,
        int hashVersion,
        bool setContent,
        IHashContent hashContent)
      {
        if (setContent)
          hashContent.Clear(HashProcs.IsCompatibleSign(hashVersion));
        if (HashProcs.SimpleVersion(hashVersion) < 4)
          return hashVersion;
        using (new RemoteLock((object) idbObject))
        {
          if (HashProcs.IsCompatibleSign(hashVersion))
          {
            bool flag1 = false;
            IDBAttribute attributeByGuid = idbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid == null || (attributeByGuid as IDBAttributeEx).IsNull)
            {
              flag1 = true;
            }
            else
            {
              using (new RemoteLock((object) attributeByGuid))
              {
                long position = siStream.Position;
                for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
                {
                  attributeByGuid.Index = index;
                  BlobInformation blobInformation = (attributeByGuid as IBlobReader).OpenBlob(-1);
                  if (blobInformation.FileType == FileTypes.ftNormal)
                  {
                    bool flag2 = blobInformation.RealFileSize <= Consts.BlobInMemoryOperationalLimit;
                    using (IsolatedStorageFile isolatedStorageFile = !flag2 ? HashProcs.GetIsolatedStorageFile() : (IsolatedStorageFile) null)
                    {
                      string str = Guid.NewGuid().ToString();
                      try
                      {
                        using (Stream aDestStream = !flag2 ? (Stream) new IsolatedStorageFileStream(str, FileMode.Create, FileAccess.ReadWrite, isolatedStorageFile) : (Stream) new MemoryStream())
                        {
                          new BlobProcReader(attributeByGuid, 0, aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(attributeByGuid.Session);
                          aDestStream.Position = 0L;
                          aDestStream.CopyTo(siStream);
                        }
                      }
                      finally
                      {
                        if (!flag2)
                        {
                          try
                          {
                            isolatedStorageFile.DeleteFile(str);
                          }
                          catch
                          {
                          }
                        }
                      }
                    }
                    if (setContent)
                      hashContent.Files.Add(blobInformation.FileName);
                  }
                }
                if (setContent && siStream.Position - position == 0L)
                  hashContent.Files.Clear();
                if (hashContent.Files.Count == 0 & setContent)
                  flag1 = true;
              }
            }
            if (!flag1)
              return hashVersion;
            hashContent.Compatible = false;
            hashVersion = HashProcs.ClearCompatibleSign(hashVersion);
          }
          HashProcs.AddAttributableAttributesToHashStream((IDBAttributable) idbObject, siStream, hashVersion, setContent, hashContent.Attributes);
          if (setContent)
          {
            DataTable applicabilitiesList = idbObject.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, idbObject.ObjectType);
            List<int> intList = new List<int>();
            if (applicabilitiesList != null)
            {
              if (applicabilitiesList.Rows.Count > 0)
              {
                foreach (DataRow row1 in (InternalDataCollectionBase) applicabilitiesList.Rows)
                {
                  if (Convert.ToBoolean(row1["F_CONTENT"]))
                  {
                    int int32 = Convert.ToInt32(row1["F_RELATION_TYPE"]);
                    if (intList.IndexOf(int32) == -1)
                    {
                      intList.Add(int32);
                      IDBRelationType relationType = idbObject.Session.GetRelationType(int32);
                      IDBRelationCollection relationCollection = idbObject.Session.GetRelationCollection(relationType.RelationType);
                      relationCollection.LocalTypesMode = true;
                      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[4]
                      {
                        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
                        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
                        (object) ObligatoryObjectAttributes.F_OBJ_GUID,
                        (object) ObligatoryObjectAttributes.F_PRJ_GUID
                      }, new object[1]
                      {
                        (object) ObligatoryObjectAttributes.F_PRJLINK_ID
                      }, new SortOrders[1]{ SortOrders.ASC });
                      DataTable dataTable = relationCollection.ConsistFrom(paramSet, idbObject.ObjectID);
                      if (dataTable != null && dataTable.Rows.Count > 0)
                      {
                        foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
                        {
                          string str = Convert.ToString(row2[2]);
                          string g = Convert.ToString(row2[3]);
                          IDBRelation relation = idbObject.Session.GetRelation(new Guid(g), -1L, false);
                          RelationHashContentClass attributesList = new RelationHashContentClass(new Guid(g));
                          hashContent.Relations.Add(attributesList);
                          HashProcs.AddValueToHashStream(siStream, (object) g);
                          HashProcs.AddValueToHashStream(siStream, (object) str);
                          if (relation != null)
                            HashProcs.AddAttributableAttributesToHashStream((IDBAttributable) relation, siStream, hashVersion, setContent, (List<AttributeHashContentClass>) attributesList);
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          else
          {
            for (int index = 0; index < hashContent.Relations.Count; ++index)
            {
              RelationHashContentClass relation1 = hashContent.Relations[index];
              IDBRelation relation2 = idbObject.Session.GetRelation(relation1.Guid, -1L, false);
              if (relation2 != null)
              {
                IDBObject objectById = idbObject.Session.GetObjectByID(relation2.PartID, false);
                if (objectById != null)
                {
                  HashProcs.AddValueToHashStream(siStream, (object) relation2.GUID.ToString());
                  HashProcs.AddValueToHashStream(siStream, (object) objectById.GUID.ToString());
                  HashProcs.AddAttributableAttributesToHashStream((IDBAttributable) relation2, siStream, hashVersion, setContent, (List<AttributeHashContentClass>) relation1);
                }
              }
            }
          }
        }
        return hashVersion;
      }

      private static void AddAttributableAttributesToHashStream(
        IDBAttributable idbAttributable,
        Stream siStream,
        int hashVersion,
        bool setContent,
        List<AttributeHashContentClass> attributesList)
      {
        if (setContent)
        {
          for (int AttrIndex = 0; AttrIndex < idbAttributable.Attributes.Count; ++AttrIndex)
          {
            IDBAttribute attribute = idbAttributable.Attributes[AttrIndex];
            if (attribute.AttributeType.IsContent)
            {
              HashProcs.AddAttributeToHashStream(attribute, siStream, hashVersion);
              attributesList.Add(new AttributeHashContentClass(attribute.AttributeType.PropertiesStructure.AttributeGuid));
            }
          }
        }
        else
        {
          for (int index = 0; index < attributesList.Count; ++index)
          {
            IDBAttribute byGuid = idbAttributable.Attributes.FindByGUID(attributesList[index].Guid);
            if (byGuid != null)
              HashProcs.AddAttributeToHashStream(byGuid, siStream, hashVersion);
          }
        }
      }

      private static void AddAttributeToHashStream(IDBAttribute attr, Stream siStream, int hashVersion)
      {
        bool flag = false;
        using (RemoteLock remoteLock = new RemoteLock())
        {
          if (attr.AttributeType.AttributeType == FieldTypes.ftBlob || attr.AttributeType.AttributeType == FieldTypes.ftFile || attr.AttributeType.AttributeType == FieldTypes.ftShortBlob)
            remoteLock.Add((object) attr);
          for (int index = 0; index < attr.ValuesCount; ++index)
          {
            attr.Index = index;
            if (attr.AttributeType.AttributeType != FieldTypes.ftBlob && attr.AttributeType.AttributeType != FieldTypes.ftFile && attr.AttributeType.AttributeType != FieldTypes.ftShortBlob)
            {
              if (!attr.Value.Equals((object) DBNull.Value))
              {
                object obj = attr.DataType != FieldTypes.ftDateTime ? attr.Value : (object) (attr.AsDateTime - attr.Session.TimeZoneOffset);
                if (!flag)
                {
                  HashProcs.AddValueToHashStream(siStream, (object) attr.AttributeType.PropertiesStructure.AttributeGuid);
                  flag = true;
                }
                HashProcs.AddValueToHashStream(siStream, obj);
              }
            }
            else
            {
              if (!flag)
              {
                HashProcs.AddValueToHashStream(siStream, (object) attr.AttributeType.PropertiesStructure.AttributeGuid);
                flag = true;
              }
              HashProcs.AddBlobToHashStream(attr, siStream, hashVersion);
            }
          }
        }
      }

      private static void AddBlobToHashStream(IDBAttribute attr, Stream siStream, int hashVersion)
      {
        BlobInformation blobInformation = (attr as IBlobReader).OpenBlob(-1);
        if (blobInformation.FileType != FileTypes.ftNormal)
          return;
        DateTime dateTime = blobInformation.ModifyDate - attr.Session.TimeZoneOffset;
        HashProcs.AddValueToHashStream(siStream, (object) dateTime);
        if (!string.IsNullOrEmpty(blobInformation.FileName))
        {
          string fileName = blobInformation.FileName;
          HashProcs.AddValueToHashStream(siStream, (object) fileName);
        }
        bool flag = blobInformation.RealFileSize <= Consts.BlobInMemoryOperationalLimit;
        using (IsolatedStorageFile isolatedStorageFile = !flag ? HashProcs.GetIsolatedStorageFile() : (IsolatedStorageFile) null)
        {
          string str = Guid.NewGuid().ToString();
          try
          {
            using (Stream aDestStream = !flag ? (Stream) new IsolatedStorageFileStream(str, FileMode.Create, FileAccess.ReadWrite, isolatedStorageFile) : (Stream) new MemoryStream())
            {
              new BlobProcReader(attr, 0, aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(attr.Session);
              aDestStream.Position = 0L;
              aDestStream.CopyTo(siStream);
            }
          }
          finally
          {
            if (!flag)
            {
              try
              {
                isolatedStorageFile.DeleteFile(str);
              }
              catch
              {
              }
            }
          }
        }
      }

      private static void AddValueToHashStream(Stream stream, object value)
      {
        using (MemoryStream serializationStream = new MemoryStream())
        {
          new BinaryFormatter().Serialize((Stream) serializationStream, value);
          serializationStream.WriteTo(stream);
        }
      }

      private static IsolatedStorageFile GetIsolatedStorageFile()
      {
        lock (typeof (IsolatedStorageFile))
          return IsolatedStorageFile.GetUserStoreForDomain();
      }
    }
}

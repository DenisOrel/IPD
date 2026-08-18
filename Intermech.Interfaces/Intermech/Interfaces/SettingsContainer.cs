
// Type: Intermech.Interfaces.SettingsContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения контейнера настроек</summary>
    [Serializable]
    public sealed class SettingsContainer : ICloneable, ISettingsContainer
    {
      /// <summary>Advanced user settings</summary>
      public const string scConfigFileName = "Advanced user settings";
      /// <summary>Расширенные настройки учётной записи</summary>
      private static readonly string scConfigFileRemark = LocalizationHolder.rm.GetString("Interfaces_547");
      /// <summary>
      /// F_OBJECT_ID объекта, в котором хранятся данные настройки
      /// </summary>
      private long FObjectID;
      /// <summary>
      /// ID атрибута типа ftShortBlob, в котором хранятся данные настройки
      /// </summary>
      private int FAttrID;
      /// <summary>Уникальный ключ владельца данного контейнера настроек</summary>
      private string FOwnerID = string.Empty;
      /// <summary>
      /// Дата и время последнего доступа к настройкам (свойство нужно для сборки мусора)
      /// </summary>
      private DateTime FLastAccess;
      /// <summary>
      /// Коллекция пар значений [сериализуемый ключ] = [сериализуемый класс с какими-то настройками]
      /// </summary>
      private Dictionary<object, object> FSettings = new Dictionary<object, object>();

      /// <summary>
      /// F_OBJECT_ID объекта, в котором хранятся данные настройки
      /// </summary>
      public long ObjectID
      {
        get => this.FObjectID;
        set => this.FObjectID = value;
      }

      /// <summary>
      /// ID атрибута типа ftShortBlob, в котором хранятся данные настройки
      /// </summary>
      public int AttrID
      {
        get => this.FAttrID;
        set => this.FAttrID = value;
      }

      /// <summary>Уникальный ключ владельца данного контейнера настроек</summary>
      public string OwnerID
      {
        get => this.FOwnerID;
        set => this.FOwnerID = value;
      }

      /// <summary>
      /// Дата и время последнего доступа к настройкам (свойство нужно для сборки мусора)
      /// </summary>
      public DateTime LastAccess
      {
        get => this.FLastAccess;
        set => this.FLastAccess = value;
      }

      /// <summary>
      /// Ссылка на интерфейс коллекции значений [Ключ]=[Значение],
      /// где [Ключ] - уникальное сериализуемое значение-ключ,
      /// а [Значение] - ссылка на сериализуемый объект, в котором что-то хранится
      /// </summary>
      public IDictionary Settings => (IDictionary) this.FSettings;

      /// <summary>
      /// Получить или установить значение настроечного класса с определённым ключом
      /// </summary>
      /// <param name="Key">Уникальный (в пределах коллекции Settings) сериализуемый ключ настроечного класса</param>
      public object this[object Key]
      {
        get
        {
          object obj = (object) null;
          if (this.FSettings == null)
            this.FSettings = new Dictionary<object, object>();
          try
          {
            if (this.FSettings.ContainsKey(Key))
              obj = this.FSettings[Key];
          }
          catch
          {
            obj = (object) null;
          }
          this.FLastAccess = DateTime.UtcNow;
          return obj;
        }
        set
        {
          if (this.FSettings == null)
            this.FSettings = new Dictionary<object, object>();
          this.FSettings[Key] = value;
          this.FLastAccess = DateTime.UtcNow;
        }
      }

      /// <summary>Создать пустой экземпляр класса CurrentVersionRule</summary>
      public SettingsContainer()
      {
        if (this.FSettings == null)
          this.FSettings = new Dictionary<object, object>();
        this.Clear();
        this.FOwnerID = string.Empty;
        this.FObjectID = 0L;
        this.FAttrID = 0;
        this.FLastAccess = DateTime.UtcNow;
      }

      /// <summary>Создать пустой экземпляр класса CurrentVersionRule</summary>
      /// <param name="AObjectID">F_OBJECT_ID объекта, в котором хранятся данные настройки</param>
      /// <param name="AAttrID">ID атрибута типа ftShortBlob, в котором хранятся данные настройки</param>
      /// <param name="AOwnerID">ID владельца данного контейнера настроек</param>
      public SettingsContainer(long AObjectID, int AAttrID, string AOwnerID)
      {
        this.FOwnerID = AOwnerID;
        this.FObjectID = AObjectID;
        this.FAttrID = AAttrID;
        if (this.FSettings == null)
          this.FSettings = new Dictionary<object, object>();
        this.FLastAccess = DateTime.UtcNow;
      }

      /// <summary>Очистить настройки</summary>
      public void Clear()
      {
        lock (this)
        {
          this.FSettings.Clear();
          this.FLastAccess = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы настройки</param>
      public void Assign(ISettingsContainer Source)
      {
        if (Source == this)
          return;
        this.Clear();
        if (Source == null)
          return;
        lock (Source)
        {
          lock (this)
          {
            this.FOwnerID = Source.OwnerID;
            this.FAttrID = Source.AttrID;
            this.FObjectID = Source.ObjectID;
            this.FSettings.Clear();
            if (Source.Settings != null)
            {
              IDictionaryEnumerator enumerator = Source.Settings.GetEnumerator();
              if (enumerator != null)
              {
                enumerator.Reset();
                while (enumerator.MoveNext())
                  this.FSettings.Add(enumerator.Key, enumerator.Value);
              }
            }
          }
        }
        this.FLastAccess = DateTime.UtcNow;
      }

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы настройки</param>
      public void Assign(SettingsContainer Source)
      {
        this.Clear();
        if (Source == null)
          return;
        ISettingsContainer Source1 = (ISettingsContainer) Source;
        if (Source1 == null)
          return;
        this.Assign(Source1);
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        SettingsContainer settingsContainer = new SettingsContainer();
        settingsContainer.Assign(this);
        return (object) settingsContainer;
      }

      /// <summary>Загрузить настройки из объекта базы данных</summary>
      /// <param name="session">Сессия</param>
      /// <returns>true, если чтение прошло успешно</returns>
      public bool LoadFromObject(IUserSession session)
      {
        this.Clear();
        if (session == null || this.FObjectID == 0L || this.FAttrID == 0)
          return false;
        IDBObject dbObject;
        try
        {
          dbObject = session.GetObject(this.FObjectID);
        }
        catch
        {
          return false;
        }
        if (dbObject == null)
          return false;
        IDBAttribute attributeById;
        try
        {
          attributeById = dbObject.GetAttributeByID(this.AttrID);
        }
        catch
        {
          return false;
        }
        if (attributeById != null)
        {
          lock (this)
          {
            if (this.FSettings != null)
              this.FSettings.Clear();
            this.FSettings = (Dictionary<object, object>) null;
            if (attributeById is IDBShortBlobAttribute shortBlobAttribute)
            {
              ShortBlobValue blobValue = shortBlobAttribute.GetBlobValue();
              byte[] buffer = (byte[]) null;
              if (blobValue.RealFileSize > 0L)
                buffer = blobValue.Value;
              if (buffer != null)
              {
                if (buffer.Length != 0)
                {
                  using (MemoryStream inStream = new MemoryStream(buffer))
                  {
                    try
                    {
                      MemoryStream memoryStream;
                      long num;
                      if (blobValue.ArcMethod == ArcMethods.ZLibPacked)
                      {
                        memoryStream = new MemoryStream();
                        num = ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) memoryStream);
                      }
                      else
                      {
                        num = inStream.Length;
                        memoryStream = inStream;
                      }
                      if (num > 0L)
                      {
                        memoryStream.Seek(0L, SeekOrigin.Begin);
                        this.FSettings = new BinaryFormatter().Deserialize((Stream) memoryStream) as Dictionary<object, object>;
                      }
                      if (blobValue.ArcMethod == ArcMethods.ZLibPacked)
                        memoryStream.Close();
                    }
                    catch
                    {
                      this.FSettings = (Dictionary<object, object>) null;
                    }
                  }
                }
              }
            }
          }
        }
        if (this.FSettings == null)
          this.FSettings = new Dictionary<object, object>();
        return true;
      }

      /// <summary>Сохранить настройки в объект базы данных</summary>
      /// <param name="session">Сессия</param>
      /// <returns>true, если запись прошла успешноs</returns>
      public bool SaveToObject(IUserSession session)
      {
        if (session == null || this.FObjectID == 0L || this.FAttrID == 0)
          return false;
        if (this.FSettings == null)
          this.FSettings = new Dictionary<object, object>();
        IDBObject dbObject;
        try
        {
          dbObject = session.GetObject(this.FObjectID);
        }
        catch
        {
          return false;
        }
        if (dbObject == null)
          return false;
        IDBAttribute attributeById;
        try
        {
          attributeById = dbObject.GetAttributeByID(this.AttrID);
        }
        catch
        {
          return false;
        }
        if (attributeById != null)
        {
          lock (this.FSettings)
          {
            using (MemoryStream memoryStream = new MemoryStream())
            {
              using (MemoryStream outStream = new MemoryStream())
              {
                try
                {
                  new BinaryFormatter().Serialize((Stream) memoryStream, (object) this.FSettings);
                  long num = ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelNormal, (Stream) outStream);
                  IBlobWriter blobWriter = (IBlobWriter) attributeById;
                  if (blobWriter != null)
                  {
                    long length1 = memoryStream.Length;
                    ArcMethods arcMethod = ArcMethods.ZLibPacked;
                    byte[] array;
                    long length2;
                    if (num > 0L)
                    {
                      array = outStream.ToArray();
                      length2 = outStream.Length;
                    }
                    else
                    {
                      array = memoryStream.ToArray();
                      length2 = memoryStream.Length;
                      arcMethod = ArcMethods.NotPacked;
                    }
                    BlobInformation blobInfo = new BlobInformation(length1, length2, DateTime.Now, $"{this.FObjectID}.{this.FAttrID}", arcMethod, string.Empty);
                    blobWriter.OpenBlob(blobInfo, false);
                    blobWriter.WriteDataBlock(array);
                  }
                }
                catch
                {
                  return false;
                }
              }
            }
          }
        }
        return true;
      }

      /// <summary>
      /// Загрузить настройки из конфигурации пользователя. Имя файла будет равно OwnerID
      /// </summary>
      /// <param name="session">Сессия</param>
      public bool LoadFromUserConfig(IUserSession session)
      {
        this.Clear();
        if (session == null)
          return false;
        lock (this)
        {
          if (this.FSettings != null)
            this.FSettings.Clear();
          BlobInformation config_info = new BlobInformation(0L, 0L, DateTime.Now, "Advanced user settings", ArcMethods.NotPacked, string.Format(SettingsContainer.scConfigFileRemark, (object) this.FSettings.Count));
          byte[] config_file = (byte[]) null;
          session.Configurations.LoadConfigData("Advanced user settings", out config_info, out config_file);
          if (config_info.RealFileSize > 0L)
          {
            if (config_file.Length != 0)
            {
              using (MemoryStream serializationStream = new MemoryStream(config_file))
              {
                try
                {
                  this.FSettings = new BinaryFormatter().Deserialize((Stream) serializationStream) as Dictionary<object, object>;
                }
                catch
                {
                }
              }
            }
          }
        }
        if (this.FSettings == null)
          this.FSettings = new Dictionary<object, object>();
        if (this.FOwnerID == string.Empty)
          this.FOwnerID = session.UserID.ToString();
        return true;
      }

      /// <summary>
      /// Сохранить настройки в конфигурацию пользователя. Имя файла будет равно OwnerID
      /// </summary>
      /// <param name="session">Сессия</param>
      public bool SaveToUserConfig(IUserSession session)
      {
        if (session == null)
          return false;
        if (this.FSettings == null)
          this.FSettings = new Dictionary<object, object>();
        lock (this.FSettings)
        {
          using (MemoryStream serializationStream = new MemoryStream())
          {
            new BinaryFormatter().Serialize((Stream) serializationStream, (object) this.FSettings);
            BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "Advanced user settings", ArcMethods.NotPacked, string.Format(SettingsContainer.scConfigFileRemark, (object) this.FSettings.Count));
            session.Configurations.WriteConfigData(config_info, serializationStream.ToArray());
          }
          if (this.FOwnerID == string.Empty)
            this.FOwnerID = session.UserID.ToString();
        }
        return true;
      }
    }
}

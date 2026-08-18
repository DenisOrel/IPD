
// Type: Intermech.Interfaces.Briefcase.BriefcaseImportProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Параметры импорта</summary>
    [Serializable]
    public class BriefcaseImportProperties
    {
      /// <summary>Нахождение портфеля</summary>
      public BriefcaseLocation Location;
      private string _serverTempFolder;
      /// <summary>Флаг, удалить папку портфеля после импорта</summary>
      public bool DeleteTempFolder;
      /// <summary>Производить синхронизацию метаданных</summary>
      public bool IsSinhronized;
      /// <summary>Производить импорт только объектов</summary>
      public bool ObjectsOnly;
      /// <summary>Игнорировать ошибки импорта</summary>
      public bool IgnoreErrors;
      /// <summary>Режим добавления</summary>
      public bool CreateOnly;
      /// <summary>Новый Создатель и Владелец у импортируемых объектов</summary>
      public long NewUser;

      /// <summary>Темповая папка на сервере, куда распакуется портфель</summary>
      public string ServerTempFolder
      {
        get => this._serverTempFolder;
        set
        {
          if (value != null && value.Length == 2 && value[1] == ':')
            this._serverTempFolder = value + "\\";
          else
            this._serverTempFolder = value;
        }
      }

      public BriefcaseImportProperties(
        BriefcaseLocation location,
        bool isSinhronized,
        bool importObjectsOnly,
        bool ignoreErrors,
        bool createOnly,
        long newUser)
        : this(location, isSinhronized, importObjectsOnly, ignoreErrors, string.Empty, false, createOnly, newUser)
      {
      }

      public BriefcaseImportProperties(
        BriefcaseLocation location,
        bool isSinhronized,
        bool importObjectsOnly,
        bool ignoreErrors,
        string serverTempFolder,
        bool deleteTempFolder,
        bool createOnly,
        long newUser)
      {
        this.Location = location;
        this.ServerTempFolder = serverTempFolder;
        this.IsSinhronized = isSinhronized;
        this.DeleteTempFolder = deleteTempFolder;
        this.ObjectsOnly = importObjectsOnly;
        this.IgnoreErrors = ignoreErrors;
        this.CreateOnly = createOnly;
        this.NewUser = newUser;
      }

      public static BriefcaseImportProperties Empty
      {
        get => new BriefcaseImportProperties(new BriefcaseLocation(), false, false, false, false, 0L);
      }
    }
}

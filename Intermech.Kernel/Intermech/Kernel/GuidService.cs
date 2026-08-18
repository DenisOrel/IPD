// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GuidService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Configuration;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;


namespace Intermech.Kernel;

internal sealed class GuidService : LongLifeObject, IGuidService
{
  private IDbManagerService dbManagerService;
  private ICacheDataset dbCacheService;
  private object syncRoot;
  private bool isInitialized;
  private string connectionString;
  private IDbManager db;
  private object[,] metaData;

  public GuidService(IDbManagerService dbManagerService, ICacheDataset dbCacheService)
  {
    if (dbManagerService == null)
      throw new ArgumentNullException(nameof (dbManagerService));
    if (dbCacheService == null)
      throw new ArgumentNullException(nameof (dbCacheService));
    this.dbManagerService = dbManagerService;
    this.dbCacheService = dbCacheService;
    this.syncRoot = new object();
    this.connectionString = string.Empty;
    this.metaData = new object[8, 3]
    {
      {
        (object) "IMS_LANGUAGES",
        (object) "F_LANGUAGE_NAME",
        (object) 9
      },
      {
        (object) "IMS_OBJECT_TYPES",
        (object) "F_OBJ_TYPE_NAME",
        (object) 4
      },
      {
        (object) "IMS_ATTR_GROUPS",
        (object) "F_GROUP_NAME",
        (object) 12
      },
      {
        (object) "IMS_ATTRIBUTES",
        (object) "F_NAME",
        (object) 3
      },
      {
        (object) "IMS_SUBJECT_AREAS",
        (object) "F_AREA_NAME",
        (object) 11
      },
      {
        (object) "IMS_LC_SCHEMAS",
        (object) "F_NAME",
        (object) 16 /*0x10*/
      },
      {
        (object) "IMS_RELATION_TYPES",
        (object) "F_DESCRIPTION",
        (object) 6
      },
      {
        (object) "IMS_LEVELS",
        (object) "F_LEVEL_NAME",
        (object) 8
      }
    };
  }

  public string ConnectionString
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.connectionString;
    }
  }

  private bool IsInitialized
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.isInitialized;
    }
  }

  private void InitializeLazy()
  {
    lock (this.syncRoot)
    {
      if (this.isInitialized)
        return;
      try
      {
        this.InitializeCore();
        this.isInitialized = true;
      }
      catch (Exception ex)
      {
        throw new KernelException($"При инициализации сервиса системных GUID произошла ошибка. Строка подключения: {this.connectionString}", ex);
      }
    }
  }

  private void InitializeCore()
  {
    this.connectionString = $"Server=xeon1;Database=INTERMECH;Connect Timeout=15;User ID={"Intermech"};Password={Cryptor.Decrypt("bxXQPWzaWe43lUpr4JIQvSzmZyF8LAS5h/dWaV02XH4=", "cad00016-306c-11d8-b4e9-00304f19f545")}";
    this.db = this.dbManagerService.CreateDbManager("Sql", this.connectionString);
  }

  public Guid GenerateNextSystemGuid(int categoryType, string objectName, string note)
  {
    lock (this.syncRoot)
    {
      this.InitializeLazy();
      return this.GenerateNextSystemGuidInternal(categoryType, objectName, note);
    }
  }

  private Guid GenerateNextSystemGuidInternal(int categoryType, string objectName, string note)
  {
    this.db.BeginTransaction();
    try
    {
      string lower = this.db.ExecuteScalar("SELECT TOP 1 CAST(F_GUID AS NVARCHAR(40)) AS STRGUID FROM IMS_SYSTEM_GUID ORDER BY STRGUID DESC").ToString().ToLower();
      string oldValue = lower.StartsWith("cad", true, (CultureInfo) null) ? lower.Substring(0, 8) : throw new KernelException("База системных GUID содержит неверные уникальные идентификаторы");
      string g = lower.Replace(oldValue, (Convert.ToInt32(oldValue, 16 /*0x10*/) + 1).ToString("X").ToLower());
      this.db.ExecuteNonQuery("INSERT INTO IMS_SYSTEM_GUID (F_GUID, F_OBJ_NAME, F_CATEGORY_TYPE, F_NOTE) VALUES (:guid, :name, :category, :note)", this.db.Parameter("guid", (object) g), this.db.Parameter("name", (object) objectName), this.db.Parameter("category", (object) categoryType), this.db.Parameter(nameof (note), (object) note));
      this.db.Commit();
      return new Guid(g);
    }
    catch
    {
      this.db.Rollback();
      throw;
    }
  }

  public static bool IsServiceEnabled() => AppSettingsHelper.GetBoolean("GUIDs", false);

  private void FillTable(DataTable dt, string nameColumn, int categoryType)
  {
    if (dt == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      string aGUID = Convert.ToString(row["F_GUID"]);
      if (SystemGUIDs.IsSystemGUID(aGUID))
      {
        if (this.db.ExecuteScalar("SELECT F_OBJ_NAME FROM IMS_SYSTEM_GUID WHERE F_GUID =:guid", this.db.Parameter("guid", (object) aGUID)) == null)
        {
          string str;
          if (dt.TableName == "IMS_GUID_RESOLVE")
          {
            object obj = this.db.ExecuteScalar("SELECT F_STRING_VALUE FROM IMS_OBJECT_ATTRS WHERE (F_OBJECT_ID =:id) AND (F_ATTRIBUTE_ID = 10)", this.db.Parameter("id", (object) Convert.ToInt32(row[1])));
            if (obj == null)
              obj = this.db.ExecuteScalar("SELECT F_STRING_VALUE FROM IMS_OBJECT_ATTRS WHERE (F_OBJECT_ID =:id) AND (F_ATTRIBUTE_ID = 1307)", this.db.Parameter("id", (object) Convert.ToInt32(row[1])));
            str = obj == null ? string.Empty : obj.ToString();
          }
          else
            str = Convert.ToString(row[nameColumn]);
          this.db.ExecuteNonQuery("INSERT INTO IMS_SYSTEM_GUID (F_GUID, F_OBJ_NAME, F_CATEGORY_TYPE) VALUES (:guid, :name, :category)", this.db.Parameter("guid", (object) aGUID), this.db.Parameter("name", (object) str), this.db.Parameter("category", (object) categoryType));
        }
      }
    }
  }
}

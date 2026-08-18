// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.RDBMS.Database
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using System.Data;
using System.Data.SQLite;
using System.IO;

#nullable disable
namespace XmlReaderAPI.RDBMS;

/// <summary>
/// Статический класс, позволяющий создавать новую базу данных, открывать существующую
/// </summary>
public static class Database
{
  /// <summary>
  /// Создать пустую базу данных для индекса (перезаписать существующую)
  /// </summary>
  /// <param name="fileName">База данных для индекса</param>
  public static SQLiteConnection CreateDataBase(string fileName)
  {
    SQLiteConnection.CreateFile(fileName);
    SQLiteConnection connection = new SQLiteConnection($"Data Source = {fileName};Journal Mode=Off;UTF8Encoding=True;");
    connection.Open();
    using (SQLiteCommand sqLiteCommand = new SQLiteCommand(connection))
    {
      sqLiteCommand.CommandText = "PRAGMA page_size = 8192;";
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "PRAGMA synchronous = OFF;";
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "PRAGMA journal_mode = OFF;";
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "BEGIN;";
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE TABLE 'IMS_ATTRIBUTE_TYPES' (\r\n                    'F_ATTRIBUTE_ID' INTEGER PRIMARY KEY NOT NULL UNIQUE,\r\n                    'F_NAME' CHAR,\r\n                    'F_ALIAS' CHAR,\r\n                    'F_ATTRIBUTE_TYPE' INTEGER,\r\n                    'F_GUID' CHAR,\r\n                    'IPS_F_ATTRIBUTE_ID' INTEGER\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE TABLE 'IMS_OBJECT_TYPES' (\r\n                    'F_OBJ_TYPE' INTEGER PRIMARY KEY NOT NULL UNIQUE,\r\n                    'F_OBJ_TYPE_NAME' CHAR,\r\n                    'F_ICON' CHAR,\r\n                    'F_GUID' CHAR,\r\n                    'IPS_F_OBJ_TYPE' INTEGER\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE TABLE 'IMS_RELATION_TYPES' (\r\n                    'F_RELATION_TYPE' INTEGER PRIMARY KEY NOT NULL UNIQUE,\r\n                    'F_TYPE_NAME' CHAR,\r\n                    'F_GUID' CHAR,\r\n                    'IPS_F_RELATION_TYPE' INTEGER\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE TABLE 'IMS_ATTRIBUTES' (\r\n                    'ID' integer PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,\r\n                    'IS_OBJECT' BOOL NOT NULL,\r\n                    'OWNER_ID' INTEGER NOT NULL,\r\n                    'F_ATTRIBUTE_ID' INTEGER,\r\n                    'F_ATTRIBUTE_TYPE' INTEGER,\r\n                    'F_INLIST_ID' INTEGER, \r\n                    'F_VALUE' CHAR, \r\n                    'F_STRING_VALUE' CHAR,\r\n                    'F_DATE_VALUE' CHAR, \r\n                    'F_INTEGER_VALUE' INTEGER, \r\n                    'F_DOUBLE_VALUE' DOUBLE, \r\n                    'F_GUID' CHAR,\r\n                    'F_ARC_METHOD' INTEGER, \r\n                    'F_FILENAME' CHAR, \r\n                    'F_FILESIZE' CHAR,\r\n                    'F_NOTE' CHAR,\r\n                    'F_PATH2FILE' CHAR,\r\n                    'F_LINKTYPE' INTEGER\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE TABLE 'IMS_OBJECTS' (\r\n                    'F_OBJECT_ID' INTEGER, \r\n                    'F_OBJECT_TYPE' INTEGER, \r\n                    'F_OBJECTGUID' CHAR, \r\n                    'F_ID' INTEGER, \r\n                    'F_IDGUID' CHAR, \r\n                    'F_LC_STEP' INTEGER, \r\n                    'F_CHKOUT_BY' INTEGER, \r\n                    'F_CHKOUTGUID' CHAR, \r\n                    'F_VERSION_ID' INTEGER, \r\n                    'F_PARENT_ID' INTEGER, \r\n                    'F_OBJECT_VER_TYPE' INTEGER, \r\n                    'F_OWNER_ID' INTEGER, \r\n                    'F_OWNERGUID' CHAR, \r\n                    'F_MODIFY_DATE' CHAR, \r\n                    'F_LEVEL_ID' INTEGER, \r\n                    'F_OBJ_CREATE' CHAR, \r\n                    'CAPTION' CHAR,\r\n                    'IPS_F_OBJECT_ID' INTEGER,\r\n                    'IPS_F_OBJ_TYPE'  INTEGER,\r\n                    'PARSED' INTEGER\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE TABLE 'IMS_RELATIONS' (\r\n                    'F_PRJLINK_ID' INTEGER,\r\n                    'F_RELATION_TYPE' INTEGER,\r\n                    'F_PROJ_OBJ' INTEGER,\r\n                    'F_PART_OBJ' INTEGER,\r\n                    'F_PRJ_GUID' CHAR,\r\n                    'F_PROJ_ID' CHAR,\r\n                    'F_PART_ID' CHAR,\r\n                    'F_CREATE_DATE' CHAR,\r\n                    'F_DELETE_DATE' CHAR,\r\n                    'IPS_F_PRJLINK_ID' INTEGER,\r\n                    'IPS_F_PROJ_OBJ' INTEGER,\r\n                    'IPS_F_PART_OBJ' INTEGER,\r\n                    'IPS_F_RELATION_TYPE' INTEGER,\r\n                    'PARSED' INTEGER\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_ATTRIBUTES_ATTRS' ON 'IMS_ATTRIBUTES' (\r\n                    'F_ATTRIBUTE_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_ATTRIBUTES_ITEMS_MIN' ON 'IMS_ATTRIBUTES' (\r\n                    'IS_OBJECT' ASC,\r\n                    'OWNER_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_ATTRIBUTES_ITEMS' ON 'IMS_ATTRIBUTES' (\r\n                    'IS_OBJECT' ASC,\r\n                    'OWNER_ID' ASC,\r\n                    'F_ATTRIBUTE_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_ID_OBJ' ON 'IMS_OBJECTS' (\r\n                    'F_OBJECT_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_PARSED' ON 'IMS_OBJECTS' (\r\n                    'F_OBJECT_ID' ASC, \r\n                    'PARSED' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_ID_OBJID' ON 'IMS_OBJECTS' (\r\n                    'F_OBJECT_ID' ASC, \r\n                    'F_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_ID_FID' ON 'IMS_OBJECTS' (\r\n                    'F_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_TYPE' ON 'IMS_OBJECTS' (\r\n                    'F_OBJECT_TYPE' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_ID_TYPE' ON 'IMS_OBJECTS' (\r\n                    'F_OBJECT_ID' ASC,\r\n                    'F_OBJECT_TYPE' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_GUID' ON 'IMS_OBJECTS' (\r\n                    'F_OBJECTGUID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_CHKOUT_BY' ON 'IMS_OBJECTS' (\r\n                    'F_CHKOUT_BY' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_OWNER_ID' ON 'IMS_OBJECTS' (\r\n                    'F_OWNER_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJECTS_ID_IPS' ON 'IMS_OBJECTS' (\r\n                    'IPS_F_OBJECT_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELATIONS_F_PROJ_OBJ' ON 'IMS_RELATIONS' (\r\n                    'F_PROJ_OBJ' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELATIONS_F_PART_OBJ' ON 'IMS_RELATIONS' (\r\n                    'F_PART_OBJ' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELATIONS_PROJ_TYPE' ON 'IMS_RELATIONS' (\r\n                    'F_RELATION_TYPE' ASC,\r\n                    'F_PROJ_OBJ' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELATIONS_PRJLINK_ID' ON 'IMS_RELATIONS' (\r\n                    'F_PRJLINK_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELATIONS_PARSED' ON 'IMS_RELATIONS' (\r\n                    'F_PRJLINK_ID' ASC,\r\n                    'PARSED' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELATIONS_LINKID_IPS' ON 'IMS_RELATIONS' (\r\n                    'IPS_F_PRJLINK_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_ATTRTYPES_ID' ON 'IMS_ATTRIBUTE_TYPES' (\r\n                                    'F_ATTRIBUTE_ID' ASC\r\n                                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_ATTRTYPES_ID_IPS' ON 'IMS_ATTRIBUTE_TYPES' (\r\n                    'IPS_F_ATTRIBUTE_ID' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE UNIQUE INDEX 'IMS_OBJTYPES_ID' ON 'IMS_OBJECT_TYPES' (\r\n                    'F_OBJ_TYPE' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_OBJTYPES_ID_IPS' ON 'IMS_OBJECT_TYPES' (\r\n                    'IPS_F_OBJ_TYPE' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE UNIQUE INDEX 'IMS_RELTYPES_ID' ON 'IMS_RELATION_TYPES' (\r\n                    'F_RELATION_TYPE' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "CREATE INDEX 'IMS_RELTYPES_ID_IPS' ON 'IMS_RELATION_TYPES' (\r\n                    'IPS_F_RELATION_TYPE' ASC\r\n                    );";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
      sqLiteCommand.CommandText = "COMMIT;";
      sqLiteCommand.CommandType = CommandType.Text;
      sqLiteCommand.ExecuteNonQuery();
    }
    return connection;
  }

  /// <summary>Открыть базу данных индекса</summary>
  /// <param name="fileName">База данных для индекса</param>
  public static SQLiteConnection OpenDataBase(string fileName)
  {
    return !File.Exists(fileName) ? Database.CreateDataBase(fileName) : new SQLiteConnection($"Data Source = :memory:;New=True;Password={fileName};Journal Mode=Off;UTF8Encoding=True;");
  }
}

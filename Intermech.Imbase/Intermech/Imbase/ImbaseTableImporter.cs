// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseTableImporter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Commands;
using Intermech.Imbase.Editors;
using Intermech.Imbase.FileImport;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseTableImporter
{
  internal static string _lastPath = string.Empty;

  internal static void Execute()
  {
    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
    {
      if (!string.IsNullOrEmpty(ImbaseTableImporter._lastPath))
        folderBrowserDialog.SelectedPath = ImbaseTableImporter._lastPath;
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      string selectedPath = folderBrowserDialog.SelectedPath;
      ImbaseTableImporter._lastPath = selectedPath;
      if (ImbaseTableImporter.CheckNeedFiles(selectedPath))
      {
        ImbaseTableImporter.ProcessForTable(selectedPath, true);
      }
      else
      {
        string[] directories = Directory.GetDirectories(selectedPath);
        if (directories == null || directories.Length == 0)
        {
          int num1 = (int) MessageBox.Show("В указанной папке отсутствуют необходимые файлы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
          using (ImportManyDialog importManyDialog = new ImportManyDialog(directories))
          {
            int num2 = (int) importManyDialog.ShowDialog();
          }
        }
      }
    }
  }

  internal static string ProcessForTable(string tablePath, bool showInNavigator)
  {
    string data1 = ImbaseTableImporter.ReadFromFile(tablePath, "data.xml");
    string str1 = ImbaseTableImporter.ReadFromFile(tablePath, "info.xml");
    string data2 = ImbaseTableImporter.ReadFromFile(tablePath, "structure.xml");
    string str2 = ImbaseTableImporter.ReadFromFile(tablePath, "data.txt");
    DataTable dt = new DataTable();
    int num1 = (int) dt.ReadXml((TextReader) new StringReader(str1));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject orCreateTable = ImbaseTableImporter.FindOrCreateTable(session, dt);
      int attributeId = session.GetAttributeType(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")).AttributeID;
      long num2 = Math.Abs(orCreateTable.ObjectID);
      string str3 = "." + num2.ToString();
      IDBAttribute fileAtt = orCreateTable.GetAttributeByID(attributeId);
      if (fileAtt != null)
        fileAtt.ClearValues();
      else
        fileAtt = orCreateTable.Attributes.AddAttribute(attributeId, false);
      ImbaseTableImporter.WriteString(fileAtt, data1, "data.xml" + str3);
      fileAtt.AddValue((object) null);
      ImbaseTableImporter.WriteString(fileAtt, str1, "info.xml" + str3);
      fileAtt.AddValue((object) null);
      ImbaseTableImporter.WriteString(fileAtt, data2, "structure.xml" + str3);
      fileAtt.AddValue((object) null);
      ImbaseTableImporter.WriteString(fileAtt, str2, "data.txt" + str3);
      if (orCreateTable.IsCreationMode)
        orCreateTable.CommitCreation(true);
      long parentFolder = ImbaseTableImporter.FindParentFolder(session, str2);
      (session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).ForceImportImbaseTable(session.SessionGUID, num2, parentFolder);
      if (showInNavigator)
      {
        long[] allTableLinks = ImbaseTableImporter.GetAllTableLinks(session, orCreateTable.ObjectID);
        if (allTableLinks != null && allTableLinks.Length != 0)
          ImbaseContextCommandProvider.OpenInImbaseTree(allTableLinks[0]);
        ImbaseTableImporter.CheckImportConflict(session, num2);
      }
      return orCreateTable.Caption;
    }
  }

  internal static bool CheckNeedFiles(string tablePath)
  {
    string[] files = Directory.GetFiles(tablePath);
    int num = 0;
    foreach (string str in files)
    {
      if (str.EndsWith("data.xml"))
        ++num;
      else if (str.EndsWith("info.xml"))
        ++num;
      else if (str.EndsWith("structure.xml"))
        ++num;
      else if (str.EndsWith("data.txt"))
        ++num;
    }
    return num == 4;
  }

  internal static void CheckImportConflict(IUserSession session, long objectId)
  {
    IDBObject table = session.GetObject(objectId, true);
    if (table.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false) == null)
      return;
    using (ImportOldTableConflictEditor tableConflictEditor = new ImportOldTableConflictEditor())
    {
      tableConflictEditor.InitData(session, table);
      int num = (int) tableConflictEditor.ShowDialog();
    }
  }

  internal static long[] GetAllTableLinks(IUserSession session, long tableId)
  {
    DataTable dataTable = session.GetObjectCollection(Consts.ImbaseTableRefTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) tableId, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }));
    List<long> longList = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      longList.Add(Convert.ToInt64(row[0]));
    return longList.ToArray();
  }

  internal static long FindParentFolder(IUserSession session, string attList)
  {
    string[] strArray = attList.Split(new string[1]
    {
      Environment.NewLine
    }, StringSplitOptions.RemoveEmptyEntries);
    string path = (string) null;
    string folderData = (string) null;
    Dictionary<string, string> blobsList = new Dictionary<string, string>(32 /*0x20*/);
    foreach (string str1 in strArray)
    {
      int length = str1.IndexOf('=');
      if (length != -1)
      {
        string key = str1.Substring(0, length).Trim();
        if (!string.IsNullOrEmpty(key))
        {
          string str2 = str1.Substring(length + 1, str1.Length - length - 1).Trim();
          if (!string.IsNullOrEmpty(str2))
          {
            switch (key)
            {
              case "F_PATH":
                if (!string.IsNullOrEmpty(str2))
                {
                  path = str2;
                  continue;
                }
                continue;
              case "F_FOLDERDATA":
                if (!string.IsNullOrEmpty(str2))
                {
                  folderData = str2;
                  continue;
                }
                continue;
              default:
                if (key.StartsWith("F_BLOB") && !blobsList.ContainsKey(key))
                {
                  blobsList[key] = str2;
                  continue;
                }
                continue;
            }
          }
        }
      }
    }
    return !string.IsNullOrEmpty(path) ? ImbaseFolderCreator.FindOrCreatePath(session, path, folderData, blobsList) : -1L;
  }

  internal static long GetTableIdByTableName(string value, IUserSession session)
  {
    DataTable dataTable = session.GetObjectCollection(Consts.ImbaseTableTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Consts.ImbaseInternalTableNameAttID, RelationalOperators.Equal, (object) value, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
  }

  internal static IDBObject FindOrCreateTable(IUserSession session, DataTable dt)
  {
    string newValue = Convert.ToString(dt.Rows[0]["F_TABLE"]);
    long tableIdByTableName = ImbaseTableImporter.GetTableIdByTableName(newValue, session);
    if (tableIdByTableName != -1L)
      return session.GetObject(tableIdByTableName);
    IDBObject orCreateTable = session.GetObjectCollection(Consts.ImbaseTableTypeID).Create();
    orCreateTable.TryToAddOrDelAttribute(Consts.ImbaseInternalTableNameAttID, (object) newValue);
    orCreateTable.Caption = Convert.ToString(dt.Rows[0]["F_DESCR"]);
    return orCreateTable;
  }

  private static string ReadFromFile(string path, string fileName)
  {
    using (StreamReader streamReader = new StreamReader(Path.Combine(path, fileName), Encoding.Unicode))
      return streamReader.ReadToEnd();
  }

  private static void WriteString(IDBAttribute fileAtt, string data, string fileName)
  {
    using (MemoryStream inStream = new MemoryStream(data.Length * 2))
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) inStream, Encoding.Unicode))
        {
          streamWriter.Write(data);
          streamWriter.Flush();
          inStream.Position = 0L;
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, 5);
          byte[] array = outStream.ToArray();
          BlobInformation blobInfo = (fileAtt as IBlobReader).OpenBlob(-1) with
          {
            FileName = fileName
          };
          (fileAtt as IBlobWriter).OpenBlob(blobInfo, true);
          blobInfo.PackedFileSize = (long) array.Length;
          blobInfo.RealFileSize = inStream.Length;
          blobInfo.ModifyDate = DateTime.Now;
          blobInfo.ArcMethod = ArcMethods.ZLibPacked;
          IBlobWriter blobWriter = fileAtt as IBlobWriter;
          blobWriter.OpenBlob(blobInfo, false);
          blobWriter.WriteDataBlock(array);
        }
      }
    }
  }
}

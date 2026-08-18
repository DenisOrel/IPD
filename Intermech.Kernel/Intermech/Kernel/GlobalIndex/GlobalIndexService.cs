// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.GlobalIndexService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.GlobalIndex;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;


namespace Intermech.Kernel.GlobalIndex;

public class GlobalIndexService : 
  LongLifeObject,
  IGlobalIndexService,
  IGlobalIndexSettings,
  IGlobalIndexHelper
{
  private IDbManagerService _DbManagerService;
  private int _MinWordLength = 3;
  private long _IndexedObjects;
  private UserSession _IndexerSession;
  private IStemmer _RusStemmer = (IStemmer) new RussianStemmer();
  private IStemmer _EngStemmer = (IStemmer) new EnglishStemmer();
  private const int PROCESS_NOT_STARTED = 0;
  private const int PROCESS_STARTED = 1;
  private int _QueueInProcess;
  private List<IIndexerFileConverter> _Converters = new List<IIndexerFileConverter>();
  private IndexerTask _IndexerTask;
  private ComputeRelevancyTask _ComputeRelevancyTask;
  private bool _SaveSearchQueryHistory;
  private string _NotIndexingExtensions = string.Empty;
  private List<string> _NotIndexingExtensionsList = new List<string>();

  public GlobalIndexService(IDbManagerService dbManagerService, IDBTimedEvents te)
  {
    if (dbManagerService == null)
      throw new ArgumentNullException(nameof (dbManagerService));
    if (te == null)
      throw new ArgumentNullException(nameof (te));
    this._DbManagerService = dbManagerService;
    this._IndexerSession = (UserSession) te.GetSystemSessionPermanentClone("IndexerSession");
    this.ComputeIndexedObjects(this._IndexerSession.DataManager);
    this._MinWordLength = Convert.ToInt32(this._IndexerSession.Configurations.ReadInteger("KERNEL", "GLOBAL_INDEX", "MIN_WORD_LENGTH", (long) this._MinWordLength, DBConfigMode.GlobalOnly));
    this._SaveSearchQueryHistory = this._IndexerSession.Configurations.ReadBool("KERNEL", "GLOBAL_INDEX", "SAVE_SEARCH_HISTORY", false, DBConfigMode.GlobalOnly);
    string str = ConfigurationManager.AppSettings.Get(nameof (NotIndexingExtensions));
    if (str != null && str != string.Empty)
      this.NotIndexingExtensions = str;
    this._IndexerTask = new IndexerTask(this);
    this._ComputeRelevancyTask = new ComputeRelevancyTask(this);
    te.RegisterService((object) this._IndexerTask);
    te.RegisterService((object) this._ComputeRelevancyTask);
  }

  public long ComputeIndexedObjects(IDbManager db)
  {
    long int64 = Convert.ToInt64(db.ExecuteScalar("SELECT COUNT(distinct F_OBJECT_ID) FROM IMS_INDEX_RESULT"));
    this._IndexedObjects = int64;
    return int64;
  }

  public void IncIndexedObjects() => ++this._IndexedObjects;

  public void GetSQLforWord(
    IUserSession session,
    string word1,
    GlobalIndexSearchValue condition,
    List<long> words_id)
  {
    words_id.Clear();
    List<string> stringList = new List<string>();
    if ((condition.SearchOptions & GlobalIndexSearchOptions.SubstringSearch) == GlobalIndexSearchOptions.None && condition.Value.Trim().IndexOf('"') != 0)
      stringList.Add(condition.Value.Trim());
    if ((condition.SearchOptions & GlobalIndexSearchOptions.StemmedWords) == GlobalIndexSearchOptions.StemmedWords)
    {
      string normalForm;
      string stemmedForm;
      int indexWords = (int) this.GetIndexWords(word1, out normalForm, out stemmedForm);
      stringList.Add(normalForm);
      if (stemmedForm != normalForm)
        stringList.Add(stemmedForm);
      string indexedString = ServerStringNormalizer.GetIndexedString(word1);
      if (indexedString != normalForm && indexedString != stemmedForm)
        stringList.Add(indexedString);
      if (word1 != normalForm && word1 != stemmedForm && word1 != indexedString)
        stringList.Add(word1);
    }
    else
    {
      string word = word1;
      if (this.GetWordLanguage(ref word) == WordLanguageEnum.Russian)
      {
        string indexedString = ServerStringNormalizer.GetIndexedString(word1);
        if (indexedString != word)
          stringList.Add(indexedString);
      }
      stringList.Add(word);
      if (word != word1)
        stringList.Add(word1);
    }
    IDbManager dataManager = (session as UserSession).DataManager;
    if ((condition.SearchOptions & GlobalIndexSearchOptions.SubstringSearch) == GlobalIndexSearchOptions.SubstringSearch)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append("SELECT F_WORD_ID FROM IMS_INDEX_WORDS WHERE ");
        IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[stringList.Count];
        for (int index = 0; index < stringList.Count; ++index)
        {
          stringBuilder.AppendFormat("(F_WORD LIKE :p{0}) OR ", (object) index);
          dbDataParameterArray[index] = dataManager.Parameter(":p" + index.ToString(), (object) $"%{stringList[index]}%");
        }
        stringBuilder.Length -= 4;
        DataTable dataTable = dataManager.ExecuteDataTable(stringBuilder.ToString(), dbDataParameterArray);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          words_id.Add(Convert.ToInt64(dataTable.Rows[index][0]));
      }
    }
    else if (stringList.Count == 1)
    {
      object obj = dataManager.ExecuteScalar("SELECT F_WORD_ID FROM IMS_INDEX_WORDS WHERE F_WORD = :wordStr", dataManager.Parameter("wordStr", (object) stringList[0]));
      if (obj == null || obj == DBNull.Value)
        return;
      words_id.Add(Convert.ToInt64(obj));
    }
    else
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append("SELECT F_WORD_ID FROM IMS_INDEX_WORDS WHERE F_WORD IN (");
        IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[stringList.Count];
        for (int index = 0; index < stringList.Count; ++index)
        {
          stringBuilder.AppendFormat(":p{0},", (object) index);
          dbDataParameterArray[index] = dataManager.Parameter(":p" + index.ToString(), (object) stringList[index]);
        }
        --stringBuilder.Length;
        DataTable dataTable = dataManager.ExecuteDataTable(stringBuilder.ToString() + ")", dbDataParameterArray);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          words_id.Add(Convert.ToInt64(dataTable.Rows[index][0]));
      }
    }
  }

  public string[] GetWords(ConditionStructure cond)
  {
    if (!(cond.Value is GlobalIndexSearchValue indexSearchValue))
      throw new KernelException(sc_13035.ssp_appserver_13036());
    int attributeID = 0;
    if (cond.Attribute is int)
      attributeID = Convert.ToInt32(cond.Attribute);
    indexSearchValue.Value = indexSearchValue.Value.Trim();
    string[] words;
    if (indexSearchValue.Value != string.Empty && indexSearchValue.Value[0] == '"' && indexSearchValue.Value[indexSearchValue.Value.Length - 1] == '"')
    {
      words = new string[1]
      {
        indexSearchValue.Value.Replace('"', ' ').Trim()
      };
    }
    else
    {
      words = this.SplitText(indexSearchValue.Value, attributeID);
      if (words.Length == 0)
        words = new string[1]{ indexSearchValue.Value };
    }
    return words;
  }

  public string[] SplitText(string text, int attributeID)
  {
    return text.Split(IndexerAttributeSettings.DefaultDelimiterChars, StringSplitOptions.RemoveEmptyEntries);
  }

  public void IndexText(IndexQueueProperties attribute, UserSession session1)
  {
    if (attribute.DataType == FieldTypes.ftFile)
      this.AddToQueue(session1.DataManager, attribute.ObjectID, attribute.AttributeID, attribute.InlistID);
    else
      this.IndexTextInternal(attribute, session1);
  }

  public void IndexTextInternal(IndexQueueProperties attribute, UserSession session1)
  {
    IDbManager dataManager = session1.DataManager;
    if (attribute.ID <= 0L)
      throw new KernelExceptionID(sc_13035.ssp_appserver_13037(1304681243), (object) MetaDataHelper.GetAttributeTypeName(attribute.AttributeID));
    if (attribute.ObjectID < 0L)
    {
      object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) attribute.ObjectID));
      if (obj == null || obj == DBNull.Value)
      {
        attribute.ObjectID = -attribute.ObjectID;
        IDBObject dbObject = session1.GetObject(attribute.ObjectID, false);
        if (dbObject == null)
          return;
        IDBAttribute attributeById = dbObject.GetAttributeByID(attribute.AttributeID);
        attribute.Text = string.Empty;
        if (attributeById != null)
        {
          try
          {
            attributeById.Index = attribute.InlistID;
            if (!attributeById.IsNull)
              attribute.Text = attributeById.Value.ToString();
          }
          catch
          {
          }
        }
      }
    }
    string text = attribute.Text.Trim();
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) attribute.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("attrID", (object) attribute.AttributeID);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("inlistID", (object) attribute.InlistID);
    if (text.Length == 0)
    {
      dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", dbDataParameter1, dbDataParameter2, dbDataParameter3);
      dataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE IMS_INDEX_RESULT.F_OBJECT_ID = :objID AND NOT EXISTS(SELECT * FROM IMS_GLOBAL_INDEX WHERE IMS_GLOBAL_INDEX.F_OBJECT_ID = :objID AND IMS_GLOBAL_INDEX.F_WORD_ID = IMS_INDEX_RESULT.F_WORD_ID)", dbDataParameter1);
    }
    else
    {
      string[] strArray;
      if ((attribute.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue)
        strArray = new string[1]{ text };
      else
        strArray = this.SplitText(text, attribute.AttributeID);
      if (strArray.Length == 0)
        return;
      try
      {
        int num1 = 0;
        SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index].Length > Consts.MaxIndexWordLength)
            strArray[index] = strArray[index].Substring(0, Consts.MaxIndexWordLength);
          strArray[index] = strArray[index].Trim();
          if (strArray[index].Length >= this.MinWordLength)
          {
            ++num1;
            string normalForm;
            string stemmedForm;
            if ((attribute.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue)
            {
              normalForm = strArray[index];
              int wordLanguage = (int) this.GetWordLanguage(ref normalForm);
              stemmedForm = normalForm;
            }
            else
            {
              int indexWords = (int) this.GetIndexWords(strArray[index], out normalForm, out stemmedForm);
            }
            int num2;
            if (sortedDictionary.TryGetValue(normalForm, out num2))
              sortedDictionary[normalForm] = ++num2;
            else
              sortedDictionary.Add(normalForm, 1);
            if (normalForm != stemmedForm)
            {
              if (sortedDictionary.TryGetValue(stemmedForm, out num2))
                sortedDictionary[stemmedForm] = ++num2;
              else
                sortedDictionary.Add(stemmedForm, 1);
            }
          }
        }
        IDbDataParameter dbDataParameter4 = dataManager.Parameter("word_Par", (object) string.Empty);
        IDbDataParameter dbDataParameter5 = dataManager.Parameter("wordID_Par", (object) 0L);
        IDbDataParameter dbDataParameter6 = dataManager.Parameter("fid", (object) 0L);
        if (sortedDictionary.Count > 0)
        {
          object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_INDEX_RESULT WHERE F_OBJECT_ID = :objID", dbDataParameter1);
          if (obj == null || obj == DBNull.Value)
            this.IncIndexedObjects();
        }
        dataManager.BeginTransaction();
        try
        {
          dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", dbDataParameter1, dbDataParameter2, dbDataParameter3);
          foreach (KeyValuePair<string, int> keyValuePair in sortedDictionary)
          {
            dbDataParameter4.Value = (object) keyValuePair.Key;
            double tf = (double) keyValuePair.Value / (double) num1;
            bool flag = false;
            int d_t1 = 0;
            DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_WORD_ID, F_OBJECT_COUNT FROM IMS_INDEX_WORDS WHERE F_WORD = :word_Par", dbDataParameter4);
            if (dataTable.Rows.Count > 0)
            {
              flag = true;
              dbDataParameter5.Value = (object) Convert.ToInt64(dataTable.Rows[0][0]);
              d_t1 = Convert.ToInt32(dataTable.Rows[0][1]);
            }
            if (flag)
            {
              dataManager.ExecuteNonQuery("INSERT INTO IMS_GLOBAL_INDEX (F_WORD_ID, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_ID, F_TF, F_WORD_REPEAT) VALUES (:wordID_Par, :objID, :attrID, :inlistID, :fid, :ftf, :word_repeat)", dbDataParameter5, dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter6, dataManager.Parameter("ftf", (object) tf), dataManager.Parameter("word_repeat", (object) keyValuePair.Value));
              object obj = dataManager.ExecuteScalar($"SELECT {dataManager.DataProvider.GetRoundSQL("F_TF_IDF", 16 /*0x10*/)} FROM IMS_INDEX_RESULT WHERE F_WORD_ID = :wordID_Par AND F_OBJECT_ID = :objID", dbDataParameter5, dbDataParameter1);
              if (obj != null && obj != DBNull.Value)
              {
                double num3 = Convert.ToDouble(obj);
                double num4 = this.CalcTF_IDF(tf, d_t1);
                if (num4 > num3)
                  dataManager.ExecuteNonQuery("UPDATE IMS_INDEX_RESULT SET F_TF_IDF = :tf_idf WHERE F_WORD_ID = :wordID_Par AND F_OBJECT_ID = :objID", dataManager.Parameter("tf_idf", (object) num4), dbDataParameter5, dbDataParameter1);
              }
              else
              {
                int d_t2 = d_t1 + 1;
                dataManager.ExecuteNonQuery("UPDATE IMS_INDEX_WORDS SET F_OBJECT_COUNT = F_OBJECT_COUNT + 1 WHERE F_WORD_ID = :wordID_Par", dbDataParameter5);
                dataManager.ExecuteNonQuery("INSERT INTO IMS_INDEX_RESULT (F_WORD_ID, F_OBJECT_ID, F_TF_IDF) VALUES (:wordID_Par, :objID, :tf_idf)", dbDataParameter5, dbDataParameter1, dataManager.Parameter("tf_idf", (object) this.CalcTF_IDF(tf, d_t2)));
              }
            }
            else
            {
              dbDataParameter6.Value = (object) attribute.ID;
              dbDataParameter5.Value = (object) dataManager.DataProvider.NextGeneratorValue("IMS_WORD_ID_GEN", dataManager);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_INDEX_WORDS (F_WORD, F_WORD_ID, F_OBJECT_COUNT) VALUES (:word_Par, :wordID_Par, :obj_count)", dbDataParameter4, dbDataParameter5, dataManager.Parameter("obj_count", (object) 1));
              dataManager.ExecuteNonQuery("INSERT INTO IMS_GLOBAL_INDEX (F_WORD_ID, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_ID, F_TF, F_WORD_REPEAT) VALUES (:wordID_Par, :objID, :attrID, :inlistID, :fid, :ftf, :word_repeat)", dbDataParameter5, dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter6, dataManager.Parameter("ftf", (object) tf), dataManager.Parameter("word_repeat", (object) keyValuePair.Value));
              dataManager.ExecuteNonQuery("INSERT INTO IMS_INDEX_RESULT (F_WORD_ID, F_OBJECT_ID, F_TF_IDF) VALUES (:wordID_Par, :objID, :tf_idf)", dbDataParameter5, dbDataParameter1, dataManager.Parameter("tf_idf", (object) this.CalcTF_IDF(tf, 1)));
            }
          }
          dataManager.Commit();
        }
        catch
        {
          dataManager.Rollback();
          throw;
        }
        dataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE IMS_INDEX_RESULT.F_OBJECT_ID = :objID AND NOT EXISTS(SELECT * FROM IMS_GLOBAL_INDEX WHERE IMS_GLOBAL_INDEX.F_OBJECT_ID = :objID AND IMS_GLOBAL_INDEX.F_WORD_ID = IMS_INDEX_RESULT.F_WORD_ID)", dbDataParameter1);
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("timeout") || ex.Message.Contains("deadlock"))
          this.AddToQueue(dataManager, attribute.ObjectID, attribute.AttributeID, attribute.InlistID);
        throw;
      }
    }
  }

  private double CalcTF_IDF(double tf, int d_t)
  {
    return tf * Math.Log((double) this._IndexedObjects / (double) d_t);
  }

  public WordLanguageEnum GetWordLanguage(ref string word)
  {
    WordLanguageEnum wordLanguage = WordLanguageEnum.None;
    word = word.Trim().ToUpper().Replace('Ё', 'Е');
    if (word.Length == 0)
      return wordLanguage;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    for (int index = 0; index < word.Length; ++index)
    {
      if (char.IsLetter(word[index]))
      {
        if (ServerStringNormalizer.IsUpperRus(word[index]))
        {
          ++num1;
          if (ServerStringNormalizer.RusLettersUpper.IndexOf(word[index]) >= 0)
            ++num2;
        }
        else if (ServerStringNormalizer.LatLettersUpper.IndexOf(word[index]) >= 0)
          ++num3;
      }
      else
      {
        wordLanguage = WordLanguageEnum.Symbols;
        break;
      }
    }
    if (wordLanguage == WordLanguageEnum.None)
    {
      if (num1 == 0)
        wordLanguage = WordLanguageEnum.English;
      else if (num1 == word.Length)
        wordLanguage = WordLanguageEnum.Russian;
      else if (word.Length - num1 == num3)
      {
        word = ServerStringNormalizer.NormalizeToUpperRus(word);
        wordLanguage = WordLanguageEnum.Russian;
      }
      else if (num1 == num2)
      {
        word = ServerStringNormalizer.NormalizeToUpperLat(word);
        wordLanguage = WordLanguageEnum.Russian;
      }
      else
      {
        word = ServerStringNormalizer.NormalizeToUpperLat(word);
        wordLanguage = WordLanguageEnum.Mix;
      }
    }
    else
      word = ServerStringNormalizer.NormalizeToUpperLat(word);
    return wordLanguage;
  }

  public WordLanguageEnum GetIndexWords(string word, out string normalForm, out string stemmedForm)
  {
    WordLanguageEnum wordLanguage = this.GetWordLanguage(ref word);
    normalForm = word;
    IStemmer stemmer;
    switch (wordLanguage)
    {
      case WordLanguageEnum.English:
        stemmer = this._EngStemmer;
        break;
      case WordLanguageEnum.Russian:
        stemmer = this._RusStemmer;
        break;
      default:
        stemmedForm = word;
        return wordLanguage;
    }
    stemmedForm = stemmer.Stem(word).ToUpper();
    return wordLanguage;
  }

  public void ClearTrash(IDbManager db)
  {
    if (Interlocked.CompareExchange(ref this._QueueInProcess, 1, 0) != 0)
      return;
    try
    {
      db.SetAdminCommandTimeout();
      db.ExecuteNonQuery(sc_13035.ssp_appserver_13038());
      db.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE NOT EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_INDEX_RESULT.F_OBJECT_ID)");
      db.ExecuteNonQuery(sc_13035.ssp_appserver_13039());
      db.ExecuteNonQuery("DELETE FROM IMS_INDEX_WORDS WHERE NOT EXISTS(SELECT F_WORD_ID FROM IMS_INDEX_RESULT WHERE IMS_INDEX_WORDS.F_WORD_ID = IMS_INDEX_RESULT.F_WORD_ID)");
      db.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE NOT EXISTS(SELECT F_WORD_ID FROM IMS_INDEX_WORDS WHERE IMS_INDEX_WORDS.F_WORD_ID = IMS_INDEX_RESULT.F_WORD_ID)");
    }
    finally
    {
      db.SetNormalCommandTimeout();
      Interlocked.Exchange(ref this._QueueInProcess, 0);
    }
  }

  private void AddToQueue(IDbManager db, long objectID, int attrID, int inlistID)
  {
    if (db.DataProvider.Name == "Sql")
    {
      db.ExecuteNonQuery($"INSERT INTO IMS_INDEX_QUEUE (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) VALUES (:objID, :attrID, :inlistID, {db.DataProvider.Now})", db.Parameter("objID", (object) objectID), db.Parameter(nameof (attrID), (object) attrID), db.Parameter(nameof (inlistID), (object) inlistID));
    }
    else
    {
      long num = db.DataProvider.NextGeneratorValue("IMS_WORD_QUEUE_GEN", db);
      db.ExecuteNonQuery($"INSERT INTO IMS_INDEX_QUEUE (F_KEY, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) VALUES (:keyID, :objID, :attrID, :inlistID, {db.DataProvider.Now})", db.Parameter("keyID", (object) num), db.Parameter("objID", (object) objectID), db.Parameter(nameof (attrID), (object) attrID), db.Parameter(nameof (inlistID), (object) inlistID));
    }
  }

  public void AddToQueue(IDBAttributeType attrType)
  {
    UserSession userSession = (attrType as DBSessionable).UserSession;
    userSession.StartTransaction();
    try
    {
      if (attrType is IDBAttributeType4Object)
      {
        int[] objectTypesForModify = (attrType as DBAttributeType4Object).GetObjectTypesForModify();
        for (int index = 0; index < objectTypesForModify.Length; ++index)
        {
          string attributesTableName = userSession.DBCache.GetAttributesTableName(objectTypesForModify[index]);
          if (userSession.DataManager.DataProvider.Name == "Sql")
          {
            if (attributesTableName == "IMS_OBJECT_ATTRS")
              userSession.DataManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_INDEX_QUEUE (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) SELECT {1}.F_OBJECT_ID, :attrID, F_INLIST_ID, {0} FROM {1}, IMS_OBJECTS WHERE F_ATTRIBUTE_ID = :attrID AND IMS_OBJECTS.F_OBJECT_ID = {1}.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_TYPE = :objType", (object) userSession.DataManager.DataProvider.Now, (object) attributesTableName), userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID), userSession.DataManager.Parameter("objType", (object) objectTypesForModify[index]));
            else
              userSession.DataManager.ExecuteNonQuery($"INSERT INTO IMS_INDEX_QUEUE (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) SELECT F_OBJECT_ID, :attrID, F_INLIST_ID, {userSession.DataManager.DataProvider.Now} FROM {attributesTableName} WHERE F_ATTRIBUTE_ID = :attrID", userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID));
          }
          else if (attributesTableName == "IMS_OBJECT_ATTRS")
            userSession.DataManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_INDEX_QUEUE (F_KEY, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) SELECT {2}, {1}.F_OBJECT_ID, :attrID, F_INLIST_ID, {0} FROM {1}, IMS_OBJECTS WHERE F_ATTRIBUTE_ID = :attrID AND IMS_OBJECTS.F_OBJECT_ID = {1}.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_TYPE = :objType", (object) userSession.DataManager.DataProvider.Now, (object) attributesTableName, (object) userSession.DataManager.DataProvider.InsertGeneratorValueString("IMS_WORD_QUEUE_GEN")), userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID), userSession.DataManager.Parameter("objType", (object) objectTypesForModify[index]));
          else
            userSession.DataManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_INDEX_QUEUE (F_KEY, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) SELECT {2}, F_OBJECT_ID, :attrID, F_INLIST_ID, {0} FROM {1} WHERE F_ATTRIBUTE_ID = :attrID", (object) userSession.DataManager.DataProvider.Now, (object) attributesTableName, (object) userSession.DataManager.DataProvider.InsertGeneratorValueString("IMS_WORD_QUEUE_GEN")), userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID));
        }
      }
      else
      {
        List<string> objectAttrsTables = userSession.DBCache.GetObjectAttrsTables();
        for (int index = 0; index < objectAttrsTables.Count; ++index)
        {
          if (userSession.DataManager.DataProvider.Name == "Sql")
            userSession.DataManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_INDEX_QUEUE (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) SELECT {1}.F_OBJECT_ID, :attrID, F_INLIST_ID, {0} FROM {1}, IMS_OBJECTS WHERE F_ATTRIBUTE_ID = :attrID AND IMS_OBJECTS.F_OBJECT_ID = {1}.F_OBJECT_ID AND (NOT EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES WHERE IMS_ATTR4OBJ_TYPES.F_OBJECT_TYPE = IMS_OBJECTS.F_OBJECT_TYPE AND IMS_ATTR4OBJ_TYPES.F_ATTRIBUTE_ID = :attrID))", (object) userSession.DataManager.DataProvider.Now, (object) objectAttrsTables[index]), userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID));
          else
            userSession.DataManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_INDEX_QUEUE (F_KEY, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_MODIFY_DATE) SELECT {2}, {1}.F_OBJECT_ID, :attrID, F_INLIST_ID, {0} FROM {1}, IMS_OBJECTS WHERE F_ATTRIBUTE_ID = :attrID AND IMS_OBJECTS.F_OBJECT_ID = {1}.F_OBJECT_ID AND (NOT EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES WHERE IMS_ATTR4OBJ_TYPES.F_OBJECT_TYPE = IMS_OBJECTS.F_OBJECT_TYPE AND IMS_ATTR4OBJ_TYPES.F_ATTRIBUTE_ID = :attrID))", (object) userSession.DataManager.DataProvider.Now, (object) objectAttrsTables[index], (object) userSession.DataManager.DataProvider.InsertGeneratorValueString("IMS_WORD_QUEUE_GEN")), userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID));
        }
      }
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
  }

  public void DeleteFromIndex(IDBAttributeType attrType)
  {
    UserSession userSession = (attrType as DBSessionable).UserSession;
    userSession.StartTransaction();
    try
    {
      if (attrType is IDBAttributeType4Object)
      {
        int[] objectTypesForModify = (attrType as DBAttributeType4Object).GetObjectTypesForModify();
        for (int index = 0; index < objectTypesForModify.Length; ++index)
        {
          userSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_QUEUE WHERE F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_TYPE = :objType AND IMS_OBJECTS.F_OBJECT_ID = IMS_INDEX_QUEUE.F_OBJECT_ID)", userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID), userSession.DataManager.Parameter("objType", (object) objectTypesForModify[index]));
          userSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_TYPE = :objType AND IMS_OBJECTS.F_OBJECT_ID = IMS_GLOBAL_INDEX.F_OBJECT_ID)", userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID), userSession.DataManager.Parameter("objType", (object) objectTypesForModify[index]));
        }
      }
      else
      {
        userSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_QUEUE WHERE F_ATTRIBUTE_ID = :attrID AND (NOT EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES, IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_INDEX_QUEUE.F_OBJECT_ID AND IMS_ATTR4OBJ_TYPES.F_OBJECT_TYPE = IMS_OBJECTS.F_OBJECT_TYPE AND IMS_ATTR4OBJ_TYPES.F_ATTRIBUTE_ID = :attrID))", userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID));
        userSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_ATTRIBUTE_ID = :attrID AND (NOT EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES, IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_GLOBAL_INDEX.F_OBJECT_ID AND IMS_ATTR4OBJ_TYPES.F_OBJECT_TYPE = IMS_OBJECTS.F_OBJECT_TYPE AND IMS_ATTR4OBJ_TYPES.F_ATTRIBUTE_ID = :attrID))", userSession.DataManager.Parameter("attrID", (object) attrType.AttributeID));
      }
      userSession.DataManager.ExecuteNonQuery(sc_13035.ssp_appserver_13040());
      this.ComputeIndexedObjects(userSession.DataManager);
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
    }
  }

  public void ComputeRelevancy(IDbManager db)
  {
    this.ClearTrash(db);
    long indexedObjects = this.ComputeIndexedObjects(db);
    if (Interlocked.CompareExchange(ref this._QueueInProcess, 1, 0) != 0)
      return;
    try
    {
      db.SetAdminCommandTimeout();
      db.ExecuteNonQuery(sc_13035.ssp_appserver_13041());
      db.ExecuteNonQuery($"UPDATE IMS_INDEX_RESULT SET F_TF_IDF = {db.DataProvider.GetRoundSQL($"(SELECT MAX(F_TF) FROM IMS_GLOBAL_INDEX WHERE IMS_GLOBAL_INDEX.F_OBJECT_ID = IMS_INDEX_RESULT.F_OBJECT_ID AND IMS_GLOBAL_INDEX.F_WORD_ID = IMS_INDEX_RESULT.F_WORD_ID)*{db.DataProvider.Ln}(:allDocs/(SELECT F_OBJECT_COUNT FROM IMS_INDEX_WORDS WHERE IMS_INDEX_WORDS.F_WORD_ID = IMS_INDEX_RESULT.F_WORD_ID))", 12)}", db.Parameter("allDocs", (object) indexedObjects));
    }
    finally
    {
      db.SetNormalCommandTimeout();
      Interlocked.Exchange(ref this._QueueInProcess, 0);
    }
  }

  public void RegisterFileConverter(IIndexerFileConverter converter)
  {
    for (int index = 0; index < this._Converters.Count; ++index)
    {
      if (converter.Priority >= this._Converters[index].Priority)
      {
        this._Converters.Insert(index, converter);
        return;
      }
    }
    this._Converters.Add(converter);
  }

  private bool IndexByQueue(GlobalIndexService.AttributeInQueue attrKey)
  {
    bool flag = false;
    IDBObject dbObject = this._IndexerSession.GetObject(attrKey.ObjectID, false);
    if (dbObject == null && attrKey.ObjectID < 0L)
      dbObject = this._IndexerSession.GetObject(-attrKey.ObjectID, false);
    if (dbObject != null)
    {
      flag = dbObject.CheckoutBy != 0L;
      IDBAttribute attributeById = dbObject.GetAttributeByID(attrKey.AttributeID);
      if (attributeById != null && attrKey.InlistID >= 0)
      {
        this._IndexerSession.StartTransaction();
        try
        {
          if (attrKey.InlistID >= attributeById.ValuesCount)
          {
            this.IndexTextInternal(new IndexQueueProperties(attrKey.ObjectID, attrKey.AttributeID, attrKey.InlistID, dbObject.ID, string.Empty, attributeById.AttributeType.Options, attributeById.AttributeType.AttributeType), this._IndexerSession);
          }
          else
          {
            attributeById.Index = attrKey.InlistID;
            if (attributeById.IsNull)
              this.IndexTextInternal(new IndexQueueProperties(string.Empty, attributeById), this._IndexerSession);
            else if (attributeById.DataType == FieldTypes.ftFile)
              this.IndexFileAttribute(attributeById, this._IndexerSession);
            else if (attributeById.DataType == FieldTypes.ftObjectLink)
              this.IndexTextInternal(new IndexQueueProperties(attributeById.AsString, attributeById), this._IndexerSession);
            else
              this.IndexTextInternal(new IndexQueueProperties(attributeById.Value.ToString(), attributeById), this._IndexerSession);
          }
          this._IndexerSession.Commit();
        }
        catch (Exception ex)
        {
          this._IndexerSession.Rollback();
          this._IndexerSession.EventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("IndexingError"), (object) attributeById.Name, (object) dbObject.NameInMessages, (object) ex.Message), Consts.traceAlways, IndexerAttributeSettings.GlobalIndexLogFileName);
          this._IndexerSession.EventLog.AddToTrace(ex.StackTrace, Consts.traceAlways, IndexerAttributeSettings.GlobalIndexLogFileName);
        }
      }
    }
    return flag;
  }

  public bool ProcessQueue()
  {
    if (Interlocked.CompareExchange(ref this._QueueInProcess, 1, 0) == 0)
    {
label_1:
      try
      {
        object obj = this._IndexerSession.DataManager.ExecuteScalar("SELECT MIN(F_KEY) FROM IMS_INDEX_QUEUE");
        if (obj != null)
        {
          if (obj != DBNull.Value)
          {
            DataTable dataTable = this._IndexerSession.DataManager.ExecuteDataTable("SELECT F_KEY, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID FROM IMS_INDEX_QUEUE WHERE F_KEY < :maxKeyID", this._IndexerSession.DataManager.Parameter("maxKeyID", (object) (Convert.ToInt64(obj) + 100000L)));
            Dictionary<GlobalIndexService.AttributeInQueue, bool> dictionary = new Dictionary<GlobalIndexService.AttributeInQueue, bool>(dataTable.Rows.Count);
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              this._IndexerSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_QUEUE WHERE F_KEY = :keyID", this._IndexerSession.DataManager.Parameter("keyID", (object) Convert.ToInt64(dataTable.Rows[index][0])));
              GlobalIndexService.AttributeInQueue attributeInQueue = new GlobalIndexService.AttributeInQueue(Convert.ToInt64(dataTable.Rows[index][1]), Convert.ToInt32(dataTable.Rows[index][2]), Convert.ToInt32(dataTable.Rows[index][3]));
              if (!dictionary.ContainsKey(attributeInQueue))
              {
                dictionary.Add(attributeInQueue, true);
                if (this.IndexByQueue(attributeInQueue))
                {
                  attributeInQueue.ObjectID = -attributeInQueue.ObjectID;
                  this.IndexByQueue(attributeInQueue);
                }
              }
            }
            goto label_1;
          }
        }
      }
      finally
      {
        Interlocked.Exchange(ref this._QueueInProcess, 0);
      }
    }
    return true;
  }

  public void SaveSearchQuery(SearchQueryProperties query, IDbManager db)
  {
    if (query.QueryStr.Length <= 0)
      return;
    if (query.QueryStr[0] != '?')
    {
      string indexedString = ServerStringNormalizer.GetIndexedString(query.QueryStr);
      object obj = db.ExecuteScalar("SELECT F_QUERY_COUNTER FROM IMS_QUERIES_RESULT WHERE F_QUERY_STR = :qryStr", db.Parameter("qryStr", (object) query.QueryStr));
      if (obj == null || obj == DBNull.Value)
        db.ExecuteNonQuery("INSERT INTO IMS_QUERIES_RESULT (F_QUERY_STR, F_QUERY_NORM, F_QUERY_DATE, F_QUERY_COUNTER) VALUES (:qryStr, :qryNorm, :qryDate, 1)", db.Parameter("qryStr", (object) query.QueryStr), db.Parameter("qryNorm", (object) indexedString), db.Parameter("qryDate", (object) query.QueryTime));
      else
        db.ExecuteNonQuery("UPDATE IMS_QUERIES_RESULT SET F_QUERY_NORM = :qryNorm, F_QUERY_DATE = :qryDate, F_QUERY_COUNTER = :qryCnt WHERE F_QUERY_STR = :qryStr", db.Parameter("qryStr", (object) query.QueryStr), db.Parameter("qryNorm", (object) indexedString), db.Parameter("qryDate", (object) query.QueryTime), db.Parameter("qryCnt", (object) (Convert.ToInt32(obj) + 1)));
    }
    if (!this.IsSaveSearchQueryHistory)
      return;
    db.ExecuteNonQuery("INSERT INTO IMS_SEARCH_QUERIES (F_QUERY_STR, F_USER_ID, F_QUERY_DATE, F_ACCESS) VALUES (:qryStr, :usrID, :qryDate, :accessLevel)", db.Parameter("qryStr", (object) query.QueryStr), db.Parameter("usrID", (object) query.UserID), db.Parameter("qryDate", (object) query.QueryTime), db.Parameter("accessLevel", (object) query.AccessLevel));
  }

  private void IndexFileAttribute(IDBAttribute attr, UserSession session1)
  {
    if (attr.IsNull)
    {
      this.IndexTextInternal(new IndexQueueProperties(string.Empty, attr), session1);
    }
    else
    {
      bool flag = false;
      if (this._NotIndexingExtensionsList == null || this._NotIndexingExtensionsList.Count <= 1 || !this._NotIndexingExtensionsList.Contains(Path.GetExtension(attr.AsString).ToLower()))
      {
        for (int index = 0; index < this._Converters.Count; ++index)
        {
          if (this._Converters[index].CanGetPlainText(attr))
          {
            this.IndexTextInternal(new IndexQueueProperties($"{this._Converters[index].GetPlainText(attr)} {attr.AsString}", attr), session1);
            flag = true;
            break;
          }
        }
      }
      if (flag)
        return;
      this.IndexTextInternal(new IndexQueueProperties(attr.AsString, attr), session1);
    }
  }

  internal void CheckOutIndex(long objectID, UserSession session)
  {
    IDbManager dataManager = session.DataManager;
    dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_OBJECT_ID = :widPar", dataManager.Parameter("widPar", (object) -objectID));
    dataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE F_OBJECT_ID = :widPar", dataManager.Parameter("widPar", (object) -objectID));
    IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) objectID);
    dataManager.ExecuteNonQuery("INSERT INTO IMS_GLOBAL_INDEX (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_WORD_ID, F_ID, F_TF, F_WORD_REPEAT) SELECT -F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_WORD_ID, F_ID, F_TF, F_WORD_REPEAT FROM IMS_GLOBAL_INDEX WHERE F_OBJECT_ID = :objID", dbDataParameter);
    dataManager.ExecuteNonQuery("INSERT INTO IMS_INDEX_RESULT (F_OBJECT_ID, F_WORD_ID, F_TF_IDF) SELECT -F_OBJECT_ID, F_WORD_ID, F_TF_IDF FROM IMS_INDEX_RESULT WHERE F_OBJECT_ID = :objID", dbDataParameter);
  }

  internal void CheckInIndex(long objectID, UserSession session)
  {
    IDbManager dataManager = session.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) -objectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("wobjID", (object) objectID);
    dataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE F_OBJECT_ID = :objID", dbDataParameter1);
    dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_OBJECT_ID = :objID", dbDataParameter1);
    dataManager.ExecuteNonQuery("UPDATE IMS_INDEX_RESULT SET F_OBJECT_ID = :objID WHERE F_OBJECT_ID = :wobjID", dbDataParameter2, dbDataParameter1);
    dataManager.ExecuteNonQuery("UPDATE IMS_GLOBAL_INDEX SET F_OBJECT_ID = :objID WHERE F_OBJECT_ID = :wobjID", dbDataParameter2, dbDataParameter1);
  }

  public void GetIndexQueue(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    DataTable dataTable = sessionById.IsAdmin ? sessionById.DataManager.ExecuteDataTable(sc_13035.ssp_appserver_13043()) : throw new KernelExceptionID(sc_13035.ssp_appserver_13042(45892068));
    if (dataTable.Rows.Count > 0)
    {
      List<long> longList = new List<long>(dataTable.Rows.Count);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long num = Convert.ToInt64(dataTable.Rows[index][0]);
        if (Convert.ToInt64(dataTable.Rows[index][1]) == sessionById.UserID)
        {
          if (num > 0L)
            num = -num;
        }
        else if (num < 0L)
          num = -num;
        if (longList.IndexOf(num) < 0)
          longList.Add(num);
      }
      throw new ObjectsFoundException(string.Format(sc_13035.ssp_appserver_13044(), (object) longList.Count), "Объекты в очереди на индексацию", longList.ToArray());
    }
  }

  public int MinWordLength => this._MinWordLength;

  public void SetMinWordLength(Guid sessionGUID, int minLen)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13035.ssp_appserver_13045(1343757169));
    if (minLen == this._MinWordLength || minLen < 1)
      return;
    sessionById.Configurations.WriteInteger("KERNEL", "GLOBAL_INDEX", "MIN_WORD_LENGTH", (long) minLen, 0L);
    this._MinWordLength = minLen;
  }

  public long QueueLength
  {
    get
    {
      using (IDbManager dbManager = this._DbManagerService.CreateDbManager())
      {
        object obj = dbManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_INDEX_QUEUE");
        return obj != null && obj != DBNull.Value ? Convert.ToInt64(obj) : 0L;
      }
    }
  }

  public string[] ConvertersList
  {
    get
    {
      string[] convertersList = new string[this._Converters.Count];
      for (int index = 0; index < this._Converters.Count; ++index)
        convertersList[index] = this._Converters[index].Caption;
      return convertersList;
    }
  }

  public bool IsSaveSearchQueryHistory => this._SaveSearchQueryHistory;

  public void SetSaveSearchQueryHistoryMode(Guid sessionGUID, bool value)
  {
    if (this._SaveSearchQueryHistory == value)
      return;
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13035.ssp_appserver_13046(1597024947));
    sessionById.Configurations.WriteBool("KERNEL", "GLOBAL_INDEX", "SAVE_SEARCH_HISTORY", value, 0L);
    this._SaveSearchQueryHistory = value;
  }

  public string NotIndexingExtensions
  {
    get => this._NotIndexingExtensions;
    set
    {
      if (!(this._NotIndexingExtensions != value))
        return;
      List<string> stringList = new List<string>((IEnumerable<string>) value.ToLower().Split(','));
      for (int index = 0; index < stringList.Count; ++index)
      {
        stringList[index] = stringList[index].Trim();
        if (stringList[index].Length > 0 && stringList[index][0] != '.')
          stringList[index] = "." + stringList[index];
      }
      this._NotIndexingExtensionsList = stringList;
      this._NotIndexingExtensions = value;
    }
  }

  public void SetNotIndexingExtensions(Guid sessionGUID, string value)
  {
    if (!(this.NotIndexingExtensions != value))
      return;
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13035.ssp_appserver_13047(1238662457));
    this.NotIndexingExtensions = value;
    sessionById.Configurations.WriteString("KERNEL", "GLOBAL_INDEX", "NOT_INDEX_EXT", value, 0L);
  }

  public int AddToQueue(Guid sessionGuid, long[] objectsID)
  {
    UserSession session = UserSession.GetSessionByID(sessionGuid) as UserSession;
    if (!session.IsAdmin)
      throw new KernelExceptionID(sc_13035.ssp_appserver_13048(1296491264));
    int result = 0;
    for (int index = 0; index < objectsID.Length; ++index)
    {
      IDBObject dbObject1 = session.GetObject(objectsID[index], false);
      if (dbObject1 != null)
      {
        AddObjectToQueue(dbObject1);
        if (dbObject1.CheckoutBy != 0L)
        {
          IDBObject dbObject2 = session.GetObject(-objectsID[index], false);
          if (dbObject2 != null)
            AddObjectToQueue(dbObject2);
        }
      }
    }
    return result;

    void AddObjectToQueue(IDBObject obj)
    {
      for (int AttrIndex = 0; AttrIndex < obj.Attributes.Count; ++AttrIndex)
      {
        IDBAttribute attribute = obj.Attributes[AttrIndex];
        if ((attribute.AttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
        {
          for (int inlistID = 0; inlistID < attribute.ValuesCount; ++inlistID)
            this.AddToQueue(session.DataManager, obj.ObjectID, attribute.AttributeID, inlistID);
          result++;
        }
      }
    }
  }

  public string[] GetSimilarQueries(Guid sessionGuid, string beginStr, int maxStrings)
  {
    if (beginStr.Length <= 0)
      return new string[0];
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    string indexedString = ServerStringNormalizer.GetIndexedString(beginStr);
    DataTable dataTable = sessionById.DataManager.ExecuteDataTable("SELECT F_QUERY_STR FROM IMS_QUERIES_RESULT WHERE F_QUERY_NORM LIKE :qryStr ORDER BY F_QUERY_COUNTER DESC, F_QUERY_DATE DESC", sessionById.DataManager.Parameter("qryStr", (object) (indexedString + "%")));
    if (dataTable.Rows.Count < maxStrings)
      maxStrings = dataTable.Rows.Count;
    List<string> stringList = new List<string>(maxStrings);
    for (int index = 0; index < maxStrings; ++index)
      stringList.Add(dataTable.Rows[index][0].ToString());
    return stringList.ToArray();
  }

  public DataTable GetQueriesHistory(Guid sessionGuid)
  {
    return this.GetQueriesHistory(sessionGuid, -1L, DateTime.MinValue, DateTime.MaxValue);
  }

  public DataTable GetQueriesHistory(
    Guid sessionGuid,
    long userID,
    DateTime beginDate,
    DateTime endDate)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    if (sessionById.UserID != userID && !sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13035.ssp_appserver_13049(856052543));
    IDbManager dataManager = sessionById.DataManager;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(4);
      dbDataParameterList.Add(dataManager.Parameter("levelID", (object) sessionById.SecurityLevel));
      if (userID > 0L)
      {
        stringBuilder.Append(" AND F_USER_ID = :userID");
        dbDataParameterList.Add(dataManager.Parameter(nameof (userID), (object) userID));
      }
      if (beginDate != DateTime.MinValue)
      {
        stringBuilder.Append(" AND F_QUERY_DATE >= :beginDate");
        dbDataParameterList.Add(dataManager.Parameter(nameof (beginDate), (object) beginDate.Date));
      }
      if (endDate != DateTime.MaxValue)
      {
        stringBuilder.Append(" AND F_QUERY_DATE < :endDate");
        dbDataParameterList.Add(dataManager.Parameter(nameof (endDate), (object) (endDate.Date + TimeSpan.FromDays(1.0))));
      }
      return sessionById.DataManager.ExecuteDataTable(string.Format(sc_13035.ssp_appserver_13050(), (object) stringBuilder.ToString()), dbDataParameterList.ToArray());
    }
  }

  private class AttributeInQueue
  {
    public long ObjectID;
    public int AttributeID;
    public int InlistID;

    public AttributeInQueue(long objectID, int attributeID, int inlistID)
    {
      this.ObjectID = objectID;
      this.AttributeID = attributeID;
      this.InlistID = inlistID;
    }

    public override int GetHashCode()
    {
      return this.ObjectID.GetHashCode() << 16 /*0x10*/ ^ this.AttributeID.GetHashCode() << 8 ^ this.InlistID.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is GlobalIndexService.AttributeInQueue))
        return base.Equals(obj);
      GlobalIndexService.AttributeInQueue attributeInQueue = obj as GlobalIndexService.AttributeInQueue;
      return this.ObjectID == attributeInQueue.ObjectID && this.AttributeID == attributeInQueue.AttributeID && this.InlistID == attributeInQueue.InlistID;
    }
  }
}

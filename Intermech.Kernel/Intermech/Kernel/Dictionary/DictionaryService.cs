// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Dictionary.DictionaryService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Dictionary;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Dictionary;

internal sealed class DictionaryService : 
  LongLifeObject,
  IDictionaryServerService,
  IDictionaryService
{
  private readonly System.Collections.Generic.Dictionary<string, LangHelper> _id2LangCache = new System.Collections.Generic.Dictionary<string, LangHelper>();

  private LangHelper LoadLanguage(string id, string name, int def, IUserSession session)
  {
    if (this._id2LangCache.ContainsKey(id))
      return this._id2LangCache[id];
    LangHelper[] langHelperArray = this.LoadLanguages(session.SessionGUID);
    LangHelper langHelper1 = (LangHelper) null;
    if (langHelperArray != null)
    {
      foreach (LangHelper langHelper2 in langHelperArray)
      {
        if (langHelper2 != null && langHelper2.ID == id)
        {
          langHelper1 = langHelper2;
          break;
        }
      }
    }
    if (langHelper1 == null)
      langHelper1 = new LangHelper(id, name, def);
    this._id2LangCache.Add(id, langHelper1);
    return langHelper1;
  }

  private void SaveLanguages(LangHelper[] langHelpers, bool appendMode, Guid session)
  {
    IUserSession sessionById = UserSession.GetSessionByID(session);
    if (sessionById == null)
      return;
    System.Collections.Generic.Dictionary<string, LangHelper> dictionary = new System.Collections.Generic.Dictionary<string, LangHelper>();
    if (appendMode)
    {
      LangHelper[] langHelperArray = this.LoadLanguages(session);
      if (langHelperArray != null && langHelperArray.Length != 0)
      {
        foreach (LangHelper langHelper in langHelperArray)
        {
          if (langHelper != null)
            dictionary.Add(langHelper.ID, langHelper);
        }
      }
    }
    foreach (LangHelper langHelper in langHelpers)
    {
      if (langHelper != null)
      {
        if (!dictionary.ContainsKey(langHelper.ID))
          dictionary.Add(langHelper.ID, langHelper);
        else
          dictionary[langHelper.ID] = langHelper;
        if (this._id2LangCache.ContainsKey(langHelper.ID))
          this._id2LangCache[langHelper.ID] = langHelper;
      }
    }
    IDBConfigurations configurations = sessionById.Configurations;
    if (configurations == null)
      return;
    List<LangHelper> graph = new List<LangHelper>((IEnumerable<LangHelper>) dictionary.Values);
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) graph);
      BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, DictionaryServiceHolder.DictListHeader, ArcMethods.NotPacked, string.Empty);
      configurations.WriteConfigData(config_info, serializationStream.ToArray());
    }
  }

  public void SaveLanguages(LangHelper[] langHelpers, Guid session)
  {
    this.SaveLanguages(langHelpers, true, session);
  }

  public LangHelper[] LoadLanguages(Guid session)
  {
    IUserSession userSession = UserSession.GetSessionByID(session);
    List<LangHelper> langHelperList1 = new List<LangHelper>();
    bool flag = false;
    try
    {
      if (userSession == null)
      {
        userSession = ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, true).GetSystemSessionTemporaryClone(nameof (LoadLanguages));
        flag = true;
      }
      IDBConfigurations configurations = userSession.Configurations;
      if (configurations == null)
        return langHelperList1.ToArray();
      BlobInformation config_info;
      byte[] config_file;
      configurations.LoadConfigData(DictionaryServiceHolder.DictListHeader, out config_info, out config_file);
      if (config_info.RealFileSize > 0L && config_file.Length != 0)
      {
        List<LangHelper> langHelperList2 = (List<LangHelper>) null;
        using (MemoryStream serializationStream = new MemoryStream(config_file))
        {
          try
          {
            langHelperList2 = new BinaryFormatter().Deserialize((Stream) serializationStream) as List<LangHelper>;
          }
          catch
          {
          }
        }
        System.Collections.Generic.Dictionary<string, LangHelper> dictionary = new System.Collections.Generic.Dictionary<string, LangHelper>();
        if (langHelperList2 != null && langHelperList2.Count != 0)
        {
          foreach (LangHelper langHelper in langHelperList2)
          {
            if (langHelper != null)
              dictionary.Add(langHelper.ID, langHelper);
          }
        }
        IDBLanguageCollection languageCollection = userSession.GetLanguageCollection();
        if (languageCollection != null)
        {
          DataTable dataTable = languageCollection.Select(string.Empty);
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              string str = Convert.ToString(row["F_LANGUAGE_ID"]);
              if (!dictionary.ContainsKey(str))
              {
                string name = Convert.ToString(row["F_LANGUAGE_NAME"]);
                int int32 = Convert.ToInt32(row["F_DEFAULT"]);
                dictionary.Add(str, new LangHelper(str, name, int32));
              }
            }
          }
        }
        langHelperList1.AddRange((IEnumerable<LangHelper>) dictionary.Values);
      }
      else
      {
        IDBLanguageCollection languageCollection = userSession.GetLanguageCollection();
        if (languageCollection != null)
        {
          DataTable dataTable = languageCollection.Select(string.Empty);
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              string id = Convert.ToString(row["F_LANGUAGE_ID"]);
              string name = Convert.ToString(row["F_LANGUAGE_NAME"]);
              int int32 = Convert.ToInt32(row["F_DEFAULT"]);
              LangHelper langHelper1 = new LangHelper(id, name, int32);
              langHelperList1.Add(langHelper1);
              IDBAttribute configAttribute = configurations.GetConfigAttribute(DictionaryServiceHolder.DictHeader + id);
              if (configAttribute != null)
              {
                configurations.LoadConfigData(DictionaryServiceHolder.DictHeader + id, out config_info, out config_file);
                if (config_info.RealFileSize > 0L && config_file.Length != 0)
                {
                  using (MemoryStream serializationStream = new MemoryStream(config_file))
                  {
                    try
                    {
                      LangHelper langHelper2 = new BinaryFormatter().Deserialize((Stream) serializationStream) as LangHelper;
                      langHelper1.Words.AddRange((IEnumerable<DictWord>) langHelper2.Words);
                    }
                    catch
                    {
                    }
                  }
                }
                configAttribute.Delete(0L);
              }
            }
            this.SaveLanguages(langHelperList1.ToArray(), false, userSession.SessionGUID);
          }
        }
      }
    }
    finally
    {
      if (flag && userSession != null)
        userSession.Logout(nameof (LoadLanguages));
    }
    return langHelperList1.ToArray();
  }

  public string GetDescription(IDBAttribute attr)
  {
    if (attr == null)
      return string.Empty;
    if (attr.AttributeType.AttributeType != FieldTypes.ftString && attr.AttributeType.AttributeType != FieldTypes.ftMemo && attr.AttributeType.AttributeType != FieldTypes.ftShortBlob)
      throw new Exception(LocalizationHolder.rm.GetString(sc_12993.ssp_appserver_12994()));
    string description = Convert.ToString(attr.Value);
    if (!(attr is DBAttribute dbAttribute1))
      return description;
    IUserSession session = dbAttribute1.GetSession();
    if (!(dbAttribute1.ParentObject is IDBAttributable parentObject))
      return description;
    string str1 = MetaDataHelper.GetAttributeType(attr.AttributeType.AttributeID).LanguageID;
    if (str1.Equals(string.Empty) || str1.Equals(" "))
      str1 = session.IdentHelper.DefaultLanguageID;
    LangHelper lang;
    if (!this._id2LangCache.ContainsKey(str1))
    {
      IDBLanguageType language = session.GetLanguage(str1);
      lang = this.LoadLanguage(language.LanguageID, language.LanguageName, language.IsDefaultLanguage ? 1 : 0, session);
    }
    else
      lang = this._id2LangCache[str1];
    try
    {
      string str2 = string.Empty;
      while (description.IndexOf("[", StringComparison.Ordinal) >= 0)
      {
        int length1 = description.IndexOf("[", StringComparison.Ordinal);
        str2 += description.Substring(0, length1);
        description = description.Remove(0, length1 + 1);
        int length2 = description.IndexOf(":", StringComparison.Ordinal);
        int length3 = description.IndexOf("]", StringComparison.Ordinal);
        if (length3 >= 0)
        {
          if (length2 >= 0 && length2 < length3)
          {
            string attributeName = description.Substring(0, length2);
            string str3 = description.Remove(0, length2 + 1);
            int length4 = length3 - (length2 + 1);
            string wordName = str3.Substring(0, length4);
            description = str3.Remove(0, length4 + 1);
            IDBAttribute dbAttribute2 = (IDBAttribute) null;
            int attributeId;
            if (this.FindAttributeID(session, attributeName, out attributeId))
              dbAttribute2 = parentObject.GetAttributeByID(attributeId);
            if (dbAttribute2 != null)
            {
              Type type = dbAttribute2.Value.GetType();
              if (!(type == typeof (long)) && !(type == typeof (double)))
                throw new Exception(LocalizationHolder.rm.GetString(sc_12993.ssp_appserver_12995()));
              string empty = string.Empty;
              if (type == typeof (long))
                empty = ExtFinder.GetString(dbAttribute2.AsInteger, lang, wordName);
              else if (type == typeof (double))
                empty = ExtFinder.GetString(Convert.ToInt64(Math.Floor(dbAttribute2.AsDouble)), lang, wordName);
              str2 = str2 + dbAttribute2.AsString + (empty.Length > 0 ? " " + empty : "");
            }
            else
              str2 += ExtFinder.GetString(1L, lang, wordName);
          }
          else
          {
            string attributeName = description.Substring(0, length3);
            description = description.Remove(0, length3 + 1);
            IDBAttribute dbAttribute3 = (IDBAttribute) null;
            int attributeId;
            if (this.FindAttributeID(session, attributeName, out attributeId))
              dbAttribute3 = parentObject.GetAttributeByID(attributeId);
            if (dbAttribute3 != null)
              str2 += dbAttribute3.AsString;
          }
        }
        else
          break;
      }
      return str2 + description;
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_12993.ssp_appserver_12996()), (object) Convert.ToString(attr.Value), (object) attr.AttributeID, (object) attr.DBObjectID), ex);
    }
  }

  public bool IsAttributeExistsInValue(IDBAttributeType attrType, string formula)
  {
    if (attrType == null)
      return false;
    string str = formula;
    return str.IndexOf($"[{attrType.Alias}]", StringComparison.Ordinal) > -1 || str.IndexOf($"[{attrType.ShortName}]", StringComparison.Ordinal) > -1;
  }

  public List<AttributeValues> ParseAttributes(
    Guid sessionGuid,
    List<AttributeValues> parseList,
    System.Collections.Generic.Dictionary<string, AttributeValues> forParseDict)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById != null && parseList != null && parseList.Count > 0)
    {
      List<string> lockedAttrs = new List<string>();
      System.Collections.Generic.Dictionary<string, string> parsedAttrs = new System.Collections.Generic.Dictionary<string, string>();
      foreach (AttributeValues parse in parseList)
      {
        string av;
        if (!string.IsNullOrEmpty(parse.AttributeAlias) && parsedAttrs.ContainsKey(parse.AttributeAlias))
        {
          av = parsedAttrs[parse.AttributeAlias];
        }
        else
        {
          bool hasProplem;
          av = this.ParseAV(sessionById, parse, forParseDict, out hasProplem, lockedAttrs, parsedAttrs);
          if (!string.IsNullOrEmpty(parse.AttributeAlias) && !hasProplem)
            parsedAttrs.Add(parse.AttributeAlias, av);
        }
        parse.Descriptions = new object[1]{ (object) av };
      }
    }
    return parseList;
  }

  private List<string> GetAliasesFromString(string value)
  {
    List<string> aliasesFromString = (List<string>) null;
    List<string> list = ((IEnumerable<string>) value.Split('[')).Where<string>((System.Func<string, bool>) (str => str.Contains<char>(']'))).ToList<string>();
    if (list.Count > 0)
    {
      aliasesFromString = new List<string>(list.Count);
      foreach (string str1 in list)
      {
        string str2 = str1.Substring(0, str1.IndexOf(']'));
        if (!aliasesFromString.Contains(str2))
          aliasesFromString.Add(str2);
      }
    }
    return aliasesFromString;
  }

  private string GetValue(IUserSession userSession, AttributeValues av)
  {
    string empty = string.Empty;
    if (av?.Values == null || av.Values.Length == 0)
      return empty;
    string s = Convert.ToString(av.Values[0]);
    long result;
    if (av.AttributeType != FieldTypes.ftObjectLink || !long.TryParse(s, out result))
      return s;
    QuickObjectInfo objectInfo = userSession.GetObjectInfo(result);
    s = !objectInfo.Empty ? objectInfo.Caption : string.Empty;
    return s;
  }

  private string ParseAV(
    IUserSession userSession,
    AttributeValues av,
    System.Collections.Generic.Dictionary<string, AttributeValues> forParseDict,
    out bool hasProplem,
    List<string> lockedAttrs,
    System.Collections.Generic.Dictionary<string, string> parsedAttrs)
  {
    string av1 = this.GetValue(userSession, av);
    List<string> aliasesFromString = this.GetAliasesFromString(av1);
    hasProplem = false;
    if (aliasesFromString != null)
    {
      foreach (string key in aliasesFromString)
      {
        if (forParseDict.ContainsKey(key))
        {
          if (lockedAttrs.Contains(key))
            hasProplem = true;
          else if (parsedAttrs.ContainsKey(key))
          {
            av1 = av1.Replace($"[{key}]", parsedAttrs[key]);
          }
          else
          {
            lockedAttrs.Add(av.AttributeAlias);
            string av2 = this.ParseAV(userSession, forParseDict[key], forParseDict, out hasProplem, lockedAttrs, parsedAttrs);
            lockedAttrs.Remove(av.AttributeAlias);
            if (!hasProplem)
            {
              av1 = av1.Replace($"[{key}]", av2);
              parsedAttrs.Add(key, av2);
            }
          }
        }
      }
    }
    return av1;
  }

  private bool FindAttributeID(IUserSession session, string attributeName, out int attributeId)
  {
    attributeId = 0;
    DataTable table = ServiceUtils.GetService<ICacheDataset>((object) ApplicationServices.Container, false)?.GetTable("IMS_ATTRIBUTES");
    if (table == null)
      return false;
    string filterExpression = $"F_ALIAS = '{attributeName}' or F_SHORT_NAME = '{attributeName}'";
    string sort = "F_ALIAS ASC";
    DataRow[] dataRowArray = table.Select(filterExpression, sort);
    if (dataRowArray.Length == 0)
      return false;
    int num = -1;
    foreach (DataRow dataRow in dataRowArray)
    {
      string str1 = dataRow["F_ALIAS"].ToString();
      string str2 = dataRow["F_SHORT_NAME"].ToString();
      if (attributeName.Equals(str1))
      {
        attributeId = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
        return true;
      }
      if (num.Equals(-1) && attributeName.Equals(str2))
        num = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
    }
    if (num == 0)
      return false;
    attributeId = num;
    return true;
  }
}

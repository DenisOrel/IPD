
// Type: Intermech.Controls.SpellCheck.WordDict
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Controls.SpellCheck;

public class WordDict
{
  private bool dictLoaded;
  private string dictFName;
  private Hashtable _baseWords;
  private bool _nosuggest;
  private Struct.PhoneticRuleCollection _phoneticRules;
  private ArrayList _possibleBaseWords;
  private Struct.AffixRuleCollection _prefixRules;
  private ArrayList _replaceCharacters;
  private Struct.AffixRuleCollection _suffixRules;
  private string _tryCharacters;
  private Hashtable _UserWords;
  private string DictonaryPathFile;

  public Hashtable UserWords
  {
    get => this._UserWords;
    set => this._UserWords = value;
  }

  public WordDict()
  {
    this.DictonaryPathFile = "";
    this._UserWords = new Hashtable();
    this._baseWords = new Hashtable();
    this._tryCharacters = "";
    this._prefixRules = new Struct.AffixRuleCollection();
    this._suffixRules = new Struct.AffixRuleCollection();
    this._possibleBaseWords = new ArrayList();
    this._phoneticRules = new Struct.PhoneticRuleCollection();
    this._replaceCharacters = new ArrayList();
    this._nosuggest = false;
  }

  public WordDict(string DictFname)
  {
    this.DictonaryPathFile = "";
    this._UserWords = new Hashtable();
    this._baseWords = new Hashtable();
    this._tryCharacters = "";
    this._prefixRules = new Struct.AffixRuleCollection();
    this._suffixRules = new Struct.AffixRuleCollection();
    this._possibleBaseWords = new ArrayList();
    this._phoneticRules = new Struct.PhoneticRuleCollection();
    this._replaceCharacters = new ArrayList();
    this._nosuggest = false;
    this.dictFName = DictFname;
  }

  public Struct.TestResult Contains(string word)
  {
    if (!this.dictLoaded)
      this.LoadDictonary(this.dictFName);
    word = word.ToLower().Trim();
    foreach (char ch in word)
    {
      if (this._tryCharacters.IndexOf(ch) < 0)
        return Struct.TestResult.WordHasNoLetterSymbol;
    }
    if (Struct._htmlRegex.IsMatch(word))
      return Struct.TestResult.isHtmlTag;
    this._possibleBaseWords.Clear();
    if (this._UserWords.ContainsKey((object) word))
      return Struct.TestResult.WordInUserDict;
    if (this._baseWords.ContainsKey((object) word))
      return Struct.TestResult.WordInBaseDict;
    ArrayList c = new ArrayList();
    c.Add((object) word);
    foreach (Struct.AffixRule affixRule in this._suffixRules.Values)
    {
      foreach (Struct.AffixEntry affixEntry in (List<Struct.AffixEntry>) affixRule.AffixEntries)
      {
        string str = Utility.RemoveSuffix(word, affixEntry);
        if (str != word)
        {
          if (this._baseWords.Contains((object) str) && this.VerifyAffixKey(str, affixRule.Name[0]))
            return Struct.TestResult.WordInBaseDict;
          if (affixRule.AllowCombine)
            c.Add((object) str);
          else
            this._possibleBaseWords.Add((object) str);
        }
      }
    }
    this._possibleBaseWords.AddRange((ICollection) c);
    foreach (Struct.AffixRule affixRule in this._prefixRules.Values)
    {
      foreach (Struct.AffixEntry affixEntry in (List<Struct.AffixEntry>) affixRule.AffixEntries)
      {
        foreach (string word1 in c)
        {
          string str = Utility.RemovePrefix(word1, affixEntry);
          if (str != word1)
          {
            if (this._baseWords.Contains((object) str) && this.VerifyAffixKey(str, affixRule.Name[0]))
              return Struct.TestResult.WordInBaseDict;
            this._possibleBaseWords.Add((object) str);
          }
        }
      }
    }
    return Struct.TestResult.UnknownWord;
  }

  private string GetCodepage(string DictFname)
  {
    string codepage = "";
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.Controls.SpellCheck.ru_RU.aff"))
    {
      using (StreamReader streamReader = new StreamReader(manifestResourceStream, Encoding.Default))
      {
        while (streamReader.Peek() >= 0)
        {
          string input = streamReader.ReadLine().Trim();
          if (input.Length > 0)
          {
            MatchCollection matchCollection = Struct._spaceRegx.Matches(input);
            if (matchCollection[0].Value.ToLower() == "set")
            {
              codepage = matchCollection[1].Value.ToLower();
              break;
            }
          }
        }
      }
    }
    return codepage;
  }

  public void LoadDictonary(string DictFname1)
  {
    this.UserFileLoadDB(this.UserWords);
    this.dictLoaded = true;
    this.DictonaryPathFile = Path.ChangeExtension(DictFname1, "");
    this._baseWords.Clear();
    this._suffixRules.Clear();
    this._prefixRules.Clear();
    this._tryCharacters = "";
    Regex regex = new Regex("[^\\s]+", RegexOptions.Compiled);
    Struct.AffixRule currentRule = (Struct.AffixRule) null;
    string codepage = this.GetCodepage(this.DictonaryPathFile);
    if (SessionKeeper.CurrentAllocator != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        Stream stream1 = (Stream) null;
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cadd9bd2-306c-11d8-b4e9-00304f19f545"));
        if (dbObject != null)
        {
          BlobReaderStream blobReaderStream = new BlobReaderStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 0, 0, sessionKeeper.Session);
          if (blobReaderStream.BlobInformation.FileName == null || !blobReaderStream.BlobInformation.FileName.EndsWith("aff"))
          {
            blobReaderStream.Dispose();
            BlobInformation info = new BlobInformation(0L, 0L, DateTime.Now, "russian-aot-ieyo.aff", ArcMethods.NotPacked, string.Empty);
            using (BlobWriterStream destination = new BlobWriterStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 0, 0, info, sessionKeeper.Session))
            {
              using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.Controls.SpellCheck.russian-aot-ieyo.aff"))
                manifestResourceStream.CopyTo((Stream) destination);
              destination.Commit();
            }
            blobReaderStream = new BlobReaderStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 0, 0, sessionKeeper.Session);
          }
          stream1 = (Stream) blobReaderStream;
        }
        this.LoadAffDictionary(stream1, currentRule, codepage);
        Stream stream2 = (Stream) null;
        if (dbObject != null)
        {
          BlobReaderStream blobReaderStream = new BlobReaderStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 1, 0, sessionKeeper.Session);
          if (blobReaderStream.BlobInformation.FileName == null || !blobReaderStream.BlobInformation.FileName.EndsWith("dic"))
          {
            blobReaderStream.Dispose();
            BlobInformation info = new BlobInformation(0L, 0L, DateTime.Now, "russian-aot-ieyo.dic", ArcMethods.NotPacked, string.Empty);
            using (BlobWriterStream destination = new BlobWriterStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 1, 0, info, sessionKeeper.Session))
            {
              using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.Controls.SpellCheck.russian-aot-ieyo.dic"))
                manifestResourceStream.CopyTo((Stream) destination);
              destination.Commit();
            }
            blobReaderStream = new BlobReaderStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 1, 0, sessionKeeper.Session);
          }
          stream2 = (Stream) blobReaderStream;
        }
        this.LoadDicDictionary(stream2, codepage);
      }
    }
    else
    {
      this.LoadAffDictionary(this.GetType().Assembly.GetManifestResourceStream("Intermech.Controls.SpellCheck.russian-aot-ieyo.aff"), currentRule, codepage);
      this.LoadDicDictionary(this.GetType().Assembly.GetManifestResourceStream("Intermech.Controls.SpellCheck.russian-aot-ieyo.dic"), codepage);
    }
  }

  private void LoadAffDictionary(Stream stream, Struct.AffixRule currentRule, string codepage)
  {
    Regex regex = new Regex("[^\\s]+", RegexOptions.Compiled);
    int num = 0;
    using (stream)
    {
      using (StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding(codepage)))
      {
        while (streamReader.Peek() >= 0)
        {
          string input = streamReader.ReadLine().Trim();
          ++num;
          if (input.Length > 0)
          {
            MatchCollection matchCollection = regex.Matches(input);
            if (!(matchCollection[0].Value == "#"))
            {
              switch (matchCollection.Count)
              {
                case 2:
                  string lower1 = matchCollection[0].Value.ToLower();
                  switch (lower1)
                  {
                    case "set":
                      continue;
                    case "try":
                      this._tryCharacters = matchCollection[1].Value;
                      break;
                  }
                  if (lower1 == "nosuggest" && matchCollection[0].Value == "!")
                  {
                    this._nosuggest = true;
                    continue;
                  }
                  continue;
                case 3:
                  if (matchCollection[0].Value.ToLower() == "rep")
                  {
                    this._replaceCharacters.Add((object) input.Substring(4).Trim());
                    continue;
                  }
                  continue;
                case 4:
                  string lower2 = matchCollection[0].Value.ToLower();
                  if ((lower2 == "sfx" || lower2 == "pfx" ? 1 : 0) != 0)
                  {
                    currentRule = new Struct.AffixRule();
                    currentRule.Name = matchCollection[1].Value;
                    if (matchCollection[2].Value == "Y")
                      currentRule.AllowCombine = true;
                    if (matchCollection[0].Value.ToLower() == "sfx")
                    {
                      if (!this._suffixRules.ContainsKey(currentRule.Name))
                      {
                        this._suffixRules.Add(currentRule.Name, currentRule);
                        continue;
                      }
                      continue;
                    }
                    this._prefixRules.Add(currentRule.Name, currentRule);
                    continue;
                  }
                  continue;
                case 5:
                  string lower3 = matchCollection[0].Value.ToLower();
                  if ((lower3 == "sfx" || lower3 == "pfx" ? 1 : 0) != 0 && currentRule.Name == matchCollection[1].Value)
                  {
                    Struct.AffixEntry entry = new Struct.AffixEntry();
                    if (matchCollection[2].Value != "0")
                      entry.StripCharacters = matchCollection[2].Value;
                    entry.AddCharacters = matchCollection[3].Value;
                    Utility.EncodeConditions(matchCollection[4].Value, entry);
                    currentRule.AffixEntries.Add(entry);
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }
  }

  private void LoadDicDictionary(Stream stream, string codepage)
  {
    using (stream)
    {
      using (StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding(codepage)))
      {
        while (streamReader.Peek() >= 0)
        {
          string str = streamReader.ReadLine().Trim();
          if (str.Length > 0)
          {
            string[] strArray = str.Split('/');
            Struct.Word word = new Struct.Word();
            word.text = strArray[0].ToLower().Trim();
            if (strArray.Length >= 2)
              word.AffixKeys = strArray[1];
            if (strArray.Length >= 3)
              word.PhoneticCode = strArray[2];
            if (!this._baseWords.ContainsKey((object) word.text))
              this._baseWords.Add((object) word.text, (object) word);
          }
        }
      }
    }
  }

  public void UserFileRemove(string word)
  {
    if (!this._UserWords.ContainsKey((object) word))
      return;
    this._UserWords.Remove((object) word);
  }

  public void UserFileAdd(string word)
  {
    this._UserWords.Add((object) word, (object) word);
    this.UserFileSave(this._UserWords, false);
  }

  public void UserFileClear() => this._UserWords.Clear();

  public Hashtable UserFileLoad(string dictionary)
  {
    Hashtable hashtable = new Hashtable();
    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(dictionary)))
    {
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream, Encoding.Default))
      {
        while (streamReader.Peek() >= 0)
        {
          string str = streamReader.ReadLine().Trim();
          if (str.Length > 0 && str.Length < 200)
            hashtable[(object) str.ToLower()] = (object) null;
        }
      }
    }
    return hashtable;
  }

  public void UserFileLoadDB(Hashtable dict)
  {
    try
    {
      if (SessionKeeper.CurrentAllocator == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cadd9bd4-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject == null)
          return;
        using (BlobReaderStream blobReaderStream = new BlobReaderStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 0, 0, sessionKeeper.Session))
        {
          if (blobReaderStream.BlobInformation.FileName == null || !blobReaderStream.BlobInformation.FileName.EndsWith("userdict"))
            return;
          MemoryStream destination = new MemoryStream();
          blobReaderStream.CopyTo((Stream) destination);
          destination.Position = 0L;
          using (StreamReader streamReader = new StreamReader((Stream) destination, Encoding.Default))
          {
            while (!streamReader.EndOfStream)
            {
              string str = streamReader.ReadLine().Trim();
              if (str.Length > 0 && str.Length < 200)
                dict[(object) str.ToLower()] = (object) null;
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
  }

  public string UserFileSave(Hashtable hash, bool ignoreDB)
  {
    List<string> stringList = new List<string>();
    foreach (object key in (IEnumerable) hash.Keys)
    {
      string str = Convert.ToString(key);
      if (!string.IsNullOrEmpty(str))
        stringList.Add(str);
    }
    if (!ignoreDB)
    {
      Hashtable dict = new Hashtable();
      this.UserFileLoadDB(dict);
      foreach (object key in (IEnumerable) dict.Keys)
      {
        string str = Convert.ToString(key);
        if (!string.IsNullOrEmpty(str))
          stringList.Add(str);
      }
    }
    stringList.Sort();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cadd9bd4-306c-11d8-b4e9-00304f19f545"), false);
      if (dbObject != null)
      {
        BlobInformation info = new BlobInformation(0L, 0L, DateTime.Now, "dictionary.userdict", ArcMethods.NotPacked, string.Empty);
        using (BlobWriterStream blobWriterStream = new BlobWriterStream(dbObject.ObjectID, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) "cad0004b-306c-11d8-b4e9-00304f19f545"), 0, 0, info, sessionKeeper.Session))
        {
          using (StreamWriter streamWriter = new StreamWriter((Stream) blobWriterStream, Encoding.Default))
          {
            streamWriter.NewLine = "\n";
            foreach (string str in stringList)
              streamWriter.WriteLine(str);
            streamWriter.Flush();
            blobWriterStream.Commit();
          }
        }
      }
    }
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream, Encoding.Default))
      {
        streamWriter.NewLine = "\n";
        foreach (string str in stringList)
          streamWriter.WriteLine(str);
        streamWriter.Flush();
        return Convert.ToBase64String(memoryStream.GetBuffer());
      }
    }
  }

  private bool VerifyAffixKey(string word, char affixKey)
  {
    return new ArrayList((ICollection) ((Struct.Word) this._baseWords[(object) word]).AffixKeys.ToCharArray()).Contains((object) affixKey);
  }

  public ArrayList PossibleWord => this._possibleBaseWords;
}

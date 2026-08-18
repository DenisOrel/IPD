// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.KeywordReplacementScheme
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Document.DBCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Вспомогательный статический класс для работы с атрибутами, отображаемыми в примечаниях спецификаций
/// </summary>
public class KeywordReplacementScheme : SettingsSchemeBase
{
  public Dictionary<string, string> Data = new Dictionary<string, string>();
  private static readonly Dictionary<string, string> defaultDictionary = new Dictionary<string, string>()
  {
    {
      "Болт",
      "Болты"
    },
    {
      "Вилка",
      "Вилки"
    },
    {
      "Винт",
      "Винты"
    },
    {
      "Гайка",
      "Гайки"
    },
    {
      "Гвоздь",
      "Гвозди"
    },
    {
      "Кольцо",
      "Кольца"
    }
  };

  public KeywordReplacementScheme() => this.SetDefault();

  public void SetDefault()
  {
    this.Data.Clear();
    KeywordReplacementScheme.defaultDictionary.Select<KeyValuePair<string, string>, KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, KeyValuePair<string, string>>) (di => new KeyValuePair<string, string>(di.Key, di.Value))).ToList<KeyValuePair<string, string>>().ForEach((Action<KeyValuePair<string, string>>) (li => this.Data[li.Key] = li.Value));
  }

  public bool Validate(string keyword, string replacement, bool checkKey = true)
  {
    if (string.IsNullOrWhiteSpace(keyword) || string.IsNullOrWhiteSpace(replacement))
      return false;
    return !this.Data.ContainsKey(keyword) || !checkKey;
  }

  public void AddOrUpdate(string keyword, string replacement)
  {
    if (!this.Validate(keyword, replacement, false))
      return;
    this.Data[keyword] = replacement;
  }

  public bool Remove(string keyword) => this.Data.ContainsKey(keyword) && this.Data.Remove(keyword);

  /// <summary>Загрузить настройки из потока</summary>
  /// <param name="stream">Поток, в который хранятся данные</param>
  public void LoadFromXML(Stream stream)
  {
    stream.Position = 0L;
    XDocument xdocument = XDocument.Load(stream);
    this.Data.Clear();
    XName name = (XName) "KeywordReplacement";
    foreach (KeyValuePair<string, string> keyValuePair in xdocument.Descendants(name).Select<XElement, KeyValuePair<string, string>>((Func<XElement, KeyValuePair<string, string>>) (kr => new KeyValuePair<string, string>(kr.Attribute((XName) "Keyword").Value, kr.Attribute((XName) "Replacement").Value))).OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (k => k.Key)).ToList<KeyValuePair<string, string>>())
      this.Data[keyValuePair.Key] = keyValuePair.Value;
  }

  /// <summary>Загрузить из объекта БД список атрибутов, отображаемых в графе Примечание документов AVS</summary>
  /// <param name="settingsObjectID">Идентификатор владельца настроек</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута с настройками</param>
  /// <param name="session">Сессия</param>
  public void LoadFromDBObjectAttribute(
    long settingsObjectID,
    int settingsAttributeID,
    IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    Guid empty = Guid.Empty;
    if (settingsObjectID.IsDefinedId())
      session.GetObjectInfo(settingsObjectID);
    IDBAttribute attributeById = session.GetObjectActual(settingsObjectID, true).GetAttributeByID(settingsAttributeID);
    if (attributeById == null)
      return;
    using (MemoryStream aDestStream = new MemoryStream())
    {
      new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(session);
      aDestStream.Position = 0L;
      if (aDestStream.Length == 0L)
        return;
      this.LoadFromXML((Stream) aDestStream);
    }
  }

  /// <summary>Сохранить настройки в поток</summary>
  /// <param name="stream">Поток, в который сохраняются данные</param>
  protected override void SaveToXmlDocument(MemoryStream stream)
  {
    new XDocument(new object[1]
    {
      (object) new XElement((XName) "KeywordReplacements", (object) this.Data.Select<KeyValuePair<string, string>, XElement>((Func<KeyValuePair<string, string>, XElement>) (item => new XElement((XName) "KeywordReplacement", new object[2]
      {
        (object) new XAttribute((XName) "Keyword", (object) item.Key),
        (object) new XAttribute((XName) "Replacement", (object) item.Value)
      }))))
    }).Save((Stream) stream);
  }

  /// <summary>Сохранить в объект БД список атрибутов, отображаемых в графе Примечание документов AVS</summary>
  /// <param name="settingsObjectID">Идентификатор владельца настроек</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута с настройками</param>
  /// <param name="session">Сессия</param>
  public void SaveToDBObjectAttribute(long settingsObjectID, int settingsAttributeID)
  {
    this.SaveParamsDataToObjectAttribute(settingsObjectID, settingsAttributeID);
  }
}

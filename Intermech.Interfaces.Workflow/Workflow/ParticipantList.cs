// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ParticipantList
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Workflow;

/// <summary>Summary description for ParticipantsList.</summary>
[Serializable]
public class ParticipantList : 
  BriefcaseAccessor,
  IEnumerable<Participant>,
  IEnumerable,
  IValidatedItem
{
  /// <summary>Все исполнители учавствуют в действии</summary>
  public bool EveryOne = true;
  public string XmlSection = string.Empty;
  public static OnGetParticipantName OnGetParticipantName;
  private IUserSession _userSession;
  private List<Participant> _list = new List<Participant>();
  private bool _writeGuids;

  public IUserSession Session => this._userSession;

  public ParticipantList()
    : this((IUserSession) null)
  {
  }

  public ParticipantList(IUserSession session)
  {
    this.XmlSection = "Participants";
    this._userSession = session;
  }

  public ParticipantList(string xmlSection) => this.XmlSection = xmlSection;

  public Participant this[int index]
  {
    get => this._list[index];
    set => this._list[index] = value;
  }

  public void SetSession(IUserSession session) => this._userSession = session;

  public Participant AddParticipant(ParticipantKind Kind, long ID)
  {
    Participant participant = new Participant(Kind, ID);
    this.Add(participant);
    return participant;
  }

  public Participant FindEqual(Participant part)
  {
    foreach (Participant equal in this)
    {
      if (part.Equals((object) equal))
        return equal;
    }
    return (Participant) null;
  }

  public void SaveToStream(Stream stream)
  {
    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
    writer.Formatting = Formatting.Indented;
    this.Write(writer);
    writer.Flush();
  }

  public Participant Find(ParticipantKind kind, long id)
  {
    foreach (Participant participant in this)
    {
      if (participant != null && participant.Kind == kind && participant.ID == id)
        return participant;
    }
    return (Participant) null;
  }

  /// <summary>
  /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
  /// </summary>
  public bool WriteGuids
  {
    get => this._writeGuids || this.Invalid;
    set => this._writeGuids = value;
  }

  private Guid ParticipantToGuid(Participant p)
  {
    Guid guid = Guid.Empty;
    if (p.Kind != ParticipantKind.Variable && this._userSession != null)
    {
      IDBObject dbObject = this._userSession.GetObject(p.ID, false);
      if (dbObject != null)
        guid = dbObject.ObjectGUID;
    }
    else
      guid = SimpleFuncs.AttributeIDToGuid((int) p.ID);
    return guid;
  }

  public void Write(XmlTextWriter writer)
  {
    writer.WriteStartElement(this.XmlSection);
    if (!this.EveryOne)
      writer.WriteAttributeString("Flag", "1");
    for (int index = 0; index < this.Count; ++index)
    {
      Participant p = this[index];
      writer.WriteStartElement(p.Kind.ToString());
      writer.WriteStartElement("ID");
      writer.WriteString(Convert.ToString(p.ID));
      writer.WriteEndElement();
      if (this.WriteGuids)
      {
        writer.WriteStartElement("Guid");
        Guid guid = p.Guid;
        if (guid == Guid.Empty)
          guid = this.ParticipantToGuid(p);
        writer.WriteString(guid.ToString());
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
  }

  private static object StringToEnum(Type t, string Value)
  {
    foreach (FieldInfo field in t.GetFields())
    {
      if (field.Name == Value)
        return field.GetValue((object) null);
    }
    throw new Exception($"Can't convert {Value} to {t.ToString()}");
  }

  public void Read(XmlTextReader reader)
  {
    do
      ;
    while (reader.Read() && (reader.NodeType != XmlNodeType.Element || !(reader.Name == this.XmlSection)));
    if (reader.EOF)
      return;
    reader.MoveToAttribute("Flag");
    if (reader.ReadAttributeValue())
      this.EveryOne = false;
    if (reader.IsEmptyElement)
    {
      reader.Read();
    }
    else
    {
      NameValueCollection nameValueCollection = (NameValueCollection) null;
      string name1 = "";
      ArrayList arrayList = new ArrayList();
      foreach (string name2 in Enum.GetNames(typeof (ParticipantKind)))
        arrayList.Add((object) name2);
      int num1 = -1;
      string str = "";
      while (reader.Read())
      {
        if (reader.NodeType == XmlNodeType.Element)
        {
          name1 = reader.Name;
          if (str == "")
            str = name1;
        }
        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == this.XmlSection)
        {
          reader.ReadEndElement();
          break;
        }
        if (num1 == -1)
          num1 = arrayList.IndexOf((object) reader.Name);
        if (num1 != -1)
        {
          if (nameValueCollection == null && reader.NodeType == XmlNodeType.Element)
            nameValueCollection = new NameValueCollection();
          else if (nameValueCollection != null && reader.NodeType == XmlNodeType.EndElement && reader.Name == str)
          {
            long num2 = Convert.ToInt64(nameValueCollection["ID"]);
            long num3 = num2;
            ParticipantKind Kind = (ParticipantKind) num1;
            int num4 = 0;
            string g = nameValueCollection["Guid"];
            Guid guid = Guid.Empty;
            if (g != null)
            {
              guid = new Guid(g);
              num3 = 0L;
              if (Kind != ParticipantKind.Variable && this._userSession != null)
              {
                QuickObjectInfo objectInfo = this._userSession.GetObjectInfo(guid);
                if (!objectInfo.Empty)
                {
                  num3 = objectInfo.ObjectID;
                  num4 = objectInfo.ObjectTypeID;
                }
              }
              else
                num3 = (long) SimpleFuncs.AttributeGuidToID(guid);
            }
            if (num3 != 0L)
              num2 = num3;
            Participant participant = this.AddParticipant(Kind, num2);
            participant._objectType = num4;
            if (num3 == 0L)
            {
              participant._guid = guid;
              if (this.Briefcase != null)
                g = this.Briefcase.GetCaption(Kind == ParticipantKind.Variable ? Domain.Variables : Domain.Objects, num2);
              if (g == null)
                g = "?";
              participant._caption = g;
            }
            num1 = -1;
            nameValueCollection = (NameValueCollection) null;
            str = "";
          }
          if (nameValueCollection != null && reader.NodeType == XmlNodeType.Text)
            nameValueCollection[name1] = reader.Value;
        }
      }
    }
  }

  public void LoadFromStream(Stream stream)
  {
    this.Clear();
    if (stream.Length == 0L)
      return;
    XmlTextReader reader = new XmlTextReader(stream);
    this.Read(reader);
    reader.Close();
  }

  public string AsString
  {
    get
    {
      MemoryStream memoryStream = new MemoryStream();
      this.SaveToStream((Stream) memoryStream);
      memoryStream.Position = 0L;
      StreamReader streamReader = new StreamReader((Stream) memoryStream);
      try
      {
        return streamReader.ReadToEnd();
      }
      finally
      {
        streamReader.Close();
        memoryStream.Close();
      }
    }
    set
    {
      MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value));
      try
      {
        this.LoadFromStream((Stream) memoryStream);
        memoryStream.Close();
      }
      finally
      {
        memoryStream.Close();
      }
    }
  }

  public void Assign(ParticipantList src)
  {
    this.Clear();
    this.EveryOne = src.EveryOne;
    for (int index = 0; index < src.Count; ++index)
    {
      Participant participant1 = src[index];
      Participant participant2 = this.AddParticipant(participant1.Kind, participant1.ID);
      participant2._caption = participant1._caption;
      participant2._guid = participant1._guid;
      participant2._objectType = participant1._objectType;
    }
  }

  public string ToUserString(int maxCount = 0)
  {
    string userString = "";
    for (int index = 0; index < this.Count; ++index)
    {
      Participant participant = this[index];
      if (userString != "")
        userString += ", ";
      userString += participant.DisplayName;
      if (maxCount != 0 && index >= maxCount)
      {
        userString += "…";
        break;
      }
    }
    return userString;
  }

  public void DeleteEquals()
  {
    for (int index1 = 0; index1 < this.Count; ++index1)
    {
      for (int index2 = index1 + 1; index2 < this.Count; ++index2)
      {
        if (this[index1] != null && this[index2] != null && this[index1].Equals((object) this[index2]))
          this[index1] = (Participant) null;
      }
    }
    for (int index = this.Count - 1; index >= 0; --index)
    {
      if (this[index] == null)
        this.RemoveAt(index);
    }
  }

  public bool ProcessVariableReferences(int varAttrID, bool doDeletion)
  {
    if (!doDeletion)
      return this.Find(ParticipantKind.Variable, (long) varAttrID) != null;
    Participant participant = this.Find(ParticipantKind.Variable, (long) varAttrID);
    if (participant == null)
      return false;
    this.Remove(participant);
    return true;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ParticipantList))
      return base.Equals(obj);
    ParticipantList participantList = (ParticipantList) obj;
    if (this.Count != participantList.Count)
      return false;
    for (int index = 0; index < this.Count; ++index)
    {
      if (!this[index].Equals((object) participantList[index]))
        return false;
    }
    return true;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static string ExtractAddData(string s)
  {
    int num = s.IndexOf("<!--");
    return num == -1 ? "" : s.Substring(num + 4, s.Length - num - 7);
  }

  public static void InsertAddData(ref string s, string addData) => s = $"{s}\r\n<!--{addData}-->";

  public bool Replace(ParticipantKind kind, long oldID, long newID)
  {
    bool flag = false;
    for (int index = this.Count - 1; index >= 0; --index)
    {
      if (this[index].Kind == kind && this[index].ID == oldID)
      {
        this[index].ID = newID;
        flag = true;
      }
    }
    if (flag)
      this.DeleteEquals();
    return flag;
  }

  /// <summary>
  /// Возвращает набор идентификаторов версий объектов (пользователей, групп и должностей), включенных в список
  /// </summary>
  public HashSet<long> ObjectIDs
  {
    get
    {
      HashSet<long> objectIds = new HashSet<long>();
      foreach (Participant participant in this._list)
      {
        if (participant.Kind != ParticipantKind.Variable)
          objectIds.Add(participant.ID);
      }
      return objectIds;
    }
  }

  /// <summary>Содержит ли недопустимые элементы</summary>
  public bool Invalid
  {
    get
    {
      foreach (Participant participant in this)
      {
        if (participant.Invalid)
          return true;
      }
      return false;
    }
  }

  public IEnumerator<Participant> GetEnumerator()
  {
    return (IEnumerator<Participant>) this._list.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._list.GetEnumerator();

  public int Count => this._list.Count;

  public void Add(Participant item)
  {
    this._list.Add(item);
    ParticipantList.ModifyItems participantsChanged = this.ParticipantsChanged;
    if (participantsChanged == null)
      return;
    participantsChanged();
  }

  public bool Remove(Participant item)
  {
    int num = this._list.Remove(item) ? 1 : 0;
    ParticipantList.ModifyItems participantsChanged = this.ParticipantsChanged;
    if (participantsChanged == null)
      return num != 0;
    participantsChanged();
    return num != 0;
  }

  public void RemoveAt(int index)
  {
    this._list.RemoveAt(index);
    ParticipantList.ModifyItems participantsChanged = this.ParticipantsChanged;
    if (participantsChanged == null)
      return;
    participantsChanged();
  }

  public void Clear()
  {
    this._list.Clear();
    ParticipantList.ModifyItems participantsChanged = this.ParticipantsChanged;
    if (participantsChanged == null)
      return;
    participantsChanged();
  }

  public event ParticipantList.ModifyItems ParticipantsChanged;

  public delegate void ModifyItems();
}

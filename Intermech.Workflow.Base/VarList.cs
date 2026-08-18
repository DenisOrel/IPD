using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Workflow
{
    /// <summary>Summary description for VarList.</summary>
    [Serializable]
    public class VarList : BriefcaseAccessor, IEnumerable<Variable>, IEnumerable, IValidatedItem
    {
      private List<Variable> _list = new List<Variable>();
      private bool _modified;
      private bool _isServer;
      private bool _isPumper;
      private IUserSession _userSession;
      protected bool _virtualAdded;
      private bool _writeGuids;
      public List<int> EditableVarIDs = new List<int>();
      public bool SystemAdded;
      private const string _hasMoreVarsSuffix = ",..";
      private bool _attributeExists;
      private int _systemVariablesCount;

      public Variable this[int index]
      {
        get => this._list[index];
        set => this._list[index] = value;
      }

      public bool Modified
      {
        get
        {
          if (this._modified)
            return this._modified;
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].Modified)
              return true;
          }
          return false;
        }
        set
        {
          this._modified = value;
          for (int index = 0; index < this.Count; ++index)
            this[index].SetModified(value);
        }
      }

      public IUserSession Session => this._userSession;

      public VarList(IUserSession session, bool isServer, bool isPumper)
      {
        this._isPumper = isPumper;
        this._isServer = isServer;
        this._userSession = session;
      }

      public VarList(IDBObject src, bool isServer, bool isPumper)
      {
        this._isPumper = isPumper;
        this._isServer = isServer;
        if (src == null)
          return;
        this._userSession = src.Session;
        this.Load(src);
      }

      public bool VirtualAdded
      {
        get => this._virtualAdded;
        set => this._virtualAdded = value;
      }

      public static void FillPossibleValues(IDBAttributeType atype, StringList sl)
      {
        if (atype.MultipleValued == MultiValueModes.SingleValue || atype.MultipleValued == MultiValueModes.MultiValues)
          return;
        sl.Clear();
        foreach (DataRow row in (InternalDataCollectionBase) atype.GetPossibleValues().Rows)
          sl.Add(row[1].ToString());
        if ((atype.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
          return;
        sl.Insert(0, "");
      }

      public static void FillPossibleValues(IMSAttributeType atype, StringList sl)
      {
        if (atype.MultiValueMode == MultiValueModes.SingleValue || atype.MultiValueMode == MultiValueModes.MultiValues)
          return;
        sl.Clear();
        if (atype.PossibleValues != null)
        {
          foreach (object possibleValue in atype.PossibleValues)
            sl.Add(possibleValue.ToString());
        }
        if ((atype.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
          return;
        sl.Insert(0, "");
      }

      public Variable AddVariable(int typeID)
      {
        Variable variable1 = this.GetVariable(typeID);
        if (variable1 != null)
        {
          if (variable1.Deleted)
            variable1.Deleted = false;
          return variable1;
        }
        Variable variable2 = new Variable(this);
        variable2.AttrTypeID = typeID;
        variable2._new = true;
        this.Add(variable2);
        this.Changed();
        return variable2;
      }

      public void Changed()
      {
        this._modified = true;
        if (!this._isServer || this._isPumper)
          return;
        this._virtualAdded = false;
      }

      public void Clear()
      {
        this._list.Clear();
        this.SystemAdded = false;
        if (this._isServer && !this._isPumper)
          this._virtualAdded = false;
        this.Changed();
      }

      public Variable AddVariable(int typeID, string name, VarType type, object[] addInfo)
      {
        Variable variable = this.AddVariable(typeID);
        variable._name = name;
        variable._type = type;
        variable.AddInfo = addInfo;
        return variable;
      }

      public Variable GetVariable(int typeID)
      {
        if (typeID != 0)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].AttrTypeID == typeID)
              return this[index];
          }
        }
        return (Variable) null;
      }

      public Variable GetVariable(string name)
      {
        if (name != "")
        {
          foreach (Variable variable in this)
          {
            if (variable.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) || variable.ShortName.Equals(name, StringComparison.CurrentCultureIgnoreCase))
              return variable;
          }
        }
        return (Variable) null;
      }

      public void DeleteVariable(int typeID)
      {
        int index = this.IndexOf(this.GetVariable(typeID));
        if (index == -1)
          return;
        this.RemoveAt(index);
        this.Changed();
      }

      /// <summary>
      /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
      /// </summary>
      public bool WriteGuids
      {
        get => this._writeGuids || this.Invalid;
        set => this._writeGuids = value;
      }

      public void SaveToStream(Stream stream)
      {
        XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement("Vars");
        foreach (Variable variable in this)
        {
          if (!(variable is ISystemVariable))
          {
            xmlTextWriter.WriteStartElement("Var");
            xmlTextWriter.WriteStartElement("ID");
            xmlTextWriter.WriteString(Convert.ToString(variable.AttrTypeID));
            xmlTextWriter.WriteEndElement();
            if (this.WriteGuids)
            {
              xmlTextWriter.WriteStartElement("Guid");
              xmlTextWriter.WriteString(variable.AttrTypeGuid.ToString());
              xmlTextWriter.WriteEndElement();
            }
            string text = variable.StoredValue;
            if (text != "" || variable.VarType == VarType.String || variable.VarType == VarType.Text)
            {
              xmlTextWriter.WriteStartElement("Value");
              if (this.WriteGuids && variable.VarType == VarType.ParticipantList)
                text = new ParticipantList(this.Session)
                {
                  AsString = text,
                  WriteGuids = true
                }.AsString;
              xmlTextWriter.WriteString(text);
              xmlTextWriter.WriteEndElement();
            }
            xmlTextWriter.WriteEndElement();
          }
        }
        xmlTextWriter.WriteEndElement();
        xmlTextWriter.Flush();
      }

      public void Read(XmlTextReader reader)
      {
        reader.ReadStartElement("Vars");
        NameValueCollection nameValueCollection = (NameValueCollection) null;
        string name = "";
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element)
            name = reader.Name;
          if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Vars")
          {
            reader.ReadEndElement();
            break;
          }
          if (nameValueCollection == null && reader.NodeType == XmlNodeType.Element)
            nameValueCollection = new NameValueCollection();
          else if (nameValueCollection != null && reader.NodeType == XmlNodeType.EndElement && reader.Name == "Var")
          {
            int typeID = 0;
            int int32 = Convert.ToInt32(nameValueCollection["ID"]);
            string g = nameValueCollection["Guid"];
            if (g != null)
            {
              int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(g));
              if (attributeTypeId != -1)
                typeID = attributeTypeId;
            }
            else
              typeID = int32;
            Variable variable = this.AddVariable(typeID);
            variable._new = false;
            if (g != null)
            {
              variable._attrTypeGuid = new Guid(g);
              variable._oldID = int32;
            }
            variable.StoredValue = nameValueCollection["Value"] != null ? Convert.ToString(nameValueCollection["Value"]) : "";
            nameValueCollection = (NameValueCollection) null;
          }
          if (nameValueCollection != null && reader.NodeType == XmlNodeType.Text)
            nameValueCollection[name] = reader.Value;
        }
      }

      public void LoadFromStream(Stream stream)
      {
        this.Clear();
        if (stream.Length == 0L)
          return;
        if (stream.Position != 0L)
          stream.Position = 0L;
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

      public bool FillByVirtualAttribute(IDBObject obj, IDBAttribute attr)
      {
        foreach (Variable variable in this)
        {
          if (attr.AttributeID == variable.AttrTypeID)
          {
            variable.TypedValue = attr.Value;
            return variable.Modified;
          }
        }
        return false;
      }

      public bool FillByVirtualAttributes(IDBObject obj)
      {
        bool flag = false;
        for (int index = 0; index < this.Count; ++index)
        {
          IDBAttribute byId = obj.Attributes.FindByID(this[index].AttrTypeID);
          if (byId != null)
          {
            this[index].TypedValue = byId.Value;
            if (this[index].Modified)
              flag = true;
          }
        }
        return flag;
      }

      public void Assign(VarList Src)
      {
        this.AsString = Src.AsString;
        this.SystemAdded = false;
      }

      public List<int> TypeIDs
      {
        get
        {
          List<int> typeIds = new List<int>();
          foreach (Variable variable in this)
          {
            if (variable.AttrTypeID != 0 && variable.Kind == VarKind.User)
              typeIds.Add(variable.AttrTypeID);
          }
          return typeIds;
        }
      }

      public string TypeIDsString
      {
        get
        {
          string typeIdsString = "";
          List<int> typeIds = this.TypeIDs;
          int count = typeIds.Count;
          for (int index = 0; index < count; ++index)
          {
            string str = $",{typeIds[index]:X}";
            if (typeIdsString.Length + str.Length + (index + 1 < count ? 3 : 1) > Consts.MaxNoteLength)
              return typeIdsString + ",..";
            typeIdsString += str;
          }
          if (typeIdsString != "")
            typeIdsString += ",";
          return typeIdsString;
        }
      }

      public bool AttributeExists => this._attributeExists;

      public void Load(IDBObject src)
      {
        IDBAttribute attributeById = src.GetAttributeByID(wfConsts.AttrVariablesID);
        this._attributeExists = attributeById != null;
        if (!this._attributeExists)
          return;
        this.Load(attributeById as IBlobReader);
      }

      public void Load(IBlobReader reader)
      {
        if (reader == null)
          return;
        using (MemoryStream memoryStream = new MemoryStream())
        {
          BlobInformation blobInformation = reader.OpenBlob(0);
          try
          {
            if (blobInformation.RealFileSize > 0L)
            {
              byte[] buffer = reader.ReadDataBlock((int) blobInformation.RealFileSize);
              memoryStream.Write(buffer, 0, buffer.Length);
            }
          }
          finally
          {
            reader.CloseBlob();
          }
          memoryStream.Position = 0L;
          this.LoadFromStream((Stream) memoryStream);
        }
      }

      public void Save(IBlobWriter writer, bool WriteTypeIDs)
      {
        if (writer == null)
          return;
        MemoryStream memoryStream = new MemoryStream();
        try
        {
          this.SaveToStream((Stream) memoryStream);
          BlobInformation blobInfo = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "", ArcMethods.NotPacked, WriteTypeIDs ? this.TypeIDsString : "");
          if (!writer.OpenBlob(blobInfo, false))
            return;
          writer.WriteDataBlock(memoryStream.ToArray());
        }
        finally
        {
          memoryStream.Close();
        }
      }

      public void Save(IDBObject obj, bool WriteTypeIDs)
      {
        IDBAttribute writer = obj.GetAttributeByID(wfConsts.AttrVariablesID) ?? obj.Attributes.AddAttribute(wfConsts.AttrVariablesID, false);
        this._attributeExists = true;
        this.Save(writer as IBlobWriter, WriteTypeIDs);
      }

      public override string ToString() => this.AsString;

      /// <summary>
      /// Возвращает набор идентификаторов версий связанных объектов (пользователей, групп и должностей)
      /// </summary>
      public HashSet<long> ObjectIDs
      {
        get
        {
          HashSet<long> objectIds = new HashSet<long>();
          foreach (Variable variable in this)
          {
            if (variable.Kind == VarKind.User && variable.VarType == VarType.ParticipantList)
              objectIds.UnionWith((IEnumerable<long>) variable.AsParticipants.ObjectIDs);
          }
          return objectIds;
        }
      }

      public int SystemVariablesCount => this._systemVariablesCount;

      public void AddSystemVariables(IDBObject obj)
      {
        if (this._isPumper || this.SystemAdded)
          return;
        this.SystemAdded = true;
        IScheme scheme = !(obj is IScheme) ? (obj as IActivity).Process : (IScheme) obj;
        this.Add((Variable) new StarterVariable(this, (IDBObject) scheme));
        this.Add((Variable) new SenderVariable(this, obj));
        this.Add((Variable) new TaskPercentVariable(this, (IDBObject) scheme));
        this._systemVariablesCount = 3;
        foreach (DataRow row in (InternalDataCollectionBase) obj.Session.GetAttributesGroup(wfConsts.WorkflowSysVarsGroupID).Attributes.Select("", (object[]) null).Rows)
        {
          int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
          if (this.GetVariable(int32) == null)
          {
            object obj1 = row["F_DEFAULT_VALUE"];
            string defValue = DBNull.Value.Equals(obj1) ? "" : obj1.ToString();
            this.Add((Variable) new SystemVariable(this, (IDBObject) scheme, int32, defValue));
            ++this._systemVariablesCount;
          }
        }
        this.Changed();
      }

      public IEnumerator<Variable> GetEnumerator()
      {
        return (IEnumerator<Variable>) this._list.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._list.GetEnumerator();

      public int Count => this._list.Count;

      public void Add(Variable item) => this._list.Add(item);

      public bool Remove(Variable item) => this._list.Remove(item);

      public void RemoveAt(int index) => this._list.RemoveAt(index);

      public int IndexOf(Variable item) => this._list.IndexOf(item);

      public bool Invalid
      {
        get
        {
          foreach (Variable variable in this)
          {
            if (variable.Invalid)
              return true;
          }
          return false;
        }
      }
    }
}

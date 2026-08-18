// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Variable
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Briefcase;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace Intermech.Workflow
{
    [Serializable]
    public class Variable : IComparable<Variable>, IValidatedItem
    {
      internal string _name = "";
      internal string _shortName = "";
      internal VarType _type = VarType.Unknown;
      protected VarList _owner;
      protected string _value = "";
      internal string _note = "";
      public int AttrTypeID;
      internal int _oldID;
      internal bool _new;
      private bool _modified;
      internal Variable.ModifiedFlag ModifiedFlags;
      private bool _deleted;
      private ParticipantList _parts;
      public VarKind Kind;
      public StringList _valuesList = new StringList();
      private string _dateFormat = "dd'.'MM'.'yyyy H':'mm':'ss";
      private bool _inInit;
      internal Guid _attrTypeGuid;

      public VarType VariableType
      {
        get => this._type;
        set => this._type = value;
      }

      public bool New => this._new;

      public bool Modified => this._modified;

      internal void SetModified(bool value, Variable.ModifiedFlag flags = (Variable.ModifiedFlag) 0)
      {
        if (value)
        {
          this._modified = true;
          this.ModifiedFlags |= flags;
        }
        else
          this.ModifiedFlags = (Variable.ModifiedFlag) 0;
      }

      public Variable(VarList owner) => this._owner = owner;

      public virtual bool Calculated => false;

      protected virtual string GetValue() => this._value;

      protected virtual void AfterSetValue()
      {
      }

      public string Value
      {
        get => this.GetValue();
        set
        {
          if (this.VarType == VarType.StringList && value != "" && !this.ValuesList.Contains(value))
            value = string.Empty;
          if (!(value != this._value))
            return;
          switch (this.VarType)
          {
            case VarType.Integer:
              Convert.ToInt64(value);
              break;
            case VarType.Float:
              Convert.ToDouble(value);
              break;
            case VarType.DateTime:
              if (!string.IsNullOrEmpty(value) && this.ToDateTime(value) == DateTime.MinValue)
              {
                Convert.ToDateTime(value);
                break;
              }
              break;
          }
          this.SetModified(true, Variable.ModifiedFlag.Value);
          this.ClearCache();
          this._value = value;
          this.AfterSetValue();
        }
      }

      private void ClearCache()
      {
      }

      public string StoredValue
      {
        get => this.Value;
        set
        {
          if (this.VarType == VarType.ParticipantList && value == "<Participants />")
            value = "";
          try
          {
            this.Value = value;
          }
          catch (FormatException ex)
          {
          }
        }
      }

      public string Note => this._note;

      public StringList ValuesList
      {
        get
        {
          this.Init();
          return this._valuesList;
        }
        set
        {
          if (this._valuesList.Equals((object) value))
            return;
          this._valuesList = value;
          this.ClearCache();
          this.SetModified(true, Variable.ModifiedFlag.ValuesList);
        }
      }

      public StringList AsStringList
      {
        get
        {
          if (this._type != VarType.StringList)
            throw new Exception("Cannot access AsStringList, var type mismatch");
          StringList asStringList = new StringList();
          asStringList.AddRange((IEnumerable<string>) this.ValuesList);
          asStringList.Insert(0, this.Value);
          return asStringList;
        }
      }

      /// <summary>
      /// Возвращает значение переменной нужного дотнетовского типа.
      /// Т.е. для boolean будет возвращен boolean и т.д.
      /// </summary>
      public object TypedValue
      {
        get
        {
          switch (this.VarType)
          {
            case VarType.Integer:
              return (object) this.AsInteger;
            case VarType.Float:
              return (object) this.AsFloat;
            case VarType.DateTime:
              return (object) this.AsDateTime;
            case VarType.Boolean:
              return (object) this.AsBoolean;
            case VarType.Archive:
              return (object) this.AsGuid;
            default:
              return (object) this.Value;
          }
        }
        set
        {
          switch (this.VarType)
          {
            case VarType.DateTime:
              DateTime dateTime = this.ToDateTime(value.ToString());
              if (dateTime == DateTime.MinValue)
                Convert.ToDateTime(value);
              this.AsDateTime = dateTime;
              break;
            case VarType.Boolean:
              this.AsBoolean = Convert.ToBoolean(value);
              break;
            case VarType.Archive:
              if (value is Guid)
              {
                this.Value = value.ToString();
                break;
              }
              this.AsInteger = !DBNull.Value.Equals(value) ? Convert.ToInt64(value) : 0L;
              break;
            default:
              this.Value = value.ToString();
              break;
          }
        }
      }

      public bool AsBoolean
      {
        get
        {
          bool result = false;
          if (!bool.TryParse(this.Value, out result))
            result = Convert.ToBoolean(this.AsInteger);
          return result;
        }
        set => this.Value = value ? "1" : "0";
      }

      public long AsInteger
      {
        get
        {
          long result = 0;
          long.TryParse(this.Value, out result);
          return result;
        }
        set
        {
          if (this.VarType == VarType.Archive)
          {
            IDBObject dbObject = this._owner.Session?.GetObject(value, false);
            if (dbObject == null)
              return;
            this.Value = dbObject.ObjectGUID.ToString();
          }
          else
            this.Value = value.ToString();
        }
      }

      public double AsFloat
      {
        get
        {
          double result = 0.0;
          double.TryParse(this.Value, out result);
          return result;
        }
      }

      public Guid AsGuid
      {
        get
        {
          Guid asGuid = Guid.Empty;
          if (!string.IsNullOrEmpty(this.Value))
          {
            if (this.Value != "0")
            {
              try
              {
                asGuid = new Guid(this.Value);
              }
              catch
              {
              }
            }
          }
          return asGuid;
        }
      }

      internal DateTime ToDateTime(string s)
      {
        DateTime result = DateTime.MinValue;
        if (!DateTime.TryParseExact(s, this._dateFormat, (IFormatProvider) null, DateTimeStyles.AssumeLocal, out result))
          DateTime.TryParse(s, out result);
        return result;
      }

      public DateTime AsDateTime
      {
        get => string.IsNullOrEmpty(this.Value) ? DateTime.Now : this.ToDateTime(this.Value);
        set => this.Value = value.ToString(this._dateFormat);
      }

      public bool Deleted
      {
        get => this._deleted;
        set
        {
          if (value == this._deleted)
            return;
          this._modified = true;
          this._deleted = value;
        }
      }

      public string UserValue
      {
        get
        {
          switch (this._type)
          {
            case VarType.ParticipantList:
              return this.AsParticipants.ToUserString();
            case VarType.Boolean:
              return this.AsBoolean ? LocalizationHolder.GetString("BoolTrue") : LocalizationHolder.GetString("BoolFalse");
            case VarType.Archive:
              IDBObject dbObject = this._owner?.Session?.GetObject(this.AsGuid, false);
              return dbObject != null ? dbObject.Caption : "???";
            default:
              return this.Value;
          }
        }
      }

      public ParticipantList AsParticipants
      {
        get
        {
          if (this._type != VarType.ParticipantList)
            throw new Exception("Cannot access AsParticipantList, var type mismatch");
          if (this._parts == null)
            this._parts = new ParticipantList(this._owner.Session);
          this._parts.AsString = this.Value;
          return this._parts;
        }
      }

      private void Init()
      {
        if (this._inInit)
          return;
        this._inInit = true;
        try
        {
          if (this.AttrTypeID == 0 && this.AttrTypeGuid == Guid.Empty || !(this._name == ""))
            return;
          IMSAttributeType imsAttributeType = (IMSAttributeType) null;
          if (this.AttrTypeID == 0)
          {
            if (this.AttrTypeGuid != Guid.Empty)
              imsAttributeType = MetaDataHelper.GetAttributeType(this.AttrTypeGuid);
            if (imsAttributeType == null && this._owner.Briefcase != null)
            {
              if (!(this._owner.Briefcase.Map.Get(Domain.Variables, (long) this._oldID) is MapperVariable mapperVariable))
                return;
              this._name = mapperVariable.Caption;
              this._type = mapperVariable.Type;
              this._valuesList = mapperVariable.ValuesList;
              return;
            }
          }
          else
            imsAttributeType = MetaDataHelper.GetAttributeType(this.AttrTypeID);
          if (imsAttributeType != null)
          {
            this._name = imsAttributeType.Name;
            this._shortName = imsAttributeType.ShortName;
            this._type = MiscFunx.DetermineVarType(imsAttributeType);
            this._attrTypeGuid = imsAttributeType.AttributeGuid;
            this._note = imsAttributeType.Note;
            VarList.FillPossibleValues(imsAttributeType, this._valuesList);
          }
          else
            this._name = this.AttrTypeGuid != Guid.Empty ? this.AttrTypeGuid.ToString() : "?";
        }
        finally
        {
          this._inInit = false;
        }
      }

      public VarType VarType
      {
        get
        {
          if (this._type == VarType.Unknown)
            this.Init();
          return this._type;
        }
        set
        {
          if (value == this.VarType)
            return;
          this._type = value;
          this.SetModified(true, Variable.ModifiedFlag.ValuesList);
        }
      }

      public string Name
      {
        get
        {
          this.Init();
          return this._name;
        }
        set
        {
          if (!(this.Name != value))
            return;
          this._name = value;
          this.SetModified(true, Variable.ModifiedFlag.Name);
        }
      }

      public string ShortName
      {
        get
        {
          this.Init();
          return this._shortName;
        }
      }

      public Guid AttrTypeGuid
      {
        get
        {
          this.Init();
          return this._attrTypeGuid;
        }
      }

      public object[] AddInfo
      {
        get
        {
          string empty = string.Empty;
          return new object[1]
          {
            (object) (this.VarType == VarType.StringList ? this.AsStringList.Text : this.StoredValue)
          };
        }
        set
        {
          if (value == null || value.Length == 0)
            return;
          string str1 = value[0].ToString();
          if (this.VarType == VarType.StringList)
          {
            StringList stringList = new StringList()
            {
              Text = str1
            };
            string str2 = stringList.Count > 0 ? stringList[0] : string.Empty;
            if (stringList.Count > 0)
              stringList.RemoveAt(0);
            this.ValuesList = stringList;
            this.StoredValue = str2;
          }
          else
            this.StoredValue = str1;
        }
      }

      public override string ToString() => this.Name;

      public FieldTypes FieldType => MiscFunx.GetFieldTypeEx(this.VarType).FieldType;

      public int CompareTo(Variable other) => this.Name.CompareTo(other.Name);

      public bool IsEmpty
      {
        get
        {
          switch (this.VarType)
          {
            case VarType.Integer:
              return this.AsInteger == 0L;
            case VarType.Float:
              return this.AsFloat == 0.0;
            case VarType.Boolean:
              return !this.AsBoolean;
            case VarType.Archive:
              return Guid.Empty.Equals(this.AsGuid);
            default:
              return this.Value == "";
          }
        }
      }

      public bool Invalid => this.AttrTypeID == 0 && !this.New;

      [Flags]
      internal enum ModifiedFlag
      {
        Name = 1,
        Value = 2,
        ValuesList = 4,
        Type = ValuesList, // 0x00000004
      }
    }
}

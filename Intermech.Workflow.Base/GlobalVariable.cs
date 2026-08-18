// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GlobalVariable
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace Intermech.Workflow
{
    public class GlobalVariable : IGlobalVariable
    {
      private IScheme _ownerScheme;
      private VarType _variableType;
      private VarKind _kind;
      private int _variableID;
      private IDBAttribute _variableAttribute;
      private float _asFloat;
      private Guid _asGuid;
      private string _name;
      private string _shortName;
      private object[] _addInfo;
      private bool _deleted;
      private string _value = string.Empty;
      private string _dateFormat = "dd'.'MM'.'yyyy H':'mm':'ss";
      public StringList _valuesList = new StringList();
      internal Guid _attrTypeGuid;
      private bool _inInit;
      private string _note;

      public int VariableID
      {
        get => this._variableID;
        set => this._variableID = value;
      }

      public VarKind Kind
      {
        get => this._kind;
        set => this._kind = value;
      }

      public VarType VariableType
      {
        get => this._variableType;
        set => this._variableType = value;
      }

      public IScheme OwnerScheme
      {
        get => this._ownerScheme;
        set => this._ownerScheme = value;
      }

      public GlobalVariable(IScheme ownerScheme) => this._ownerScheme = ownerScheme;

      private IDBAttribute VariableAttribute
      {
        get
        {
          return this._variableAttribute ?? (this._variableAttribute = this.OwnerScheme.GetAttributeByID(this.VariableID));
        }
      }

      public DateTime AsDateTime
      {
        get => this.VariableAttribute.AsDateTime;
        set => this.VariableAttribute.AsDateTime = value;
      }

      public float AsFloat
      {
        get => this._asFloat;
        set => this._asFloat = value;
      }

      public Guid AsGuid
      {
        get => this._asGuid;
        set => this._asGuid = value;
      }

      public long AsInteger
      {
        get => this.VariableAttribute.AsInteger;
        set => this.VariableAttribute.AsInteger = value;
      }

      public bool AsBoolean
      {
        get => this.VariableAttribute.AsBoolean;
        set => this.VariableAttribute.AsBoolean = value;
      }

      public string Value
      {
        get => this._value;
        set
        {
          if (this.VariableType == VarType.StringList && value != "" && !this.ValuesList.Contains(value))
            value = string.Empty;
          if (!(value != this._value))
            return;
          switch (this.VariableType)
          {
            case VarType.Integer:
              Convert.ToInt64(value);
              break;
            case VarType.Float:
              Convert.ToDouble(value);
              break;
            case VarType.DateTime:
              if (this.ToDateTime(value) == DateTime.MinValue)
              {
                Convert.ToDateTime(value);
                break;
              }
              break;
          }
          this._value = value;
        }
      }

      internal DateTime ToDateTime(string s)
      {
        DateTime result = DateTime.MinValue;
        if (!DateTime.TryParseExact(s, this._dateFormat, (IFormatProvider) null, DateTimeStyles.AssumeLocal, out result))
          DateTime.TryParse(s, out result);
        return result;
      }

      public StringList AsStringList
      {
        get
        {
          if (this._variableType != VarType.StringList)
            throw new Exception("Cannot access AsStringList, var type mismatch");
          StringList asStringList = new StringList();
          asStringList.AddRange((IEnumerable<string>) this.ValuesList);
          asStringList.Insert(0, this.Value);
          return asStringList;
        }
      }

      public StringList ValuesList
      {
        get => this._valuesList;
        set
        {
          if (this._valuesList.Equals((object) value))
            return;
          this._valuesList = value;
        }
      }

      /// <summary>Имя атрибута</summary>
      public string Name
      {
        get => this._name;
        set => this._name = value;
      }

      /// <summary>Короткое имя атрибута</summary>
      public string ShortName
      {
        get => this._shortName;
        set => this._shortName = value;
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
            (object) (this.VariableType == VarType.StringList ? this.AsStringList.Text : this.StoredValue)
          };
        }
        set
        {
          if (value == null || value.Length == 0)
            return;
          string str1 = value[0].ToString();
          if (this.VariableType == VarType.StringList)
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

      public bool Deleted
      {
        get => this._deleted;
        set => this._deleted = value;
      }

      public string StoredValue
      {
        get => this.Value;
        set
        {
          if (this.VariableType == VarType.ParticipantList && value == "<Participants />")
            value = string.Empty;
          try
          {
            this.Value = value;
          }
          catch (FormatException ex)
          {
          }
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
          switch (this.VariableType)
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
          switch (this.VariableType)
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
              if (value is Guid guid)
              {
                this.AsGuid = guid;
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

      public string Note => this._note;

      private void Init()
      {
        if (this._inInit)
          return;
        this._inInit = true;
        try
        {
          if (this.VariableID == 0 && this.AttrTypeGuid == Guid.Empty || !(this._name == ""))
            return;
          IMSAttributeType imsAttributeType = (IMSAttributeType) null;
          if (this.VariableID == 0)
          {
            if (this.AttrTypeGuid != Guid.Empty)
              imsAttributeType = MetaDataHelper.GetAttributeType(this.AttrTypeGuid);
            if (imsAttributeType != null)
              ;
          }
          else
            imsAttributeType = MetaDataHelper.GetAttributeType(this.VariableID);
          if (imsAttributeType != null)
          {
            this._name = imsAttributeType.Name;
            this._shortName = imsAttributeType.ShortName;
            this._variableType = MiscFunx.DetermineVarType(imsAttributeType);
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
    }
}

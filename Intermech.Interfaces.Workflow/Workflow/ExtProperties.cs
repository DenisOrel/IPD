// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ExtProperties
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech.Workflow;

public class ExtProperties
{
  private XmlIni _props;
  private int _attrID;
  private string _flags = "";

  protected XmlIni Props
  {
    get
    {
      XmlIni props = this._props;
      return this._props;
    }
  }

  public ExtProperties(IDBObject obj, int attributeID)
  {
    this._attrID = attributeID;
    this._props = new XmlIni();
    IDBAttribute attributeById = obj.GetAttributeByID(attributeID);
    if (attributeById == null)
      return;
    this._flags = attributeById.AsString;
    if (this._flags.StartsWith("<"))
      this._flags = "";
    this.LoadFromStream(attributeById as IBlobReader);
  }

  public ExtProperties(string xml)
  {
    this._props = new XmlIni();
    using (Stream stream = StreamHelper.StringToStream(xml))
      this._props.Load(stream);
  }

  public void Save(IDBObject activity)
  {
    if (!this.Modified)
      return;
    this.SaveToStream((activity.GetAttributeByID(this._attrID) ?? activity.Attributes.AddAttribute(this._attrID, false, (object[]) null)) as IBlobWriter, this.Flags);
  }

  private void SaveToStream(IBlobWriter writer, string note)
  {
    StreamHelper.SaveToBlobStream(writer, new ProcessStreamDelegate(this._props.Save), note);
  }

  private void LoadFromStream(IBlobReader reader)
  {
    StreamHelper.LoadFromBlobStream(reader, new ProcessStreamDelegate(this._props.Load));
  }

  public string Read(string Name) => this.Props.ReadString("Props", Name);

  public long ReadInteger(string Name) => this.Props.ReadInteger("Props", Name);

  public long ReadInteger(string Name, long DefaultValue)
  {
    return this.Props.ReadInteger("Props", Name, DefaultValue);
  }

  public bool ReadBool(string Name) => this.Props.ReadBoolean("Props", Name);

  /// <summary>
  /// Читает значения списка, разделенные запятыми. Если значений нет, возвращает null
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="Name"></param>
  /// <returns></returns>
  public List<T> ReadList<T>(string Name) where T : struct, IConvertible
  {
    List<T> objList = (List<T>) null;
    string str1 = this.Read(Name);
    if (str1 != "")
    {
      objList = new List<T>();
      string str2 = str1;
      string[] separator = new string[1]{ "," };
      foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        objList.Add((T) Convert.ChangeType((object) str3, typeof (T)));
    }
    return objList;
  }

  public List<T> ReadList<T>(string Name, List<T> defaultValue) where T : struct, IConvertible
  {
    List<T> objList = (List<T>) null;
    string str1 = this.Read(Name);
    if (str1 != "")
    {
      objList = new List<T>();
      string str2 = str1;
      string[] separator = new string[1]{ "," };
      foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        objList.Add((T) Convert.ChangeType((object) str3, typeof (T)));
    }
    return objList ?? defaultValue;
  }

  /// <returns>True если значение было модифицировано и его нужно сохранить</returns>
  public bool Write(string Name, long Value, ExtPropertiesFlag Flag, string DefaultValue = "")
  {
    return this.Write(Name, Value.ToString(), new ExtPropertiesFlag[1]
    {
      Flag
    }, DefaultValue);
  }

  /// <returns>True если значение было модифицировано и его нужно сохранить</returns>
  public bool Write(string Name, string Value, ExtPropertiesFlag Flag, string DefaultValue = "")
  {
    return this.Write(Name, Value, new ExtPropertiesFlag[1]
    {
      Flag
    }, DefaultValue);
  }

  /// <returns>True если значение было модифицировано и его нужно сохранить</returns>
  public bool Write(string Name, string Value, ExtPropertiesFlag[] Flags, string DefaultValue = "")
  {
    bool flag1 = this.Props.WriteString("Props", Name, Value, DefaultValue);
    if (Value != DefaultValue)
    {
      foreach (ExtPropertiesFlag flag2 in Flags)
      {
        if (!this.HasFlag(flag2))
        {
          this._flags += ((char) flag2).ToString();
          flag1 = true;
        }
      }
    }
    else
    {
      foreach (ExtPropertiesFlag flag3 in Flags)
      {
        FieldInfo field = flag3.GetType().GetField(flag3.ToString());
        if ((!(field != (FieldInfo) null) ? 0 : (field.GetCustomAttributes(typeof (MultiFlag), false).Length != 0 ? 1 : 0)) == 0 && this.HasFlag(flag3))
        {
          this._flags = this._flags.Replace(((char) flag3).ToString(), "");
          flag1 = true;
        }
      }
    }
    return flag1;
  }

  public void WriteBool(string Name, bool Value, ExtPropertiesFlag Flag)
  {
    this.Write(Name, Value ? "1" : "0", Flag, "0");
  }

  /// <summary>
  /// Сохраняет значения списка в строку через запятую. Возвращает True, если значение было записано.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="Name"></param>
  /// <param name="List"></param>
  /// <param name="Flag"></param>
  /// <returns></returns>
  public bool WriteList<T>(string Name, List<T> List, ExtPropertiesFlag Flag) where T : struct, IConvertible
  {
    string str = "";
    if (List != null)
      str = string.Join<T>(",", (IEnumerable<T>) List.ToArray());
    return this.Write(Name, str, Flag);
  }

  public bool Modified => this._props != null && this._props.Modified;

  public XmlIni Ini => this.Props;

  public string Flags => this._flags;

  public bool HasFlag(ExtPropertiesFlag Flag) => this._flags.Contains(((char) Flag).ToString());
}

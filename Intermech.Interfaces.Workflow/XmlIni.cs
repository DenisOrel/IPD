// Decompiled with JetBrains decompiler
// Type: Intermech.XmlIni
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech;

public class XmlIni
{
  private bool _modified;
  public string MainSection = "";
  private XmlNode _root;
  private XmlDocument _xml;

  public bool Modified => this._modified;

  public string ReadString(string Section, string Name, string DefaultValue)
  {
    if (this.MainSection != "")
    {
      if (Section != "")
        Section = "/" + Section;
      Section = this.MainSection + Section;
    }
    if (Section != "")
      Section += "/";
    XmlNode xmlNode = this.Root.SelectSingleNode(Section + Name);
    return xmlNode == null ? DefaultValue : xmlNode.InnerText;
  }

  public string ReadString(string Section, string Name) => this.ReadString(Section, Name, "");

  public XmlNode Root
  {
    get
    {
      if (this._root == null)
      {
        this._root = (XmlNode) this._xml.DocumentElement;
        if (this._root == null)
        {
          this._root = (XmlNode) this._xml.CreateElement("ini");
          this._xml.AppendChild(this._root);
        }
      }
      return this._root;
    }
  }

  private XmlNode createSection(string Section)
  {
    XmlNode newChild = (XmlNode) null;
    XmlNode xmlNode = this.Root;
    string str1 = Section;
    char[] chArray = new char[1]{ '/' };
    foreach (string str2 in str1.Split(chArray))
    {
      if (str2 == "")
      {
        newChild = xmlNode;
        break;
      }
      newChild = xmlNode.SelectSingleNode(str2);
      if (newChild == null)
      {
        newChild = (XmlNode) this._xml.CreateElement(str2);
        xmlNode.AppendChild(newChild);
        this._modified = true;
      }
      xmlNode = newChild;
    }
    return newChild != null ? newChild : throw new ArgumentException();
  }

  /// <returns>True если значение было модифицировано и его нужно сохранить</returns>
  public bool WriteString(string Section, string Name, string Value)
  {
    return this.WriteString(Section, Name, Value, "");
  }

  /// <returns>True если значение было модифицировано и его нужно сохранить</returns>
  public bool WriteString(string Section, string Name, string Value, string DefaultValue)
  {
    bool flag1 = false;
    if (this.MainSection != "")
    {
      if (Section != "")
        Section = "/" + Section;
      Section = this.MainSection + Section;
    }
    if (Section != "")
      Section += "/";
    bool flag2 = Value == DefaultValue;
    XmlNode oldChild = this.Root.SelectSingleNode(Section + Name);
    if (oldChild == null)
    {
      if (flag2)
        return false;
      XmlNode section = this.createSection(Section);
      oldChild = (XmlNode) this._xml.CreateElement(Name);
      XmlNode newChild = oldChild;
      section.AppendChild(newChild);
      flag1 = true;
    }
    else if (flag2)
    {
      oldChild.ParentNode.RemoveChild(oldChild);
      flag1 = true;
      oldChild = (XmlNode) null;
    }
    if (oldChild != null)
    {
      if (!flag1 && oldChild.InnerText != Value)
        flag1 = true;
      oldChild.InnerText = Value;
    }
    if (flag1)
      this._modified = true;
    return flag1;
  }

  public long ReadInteger(string Section, string Name, long DefaultValue)
  {
    string s = this.ReadString(Section, Name, "");
    if (s != "")
      long.TryParse(s, out DefaultValue);
    return DefaultValue;
  }

  public long ReadInteger(string Section, string Name) => this.ReadInteger(Section, Name, 0L);

  public void WriteInteger(string Section, string Name, long Value)
  {
    this.WriteString(Section, Name, Value.ToString());
  }

  public bool ReadBoolean(string Section, string Name, bool DefaultValue)
  {
    return Convert.ToBoolean(this.ReadInteger(Section, Name, (long) Convert.ToInt32(DefaultValue)));
  }

  public bool ReadBoolean(string Section, string Name) => this.ReadBoolean(Section, Name, false);

  public void WriteBoolean(string Section, string Name, bool Value)
  {
    this.WriteInteger(Section, Name, (long) Convert.ToInt32(Value));
  }

  public XmlIni() => this._xml = new XmlDocument();

  private void UnicodeToUTF8(Stream src, Stream dst)
  {
    using (StreamReader streamReader = new StreamReader(src, Encoding.Unicode))
    {
      string end = streamReader.ReadToEnd();
      Encoding.UTF8.GetBytes(end);
      StreamWriter streamWriter = new StreamWriter(dst, Encoding.UTF8);
      streamWriter.Write(end);
      streamWriter.Flush();
      dst.Position = 0L;
    }
  }

  public void Load(Stream stream)
  {
    if (stream.Length > 0L)
    {
      if (stream.Position != 0L)
        stream.Position = 0L;
      bool flag = false;
      byte[] buffer = new byte[4];
      if (stream.Read(buffer, 0, 4) == 4 && buffer[1] == (byte) 0 && buffer[3] == (byte) 0)
        flag = true;
      stream.Position = 0L;
      if (flag)
      {
        using (StreamReader txtReader = new StreamReader(stream, Encoding.Unicode))
          this._xml.Load((TextReader) txtReader);
      }
      else
        this._xml.Load(stream);
    }
    this._modified = false;
  }

  public void Save(Stream stream)
  {
    if (!(this._xml.FirstChild is XmlDeclaration))
      this._xml.InsertBefore((XmlNode) this._xml.CreateXmlDeclaration("1.0", (string) null, (string) null), this.Root);
    stream.Position = 0L;
    this._xml.Save(stream);
    this._modified = false;
  }

  public string AsString
  {
    get
    {
      using (StringWriter w = new StringWriter())
      {
        this._xml.WriteTo((XmlWriter) new XmlTextWriter((TextWriter) w));
        return w.ToString();
      }
    }
    set
    {
      if (value != "")
        this._xml.LoadXml(value);
      else
        this._xml.RemoveAll();
    }
  }
}

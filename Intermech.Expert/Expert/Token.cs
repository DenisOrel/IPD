// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Token
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.SelectionService;
using Intermech.Localization;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Elemental formula token</summary>
public class Token : ISaveLoad, ICloneable
{
  public static readonly int LeftAssoc = 0;
  public static readonly int RightAssoc = 1;
  public static readonly int _AsString = 61680;
  public static readonly int _Ref = 47802;
  public static readonly float _SIGN = 9E-19f;
  public TokenType type;
  private string _text;
  public long iValue;
  public double fValue;
  public string _Guid;
  public SelectionParameterTypes spt;
  public int info;
  public TokenState state;
  [NonSerialized]
  public Token.StackInfo stackInfo;
  [NonSerialized]
  public int StartPos;

  public string text
  {
    get => this.type == TokenType.Integer ? Convert.ToString(this.iValue) : this._text;
    set => this._text = value;
  }

  public string trueText => this._text;

  public string fullText(TempFormula tf)
  {
    if (this.type != TokenType.Attribute)
      return this.trueText;
    StringBuilder stringBuilder = new StringBuilder();
    int info = this.info;
    PairName pairName = tf.pairNames[info];
    if (pairName.objTypeLongName != "")
      stringBuilder.Append($"<{pairName.objTypeLongName}>.");
    if (pairName.attrLongName.StartsWith("<") && pairName.attrLongName.EndsWith(">"))
      stringBuilder.Append(pairName.attrLongName);
    else
      stringBuilder.Append($"<{pairName.attrLongName}>");
    return stringBuilder.ToString();
  }

  public Token(TokenType type, string text)
  {
    this.type = type;
    this._text = text;
    this.info = 0;
    this.AssignStackInfo();
    this._Guid = "";
  }

  public Token(Token t)
  {
    this.type = t.type;
    this._text = t._text;
    this.info = t.info;
    this.iValue = t.iValue;
    this.fValue = t.fValue;
    this.stackInfo = t.stackInfo;
    this._Guid = t._Guid;
    this.spt = t.spt;
  }

  public Token(ref BinaryReader br, int Version) => this.Load(br, Version);

  public Token(string _text)
  {
    this.text = _text;
    this.info = 0;
    string lower = this.text.Trim().ToLower(CultureInfo.CurrentUICulture);
    switch (lower)
    {
      case "":
        break;
      case ")":
        this.type = TokenType.ClosingBrace;
        break;
      case "(":
        this.type = TokenType.OpeningBrace;
        break;
      case "pi":
        this.type = TokenType.Float;
        this.fValue = Math.PI;
        break;
      case "e":
        this.type = TokenType.Float;
        this.fValue = Math.E;
        break;
      case "+":
      case "-":
      case "*":
      case "/":
      case "^":
      case ">":
      case "<":
      case ">=":
      case "<=":
      case "<>":
      case "=":
        this.type = TokenType.BinaryOper;
        this.text = $" {this.text} ";
        break;
      default:
        if (!lower.Equals(LocalizationHolder.rm.GetString("Expert_13")) && !lower.Equals(LocalizationHolder.rm.GetString("Expert_14")))
        {
          if (lower[lower.Length - 1] == '(')
          {
            string str = lower.Substring(0, lower.Length - 1);
            for (int index = 0; index < ExpertFunc._funcs.Length; ++index)
            {
              if (str == ExpertFunc._funcs[index].text)
              {
                this.info = ExpertFunc._funcs[index].info;
                this.type = TokenType.FuncCall;
              }
            }
            break;
          }
          this.InitLiteral(this.text);
          break;
        }
        goto case "+";
    }
  }

  public void InitLiteral(string s)
  {
    if (s != "" && (s[0] != '"' || s[s.Length - 1] != '"'))
    {
      switch (s.ToUpper())
      {
        case "ДА":
        case "Д":
        case "YES":
        case "Y":
          this.type = TokenType.Boolean;
          this.iValue = 1L;
          this.text = "ДА";
          return;
        case "НЕТ":
        case "Н":
        case "NO":
        case "N":
          this.type = TokenType.Boolean;
          this.iValue = 0L;
          this.text = "НЕТ";
          return;
        default:
          if (s.Trim() == ",")
          {
            this.type = TokenType.Divider;
            this.text = ", ";
            return;
          }
          if (long.TryParse(s, out this.iValue))
          {
            this.type = TokenType.Integer;
            this.text = s.Trim();
            return;
          }
          CultureInfo invariantCulture = CultureInfo.InvariantCulture;
          if (double.TryParse(s, NumberStyles.Float, (IFormatProvider) invariantCulture, out this.fValue))
          {
            this.type = TokenType.Float;
            this.text = s.Trim();
            return;
          }
          DateTime result;
          if (DateTime.TryParse(s, out result))
          {
            this.type = TokenType.Date;
            this.iValue = result.Ticks;
            this.text = s.Trim();
            return;
          }
          break;
      }
    }
    this.text = s.Length < 2 || s[0] != '"' || s[s.Length - 1] != '"' ? $"\"{s}\"" : s;
    this.text = this.text.Replace("\\n", "\n\r");
    this.type = TokenType.String;
  }

  public static event EventHandler BeautifyEvent;

  public void Beautify()
  {
    if (this.type == TokenType.BinaryOper && this.text != "")
    {
      if (!this.text.StartsWith(" "))
        this.text = " " + this.text;
      if (!this.text.EndsWith(" "))
        this.text += " ";
    }
    if (this.type != TokenType.Integer || this.spt == SelectionParameterTypes.sptBlob || this.spt == SelectionParameterTypes.sptNone)
      return;
    EventHandler beautifyEvent = Token.BeautifyEvent;
    if (beautifyEvent == null)
      return;
    beautifyEvent((object) this, (EventArgs) null);
  }

  public int InputPrec
  {
    get => this.stackInfo != null ? this.stackInfo.InputPrec : 0;
    set
    {
      if (this.stackInfo == null)
        return;
      this.stackInfo.InputPrec = value;
    }
  }

  public int StackPrec
  {
    get => this.stackInfo != null ? this.stackInfo.StackPrec : 0;
    set
    {
      if (this.stackInfo == null)
        return;
      this.stackInfo.StackPrec = value;
    }
  }

  public int Rank
  {
    get => this.stackInfo != null ? this.stackInfo.Rank : 0;
    set
    {
      if (this.stackInfo == null)
        return;
      this.stackInfo.Rank = value;
    }
  }

  public void AssignStackInfo()
  {
    this.stackInfo = new Token.StackInfo();
    switch (this.type)
    {
      case TokenType.UnaryOper:
        if (!this.text.Trim().Equals("-") && !this.text.Trim().Equals(LocalizationHolder.rm.GetString("Expert_15")))
          break;
        this.InputPrec = 0;
        this.StackPrec = 10;
        this.Rank = 0;
        break;
      case TokenType.BinaryOper:
        if (this.text.Trim().Equals(":"))
        {
          this.InputPrec = 0;
          this.StackPrec = 0;
          this.Rank = -1;
          break;
        }
        if (this.text.Trim().Equals(LocalizationHolder.rm.GetString("Expert_16")))
        {
          this.InputPrec = 1;
          this.StackPrec = 1;
          this.Rank = -1;
          break;
        }
        if (this.text.Trim().Equals(LocalizationHolder.rm.GetString("Expert_17")))
        {
          this.InputPrec = 2;
          this.StackPrec = 2;
          this.Rank = -1;
          break;
        }
        if (this.text.Trim().Equals("<") || this.text.Trim().Equals("<=") || this.text.Trim().Equals("=") || this.text.Trim().Equals("<>") || this.text.Trim().Equals(">") || this.text.Trim().Equals(">="))
        {
          this.InputPrec = 3;
          this.StackPrec = 3;
          this.Rank = -1;
          break;
        }
        if (this.text.Trim().Equals("+") || this.text.Trim().Equals("-"))
        {
          this.InputPrec = 4;
          this.StackPrec = 4;
          this.Rank = -1;
          break;
        }
        if (this.text.Trim().Equals("*") || this.text.Trim().Equals("/"))
        {
          this.InputPrec = 5;
          this.StackPrec = 5;
          this.Rank = -1;
          break;
        }
        if (this.text.Trim().Equals("^"))
        {
          this.InputPrec = 7;
          this.StackPrec = 6;
          this.Rank = -1;
          this.info = Token.RightAssoc;
          break;
        }
        if (!this.text.Trim().Equals("?"))
          break;
        this.InputPrec = 11;
        this.StackPrec = 6;
        this.Rank = -1;
        break;
      case TokenType.OpeningBrace:
        this.InputPrec = 8;
        this.StackPrec = -1;
        this.Rank = 0;
        switch (this.text)
        {
          case "(":
            this.info = 0;
            return;
          case "[":
            this.info = 1;
            return;
          case "{":
            this.info = 2;
            return;
          default:
            return;
        }
      case TokenType.ClosingBrace:
        this.InputPrec = 0;
        this.StackPrec = 0;
        this.Rank = 0;
        switch (this.text)
        {
          case ")":
            this.info = 0;
            return;
          case "]":
            this.info = 1;
            return;
          case "}":
            this.info = 2;
            return;
          default:
            return;
        }
      case TokenType.FuncCall:
        this.InputPrec = 8;
        this.StackPrec = -1;
        this.Rank = 0;
        break;
      default:
        this.InputPrec = 0;
        this.StackPrec = 0;
        this.Rank = 1;
        break;
    }
  }

  public void Load(BinaryReader br, int Version)
  {
    this.type = (TokenType) br.ReadInt32();
    this._text = br.ReadString();
    int length = this._text.IndexOf('@');
    if (length >= 0)
    {
      this._Guid = this._text.Substring(0, length);
      this._text = this._text.Remove(0, length + 1);
      int startIndex = this._Guid.IndexOf("|");
      if (startIndex >= 0)
      {
        string str = this._Guid.Substring(startIndex + 1, this._Guid.Length - startIndex - 1);
        if (str != "")
          this.spt = (SelectionParameterTypes) Convert.ToInt32(str);
        this._Guid = this._Guid.Remove(startIndex);
      }
    }
    else
      this._Guid = "";
    this.iValue = br.ReadInt64();
    this.fValue = br.ReadDouble();
    this.info = br.ReadInt32();
    this.AssignStackInfo();
  }

  public void Save(BinaryWriter bw) => this.SaveAsVer(bw, ExpertConsts.FormulaVersion);

  public void SaveAsVer(BinaryWriter bw, int Version)
  {
    bw.Write((int) this.type);
    string str1;
    if (!(this._Guid == "") && this._Guid != null)
      str1 = $"{this._Guid}|{Convert.ToString((int) this.spt)}@{this._text}";
    else
      str1 = this._text;
    string str2 = str1;
    bw.Write(str2);
    bw.Write(this.iValue);
    bw.Write(this.fValue);
    bw.Write(this.info);
  }

  public static event Token.AfterLoadTokenHandler AfterLoadToken;

  internal void OnAfterLoadToken(Token t)
  {
    Token.AfterLoadTokenHandler afterLoadToken = Token.AfterLoadToken;
    if (afterLoadToken == null)
      return;
    afterLoadToken(t);
  }

  public void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteStartElement(nameof (Token));
    writer.WriteAttributeString("type", Convert.ToString((int) this.type));
    writer.WriteAttributeString("info", Convert.ToString(this.info));
    XmlTextWriter xmlTextWriter = writer;
    string str;
    if (!(this._Guid == "") && this._Guid != null)
      str = $"{this._Guid}|{Convert.ToString((int) this.spt)}@{this._text}";
    else
      str = this._text;
    xmlTextWriter.WriteElementString("text", str);
    if (this.type == TokenType.Command || this.type == TokenType.Date || this.type == TokenType.Integer || this.type == TokenType.Attribute || this.type == TokenType.Measured || this.type == TokenType.Boolean || this.type == TokenType.FuncCall)
      writer.WriteElementString("i-value", Convert.ToString(this.iValue));
    if (!this.type.OneOf<TokenType>(TokenType.Float, TokenType.Measured))
    {
      if (!this.type.OneOf<TokenType>(TokenType.Attribute, TokenType.FuncCall) || Math.Abs(this.fValue) <= 9.9999996826552254E-21)
        goto label_8;
    }
    writer.WriteElementString("f-value", Convert.ToString(this.fValue));
label_8:
    if (this.spt != SelectionParameterTypes.sptNone)
      writer.WriteElementString("sel-parmTypes", Convert.ToString((int) this.spt));
    writer.WriteEndElement();
  }

  public Token(XmlNode node)
  {
    if (node.NodeType != XmlNodeType.Element || node.Name != nameof (Token))
      throw new AbortException("Wrong XML node for token!");
    this.type = TokenType.String;
    this.info = 0;
    if (node.Attributes != null)
    {
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) node.Attributes)
      {
        if (attribute.Name == nameof (type))
          this.type = (TokenType) Convert.ToInt32(attribute.Value);
        else if (attribute.Name == nameof (info))
          this.info = Convert.ToInt32(attribute.Value);
      }
    }
    if (node.HasChildNodes)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (text))
        {
          this._text = childNode.InnerText;
          int length = this._text.IndexOf('@');
          if (length >= 0)
          {
            this._Guid = this._text.Substring(0, length);
            this._text = this._text.Remove(0, length + 1);
            int startIndex = this._Guid.IndexOf("|");
            if (startIndex >= 0)
            {
              string str = this._Guid.Substring(startIndex + 1, this._Guid.Length - startIndex - 1);
              if (str != "")
                this.spt = (SelectionParameterTypes) Convert.ToInt32(str);
              this._Guid = this._Guid.Remove(startIndex);
            }
            if (GuidHelper.IsGuid(this._Guid))
            {
              try
              {
                Guid guid = new Guid(this._Guid);
              }
              catch
              {
                this._Guid = "";
              }
            }
            else
              this._Guid = "";
          }
          else
            this._Guid = "";
        }
        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "i-value")
          this.iValue = Convert.ToInt64(childNode.InnerText);
        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "f-value")
        {
          string innerText = childNode.InnerText;
          try
          {
            this.fValue = Convert.ToDouble(innerText);
          }
          catch (FormatException ex)
          {
            this.fValue = Convert.ToDouble(innerText.IndexOf(".") < 0 ? innerText.Replace(",", ".") : innerText.Replace(".", ","));
          }
        }
        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "sel-parmTypes")
          this.spt = (SelectionParameterTypes) Convert.ToInt32(childNode.InnerText);
      }
    }
    this.AssignStackInfo();
    this.OnAfterLoadToken(this);
  }

  public object Clone() => (object) new Token(this);

  public Token CloneToken() => (Token) this.Clone();

  public class StackInfo
  {
    public int InputPrec;
    public int StackPrec;
    public int Rank;
  }

  public delegate void AfterLoadTokenHandler(Token t);
}

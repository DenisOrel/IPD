// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Tokenizer
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Workflow
{
    public class Tokenizer
    {
      private string _src = "";
      private int _index;
      private int _literalCharIndex;
      public List<char> Delimiters = new List<char>((IEnumerable<char>) new char[2]
      {
        ' ',
        Convert.ToChar(9)
      });
      public string[] Literals = new string[13]
      {
        "(",
        ")",
        "^",
        "*",
        "/",
        "+",
        "-",
        "=",
        "<>",
        "<=",
        "<",
        ">=",
        ">"
      };

      public Tokenizer(string source) => this._src = source.Trim(this.Delimiters.ToArray());

      public bool EOS => this._index >= this._src.Length;

      public string NextToken()
      {
        StringBuilder stringBuilder1 = (StringBuilder) null;
        StringBuilder stringBuilder2 = (StringBuilder) null;
        this._literalCharIndex = 0;
        while (!this.EOS)
        {
          if (this.Delimiters.Contains(this._src[this._index]))
          {
            if (stringBuilder1 != null)
            {
              ++this._index;
              return stringBuilder1.ToString();
            }
          }
          else
          {
            bool flag = false;
            foreach (string literal in this.Literals)
            {
              if (this._literalCharIndex < literal.Length && (int) literal[this._literalCharIndex] == (int) this._src[this._index])
              {
                if (stringBuilder2 == null)
                  stringBuilder2 = new StringBuilder();
                stringBuilder2.Append(this._src[this._index]);
                ++this._literalCharIndex;
                flag = true;
                break;
              }
            }
            if (flag && stringBuilder1 != null)
              return stringBuilder1.ToString();
            if (!flag && stringBuilder2 != null)
              return stringBuilder2.ToString();
            if (stringBuilder2 == null)
            {
              if (stringBuilder1 == null)
                stringBuilder1 = new StringBuilder();
              stringBuilder1.Append(this._src[this._index]);
            }
          }
          ++this._index;
        }
        if (stringBuilder1 != null)
          return stringBuilder1.ToString();
        return stringBuilder2?.ToString();
      }
    }
}

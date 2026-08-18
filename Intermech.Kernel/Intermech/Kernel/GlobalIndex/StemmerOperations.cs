// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.StemmerOperations
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Text;


namespace Intermech.Kernel.GlobalIndex;

public class StemmerOperations
{
  protected StringBuilder current;
  protected int cursor;
  protected int limit;
  protected int limit_backward;
  protected int bra;
  protected int ket;

  protected StemmerOperations()
  {
    this.current = new StringBuilder();
    this.setCurrent("");
  }

  protected void setCurrent(string value)
  {
    this.current.Remove(0, this.current.Length);
    this.current.Append(value);
    this.cursor = 0;
    this.limit = this.current.Length;
    this.limit_backward = 0;
    this.bra = this.cursor;
    this.ket = this.limit;
  }

  protected string getCurrent() => this.current.ToString();

  protected void copy_from(StemmerOperations other)
  {
    this.current = other.current;
    this.cursor = other.cursor;
    this.limit = other.limit;
    this.limit_backward = other.limit_backward;
    this.bra = other.bra;
    this.ket = other.ket;
  }

  protected bool in_grouping(char[] s, int min, int max)
  {
    if (this.cursor >= this.limit)
      return false;
    int num1 = (int) this.current[this.cursor];
    if (num1 > max || num1 < min)
      return false;
    int num2 = num1 - min;
    if (((int) s[num2 >> 3] & 1 << (num2 & 7)) == 0)
      return false;
    ++this.cursor;
    return true;
  }

  protected bool in_grouping_b(char[] s, int min, int max)
  {
    if (this.cursor <= this.limit_backward)
      return false;
    int num1 = (int) this.current[this.cursor - 1];
    if (num1 > max || num1 < min)
      return false;
    int num2 = num1 - min;
    if (((int) s[num2 >> 3] & 1 << (num2 & 7)) == 0)
      return false;
    --this.cursor;
    return true;
  }

  protected bool out_grouping(char[] s, int min, int max)
  {
    if (this.cursor >= this.limit)
      return false;
    int num1 = (int) this.current[this.cursor];
    if (num1 > max || num1 < min)
    {
      ++this.cursor;
      return true;
    }
    int num2 = num1 - min;
    if (((int) s[num2 >> 3] & 1 << (num2 & 7)) != 0)
      return false;
    ++this.cursor;
    return true;
  }

  protected bool out_grouping_b(char[] s, int min, int max)
  {
    if (this.cursor <= this.limit_backward)
      return false;
    int num1 = (int) this.current[this.cursor - 1];
    if (num1 > max || num1 < min)
    {
      --this.cursor;
      return true;
    }
    int num2 = num1 - min;
    if (((int) s[num2 >> 3] & 1 << (num2 & 7)) != 0)
      return false;
    --this.cursor;
    return true;
  }

  protected bool in_range(int min, int max)
  {
    if (this.cursor >= this.limit)
      return false;
    int num = (int) this.current[this.cursor];
    if (num > max || num < min)
      return false;
    ++this.cursor;
    return true;
  }

  protected bool in_range_b(int min, int max)
  {
    if (this.cursor <= this.limit_backward)
      return false;
    int num = (int) this.current[this.cursor - 1];
    if (num > max || num < min)
      return false;
    --this.cursor;
    return true;
  }

  protected bool out_range(int min, int max)
  {
    if (this.cursor >= this.limit)
      return false;
    int num = (int) this.current[this.cursor];
    if (num <= max && num >= min)
      return false;
    ++this.cursor;
    return true;
  }

  protected bool out_range_b(int min, int max)
  {
    if (this.cursor <= this.limit_backward)
      return false;
    int num = (int) this.current[this.cursor - 1];
    if (num <= max && num >= min)
      return false;
    --this.cursor;
    return true;
  }

  protected bool eq_s(int s_size, string s)
  {
    if (this.limit - this.cursor < s_size)
      return false;
    for (int index = 0; index != s_size; ++index)
    {
      if ((int) this.current[this.cursor + index] != (int) s[index])
        return false;
    }
    this.cursor += s_size;
    return true;
  }

  protected bool eq_s_b(int s_size, string s)
  {
    if (this.cursor - this.limit_backward < s_size)
      return false;
    for (int index = 0; index != s_size; ++index)
    {
      if ((int) this.current[this.cursor - s_size + index] != (int) s[index])
        return false;
    }
    this.cursor -= s_size;
    return true;
  }

  protected bool eq_v(StringBuilder s) => this.eq_s(s.Length, s.ToString());

  protected bool eq_v_b(StringBuilder s) => this.eq_s_b(s.Length, s.ToString());

  internal int find_among(Among[] v, int v_size)
  {
    int index1 = 0;
    int num1 = v_size;
    int cursor = this.cursor;
    int limit = this.limit;
    int num2 = 0;
    int num3 = 0;
    bool flag = false;
    while (true)
    {
      do
      {
        int index2 = index1 + (num1 - index1 >> 1);
        int num4 = 0;
        int num5 = num2 < num3 ? num2 : num3;
        Among among = v[index2];
        for (int index3 = num5; index3 < among.s_size; ++index3)
        {
          if (cursor + num5 == limit)
          {
            num4 = -1;
            break;
          }
          num4 = (int) this.current[cursor + num5] - (int) among.s[index3];
          if (num4 == 0)
            ++num5;
          else
            break;
        }
        if (num4 < 0)
        {
          num1 = index2;
          num3 = num5;
        }
        else
        {
          index1 = index2;
          num2 = num5;
        }
      }
      while (num1 - index1 > 1);
      if (index1 <= 0 && num1 != index1 && !flag)
        flag = true;
      else
        break;
    }
    do
    {
      Among among = v[index1];
      if (num2 >= among.s_size)
      {
        this.cursor = cursor + among.s_size;
        if (among.method == null)
          return among.result;
      }
      index1 = among.substring_i;
    }
    while (index1 >= 0);
    return 0;
  }

  internal int find_among_b(Among[] v, int v_size)
  {
    int index1 = 0;
    int num1 = v_size;
    int cursor = this.cursor;
    int limitBackward = this.limit_backward;
    int num2 = 0;
    int num3 = 0;
    bool flag = false;
    while (true)
    {
      do
      {
        int index2 = index1 + (num1 - index1 >> 1);
        int num4 = 0;
        int num5 = num2 < num3 ? num2 : num3;
        Among among = v[index2];
        for (int index3 = among.s_size - 1 - num5; index3 >= 0; --index3)
        {
          if (cursor - num5 == limitBackward)
          {
            num4 = -1;
            break;
          }
          num4 = (int) this.current[cursor - 1 - num5] - (int) among.s[index3];
          if (num4 == 0)
            ++num5;
          else
            break;
        }
        if (num4 < 0)
        {
          num1 = index2;
          num3 = num5;
        }
        else
        {
          index1 = index2;
          num2 = num5;
        }
      }
      while (num1 - index1 > 1);
      if (index1 <= 0 && num1 != index1 && !flag)
        flag = true;
      else
        break;
    }
    do
    {
      Among among = v[index1];
      if (num2 >= among.s_size)
      {
        this.cursor = cursor - among.s_size;
        if (among.method == null)
          return among.result;
      }
      index1 = among.substring_i;
    }
    while (index1 >= 0);
    return 0;
  }

  protected int replace_s(int c_bra, int c_ket, string s)
  {
    int num = s.Length - (c_ket - c_bra);
    this.current = this.StringBufferReplace(c_bra, c_ket, this.current, s);
    this.limit += num;
    if (this.cursor >= c_ket)
      this.cursor += num;
    else if (this.cursor > c_bra)
      this.cursor = c_bra;
    return num;
  }

  private StringBuilder StringBufferReplace(int start, int end, StringBuilder s, string s1)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < start; ++index)
      stringBuilder.Insert(stringBuilder.Length, s[index]);
    stringBuilder.Insert(stringBuilder.Length, s1);
    for (int index = end; index < s.Length; ++index)
      stringBuilder.Insert(stringBuilder.Length, s[index]);
    return stringBuilder;
  }

  protected void slice_check()
  {
    if (this.bra < 0 || this.bra > this.ket || this.ket > this.limit)
      return;
    int limit = this.limit;
    int length = this.current.Length;
  }

  protected void slice_from(string s)
  {
    this.slice_check();
    this.replace_s(this.bra, this.ket, s);
  }

  protected void slice_from(StringBuilder s) => this.slice_from(s.ToString());

  protected void slice_del() => this.slice_from("");

  protected void insert(int c_bra, int c_ket, string s)
  {
    int num = this.replace_s(c_bra, c_ket, s);
    if (c_bra <= this.bra)
      this.bra += num;
    if (c_bra > this.ket)
      return;
    this.ket += num;
  }

  protected void insert(int c_bra, int c_ket, StringBuilder s)
  {
    this.insert(c_bra, c_ket, s.ToString());
  }

  protected StringBuilder slice_to(StringBuilder s)
  {
    this.slice_check();
    int length = this.ket - this.bra;
    return this.StringBufferReplace(0, s.Length, s, this.current.ToString().Substring(this.bra, length));
  }

  protected StringBuilder assign_to(StringBuilder s)
  {
    return this.StringBufferReplace(0, s.Length, s, this.current.ToString().Substring(0, this.limit));
  }

  protected void removeDerivational()
  {
    int length = this.current.Length;
    if (length > 8 && this.current.ToString().Substring(length - 6, 6).Equals("obinec"))
    {
      this.current = this.current.Remove(length - 6, 6);
    }
    else
    {
      if (length > 7)
      {
        if (this.current.ToString().Substring(length - 5, 5).Equals("ionář"))
        {
          this.current = this.current.Remove(length - 4, 4);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 5, 5).Equals("ovisk") || this.current.ToString().Substring(length - 5, 5).Equals("ovstv") || this.current.ToString().Substring(length - 5, 5).Equals("ovišt") || this.current.ToString().Substring(length - 5, 5).Equals("ovník"))
        {
          this.current = this.current.Remove(length - 5, 5);
          return;
        }
      }
      if (length > 6)
      {
        if (this.current.ToString().Substring(length - 4, 4).Equals("ásek") || this.current.ToString().Substring(length - 4, 4).Equals("loun") || this.current.ToString().Substring(length - 4, 4).Equals("nost") || this.current.ToString().Substring(length - 4, 4).Equals("teln") || this.current.ToString().Substring(length - 4, 4).Equals("ovec") || this.current.ToString().Substring(length - 5, 5).Equals("ovík") || this.current.ToString().Substring(length - 4, 4).Equals("ovtv") || this.current.ToString().Substring(length - 4, 4).Equals("ovin") || this.current.ToString().Substring(length - 4, 4).Equals("štin"))
        {
          this.current = this.current.Remove(length - 4, 4);
          return;
        }
        if (this.current.ToString().Substring(length - 4, 4).Equals("enic") || this.current.ToString().Substring(length - 4, 4).Equals("inec") || this.current.ToString().Substring(length - 4, 4).Equals("itel"))
        {
          this.current = this.current.Remove(length - 3, 3);
          this.palatalise();
          return;
        }
      }
      if (length > 5)
      {
        if (this.current.ToString().Substring(length - 3, 3).Equals("árn"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("ěnk"))
        {
          this.current = this.current.Remove(length - 2, 2);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("ián") || this.current.ToString().Substring(length - 3, 3).Equals("ist") || this.current.ToString().Substring(length - 3, 3).Equals("isk") || this.current.ToString().Substring(length - 3, 3).Equals("išt") || this.current.ToString().Substring(length - 3, 3).Equals("itb") || this.current.ToString().Substring(length - 3, 3).Equals("írn"))
        {
          this.current = this.current.Remove(length - 2, 2);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("och") || this.current.ToString().Substring(length - 3, 3).Equals("ost") || this.current.ToString().Substring(length - 3, 3).Equals("ovn") || this.current.ToString().Substring(length - 3, 3).Equals("oun") || this.current.ToString().Substring(length - 3, 3).Equals("out") || this.current.ToString().Substring(length - 3, 3).Equals("ouš"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("ušk"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("kyn") || this.current.ToString().Substring(length - 3, 3).Equals("čan") || this.current.ToString().Substring(length - 3, 3).Equals("kář") || this.current.ToString().Substring(length - 3, 3).Equals("néř") || this.current.ToString().Substring(length - 3, 3).Equals("ník") || this.current.ToString().Substring(length - 3, 3).Equals("ctv") || this.current.ToString().Substring(length - 3, 3).Equals("stv"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
      }
      if (length > 4)
      {
        if (this.current.ToString().Substring(length - 2, 2).Equals("áč") || this.current.ToString().Substring(length - 2, 2).Equals("ač") || this.current.ToString().Substring(length - 2, 2).Equals("án") || this.current.ToString().Substring(length - 2, 2).Equals("an") || this.current.ToString().Substring(length - 2, 2).Equals("ář") || this.current.ToString().Substring(length - 2, 2).Equals("as"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("ec") || this.current.ToString().Substring(length - 2, 2).Equals("en") || this.current.ToString().Substring(length - 2, 2).Equals("ěn") || this.current.ToString().Substring(length - 2, 2).Equals("éř"))
        {
          this.current = this.current.Remove(length - 1, 1);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("íř") || this.current.ToString().Substring(length - 2, 2).Equals("ic") || this.current.ToString().Substring(length - 2, 2).Equals("in") || this.current.ToString().Substring(length - 2, 2).Equals("ín") || this.current.ToString().Substring(length - 2, 2).Equals("it") || this.current.ToString().Substring(length - 2, 2).Equals("iv"))
        {
          this.current = this.current.Remove(length - 1, 1);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("ob") || this.current.ToString().Substring(length - 2, 2).Equals("ot") || this.current.ToString().Substring(length - 2, 2).Equals("ov") || this.current.ToString().Substring(length - 2, 2).Equals("oň"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("ul"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("yn"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("čk") || this.current.ToString().Substring(length - 2, 2).Equals("čn") || this.current.ToString().Substring(length - 2, 2).Equals("dl") || this.current.ToString().Substring(length - 2, 2).Equals("nk") || this.current.ToString().Substring(length - 2, 2).Equals("tv") || this.current.ToString().Substring(length - 2, 2).Equals("tk") || this.current.ToString().Substring(length - 2, 2).Equals("vk"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
      }
      if (length <= 3 || this.current.ToString()[this.current.Length - 1] != 'c' && this.current.ToString()[this.current.Length - 1] != 'č' && this.current.ToString()[this.current.Length - 1] != 'k' && this.current.ToString()[this.current.Length - 1] != 'l' && this.current.ToString()[this.current.Length - 1] != 'n' && this.current.ToString()[this.current.Length - 1] != 't')
        return;
      this.current = this.current.Remove(length - 1, 1);
    }
  }

  protected void removeAugmentative()
  {
    int length = this.current.Length;
    if (length > 6 && this.current.ToString().Substring(length - 4, 4).Equals("ajzn"))
      this.current = this.current.Remove(length - 4, 4);
    else if (length > 5 && (this.current.ToString().Substring(length - 3, 3).Equals("izn") || this.current.ToString().Substring(length - 3, 3).Equals("isk")))
    {
      this.current = this.current.Remove(length - 2, 2);
      this.palatalise();
    }
    else
    {
      if (length <= 4 || !this.current.ToString().Substring(length - 2, 2).Equals("\00e1k"))
        return;
      this.current = this.current.Remove(length - 2, 2);
    }
  }

  protected void removeDiminutive()
  {
    int length = this.current.Length;
    if (length > 7 && this.current.ToString().Substring(length - 5, 5).Equals("oušek"))
    {
      this.current = this.current.Remove(length - 5, 5);
    }
    else
    {
      if (length > 6)
      {
        if (this.current.ToString().Substring(length - 4, 4).Equals("eček") || this.current.ToString().Substring(length - 4, 4).Equals("éček") || this.current.ToString().Substring(length - 4, 4).Equals("iček") || this.current.ToString().Substring(length - 4, 4).Equals("íček") || this.current.ToString().Substring(length - 4, 4).Equals("enek") || this.current.ToString().Substring(length - 4, 4).Equals("ének") || this.current.ToString().Substring(length - 4, 4).Equals("inek") || this.current.ToString().Substring(length - 4, 4).Equals("ínek"))
        {
          this.current = this.current.Remove(length - 3, 3);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 4, 4).Equals("áček") || this.current.ToString().Substring(length - 4, 4).Equals("aček") || this.current.ToString().Substring(length - 4, 4).Equals("oček") || this.current.ToString().Substring(length - 4, 4).Equals("uček") || this.current.ToString().Substring(length - 4, 4).Equals("anek") || this.current.ToString().Substring(length - 4, 4).Equals("onek") || this.current.ToString().Substring(length - 4, 4).Equals("unek") || this.current.ToString().Substring(length - 4, 4).Equals("ánek"))
        {
          this.current = this.current.Remove(length - 4, 4);
          return;
        }
      }
      if (length > 5)
      {
        if (this.current.ToString().Substring(length - 3, 3).Equals("ečk") || this.current.ToString().Substring(length - 3, 3).Equals("éčk") || this.current.ToString().Substring(length - 3, 3).Equals("ičk") || this.current.ToString().Substring(length - 3, 3).Equals("íčk") || this.current.ToString().Substring(length - 3, 3).Equals("enk") || this.current.ToString().Substring(length - 3, 3).Equals("énk") || this.current.ToString().Substring(length - 3, 3).Equals("ink") || this.current.ToString().Substring(length - 3, 3).Equals("ínk"))
        {
          this.current = this.current.Remove(length - 3, 3);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("áčk") || this.current.ToString().Substring(length - 3, 3).Equals("au010dk") || this.current.ToString().Substring(length - 3, 3).Equals("očk") || this.current.ToString().Substring(length - 3, 3).Equals("učk") || this.current.ToString().Substring(length - 3, 3).Equals("ank") || this.current.ToString().Substring(length - 3, 3).Equals("onk") || this.current.ToString().Substring(length - 3, 3).Equals("unk"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("átk") || this.current.ToString().Substring(length - 3, 3).Equals("ánk") || this.current.ToString().Substring(length - 3, 3).Equals("ušk"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
      }
      if (length > 4)
      {
        if (this.current.ToString().Substring(length - 2, 2).Equals("ek") || this.current.ToString().Substring(length - 2, 2).Equals("ék") || this.current.ToString().Substring(length - 2, 2).Equals("ík") || this.current.ToString().Substring(length - 2, 2).Equals("ik"))
        {
          this.current = this.current.Remove(length - 1, 1);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("ák") || this.current.ToString().Substring(length - 2, 2).Equals("ak") || this.current.ToString().Substring(length - 2, 2).Equals("ok") || this.current.ToString().Substring(length - 2, 2).Equals("uk"))
        {
          this.current = this.current.Remove(length - 1, 1);
          return;
        }
      }
      if (length <= 3 || !this.current.ToString().Substring(length - 1, 1).Equals("k"))
        return;
      this.current = this.current.Remove(length - 1, 1);
    }
  }

  protected void removeComparative()
  {
    int length = this.current.Length;
    if (length <= 5 || !this.current.ToString().Substring(length - 3, 3).Equals("ejš") && !this.current.ToString().Substring(length - 3, 3).Equals("ějš"))
      return;
    this.current = this.current.Remove(length - 2, 2);
    this.palatalise();
  }

  private void palatalise()
  {
    int length = this.current.Length;
    if (this.current.ToString().Substring(length - 2, 2).Equals("ci") || this.current.ToString().Substring(length - 2, 2).Equals("ce") || this.current.ToString().Substring(length - 2, 2).Equals("či") || this.current.ToString().Substring(length - 2, 2).Equals("če"))
      this.current = this.StringBufferReplace(length - 2, length, this.current, "k");
    else if (this.current.ToString().Substring(length - 2, 2).Equals("zi") || this.current.ToString().Substring(length - 2, 2).Equals("ze") || this.current.ToString().Substring(length - 2, 2).Equals("ži") || this.current.ToString().Substring(length - 2, 2).Equals("že"))
      this.current = this.StringBufferReplace(length - 2, length, this.current, "h");
    else if (this.current.ToString().Substring(length - 3, 3).Equals("čtě") || this.current.ToString().Substring(length - 3, 3).Equals("čti") || this.current.ToString().Substring(length - 3, 3).Equals("čtí"))
      this.current = this.StringBufferReplace(length - 3, length, this.current, "ck");
    else if (this.current.ToString().Substring(length - 2, 2).Equals("ště") || this.current.ToString().Substring(length - 2, 2).Equals("šti") || this.current.ToString().Substring(length - 2, 2).Equals("ští"))
      this.current = this.StringBufferReplace(length - 2, length, this.current, "sk");
    else
      this.current = this.current.Remove(length - 1, 1);
  }

  protected void removePossessives()
  {
    int length = this.current.Length;
    if (length <= 5)
      return;
    if (this.current.ToString().Substring(length - 2, 2).Equals("ov"))
      this.current = this.current.Remove(length - 2, 2);
    else if (this.current.ToString().Substring(length - 2, 2).Equals("ův"))
    {
      this.current = this.current.Remove(length - 2, 2);
    }
    else
    {
      if (!this.current.ToString().Substring(length - 2, 2).Equals("in"))
        return;
      this.current = this.current.Remove(length - 1, 1);
      this.palatalise();
    }
  }

  protected void removeCase()
  {
    int length = this.current.Length;
    if (length > 7 && this.current.ToString().Substring(length - 5, 5).Equals("atech"))
    {
      this.current = this.current.Remove(length - 5, 5);
    }
    else
    {
      if (length > 6)
      {
        if (this.current.ToString().Substring(length - 4, 4).Equals("ětem"))
        {
          this.current = this.current.Remove(length - 3, 3);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 4, 4).Equals("atům"))
        {
          this.current = this.current.Remove(length - 4, 4);
          return;
        }
      }
      if (length > 5)
      {
        if (this.current.ToString().Substring(length - 3, 3).Equals("ech") || this.current.ToString().Substring(length - 3, 3).Equals("ich") || this.current.ToString().Substring(length - 3, 3).Equals("ích"))
        {
          this.current = this.current.Remove(length - 2, 2);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("ého") || this.current.ToString().Substring(length - 3, 3).Equals("ěmi") || this.current.ToString().Substring(length - 3, 3).Equals("emi") || this.current.ToString().Substring(length - 3, 3).Equals("ému") || this.current.ToString().Substring(length - 3, 3).Equals("eti") || this.current.ToString().Substring(length - 3, 3).Equals("iho") || this.current.ToString().Substring(length - 3, 3).Equals("ího") || this.current.ToString().Substring(length - 3, 3).Equals("ími") || this.current.ToString().Substring(length - 3, 3).Equals("imu"))
        {
          this.current = this.current.Remove(length - 2, 2);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 3, 3).Equals("ách") || this.current.ToString().Substring(length - 3, 3).Equals("ata") || this.current.ToString().Substring(length - 3, 3).Equals("aty") || this.current.ToString().Substring(length - 3, 3).Equals("ých") || this.current.ToString().Substring(length - 3, 3).Equals("ama") || this.current.ToString().Substring(length - 3, 3).Equals("ami") || this.current.ToString().Substring(length - 3, 3).Equals("ové") || this.current.ToString().Substring(length - 3, 3).Equals("ovi") || this.current.ToString().Substring(length - 3, 3).Equals("ými"))
        {
          this.current = this.current.Remove(length - 3, 3);
          return;
        }
      }
      if (length > 4)
      {
        if (this.current.ToString().Substring(length - 2, 2).Equals("em"))
        {
          this.current = this.current.Remove(length - 1, 1);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("es") || this.current.ToString().Substring(length - 2, 2).Equals("ém") || this.current.ToString().Substring(length - 2, 2).Equals("ím"))
        {
          this.current = this.current.Remove(length - 2, 2);
          this.palatalise();
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("ům"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
        if (this.current.ToString().Substring(length - 2, 2).Equals("at") || this.current.ToString().Substring(length - 2, 2).Equals("ám") || this.current.ToString().Substring(length - 2, 2).Equals("os") || this.current.ToString().Substring(length - 2, 2).Equals("us") || this.current.ToString().Substring(length - 2, 2).Equals("ým") || this.current.ToString().Substring(length - 2, 2).Equals("mi") || this.current.ToString().Substring(length - 2, 2).Equals("ou"))
        {
          this.current = this.current.Remove(length - 2, 2);
          return;
        }
      }
      if (length <= 3)
        return;
      if (this.current.ToString().Substring(length - 1, 1).Equals("e") || this.current.ToString().Substring(length - 1, 1).Equals("i"))
        this.palatalise();
      else if (this.current.ToString().Substring(length - 1, 1).Equals("í") || this.current.ToString().Substring(length - 1, 1).Equals("ě"))
        this.palatalise();
      else if (this.current.ToString().Substring(length - 1, 1).Equals("u") || this.current.ToString().Substring(length - 1, 1).Equals("y") || this.current.ToString().Substring(length - 1, 1).Equals("ů"))
      {
        this.current = this.current.Remove(length - 1, 1);
      }
      else
      {
        if (!this.current.ToString().Substring(length - 1, 1).Equals("a") && !this.current.ToString().Substring(length - 1, 1).Equals("o") && !this.current.ToString().Substring(length - 1, 1).Equals("á") && !this.current.ToString().Substring(length - 1, 1).Equals("é") && !this.current.ToString().Substring(length - 1, 1).Equals("ý"))
          return;
        this.current = this.current.Remove(length - 1, 1);
      }
    }
  }
}

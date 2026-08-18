// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.EnglishStemmer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server.GlobalIndex;


namespace Intermech.Kernel.GlobalIndex;

public class EnglishStemmer : StemmerOperations, IStemmer
{
  private static readonly Among[] a_0 = new Among[3]
  {
    new Among("arsen", -1, -1, (Among.boolDel) null),
    new Among("commun", -1, -1, (Among.boolDel) null),
    new Among("gener", -1, -1, (Among.boolDel) null)
  };
  private static readonly Among[] a_1 = new Among[3]
  {
    new Among("'", -1, 1, (Among.boolDel) null),
    new Among("'s'", 0, 1, (Among.boolDel) null),
    new Among("'s", -1, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_2 = new Among[6]
  {
    new Among("ied", -1, 2, (Among.boolDel) null),
    new Among("s", -1, 3, (Among.boolDel) null),
    new Among("ies", 1, 2, (Among.boolDel) null),
    new Among("sses", 1, 1, (Among.boolDel) null),
    new Among("ss", 1, -1, (Among.boolDel) null),
    new Among("us", 1, -1, (Among.boolDel) null)
  };
  private static readonly Among[] a_3 = new Among[13]
  {
    new Among("", -1, 3, (Among.boolDel) null),
    new Among("bb", 0, 2, (Among.boolDel) null),
    new Among("dd", 0, 2, (Among.boolDel) null),
    new Among("ff", 0, 2, (Among.boolDel) null),
    new Among("gg", 0, 2, (Among.boolDel) null),
    new Among("bl", 0, 1, (Among.boolDel) null),
    new Among("mm", 0, 2, (Among.boolDel) null),
    new Among("nn", 0, 2, (Among.boolDel) null),
    new Among("pp", 0, 2, (Among.boolDel) null),
    new Among("rr", 0, 2, (Among.boolDel) null),
    new Among("at", 0, 1, (Among.boolDel) null),
    new Among("tt", 0, 2, (Among.boolDel) null),
    new Among("iz", 0, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_4 = new Among[6]
  {
    new Among("ed", -1, 2, (Among.boolDel) null),
    new Among("eed", 0, 1, (Among.boolDel) null),
    new Among("ing", -1, 2, (Among.boolDel) null),
    new Among("edly", -1, 2, (Among.boolDel) null),
    new Among("eedly", 3, 1, (Among.boolDel) null),
    new Among("ingly", -1, 2, (Among.boolDel) null)
  };
  private static readonly Among[] a_5 = new Among[24]
  {
    new Among("anci", -1, 3, (Among.boolDel) null),
    new Among("enci", -1, 2, (Among.boolDel) null),
    new Among("ogi", -1, 13, (Among.boolDel) null),
    new Among("li", -1, 16 /*0x10*/, (Among.boolDel) null),
    new Among("bli", 3, 12, (Among.boolDel) null),
    new Among("abli", 4, 4, (Among.boolDel) null),
    new Among("alli", 3, 8, (Among.boolDel) null),
    new Among("fulli", 3, 14, (Among.boolDel) null),
    new Among("lessli", 3, 15, (Among.boolDel) null),
    new Among("ousli", 3, 10, (Among.boolDel) null),
    new Among("entli", 3, 5, (Among.boolDel) null),
    new Among("aliti", -1, 8, (Among.boolDel) null),
    new Among("biliti", -1, 12, (Among.boolDel) null),
    new Among("iviti", -1, 11, (Among.boolDel) null),
    new Among("tional", -1, 1, (Among.boolDel) null),
    new Among("ational", 14, 7, (Among.boolDel) null),
    new Among("alism", -1, 8, (Among.boolDel) null),
    new Among("ation", -1, 7, (Among.boolDel) null),
    new Among("ization", 17, 6, (Among.boolDel) null),
    new Among("izer", -1, 6, (Among.boolDel) null),
    new Among("ator", -1, 7, (Among.boolDel) null),
    new Among("iveness", -1, 11, (Among.boolDel) null),
    new Among("fulness", -1, 9, (Among.boolDel) null),
    new Among("ousness", -1, 10, (Among.boolDel) null)
  };
  private static readonly Among[] a_6 = new Among[9]
  {
    new Among("icate", -1, 4, (Among.boolDel) null),
    new Among("ative", -1, 6, (Among.boolDel) null),
    new Among("alize", -1, 3, (Among.boolDel) null),
    new Among("iciti", -1, 4, (Among.boolDel) null),
    new Among("ical", -1, 4, (Among.boolDel) null),
    new Among("tional", -1, 1, (Among.boolDel) null),
    new Among("ational", 5, 2, (Among.boolDel) null),
    new Among("ful", -1, 5, (Among.boolDel) null),
    new Among("ness", -1, 5, (Among.boolDel) null)
  };
  private static readonly Among[] a_7 = new Among[18]
  {
    new Among("ic", -1, 1, (Among.boolDel) null),
    new Among("ance", -1, 1, (Among.boolDel) null),
    new Among("ence", -1, 1, (Among.boolDel) null),
    new Among("able", -1, 1, (Among.boolDel) null),
    new Among("ible", -1, 1, (Among.boolDel) null),
    new Among("ate", -1, 1, (Among.boolDel) null),
    new Among("ive", -1, 1, (Among.boolDel) null),
    new Among("ize", -1, 1, (Among.boolDel) null),
    new Among("iti", -1, 1, (Among.boolDel) null),
    new Among("al", -1, 1, (Among.boolDel) null),
    new Among("ism", -1, 1, (Among.boolDel) null),
    new Among("ion", -1, 2, (Among.boolDel) null),
    new Among("er", -1, 1, (Among.boolDel) null),
    new Among("ous", -1, 1, (Among.boolDel) null),
    new Among("ant", -1, 1, (Among.boolDel) null),
    new Among("ent", -1, 1, (Among.boolDel) null),
    new Among("ment", 15, 1, (Among.boolDel) null),
    new Among("ement", 16 /*0x10*/, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_8 = new Among[2]
  {
    new Among("e", -1, 1, (Among.boolDel) null),
    new Among("l", -1, 2, (Among.boolDel) null)
  };
  private static readonly Among[] a_9 = new Among[8]
  {
    new Among("succeed", -1, -1, (Among.boolDel) null),
    new Among("proceed", -1, -1, (Among.boolDel) null),
    new Among("exceed", -1, -1, (Among.boolDel) null),
    new Among("canning", -1, -1, (Among.boolDel) null),
    new Among("inning", -1, -1, (Among.boolDel) null),
    new Among("earring", -1, -1, (Among.boolDel) null),
    new Among("herring", -1, -1, (Among.boolDel) null),
    new Among("outing", -1, -1, (Among.boolDel) null)
  };
  private static readonly Among[] a_10 = new Among[18]
  {
    new Among("andes", -1, -1, (Among.boolDel) null),
    new Among("atlas", -1, -1, (Among.boolDel) null),
    new Among("bias", -1, -1, (Among.boolDel) null),
    new Among("cosmos", -1, -1, (Among.boolDel) null),
    new Among("dying", -1, 3, (Among.boolDel) null),
    new Among("early", -1, 9, (Among.boolDel) null),
    new Among("gently", -1, 7, (Among.boolDel) null),
    new Among("howe", -1, -1, (Among.boolDel) null),
    new Among("idly", -1, 6, (Among.boolDel) null),
    new Among("lying", -1, 4, (Among.boolDel) null),
    new Among("news", -1, -1, (Among.boolDel) null),
    new Among("only", -1, 10, (Among.boolDel) null),
    new Among("singly", -1, 11, (Among.boolDel) null),
    new Among("skies", -1, 2, (Among.boolDel) null),
    new Among("skis", -1, 1, (Among.boolDel) null),
    new Among("sky", -1, -1, (Among.boolDel) null),
    new Among("tying", -1, 5, (Among.boolDel) null),
    new Among("ugly", -1, 8, (Among.boolDel) null)
  };
  private static readonly char[] g_v = new char[4]
  {
    '\u0011',
    'A',
    '\u0010',
    '\u0001'
  };
  private static readonly char[] g_v_WXY = new char[5]
  {
    '\u0001',
    '\u0011',
    'A',
    'Ð',
    '\u0001'
  };
  private static readonly char[] g_valid_LI = new char[3]
  {
    '7',
    '\u008D',
    '\u0002'
  };
  private bool B_Y_found;
  private int I_p2;
  private int I_p1;

  private bool r_prelude()
  {
    bool flag1 = false;
    this.B_Y_found = false;
    int cursor1 = this.cursor;
    this.bra = this.cursor;
    if (this.eq_s(1, "'"))
    {
      this.ket = this.cursor;
      this.slice_del();
    }
    this.cursor = cursor1;
    int cursor2 = this.cursor;
    this.bra = this.cursor;
    if (this.eq_s(1, "y"))
    {
      this.ket = this.cursor;
      this.slice_from("Y");
      this.B_Y_found = true;
    }
    this.cursor = cursor2;
    int cursor3 = this.cursor;
    int cursor4;
    bool flag2;
    do
    {
      cursor4 = this.cursor;
      while (true)
      {
        int cursor5 = this.cursor;
        if (this.in_grouping(EnglishStemmer.g_v, 97, 121))
        {
          this.bra = this.cursor;
          if (this.eq_s(1, "y"))
          {
            this.ket = this.cursor;
            this.cursor = cursor5;
            flag1 = true;
            int num = flag1 ? 1 : 0;
          }
        }
        if (!flag1)
        {
          this.cursor = cursor5;
          if (this.cursor < this.limit)
            ++this.cursor;
          else
            goto label_12;
        }
        else
          break;
      }
      flag1 = false;
      goto label_14;
label_12:
      flag1 = true;
label_14:
      flag2 = true;
      if (flag1)
        break;
      this.slice_from("Y");
      this.B_Y_found = true;
    }
    while (flag2);
    this.cursor = cursor4;
    this.cursor = cursor3;
    return true;
  }

  private bool r_mark_regions()
  {
    bool flag1 = false;
    this.I_p1 = this.limit;
    this.I_p2 = this.limit;
    int cursor1 = this.cursor;
    int cursor2 = this.cursor;
    if (this.find_among(EnglishStemmer.a_0, 3) != 0)
    {
      flag1 = true;
      int num = flag1 ? 1 : 0;
    }
    bool flag2;
    if (flag1)
    {
      flag2 = false;
    }
    else
    {
      this.cursor = cursor2;
      while (true)
      {
        if (this.in_grouping(EnglishStemmer.g_v, 97, 121))
        {
          flag1 = true;
          int num = flag1 ? 1 : 0;
        }
        if (!flag1)
        {
          if (this.cursor < this.limit)
            ++this.cursor;
          else
            goto label_30;
        }
        else
          break;
      }
      bool flag3 = false;
      while (true)
      {
        if (this.out_grouping(EnglishStemmer.g_v, 97, 121))
        {
          flag3 = true;
          int num = flag3 ? 1 : 0;
        }
        if (!flag3)
        {
          if (this.cursor < this.limit)
            ++this.cursor;
          else
            goto label_30;
        }
        else
          break;
      }
      flag2 = false;
    }
    this.I_p1 = this.cursor;
    while (true)
    {
      if (this.in_grouping(EnglishStemmer.g_v, 97, 121))
      {
        flag2 = true;
        int num = flag2 ? 1 : 0;
      }
      if (!flag2)
      {
        if (this.cursor < this.limit)
          ++this.cursor;
        else
          goto label_30;
      }
      else
        break;
    }
    bool flag4 = false;
    while (true)
    {
      if (this.out_grouping(EnglishStemmer.g_v, 97, 121))
      {
        flag4 = true;
        int num = flag4 ? 1 : 0;
      }
      if (!flag4)
      {
        if (this.cursor < this.limit)
          ++this.cursor;
        else
          goto label_30;
      }
      else
        break;
    }
    this.I_p2 = this.cursor;
label_30:
    this.cursor = cursor1;
    return true;
  }

  private bool r_shortv()
  {
    bool flag = false;
    int num1 = this.limit - this.cursor;
    if (this.out_grouping_b(EnglishStemmer.g_v_WXY, 89, 121) && this.in_grouping_b(EnglishStemmer.g_v, 97, 121) && this.out_grouping_b(EnglishStemmer.g_v, 97, 121))
    {
      flag = true;
      int num2 = flag ? 1 : 0;
    }
    if (!flag)
    {
      this.cursor = this.limit - num1;
      if (!this.out_grouping_b(EnglishStemmer.g_v, 97, 121) || !this.in_grouping_b(EnglishStemmer.g_v, 97, 121) || this.cursor > this.limit_backward)
        return false;
    }
    return true;
  }

  private bool r_R1() => this.I_p1 <= this.cursor;

  private bool r_R2() => this.I_p2 <= this.cursor;

  private bool r_Step_1a()
  {
    bool flag1 = false;
    int num1 = this.limit - this.cursor;
    this.ket = this.cursor;
    int amongB1 = this.find_among_b(EnglishStemmer.a_1, 3);
    if (amongB1 == 0)
    {
      this.cursor = this.limit - num1;
    }
    else
    {
      this.bra = this.cursor;
      if (amongB1 != 0)
      {
        if (amongB1 == 1)
          this.slice_del();
      }
      else
      {
        this.cursor = this.limit - num1;
        flag1 = true;
      }
      if (flag1)
        flag1 = false;
    }
    this.ket = this.cursor;
    int amongB2 = this.find_among_b(EnglishStemmer.a_2, 6);
    if (amongB2 == 0)
      return false;
    this.bra = this.cursor;
    bool flag2;
    switch (amongB2)
    {
      case 0:
        return false;
      case 1:
        this.slice_from("ss");
        break;
      case 2:
        int num2 = this.limit - this.cursor;
        int num3 = this.cursor - 2;
        if (this.limit_backward <= num3 && num3 <= this.limit)
        {
          this.cursor = num3;
          this.slice_from("i");
          flag1 = true;
          int num4 = flag1 ? 1 : 0;
        }
        if (flag1)
        {
          flag2 = false;
          break;
        }
        this.cursor = this.limit - num2;
        this.slice_from("ie");
        break;
      case 3:
        if (this.cursor <= this.limit_backward)
          return false;
        --this.cursor;
        while (true)
        {
          if (this.in_grouping_b(EnglishStemmer.g_v, 97, 121))
          {
            flag1 = true;
            int num5 = flag1 ? 1 : 0;
          }
          if (!flag1)
          {
            if (this.cursor > this.limit_backward)
              --this.cursor;
            else
              goto label_26;
          }
          else
            break;
        }
        flag2 = false;
        this.slice_del();
        break;
label_26:
        return false;
    }
    return true;
  }

  private bool r_Step_1b()
  {
    bool flag = false;
    this.ket = this.cursor;
    int amongB1 = this.find_among_b(EnglishStemmer.a_4, 6);
    if (amongB1 == 0)
      return false;
    this.bra = this.cursor;
    switch (amongB1)
    {
      case 0:
        return false;
      case 1:
        if (!this.r_R1())
          return false;
        this.slice_from("ee");
        break;
      case 2:
        int num1 = this.limit - this.cursor;
        while (true)
        {
          if (this.in_grouping_b(EnglishStemmer.g_v, 97, 121))
          {
            flag = true;
            int num2 = flag ? 1 : 0;
          }
          if (!flag)
          {
            if (this.cursor > this.limit_backward)
              --this.cursor;
            else
              goto label_13;
          }
          else
            break;
        }
        this.cursor = this.limit - num1;
        this.slice_del();
        int num3 = this.limit - this.cursor;
        int amongB2 = this.find_among_b(EnglishStemmer.a_3, 13);
        if (amongB2 == 0)
          return false;
        this.cursor = this.limit - num3;
        switch (amongB2)
        {
          case 0:
            return false;
          case 1:
            int cursor1 = this.cursor;
            this.insert(this.cursor, this.cursor, "e");
            this.cursor = cursor1;
            goto label_27;
          case 2:
            this.ket = this.cursor;
            if (this.cursor <= this.limit_backward)
              return false;
            --this.cursor;
            this.bra = this.cursor;
            this.slice_del();
            goto label_27;
          case 3:
            if (this.cursor != this.I_p1)
              return false;
            int num4 = this.limit - this.cursor;
            if (!this.r_shortv())
              return false;
            this.cursor = this.limit - num4;
            int cursor2 = this.cursor;
            this.insert(this.cursor, this.cursor, "e");
            this.cursor = cursor2;
            goto label_27;
          default:
            goto label_27;
        }
label_13:
        return false;
    }
label_27:
    return true;
  }

  private bool r_Step_1c()
  {
    bool flag1 = false;
    this.ket = this.cursor;
    int num1 = this.limit - this.cursor;
    if (this.eq_s_b(1, "y"))
    {
      flag1 = true;
      int num2 = flag1 ? 1 : 0;
    }
    if (!flag1)
    {
      this.cursor = this.limit - num1;
      if (!this.eq_s_b(1, "Y"))
        return false;
    }
    this.bra = this.cursor;
    if (!this.out_grouping_b(EnglishStemmer.g_v, 97, 121))
      return false;
    int num3 = this.limit - this.cursor;
    bool flag2 = true;
    if (this.cursor <= this.limit_backward && flag2)
      return false;
    this.cursor = this.limit - num3;
    this.slice_from("i");
    return true;
  }

  private bool r_Step_2()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(EnglishStemmer.a_5, 24);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    if (!this.r_R1())
      return false;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        this.slice_from("tion");
        break;
      case 2:
        this.slice_from("ence");
        break;
      case 3:
        this.slice_from("ance");
        break;
      case 4:
        this.slice_from("able");
        break;
      case 5:
        this.slice_from("ent");
        break;
      case 6:
        this.slice_from("ize");
        break;
      case 7:
        this.slice_from("ate");
        break;
      case 8:
        this.slice_from("al");
        break;
      case 9:
        this.slice_from("ful");
        break;
      case 10:
        this.slice_from("ous");
        break;
      case 11:
        this.slice_from("ive");
        break;
      case 12:
        this.slice_from("ble");
        break;
      case 13:
        if (!this.eq_s_b(1, "l"))
          return false;
        this.slice_from("og");
        break;
      case 14:
        this.slice_from("ful");
        break;
      case 15:
        this.slice_from("less");
        break;
      case 16 /*0x10*/:
        if (!this.in_grouping_b(EnglishStemmer.g_valid_LI, 99, 116))
          return false;
        this.slice_del();
        break;
    }
    return true;
  }

  private bool r_Step_3()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(EnglishStemmer.a_6, 9);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    if (!this.r_R1())
      return false;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        this.slice_from("tion");
        break;
      case 2:
        this.slice_from("ate");
        break;
      case 3:
        this.slice_from("al");
        break;
      case 4:
        this.slice_from("ic");
        break;
      case 5:
        this.slice_del();
        break;
      case 6:
        if (!this.r_R2())
          return false;
        this.slice_del();
        break;
    }
    return true;
  }

  private bool r_Step_4()
  {
    bool flag = false;
    this.ket = this.cursor;
    int amongB = this.find_among_b(EnglishStemmer.a_7, 18);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    if (!this.r_R2())
      return false;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        this.slice_del();
        break;
      case 2:
        int num1 = this.limit - this.cursor;
        if (this.eq_s_b(1, "s"))
        {
          flag = true;
          int num2 = flag ? 1 : 0;
        }
        if (!flag)
        {
          this.cursor = this.limit - num1;
          if (!this.eq_s_b(1, "t"))
            return false;
        }
        this.slice_del();
        break;
    }
    return true;
  }

  private bool r_Step_5()
  {
    bool flag1 = false;
    this.ket = this.cursor;
    int amongB = this.find_among_b(EnglishStemmer.a_8, 2);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        int num1 = this.limit - this.cursor;
        if (this.r_R2())
        {
          flag1 = true;
          int num2 = flag1 ? 1 : 0;
        }
        if (!flag1)
        {
          this.cursor = this.limit - num1;
          if (!this.r_R1())
            return false;
          int num3 = this.limit - this.cursor;
          bool flag2 = true;
          if (this.r_shortv() && flag2)
            return false;
          this.cursor = this.limit - num3;
        }
        this.slice_del();
        break;
      case 2:
        if (!this.r_R2() || !this.eq_s_b(1, "l"))
          return false;
        this.slice_del();
        break;
    }
    return true;
  }

  private bool r_exception2()
  {
    this.ket = this.cursor;
    if (this.find_among_b(EnglishStemmer.a_9, 8) == 0)
      return false;
    this.bra = this.cursor;
    return this.cursor <= this.limit_backward;
  }

  private bool r_exception1()
  {
    this.bra = this.cursor;
    int among = this.find_among(EnglishStemmer.a_10, 18);
    if (among == 0)
      return false;
    this.ket = this.cursor;
    if (this.cursor < this.limit)
      return false;
    switch (among)
    {
      case 0:
        return false;
      case 1:
        this.slice_from("ski");
        break;
      case 2:
        this.slice_from("sky");
        break;
      case 3:
        this.slice_from("die");
        break;
      case 4:
        this.slice_from("lie");
        break;
      case 5:
        this.slice_from("tie");
        break;
      case 6:
        this.slice_from("idl");
        break;
      case 7:
        this.slice_from("gentl");
        break;
      case 8:
        this.slice_from("ugli");
        break;
      case 9:
        this.slice_from("earli");
        break;
      case 10:
        this.slice_from("onli");
        break;
      case 11:
        this.slice_from("singl");
        break;
    }
    return true;
  }

  private bool r_postlude()
  {
    bool flag1 = false;
    if (!this.B_Y_found)
      return false;
    int cursor1;
    bool flag2;
    do
    {
      cursor1 = this.cursor;
      while (true)
      {
        int cursor2 = this.cursor;
        this.bra = this.cursor;
        if (this.eq_s(1, "Y"))
        {
          this.ket = this.cursor;
          this.cursor = cursor2;
          flag1 = true;
          int num = flag1 ? 1 : 0;
        }
        if (!flag1)
        {
          this.cursor = cursor2;
          if (this.cursor < this.limit)
            ++this.cursor;
          else
            goto label_8;
        }
        else
          break;
      }
      flag1 = false;
      goto label_10;
label_8:
      flag1 = true;
label_10:
      flag2 = true;
      if (flag1)
        break;
      this.slice_from("y");
    }
    while (flag2);
    this.cursor = cursor1;
    return true;
  }

  public bool CanStem()
  {
    bool flag1 = false;
    int cursor1 = this.cursor;
    if (this.r_exception1())
    {
      flag1 = true;
      int num = flag1 ? 1 : 0;
    }
    bool flag2;
    if (flag1)
    {
      flag2 = false;
    }
    else
    {
      this.cursor = cursor1;
      int cursor2 = this.cursor;
      int num1 = this.cursor + 3;
      if (0 <= num1 && num1 <= this.limit)
      {
        this.cursor = num1;
        flag1 = true;
        int num2 = flag1 ? 1 : 0;
      }
      if (flag1)
      {
        flag1 = false;
      }
      else
      {
        this.cursor = cursor2;
        if (true)
          goto label_15;
      }
      this.cursor = cursor1;
      int cursor3 = this.cursor;
      this.r_prelude();
      this.cursor = cursor3;
      int cursor4 = this.cursor;
      this.r_mark_regions();
      this.cursor = cursor4;
      this.limit_backward = this.cursor;
      this.cursor = this.limit;
      int num3 = this.limit - this.cursor;
      this.r_Step_1a();
      this.cursor = this.limit - num3;
      int num4 = this.limit - this.cursor;
      if (this.r_exception2())
      {
        flag1 = true;
        int num5 = flag1 ? 1 : 0;
      }
      if (flag1)
      {
        flag2 = false;
      }
      else
      {
        this.cursor = this.limit - num4;
        int num6 = this.limit - this.cursor;
        this.r_Step_1b();
        this.cursor = this.limit - num6;
        int num7 = this.limit - this.cursor;
        this.r_Step_1c();
        this.cursor = this.limit - num7;
        int num8 = this.limit - this.cursor;
        this.r_Step_2();
        this.cursor = this.limit - num8;
        int num9 = this.limit - this.cursor;
        this.r_Step_3();
        this.cursor = this.limit - num9;
        int num10 = this.limit - this.cursor;
        this.r_Step_4();
        this.cursor = this.limit - num10;
        int num11 = this.limit - this.cursor;
        this.r_Step_5();
        this.cursor = this.limit - num11;
      }
      this.cursor = this.limit_backward;
      int cursor5 = this.cursor;
      this.r_postlude();
      this.cursor = cursor5;
    }
label_15:
    return true;
  }

  public string Stem(string s)
  {
    this.setCurrent(s.ToLowerInvariant());
    this.CanStem();
    return this.getCurrent();
  }
}

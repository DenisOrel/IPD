// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.RussianStemmer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server.GlobalIndex;


namespace Intermech.Kernel.GlobalIndex;

public class RussianStemmer : StemmerOperations, IStemmer
{
  private static readonly RussianStemmer methodObject = new RussianStemmer();
  private static readonly Among[] a_0 = new Among[9]
  {
    new Among("в", -1, 1, (Among.boolDel) null),
    new Among("ив", 0, 2, (Among.boolDel) null),
    new Among("ыв", 0, 2, (Among.boolDel) null),
    new Among("вши", -1, 1, (Among.boolDel) null),
    new Among("ивши", 3, 2, (Among.boolDel) null),
    new Among("ывши", 3, 2, (Among.boolDel) null),
    new Among("вшись", -1, 1, (Among.boolDel) null),
    new Among("ившись", 6, 2, (Among.boolDel) null),
    new Among("ывшись", 6, 2, (Among.boolDel) null)
  };
  private static readonly Among[] a_1 = new Among[26]
  {
    new Among("ее", -1, 1, (Among.boolDel) null),
    new Among("ие", -1, 1, (Among.boolDel) null),
    new Among("ое", -1, 1, (Among.boolDel) null),
    new Among("ые", -1, 1, (Among.boolDel) null),
    new Among("ими", -1, 1, (Among.boolDel) null),
    new Among("ыми", -1, 1, (Among.boolDel) null),
    new Among("ей", -1, 1, (Among.boolDel) null),
    new Among("ий", -1, 1, (Among.boolDel) null),
    new Among("ой", -1, 1, (Among.boolDel) null),
    new Among("ый", -1, 1, (Among.boolDel) null),
    new Among("ем", -1, 1, (Among.boolDel) null),
    new Among("им", -1, 1, (Among.boolDel) null),
    new Among("ом", -1, 1, (Among.boolDel) null),
    new Among("ым", -1, 1, (Among.boolDel) null),
    new Among("его", -1, 1, (Among.boolDel) null),
    new Among("ого", -1, 1, (Among.boolDel) null),
    new Among("ему", -1, 1, (Among.boolDel) null),
    new Among("ому", -1, 1, (Among.boolDel) null),
    new Among("их", -1, 1, (Among.boolDel) null),
    new Among("ых", -1, 1, (Among.boolDel) null),
    new Among("ею", -1, 1, (Among.boolDel) null),
    new Among("ою", -1, 1, (Among.boolDel) null),
    new Among("ую", -1, 1, (Among.boolDel) null),
    new Among("юю", -1, 1, (Among.boolDel) null),
    new Among("ая", -1, 1, (Among.boolDel) null),
    new Among("яя", -1, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_2 = new Among[8]
  {
    new Among("ем", -1, 1, (Among.boolDel) null),
    new Among("нн", -1, 1, (Among.boolDel) null),
    new Among("вш", -1, 1, (Among.boolDel) null),
    new Among("ивш", 2, 2, (Among.boolDel) null),
    new Among("ывш", 2, 2, (Among.boolDel) null),
    new Among("щ", -1, 1, (Among.boolDel) null),
    new Among("ющ", 5, 1, (Among.boolDel) null),
    new Among("ующ", 6, 2, (Among.boolDel) null)
  };
  private static readonly Among[] a_3 = new Among[2]
  {
    new Among("сь", -1, 1, (Among.boolDel) null),
    new Among("ся", -1, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_4 = new Among[46]
  {
    new Among("ла", -1, 1, (Among.boolDel) null),
    new Among("ила", 0, 2, (Among.boolDel) null),
    new Among("ыла", 0, 2, (Among.boolDel) null),
    new Among("на", -1, 1, (Among.boolDel) null),
    new Among("ена", 3, 2, (Among.boolDel) null),
    new Among("ете", -1, 1, (Among.boolDel) null),
    new Among("ите", -1, 2, (Among.boolDel) null),
    new Among("йте", -1, 1, (Among.boolDel) null),
    new Among("ейте", 7, 2, (Among.boolDel) null),
    new Among("уйте", 7, 2, (Among.boolDel) null),
    new Among("ли", -1, 1, (Among.boolDel) null),
    new Among("или", 10, 2, (Among.boolDel) null),
    new Among("ыли", 10, 2, (Among.boolDel) null),
    new Among("й", -1, 1, (Among.boolDel) null),
    new Among("ей", 13, 2, (Among.boolDel) null),
    new Among("уй", 13, 2, (Among.boolDel) null),
    new Among("л", -1, 1, (Among.boolDel) null),
    new Among("ил", 16 /*0x10*/, 2, (Among.boolDel) null),
    new Among("ыл", 16 /*0x10*/, 2, (Among.boolDel) null),
    new Among("ем", -1, 1, (Among.boolDel) null),
    new Among("им", -1, 2, (Among.boolDel) null),
    new Among("ым", -1, 2, (Among.boolDel) null),
    new Among("н", -1, 1, (Among.boolDel) null),
    new Among("ен", 22, 2, (Among.boolDel) null),
    new Among("ло", -1, 1, (Among.boolDel) null),
    new Among("ило", 24, 2, (Among.boolDel) null),
    new Among("ыло", 24, 2, (Among.boolDel) null),
    new Among("но", -1, 1, (Among.boolDel) null),
    new Among("ено", 27, 2, (Among.boolDel) null),
    new Among("нно", 27, 1, (Among.boolDel) null),
    new Among("ет", -1, 1, (Among.boolDel) null),
    new Among("ует", 30, 2, (Among.boolDel) null),
    new Among("ит", -1, 2, (Among.boolDel) null),
    new Among("ыт", -1, 2, (Among.boolDel) null),
    new Among("ют", -1, 1, (Among.boolDel) null),
    new Among("уют", 34, 2, (Among.boolDel) null),
    new Among("ят", -1, 2, (Among.boolDel) null),
    new Among("ны", -1, 1, (Among.boolDel) null),
    new Among("ены", 37, 2, (Among.boolDel) null),
    new Among("ть", -1, 1, (Among.boolDel) null),
    new Among("ить", 39, 2, (Among.boolDel) null),
    new Among("ыть", 39, 2, (Among.boolDel) null),
    new Among("ешь", -1, 1, (Among.boolDel) null),
    new Among("ишь", -1, 2, (Among.boolDel) null),
    new Among("ю", -1, 2, (Among.boolDel) null),
    new Among("ую", 44, 2, (Among.boolDel) null)
  };
  private static readonly Among[] a_5 = new Among[36]
  {
    new Among("а", -1, 1, (Among.boolDel) null),
    new Among("ев", -1, 1, (Among.boolDel) null),
    new Among("ов", -1, 1, (Among.boolDel) null),
    new Among("е", -1, 1, (Among.boolDel) null),
    new Among("ие", 3, 1, (Among.boolDel) null),
    new Among("ье", 3, 1, (Among.boolDel) null),
    new Among("и", -1, 1, (Among.boolDel) null),
    new Among("еи", 6, 1, (Among.boolDel) null),
    new Among("ии", 6, 1, (Among.boolDel) null),
    new Among("ами", 6, 1, (Among.boolDel) null),
    new Among("ями", 6, 1, (Among.boolDel) null),
    new Among("иями", 10, 1, (Among.boolDel) null),
    new Among("й", -1, 1, (Among.boolDel) null),
    new Among("ей", 12, 1, (Among.boolDel) null),
    new Among("ией", 13, 1, (Among.boolDel) null),
    new Among("ий", 12, 1, (Among.boolDel) null),
    new Among("ой", 12, 1, (Among.boolDel) null),
    new Among("ам", -1, 1, (Among.boolDel) null),
    new Among("ем", -1, 1, (Among.boolDel) null),
    new Among("ием", 18, 1, (Among.boolDel) null),
    new Among("ом", -1, 1, (Among.boolDel) null),
    new Among("ям", -1, 1, (Among.boolDel) null),
    new Among("иям", 21, 1, (Among.boolDel) null),
    new Among("о", -1, 1, (Among.boolDel) null),
    new Among("у", -1, 1, (Among.boolDel) null),
    new Among("ах", -1, 1, (Among.boolDel) null),
    new Among("ях", -1, 1, (Among.boolDel) null),
    new Among("иях", 26, 1, (Among.boolDel) null),
    new Among("ы", -1, 1, (Among.boolDel) null),
    new Among("ь", -1, 1, (Among.boolDel) null),
    new Among("ю", -1, 1, (Among.boolDel) null),
    new Among("ию", 30, 1, (Among.boolDel) null),
    new Among("ью", 30, 1, (Among.boolDel) null),
    new Among("я", -1, 1, (Among.boolDel) null),
    new Among("ия", 33, 1, (Among.boolDel) null),
    new Among("ья", 33, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_6 = new Among[2]
  {
    new Among("ост", -1, 1, (Among.boolDel) null),
    new Among("ость", -1, 1, (Among.boolDel) null)
  };
  private static readonly Among[] a_7 = new Among[4]
  {
    new Among("ейше", -1, 1, (Among.boolDel) null),
    new Among("н", -1, 2, (Among.boolDel) null),
    new Among("ейш", -1, 1, (Among.boolDel) null),
    new Among("ь", -1, 3, (Among.boolDel) null)
  };
  private static readonly char[] g_v = new char[4]
  {
    '!',
    'A',
    '\b',
    'è'
  };
  private int I_p2;
  private int I_pV;

  private bool r_mark_regions()
  {
    bool flag1 = false;
    bool flag2 = false;
    this.I_pV = this.limit;
    this.I_p2 = this.limit;
    int cursor = this.cursor;
    while (true)
    {
      if (this.in_grouping(RussianStemmer.g_v, 1072, 1103))
      {
        flag2 = true;
        int num = flag2 ? 1 : 0;
      }
      if (!flag2)
      {
        if (this.cursor < this.limit)
          ++this.cursor;
        else
          break;
      }
      else
        goto label_7;
    }
    flag1 = true;
label_7:
    if (!flag1)
    {
      bool flag3 = false;
      bool flag4 = false;
      this.I_pV = this.cursor;
      while (true)
      {
        if (this.out_grouping(RussianStemmer.g_v, 1072, 1103))
        {
          flag4 = true;
          int num = flag4 ? 1 : 0;
        }
        if (!flag4)
        {
          if (this.cursor < this.limit)
            ++this.cursor;
          else
            break;
        }
        else
          goto label_15;
      }
      flag3 = true;
label_15:
      if (!flag3)
      {
        bool flag5 = false;
        bool flag6 = false;
        while (true)
        {
          if (this.in_grouping(RussianStemmer.g_v, 1072, 1103))
          {
            flag6 = true;
            int num = flag6 ? 1 : 0;
          }
          if (!flag6)
          {
            if (this.cursor < this.limit)
              ++this.cursor;
            else
              break;
          }
          else
            goto label_23;
        }
        flag5 = true;
label_23:
        if (!flag5)
        {
          bool flag7 = false;
          bool flag8 = false;
          while (true)
          {
            if (this.out_grouping(RussianStemmer.g_v, 1072, 1103))
            {
              flag8 = true;
              int num = flag8 ? 1 : 0;
            }
            if (!flag8)
            {
              if (this.cursor < this.limit)
                ++this.cursor;
              else
                break;
            }
            else
              goto label_31;
          }
          flag7 = true;
label_31:
          if (!flag7)
            this.I_p2 = this.cursor;
        }
      }
    }
    this.cursor = cursor;
    return true;
  }

  private bool r_R2() => this.I_p2 <= this.cursor;

  private bool r_perfective_gerund()
  {
    bool flag = false;
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_0, 9);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        int num1 = this.limit - this.cursor;
        if (this.eq_s_b(1, "а"))
        {
          flag = true;
          int num2 = flag ? 1 : 0;
        }
        if (!flag)
        {
          this.cursor = this.limit - num1;
          if (!this.eq_s_b(1, "я"))
            return false;
        }
        this.slice_del();
        break;
      case 2:
        this.slice_del();
        break;
    }
    return true;
  }

  private bool r_adjective()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_1, 26);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    if (amongB == 0)
      return false;
    if (amongB == 1)
      this.slice_del();
    return true;
  }

  private bool r_adjectival()
  {
    bool flag1 = false;
    bool flag2 = false;
    if (!this.r_adjective())
      return false;
    int num1 = this.limit - this.cursor;
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_2, 8);
    if (amongB == 0)
    {
      this.cursor = this.limit - num1;
    }
    else
    {
      this.bra = this.cursor;
      switch (amongB)
      {
        case 0:
          this.cursor = this.limit - num1;
          goto default;
        case 1:
          int num2 = this.limit - this.cursor;
          if (this.eq_s_b(1, "а"))
          {
            flag2 = true;
            int num3 = flag2 ? 1 : 0;
          }
          if (!flag2)
          {
            this.cursor = this.limit - num2;
            if (!this.eq_s_b(1, "я"))
            {
              this.cursor = this.limit - num1;
              goto default;
            }
          }
          this.slice_del();
          break;
        case 2:
          this.slice_del();
          break;
        default:
          flag1 = true;
          break;
      }
      int num4 = flag1 ? 1 : 0;
    }
    return true;
  }

  private bool r_reflexive()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_3, 2);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    if (amongB == 0)
      return false;
    if (amongB == 1)
      this.slice_del();
    return true;
  }

  private bool r_verb()
  {
    bool flag = false;
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_4, 46);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        int num1 = this.limit - this.cursor;
        if (this.eq_s_b(1, "а"))
        {
          flag = true;
          int num2 = flag ? 1 : 0;
        }
        if (!flag)
        {
          this.cursor = this.limit - num1;
          if (!this.eq_s_b(1, "я"))
            return false;
        }
        this.slice_del();
        break;
      case 2:
        this.slice_del();
        break;
    }
    return true;
  }

  private bool r_noun()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_5, 36);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    if (amongB == 0)
      return false;
    if (amongB == 1)
      this.slice_del();
    return true;
  }

  private bool r_derivational()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_6, 2);
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
    }
    return true;
  }

  private bool r_tidy_up()
  {
    this.ket = this.cursor;
    int amongB = this.find_among_b(RussianStemmer.a_7, 4);
    if (amongB == 0)
      return false;
    this.bra = this.cursor;
    switch (amongB)
    {
      case 0:
        return false;
      case 1:
        this.slice_del();
        this.ket = this.cursor;
        if (!this.eq_s_b(1, "н"))
          return false;
        this.bra = this.cursor;
        if (!this.eq_s_b(1, "н"))
          return false;
        this.slice_del();
        break;
      case 2:
        if (!this.eq_s_b(1, "н"))
          return false;
        this.slice_del();
        break;
      case 3:
        this.slice_del();
        break;
    }
    return true;
  }

  public bool CanStem()
  {
    bool flag1 = false;
    bool flag2 = false;
    int cursor = this.cursor;
    this.r_mark_regions();
    this.cursor = cursor;
    this.limit_backward = this.cursor;
    this.cursor = this.limit;
    int num1 = this.limit - this.cursor;
    if (this.cursor < this.I_pV)
      return false;
    this.cursor = this.I_pV;
    int limitBackward = this.limit_backward;
    this.limit_backward = this.cursor;
    this.cursor = this.limit - num1;
    int num2 = this.limit - this.cursor;
    int num3 = this.limit - this.cursor;
    if (this.r_perfective_gerund())
    {
      flag2 = true;
      int num4 = flag2 ? 1 : 0;
    }
    if (!flag2)
    {
      this.cursor = this.limit - num3;
      int num5 = this.limit - this.cursor;
      if (!this.r_reflexive())
        this.cursor = this.limit - num5;
      bool flag3 = false;
      int num6 = this.limit - this.cursor;
      if (this.r_adjectival())
      {
        flag3 = true;
        int num7 = flag3 ? 1 : 0;
      }
      if (!flag3)
      {
        this.cursor = this.limit - num6;
        if (this.r_verb())
        {
          flag3 = true;
          int num8 = flag3 ? 1 : 0;
        }
        if (!flag3)
        {
          this.cursor = this.limit - num6;
          if (!this.r_noun())
            flag1 = true;
        }
      }
      int num9 = flag1 ? 1 : 0;
    }
    int num10 = flag1 ? 1 : 0;
    this.cursor = this.limit - num2;
    int num11 = this.limit - this.cursor;
    this.ket = this.cursor;
    if (!this.eq_s_b(1, "и"))
    {
      this.cursor = this.limit - num11;
    }
    else
    {
      this.bra = this.cursor;
      this.slice_del();
    }
    int num12 = this.limit - this.cursor;
    this.r_derivational();
    this.cursor = this.limit - num12;
    int num13 = this.limit - this.cursor;
    this.r_tidy_up();
    this.cursor = this.limit - num13;
    this.limit_backward = limitBackward;
    this.cursor = this.limit_backward;
    return true;
  }

  public string Stem(string s)
  {
    this.setCurrent(s.ToLowerInvariant());
    this.CanStem();
    return this.getCurrent();
  }
}

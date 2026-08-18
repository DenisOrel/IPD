// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertFunc
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Expert;

/// <summary>Functions used in formulae</summary>
public class ExpertFunc
{
  public static readonly FuncData[] _funcs = new FuncData[86]
  {
    new FuncData(FormulaFunc.sin, "sin", "F", 'F', 1),
    new FuncData(FormulaFunc.cos, "cos", "F", 'F', 2),
    new FuncData(FormulaFunc.tg, "tg", "F", 'F', 3),
    new FuncData(FormulaFunc.ln, "ln", "F", 'F', 4),
    new FuncData(FormulaFunc.lg, "lg", "F", 'F', 5),
    new FuncData(FormulaFunc.atg, "atg", "F", 'F', 6),
    new FuncData(FormulaFunc.exp, "exp", "F", 'F', 7),
    new FuncData(FormulaFunc.sqrt, "sqrt", "F", 'F', 8),
    new FuncData(FormulaFunc.abs, "abs", "F", 'F', 9),
    new FuncData(FormulaFunc.STR, LocalizationHolder.rm.GetString("Expert_50"), "A", 'S', 50),
    new FuncData(FormulaFunc.has_child, LocalizationHolder.rm.GetString("Expert_51"), "T", 'B', 51),
    new FuncData(FormulaFunc.has_child_link, LocalizationHolder.rm.GetString("Expert_52"), "TR", 'B', 52),
    new FuncData(FormulaFunc.has_parent, LocalizationHolder.rm.GetString("Expert_53"), "T", 'B', 53),
    new FuncData(FormulaFunc.has_parent_link, LocalizationHolder.rm.GetString("Expert_54"), "TR", 'B', 54),
    new FuncData(FormulaFunc.def, "def", "S", 'B', 101),
    new FuncData(FormulaFunc.nom, "nom", "S", 'S', 102),
    new FuncData(FormulaFunc.kv, "kv", "S", 'S', 103),
    new FuncData(FormulaFunc.hi, "hi", "S", 'S', 104),
    new FuncData(FormulaFunc.lo, "lo", "S", 'S', 105),
    new FuncData(FormulaFunc.kt, "kt", "S", 'S', 106),
    new FuncData(FormulaFunc.st, "st", "S", 'S', 107),
    new FuncData(FormulaFunc.ctn, "ctn", "I", 'S', 108),
    new FuncData(FormulaFunc.rnd, "rnd", "F", 'I', 109),
    new FuncData(FormulaFunc.rnde, "rnde", "FI", 'F', 110),
    new FuncData(FormulaFunc.rndg, "rndg", "FI", 'F', 111),
    new FuncData(FormulaFunc.Int, "int", "F", 'I', 112 /*0x70*/),
    new FuncData(FormulaFunc.frac, "frac", "F", 'F', 113),
    new FuncData(FormulaFunc.has, "has", "SS", 'B', 114),
    new FuncData(FormulaFunc.begs, "begs", "SS", 'B', 115),
    new FuncData(FormulaFunc.ends, "ends", "SS", 'B', 116),
    new FuncData(FormulaFunc.upp, "upp", "S", 'S', 117),
    new FuncData(FormulaFunc.low, "low", "S", 'S', 118),
    new FuncData(FormulaFunc.now, LocalizationHolder.rm.GetString("Expert_55"), "", 'D', 119),
    new FuncData(FormulaFunc.flag, LocalizationHolder.rm.GetString("Expert_56"), "II", 'B', 120),
    new FuncData(FormulaFunc.flag_a, LocalizationHolder.rm.GetString("Expert_57"), "IA", 'B', 121),
    new FuncData(FormulaFunc.rnd_m, "rnd", "M", 'I', 122),
    new FuncData(FormulaFunc.rnde_m, "rnde", "MI", 'M', 123),
    new FuncData(FormulaFunc.rndg_m, "rndg", "MI", 'M', 124),
    new FuncData(FormulaFunc.Int_m, "int", "M", 'M', 125),
    new FuncData(FormulaFunc.frac, "frac", "M", 'F', 126),
    new FuncData(FormulaFunc.date, LocalizationHolder.rm.GetString("Expert_203"), "D", 'S', (int) sbyte.MaxValue),
    new FuncData(FormulaFunc.num, "num", "M", 'F', 128 /*0x80*/),
    new FuncData(FormulaFunc.s_int, LocalizationHolder.rm.GetString("Expert_204"), "S", 'I', 129),
    new FuncData(FormulaFunc.s_float, LocalizationHolder.rm.GetString("Expert_205"), "S", 'F', 130),
    new FuncData(FormulaFunc.s_measured, LocalizationHolder.rm.GetString("Expert_206"), "S", 'M', 131),
    new FuncData(FormulaFunc.isp_num, LocalizationHolder.rm.GetString("Expert_207"), "I", 'I', 132),
    new FuncData(FormulaFunc.len, "len", "S", 'I', 133),
    new FuncData(FormulaFunc.pos, LocalizationHolder.rm.GetString("Expert_208"), "SS", 'I', 134),
    new FuncData(FormulaFunc.substr, LocalizationHolder.rm.GetString("Expert_209"), "SII", 'S', 135),
    new FuncData(FormulaFunc.value, LocalizationHolder.rm.GetString("Expert_210"), "M", 'F', 136),
    new FuncData(FormulaFunc.unit, LocalizationHolder.rm.GetString("Expert_211"), "M", 'S', 137),
    new FuncData(FormulaFunc.val2, "val2", "SS", 'S', 138),
    new FuncData(FormulaFunc.val3, "val3", "SSS", 'S', 139),
    new FuncData(FormulaFunc.no_sht, "nosht", "M", 'S', 140),
    new FuncData(FormulaFunc.child, LocalizationHolder.rm.GetString("Expert_212"), "I", 'B', 141),
    new FuncData(FormulaFunc.parent, LocalizationHolder.rm.GetString("Expert_213"), "I", 'B', 142),
    new FuncData(FormulaFunc.to_MU, LocalizationHolder.rm.GetString("Expert_214"), "MS", 'M', 143),
    new FuncData(FormulaFunc.expanded, LocalizationHolder.rm.GetString("Expert_218"), "I", 'B', 144 /*0x90*/),
    new FuncData(FormulaFunc.unbreak_space, LocalizationHolder.rm.GetString("Expert_226"), "S", 'S', 145),
    new FuncData(FormulaFunc.skipNull, LocalizationHolder.rm.GetString("Expert_219"), "A", 'S', 55),
    new FuncData(FormulaFunc.skipNull_0, LocalizationHolder.rm.GetString("Expert_220"), "A", 'F', 56),
    new FuncData(FormulaFunc.skipNull_1, LocalizationHolder.rm.GetString("Expert_248"), "A", 'F', 57),
    new FuncData(FormulaFunc.obj_child, LocalizationHolder.rm.GetString("Expert_230"), "II", 'B', 146),
    new FuncData(FormulaFunc.obj_parent, LocalizationHolder.rm.GetString("Expert_231"), "II", 'B', 147),
    new FuncData(FormulaFunc.clos_min, LocalizationHolder.rm.GetString("Expert_239"), "FP", 'F', 148),
    new FuncData(FormulaFunc.clos_max, LocalizationHolder.rm.GetString("Expert_240"), "FP", 'F', 149),
    new FuncData(FormulaFunc.clos_min_m, LocalizationHolder.rm.GetString("Expert_239"), "MP", 'M', 150),
    new FuncData(FormulaFunc.clos_max_m, LocalizationHolder.rm.GetString("Expert_240"), "MP", 'M', 151),
    new FuncData(FormulaFunc.str_list, LocalizationHolder.rm.GetString("Expert_241"), "SA", 'S', 152),
    new FuncData(FormulaFunc.ref_list, LocalizationHolder.rm.GetString("Expert_242"), "SAA", 'S', 153),
    new FuncData(FormulaFunc.time_diff, LocalizationHolder.rm.GetString("Expert_251"), "DD", 'I', 154),
    new FuncData(FormulaFunc.str_by_div, LocalizationHolder.rm.GetString("Expert_252"), "PS", 'S', 155),
    new FuncData(FormulaFunc.classify, LocalizationHolder.rm.GetString("Expert_253"), "II", 'B', 156),
    new FuncData(FormulaFunc.ra, "ra", "FP", 'F', 157),
    new FuncData(FormulaFunc.ra2, "ra2", "FPF", 'F', 158),
    new FuncData(FormulaFunc.ra_m, "ra", "MP", 'M', 159),
    new FuncData(FormulaFunc.ra2_m, "ra2", "MPF", 'M', 160 /*0xA0*/),
    new FuncData(FormulaFunc.em_Code, LocalizationHolder.rm.GetString("Expert_258"), "M", 'I', 161),
    new FuncData(FormulaFunc.dt_Name, LocalizationHolder.rm.GetString("Expert_259"), "I", 'S', 162),
    new FuncData(FormulaFunc.minus, LocalizationHolder.rm.GetString("Expert_264"), "F", 'S', 163),
    new FuncData(FormulaFunc.minus_m, LocalizationHolder.rm.GetString("Expert_264"), "M", 'S', 164),
    new FuncData(FormulaFunc.formt, LocalizationHolder.rm.GetString("Expert_266"), "PS", 'S', 165),
    new FuncData(FormulaFunc.MU_code, LocalizationHolder.rm.GetString("Expert_268"), "M", 'I', 166),
    new FuncData(FormulaFunc.MU_coeff, LocalizationHolder.rm.GetString("Expert_269"), "M", 'I', 167),
    new FuncData(FormulaFunc.str_replace, LocalizationHolder.rm.GetString("Expert_270"), "SSS", 'S', 168),
    new FuncData(FormulaFunc.trim, LocalizationHolder.rm.GetString("Expert_280"), "S", 'S', 169)
  };

  public static FuncData funcs(int index)
  {
    FuncData funcData1;
    if (index < 1000)
    {
      funcData1 = ExpertFunc._funcs[index];
    }
    else
    {
      GetUserDataHandler getUserFunc = ExpertFunc.GetUserFunc;
      funcData1 = getUserFunc != null ? getUserFunc(index) : (FuncData) null;
    }
    FuncData funcData2 = new FuncData(funcData1.func, funcData1.text, funcData1.parmTypes, funcData1.result);
    for (int index1 = 0; index1 < funcData2.parmTypes.Length; ++index1)
    {
      if (funcData2.parmTypes[index1] == DataType.ObjType || funcData2.parmTypes[index1] == DataType.RelType)
        funcData2.parmTypes[index1] = DataType.Integer;
    }
    return funcData2;
  }

  public static FuncData real_funcs(int index)
  {
    if (index <= 1000)
      return ExpertFunc._funcs[index];
    GetUserDataHandler getUserFunc = ExpertFunc.GetUserFunc;
    return getUserFunc == null ? (FuncData) null : getUserFunc(index);
  }

  public static int GetFuncIndex(FormulaFunc func)
  {
    for (int funcIndex = 0; funcIndex < ExpertFunc._funcs.Length; ++funcIndex)
    {
      if (ExpertFunc._funcs[funcIndex].func == func)
        return funcIndex;
    }
    return -1;
  }

  public static event GetUserDataHandler GetUserFunc;
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.FormulaFunc
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_56")]
[Category("Expert System")]
public enum FormulaFunc
{
  [Description("sin(F)")] sin = 1,
  [Description("cos(F)")] cos = 2,
  [Description("tg(F)")] tg = 3,
  [Description("ln(F)")] ln = 4,
  [Description("lg(F)")] lg = 5,
  [Description("atg(F)")] atg = 6,
  [Description("exp(F)")] exp = 7,
  [Description("sqrt(F)")] sqrt = 8,
  [Description("abs(F)")] abs = 9,
  [CustomDescription("Attribute.Expert_57")] STR = 50, // 0x00000032
  [CustomDescription("Attribute.Expert_58")] has_child = 51, // 0x00000033
  [CustomDescription("Attribute.Expert_59")] has_child_link = 52, // 0x00000034
  [CustomDescription("Attribute.Expert_60")] has_parent = 53, // 0x00000035
  [CustomDescription("Attribute.Expert_61")] has_parent_link = 54, // 0x00000036
  [CustomDescription("Attribute.Expert_162")] skipNull = 55, // 0x00000037
  [CustomDescription("Attribute.Expert_163")] skipNull_0 = 56, // 0x00000038
  [CustomDescription("Attribute.Expert_173")] skipNull_1 = 57, // 0x00000039
  [Description("def(S)")] def = 101, // 0x00000065
  [Description("nom(S)")] nom = 102, // 0x00000066
  [Description("kv(S)")] kv = 103, // 0x00000067
  [Description("hi(S)")] hi = 104, // 0x00000068
  [Description("lo(S)")] lo = 105, // 0x00000069
  [Description("kt(S)")] kt = 106, // 0x0000006A
  [Description("st(S)")] st = 107, // 0x0000006B
  [Description("ctn(I)")] ctn = 108, // 0x0000006C
  [Description("rnd(F)")] rnd = 109, // 0x0000006D
  [Description("rnde(F,I)")] rnde = 110, // 0x0000006E
  [Description("rndg(F,I)")] rndg = 111, // 0x0000006F
  [Description("int(F)")] Int = 112, // 0x00000070
  [Description("frac(F)")] frac = 113, // 0x00000071
  [Description("has(S,S)")] has = 114, // 0x00000072
  [Description("begs(S,S)")] begs = 115, // 0x00000073
  [Description("ends(S,S)")] ends = 116, // 0x00000074
  [Description("upp(S)")] upp = 117, // 0x00000075
  [Description("low(S)")] low = 118, // 0x00000076
  [CustomDescription("Attribute.Expert_62")] now = 119, // 0x00000077
  [CustomDescription("Attribute.Expert_63")] flag = 120, // 0x00000078
  [CustomDescription("Attribute.Expert_64")] flag_a = 121, // 0x00000079
  [Description("rnd(M)")] rnd_m = 122, // 0x0000007A
  [Description("rnde(M,I)")] rnde_m = 123, // 0x0000007B
  [Description("rndg(M,I)")] rndg_m = 124, // 0x0000007C
  [Description("int(M)")] Int_m = 125, // 0x0000007D
  [Description("frac(M)")] frac_m = 126, // 0x0000007E
  [CustomDescription("Attribute.Expert_147")] date = 127, // 0x0000007F
  [Description("num(M)")] num = 128, // 0x00000080
  [CustomDescription("Attribute.Expert_149")] s_int = 129, // 0x00000081
  [CustomDescription("Attribute.Expert_150")] s_float = 130, // 0x00000082
  [CustomDescription("Attribute.Expert_151")] s_measured = 131, // 0x00000083
  [CustomDescription("Attribute.Expert_152")] isp_num = 132, // 0x00000084
  [Description("len(S)")] len = 133, // 0x00000085
  [CustomDescription("Attribute.Expert_153")] pos = 134, // 0x00000086
  [CustomDescription("Attribute.Expert_154")] substr = 135, // 0x00000087
  [CustomDescription("Attribute.Expert_155")] value = 136, // 0x00000088
  [CustomDescription("Attribute.Expert_156")] unit = 137, // 0x00000089
  [Description("val2(S,S)")] val2 = 138, // 0x0000008A
  [Description("val3(S,S,S)")] val3 = 139, // 0x0000008B
  [Description("nosht(M)")] no_sht = 140, // 0x0000008C
  [CustomDescription("Attribute.Expert_157")] child = 141, // 0x0000008D
  [CustomDescription("Attribute.Expert_158")] parent = 142, // 0x0000008E
  [CustomDescription("Attribute.Expert_160")] to_MU = 143, // 0x0000008F
  [CustomDescription("Attribute.Expert_161")] expanded = 144, // 0x00000090
  [CustomDescription("Attribute.Expert_164")] unbreak_space = 145, // 0x00000091
  [CustomDescription("Attribute.Expert_167")] obj_child = 146, // 0x00000092
  [CustomDescription("Attribute.Expert_168")] obj_parent = 147, // 0x00000093
  [CustomDescription("Attribute.Expert_169")] clos_min = 148, // 0x00000094
  [CustomDescription("Attribute.Expert_170")] clos_max = 149, // 0x00000095
  [CustomDescription("Attribute.Expert_169")] clos_min_m = 150, // 0x00000096
  [CustomDescription("Attribute.Expert_170")] clos_max_m = 151, // 0x00000097
  [CustomDescription("Attribute.Expert_171")] str_list = 152, // 0x00000098
  [CustomDescription("Attribute.Expert_172")] ref_list = 153, // 0x00000099
  [CustomDescription("Attribute.Expert_174")] time_diff = 154, // 0x0000009A
  [CustomDescription("Attribute.Expert_175")] str_by_div = 155, // 0x0000009B
  [CustomDescription("Attribute.Expert_176")] classify = 156, // 0x0000009C
  [CustomDescription("Attribute.Expert_178")] ra = 157, // 0x0000009D
  [CustomDescription("Attribute.Expert_179")] ra2 = 158, // 0x0000009E
  [CustomDescription("Attribute.Expert_180")] ra_m = 159, // 0x0000009F
  [CustomDescription("Attribute.Expert_181")] ra2_m = 160, // 0x000000A0
  [CustomDescription("Attribute.Expert_182")] em_Code = 161, // 0x000000A1
  [CustomDescription("Attribute.Expert_183")] dt_Name = 162, // 0x000000A2
  [CustomDescription("Attribute.Expert_184")] minus = 163, // 0x000000A3
  [CustomDescription("Attribute.Expert_185")] minus_m = 164, // 0x000000A4
  [CustomDescription("Attribute.Expert_186")] formt = 165, // 0x000000A5
  [CustomDescription("Attribute.Expert_225")] MU_code = 166, // 0x000000A6
  [CustomDescription("Attribute.Expert_226")] MU_coeff = 167, // 0x000000A7
  [CustomDescription("Attribute.Expert_227")] str_replace = 168, // 0x000000A8
  [CustomDescription("Attribute.Expert_230")] trim = 169, // 0x000000A9
}

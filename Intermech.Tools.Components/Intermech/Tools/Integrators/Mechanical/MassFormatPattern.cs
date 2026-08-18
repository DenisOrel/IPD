// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MassFormatPattern
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Кодирует значения атрибута CADMECH MassaFormat. Это определение и комментарии к нему взяты из исходников CADMECH, где оно называется MassaPatternEnum.
/// Уточнить можно у Франца Печкова.
/// </summary>
/// <remarks>
/// Если MassaPatternEnum задан с минусом, значит масса вынесена в таблицу в чертеже, где она форматируется в соответствии со значением MassaPatternEnum,
/// а в штампе чертежа пишется См.табл. но пользователью предлагается выбрать формат отображения массы,
/// за исклбчением случая когда MassaPatternEnum = mpOverride (-1)
/// </remarks>
internal enum MassFormatPattern
{
  mpOverride = -1, // 0xFFFFFFFF
  mpUnknown = 0,
  mpGramme0 = 10, // 0x0000000A
  mpGramme1 = 11, // 0x0000000B
  mpGramme2 = 12, // 0x0000000C
  mpGramme3 = 13, // 0x0000000D
  mpKg0 = 20, // 0x00000014
  mpKg1 = 21, // 0x00000015
  mpKg2 = 22, // 0x00000016
  mpKg3 = 23, // 0x00000017
  mpKg4 = 24, // 0x00000018
  mpKg5 = 25, // 0x00000019
  mpKg_2 = 28, // 0x0000001C
  mpKg_1 = 29, // 0x0000001D
  mpTonne0 = 30, // 0x0000001E
  mpTonne1 = 31, // 0x0000001F
  mpTonne2 = 32, // 0x00000020
  mpTonne3 = 33, // 0x00000021
}

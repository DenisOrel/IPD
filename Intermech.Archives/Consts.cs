// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Consts
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives;

/// <summary>Разное</summary>
public class Consts
{
  /// <summary>
  /// Guid категории архивов (отображение в навигаторе как "Архивы и документы")
  /// </summary>
  public static readonly Guid CategoryArchivesNodeGuid = new Guid("CF666FB8-92A4-4b39-BA59-6EA06B342BF7");
  /// <summary>Числовой идентификатор нода "Архивы и документы"</summary>
  public static int CategoryArchivesNode = 0;
  /// <summary>
  /// Строка для хранения в настройках "Отображения документов во вложенных архивах"
  /// </summary>
  public static readonly string ShowInternalDocumsProperty = nameof (ShowInternalDocums);
  /// <summary>
  /// Переменная для хранения текущего значения "Отображения документов во вложенных архивах"
  /// </summary>
  public static bool ShowInternalDocums = false;
  /// <summary>Строка для хранения в настройках ширины колонок</summary>
  public static readonly string ColumnsWidthProperty = nameof (ColumnsWidth);
  /// <summary>Строка для хранения в настройках видимости колонок</summary>
  public static readonly string ColumnsVisibleProperty = nameof (ColumnsVisible);
  /// <summary>
  /// Строка для хранения в настройках группировки по колонкам
  /// </summary>
  public static readonly string GroupByColumnsProperty = nameof (GroupByColumns);
  /// <summary>
  /// Хранение ширины колонок
  /// [Название поля (FieldName)]=[Ширина колонки]
  /// </summary>
  public static Dictionary<string, int> ColumnsWidth = new Dictionary<string, int>();
  /// <summary>
  /// Хранение видимости колонок
  /// [Название поля (FieldName)]
  /// </summary>
  public static List<string> ColumnsVisible = new List<string>();
  /// <summary>
  /// Хранение колонок для группировки
  /// [Название поля (FieldName)]
  /// </summary>
  public static List<string> GroupByColumns = new List<string>();
  /// <summary>имя для окна Архивы документов</summary>
  public static string ArchivesWindowName = "ArchivesWindow";
  /// <summary>
  /// Константа для использования настроек роли по умолчанию в админских настройках. = -1
  /// </summary>
  public static long DefaultRoleId = -1;
  /// <summary>гуид для окна архивы документов</summary>
  public static Guid ArchivesWindowGuid = new Guid("CF666FB8-92A4-4b39-BA70-7EA17B353BF8");
  public static Guid ColumnsSettingsAttrGuid = new Guid("cadd9bb7-306c-11d8-b4e9-00304f19f545");
  internal static readonly int appId = 336;
  internal static readonly byte[][] Key = new byte[32 /*0x20*/][]
  {
    new byte[16 /*0x10*/]
    {
      (byte) 144 /*0x90*/,
      (byte) 130,
      (byte) 102,
      (byte) 59,
      (byte) 169,
      (byte) 120,
      (byte) 250,
      (byte) 122,
      (byte) 251,
      (byte) 201,
      (byte) 0,
      (byte) 30,
      (byte) 41,
      (byte) 203,
      (byte) 94,
      (byte) 37
    },
    new byte[16 /*0x10*/]
    {
      (byte) 38,
      (byte) 37,
      (byte) 40,
      (byte) 55,
      (byte) 58,
      (byte) 26,
      (byte) 118,
      (byte) 207,
      (byte) 102,
      (byte) 66,
      (byte) 131,
      (byte) 124,
      (byte) 94,
      (byte) 101,
      (byte) 141,
      (byte) 133
    },
    new byte[16 /*0x10*/]
    {
      (byte) 252,
      (byte) 233,
      (byte) 234,
      (byte) 231,
      (byte) 107,
      (byte) 168,
      (byte) 5,
      (byte) 42,
      (byte) 221,
      (byte) 193,
      (byte) 191,
      (byte) 171,
      (byte) 60,
      (byte) 23,
      (byte) 43,
      (byte) 194
    },
    new byte[16 /*0x10*/]
    {
      (byte) 253,
      (byte) 58,
      (byte) 173,
      (byte) 124,
      (byte) 94,
      (byte) 51,
      (byte) 167,
      (byte) 151,
      (byte) 83,
      (byte) 100,
      (byte) 170,
      (byte) 170,
      (byte) 140,
      (byte) 211,
      (byte) 128 /*0x80*/,
      (byte) 134
    },
    new byte[16 /*0x10*/]
    {
      (byte) 92,
      (byte) 201,
      (byte) 77,
      (byte) 34,
      (byte) 166,
      (byte) 140,
      (byte) 248,
      (byte) 191,
      (byte) 251,
      (byte) 224 /*0xE0*/,
      (byte) 88,
      (byte) 21,
      (byte) 107,
      (byte) 193,
      (byte) 38,
      (byte) 37
    },
    new byte[16 /*0x10*/]
    {
      (byte) 4,
      (byte) 2,
      (byte) 61,
      (byte) 226,
      (byte) 202,
      (byte) 213,
      (byte) 239,
      (byte) 156,
      (byte) 16 /*0x10*/,
      (byte) 245,
      (byte) 121,
      (byte) 131,
      (byte) 97,
      (byte) 131,
      (byte) 127 /*0x7F*/,
      (byte) 219
    },
    new byte[16 /*0x10*/]
    {
      (byte) 99,
      (byte) 4,
      (byte) 62,
      (byte) 115,
      (byte) 2,
      (byte) 189,
      (byte) 10,
      (byte) 231,
      (byte) 124,
      (byte) 60,
      (byte) 94,
      (byte) 231,
      (byte) 185,
      (byte) 78,
      (byte) 62,
      (byte) 228
    },
    new byte[16 /*0x10*/]
    {
      (byte) 24,
      (byte) 39,
      (byte) 39,
      (byte) 161,
      (byte) 88,
      (byte) 196,
      (byte) 84,
      (byte) 146,
      (byte) 168,
      (byte) 23,
      (byte) 94,
      (byte) 96 /*0x60*/,
      (byte) 197,
      (byte) 240 /*0xF0*/,
      (byte) 84,
      (byte) 146
    },
    new byte[16 /*0x10*/]
    {
      (byte) 157,
      (byte) 61,
      (byte) 193,
      (byte) 95,
      (byte) 125,
      (byte) 60,
      (byte) 126,
      (byte) 112 /*0x70*/,
      (byte) 241,
      (byte) 42,
      (byte) 235,
      (byte) 139,
      (byte) 80 /*0x50*/,
      (byte) 239,
      (byte) 190,
      (byte) 27
    },
    new byte[16 /*0x10*/]
    {
      (byte) 173,
      (byte) 149,
      (byte) 237,
      (byte) 198,
      (byte) 186,
      (byte) 203,
      (byte) 169,
      (byte) 93,
      (byte) 165,
      (byte) 90,
      (byte) 2,
      (byte) 254,
      (byte) 139,
      (byte) 79,
      (byte) 56,
      (byte) 148
    },
    new byte[16 /*0x10*/]
    {
      (byte) 155,
      (byte) 153,
      (byte) 71,
      (byte) 55,
      (byte) 216,
      (byte) 184,
      (byte) 191,
      (byte) 158,
      (byte) 190,
      (byte) 101,
      (byte) 165,
      (byte) 106,
      (byte) 46,
      (byte) 16 /*0x10*/,
      (byte) 6,
      (byte) 172
    },
    new byte[16 /*0x10*/]
    {
      (byte) 156,
      (byte) 221,
      (byte) 243,
      (byte) 202,
      (byte) 119,
      (byte) 192 /*0xC0*/,
      (byte) 191,
      (byte) 105,
      (byte) 242,
      (byte) 16 /*0x10*/,
      (byte) 132,
      (byte) 75,
      (byte) 159,
      (byte) 71,
      (byte) 39,
      (byte) 198
    },
    new byte[16 /*0x10*/]
    {
      (byte) 162,
      (byte) 115,
      (byte) 120,
      (byte) 45,
      byte.MaxValue,
      (byte) 198,
      (byte) 167,
      (byte) 39,
      (byte) 133,
      (byte) 206,
      (byte) 130,
      (byte) 216,
      (byte) 209,
      (byte) 127 /*0x7F*/,
      (byte) 151,
      (byte) 140
    },
    new byte[16 /*0x10*/]
    {
      (byte) 164,
      (byte) 63 /*0x3F*/,
      (byte) 62,
      (byte) 90,
      (byte) 220,
      (byte) 144 /*0x90*/,
      (byte) 14,
      (byte) 121,
      (byte) 94,
      (byte) 104,
      (byte) 227,
      (byte) 117,
      (byte) 199,
      (byte) 49,
      (byte) 145,
      (byte) 38
    },
    new byte[16 /*0x10*/]
    {
      (byte) 125,
      (byte) 66,
      (byte) 76,
      (byte) 31 /*0x1F*/,
      (byte) 6,
      (byte) 6,
      (byte) 11,
      (byte) 229,
      (byte) 125,
      (byte) 35,
      (byte) 161,
      (byte) 24,
      (byte) 150,
      (byte) 20,
      (byte) 65,
      (byte) 225
    },
    new byte[16 /*0x10*/]
    {
      (byte) 218,
      (byte) 172,
      (byte) 181,
      (byte) 146,
      (byte) 128 /*0x80*/,
      (byte) 18,
      (byte) 139,
      (byte) 172,
      (byte) 197,
      (byte) 15,
      (byte) 244,
      (byte) 219,
      (byte) 139,
      (byte) 227,
      (byte) 43,
      (byte) 13
    },
    new byte[16 /*0x10*/]
    {
      (byte) 35,
      (byte) 63 /*0x3F*/,
      (byte) 233,
      (byte) 220,
      (byte) 119,
      (byte) 145,
      (byte) 28,
      (byte) 213,
      (byte) 242,
      (byte) 32 /*0x20*/,
      (byte) 177,
      (byte) 10,
      (byte) 53,
      (byte) 231,
      (byte) 205,
      (byte) 63 /*0x3F*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 185,
      (byte) 240 /*0xF0*/,
      (byte) 46,
      (byte) 140,
      (byte) 126,
      (byte) 223,
      (byte) 163,
      (byte) 94,
      (byte) 104,
      (byte) 127 /*0x7F*/,
      (byte) 136,
      (byte) 232,
      (byte) 227,
      (byte) 160 /*0xA0*/,
      (byte) 109,
      (byte) 225
    },
    new byte[16 /*0x10*/]
    {
      (byte) 205,
      (byte) 24,
      (byte) 237,
      (byte) 236,
      (byte) 195,
      (byte) 231,
      (byte) 165,
      (byte) 155,
      (byte) 103,
      (byte) 146,
      (byte) 176 /*0xB0*/,
      (byte) 220,
      (byte) 37,
      (byte) 142,
      (byte) 145,
      (byte) 113
    },
    new byte[16 /*0x10*/]
    {
      (byte) 103,
      (byte) 154,
      (byte) 215,
      (byte) 35,
      (byte) 5,
      (byte) 236,
      (byte) 54,
      (byte) 114,
      (byte) 171,
      (byte) 235,
      (byte) 66,
      (byte) 216,
      (byte) 150,
      (byte) 187,
      (byte) 147,
      (byte) 225
    },
    new byte[16 /*0x10*/]
    {
      (byte) 140,
      (byte) 160 /*0xA0*/,
      (byte) 180,
      (byte) 146,
      (byte) 150,
      (byte) 187,
      (byte) 21,
      (byte) 235,
      (byte) 38,
      (byte) 118,
      (byte) 45,
      (byte) 230,
      (byte) 85,
      (byte) 1,
      (byte) 176 /*0xB0*/,
      (byte) 150
    },
    new byte[16 /*0x10*/]
    {
      (byte) 168,
      (byte) 230,
      (byte) 144 /*0x90*/,
      (byte) 111,
      (byte) 87,
      (byte) 116,
      (byte) 49,
      (byte) 136,
      (byte) 243,
      (byte) 93,
      (byte) 92,
      (byte) 186,
      (byte) 131,
      byte.MaxValue,
      (byte) 177,
      (byte) 200
    },
    new byte[16 /*0x10*/]
    {
      (byte) 86,
      (byte) 98,
      (byte) 75,
      (byte) 125,
      (byte) 38,
      (byte) 85,
      (byte) 29,
      (byte) 60,
      (byte) 153,
      (byte) 78,
      (byte) 116,
      (byte) 229,
      (byte) 225,
      (byte) 63 /*0x3F*/,
      (byte) 176 /*0xB0*/,
      (byte) 99
    },
    new byte[16 /*0x10*/]
    {
      (byte) 114,
      (byte) 65,
      (byte) 191,
      (byte) 225,
      (byte) 230,
      (byte) 227,
      (byte) 253,
      (byte) 116,
      (byte) 67,
      (byte) 15,
      (byte) 115,
      (byte) 136,
      (byte) 49,
      (byte) 205,
      (byte) 232,
      (byte) 109
    },
    new byte[16 /*0x10*/]
    {
      (byte) 143,
      (byte) 64 /*0x40*/,
      (byte) 125,
      (byte) 148,
      (byte) 51,
      (byte) 58,
      (byte) 168,
      (byte) 17,
      (byte) 3,
      (byte) 1,
      (byte) 197,
      (byte) 76,
      (byte) 186,
      (byte) 190,
      (byte) 70,
      (byte) 102
    },
    new byte[16 /*0x10*/]
    {
      (byte) 76,
      (byte) 197,
      (byte) 133,
      (byte) 117,
      (byte) 187,
      (byte) 78,
      (byte) 182,
      (byte) 215,
      (byte) 111,
      (byte) 130,
      (byte) 22,
      (byte) 109,
      (byte) 231,
      (byte) 173,
      (byte) 46,
      (byte) 162
    },
    new byte[16 /*0x10*/]
    {
      (byte) 226,
      (byte) 26,
      (byte) 134,
      (byte) 195,
      (byte) 67,
      (byte) 19,
      (byte) 75,
      (byte) 54,
      (byte) 131,
      (byte) 119,
      (byte) 248,
      (byte) 72,
      (byte) 71,
      (byte) 47,
      (byte) 10,
      (byte) 230
    },
    new byte[16 /*0x10*/]
    {
      (byte) 139,
      (byte) 247,
      (byte) 220,
      (byte) 71,
      (byte) 108,
      (byte) 147,
      (byte) 30,
      (byte) 156,
      (byte) 194,
      (byte) 175,
      (byte) 101,
      (byte) 201,
      (byte) 73,
      (byte) 150,
      (byte) 98,
      (byte) 137
    },
    new byte[16 /*0x10*/]
    {
      (byte) 198,
      (byte) 201,
      (byte) 87,
      (byte) 20,
      (byte) 131,
      (byte) 27,
      (byte) 98,
      (byte) 27,
      (byte) 185,
      (byte) 57,
      (byte) 121,
      (byte) 36,
      (byte) 143,
      (byte) 208 /*0xD0*/,
      (byte) 130,
      (byte) 218
    },
    new byte[16 /*0x10*/]
    {
      (byte) 206,
      (byte) 167,
      (byte) 80 /*0x50*/,
      (byte) 192 /*0xC0*/,
      (byte) 225,
      (byte) 188,
      (byte) 69,
      (byte) 38,
      (byte) 158,
      (byte) 154,
      (byte) 140,
      (byte) 152,
      (byte) 167,
      (byte) 4,
      (byte) 225,
      (byte) 48 /*0x30*/
    },
    new byte[16 /*0x10*/]
    {
      (byte) 7,
      (byte) 14,
      (byte) 2,
      (byte) 199,
      (byte) 184,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 123,
      (byte) 245,
      (byte) 70,
      (byte) 17,
      (byte) 187,
      (byte) 65,
      (byte) 96 /*0x60*/,
      (byte) 202,
      (byte) 212
    },
    new byte[16 /*0x10*/]
    {
      (byte) 193,
      (byte) 199,
      (byte) 83,
      (byte) 199,
      (byte) 43,
      (byte) 7,
      (byte) 205,
      (byte) 61,
      (byte) 242,
      (byte) 143,
      (byte) 95,
      (byte) 173,
      (byte) 245,
      (byte) 79,
      (byte) 102,
      (byte) 73
    }
  };
}

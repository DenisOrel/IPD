// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Haval3
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class Haval3 : Haval
{
  public Haval3(HashSizeEnum a_hash_size)
    : base(HashRounds.Rounds3, a_hash_size)
  {
  }

  protected override unsafe void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    uint[] array = new uint[32 /*0x20*/];
    fixed (uint* dest = array)
    {
      Converters.le32_copy(a_data, a_index, (IntPtr) (void*) dest, 0, 128 /*0x80*/);
      uint a_value1 = this.hash[0];
      uint a_value2 = this.hash[1];
      uint a_value3 = this.hash[2];
      uint a_value4 = this.hash[3];
      uint a_value5 = this.hash[4];
      uint a_value6 = this.hash[5];
      uint a_value7 = this.hash[6];
      uint a_value8 = this.hash[7];
      uint a_value9 = (uint) ((int) a_value3 & ((int) a_value5 ^ (int) a_value4) ^ (int) a_value7 & (int) a_value1 ^ (int) a_value6 & (int) a_value2) ^ a_value5;
      uint a_value10 = array[0] + Bits.RotateRight32(a_value9, 7) + Bits.RotateRight32(a_value8, 11);
      uint a_value11 = (uint) ((int) a_value2 & ((int) a_value4 ^ (int) a_value3) ^ (int) a_value6 & (int) a_value10 ^ (int) a_value5 & (int) a_value1) ^ a_value4;
      uint a_value12 = array[1] + Bits.RotateRight32(a_value11, 7) + Bits.RotateRight32(a_value7, 11);
      uint a_value13 = (uint) ((int) a_value1 & ((int) a_value3 ^ (int) a_value2) ^ (int) a_value5 & (int) a_value12 ^ (int) a_value4 & (int) a_value10) ^ a_value3;
      uint a_value14 = array[2] + Bits.RotateRight32(a_value13, 7) + Bits.RotateRight32(a_value6, 11);
      uint a_value15 = (uint) ((int) a_value10 & ((int) a_value2 ^ (int) a_value1) ^ (int) a_value4 & (int) a_value14 ^ (int) a_value3 & (int) a_value12) ^ a_value2;
      uint a_value16 = array[3] + Bits.RotateRight32(a_value15, 7) + Bits.RotateRight32(a_value5, 11);
      uint a_value17 = (uint) ((int) a_value12 & ((int) a_value1 ^ (int) a_value10) ^ (int) a_value3 & (int) a_value16 ^ (int) a_value2 & (int) a_value14) ^ a_value1;
      uint a_value18 = array[4] + Bits.RotateRight32(a_value17, 7) + Bits.RotateRight32(a_value4, 11);
      uint a_value19 = (uint) ((int) a_value14 & ((int) a_value10 ^ (int) a_value12) ^ (int) a_value2 & (int) a_value18 ^ (int) a_value1 & (int) a_value16) ^ a_value10;
      uint a_value20 = array[5] + Bits.RotateRight32(a_value19, 7) + Bits.RotateRight32(a_value3, 11);
      uint a_value21 = (uint) ((int) a_value16 & ((int) a_value12 ^ (int) a_value14) ^ (int) a_value1 & (int) a_value20 ^ (int) a_value10 & (int) a_value18) ^ a_value12;
      uint a_value22 = array[6] + Bits.RotateRight32(a_value21, 7) + Bits.RotateRight32(a_value2, 11);
      uint a_value23 = (uint) ((int) a_value18 & ((int) a_value14 ^ (int) a_value16) ^ (int) a_value10 & (int) a_value22 ^ (int) a_value12 & (int) a_value20) ^ a_value14;
      uint a_value24 = array[7] + Bits.RotateRight32(a_value23, 7) + Bits.RotateRight32(a_value1, 11);
      uint a_value25 = (uint) ((int) a_value20 & ((int) a_value16 ^ (int) a_value18) ^ (int) a_value12 & (int) a_value24 ^ (int) a_value14 & (int) a_value22) ^ a_value16;
      uint a_value26 = array[8] + Bits.RotateRight32(a_value25, 7) + Bits.RotateRight32(a_value10, 11);
      uint a_value27 = (uint) ((int) a_value22 & ((int) a_value18 ^ (int) a_value20) ^ (int) a_value14 & (int) a_value26 ^ (int) a_value16 & (int) a_value24) ^ a_value18;
      uint a_value28 = array[9] + Bits.RotateRight32(a_value27, 7) + Bits.RotateRight32(a_value12, 11);
      uint a_value29 = (uint) ((int) a_value24 & ((int) a_value20 ^ (int) a_value22) ^ (int) a_value16 & (int) a_value28 ^ (int) a_value18 & (int) a_value26) ^ a_value20;
      uint a_value30 = array[10] + Bits.RotateRight32(a_value29, 7) + Bits.RotateRight32(a_value14, 11);
      uint a_value31 = (uint) ((int) a_value26 & ((int) a_value22 ^ (int) a_value24) ^ (int) a_value18 & (int) a_value30 ^ (int) a_value20 & (int) a_value28) ^ a_value22;
      uint a_value32 = array[11] + Bits.RotateRight32(a_value31, 7) + Bits.RotateRight32(a_value16, 11);
      uint a_value33 = (uint) ((int) a_value28 & ((int) a_value24 ^ (int) a_value26) ^ (int) a_value20 & (int) a_value32 ^ (int) a_value22 & (int) a_value30) ^ a_value24;
      uint a_value34 = array[12] + Bits.RotateRight32(a_value33, 7) + Bits.RotateRight32(a_value18, 11);
      uint a_value35 = (uint) ((int) a_value30 & ((int) a_value26 ^ (int) a_value28) ^ (int) a_value22 & (int) a_value34 ^ (int) a_value24 & (int) a_value32) ^ a_value26;
      uint a_value36 = array[13] + Bits.RotateRight32(a_value35, 7) + Bits.RotateRight32(a_value20, 11);
      uint a_value37 = (uint) ((int) a_value32 & ((int) a_value28 ^ (int) a_value30) ^ (int) a_value24 & (int) a_value36 ^ (int) a_value26 & (int) a_value34) ^ a_value28;
      uint a_value38 = array[14] + Bits.RotateRight32(a_value37, 7) + Bits.RotateRight32(a_value22, 11);
      uint a_value39 = (uint) ((int) a_value34 & ((int) a_value30 ^ (int) a_value32) ^ (int) a_value26 & (int) a_value38 ^ (int) a_value28 & (int) a_value36) ^ a_value30;
      uint a_value40 = array[15] + Bits.RotateRight32(a_value39, 7) + Bits.RotateRight32(a_value24, 11);
      uint a_value41 = (uint) ((int) a_value36 & ((int) a_value32 ^ (int) a_value34) ^ (int) a_value28 & (int) a_value40 ^ (int) a_value30 & (int) a_value38) ^ a_value32;
      uint a_value42 = array[16 /*0x10*/] + Bits.RotateRight32(a_value41, 7) + Bits.RotateRight32(a_value26, 11);
      uint a_value43 = (uint) ((int) a_value38 & ((int) a_value34 ^ (int) a_value36) ^ (int) a_value30 & (int) a_value42 ^ (int) a_value32 & (int) a_value40) ^ a_value34;
      uint a_value44 = array[17] + Bits.RotateRight32(a_value43, 7) + Bits.RotateRight32(a_value28, 11);
      uint a_value45 = (uint) ((int) a_value40 & ((int) a_value36 ^ (int) a_value38) ^ (int) a_value32 & (int) a_value44 ^ (int) a_value34 & (int) a_value42) ^ a_value36;
      uint a_value46 = array[18] + Bits.RotateRight32(a_value45, 7) + Bits.RotateRight32(a_value30, 11);
      uint a_value47 = (uint) ((int) a_value42 & ((int) a_value38 ^ (int) a_value40) ^ (int) a_value34 & (int) a_value46 ^ (int) a_value36 & (int) a_value44) ^ a_value38;
      uint a_value48 = array[19] + Bits.RotateRight32(a_value47, 7) + Bits.RotateRight32(a_value32, 11);
      uint a_value49 = (uint) ((int) a_value44 & ((int) a_value40 ^ (int) a_value42) ^ (int) a_value36 & (int) a_value48 ^ (int) a_value38 & (int) a_value46) ^ a_value40;
      uint a_value50 = array[20] + Bits.RotateRight32(a_value49, 7) + Bits.RotateRight32(a_value34, 11);
      uint a_value51 = (uint) ((int) a_value46 & ((int) a_value42 ^ (int) a_value44) ^ (int) a_value38 & (int) a_value50 ^ (int) a_value40 & (int) a_value48) ^ a_value42;
      uint a_value52 = array[21] + Bits.RotateRight32(a_value51, 7) + Bits.RotateRight32(a_value36, 11);
      uint a_value53 = (uint) ((int) a_value48 & ((int) a_value44 ^ (int) a_value46) ^ (int) a_value40 & (int) a_value52 ^ (int) a_value42 & (int) a_value50) ^ a_value44;
      uint a_value54 = array[22] + Bits.RotateRight32(a_value53, 7) + Bits.RotateRight32(a_value38, 11);
      uint a_value55 = (uint) ((int) a_value50 & ((int) a_value46 ^ (int) a_value48) ^ (int) a_value42 & (int) a_value54 ^ (int) a_value44 & (int) a_value52) ^ a_value46;
      uint a_value56 = array[23] + Bits.RotateRight32(a_value55, 7) + Bits.RotateRight32(a_value40, 11);
      uint a_value57 = (uint) ((int) a_value52 & ((int) a_value48 ^ (int) a_value50) ^ (int) a_value44 & (int) a_value56 ^ (int) a_value46 & (int) a_value54) ^ a_value48;
      uint a_value58 = array[24] + Bits.RotateRight32(a_value57, 7) + Bits.RotateRight32(a_value42, 11);
      uint a_value59 = (uint) ((int) a_value54 & ((int) a_value50 ^ (int) a_value52) ^ (int) a_value46 & (int) a_value58 ^ (int) a_value48 & (int) a_value56) ^ a_value50;
      uint a_value60 = array[25] + Bits.RotateRight32(a_value59, 7) + Bits.RotateRight32(a_value44, 11);
      uint a_value61 = (uint) ((int) a_value56 & ((int) a_value52 ^ (int) a_value54) ^ (int) a_value48 & (int) a_value60 ^ (int) a_value50 & (int) a_value58) ^ a_value52;
      uint a_value62 = array[26] + Bits.RotateRight32(a_value61, 7) + Bits.RotateRight32(a_value46, 11);
      uint a_value63 = (uint) ((int) a_value58 & ((int) a_value54 ^ (int) a_value56) ^ (int) a_value50 & (int) a_value62 ^ (int) a_value52 & (int) a_value60) ^ a_value54;
      uint a_value64 = array[27] + Bits.RotateRight32(a_value63, 7) + Bits.RotateRight32(a_value48, 11);
      uint a_value65 = (uint) ((int) a_value60 & ((int) a_value56 ^ (int) a_value58) ^ (int) a_value52 & (int) a_value64 ^ (int) a_value54 & (int) a_value62) ^ a_value56;
      uint a_value66 = array[28] + Bits.RotateRight32(a_value65, 7) + Bits.RotateRight32(a_value50, 11);
      uint a_value67 = (uint) ((int) a_value62 & ((int) a_value58 ^ (int) a_value60) ^ (int) a_value54 & (int) a_value66 ^ (int) a_value56 & (int) a_value64) ^ a_value58;
      uint a_value68 = array[29] + Bits.RotateRight32(a_value67, 7) + Bits.RotateRight32(a_value52, 11);
      uint a_value69 = (uint) ((int) a_value64 & ((int) a_value60 ^ (int) a_value62) ^ (int) a_value56 & (int) a_value68 ^ (int) a_value58 & (int) a_value66) ^ a_value60;
      uint a_value70 = array[30] + Bits.RotateRight32(a_value69, 7) + Bits.RotateRight32(a_value54, 11);
      uint a_value71 = (uint) ((int) a_value66 & ((int) a_value62 ^ (int) a_value64) ^ (int) a_value58 & (int) a_value70 ^ (int) a_value60 & (int) a_value68) ^ a_value62;
      uint a_value72 = array[31 /*0x1F*/] + Bits.RotateRight32(a_value71, 7) + Bits.RotateRight32(a_value56, 11);
      uint a_value73 = (uint) ((int) a_value62 & ((int) a_value66 & ~(int) a_value72 ^ (int) a_value70 & (int) a_value68 ^ (int) a_value64 ^ (int) a_value60) ^ (int) a_value70 & ((int) a_value66 ^ (int) a_value68) ^ (int) a_value72 & (int) a_value68) ^ a_value60;
      uint a_value74 = array[5] + 1160258022U + Bits.RotateRight32(a_value73, 7) + Bits.RotateRight32(a_value58, 11);
      uint a_value75 = (uint) ((int) a_value64 & ((int) a_value68 & ~(int) a_value74 ^ (int) a_value72 & (int) a_value70 ^ (int) a_value66 ^ (int) a_value62) ^ (int) a_value72 & ((int) a_value68 ^ (int) a_value70) ^ (int) a_value74 & (int) a_value70) ^ a_value62;
      uint a_value76 = array[14] + 953160567U + Bits.RotateRight32(a_value75, 7) + Bits.RotateRight32(a_value60, 11);
      uint a_value77 = (uint) ((int) a_value66 & ((int) a_value70 & ~(int) a_value76 ^ (int) a_value74 & (int) a_value72 ^ (int) a_value68 ^ (int) a_value64) ^ (int) a_value74 & ((int) a_value70 ^ (int) a_value72) ^ (int) a_value76 & (int) a_value72) ^ a_value64;
      uint a_value78 = array[26] + 3193202383U + Bits.RotateRight32(a_value77, 7) + Bits.RotateRight32(a_value62, 11);
      uint a_value79 = (uint) ((int) a_value68 & ((int) a_value72 & ~(int) a_value78 ^ (int) a_value76 & (int) a_value74 ^ (int) a_value70 ^ (int) a_value66) ^ (int) a_value76 & ((int) a_value72 ^ (int) a_value74) ^ (int) a_value78 & (int) a_value74) ^ a_value66;
      uint a_value80 = array[18] + 887688300U + Bits.RotateRight32(a_value79, 7) + Bits.RotateRight32(a_value64, 11);
      uint a_value81 = (uint) ((int) a_value70 & ((int) a_value74 & ~(int) a_value80 ^ (int) a_value78 & (int) a_value76 ^ (int) a_value72 ^ (int) a_value68) ^ (int) a_value78 & ((int) a_value74 ^ (int) a_value76) ^ (int) a_value80 & (int) a_value76) ^ a_value68;
      uint a_value82 = array[11] + 3232508343U + Bits.RotateRight32(a_value81, 7) + Bits.RotateRight32(a_value66, 11);
      uint a_value83 = (uint) ((int) a_value72 & ((int) a_value76 & ~(int) a_value82 ^ (int) a_value80 & (int) a_value78 ^ (int) a_value74 ^ (int) a_value70) ^ (int) a_value80 & ((int) a_value76 ^ (int) a_value78) ^ (int) a_value82 & (int) a_value78) ^ a_value70;
      uint a_value84 = array[28] + 3380367581U + Bits.RotateRight32(a_value83, 7) + Bits.RotateRight32(a_value68, 11);
      uint a_value85 = (uint) ((int) a_value74 & ((int) a_value78 & ~(int) a_value84 ^ (int) a_value82 & (int) a_value80 ^ (int) a_value76 ^ (int) a_value72) ^ (int) a_value82 & ((int) a_value78 ^ (int) a_value80) ^ (int) a_value84 & (int) a_value80) ^ a_value72;
      uint a_value86 = array[7] + 1065670069U + Bits.RotateRight32(a_value85, 7) + Bits.RotateRight32(a_value70, 11);
      uint a_value87 = (uint) ((int) a_value76 & ((int) a_value80 & ~(int) a_value86 ^ (int) a_value84 & (int) a_value82 ^ (int) a_value78 ^ (int) a_value74) ^ (int) a_value84 & ((int) a_value80 ^ (int) a_value82) ^ (int) a_value86 & (int) a_value82) ^ a_value74;
      uint a_value88 = array[16 /*0x10*/] + 3041331479U + Bits.RotateRight32(a_value87, 7) + Bits.RotateRight32(a_value72, 11);
      uint a_value89 = (uint) ((int) a_value78 & ((int) a_value82 & ~(int) a_value88 ^ (int) a_value86 & (int) a_value84 ^ (int) a_value80 ^ (int) a_value76) ^ (int) a_value86 & ((int) a_value82 ^ (int) a_value84) ^ (int) a_value88 & (int) a_value84) ^ a_value76;
      uint a_value90 = array[0] + 2450970073U + Bits.RotateRight32(a_value89, 7) + Bits.RotateRight32(a_value74, 11);
      uint a_value91 = (uint) ((int) a_value80 & ((int) a_value84 & ~(int) a_value90 ^ (int) a_value88 & (int) a_value86 ^ (int) a_value82 ^ (int) a_value78) ^ (int) a_value88 & ((int) a_value84 ^ (int) a_value86) ^ (int) a_value90 & (int) a_value86) ^ a_value78;
      uint a_value92 = array[23] + 2306472731U + Bits.RotateRight32(a_value91, 7) + Bits.RotateRight32(a_value76, 11);
      uint a_value93 = (uint) ((int) a_value82 & ((int) a_value86 & ~(int) a_value92 ^ (int) a_value90 & (int) a_value88 ^ (int) a_value84 ^ (int) a_value80) ^ (int) a_value90 & ((int) a_value86 ^ (int) a_value88) ^ (int) a_value92 & (int) a_value88) ^ a_value80;
      uint a_value94 = array[20] + 3509652390U + Bits.RotateRight32(a_value93, 7) + Bits.RotateRight32(a_value78, 11);
      uint a_value95 = (uint) ((int) a_value84 & ((int) a_value88 & ~(int) a_value94 ^ (int) a_value92 & (int) a_value90 ^ (int) a_value86 ^ (int) a_value82) ^ (int) a_value92 & ((int) a_value88 ^ (int) a_value90) ^ (int) a_value94 & (int) a_value90) ^ a_value82;
      uint a_value96 = array[22] + 2564797868U + Bits.RotateRight32(a_value95, 7) + Bits.RotateRight32(a_value80, 11);
      uint a_value97 = (uint) ((int) a_value86 & ((int) a_value90 & ~(int) a_value96 ^ (int) a_value94 & (int) a_value92 ^ (int) a_value88 ^ (int) a_value84) ^ (int) a_value94 & ((int) a_value90 ^ (int) a_value92) ^ (int) a_value96 & (int) a_value92) ^ a_value84;
      uint a_value98 = array[1] + 805139163U + Bits.RotateRight32(a_value97, 7) + Bits.RotateRight32(a_value82, 11);
      uint a_value99 = (uint) ((int) a_value88 & ((int) a_value92 & ~(int) a_value98 ^ (int) a_value96 & (int) a_value94 ^ (int) a_value90 ^ (int) a_value86) ^ (int) a_value96 & ((int) a_value92 ^ (int) a_value94) ^ (int) a_value98 & (int) a_value94) ^ a_value86;
      uint a_value100 = array[10] + 3491422135U + Bits.RotateRight32(a_value99, 7) + Bits.RotateRight32(a_value84, 11);
      uint a_value101 = (uint) ((int) a_value90 & ((int) a_value94 & ~(int) a_value100 ^ (int) a_value98 & (int) a_value96 ^ (int) a_value92 ^ (int) a_value88) ^ (int) a_value98 & ((int) a_value94 ^ (int) a_value96) ^ (int) a_value100 & (int) a_value96) ^ a_value88;
      uint a_value102 = array[4] + 3101798381U + Bits.RotateRight32(a_value101, 7) + Bits.RotateRight32(a_value86, 11);
      uint a_value103 = (uint) ((int) a_value92 & ((int) a_value96 & ~(int) a_value102 ^ (int) a_value100 & (int) a_value98 ^ (int) a_value94 ^ (int) a_value90) ^ (int) a_value100 & ((int) a_value96 ^ (int) a_value98) ^ (int) a_value102 & (int) a_value98) ^ a_value90;
      uint a_value104 = array[8] + 1780907670U + Bits.RotateRight32(a_value103, 7) + Bits.RotateRight32(a_value88, 11);
      uint a_value105 = (uint) ((int) a_value94 & ((int) a_value98 & ~(int) a_value104 ^ (int) a_value102 & (int) a_value100 ^ (int) a_value96 ^ (int) a_value92) ^ (int) a_value102 & ((int) a_value98 ^ (int) a_value100) ^ (int) a_value104 & (int) a_value100) ^ a_value92;
      uint a_value106 = array[30] + 3128725573U + Bits.RotateRight32(a_value105, 7) + Bits.RotateRight32(a_value90, 11);
      uint a_value107 = (uint) ((int) a_value96 & ((int) a_value100 & ~(int) a_value106 ^ (int) a_value104 & (int) a_value102 ^ (int) a_value98 ^ (int) a_value94) ^ (int) a_value104 & ((int) a_value100 ^ (int) a_value102) ^ (int) a_value106 & (int) a_value102) ^ a_value94;
      uint a_value108 = array[3] + 4046225305U + Bits.RotateRight32(a_value107, 7) + Bits.RotateRight32(a_value92, 11);
      uint a_value109 = (uint) ((int) a_value98 & ((int) a_value102 & ~(int) a_value108 ^ (int) a_value106 & (int) a_value104 ^ (int) a_value100 ^ (int) a_value96) ^ (int) a_value106 & ((int) a_value102 ^ (int) a_value104) ^ (int) a_value108 & (int) a_value104) ^ a_value96;
      uint a_value110 = array[21] + 614570311U + Bits.RotateRight32(a_value109, 7) + Bits.RotateRight32(a_value94, 11);
      uint a_value111 = (uint) ((int) a_value100 & ((int) a_value104 & ~(int) a_value110 ^ (int) a_value108 & (int) a_value106 ^ (int) a_value102 ^ (int) a_value98) ^ (int) a_value108 & ((int) a_value104 ^ (int) a_value106) ^ (int) a_value110 & (int) a_value106) ^ a_value98;
      uint a_value112 = array[9] + 3012652279U + Bits.RotateRight32(a_value111, 7) + Bits.RotateRight32(a_value96, 11);
      uint a_value113 = (uint) ((int) a_value102 & ((int) a_value106 & ~(int) a_value112 ^ (int) a_value110 & (int) a_value108 ^ (int) a_value104 ^ (int) a_value100) ^ (int) a_value110 & ((int) a_value106 ^ (int) a_value108) ^ (int) a_value112 & (int) a_value108) ^ a_value100;
      uint a_value114 = array[17] + 134345442U + Bits.RotateRight32(a_value113, 7) + Bits.RotateRight32(a_value98, 11);
      uint a_value115 = (uint) ((int) a_value104 & ((int) a_value108 & ~(int) a_value114 ^ (int) a_value112 & (int) a_value110 ^ (int) a_value106 ^ (int) a_value102) ^ (int) a_value112 & ((int) a_value108 ^ (int) a_value110) ^ (int) a_value114 & (int) a_value110) ^ a_value102;
      uint a_value116 = array[24] + 2240740374U + Bits.RotateRight32(a_value115, 7) + Bits.RotateRight32(a_value100, 11);
      uint a_value117 = (uint) ((int) a_value106 & ((int) a_value110 & ~(int) a_value116 ^ (int) a_value114 & (int) a_value112 ^ (int) a_value108 ^ (int) a_value104) ^ (int) a_value114 & ((int) a_value110 ^ (int) a_value112) ^ (int) a_value116 & (int) a_value112) ^ a_value104;
      uint a_value118 = array[29] + 1667834072U + Bits.RotateRight32(a_value117, 7) + Bits.RotateRight32(a_value102, 11);
      uint a_value119 = (uint) ((int) a_value108 & ((int) a_value112 & ~(int) a_value118 ^ (int) a_value116 & (int) a_value114 ^ (int) a_value110 ^ (int) a_value106) ^ (int) a_value116 & ((int) a_value112 ^ (int) a_value114) ^ (int) a_value118 & (int) a_value114) ^ a_value106;
      uint a_value120 = array[6] + 1901547113U + Bits.RotateRight32(a_value119, 7) + Bits.RotateRight32(a_value104, 11);
      uint a_value121 = (uint) ((int) a_value110 & ((int) a_value114 & ~(int) a_value120 ^ (int) a_value118 & (int) a_value116 ^ (int) a_value112 ^ (int) a_value108) ^ (int) a_value118 & ((int) a_value114 ^ (int) a_value116) ^ (int) a_value120 & (int) a_value116) ^ a_value108;
      uint a_value122 = array[19] + 2757295779U + Bits.RotateRight32(a_value121, 7) + Bits.RotateRight32(a_value106, 11);
      uint a_value123 = (uint) ((int) a_value112 & ((int) a_value116 & ~(int) a_value122 ^ (int) a_value120 & (int) a_value118 ^ (int) a_value114 ^ (int) a_value110) ^ (int) a_value120 & ((int) a_value116 ^ (int) a_value118) ^ (int) a_value122 & (int) a_value118) ^ a_value110;
      uint a_value124 = array[12] + 4103290238U + Bits.RotateRight32(a_value123, 7) + Bits.RotateRight32(a_value108, 11);
      uint a_value125 = (uint) ((int) a_value114 & ((int) a_value118 & ~(int) a_value124 ^ (int) a_value122 & (int) a_value120 ^ (int) a_value116 ^ (int) a_value112) ^ (int) a_value122 & ((int) a_value118 ^ (int) a_value120) ^ (int) a_value124 & (int) a_value120) ^ a_value112;
      uint a_value126 = array[15] + 227898511U + Bits.RotateRight32(a_value125, 7) + Bits.RotateRight32(a_value110, 11);
      uint a_value127 = (uint) ((int) a_value116 & ((int) a_value120 & ~(int) a_value126 ^ (int) a_value124 & (int) a_value122 ^ (int) a_value118 ^ (int) a_value114) ^ (int) a_value124 & ((int) a_value120 ^ (int) a_value122) ^ (int) a_value126 & (int) a_value122) ^ a_value114;
      uint a_value128 = array[13] + 1921955416U + Bits.RotateRight32(a_value127, 7) + Bits.RotateRight32(a_value112, 11);
      uint a_value129 = (uint) ((int) a_value118 & ((int) a_value122 & ~(int) a_value128 ^ (int) a_value126 & (int) a_value124 ^ (int) a_value120 ^ (int) a_value116) ^ (int) a_value126 & ((int) a_value122 ^ (int) a_value124) ^ (int) a_value128 & (int) a_value124) ^ a_value116;
      uint a_value130 = array[2] + 1904987480U + Bits.RotateRight32(a_value129, 7) + Bits.RotateRight32(a_value114, 11);
      uint a_value131 = (uint) ((int) a_value120 & ((int) a_value124 & ~(int) a_value130 ^ (int) a_value128 & (int) a_value126 ^ (int) a_value122 ^ (int) a_value118) ^ (int) a_value128 & ((int) a_value124 ^ (int) a_value126) ^ (int) a_value130 & (int) a_value126) ^ a_value118;
      uint a_value132 = array[25] + 2182433518U + Bits.RotateRight32(a_value131, 7) + Bits.RotateRight32(a_value116, 11);
      uint a_value133 = (uint) ((int) a_value122 & ((int) a_value126 & ~(int) a_value132 ^ (int) a_value130 & (int) a_value128 ^ (int) a_value124 ^ (int) a_value120) ^ (int) a_value130 & ((int) a_value126 ^ (int) a_value128) ^ (int) a_value132 & (int) a_value128) ^ a_value120;
      uint a_value134 = array[31 /*0x1F*/] + 2069144605U + Bits.RotateRight32(a_value133, 7) + Bits.RotateRight32(a_value118, 11);
      uint a_value135 = (uint) ((int) a_value124 & ((int) a_value128 & ~(int) a_value134 ^ (int) a_value132 & (int) a_value130 ^ (int) a_value126 ^ (int) a_value122) ^ (int) a_value132 & ((int) a_value128 ^ (int) a_value130) ^ (int) a_value134 & (int) a_value130) ^ a_value122;
      uint a_value136 = array[27] + 3260701109U + Bits.RotateRight32(a_value135, 7) + Bits.RotateRight32(a_value120, 11);
      uint a_value137 = (uint) ((int) a_value130 & ((int) a_value126 & (int) a_value128 ^ (int) a_value124 ^ (int) a_value136) ^ (int) a_value126 & (int) a_value132 ^ (int) a_value128 & (int) a_value134) ^ a_value136;
      uint a_value138 = array[19] + 2620446009U + Bits.RotateRight32(a_value137, 7) + Bits.RotateRight32(a_value122, 11);
      uint a_value139 = (uint) ((int) a_value132 & ((int) a_value128 & (int) a_value130 ^ (int) a_value126 ^ (int) a_value138) ^ (int) a_value128 & (int) a_value134 ^ (int) a_value130 & (int) a_value136) ^ a_value138;
      uint a_value140 = array[9] + 720527379U + Bits.RotateRight32(a_value139, 7) + Bits.RotateRight32(a_value124, 11);
      uint a_value141 = (uint) ((int) a_value134 & ((int) a_value130 & (int) a_value132 ^ (int) a_value128 ^ (int) a_value140) ^ (int) a_value130 & (int) a_value136 ^ (int) a_value132 & (int) a_value138) ^ a_value140;
      uint a_value142 = array[4] + 3318853667U + Bits.RotateRight32(a_value141, 7) + Bits.RotateRight32(a_value126, 11);
      uint a_value143 = (uint) ((int) a_value136 & ((int) a_value132 & (int) a_value134 ^ (int) a_value130 ^ (int) a_value142) ^ (int) a_value132 & (int) a_value138 ^ (int) a_value134 & (int) a_value140) ^ a_value142;
      uint a_value144 = array[20] + 677414384U + Bits.RotateRight32(a_value143, 7) + Bits.RotateRight32(a_value128, 11);
      uint a_value145 = (uint) ((int) a_value138 & ((int) a_value134 & (int) a_value136 ^ (int) a_value132 ^ (int) a_value144) ^ (int) a_value134 & (int) a_value140 ^ (int) a_value136 & (int) a_value142) ^ a_value144;
      uint a_value146 = array[28] + 3393288472U + Bits.RotateRight32(a_value145, 7) + Bits.RotateRight32(a_value130, 11);
      uint a_value147 = (uint) ((int) a_value140 & ((int) a_value136 & (int) a_value138 ^ (int) a_value134 ^ (int) a_value146) ^ (int) a_value136 & (int) a_value142 ^ (int) a_value138 & (int) a_value144) ^ a_value146;
      uint a_value148 = array[17] + 3101374703U + Bits.RotateRight32(a_value147, 7) + Bits.RotateRight32(a_value132, 11);
      uint a_value149 = (uint) ((int) a_value142 & ((int) a_value138 & (int) a_value140 ^ (int) a_value136 ^ (int) a_value148) ^ (int) a_value138 & (int) a_value144 ^ (int) a_value140 & (int) a_value146) ^ a_value148;
      uint a_value150 = array[8] + 2390351024U + Bits.RotateRight32(a_value149, 7) + Bits.RotateRight32(a_value134, 11);
      uint a_value151 = (uint) ((int) a_value144 & ((int) a_value140 & (int) a_value142 ^ (int) a_value138 ^ (int) a_value150) ^ (int) a_value140 & (int) a_value146 ^ (int) a_value142 & (int) a_value148) ^ a_value150;
      uint a_value152 = array[22] + 1614419982U + Bits.RotateRight32(a_value151, 7) + Bits.RotateRight32(a_value136, 11);
      uint a_value153 = (uint) ((int) a_value146 & ((int) a_value142 & (int) a_value144 ^ (int) a_value140 ^ (int) a_value152) ^ (int) a_value142 & (int) a_value148 ^ (int) a_value144 & (int) a_value150) ^ a_value152;
      uint a_value154 = array[29] + 1822297739U + Bits.RotateRight32(a_value153, 7) + Bits.RotateRight32(a_value138, 11);
      uint a_value155 = (uint) ((int) a_value148 & ((int) a_value144 & (int) a_value146 ^ (int) a_value142 ^ (int) a_value154) ^ (int) a_value144 & (int) a_value150 ^ (int) a_value146 & (int) a_value152) ^ a_value154;
      uint a_value156 = array[14] + 2954791486U + Bits.RotateRight32(a_value155, 7) + Bits.RotateRight32(a_value140, 11);
      uint a_value157 = (uint) ((int) a_value150 & ((int) a_value146 & (int) a_value148 ^ (int) a_value144 ^ (int) a_value156) ^ (int) a_value146 & (int) a_value152 ^ (int) a_value148 & (int) a_value154) ^ a_value156;
      uint a_value158 = array[25] + 3608508353U + Bits.RotateRight32(a_value157, 7) + Bits.RotateRight32(a_value142, 11);
      uint a_value159 = (uint) ((int) a_value152 & ((int) a_value148 & (int) a_value150 ^ (int) a_value146 ^ (int) a_value158) ^ (int) a_value148 & (int) a_value154 ^ (int) a_value150 & (int) a_value156) ^ a_value158;
      uint a_value160 = array[12] + 3174124327U + Bits.RotateRight32(a_value159, 7) + Bits.RotateRight32(a_value144, 11);
      uint a_value161 = (uint) ((int) a_value154 & ((int) a_value150 & (int) a_value152 ^ (int) a_value148 ^ (int) a_value160) ^ (int) a_value150 & (int) a_value156 ^ (int) a_value152 & (int) a_value158) ^ a_value160;
      uint a_value162 = array[24] + 2024746970U + Bits.RotateRight32(a_value161, 7) + Bits.RotateRight32(a_value146, 11);
      uint a_value163 = (uint) ((int) a_value156 & ((int) a_value152 & (int) a_value154 ^ (int) a_value150 ^ (int) a_value162) ^ (int) a_value152 & (int) a_value158 ^ (int) a_value154 & (int) a_value160) ^ a_value162;
      uint a_value164 = array[30] + 1432378464U + Bits.RotateRight32(a_value163, 7) + Bits.RotateRight32(a_value148, 11);
      uint a_value165 = (uint) ((int) a_value158 & ((int) a_value154 & (int) a_value156 ^ (int) a_value152 ^ (int) a_value164) ^ (int) a_value154 & (int) a_value160 ^ (int) a_value156 & (int) a_value162) ^ a_value164;
      uint a_value166 = array[16 /*0x10*/] + 3864339955U + Bits.RotateRight32(a_value165, 7) + Bits.RotateRight32(a_value150, 11);
      uint a_value167 = (uint) ((int) a_value160 & ((int) a_value156 & (int) a_value158 ^ (int) a_value154 ^ (int) a_value166) ^ (int) a_value156 & (int) a_value162 ^ (int) a_value158 & (int) a_value164) ^ a_value166;
      uint a_value168 = array[26] + 2857741204U + Bits.RotateRight32(a_value167, 7) + Bits.RotateRight32(a_value152, 11);
      uint a_value169 = (uint) ((int) a_value162 & ((int) a_value158 & (int) a_value160 ^ (int) a_value156 ^ (int) a_value168) ^ (int) a_value158 & (int) a_value164 ^ (int) a_value160 & (int) a_value166) ^ a_value168;
      uint a_value170 = array[31 /*0x1F*/] + 1464375394U + Bits.RotateRight32(a_value169, 7) + Bits.RotateRight32(a_value154, 11);
      uint a_value171 = (uint) ((int) a_value164 & ((int) a_value160 & (int) a_value162 ^ (int) a_value158 ^ (int) a_value170) ^ (int) a_value160 & (int) a_value166 ^ (int) a_value162 & (int) a_value168) ^ a_value170;
      uint a_value172 = array[15] + 1676153920U + Bits.RotateRight32(a_value171, 7) + Bits.RotateRight32(a_value156, 11);
      uint a_value173 = (uint) ((int) a_value166 & ((int) a_value162 & (int) a_value164 ^ (int) a_value160 ^ (int) a_value172) ^ (int) a_value162 & (int) a_value168 ^ (int) a_value164 & (int) a_value170) ^ a_value172;
      uint a_value174 = array[7] + 1439316330U + Bits.RotateRight32(a_value173, 7) + Bits.RotateRight32(a_value158, 11);
      uint a_value175 = (uint) ((int) a_value168 & ((int) a_value164 & (int) a_value166 ^ (int) a_value162 ^ (int) a_value174) ^ (int) a_value164 & (int) a_value170 ^ (int) a_value166 & (int) a_value172) ^ a_value174;
      uint a_value176 = array[3] + 715854006U + Bits.RotateRight32(a_value175, 7) + Bits.RotateRight32(a_value160, 11);
      uint a_value177 = (uint) ((int) a_value170 & ((int) a_value166 & (int) a_value168 ^ (int) a_value164 ^ (int) a_value176) ^ (int) a_value166 & (int) a_value172 ^ (int) a_value168 & (int) a_value174) ^ a_value176;
      uint a_value178 = array[1] + 3033291828U + Bits.RotateRight32(a_value177, 7) + Bits.RotateRight32(a_value162, 11);
      uint a_value179 = (uint) ((int) a_value172 & ((int) a_value168 & (int) a_value170 ^ (int) a_value166 ^ (int) a_value178) ^ (int) a_value168 & (int) a_value174 ^ (int) a_value170 & (int) a_value176) ^ a_value178;
      uint a_value180 = array[0] + 289532110U + Bits.RotateRight32(a_value179, 7) + Bits.RotateRight32(a_value164, 11);
      uint a_value181 = (uint) ((int) a_value174 & ((int) a_value170 & (int) a_value172 ^ (int) a_value168 ^ (int) a_value180) ^ (int) a_value170 & (int) a_value176 ^ (int) a_value172 & (int) a_value178) ^ a_value180;
      uint a_value182 = array[18] + 2706671279U + Bits.RotateRight32(a_value181, 7) + Bits.RotateRight32(a_value166, 11);
      uint a_value183 = (uint) ((int) a_value176 & ((int) a_value172 & (int) a_value174 ^ (int) a_value170 ^ (int) a_value182) ^ (int) a_value172 & (int) a_value178 ^ (int) a_value174 & (int) a_value180) ^ a_value182;
      uint a_value184 = array[27] + 2087905683U + Bits.RotateRight32(a_value183, 7) + Bits.RotateRight32(a_value168, 11);
      uint a_value185 = (uint) ((int) a_value178 & ((int) a_value174 & (int) a_value176 ^ (int) a_value172 ^ (int) a_value184) ^ (int) a_value174 & (int) a_value180 ^ (int) a_value176 & (int) a_value182) ^ a_value184;
      uint num1 = array[13] + 3018724369U + Bits.RotateRight32(a_value185, 7) + Bits.RotateRight32(a_value170, 11);
      uint a_value186 = (uint) ((int) a_value180 & ((int) a_value176 & (int) a_value178 ^ (int) a_value174 ^ (int) num1) ^ (int) a_value176 & (int) a_value182 ^ (int) a_value178 & (int) a_value184) ^ num1;
      uint num2 = array[6] + 1668267050U + Bits.RotateRight32(a_value186, 7) + Bits.RotateRight32(a_value172, 11);
      uint a_value187 = (uint) ((int) a_value182 & ((int) a_value178 & (int) a_value180 ^ (int) a_value176 ^ (int) num2) ^ (int) a_value178 & (int) a_value184 ^ (int) a_value180 & (int) num1) ^ num2;
      uint num3 = array[21] + 732546397U + Bits.RotateRight32(a_value187, 7) + Bits.RotateRight32(a_value174, 11);
      uint a_value188 = (uint) ((int) a_value184 & ((int) a_value180 & (int) a_value182 ^ (int) a_value178 ^ (int) num3) ^ (int) a_value180 & (int) num1 ^ (int) a_value182 & (int) num2) ^ num3;
      uint num4 = array[10] + 1947742710U + Bits.RotateRight32(a_value188, 7) + Bits.RotateRight32(a_value176, 11);
      uint a_value189 = (uint) ((int) num1 & ((int) a_value182 & (int) a_value184 ^ (int) a_value180 ^ (int) num4) ^ (int) a_value182 & (int) num2 ^ (int) a_value184 & (int) num3) ^ num4;
      uint num5 = array[23] + 3462151702U + Bits.RotateRight32(a_value189, 7) + Bits.RotateRight32(a_value178, 11);
      uint a_value190 = (uint) ((int) num2 & ((int) a_value184 & (int) num1 ^ (int) a_value182 ^ (int) num5) ^ (int) a_value184 & (int) num3 ^ (int) num1 & (int) num4) ^ num5;
      uint num6 = array[11] + 2609353502U + Bits.RotateRight32(a_value190, 7) + Bits.RotateRight32(a_value180, 11);
      uint a_value191 = (uint) ((int) num3 & ((int) num1 & (int) num2 ^ (int) a_value184 ^ (int) num6) ^ (int) num1 & (int) num4 ^ (int) num2 & (int) num5) ^ num6;
      uint num7 = array[5] + 2950085171U + Bits.RotateRight32(a_value191, 7) + Bits.RotateRight32(a_value182, 11);
      uint a_value192 = (uint) ((int) num4 & ((int) num2 & (int) num3 ^ (int) num1 ^ (int) num7) ^ (int) num2 & (int) num5 ^ (int) num3 & (int) num6) ^ num7;
      this.hash[0] = this.hash[0] + (array[2] + 1814351708U + Bits.RotateRight32(a_value192, 7) + Bits.RotateRight32(a_value184, 11));
      this.hash[1] = this.hash[1] + num7;
      this.hash[2] = this.hash[2] + num6;
      this.hash[3] = this.hash[3] + num5;
      this.hash[4] = this.hash[4] + num4;
      this.hash[5] = this.hash[5] + num3;
      this.hash[6] = this.hash[6] + num2;
      this.hash[7] = this.hash[7] + num1;
      Intermech.Hashes.Utils.Utils.Memset(ref array, (byte) 0);
    }
  }
}

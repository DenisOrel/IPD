// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2S
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Crypto.Blake2SConfigurations;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal class Blake2S : Hash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  public static readonly string InvalidConfigLength = "Config Length Must Be 8 Words";
  public static readonly string ConfigNil = "Config Cannot Be Nil";
  public static readonly string InvalidXOFSize = "XOFSize in Bits must be Multiples of 8 and be Between {0} and {1} Bytes.";
  public static readonly string OutputLengthInvalid = "Output Length is above the Digest Length";
  public static readonly string OutputBufferTooShort = "Output Buffer Too Short";
  public static readonly string MaximumOutputLengthExceeded = "Maximum Length is 2^32 blocks of 32 bytes";
  public static readonly string WritetoXofAfterReadError = "\"{0}\" Write to Xof after Read not Allowed";
  protected uint[] State;
  protected uint[] M;
  protected byte[] Buffer;
  protected IBlake2STreeConfig TreeConfig;
  protected IBlake2SConfig Config;
  private bool DoTransformKeyBlock;
  private const int BlockSizeInBytes = 64 /*0x40*/;
  private const uint IV0 = 1779033703;
  private const uint IV1 = 3144134277;
  private const uint IV2 = 1013904242;
  private const uint IV3 = 2773480762;
  private const uint IV4 = 1359893119;
  private const uint IV5 = 2600822924;
  private const uint IV6 = 528734635;
  private const uint IV7 = 1541459225;

  protected int FilledBufferCount { get; set; }

  protected uint Counter0 { get; set; }

  protected uint Counter1 { get; set; }

  protected uint FinalizationFlag0 { get; set; }

  protected uint FinalizationFlag1 { get; set; }

  public Blake2S()
    : this((IBlake2SConfig) new Blake2SConfig())
  {
  }

  public Blake2S(IBlake2SConfig a_Config)
    : this(a_Config, (IBlake2STreeConfig) null)
  {
  }

  public Blake2S(
    IBlake2SConfig a_Config,
    IBlake2STreeConfig a_TreeConfig,
    bool a_DoTransformKeyBlock = true)
    : base(a_Config != null ? a_Config.HashSize : -1, 64 /*0x40*/)
  {
    this.Config = a_Config;
    this.TreeConfig = a_TreeConfig;
    this.DoTransformKeyBlock = a_DoTransformKeyBlock;
    if (this.Config == null)
      this.Config = (IBlake2SConfig) Blake2SConfig.DefaultConfig;
    this.HashSize = this.Config.HashSize;
    this.State = new uint[8];
    this.M = new uint[16 /*0x10*/];
    this.Buffer = new byte[64 /*0x40*/];
  }

  public override string Name => $"{this.GetType().Name}_{this.HashSize * 8}";

  public Blake2S CloneInternal()
  {
    Blake2S blake2S = new Blake2S(this.Config.Clone(), this.TreeConfig?.Clone(), this.DoTransformKeyBlock);
    blake2S.State = this.State.DeepCopy();
    blake2S.M = this.M.DeepCopy();
    blake2S.Buffer = this.Buffer.DeepCopy();
    blake2S.FilledBufferCount = this.FilledBufferCount;
    blake2S.Counter0 = this.Counter0;
    blake2S.Counter1 = this.Counter1;
    blake2S.FinalizationFlag0 = this.FinalizationFlag0;
    blake2S.FinalizationFlag1 = this.FinalizationFlag1;
    blake2S.BufferSize = this.BufferSize;
    return blake2S;
  }

  public override IHash Clone() => (IHash) this.CloneInternal();

  public override void Initialize()
  {
    byte[] numArray = (byte[]) null;
    uint[] array = Blake2SIvBuilder.ConfigS(this.Config, this.TreeConfig);
    if (this.DoTransformKeyBlock && !this.Config.Key.Empty())
    {
      numArray = this.Config.Key.DeepCopy();
      Array.Resize<byte>(ref numArray, 64 /*0x40*/);
    }
    if (array.Empty())
      throw new ArgumentNullHashLibException(Blake2S.ConfigNil);
    if (array.Length != 8)
      throw new ArgumentHashLibException(Blake2S.InvalidConfigLength);
    this.State[0] = 1779033703U;
    this.State[1] = 3144134277U;
    this.State[2] = 1013904242U;
    this.State[3] = 2773480762U;
    this.State[4] = 1359893119U;
    this.State[5] = 2600822924U;
    this.State[6] = 528734635U;
    this.State[7] = 1541459225U;
    this.Counter0 = 0U;
    this.Counter1 = 0U;
    this.FinalizationFlag0 = 0U;
    this.FinalizationFlag1 = 0U;
    this.FilledBufferCount = 0;
    ArrayUtils.ZeroFill(ref this.Buffer);
    ArrayUtils.ZeroFill(ref this.M);
    for (int index = 0; index < 8; ++index)
      this.State[index] = this.State[index] ^ array[index];
    if (!this.DoTransformKeyBlock || numArray.Empty())
      return;
    this.TransformBytes(numArray, 0, numArray.Length);
    ArrayUtils.ZeroFill(ref numArray);
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int num = a_index;
    int n = 64 /*0x40*/ - this.FilledBufferCount;
    if (this.FilledBufferCount > 0 && a_length > n)
    {
      if (n > 0)
        Intermech.Hashes.Utils.Utils.Memmove(ref this.Buffer, a_data, n, num, this.FilledBufferCount);
      this.Blake2SIncrementCounter(64U /*0x40*/);
      this.Compress(ref this.Buffer, 0);
      num += n;
      a_length -= n;
      this.FilledBufferCount = 0;
    }
    for (; a_length > 64 /*0x40*/; a_length -= 64 /*0x40*/)
    {
      this.Blake2SIncrementCounter(64U /*0x40*/);
      this.Compress(ref a_data, num);
      num += 64 /*0x40*/;
    }
    if (a_length <= 0)
      return;
    Intermech.Hashes.Utils.Utils.Memmove(ref this.Buffer, a_data, a_length, num, this.FilledBufferCount);
    this.FilledBufferCount += a_length;
  }

  public override unsafe IHashResult TransformFinal()
  {
    this.Finish();
    byte[] a_hash = new byte[this.HashSize];
    fixed (uint* src = this.State)
      fixed (byte* dest = a_hash)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, a_hash.Length);
    HashResult hashResult = new HashResult(a_hash);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  private void Blake2SIncrementCounter(uint a_IncrementCount)
  {
    this.Counter0 += a_IncrementCount;
    this.Counter1 += this.Counter0 < a_IncrementCount ? 1U : 0U;
  }

  private void MixScalar()
  {
    uint num1 = this.M[0];
    uint num2 = this.M[1];
    uint num3 = this.M[2];
    uint num4 = this.M[3];
    uint num5 = this.M[4];
    uint num6 = this.M[5];
    uint num7 = this.M[6];
    uint num8 = this.M[7];
    uint num9 = this.M[8];
    uint num10 = this.M[9];
    uint num11 = this.M[10];
    uint num12 = this.M[11];
    uint num13 = this.M[12];
    uint num14 = this.M[13];
    uint num15 = this.M[14];
    uint num16 = this.M[15];
    uint num17 = this.State[0];
    uint num18 = this.State[1];
    uint num19 = this.State[2];
    uint num20 = this.State[3];
    uint num21 = this.State[4];
    uint num22 = this.State[5];
    uint num23 = this.State[6];
    uint num24 = this.State[7];
    uint num25 = 1779033703;
    uint num26 = 3144134277;
    uint num27 = 1013904242;
    uint num28 = 2773480762;
    uint num29 = 1359893119U ^ this.Counter0;
    uint num30 = 2600822924U ^ this.Counter1;
    uint num31 = 528734635U ^ this.FinalizationFlag0;
    uint num32 = 1541459225U ^ this.FinalizationFlag1;
    uint num33 = num17 + num1 + num21;
    uint num34 = Bits.RotateRight32(num29 ^ num33, 16 /*0x10*/);
    uint num35 = num25 + num34;
    uint num36 = Bits.RotateRight32(num21 ^ num35, 12);
    uint num37 = num18 + num3 + num22;
    uint num38 = Bits.RotateRight32(num30 ^ num37, 16 /*0x10*/);
    uint num39 = num26 + num38;
    uint num40 = Bits.RotateRight32(num22 ^ num39, 12);
    uint num41 = num19 + num5 + num23;
    uint num42 = Bits.RotateRight32(num31 ^ num41, 16 /*0x10*/);
    uint num43 = num27 + num42;
    uint num44 = Bits.RotateRight32(num23 ^ num43, 12);
    uint num45 = num20 + num7 + num24;
    uint num46 = Bits.RotateRight32(num32 ^ num45, 16 /*0x10*/);
    uint num47 = num28 + num46;
    uint num48 = Bits.RotateRight32(num24 ^ num47, 12);
    uint num49 = num41 + num6 + num44;
    uint num50 = Bits.RotateRight32(num42 ^ num49, 8);
    uint num51 = num43 + num50;
    uint num52 = Bits.RotateRight32(num44 ^ num51, 7);
    uint num53 = num45 + num8 + num48;
    uint num54 = Bits.RotateRight32(num46 ^ num53, 8);
    uint num55 = num47 + num54;
    uint num56 = Bits.RotateRight32(num48 ^ num55, 7);
    uint num57 = num37 + num4 + num40;
    uint num58 = Bits.RotateRight32(num38 ^ num57, 8);
    uint num59 = num39 + num58;
    uint num60 = Bits.RotateRight32(num40 ^ num59, 7);
    uint num61 = num33 + num2 + num36;
    uint num62 = Bits.RotateRight32(num34 ^ num61, 8);
    uint num63 = num35 + num62;
    uint num64 = Bits.RotateRight32(num36 ^ num63, 7);
    uint num65 = num61 + num9 + num60;
    uint num66 = Bits.RotateRight32(num54 ^ num65, 16 /*0x10*/);
    uint num67 = num51 + num66;
    uint num68 = Bits.RotateRight32(num60 ^ num67, 12);
    uint num69 = num57 + num11 + num52;
    uint num70 = Bits.RotateRight32(num62 ^ num69, 16 /*0x10*/);
    uint num71 = num55 + num70;
    uint num72 = Bits.RotateRight32(num52 ^ num71, 12);
    uint num73 = num49 + num13 + num56;
    uint num74 = Bits.RotateRight32(num58 ^ num73, 16 /*0x10*/);
    uint num75 = num63 + num74;
    uint num76 = Bits.RotateRight32(num56 ^ num75, 12);
    uint num77 = num53 + num15 + num64;
    uint num78 = Bits.RotateRight32(num50 ^ num77, 16 /*0x10*/);
    uint num79 = num59 + num78;
    uint num80 = Bits.RotateRight32(num64 ^ num79, 12);
    uint num81 = num73 + num14 + num76;
    uint num82 = Bits.RotateRight32(num74 ^ num81, 8);
    uint num83 = num75 + num82;
    uint num84 = Bits.RotateRight32(num76 ^ num83, 7);
    uint num85 = num77 + num16 + num80;
    uint num86 = Bits.RotateRight32(num78 ^ num85, 8);
    uint num87 = num79 + num86;
    uint num88 = Bits.RotateRight32(num80 ^ num87, 7);
    uint num89 = num69 + num12 + num72;
    uint num90 = Bits.RotateRight32(num70 ^ num89, 8);
    uint num91 = num71 + num90;
    uint num92 = Bits.RotateRight32(num72 ^ num91, 7);
    uint num93 = num65 + num10 + num68;
    uint num94 = Bits.RotateRight32(num66 ^ num93, 8);
    uint num95 = num67 + num94;
    uint num96 = Bits.RotateRight32(num68 ^ num95, 7);
    uint num97 = num93 + num15 + num88;
    uint num98 = Bits.RotateRight32(num90 ^ num97, 16 /*0x10*/);
    uint num99 = num83 + num98;
    uint num100 = Bits.RotateRight32(num88 ^ num99, 12);
    uint num101 = num89 + num5 + num96;
    uint num102 = Bits.RotateRight32(num82 ^ num101, 16 /*0x10*/);
    uint num103 = num87 + num102;
    uint num104 = Bits.RotateRight32(num96 ^ num103, 12);
    uint num105 = num81 + num10 + num92;
    uint num106 = Bits.RotateRight32(num86 ^ num105, 16 /*0x10*/);
    uint num107 = num95 + num106;
    uint num108 = Bits.RotateRight32(num92 ^ num107, 12);
    uint num109 = num85 + num14 + num84;
    uint num110 = Bits.RotateRight32(num94 ^ num109, 16 /*0x10*/);
    uint num111 = num91 + num110;
    uint num112 = Bits.RotateRight32(num84 ^ num111, 12);
    uint num113 = num105 + num16 + num108;
    uint num114 = Bits.RotateRight32(num106 ^ num113, 8);
    uint num115 = num107 + num114;
    uint num116 = Bits.RotateRight32(num108 ^ num115, 7);
    uint num117 = num109 + num7 + num112;
    uint num118 = Bits.RotateRight32(num110 ^ num117, 8);
    uint num119 = num111 + num118;
    uint num120 = Bits.RotateRight32(num112 ^ num119, 7);
    uint num121 = num101 + num9 + num104;
    uint num122 = Bits.RotateRight32(num102 ^ num121, 8);
    uint num123 = num103 + num122;
    uint num124 = Bits.RotateRight32(num104 ^ num123, 7);
    uint num125 = num97 + num11 + num100;
    uint num126 = Bits.RotateRight32(num98 ^ num125, 8);
    uint num127 = num99 + num126;
    uint num128 = Bits.RotateRight32(num100 ^ num127, 7);
    uint num129 = num125 + num2 + num124;
    uint num130 = Bits.RotateRight32(num118 ^ num129, 16 /*0x10*/);
    uint num131 = num115 + num130;
    uint num132 = Bits.RotateRight32(num124 ^ num131, 12);
    uint num133 = num121 + num1 + num116;
    uint num134 = Bits.RotateRight32(num126 ^ num133, 16 /*0x10*/);
    uint num135 = num119 + num134;
    uint num136 = Bits.RotateRight32(num116 ^ num135, 12);
    uint num137 = num113 + num12 + num120;
    uint num138 = Bits.RotateRight32(num122 ^ num137, 16 /*0x10*/);
    uint num139 = num127 + num138;
    uint num140 = Bits.RotateRight32(num120 ^ num139, 12);
    uint num141 = num117 + num6 + num128;
    uint num142 = Bits.RotateRight32(num114 ^ num141, 16 /*0x10*/);
    uint num143 = num123 + num142;
    uint num144 = Bits.RotateRight32(num128 ^ num143, 12);
    uint num145 = num137 + num8 + num140;
    uint num146 = Bits.RotateRight32(num138 ^ num145, 8);
    uint num147 = num139 + num146;
    uint num148 = Bits.RotateRight32(num140 ^ num147, 7);
    uint num149 = num141 + num4 + num144;
    uint num150 = Bits.RotateRight32(num142 ^ num149, 8);
    uint num151 = num143 + num150;
    uint num152 = Bits.RotateRight32(num144 ^ num151, 7);
    uint num153 = num133 + num3 + num136;
    uint num154 = Bits.RotateRight32(num134 ^ num153, 8);
    uint num155 = num135 + num154;
    uint num156 = Bits.RotateRight32(num136 ^ num155, 7);
    uint num157 = num129 + num13 + num132;
    uint num158 = Bits.RotateRight32(num130 ^ num157, 8);
    uint num159 = num131 + num158;
    uint num160 = Bits.RotateRight32(num132 ^ num159, 7);
    uint num161 = num157 + num12 + num152;
    uint num162 = Bits.RotateRight32(num154 ^ num161, 16 /*0x10*/);
    uint num163 = num147 + num162;
    uint num164 = Bits.RotateRight32(num152 ^ num163, 12);
    uint num165 = num153 + num13 + num160;
    uint num166 = Bits.RotateRight32(num146 ^ num165, 16 /*0x10*/);
    uint num167 = num151 + num166;
    uint num168 = Bits.RotateRight32(num160 ^ num167, 12);
    uint num169 = num145 + num6 + num156;
    uint num170 = Bits.RotateRight32(num150 ^ num169, 16 /*0x10*/);
    uint num171 = num159 + num170;
    uint num172 = Bits.RotateRight32(num156 ^ num171, 12);
    uint num173 = num149 + num16 + num148;
    uint num174 = Bits.RotateRight32(num158 ^ num173, 16 /*0x10*/);
    uint num175 = num155 + num174;
    uint num176 = Bits.RotateRight32(num148 ^ num175, 12);
    uint num177 = num169 + num3 + num172;
    uint num178 = Bits.RotateRight32(num170 ^ num177, 8);
    uint num179 = num171 + num178;
    uint num180 = Bits.RotateRight32(num172 ^ num179, 7);
    uint num181 = num173 + num14 + num176;
    uint num182 = Bits.RotateRight32(num174 ^ num181, 8);
    uint num183 = num175 + num182;
    uint num184 = Bits.RotateRight32(num176 ^ num183, 7);
    uint num185 = num165 + num1 + num168;
    uint num186 = Bits.RotateRight32(num166 ^ num185, 8);
    uint num187 = num167 + num186;
    uint num188 = Bits.RotateRight32(num168 ^ num187, 7);
    uint num189 = num161 + num9 + num164;
    uint num190 = Bits.RotateRight32(num162 ^ num189, 8);
    uint num191 = num163 + num190;
    uint num192 = Bits.RotateRight32(num164 ^ num191, 7);
    uint num193 = num189 + num11 + num188;
    uint num194 = Bits.RotateRight32(num182 ^ num193, 16 /*0x10*/);
    uint num195 = num179 + num194;
    uint num196 = Bits.RotateRight32(num188 ^ num195, 12);
    uint num197 = num185 + num4 + num180;
    uint num198 = Bits.RotateRight32(num190 ^ num197, 16 /*0x10*/);
    uint num199 = num183 + num198;
    uint num200 = Bits.RotateRight32(num180 ^ num199, 12);
    uint num201 = num177 + num8 + num184;
    uint num202 = Bits.RotateRight32(num186 ^ num201, 16 /*0x10*/);
    uint num203 = num191 + num202;
    uint num204 = Bits.RotateRight32(num184 ^ num203, 12);
    uint num205 = num181 + num10 + num192;
    uint num206 = Bits.RotateRight32(num178 ^ num205, 16 /*0x10*/);
    uint num207 = num187 + num206;
    uint num208 = Bits.RotateRight32(num192 ^ num207, 12);
    uint num209 = num201 + num2 + num204;
    uint num210 = Bits.RotateRight32(num202 ^ num209, 8);
    uint num211 = num203 + num210;
    uint num212 = Bits.RotateRight32(num204 ^ num211, 7);
    uint num213 = num205 + num5 + num208;
    uint num214 = Bits.RotateRight32(num206 ^ num213, 8);
    uint num215 = num207 + num214;
    uint num216 = Bits.RotateRight32(num208 ^ num215, 7);
    uint num217 = num197 + num7 + num200;
    uint num218 = Bits.RotateRight32(num198 ^ num217, 8);
    uint num219 = num199 + num218;
    uint num220 = Bits.RotateRight32(num200 ^ num219, 7);
    uint num221 = num193 + num15 + num196;
    uint num222 = Bits.RotateRight32(num194 ^ num221, 8);
    uint num223 = num195 + num222;
    uint num224 = Bits.RotateRight32(num196 ^ num223, 7);
    uint num225 = num221 + num8 + num216;
    uint num226 = Bits.RotateRight32(num218 ^ num225, 16 /*0x10*/);
    uint num227 = num211 + num226;
    uint num228 = Bits.RotateRight32(num216 ^ num227, 12);
    uint num229 = num217 + num4 + num224;
    uint num230 = Bits.RotateRight32(num210 ^ num229, 16 /*0x10*/);
    uint num231 = num215 + num230;
    uint num232 = Bits.RotateRight32(num224 ^ num231, 12);
    uint num233 = num209 + num14 + num220;
    uint num234 = Bits.RotateRight32(num214 ^ num233, 16 /*0x10*/);
    uint num235 = num223 + num234;
    uint num236 = Bits.RotateRight32(num220 ^ num235, 12);
    uint num237 = num213 + num12 + num212;
    uint num238 = Bits.RotateRight32(num222 ^ num237, 16 /*0x10*/);
    uint num239 = num219 + num238;
    uint num240 = Bits.RotateRight32(num212 ^ num239, 12);
    uint num241 = num233 + num13 + num236;
    uint num242 = Bits.RotateRight32(num234 ^ num241, 8);
    uint num243 = num235 + num242;
    uint num244 = Bits.RotateRight32(num236 ^ num243, 7);
    uint num245 = num237 + num15 + num240;
    uint num246 = Bits.RotateRight32(num238 ^ num245, 8);
    uint num247 = num239 + num246;
    uint num248 = Bits.RotateRight32(num240 ^ num247, 7);
    uint num249 = num229 + num2 + num232;
    uint num250 = Bits.RotateRight32(num230 ^ num249, 8);
    uint num251 = num231 + num250;
    uint num252 = Bits.RotateRight32(num232 ^ num251, 7);
    uint num253 = num225 + num10 + num228;
    uint num254 = Bits.RotateRight32(num226 ^ num253, 8);
    uint num255 = num227 + num254;
    uint num256 = Bits.RotateRight32(num228 ^ num255, 7);
    uint num257 = num253 + num3 + num252;
    uint num258 = Bits.RotateRight32(num246 ^ num257, 16 /*0x10*/);
    uint num259 = num243 + num258;
    uint num260 = Bits.RotateRight32(num252 ^ num259, 12);
    uint num261 = num249 + num6 + num244;
    uint num262 = Bits.RotateRight32(num254 ^ num261, 16 /*0x10*/);
    uint num263 = num247 + num262;
    uint num264 = Bits.RotateRight32(num244 ^ num263, 12);
    uint num265 = num241 + num5 + num248;
    uint num266 = Bits.RotateRight32(num250 ^ num265, 16 /*0x10*/);
    uint num267 = num255 + num266;
    uint num268 = Bits.RotateRight32(num248 ^ num267, 12);
    uint num269 = num245 + num16 + num256;
    uint num270 = Bits.RotateRight32(num242 ^ num269, 16 /*0x10*/);
    uint num271 = num251 + num270;
    uint num272 = Bits.RotateRight32(num256 ^ num271, 12);
    uint num273 = num265 + num1 + num268;
    uint num274 = Bits.RotateRight32(num266 ^ num273, 8);
    uint num275 = num267 + num274;
    uint num276 = Bits.RotateRight32(num268 ^ num275, 7);
    uint num277 = num269 + num9 + num272;
    uint num278 = Bits.RotateRight32(num270 ^ num277, 8);
    uint num279 = num271 + num278;
    uint num280 = Bits.RotateRight32(num272 ^ num279, 7);
    uint num281 = num261 + num11 + num264;
    uint num282 = Bits.RotateRight32(num262 ^ num281, 8);
    uint num283 = num263 + num282;
    uint num284 = Bits.RotateRight32(num264 ^ num283, 7);
    uint num285 = num257 + num7 + num260;
    uint num286 = Bits.RotateRight32(num258 ^ num285, 8);
    uint num287 = num259 + num286;
    uint num288 = Bits.RotateRight32(num260 ^ num287, 7);
    uint num289 = num285 + num10 + num280;
    uint num290 = Bits.RotateRight32(num282 ^ num289, 16 /*0x10*/);
    uint num291 = num275 + num290;
    uint num292 = Bits.RotateRight32(num280 ^ num291, 12);
    uint num293 = num281 + num6 + num288;
    uint num294 = Bits.RotateRight32(num274 ^ num293, 16 /*0x10*/);
    uint num295 = num279 + num294;
    uint num296 = Bits.RotateRight32(num288 ^ num295, 12);
    uint num297 = num273 + num3 + num284;
    uint num298 = Bits.RotateRight32(num278 ^ num297, 16 /*0x10*/);
    uint num299 = num287 + num298;
    uint num300 = Bits.RotateRight32(num284 ^ num299, 12);
    uint num301 = num277 + num11 + num276;
    uint num302 = Bits.RotateRight32(num286 ^ num301, 16 /*0x10*/);
    uint num303 = num283 + num302;
    uint num304 = Bits.RotateRight32(num276 ^ num303, 12);
    uint num305 = num297 + num5 + num300;
    uint num306 = Bits.RotateRight32(num298 ^ num305, 8);
    uint num307 = num299 + num306;
    uint num308 = Bits.RotateRight32(num300 ^ num307, 7);
    uint num309 = num301 + num16 + num304;
    uint num310 = Bits.RotateRight32(num302 ^ num309, 8);
    uint num311 = num303 + num310;
    uint num312 = Bits.RotateRight32(num304 ^ num311, 7);
    uint num313 = num293 + num8 + num296;
    uint num314 = Bits.RotateRight32(num294 ^ num313, 8);
    uint num315 = num295 + num314;
    uint num316 = Bits.RotateRight32(num296 ^ num315, 7);
    uint num317 = num289 + num1 + num292;
    uint num318 = Bits.RotateRight32(num290 ^ num317, 8);
    uint num319 = num291 + num318;
    uint num320 = Bits.RotateRight32(num292 ^ num319, 7);
    uint num321 = num317 + num15 + num316;
    uint num322 = Bits.RotateRight32(num310 ^ num321, 16 /*0x10*/);
    uint num323 = num307 + num322;
    uint num324 = Bits.RotateRight32(num316 ^ num323, 12);
    uint num325 = num313 + num12 + num308;
    uint num326 = Bits.RotateRight32(num318 ^ num325, 16 /*0x10*/);
    uint num327 = num311 + num326;
    uint num328 = Bits.RotateRight32(num308 ^ num327, 12);
    uint num329 = num305 + num7 + num312;
    uint num330 = Bits.RotateRight32(num314 ^ num329, 16 /*0x10*/);
    uint num331 = num319 + num330;
    uint num332 = Bits.RotateRight32(num312 ^ num331, 12);
    uint num333 = num309 + num4 + num320;
    uint num334 = Bits.RotateRight32(num306 ^ num333, 16 /*0x10*/);
    uint num335 = num315 + num334;
    uint num336 = Bits.RotateRight32(num320 ^ num335, 12);
    uint num337 = num329 + num9 + num332;
    uint num338 = Bits.RotateRight32(num330 ^ num337, 8);
    uint num339 = num331 + num338;
    uint num340 = Bits.RotateRight32(num332 ^ num339, 7);
    uint num341 = num333 + num14 + num336;
    uint num342 = Bits.RotateRight32(num334 ^ num341, 8);
    uint num343 = num335 + num342;
    uint num344 = Bits.RotateRight32(num336 ^ num343, 7);
    uint num345 = num325 + num13 + num328;
    uint num346 = Bits.RotateRight32(num326 ^ num345, 8);
    uint num347 = num327 + num346;
    uint num348 = Bits.RotateRight32(num328 ^ num347, 7);
    uint num349 = num321 + num2 + num324;
    uint num350 = Bits.RotateRight32(num322 ^ num349, 8);
    uint num351 = num323 + num350;
    uint num352 = Bits.RotateRight32(num324 ^ num351, 7);
    uint num353 = num349 + num3 + num344;
    uint num354 = Bits.RotateRight32(num346 ^ num353, 16 /*0x10*/);
    uint num355 = num339 + num354;
    uint num356 = Bits.RotateRight32(num344 ^ num355, 12);
    uint num357 = num345 + num7 + num352;
    uint num358 = Bits.RotateRight32(num338 ^ num357, 16 /*0x10*/);
    uint num359 = num343 + num358;
    uint num360 = Bits.RotateRight32(num352 ^ num359, 12);
    uint num361 = num337 + num1 + num348;
    uint num362 = Bits.RotateRight32(num342 ^ num361, 16 /*0x10*/);
    uint num363 = num351 + num362;
    uint num364 = Bits.RotateRight32(num348 ^ num363, 12);
    uint num365 = num341 + num9 + num340;
    uint num366 = Bits.RotateRight32(num350 ^ num365, 16 /*0x10*/);
    uint num367 = num347 + num366;
    uint num368 = Bits.RotateRight32(num340 ^ num367, 12);
    uint num369 = num361 + num12 + num364;
    uint num370 = Bits.RotateRight32(num362 ^ num369, 8);
    uint num371 = num363 + num370;
    uint num372 = Bits.RotateRight32(num364 ^ num371, 7);
    uint num373 = num365 + num4 + num368;
    uint num374 = Bits.RotateRight32(num366 ^ num373, 8);
    uint num375 = num367 + num374;
    uint num376 = Bits.RotateRight32(num368 ^ num375, 7);
    uint num377 = num357 + num11 + num360;
    uint num378 = Bits.RotateRight32(num358 ^ num377, 8);
    uint num379 = num359 + num378;
    uint num380 = Bits.RotateRight32(num360 ^ num379, 7);
    uint num381 = num353 + num13 + num356;
    uint num382 = Bits.RotateRight32(num354 ^ num381, 8);
    uint num383 = num355 + num382;
    uint num384 = Bits.RotateRight32(num356 ^ num383, 7);
    uint num385 = num381 + num5 + num380;
    uint num386 = Bits.RotateRight32(num374 ^ num385, 16 /*0x10*/);
    uint num387 = num371 + num386;
    uint num388 = Bits.RotateRight32(num380 ^ num387, 12);
    uint num389 = num377 + num8 + num372;
    uint num390 = Bits.RotateRight32(num382 ^ num389, 16 /*0x10*/);
    uint num391 = num375 + num390;
    uint num392 = Bits.RotateRight32(num372 ^ num391, 12);
    uint num393 = num369 + num16 + num376;
    uint num394 = Bits.RotateRight32(num378 ^ num393, 16 /*0x10*/);
    uint num395 = num383 + num394;
    uint num396 = Bits.RotateRight32(num376 ^ num395, 12);
    uint num397 = num373 + num2 + num384;
    uint num398 = Bits.RotateRight32(num370 ^ num397, 16 /*0x10*/);
    uint num399 = num379 + num398;
    uint num400 = Bits.RotateRight32(num384 ^ num399, 12);
    uint num401 = num393 + num15 + num396;
    uint num402 = Bits.RotateRight32(num394 ^ num401, 8);
    uint num403 = num395 + num402;
    uint num404 = Bits.RotateRight32(num396 ^ num403, 7);
    uint num405 = num397 + num10 + num400;
    uint num406 = Bits.RotateRight32(num398 ^ num405, 8);
    uint num407 = num399 + num406;
    uint num408 = Bits.RotateRight32(num400 ^ num407, 7);
    uint num409 = num389 + num6 + num392;
    uint num410 = Bits.RotateRight32(num390 ^ num409, 8);
    uint num411 = num391 + num410;
    uint num412 = Bits.RotateRight32(num392 ^ num411, 7);
    uint num413 = num385 + num14 + num388;
    uint num414 = Bits.RotateRight32(num386 ^ num413, 8);
    uint num415 = num387 + num414;
    uint num416 = Bits.RotateRight32(num388 ^ num415, 7);
    uint num417 = num413 + num13 + num408;
    uint num418 = Bits.RotateRight32(num410 ^ num417, 16 /*0x10*/);
    uint num419 = num403 + num418;
    uint num420 = Bits.RotateRight32(num408 ^ num419, 12);
    uint num421 = num409 + num2 + num416;
    uint num422 = Bits.RotateRight32(num402 ^ num421, 16 /*0x10*/);
    uint num423 = num407 + num422;
    uint num424 = Bits.RotateRight32(num416 ^ num423, 12);
    uint num425 = num401 + num15 + num412;
    uint num426 = Bits.RotateRight32(num406 ^ num425, 16 /*0x10*/);
    uint num427 = num415 + num426;
    uint num428 = Bits.RotateRight32(num412 ^ num427, 12);
    uint num429 = num405 + num5 + num404;
    uint num430 = Bits.RotateRight32(num414 ^ num429, 16 /*0x10*/);
    uint num431 = num411 + num430;
    uint num432 = Bits.RotateRight32(num404 ^ num431, 12);
    uint num433 = num425 + num14 + num428;
    uint num434 = Bits.RotateRight32(num426 ^ num433, 8);
    uint num435 = num427 + num434;
    uint num436 = Bits.RotateRight32(num428 ^ num435, 7);
    uint num437 = num429 + num11 + num432;
    uint num438 = Bits.RotateRight32(num430 ^ num437, 8);
    uint num439 = num431 + num438;
    uint num440 = Bits.RotateRight32(num432 ^ num439, 7);
    uint num441 = num421 + num16 + num424;
    uint num442 = Bits.RotateRight32(num422 ^ num441, 8);
    uint num443 = num423 + num442;
    uint num444 = Bits.RotateRight32(num424 ^ num443, 7);
    uint num445 = num417 + num6 + num420;
    uint num446 = Bits.RotateRight32(num418 ^ num445, 8);
    uint num447 = num419 + num446;
    uint num448 = Bits.RotateRight32(num420 ^ num447, 7);
    uint num449 = num445 + num1 + num444;
    uint num450 = Bits.RotateRight32(num438 ^ num449, 16 /*0x10*/);
    uint num451 = num435 + num450;
    uint num452 = Bits.RotateRight32(num444 ^ num451, 12);
    uint num453 = num441 + num7 + num436;
    uint num454 = Bits.RotateRight32(num446 ^ num453, 16 /*0x10*/);
    uint num455 = num439 + num454;
    uint num456 = Bits.RotateRight32(num436 ^ num455, 12);
    uint num457 = num433 + num10 + num440;
    uint num458 = Bits.RotateRight32(num442 ^ num457, 16 /*0x10*/);
    uint num459 = num447 + num458;
    uint num460 = Bits.RotateRight32(num440 ^ num459, 12);
    uint num461 = num437 + num9 + num448;
    uint num462 = Bits.RotateRight32(num434 ^ num461, 16 /*0x10*/);
    uint num463 = num443 + num462;
    uint num464 = Bits.RotateRight32(num448 ^ num463, 12);
    uint num465 = num457 + num3 + num460;
    uint num466 = Bits.RotateRight32(num458 ^ num465, 8);
    uint num467 = num459 + num466;
    uint num468 = Bits.RotateRight32(num460 ^ num467, 7);
    uint num469 = num461 + num12 + num464;
    uint num470 = Bits.RotateRight32(num462 ^ num469, 8);
    uint num471 = num463 + num470;
    uint num472 = Bits.RotateRight32(num464 ^ num471, 7);
    uint num473 = num453 + num4 + num456;
    uint num474 = Bits.RotateRight32(num454 ^ num473, 8);
    uint num475 = num455 + num474;
    uint num476 = Bits.RotateRight32(num456 ^ num475, 7);
    uint num477 = num449 + num8 + num452;
    uint num478 = Bits.RotateRight32(num450 ^ num477, 8);
    uint num479 = num451 + num478;
    uint num480 = Bits.RotateRight32(num452 ^ num479, 7);
    uint num481 = num477 + num14 + num472;
    uint num482 = Bits.RotateRight32(num474 ^ num481, 16 /*0x10*/);
    uint num483 = num467 + num482;
    uint num484 = Bits.RotateRight32(num472 ^ num483, 12);
    uint num485 = num473 + num8 + num480;
    uint num486 = Bits.RotateRight32(num466 ^ num485, 16 /*0x10*/);
    uint num487 = num471 + num486;
    uint num488 = Bits.RotateRight32(num480 ^ num487, 12);
    uint num489 = num465 + num13 + num476;
    uint num490 = Bits.RotateRight32(num470 ^ num489, 16 /*0x10*/);
    uint num491 = num479 + num490;
    uint num492 = Bits.RotateRight32(num476 ^ num491, 12);
    uint num493 = num469 + num4 + num468;
    uint num494 = Bits.RotateRight32(num478 ^ num493, 16 /*0x10*/);
    uint num495 = num475 + num494;
    uint num496 = Bits.RotateRight32(num468 ^ num495, 12);
    uint num497 = num489 + num2 + num492;
    uint num498 = Bits.RotateRight32(num490 ^ num497, 8);
    uint num499 = num491 + num498;
    uint num500 = Bits.RotateRight32(num492 ^ num499, 7);
    uint num501 = num493 + num10 + num496;
    uint num502 = Bits.RotateRight32(num494 ^ num501, 8);
    uint num503 = num495 + num502;
    uint num504 = Bits.RotateRight32(num496 ^ num503, 7);
    uint num505 = num485 + num15 + num488;
    uint num506 = Bits.RotateRight32(num486 ^ num505, 8);
    uint num507 = num487 + num506;
    uint num508 = Bits.RotateRight32(num488 ^ num507, 7);
    uint num509 = num481 + num12 + num484;
    uint num510 = Bits.RotateRight32(num482 ^ num509, 8);
    uint num511 = num483 + num510;
    uint num512 = Bits.RotateRight32(num484 ^ num511, 7);
    uint num513 = num509 + num6 + num508;
    uint num514 = Bits.RotateRight32(num502 ^ num513, 16 /*0x10*/);
    uint num515 = num499 + num514;
    uint num516 = Bits.RotateRight32(num508 ^ num515, 12);
    uint num517 = num505 + num16 + num500;
    uint num518 = Bits.RotateRight32(num510 ^ num517, 16 /*0x10*/);
    uint num519 = num503 + num518;
    uint num520 = Bits.RotateRight32(num500 ^ num519, 12);
    uint num521 = num497 + num9 + num504;
    uint num522 = Bits.RotateRight32(num506 ^ num521, 16 /*0x10*/);
    uint num523 = num511 + num522;
    uint num524 = Bits.RotateRight32(num504 ^ num523, 12);
    uint num525 = num501 + num3 + num512;
    uint num526 = Bits.RotateRight32(num498 ^ num525, 16 /*0x10*/);
    uint num527 = num507 + num526;
    uint num528 = Bits.RotateRight32(num512 ^ num527, 12);
    uint num529 = num521 + num7 + num524;
    uint num530 = Bits.RotateRight32(num522 ^ num529, 8);
    uint num531 = num523 + num530;
    uint num532 = Bits.RotateRight32(num524 ^ num531, 7);
    uint num533 = num525 + num11 + num528;
    uint num534 = Bits.RotateRight32(num526 ^ num533, 8);
    uint num535 = num527 + num534;
    uint num536 = Bits.RotateRight32(num528 ^ num535, 7);
    uint num537 = num517 + num5 + num520;
    uint num538 = Bits.RotateRight32(num518 ^ num537, 8);
    uint num539 = num519 + num538;
    uint num540 = Bits.RotateRight32(num520 ^ num539, 7);
    uint num541 = num513 + num1 + num516;
    uint num542 = Bits.RotateRight32(num514 ^ num541, 8);
    uint num543 = num515 + num542;
    uint num544 = Bits.RotateRight32(num516 ^ num543, 7);
    uint num545 = num541 + num7 + num536;
    uint num546 = Bits.RotateRight32(num538 ^ num545, 16 /*0x10*/);
    uint num547 = num531 + num546;
    uint num548 = Bits.RotateRight32(num536 ^ num547, 12);
    uint num549 = num537 + num15 + num544;
    uint num550 = Bits.RotateRight32(num530 ^ num549, 16 /*0x10*/);
    uint num551 = num535 + num550;
    uint num552 = Bits.RotateRight32(num544 ^ num551, 12);
    uint num553 = num529 + num12 + num540;
    uint num554 = Bits.RotateRight32(num534 ^ num553, 16 /*0x10*/);
    uint num555 = num543 + num554;
    uint num556 = Bits.RotateRight32(num540 ^ num555, 12);
    uint num557 = num533 + num1 + num532;
    uint num558 = Bits.RotateRight32(num542 ^ num557, 16 /*0x10*/);
    uint num559 = num539 + num558;
    uint num560 = Bits.RotateRight32(num532 ^ num559, 12);
    uint num561 = num553 + num4 + num556;
    uint num562 = Bits.RotateRight32(num554 ^ num561, 8);
    uint num563 = num555 + num562;
    uint num564 = Bits.RotateRight32(num556 ^ num563, 7);
    uint num565 = num557 + num9 + num560;
    uint num566 = Bits.RotateRight32(num558 ^ num565, 8);
    uint num567 = num559 + num566;
    uint num568 = Bits.RotateRight32(num560 ^ num567, 7);
    uint num569 = num549 + num10 + num552;
    uint num570 = Bits.RotateRight32(num550 ^ num569, 8);
    uint num571 = num551 + num570;
    uint num572 = Bits.RotateRight32(num552 ^ num571, 7);
    uint num573 = num545 + num16 + num548;
    uint num574 = Bits.RotateRight32(num546 ^ num573, 8);
    uint num575 = num547 + num574;
    uint num576 = Bits.RotateRight32(num548 ^ num575, 7);
    uint num577 = num573 + num13 + num572;
    uint num578 = Bits.RotateRight32(num566 ^ num577, 16 /*0x10*/);
    uint num579 = num563 + num578;
    uint num580 = Bits.RotateRight32(num572 ^ num579, 12);
    uint num581 = num569 + num14 + num564;
    uint num582 = Bits.RotateRight32(num574 ^ num581, 16 /*0x10*/);
    uint num583 = num567 + num582;
    uint num584 = Bits.RotateRight32(num564 ^ num583, 12);
    uint num585 = num561 + num2 + num568;
    uint num586 = Bits.RotateRight32(num570 ^ num585, 16 /*0x10*/);
    uint num587 = num575 + num586;
    uint num588 = Bits.RotateRight32(num568 ^ num587, 12);
    uint num589 = num565 + num11 + num576;
    uint num590 = Bits.RotateRight32(num562 ^ num589, 16 /*0x10*/);
    uint num591 = num571 + num590;
    uint num592 = Bits.RotateRight32(num576 ^ num591, 12);
    uint num593 = num585 + num5 + num588;
    uint num594 = Bits.RotateRight32(num586 ^ num593, 8);
    uint num595 = num587 + num594;
    uint num596 = Bits.RotateRight32(num588 ^ num595, 7);
    uint num597 = num589 + num6 + num592;
    uint num598 = Bits.RotateRight32(num590 ^ num597, 8);
    uint num599 = num591 + num598;
    uint num600 = Bits.RotateRight32(num592 ^ num599, 7);
    uint num601 = num581 + num8 + num584;
    uint num602 = Bits.RotateRight32(num582 ^ num601, 8);
    uint num603 = num583 + num602;
    uint num604 = Bits.RotateRight32(num584 ^ num603, 7);
    uint num605 = num577 + num3 + num580;
    uint num606 = Bits.RotateRight32(num578 ^ num605, 8);
    uint num607 = num579 + num606;
    uint num608 = Bits.RotateRight32(num580 ^ num607, 7);
    uint num609 = num605 + num11 + num600;
    uint num610 = Bits.RotateRight32(num602 ^ num609, 16 /*0x10*/);
    uint num611 = num595 + num610;
    uint num612 = Bits.RotateRight32(num600 ^ num611, 12);
    uint num613 = num601 + num9 + num608;
    uint num614 = Bits.RotateRight32(num594 ^ num613, 16 /*0x10*/);
    uint num615 = num599 + num614;
    uint num616 = Bits.RotateRight32(num608 ^ num615, 12);
    uint num617 = num593 + num8 + num604;
    uint num618 = Bits.RotateRight32(num598 ^ num617, 16 /*0x10*/);
    uint num619 = num607 + num618;
    uint num620 = Bits.RotateRight32(num604 ^ num619, 12);
    uint num621 = num597 + num2 + num596;
    uint num622 = Bits.RotateRight32(num606 ^ num621, 16 /*0x10*/);
    uint num623 = num603 + num622;
    uint num624 = Bits.RotateRight32(num596 ^ num623, 12);
    uint num625 = num617 + num7 + num620;
    uint num626 = Bits.RotateRight32(num618 ^ num625, 8);
    uint num627 = num619 + num626;
    uint num628 = Bits.RotateRight32(num620 ^ num627, 7);
    uint num629 = num621 + num6 + num624;
    uint num630 = Bits.RotateRight32(num622 ^ num629, 8);
    uint num631 = num623 + num630;
    uint num632 = Bits.RotateRight32(num624 ^ num631, 7);
    uint num633 = num613 + num5 + num616;
    uint num634 = Bits.RotateRight32(num614 ^ num633, 8);
    uint num635 = num615 + num634;
    uint num636 = Bits.RotateRight32(num616 ^ num635, 7);
    uint num637 = num609 + num3 + num612;
    uint num638 = Bits.RotateRight32(num610 ^ num637, 8);
    uint num639 = num611 + num638;
    uint num640 = Bits.RotateRight32(num612 ^ num639, 7);
    uint num641 = num637 + num16 + num636;
    uint num642 = Bits.RotateRight32(num630 ^ num641, 16 /*0x10*/);
    uint num643 = num627 + num642;
    uint num644 = Bits.RotateRight32(num636 ^ num643, 12);
    uint num645 = num633 + num10 + num628;
    uint num646 = Bits.RotateRight32(num638 ^ num645, 16 /*0x10*/);
    uint num647 = num631 + num646;
    uint num648 = Bits.RotateRight32(num628 ^ num647, 12);
    uint num649 = num625 + num4 + num632;
    uint num650 = Bits.RotateRight32(num634 ^ num649, 16 /*0x10*/);
    uint num651 = num639 + num650;
    uint num652 = Bits.RotateRight32(num632 ^ num651, 12);
    uint num653 = num629 + num14 + num640;
    uint num654 = Bits.RotateRight32(num626 ^ num653, 16 /*0x10*/);
    uint num655 = num635 + num654;
    uint num656 = Bits.RotateRight32(num640 ^ num655, 12);
    uint num657 = num649 + num13 + num652;
    uint num658 = Bits.RotateRight32(num650 ^ num657, 8);
    uint num659 = num651 + num658;
    uint num660 = Bits.RotateRight32(num652 ^ num659, 7);
    uint num661 = num653 + num1 + num656;
    uint num662 = Bits.RotateRight32(num654 ^ num661, 8);
    uint num663 = num655 + num662;
    uint num664 = Bits.RotateRight32(num656 ^ num663, 7);
    uint num665 = num645 + num15 + num648;
    uint num666 = Bits.RotateRight32(num646 ^ num665, 8);
    uint num667 = num647 + num666;
    uint num668 = Bits.RotateRight32(num648 ^ num667, 7);
    uint num669 = num641 + num12 + num644;
    uint num670 = Bits.RotateRight32(num642 ^ num669, 8);
    uint num671 = num643 + num670;
    uint num672 = Bits.RotateRight32(num644 ^ num671, 7);
    this.State[0] = this.State[0] ^ num669 ^ num659;
    this.State[1] = this.State[1] ^ num665 ^ num663;
    this.State[2] = this.State[2] ^ num657 ^ num671;
    this.State[3] = this.State[3] ^ num661 ^ num667;
    this.State[4] = this.State[4] ^ num664 ^ num666;
    this.State[5] = this.State[5] ^ num672 ^ num658;
    this.State[6] = this.State[6] ^ num668 ^ num662;
    this.State[7] = this.State[7] ^ num660 ^ num670;
  }

  private unsafe void Compress(ref byte[] block, int start)
  {
    fixed (uint* dest = this.M)
      fixed (byte* src = block)
        Converters.le32_copy((IntPtr) (void*) src, start, (IntPtr) (void*) dest, 0, this.BlockSize);
    this.MixScalar();
  }

  protected void Finish()
  {
    this.Blake2SIncrementCounter((uint) this.FilledBufferCount);
    this.FinalizationFlag0 = uint.MaxValue;
    if (this.TreeConfig != null && this.TreeConfig.IsLastNode)
      this.FinalizationFlag1 = uint.MaxValue;
    int num = this.Buffer.Length - this.FilledBufferCount;
    if (num > 0)
      ArrayUtils.Fill(ref this.Buffer, this.FilledBufferCount, num + this.FilledBufferCount, (byte) 0);
    this.Compress(ref this.Buffer, 0);
  }
}

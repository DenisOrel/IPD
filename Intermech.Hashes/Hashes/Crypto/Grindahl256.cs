// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Grindahl256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Grindahl256 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private uint[] state;
  private uint[] temp;
  private static uint[] table_0 = new uint[256 /*0x0100*/];
  private static uint[] table_1 = new uint[256 /*0x0100*/];
  private static uint[] table_2 = new uint[256 /*0x0100*/];
  private static uint[] table_3 = new uint[256 /*0x0100*/];
  private static readonly uint[] master_table = new uint[256 /*0x0100*/]
  {
    3328402341U,
    4168907908U,
    4000806809U,
    4135287693U,
    4294111757U,
    3597364157U,
    3731845041U,
    2445657428U,
    1613770832U,
    33620227U,
    3462883241U,
    1445669757U,
    3892248089U,
    3050821474U,
    1303096294U,
    3967186586U,
    2412431941U,
    528646813U,
    2311702848U,
    4202528135U,
    4026202645U,
    2992200171U,
    2387036105U,
    4226871307U,
    1101901292U,
    3017069671U,
    1604494077U,
    1169141738U,
    597466303U,
    1403299063U,
    3832705686U,
    2613100635U,
    1974974402U,
    3791519004U,
    1033081774U,
    1277568618U,
    1815492186U,
    2118074177U,
    4126668546U,
    2211236943U,
    1748251740U,
    1369810420U,
    3521504564U,
    4193382664U,
    3799085459U,
    2883115123U,
    1647391059U,
    706024767U,
    134480908U,
    2512897874U,
    1176707941U,
    2646852446U,
    806885416U,
    932615841U,
    168101135U,
    798661301U,
    235341577U,
    605164086U,
    461406363U,
    3756188221U,
    3454790438U,
    1311188841U,
    2142417613U,
    3933566367U,
    302582043U,
    495158174U,
    1479289972U,
    874125870U,
    907746093U,
    3698224818U,
    3025820398U,
    1537253627U,
    2756858614U,
    1983593293U,
    3084310113U,
    2108928974U,
    1378429307U,
    3722699582U,
    1580150641U,
    327451799U,
    2790478837U,
    3117535592U,
    0U,
    3253595436U,
    1075847264U,
    3825007647U,
    2041688520U,
    3059440621U,
    3563743934U,
    2378943302U,
    1740553945U,
    1916352843U,
    2487896798U,
    2555137236U,
    2958579944U,
    2244988746U,
    3151024235U,
    3320835882U,
    1336584933U,
    3992714006U,
    2252555205U,
    2588757463U,
    1714631509U,
    293963156U,
    2319795663U,
    3925473552U,
    67240454U,
    4269768577U,
    2689618160U,
    2017213508U,
    631218106U,
    1269344483U,
    2723238387U,
    1571005438U,
    2151694528U,
    93294474U,
    1066570413U,
    563977660U,
    1882732616U,
    4059428100U,
    1673313503U,
    2008463041U,
    2950355573U,
    1109467491U,
    537923632U,
    3858759450U,
    4260623118U,
    3218264685U,
    2177748300U,
    403442708U,
    638784309U,
    3287084079U,
    3193921505U,
    899127202U,
    2286175436U,
    773265209U,
    2479146071U,
    1437050866U,
    4236148354U,
    2050833735U,
    3362022572U,
    3126681063U,
    840505643U,
    3866325909U,
    3227541664U,
    427917720U,
    2655997905U,
    2749160575U,
    1143087718U,
    1412049534U,
    999329963U,
    193497219U,
    2353415882U,
    3354324521U,
    1807268051U,
    672404540U,
    2816401017U,
    3160301282U,
    369822493U,
    2916866934U,
    3688947771U,
    1681011286U,
    1949973070U,
    336202270U,
    2454276571U,
    201721354U,
    1210328172U,
    3093060836U,
    2680341085U,
    3184776046U,
    1135389935U,
    3294782118U,
    965841320U,
    831886756U,
    3554993207U,
    4068047243U,
    3588745010U,
    2345191491U,
    1849112409U,
    3664604599U,
    26054028U,
    2983581028U,
    2622377682U,
    1235855840U,
    3630984372U,
    2891339514U,
    4092916743U,
    3488279077U,
    3395642799U,
    4101667470U,
    1202630377U,
    268961816U,
    1874508501U,
    4034427016U,
    1243948399U,
    1546530418U,
    941366308U,
    1470539505U,
    1941222599U,
    2546386513U,
    3421038627U,
    2715671932U,
    3899946140U,
    1042226977U,
    2521517021U,
    1639824860U,
    227249030U,
    260737669U,
    3765465232U,
    2084453954U,
    1907733956U,
    3429263018U,
    2420656344U,
    100860677U,
    4160157185U,
    470683154U,
    3261161891U,
    1781871967U,
    2924959737U,
    1773779408U,
    394692241U,
    2579611992U,
    974986535U,
    664706745U,
    3655459128U,
    3958962195U,
    731420851U,
    571543859U,
    3530123707U,
    2849626480U,
    126783113U,
    865375399U,
    765172662U,
    1008606754U,
    361203602U,
    3387549984U,
    2278477385U,
    2857719295U,
    1344809080U,
    2782912378U,
    59542671U,
    1503764984U,
    160008576U,
    437062935U,
    1707065306U,
    3622233649U,
    2218934982U,
    3496503480U,
    2185314755U,
    697932208U,
    1512910199U,
    504303377U,
    2075177163U,
    2824099068U,
    1841019862U,
    739644986U
  };

  static unsafe Grindahl256()
  {
    fixed (uint* dest = Grindahl256.table_0)
      fixed (uint* result1 = Grindahl256.table_1)
        fixed (uint* result2 = Grindahl256.table_2)
          fixed (uint* result3 = Grindahl256.table_3)
            fixed (uint* src = Grindahl256.master_table)
            {
              Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) src, 1024 /*0x0400*/);
              Grindahl256.CalcTable(1, result1);
              Grindahl256.CalcTable(2, result2);
              Grindahl256.CalcTable(3, result3);
            }
  }

  public Grindahl256()
    : base(32 /*0x20*/, 4)
  {
    this.state = new uint[13];
    this.temp = new uint[13];
  }

  public override IHash Clone()
  {
    Grindahl256 grindahl256 = new Grindahl256();
    grindahl256.buffer = this.buffer.Clone();
    grindahl256.processed_bytes = this.processed_bytes;
    grindahl256.state = this.state.DeepCopy();
    grindahl256.temp = this.temp.DeepCopy();
    grindahl256.BufferSize = this.BufferSize;
    return (IHash) grindahl256;
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.state);
    ArrayUtils.ZeroFill(ref this.temp);
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[32 /*0x20*/];
    fixed (uint* src = &this.state[5])
      fixed (byte* dest = result)
        Converters.be32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override unsafe void Finish()
  {
    int length = 12 - (int) ((long) this.processed_bytes & 3L);
    long x = (long) (this.processed_bytes >> 2) + 1L;
    byte[] a_out = new byte[length];
    a_out[0] = (byte) 128 /*0x80*/;
    Converters.ReadUInt64AsBytesLE(Converters.be2me_64((ulong) x), ref a_out, length - 8);
    this.TransformBytes(a_out, 0, length - 4);
    fixed (byte* a_in = a_out)
    {
      this.state[0] = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, length - 4);
      this.state[0] = Converters.be2me_32(this.state[0]);
    }
    this.InjectMsg(true);
    for (uint index = 0; index < 8U; ++index)
      this.InjectMsg(true);
  }

  protected override void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    this.state[0] = Converters.ReadBytesAsUInt32LE(a_data, a_index);
    this.state[0] = Converters.be2me_32(this.state[0]);
    this.InjectMsg(false);
  }

  private static unsafe void CalcTable(int i, uint* result)
  {
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      result[index] = Grindahl256.master_table[index] >> i * 8 | Grindahl256.master_table[index] << 32 /*0x20*/ - i * 8;
  }

  private void InjectMsg(bool a_full_process)
  {
    this.state[12] = this.state[12] ^ 1U;
    if (a_full_process)
      this.temp[0] = Grindahl256.table_0[(int) (byte) (this.state[12] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[11] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[9] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[3]];
    this.temp[1] = Grindahl256.table_0[(int) (byte) (this.state[0] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[12] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[10] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[4]];
    this.temp[2] = Grindahl256.table_0[(int) (byte) (this.state[1] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[0] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[11] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[5]];
    this.temp[3] = Grindahl256.table_0[(int) (byte) (this.state[2] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[1] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[12] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[6]];
    this.temp[4] = Grindahl256.table_0[(int) (byte) (this.state[3] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[2] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[0] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[7]];
    this.temp[5] = Grindahl256.table_0[(int) (byte) (this.state[4] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[3] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[1] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[8]];
    this.temp[6] = Grindahl256.table_0[(int) (byte) (this.state[5] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[4] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[2] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[9]];
    this.temp[7] = Grindahl256.table_0[(int) (byte) (this.state[6] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[5] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[3] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[10]];
    this.temp[8] = Grindahl256.table_0[(int) (byte) (this.state[7] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[6] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[4] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[11]];
    this.temp[9] = Grindahl256.table_0[(int) (byte) (this.state[8] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[7] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[5] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[12]];
    this.temp[10] = Grindahl256.table_0[(int) (byte) (this.state[9] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[8] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[6] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[0]];
    this.temp[11] = Grindahl256.table_0[(int) (byte) (this.state[10] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[9] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[7] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[1]];
    this.temp[12] = Grindahl256.table_0[(int) (byte) (this.state[11] >> 24)] ^ Grindahl256.table_1[(int) (byte) (this.state[10] >> 16 /*0x10*/)] ^ Grindahl256.table_2[(int) (byte) (this.state[8] >> 8)] ^ Grindahl256.table_3[(int) (byte) this.state[2]];
    uint[] temp = this.temp;
    this.temp = this.state;
    this.state = temp;
  }
}

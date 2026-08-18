// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Grindahl512
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Grindahl512 : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash, ITransformBlock
{
  private ulong[] state;
  private ulong[] temp;
  private static ulong[] table_0 = new ulong[256 /*0x0100*/];
  private static ulong[] table_1 = new ulong[256 /*0x0100*/];
  private static ulong[] table_2 = new ulong[256 /*0x0100*/];
  private static ulong[] table_3 = new ulong[256 /*0x0100*/];
  private static ulong[] table_4 = new ulong[256 /*0x0100*/];
  private static ulong[] table_5 = new ulong[256 /*0x0100*/];
  private static ulong[] table_6 = new ulong[256 /*0x0100*/];
  private static ulong[] table_7 = new ulong[256 /*0x0100*/];
  private static readonly ulong[] master_table = new ulong[256 /*0x0100*/]
  {
    14295379144059736482UL,
    17905323569371222822UL,
    17183334601843878226UL,
    17760925858332475650UL,
    18443070493470825012UL,
    15450561382150660546UL,
    16028152363752978930UL,
    10504018557827983435UL,
    6931093428530552923UL,
    144397779761366540UL,
    14872970125662046610UL,
    6209104529720605423UL,
    16717079061900374628UL,
    13103178524810986643UL,
    5596755642723325877UL,
    17038936822083563358UL,
    10361316018932582927UL,
    2270520367136907586UL,
    9928687897380290587UL,
    18049721349131539754UL,
    17292409447461759572UL,
    12851401415184010625UL,
    10252241173314701577UL,
    18154275002667570732UL,
    4732629560198153117UL,
    12958215664760966791UL,
    6891249319482483161UL,
    5021425119718788997UL,
    2566097714782430666UL,
    6027123236957320689UL,
    16461345840481253230UL,
    11223181574140507767UL,
    8482450641909686053UL,
    16284450940347037808UL,
    4437052212567623566UL,
    5487115630914819251UL,
    7797480107096650867UL,
    9097060124947874335UL,
    17723907477159347208UL,
    9497190142570044967UL,
    7508684547576019051UL,
    5883290606207235069UL,
    15124747510174279888UL,
    18010442371917484064UL,
    16316948129438317946UL,
    12382885004313290455UL,
    7075491208290870871UL,
    3032353374988697340UL,
    577591119045466160UL,
    10792814048629138515UL,
    5053922291629673103UL,
    11368144434190531683UL,
    3465546714272780448UL,
    4005554320314227122UL,
    721988898806832700UL,
    3430223797309698530UL,
    1010784458329559588UL,
    2599160035704597720UL,
    1981724807614180698UL,
    16132706017289009908UL,
    14838212340529843352UL,
    5631513410675135167UL,
    9201613795664298265UL,
    16894539111040623946UL,
    1299580017852298860UL,
    2126687736385775438UL,
    6353502309485111499UL,
    3754342273795513528UL,
    3898740053556878004UL,
    15883754583992664062UL,
    12995799126223800213UL,
    6602453759961853377UL,
    11840616888132892661UL,
    8519469005902420527UL,
    13247011155562119839UL,
    9057781164914214677UL,
    5920308970199971575UL,
    15988873386538925304UL,
    6786695648766059231UL,
    1406394284609647978UL,
    11985014667894256121UL,
    13389713419571220667UL,
    0UL,
    13974086464165206192UL,
    4620728952348721307UL,
    16428283571097124476UL,
    8768985605393582861UL,
    13140196905985163673UL,
    15306163602390345678UL,
    10217483388181449731UL,
    7475622226650704201UL,
    8230673446381786679UL,
    10685434650041952085UL,
    10974230140843111293UL,
    12707003635422645133UL,
    9642153002620064819UL,
    13533546050322355895UL,
    14262881954968458408UL,
    5740588273473409465UL,
    17148576816711674968UL,
    9674650191712391481UL,
    11118627920604476785UL,
    7364286767811504719UL,
    1262561653858513766UL,
    9963445682513544465UL,
    16859781325908418624UL,
    288795559522733080UL,
    18338516839934794034UL,
    11551821397331737581UL,
    8663866785666926603UL,
    2711060643554029534UL,
    5451792713952779681UL,
    11696219177093103073UL,
    6747416688732399573UL,
    9241456921149872941UL,
    400696167372164894UL,
    4580884843318755714UL,
    2422265084031296454UL,
    8086275666621468731UL,
    17435111986356094992UL,
    7186826667130070353UL,
    8626283272659769641UL,
    12671680495114447567UL,
    4765126732109039255UL,
    2310364476181864640UL,
    16573246431150290024UL,
    18299237862720740408UL,
    13822341541123513007UL,
    9353357511818909739UL,
    1732773357136390224UL,
    2743557815465962196UL,
    14117919094915292860UL,
    13717787887587482025UL,
    3861721689563094974UL,
    9819047902752178973UL,
    3321148934511424228UL,
    10647851188579118663UL,
    6172086165727870949UL,
    18194119060174479166UL,
    8808264565427244551UL,
    14439776855098475398UL,
    13428992396786324913UL,
    3609944494034146988UL,
    16605743620241568098UL,
    13862185873496169398UL,
    1837892176863046486UL,
    11407423411405633897UL,
    11807554618751909607UL,
    4909524511869357187UL,
    6064706749960289507UL,
    4292089283796028826UL,
    831063761605107002UL,
    10107843393553338117UL,
    14406714585718543012UL,
    7762157190134611297UL,
    2887955595227330800UL,
    12096350109553062655UL,
    13573390107826118565UL,
    1588375577375029876UL,
    12527847864363314371UL,
    15843910526485755628UL,
    7219888988051188803UL,
    8375071226142104611UL,
    1443977797613665400UL,
    10541036939002162497UL,
    866386678568195112UL,
    5198320071394187435UL,
    13284594617024959421UL,
    11511977064941664879UL,
    13678508910372379811UL,
    4876462190948238737UL,
    14150981364299421614UL,
    4148256653044894614UL,
    3572926130040361894UL,
    15268580140924366556UL,
    17472130367529225498UL,
    15413543000977532104UL,
    10072520528131425815UL,
    7941877886856966783UL,
    15739356872949724650UL,
    111900607849431814UL,
    12814383034009831563UL,
    11263025631644270437UL,
    5307960083202694061UL,
    15594959093189407718UL,
    12418207869735202757UL,
    17578944617106181660UL,
    14982044971279927956UL,
    14584174634858792330UL,
    17616528078572160782UL,
    5165257750468872585UL,
    1155182238090932320UL,
    8050952749655241081UL,
    17327732587768908566UL,
    5342717851154505383UL,
    6642297869005743315UL,
    4043137833318238352UL,
    6315918796477954537UL,
    8337487713139135793UL,
    10936646679380271711UL,
    14693249480476673676UL,
    11663721988000774379UL,
    16750141331280307014UL,
    4476331172602331780UL,
    10829832429803315545UL,
    7042994036379984733UL,
    976026690376701742UL,
    1119859321127833890UL,
    16172550349678001014UL,
    8952662345187558419UL,
    8193655082389050173UL,
    14728572345901731742UL,
    10396639159240797005UL,
    433193339284097556UL,
    17867740107909431812UL,
    2021568916659119176UL,
    14006583653256486330UL,
    7653082327336336999UL,
    12562605649496566217UL,
    7618324559384525677UL,
    1695189844132378994UL,
    11079348943389372539UL,
    4187535613079604892UL,
    2854893274305161682UL,
    15700077895735668960UL,
    17003613956658505292UL,
    3141428237786971642UL,
    2454762255943231180UL,
    15161765891347410394UL,
    12239052373562155227UL,
    544528798123297042UL,
    3716758760791496106UL,
    3286391166558566382UL,
    4331933392840967304UL,
    1551357213381246846UL,
    14549416849726587008UL,
    9785985633371198015UL,
    12273810158695409105UL,
    5775911190439653627UL,
    11952517478801929459UL,
    255733238600566026UL,
    6458621129211767757UL,
    687231130853972790UL,
    1877171136897756764UL,
    7331789595900620613UL,
    15557375631727616708UL,
    9530252411951028021UL,
    15017368111587093462UL,
    9385854700911238433UL,
    2997595607035837430UL,
    6497900089245429447UL,
    2165966696420483652UL,
    8912818236143668481UL,
    12129412378934043613UL,
    7907120118905157493UL,
    3176751154750059752UL
  };

  static unsafe Grindahl512()
  {
    fixed (ulong* dest = Grindahl512.table_0)
      fixed (ulong* result1 = Grindahl512.table_1)
        fixed (ulong* result2 = Grindahl512.table_2)
          fixed (ulong* result3 = Grindahl512.table_3)
            fixed (ulong* result4 = Grindahl512.table_4)
              fixed (ulong* result5 = Grindahl512.table_5)
                fixed (ulong* result6 = Grindahl512.table_6)
                  fixed (ulong* result7 = Grindahl512.table_7)
                    fixed (ulong* src = Grindahl512.master_table)
                    {
                      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) src, 2048 /*0x0800*/);
                      Grindahl512.CalcTable(1, result1);
                      Grindahl512.CalcTable(2, result2);
                      Grindahl512.CalcTable(3, result3);
                      Grindahl512.CalcTable(4, result4);
                      Grindahl512.CalcTable(5, result5);
                      Grindahl512.CalcTable(6, result6);
                      Grindahl512.CalcTable(7, result7);
                    }
  }

  public Grindahl512()
    : base(64 /*0x40*/, 8)
  {
    this.state = new ulong[13];
    this.temp = new ulong[13];
  }

  public override IHash Clone()
  {
    Grindahl512 grindahl512 = new Grindahl512();
    grindahl512.buffer = this.buffer.Clone();
    grindahl512.processed_bytes = this.processed_bytes;
    grindahl512.state = this.state.DeepCopy();
    grindahl512.temp = this.temp.DeepCopy();
    grindahl512.BufferSize = this.BufferSize;
    return (IHash) grindahl512;
  }

  public override void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.state);
    ArrayUtils.ZeroFill(ref this.temp);
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[64 /*0x40*/];
    fixed (ulong* src = &this.state[5])
      fixed (byte* dest = result)
        Converters.be64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  protected override unsafe void Finish()
  {
    int length = 16 /*0x10*/ - (int) ((long) this.processed_bytes & 7L);
    long x = (long) (this.processed_bytes >> 3) + 1L;
    byte[] a_out = new byte[length];
    a_out[0] = (byte) 128 /*0x80*/;
    Converters.ReadUInt64AsBytesLE(Converters.be2me_64((ulong) x), ref a_out, length - 8);
    this.TransformBytes(a_out, 0, length - 8);
    fixed (byte* a_in = a_out)
    {
      this.state[0] = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, length - 8);
      this.state[0] = Converters.be2me_64(this.state[0]);
    }
    this.InjectMsg(true);
    for (uint index = 0; index < 8U; ++index)
      this.InjectMsg(true);
  }

  protected override void TransformBlock(IntPtr a_data, int a_data_length, int a_index)
  {
    this.state[0] = Converters.ReadBytesAsUInt64LE(a_data, a_index);
    this.state[0] = Converters.be2me_64(this.state[0]);
    this.InjectMsg(false);
  }

  private static unsafe void CalcTable(int i, ulong* result)
  {
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      result[index] = Bits.RotateRight64(Grindahl512.master_table[index], i * 8);
  }

  private void InjectMsg(bool a_full_process)
  {
    this.state[12] = this.state[12] ^ 1UL;
    if (a_full_process)
      this.temp[0] = Grindahl512.table_0[(int) (byte) (this.state[12] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[11] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[10] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[9] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[8] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[7] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[6] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[5]];
    this.temp[1] = Grindahl512.table_0[(int) (byte) (this.state[0] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[12] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[11] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[10] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[9] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[8] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[7] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[6]];
    this.temp[2] = Grindahl512.table_0[(int) (byte) (this.state[1] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[0] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[12] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[11] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[10] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[9] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[8] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[7]];
    this.temp[3] = Grindahl512.table_0[(int) (byte) (this.state[2] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[1] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[0] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[12] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[11] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[10] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[9] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[8]];
    this.temp[4] = Grindahl512.table_0[(int) (byte) (this.state[3] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[2] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[1] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[0] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[12] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[11] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[10] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[9]];
    this.temp[5] = Grindahl512.table_0[(int) (byte) (this.state[4] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[3] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[2] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[1] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[0] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[12] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[11] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[10]];
    this.temp[6] = Grindahl512.table_0[(int) (byte) (this.state[5] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[4] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[3] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[2] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[1] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[0] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[12] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[11]];
    this.temp[7] = Grindahl512.table_0[(int) (byte) (this.state[6] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[5] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[4] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[3] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[2] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[1] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[0] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[12]];
    this.temp[8] = Grindahl512.table_0[(int) (byte) (this.state[7] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[6] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[5] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[4] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[3] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[2] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[1] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[0]];
    this.temp[9] = Grindahl512.table_0[(int) (byte) (this.state[8] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[7] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[6] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[5] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[4] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[3] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[2] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[1]];
    this.temp[10] = Grindahl512.table_0[(int) (byte) (this.state[9] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[8] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[7] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[6] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[5] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[4] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[3] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[2]];
    this.temp[11] = Grindahl512.table_0[(int) (byte) (this.state[10] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[9] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[8] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[7] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[6] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[5] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[4] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[3]];
    this.temp[12] = Grindahl512.table_0[(int) (byte) (this.state[11] >> 56)] ^ Grindahl512.table_1[(int) (byte) (this.state[10] >> 48 /*0x30*/)] ^ Grindahl512.table_2[(int) (byte) (this.state[9] >> 40)] ^ Grindahl512.table_3[(int) (byte) (this.state[8] >> 32 /*0x20*/)] ^ Grindahl512.table_4[(int) (byte) (this.state[7] >> 24)] ^ Grindahl512.table_5[(int) (byte) (this.state[6] >> 16 /*0x10*/)] ^ Grindahl512.table_6[(int) (byte) (this.state[5] >> 8)] ^ Grindahl512.table_7[(int) (byte) this.state[4]];
    ulong[] temp = this.temp;
    this.temp = this.state;
    this.state = temp;
  }
}

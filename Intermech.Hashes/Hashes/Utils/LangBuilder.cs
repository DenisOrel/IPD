// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Utils.LangBuilder
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Checksum;
using Intermech.Hashes.Crypto;
using Intermech.Hashes.Crypto.Blake2BConfigurations;
using Intermech.Hashes.Crypto.Blake2SConfigurations;
using Intermech.Hashes.Hash128;
using Intermech.Hashes.Hash32;
using Intermech.Hashes.Hash64;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2BConfigurations;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;

#nullable disable
namespace Intermech.Hashes.Utils;

internal static class LangBuilder
{
  public static IHash Reducer(string hash_string) => LangBuilder.Core(hash_string);

  private static string Strip(string hash_string)
  {
    return hash_string.Trim().Replace(" ", "").Replace("_", "").Replace("-", "").ToLower();
  }

  private static IHash Core(string hash_string)
  {
    switch (LangBuilder.Strip(hash_string))
    {
      case "Grindahl":
      case "Grindahl256":
        return (IHash) new Grindahl256();
      case "Snefru":
      case "Snefru8128":
        return (IHash) new Snefru(8, 128 /*0x80*/);
      case "Snefru8256":
        return (IHash) new Snefru(8, 256 /*0x0100*/);
      case "adler":
      case "adler32":
        return (IHash) new Adler32();
      case "ap":
        return (IHash) new AP();
      case "bernstein":
        return (IHash) new Bernstein();
      case "bernstein1":
        return (IHash) new Bernstein1();
      case "bkdr":
        return (IHash) new BKDR();
      case "blake2b":
        return (IHash) new Blake2B((IBlake2BConfig) new Blake2BConfig(), (IBlake2BTreeConfig) null);
      case "blake2b160":
        return (IHash) new Blake2B((IBlake2BConfig) new Blake2BConfig(HashSizeEnum.HashSize160), (IBlake2BTreeConfig) null);
      case "blake2b256":
        return (IHash) new Blake2B((IBlake2BConfig) new Blake2BConfig(HashSizeEnum.HashSize256), (IBlake2BTreeConfig) null);
      case "blake2b384":
        return (IHash) new Blake2B((IBlake2BConfig) new Blake2BConfig(HashSizeEnum.HashSize384), (IBlake2BTreeConfig) null);
      case "blake2b512":
        return (IHash) new Blake2B((IBlake2BConfig) new Blake2BConfig(), (IBlake2BTreeConfig) null);
      case "blake2s":
        return (IHash) new Blake2S((IBlake2SConfig) new Blake2SConfig(), (IBlake2STreeConfig) null);
      case "blake2s128":
        return (IHash) new Blake2S((IBlake2SConfig) new Blake2SConfig(HashSizeEnum.HashSize128), (IBlake2STreeConfig) null);
      case "blake2s160":
        return (IHash) new Blake2S((IBlake2SConfig) new Blake2SConfig(HashSizeEnum.HashSize160), (IBlake2STreeConfig) null);
      case "blake2s224":
        return (IHash) new Blake2S((IBlake2SConfig) new Blake2SConfig(HashSizeEnum.HashSize224), (IBlake2STreeConfig) null);
      case "blake2s256":
        return (IHash) new Blake2S((IBlake2SConfig) new Blake2SConfig(), (IBlake2STreeConfig) null);
      case "blake3":
        return (IHash) Blake3.CreateBlake3();
      case "blake3256":
        return (IHash) Blake3.CreateBlake3();
      case "crc16":
      case "crc16buypass":
      case "crcbuypass":
        return (IHash) new CRC16_BUYPASS();
      case "crc32":
      case "crc32pkzip":
      case "crcpkzip":
        return (IHash) new CRC32_PKZIP();
      case "crc32castagnoli":
      case "crccastagnoli":
        return (IHash) new CRC32_CASTAGNOLI();
      case "crc64":
      case "crc64ecma":
      case "crc64ecma182":
      case "crcecma":
        return (IHash) new CRC64_ECMA_182();
      case "dek":
        return (IHash) new DEK();
      case "djb":
        return (IHash) new DJB();
      case "elf":
        return (IHash) new ELF();
      case "fnv":
        return (IHash) new FNV();
      case "fnv1a":
        return (IHash) new FNV1a();
      case "fnv1a64":
        return (IHash) new FNV1a64();
      case "fnv64":
        return (IHash) new FNV64();
      case "gost":
        return (IHash) new Gost();
      case "gost2012":
      case "gost2012256":
      case "gost256":
      case "gost2562012":
      case "gost34112012256":
        return (IHash) new GOST3411_2012_256();
      case "gost2012512":
      case "gost34112012512":
      case "gost512":
      case "gost5122012":
        return (IHash) new GOST3411_2012_512();
      case "grindahl512":
        return (IHash) new Grindahl512();
      case "has160":
        return (IHash) new HAS160();
      case "haval3128":
        return (IHash) new Haval_3_128();
      case "haval3160":
        return (IHash) new Haval_3_160();
      case "haval3192":
        return (IHash) new Haval_3_192();
      case "haval3224":
        return (IHash) new Haval_3_224();
      case "haval3256":
        return (IHash) new Haval_3_256();
      case "haval4128":
        return (IHash) new Haval_4_128();
      case "haval4160":
        return (IHash) new Haval_4_160();
      case "haval4192":
        return (IHash) new Haval_4_192();
      case "haval4224":
        return (IHash) new Haval_4_224();
      case "haval4256":
        return (IHash) new Haval_4_256();
      case "haval5128":
        return (IHash) new Haval_5_128();
      case "haval5160":
        return (IHash) new Haval_5_160();
      case "haval5192":
        return (IHash) new Haval_5_192();
      case "haval5224":
        return (IHash) new Haval_5_224();
      case "haval5256":
        return (IHash) new Haval_5_256();
      case "jenkins3":
        return (IHash) new Jenkins3();
      case "js":
        return (IHash) new JS();
      case "keccak224":
        return (IHash) new Keccak_224();
      case "keccak256":
        return (IHash) new Keccak_256();
      case "keccak288":
        return (IHash) new Keccak_288();
      case "keccak384":
        return (IHash) new Keccak_384();
      case "keccak512":
        return (IHash) new Keccak_512();
      case "md2":
        return (IHash) new MD2();
      case "md4":
        return (IHash) new MD4();
      case "md5":
        return (IHash) new MD5();
      case "murmur2":
        return (IHash) new Murmur2();
      case "murmur264":
        return (IHash) new Murmur2_64();
      case "murmurhash3128":
      case "murmurhash3128x86":
      case "murmurhash3x86128":
        return (IHash) new MurmurHash3_x86_128();
      case "murmurhash3128x64":
      case "murmurhash3x64128":
        return (IHash) new MurmurHash3_x64_128();
      case "murmurhash332":
      case "murmurhash332x86":
      case "murmurhash3x8632":
        return (IHash) new MurmurHash3_x86_32();
      case "nulldigest":
        return (IHash) new NullDigest();
      case "oneattime":
        return (IHash) new OneAtTime();
      case "panama":
        return (IHash) new Panama();
      case "pjw":
        return (IHash) new PJW();
      case "radiogatun":
      case "radiogatun32":
        return (IHash) new RadioGatun32();
      case "radiogatun64":
        return (IHash) new RadioGatun64();
      case "ripemd":
        return (IHash) new RIPEMD();
      case "ripemd128":
        return (IHash) new RIPEMD128();
      case "ripemd160":
        return (IHash) new RIPEMD160();
      case "ripemd256":
        return (IHash) new RIPEMD256();
      case "ripemd320":
        return (IHash) new RIPEMD320();
      case "rotating":
        return (IHash) new Rotating();
      case "rs":
        return (IHash) new RS();
      case "sdbm":
        return (IHash) new SDBM();
      case "sha0":
        return (IHash) new SHA0();
      case "sha1":
        return (IHash) new SHA1();
      case "sha2224":
        return (IHash) new SHA2_224();
      case "sha2256":
        return (IHash) new SHA2_256();
      case "sha2384":
        return (IHash) new SHA2_384();
      case "sha2512":
        return (IHash) new SHA2_512();
      case "sha2512224":
        return (IHash) new SHA2_512_224();
      case "sha2512256":
        return (IHash) new SHA2_512_256();
      case "sha3224":
        return (IHash) new SHA3_224();
      case "sha3256":
        return (IHash) new SHA3_256();
      case "sha3384":
        return (IHash) new SHA3_384();
      case "sha3512":
        return (IHash) new SHA3_512();
      case "shiftandxor":
        return (IHash) new ShiftAndXor();
      case "siphash24":
        return (IHash) new SipHash2_4();
      case "superfast":
        return (IHash) new SuperFast();
      case "tiger23128":
        return Tiger2_128.CreateRound3();
      case "tiger23160":
        return Tiger2_160.CreateRound3();
      case "tiger23192":
        return Tiger2_192.CreateRound3();
      case "tiger24128":
        return Tiger2_128.CreateRound4();
      case "tiger24160":
        return Tiger2_160.CreateRound4();
      case "tiger24192":
        return Tiger2_192.CreateRound4();
      case "tiger25128":
        return Tiger2_128.CreateRound5();
      case "tiger25160":
        return Tiger2_160.CreateRound5();
      case "tiger25192":
        return Tiger2_192.CreateRound5();
      case "tiger3128":
        return Tiger_128.CreateRound3();
      case "tiger3160":
        return Tiger_160.CreateRound3();
      case "tiger3192":
        return Tiger_192.CreateRound3();
      case "tiger4128":
        return Tiger_128.CreateRound4();
      case "tiger4160":
        return Tiger_160.CreateRound4();
      case "tiger4192":
        return Tiger_192.CreateRound4();
      case "tiger5128":
        return Tiger_128.CreateRound5();
      case "tiger5160":
        return Tiger_160.CreateRound5();
      case "tiger5192":
        return Tiger_192.CreateRound5();
      case "whirlpool":
        return (IHash) new WhirlPool();
      case "xxhash32":
        return (IHash) new XXHash32();
      case "xxhash64":
        return (IHash) new XXHash64();
      default:
        throw new NotImplementedHashLibException($"Hash string: \"{hash_string}\" is unknown or not in correct format.");
    }
  }
}

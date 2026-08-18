// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.HashFactory
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Checksum;
using Intermech.Hashes.Crypto;
using Intermech.Hashes.Crypto.Blake2BConfigurations;
using Intermech.Hashes.Crypto.Blake2SConfigurations;
using Intermech.Hashes.Hash128;
using Intermech.Hashes.Hash32;
using Intermech.Hashes.Hash64;
using Intermech.Hashes.KDF;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2BConfigurations;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;

#nullable disable
namespace Intermech.Hashes.Base;

public static class HashFactory
{
  public static IHash CreateHash(string hash_string) => LangBuilder.Reducer(hash_string);

  public static class NullDigestFactory
  {
    public static IHash CreateNullDigest() => (IHash) new NullDigest();
  }

  public static class Checksum
  {
    public static IHash CreateCRC(
      int width,
      ulong polynomial,
      ulong initialValue,
      bool reflectIn,
      bool reflectOut,
      ulong outputXor,
      ulong checkValue,
      string[] Names)
    {
      return (IHash) new CRC(width, polynomial, initialValue, reflectIn, reflectOut, outputXor, checkValue, Names);
    }

    public static IHash CreateCRC(CRCStandard value) => (IHash) CRC.CreateCRCObject(value);

    public static IHash CreateCRC16(
      ulong polynomial,
      ulong initialValue,
      bool reflectIn,
      bool reflectOut,
      ulong outputXor,
      ulong checkValue,
      string[] Names)
    {
      return (IHash) new CRC16(polynomial, initialValue, reflectIn, reflectOut, outputXor, checkValue, Names);
    }

    public static IHash CreateCRC16_BUYPASS() => (IHash) new CRC16_BUYPASS();

    public static IHash CreateCRC32(
      ulong polynomial,
      ulong initialValue,
      bool reflectIn,
      bool reflectOut,
      ulong outputXor,
      ulong checkValue,
      string[] Names)
    {
      return (IHash) new CRC32(polynomial, initialValue, reflectIn, reflectOut, outputXor, checkValue, Names);
    }

    public static IHash CreateCRC32_CASTAGNOLI() => (IHash) new CRC32_CASTAGNOLI_Fast();

    public static IHash CreateCRC32_PKZIP() => (IHash) new CRC32_PKZIP_Fast();

    public static IHash CreateCRC64(
      ulong polynomial,
      ulong initialValue,
      bool reflectIn,
      bool reflectOut,
      ulong outputXor,
      ulong checkValue,
      string[] Names)
    {
      return (IHash) new CRC64(polynomial, initialValue, reflectIn, reflectOut, outputXor, checkValue, Names);
    }

    public static IHash CreateCRC64_ECMA_182() => (IHash) new CRC64_ECMA_182();

    public static IHash CreateAdler32() => (IHash) new Adler32();
  }

  public static class Crypto
  {
    public static IHash CreateHAS160() => (IHash) new HAS160();

    public static IHash CreatePanama() => (IHash) new Panama();

    public static IHash CreateWhirlPool() => (IHash) new WhirlPool();

    public static IHash CreateGost() => (IHash) new Gost();

    public static IHash CreateGOST3411_2012_256() => (IHash) new GOST3411_2012_256();

    public static IHash CreateGOST3411_2012_512() => (IHash) new GOST3411_2012_512();

    public static IHash CreateHaval(HashRounds a_rounds, HashSizeEnum a_hash_size)
    {
      switch (a_rounds)
      {
        case HashRounds.Rounds3:
          switch (a_hash_size)
          {
            case HashSizeEnum.HashSize128:
              return HashFactory.Crypto.CreateHaval_3_128();
            case HashSizeEnum.HashSize160:
              return HashFactory.Crypto.CreateHaval_3_160();
            case HashSizeEnum.HashSize192:
              return HashFactory.Crypto.CreateHaval_3_192();
            case HashSizeEnum.HashSize224:
              return HashFactory.Crypto.CreateHaval_3_224();
            case HashSizeEnum.HashSize256:
              return HashFactory.Crypto.CreateHaval_3_256();
            default:
              throw new ArgumentHashLibException(Haval.InvalidHavalHashSize);
          }
        case HashRounds.Rounds4:
          switch (a_hash_size)
          {
            case HashSizeEnum.HashSize128:
              return HashFactory.Crypto.CreateHaval_4_128();
            case HashSizeEnum.HashSize160:
              return HashFactory.Crypto.CreateHaval_4_160();
            case HashSizeEnum.HashSize192:
              return HashFactory.Crypto.CreateHaval_4_192();
            case HashSizeEnum.HashSize224:
              return HashFactory.Crypto.CreateHaval_4_224();
            case HashSizeEnum.HashSize256:
              return HashFactory.Crypto.CreateHaval_4_256();
            default:
              throw new ArgumentHashLibException(Haval.InvalidHavalHashSize);
          }
        case HashRounds.Rounds5:
          switch (a_hash_size)
          {
            case HashSizeEnum.HashSize128:
              return HashFactory.Crypto.CreateHaval_5_128();
            case HashSizeEnum.HashSize160:
              return HashFactory.Crypto.CreateHaval_5_160();
            case HashSizeEnum.HashSize192:
              return HashFactory.Crypto.CreateHaval_5_192();
            case HashSizeEnum.HashSize224:
              return HashFactory.Crypto.CreateHaval_5_224();
            case HashSizeEnum.HashSize256:
              return HashFactory.Crypto.CreateHaval_5_256();
            default:
              throw new ArgumentHashLibException(Haval.InvalidHavalHashSize);
          }
        default:
          throw new ArgumentHashLibException(Haval.InvalidHavalRound);
      }
    }

    public static IHash CreateHaval_3_128() => (IHash) new Haval_3_128();

    public static IHash CreateHaval_4_128() => (IHash) new Haval_4_128();

    public static IHash CreateHaval_5_128() => (IHash) new Haval_5_128();

    public static IHash CreateHaval_3_160() => (IHash) new Haval_3_160();

    public static IHash CreateHaval_4_160() => (IHash) new Haval_4_160();

    public static IHash CreateHaval_5_160() => (IHash) new Haval_5_160();

    public static IHash CreateHaval_3_192() => (IHash) new Haval_3_192();

    public static IHash CreateHaval_4_192() => (IHash) new Haval_4_192();

    public static IHash CreateHaval_5_192() => (IHash) new Haval_5_192();

    public static IHash CreateHaval_3_224() => (IHash) new Haval_3_224();

    public static IHash CreateHaval_4_224() => (IHash) new Haval_4_224();

    public static IHash CreateHaval_5_224() => (IHash) new Haval_5_224();

    public static IHash CreateHaval_3_256() => (IHash) new Haval_3_256();

    public static IHash CreateHaval_4_256() => (IHash) new Haval_4_256();

    public static IHash CreateHaval_5_256() => (IHash) new Haval_5_256();

    public static IHash CreateRadioGatun32() => (IHash) new RadioGatun32();

    public static IHash CreateRadioGatun64() => (IHash) new RadioGatun64();

    public static IHash CreateGrindahl256() => (IHash) new Grindahl256();

    public static IHash CreateGrindahl512() => (IHash) new Grindahl512();

    public static IHash CreateRIPEMD() => (IHash) new RIPEMD();

    public static IHash CreateRIPEMD128() => (IHash) new RIPEMD128();

    public static IHash CreateRIPEMD160() => (IHash) new RIPEMD160();

    public static IHash CreateRIPEMD256() => (IHash) new RIPEMD256();

    public static IHash CreateRIPEMD320() => (IHash) new RIPEMD320();

    public static IHash CreateSnefru(int a_security_level, HashSizeEnum a_hash_size)
    {
      if (a_security_level < 1)
        throw new ArgumentHashLibException(Snefru.InvalidSnefruLevel);
      return a_hash_size == HashSizeEnum.HashSize128 || a_hash_size == HashSizeEnum.HashSize256 ? (IHash) new Snefru(a_security_level, (int) a_hash_size) : throw new ArgumentHashLibException(Snefru.InvalidSnefruHashSize);
    }

    public static IHash CreateSnefru_8_128()
    {
      return HashFactory.Crypto.CreateSnefru(8, HashSizeEnum.HashSize128);
    }

    public static IHash CreateSnefru_8_256()
    {
      return HashFactory.Crypto.CreateSnefru(8, HashSizeEnum.HashSize256);
    }

    public static IHash CreateMD2() => (IHash) new MD2();

    public static IHash CreateMD4() => (IHash) new MD4();

    public static IHash CreateMD5() => (IHash) new MD5();

    public static IHash CreateSHA0() => (IHash) new SHA0();

    public static IHash CreateSHA1() => (IHash) new SHA1();

    public static IHash CreateSHA2_224() => (IHash) new SHA2_224();

    public static IHash CreateSHA2_256() => (IHash) new SHA2_256();

    public static IHash CreateSHA2_384() => (IHash) new SHA2_384();

    public static IHash CreateSHA2_512() => (IHash) new SHA2_512();

    public static IHash CreateSHA2_512_224() => (IHash) new SHA2_512_224();

    public static IHash CreateSHA2_512_256() => (IHash) new SHA2_512_256();

    public static IHash CreateSHA3_224() => (IHash) new SHA3_224();

    public static IHash CreateSHA3_256() => (IHash) new SHA3_256();

    public static IHash CreateSHA3_384() => (IHash) new SHA3_384();

    public static IHash CreateSHA3_512() => (IHash) new SHA3_512();

    public static IHash CreateKeccak_224() => (IHash) new Keccak_224();

    public static IHash CreateKeccak_256() => (IHash) new Keccak_256();

    public static IHash CreateKeccak_288() => (IHash) new Keccak_288();

    public static IHash CreateKeccak_384() => (IHash) new Keccak_384();

    public static IHash CreateKeccak_512() => (IHash) new Keccak_512();

    public static IHash CreateBlake2B(IBlake2BConfig a_Config = null, IBlake2BTreeConfig a_TreeConfig = null)
    {
      return (IHash) new Blake2B(a_Config ?? (IBlake2BConfig) Blake2BConfig.DefaultConfig, a_TreeConfig);
    }

    public static IHash CreateBlake2B_160()
    {
      return HashFactory.Crypto.CreateBlake2B((IBlake2BConfig) new Blake2BConfig(HashSizeEnum.HashSize160));
    }

    public static IHash CreateBlake2B_256()
    {
      return HashFactory.Crypto.CreateBlake2B((IBlake2BConfig) new Blake2BConfig(HashSizeEnum.HashSize256));
    }

    public static IHash CreateBlake2B_384()
    {
      return HashFactory.Crypto.CreateBlake2B((IBlake2BConfig) new Blake2BConfig(HashSizeEnum.HashSize384));
    }

    public static IHash CreateBlake2B_512()
    {
      return HashFactory.Crypto.CreateBlake2B((IBlake2BConfig) new Blake2BConfig());
    }

    public static IHash CreateBlake2S(IBlake2SConfig a_Config = null, IBlake2STreeConfig a_TreeConfig = null)
    {
      return (IHash) new Blake2S(a_Config ?? (IBlake2SConfig) Blake2SConfig.DefaultConfig, a_TreeConfig);
    }

    public static IHash CreateBlake2S_128()
    {
      return HashFactory.Crypto.CreateBlake2S((IBlake2SConfig) new Blake2SConfig(HashSizeEnum.HashSize128));
    }

    public static IHash CreateBlake2S_160()
    {
      return HashFactory.Crypto.CreateBlake2S((IBlake2SConfig) new Blake2SConfig(HashSizeEnum.HashSize160));
    }

    public static IHash CreateBlake2S_224()
    {
      return HashFactory.Crypto.CreateBlake2S((IBlake2SConfig) new Blake2SConfig(HashSizeEnum.HashSize224));
    }

    public static IHash CreateBlake2S_256()
    {
      return HashFactory.Crypto.CreateBlake2S((IBlake2SConfig) new Blake2SConfig());
    }

    public static IHash CreateBlake2BP(int a_HashSize, byte[] a_Key)
    {
      return (IHash) new Blake2BP(a_HashSize, a_Key);
    }

    public static IHash CreateBlake2SP(int a_HashSize, byte[] a_Key)
    {
      return (IHash) new Blake2SP(a_HashSize, a_Key);
    }

    public static IHash CreateBlake3_256(byte[] a_Key) => (IHash) Blake3.CreateBlake3(a_Key: a_Key);

    public static IHash CreateTiger(int a_hash_size, HashRounds a_rounds)
    {
      return a_hash_size == 16 /*0x10*/ || a_hash_size == 20 || a_hash_size == 24 ? (IHash) new Tiger_Base(a_hash_size, a_rounds) : throw new ArgumentHashLibException(Tiger.InvalidTigerHashSize);
    }

    public static IHash CreateTiger_3_128() => Tiger_128.CreateRound3();

    public static IHash CreateTiger_3_160() => Tiger_160.CreateRound3();

    public static IHash CreateTiger_3_192() => Tiger_192.CreateRound3();

    public static IHash CreateTiger_4_128() => Tiger_128.CreateRound4();

    public static IHash CreateTiger_4_160() => Tiger_160.CreateRound4();

    public static IHash CreateTiger_4_192() => Tiger_192.CreateRound4();

    public static IHash CreateTiger_5_128() => Tiger_128.CreateRound5();

    public static IHash CreateTiger_5_160() => Tiger_160.CreateRound5();

    public static IHash CreateTiger_5_192() => Tiger_192.CreateRound5();

    public static IHash CreateTiger2(int a_hash_size, HashRounds a_rounds)
    {
      return a_hash_size == 16 /*0x10*/ || a_hash_size == 20 || a_hash_size == 24 ? (IHash) new Tiger2_Base(a_hash_size, a_rounds) : throw new ArgumentHashLibException(Tiger2.InvalidTiger2HashSize);
    }

    public static IHash CreateTiger2_3_128() => Tiger2_128.CreateRound3();

    public static IHash CreateTiger2_3_160() => Tiger2_160.CreateRound3();

    public static IHash CreateTiger2_3_192() => Tiger2_192.CreateRound3();

    public static IHash CreateTiger2_4_128() => Tiger2_128.CreateRound4();

    public static IHash CreateTiger2_4_160() => Tiger2_160.CreateRound4();

    public static IHash CreateTiger2_4_192() => Tiger2_192.CreateRound4();

    public static IHash CreateTiger2_5_128() => Tiger2_128.CreateRound5();

    public static IHash CreateTiger2_5_160() => Tiger2_160.CreateRound5();

    public static IHash CreateTiger2_5_192() => Tiger2_192.CreateRound5();
  }

  public static class Hash32
  {
    public static IHash CreateAP() => (IHash) new AP();

    public static IHash CreateBernstein() => (IHash) new Bernstein();

    public static IHash CreateBernstein1() => (IHash) new Bernstein1();

    public static IHash CreateBKDR() => (IHash) new BKDR();

    public static IHash CreateDEK() => (IHash) new DEK();

    public static IHash CreateDJB() => (IHash) new DJB();

    public static IHash CreateELF() => (IHash) new ELF();

    public static IHash CreateFNV() => (IHash) new FNV();

    public static IHash CreateFNV1a() => (IHash) new FNV1a();

    public static IHash CreateJenkins3(int initialValue = 0) => (IHash) new Jenkins3(initialValue);

    public static IHash CreateJS() => (IHash) new JS();

    public static IHashWithKey CreateMurmur2() => (IHashWithKey) new Murmur2();

    public static IHashWithKey CreateMurmurHash3_x86_32()
    {
      return (IHashWithKey) new MurmurHash3_x86_32();
    }

    public static IHash CreateOneAtTime() => (IHash) new OneAtTime();

    public static IHash CreatePJW() => (IHash) new PJW();

    public static IHash CreateRotating() => (IHash) new Rotating();

    public static IHash CreateRS() => (IHash) new RS();

    public static IHash CreateSDBM() => (IHash) new SDBM();

    public static IHash CreateShiftAndXor() => (IHash) new ShiftAndXor();

    public static IHash CreateSuperFast() => (IHash) new SuperFast();

    public static IHashWithKey CreateXXHash32() => (IHashWithKey) new XXHash32();
  }

  public static class Hash64
  {
    public static IHash CreateFNV() => (IHash) new FNV64();

    public static IHash CreateFNV1a() => (IHash) new FNV1a64();

    public static IHashWithKey CreateMurmur2() => (IHashWithKey) new Murmur2_64();

    public static IHashWithKey CreateSipHash2_4() => (IHashWithKey) new SipHash2_4();

    public static IHashWithKey CreateXXHash64() => (IHashWithKey) new XXHash64();
  }

  public static class Hash128
  {
    public static IHashWithKey CreateMurmurHash3_x86_128()
    {
      return (IHashWithKey) new MurmurHash3_x86_128();
    }

    public static IHashWithKey CreateMurmurHash3_x64_128()
    {
      return (IHashWithKey) new MurmurHash3_x64_128();
    }
  }

  public static class XOF
  {
    public static IHash CreateShake_128(ulong a_XofSizeInBits)
    {
      Shake_128 shake128 = new Shake_128();
      shake128.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) shake128;
    }

    public static IHash CreateShake_256(ulong a_XofSizeInBits)
    {
      Shake_256 shake256 = new Shake_256();
      shake256.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) shake256;
    }

    public static IHash CreateCShake_128(byte[] AN, byte[] AS, ulong a_XofSizeInBits)
    {
      CShake_128 cshake128 = new CShake_128(AN, AS);
      cshake128.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) cshake128;
    }

    public static IHash CreateCShake_256(byte[] AN, byte[] AS, ulong a_XofSizeInBits)
    {
      CShake_256 cshake256 = new CShake_256(AN, AS);
      cshake256.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) cshake256;
    }

    public static IHash CreateBlake2XS(IBlake2XSConfig a_Blake2XSConfig, ulong a_XofSizeInBits)
    {
      Blake2XS blake2Xs = new Blake2XS(a_Blake2XSConfig);
      blake2Xs.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) blake2Xs;
    }

    public static IHash CreateBlake2XS(byte[] a_Key, ulong a_XofSizeInBits)
    {
      Blake2SConfig a_Blake2SConfig = new Blake2SConfig(32 /*0x20*/);
      a_Blake2SConfig.Key = a_Key.DeepCopy();
      return HashFactory.XOF.CreateBlake2XS((IBlake2XSConfig) new Blake2XSConfig((IBlake2SConfig) a_Blake2SConfig), a_XofSizeInBits);
    }

    public static IHash CreateBlake2XB(IBlake2XBConfig a_Blake2XBConfig, ulong a_XofSizeInBits)
    {
      Blake2XB blake2Xb = new Blake2XB(a_Blake2XBConfig);
      blake2Xb.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) blake2Xb;
    }

    public static IHash CreateBlake2XB(byte[] a_Key, ulong a_XofSizeInBits)
    {
      Blake2BConfig a_Blake2BConfig = new Blake2BConfig(64 /*0x40*/);
      a_Blake2BConfig.Key = a_Key.DeepCopy();
      return HashFactory.XOF.CreateBlake2XB((IBlake2XBConfig) new Blake2XBConfig((IBlake2BConfig) a_Blake2BConfig), a_XofSizeInBits);
    }

    public static IHash CreateBlake3XOF(byte[] a_Key, ulong a_XofSizeInBits)
    {
      Blake3XOF blake3Xof = Blake3XOF.CreateBlake3XOF(32 /*0x20*/, a_Key);
      blake3Xof.XOFSizeInBits = a_XofSizeInBits;
      return (IHash) blake3Xof;
    }

    public static IHash CreateKMAC128XOF(
      byte[] a_KMACKey,
      byte[] a_Customization,
      ulong a_XofSizeInBits)
    {
      return (IHash) KMAC128XOF.CreateKMAC128XOF(a_KMACKey, a_Customization, a_XofSizeInBits);
    }

    public static IHash CreateKMAC256XOF(
      byte[] a_KMACKey,
      byte[] a_Customization,
      ulong a_XofSizeInBits)
    {
      return (IHash) KMAC256XOF.CreateKMAC256XOF(a_KMACKey, a_Customization, a_XofSizeInBits);
    }
  }

  public static class KMAC
  {
    public static IHash CreateKMAC128(
      byte[] a_KMACKey,
      byte[] a_Customization,
      ulong a_OutputLengthInBits)
    {
      return (IHash) KMAC128.CreateKMAC128(a_KMACKey, a_Customization, a_OutputLengthInBits);
    }

    public static IHash CreateKMAC256(
      byte[] a_KMACKey,
      byte[] a_Customization,
      ulong a_OutputLengthInBits)
    {
      return (IHash) KMAC256.CreateKMAC256(a_KMACKey, a_Customization, a_OutputLengthInBits);
    }
  }

  public static class HMAC
  {
    public static IHMAC CreateHMAC(IHash hash, byte[] a_HMACKey)
    {
      return (IHMAC) HMACNotBuildInAdapter.CreateHMAC(hash, a_HMACKey);
    }
  }

  public static class Blake2BMAC
  {
    public static IBlake2BMAC CreateBlake2BMAC(
      byte[] a_Blake2BKey,
      byte[] a_Salt,
      byte[] a_Personalisation,
      int a_OutputLengthInBits)
    {
      return Blake2BMACNotBuildInAdapter.CreateBlake2BMAC(a_Blake2BKey, a_Salt, a_Personalisation, a_OutputLengthInBits);
    }
  }

  public static class Blake2SMAC
  {
    public static IBlake2SMAC CreateBlake2SMAC(
      byte[] a_Blake2SKey,
      byte[] a_Salt,
      byte[] a_Personalisation,
      int a_OutputLengthInBits)
    {
      return Blake2SMACNotBuildInAdapter.CreateBlake2SMAC(a_Blake2SKey, a_Salt, a_Personalisation, a_OutputLengthInBits);
    }
  }

  public static class KDF
  {
    public static class PBKDF2_HMAC
    {
      public static IPBKDF2_HMAC CreatePBKDF2_HMAC(
        IHash a_hash,
        byte[] a_password,
        byte[] a_salt,
        uint a_iterations)
      {
        if (a_hash == null)
          throw new ArgumentNullHashLibException(PBKDF2_HMACNotBuildInAdapter.UninitializedInstance);
        if (a_password.Empty())
          throw new ArgumentNullHashLibException(PBKDF2_HMACNotBuildInAdapter.EmptyPassword);
        if (a_salt.Empty())
          throw new ArgumentNullHashLibException(PBKDF2_HMACNotBuildInAdapter.EmptySalt);
        if (a_iterations < 1U)
          throw new ArgumentHashLibException(PBKDF2_HMACNotBuildInAdapter.IterationTooSmall);
        return (IPBKDF2_HMAC) new PBKDF2_HMACNotBuildInAdapter(a_hash, a_password, a_salt, a_iterations);
      }
    }

    public static class PBKDF_Blake3
    {
      public static IPBKDF_Blake3NotBuiltIn CreatePBKDF_Blake3(byte[] a_key, byte[] ctx)
      {
        return (IPBKDF_Blake3NotBuiltIn) new PBKDF_Blake3NotBuiltInAdapter(a_key, ctx);
      }
    }

    public static class PBKDF_Argon2
    {
      public static IPBKDF_Argon2 CreatePBKDF_Argon2(
        byte[] a_Password,
        IArgon2Parameters a_Argon2Parameters)
      {
        return (IPBKDF_Argon2) new PBKDF_Argon2NotBuildInAdapter(a_Password, a_Argon2Parameters);
      }
    }

    public static class PBKDF_Scrypt
    {
      public static IPBKDF_Scrypt CreatePBKDF_Scrypt(
        byte[] a_PasswordBytes,
        byte[] a_SaltBytes,
        int a_Cost,
        int a_BlockSize,
        int a_Parallelism)
      {
        return (IPBKDF_Scrypt) new PBKDF_ScryptNotBuildInAdapter(a_PasswordBytes, a_SaltBytes, a_Cost, a_BlockSize, a_Parallelism);
      }
    }
  }
}

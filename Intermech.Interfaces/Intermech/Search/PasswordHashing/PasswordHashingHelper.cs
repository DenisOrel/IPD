
// Type: Intermech.Search.PasswordHashing.PasswordHashingHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Protection;
using System.Linq;


namespace Intermech.Search.PasswordHashing
{
    public static class PasswordHashingHelper
    {
      public static PasswordHashAlgorithm ConvertChartToPasswordHashAlgorithm(char cryptMethod)
      {
        if ((int) cryptMethod == (int) CryptHelper.MD5Crypt)
          return PasswordHashAlgorithm.MD5;
        return (int) cryptMethod == (int) CryptHelper.SHA1Crypt ? PasswordHashAlgorithm.SHA1 : PasswordHashAlgorithm.None;
      }

      public static char ConvertPasswordHashAlgorithmToChar(PasswordHashAlgorithm passwordHashAlgorithm)
      {
        if (passwordHashAlgorithm == PasswordHashAlgorithm.MD5)
          return CryptHelper.MD5Crypt;
        return passwordHashAlgorithm == PasswordHashAlgorithm.SHA1 ? CryptHelper.SHA1Crypt : char.MinValue;
      }

      public static PasswordHashAlgorithm GetPasswordHashAlgorithm(string completedPasswordHash)
      {
        return string.IsNullOrEmpty(completedPasswordHash) ? PasswordHashAlgorithm.None : PasswordHashingHelper.ConvertChartToPasswordHashAlgorithm(completedPasswordHash.FirstOrDefault<char>());
      }

      public static PswPackage CreatePswPackage(
        string passwordHash,
        PasswordHashAlgorithm passwordHashAlgorithm)
      {
        PswPackage pswPackage = new PswPackage();
        pswPackage.NoneCryptStr = string.Empty;
        switch (passwordHashAlgorithm)
        {
          case PasswordHashAlgorithm.MD5:
            pswPackage.MD5CryptHash = CryptHelper.MD5Crypt.ToString() + passwordHash;
            break;
          case PasswordHashAlgorithm.SHA1:
            pswPackage.SHA1CryptHash = CryptHelper.SHA1Crypt.ToString() + passwordHash;
            break;
          default:
            pswPackage.NoneCryptStr = CryptHelper.NoneCrypt.ToString() + passwordHash;
            break;
        }
        return pswPackage;
      }

      public static char GetCryptMethod(string passwordHash)
      {
        return string.IsNullOrEmpty(passwordHash) ? CryptHelper.NoneCrypt : passwordHash[0];
      }
    }
}

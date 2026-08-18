
// Type: Intermech.Protection.PswPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Protection
{
    /// <summary>
    /// Контейнер для хранения различных вариантов хэшей пароля
    /// </summary>
    [Serializable]
    public class PswPackage : ICloneable
    {
      /// <summary>Пароль в открытом виде</summary>
      public string NoneCryptStr { get; set; }

      /// <summary>Хэш пароля в SHA1</summary>
      public string SHA1CryptHash { get; set; }

      /// <summary>Хэш пароля в MD5</summary>
      public string MD5CryptHash { get; set; }

      public PswPackage(string password, char cryptMode)
      {
        this.SHA1CryptHash = CryptHelper.CryptPassword(password, CryptHelper.SHA1Crypt);
        this.MD5CryptHash = CryptHelper.CryptPassword(password, CryptHelper.MD5Crypt);
        if ((int) cryptMode == (int) CryptHelper.NoneCrypt)
          this.NoneCryptStr = CryptHelper.CryptPassword(password, CryptHelper.NoneCrypt);
        else
          this.NoneCryptStr = string.Empty;
      }

      public PswPackage(PswPackage source)
      {
        this.SHA1CryptHash = source.SHA1CryptHash;
        this.MD5CryptHash = source.MD5CryptHash;
        this.NoneCryptStr = source.NoneCryptStr;
      }

      /// <summary>Конструктор, создающий пустой пароль</summary>
      public PswPackage() => this.EmptyInit();

      private void EmptyInit()
      {
        this.NoneCryptStr = string.Empty;
        this.SHA1CryptHash = string.Empty;
        this.MD5CryptHash = string.Empty;
      }

      /// <summary>
      /// Проверяет хэш пароля на совпадение с содержимым класса
      /// </summary>
      /// <param name="hash">Хэш пароля</param>
      /// <returns></returns>
      public bool IsValidPassword(string hash)
      {
        return hash == string.Empty ? CryptHelper.CryptPassword(hash, CryptHelper.SHA1Crypt) == this.SHA1CryptHash || CryptHelper.CryptPassword(hash, CryptHelper.MD5Crypt) == this.MD5CryptHash : hash == this.SHA1CryptHash || hash == this.MD5CryptHash || hash == this.NoneCryptStr;
      }

      /// <summary>Возвращает true если пароль пустой</summary>
      public bool IsEmpty
      {
        get
        {
          return this.SHA1CryptHash == CryptHelper.CryptPassword(string.Empty, CryptHelper.SHA1Crypt) && this.MD5CryptHash == CryptHelper.CryptPassword(string.Empty, CryptHelper.MD5Crypt) && this.NoneCryptStr == string.Empty;
        }
      }

      /// <summary>
      /// Возвращает true, если пароль еще не был присвоен (была пустая инициализация класса)
      /// </summary>
      public bool NotInited
      {
        get
        {
          return this.SHA1CryptHash == string.Empty && this.MD5CryptHash == string.Empty && this.NoneCryptStr == string.Empty;
        }
      }

      public override string ToString() => this.NoneCryptStr;

      public object Clone() => (object) new PswPackage(this);

      /// <summary>
      /// Возвращает хэш, соответствующий указанному методу шифрования
      /// </summary>
      /// <param name="cryptMethod">Метод шифрования из констант в CryptHelper</param>
      /// <returns>Хэш, хранящийся в данном классе</returns>
      public string GetHash(char cryptMethod)
      {
        if ((int) cryptMethod == (int) CryptHelper.SHA1Crypt)
          return this.SHA1CryptHash;
        if ((int) cryptMethod == (int) CryptHelper.MD5Crypt)
          return this.MD5CryptHash;
        if ((int) cryptMethod == (int) CryptHelper.NoneCrypt)
          return this.NoneCryptStr;
        throw new KernelException("Unknown crypt method: " + cryptMethod.ToString());
      }
    }
}

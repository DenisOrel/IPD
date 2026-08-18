
// Type: Intermech.Interfaces.IDBEncryptedAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Protection;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс шифрованного атрибута</summary>
    public interface IDBEncryptedAttribute
    {
      /// <summary>
      /// Возвращает true, если в атрибуте содержится строка nowValue
      /// </summary>
      bool ValidateCurrent(string nowValue);

      /// <summary>
      /// Проверяет новый пароль на соответствие текущей политики безопасности
      /// </summary>
      void ValidateNew(string newValue);

      /// <summary>Возвращает текущий метод шифрования паролей</summary>
      char CurrentCryptMethod { get; }

      /// <summary>Устанавливает новый пароль</summary>
      /// <param name="psw">Хэши пароля</param>
      void SetPassword(PswPackage psw);
    }
}

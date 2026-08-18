
// Type: Intermech.Client.Specialized.SimpleSessionPoolLoginInfo
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Interfaces.Client;


namespace Intermech.Client.Specialized
{
    /// <summary>
    /// Контейнер для параметров логина в специализированном клиенте IPS.
    /// </summary>
    public class SimpleSessionPoolLoginInfo : UserSessionLoginInfo
    {
      private string _password;

      /// <summary>
      /// Заполняет свойства текущего объекта, копируя значения из указанного объекта.
      /// </summary>
      /// <param name="anotherLoginInfo">Другой объект</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="anotherLoginInfo" /> не должен быть равен null</exception>
      public override void Assign(UserSessionLoginInfo anotherLoginInfo)
      {
        base.Assign(anotherLoginInfo);
        if (anotherLoginInfo is SimpleSessionPoolLoginInfo sessionPoolLoginInfo)
          this.Password = sessionPoolLoginInfo.Password;
        else
          this.Password = (string) null;
      }

      /// <summary>Возвращает или задает пароль пользователя.</summary>
      public string Password
      {
        get => this._password;
        set => this._password = value;
      }
    }
}


// Type: Intermech.Interfaces.UserLoginEventsHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;
using System.IO;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Позволяет определить разновидность логина пользователя в базу данных - первый логин в базу, последующие логины, а также первый логин в базу, после того,
    /// как она была восстановлена из резервной копии.
    /// </summary>
    /// <remarks>
    /// Этот класс используется клиентами IPS, которые в локальных файлах кэшируют информацию из базы данных.
    /// После восстановления базы из резервной копии эта информация должна быть удалена из локальных файлов.
    /// </remarks>
    public sealed class UserLoginEventsHandler
    {
      private string markerFilePath;
      private bool alreadyChecked;
      private UserLoginEventsHandler.LoginKind loginKind;

      /// <summary>
      /// Возвращает или задает абсолютный путь к файлу, где хранится служебная информация, требуемая для слежения за состоянием базы данных.
      /// </summary>
      public string MarkerFilePath
      {
        get => this.markerFilePath;
        set => this.markerFilePath = value;
      }

      /// <summary>Событие, порождаемое при первом входе в базу данных.</summary>
      public event EventHandler FirstLogin;

      /// <summary>
      /// Событие, порождаемое при каждом последующем входе в базу данных.
      /// </summary>
      public event EventHandler NormalLogin;

      /// <summary>
      /// Событие, порождаемое при первом входе в базу данных после ее восстановления из резервной копии.
      /// </summary>
      public event EventHandler LoginAfterDbRestore;

      /// <summary>
      /// Выполняет определение разновидности логина и передает управление соответствующему событию. Метод может вызываться множество раз, при
      /// последующих вызовах он использует кэшированное значение разновидности логина.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Свойство MarkertFilePath заполнено неправильно</exception>
      public void CheckLogin()
      {
        if (string.IsNullOrEmpty(this.markerFilePath))
          throw new InvalidOperationException("The property MarkerFilePath is empty.");
        if (!Path.IsPathRooted(this.markerFilePath))
          throw new InvalidAreaIDException("The property MarkerFilePath must contains an absolute path.");
        if (!this.alreadyChecked)
        {
          UserLoginEvents userLoginEvents;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            userLoginEvents = sessionKeeper.Session.GetUserLoginEvents();
          userLoginEvents.CurrentLoginDateTime = UserLoginEventsHandler.RoundToSeconds(userLoginEvents.CurrentLoginDateTime);
          userLoginEvents.PrevLoginDateTime = UserLoginEventsHandler.RoundToSeconds(userLoginEvents.PrevLoginDateTime);
          this.loginKind = this.GetLoginKind(userLoginEvents);
          this.WriteLoginMarker(userLoginEvents.CurrentLoginDateTime);
          this.alreadyChecked = true;
        }
        this.DispatchEvent(this.loginKind);
      }

      private DateTime ReadLoginMarker()
      {
        return Convert.ToDateTime(File.ReadAllText(this.markerFilePath, Encoding.ASCII), (IFormatProvider) CultureInfo.InvariantCulture);
      }

      private void WriteLoginMarker(DateTime loginMarker)
      {
        File.WriteAllText(this.markerFilePath, Convert.ToString(loginMarker, (IFormatProvider) CultureInfo.InvariantCulture));
      }

      private UserLoginEventsHandler.LoginKind GetLoginKind(UserLoginEvents loginEvents)
      {
        if (loginEvents.PrevLoginDateTime == DateTime.MinValue)
          return UserLoginEventsHandler.LoginKind.First;
        if (!File.Exists(this.markerFilePath))
          return UserLoginEventsHandler.LoginKind.Unknown;
        try
        {
          DateTime dateTime = this.ReadLoginMarker();
          if (dateTime == loginEvents.PrevLoginDateTime)
            return UserLoginEventsHandler.LoginKind.Normal;
          return dateTime > loginEvents.PrevLoginDateTime ? UserLoginEventsHandler.LoginKind.AfterRestore : UserLoginEventsHandler.LoginKind.Unknown;
        }
        catch (FormatException ex)
        {
          return UserLoginEventsHandler.LoginKind.Unknown;
        }
      }

      private void DispatchEvent(UserLoginEventsHandler.LoginKind loginKind)
      {
        switch (loginKind)
        {
          case UserLoginEventsHandler.LoginKind.First:
            if (this.FirstLogin == null)
              break;
            this.FirstLogin((object) this, EventArgs.Empty);
            break;
          case UserLoginEventsHandler.LoginKind.Normal:
          case UserLoginEventsHandler.LoginKind.Unknown:
            if (this.NormalLogin == null)
              break;
            this.NormalLogin((object) this, EventArgs.Empty);
            break;
          case UserLoginEventsHandler.LoginKind.AfterRestore:
            if (this.LoginAfterDbRestore == null)
              break;
            this.LoginAfterDbRestore((object) this, EventArgs.Empty);
            break;
          default:
            throw new NotImplementedException();
        }
      }

      private static DateTime RoundToSeconds(DateTime value)
      {
        return value.Millisecond != 0 ? value - TimeSpan.FromMilliseconds((double) value.Millisecond) : value;
      }

      private enum LoginKind
      {
        First,
        Normal,
        AfterRestore,
        Unknown,
      }
    }
}

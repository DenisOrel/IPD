
// Type: Intermech.Interfaces.UserSessionLostInterceptor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Optimized;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Клиентский перехватчик синхронных вызовов через remoting, предназначенный для обнаружения
    /// односторонних ошибок remoting, приводящих к потере пользовательской сессии.
    /// Реализация класса не является thread safe.
    /// </summary>
    /// <remarks>
    /// <para>
    /// В случае односторонней ошибки клиентская сторона remoting больше не ждет ответа от
    /// серверной стороны, но выполнение вызова на серверной стороне продолжается. Поэтому
    /// текущую пользовательскую сессию нельзя использовать, и к ней нельзя обращаться,
    /// так как это гарантировано приведет к ошибке многопоточного доступа.</para>
    /// <para>
    /// Перехватчик работает только совместно <see cref="T:Intermech.Interfaces.SessionKeeper" />, так как последний способен
    /// отключать и отбрасывать потерянные пользовательские сессии. Кроме того, наличие
    /// активного <see cref="T:Intermech.Interfaces.SessionKeeper" /> позволяет отличить односторонние ошибки remoting от
    /// двусторонних (например, падение сервера приложений).</para>
    /// </remarks>
    public sealed class UserSessionLostInterceptor : IClientFormatterSinkInterceptor
    {
      private const string clientThreadKeyProperty = "X-IPS-ClientThreadKey";
      private static readonly Guid randomAppKey = Guid.NewGuid();
      private static readonly ClientSideDisconnectionProtectedMethods disconnectionProtectedMethods = new ClientSideDisconnectionProtectedMethods();
      private static readonly ClientSideThreadKeyCodec threadKeyCodec = new ClientSideThreadKeyCodec();

      /// <summary>
      /// Возвращает или задает метод для записи событий в лог-файл.
      /// Значение свойства может быть не задано и равно null.
      /// </summary>
      public Action<string> LogAction { get; set; }

      /// <summary>
      /// Вызывается перед отправкой сообщения на серверную сторону.
      /// </summary>
      /// <param name="msg">Сообщение запроса</param>
      /// <param name="requestHeaders">Заголовки запроса</param>
      /// <param name="requestStream">Сериализованный поток запроса</param>
      public void ProcessMessageStart(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream)
      {
        if (!(msg is IMethodMessage methodMessage) || !UserSessionLostInterceptor.disconnectionProtectedMethods.CanProtect(methodMessage.MethodBase))
          return;
        requestHeaders[(object) "X-IPS-ClientThreadKey"] = (object) this.GetClientThreadKey();
      }

      private string GetClientThreadKey()
      {
        SessionKeeperResourceContext current = SessionKeeperResourceContext.Current;
        Guid appKey = current.Depth != 0 ? current.SessionGUID : UserSessionLostInterceptor.randomAppKey;
        int managedThreadId = Thread.CurrentThread.ManagedThreadId;
        return UserSessionLostInterceptor.threadKeyCodec.Encode(appKey, managedThreadId);
      }

      private string AppendClientThreadInfo(string text)
      {
        return $"Client thread ID: {this.GetClientThreadKey()}, Client thread name: '{Thread.CurrentThread.Name}', ClientFormatterSink: {text}";
      }

      /// <summary>
      /// Вызывается после получения ответного сообщения от серверной сторону.
      /// </summary>
      /// <param name="msg">Сообщение запроса</param>
      /// <param name="requestHeaders">Заголовки запроса</param>
      /// <param name="requestStream">Сериализованный поток запроса</param>
      /// <param name="responseMsg">Сообщение ответа от серверной стороны</param>
      /// <param name="responseHeaders">Заголовки ответа</param>
      /// <param name="responseStream">Сериализованный поток ответа</param>
      public void ProcessMessageFinish(
        IMessage msg,
        ITransportHeaders requestHeaders,
        Stream requestStream,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Stream responseStream)
      {
        SessionKeeperResourceContext current = SessionKeeperResourceContext.Current;
        if (current.Depth == 0 || !(responseMsg is IMethodReturnMessage methodReturnMessage) || !(methodReturnMessage.Exception is UserSessionLostException))
          return;
        current.IsSessionLost = true;
        if (this.LogAction == null)
          return;
        this.LogSessionIsLost();
      }

      /// <summary>
      /// Вызывается при любых исключениях в процессе взаимодействия с серверной стороной.
      /// Метод ни в коем случае не должен бросать исключений.
      /// </summary>
      /// <param name="msg">Сообщение запроса</param>
      /// <param name="requestHeaders">Заголовки запроса. Значение может быть равно null</param>
      /// <param name="responseMsg">Сообщение ответа. Значение может быть равно null</param>
      /// <param name="responseHeaders">Заголовки ответа. Значение может быть равно null</param>
      /// <param name="exception">Объект исключения</param>
      public void ProcessMessageFailed(
        IMessage msg,
        ITransportHeaders requestHeaders,
        IMessage responseMsg,
        ITransportHeaders responseHeaders,
        Exception exception)
      {
        if (this.LogAction == null)
          return;
        this.LogFormatterSinkExceptionInfo(exception);
      }

      private void LogSessionIsLost()
      {
        this.LogAction(this.AppendClientThreadInfo("Обнаружена односторонняя ошибка remoting. Текущая пользовательская сессия была отключена."));
      }

      private void LogFormatterSinkExceptionInfo(Exception exception)
      {
        string sinkExceptionInfo = this.GetFormatterSinkExceptionInfo(exception);
        if (string.IsNullOrEmpty(sinkExceptionInfo))
          return;
        this.LogAction(this.AppendClientThreadInfo(sinkExceptionInfo));
      }

      private string GetFormatterSinkExceptionInfo(Exception exception)
      {
        switch (exception)
        {
          case SocketException _:
            SocketException socketException = (SocketException) exception;
            return $"SocketException: status={socketException.NativeErrorCode}, message={socketException.Message}";
          case WebException _:
            WebException webException = (WebException) exception;
            return $"WebException: status={webException.Status}, message={webException.Message}";
          case RemotingTimeoutException _:
            if (exception.InnerException is WebException)
              return this.GetFormatterSinkExceptionInfo(exception.InnerException);
            break;
        }
        return $"{exception.GetType().Name}: message={exception.Message}";
      }
    }
}

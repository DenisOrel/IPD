using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;
using System.Text;


namespace Intermech.PdfPrintCenter.Connector
{
    /// <summary>
    /// Класс исключений для безопасной передачи по ipc-каналу необработанных исключений из
    /// текущего приложения в другое приложение.
    /// </summary>
    [Serializable]
    public class IpcFaultException : Exception
    {
        private string originalStackTrace;
        [NonSerialized]
        private string combinedStackTrace;

        /// <summary>Создает объект исключения.</summary>
        /// <param name="originalMessage">Исходное сообщение об ошибке</param>
        /// <param name="originalStackTrace">Исходный stack trace</param>
        public IpcFaultException(string originalMessage, string originalStackTrace)
          : base(originalMessage)
        {
            this.originalStackTrace = originalStackTrace;
        }

        /// <summary>Создает объект исключения.</summary>
        /// <param name="info">Сериализованное представление объекта</param>
        /// <param name="context">Контекст сериализации</param>
        protected IpcFaultException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
            this.originalStackTrace = info.GetString(nameof(originalStackTrace));
        }

        /// <summary>
        /// Создает сериализованное представление текущего объекта.
        /// </summary>
        /// <param name="info">Сериализованное представление объекта</param>
        /// <param name="context">Контекст сериализации</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("originalStackTrace", (object)this.originalStackTrace);
        }

        /// <summary>
        /// Создает исключение типа <see cref="T:Intermech.PdfPrintCenter.Connector.IpcFaultException" /> из исходного исключения
        /// текущего приложения для передачи в другое приложение.
        /// </summary>
        /// <param name="exception">Исходное исключение текущего приложения</param>
        /// <returns>Созданное исключение</returns>
        public static IpcFaultException FromOriginalException(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));
            IpcFaultException ipcFaultException = new IpcFaultException(exception.Message, exception.StackTrace);
            RemoteExceptionData remoteExceptionData = new RemoteExceptionData();
            remoteExceptionData.IsUnderConstruction = true;
            remoteExceptionData.AddBuilder((RemoteExceptionDataBuilder)new RemoteStackTraceBuilder((Exception)ipcFaultException, remoteExceptionData));
            RemoteExceptionData.Set((Exception)ipcFaultException, remoteExceptionData);
            return ipcFaultException;
        }

        /// <summary>Возвращает исходный stack trace.</summary>
        public string OriginalStackTrace => this.originalStackTrace;

        /// <summary>
        /// Возвращает комбинированный stack trace исключения, включающий собственный stack trace и
        /// исходный stack trace в другом приложении, где произошло исходное исключение.
        /// </summary>
        public override string StackTrace
        {
            get
            {
                if (this.combinedStackTrace == null)
                    this.combinedStackTrace = this.CombineStackTraces(base.StackTrace, this.originalStackTrace);
                return this.combinedStackTrace;
            }
        }

        private string CombineStackTraces(string localStackTrace, string externalStackTrace)
        {
            if (string.IsNullOrEmpty(externalStackTrace))
                return localStackTrace;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(localStackTrace);
            stringBuilder.AppendLine("--------");
            stringBuilder.AppendLine("External stack trace:");
            stringBuilder.Append(externalStackTrace);
            return stringBuilder.ToString();
        }
    }
}

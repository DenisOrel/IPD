
// Type: Intermech.UserSessionForgottenTransactionException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class UserSessionForgottenTransactionException : KernelException
    {
      public UserSessionForgottenTransactionException()
        : this(LocalizationHolder.rm.GetString("Interfaces_Except_452"))
      {
      }

      public UserSessionForgottenTransactionException(string message)
        : base(message)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected UserSessionForgottenTransactionException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
      {
      }

      /// <summary>
      /// Проверка наличие UserSessionThreadConflictException в исключении
      /// </summary>
      /// <param name="e"></param>
      /// <returns></returns>
      public static bool ContainException(Exception e)
      {
        if (e == null)
          return false;
        return e is UserSessionForgottenTransactionException || UserSessionForgottenTransactionException.ContainException(e.InnerException);
      }
    }
}

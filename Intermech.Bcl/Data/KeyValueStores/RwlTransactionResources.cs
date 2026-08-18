
// Type: Intermech.Data.KeyValueStores.RwlTransactionResources
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Data.KeyValueStores
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class RwlTransactionResources
    {
      private static ResourceManager resourceMan;
      private static CultureInfo resourceCulture;

      internal RwlTransactionResources()
      {
      }

      /// <summary>
      ///   Returns the cached ResourceManager instance used by this class.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (RwlTransactionResources.resourceMan == null)
            RwlTransactionResources.resourceMan = new ResourceManager("Intermech.Data.KeyValueStores.RwlTransactionResources", typeof (RwlTransactionResources).Assembly);
          return RwlTransactionResources.resourceMan;
        }
      }

      /// <summary>
      ///   Overrides the current thread's CurrentUICulture property for all
      ///   resource lookups using this strongly typed resource class.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => RwlTransactionResources.resourceCulture;
        set => RwlTransactionResources.resourceCulture = value;
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось начать новую транзакцию из-за таймаута ожидания завершения параллельной транзакции..
      /// </summary>
      internal static string SR_BeginTransactionTimeout
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_BeginTransactionTimeout), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Невозможно начать новую транзакцию, пока текущая транзакция не будет завершена..
      /// </summary>
      internal static string SR_CantBeginAnotherTransaction
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_CantBeginAnotherTransaction), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Невозможно завершить транзакцию, так как у текущего потока нет активной транзакции..
      /// </summary>
      internal static string SR_CantEndNonexistentTransaction
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_CantEndNonexistentTransaction), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Требуется активная транзакция на чтение..
      /// </summary>
      internal static string SR_ReadTransactionRequired
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_ReadTransactionRequired), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Выполнение этой транзакции уже было завершено..
      /// </summary>
      internal static string SR_TransactionIsAlreadyEnded
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_TransactionIsAlreadyEnded), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Требуется, чтобы у текущего потока не было активной транзакции..
      /// </summary>
      internal static string SR_TransactionIsNotAllowed
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_TransactionIsNotAllowed), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Менеджер транзакций был выключен..
      /// </summary>
      internal static string SR_TransactionManagerWasTurnedOff
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_TransactionManagerWasTurnedOff), RwlTransactionResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Требуется активная транзакция на запись..
      /// </summary>
      internal static string SR_WriteTransactionRequired
      {
        get
        {
          return RwlTransactionResources.ResourceManager.GetString(nameof (SR_WriteTransactionRequired), RwlTransactionResources.resourceCulture);
        }
      }
    }
}

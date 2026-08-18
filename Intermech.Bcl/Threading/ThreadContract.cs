
// Type: Intermech.Threading.ThreadContract
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    public static class ThreadContract
    {
      /// <summary>
      /// Проверяет, что доступ к текущему объекту уже синхронизирован в открытом методе текущего объекта или обработчике события где-то выше по call stack.
      /// </summary>
      /// <param name="syncRoot">Вспомогательный объект для синхронизации доступа</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="syncRoot" /> содержит null</exception>
      /// <exception cref="T:System.InvalidOperationException">доступ к текущему объекту не был синхронизирован</exception>
      [Conditional("DEBUG")]
      public static void CheckLockedAtSyncRoot(object syncRoot)
      {
        if (syncRoot == null)
          throw new ArgumentNullException(nameof (syncRoot));
        if (Monitor.IsEntered(syncRoot))
          return;
        ThreadContract.ThrowNoLockException();
      }

      [Conditional("DEBUG")]
      public static void CheckNotReadLocked(ReaderWriterLockSlim rwl)
      {
        if (rwl == null)
          throw new ArgumentNullException(nameof (rwl));
        if (!rwl.IsReadLockHeld)
          return;
        ThreadContract.ThrowAlreadyLockedException();
      }

      [Conditional("DEBUG")]
      public static void CheckLocked(ReaderWriterLockSlim rwl)
      {
        if (rwl == null)
          throw new ArgumentNullException(nameof (rwl));
        if (rwl.IsReadLockHeld || rwl.IsWriteLockHeld)
          return;
        ThreadContract.ThrowNoLockException();
      }

      [Conditional("DEBUG")]
      public static void CheckReadLocked(ReaderWriterLockSlim rwl)
      {
        if (rwl == null)
          throw new ArgumentNullException(nameof (rwl));
        if (rwl.IsReadLockHeld)
          return;
        ThreadContract.ThrowNoLockException();
      }

      [Conditional("DEBUG")]
      public static void CheckWriteLocked(ReaderWriterLockSlim rwl)
      {
        if (rwl == null)
          throw new ArgumentNullException(nameof (rwl));
        if (rwl.IsWriteLockHeld)
          return;
        ThreadContract.ThrowNoLockException();
      }

      private static void ThrowAlreadyLockedException()
      {
        throw new InvalidOperationException("To access an object in thread-safe manner you must not lock it.");
      }

      private static void ThrowNoLockException()
      {
        throw new InvalidOperationException("To access an object in thread-safe manner you must lock it.");
      }
    }
}

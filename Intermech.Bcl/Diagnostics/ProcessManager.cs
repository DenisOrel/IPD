
// Type: Intermech.Diagnostics.ProcessManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Вспомогательный класс для получения информации о процессах.
    /// Предоставляет улучшенную версию некоторых методов класса System.Diagnostics.Process.
    /// Реализация методов системного класса получена с помощью reverse engineering.
    /// </summary>
    public static class ProcessManager
    {
      private const int DefaultCachedBufferSize = 131072 /*0x020000*/;
      private const int SystemProcessId = 4;
      private static long[] cachedBuffer;

      /// <summary>
      /// Позволяет получить список процессов, имена которых совпадают с <paramref name="processName" />.
      /// </summary>
      /// <param name="processName"></param>
      /// <returns></returns>
      /// <remarks>
      /// В отличие от метода в системном классе,
      /// данный корректно обрабатывает ситуации с одноименными процессами разной битности,
      /// одноименными процессами, запущенными разными пользователями
      /// </remarks>
      public static Process[] GetProcessesByName(string processName)
      {
        if (processName == null)
          processName = string.Empty;
            ProcessInfo[] processInfos = ProcessManager.GetProcessInfos();
        List<Process> processList = new List<Process>();
        for (int index = 0; index < processInfos.Length; ++index)
        {
          try
          {
            if (string.Equals(processName, processInfos[index].processName, StringComparison.OrdinalIgnoreCase))
            {
              Process processById = Process.GetProcessById(processInfos[index].processId);
              processList.Add(processById);
            }
          }
          catch
          {
          }
        }
        return processList.ToArray();
      }

      private static ProcessInfo[] GetProcessInfos()
      {
        return ProcessManager.GetProcessInfosCore();
      }

      private static ProcessInfo[] GetProcessInfosCore()
      {
        return ProcessManager.GetProcessInfos((Predicate<int>) null);
      }

      private static ProcessInfo[] GetProcessInfos(Predicate<int> processIdFilter = null)
      {
        int returnedSize = 0;
        GCHandle gcHandle = new GCHandle();
        int num = 131072 /*0x020000*/;
        long[] numArray = Interlocked.Exchange(ref ProcessManager.cachedBuffer, (long[]) null);
        try
        {
          int error;
          do
          {
            if (numArray == null)
              numArray = new long[(num + 7) / 8];
            else
              num = numArray.Length * 8;
            gcHandle = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
            error = ProcessManager.NativeMethods.NtQuerySystemInformation(5, gcHandle.AddrOfPinnedObject(), num, out returnedSize);
            if (error == -1073741820 /*0xC0000004*/)
            {
              if (gcHandle.IsAllocated)
                gcHandle.Free();
              numArray = (long[]) null;
              num = ProcessManager.GetNewBufferSize(num, returnedSize);
            }
          }
          while (error == -1073741820 /*0xC0000004*/);
          if (error < 0)
            throw new InvalidOperationException("Не удалось получить информацию о процессах.", (Exception) new Win32Exception(error));
          return ProcessManager.GetProcessInfos(gcHandle.AddrOfPinnedObject(), processIdFilter);
        }
        finally
        {
          Interlocked.Exchange(ref ProcessManager.cachedBuffer, numArray);
          if (gcHandle.IsAllocated)
            gcHandle.Free();
        }
      }

      private static ProcessInfo[] GetProcessInfos(
        IntPtr dataPtr,
        Predicate<int> processIdFilter)
      {
        Hashtable hashtable = new Hashtable(60);
        long num = 0;
        while (true)
        {
          IntPtr ptr = (IntPtr) ((long) dataPtr + num);
                SystemProcessInformation processInformation = new SystemProcessInformation();
                SystemProcessInformation structure = processInformation;
          Marshal.PtrToStructure(ptr, (object) structure);
          int int32 = processInformation.UniqueProcessId.ToInt32();
          if (processIdFilter == null || processIdFilter(int32))
          {
                    ProcessInfo processInfo = new ProcessInfo()
            {
              processId = int32,
              handleCount = (int) processInformation.HandleCount,
              sessionId = (int) processInformation.SessionId,
              poolPagedBytes = (long) (ulong) processInformation.QuotaPagedPoolUsage,
              poolNonpagedBytes = (long) (ulong) processInformation.QuotaNonPagedPoolUsage,
              virtualBytes = (long) (ulong) processInformation.VirtualSize,
              virtualBytesPeak = (long) (ulong) processInformation.PeakVirtualSize,
              workingSetPeak = (long) (ulong) processInformation.PeakWorkingSetSize,
              workingSet = (long) (ulong) processInformation.WorkingSetSize,
              pageFileBytesPeak = (long) (ulong) processInformation.PeakPagefileUsage,
              pageFileBytes = (long) (ulong) processInformation.PagefileUsage,
              privateBytes = (long) (ulong) processInformation.PrivatePageCount,
              basePriority = processInformation.BasePriority
            };
            processInfo.processName = !(processInformation.NamePtr == IntPtr.Zero) ? ProcessManager.GetProcessShortName(Marshal.PtrToStringUni(processInformation.NamePtr, (int) processInformation.NameLength / 2)) : (processInfo.processId == 4 ? "System" : (processInfo.processId == 0 ? "Idle" : processInfo.processId.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
            hashtable[(object) processInfo.processId] = (object) processInfo;
          }
          if (processInformation.NextEntryOffset != 0U)
            num += (long) processInformation.NextEntryOffset;
          else
            break;
        }
            ProcessInfo[] processInfos = new ProcessInfo[hashtable.Values.Count];
        hashtable.Values.CopyTo((Array) processInfos, 0);
        return processInfos;
      }

      private static int GetNewBufferSize(int existingBufferSize, int requiredSize)
      {
        if (requiredSize == 0)
        {
          int num = existingBufferSize * 2;
          return num >= existingBufferSize ? num : throw new OutOfMemoryException();
        }
        int num1 = requiredSize + 10240;
        return num1 >= requiredSize ? num1 : throw new OutOfMemoryException();
      }

      private static string GetProcessShortName(string name)
      {
        if (string.IsNullOrEmpty(name))
          return string.Empty;
        int num1 = -1;
        int startIndex1 = -1;
        for (int index = 0; index < name.Length; ++index)
        {
          if (name[index] == '\\')
            num1 = index;
          else if (name[index] == '.')
            startIndex1 = index;
        }
        int num2 = startIndex1 != -1 ? (!string.Equals(".exe", name.Substring(startIndex1), StringComparison.OrdinalIgnoreCase) ? name.Length - 1 : startIndex1 - 1) : name.Length - 1;
        int startIndex2 = num1 != -1 ? num1 + 1 : 0;
        return name.Substring(startIndex2, num2 - startIndex2 + 1);
      }

      private static class NativeMethods
      {
        [DllImport("ntdll.dll", CharSet = CharSet.Auto)]
        public static extern int NtQuerySystemInformation(
          int query,
          IntPtr dataPtr,
          int size,
          out int returnedSize);
      }

      private class ProcessInfo
      {
        public int basePriority;
        public string processName;
        public int processId;
        public int handleCount;
        public long poolPagedBytes;
        public long poolNonpagedBytes;
        public long virtualBytes;
        public long virtualBytesPeak;
        public long workingSetPeak;
        public long workingSet;
        public long pageFileBytesPeak;
        public long pageFileBytes;
        public long privateBytes;
        public int mainModuleId;
        public int sessionId;
      }

      [StructLayout(LayoutKind.Sequential)]
      private class SystemProcessInformation
      {
        internal uint NextEntryOffset;
        internal uint NumberOfThreads;
        private long SpareLi1;
        private long SpareLi2;
        private long SpareLi3;
        private long CreateTime;
        private long UserTime;
        private long KernelTime;
        internal ushort NameLength;
        internal ushort MaximumNameLength;
        internal IntPtr NamePtr;
        internal int BasePriority;
        internal IntPtr UniqueProcessId;
        internal IntPtr InheritedFromUniqueProcessId;
        internal uint HandleCount;
        internal uint SessionId;
        internal UIntPtr PageDirectoryBase;
        internal UIntPtr PeakVirtualSize;
        internal UIntPtr VirtualSize;
        internal uint PageFaultCount;
        internal UIntPtr PeakWorkingSetSize;
        internal UIntPtr WorkingSetSize;
        internal UIntPtr QuotaPeakPagedPoolUsage;
        internal UIntPtr QuotaPagedPoolUsage;
        internal UIntPtr QuotaPeakNonPagedPoolUsage;
        internal UIntPtr QuotaNonPagedPoolUsage;
        internal UIntPtr PagefileUsage;
        internal UIntPtr PeakPagefileUsage;
        internal UIntPtr PrivatePageCount;
        private long ReadOperationCount;
        private long WriteOperationCount;
        private long OtherOperationCount;
        private long ReadTransferCount;
        private long WriteTransferCount;
        private long OtherTransferCount;
      }
    }
}

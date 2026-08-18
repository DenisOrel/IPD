// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Services.PrintCenterStartupService
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System;
using System.Threading;


namespace Intermech.PdfPrintCenter.Services
{
    internal class PrintCenterStartupService : IDisposable
    {
        private object syncRoot;
        private bool startedFlag;
        private ManualResetEventSlim startedEvent;

        public PrintCenterStartupService()
        {
            this.syncRoot = new object();
            this.startedFlag = false;
            this.startedEvent = new ManualResetEventSlim(false);
        }

        public void Dispose()
        {
            if (this.startedEvent == null)
                return;
            this.startedEvent.Dispose();
            this.startedEvent = (ManualResetEventSlim)null;
        }

        public bool Started
        {
            get
            {
                lock (this.syncRoot)
                    return this.startedFlag;
            }
        }

        public ManualResetEventSlim StartedEvent => this.startedEvent;

        public void SetStarted()
        {
            lock (this.syncRoot)
            {
                if (this.startedFlag)
                    return;
                this.startedFlag = true;
                this.startedEvent.Set();
            }
        }
    }
}

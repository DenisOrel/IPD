
// Type: Intermech.Settings.PersistentSettingsObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Threading;


namespace Intermech.Settings
{
    public abstract class PersistentSettingsObject : SettingsObject
    {
      private int saveLatch;

      public abstract void Load();

      public abstract void Save();

      public void SaveInBackground()
      {
        if (Interlocked.CompareExchange(ref this.saveLatch, 1, 0) != 0)
          return;
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.SaveWorker));
      }

      private void SaveWorker(object state0)
      {
        try
        {
          Thread.Sleep(2000);
          this.Save();
        }
        catch
        {
        }
        finally
        {
          Interlocked.Exchange(ref this.saveLatch, 0);
        }
      }
    }
}


// Type: Intermech.ProgressReporter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    internal sealed class ProgressReporter
    {
      private long totalItems;
      private long processedItems;
      private PercentEventHandler progressHandler;
      private double progressFactor;
      private double? lastProgress;

      public ProgressReporter() => this.Reset();

      public void Reset() => this.Initialize(0L, (PercentEventHandler) null);

      public void Initialize(long totalItems, PercentEventHandler progressHandler)
      {
        this.totalItems = totalItems >= 0L ? totalItems : throw new ArgumentOutOfRangeException(nameof (totalItems));
        this.processedItems = 0L;
        this.progressHandler = progressHandler;
        this.lastProgress = new double?();
        if (!this.Active)
          return;
        this.progressFactor = 100.0 / (double) this.totalItems;
        this.ReportProgressCore();
      }

      public void Finish()
      {
        if (!this.Active || this.processedItems >= this.totalItems)
          return;
        this.UpdateProgress(this.totalItems - this.processedItems);
      }

      public void SetProgress(long processedItems)
      {
        if (processedItems < 0L)
          throw new ArgumentOutOfRangeException(nameof (processedItems));
        if (!this.Active)
          return;
        this.processedItems = processedItems;
        if (this.processedItems > this.totalItems)
          this.processedItems = this.totalItems;
        this.ReportProgressCore();
      }

      public void UpdateProgress(long processedItems)
      {
        if (processedItems < 0L)
          throw new ArgumentOutOfRangeException(nameof (processedItems));
        if (!this.Active || processedItems <= 0L)
          return;
        this.processedItems += processedItems;
        if (this.processedItems > this.totalItems)
          this.processedItems = this.totalItems;
        this.ReportProgressCore();
      }

      public void ReportProgress()
      {
        if (!this.Active)
          return;
        this.ReportProgressCore();
      }

      private void ReportProgressCore()
      {
        double num1 = (double) this.processedItems * this.progressFactor;
        if (MathUtils.AlmostEqual(num1, 100.0))
          num1 = 100.0;
        if (this.lastProgress.HasValue && num1 < 100.0)
        {
          double num2 = num1;
          double? lastProgress = this.lastProgress;
          double num3 = 1.0;
          double? nullable = lastProgress.HasValue ? new double?(lastProgress.GetValueOrDefault() + num3) : new double?();
          double valueOrDefault = nullable.GetValueOrDefault();
          if (!(num2 >= valueOrDefault & nullable.HasValue))
            return;
        }
        this.lastProgress = new double?(num1);
        this.progressHandler((object) this, new PercentEventArgs((int) Math.Ceiling(num1)));
      }

      public bool Active => this.totalItems != 0L && this.progressHandler != null;
    }
}

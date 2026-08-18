
// Type: Intermech.Interfaces.BlobProcCustomClass
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Threading;


namespace Intermech.Interfaces
{
    public class BlobProcCustomClass
    {
      protected BlobProcessorMode mode;
      protected IUserSession session;
      /// <summary>
      ///  использовать данную сессию для работы с блобами.
      ///  если не назначена, то сессия должна браться у SessionKeeper ( рекомендовано для клиентов ).
      /// </summary>
      protected bool inThread;
      protected bool abortThreadFlag;
      protected IDBAttribute lIDBAttribute;
      protected long elementID;
      protected AttributableElements attributableElement;
      protected int attributeID;
      protected int index;
      protected int dataBlockSize = Consts.DefaultBlobBlockSize;
      protected bool result = true;
      protected Thread thread;

      public bool InThread => this.inThread;

      public bool AbortThreadFlag => this.abortThreadFlag;

      public void SetAbortThreadFlag()
      {
        if (this.inThread)
          this.abortThreadFlag = true;
        else
          this.abortThreadFlag = false;
      }

      protected void OnProgress(int value)
      {
        if (this.Progress == null)
          return;
        this.Progress(this, this.mode, value);
      }

      protected void OnPackProgress(object sender, PercentEventArgs e) => this.OnProgress(e.Percent);

      protected void OnUnpackProgress(object sender, PercentEventArgs e) => this.OnProgress(e.Percent);

      protected void OnThreadFinish(
        bool result,
        string message,
        Exception exception,
        BlobInformation bi)
      {
        if (this.ThreadFinish == null)
          return;
        this.ThreadFinish(this, result, (object) message, exception, bi);
      }

      public IDBAttribute LIDBAttribute => this.lIDBAttribute;

      public long ElementID => this.elementID;

      public AttributableElements AttributableElement => this.attributableElement;

      public int AttributeID => this.attributeID;

      public int Index => this.index;

      public int DataBlockSize => this.dataBlockSize;

      public event BlobProcCustomClass.ProgressEventHandler Progress;

      public event BlobProcCustomClass.ThreadFinishEventHandler ThreadFinish;

      public bool Result => this.result;

      public Thread Thread => this.thread;

      public static IDBAttribute GetAttributeInterface(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        IUserSession iSession)
      {
        IDBAttribute attributeInterface = (IDBAttribute) null;
        switch (aAttributableElement)
        {
          case AttributableElements.Object:
            IDBObject dbObject = iSession.GetObject(aElementID);
            if (dbObject == null)
              AbortException.Abort(LocalizationHolder.rm.GetString("Client.Core_1059"));
            attributeInterface = dbObject.GetAttributeByID(aAttributeID);
            break;
          case AttributableElements.Relation:
            IDBRelation relation = iSession.GetRelation(aElementID);
            if (relation == null)
              AbortException.Abort(LocalizationHolder.rm.GetString("Client.Core_1060"));
            attributeInterface = relation.GetAttributeByID(aAttributeID);
            break;
        }
        if (attributeInterface != null)
          attributeInterface.Index = aIndex;
        return attributeInterface;
      }

      public delegate void ProgressEventHandler(
        BlobProcCustomClass sender,
        BlobProcessorMode mode,
        int progress);

      public delegate void ThreadFinishEventHandler(
        BlobProcCustomClass sender,
        bool result,
        object message,
        Exception exception,
        BlobInformation bi);
    }
}

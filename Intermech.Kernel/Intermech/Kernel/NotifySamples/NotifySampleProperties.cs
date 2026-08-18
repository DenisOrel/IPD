// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.NotifySamples.NotifySampleProperties
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.NotifySamples;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.Kernel.NotifySamples;

internal class NotifySampleProperties
{
  public string Name;
  public List<long> LastObjectsList = new List<long>();
  public int SearchPeriod;
  public DateTime LastSearchTime;
  public NotifyMode Mode;
  public long SampleID;
  public ConditionStructure[] Conditions;
  public bool IsListModified;

  public NotifySampleProperties(IDBObject sample) => this.LoadFromObject(sample);

  private void ReadFromStream(Stream strm)
  {
    this.LastObjectsList.Clear();
    strm.Seek(0L, SeekOrigin.Begin);
    using (BinaryReader binaryReader = new BinaryReader(strm))
    {
      while (strm.Position < strm.Length)
        this.LastObjectsList.Add(binaryReader.ReadInt64());
    }
  }

  public void LoadFromObject(IDBObject sample)
  {
    this.SampleID = sample.ObjectID;
    this.SearchPeriod = Convert.ToInt32(sample.Attributes.FindByID(NotifySamplesConst.NotifyPeriodAttr).AsInteger);
    this.Mode = (NotifyMode) Convert.ToInt32(sample.Attributes.FindByID(NotifySamplesConst.NotifyModeAttr).AsInteger);
    this.Name = sample.Attributes.FindByID(NotifySamplesConst.NameAttr).AsString;
    this.LastSearchTime = sample.Attributes.FindByID(NotifySamplesConst.CheckDateAttr).AsDateTime;
    IDBAttribute byId = sample.Attributes.FindByID(NotifySamplesConst.ObjectsListAttr);
    if (byId != null)
    {
      IBlobReader blobReader = byId as IBlobReader;
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      try
      {
        this.LastSearchTime = blobInformation.ModifyDate;
        if (byId.IsNull)
          return;
        if (blobInformation.PackedFileSize == 0L)
          blobReader.CloseBlob();
        byte[] buffer = blobReader.ReadDataBlock();
        if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
        {
          using (MemoryStream inStream = new MemoryStream(buffer))
          {
            using (MemoryStream memoryStream = new MemoryStream(Convert.ToInt32(blobInformation.RealFileSize)))
            {
              ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
              this.ReadFromStream((Stream) memoryStream);
            }
          }
        }
        else
        {
          using (MemoryStream strm = new MemoryStream(buffer))
            this.ReadFromStream((Stream) strm);
        }
      }
      finally
      {
        blobReader.CloseBlob();
      }
    }
    else
      this.LastSearchTime = DateTime.UtcNow + (sample as DBSessionable).UserSession.TimeZoneOffset;
  }

  public void SaveToObject(IUserSession session)
  {
    IDBObject dbObject = session.GetObject(this.SampleID, false);
    if (dbObject == null)
      return;
    if (this.IsListModified)
    {
      IBlobWriter attributeById = dbObject.GetAttributeByID(NotifySamplesConst.ObjectsListAttr) as IBlobWriter;
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      using (MemoryStream outStream = new MemoryStream())
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          memoryStream.Seek(0L, SeekOrigin.Begin);
          using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
          {
            for (int index = 0; index < this.LastObjectsList.Count; ++index)
              binaryWriter.Write(this.LastObjectsList[index]);
            memoryStream.Position = 0L;
            service.PackStream((Stream) outStream, (Stream) memoryStream, 5);
            if (outStream.Length > (long) Consts.MaxShortBlobSize)
              throw new KernelException(string.Format(sc_13291.ssp_appserver_13292(), (object) dbObject.Caption, (object) this.LastObjectsList.Count, (object) outStream.Length));
            outStream.Seek(0L, SeekOrigin.Begin);
            BlobInformation blobInfo = new BlobInformation(memoryStream.Length, outStream.Length, this.LastSearchTime, string.Empty, ArcMethods.ZLibPacked, string.Empty);
            if (attributeById.OpenBlob(blobInfo, false))
              attributeById.WriteDataBlock(outStream.ToArray());
          }
        }
      }
      this.IsListModified = false;
    }
    dbObject.GetAttributeByID(NotifySamplesConst.CheckDateAttr).AsDateTime = this.LastSearchTime;
  }

  public void ProcessSample(NSResult dif, IUserSession session)
  {
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    ConditionStructure[] conditions = new ConditionStructure[this.Conditions.Length];
    this.Conditions.CopyTo((Array) conditions, 0);
    DataTable dataTable = session.GetObjectCollection(-1).Select(new DBRecordSetParams(conditions, new object[1]
    {
      (object) -2
    }, new object[1]{ (object) -2 }, new SortOrders[1]
    {
      SortOrders.ASC
    }));
    if (dataTable.Rows.Count > 0)
    {
      DataRow[] dataRowArray;
      if (Convert.ToInt64(dataTable.Rows[0][0]) < 0L)
      {
        int index = 0;
        while (index < dataTable.Rows.Count && Convert.ToInt64(dataTable.Rows[index][0]) < 0L)
          dataTable.Rows[index][0] = (object) -Convert.ToInt64(dataTable.Rows[index++][0]);
        dataTable.AcceptChanges();
        dataRowArray = dataTable.Select(string.Empty, dataTable.Columns[0].Caption);
      }
      else
        dataRowArray = dataTable.Select();
      int index1 = 0;
      int index2 = 0;
      while (index2 < dataRowArray.Length && index1 < this.LastObjectsList.Count)
      {
        long int64 = Convert.ToInt64(dataRowArray[index2][0]);
        if (int64 > this.LastObjectsList[index1])
        {
          if (this.Mode != NotifyMode.IncludedObjects)
            longList2.Add(this.LastObjectsList[index1]);
          ++index1;
        }
        else if (int64 < this.LastObjectsList[index1])
        {
          if (this.Mode != NotifyMode.ExcludedObjects)
            longList1.Add(int64);
          ++index2;
        }
        else
        {
          ++index2;
          ++index1;
        }
      }
      if (index1 >= this.LastObjectsList.Count)
      {
        if (this.Mode != NotifyMode.ExcludedObjects)
        {
          for (int index3 = index2; index3 < dataRowArray.Length; ++index3)
            longList1.Add(Convert.ToInt64(dataRowArray[index3][0]));
        }
      }
      else if (index2 >= dataRowArray.Length && this.Mode != NotifyMode.IncludedObjects)
      {
        for (int index4 = index1; index4 < this.LastObjectsList.Count; ++index4)
          longList2.Add(this.LastObjectsList[index4]);
      }
      if (longList1.Count <= 0 && longList2.Count <= 0)
        return;
      this.LastObjectsList.Clear();
      for (int index5 = 0; index5 < dataRowArray.Length; ++index5)
        this.LastObjectsList.Add(Convert.ToInt64(dataRowArray[index5][0]));
      this.IsListModified = true;
      dif.Samples.Add(new NSDifferences(longList1.ToArray(), longList2.ToArray(), this.SampleID, this.Name));
    }
    else
    {
      if (this.Mode == NotifyMode.IncludedObjects || this.LastObjectsList.Count <= 0)
        return;
      dif.Samples.Add(new NSDifferences(new long[0], this.LastObjectsList.ToArray(), this.SampleID, this.Name));
      this.LastObjectsList.Clear();
      this.IsListModified = true;
    }
  }
}

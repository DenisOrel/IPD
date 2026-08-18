// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.Common.ClientUtils
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.Common;

internal class ClientUtils
{
  private static List<FileDescription> getObjectFiles(long objectID)
  {
    List<FileDescription> objectFiles = new List<FileDescription>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        attributeByGuid.Index = index;
        using (ImChunkedStream aDestStream = new ImChunkedStream())
        {
          BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader.ReadData();
          BlobInformation blobInformation = blobProcReader.BlobInformation;
          if (!(Path.GetExtension(blobInformation.FileName).ToUpper() != ".PDF"))
          {
            aDestStream.Position = 0L;
            byte[] array = aDestStream.ToArray();
            if (array != null)
              objectFiles.Add(new FileDescription(dbObject.Caption, blobInformation.FileName, blobInformation.RealFileSize, blobInformation.FileType, blobInformation.ModifyDate, array));
          }
        }
      }
    }
    return objectFiles;
  }

  public static FileDescription FindAuthenticObjectFile(long objectID)
  {
    if (objectID == 0L)
      return FileDescription.Empty;
    FileDescription objectAuthenticalFile = ClientUtils.GetObjectAuthenticalFile(objectID);
    if (objectAuthenticalFile.FileType != FileTypes.ftUnknown)
      return objectAuthenticalFile;
    int num = (int) MessageBox.Show($"Отсутствует аутентичный файл PDF у объекта (Идентификатор версии объекта {objectID})");
    return FileDescription.Empty;
  }

  private static FileDescription GetObjectAuthenticalFile(long objectID)
  {
    FileDescription objectAuthenticalFile = FileDescription.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        attributeByGuid.Index = index;
        using (ImChunkedStream aDestStream = new ImChunkedStream())
        {
          BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader.ReadData();
          BlobInformation blobInformation = blobProcReader.BlobInformation;
          if (blobInformation.FileType == FileTypes.ftAuthentical)
          {
            if (!(Path.GetExtension(blobInformation.FileName).ToUpper() != ".PDF"))
            {
              aDestStream.Position = 0L;
              byte[] array = aDestStream.ToArray();
              if (array != null)
              {
                objectAuthenticalFile = new FileDescription(dbObject.Caption, blobInformation.FileName, blobInformation.RealFileSize, blobInformation.FileType, blobInformation.ModifyDate, array);
                return objectAuthenticalFile;
              }
            }
          }
        }
      }
    }
    return objectAuthenticalFile;
  }

  public static FileDescription FindObjectFile(long objectID)
  {
    if (objectID == 0L)
      return FileDescription.Empty;
    List<FileDescription> objectFiles = ClientUtils.getObjectFiles(objectID);
    using (new SessionKeeper())
    {
      if (objectFiles.Count == 0)
      {
        int num = (int) MessageBox.Show($"Отсутствует Файл PDF у объекта (Идентификатор версии объекта {objectID})");
        return FileDescription.Empty;
      }
      return objectFiles.Count == 1 ? objectFiles[0] : ClientUtils.showFileSelectionDialog(objectFiles);
    }
  }

  private static FileDescription showFileSelectionDialog(List<FileDescription> files)
  {
    using (SelectFileDialog selectFileDialog = new SelectFileDialog(files))
    {
      if (selectFileDialog.ShowDialog() == DialogResult.OK)
        return selectFileDialog.SelectedFile;
    }
    return FileDescription.Empty;
  }

  public static FileDescription ShowObjectSelectionDialog()
  {
    long[] numArray = SelectionWindow.SelectObjects("Выберите объект, для сравнения PDF файла", "Выберите объект, для сравнения PDF файла", (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Версии объектов", new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(HelperConsts.ObjtypeDocument)
    }), SelectionOptions.Default | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect);
    long objectID = 0;
    if (numArray != null && numArray.Length != 0)
      objectID = numArray[0];
    return ClientUtils.FindObjectFile(objectID);
  }
}

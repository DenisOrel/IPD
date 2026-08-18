
// Type: Intermech.Interfaces.WebPortal.TransferedObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using System;
using System.IO;
using System.Text;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Класс, описывающий передаваемый/принимаемый объект/связь
    /// между порталом и узлом информационной системы
    /// </summary>
    [Serializable]
    public class TransferedObject : ITransferedObject
    {
      /// <summary>Тип изменения (добавление/изменение/удаление)</summary>
      public ChangeType ChangesType;
      /// <summary>
      /// Флаг того, что передаваемый/принимаемый объект/связь обработан
      /// </summary>
      public bool Completed;

      /// <summary>Глобальный идентификатор экземпляра TransferedObject</summary>
      public string GUID { get; set; }

      /// <summary>Категория (объект/связь)</summary>
      public TransferedObjectCategory Category { get; set; }

      /// <summary>Список файлов с данными</summary>
      public string[] DataFiles { get; set; }

      /// <summary>
      /// Дополнительные данные, передаваемые с публикуемым объектом/связью
      /// </summary>
      public TransferedObjectTag Tag { get; set; }

      public TransferedObject() => this.GUID = Guid.NewGuid().ToString();

      public TransferedObject(Guid unitGuid, TransferedObjectCategory category)
        : this(unitGuid, category, (TransferedObjectTag) null)
      {
      }

      public TransferedObject(
        Guid unitGuid,
        TransferedObjectCategory category,
        TransferedObjectTag tag)
        : this(unitGuid, ChangeType.ctUpdate, category, tag)
      {
      }

      public TransferedObject(
        Guid unitGuid,
        ChangeType changesType,
        TransferedObjectCategory category,
        TransferedObjectTag tag)
      {
        this.GUID = unitGuid.ToString();
        this.ChangesType = changesType;
        this.Category = category;
        this.DataFiles = (string[]) null;
        this.Tag = tag;
        this.Completed = false;
      }

      public TransferedObject(ChangeType changesType, TransferedObjectCategory category)
        : this(changesType, category, (string[]) null, (TransferedObjectTag) null)
      {
      }

      public TransferedObject(
        ChangeType changesType,
        TransferedObjectCategory category,
        string[] dataFiles)
        : this(changesType, category, dataFiles, (TransferedObjectTag) null)
      {
      }

      public TransferedObject(
        ChangeType changesType,
        TransferedObjectCategory category,
        TransferedObjectTag tag)
        : this(changesType, category, (string[]) null, tag)
      {
      }

      public TransferedObject(
        ChangeType changesType,
        TransferedObjectCategory category,
        string[] dataFiles,
        TransferedObjectTag tag)
        : this()
      {
        this.ChangesType = changesType;
        this.Category = category;
        this.DataFiles = dataFiles;
        this.Tag = tag;
        this.Completed = false;
      }

      public TransferedObject(
        Guid unitGuid,
        ChangeType changesType,
        TransferedObjectCategory category,
        string[] dataFiles,
        TransferedObjectTag tag)
        : this(unitGuid, changesType, category, tag)
      {
        this.DataFiles = dataFiles;
        this.Completed = false;
      }

      protected static string GetString(int length, BinaryReader br)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(br.ReadChars(length));
        return stringBuilder.ToString();
      }

      protected void SaveGuid(BinaryWriter writer)
      {
        writer.Write(this.GUID.Length);
        writer.Write(this.GUID.ToCharArray());
      }

      public virtual void Save(BinaryWriter writer)
      {
        this.SaveGuid(writer);
        writer.Write((int) this.Category);
        writer.Write((int) this.ChangesType);
        writer.Write(this.Completed);
        if (this.DataFiles != null)
        {
          writer.Write(this.DataFiles.Length);
          for (int index = 0; index < this.DataFiles.Length; ++index)
          {
            writer.Write(this.DataFiles[index].Length);
            writer.Write(this.DataFiles[index].ToCharArray());
          }
        }
        else
          writer.Write(0);
        if (this.Tag != null)
        {
          writer.Write(1);
          this.Tag.Save(writer);
        }
        else
          writer.Write(0);
      }

      public byte[] Save()
      {
        using (ImChunkedStream output = new ImChunkedStream())
        {
          BinaryWriter writer = new BinaryWriter((Stream) output, Encoding.UTF8);
          try
          {
            this.Save(writer);
          }
          finally
          {
            writer.Flush();
          }
          return output.ToArray();
        }
      }

      public static string LoadGUID(byte[] bytes)
      {
        using (Stream input = (Stream) new MemoryStream(bytes))
        {
          input.Position = 0L;
          using (BinaryReader br = new BinaryReader(input, Encoding.UTF8))
          {
            int length = br.ReadInt32();
            return length > 0 ? TransferedObject.GetString(length, br) : Guid.NewGuid().ToString();
          }
        }
      }

      protected void LoadGuid(BinaryReader reader)
      {
        int length = reader.ReadInt32();
        this.GUID = length > 0 ? TransferedObject.GetString(length, reader) : Guid.NewGuid().ToString();
      }

      public virtual void Load(BinaryReader reader)
      {
        this.LoadGuid(reader);
        this.Category = (TransferedObjectCategory) reader.ReadInt32();
        this.ChangesType = (ChangeType) reader.ReadInt32();
        this.Completed = reader.ReadBoolean();
        int length1 = reader.ReadInt32();
        if (length1 > 0)
        {
          this.DataFiles = new string[length1];
          for (int index = 0; index < length1; ++index)
          {
            int length2 = reader.ReadInt32();
            if (length2 > 0)
              this.DataFiles[index] = TransferedObject.GetString(length2, reader);
          }
        }
        if (reader.ReadInt32() != 1)
          return;
        TransferedObjectTag transferedObjectTag = (TransferedObjectTag) null;
        switch (this.Category)
        {
          case TransferedObjectCategory.Object:
          case TransferedObjectCategory.ObjectLink:
          case TransferedObjectCategory.AutoTransfer:
          case TransferedObjectCategory.AttributesContainer:
          case TransferedObjectCategory.GroupObject:
          case TransferedObjectCategory.Receipt:
            transferedObjectTag = (TransferedObjectTag) new ObjectTag();
            break;
          case TransferedObjectCategory.Relation:
          case TransferedObjectCategory.GroupRelation:
            transferedObjectTag = (TransferedObjectTag) new RelationTag();
            break;
          case TransferedObjectCategory.Packet:
            transferedObjectTag = (TransferedObjectTag) new PacketTag();
            break;
          case TransferedObjectCategory.IncompleteRelation:
            transferedObjectTag = (TransferedObjectTag) new IncompleteRelationTag();
            break;
        }
        transferedObjectTag.Load(reader);
        this.Tag = transferedObjectTag;
      }

      public void Load(byte[] bytes)
      {
        using (Stream input = (Stream) new MemoryStream(bytes))
        {
          input.Position = 0L;
          using (BinaryReader reader = new BinaryReader(input, Encoding.UTF8))
            this.Load(reader);
        }
      }

      public static TransferedObject LoadFromFile(string fileName)
      {
        TransferedObject transferedObject = new TransferedObject();
        FileStream fileStream = new FileStream(fileName, FileMode.Open);
        try
        {
          byte[] numArray = new byte[fileStream.Length];
          fileStream.Read(numArray, 0, Convert.ToInt32(fileStream.Length));
          transferedObject.Load(numArray);
          return transferedObject;
        }
        catch (Exception ex)
        {
          throw new Exception($"Ошибка при чтении файла с данными '{fileName}': {ex.Message}");
        }
        finally
        {
          fileStream.Flush();
          fileStream.Close();
        }
      }

      public virtual TransferedObject Clone()
      {
        return new TransferedObject(new Guid(this.GUID), this.ChangesType, this.Category, this.DataFiles != null ? (string[]) this.DataFiles.Clone() : (string[]) null, this.Tag?.Clone())
        {
          Completed = this.Completed
        };
      }
    }
}

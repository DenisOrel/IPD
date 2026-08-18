
// Type: IMClient.UINotificationsStorage




using Intermech;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;


namespace IMClient
{
    internal sealed class UINotificationsStorage
    {
      private const string ConfigurationFileName = "UINotifications.dat";
      private DataContractSerializer itemsSerializer;

      public UINotificationsStorage()
      {
        this.itemsSerializer = new DataContractSerializer(typeof (UINotificationsStorage.SavedUINotificationsDocument), (IEnumerable<Type>) Type.EmptyTypes, (int) short.MaxValue, false, false, (IDataContractSurrogate) new UINotificationsStorage.UINotificationEventArgsSurrogates());
      }

      public ICollection<UINotification> LoadFromUserConfiguration()
      {
        using (Stream stream = this.LoadStreamFromUserConfiguration())
          return stream.Length != 0L ? ((UINotificationsStorage.SavedUINotificationsDocument) this.itemsSerializer.ReadObject(stream)).Items : (ICollection<UINotification>) new UINotification[0];
      }

      private Stream LoadStreamFromUserConfiguration()
      {
        ImChunkedStream aDestStream = new ImChunkedStream();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          new BlobProcReader(sessionKeeper.Session.Configurations.GetConfigAttribute("UINotifications.dat"), 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        aDestStream.Seek(0L, SeekOrigin.Begin);
        return (Stream) aDestStream;
      }

      public void SaveToUserConfiguration(ICollection<UINotification> eventArgsCollection)
      {
        UINotificationsStorage.SavedUINotificationsDocument graph = new UINotificationsStorage.SavedUINotificationsDocument(eventArgsCollection);
        using (ImChunkedStream imChunkedStream = new ImChunkedStream())
        {
          this.itemsSerializer.WriteObject((Stream) imChunkedStream, (object) graph);
          imChunkedStream.Flush();
          imChunkedStream.Seek(0L, SeekOrigin.Begin);
          this.SaveStreamToUserConfiguration((Stream) imChunkedStream);
        }
      }

      private void SaveStreamToUserConfiguration(Stream stream)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          BlobInformation aBlobInformation = new BlobInformation(stream.Length, 0L, DateTime.Now, "UINotifications.dat", ArcMethods.ZLibPacked, string.Empty);
          new BlobProcWriter(sessionKeeper.Session.Configurations.GetConfigAttribute("UINotifications.dat"), 0, aBlobInformation, stream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        }
      }

      [DataContract(Name = "Document", Namespace = "")]
      [Serializable]
      private sealed class SavedUINotificationsDocument
      {
        private ICollection<UINotification> items;

        public SavedUINotificationsDocument(ICollection<UINotification> items) => this.items = items;

        [DataMember]
        public ICollection<UINotification> Items
        {
          [DebuggerStepThrough] get => this.items;
          [DebuggerStepThrough] private set
          {
            this.items = value ?? throw new ArgumentNullException(nameof (value));
          }
        }

        [OnDeserialized]
        private void OnDeserializedMethod(StreamingContext context)
        {
          if (this.items != null && this.items.Count != 0)
            return;
          this.items = (ICollection<UINotification>) new UINotification[0];
        }
      }

      [DataContract(Namespace = "")]
      private sealed class ExceptionSurrogate
      {
        public ExceptionSurrogate(byte[] binaryData) => this.BinaryData = binaryData;

        [DataMember]
        public byte[] BinaryData { get; private set; }
      }

      private sealed class UINotificationEventArgsSurrogates : IDataContractSurrogate
      {
        private static readonly IFormatter binaryFormatter = UINotificationsStorage.UINotificationEventArgsSurrogates.CreateBinaryFormatter();

        public Type GetDataContractType(Type type)
        {
          return typeof (Exception).IsAssignableFrom(type) ? typeof (UINotificationsStorage.ExceptionSurrogate) : type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
          return obj is Exception exception ? (object) this.ConvertToExceptionSurrogate(exception) : obj;
        }

        private UINotificationsStorage.ExceptionSurrogate ConvertToExceptionSurrogate(
          Exception exception)
        {
          using (ImChunkedStream serializationStream = new ImChunkedStream())
          {
            UINotificationsStorage.UINotificationEventArgsSurrogates.binaryFormatter.Serialize((Stream) serializationStream, (object) exception);
            serializationStream.Flush();
            return new UINotificationsStorage.ExceptionSurrogate(serializationStream.ToArray());
          }
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
          return obj is UINotificationsStorage.ExceptionSurrogate exceptionSurrogate ? (object) this.ConvertToException(exceptionSurrogate) : obj;
        }

        private Exception ConvertToException(
          UINotificationsStorage.ExceptionSurrogate exceptionSurrogate)
        {
          using (MemoryStream serializationStream = new MemoryStream(exceptionSurrogate.BinaryData, false))
            return (Exception) UINotificationsStorage.UINotificationEventArgsSurrogates.binaryFormatter.Deserialize((Stream) serializationStream);
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
          return typeName.Equals("ExceptionSurrogate") ? typeof (Exception) : (Type) null;
        }

        public CodeTypeDeclaration ProcessImportedType(
          CodeTypeDeclaration typeDeclaration,
          CodeCompileUnit compileUnit)
        {
          return typeDeclaration;
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType) => (object) null;

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
          return (object) null;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
        }

        private static IFormatter CreateBinaryFormatter()
        {
          return (IFormatter) new BinaryFormatter()
          {
            AssemblyFormat = FormatterAssemblyStyle.Simple,
            FilterLevel = TypeFilterLevel.Full,
            TypeFormat = FormatterTypeStyle.TypesWhenNeeded
          };
        }
      }
    }
}


// Type: Intermech.Data.EntityDb.ServiceObjectAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Reflection;


namespace Intermech.Data.EntityDb
{
    [CLSCompliant(false)]
    public abstract class ServiceObjectAttribute : Attribute
    {
      private readonly Type creatorType;
      private readonly object[] args;
      private IServiceObjectCreator creator;

      public ServiceObjectAttribute(Type creatorType, params object[] args)
      {
        if (creatorType == (Type) null)
          throw new ArgumentNullException(nameof (creatorType));
        if (args == null)
          throw new ArgumentNullException(nameof (args));
        this.creatorType = creatorType;
        this.args = args;
      }

      public IServiceObjectCreator Creator
      {
        get
        {
          if (this.creator == null)
            this.creator = (IServiceObjectCreator) Activator.CreateInstance(this.creatorType, this.args);
          return this.creator;
        }
      }

      public interface IServiceObjectCreator
      {
        Type GetObjectType();

        object CreateInstance();
      }

      public sealed class NewObject : IServiceObjectCreator
        {
        private readonly Type objectType;
        private readonly object[] createArgs;

        public NewObject(Type objectType, params object[] createArgs)
        {
          if (objectType == (Type) null)
            throw new ArgumentNullException(nameof (objectType));
          if (createArgs == null)
            throw new ArgumentNullException(nameof (createArgs));
          this.objectType = objectType;
          this.createArgs = createArgs;
        }

        public Type GetObjectType() => this.objectType;

        public object CreateInstance() => Activator.CreateInstance(this.objectType, this.createArgs);
      }

      public sealed class StaticProperty : IServiceObjectCreator
        {
        private readonly PropertyInfo propInfo;
        private readonly Type objectType;

        public StaticProperty(Type type, string propertyName)
        {
          if (type == (Type) null)
            throw new ArgumentNullException(nameof (type));
          this.propInfo = propertyName != null ? type.GetProperty(propertyName) : throw new ArgumentNullException(nameof (propertyName));
          this.objectType = !(this.propInfo == (PropertyInfo) null) ? this.propInfo.PropertyType : throw new EntityDatabaseException($"No property '{propertyName}' found in type '{type.FullName}'.");
        }

        public Type GetObjectType() => this.objectType;

        public object CreateInstance() => this.propInfo.GetValue((object) null, (object[]) null);
      }

      public sealed class StaticMethod : IServiceObjectCreator
        {
        private readonly MethodInfo methodInfo;
        private readonly Type objectType;

        public StaticMethod(Type type, string methodName)
        {
          if (type == (Type) null)
            throw new ArgumentNullException(nameof (type));
          this.methodInfo = methodName != null ? type.GetMethod(methodName) : throw new ArgumentNullException(nameof (methodName));
          this.objectType = !(this.methodInfo == (MethodInfo) null) ? this.methodInfo.ReturnType : throw new ArgumentException($"No method '{methodName}' found in type '{type.FullName}'.", nameof (methodName));
          if (this.objectType == (Type) null)
            throw new ArgumentException($"Method '{methodName}' of type '{type.FullName}' has no return value.", nameof (methodName));
        }

        public Type GetObjectType() => this.objectType;

        public object CreateInstance() => this.methodInfo.Invoke((object) null, (object[]) null);
      }
    }
}

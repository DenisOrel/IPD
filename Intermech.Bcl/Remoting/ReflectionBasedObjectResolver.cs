
// Type: Intermech.Remoting.ReflectionBasedObjectResolver
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Reflection;


namespace Intermech.Remoting
{
    public sealed class ReflectionBasedObjectResolver : IRemotingObjectResolver
    {
      private readonly Type identityHolderType;
      private readonly MethodInfo resolveIdentityMethod;
      private readonly Type identityType;
      private readonly PropertyInfo tpOrObjectProperty;
      [ThreadStatic]
      private object[] resolveArgs;

      public ReflectionBasedObjectResolver()
      {
        this.identityHolderType = Type.GetType("System.Runtime.Remoting.IdentityHolder");
        this.resolveIdentityMethod = this.identityHolderType.GetMethod("ResolveIdentity", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
        this.identityType = Type.GetType("System.Runtime.Remoting.Identity");
        this.tpOrObjectProperty = this.identityType.GetProperty("TPOrObject", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty);
      }

      public MarshalByRefObject TryGetObject(string uri)
      {
        if (this.resolveArgs == null)
          this.resolveArgs = new object[1];
        this.resolveArgs[0] = (object) uri;
        object obj = this.resolveIdentityMethod.Invoke((object) null, this.resolveArgs);
        return obj != null ? (MarshalByRefObject) this.tpOrObjectProperty.GetValue(obj, (object[]) null) : (MarshalByRefObject) null;
      }
    }
}

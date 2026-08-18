// Decompiled with JetBrains decompiler
// Type: Intermech.Map2.ReflectClass
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Reflection;
using System.Reflection.Emit;


namespace Intermech.Map2
{
    public class ReflectClass
    {
      public static MethodDelegate GetMethodDelegate(
        Type owner,
        Type instance,
        string methodName,
        Type returnType,
        Type parameterType)
      {
        MethodInfo method = instance.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[1]
        {
          parameterType
        }, (ParameterModifier[]) null);
        if (method == (MethodInfo) null)
          return (MethodDelegate) null;
        DynamicMethod dynamicMethod = new DynamicMethod(methodName + "Wrapper", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, typeof (object), new Type[2]
        {
          typeof (object),
          typeof (object)
        }, owner, false);
        ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Unbox_Any, instance);
        ilGenerator.Emit(OpCodes.Ldarg_1);
        ilGenerator.Emit(OpCodes.Unbox_Any, parameterType);
        ilGenerator.Emit(OpCodes.Callvirt, method);
        ilGenerator.Emit(OpCodes.Box, returnType);
        ilGenerator.Emit(OpCodes.Ret);
        return (MethodDelegate) dynamicMethod.CreateDelegate(typeof (MethodDelegate));
      }
    }
}

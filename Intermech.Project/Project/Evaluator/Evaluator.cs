// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.Evaluator
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Project.Properties;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;

#nullable disable
namespace Intermech.Project.Evaluator;

public class Evaluator
{
  [CanBeNull]
  private static CSharpCodeProvider _codeProvider;
  [CanBeNull]
  private static CompilerParameters _compilerParameters;
  private static int _busyCounter;
  public static bool InDebug;

  [NotNull]
  protected static CSharpCodeProvider CodeProvider
  {
    get => Intermech.Project.Evaluator.Evaluator._codeProvider ?? (Intermech.Project.Evaluator.Evaluator._codeProvider = new CSharpCodeProvider());
  }

  [NotNull]
  protected static CompilerParameters CompilerParameters
  {
    get
    {
      if (Intermech.Project.Evaluator.Evaluator._compilerParameters == null)
      {
        Intermech.Project.Evaluator.Evaluator._compilerParameters = new CompilerParameters();
        Intermech.Project.Evaluator.Evaluator._compilerParameters.GenerateExecutable = false;
        Intermech.Project.Evaluator.Evaluator._compilerParameters.GenerateInMemory = true;
        StringCollection referencedAssemblies = Intermech.Project.Evaluator.Evaluator._compilerParameters.ReferencedAssemblies;
        foreach (AssemblyName referencedAssembly in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        {
          Assembly assembly = Assembly.ReflectionOnlyLoad(referencedAssembly.FullName);
          referencedAssemblies.Add(assembly.Location);
        }
        string location = Assembly.GetExecutingAssembly().Location;
        referencedAssemblies.Add(location);
      }
      return Intermech.Project.Evaluator.Evaluator._compilerParameters;
    }
  }

  public static bool Eval([NotNull] Task task, [NotNull] TaskFilter filter)
  {
    CompiledFilterInfo compiledFilterInfo = (CompiledFilterInfo) null;
    if (filter.Tag != null)
      compiledFilterInfo = filter.Tag as CompiledFilterInfo;
    if (compiledFilterInfo == null)
    {
      compiledFilterInfo = new CompiledFilterInfo();
      filter.Tag = (object) compiledFilterInfo;
    }
    return Intermech.Project.Evaluator.Evaluator.Eval((object) task, filter.Expressions, ref compiledFilterInfo.CompilerResults, ref compiledFilterInfo.CachedExpressionsHash);
  }

  public static bool Busy => Intermech.Project.Evaluator.Evaluator._busyCounter > 0;

  private static bool Eval(
    [CanBeNull] object obj,
    [NotNull] ExpressionList expressions,
    [CanBeNull] ref CompilerResults compilerResults,
    ref int cachedExpressionsHash)
  {
    if (compilerResults == null || cachedExpressionsHash != expressions.GetHashCode())
    {
      if (Intermech.Project.Evaluator.Evaluator.Busy)
        return false;
      Interlocked.Increment(ref Intermech.Project.Evaluator.Evaluator._busyCounter);
      try
      {
        string str1 = string.Empty;
        string str2 = string.Empty;
        bool flag = false;
        foreach (Expression expression in (List<Expression>) expressions)
        {
          if (expression.Property == null)
            throw new ArgumentException(string.Format(Resources.InvalidConditionErr, (object) expression));
          string str3 = "t." + expression.Property.Name;
          Type propType = expression.Property.PropType;
          object obj1 = expression.Value;
          if (obj1 is PossibleValue possibleValue)
            obj1 = possibleValue.Value;
          string str4;
          Type type1;
          if (obj1 is PropInfo propInfo)
          {
            str4 = "t." + propInfo.Name;
            type1 = propInfo.PropType;
          }
          else
          {
            type1 = obj1.GetType();
            if (obj1 is string)
            {
              str4 = obj1.ToString();
              if (str4.Length > 0)
              {
                if (str4[0] == '@')
                  str4 = str4.Remove(0, 1);
                else if (double.TryParse(str4, out double _))
                  type1 = typeof (double);
                else
                  str4 = $"\"{str4.Replace("\"", "\\\"")}\"";
              }
            }
            else if (obj1 is bool)
              str4 = Convert.ToBoolean(obj1) ? "true" : "false";
            else if (obj1 is DateTime dateTime)
            {
              str4 = $"DateTime.FromBinary({dateTime.ToBinary()})";
            }
            else
            {
              Type type2 = obj1.GetType();
              str4 = !type2.IsEnum ? obj1.ToString() : $"{type2.FullName}.{obj1}";
            }
          }
          if (str1 != string.Empty)
          {
            if (expression.GroupOperation == GroupOperation.And)
            {
              str1 += " && ";
            }
            else
            {
              if (str2 != string.Empty)
                str1 += str2;
              if (!flag)
              {
                str1 = $"({str1})";
                flag = true;
              }
              str1 += " || (";
              str2 = ")";
            }
          }
          string format = string.Empty;
          if (propType != type1)
          {
            if (propType == typeof (string))
              format = "Convert.ToString({0})";
            else if (propType == typeof (bool))
              format = "Convert.ToBoolean({0})";
            else if (propType == typeof (int))
              format = "Convert.ToInt32({0})";
            else if (propType == typeof (DateTime))
              format = "Convert.ToDateTime({0})";
            else if (propType.IsEnum)
              format = $"({propType.FullName}){{0}}";
          }
          if (format != string.Empty)
            str4 = string.Format(format, (object) str4);
          string str5 = expression.Operation.GetMapping(expression.Property) ?? string.Empty;
          string str6 = str5;
          string str7 = str5.Replace("%v%", str4);
          if (str7 != str6)
            str4 = (string) null;
          if (str7.Length > 1 && str7[0] == '!' && str7 != "!=")
          {
            str7 = str7.Substring(1);
            str3 = "!" + str3;
          }
          str1 = $"{str1} ({str3}{str7}{str4})";
        }
        if (str2 != string.Empty)
          str1 += str2;
        if (str1 == string.Empty)
        {
          compilerResults = (CompilerResults) null;
          return true;
        }
        string str8 = $"namespace ns{{using System;using Intermech.Project;class class1{{public static bool Evaluate(Task t){{return {str1};}}}}}} ";
        compilerResults = Intermech.Project.Evaluator.Evaluator.CodeProvider.CompileAssemblyFromSource(Intermech.Project.Evaluator.Evaluator.CompilerParameters, str8);
        cachedExpressionsHash = expressions.GetHashCode();
      }
      finally
      {
        Interlocked.Decrement(ref Intermech.Project.Evaluator.Evaluator._busyCounter);
      }
    }
    CompilerErrorCollection errors = compilerResults.Errors;
    if (errors.Count > 0)
    {
      string message = string.Format(Resources.InvalidConditionErr, (object) expressions);
      if (Intermech.Project.Evaluator.Evaluator.InDebug)
      {
        message += "\r\nCOMPILE:";
        foreach (CompilerError compilerError in (CollectionBase) errors)
          message = $"{message}\r\n{compilerError.ErrorText}";
      }
      throw new ArgumentException(message);
    }
    try
    {
      return Intermech.Diagnostics.Check.Is<bool>(compilerResults.CompiledAssembly.GetType("ns.class1").GetMethod("Evaluate").Invoke((object) null, new object[1]
      {
        obj
      }), "invokeResult");
    }
    catch (Exception ex)
    {
      string message = string.Format(Resources.InvalidConditionErr, (object) expressions);
      if (Intermech.Project.Evaluator.Evaluator.InDebug)
        message = $"{message}\r\nEXEC:\r\n{(object) ex}";
      throw new ArgumentException(message);
    }
  }
}

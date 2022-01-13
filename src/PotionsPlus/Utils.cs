using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotionsPlus
{
  public static class Utils
  {
    [UsedImplicitly]
    public static IEnumerable<string> AllNames(Type type)
    {
      foreach (var fieldInfo in type.GetFields().Where(f1 => f1.FieldType == typeof(string)))
      {
        yield return fieldInfo.GetValue(null).ToString();
      }
    }

    /// <summary>
    /// Writes Debug messages if a DEBUG Build
    /// </summary>
    /// <param name="msg">Message to print to the log.</param>
    [System.Diagnostics.Conditional("DEBUG")]
    public static void LogDebug(string msg)
    {
      try
      {
        Jotunn.Logger.LogDebug(msg);
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }
  }
}

using System;
using System.Reflection;
using System.Runtime.InteropServices;

public class Program
{
    public static void Main()
    {
        Console.Title = "HardAntiInvoke | Made by https://github.com/GabryB03/";

        if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
        {
            NotPassed("Anti Invoke - Entry Point Call - CHECK 1", "The entry point method was called by another assembly.");
        }
        else
        {
            Passed("Anti Invoke - Entry Point Call - CHECK 1", "The entry point method was not called by another assembly.");
        }

        if (typeof(Assembly).GetMethod("GetCallingAssembly").Invoke(null, null) != typeof(Assembly).GetMethod("GetExecutingAssembly").Invoke(null, null))
        {
            NotPassed("Anti Invoke - Entry Point Call - CHECK 2", "The entry point method was called by another assembly.");
        }
        else
        {
            Passed("Anti Invoke - Entry Point Call - CHECK 2", "The entry point method was not called by another assembly.");
        }

        if (Type.GetType("System.Reflection.Assembly").GetMethod("GetCallingAssembly").Invoke(null, null) != Type.GetType("System.Reflection.Assembly").GetMethod("GetExecutingAssembly").Invoke(null, null))
        {
            NotPassed("Anti Invoke - Entry Point Call - CHECK 3", "The entry point method was called by another assembly.");
        }
        else
        {
            Passed("Anti Invoke - Entry Point Call - CHECK 3", "The entry point method was not called by another assembly.");
        }

        if ((bool) Type.GetType("System.Reflection.Assembly").GetMethod("op_Inequality").Invoke(null, new object[] { Type.GetType("System.Reflection.Assembly").GetMethod("GetCallingAssembly").Invoke(null, null), Type.GetType("System.Reflection.Assembly").GetMethod("GetExecutingAssembly").Invoke(null, null) }))
        {
            NotPassed("Anti Invoke - Entry Point Call - CHECK 4", "The entry point method was called by another assembly.");
        }
        else
        {
            Passed("Anti Invoke - Entry Point Call - CHECK 4", "The entry point method was not called by another assembly.");
        }

        if (IsFunctionPatched(typeof(Assembly).GetMethod("GetCallingAssembly")))
        {
            NotPassed("Anti Invoke - Method Patches - CHECK 1", "The method 'System.Reflection.GetCallingAssembly' is patched.");
        }
        else
        {
            Passed("Anti Invoke - Method Patches - CHECK 1", "The method 'System.Reflection.GetCallingAssembly' is not patched.");
        }

        if (IsFunctionPatched(typeof(Assembly).GetMethod("GetExecutingAssembly")))
        {
            NotPassed("Anti Invoke - Method Patches - CHECK 2", "The method 'System.Reflection.GetExecutingAssembly' is patched.");
        }
        else
        {
            Passed("Anti Invoke - Method Patches - CHECK 2", "The method 'System.Reflection.GetExecutingAssembly' is not patched.");
        }

        if (IsFunctionPatched(Type.GetType("System.Reflection.Assembly").GetMethod("GetCallingAssembly")))
        {
            NotPassed("Anti Invoke - Method Patches - CHECK 3", "The method 'System.Reflection.GetCallingAssembly' is patched.");
        }
        else
        {
            Passed("Anti Invoke - Method Patches - CHECK 3", "The method 'System.Reflection.GetCallingAssembly' is not patched.");
        }

        if (IsFunctionPatched(Type.GetType("System.Reflection.Assembly").GetMethod("GetExecutingAssembly")))
        {
            NotPassed("Anti Invoke - Method Patches - CHECK 4", "The method 'System.Reflection.GetExecutingAssembly' is patched.");
        }
        else
        {
            Passed("Anti Invoke - Method Patches - CHECK 4", "The method 'System.Reflection.GetExecutingAssembly' is not patched.");
        }

        Console.ReadLine();
    }

    public static unsafe bool IsFunctionPatched(MethodBase methodBase)
    {
        IntPtr functionPointer = methodBase.MethodHandle.GetFunctionPointer();
        byte firstByte1 = Marshal.ReadByte(functionPointer);
        byte firstByte2 = *(byte*)(void*)functionPointer;
        return firstByte1 == 0xE9 || firstByte1 == 0x33 || firstByte1 == 255 || firstByte1 == 0x90 || firstByte1 == 0x00
            || firstByte2 == 0xE9 || firstByte2 == 0x33 || firstByte2 == 255 || firstByte2 == 0x90 || firstByte2 == 0x00;
    }

    public static void Passed(string type, string details)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("PASSED");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] [");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(type);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] " + details);
        Console.WriteLine();
    }

    public static void NotPassed(string type, string details)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("NOT PASSED");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] [");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(type);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] " + details);
        Console.WriteLine();
    }
}
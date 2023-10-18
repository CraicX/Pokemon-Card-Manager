//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  Utils
//
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PokeCardManager.Classes;
public static class Utils
{

    public static string Path(params string[] Args)
    {
        var rPath = "";

        if (Args.Length <= 0)
        {
            return rPath;
        }

        foreach (var pathSect in Args)
        {
            rPath += "\\" + pathSect;
        }

        return rPath.Replace("\\\\", "\\")[1..];

    }

    public static T Map<T, TU>(this T target, TU source)
    {
        // get property list of the target object.
        // this is a reflection extension which simply gets properties (CanWrite = true).
        var tprops = target.GetType().GetProperties();


        tprops.Where(x => x.CanWrite == true).ToList().ForEach(prop =>
        {
            // check whether source object has the the property
            var sp = source.GetType().GetProperty(prop.Name);
            if (sp != null)
            {
                // if yes, copy the value to the matching property
                var value = sp.GetValue(source, null);
                target.GetType().GetProperty(prop.Name).SetValue(target, value, null);
            }
        });

        return target;
    }

    

    public static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        //Now Create all of the directories
        foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        //Copy all the files & Replaces any files with the same name
        foreach (var newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }

    public static T Pop<T>(this List<T> ts)
    {
        var result = ts[0];

        ts.RemoveAt(0);

        return result;
    }



    public static string ReadResource(string name)
    {
        // Determine path
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = name;
        // Format: "{Namespace}.{Folder}.{filename}.{Extension}"

        resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

        using var stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        else
        {
            return "";
        }
    }

    public static byte[] ReadResourceBytes(string name)
    {
        // Determine path
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = name;
        resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));
        using var stream = assembly.GetManifestResourceStream(resourcePath);

        if (stream != null)
        {
            var bytes = new byte[stream.Length];
            var numBytesToRead = (int)stream.Length;
            var numBytesRead = 0;

            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                var n = stream.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                {
                    break;
                }

                numBytesRead += n;
                numBytesToRead -= n;
            }

            numBytesToRead = bytes.Length;

            return bytes;
        }
        else
        {

            return new byte[0];
        }
    }

    public static Stream StreamResource(string name)
    {
        // Determine path
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = name;

        // Format: "{Namespace}.{Folder}.{filename}.{Extension}"

        resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));
        var stream = assembly.GetManifestResourceStream(resourcePath);

        return stream ?? Stream.Null;

    }

    public static int GetListHashCode<TItem>(this IEnumerable<TItem> list)
    {
        if (list == null)
        {
            return 0;
        }

        const int seedValue = 0x2D2816FE;
        const int primeNumber = 397;
        return list.Aggregate(seedValue, (current, item) => (current * primeNumber) + (Equals(item, default(TItem)) ? 0 : item.GetHashCode()));
    }

}

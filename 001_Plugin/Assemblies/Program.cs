using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Remoting;
using SDKInterface;

namespace Assemblies
{
    public sealed class Program
    {
        public static void Main()
        {
            String addInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            String[] addInAssemblies = Directory.GetFiles(addInDir, "*.dll");

            var addInTypes = new List<IAddIn>();

            AppDomain ad2 = AppDomain.CreateDomain("PluginAD");

            //foreach (Assembly addInAssembly in addInAssemblies.Select(Assembly.LoadFrom))
            //{
            //    addInTypes.AddRange(addInAssembly.GetExportedTypes()
            //                            .Where(t => t.IsClass && typeof(IAddIn).IsAssignableFrom(t))
            //                            .Select(t => ad2.CreateInstanceAndUnwrap(addInAssembly.FullName, t.FullName)).Cast<IAddIn>());
            //}

            var q = from assembly in addInAssemblies.Select(Assembly.LoadFrom)
                    from type in assembly.GetExportedTypes()
                    where type.IsClass && typeof(IAddIn).IsAssignableFrom(type)
                    select ad2.CreateInstanceAndUnwrap(assembly.FullName, type.FullName);

            Console.WriteLine("Найдено {0} подключаемых модуля!", q.ToArray().Length);

            foreach (IAddIn t in q)
            {
                Console.WriteLine("Модуль {0} доступен по {1}", t,
                                  RemotingServices.IsTransparentProxy(t) ?
                                    "ссылке" : "значению");

                Console.WriteLine(t.DoSomething(5));
                Console.WriteLine(new string('-', 45));
            }
            AppDomain.Unload(ad2);
            Console.ReadKey();
        }
    }
}
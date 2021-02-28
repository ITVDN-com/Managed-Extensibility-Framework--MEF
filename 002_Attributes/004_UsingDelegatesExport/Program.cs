using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportAttributeCA
{

    public delegate void MyDel();
    
    public class MyExport
    {
        // Экспортом выступает метод
        [Export("ExportMethod", typeof(MyDel))]
        public void Method()
        {
            Console.WriteLine("Hello!");
        }
    }

    public class Importer
    {
        // Создаем импорт
        [Import("ExportMethod", typeof(MyDel))]
        public MyDel Method { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            // Создаем каталог, способный обнаружить части в типе
            TypeCatalog catalog = new TypeCatalog(typeof(MyExport));
            CompositionContainer container = new CompositionContainer(catalog);

            Importer importer = new Importer();

            // Компонуем экспорт и импорт
            container.ComposeParts(importer);

            importer.Method();
        }
    }
}

using M_I_FE.Metodos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace M_I_FE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type type = typeof(ECF_31.ECFItemRetencion);
            foreach (PropertyInfo property in type.GetProperties())
            {
                Console.WriteLine($"{property.Name} = Data[\"{property.Name}\"],");
            }

            // Iniciar el cronómetro
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo("E:\\Proyectos\\SIFA_FE\\M_I_FE\\Data\\DataSet.xlsx"));

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            LeerXlsx leerXlsx = new LeerXlsx();

            List<int> filas = leerXlsx.ObtenerXlsxSinEnviar(worksheet, worksheet.Dimension.End.Column);

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            foreach (var fila in filas)
            {
                list.Add(leerXlsx.ObtenerXlsxFilaDic(fila, worksheet, worksheet.Dimension.End.Column));
                Console.WriteLine(fila);
            }

            Metodos_XML.Generar_XML_ECF31(list[0]);

            // Detener el cronómetro
            stopwatch.Stop();
            // Mostrar el tiempo de ejecución
            Console.WriteLine("Tiempo de ejecución: {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}

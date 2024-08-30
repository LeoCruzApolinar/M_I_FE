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
            //Type type = typeof(ECF_47.ECFItemRetencion);
            //foreach (PropertyInfo property in type.GetProperties())
            //{
            //    Console.WriteLine($"{property.Name} = Data[\"{property.Name}\"],");
            //}


            // Iniciar el cronómetro
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            Metodos_XML.GenerarXML();

            // Detener el cronómetro
            //stopwatch.Stop();
            //// Mostrar el tiempo de ejecución
            //Console.WriteLine("Tiempo de ejecución: {0} ms", stopwatch.ElapsedMilliseconds);



            //Metodos_General metodos_General = new Metodos_General();
            
        }
    }
}

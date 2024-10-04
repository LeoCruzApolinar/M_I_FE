using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;

namespace M_I_FE.Metodos
{
    public class Metodos_General
    {
        public static int contador = 0;

        private static string parentDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."));



        public static string ObtenerRutaBase(string Carpeta, string File)
        {
            // Obtiene la ruta actual (puede ser reemplazada por la ruta real)
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;

            // Obtener la ruta del directorio base
            string baseDirectory = Directory.GetParent(currentPath).Parent.Parent.Parent.FullName;

            return Path.Combine(baseDirectory, Carpeta, File);
            
        }


        /// <summary>
        /// Guarda el contenido proporcionado en un archivo dentro de una carpeta especificada.
        /// </summary>
        /// <param name="content">El contenido que se guardará en el archivo.</param>
        /// <param name="carpeta">El nombre de la carpeta donde se guardará el archivo.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando la cadena de contenido es null.</exception>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no tiene los permisos necesarios para crear la carpeta o el archivo.</exception>
        /// <exception cref="IOException">Se lanza cuando ocurre un error de E/S al intentar guardar el archivo.</exception>
        public static void SaveContentToFile(string content, string carpeta)
        {

            string fileName = $"correctedXmlOutput_{contador}.xml";
            string tempDirectory = Path.Combine(ObtenerRutaBase("Temp", ""), fileName); // Asumiendo que quieres guardar un archivo específico
            string baseDirectory = ObtenerRutaBase("Temp", "");

            // Crear la carpeta si no existe
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content), "El contenido no puede ser null.");
            }

            try
            {
                // Guarda el contenido en el archivo especificado
                File.WriteAllText(tempDirectory, content);

                // Log descriptivo del archivo guardado
                Console.WriteLine($"Archivo guardado correctamente en: {tempDirectory}");

                // Incrementa el contador después de guardar
                contador++;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones para problemas de escritura en disco
                Console.WriteLine($"Error al guardar el archivo en {tempDirectory}: {ex.Message}");
            }
        }



        /// <summary>
        /// Elimina todos los espacios en blanco de la cadena de entrada.
        /// </summary>
        /// <param name="input">Cadena de entrada.</param>
        /// <returns>Cadena sin espacios en blanco.</returns>
        /// <exception cref="ArgumentNullException">Se lanza cuando la cadena de entrada es null.</exception>
        public static string EliminarEspacios(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "La cadena de entrada no puede ser null.");
            }

            return input.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Verifica si la cadena de entrada es un número decimal válido.
        /// </summary>
        /// <param name="str">Cadena de entrada.</param>
        /// <returns>True si la cadena es un número decimal válido; de lo contrario, false.</returns>
        public static bool EsNumero(string str)
        {
            return decimal.TryParse(str, out _);
        }

        /// <summary>
        /// Intenta analizar un valor decimal a partir de un diccionario dado una clave específica.
        /// </summary>
        /// <param name="data">Diccionario que contiene los datos.</param>
        /// <param name="key">Clave para buscar en el diccionario.</param>
        /// <returns>El valor decimal si se encuentra y se analiza correctamente; de lo contrario, el valor predeterminado de decimal.</returns>
        public static decimal TryParseDecimal(Dictionary<string, string> data, string key)
        {
            var val = data[key];
            if (decimal.TryParse(val, out var result))
            {
                result = Math.Round(result, 3);
                return result;
            }
            else
            {
                // Manejar el caso en que la clave no se encuentra o el valor no es un decimal válido
                // Puede lanzar una excepción, devolver un valor predeterminado o registrar una advertencia
                return default;
            }
        }

    }
}

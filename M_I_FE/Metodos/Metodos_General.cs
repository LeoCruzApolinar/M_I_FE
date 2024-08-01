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
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content), "El contenido no puede ser null.");
            }

            // Genera un UID aleatorio
            string uid = Guid.NewGuid().ToString();

            // Especifica la ruta completa de la carpeta. Utiliza Path.Combine para garantizar que la ruta sea válida.
            string folderPath = Configuration.GetSetting("CarpetaTemp", "Rutas", "ruta");

            // Crea la carpeta si no existe
            Directory.CreateDirectory(folderPath);

            // Especifica la ruta del archivo dentro de la carpeta
            //_{ uid}
            string fileName = $"correctedXmlOutput_{contador}.xml";
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                // Guarda el contenido en el archivo especificado
                File.WriteAllText(filePath, content);

                // Log descriptivo del archivo guardado
                Console.WriteLine($"Archivo guardado correctamente en: {filePath}");

                // Incrementa el contador después de guardar
                contador++;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones para problemas de escritura en disco
                Console.WriteLine($"Error al guardar el archivo en {filePath}: {ex.Message}");
            }

            //// Imprime la lista completa de archivos guardados
            //ListFilesInDirectory(folderPath);
        }

        /// <summary>
        /// Imprime la lista de todos los archivos guardados en la carpeta especificada.
        /// </summary>
        /// <param name="folderPath">La ruta completa de la carpeta.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando la ruta de la carpeta es null.</exception>
        /// <exception cref="DirectoryNotFoundException">Se lanza cuando la carpeta especificada no existe.</param>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no tiene los permisos necesarios para acceder a la carpeta.</exception>
        public static void ListFilesInDirectory(string folderPath)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath), "La ruta de la carpeta no puede ser null.");
            }

            try
            {
                string[] files = Directory.GetFiles(folderPath);
                Console.WriteLine("Archivos guardados:");

                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar los archivos en {folderPath}: {ex.Message}");
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
            if (data.ContainsKey(key) && decimal.TryParse(data[key], out var result))
            {
                return result;
            }
            else
            {
                // Manejar el caso en que la clave no se encuentra o el valor no es un decimal válido
                // Puede lanzar una excepción, devolver un valor predeterminado o registrar una advertencia
                return default;
            }
        }

        public class XmlCorrector
        {
            /// <summary>
            /// Elimina los saltos de línea de la cadena de entrada.
            /// </summary>
            /// <param name="input">Cadena de entrada.</param>
            /// <returns>Cadena sin saltos de línea.</returns>
            /// <exception cref="ArgumentNullException">Se lanza cuando la cadena de entrada es null.</exception>
            static string EliminarSaltosDeLinea(string input)
            {
                if (input == null)
                {
                    throw new ArgumentNullException(nameof(input), "La cadena de entrada no puede ser null.");
                }

                return input.Replace("\r", "").Replace("\n", "");
            }

            /// <summary>
            /// Corrige un XML eliminando etiquetas vacías y validándolo contra un esquema XSD opcional.
            /// </summary>
            /// <param name="inputXml">Cadena XML de entrada.</param>
            /// <param name="xsdPath">Ruta opcional al esquema XSD para validación.</param>
            /// <returns>Cadena XML corregida.</returns>
            public static string CorrectXml(string inputXml, string xsdPath = null)
            {
              
                try
                {
                    // Cargar el XML en un XDocument
                    XDocument xdoc = XDocument.Parse(inputXml, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);
                  
                    // Validar el XML contra el esquema XSD si se proporciona
                    if (!string.IsNullOrEmpty(xsdPath))
                    {
                        if (!ValidateXml(xdoc, xsdPath))
                        {
                            Console.WriteLine("El XML no es válido según el esquema XSD proporcionado.");
                        }
                    }

                    // Eliminar las etiquetas vacías
                    RemoveEmptyElements(xdoc.Root);

                    // Devolver el XML corregido como una cadena
                    return xdoc.ToString();
                }
                catch (XmlException ex)
                {
                    Console.WriteLine("Error parsing XML: " + ex.Message);
                    return null;
                }
            }

            /// <summary>
            /// Elimina recursivamente los elementos vacíos de un elemento XML.
            /// </summary>
            /// <param name="element">Elemento XML raíz.</param>
            private static void RemoveEmptyElements(XElement element)
            {
                // Crear una lista de elementos que deben ser eliminados
                var emptyElements = new List<XElement>();

                foreach (var el in element.Elements())
                {
                    // Eliminar etiquetas vacías recursivamente
                    RemoveEmptyElements(el);

                    // Agregar elementos vacíos a la lista
                    if (string.IsNullOrWhiteSpace(el.Value) && !el.HasElements)
                    {
                        emptyElements.Add(el);
                    }
                }

                // Eliminar los elementos vacíos
                foreach (var el in emptyElements)
                {
                    el.Remove();
                }
            }

            /// <summary>
            /// Valida un documento XML contra un esquema XSD.
            /// </summary>
            /// <param name="xdoc">Documento XML a validar.</param>
            /// <param name="xsdPath">Ruta al esquema XSD.</param>
            /// <returns>True si el documento XML es válido; de lo contrario, false.</returns>
            private static bool ValidateXml(XDocument xdoc, string xsdPath)
            {
                bool isValid = true;
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", xsdPath);

                xdoc.Validate(schemas, (o, e) =>
                {
                    Console.WriteLine($"{e.Severity}: {e.Message}");
                    isValid = false;
                });

                return isValid;
            }

            public static Dictionary<string, string> FindValuesNotInXml(Dictionary<string, string> sourceDict, XmlDocument xmlDoc)
            {
                // Resultado final
                Dictionary<string, string> missingValues = new Dictionary<string, string>();

                // Obtener el nodo raíz del XML
                XmlNode root = xmlDoc.DocumentElement;

                // Iterar sobre el diccionario a partir del índice 3
                int index = 0;
                foreach (var kvp in sourceDict)
                {
                    if (index >= 3) // Comenzar a partir del índice 3
                    {
                        string key = kvp.Key; // Clave del diccionario
                        string value = kvp.Value;

                        // Verificar si el valor no es null
                        if (!string.IsNullOrEmpty(value))
                        {
                            bool valueFound = false;

                            // Buscar el valor en todos los nodos del XML
                            foreach (XmlNode node in root.SelectNodes("//*"))
                            {
                                if (node.InnerText == value)
                                {
                                    valueFound = true;
                                    break;
                                }
                            }

                            // Si no se encuentra el valor en el XML, agregar al diccionario de resultados
                            if (!valueFound)
                            {
                                missingValues[key] = value;
                            }
                        }
                    }
                    index++;
                }

                return missingValues;
            }
        }

        public static class Configuration
        {
            private static string _filePath = @"..\..\..\Configuracion.xml";

            /// <summary>
            /// Obtiene el valor de una configuración específica a partir del archivo XML.
            /// </summary>
            /// <param name="settingName">El nombre de la configuración cuyo valor se desea obtener.</param>
            /// <param name="Elementos">El nombre del elemento principal.</param>
            /// <param name="Elemento">El nombre del elemento hijo.</param>
            /// <returns>El valor de la configuración si se encuentra, de lo contrario, null.</returns>
            public static string GetSetting(string settingName, string Elementos, string Elemento)
            {
                // Carga el documento XML desde la ruta especificada.
                XDocument doc = XDocument.Load(_filePath);

                // Busca el elemento principal.
                XElement parentElement = doc.Root.Element(Elementos);

                // Itera a través de todos los elementos de tipo "Elemento".
                foreach (var settingElement in parentElement?.Elements(Elemento) ?? Enumerable.Empty<XElement>())
                {
                    // Si el atributo "name" coincide con el nombre de la configuración, devuelve el valor del atributo "value".
                    if (settingElement.Attribute("name")?.Value == settingName)
                    {
                        string value = settingElement.Attribute("value")?.Value;

                        // Si el elemento principal es "Rutas", combina la ruta con el directorio padre.
                        if (Elementos == "Rutas")
                        {
                            string ruta = Path.Combine(parentDirectory, value);
                            return ruta;
                        }
                        else
                        {
                            return value;
                        }
                    }
                }

                // Si no se encuentra el elemento, devuelve null.
                return null;
            }

            /// <summary>
            /// Establece el valor de una configuración específica en el archivo XML.
            /// </summary>
            /// <param name="settingName">El nombre de la configuración cuyo valor se desea establecer.</param>
            /// <param name="settingValue">El nuevo valor de la configuración.</param>
            public static void SetSetting(string settingName, string settingValue)
            {
                // Carga el documento XML desde la ruta especificada.
                XDocument doc = XDocument.Load(_filePath);

                // Busca el elemento "settings".
                XElement settingsElement = doc.Root.Element("settings");

                if (settingsElement != null)
                {
                    // Busca el elemento "setting" dentro del elemento "settings".
                    XElement settingElement = settingsElement.Element("setting");

                    // Si se encuentra el elemento y su atributo "name" coincide con el nombre de la configuración, actualiza el valor del atributo "value".
                    if (settingElement != null && settingElement.Attribute("name")?.Value == settingName)
                    {
                        settingElement.SetAttributeValue("value", settingValue);
                    }
                    // Si no se encuentra el elemento, crea uno nuevo con los atributos especificados y lo agrega al elemento "settings".
                    else
                    {
                        XElement newSetting = new XElement("setting",
                            new XAttribute("name", settingName),
                            new XAttribute("value", settingValue)
                        );
                        settingsElement.Add(newSetting);
                    }
                }

                // Guarda los cambios en el archivo XML.
                doc.Save(_filePath);
            }
        }

    }
}

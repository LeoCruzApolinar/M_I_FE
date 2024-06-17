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
        public static string EliminarEspacios(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "La cadena de entrada no puede ser null.");
            }

            return input.Replace(" ", string.Empty);
        }
        public static bool EsNumero(string str)
        {
            return decimal.TryParse(str, out _);
        }

        public static decimal TryParseDecimal(Dictionary<string, string> data, string key)
        {
            if (data.ContainsKey(key) && decimal.TryParse(data[key], out var result))
            {
                return result;
            }
            else
            {
                // Handle the case where the key is not found or the value is not a valid decimal
                // You might want to throw an exception, return a default value, or log a warning
                return default;
            }
        }

        public class XmlCorrector
        {
            static string EliminarSaltosDeLinea(string input)
            {
                if (input == null)
                {
                    throw new ArgumentNullException(nameof(input), "La cadena de entrada no puede ser null.");
                }

                return input.Replace("\r", "").Replace("\n", "");
            }
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
                            Console.WriteLine("El XML no es válido según el esquema XSD proporcionado. \n\n\n");
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
        }
    }
}

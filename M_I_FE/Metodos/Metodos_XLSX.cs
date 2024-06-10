using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace M_I_FE.Metodos
{
    public class LeerXlsx
    {


        /// <summary>
        /// Obtiene una fila específica de una hoja de cálculo de Excel y la almacena en un diccionario.
        /// </summary>
        /// <param name="FilaDatos">Número de la fila de la cual se obtendrán los datos.</param>
        /// <param name="sheet">Hoja de cálculo de Excel de la cual se extraerán los datos.</param>
        /// <param name="DimensionColum">Número de columnas que contiene la hoja de cálculo.</param>
        /// <returns>
        /// Un diccionario que contiene los valores de la fila especificada. Las claves del diccionario son los valores de las celdas de la primera fila (encabezados) y los valores son los datos de la fila especificada.
        /// </returns>
        public Dictionary<string, string> ObtenerXlsxFilaDic(int FilaDatos, ExcelWorksheet sheet, int DimensionColum)
        {
            // Crear un diccionario con la capacidad especificada
            Dictionary<string, string> XlsxFila = new Dictionary<string, string>(DimensionColum);

            // Recorrer las columnas de la hoja de cálculo
            for (int i = 1; i <= DimensionColum; i++)
            {
                var key = sheet.Cells[1, i].Value?.ToString();
                string value = sheet.Cells[FilaDatos, i].Value?.ToString();

                // Verificar si el valor de la clave es nulo
                if (key != null && value!= null)
                {
                    if (value == "#e")
                    {
                        // Asigna null a value
                        value = null;
                    }

                    // Asignar el valor de la celda de la fila de datos al diccionario, usando como clave el valor de la celda de la primera fila (encabezado)
                    XlsxFila[key] = value;
                }
                else
                {
                    // Manejar el caso donde el valor de la celda es nulo
                    // Puedes optar por omitirlo o manejarlo de otra manera según sea necesario
                }
            }

            // Retornar el diccionario con los datos de la fila
            return XlsxFila;
        }

        /// <summary>
        /// Obtiene una lista de números de fila para las filas en las que el estado de envío es cero en una hoja de cálculo de Excel.
        /// </summary>
        /// <param name="sheet">La hoja de cálculo de Excel de la que se extraerán los datos.</param>
        /// <param name="DimensionColum">El número de columnas que contiene la hoja de cálculo.</param>
        /// <returns>
        /// Una lista de números de fila para las filas en las que el estado de envío es cero.
        /// </returns>
        public List<int> ObtenerXlsxSinEnviar(ExcelWorksheet sheet, int DimensionColum)
        {
            // Lista para almacenar los números de fila con estado de envío cero
            List<int> filasConEstadoEnvioCero = new List<int>();

            // Recorre las filas de la hoja de cálculo (asumiendo que los datos comienzan en la fila 2)
            for (int fila = 2; fila <= DimensionColum; fila++)
            {
                // Obtiene la celda que contiene el estado de envío en la fila actual (asumiendo que 'EstadoEnvio' está en la cuarta columna)
                var celdaEstadoEnvio = sheet.Cells[fila, 4];

                // Verifica si la celda tiene un valor y si ese valor es cero
                if (celdaEstadoEnvio.Value != null && celdaEstadoEnvio.Value.ToString() == "0")
                {
                    // Agrega el número de fila a la lista de filas con estado de envío cero
                    filasConEstadoEnvioCero.Add(fila);
                }
            }

            // Retorna la lista de números de fila con estado de envío cero
            return filasConEstadoEnvioCero;
        }



    }
}

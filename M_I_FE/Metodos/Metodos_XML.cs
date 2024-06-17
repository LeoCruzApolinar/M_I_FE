using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace M_I_FE.Metodos
{
    public static class Metodos_XML
    {
        public static void GenerarXML() 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo("E:\\Proyectos\\M_I_FE\\M_I_FE\\Datos\\DataFile.xlsx")))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                LeerXlsx leerXlsx = new LeerXlsx();

                List<int> filas = leerXlsx.ObtenerXlsxSinEnviar(worksheet, worksheet.Dimension.End.Column);

                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

                foreach (var fila in filas)
                {
                    list.Add(leerXlsx.ObtenerXlsxFilaDic(fila, worksheet, worksheet.Dimension.End.Column));
                }
                foreach (var fila in list)
                {
                    switch (fila["TipoeCF"])
                    {
                        case "31":
                            Metodo_F31_Generar.Generar_XML_ECF31(fila);
                            break;
                        case "32":
                            Metodo_F32_Generar.Generar_XML_ECF32(fila);
                            break;
                        case "33":
                            Metodo_F33_Generar.Generar_XML_ECF33(fila);
                            break;
                        case "34":
                            Metodo_F34_Generar.Generar_XML_ECF34(fila);
                            break;
                        case "41":
                            Metodo_F41_Generar.Generar_XML_ECF41(fila);
                            break;
                        case "43":
                            Metodo_F43_Generar.Generar_XML_ECF43(fila);
                            break;
                        case "44":
                            Metodo_F44_Generar.Generar_XML_ECF44(fila);
                            break;
                        case "45":
                            Metodo_F45_Generar.Generar_XML_ECF45(fila);
                            break;
                        case "46":
                            Metodo_F46_Generar.Generar_XML_ECF46(fila);
                            break;
                        case "47":
                            Metodo_F47_Generar.Generar_XML_ECF47(fila);
                            break;
                        default:
                            throw new ArgumentException("Tipo de método no válido");
                    }

                }

                Metodo_F31_Generar.Generar_XML_ECF31(list[0]);
            }
        }
    }
}

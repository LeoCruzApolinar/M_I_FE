using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace M_I_FE.Metodos
{
    public static class Metodos_XML
    {
        public class ObtenerValor()
        {
            public static ECF_31.TipoIngresosValidationType ObtenerTipoIngresos(string Valor)
            {
                ECF_31.TipoIngresosValidationType tipoIngresosValidationType = new ECF_31.TipoIngresosValidationType();
                switch (Valor)
                {
                    case "01" or "1":
                        tipoIngresosValidationType = ECF_31.TipoIngresosValidationType.Item01;
                        break;
                    case "02" or "2":
                        tipoIngresosValidationType = ECF_31.TipoIngresosValidationType.Item02;
                        break;
                    case "03" or "3":
                        tipoIngresosValidationType = ECF_31.TipoIngresosValidationType.Item03;
                        break;
                    case "04" or "4":
                        tipoIngresosValidationType = ECF_31.TipoIngresosValidationType.Item04;
                        break;
                    case "05" or "5":
                        tipoIngresosValidationType = ECF_31.TipoIngresosValidationType.Item05;
                        break;
                    case "06" or "6":
                        tipoIngresosValidationType = ECF_31.TipoIngresosValidationType.Item06;
                        break;

                }
                return tipoIngresosValidationType;
            }

            public static ECF_31.TipoPagoType ObtenerTipoPago(string Valor)
            {
                ECF_31.TipoPagoType tipoPagoType = new ECF_31.TipoPagoType();
                switch (Valor)
                {
                    case "01" or "1":
                        tipoPagoType = ECF_31.TipoPagoType.Item1;
                        break;
                    case "02" or "2":
                        tipoPagoType = ECF_31.TipoPagoType.Item2;
                        break;
                    case "03" or "3":
                        tipoPagoType = ECF_31.TipoPagoType.Item3;
                        break;

                }
                return tipoPagoType;
            }

            public static ECF_31.FormaPagoType ObtenerFormaPago(string Valor) 
            {
                ECF_31.FormaPagoType formaPagoType = new ECF_31.FormaPagoType();
                switch (Valor)
                {
                    case "01" or "1":
                        formaPagoType = ECF_31.FormaPagoType.Item1;
                        break;
                    case "02" or "2":
                        formaPagoType = ECF_31.FormaPagoType.Item2;
                        break;
                    case "03" or "3":
                        formaPagoType = ECF_31.FormaPagoType.Item3;
                        break;
                    case "04" or "4":
                        formaPagoType = ECF_31.FormaPagoType.Item4;
                        break;
                    case "05" or "5":
                        formaPagoType = ECF_31.FormaPagoType.Item5;
                        break;
                    case "06" or "6":
                        formaPagoType = ECF_31.FormaPagoType.Item6;
                        break;
                    case "07" or "7":
                        formaPagoType = ECF_31.FormaPagoType.Item7;
                        break;
                    case "08" or "8":
                        formaPagoType = ECF_31.FormaPagoType.Item8;
                        break;

                }
                return formaPagoType;

            }

            public static List<ECF_31.ECFEncabezadoIdDocFormaDePago> ObtenerTablaFormasPago(Dictionary<string, string> datos)
            {
                List<ECF_31.ECFEncabezadoIdDocFormaDePago> listaFormasPago = new List<ECF_31.ECFEncabezadoIdDocFormaDePago>();

                int indice = 1;
                while (true)
                {
                    string formaPagoKey = $"FormaPago[{indice}]";
                    string montoPagoKey = $"MontoPago[{indice}]";

                    if (datos.ContainsKey(formaPagoKey) && datos.ContainsKey(montoPagoKey))
                    {
                        if (Metodos_General.EsNumero(datos[formaPagoKey]) && Metodos_General.EsNumero(datos[montoPagoKey]))
                        {
                            ECF_31.ECFEncabezadoIdDocFormaDePago formaPago = new ECF_31.ECFEncabezadoIdDocFormaDePago
                            {
                                FormaPago = ObtenerFormaPago(datos[formaPagoKey]),
                                MontoPago = decimal.Parse(datos[montoPagoKey]),
                            };

                            listaFormasPago.Add(formaPago);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }
                }

                return listaFormasPago;
            }

            public static ECF_31.TipoCuentaPagoType ObtenerTipoCuentaPago(string Valor)
            {
                ECF_31.TipoCuentaPagoType tipoCuentaPagoType = new ECF_31.TipoCuentaPagoType();
                switch (Valor)
                {
                    case "CT":
                        tipoCuentaPagoType = ECF_31.TipoCuentaPagoType.CT;
                        break;
                    case "AH":
                        tipoCuentaPagoType = ECF_31.TipoCuentaPagoType.AH;
                        break;
                    case "OT":
                        tipoCuentaPagoType = ECF_31.TipoCuentaPagoType.OT;
                        break;

                }
                return tipoCuentaPagoType;

            }
        }
        public static void Generar_XML_ECF31(Dictionary<string, string> Data) 
        {
            var eCF_31 = new ECF_31.ECF();

            eCF_31.Encabezado = new ECF_31.ECFEncabezado()
            {
                IdDoc = new ECF_31.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_31.TipoeCFType.Item31,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    IndicadorEnvioDiferidoSpecified = (Data["IndicadorEnvioDiferido"] == "1"),
                    IndicadorMontoGravadoSpecified = (Data["IndicadorMontoGravado"] == "1"),
                    TipoIngresos = ObtenerValor.ObtenerTipoIngresos(Data["TipoIngresos"]),
                    TipoPago = ObtenerValor.ObtenerTipoPago(Data["TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValor.ObtenerTablaFormasPago(Data).ToArray(),
                    TipoCuentaPago = ObtenerValor.ObtenerTipoCuentaPago(Data["TipoCuentaPago"]),
                    NumeroCuentaPago = Data["NumeroCuentaPago"],
                    BancoPago = Data["BancoPago"],
                    FechaDesde = Data["FechaDesde"],
                    FechaHasta = Data["FechaHasta"],
                    TotalPaginas = Data["TotalPaginas"]
                },
                Emisor = new ECF_31.ECFEncabezadoEmisor() 
                {
                    RNCEmisor = Data["RNCEmisor"],
                    RazonSocialEmisor = Data["RazonSocialEmisor"],
                    NombreComercial = Data["NombreComercial"],
                    Sucursal = Data["Sucursal"],
                    DireccionEmisor = null,
                }
            };
            XmlSerializer serializer = new XmlSerializer(typeof(ECF_31.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_31);
                string xmlOutput = writer.ToString();
                Console.WriteLine(xmlOutput);
            }
            int a =0;
        }
    }
}

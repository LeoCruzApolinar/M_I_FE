using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace M_I_FE.Metodos
{
    public class Metodo_F47_Generar
    {
        public class ObtenerValorECF_47()
        {
            public static ECF_47.TipoMonedaType ObtenerMonedaPorCodigo(string codigo)
            {
                switch (codigo?.ToUpper()) // Convertimos el código a mayúsculas para asegurar la coincidencia
                {
                    case "BRL":
                        return ECF_47.TipoMonedaType.BRL;
                    case "CAD":
                        return ECF_47.TipoMonedaType.CAD;
                    case "CHF":
                        return ECF_47.TipoMonedaType.CHF;
                    case "CHY":
                        return ECF_47.TipoMonedaType.CHY;
                    case "XDR":
                        return ECF_47.TipoMonedaType.XDR;
                    case "DKK":
                        return ECF_47.TipoMonedaType.DKK;
                    case "EUR":
                        return ECF_47.TipoMonedaType.EUR;
                    case "GBP":
                        return ECF_47.TipoMonedaType.GBP;
                    case "JPY":
                        return ECF_47.TipoMonedaType.JPY;
                    case "NOK":
                        return ECF_47.TipoMonedaType.NOK;
                    case "SCP":
                        return ECF_47.TipoMonedaType.SCP;
                    case "SEK":
                        return ECF_47.TipoMonedaType.SEK;
                    case "USD":
                        return ECF_47.TipoMonedaType.USD;
                    case "VEF":
                        return ECF_47.TipoMonedaType.VEF;
                    case "HTG":
                        return ECF_47.TipoMonedaType.HTG;
                    case "MXN":
                        return ECF_47.TipoMonedaType.MXN;
                    default:
                        return default;
                }
            }


            public static dynamic ObtenerTipoGeneral<T>(string codigo)
            {
                if (codigo == null)
                {
                    return default(T);
                }

                foreach (var field in typeof(T).GetFields())
                {
                    var attribute = (System.Xml.Serialization.XmlEnumAttribute)Attribute.GetCustomAttribute(field, typeof(System.Xml.Serialization.XmlEnumAttribute));
                    if (attribute != null && attribute.Name == codigo)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                return default(T);
            }

            public static ECF_47.ECFEncabezadoIdDocFormaDePago[] ObtenerTablaFormasPago(Dictionary<string, string> datos)
            {
                List<ECF_47.ECFEncabezadoIdDocFormaDePago> listaFormasPago = new List<ECF_47.ECFEncabezadoIdDocFormaDePago>();

                int indice = 1;
                while (true)
                {
                    string formaPagoKey = $"FormaPago[{indice}]";
                    string montoPagoKey = $"MontoPago[{indice}]";

                    if (datos.ContainsKey(formaPagoKey) && datos.ContainsKey(montoPagoKey))
                    {
                        if (Metodos_General.EsNumero(datos[formaPagoKey]) && Metodos_General.EsNumero(datos[montoPagoKey]))
                        {
                            ECF_47.ECFEncabezadoIdDocFormaDePago formaPago = new ECF_47.ECFEncabezadoIdDocFormaDePago
                            {
                                FormaPago = ObtenerTipoGeneral<ECF_47.FormaPagoType>(datos[formaPagoKey]),
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

                return listaFormasPago.Count == 0 ? default : listaFormasPago.ToArray();
            }

            public static string[] ObtenerTablaTelefonoEmisor(Dictionary<string, string> datos)
            {
                List<string> listaTelefono = new List<string>();

                int indice = 1;
                while (true)
                {
                    string telefono = $"TelefonoEmisor[{indice}]";

                    if (datos.ContainsKey(telefono))
                    {
                        if (datos[telefono] != null)
                        {

                            listaTelefono.Add(datos[telefono]);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }
                }

                return listaTelefono.Count == 0 ? default : listaTelefono.ToArray();
            }

            public static ECF_47.ECFItem[] ObtenerDetalleItem(Dictionary<string, string> Data)
            {
                List<ECF_47.ECFItem> eCFItems = new List<ECF_47.ECFItem>();
                int indice = 1;

                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLinea[{indice}]"))
                    {
                        if (Data[$"NumeroLinea[{indice}]"] != null)
                        {

                            ECF_47.ECFItem eCFItem = new ECF_47.ECFItem()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TablaCodigosItem = ObtenerCodigosItems(Data, indice),
                                IndicadorFacturacion = ObtenerTipoGeneral<ECF_47.IndicadorFacturacionType>(Data[$"IndicadorFacturacion[{indice}]"]),
                                Retencion = new ECF_47.ECFItemRetencion()
                                {
                                    IndicadorAgenteRetencionoPercepcion = ObtenerTipoGeneral<ECF_47.IndicadorAgenteRetencionoPercepcionType>(Data[$"IndicadorAgenteRetencionoPercepcion[{indice}]"]),
                                    MontoISRRetenido = Metodos_General.TryParseDecimal(Data, $"MontoISRRetenido[{indice}]"),
                                   

                                },
                                NombreItem = Data[$"NombreItem[{indice}]"],
                                IndicadorBienoServicio = ObtenerTipoGeneral<ECF_47.IndicadorBienoServicioType>(Data[$"IndicadorBienoServicio[{indice}]"]),
                                DescripcionItem = Data[$"DescripcionItem[{indice}]"],
                                CantidadItem = Metodos_General.TryParseDecimal(Data, $"CantidadItem[{indice}]"),
                                UnidadMedida = ObtenerTipoGeneral<ECF_47.UnidadMedidaType>(Data[$"UnidadMedida[{indice}]"]),
                                UnidadMedidaSpecified = Metodos_General.EsNumero(Data[$"UnidadMedida[{indice}]"]),
                                OtraMonedaDetalle = new ECF_47.ECFItemOtraMonedaDetalle()
                                {
                                    PrecioOtraMoneda = Metodos_General.TryParseDecimal(Data, $"PrecioOtraMoneda[{indice}]"),
                                    PrecioOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"PrecioOtraMoneda[{indice}]"]),
                                    DescuentoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"DescuentoOtraMoneda[{indice}]"),
                                    DescuentoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"DescuentoOtraMoneda[{indice}]"]),
                                    RecargoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"RecargoOtraMoneda[{indice}]"),
                                    RecargoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"RecargoOtraMoneda[{indice}]"]),
                                    MontoItemOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoItemOtraMoneda[{indice}]"),
                                    MontoItemOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoItemOtraMoneda[{indice}]"]),
                                },
                                PrecioUnitarioItem = Metodos_General.TryParseDecimal(Data, $"PrecioUnitarioItem[{indice}]"),
                                MontoItem = Metodos_General.TryParseDecimal(Data, $"MontoItem[{indice}]"),
                            };
                            // Agregar el objeto a la lista
                            eCFItems.Add(eCFItem);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }

                return eCFItems.Count == 0 ? default : eCFItems.ToArray();
            }

            public static ECF_47.ECFItemCodigosItem[] ObtenerCodigosItems(Dictionary<string, string> Data, int ind)
            {
                List<ECF_47.ECFItemCodigosItem> eCFItemCodigosItems = new List<ECF_47.ECFItemCodigosItem>();

                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoCodigo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoCodigo[{ind}][{indice}]"] != null)
                        {

                            ECF_47.ECFItemCodigosItem eCFItemCodigosItem = new ECF_47.ECFItemCodigosItem()
                            {
                                TipoCodigo = Data[$"TipoCodigo[{ind}][{indice}]"],
                                CodigoItem = Data[$"CodigoItem[{ind}][{indice}]"],
                            };
                            // Agregar el objeto a la lista
                            eCFItemCodigosItems.Add(eCFItemCodigosItem);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }

                return eCFItemCodigosItems.Count == 0 ? default : eCFItemCodigosItems.ToArray();

            }

            public static ECF_47.ECFSubtotal[] ObtenerSubtotalesl(Dictionary<string, string> Data)
            {
                List<ECF_47.ECFSubtotal> ListaSubtotales = new List<ECF_47.ECFSubtotal>();
                int indice = 1;
                if (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal"))
                    {
                        if (Data[$"NumeroSubTotal"] != null)
                        {

                            ECF_47.ECFSubtotal data = new ECF_47.ECFSubtotal()
                            {
                                NumeroSubTotal = Data["NumeroSubTotal"],
                                DescripcionSubtotal = Data["DescripcionSubtotal"],
                                Orden = Data["Orden"],
                                SubTotalExento = Metodos_General.TryParseDecimal(Data, $"SubTotalExento"),
                                SubTotalExentoSpecified = Metodos_General.EsNumero(Data[$"SubTotalExento"]),
                                MontoSubTotal = Metodos_General.TryParseDecimal(Data, $"MontoSubTotal"),
                                MontoSubTotalSpecified = Metodos_General.EsNumero(Data[$"MontoSubTotal"]),
                                Lineas = Data["Lineas"],
                            };
                            // Agregar el objeto a la lista
                            ListaSubtotales.Add(data);

                        }
                        indice++;
                    }
                    else
                    {

                    }

                }



                return ListaSubtotales.Count == 0 ? default : ListaSubtotales.ToArray();
            }


            public static ECF_47.ECFPagina[] ObtenerECFPagina(Dictionary<string, string> Data)
            {
                List<ECF_47.ECFPagina> ListaECFPagina = new List<ECF_47.ECFPagina>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"PaginaNo[{indice}]"))
                    {
                        if (Data[$"PaginaNo[{indice}]"] != null)
                        {

                            ECF_47.ECFPagina data = new ECF_47.ECFPagina()
                            {
                                PaginaNo = Data[$"PaginaNo[{indice}]"],
                                NoLineaDesde = Data[$"NoLineaDesde[{indice}]"],
                                NoLineaHasta = Data[$"NoLineaHasta[{indice}]"],
                                SubtotalExentoPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalExentoPagina[{indice}]"),
                                SubtotalExentoPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalExentoPagina[{indice}]"]),
                                MontoSubtotalPagina = Metodos_General.TryParseDecimal(Data, $"MontoSubtotalPagina[{indice}]"),
                                MontoSubtotalPaginaSpecified = Metodos_General.EsNumero(Data[$"MontoSubtotalPagina[{indice}]"]),
                            };

                            // Agregar el objeto a la lista
                            ListaECFPagina.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return ListaECFPagina.Count == 0 ? default : ListaECFPagina.ToArray();
            }


        }
        public static void Generar_XML_ECF47(Dictionary<string, string> Data)
        {
            ECF_47.ECF eCF_47 = new ECF_47.ECF();
            eCF_47.Encabezado = new ECF_47.ECFEncabezado()
            {
                Version = Metodos_General.TryParseDecimal(Data, "Version"),
                IdDoc = new ECF_47.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_47.TipoeCFType.Item47,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    TipoPago = ObtenerValorECF_47.ObtenerTipoGeneral<ECF_47.TipoPagoType>(Data["TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValorECF_47.ObtenerTablaFormasPago(Data),
                    TipoCuentaPago = ObtenerValorECF_47.ObtenerTipoGeneral<ECF_47.TipoCuentaPagoType>(Data["TipoCuentaPago"]),
                    NumeroCuentaPago = Data["NumeroCuentaPago"],
                    BancoPago = Data["BancoPago"],
                    FechaDesde = Data["FechaDesde"],
                    FechaHasta = Data["FechaHasta"],
                    TotalPaginas = Data["TotalPaginas"]
                },
                Emisor = new ECF_47.ECFEncabezadoEmisor()
                {
                    RNCEmisor = Data["RNCEmisor"],
                    RazonSocialEmisor = Data["RazonSocialEmisor"],
                    NombreComercial = Data["NombreComercial"],
                    Sucursal = Data["Sucursal"],
                    DireccionEmisor = Data["DireccionEmisor"],
                    MunicipioSpecified = Data.ContainsKey("Municipio") && Data["Municipio"] != null,
                    Municipio = ObtenerValorECF_47.ObtenerTipoGeneral<ECF_47.ProvinciaMunicipioType>(Data["Municipio"]),
                    ProvinciaSpecified = Data.ContainsKey("Provincia") && Data["Provincia"] != null,
                    Provincia = ObtenerValorECF_47.ObtenerTipoGeneral<ECF_47.ProvinciaMunicipioType>(Data["Provincia"]),
                    TablaTelefonoEmisor = ObtenerValorECF_47.ObtenerTablaTelefonoEmisor(Data),
                    CorreoEmisor = Data["CorreoEmisor"],
                    WebSite = Data["WebSite"],
                    ActividadEconomica = Data["ActividadEconomica"],
                    NumeroFacturaInterna = Data["NumeroFacturaInterna"],
                    NumeroPedidoInterno = Data["NumeroPedidoInterno"],
                    InformacionAdicionalEmisor = Data["InformacionAdicionalEmisor"],
                    FechaEmision = Data["FechaEmision"],

                },
                Comprador = new ECF_47.ECFEncabezadoComprador()
                {
                    IdentificadorExtranjero = Data["IdentificadorExtranjero"],
                    RazonSocialComprador = Data["RazonSocialComprador"],
                },
                Transporte = new ECF_47.ECFEncabezadoTransporte()
                {
                    PaisDestino = Data["PaisDestino"],
                },
                Totales = new ECF_47.ECFEncabezadoTotales()
                {

                    MontoExento = Metodos_General.TryParseDecimal(Data, "MontoExento"),
                    MontoExentoSpecified = Metodos_General.EsNumero(Data["MontoExento"]),
                    MontoTotal = Metodos_General.TryParseDecimal(Data, "MontoTotal"),
                    MontoPeriodo = Metodos_General.TryParseDecimal(Data, "MontoPeriodo"),
                    MontoPeriodoSpecified = Metodos_General.EsNumero(Data["MontoPeriodo"]),
                    SaldoAnterior = Metodos_General.TryParseDecimal(Data, "SaldoAnterior"),
                    SaldoAnteriorSpecified = Metodos_General.EsNumero(Data["SaldoAnterior"]),
                    MontoAvancePago = Metodos_General.TryParseDecimal(Data, "MontoAvancePago"),
                    MontoAvancePagoSpecified = Metodos_General.EsNumero(Data["MontoAvancePago"]),
                    ValorPagar = Metodos_General.TryParseDecimal(Data, "ValorPagar"),
                    ValorPagarSpecified = Metodos_General.EsNumero(Data["ValorPagar"]),
                    TotalISRRetencion = Metodos_General.TryParseDecimal(Data, "TotalISRRetencion"),
                    TotalISRRetencionSpecified = Metodos_General.EsNumero(Data["TotalISRRetencion"]),
                },
                
                OtraMoneda = new ECF_47.ECFEncabezadoOtraMoneda()
                {
                    TipoMoneda = ObtenerValorECF_47.ObtenerMonedaPorCodigo(Data["TipoMoneda"]),
                    TipoMonedaSpecified = (Data["TipoMoneda"] != null),
                    TipoCambio = Metodos_General.TryParseDecimal(Data, "TipoCambio"),
                    TipoCambioSpecified = Metodos_General.EsNumero(Data["TipoCambio"]),
                    MontoExentoOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoExentoOtraMoneda"),
                    MontoExentoOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoExentoOtraMoneda"]),
                    MontoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoTotalOtraMoneda"),
                    MontoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoTotalOtraMoneda"]),

                },
            };
            eCF_47.DetallesItems = ObtenerValorECF_47.ObtenerDetalleItem(Data);
            eCF_47.Subtotales = ObtenerValorECF_47.ObtenerSubtotalesl(Data);
            eCF_47.Paginacion = ObtenerValorECF_47.ObtenerECFPagina(Data);
            eCF_47.InformacionReferencia = new ECF_47.ECFInformacionReferencia
            {
                NCFModificado = Data["NCFModificado"],
                RNCOtroContribuyente = Data["RNCOtroContribuyente"],
                FechaNCFModificado = Data["FechaNCFModificado"],
                CodigoModificacion = ObtenerValorECF_47.ObtenerTipoGeneral<ECF_47.CodigoModificacionType>(Data["CodigoModificacion"]),
                CodigoModificacionSpecified = Metodos_General.EsNumero(Data["CodigoModificacion"]),
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ECF_47.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_47);
                string xmlOutput = writer.ToString();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlOutput);
                Metodos_General.XmlCorrector.FindValuesNotInXml(Data, xmlDocument);
                string a = Metodos_General.XmlCorrector.CorrectXml(xmlOutput, "E:\\Proyectos\\M_I_FE\\M_I_FE\\XSD\\e-CF 47 v.1.0.xsd");
                Console.WriteLine(a);

                Metodos_General.SaveContentToFile(a, "47");
            }
        }
    }
}

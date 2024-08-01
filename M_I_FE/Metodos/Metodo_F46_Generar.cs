using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace M_I_FE.Metodos
{
    public class Metodo_F46_Generar
    {
        public class ObtenerValorECF_46()
        {
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

            public static ECF_46.ECFEncabezadoIdDocFormaDePago[] ObtenerTablaFormasPago(Dictionary<string, string> datos)
            {
                List<ECF_46.ECFEncabezadoIdDocFormaDePago> listaFormasPago = new List<ECF_46.ECFEncabezadoIdDocFormaDePago>();

                int indice = 1;
                while (true)
                {
                    string formaPagoKey = $"FormaPago[{indice}]";
                    string montoPagoKey = $"MontoPago[{indice}]";

                    if (datos.ContainsKey(formaPagoKey) && datos.ContainsKey(montoPagoKey))
                    {
                        if (Metodos_General.EsNumero(datos[formaPagoKey]) && Metodos_General.EsNumero(datos[montoPagoKey]))
                        {
                            ECF_46.ECFEncabezadoIdDocFormaDePago formaPago = new ECF_46.ECFEncabezadoIdDocFormaDePago
                            {
                                FormaPago = ObtenerTipoGeneral<ECF_46.FormaPagoType>(datos[formaPagoKey]),
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

            public static ECF_46.ECFItem[] ObtenerDetalleItem(Dictionary<string, string> Data)
            {
                List<ECF_46.ECFItem> eCFItems = new List<ECF_46.ECFItem>();
                int indice = 1;

                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLinea[{indice}]"))
                    {
                        if (Data[$"NumeroLinea[{indice}]"] != null)
                        {

                            ECF_46.ECFItem eCFItem = new ECF_46.ECFItem()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TablaCodigosItem = ObtenerCodigosItems(Data, indice),
                                IndicadorFacturacion = ObtenerTipoGeneral<ECF_46.IndicadorFacturacionType>(Data[$"IndicadorFacturacion[{indice}]"]),
                                NombreItem = Data[$"NombreItem[{indice}]"],
                                IndicadorBienoServicio = ObtenerTipoGeneral<ECF_46.IndicadorBienoServicioType>(Data[$"IndicadorBienoServicio[{indice}]"]),
                                DescripcionItem = Data[$"DescripcionItem[{indice}]"],
                                CantidadItem = Metodos_General.TryParseDecimal(Data, $"CantidadItem[{indice}]"),
                                UnidadMedida = ObtenerTipoGeneral<ECF_46.UnidadMedidaType>(Data[$"UnidadMedida[{indice}]"]),
                                UnidadMedidaSpecified = Metodos_General.EsNumero(Data[$"UnidadMedida[{indice}]"]),
                                FechaElaboracion = Data[$"FechaElaboracion[{indice}]"],
                                FechaVencimientoItem = Data[$"FechaVencimientoItem[{indice}]"],
                                Mineria = new ECF_46.ECFItemMineria() 
                                {
                                    PesoNetoKilogramo = Metodos_General.TryParseDecimal(Data, $"PesoNetoKilogramo[{indice}]"),
                                    PesoNetoKilogramoSpecified = Metodos_General.EsNumero(Data[$"PesoNetoKilogramo[{indice}]"]),
                                    PesoNetoMineria = Metodos_General.TryParseDecimal(Data, $"PesoNetoMineria[{indice}]"),
                                    PesoNetoMineriaSpecified = Metodos_General.EsNumero(Data[$"PesoNetoMineria[{indice}]"]),
                                    TipoAfiliacion = ObtenerTipoGeneral<ECF_46.TipoAfiliacionType>(Data[$"TipoAfiliacion[{indice}]"]),
                                    TipoAfiliacionSpecified = Metodos_General.EsNumero(Data[$"TipoAfiliacion[{indice}]"]),
                                    Liquidacion = ObtenerTipoGeneral<ECF_46.LiquidacionType>(Data[$"Liquidacion[{indice}]"]),
                                    LiquidacionSpecified = Metodos_General.EsNumero(Data[$"Liquidacion[{indice}]"]),

                                },
                                PrecioUnitarioItem = Metodos_General.TryParseDecimal(Data, $"PrecioUnitarioItem[{indice}]"),
                                DescuentoMonto = Metodos_General.TryParseDecimal(Data, $"DescuentoMonto[{indice}]"),
                                DescuentoMontoSpecified = Metodos_General.EsNumero(Data[$"DescuentoMonto[{indice}]"]),
                                TablaSubDescuento = ObtenerTablaECFItemSubDescuento(Data),
                                RecargoMonto = Metodos_General.TryParseDecimal(Data, $"RecargoMonto[{indice}]"),
                                RecargoMontoSpecified = Metodos_General.EsNumero(Data[$"RecargoMonto[{indice}]"]),
                                TablaSubRecargo = ObtenerTablaECFItemSubRecargo(Data),
                                OtraMonedaDetalle = new ECF_46.ECFItemOtraMonedaDetalle()
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

            public static ECF_46.ECFItemCodigosItem[] ObtenerCodigosItems(Dictionary<string, string> Data, int ind)
            {
                List<ECF_46.ECFItemCodigosItem> eCFItemCodigosItems = new List<ECF_46.ECFItemCodigosItem>();

                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoCodigo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoCodigo[{ind}][{indice}]"] != null)
                        {

                            ECF_46.ECFItemCodigosItem eCFItemCodigosItem = new ECF_46.ECFItemCodigosItem()
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

            public static ECF_46.ECFItemSubDescuento[] ObtenerTablaECFItemSubDescuento(Dictionary<string, string> Data)
            {
                List<ECF_46.ECFItemSubDescuento> ListaECFItemSubDescuento = new List<ECF_46.ECFItemSubDescuento>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubDescuento[{indice}]"))
                    {
                        if (Data[$"TipoSubDescuento[{indice}]"] != null)
                        {

                            ECF_46.ECFItemSubDescuento data = new ECF_46.ECFItemSubDescuento()
                            {
                                TipoSubDescuento = ObtenerTipoGeneral<ECF_46.ECFItemSubDescuento>(Data[$"TipoSubDescuento[{indice}]"]),
                                SubDescuentoPorcentaje = Metodos_General.TryParseDecimal(Data, $"SubDescuentoPorcentaje[{indice}]"),
                                SubDescuentoPorcentajeSpecified = Metodos_General.EsNumero(Data[$"SubDescuentoPorcentaje[{indice}]"]),
                                MontoSubDescuento = Metodos_General.TryParseDecimal(Data, $"MontoSubDescuento[{indice}]"),
                                MontoSubDescuentoSpecified = Metodos_General.EsNumero(Data[$"MontoSubDescuento[{indice}]"]),
                            };
                            // Agregar el objeto a la lista
                            ListaECFItemSubDescuento.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return ListaECFItemSubDescuento.Count == 0 ? default : ListaECFItemSubDescuento.ToArray();
            }

            public static ECF_46.ECFItemSubRecargo[] ObtenerTablaECFItemSubRecargo(Dictionary<string, string> Data)
            {
                List<ECF_46.ECFItemSubRecargo> ListaECFItemSubRecargo = new List<ECF_46.ECFItemSubRecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubRecargo[{indice}]"))
                    {
                        if (Data[$"TipoSubRecargo[{indice}]"] != null)
                        {

                            ECF_46.ECFItemSubRecargo data = new ECF_46.ECFItemSubRecargo()
                            {
                                TipoSubRecargo = ObtenerTipoGeneral<ECF_46.ECFItemSubRecargo>($"TipoSubRecargo[{indice}]"),
                                SubRecargoPorcentaje = Metodos_General.TryParseDecimal(Data, $"SubRecargoPorcentaje[{indice}]"),
                                SubRecargoPorcentajeSpecified = Metodos_General.EsNumero(Data[$"SubRecargoPorcentaje[{indice}]"]),
                                MontoSubRecargo = Metodos_General.TryParseDecimal(Data, $"MontoSubRecargo[{indice}]"),
                                MontoSubRecargoSpecified = Metodos_General.EsNumero(Data[$"MontoSubRecargo[{indice}]"]),
                            };
                            // Agregar el objeto a la lista
                            ListaECFItemSubRecargo.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return ListaECFItemSubRecargo.Count == 0 ? default : ListaECFItemSubRecargo.ToArray();
            }

            public static ECF_46.ECFSubtotal[] ObtenerSubtotalesl(Dictionary<string, string> Data)
            {
                List<ECF_46.ECFSubtotal> ListaSubtotales = new List<ECF_46.ECFSubtotal>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal[{indice}]"))
                    {
                        if (Data[$"NumeroSubTotal[{indice}]"] != null)
                        {

                            ECF_46.ECFSubtotal data = new ECF_46.ECFSubtotal()
                            {
                                NumeroSubTotal = Data["NumeroSubTotal"],
                                DescripcionSubtotal = Data["DescripcionSubtotal"],
                                Orden = Data["Orden"],
                                SubTotalMontoGravadoTotal = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoTotal[{indice}]"),
                                SubTotalMontoGravadoTotalSpecified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoTotal[{indice}]"]),
                                SubTotalMontoGravadoI3 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI3[{indice}]"),
                                SubTotalMontoGravadoI3Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI3[{indice}]"]),
                                SubTotaITBIS = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS[{indice}]"),
                                SubTotaITBISSpecified = Metodos_General.EsNumero(Data[$"SubTotaITBIS[{indice}]"]),
                                SubTotaITBIS3 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS3[{indice}]"),
                                SubTotaITBIS3Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS3[{indice}]"]),
                                MontoSubTotal = Metodos_General.TryParseDecimal(Data, $"MontoSubTotal[{indice}]"),
                                MontoSubTotalSpecified = Metodos_General.EsNumero(Data[$"MontoSubTotal[{indice}]"]),
                                Lineas = Data["Lineas"],
                            };
                            // Agregar el objeto a la lista
                            ListaSubtotales.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return ListaSubtotales.Count == 0 ? default : ListaSubtotales.ToArray();
            }

            public static ECF_46.ECFDescuentoORecargo[] ObtenerDescuentosORecargos(Dictionary<string, string> Data)
            {
                List<ECF_46.ECFDescuentoORecargo> ListaDescuentosORecargos = new List<ECF_46.ECFDescuentoORecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal[{indice}]"))
                    {
                        if (Data[$"NumeroSubTotal[{indice}]"] != null)
                        {

                            ECF_46.ECFDescuentoORecargo data = new ECF_46.ECFDescuentoORecargo()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TipoAjuste = ObtenerTipoGeneral<ECF_46.TipoAjusteType>($"TipoAjuste[{indice}]"),
                                DescripcionDescuentooRecargo = Data["DescripcionDescuentooRecargo"],
                                TipoValor = ObtenerTipoGeneral<ECF_46.TipoDescuentoRecargoType>($"TipoValor[{indice}]"),
                                TipoValorSpecified = Metodos_General.EsNumero(Data[$"TipoValor[{indice}]"]),
                                ValorDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"ValorDescuentooRecargo[{indice}]"),
                                ValorDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"ValorDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargo[{indice}]"),
                                MontoDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargoOtraMoneda[{indice}]"),
                                MontoDescuentooRecargoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargoOtraMoneda[{indice}]"]),
                                IndicadorFacturacionDescuentooRecargo = ObtenerTipoGeneral<ECF_46.IndicadorFacturacionDRType>($"IndicadorFacturacionDescuentooRecargo[{indice}]"),
                                IndicadorFacturacionDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"IndicadorFacturacionDescuentooRecargo[{indice}]"]),
                            };
                            // Agregar el objeto a la lista
                            ListaDescuentosORecargos.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return ListaDescuentosORecargos.Count == 0 ? default : ListaDescuentosORecargos.ToArray();
            }

            public static ECF_46.ECFPagina[] ObtenerECFPagina(Dictionary<string, string> Data)
            {
                List<ECF_46.ECFPagina> ListaECFPagina = new List<ECF_46.ECFPagina>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal[{indice}]"))
                    {
                        if (Data[$"NumeroSubTotal[{indice}]"] != null)
                        {

                            ECF_46.ECFPagina data = new ECF_46.ECFPagina()
                            {
                                PaginaNo = Data[$"PaginaNo[{indice}]"],
                                NoLineaDesde = Data[$"NoLineaDesde[{indice}]"],
                                NoLineaHasta = Data[$"NoLineaHasta[{indice}]"],
                                SubtotalMontoGravadoPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoGravadoPagina[{indice}]"),
                                SubtotalMontoGravadoPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoGravadoPagina[{indice}]"]),
                                SubtotalMontoGravado3Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoGravado3Pagina[{indice}]"),
                                SubtotalMontoGravado3PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoGravado3Pagina[{indice}]"]),
                                SubtotalItbisPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalItbisPagina[{indice}]"),
                                SubtotalItbisPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalItbisPagina[{indice}]"]),
                                SubtotalItbis3Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalItbis3Pagina[{indice}]"),
                                SubtotalItbis3PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalItbis3Pagina[{indice}]"]),
                                MontoSubtotalPagina = Metodos_General.TryParseDecimal(Data, $"MontoSubtotalPagina[{indice}]"),
                                MontoSubtotalPaginaSpecified = Metodos_General.EsNumero(Data[$"MontoSubtotalPagina[{indice}]"]),
                                SubtotalMontoNoFacturablePagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoNoFacturablePagina[{indice}]"),
                                SubtotalMontoNoFacturablePaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoNoFacturablePagina[{indice}]"]),
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
        public static void Generar_XML_ECF46(Dictionary<string, string> Data)
        {
            ECF_46.ECF eCF_46 = new ECF_46.ECF();
            eCF_46.Encabezado = new ECF_46.ECFEncabezado()
            {
                Version = Metodos_General.TryParseDecimal(Data, "Version"),
                IdDoc = new ECF_46.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_46.TipoeCFType.Item46,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    IndicadorEnvioDiferidoSpecified = (Data["IndicadorEnvioDiferido"] == "1" || Data["IndicadorEnvioDiferido"] == "0"),
                    IndicadorEnvioDiferido = ECF_46.IndicadorEnvioDiferidoType.Item1,
                    TipoIngresos = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.TipoIngresosValidationType>(Data["TipoIngresos"]),
                    TipoPago = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.TipoPagoType>(Data["TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValorECF_46.ObtenerTablaFormasPago(Data),
                    TipoCuentaPago = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.TipoCuentaPagoType>(Data["TipoCuentaPago"]),
                    NumeroCuentaPago = Data["NumeroCuentaPago"],
                    BancoPago = Data["BancoPago"],
                    FechaDesde = Data["FechaDesde"],
                    FechaHasta = Data["FechaHasta"],
                    TotalPaginas = Data["TotalPaginas"]
                },
                Emisor = new ECF_46.ECFEncabezadoEmisor()
                {
                    RNCEmisor = Data["RNCEmisor"],
                    RazonSocialEmisor = Data["RazonSocialEmisor"],
                    NombreComercial = Data["NombreComercial"],
                    Sucursal = Data["Sucursal"],
                    DireccionEmisor = Data["DireccionEmisor"],
                    MunicipioSpecified = Data.ContainsKey("Municipio") && Data["Municipio"] != null,
                    Municipio = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.ProvinciaMunicipioType>(Data["Municipio"]),
                    ProvinciaSpecified = Data.ContainsKey("Provincia") && Data["Provincia"] != null,
                    Provincia = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.ProvinciaMunicipioType>(Data["Provincia"]),
                    TablaTelefonoEmisor = ObtenerValorECF_46.ObtenerTablaTelefonoEmisor(Data),
                    CorreoEmisor = Data["CorreoEmisor"],
                    WebSite = Data["WebSite"],
                    ActividadEconomica = Data["ActividadEconomica"],
                    CodigoVendedor = Data["CodigoVendedor"],
                    NumeroFacturaInterna = Data["NumeroFacturaInterna"],
                    NumeroPedidoInterno = Data["NumeroPedidoInterno"],
                    ZonaVenta = Data["ZonaVenta"],
                    RutaVenta = Data["RutaVenta"],
                    InformacionAdicionalEmisor = Data["InformacionAdicionalEmisor"],
                    FechaEmision = Data["FechaEmision"],

                },
                Comprador = new ECF_46.ECFEncabezadoComprador()
                {
                    RNCComprador = Data["RNCComprador"],
                    IdentificadorExtranjero = Data["IdentificadorExtranjero"],
                    RazonSocialComprador = Data["RazonSocialComprador"],
                    ContactoComprador = Data["ContactoComprador"],
                    CorreoComprador = Data["CorreoComprador"],
                    DireccionComprador = Data["DireccionComprador"],
                    MunicipioCompradorSpecified = Data.ContainsKey("MunicipioComprador") && Data["MunicipioComprador"] != null,
                    MunicipioComprador = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.ProvinciaMunicipioType>(Data["MunicipioComprador"]),
                    ProvinciaCompradorSpecified = Data.ContainsKey("ProvinciaComprador") && Data["ProvinciaComprador"] != null,
                    ProvinciaComprador = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.ProvinciaMunicipioType>(Data["ProvinciaComprador"]),
                    PaisComprador = Data["PaisComprador"],
                    FechaEntrega = Data["FechaEntrega"],
                    ContactoEntrega = Data["ContactoEntrega"],
                    DireccionEntrega = Data["DireccionEntrega"],
                    TelefonoAdicional = Data["TelefonoAdicional"],
                    FechaOrdenCompra = Data["FechaOrdenCompra"],
                    NumeroOrdenCompra = Data["NumeroOrdenCompra"],
                    CodigoInternoComprador = Data["CodigoInternoComprador"],
                    ResponsablePago = Data["ResponsablePago"],
                    InformacionAdicionalComprador = Data["InformacionAdicionalComprador"],
                },
                InformacionesAdicionales = new ECF_46.ECFEncabezadoInformacionesAdicionales
                {
                    FechaEmbarque = Data["FechaEmbarque"],
                    NumeroEmbarque = Data["NumeroEmbarque"],
                    NumeroContenedor = Data["NumeroContenedor"],
                    NumeroReferencia = Data["NumeroReferencia"],
                    NombrePuertoEmbarque = Data["NombrePuertoEmbarque"],
                    CondicionesEntrega = Data["CondicionesEntrega"],
                    TotalFob = Metodos_General.TryParseDecimal(Data, "TotalFob"),
                    TotalFobSpecified = Metodos_General.EsNumero(Data["TotalFob"]),
                    Seguro = Metodos_General.TryParseDecimal(Data, "Seguro"),
                    SeguroSpecified = Metodos_General.EsNumero(Data["Seguro"]),
                    Flete = Metodos_General.TryParseDecimal(Data, "Flete"),
                    FleteSpecified = Metodos_General.EsNumero(Data["Flete"]),
                    OtrosGastos = Metodos_General.TryParseDecimal(Data, "OtrosGastos"),
                    OtrosGastosSpecified = Metodos_General.EsNumero(Data["OtrosGastos"]),
                    TotalCif = Metodos_General.TryParseDecimal(Data, "TotalCif"),
                    TotalCifSpecified = Metodos_General.EsNumero(Data["TotalCif"]),
                    RegimenAduanero = Data["RegimenAduanero"],
                    NombrePuertoSalida = Data["NombrePuertoSalida"],
                    NombrePuertoDesembarque = Data["NombrePuertoDesembarque"],
                    PesoBruto = Metodos_General.TryParseDecimal(Data, "PesoBruto"),
                    PesoBrutoSpecified = Metodos_General.EsNumero(Data["PesoBruto"]),
                    PesoNeto = Metodos_General.TryParseDecimal(Data, "PesoNeto"),
                    PesoNetoSpecified = Metodos_General.EsNumero(Data["PesoNeto"]),
                    UnidadPesoBruto = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.UnidadMedidaType>(Data["UnidadPesoBruto"]),
                    UnidadPesoBrutoSpecified = Metodos_General.EsNumero(Data["UnidadPesoBruto"]),
                    UnidadPesoNeto = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.UnidadMedidaType>(Data["UnidadPesoNeto"]),
                    UnidadPesoNetoSpecified = Metodos_General.EsNumero(Data["UnidadPesoNeto"]),
                    CantidadBulto = Metodos_General.TryParseDecimal(Data, "CantidadBulto"),
                    CantidadBultoSpecified = Metodos_General.EsNumero(Data["CantidadBulto"]),
                    UnidadBulto = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.UnidadMedidaType>(Data["UnidadBulto"]),
                    UnidadBultoSpecified = Metodos_General.EsNumero(Data["UnidadBulto"]),
                    VolumenBulto = Metodos_General.TryParseDecimal(Data, "VolumenBulto"),
                    VolumenBultoSpecified = Metodos_General.EsNumero(Data["VolumenBulto"]),
                    UnidadVolumen = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.UnidadMedidaType>(Data["UnidadVolumen"]),
                    UnidadVolumenSpecified = Metodos_General.EsNumero(Data["UnidadVolumen"]),
                },
                Transporte = new ECF_46.ECFEncabezadoTransporte()
                {
                    ViaTransporte =ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.ViaTransporteType>("ViaTransporte") ,
                    ViaTransporteSpecified = Metodos_General.EsNumero(Data["ViaTransporte"]),
                    PaisOrigen = Data["PaisOrigen"],
                    DireccionDestino = Data["DireccionDestino"],
                    PaisDestino = Data["PaisDestino"],
                    RNCIdentificacionCompaniaTransportista = Data["RNCIdentificacionCompaniaTransportista"],
                    NombreCompaniaTransportista = Data["NombreCompaniaTransportista"],
                    NumeroViaje = Data["NumeroViaje"],
                    Conductor = Data["Conductor"],
                    DocumentoTransporte = Data["DocumentoTransporte"],
                    Ficha = Data["Ficha"],
                    Placa = Data["Placa"],
                    RutaTransporte = Data["RutaTransporte"],
                    ZonaTransporte = Data["ZonaTransporte"],
                    NumeroAlbaran = Data["NumeroAlbaran"],
                },
                Totales = new ECF_46.ECFEncabezadoTotales()
                {
                    MontoGravadoTotal = Metodos_General.TryParseDecimal(Data, "MontoGravadoTotal"),
                    MontoGravadoTotalSpecified = Metodos_General.EsNumero(Data["MontoGravadoTotal"]),
                    MontoGravadoI3 = Metodos_General.TryParseDecimal(Data, "MontoGravadoI3"),
                    MontoGravadoI3Specified = Metodos_General.EsNumero(Data["MontoGravadoI3"]),
                    ITBIS3 = Data["ITBIS3"],
                    TotalITBIS = Metodos_General.TryParseDecimal(Data, "TotalITBIS"),
                    TotalITBISSpecified = Metodos_General.EsNumero(Data["TotalITBIS"]),
                    TotalITBIS3 = Metodos_General.TryParseDecimal(Data, "TotalITBIS3"),
                    TotalITBIS3Specified = Metodos_General.EsNumero(Data["TotalITBIS3"]),
                    MontoTotal = Metodos_General.TryParseDecimal(Data, "MontoTotal"),
                    MontoNoFacturable = Metodos_General.TryParseDecimal(Data, "MontoNoFacturable"),
                    MontoNoFacturableSpecified = Metodos_General.EsNumero(Data["MontoNoFacturable"]),
                    MontoPeriodo = Metodos_General.TryParseDecimal(Data, "MontoPeriodo"),
                    MontoPeriodoSpecified = Metodos_General.EsNumero(Data["MontoPeriodo"]),
                    SaldoAnterior = Metodos_General.TryParseDecimal(Data, "SaldoAnterior"),
                    SaldoAnteriorSpecified = Metodos_General.EsNumero(Data["SaldoAnterior"]),
                    MontoAvancePago = Metodos_General.TryParseDecimal(Data, "MontoAvancePago"),
                    MontoAvancePagoSpecified = Metodos_General.EsNumero(Data["MontoAvancePago"]),
                    ValorPagar = Metodos_General.TryParseDecimal(Data, "ValorPagar"),
                    ValorPagarSpecified = Metodos_General.EsNumero(Data["ValorPagar"]),
                },
                OtraMoneda = new ECF_46.ECFEncabezadoOtraMoneda()
                {
                    TipoMoneda = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.TipoMonedaType>(Data["TipoMoneda"]),
                    TipoMonedaSpecified = Metodos_General.EsNumero(Data["TipoMoneda"]),
                    TipoCambio = Metodos_General.TryParseDecimal(Data, "TipoCambio"),
                    TipoCambioSpecified = Metodos_General.EsNumero(Data["TipoCambio"]),
                    MontoGravadoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoGravadoTotalOtraMoneda"),
                    MontoGravadoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoGravadoTotalOtraMoneda"]),
                    MontoGravado3OtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoGravado3OtraMoneda"),
                    MontoGravado3OtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoGravado3OtraMoneda"]),
                    TotalITBISOtraMoneda = Metodos_General.TryParseDecimal(Data, "TotalITBISOtraMoneda"),
                    TotalITBISOtraMonedaSpecified = Metodos_General.EsNumero(Data["TotalITBISOtraMoneda"]),
                    TotalITBIS3OtraMoneda = Metodos_General.TryParseDecimal(Data, "TotalITBIS3OtraMoneda"),
                    TotalITBIS3OtraMonedaSpecified = Metodos_General.EsNumero(Data["TotalITBIS3OtraMoneda"]),
                    MontoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoTotalOtraMoneda"),
                    MontoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoTotalOtraMoneda"]),

                },
            };
            eCF_46.DetallesItems = ObtenerValorECF_46.ObtenerDetalleItem(Data);
            eCF_46.Subtotales = ObtenerValorECF_46.ObtenerSubtotalesl(Data);
            eCF_46.DescuentosORecargos = ObtenerValorECF_46.ObtenerDescuentosORecargos(Data);
            eCF_46.Paginacion = ObtenerValorECF_46.ObtenerECFPagina(Data);
            eCF_46.InformacionReferencia = new ECF_46.ECFInformacionReferencia
            {
                NCFModificado = Data["NCFModificado"],
                RNCOtroContribuyente = Data["RNCOtroContribuyente"],
                FechaNCFModificado = Data["FechaNCFModificado"],
                CodigoModificacion = ObtenerValorECF_46.ObtenerTipoGeneral<ECF_46.CodigoModificacionType>(Data["CodigoModificacion"]),
                CodigoModificacionSpecified = Metodos_General.EsNumero(Data["CodigoModificacion"]),
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ECF_46.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_46);
                string xmlOutput = writer.ToString();

                string a = Metodos_General.XmlCorrector.CorrectXml(xmlOutput, "E:\\Proyectos\\M_I_FE\\M_I_FE\\XSD\\e-CF 46 v.1.0.xsd");
                Console.WriteLine(a);

                Metodos_General.SaveContentToFile(a, "46");
            }
        }
    }
}

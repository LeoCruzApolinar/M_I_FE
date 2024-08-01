using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace M_I_FE.Metodos
{
    public class Metodo_F41_Generar
    {
        public class ObtenerValorECF_41()
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

            public static ECF_41.ECFEncabezadoIdDocFormaDePago[] ObtenerTablaFormasPago(Dictionary<string, string> datos)
            {
                List<ECF_41.ECFEncabezadoIdDocFormaDePago> listaFormasPago = new List<ECF_41.ECFEncabezadoIdDocFormaDePago>();

                int indice = 1;
                while (true)
                {
                    string formaPagoKey = $"FormaPago[{indice}]";
                    string montoPagoKey = $"MontoPago[{indice}]";

                    if (datos.ContainsKey(formaPagoKey) && datos.ContainsKey(montoPagoKey))
                    {
                        if (Metodos_General.EsNumero(datos[formaPagoKey]) && Metodos_General.EsNumero(datos[montoPagoKey]))
                        {
                            ECF_41.ECFEncabezadoIdDocFormaDePago formaPago = new ECF_41.ECFEncabezadoIdDocFormaDePago
                            {
                                FormaPago = ObtenerTipoGeneral<ECF_41.FormaPagoType>(datos[formaPagoKey]),
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

            public static ECF_41.ECFItem[] ObtenerDetalleItem(Dictionary<string, string> Data)
            {
                List<ECF_41.ECFItem> eCFItems = new List<ECF_41.ECFItem>();
                int indice = 1;

                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLinea[{indice}]"))
                    {
                        if (Data[$"NumeroLinea[{indice}]"] != null)
                        {

                            ECF_41.ECFItem eCFItem = new ECF_41.ECFItem()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TablaCodigosItem = ObtenerCodigosItems(Data, indice),
                                IndicadorFacturacion = ObtenerTipoGeneral<ECF_41.IndicadorFacturacionType>(Data[$"IndicadorFacturacion[{indice}]"]),
                                Retencion = new ECF_41.ECFItemRetencion()
                                {
                                    IndicadorAgenteRetencionoPercepcion = ObtenerTipoGeneral<ECF_41.IndicadorAgenteRetencionoPercepcionType>(Data[$"IndicadorAgenteRetencionoPercepcion[{indice}]"]),
                                    MontoITBISRetenido = Metodos_General.TryParseDecimal(Data, $"MontoITBISRetenido[{indice}]"),
                                    MontoITBISRetenidoSpecified = Metodos_General.EsNumero(Data[$"MontoITBISRetenido[{indice}]"]),
                                    MontoISRRetenido = Metodos_General.TryParseDecimal(Data, $"MontoISRRetenido[{indice}]"),
                                    MontoISRRetenidoSpecified = Metodos_General.EsNumero(Data[$"MontoISRRetenido[{indice}]"]),

                                },
                                NombreItem = Data[$"NombreItem[{indice}]"],
                                IndicadorBienoServicio = ObtenerTipoGeneral<ECF_41.IndicadorBienoServicioType>(Data[$"IndicadorBienoServicio[{indice}]"]),
                                DescripcionItem = Data[$"DescripcionItem[{indice}]"],
                                CantidadItem = Metodos_General.TryParseDecimal(Data, $"CantidadItem[{indice}]"),
                                UnidadMedida = ObtenerTipoGeneral<ECF_41.UnidadMedidaType>(Data[$"UnidadMedida[{indice}]"]),
                                UnidadMedidaSpecified = Metodos_General.EsNumero(Data[$"UnidadMedida[{indice}]"]),
                                FechaElaboracion = Data[$"FechaElaboracion[{indice}]"],
                                FechaVencimientoItem = Data[$"FechaVencimientoItem[{indice}]"],
                                PrecioUnitarioItem = Metodos_General.TryParseDecimal(Data, $"PrecioUnitarioItem[{indice}]"),
                                DescuentoMonto = Metodos_General.TryParseDecimal(Data, $"DescuentoMonto[{indice}]"),
                                DescuentoMontoSpecified = Metodos_General.EsNumero(Data[$"DescuentoMonto[{indice}]"]),
                                TablaSubDescuento = ObtenerTablaECFItemSubDescuento(Data, indice),
                                RecargoMonto = Metodos_General.TryParseDecimal(Data, $"RecargoMonto[{indice}]"),
                                RecargoMontoSpecified = Metodos_General.EsNumero(Data[$"RecargoMonto[{indice}]"]),
                                TablaSubRecargo = ObtenerTablaECFItemSubRecargo(Data, indice),
                                OtraMonedaDetalle = new ECF_41.ECFItemOtraMonedaDetalle()
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

            public static ECF_41.ECFItemCodigosItem[] ObtenerCodigosItems(Dictionary<string, string> Data, int ind)
            {
                List<ECF_41.ECFItemCodigosItem> eCFItemCodigosItems = new List<ECF_41.ECFItemCodigosItem>();

                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoCodigo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoCodigo[{ind}][{indice}]"] != null)
                        {

                            ECF_41.ECFItemCodigosItem eCFItemCodigosItem = new ECF_41.ECFItemCodigosItem()
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

            public static ECF_41.ECFItemSubDescuento[] ObtenerTablaECFItemSubDescuento(Dictionary<string, string> Data, int ind)
            {
                List<ECF_41.ECFItemSubDescuento> ListaECFItemSubDescuento = new List<ECF_41.ECFItemSubDescuento>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubDescuento[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoSubDescuento[{ind}][{indice}]"] != null)
                        {

                            ECF_41.ECFItemSubDescuento data = new ECF_41.ECFItemSubDescuento()
                            {
                                TipoSubDescuento = ObtenerTipoGeneral<ECF_41.TipoDescuentoRecargoType>(Data[$"TipoSubDescuento[{ind}][{indice}]"]),
                                SubDescuentoPorcentaje = Metodos_General.TryParseDecimal(Data, $"SubDescuentoPorcentaje[{ind}][{indice}]"),
                                SubDescuentoPorcentajeSpecified = Metodos_General.EsNumero(Data[$"SubDescuentoPorcentaje[{ind}][{indice}]"]),
                                MontoSubDescuento = Metodos_General.TryParseDecimal(Data, $"MontoSubDescuento[{ind}][{indice}]"),
                                MontoSubDescuentoSpecified = Metodos_General.EsNumero(Data[$"MontoSubDescuento[{ind}][{indice}]"]),
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

            public static ECF_41.ECFItemSubRecargo[] ObtenerTablaECFItemSubRecargo(Dictionary<string, string> Data, int ind)
            {
                List<ECF_41.ECFItemSubRecargo> ListaECFItemSubRecargo = new List<ECF_41.ECFItemSubRecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubRecargo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoSubRecargo[{ind}][{indice}]"] != null)
                        {
                            Console.WriteLine(Data[$"MontosubRecargo[{ind}][{indice}]"]);
                            ECF_41.ECFItemSubRecargo data = new ECF_41.ECFItemSubRecargo()
                            {
                                TipoSubRecargo = ObtenerTipoGeneral<ECF_41.TipoDescuentoRecargoType>($"TipoSubRecargo[{ind}][{indice}]"),
                                SubRecargoPorcentaje = Metodos_General.TryParseDecimal(Data, $"SubRecargoPorcentaje[{ind}][{indice}]"),
                                SubRecargoPorcentajeSpecified = Metodos_General.EsNumero(Data[$"SubRecargoPorcentaje[{ind}][{indice}]"]),
                                MontoSubRecargoSpecified = Metodos_General.EsNumero(Data[$"MontosubRecargo[{ind}][{indice}]"]),
                                MontoSubRecargo = Metodos_General.TryParseDecimal(Data, $"MontosubRecargo[{ind}][{indice}]"),

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

            public static ECF_41.ECFSubtotal[] ObtenerSubtotalesl(Dictionary<string, string> Data)
            {
                List<ECF_41.ECFSubtotal> ListaSubtotales = new List<ECF_41.ECFSubtotal>();
                int indice = 1;
                if (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal"))
                    {
                        if (Data[$"NumeroSubTotal"] != null)
                        {

                            ECF_41.ECFSubtotal data = new ECF_41.ECFSubtotal()
                            {
                                NumeroSubTotal = Data["NumeroSubTotal"],
                                DescripcionSubtotal = Data["DescripcionSubtotal"],
                                Orden = Data["Orden"],
                                SubTotalMontoGravadoTotal = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoTotal"),
                                SubTotalMontoGravadoTotalSpecified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoTotal"]),
                                SubTotalMontoGravadoI1 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI1"),
                                SubTotalMontoGravadoI1Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI1"]),
                                SubTotalMontoGravadoI2 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI2"),
                                SubTotalMontoGravadoI2Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI2"]),
                                SubTotalMontoGravadoI3 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI3"),
                                SubTotalMontoGravadoI3Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI3"]),
                                SubTotaITBIS = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS"),
                                SubTotaITBISSpecified = Metodos_General.EsNumero(Data[$"SubTotaITBIS"]),
                                SubTotaITBIS1 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS1"),
                                SubTotaITBIS1Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS1"]),
                                SubTotaITBIS2 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS2"),
                                SubTotaITBIS2Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS2"]),
                                SubTotaITBIS3 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS3"),
                                SubTotaITBIS3Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS3"]),
                                SubTotalImpuestoAdicional = Metodos_General.TryParseDecimal(Data, $"SubTotalImpuestoAdicional"),
                                SubTotalImpuestoAdicionalSpecified = Metodos_General.EsNumero(Data[$"SubTotalImpuestoAdicional"]),
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

            public static ECF_41.ECFDescuentoORecargo[] ObtenerDescuentosORecargos(Dictionary<string, string> Data)
            {
                List<ECF_41.ECFDescuentoORecargo> ListaDescuentosORecargos = new List<ECF_41.ECFDescuentoORecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLineaDoR[{indice}]"))
                    {
                        if (Data[$"NumeroLineaDoR[{indice}]"] != null)
                        {

                            ECF_41.ECFDescuentoORecargo data = new ECF_41.ECFDescuentoORecargo()
                            {
                                NumeroLinea = Data[$"NumeroLineaDoR[{indice}]"],
                                TipoAjuste = ObtenerTipoGeneral<ECF_41.TipoAjusteType>($"TipoAjuste[{indice}]"),
                                DescripcionDescuentooRecargo = Data[$"DescripcionDescuentooRecargo[{indice}]"],
                                TipoValor = ObtenerTipoGeneral<ECF_41.TipoDescuentoRecargoType>($"TipoValor[{indice}]"),
                                TipoValorSpecified = (Data[$"TipoValor[{indice}]"] != null),
                                ValorDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"ValorDescuentooRecargo[{indice}]"),
                                ValorDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"ValorDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargo[{indice}]"),
                                MontoDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargoOtraMoneda[{indice}]"),
                                MontoDescuentooRecargoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargoOtraMoneda[{indice}]"]),
                                IndicadorFacturacionDescuentooRecargo = ObtenerTipoGeneral<ECF_41.IndicadorFacturacionDRType>(Data[$"IndicadorFacturacionDescuentooRecargo[{indice}]"]),
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

            public static ECF_41.ECFPagina[] ObtenerECFPagina(Dictionary<string, string> Data)
            {
                List<ECF_41.ECFPagina> ListaECFPagina = new List<ECF_41.ECFPagina>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"PaginaNo[{indice}]"))
                    {
                        if (Data[$"PaginaNo[{indice}]"] != null)
                        {

                            ECF_41.ECFPagina data = new ECF_41.ECFPagina()
                            {
                                PaginaNo = Data[$"PaginaNo[{indice}]"],
                                NoLineaDesde = Data[$"NoLineaDesde[{indice}]"],
                                NoLineaHasta = Data[$"NoLineaHasta[{indice}]"],
                                SubtotalMontoGravadoPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoGravadoPagina[{indice}]"),
                                SubtotalMontoGravadoPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoGravadoPagina[{indice}]"]),
                                SubtotalMontoGravado1Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoGravado1Pagina[{indice}]"),
                                SubtotalMontoGravado1PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoGravado1Pagina[{indice}]"]),
                                SubtotalMontoGravado2Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoGravado2Pagina[{indice}]"),
                                SubtotalMontoGravado2PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoGravado2Pagina[{indice}]"]),
                                SubtotalMontoGravado3Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalMontoGravado3Pagina[{indice}]"),
                                SubtotalMontoGravado3PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalMontoGravado3Pagina[{indice}]"]),
                                SubtotalExentoPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalExentoPagina[{indice}]"),
                                SubtotalExentoPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalExentoPagina[{indice}]"]),
                                SubtotalItbisPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalItbisPagina[{indice}]"),
                                SubtotalItbisPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalItbisPagina[{indice}]"]),
                                SubtotalItbis1Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalItbis1Pagina[{indice}]"),
                                SubtotalItbis1PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalItbis1Pagina[{indice}]"]),
                                SubtotalItbis2Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalItbis2Pagina[{indice}]"),
                                SubtotalItbis2PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalItbis2Pagina[{indice}]"]),
                                SubtotalItbis3Pagina = Metodos_General.TryParseDecimal(Data, $"SubtotalItbis3Pagina[{indice}]"),
                                SubtotalItbis3PaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalItbis3Pagina[{indice}]"]),
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
        public static void Generar_XML_ECF41(Dictionary<string, string> Data)
        {
            ECF_41.ECF eCF_41 = new ECF_41.ECF();
            eCF_41.Encabezado = new ECF_41.ECFEncabezado()
            {
                Version = Metodos_General.TryParseDecimal(Data, "Version"),
                IdDoc = new ECF_41.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_41.TipoeCFType.Item41,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    IndicadorMontoGravadoSpecified = (Data["IndicadorMontoGravado"] == "1" || Data["IndicadorMontoGravado"] == "0"),
                    IndicadorMontoGravado = (Data["IndicadorMontoGravado"] == "1") ? ECF_41.IndicadorMontoGravadoType.Item1 : ECF_41.IndicadorMontoGravadoType.Item0,
                    TipoPago = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.TipoPagoType>(Data["TipoPago"]),
                    TipoPagoSpecified = Metodos_General.EsNumero(Data[$"TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValorECF_41.ObtenerTablaFormasPago(Data),
                    TipoCuentaPago = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.TipoCuentaPagoType>(Data["TipoCuentaPago"]),
                    NumeroCuentaPago = Data["NumeroCuentaPago"],
                    BancoPago = Data["BancoPago"],
                    TotalPaginas = Data["TotalPaginas"]
                },
                Emisor = new ECF_41.ECFEncabezadoEmisor()
                {
                    RNCEmisor = Data["RNCEmisor"],
                    RazonSocialEmisor = Data["RazonSocialEmisor"],
                    NombreComercial = Data["NombreComercial"],
                    Sucursal = Data["Sucursal"],
                    DireccionEmisor = Data["DireccionEmisor"],
                    MunicipioSpecified = Data.ContainsKey("Municipio") && Data["Municipio"] != null,
                    Municipio = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.ProvinciaMunicipioType>(Data["Municipio"]),
                    ProvinciaSpecified = Data.ContainsKey("Provincia") && Data["Provincia"] != null,
                    Provincia = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.ProvinciaMunicipioType>(Data["Provincia"]),
                    TablaTelefonoEmisor = ObtenerValorECF_41.ObtenerTablaTelefonoEmisor(Data),
                    CorreoEmisor = Data["CorreoEmisor"],
                    WebSite = Data["WebSite"],
                    ActividadEconomica = Data["ActividadEconomica"],
                    NumeroFacturaInterna = Data["NumeroFacturaInterna"],
                    NumeroPedidoInterno = Data["NumeroPedidoInterno"],
                    InformacionAdicionalEmisor = Data["InformacionAdicionalEmisor"],
                    FechaEmision = Data["FechaEmision"],

                },
                Comprador = new ECF_41.ECFEncabezadoComprador()
                {
                    RNCComprador = Data["RNCComprador"],
                    RazonSocialComprador = Data["RazonSocialComprador"],
                    ContactoComprador = Data["ContactoComprador"],
                    CorreoComprador = Data["CorreoComprador"],
                    DireccionComprador = Data["DireccionComprador"],
                    MunicipioCompradorSpecified = Data.ContainsKey("MunicipioComprador") && Data["MunicipioComprador"] != null,
                    MunicipioComprador = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.ProvinciaMunicipioType>(Data["MunicipioComprador"]),
                    ProvinciaCompradorSpecified = Data.ContainsKey("ProvinciaComprador") && Data["ProvinciaComprador"] != null,
                    ProvinciaComprador = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.ProvinciaMunicipioType>(Data["ProvinciaComprador"]),
                    CodigoInternoComprador = Data["CodigoInternoComprador"],
                    ResponsablePago = Data["ResponsablePago"],
                    InformacionAdicionalComprador = Data["InformacionAdicionalComprador"],
                },
                Totales = new ECF_41.ECFEncabezadoTotales()
                {
                    MontoGravadoTotal = Metodos_General.TryParseDecimal(Data, "MontoGravadoTotal"),
                    MontoGravadoTotalSpecified = Metodos_General.EsNumero(Data["MontoGravadoTotal"]),
                    MontoGravadoI1 = Metodos_General.TryParseDecimal(Data, "MontoGravadoI1"),
                    MontoGravadoI1Specified = Metodos_General.EsNumero(Data["MontoGravadoI1"]),
                    MontoGravadoI2 = Metodos_General.TryParseDecimal(Data, "MontoGravadoI2"),
                    MontoGravadoI2Specified = Metodos_General.EsNumero(Data["MontoGravadoI2"]),
                    MontoGravadoI3 = Metodos_General.TryParseDecimal(Data, "MontoGravadoI3"),
                    MontoGravadoI3Specified = Metodos_General.EsNumero(Data["MontoGravadoI3"]),
                    MontoExento = Metodos_General.TryParseDecimal(Data, "MontoExento"),
                    MontoExentoSpecified = Metodos_General.EsNumero(Data["MontoExento"]),
                    ITBIS1 = Data["ITBIS1"],
                    ITBIS2 = Data["ITBIS2"],
                    ITBIS3 = Data["ITBIS3"],
                    TotalITBIS = Metodos_General.TryParseDecimal(Data, "TotalITBIS"),
                    TotalITBISSpecified = Metodos_General.EsNumero(Data["TotalITBIS"]),
                    TotalITBIS1 = Metodos_General.TryParseDecimal(Data, "TotalITBIS1"),
                    TotalITBIS1Specified = Metodos_General.EsNumero(Data["TotalITBIS1"]),
                    TotalITBIS2 = Metodos_General.TryParseDecimal(Data, "TotalITBIS2"),
                    TotalITBIS2Specified = Metodos_General.EsNumero(Data["TotalITBIS2"]),
                    TotalITBIS3 = Metodos_General.TryParseDecimal(Data, "TotalITBIS3"),
                    TotalITBIS3Specified = Metodos_General.EsNumero(Data["TotalITBIS3"]),
                    MontoTotal = Metodos_General.TryParseDecimal(Data, "MontoTotal"),
                    MontoPeriodo = Metodos_General.TryParseDecimal(Data, "MontoPeriodo"),
                    MontoPeriodoSpecified = Metodos_General.EsNumero(Data["MontoPeriodo"]),
                    SaldoAnterior = Metodos_General.TryParseDecimal(Data, "SaldoAnterior"),
                    SaldoAnteriorSpecified = Metodos_General.EsNumero(Data["SaldoAnterior"]),
                    MontoAvancePago = Metodos_General.TryParseDecimal(Data, "MontoAvancePago"),
                    MontoAvancePagoSpecified = Metodos_General.EsNumero(Data["MontoAvancePago"]),
                    ValorPagar = Metodos_General.TryParseDecimal(Data, "ValorPagar"),
                    ValorPagarSpecified = Metodos_General.EsNumero(Data["ValorPagar"]),
                    TotalITBISRetenido = Metodos_General.TryParseDecimal(Data, "TotalITBISRetenido"),
                    TotalITBISRetenidoSpecified = Metodos_General.EsNumero(Data["TotalITBISRetenido"]),
                    TotalISRRetencion = Metodos_General.TryParseDecimal(Data, "TotalISRRetencion"),
                    TotalISRRetencionSpecified = Metodos_General.EsNumero(Data["TotalISRRetencion"]),
                    TotalITBISPercepcion = Metodos_General.TryParseDecimal(Data, "TotalITBISPercepcion"),
                    TotalITBISPercepcionSpecified = Metodos_General.EsNumero(Data["TotalITBISPercepcion"]),
                    TotalISRPercepcion = Metodos_General.TryParseDecimal(Data, "TotalISRPercepcion"),
                    TotalISRPercepcionSpecified = Metodos_General.EsNumero(Data["TotalISRPercepcion"])
                },
                OtraMoneda = new ECF_41.ECFEncabezadoOtraMoneda()
                {
                    TipoMoneda = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.TipoMonedaType>(Data["TipoMoneda"]),
                    TipoMonedaSpecified = Metodos_General.EsNumero(Data["TipoMoneda"]),
                    TipoCambio = Metodos_General.TryParseDecimal(Data, "TipoCambio"),
                    TipoCambioSpecified = Metodos_General.EsNumero(Data["TipoCambio"]),
                    MontoGravadoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoGravadoTotalOtraMoneda"),
                    MontoGravadoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoGravadoTotalOtraMoneda"]),
                    MontoGravado1OtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoGravado1OtraMoneda"),
                    MontoGravado1OtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoGravado1OtraMoneda"]),
                    MontoGravado2OtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoGravado2OtraMoneda"),
                    MontoGravado2OtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoGravado2OtraMoneda"]),
                    MontoGravado3OtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoGravado3OtraMoneda"),
                    MontoGravado3OtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoGravado3OtraMoneda"]),
                    MontoExentoOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoExentoOtraMoneda"),
                    MontoExentoOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoExentoOtraMoneda"]),
                    TotalITBISOtraMoneda = Metodos_General.TryParseDecimal(Data, "TotalITBISOtraMoneda"),
                    TotalITBISOtraMonedaSpecified = Metodos_General.EsNumero(Data["TotalITBISOtraMoneda"]),
                    TotalITBIS1OtraMoneda = Metodos_General.TryParseDecimal(Data, "TotalITBIS1OtraMoneda"),
                    TotalITBIS1OtraMonedaSpecified = Metodos_General.EsNumero(Data["TotalITBIS1OtraMoneda"]),
                    TotalITBIS2OtraMoneda = Metodos_General.TryParseDecimal(Data, "TotalITBIS2OtraMoneda"),
                    TotalITBIS2OtraMonedaSpecified = Metodos_General.EsNumero(Data["TotalITBIS2OtraMoneda"]),
                    TotalITBIS3OtraMoneda = Metodos_General.TryParseDecimal(Data, "TotalITBIS3OtraMoneda"),
                    TotalITBIS3OtraMonedaSpecified = Metodos_General.EsNumero(Data["TotalITBIS3OtraMoneda"]),
                    MontoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoTotalOtraMoneda"),
                    MontoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoTotalOtraMoneda"]),

                },
            };
            eCF_41.DetallesItems = ObtenerValorECF_41.ObtenerDetalleItem(Data);
            eCF_41.Subtotales = ObtenerValorECF_41.ObtenerSubtotalesl(Data);
            eCF_41.DescuentosORecargos = ObtenerValorECF_41.ObtenerDescuentosORecargos(Data);
            eCF_41.Paginacion = ObtenerValorECF_41.ObtenerECFPagina(Data);
            eCF_41.InformacionReferencia = new ECF_41.ECFInformacionReferencia
            {
                NCFModificado = Data["NCFModificado"],
                RNCOtroContribuyente = Data["RNCOtroContribuyente"],
                FechaNCFModificado = Data["FechaNCFModificado"],
                CodigoModificacion = ObtenerValorECF_41.ObtenerTipoGeneral<ECF_41.CodigoModificacionType>(Data["CodigoModificacion"]),
                CodigoModificacionSpecified = Metodos_General.EsNumero(Data["CodigoModificacion"]),
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ECF_41.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_41);
                string xmlOutput = writer.ToString();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlOutput);
                Metodos_General.XmlCorrector.FindValuesNotInXml(Data, xmlDocument);
                string a = Metodos_General.XmlCorrector.CorrectXml(xmlOutput, "E:\\Proyectos\\M_I_FE\\M_I_FE\\XSD\\e-CF 41 v.1.0.xsd");
                Console.WriteLine(a);

                Metodos_General.SaveContentToFile(a, "41");
            }
        }
    }
}

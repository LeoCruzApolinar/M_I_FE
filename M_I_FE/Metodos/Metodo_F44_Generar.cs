using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace M_I_FE.Metodos
{
    public class Metodo_F44_Generar
    {
        public class ObtenerValorECF_44()
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

            public static ECF_44.ECFEncabezadoIdDocFormaDePago[] ObtenerTablaFormasPago(Dictionary<string, string> datos)
            {
                List<ECF_44.ECFEncabezadoIdDocFormaDePago> listaFormasPago = new List<ECF_44.ECFEncabezadoIdDocFormaDePago>();

                int indice = 1;
                while (true)
                {
                    string formaPagoKey = $"FormaPago[{indice}]";
                    string montoPagoKey = $"MontoPago[{indice}]";

                    if (datos.ContainsKey(formaPagoKey) && datos.ContainsKey(montoPagoKey))
                    {
                        if (Metodos_General.EsNumero(datos[formaPagoKey]) && Metodos_General.EsNumero(datos[montoPagoKey]))
                        {
                            ECF_44.ECFEncabezadoIdDocFormaDePago formaPago = new ECF_44.ECFEncabezadoIdDocFormaDePago
                            {
                                FormaPago = ObtenerTipoGeneral<ECF_44.FormaPagoType>(datos[formaPagoKey]),
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

            public static ECF_44.ECFEncabezadoTotalesImpuestoAdicional[] ObtenerImpuestosAdicionales(Dictionary<string, string> Data)
            {
                List<ECF_44.ECFEncabezadoTotalesImpuestoAdicional> ImpuestosAdicionales = new List<ECF_44.ECFEncabezadoTotalesImpuestoAdicional>();
                int indice = 1;
                while (Data != null)
                {


                    if (Data.ContainsKey($"TipoImpuesto[{indice}]"))
                    {
                        if (Data[$"TipoImpuesto[{indice}]"] != null)
                        {


                            ECF_44.ECFEncabezadoTotalesImpuestoAdicional eCFEncabezadoTotalesImpuestoAdicional = new ECF_44.ECFEncabezadoTotalesImpuestoAdicional()
                            {
                                TipoImpuesto = ObtenerTipoGeneral<ECF_44.CodificacionTipoImpuestosType>(Data[$"TipoImpuesto[{indice}]"]),
                                TasaImpuestoAdicional = Metodos_General.TryParseDecimal(Data, $"TasaImpuestoAdicional[{indice}]"),
                            };
                            // Agregar el objeto a la lista
                            ImpuestosAdicionales.Add(eCFEncabezadoTotalesImpuestoAdicional);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }

                return ImpuestosAdicionales.Count == 0 ? default : ImpuestosAdicionales.ToArray();
            }

            public static ECF_44.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda[] ObtenerImpuestosAdicionalesOtraMoneda(Dictionary<string, string> Data)
            {
                List<ECF_44.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda> ImpuestosAdicionalesOtraMoneda = new List<ECF_44.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoImpuestoOtraMoneda[{indice}]"))
                    {
                        if (Data[$"TipoImpuestoOtraMoneda[{indice}]"] != null)
                        {

                            ECF_44.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda eCFEncabezadoTotalesImpuestoAdicionalOtraMoneda = new ECF_44.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda()
                            {
                                TipoImpuestoOtraMoneda = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.CodificacionTipoImpuestosType>(Data[$"TipoImpuestoOtraMoneda[{indice}]"]),
                                TasaImpuestoAdicionalOtraMoneda = Metodos_General.TryParseDecimal(Data, $"TasaImpuestoAdicionalOtraMoneda[{indice}]"),
                                OtrosImpuestosAdicionalesOtraMoneda = Metodos_General.TryParseDecimal(Data, $"OtrosImpuestosAdicionalesOtraMoneda[{indice}]"),
                                OtrosImpuestosAdicionalesOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"OtrosImpuestosAdicionalesOtraMoneda[{indice}]"]),
                            };
                            // Agregar el objeto a la lista
                            ImpuestosAdicionalesOtraMoneda.Add(eCFEncabezadoTotalesImpuestoAdicionalOtraMoneda);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }
                return ImpuestosAdicionalesOtraMoneda.Count == 0 ? default : ImpuestosAdicionalesOtraMoneda.ToArray();

            }

            public static ECF_44.ECFItem[] ObtenerDetalleItem(Dictionary<string, string> Data)
            {
                List<ECF_44.ECFItem> eCFItems = new List<ECF_44.ECFItem>();
                int indice = 1;

                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLinea[{indice}]"))
                    {
                        if (Data[$"NumeroLinea[{indice}]"] != null)
                        {

                            ECF_44.ECFItem eCFItem = new ECF_44.ECFItem()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TablaCodigosItem = ObtenerCodigosItems(Data, indice),
                                IndicadorFacturacion = ObtenerTipoGeneral<ECF_44.IndicadorFacturacionType>(Data[$"IndicadorFacturacion[{indice}]"]),
                                NombreItem = Data[$"NombreItem[{indice}]"],
                                IndicadorBienoServicio = ObtenerTipoGeneral<ECF_44.IndicadorBienoServicioType>(Data[$"IndicadorBienoServicio[{indice}]"]),
                                DescripcionItem = Data[$"DescripcionItem[{indice}]"],
                                CantidadItem = Metodos_General.TryParseDecimal(Data, $"CantidadItem[{indice}]"),
                                UnidadMedida = ObtenerTipoGeneral<ECF_44.UnidadMedidaType>(Data[$"UnidadMedida[{indice}]"]),
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
                                TablaImpuestoAdicional = ObtenerTablaImpuestoAdicional(Data, indice),
                                OtraMonedaDetalle = new ECF_44.ECFItemOtraMonedaDetalle()
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

            public static ECF_44.ECFItemCodigosItem[] ObtenerCodigosItems(Dictionary<string, string> Data, int ind)
            {
                List<ECF_44.ECFItemCodigosItem> eCFItemCodigosItems = new List<ECF_44.ECFItemCodigosItem>();

                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoCodigo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoCodigo[{ind}][{indice}]"] != null)
                        {

                            ECF_44.ECFItemCodigosItem eCFItemCodigosItem = new ECF_44.ECFItemCodigosItem()
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

            public static ECF_44.ECFItemSubDescuento[] ObtenerTablaECFItemSubDescuento(Dictionary<string, string> Data, int ind)
            {
                List<ECF_44.ECFItemSubDescuento> ListaECFItemSubDescuento = new List<ECF_44.ECFItemSubDescuento>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubDescuento[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoSubDescuento[{ind}][{indice}]"] != null)
                        {

                            ECF_44.ECFItemSubDescuento data = new ECF_44.ECFItemSubDescuento()
                            {
                                TipoSubDescuento = ObtenerTipoGeneral<ECF_44.TipoDescuentoRecargoType>(Data[$"TipoSubDescuento[{ind}][{indice}]"]),
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

            public static ECF_44.ECFItemSubRecargo[] ObtenerTablaECFItemSubRecargo(Dictionary<string, string> Data, int ind)
            {
                List<ECF_44.ECFItemSubRecargo> ListaECFItemSubRecargo = new List<ECF_44.ECFItemSubRecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubRecargo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoSubRecargo[{ind}][{indice}]"] != null)
                        {
                            ECF_44.ECFItemSubRecargo data = new ECF_44.ECFItemSubRecargo()
                            {
                                TipoSubRecargo = ObtenerTipoGeneral<ECF_44.TipoDescuentoRecargoType>($"TipoSubRecargo[{ind}][{indice}]"),
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

            public static ECF_44.ECFItemImpuestoAdicional[] ObtenerTablaImpuestoAdicional(Dictionary<string, string> Data, int ind)
            {
                List<ECF_44.ECFItemImpuestoAdicional> ListaImpuestoAdicional = new List<ECF_44.ECFItemImpuestoAdicional>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoImpuesto[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoImpuesto[{ind}][{indice}]"] != null)
                        {

                            var a = Data[$"TipoImpuesto[{ind}][{indice}]"];

                            ECF_44.ECFItemImpuestoAdicional data = new ECF_44.ECFItemImpuestoAdicional()
                            {
                                TipoImpuesto = ObtenerTipoGeneral<ECF_44.CodificacionTipoImpuestosType>(Data[$"TipoImpuesto[{ind}][{indice}]"]),
                            };
                            // Agregar el objeto a la lista
                            ListaImpuestoAdicional.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return ListaImpuestoAdicional.Count == 0 ? default : ListaImpuestoAdicional.ToArray();
            }

            public static ECF_44.ECFSubtotal[] ObtenerSubtotalesl(Dictionary<string, string> Data)
            {
                List<ECF_44.ECFSubtotal> ListaSubtotales = new List<ECF_44.ECFSubtotal>();
                int indice = 1;
                if (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal"))
                    {
                        if (Data[$"NumeroSubTotal"] != null)
                        {

                            ECF_44.ECFSubtotal data = new ECF_44.ECFSubtotal()
                            {
                                NumeroSubTotal = Data["NumeroSubTotal"],
                                DescripcionSubtotal = Data["DescripcionSubtotal"],
                                Orden = Data["Orden"],
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

            public static ECF_44.ECFDescuentoORecargo[] ObtenerDescuentosORecargos(Dictionary<string, string> Data)
            {
                List<ECF_44.ECFDescuentoORecargo> ListaDescuentosORecargos = new List<ECF_44.ECFDescuentoORecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLineaDoR[{indice}]"))
                    {
                        if (Data[$"NumeroLineaDoR[{indice}]"] != null)
                        {

                            ECF_44.ECFDescuentoORecargo data = new ECF_44.ECFDescuentoORecargo()
                            {
                                NumeroLinea = Data[$"NumeroLineaDoR[{indice}]"],
                                TipoAjuste = ObtenerTipoGeneral<ECF_44.TipoAjusteType>($"TipoAjuste[{indice}]"),
                                DescripcionDescuentooRecargo = Data[$"DescripcionDescuentooRecargo[{indice}]"],
                                TipoValor = ObtenerTipoGeneral<ECF_44.TipoDescuentoRecargoType>(Data[$"TipoValor[{indice}]"]),
                                TipoValorSpecified = (Data[$"TipoValor[{indice}]"] != null),
                                ValorDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"ValorDescuentooRecargo[{indice}]"),
                                ValorDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"ValorDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargo[{indice}]"),
                                MontoDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargoOtraMoneda[{indice}]"),
                                MontoDescuentooRecargoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargoOtraMoneda[{indice}]"]),
                                IndicadorFacturacionDescuentooRecargo = ObtenerTipoGeneral<ECF_44.IndicadorFacturacionDRType>(Data[$"IndicadorFacturacionDescuentooRecargo[{indice}]"]),
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

            public static ECF_44.ECFPagina[] ObtenerECFPagina(Dictionary<string, string> Data)
            {
                List<ECF_44.ECFPagina> ListaECFPagina = new List<ECF_44.ECFPagina>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"PaginaNo[{indice}]"))
                    {
                        if (Data[$"PaginaNo[{indice}]"] != null)
                        {

                            ECF_44.ECFPagina data = new ECF_44.ECFPagina()
                            {
                                PaginaNo = Data[$"PaginaNo[{indice}]"],
                                NoLineaDesde = Data[$"NoLineaDesde[{indice}]"],
                                NoLineaHasta = Data[$"NoLineaHasta[{indice}]"],
                                SubtotalImpuestoAdicionalPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalImpuestoAdicionalPagina[{indice}]"),
                                SubtotalImpuestoAdicionalPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalImpuestoAdicionalPagina[{indice}]"]),
                                SubtotalImpuestoAdicional = new ECF_44.ECFPaginaSubtotalImpuestoAdicional()
                                {
                                    SubtotalOtrosImpuesto = Metodos_General.TryParseDecimal(Data, $"SubtotalOtrosImpuesto[{indice}]"),
                                    SubtotalOtrosImpuestoSpecified = Metodos_General.EsNumero(Data[$"SubtotalOtrosImpuesto[{indice}]"]),
                                },
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
        public static void Generar_XML_ECF44(Dictionary<string, string> Data)
        {
            ECF_44.ECF eCF_44 = new ECF_44.ECF();
            eCF_44.Encabezado = new ECF_44.ECFEncabezado()
            {
                Version = Metodos_General.TryParseDecimal(Data, "Version"),
                IdDoc = new ECF_44.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_44.TipoeCFType.Item44,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    IndicadorEnvioDiferidoSpecified = (Data["IndicadorEnvioDiferido"] == "1" || Data["IndicadorEnvioDiferido"] == "0"),
                    IndicadorEnvioDiferido = ECF_44.IndicadorEnvioDiferidoType.Item1,
                    TipoIngresos = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.TipoIngresosValidationType>(Data["TipoIngresos"]),
                    TipoPago = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.TipoPagoType>(Data["TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValorECF_44.ObtenerTablaFormasPago(Data),
                    TipoCuentaPago = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.TipoCuentaPagoType>(Data["TipoCuentaPago"]),
                    TipoCuentaPagoSpecified = (Data["TipoCuentaPago"] != null),
                    NumeroCuentaPago = Data["NumeroCuentaPago"],
                    BancoPago = Data["BancoPago"],
                    FechaDesde = Data["FechaDesde"],
                    FechaHasta = Data["FechaHasta"],
                    TotalPaginas = Data["TotalPaginas"]
                },
                Emisor = new ECF_44.ECFEncabezadoEmisor()
                {
                    RNCEmisor = Data["RNCEmisor"],
                    RazonSocialEmisor = Data["RazonSocialEmisor"],
                    NombreComercial = Data["NombreComercial"],
                    Sucursal = Data["Sucursal"],
                    DireccionEmisor = Data["DireccionEmisor"],
                    MunicipioSpecified = Data.ContainsKey("Municipio") && Data["Municipio"] != null,
                    Municipio = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.ProvinciaMunicipioType>(Data["Municipio"]),
                    ProvinciaSpecified = Data.ContainsKey("Provincia") && Data["Provincia"] != null,
                    Provincia = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.ProvinciaMunicipioType>(Data["Provincia"]),
                    TablaTelefonoEmisor = ObtenerValorECF_44.ObtenerTablaTelefonoEmisor(Data),
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
                Comprador = new ECF_44.ECFEncabezadoComprador()
                {
                    RNCComprador = Data["RNCComprador"],
                    IdentificadorExtranjero = Data["IdentificadorExtranjero"],
                    RazonSocialComprador = Data["RazonSocialComprador"],
                    ContactoComprador = Data["ContactoComprador"],
                    CorreoComprador = Data["CorreoComprador"],
                    DireccionComprador = Data["DireccionComprador"],
                    MunicipioCompradorSpecified = Data.ContainsKey("MunicipioComprador") && Data["MunicipioComprador"] != null,
                    MunicipioComprador = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.ProvinciaMunicipioType>(Data["MunicipioComprador"]),
                    ProvinciaCompradorSpecified = Data.ContainsKey("ProvinciaComprador") && Data["ProvinciaComprador"] != null,
                    ProvinciaComprador = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.ProvinciaMunicipioType>(Data["ProvinciaComprador"]),
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
                InformacionesAdicionales = new ECF_44.ECFEncabezadoInformacionesAdicionales
                {
                    FechaEmbarque = Data["FechaEmbarque"],
                    NumeroEmbarque = Data["NumeroEmbarque"],
                    NumeroContenedor = Data["NumeroContenedor"],
                    NumeroReferencia = Data["NumeroReferencia"],
                    PesoBruto = Metodos_General.TryParseDecimal(Data, "PesoBruto"),
                    PesoBrutoSpecified = Metodos_General.EsNumero(Data["PesoBruto"]),
                    PesoNeto = Metodos_General.TryParseDecimal(Data, "PesoNeto"),
                    PesoNetoSpecified = Metodos_General.EsNumero(Data["PesoNeto"]),
                    UnidadPesoBruto = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.UnidadMedidaType>(Data["UnidadPesoBruto"]),
                    UnidadPesoBrutoSpecified = Metodos_General.EsNumero(Data["UnidadPesoBruto"]),
                    UnidadPesoNeto = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.UnidadMedidaType>(Data["UnidadPesoNeto"]),
                    UnidadPesoNetoSpecified = Metodos_General.EsNumero(Data["UnidadPesoNeto"]),
                    CantidadBulto = Metodos_General.TryParseDecimal(Data, "CantidadBulto"),
                    CantidadBultoSpecified = Metodos_General.EsNumero(Data["CantidadBulto"]),
                    UnidadBulto = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.UnidadMedidaType>(Data["UnidadBulto"]),
                    UnidadBultoSpecified = Metodos_General.EsNumero(Data["UnidadBulto"]),
                    VolumenBulto = Metodos_General.TryParseDecimal(Data, "VolumenBulto"),
                    VolumenBultoSpecified = Metodos_General.EsNumero(Data["VolumenBulto"]),
                    UnidadVolumen = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.UnidadMedidaType>(Data["UnidadVolumen"]),
                    UnidadVolumenSpecified = Metodos_General.EsNumero(Data["UnidadVolumen"]),
                },
                Transporte = new ECF_44.ECFEncabezadoTransporte()
                {
                    Conductor = Data["Conductor"],
                    DocumentoTransporte = Data["DocumentoTransporte"],
                    Ficha = Data["Ficha"],
                    Placa = Data["Placa"],
                    RutaTransporte = Data["RutaTransporte"],
                    ZonaTransporte = Data["ZonaTransporte"],
                    NumeroAlbaran = Data["NumeroAlbaran"],
                },
                Totales = new ECF_44.ECFEncabezadoTotales()
                {
                    MontoExento = Metodos_General.TryParseDecimal(Data, "MontoExento"),
                    MontoExentoSpecified = Metodos_General.EsNumero(Data["MontoExento"]),
                    MontoImpuestoAdicional = Metodos_General.TryParseDecimal(Data, "MontoImpuestoAdicional"),
                    MontoImpuestoAdicionalSpecified = Metodos_General.EsNumero(Data["MontoImpuestoAdicional"]),
                    ImpuestosAdicionales = ObtenerValorECF_44.ObtenerImpuestosAdicionales(Data),
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
                OtraMoneda = new ECF_44.ECFEncabezadoOtraMoneda()
                {
                    TipoMoneda = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.TipoMonedaType>(Data["TipoMoneda"]),
                    TipoMonedaSpecified = Metodos_General.EsNumero(Data["TipoMoneda"]),
                    TipoCambio = Metodos_General.TryParseDecimal(Data, "TipoCambio"),
                    TipoCambioSpecified = Metodos_General.EsNumero(Data["TipoCambio"]),
                    MontoExentoOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoExentoOtraMoneda"),
                    MontoExentoOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoExentoOtraMoneda"]),
                    MontoImpuestoAdicionalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoImpuestoAdicionalOtraMoneda"),
                    MontoImpuestoAdicionalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoImpuestoAdicionalOtraMoneda"]),
                    ImpuestosAdicionalesOtraMoneda = ObtenerValorECF_44.ObtenerImpuestosAdicionalesOtraMoneda(Data),
                    MontoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoTotalOtraMoneda"),
                    MontoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoTotalOtraMoneda"]),

                },
            };
            eCF_44.DetallesItems = ObtenerValorECF_44.ObtenerDetalleItem(Data);
            eCF_44.Subtotales = ObtenerValorECF_44.ObtenerSubtotalesl(Data);
            eCF_44.DescuentosORecargos = ObtenerValorECF_44.ObtenerDescuentosORecargos(Data);
            eCF_44.Paginacion = ObtenerValorECF_44.ObtenerECFPagina(Data);
            eCF_44.InformacionReferencia = new ECF_44.ECFInformacionReferencia
            {
                NCFModificado = Data["NCFModificado"],
                RNCOtroContribuyente = Data["RNCOtroContribuyente"],
                FechaNCFModificado = Data["FechaNCFModificado"],
                CodigoModificacion = ObtenerValorECF_44.ObtenerTipoGeneral<ECF_44.CodigoModificacionType>(Data["CodigoModificacion"]),
                CodigoModificacionSpecified = Metodos_General.EsNumero(Data["CodigoModificacion"]),
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ECF_44.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_44);
                Metodos_General.SaveContentToFile(writer.ToString(), "44");
            }
        }
    }
}

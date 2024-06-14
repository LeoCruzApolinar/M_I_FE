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
        public class ObtenerValor()
        {
            public static dynamic ObtenerTipoGeneral<T>(string codigo)
            {
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

            public static ECF_31.ECFEncabezadoIdDocFormaDePago[] ObtenerTablaFormasPago(Dictionary<string, string> datos)
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
                                FormaPago = ObtenerTipoGeneral<ECF_31.FormaPagoType>(datos[formaPagoKey]),
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

            public static ECF_31.ECFEncabezadoTotalesImpuestoAdicional[] ObtenerImpuestosAdicionales(Dictionary<string, string> Data)
            {
                List<ECF_31.ECFEncabezadoTotalesImpuestoAdicional> ImpuestosAdicionales = new List<ECF_31.ECFEncabezadoTotalesImpuestoAdicional>();
                int indice = 1;
                while (Data != null)
                {


                    if (Data.ContainsKey($"TipoImpuesto[{indice}]"))
                    {
                        if (Data[$"TipoImpuesto[{indice}]"] != null)
                        {


                            ECF_31.ECFEncabezadoTotalesImpuestoAdicional eCFEncabezadoTotalesImpuestoAdicional = new ECF_31.ECFEncabezadoTotalesImpuestoAdicional()
                            {
                                TipoImpuesto = ObtenerTipoGeneral<ECF_31.CodificacionTipoImpuestosType>(Data[$"TipoImpuesto[{indice}]"]),
                                TasaImpuestoAdicional = Metodos_General.TryParseDecimal(Data, $"TasaImpuestoAdicional[{indice}]"),
                                MontoImpuestoSelectivoConsumoEspecifico = Metodos_General.TryParseDecimal(Data, $"MontoImpuestoSelectivoConsumoEspecifico[{indice}]"),
                                MontoImpuestoSelectivoConsumoEspecificoSpecified = Metodos_General.EsNumero(Data[$"MontoImpuestoSelectivoConsumoEspecifico[{indice}]"]),
                                MontoImpuestoSelectivoConsumoAdvalorem = Metodos_General.TryParseDecimal(Data, $"MontoImpuestoSelectivoConsumoAdvalorem[{indice}]"),
                                MontoImpuestoSelectivoConsumoAdvaloremSpecified = Metodos_General.EsNumero(Data[$"MontoImpuestoSelectivoConsumoAdvalorem[{indice}]"]),
                                OtrosImpuestosAdicionales = Metodos_General.TryParseDecimal(Data, $"OtrosImpuestosAdicionales[{indice}]"),
                                OtrosImpuestosAdicionalesSpecified = Metodos_General.EsNumero(Data[$"OtrosImpuestosAdicionales[{indice}]"])
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

            public static ECF_31.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda[] ObtenerImpuestosAdicionalesOtraMoneda(Dictionary<string, string> Data)
            {
                List<ECF_31.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda> ImpuestosAdicionalesOtraMoneda = new List<ECF_31.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoImpuestoOtraMoneda[{indice}]"))
                    {
                        if (Data[$"TipoImpuestoOtraMoneda[{indice}]"] != null)
                        {

                            ECF_31.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda eCFEncabezadoTotalesImpuestoAdicionalOtraMoneda = new ECF_31.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda()
                            {
                                TipoImpuestoOtraMoneda = ObtenerValor.ObtenerTipoGeneral<ECF_31.CodificacionTipoImpuestosType>(Data[$"TipoImpuestoOtraMoneda[{indice}]"]),
                                TasaImpuestoAdicionalOtraMoneda = Metodos_General.TryParseDecimal(Data, $"TasaImpuestoAdicionalOtraMoneda[{indice}]"),
                                MontoImpuestoSelectivoConsumoEspecificoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoImpuestoSelectivoConsumoEspecificoOtraMoneda[{indice}]"),
                                MontoImpuestoSelectivoConsumoEspecificoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoImpuestoSelectivoConsumoEspecificoOtraMoneda[{indice}]"]),
                                MontoImpuestoSelectivoConsumoAdvaloremOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoImpuestoSelectivoConsumoAdvaloremOtraMoneda[{indice}]"),
                                MontoImpuestoSelectivoConsumoAdvaloremOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoImpuestoSelectivoConsumoAdvaloremOtraMoneda[{indice}]"]),
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

            public static ECF_31.ECFItem[] ObtenerDetalleItem(Dictionary<string, string> Data)
            {
                List<ECF_31.ECFItem> eCFItems = new List<ECF_31.ECFItem>();
                int indice = 1;

                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLinea[{indice}]"))
                    {
                        if (Data[$"NumeroLinea[{indice}]"] != null)
                        {

                            ECF_31.ECFItem eCFItem = new ECF_31.ECFItem()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TablaCodigosItem = ObtenerCodigosItems(Data, indice),
                                IndicadorFacturacion = ObtenerTipoGeneral<ECF_31.IndicadorFacturacionType>(Data[$"IndicadorFacturacion[{indice}]"]),
                                Retencion = new ECF_31.ECFItemRetencion()
                                {
                                    IndicadorAgenteRetencionoPercepcion = ObtenerTipoGeneral<ECF_31.IndicadorAgenteRetencionoPercepcionType>(Data[$"IndicadorAgenteRetencionoPercepcion[{indice}]"]),
                                    IndicadorAgenteRetencionoPercepcionSpecified = Metodos_General.EsNumero(Data[$"IndicadorAgenteRetencionoPercepcion[{indice}]"]),
                                    MontoITBISRetenido = Metodos_General.TryParseDecimal(Data, $"MontoITBISRetenido[{indice}]"),
                                    MontoITBISRetenidoSpecified = Metodos_General.EsNumero(Data[$"MontoITBISRetenido[{indice}]"]),
                                    MontoISRRetenido = Metodos_General.TryParseDecimal(Data, $"MontoISRRetenido[{indice}]"),
                                    MontoISRRetenidoSpecified = Metodos_General.EsNumero(Data[$"MontoISRRetenido[{indice}]"]),

                                },
                                NombreItem = Data[$"NombreItem[{indice}]"],
                                IndicadorBienoServicio = ObtenerTipoGeneral<ECF_31.IndicadorBienoServicioType>(Data[$"IndicadorBienoServicio[{indice}]"]),
                                DescripcionItem = Data["DescripcionItem"],
                                CantidadItem = Metodos_General.TryParseDecimal(Data, $"CantidadItem[{indice}]"),
                                UnidadMedida = ObtenerTipoGeneral<ECF_31.UnidadMedidaType>(Data[$"UnidadMedida[{indice}]"]),
                                UnidadMedidaSpecified = Metodos_General.EsNumero(Data[$"UnidadMedida[{indice}]"]),
                                CantidadReferencia = Metodos_General.TryParseDecimal(Data, $"CantidadReferencia[{indice}]"),
                                CantidadReferenciaSpecified = Metodos_General.EsNumero(Data[$"CantidadReferencia[{indice}]"]),
                                UnidadReferencia = ObtenerTipoGeneral<ECF_31.UnidadMedidaType>(Data[$"UnidadReferencia[{indice}]"]),
                                //UnidadReferenciaSpecified = Data["UnidadReferenciaSpecified"],
                                //TablaSubcantidad = Data["TablaSubcantidad"],
                                //GradosAlcohol = Data["GradosAlcohol"],
                                //GradosAlcoholSpecified = Data["GradosAlcoholSpecified"],
                                //PrecioUnitarioReferencia = Data["PrecioUnitarioReferencia"],
                                //PrecioUnitarioReferenciaSpecified = Data["PrecioUnitarioReferenciaSpecified"],
                                //FechaElaboracion = Data["FechaElaboracion"],
                                //FechaVencimientoItem = Data["FechaVencimientoItem"],
                                //PrecioUnitarioItem = Data["PrecioUnitarioItem"],
                                //DescuentoMonto = Data["DescuentoMonto"],
                                //DescuentoMontoSpecified = Data["DescuentoMontoSpecified"],
                                //TablaSubDescuento = Data["TablaSubDescuento"],
                                //RecargoMonto = Data["RecargoMonto"],
                                //RecargoMontoSpecified = Data["RecargoMontoSpecified"],
                                //TablaSubRecargo = Data["TablaSubRecargo"],
                                //TablaImpuestoAdicional = Data["TablaImpuestoAdicional"],
                                //OtraMonedaDetalle = Data["OtraMonedaDetalle"],
                                //MontoItem = Data["MontoItem"],
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

            public static ECF_31.ECFItemCodigosItem[] ObtenerCodigosItems(Dictionary<string, string> Data, int ind)
            {
                List<ECF_31.ECFItemCodigosItem> eCFItemCodigosItems = new List<ECF_31.ECFItemCodigosItem>();

                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoCodigo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoCodigo[{ind}][{indice}]"] != null)
                        {

                            ECF_31.ECFItemCodigosItem eCFItemCodigosItem = new ECF_31.ECFItemCodigosItem()
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

        }
        public static void Generar_XML_ECF31(Dictionary<string, string> Data) 
        {
            var eCF_31 = new ECF_31.ECF();

            eCF_31.Encabezado = new ECF_31.ECFEncabezado()
            {
                Version = 1,
                IdDoc = new ECF_31.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_31.TipoeCFType.Item31,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    IndicadorEnvioDiferidoSpecified = (Data["IndicadorEnvioDiferido"] == "1" || Data["IndicadorEnvioDiferido"] == "0"),
                    IndicadorEnvioDiferido = ECF_31.IndicadorEnvioDiferidoType.Item1,
                    IndicadorMontoGravadoSpecified = (Data["IndicadorMontoGravado"] == "1" || Data["IndicadorMontoGravado"] == "0"),
                    IndicadorMontoGravado = (Data["IndicadorMontoGravado"] == "1") ? ECF_31.IndicadorMontoGravadoType.Item1 : ECF_31.IndicadorMontoGravadoType.Item0,
                    TipoIngresos = ObtenerValor.ObtenerTipoGeneral<ECF_31.TipoIngresosValidationType>(Data["TipoIngresos"]),
                    TipoPago = ObtenerValor.ObtenerTipoGeneral<ECF_31.TipoPagoType>(Data["TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValor.ObtenerTablaFormasPago(Data),
                    TipoCuentaPago = ObtenerValor.ObtenerTipoGeneral<ECF_31.TipoCuentaPagoType>(Data["TipoCuentaPago"]),
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
                    DireccionEmisor = Data["DireccionEmisor"],
                    MunicipioSpecified = Data.ContainsKey("Municipio") && Data["Municipio"] != null,
                    Municipio = ObtenerValor.ObtenerTipoGeneral<ECF_31.ProvinciaMunicipioType>(Data["Municipio"]),
                    ProvinciaSpecified = Data.ContainsKey("Provincia") && Data["Provincia"] != null,
                    Provincia = ObtenerValor.ObtenerTipoGeneral<ECF_31.ProvinciaMunicipioType>(Data["Provincia"]),
                    TablaTelefonoEmisor =ObtenerValor.ObtenerTablaTelefonoEmisor(Data),
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
                Comprador = new ECF_31.ECFEncabezadoComprador() 
                {
                    RNCComprador = Data["RNCComprador"],
                    RazonSocialComprador = Data["RazonSocialComprador"],
                    ContactoComprador = Data["ContactoComprador"],
                    CorreoComprador = Data["CorreoComprador"],
                    DireccionComprador = Data["DireccionComprador"],
                    MunicipioCompradorSpecified = Data.ContainsKey("MunicipioComprador") && Data["MunicipioComprador"] != null,
                    MunicipioComprador = ObtenerValor.ObtenerTipoGeneral<ECF_31.ProvinciaMunicipioType>(Data["MunicipioComprador"]),
                    ProvinciaCompradorSpecified = Data.ContainsKey("ProvinciaComprador") && Data["ProvinciaComprador"] != null,
                    ProvinciaComprador = ObtenerValor.ObtenerTipoGeneral<ECF_31.ProvinciaMunicipioType>(Data["ProvinciaComprador"]),
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
                InformacionesAdicionales = new ECF_31.ECFEncabezadoInformacionesAdicionales 
                {
                    FechaEmbarque = Data["FechaEmbarque"],
                    NumeroEmbarque = Data["NumeroEmbarque"],
                    NumeroContenedor = Data["NumeroContenedor"],
                    NumeroReferencia = Data["NumeroReferencia"],
                    PesoBruto = Metodos_General.TryParseDecimal(Data, "PesoBruto"),
                    PesoBrutoSpecified = Metodos_General.EsNumero(Data["PesoBruto"]),
                    PesoNeto = Metodos_General.TryParseDecimal(Data, "PesoNeto"),
                    PesoNetoSpecified = Metodos_General.EsNumero(Data["PesoNeto"]),
                    UnidadPesoBruto = ObtenerValor.ObtenerTipoGeneral<ECF_31.UnidadMedidaType>(Data["UnidadPesoBruto"]),
                    UnidadPesoBrutoSpecified = Metodos_General.EsNumero(Data["UnidadPesoBruto"]),
                    UnidadPesoNeto = ObtenerValor.ObtenerTipoGeneral<ECF_31.UnidadMedidaType>(Data["UnidadPesoNeto"]),
                    UnidadPesoNetoSpecified = Metodos_General.EsNumero(Data["UnidadPesoNeto"]),
                    CantidadBulto = Metodos_General.TryParseDecimal(Data, "CantidadBulto"),
                    CantidadBultoSpecified = Metodos_General.EsNumero(Data["CantidadBulto"]),
                    UnidadBulto = ObtenerValor.ObtenerTipoGeneral<ECF_31.UnidadMedidaType>(Data["UnidadBulto"]),
                    UnidadBultoSpecified = Metodos_General.EsNumero(Data["UnidadBulto"]),
                    VolumenBulto = Metodos_General.TryParseDecimal(Data, "VolumenBulto"),
                    VolumenBultoSpecified = Metodos_General.EsNumero(Data["VolumenBulto"]),
                    UnidadVolumen = ObtenerValor.ObtenerTipoGeneral<ECF_31.UnidadMedidaType>(Data["UnidadVolumen"]),
                    UnidadVolumenSpecified = Metodos_General.EsNumero(Data["UnidadVolumen"]),
                },
                Transporte = new ECF_31.ECFEncabezadoTransporte() 
                {
                    Conductor = Data["Conductor"],
                    DocumentoTransporte = Data["DocumentoTransporte"],
                    Ficha = Data["Ficha"],
                    Placa = Data["Placa"],
                    RutaTransporte = Data["RutaTransporte"],
                    ZonaTransporte = Data["ZonaTransporte"],
                    NumeroAlbaran = Data["NumeroAlbaran"],
                },
                Totales = new ECF_31.ECFEncabezadoTotales() 
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
                    MontoImpuestoAdicional = Metodos_General.TryParseDecimal(Data, "MontoImpuestoAdicional"),
                    MontoImpuestoAdicionalSpecified = Metodos_General.EsNumero(Data["MontoImpuestoAdicional"]),
                    ImpuestosAdicionales = ObtenerValor.ObtenerImpuestosAdicionales(Data),
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
                    TotalITBISRetenido = Metodos_General.TryParseDecimal(Data, "TotalITBISRetenido"),
                    TotalITBISRetenidoSpecified = Metodos_General.EsNumero(Data["TotalITBISRetenido"]),
                    TotalISRRetencion = Metodos_General.TryParseDecimal(Data, "TotalISRRetencion"),
                    TotalISRRetencionSpecified = Metodos_General.EsNumero(Data["TotalISRRetencion"]),
                    TotalITBISPercepcion = Metodos_General.TryParseDecimal(Data, "TotalITBISPercepcion"),
                    TotalITBISPercepcionSpecified = Metodos_General.EsNumero(Data["TotalITBISPercepcion"]),
                    TotalISRPercepcion = Metodos_General.TryParseDecimal(Data, "TotalISRPercepcion"),
                    TotalISRPercepcionSpecified = Metodos_General.EsNumero(Data["TotalISRPercepcion"])
                },
                OtraMoneda = new ECF_31.ECFEncabezadoOtraMoneda() 
                {
                    TipoMoneda = ObtenerValor.ObtenerTipoGeneral<ECF_31.TipoMonedaType>(Data["TipoMoneda"]),
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
                    MontoImpuestoAdicionalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoImpuestoAdicionalOtraMoneda"),
                    MontoImpuestoAdicionalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoImpuestoAdicionalOtraMoneda"]),
                    ImpuestosAdicionalesOtraMoneda = ObtenerValor.ObtenerImpuestosAdicionalesOtraMoneda(Data),
                    MontoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoTotalOtraMoneda"),
                    MontoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoTotalOtraMoneda"]),

                },
            };
            eCF_31.DetallesItems = ObtenerValor.ObtenerDetalleItem(Data);



            XmlSerializer serializer = new XmlSerializer(typeof(ECF_31.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_31);
                string xmlOutput = writer.ToString();

                string a = Metodos_General.XmlCorrector.CorrectXml(xmlOutput, "E:\\Proyectos\\M_I_FE\\M_I_FE\\XSD\\e-CF 31 v.1.0.xsd");
                Console.WriteLine(a);
            }
        }
    }
}

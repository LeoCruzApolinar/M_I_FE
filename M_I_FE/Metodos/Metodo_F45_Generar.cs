﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace M_I_FE.Metodos
{
    public class Metodo_F45_Generar
    {
        public class ObtenerValorECF_45()
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

            public static ECF_45.ECFEncabezadoIdDocFormaDePago[] ObtenerTablaFormasPago(Dictionary<string, string> datos)
            {
                List<ECF_45.ECFEncabezadoIdDocFormaDePago> listaFormasPago = new List<ECF_45.ECFEncabezadoIdDocFormaDePago>();

                int indice = 1;
                while (true)
                {
                    string formaPagoKey = $"FormaPago[{indice}]";
                    string montoPagoKey = $"MontoPago[{indice}]";

                    if (datos.ContainsKey(formaPagoKey) && datos.ContainsKey(montoPagoKey))
                    {
                        if (Metodos_General.EsNumero(datos[formaPagoKey]) && Metodos_General.EsNumero(datos[montoPagoKey]))
                        {
                            ECF_45.ECFEncabezadoIdDocFormaDePago formaPago = new ECF_45.ECFEncabezadoIdDocFormaDePago
                            {
                                FormaPago = ObtenerTipoGeneral<ECF_45.FormaPagoType>(datos[formaPagoKey]),
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

            public static ECF_45.ECFEncabezadoTotalesImpuestoAdicional[] ObtenerImpuestosAdicionales(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFEncabezadoTotalesImpuestoAdicional> ImpuestosAdicionales = new List<ECF_45.ECFEncabezadoTotalesImpuestoAdicional>();
                int indice = 1;
                while (Data != null)
                {


                    if (Data.ContainsKey($"TipoImpuesto[{indice}]"))
                    {
                        if (Data[$"TipoImpuesto[{indice}]"] != null)
                        {


                            ECF_45.ECFEncabezadoTotalesImpuestoAdicional eCFEncabezadoTotalesImpuestoAdicional = new ECF_45.ECFEncabezadoTotalesImpuestoAdicional()
                            {
                                TipoImpuesto = ObtenerTipoGeneral<ECF_45.CodificacionTipoImpuestosType>(Data[$"TipoImpuesto[{indice}]"]),
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

            public static ECF_45.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda[] ObtenerImpuestosAdicionalesOtraMoneda(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda> ImpuestosAdicionalesOtraMoneda = new List<ECF_45.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoImpuestoOtraMoneda[{indice}]"))
                    {
                        if (Data[$"TipoImpuestoOtraMoneda[{indice}]"] != null)
                        {

                            ECF_45.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda eCFEncabezadoTotalesImpuestoAdicionalOtraMoneda = new ECF_45.ECFEncabezadoOtraMonedaImpuestoAdicionalOtraMoneda()
                            {
                                TipoImpuestoOtraMoneda = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.CodificacionTipoImpuestosType>(Data[$"TipoImpuestoOtraMoneda[{indice}]"]),
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

            public static ECF_45.ECFItem[] ObtenerDetalleItem(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFItem> eCFItems = new List<ECF_45.ECFItem>();
                int indice = 1;

                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroLinea[{indice}]"))
                    {
                        if (Data[$"NumeroLinea[{indice}]"] != null)
                        {

                            ECF_45.ECFItem eCFItem = new ECF_45.ECFItem()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TablaCodigosItem = ObtenerCodigosItems(Data, indice),
                                IndicadorFacturacion = ObtenerTipoGeneral<ECF_45.IndicadorFacturacionType>(Data[$"IndicadorFacturacion[{indice}]"]),
                                NombreItem = Data[$"NombreItem[{indice}]"],
                                IndicadorBienoServicio = ObtenerTipoGeneral<ECF_45.IndicadorBienoServicioType>(Data[$"IndicadorBienoServicio[{indice}]"]),
                                DescripcionItem = Data[$"DescripcionItem[{indice}]"],
                                CantidadItem = Metodos_General.TryParseDecimal(Data, $"CantidadItem[{indice}]"),
                                UnidadMedida = ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data[$"UnidadMedida[{indice}]"]),
                                UnidadMedidaSpecified = Metodos_General.EsNumero(Data[$"UnidadMedida[{indice}]"]),
                                CantidadReferencia = Metodos_General.TryParseDecimal(Data, $"CantidadReferencia[{indice}]"),
                                CantidadReferenciaSpecified = Metodos_General.EsNumero(Data[$"CantidadReferencia[{indice}]"]),
                                UnidadReferencia = ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data[$"UnidadReferencia[{indice}]"]),
                                UnidadReferenciaSpecified = Metodos_General.EsNumero(Data[$"UnidadReferencia[{indice}]"]),
                                TablaSubcantidad = ObtenerECFItemSubcantidadItemTabla(Data),
                                GradosAlcohol = Metodos_General.TryParseDecimal(Data, $"GradosAlcohol[{indice}]"),
                                GradosAlcoholSpecified = Metodos_General.EsNumero(Data[$"GradosAlcohol[{indice}]"]),
                                PrecioUnitarioReferencia = Metodos_General.TryParseDecimal(Data, $"PrecioUnitarioReferencia[{indice}]"),
                                PrecioUnitarioReferenciaSpecified = Metodos_General.EsNumero(Data[$"PrecioUnitarioReferencia[{indice}]"]),
                                FechaElaboracion = Data[$"FechaElaboracion[{indice}]"],
                                FechaVencimientoItem = Data[$"FechaVencimientoItem[{indice}]"],
                                PrecioUnitarioItem = Metodos_General.TryParseDecimal(Data, $"PrecioUnitarioItem[{indice}]"),
                                DescuentoMonto = Metodos_General.TryParseDecimal(Data, $"DescuentoMonto[{indice}]"),
                                DescuentoMontoSpecified = Metodos_General.EsNumero(Data[$"DescuentoMonto[{indice}]"]),
                                TablaSubDescuento = ObtenerTablaECFItemSubDescuento(Data),
                                RecargoMonto = Metodos_General.TryParseDecimal(Data, $"RecargoMonto[{indice}]"),
                                RecargoMontoSpecified = Metodos_General.EsNumero(Data[$"RecargoMonto[{indice}]"]),
                                TablaSubRecargo = ObtenerTablaECFItemSubRecargo(Data),
                                TablaImpuestoAdicional = ObtenerTablaImpuestoAdicional(Data),
                                OtraMonedaDetalle = new ECF_45.ECFItemOtraMonedaDetalle()
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

            public static ECF_45.ECFItemCodigosItem[] ObtenerCodigosItems(Dictionary<string, string> Data, int ind)
            {
                List<ECF_45.ECFItemCodigosItem> eCFItemCodigosItems = new List<ECF_45.ECFItemCodigosItem>();

                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoCodigo[{ind}][{indice}]"))
                    {
                        if (Data[$"TipoCodigo[{ind}][{indice}]"] != null)
                        {

                            ECF_45.ECFItemCodigosItem eCFItemCodigosItem = new ECF_45.ECFItemCodigosItem()
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

            public static ECF_45.ECFItemSubcantidadItem[] ObtenerECFItemSubcantidadItemTabla(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFItemSubcantidadItem> eCFItemSubcantidadItems = new List<ECF_45.ECFItemSubcantidadItem>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"Subcantidad[{indice}]"))
                    {
                        if (Data[$"Subcantidad[{indice}]"] != null)
                        {

                            ECF_45.ECFItemSubcantidadItem data = new ECF_45.ECFItemSubcantidadItem()
                            {
                                Subcantidad = Metodos_General.TryParseDecimal(Data, $"Subcantidad[{indice}]"),
                                SubcantidadSpecified = Metodos_General.EsNumero(Data[$"Subcantidad[{indice}]"]),
                                CodigoSubcantidad = ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data[$"CodigoSubcantidad[{indice}]"]),
                                CodigoSubcantidadSpecified = Metodos_General.EsNumero(Data[$"CodigoSubcantidad[{indice}]"]),
                            };
                            // Agregar el objeto a la lista
                            eCFItemSubcantidadItems.Add(data);

                        }
                        indice++;
                    }
                    else
                    {
                        break;
                    }

                }



                return eCFItemSubcantidadItems.Count == 0 ? default : eCFItemSubcantidadItems.ToArray();
            }

            public static ECF_45.ECFItemSubDescuento[] ObtenerTablaECFItemSubDescuento(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFItemSubDescuento> ListaECFItemSubDescuento = new List<ECF_45.ECFItemSubDescuento>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubDescuento[{indice}]"))
                    {
                        if (Data[$"TipoSubDescuento[{indice}]"] != null)
                        {

                            ECF_45.ECFItemSubDescuento data = new ECF_45.ECFItemSubDescuento()
                            {
                                TipoSubDescuento = ObtenerTipoGeneral<ECF_45.ECFItemSubDescuento>(Data[$"TipoSubDescuento[{indice}]"]),
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

            public static ECF_45.ECFItemSubRecargo[] ObtenerTablaECFItemSubRecargo(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFItemSubRecargo> ListaECFItemSubRecargo = new List<ECF_45.ECFItemSubRecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubRecargo[{indice}]"))
                    {
                        if (Data[$"TipoSubRecargo[{indice}]"] != null)
                        {

                            ECF_45.ECFItemSubRecargo data = new ECF_45.ECFItemSubRecargo()
                            {
                                TipoSubRecargo = ObtenerTipoGeneral<ECF_45.ECFItemSubRecargo>($"TipoSubRecargo[{indice}]"),
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

            public static ECF_45.ECFItemImpuestoAdicional[] ObtenerTablaImpuestoAdicional(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFItemImpuestoAdicional> ListaImpuestoAdicional = new List<ECF_45.ECFItemImpuestoAdicional>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"TipoSubRecargo[{indice}]"))
                    {
                        if (Data[$"TipoSubRecargo[{indice}]"] != null)
                        {

                            ECF_45.ECFItemImpuestoAdicional data = new ECF_45.ECFItemImpuestoAdicional()
                            {
                                TipoImpuesto = ObtenerTipoGeneral<ECF_45.ECFItemImpuestoAdicional>($"TipoImpuesto[{indice}]"),
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

            public static ECF_45.ECFSubtotal[] ObtenerSubtotalesl(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFSubtotal> ListaSubtotales = new List<ECF_45.ECFSubtotal>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal[{indice}]"))
                    {
                        if (Data[$"NumeroSubTotal[{indice}]"] != null)
                        {

                            ECF_45.ECFSubtotal data = new ECF_45.ECFSubtotal()
                            {
                                NumeroSubTotal = Data["NumeroSubTotal"],
                                DescripcionSubtotal = Data["DescripcionSubtotal"],
                                Orden = Data["Orden"],
                                SubTotalMontoGravadoTotal = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoTotal[{indice}]"),
                                SubTotalMontoGravadoTotalSpecified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoTotal[{indice}]"]),
                                SubTotalMontoGravadoI1 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI1[{indice}]"),
                                SubTotalMontoGravadoI1Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI1[{indice}]"]),
                                SubTotalMontoGravadoI2 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI2[{indice}]"),
                                SubTotalMontoGravadoI2Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI2[{indice}]"]),
                                SubTotalMontoGravadoI3 = Metodos_General.TryParseDecimal(Data, $"SubTotalMontoGravadoI3[{indice}]"),
                                SubTotalMontoGravadoI3Specified = Metodos_General.EsNumero(Data[$"SubTotalMontoGravadoI3[{indice}]"]),
                                SubTotaITBIS = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS[{indice}]"),
                                SubTotaITBISSpecified = Metodos_General.EsNumero(Data[$"SubTotaITBIS[{indice}]"]),
                                SubTotaITBIS1 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS1[{indice}]"),
                                SubTotaITBIS1Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS1[{indice}]"]),
                                SubTotaITBIS2 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS2[{indice}]"),
                                SubTotaITBIS2Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS2[{indice}]"]),
                                SubTotaITBIS3 = Metodos_General.TryParseDecimal(Data, $"SubTotaITBIS3[{indice}]"),
                                SubTotaITBIS3Specified = Metodos_General.EsNumero(Data[$"SubTotaITBIS3[{indice}]"]),
                                SubTotalImpuestoAdicional = Metodos_General.TryParseDecimal(Data, $"SubTotalImpuestoAdicional[{indice}]"),
                                SubTotalImpuestoAdicionalSpecified = Metodos_General.EsNumero(Data[$"SubTotalImpuestoAdicional[{indice}]"]),
                                SubTotalExento = Metodos_General.TryParseDecimal(Data, $"SubTotalExento[{indice}]"),
                                SubTotalExentoSpecified = Metodos_General.EsNumero(Data[$"SubTotalExento[{indice}]"]),
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

            public static ECF_45.ECFDescuentoORecargo[] ObtenerDescuentosORecargos(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFDescuentoORecargo> ListaDescuentosORecargos = new List<ECF_45.ECFDescuentoORecargo>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal[{indice}]"))
                    {
                        if (Data[$"NumeroSubTotal[{indice}]"] != null)
                        {

                            ECF_45.ECFDescuentoORecargo data = new ECF_45.ECFDescuentoORecargo()
                            {
                                NumeroLinea = Data[$"NumeroLinea[{indice}]"],
                                TipoAjuste = ObtenerTipoGeneral<ECF_45.TipoAjusteType>($"TipoAjuste[{indice}]"),
                                IndicadorNorma1007 = ObtenerTipoGeneral<ECF_45.IndicadorNorma1007Type>($"IndicadorNorma1007[{indice}]"),
                                IndicadorNorma1007Specified = Metodos_General.EsNumero(Data[$"IndicadorNorma1007[{indice}]"]),
                                DescripcionDescuentooRecargo = Data["DescripcionDescuentooRecargo"],
                                TipoValor = ObtenerTipoGeneral<ECF_45.TipoDescuentoRecargoType>($"TipoValor[{indice}]"),
                                TipoValorSpecified = Metodos_General.EsNumero(Data[$"TipoValor[{indice}]"]),
                                ValorDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"ValorDescuentooRecargo[{indice}]"),
                                ValorDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"ValorDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargo = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargo[{indice}]"),
                                MontoDescuentooRecargoSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargo[{indice}]"]),
                                MontoDescuentooRecargoOtraMoneda = Metodos_General.TryParseDecimal(Data, $"MontoDescuentooRecargoOtraMoneda[{indice}]"),
                                MontoDescuentooRecargoOtraMonedaSpecified = Metodos_General.EsNumero(Data[$"MontoDescuentooRecargoOtraMoneda[{indice}]"]),
                                IndicadorFacturacionDescuentooRecargo = ObtenerTipoGeneral<ECF_45.IndicadorFacturacionDRType>($"IndicadorFacturacionDescuentooRecargo[{indice}]"),
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

            public static ECF_45.ECFPagina[] ObtenerECFPagina(Dictionary<string, string> Data)
            {
                List<ECF_45.ECFPagina> ListaECFPagina = new List<ECF_45.ECFPagina>();
                int indice = 1;
                while (Data != null)
                {

                    if (Data.ContainsKey($"NumeroSubTotal[{indice}]"))
                    {
                        if (Data[$"NumeroSubTotal[{indice}]"] != null)
                        {

                            ECF_45.ECFPagina data = new ECF_45.ECFPagina()
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
                                SubtotalImpuestoAdicionalPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalImpuestoAdicionalPagina[{indice}]"),
                                SubtotalImpuestoAdicionalPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalImpuestoAdicionalPagina[{indice}]"]),
                                SubtotalImpuestoAdicional = new ECF_45.ECFPaginaSubtotalImpuestoAdicional()
                                {
                                    SubtotalImpuestoSelectivoConsumoEspecificoPagina = Metodos_General.TryParseDecimal(Data, $"SubtotalImpuestoSelectivoConsumoEspecificoPagina[{indice}]"),
                                    SubtotalImpuestoSelectivoConsumoEspecificoPaginaSpecified = Metodos_General.EsNumero(Data[$"SubtotalImpuestoSelectivoConsumoEspecificoPagina[{indice}]"]),
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
        public static void Generar_XML_ECF45(Dictionary<string, string> Data)
        {
            ECF_45.ECF eCF_45 = new ECF_45.ECF();
            eCF_45.Encabezado = new ECF_45.ECFEncabezado()
            {
                Version = Metodos_General.TryParseDecimal(Data, "Version"),
                IdDoc = new ECF_45.ECFEncabezadoIdDoc()
                {
                    TipoeCF = ECF_45.TipoeCFType.Item45,
                    eNCF = Data["ENCF"],
                    FechaVencimientoSecuencia = Data["FechaVencimientoSecuencia"],
                    IndicadorEnvioDiferidoSpecified = (Data["IndicadorEnvioDiferido"] == "1" || Data["IndicadorEnvioDiferido"] == "0"),
                    IndicadorEnvioDiferido = ECF_45.IndicadorEnvioDiferidoType.Item1,
                    IndicadorMontoGravadoSpecified = (Data["IndicadorMontoGravado"] == "1" || Data["IndicadorMontoGravado"] == "0"),
                    IndicadorMontoGravado = (Data["IndicadorMontoGravado"] == "1") ? ECF_45.IndicadorMontoGravadoType.Item1 : ECF_45.IndicadorMontoGravadoType.Item0,
                    TipoIngresos = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.TipoIngresosValidationType>(Data["TipoIngresos"]),
                    TipoPago = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.TipoPagoType>(Data["TipoPago"]),
                    FechaLimitePago = Data["FechaLimitePago"],
                    TerminoPago = Data["TerminoPago"],
                    TablaFormasPago = ObtenerValorECF_45.ObtenerTablaFormasPago(Data),
                    TipoCuentaPago = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.TipoCuentaPagoType>(Data["TipoCuentaPago"]),
                    NumeroCuentaPago = Data["NumeroCuentaPago"],
                    BancoPago = Data["BancoPago"],
                    FechaDesde = Data["FechaDesde"],
                    FechaHasta = Data["FechaHasta"],
                    TotalPaginas = Data["TotalPaginas"]
                },
                Emisor = new ECF_45.ECFEncabezadoEmisor()
                {
                    RNCEmisor = Data["RNCEmisor"],
                    RazonSocialEmisor = Data["RazonSocialEmisor"],
                    NombreComercial = Data["NombreComercial"],
                    Sucursal = Data["Sucursal"],
                    DireccionEmisor = Data["DireccionEmisor"],
                    MunicipioSpecified = Data.ContainsKey("Municipio") && Data["Municipio"] != null,
                    Municipio = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.ProvinciaMunicipioType>(Data["Municipio"]),
                    ProvinciaSpecified = Data.ContainsKey("Provincia") && Data["Provincia"] != null,
                    Provincia = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.ProvinciaMunicipioType>(Data["Provincia"]),
                    TablaTelefonoEmisor = ObtenerValorECF_45.ObtenerTablaTelefonoEmisor(Data),
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
                Comprador = new ECF_45.ECFEncabezadoComprador()
                {
                    RNCComprador = Data["RNCComprador"],
                    RazonSocialComprador = Data["RazonSocialComprador"],
                    ContactoComprador = Data["ContactoComprador"],
                    CorreoComprador = Data["CorreoComprador"],
                    DireccionComprador = Data["DireccionComprador"],
                    MunicipioCompradorSpecified = Data.ContainsKey("MunicipioComprador") && Data["MunicipioComprador"] != null,
                    MunicipioComprador = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.ProvinciaMunicipioType>(Data["MunicipioComprador"]),
                    ProvinciaCompradorSpecified = Data.ContainsKey("ProvinciaComprador") && Data["ProvinciaComprador"] != null,
                    ProvinciaComprador = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.ProvinciaMunicipioType>(Data["ProvinciaComprador"]),
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
                InformacionesAdicionales = new ECF_45.ECFEncabezadoInformacionesAdicionales
                {
                    FechaEmbarque = Data["FechaEmbarque"],
                    NumeroEmbarque = Data["NumeroEmbarque"],
                    NumeroContenedor = Data["NumeroContenedor"],
                    NumeroReferencia = Data["NumeroReferencia"],
                    PesoBruto = Metodos_General.TryParseDecimal(Data, "PesoBruto"),
                    PesoBrutoSpecified = Metodos_General.EsNumero(Data["PesoBruto"]),
                    PesoNeto = Metodos_General.TryParseDecimal(Data, "PesoNeto"),
                    PesoNetoSpecified = Metodos_General.EsNumero(Data["PesoNeto"]),
                    UnidadPesoBruto = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data["UnidadPesoBruto"]),
                    UnidadPesoBrutoSpecified = Metodos_General.EsNumero(Data["UnidadPesoBruto"]),
                    UnidadPesoNeto = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data["UnidadPesoNeto"]),
                    UnidadPesoNetoSpecified = Metodos_General.EsNumero(Data["UnidadPesoNeto"]),
                    CantidadBulto = Metodos_General.TryParseDecimal(Data, "CantidadBulto"),
                    CantidadBultoSpecified = Metodos_General.EsNumero(Data["CantidadBulto"]),
                    UnidadBulto = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data["UnidadBulto"]),
                    UnidadBultoSpecified = Metodos_General.EsNumero(Data["UnidadBulto"]),
                    VolumenBulto = Metodos_General.TryParseDecimal(Data, "VolumenBulto"),
                    VolumenBultoSpecified = Metodos_General.EsNumero(Data["VolumenBulto"]),
                    UnidadVolumen = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.UnidadMedidaType>(Data["UnidadVolumen"]),
                    UnidadVolumenSpecified = Metodos_General.EsNumero(Data["UnidadVolumen"]),
                },
                Transporte = new ECF_45.ECFEncabezadoTransporte()
                {
                    Conductor = Data["Conductor"],
                    DocumentoTransporte = Data["DocumentoTransporte"],
                    Ficha = Data["Ficha"],
                    Placa = Data["Placa"],
                    RutaTransporte = Data["RutaTransporte"],
                    ZonaTransporte = Data["ZonaTransporte"],
                    NumeroAlbaran = Data["NumeroAlbaran"],
                },
                Totales = new ECF_45.ECFEncabezadoTotales()
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
                    ImpuestosAdicionales = ObtenerValorECF_45.ObtenerImpuestosAdicionales(Data),
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
                OtraMoneda = new ECF_45.ECFEncabezadoOtraMoneda()
                {
                    TipoMoneda = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.TipoMonedaType>(Data["TipoMoneda"]),
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
                    ImpuestosAdicionalesOtraMoneda = ObtenerValorECF_45.ObtenerImpuestosAdicionalesOtraMoneda(Data),
                    MontoTotalOtraMoneda = Metodos_General.TryParseDecimal(Data, "MontoTotalOtraMoneda"),
                    MontoTotalOtraMonedaSpecified = Metodos_General.EsNumero(Data["MontoTotalOtraMoneda"]),

                },
            };
            eCF_45.DetallesItems = ObtenerValorECF_45.ObtenerDetalleItem(Data);
            eCF_45.Subtotales = ObtenerValorECF_45.ObtenerSubtotalesl(Data);
            eCF_45.DescuentosORecargos = ObtenerValorECF_45.ObtenerDescuentosORecargos(Data);
            eCF_45.Paginacion = ObtenerValorECF_45.ObtenerECFPagina(Data);
            eCF_45.InformacionReferencia = new ECF_45.ECFInformacionReferencia
            {
                NCFModificado = Data["NCFModificado"],
                RNCOtroContribuyente = Data["RNCOtroContribuyente"],
                FechaNCFModificado = Data["FechaNCFModificado"],
                CodigoModificacion = ObtenerValorECF_45.ObtenerTipoGeneral<ECF_45.CodigoModificacionType>(Data["CodigoModificacion"]),
                CodigoModificacionSpecified = Metodos_General.EsNumero(Data["CodigoModificacion"]),
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ECF_45.ECF));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, eCF_45);
                Metodos_General.SaveContentToFile(writer.ToString(), "45");
            }
        }
    }
}

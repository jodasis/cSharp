using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryCompilePepe
{
    public class Pepematic
    {

        public string[] memoria = new string[255];
        public int linea = 0;
        public string acumulador = "0";
        public string instructionActual = "", codOperation = "";
        public int operando;
        public string tipoRespuesta = "";
        public const string LEER = "10", ESCRIBIR = "11", LEER_CADENA = "12", ESCRIBIR_CADENA = "13", ESCRIBIR_LINEA = "14";
        public const string CARGAR = "20", ALMACENAR = "21";
        public const string SUMAR = "30", RESTAR = "31", MULTIPLICAR = "32", DIVIDIR = "33", MODULO = "34", POTENCIA = "35";
        public const string SALTAR = "40", SALTAR_NEG = "41", SALTAR_CERO = "42", ALTO = "43";
        public const string INT = "50", DECIMAL = "51";

        public Pepematic(String rutaArchivo)
        {
            using (StreamReader archivo = new StreamReader(rutaArchivo))
            {
                int lineaActual = 0;
                while (!archivo.EndOfStream)
                    memoria[lineaActual++] = (archivo.ReadLine());
            }
        }

        public void Ejecutar()
        {
            linea = 0;
            while (linea >= 0)
            {
                instructionActual = memoria[linea++] + "";
                selectorInstruction(instructionActual);
            }
        }

        public void selectorInstruction(string regActual)
        {
            if (regActual != null)
            {
                codOperation = regActual.Substring(0, 2);
                operando = Convert.ToInt32(regActual.Substring(2), 16);
                int posIni = 0;
                switch (codOperation)
                {
                    case INT:
                    case DECIMAL:
                        tipoRespuesta = codOperation;
                        break;
                    case LEER:
                        Console.Write("Ingrese un numero entero: ");
                        memoria[Convert.ToInt32(operando)] = Console.ReadLine() + "";
                        break;
                    case ESCRIBIR:
                        Console.WriteLine("Fin del programa.");
                        Console.WriteLine("> " + memoria[Convert.ToInt32(operando)]);
                        break;
                    case CARGAR:
                        acumulador = Convert.ToString(memoria[Convert.ToInt32(operando)]);
                        break;
                    case ALMACENAR:
                        memoria[Convert.ToInt32(operando)] = Convert.ToString(acumulador);
                        break;
                    case SUMAR:
                        if ((tipoRespuesta == INT))
                        {
                            acumulador = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(acumulador)) + Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])));
                        }
                        else
                        {
                            acumulador = Convert.ToString(Convert.ToDecimal(acumulador) + Convert.ToDecimal(memoria[Convert.ToInt32(operando)]));
                        }
                        break;
                    case RESTAR:
                        if ((tipoRespuesta == INT))
                        {
                            acumulador = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(acumulador)) - Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])));
                        }
                        else
                        {
                            acumulador = Convert.ToString(Convert.ToDecimal(acumulador) - Convert.ToDecimal(memoria[Convert.ToInt32(operando)]));
                        }
                        break;
                    case MULTIPLICAR:
                        if ((tipoRespuesta == INT))
                        {
                            acumulador = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(acumulador)) * Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])));
                        }
                        else
                        {
                            acumulador = Convert.ToString(Convert.ToDecimal(acumulador) * Convert.ToDecimal(memoria[Convert.ToInt32(operando)]));
                        }
                        break;
                    case DIVIDIR:
                        if (memoria[Convert.ToInt32(operando)] != "0")
                            if ((tipoRespuesta == INT))
                            {
                                acumulador = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(acumulador)) / Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])));
                            }
                            else
                            {
                                acumulador = Convert.ToString(Convert.ToDecimal(acumulador) / Convert.ToDecimal(memoria[Convert.ToInt32(operando)]));
                            }
                        else
                        {
                            Console.WriteLine("Error: division por 0");
                            Console.WriteLine("Iniciar de nuevo el programa...");
                            linea = 0;
                        }
                        break;
                    case MODULO:
                        if (memoria[Convert.ToInt32(operando)] != "0")
                            if ((tipoRespuesta == INT))
                            {
                                acumulador = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(acumulador)) % Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])));
                            }
                            else
                            {
                                acumulador = Convert.ToString(Convert.ToDecimal(acumulador) % Convert.ToDecimal(memoria[Convert.ToInt32(operando)]));
                            }
                        else
                        {
                            Console.WriteLine("Error: division por 0");
                            Console.WriteLine("Iniciar de nuevo el programa...");
                            linea = 0;
                        }
                        break;
                    case POTENCIA:
                        if (memoria[Convert.ToInt32(operando)] != "0")
                            if ((tipoRespuesta == INT))
                            {
                                acumulador = Convert.ToString(Convert.ToInt32(Math.Pow(Convert.ToInt32(acumulador), Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])))));
                            }
                            else
                            {
                                acumulador = Convert.ToString(Convert.ToDecimal(Math.Pow(Convert.ToInt32(acumulador), Convert.ToInt32(Convert.ToDecimal(memoria[Convert.ToInt32(operando)])))));
                            }
                        else
                        {
                            Console.WriteLine("Error: division por 0");
                            Console.WriteLine("Iniciar de nuevo el programa...");
                            linea = 0;
                        }
                        break;
                    case SALTAR:
                        linea = -1;
                        break;
                    case SALTAR_NEG:
                        if (Convert.ToInt32(acumulador) < 0)
                            linea = Convert.ToInt32(operando);
                        break;
                    case SALTAR_CERO:
                        if (Convert.ToInt32(acumulador) == 0)
                            linea = Convert.ToInt32(operando);
                        break;
                    case LEER_CADENA:
                        Console.Write("Ingrese una cadena: ");
                        char[] cadena = (Console.ReadLine() + "").ToCharArray();
                        posIni = Convert.ToInt32(operando);
                        int count = cadena.Length;
                        string tamStr = cadena.Length < 10 ? "0" + cadena.Length : cadena.Length + "";
                        foreach (var item in cadena)
                        {
                            int ascii = (int)item;
                            string hexaDecimal = ascii.ToString("x");
                            memoria[posIni++] = tamStr + hexaDecimal;
                        }
                        break;
                    case ESCRIBIR_CADENA:
                        Console.WriteLine("Fin del programa.");
                        posIni = Convert.ToInt32(operando);
                        int pos = 0;
                        int tamInt = Convert.ToInt32(memoria[posIni].Substring(0, 2));
                        for (int i = 0; i < tamInt; i++)
                        {
                            pos = posIni++;
                            Char letra = Convert.ToChar(System.Convert.ToUInt32(memoria[pos].Substring(2), 16));
                            Console.WriteLine("> " + letra + " - " + (memoria[pos].Substring(2)).ToUpper());
                        }
                        break;
                    case ESCRIBIR_LINEA:
                        Console.WriteLine("====================================================================");
                        break;
                    case ALTO:
                        Console.ReadKey();
                        linea = -1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

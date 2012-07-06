/*
 * Created by SharpDevelop.
 * User: Fenriz
 * Date: 14/05/2012
 * Time: 08:11 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Memorama
{
	/// <summary>
	/// Description of LogicaJuego.
	/// </summary>
	public class LogicaJuego
	{
		
		private char[,] cartas;
		private readonly int LARGO;
		private readonly int ANCHO;
		private const int INICIO_MINUSCULAS = 97;
		private readonly int CANTIDAD_LETRAS;
		private const int MAXIMO_JUGADORES = 4;
		private int turnoActual = 1;
		private int paresDescubiertos = 0;
		private int[] puntajes;
		
		private char[] valoresAUsar;
		
		public char[,] Cartas{
			get{
				return cartas;
			}
		}
		
		public int ParesDescubiertos{
			set{
				paresDescubiertos = value;				
			}
			get{
				return paresDescubiertos;
			}
		}
		
		public int TurnoActual{
			set{
				if(value <= MAXIMO_JUGADORES){
					turnoActual = value;				
				}else{
					turnoActual = 1;
				}
			}
			get{
				return turnoActual;
			}
		}
		
		public int[] Puntajes{
			get{
				return puntajes;	
			}
			set{
				puntajes = value;
			}
		}
		
		public LogicaJuego(int largo, int ancho)
		{
			LARGO = largo;
			ANCHO = ancho;
			CANTIDAD_LETRAS =  (LARGO * ANCHO) / 2;
			puntajes = new int[MAXIMO_JUGADORES];
			cartas = new char[LARGO,ANCHO];
			valoresAUsar = new char[CANTIDAD_LETRAS];
			inicializarTableros();
			inicializaPuntajes();
			revolverTablero();
			//imprimirTabla();
		}
		
		public int dameGanador(){
			int ganador = 0;
			for(int i = 1; i < MAXIMO_JUGADORES; i++){
				if(puntajes[i] > puntajes[ganador]){
					ganador = i;
				}
			}
			return ganador + 1;
			
		}
		
		public bool hayGanador(){
			return (paresDescubiertos == CANTIDAD_LETRAS);			
		}
		

		void inicializaPuntajes(){
			for(int i = 0; i < MAXIMO_JUGADORES; i++){
				puntajes[i] = 0;
			}
			
		}
		
		public void revolverTablero(){
			Random valores = new Random(DateTime.Now.Millisecond);
			for(int i = LARGO - 1; i > 0; i--){
				for(int j = ANCHO - 1; j > 0; j--){
					int swapI = valores.Next(i+1);
					int swapJ = valores.Next(j+1);
					if(swapI != i || swapJ != j){
						char tmp = cartas[swapI,swapJ];
			            cartas[swapI,swapJ] = cartas[i,j];
			            cartas[i,j] = tmp;						
					}
				}				
			}			
		}

		
		public bool repetido(char temp){

			for(int i = 0; i < CANTIDAD_LETRAS; i++){
				if(valoresAUsar[i] == temp){
					return true;
				}							
			}
			return false;
			
		}
	
		public void inicializarTableros(){
			Random numero = new Random(DateTime.Now.Millisecond);
			int k = 0;
			for (int i = 0; i < LARGO; i++){
				for (int j = 0; j < ANCHO; j++){
					char temp;					
					do{
						temp = (char)(numero.Next(CANTIDAD_LETRAS) + INICIO_MINUSCULAS);
					}while(repetido(temp));									
					cartas[i,j] = temp;
					j++;
					cartas[i,j] = temp;	
				
					valoresAUsar[k] = temp;
					k++;
				}
				
			}			
		}
		
		public void imprimirTabla(){
			for (int i = 0; i < LARGO; i++){
				for (int j = 0; j < ANCHO; j++){
					System.Diagnostics.Debug.Write(" " +  cartas[i,j].ToString() + " ");
				}
				System.Diagnostics.Debug.WriteLine("");
			}	
		}
		
	}
}

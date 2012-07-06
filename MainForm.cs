/*
 * Created by SharpDevelop.
 * User: Fenriz
 * Date: 14/05/2012
 * Time: 08:11 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Memorama
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private const int NUMERO_FILAS = 4;
		private const int NUMERO_COLUMNAS = 6;
		private const int ARROBA = 64;
		private const string IMAGEN_FONDO = "img\\_back.png";
		private const int RETRASO_OCULTAMIENTO = 1000;
		private LogicaJuego tablero;
		public PictureBox[,] imagenes;
		public Image[,] matrizRespaldo;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			InitializeComponent();
			imagenes = new PictureBox[NUMERO_FILAS,NUMERO_COLUMNAS];
			matrizRespaldo = new Image[NUMERO_FILAS,NUMERO_COLUMNAS];
			tablero = new LogicaJuego(NUMERO_FILAS,NUMERO_COLUMNAS);

			llenarMatrizImagenes();
			mostrarCartas();
			Timer contador = new Timer();
			contador.Interval = (RETRASO_OCULTAMIENTO);
			contador.Tick += new EventHandler(contar);
			contador.Start();
		}
		
		void contar(object sender, EventArgs e){
			Timer temp = sender as Timer;
			voltearCartas();
			temp.Stop();			
		}
		
		public void mostrarCartas(){
			for (int i = 0; i < NUMERO_FILAS;i++){
				for (int j = 0; j < NUMERO_COLUMNAS;j++){					
					string valor = "img\\" + tablero.Cartas[i,j] + ".png";
					imagenes[i,j].Image = Image.FromFile(valor);					
				}				
			}			
		}
		
		public void voltearCartas(){
			for (int i = 0; i < NUMERO_FILAS;i++){
				for (int j = 0; j < NUMERO_COLUMNAS;j++){	
					Image valor = Image.FromFile(IMAGEN_FONDO);						
					imagenes[i,j].Image = valor;
					matrizRespaldo[i,j] = valor;
				}				
			}			
		}
		
		
		void llenarMatrizImagenes(){
			this.imagenes[0,0] = this.pictureBox1;
			this.imagenes[0,1] = this.pictureBox2;
			this.imagenes[0,2] = this.pictureBox3;
			this.imagenes[0,3] = this.pictureBox4;
			this.imagenes[0,4] = this.pictureBox5;
			this.imagenes[0,5] = this.pictureBox6;
			this.imagenes[1,0] = this.pictureBox7;
			this.imagenes[1,1] = this.pictureBox8;
			this.imagenes[1,2] = this.pictureBox9;
			this.imagenes[1,3] = this.pictureBox10;
			this.imagenes[1,4] = this.pictureBox11;
			this.imagenes[1,5] = this.pictureBox12;
			this.imagenes[2,0] = this.pictureBox13;
			this.imagenes[2,1] = this.pictureBox14;
			this.imagenes[2,2] = this.pictureBox15;
			this.imagenes[2,3] = this.pictureBox16;
			this.imagenes[2,4] = this.pictureBox17;
			this.imagenes[2,5] = this.pictureBox18;
			this.imagenes[3,0] = this.pictureBox19;
			this.imagenes[3,1] = this.pictureBox20;
			this.imagenes[3,2] = this.pictureBox21;
			this.imagenes[3,3] = this.pictureBox22;
			this.imagenes[3,4] = this.pictureBox23;
			this.imagenes[3,5] = this.pictureBox24;		
		}
		
		void desactivarCarta(int x, int y){
			imagenes[x,y].Enabled = false;			
		}
		
		void voltearCarta(int x, int y){
			imagenes[x,y].Image = Image.FromFile(IMAGEN_FONDO);
		}
		
		private int x,y,finalI,finalJ;
		private char comparador = (char)ARROBA;
		
		void comprobarCarta(object sender, EventArgs e)
		{
			PictureBox temp = sender as PictureBox;
			Image imagenActual;
			char letraActual;
			for(int i = 0; i < NUMERO_FILAS; i++){
				for(int j = 0; j < NUMERO_COLUMNAS; j++){
					if(imagenes[i,j] == temp){
						letraActual = tablero.Cartas[i,j];
						imagenActual = Image.FromFile("img\\" + letraActual + ".png");						
						imagenes[i,j].Image = imagenActual;
						if(comparador != (64)){
							if (letraActual == comparador){
								desactivarCarta(i,j);
								desactivarCarta(x,y);	
								tablero.ParesDescubiertos++;
								tablero.Puntajes[tablero.TurnoActual - 1]++;
								actualizarPuntaje();
								if(tablero.hayGanador()){
									int ganador = tablero.dameGanador();
									MessageBox.Show("Terminó la partida y ganó el jugador " + ganador, "Fin del juego", MessageBoxButtons.OK, MessageBoxIcon.Information, 0,0);
								}								
							}else{
								Timer temporizador = new Timer();
								temporizador.Tick += new EventHandler(pausaVoltear);
								temporizador.Interval = RETRASO_OCULTAMIENTO;
								finalI = i;
								finalJ = j;
								temporizador.Start();
								tablero.TurnoActual++;
							}
							comparador = (char)ARROBA;							
							lblTurno.Text = tablero.TurnoActual.ToString();						
						}else{
							comparador = tablero.Cartas[i,j];
							x = i;
							y = j;							
						}
					}
				}
			}
		}
					
		void actualizarPuntaje(){
			lblPuntos1.Text = tablero.Puntajes[0].ToString();
			lblPuntos2.Text = tablero.Puntajes[1].ToString();
			lblPuntos3.Text = tablero.Puntajes[2].ToString();
			lblPuntos4.Text = tablero.Puntajes[3].ToString();
			
		}
						
		void pausaVoltear(object sender, EventArgs e){
			Timer temp = sender as Timer;																		
			voltearCarta(finalI,finalJ);
			voltearCarta(x,y);
			temp.Stop();			
		}				
	}
}

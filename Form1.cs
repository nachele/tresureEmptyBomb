using System;
using System.Diagnostics.Eventing.Reader;

namespace EncontrarElTesoro
{

    public partial class Form1 : Form
    {
        #region campos
        Cell[] cells;
        int lifes;
        int points = 0;
        int cellsNumber;
        int bombsNumber;
        int tressureIndex;
        int bombIndex;
        DateTime initTime;
        TimeSpan gametime;
        Random rnd;
        string InitialTextButton;
        #endregion

        #region metodos
        public Form1()
        {

            InitializeComponent();
            InitialTextButton = button0.Text;
            InizializeGame();



        }
        private void InizializeGame() //se ejecuta 1 ved al abrir la ventana
        {
            InitializingVariables();
            InitializingButtonText();
            EmtyCellsCreation();
            TressureCellCreation();
            BombCellsCreation();
            
        }//InizializeGame();
        private void OnClick(object sender, EventArgs e) // click en cualquier boton;
        {
            GameLogic((Button)sender);
        }//OnClick();
        private void BombFound(Button button) //bomba encontrada
        {
            lifes -= 1; //-1 vida;
            if (points >= 10) //-10 de puntos; 
            {
                points -= 1;
            }
            button.Text = "x";
            MessageBox.Show("pum!!"); //mesaje de pum;
            if (lifes <= 0) // si llego a 0 vidas perdio;
            {
                points = 0;
                Labelgametipetext.Visible = true;
                GameTimeLabel.Visible = true;
                GameTime();
                MessageBox.Show("GameOver");
                InizializeGame(); // vuelve a inicializar el juego;
            }
            LifesLabel.Text = lifes.ToString();//se actualiza el label del texto;
            PointsLabel.Text = points.ToString();//se actualiza el label de los puntos;
        }//BombFound();
        private void TresureFound(Button button)//tesoro encontrado;
        {
            //mensaje de tesoro encontrado;
            points += 10;// mas 10 puntos;
            button.Text = "$"; //texto del boton dolar;
            Labelgametipetext.Visible = true;
            GameTimeLabel.Visible = true;
            GameTime();
            MessageBox.Show("enhora buena lo encontraste");
            InizializeGame(); //bvuelve a iniciar el juego;
            PointsLabel.Text = points.ToString();//se actualiza el label de puntos;
        } //TresureFound();
        private void EmtyCellsCreation() //creando celdas vacías.
        {
            for (int i = 0; i < cellsNumber; i++) //creando celdas vacias para todas las celdas;
            {
                cells[i] = new Cell(CellType.Empty);
            }
        }//EmtyCellsCreation();
        private void TressureCellCreation()//creando la celda del tesoro
        {
            tressureIndex = rnd.Next(0, 36);//indice del tesoro;
            cells[tressureIndex] = new Cell(CellType.Treasure); //introduciendo el tesoro en la celda randome.
        }//TressureCellCreation();
        private void BombCellsCreation()//creando la celda de la bomba
        {
            bool exit = true; //loop exit or stay flag;
            for (int i = 0; i < bombsNumber; i++)//introduciendo las bombas
            {
                do //repetira tantas veces como sean necesarias hasta introducir las tres bombas.
                {
                    bombIndex = rnd.Next(0, 36);
                    if (cells[bombIndex].TYPE != CellType.Treasure && cells[bombIndex].TYPE != CellType.Bomb) //si en la celda no hay ni un tesoro ni bomba pone bomba
                    {
                        cells[bombIndex] = new Cell(CellType.Bomb);
                        exit = true;
                    }
                    else
                    {
                        exit = false;
                    }
                } while (!exit);
            }
        }//BombCellsCreation();
        private void GameLogic(Button sender) //logica de juego.
        {
            Button button = sender; //obtiene el boton clickado;
            int buttonNumber = int.Parse(button.Name.Replace("button", "")); //obtiene el indice de cada boton;
            string text = button.Text; //obtiene el texto inicial del boton clickado;
            button.Text = ""; //cambia a vacio el texto del boton clikado;
            if (cells[buttonNumber].TYPE == CellType.Bomb) // si hay una bomba en el boton;
            {
                BombFound(button);
            }
            else if (cells[buttonNumber].TYPE == CellType.Treasure)//si hay un tesoro;
            {
                TresureFound(button);
            }
        } //GameLogic();
        private void InitializingVariables()
        {
            GameTimeLabel.Text = "0:0";
            initTime = DateTime.Now;
            cellsNumber = 36; //numero de celdas;
            bombsNumber = 3; //numero de bombas;
            lifes = 3; //vidas;
            LifesLabel.Text = lifes.ToString(); //etiqueta de vidas;
            PointsLabel.Text = points.ToString();//etiqueta de puntos;
            rnd = new Random(); //inicializando objeto random;
            cells = new Cell[cellsNumber];//inicializando array de celdas;
            Labelgametipetext.Visible = false;
            GameTimeLabel.Visible = false;
        }//inicializando variables.
        private void InitializingButtonText()//inicializa texto de cada boton.
        {
            foreach (Control control in this.Controls) //simbolo de interrogacion para cada boton;
            {
                if (control is Button boton && control.Name != "ResetButton")
                {
                    boton.Text = InitialTextButton;
                }
            }
        }//InitializingButtonText();
        private void GameTime()//tiempo de partida
        {
            gametime = DateTime.Now - initTime;
            GameTimeLabel.Text = gametime.TotalMinutes.ToString().Split(",")[0] + " min : " + gametime.TotalSeconds.ToString().Split(",")[0] + " sec";
        }//GameTime();
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void RestartPoints() { points = 0; }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            RestartPoints();
            InitializingVariables();
            InitializingVariables();
            InitializingButtonText();
            EmtyCellsCreation();
            TressureCellCreation();
            BombCellsCreation();
        }
    }
}

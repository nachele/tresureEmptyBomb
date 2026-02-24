using System.Diagnostics.Eventing.Reader;

namespace EncontrarElTesoro
{

    public partial class Form1 : Form
    {
        Cell[] cells;
        Random rnd;
        string InitialTextButton;
        public Form1()
        {
            
            InitializeComponent();
            InitialTextButton = button0.Text;
            InizializeGame();
               


        }
        private void InizializeGame()
        {
            int cellsNumber = 36;
            int bombsNumber = 3;
            int emptiesNumber = cellsNumber - bombsNumber - 1;
            rnd = new Random(); //inicializando objeto randome
            cells = new Cell[cellsNumber]; //inicializando array de celdas.
            int tressureIndex = rnd.Next(0, 36); //indice del tesoro.
            int bombIndex;
            int emptyIndex;
            foreach (Control control in this.Controls)
            {
                if (control is Button boton)
                {
                    boton.Text = InitialTextButton; // ejemplo
                }
            }
            bool exit = true;
            for (int i = 0; i < cellsNumber; i++)
            {
                cells[i] = new Cell(CellType.Empty);
            }
            cells[tressureIndex] = new Cell(CellType.Treasure); //introduciendo el tesoro en la celda randome.
            for (int i = 0; i < bombsNumber; i++)
            {

                do
                {
                    bombIndex = rnd.Next(0, 36);
                    if (cells[bombIndex].TYPE != CellType.Treasure && cells[bombIndex].TYPE != CellType.Bomb)
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
        }
        private void OnClick(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            int buttonNumber = int.Parse(button.Name.Replace("button", ""));
            string text = button.Text; ;
            button.Text = "";
            if (cells[buttonNumber].TYPE == CellType.Bomb)
            {
                MessageBox.Show("has palmado");
                InizializeGame();
                button.Text = text;
            }
            else if (cells[buttonNumber].TYPE  == CellType.Treasure)
            {
                MessageBox.Show("enhora buena lo encontraste");
                button.Text = text;
                InizializeGame();
            }
        }
    }
}

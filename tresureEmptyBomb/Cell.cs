using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontrarElTesoro
{
    public enum CellType
    {
        Empty,
        Treasure,
        Bomb
    }
    internal class Cell
    {
        private CellType Type;
        private bool IsRevealed;

        public Cell(CellType type)
        {
            this.Type = type;
        }
        public CellType TYPE
        {
            get { return this.Type; }
        }
    }
}

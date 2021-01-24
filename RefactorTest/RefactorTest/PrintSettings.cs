using System;

namespace RefactorTest
{
    /// <summary>
    /// Represents a config info for numeric printer. 
    /// </summary>
    public sealed class PrintSettings
    {
        /// <summary>
        /// Quantity of prime numbers to print.
        /// </summary>
        /// <value></value>
        public int PrimesQuantity { get; private set; }

        /// <summary>
        /// Number of lines per page to print.
        /// </summary>
        /// <value></value>
        public int PageLines { get; private set; }

        /// <summary>
        /// Number of columns per line to print.
        /// </summary>
        /// <value></value>
        public int LineColumns { get; private set; }
        
        /// <summary>
        /// Initialize a new instance of <see cref="PrintSettings"/> class.
        /// </summary>
        /// <param name="primesQuantity">Quantity of prime numbers to print.</param>
        /// <param name="pageLines">Number of lines per page to print.</param>
        /// <param name="lineColumns">Number of columns per line to print.</param>
        public PrintSettings(int primesQuantity, int pageLines, int lineColumns)
        {
            if (primesQuantity <= 0)
            {
                throw new ArgumentException("Número de primos debe ser un valor positivo.", nameof(primesQuantity));
            }
            if (pageLines <= 0)
            {
                throw new ArgumentException("Número de lineas por página debe ser un valor positivo.", nameof(pageLines));
            }
            if (lineColumns <= 0)
            {
                throw new ArgumentException("Número de columnas por página debe ser un valor positivo.", nameof(lineColumns));
            }
            this.PrimesQuantity = primesQuantity;
            this.PageLines = pageLines;
            this.LineColumns = lineColumns;
        }
    }
}

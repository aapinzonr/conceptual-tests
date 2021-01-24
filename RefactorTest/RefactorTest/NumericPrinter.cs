using System;

namespace RefactorTest
{
    /// <summary>
    /// Represent an numeric printer with columnar format.
    /// </summary>
    public class NumericPrinter
    {
        /// <summary>
        /// Number of lines per page of content to print.
        /// </summary>
        /// <value></value>
        private int PageLines { get; set; }

        /// <summary>
        /// Number of columns per line of content to print.
        /// </summary>
        /// <value></value>
        private int LineColumns { get; set; }

        /// <summary>
        /// Initialize a new instance of <see cref="NumericPrinter"/> class.
        /// </summary>
        /// <param name="pageLines">Number of lines per page of content to print.</param>
        /// <param name="lineColumns">Number of columns per line of content to print.</param>
        public NumericPrinter(int pageLines, int lineColumns)
        {
           if (pageLines <= 0)
            {
                throw new ArgumentException("Número de lineas por página debe ser un valor positivo.", nameof(pageLines));
            }
            if (lineColumns <= 0)
            {
                throw new ArgumentException("Número de columnas por página debe ser un valor positivo.", nameof(lineColumns));
            }
            this.PageLines = pageLines;
            this.LineColumns = lineColumns;
        }

        /// <summary>
        /// Run a print job of a numbers list.
        /// </summary>
        /// <param name="numberList">Numbers list to print.</param>
        /// <param name="numbersQuantity">Numbers quantity to print on a print job.</param>
        public void Print(int[] numberList, int numbersQuantity)
        {
            int currentPage = 1;
            int pageOffset = 1;
            int offsetSize = this.PageLines * this.LineColumns;
            while (pageOffset <= numbersQuantity)
            {
                this.PrintPage(numberList, currentPage, pageOffset, numbersQuantity);
                currentPage++;
                pageOffset += offsetSize;
            }
        }

        /// <summary>
        /// Print a content page of a print job.
        /// </summary>
        /// <param name="numberList">Numbers list to print.</param>
        /// <param name="pageNumber">Page number of a print job.</param>
        /// <param name="pageOffset">Elements quantity to print per page.</param>
        /// <param name="numbersQuantity">Total elements to print on a print job.</param>
        private void PrintPage(int[] numberList, int pageNumber, int pageOffset, int numbersQuantity)
        {
            //Header
            this.PrintPageHeader(pageNumber, numbersQuantity);
            //Body
            this.PrintPageBody(pageOffset, numberList, numbersQuantity);
            //Footer
            this.PrintPageFooter();
        }

        /// <summary>
        /// Print a content body of a page on a print job.
        /// </summary>
        /// <param name="pageOffset">Elements quantity to print per page.</param>
        /// <param name="numberList">Numbers list to print.</param>
        /// <param name="numbersQuantity">Total elements to print on a print job.</param>
        private void PrintPageBody(int pageOffset, int[] numberList, int numbersQuantity)
        {
            for (int lineOffset = pageOffset; lineOffset <= pageOffset + this.PageLines - 1; lineOffset++)
            {
                this.PrintBodyLine(numberList, numbersQuantity, lineOffset);
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Print a content line of a page on a print job.
        /// </summary>
        /// <param name="numberList">Numbers list to print.</param>
        /// <param name="numbersQuantity">Total elements to print on a print job.</param>
        /// <param name="lineOffset">Elements quantity to print per line.</param>
        private void PrintBodyLine(int[] numberList, int numbersQuantity, int lineOffset)
        {
            for (int counter = 0; counter <= this.LineColumns - 1; counter++)
            {
                if (lineOffset + counter * this.PageLines <= numbersQuantity)
                {
                    int printIndex = lineOffset + counter * this.PageLines;
                    int numberToPrint = numberList[printIndex];
                    Console.Write("{0} ", numberToPrint);
                }
            }
        }

        /// <summary>
        /// Print a header segment of a page on a print job.
        /// </summary>
        /// <param name="pageNumber">Page number of a print job.</param>
        /// <param name="numbersQuantity">Total elements to print on a print job.</param>
        private void PrintPageHeader(int pageNumber, int numbersQuantity)
        {
            Console.Write("The First ");
            Console.Write(numbersQuantity);
            Console.Write(" Prime Numbers === Page ");
            Console.Write(pageNumber);
            Console.WriteLine("\n");
        }

        /// <summary>
        ///  Print a footer segment of a page on a print job.
        /// </summary>
        private void PrintPageFooter()
        {
            Console.WriteLine("\f");
        }
    }
}
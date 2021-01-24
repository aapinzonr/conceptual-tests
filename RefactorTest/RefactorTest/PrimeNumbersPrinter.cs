using System;

namespace RefactorTest
{
    /// <summary>
    /// Represent a virtual prime numbers printer.
    /// </summary>
    public class PrimeNumbersPrinter
    {
        /// <summary>
        /// Settings instance of printer.
        /// </summary>
        /// <value></value>
        public PrintSettings Settings { get; private set; }

        /// <summary>
        /// Initialize a new instance of <see cref="PrimeNumbersPrinter"/> class.
        /// </summary>
        /// <param name="settings">Settings info of printer.</param>
        public PrimeNumbersPrinter(PrintSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentException("Configuracion de impresión no peude ser un valor nulo.", nameof(settings));
            }
            this.Settings = settings;
        }

        /// <summary>
        /// Allow print a prime numbers list according of defined settings values.
        /// </summary>
        public void Print()
        {
            //Prime numbers generation
            PrimeNumbersGenerator primesGenerator = new PrimeNumbersGenerator();
            int[] primeNumbers = primesGenerator.Generate(this.Settings.PrimesQuantity);
            //Prime numbers printing
            NumericPrinter numericPrinter = new NumericPrinter(this.Settings.PageLines, this.Settings.LineColumns);
            numericPrinter.Print(primeNumbers, this.Settings.PrimesQuantity);
        }
    }
}
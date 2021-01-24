namespace RefactorTest
{
    /// <summary>
    /// Group a necessary behavior to generate a prime numbers sets.
    /// </summary>
    public class PrimeNumbersGenerator
    {
        /// <summary>
        /// Limit of multiple elements array.
        /// </summary>
        private const int ORDINAL_MAX = 30;
        
        /// <summary>
        /// Allow generate a set of prime numbers according of specified elements quantity.
        /// </summary>
        /// <param name="primesQuantity">Quantity of prime numbers to generate.</param>
        /// <returns>Set of generated prime numbers.</returns>
        public int[] Generate(int primesQuantity)
        {
            int[] primeNumbers = new int[primesQuantity + 1];
            primeNumbers[1] = 2;
            bool isProbablyPrime;
            int multIndex = 0;
            int[] multArray = new int[ORDINAL_MAX + 1];
            int checkingNumber = 1;
            int index = 1;
            int ordinalIndex = 2;
            int primeSquare = 9;
            while (index < primesQuantity)
            {
                do
                {
                    checkingNumber += 2;
                    if (checkingNumber == primeSquare)
                    {
                        ordinalIndex++;
                        primeSquare = primeNumbers[ordinalIndex] * primeNumbers[ordinalIndex];
                        multArray[ordinalIndex - 1] = checkingNumber;
                    }
                    multIndex = 2;
                    isProbablyPrime = true;
                    while (multIndex < ordinalIndex && isProbablyPrime)
                    {
                        while (multArray[multIndex] < checkingNumber)
                        {
                            multArray[multIndex] += primeNumbers[multIndex] + primeNumbers[multIndex];
                        }
                        if (multArray[multIndex] == checkingNumber)
                        {
                            isProbablyPrime = false;
                        }
                        multIndex++;
                    }
                } while (!isProbablyPrime);
                index++;
                primeNumbers[index] = checkingNumber;
            }
            return primeNumbers;
        }
    }
}
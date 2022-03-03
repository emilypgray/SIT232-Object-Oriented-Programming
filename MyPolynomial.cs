using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace _3._3CT
{
    public class MyPolynomial
    {
        // Declare private double _coeffs
        private double[] _coeffs;

        // Declare constuctor to accept a double array
        public MyPolynomial(double[] coeffs)
        {
            _coeffs = coeffs;
        }

        // Set accessor method for _coeffs
        public double[] Coeff
        {
            get { return _coeffs; }
        }

        // Method to return degree of polynomial
        public int GetDegree()
        {
            return _coeffs.Length - 1;
        }

        // ToString method to return polynomial formatted to a string
        public override string ToString()
        {
            // Declare an empty list
            List<string> polyString = new List<string>();

            // Use for loop to iterate through elements in polynomial
            for (int i = 0; i < _coeffs.Length; i++)
            {
                // If coefficient is equal to zero, skip over this iteration of for loop
                if (_coeffs[i] == 0)
                    continue;
                else
                {
                    // If i is not the last element in the polynomial
                    if (i != _coeffs.Length - 1)
                    {
                        // If i is not the second last element in the polynomial
                        if (i != _coeffs.Length - 2)
                        {
                            // If coefficient is equal to 1, add x^n without coefficient
                            if (_coeffs[i] == 1)
                                polyString.Add($"x^{_coeffs.Length - i - 1}");
                            // If coefficient is not equal to 1, add x^n with coefficient
                            else
                                polyString.Add($"{_coeffs[i]}x^{_coeffs.Length - i - 1}");
                        }
                        // If coefficient is equal to 1, add x without coefficient
                        else
                            if (_coeffs[i] == 1)
                                polyString.Add("x");
                            // If coefficient is not equal to 1, add x with coefficient
                            else
                                polyString.Add($"{_coeffs[i]}x");
                    }
                    else
                        // If coefficient is last element, just add coefficient
                        polyString.Add($"{_coeffs[i]}");

                    // If any coefficient after the current does not equal zero, add a '+' sign between coefficients
                    for (int j = i + 1; j < _coeffs.Length; j++)
                    {
                        if ((int)_coeffs[j] != 0)
                        {
                            polyString.Add(" + ");
                            break;
                        }
                    }
                }               
            }
            // Join individual elements in the list to create one string
            string myString = string.Join("", polyString);
            return myString;
        }
        

        // Declare method to evaluate polynomial at given x value
        public double Evaluate(double x)
        {
            // Declare and initialise a temporary sum equal to 0
            double tempSum = 0;

            // Use for loop to iterate through elements in polynomial and evaluate each
            // at input x value and add to temporary sum
            for (int i = 0; i < _coeffs.Length; i++)
            {
                tempSum += _coeffs[i] * Math.Pow(x, (_coeffs.Length - i - 1));
            }

            return tempSum;
        }



        // Declare method to this polynomial to another and return a new polynomial

        public MyPolynomial Add(MyPolynomial another)
        {
            // Use accessor method to declare a new double array with values equal to 
            // another coefficients
            double[] anotherCoeffs = another.Coeff;

            // Declare an empty list to store the sum of the coefficients of both
            // polynomials
            List<double> resultList = new List<double>();

            // If length of both polynomials is the same, then sum the elements
            // at matching indexes in each polynomial and add the sum to the 
            // list
            if (_coeffs.Length == anotherCoeffs.Length)
            {
                for (int i = 0; i < _coeffs.Length; i++)
                    resultList.Add(_coeffs[i] + anotherCoeffs[i]);
            }
            // If length of first polynomial is larger than second,
            // then add each element of the second to the list with
            // number of leading zeros required to make the list
            // and the first polynomial equal in length
            else if (_coeffs.Length > anotherCoeffs.Length)
            {
                for (int i = 0; i < _coeffs.Length - anotherCoeffs.Length; i ++)
                    resultList.Add(0);
                for (int i = 0; i < anotherCoeffs.Length; i++)
                    resultList.Add(anotherCoeffs[i]);
                // Loop result list and add each element to element with matching
                // index from first polynomial
                for (int i = 0; i < _coeffs.Length; i++)
                    resultList[i] += _coeffs[i];            
            }

            else
            {
                // Repeat the process with the first polynomial if the length of the second
                // is greater
                for (int i = 0; i < anotherCoeffs.Length -_coeffs.Length; i++)
                    resultList.Add(0);
                for (int i = 0; i < _coeffs.Length; i++)
                    resultList.Add(_coeffs[i]);
                for (int i = 0; i < anotherCoeffs.Length; i++)
                    resultList[i] += anotherCoeffs[i];
            }

            // Convert resulting list to array and generate new MyPolyomial object from
            // the result. Return result
            double[] resultArray = resultList.ToArray();
            return new MyPolynomial(resultArray);
        }


        public MyPolynomial Multiply(MyPolynomial another)
        {
            // Use accessor method to declare a new double array with values equal to 
            // another coefficients
            double[] anotherCoeffs = another.Coeff;

            // Create a new two-dimensional array with rows equal to the length of 
            // the first polynomial and cols equal to the length of the second
            // polynomial
            double[,] doubleArray = new double[_coeffs.Length, anotherCoeffs.Length];

            //Create and empty list to store the product of the multiplication
            List<double> resultList = new List<double>();

            // Declare a temp sum
            double tempSum;

            // Declare four integers to be used in traversing the 2D array
            int row;
            int col;
            int j;
            int i;

            // Use nested for loops to set each element of the 2D array 
            // equal to the product of the elements at the respective indexes
            // in the first and second polynomial with the first polynomial
            // representing the rows and the second representing the columns
            for (i = 0; i < _coeffs.Length; i++)
            {
                for (j = 0; j < anotherCoeffs.Length; j++)
                    doubleArray[i, j] = _coeffs[i] * anotherCoeffs[j];
            }

            // Use for loop to iterate through each element in the first
            // row of the 2D array and adding every element on the down-left
            // diagonal from it. Output total sum of elements to the 
            // list
            for (j = 0; j < doubleArray.GetLength(1); j++)
            {
                tempSum = 0;
                row = -1;
                col = j + 1;

                while (row < doubleArray.GetLength(0) - 1 && col > 0)
                {                    
                    tempSum += doubleArray[(row + 1), (col - 1)];
                    row++;
                    col--;
                }
                resultList.Add(tempSum);
            }

            // Use for loop to iterate through each element in the last column,
            // starting from the second row, and add every element on the down-left 
            // diagonal. Output total sum of elements to the list
            for (i = 1; i < doubleArray.GetLength(0); i++)
            {
                tempSum = 0;
                row = i - 1;
                col = doubleArray.GetLength(1);

                while (row < doubleArray.GetLength(0) - 1)
                {
                    tempSum += doubleArray[(row + 1), (col - 1)];
                    row++;
                    col--;
                }
                resultList.Add(tempSum);
            }

            // Convert list to double array and return new instance of polynomial
            // using this array as input
            double[] returnArray = resultList.ToArray();
            return new MyPolynomial(returnArray);
        }

    }
}

using System;
using System.Collections.Generic;
namespace _3._3CT
{
    class TestMyPolynomial
    {
        // Create an enumeration for the menu options
        enum MenuOption
        {
            Evaluate,
            Add,            
            Multiply,
            Exit,
        }

        static void Main(string[] args)
        {
            // Call EnterPolynomial method so user can input intial double array
            double[] myDoubleArray = EnterPolynomial();

            // Create new object of MyPolynomial class using double array as input
            MyPolynomial myArray = new MyPolynomial(myDoubleArray);

            Console.WriteLine();
            Console.WriteLine("Your polynomial is: " + myArray.ToString());

            // Call the ReadUserOption method to prompt user to enter direction to program
            int menuSelect = ReadUserOption();

            // Use do while loop to continue calling the ReadUserOption method until
            // the quit option is selected
            do
            {
                // Use switch statement to perform operations based on user menu
                // selection
                switch (menuSelect)
                {
                    case (int)MenuOption.Evaluate:
                        Console.WriteLine();
                        Console.WriteLine("You have selected to evaluate your polynomial at x.");
                        Console.WriteLine();
                        // Prompt user to enter value of x
                        Console.WriteLine("Please enter value of x: ");
                        double evaluateValue = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine();
                        // Output the result of calling object's Evaluate method
                        Console.WriteLine("At x = {0} is: {1}. ", evaluateValue, myArray.Evaluate(evaluateValue));
                        Console.WriteLine();
                        menuSelect = ReadUserOption();
                        break;
                    case (int)MenuOption.Add:
                        Console.WriteLine();                        
                        Console.WriteLine("You have selected to add your polynomial to another.");
                        Console.WriteLine();
                        Console.WriteLine("You will need to input another polynomial.");
                        // Call enter polynomial function to prompt user to enter new polynomial
                        MyPolynomial another = new MyPolynomial(EnterPolynomial());
                        Console.WriteLine();
                        Console.WriteLine("Your new polynomial is: " + another.ToString());
                        Console.WriteLine();
                        // Output the results of calling object's Add method
                        Console.WriteLine("The sum of the two polynomials is: {0}", myArray.Add(another));
                        Console.WriteLine();
                        menuSelect = ReadUserOption();
                        break;
                    case (int)MenuOption.Multiply:
                        Console.WriteLine();
                        Console.WriteLine("You have selected to multiply your polynomial by another.");
                        Console.WriteLine();
                        Console.WriteLine("You will need to input another polynomial.");
                        // Call enter polynomial function to prompt user to enter new polynomial
                        MyPolynomial another1 = new MyPolynomial(EnterPolynomial());
                        Console.WriteLine();
                        Console.WriteLine("Your new polynomial is: " + another1.ToString());
                        Console.WriteLine();
                        // Output the results of calling object's Multiply method
                        Console.WriteLine("The sum of the two polynomials is: {0}", myArray.Multiply(another1));
                        Console.WriteLine();
                        menuSelect = ReadUserOption();
                        break;
                    case (int)MenuOption.Exit:
                        Console.WriteLine("You have selected exit.");
                        Console.WriteLine();
                        break;
                }
                // Quite the program
            } while (menuSelect != (int)MenuOption.Exit);
       

            // Declare new method that allows user to input coefficients to a polynomial
            // of user-declared degree
            static double[] EnterPolynomial()
            {
                bool isDegreeValid;
                bool isCoeffValid;
                // Declare empty double to return so that method has a return value should the 
                // do loop fail
                double[] emptyDouble = new double[0];
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter degree of your polynomial: ");
                    Console.WriteLine();

                    // Check if input is integer
                    if (int.TryParse(Console.ReadLine(), out int degree))
                    {
                        // Declare new double array of length degree + 1
                        double[] doubleArray = new double[degree + 1];
                        isDegreeValid = true;
                        // Check if input is in valid range
                        for (int i = 0; i < doubleArray.Length; i++)
                        {
                            // Use do-while loop to prompt user to enter all coefficients
                            do
                            {
                                Console.WriteLine($"Enter coefficient number {i + 1}");
                                // Check if coefficient can be casted to double type
                                if (double.TryParse(Console.ReadLine(), out double coefficient))
                                {
                                    // Assign coefficient to relevant index in array
                                    doubleArray[i] = coefficient;
                                    isCoeffValid = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid input. Please enter another number.");
                                    isCoeffValid = false;
                                }
                            } while (!isCoeffValid);
                        }
                        return doubleArray;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter another number.");
                        isDegreeValid = false;
                    }
                } while (!isDegreeValid);

                return emptyDouble;
            }




            static int ReadUserOption()
            {
                bool isValidInput = false;

                do
                {
                    // Show the menu
                    Console.WriteLine();
                    Console.WriteLine("Please select an option (1 - 4) from the following: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Evaluate");
                    Console.WriteLine("2. Add");
                    Console.WriteLine("3. Multiply, and");
                    Console.WriteLine("4. Exit");
                    Console.WriteLine();

                    // Use TryParse to test if input can be converted to an integer value
                    isValidInput = Int32.TryParse(Console.ReadLine(), out int menuSelection);

                    // If input can be converted to integer
                    if (isValidInput)
                    {
                        // If input in correct range
                        if (menuSelection > 0 && menuSelection < 5)
                            // Return input minus 1 to main program
                            return menuSelection - 1;
                        else
                            isValidInput = false;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again.");
                } while (!isValidInput);
                return 0;
            }
        }

    }
}

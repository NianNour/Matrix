using System;

namespace MatrixProject
{
    class Matrix
    {
        public int rows; //fields :
        public int columns;
        public int[,] array;
        public Matrix(int rows, int columns) //first constructor :
        {
            this.rows = rows;
            this.columns = columns;
            array = new int[rows, columns];
        }
        public Matrix() //second constructor :
        {
        }
        public void addToMatrix() // add value to matric to make one : 
        {
            array = new int[rows, columns];
            for (int i = 0; i < rows; i++) //
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
        public void print() //print matric : 
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(array[i, j] + "  ");
                }
                Console.Write("\n");
            }
        }
        public bool isSpars() //check if your matric is spars or not :
        {
            int numOfZeros = 0;
            int numOfNonZeros = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (array[i, j] == 0)
                        numOfZeros++;
                    else
                        numOfNonZeros++;
                }
            }
            if (numOfZeros > numOfNonZeros)
                return true;
            else
                return false;
        }
        public Matrix sumMatrix(Matrix matrix2, Matrix matrix1) //sum 2 matrices :
        {
            Matrix resMatrix = new Matrix();
            resMatrix.rows = matrix2.rows;
            resMatrix.columns = matrix2.columns;
            resMatrix.array = new int[resMatrix.rows, resMatrix.columns];
            for (int i = 0; i < resMatrix.rows; i++)
            {
                for (int j = 0; j < resMatrix.columns; j++)
                {
                    resMatrix.array[i, j] = matrix2.array[i, j] + matrix1.array[i, j];
                }
            }
            return resMatrix;
        }
        public Matrix Transpose(Matrix matrix) //to transpose a matrix : 
        {
            Matrix transposed = new Matrix();
            transposed.rows = matrix.columns;
            transposed.columns = matrix.rows;
            transposed.array = new int[transposed.rows, transposed.columns];
            for (int j = 0; j < transposed.columns; j++)
            {
                for (int i = 0; i < transposed.rows; i++)
                {
                    transposed.array[j, i] = matrix.array[i, j];
                }
            }
            return transposed;
        }

        public Matrix multiMatrix(Matrix matrix1, Matrix matrix2) // to multipy 2 matrices:
        {
            Matrix multiMatrix = new Matrix();
            multiMatrix.rows = matrix1.rows;
            multiMatrix.columns = matrix2.columns;
            multiMatrix.array = new int[matrix1.rows, matrix2.columns];

            if (matrix1.columns != matrix2.rows)
                throw new ArgumentException("The number of columns in the first matrix must be equal to the number of rows in the second matrix.");

            for (int i = 0; i < multiMatrix.rows; i++)
            {
                for (int j = 0; j < multiMatrix.columns; j++)
                {
                    for (int k = 0; k < matrix1.columns; k++)
                        multiMatrix.array[i, j] = multiMatrix.array[i, j] + matrix1.array[i, k] * matrix2.array[k, j];
                }
            }
            return multiMatrix;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix1 = new Matrix();
            Matrix matrix2 = new Matrix();

            int choice;
            do
            {
                Console.WriteLine("1. Create matrix 1");
                Console.WriteLine("2. Create matrix 2");
                Console.WriteLine("3. Add matrices");
                Console.WriteLine("4. Transpose a matrix");
                Console.WriteLine("5. Multiply matrices");
                Console.WriteLine("6. Check if matrix is sparse");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter the number of rows in matrix 1: ");
                            int rows1 = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter the number of columns in matrix 1: ");
                            int columns1 = Convert.ToInt32(Console.ReadLine());
                            matrix1 = new Matrix(rows1, columns1);
                            matrix1.addToMatrix();
                            Console.WriteLine("Matrix 1:");
                            matrix1.print();
                            break;

                        case 2:
                            Console.Write("Enter the number of rows in matrix 2: ");
                            int rows2 = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter the number of columns in matrix 2: ");
                            int columns2 = Convert.ToInt32(Console.ReadLine());
                            matrix2 = new Matrix(rows2, columns2);
                            matrix2.addToMatrix();
                            Console.WriteLine("Matrix 2:");
                            matrix2.print();
                            break;

                        case 3:
                            if (matrix1.rows == 0 || matrix2.rows == 0)
                            {
                                Console.WriteLine("Please create both matrices first.");
                                break;
                            }
                            if (matrix1.rows != matrix2.rows || matrix1.columns != matrix2.columns)
                            {
                                Console.WriteLine("Matrices must have the same dimensions.");
                                break;
                            }
                            Matrix sum = matrix1.sumMatrix(matrix1, matrix2);
                            Console.WriteLine("Matrix 1 + Matrix 2:");
                            sum.print();
                            break;

                        case 4:
                            if (matrix1.rows == 0 && matrix2.rows == 0)
                            {
                                Console.WriteLine("Please create a matrix first.");
                                break;
                            }
                            Console.Write("Enter the number of the matrix to transpose (1 or 2): ");
                            int matrixNumber = Convert.ToInt32(Console.ReadLine());
                            Matrix matrixToTranspose = matrixNumber == 1 ? matrix1 : matrix2;
                            Matrix transposed = matrixToTranspose.Transpose(matrixToTranspose);
                            Console.WriteLine("Transposed matrix:");
                            transposed.print();
                            break;

                        case 5:
                            if (matrix1.rows == 0 || matrix2.rows == 0)
                            {
                                Console.WriteLine("Please create both matrices first.");
                                break;
                            }
                            if (matrix1.columns != matrix2.rows)
                            {
                                Console.WriteLine("The number of columns in the first matrix must be equal to the number of rows in the second matrix.");
                                break;
                            }
                            Matrix product = matrix1.multiMatrix(matrix1, matrix2);
                            Console.WriteLine("Matrix 1 x Matrix 2:");
                            product.print();
                            break;

                        case 6:
                            if (matrix1.rows == 0 && matrix2.rows == 0)
                            {
                                Console.WriteLine("Please create a matrix first.");
                                break;
                            }
                            Matrix matrixToCheck = matrix1.rows != 0 ? matrix1 : matrix2;
                            bool isSparse = matrixToCheck.isSpars();
                            if (isSparse)
                            {
                                Console.WriteLine("The matrix is sparse.");
                            }
                            else
                            {
                                Console.WriteLine("The matrix is not sparse.");
                            }
                            break;

                        case 7:
                            Console.WriteLine("Exiting program...");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                Console.WriteLine();
            } while (choice != 7);
        }
    }
}

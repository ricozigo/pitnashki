
using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int[,] matrix = new int[4, 4];
        int[] numbers = new int[16];

        // Инициализируем массив числами от 1 до 15
        for (int i = 0; i < 15; i++)
        {
            numbers[i] = i + 1;
        }

        // Перемешиваем массив
        for (int i = 0; i < 14; i++)
        {
            int j = random.Next(i, 15);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        // Заполняем двумерный массив случайными неповторяющимися числами по вертикали и горизонтали
        int index = 0;
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                matrix[row, col] = numbers[index];
                index++;
            }
        }

        // Заменяем нули на -1 для представления пустого места
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (matrix[row, col] == 0)
                {
                    matrix[row, col] = -1;
                }
            }
        }

        // Выводим начальное состояние игрового поля
        PrintMatrix(matrix);

        int movesLeft = 1000000; // Количество доступных перемещений

        while (movesLeft > 0)
        {
            // Считываем ввод пользователя
            Console.WriteLine("что хочешь? (0 если слабак): ");
            int blockNumber = int.Parse(Console.ReadLine());

            if (blockNumber == 0)
            {
                break; // Выход из цикла, если введен 0
            }

            // Находим позицию блока в матрице
            int blockRow = -1;
            int blockCol = -1;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (matrix[row, col] == blockNumber)
                    {
                        blockRow = row;
                        blockCol = col;
                        break;
                    }
                }
                if (blockRow != -1)
                {
                    break;
                }
            }

            if (blockRow == -1)
            {
                Console.WriteLine("ты какой такой блок хочешь переместить");
                continue; // Пропускаем итерацию цикла и продолжаем ввод
            }

            // Проверяем, можете ли вы переместить блок в указанное направление (влево, например) и выполняем перемещение
            Console.WriteLine("куда хочешь? (смотри на стрелочки на нампаде) ");
            string direction = Console.ReadLine().ToLower();

            int newRow = blockRow;
            int newCol = blockCol;

            switch (direction)
            {
                case "4":
                    newCol--;
                    break;
                case "6":
                    newCol++;
                    break;
                case "8":
                    newRow--;
                    break;
                case "2":
                    newRow++;
                    break;
                default:
                    Console.WriteLine("открой глаза и смотри на клаву!");
                    continue; // Пропускаем итерацию цикла и продолжаем ввод
            }

            // Проверяем, не выходит ли блок за границы игрового поля и не является ли целевая ячейка -1
            if (newRow < 0 || newRow >= 4 || newCol < 0 || newCol >= 4 || matrix[newRow, newCol] != -1)
            {
                Console.WriteLine("дурень");
                continue; // Пропускаем итерацию цикла и продолжаем ввод
            }

            // Перемещаем блок
            matrix[blockRow, blockCol] = -1;
            matrix[newRow, newCol] = blockNumber;
            movesLeft--;

            // Выводим обновленное состояние игрового поля
            PrintMatrix(matrix);

            // Проверяем на выигрыш
            if (CheckWinCondition(matrix))
            {
                Console.WriteLine("Поздравляем! Ты выиграли!");
                break;
            }
        }

        Console.WriteLine("ну лох чё");
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (matrix[row, col] == -1)
                {
                    Console.Write("0\t");
                }
                else
                {
                    Console.Write(matrix[row, col] + "\t");
                }
            }
            Console.WriteLine();
        }
    }

    static bool CheckWinCondition(int[,] matrix)
    {
        int expectedValue = 1;
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (matrix[row, col] != -1)
                {
                    if (matrix[row, col] != expectedValue)
                    {
                        return false; // Не все числа идут по порядку
                    }
                    expectedValue++;
                }
            }
        }
        return true; // Все числа идут по порядку
    }
}


using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Введите строку:");
string inputStr = Console.ReadLine();
if (StringCheck(inputStr))
{
    string outputStr = StringProcessing(inputStr);
    Console.WriteLine("Обработанная строка:");
    Console.WriteLine(outputStr);
    Dictionary<char, int> charCount = GetCharCount(outputStr);
}
Console.ReadKey();


// Метод обработки строки из первого задания
static string StringProcessing(string inputStr)
{
    StringBuilder resultStr = new StringBuilder();
    if (inputStr.Length % 2 == 0)
    {
        //Если строка чётная
        int halfLength = inputStr.Length / 2;
        string firstHalf = inputStr.Substring(0, halfLength);
        string secondHalf = inputStr.Substring(halfLength);

        resultStr.Append(string.Join("", firstHalf.Reverse()));
        resultStr.Append(string.Join("", secondHalf.Reverse()));
    }
    else
    {
        //Если строка нечётная 
        resultStr.Append(string.Join("", inputStr.Reverse()));
        resultStr.Append(inputStr);
    }
    return resultStr.ToString();
}

// Метод проверки строки из второго задания
static bool StringCheck(string inputStr)
{
    if (string.IsNullOrEmpty(inputStr))
    {
        Console.WriteLine("Строка не должна быть пустой!");
        return false;
    }

    // Проверка с использованием регулярного выражения на наличие только английского алфавита в нижнем регистре
    Regex engLowCaseRegex = new Regex("^[a-z]+$");
    if (!engLowCaseRegex.IsMatch(inputStr))
    {
        Console.WriteLine("Строка должна содержать только буквы английского алфавита в нижнем регистре!");
        Console.WriteLine("Неподходящие символы:");
        foreach (char c in inputStr)
        {
            if (!Char.IsLetter(c) || !Char.IsLower(c) || !IsLatinLetter(c))
            {
                Console.Write(c + " ");
            }
        }
        Console.WriteLine();
        return false;
    }

    return true;
}

static bool IsLatinLetter(char c)
{
    return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
}

// Получение словаря символов с количеством повторений из третьего задания
static Dictionary<char, int> GetCharCount(string str)
{
    return str
        .GroupBy(c => c)
        .ToDictionary(g => g.Key, g => g.Count());
}
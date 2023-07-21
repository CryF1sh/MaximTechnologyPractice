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
    PrintCharCount(charCount);
    string longestVowelSubstring = FindLongestSubstring(outputStr);
    Console.WriteLine("Самая длинная подстрока, начинающаяся и заканчивающаяся на гласную:");
    Console.WriteLine(longestVowelSubstring);
}
Console.ReadKey();

#region Методы Задание #1
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
#endregion

#region Методы Задание #2
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
#endregion

#region Методы Задание #3
// Получение словаря символов с количеством повторений из третьего задания
static Dictionary<char, int> GetCharCount(string str)
{
    return str
        .GroupBy(c => c)
        .ToDictionary(g => g.Key, g => g.Count());
}

// Вывод на печать информации о количестве повторений каждого символа из третьего задания
static void PrintCharCount(Dictionary<char, int> charCount)
{
    Console.WriteLine("Информация о количестве повторений каждого символа:");
    foreach (char ch in charCount.Keys.OrderBy(k => k))
    {
        Console.WriteLine($"Символ '{ch}': {charCount[ch]} раз");
    }
}
#endregion

#region Методы Задание #4
// Определение самой длинной подстроки начинающаяся и заканчивающаяся на гласную
static string FindLongestSubstring(string str)
{
    // Находим все подстроки, начинающиеся и заканчивающиеся на гласные
    MatchCollection matchCollection = Regex.Matches(str, @"[aeiouy][a-z]*[aeiouy]");

    // Находим самую длинную подстроку
    string longestSubstring = string.Empty;
    foreach (Match match in matchCollection)
    {
        if (match.Length > longestSubstring.Length)
        {
            longestSubstring = match.Value;
        }
    }
    return longestSubstring;
}
#endregion
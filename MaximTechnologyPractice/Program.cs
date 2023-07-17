using System.Text;

Console.WriteLine("Введите строку:");
string inputStr = Console.ReadLine();
//bool Checked = StringCheck(inputStr); 
if (StringCheck(inputStr))
{
    string outputStr = StringProcessing(inputStr);
    Console.WriteLine("Обработанная строка:");
    Console.WriteLine(outputStr);
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

static bool StringCheck(string inputStr)
{
    if (string.IsNullOrEmpty(inputStr))
    {
        Console.WriteLine("Строка не должна быть пустой!");
        return false;
    }

    return true;
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebAPIMaximPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringProcessingController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public StringProcessingController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        // Пример ввода http://localhost:5167/api/StringProcessing?inputStr={ваша строка}

        [HttpGet]
        public async Task<IActionResult> ProcessString([FromQuery] string inputStr)
        {
            if (WordBlackList(inputStr))
            {
                return BadRequest("Слово находится в чёрном списке!");
            }

            if (StringCheck(inputStr))
            {
                string outputStr = StringProcessing(inputStr);
                Dictionary<char, int> charCount = GetCharCount(outputStr);
                string longestVowelSubstring = FindLongestSubstring(outputStr);
                string sortedStr = QuickSort(outputStr);
                int randomNumber = await GetRandomNumber(outputStr);

                var result = new
                {
                    ProcessedString = outputStr,
                    CharCount = charCount,
                    LongestVowelSubstring = longestVowelSubstring,
                    SortedString = sortedStr,
                    TrimmedProcessedString = DeleteRandomChar(outputStr, randomNumber)
                };

                return Ok(result);
            }
            else
            {
                return BadRequest("Строка должна содержать только буквы английского алфавита в нижнем регистре!");
            }
        }
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

        #region Методы Задание #5
        // Спросить об желаемом алгоритме сортировки (Быстрая сортировка или Сортировка деревом)
        static void AskSort(string str)
        {
            Console.WriteLine("Выберете метод сортировки:");
            Console.WriteLine("1. Быстрая сортировка");
            Console.WriteLine("2. Сортировка деревом\n");
            int chSort = int.Parse(Console.ReadLine());
            switch (chSort)
            {
                case 1:
                    {
                        Console.WriteLine("Отсортировання строка методом быстрой сортировки:");
                        Console.WriteLine(QuickSort(str));
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Отсортировання строка методом сортировки деревом:");
                        Console.WriteLine(TreeSort(str));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Неккорктный выбор сортировки");
                        break;
                    }
            }
        }

        static string QuickSort(string str)
        {
            string sortedResult = string.Concat(str.ToString().OrderBy(c => c));
            return sortedResult;
        }

        static string TreeSort(string str)
        {
            TreeNode root = new TreeNode(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                TreeSortClass.Insert(root, str[i]);
            }
            List<char> sortedChars = new List<char>();
            TreeSortClass.OrderTraversal(root, sortedChars);
            return new string(sortedChars.ToArray());
        }
        #endregion

        #region Методы Задание #6
        private async Task<int> GetRandomNumber(string str)
        {
            string apiUrl = $"{_appSettings.UrlApi}?min=1&max={str.Length}&count=1"; //string apiUrl = $"http://www.randomnumberapi.com/api/v1.0/randomnumber?min=1&max={str.Length}&count=1";
            await Console.Out.WriteLineAsync(apiUrl);

            int randomNumber;

            //// Показываем интерактивное сообщение об ожидании
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //Task showWaitingMessageTask = ShowWaitingMessageAsync(cancellationTokenSource.Token);

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string result = await httpClient.GetStringAsync(apiUrl);
                    int[] numbers = JsonSerializer.Deserialize<int[]>(result);
                    randomNumber = numbers[0];
                    Console.WriteLine($"\nПолучено рандомное число: {randomNumber}");
                }
            }
            catch
            {
                Random random = new Random();
                randomNumber = random.Next(1, str.Length);
                Console.WriteLine($"\nСгенерировано рандомное число: {randomNumber}");
            }
            //finally
            //{
            //    // Отменяем задачу показа сообщения об ожидании
            //    cancellationTokenSource.Cancel();
            //    await showWaitingMessageTask;
            //    Console.Write($"\r" + new string(' ', 0));
            //}
            return randomNumber;
        }

        static string DeleteRandomChar(string str, int randomNumber)
        {
            str = str.Remove(randomNumber - 1, 1);
            return str;
        }
        #endregion
        //Прикольный Taskbar с сообщением об ожидании
        static async Task ShowWaitingMessageAsync(CancellationToken cancellationToken)
        {
            string waitingMessage = "Ожидайте";
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.Write($"\r{waitingMessage}   ");
                    await Task.Delay(500, cancellationToken);
                    Console.Write($"\r{waitingMessage}.  ");
                    await Task.Delay(500, cancellationToken);
                    Console.Write($"\r{waitingMessage}.. ");
                    await Task.Delay(500, cancellationToken);
                    Console.Write($"\r{waitingMessage}...");
                    await Task.Delay(500, cancellationToken);
                }
                Console.Write("\r" + new string(' ', waitingMessage.Length + 3)); // Очищаем текст сообщения
            }
            catch
            {
                Console.Write("\r" + new string(' ', waitingMessage.Length + 3)); // Очищаем текст сообщения
            }
        }

        #region Методы Задание #8
        private bool WordBlackList(string inputStr)
        {
            if (_appSettings?.BlackList == null)
            {
                return false;
            }
            //Использую HashSet вместо List для более быстрой работы чёрного списка
            HashSet<string> blackList = new HashSet<string>(_appSettings.BlackList);

            return blackList.Contains(inputStr);
        }
        #endregion

        #region Классы Задание #5
        public class TreeNode
        {
            public char Value;
            public TreeNode Left;
            public TreeNode Right;

            public TreeNode(char value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }
        public class TreeSortClass
        {
            // Метод для вставки элемента в дерево
            public static void Insert(TreeNode node, char value)
            {
                if (value < node.Value)
                {
                    if (node.Left == null)
                    {
                        node.Left = new TreeNode(value);
                    }
                    else
                    {
                        Insert(node.Left, value);
                    }
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = new TreeNode(value);
                    }
                    else
                    {
                        Insert(node.Right, value);
                    }
                }
            }

            // Метод для обхода дерева в порядке возрастания
            public static void OrderTraversal(TreeNode node, List<char> result)
            {
                if (node != null)
                {
                    OrderTraversal(node.Left, result);
                    result.Add(node.Value);
                    OrderTraversal(node.Right, result);
                }
            }
        }
        #endregion
    }
}

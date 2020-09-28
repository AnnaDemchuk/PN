using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN
{
    public class Transform
    {
        public Dictionary<char, int> priority = new Dictionary<char, int>
        {
            {'(', 1},
            {'-', 2},
            {'+', 2},
            {'*', 3},
            {'/', 3},
            {'^', 4}
        };
        public List<string> exitArray = new List<string>();
        public List<char> operations = new List<char>();

        public double Start(string expression)
        {
            Console.WriteLine(expression);

            MakeNotation(expression);

            Console.WriteLine("=========");
            Print();

            Console.WriteLine($"Result: {CountResult(exitArray)}");

            return CountResult(exitArray);
        }

        public List<string> MakeNotation(string expression)
        {
            Check(expression);
            LastMove();

            return exitArray;
        }

        public void Check(string expression)
        {
            string next = "";
            for (int i = 0; i < expression.Length; i++)
            {
                // Если цифра
                if (Char.IsDigit(expression[i]) == true)
                {
                    while (Char.IsDigit(expression[i]) == true && priority.ContainsKey(expression[i]) != true)
                    {
                        next += expression[i];
                        i++;
                        if (expression.Length == i)
                        {
                            break;
                        }
                    }
                    exitArray.Add(next);
                    next = "";
                    i--;
                    Print();
                }


                if (expression[i] == '(')
                {
                    operations.Add(expression[i]);
                }
                // Если знак операции
                if (priority.ContainsKey(expression[i]) == true && expression[i] != '(')
                {
                    if (operations.Count == 0)
                    {
                        operations.Add(expression[i]);
                        Print();
                    }
                    else
                    {
                        // Сравнить с предыдущим знаком.
                        if (Char.IsDigit(operations.Last()) != true && priority[operations.Last()] >= priority[expression[i]])
                        {
                            // Добавить последний оператор в выход.массив.
                            exitArray.Add(operations.Last().ToString());

                            // Удалить последний оператор из операторов. 
                            operations.Remove(operations.Last());
                        }
                        // Добавить текущий оператор в операторы.
                        operations.Add(expression[i]);
                        Print();
                    }
                }

                // Переносит  все что за открывающейся скобкой в обратом порядке в массив выхода            
                if (expression[i] == ')')
                {
                    int index = operations.LastIndexOf('(');

                    for (int j = operations.Count - 1; j >= 0; j--)
                    {
                        if (j >= index)
                        {
                            exitArray.Add(operations[j].ToString());
                            operations.Remove(operations[j]);
                        }
                    }
                    // Открывающуюся скобку удалить
                    exitArray.Remove("(");
                    Print();
                }
            }
        }


        // Переносит ооператоры в обратном порядке в массив выхода
        public void LastMove()
        {
            for (int i = operations.Count - 1; i >= 0; i--)
            {
                exitArray.Add(operations[i].ToString());
            }
            operations.Clear();
        }

        public void Print()
        {
            Console.WriteLine("-----------");
            Console.Write($"exitArray : ");

            if (exitArray.Count == 0)
            {
                Console.Write("List empty");
            }
            else
            {
                foreach (var item in exitArray)
                {
                    Console.Write(item + " ");
                }
            }
            Console.WriteLine();

            Console.Write($"operations: ");
            if (exitArray.Count == 0)
            {
                Console.Write("List empty");
            }
            else
            {
                foreach (var item in operations)
                {
                    Console.Write(item + " ");
                }
            }
            Console.WriteLine("\n-----------");
        }

        public double CountResult(List<string> exitArray)
        {
            double number1;
            double number2;
            double result = 0;
            Stack<double> tmpStack = new Stack<double>();
            for (int i = 0; i < exitArray.Count; i++)
            {
                if (Char.IsDigit(exitArray[i][0]))
                {
                    double.TryParse(exitArray[i], out double temp);
                    tmpStack.Push(temp);
                }
                else
                {
                    number1 = tmpStack.Pop();
                    number2 = tmpStack.Pop();
                    switch (exitArray[i])
                    {
                        case "+":
                            result = number2 + number1;
                            break;
                        case "-":
                            result = number2 - number1;
                            break;
                        case "*":
                            result = number2 * number1;
                            break;
                        case "/":

                            result = number2 / number1;
                            break;
                        case "^":
                            result = Math.Pow(number2, number1);
                            break;
                        default:
                            Console.WriteLine("Unknown symbol");
                            break;
                    }
                    tmpStack.Push(result);
                }
            }
            return result;
        }
    }
}

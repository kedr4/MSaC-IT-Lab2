using System;
using System.Text.RegularExpressions;

namespace MSaC_IT_Lab2
{
    internal class CodeComplexityAnalyzer
    {
        public int AbsoluteComplexity { get; private set; } // CL
        public double RelativeComplexity { get; private set; } // cl
        public int MaxNestingLevel { get; private set; } // CLI
        private int totalOperators;

        // Конструктор
        public CodeComplexityAnalyzer()
        {
            AbsoluteComplexity = 0;
            RelativeComplexity = 0.0;
            MaxNestingLevel = 0;
            totalOperators = 0;
        }

        /// <summary>
        /// Метод для анализа содержимого кода.
        /// </summary>
        /// <param name="codeContent">Текст кода.</param>
        public void Analyze(string codeContent)
        {
            int currentNestingLevel = 0; // Текущий уровень вложенности
            MaxNestingLevel = 0;

            string[] lines = codeContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                //  if, else if, else
                if (IsConditionalStatement(trimmedLine))
                {
                    AbsoluteComplexity++;
                    currentNestingLevel++;
                    MaxNestingLevel = Math.Max(MaxNestingLevel, currentNestingLevel);
                }
                else if (IsLoopStatement(trimmedLine))
                {
                    AbsoluteComplexity++;
                    currentNestingLevel++;
                    MaxNestingLevel = Math.Max(MaxNestingLevel, currentNestingLevel);

                    // +2 для for (инициализация и инкремент)
                    if (trimmedLine.StartsWith("for"))
                    {
                        totalOperators += 2;
                    }
                }
                else if (IsSwitchStatement(trimmedLine))
                {
                    AbsoluteComplexity++;
                    int caseCount = CountCases(trimmedLine, lines);
                    currentNestingLevel += caseCount - (HasDefaultCase(trimmedLine, lines) ? 1 : 0);
                    MaxNestingLevel = Math.Max(MaxNestingLevel, currentNestingLevel);
                }
                else if (trimmedLine.StartsWith("}"))
                {
                    // Уменьшаем уровень вложенности при закрытии блока
                    if (currentNestingLevel > 0) currentNestingLevel--;
                }

                if (trimmedLine.EndsWith(";") || trimmedLine.Contains("="))
                {
                    totalOperators++;
                }

                // Проверка на присваивание
                if (Regex.IsMatch(trimmedLine, @"\s*[^=]*\s*=\s*[^;]*;"))
                {
                    totalOperators++; 
                }

                if (trimmedLine.StartsWith("return"))
                {
                    totalOperators++; 
                }
            }

            // Рассчитываем относительную сложность (cl)
            if (totalOperators > 0)
            {
                RelativeComplexity = (double)AbsoluteComplexity / totalOperators;
            }
        }




        /// <summary>
        /// Метод для проверки, является ли строка условным оператором.
        /// </summary>
        private bool IsConditionalStatement(string line)
        {
            return Regex.IsMatch(line, @"^\s*(if|else\s*if|else)\b");
        }

        /// <summary>
        /// Метод для проверки, является ли строка циклом.
        /// </summary>
        private bool IsLoopStatement(string line)
        {
            return Regex.IsMatch(line, @"^\s*(for|while|do)\b");
        }

        /// <summary>
        /// Метод для проверки, является ли строка оператором switch.
        /// </summary>
        private bool IsSwitchStatement(string line)
        {
            return Regex.IsMatch(line, @"^\s*switch\b");
        }

        /// <summary>
        /// Метод для подсчета количества кейсов в операторе switch.
        /// </summary>
        private int CountCases(string switchLine, string[] lines)
        {
            int caseCount = 0;
            bool switchFound = false;

            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("switch"))
                {
                    switchFound = true; // Начали анализировать switch
                }
                else if (switchFound && line.Trim().StartsWith("case"))
                {
                    caseCount++;
                }
                else if (switchFound && line.Trim().StartsWith("}"))
                {
                    break; // Завершаем анализ при выходе из switch
                }
            }

            return caseCount;
        }

        /// <summary>
        /// Метод для проверки, есть ли в операторе switch метка default.
        /// </summary>
        private bool HasDefaultCase(string switchLine, string[] lines)
        {
            bool switchFound = false;

            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("switch"))
                {
                    switchFound = true; // Начали анализировать switch
                }
                else if (switchFound && line.Trim().StartsWith("default"))
                {
                    return true; // Найдена метка default
                }
                else if (switchFound && line.Trim().StartsWith("}"))
                {
                    break; // Завершаем анализ при выходе из switch
                }
            }

            return false; // Метки default не найдено
        }

        /// <summary>
        /// Метод для вывода результатов анализа в RichTextBox.
        /// </summary>
        public void DisplayMetricsInRichTextBox(System.Windows.Forms.RichTextBox resultRichTextBox)
        {
            resultRichTextBox.Clear();
            resultRichTextBox.AppendText($"Абсолютная сложность (CL): {AbsoluteComplexity}\n");
            if (totalOperators > 0)
            {
                resultRichTextBox.AppendText($"Относительная сложность (cl): {RelativeComplexity:F3} (CL: {AbsoluteComplexity} / Операторы: {totalOperators})\n");
            }
            else
            {
                resultRichTextBox.AppendText($"Относительная сложность (cl): Неопределена (нет операторов).\n");
            }

            resultRichTextBox.AppendText($"Максимальный уровень вложенности (CLI): {MaxNestingLevel}\n");
        }
    }
}

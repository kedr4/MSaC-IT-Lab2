using Microsoft.VisualBasic.Logging;
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

        public CodeComplexityAnalyzer()
        {
            AbsoluteComplexity = 0;
            RelativeComplexity = 0.0;
            MaxNestingLevel = 0;
            totalOperators = 0;
        }

        public void Analyze(string codeContent)
        {
            int currentNestingLevel = 0;
            MaxNestingLevel = 0;
            AbsoluteComplexity = 0;
            totalOperators = 0;

            string[] lines = codeContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                // Count operators
                totalOperators += CountOperators(trimmedLine);
                
                // Count conditionals and loops
                if (IsConditionalStatement(trimmedLine) || IsLoopStatement(trimmedLine))
                {
                    AbsoluteComplexity++;
                }

                // Check for match statements
                if (IsMatchStatement(trimmedLine))
                {
                    AbsoluteComplexity++; // Count the match as a single block
                    AbsoluteComplexity += CountCases(lines); // Add number of cases including nested ifs
                }

                // Track nesting level
                if (trimmedLine.EndsWith("{"))
                {
                    currentNestingLevel++;
                    MaxNestingLevel = Math.Max(MaxNestingLevel, currentNestingLevel);
                }
                else if (trimmedLine.StartsWith("}"))
                {
                    if (currentNestingLevel > 0)
                        currentNestingLevel--;
                }
            }

            // Calculate relative complexity
            if (totalOperators > 0)
            {
                AbsoluteComplexity--;
                RelativeComplexity = (double)AbsoluteComplexity / totalOperators;
            }
        }

        private int CountOperators(string line)
        {
            var operatorRegex = new Regex(
                @"(_ =>|\<-|\+=|\-=|\*=|/=|==|\!=|\>=|\<=|\<-|\->|\=>|\|\||&&|::|<<|>>|\+|\-|\*|\/|\%|\=|\!|\>|\<|\&|\||\^|\~|\#|return)"
            );
            if (line.Contains("for"))
            {
                return 3; // Возвращаем 0, если есть for, чтобы не считать операторов
            }
            if (line.Contains("case"))
            {
                // Пробуем игнорировать `=>` и `_ =>` при подсчете
                line = line.Replace("=>", "").Replace("_ =>", "");
            }
            int operatorCount = operatorRegex.Matches(line).Count;

            

            return operatorCount;
        }

        private bool IsConditionalStatement(string line)
        {
            return Regex.IsMatch(line, @"^\s*(if|else\s*if|else)\b") || Regex.IsMatch(line, @"\bcase\b.*if\b");
        }

        private bool IsLoopStatement(string line)
        {
            return Regex.IsMatch(line, @"^\s*(for|while|do)\b");
        }

        private bool IsMatchStatement(string line)
        {
            return Regex.IsMatch(line, @"\b(match)\b");
        }

        private int CountCases(string[] lines)
        {
            int caseCount = 0;
            bool insideMatch = false;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("match"))
                {
                    insideMatch = true; // We're now inside a match statement
                }
                else if (insideMatch && trimmedLine.StartsWith("case"))
                {
                    caseCount++; // Count the case
                                 // Check for any 'if' conditions in the case statement
                    if (trimmedLine.Contains("if"))
                    {
                        caseCount++; // Consider this 'if' as an extra condition
                    }
                }
                else if (insideMatch && trimmedLine.StartsWith("default")) // Count default as part of match
                {
                    caseCount++;
                }
                else if (insideMatch && trimmedLine.StartsWith("}"))
                {
                    insideMatch = false; // We're exiting the match
                }
            }

            return caseCount;
        }


        public void DisplayMetricsInRichTextBox(System.Windows.Forms.RichTextBox richTextBox)
        {
            richTextBox.Clear();
            if (AbsoluteComplexity==-1) 
            {
                AbsoluteComplexity++;
            }
            richTextBox.AppendText($"Абсолютная сложность (CL): {AbsoluteComplexity}\n");
            richTextBox.AppendText($"Относительная сложность (cl): {AbsoluteComplexity}/{totalOperators} = {RelativeComplexity:F3}\n");
            richTextBox.AppendText($"Максимальный уровень вложенности (CLI): {MaxNestingLevel}\n");
        }
    }
}

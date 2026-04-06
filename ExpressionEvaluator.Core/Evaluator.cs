using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExpressionEvaluator.Core;

public class Evaluator
{
    public static double Evaluate(string infix)
    {
        var postfix = InfixToPostfix(infix);
        return EvaluatePostfix(postfix);
    }

    private static Queue<string> InfixToPostfix(string infix)
    {
        var postFix = new Queue<string>();
        var stack = new Stack<char>();
        

        var numberBuffer = new Queue<char>();

        foreach (var item in infix)
        {
            if (IsOperator(item))
            {

                if (numberBuffer.Count > 0)
                {
                    string num = "";
                    while (numberBuffer.Count > 0)
                    {
                        num += numberBuffer.Dequeue();
                    }
                    postFix.Enqueue(num);
                }

                if (stack.Count == 0)
                {
                    stack.Push(item);
                }
                else
                {
                    if (item == ')')
                    {
                        do
                        {
                            postFix.Enqueue(stack.Pop().ToString()); 
                        } while (stack.Peek() != '(');
                        stack.Pop();
                    }
                    else
                    {
                        if (PriorityInfix(item) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            postFix.Enqueue(stack.Pop().ToString());
                            stack.Push(item);
                        }
                    }
                }
            }
            else
            {
                numberBuffer.Enqueue(item);
            }
        }

        if (numberBuffer.Count > 0)
        {
            string num = "";
            while (numberBuffer.Count > 0)
            {
                num += numberBuffer.Dequeue();
            }
            postFix.Enqueue(num);
        }

        while (stack.Count > 0)
        {
            postFix.Enqueue(stack.Pop().ToString());
        }
        
        return postFix;
    }

    private static int PriorityStack(char item) => item switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new Exception("Sintax error."),
    };

    private static int PriorityInfix(char item) => item switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => throw new Exception("Sintax error."),
    };

    private static double EvaluatePostfix(Queue<string> postfix)
    {
        var stack = new Stack<double>();
        
        foreach (string item in postfix)
        {
            if (item.Length == 1 && IsOperator(item[0]))
            {
                var b = stack.Pop();
                var a = stack.Pop();
                stack.Push(item[0] switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => a / b,
                    '^' => Math.Pow(a, b),
                    _ => throw new Exception("Sintax error."),
                });
            }
            else
            {

                stack.Push(double.Parse(item, CultureInfo.InvariantCulture));
            }
        }
        return stack.Pop();
    }

    private static bool IsOperator(char item) => "+-*/^()".Contains(item);
}
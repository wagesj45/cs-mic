using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace csmic.Extensions
{
    /// <summary> CS-MIC extension methods. </summary>
    public static class CSMICExtension
    {
        #region String

        /// <summary> A string extension method that interprets as input the string that calls it. </summary>
        /// <param name="input"> The input to act on. </param>
        /// <returns> The output from the interpretation of the string. </returns>
        public static string Interpret(this string input)
        {
            InputInterpreter interpreter = new InputInterpreter();
            interpreter.Interpret(input);
            return interpreter.Output;
        }

        /// <summary> A string extension method that executes as macro operation. </summary>
        /// <param name="script"> The script to act on. </param>
        /// <returns> The final output of the script. </returns>
        public static string RunAsMacro(this string script)
        {
            MacroBuilder macro = new MacroBuilder(script, new InputInterpreter());
            return macro.FinalOutput;
        }

        #endregion

        #region IEnumerable<string>

        /// <summary>
        /// A string extension method that interprets as input the string that calls it.
        /// </summary>
        /// <param name="collection"> The collection to act on. </param>
        /// <returns> The output from the interpretation of the string. </returns>
        public static IEnumerable<string> Interpret(this IEnumerable<string> collection)
        {
            List<string> computed = new List<string>();
            InputInterpreter interpreter = new InputInterpreter();

            foreach (string input in collection)
            {
                interpreter.Interpret(input);
                computed.Add(interpreter.Output);
            }

            return computed;
        }

        /// <summary>
        /// A string extension method that interprets as input the string that calls it.
        /// </summary>
        /// <param name="collection"> The collection to act on. </param>
        /// <param name="action">     The action. </param>
        /// <returns> The output from the interpretation of the string. </returns>
        public static IEnumerable<string> Interpret(this IEnumerable<string> collection, Action<InputInterpreter> action)
        {
            List<string> computed = new List<string>();
            InputInterpreter interpreter = new InputInterpreter();

            foreach (string input in collection)
            {
                interpreter.Interpret(input);
                computed.Add(interpreter.Output);
                action(interpreter);
            }

            return computed;
        }

        /// <summary> Enumerates input in this collection, returning a selection on the interpreter. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="collection"> The collection to act on. </param>
        /// <param name="selection">  The selection. </param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process interpret&lt; t&gt; in this
        /// collection.
        /// </returns>
        public static IEnumerable<T> Interpret<T>(this IEnumerable<string> collection, Func<InputInterpreter, T> selection)
        {
            List<T> computed = new List<T>();
            InputInterpreter interpreter = new InputInterpreter();

            foreach (string input in collection)
            {
                interpreter.Interpret(input);
                computed.Add(selection(interpreter));
            }

            return computed;
        } 

        /// <summary> A string extension method that executes as macro operation. </summary>
        /// <param name="collection"> The collection to act on. </param>
        /// <returns> The final output of the script. </returns>
        public static MacroBuilder RunAsMacro(this IEnumerable<string> collection)
        {
            return new MacroBuilder(string.Join(Environment.NewLine, collection.ToArray()), new InputInterpreter());
        }

        #endregion

        #region IEnumerable<string> In Parallel

        /// <summary> Enumerates input in parallel in this collection. </summary>
        /// <param name="collection"> The collection to act on. </param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process interpret in parallel in this
        /// collection.
        /// </returns>
        public static IEnumerable<InputInterpreter> InterpretInParallel(this IEnumerable<string> collection)
        {
            ConcurrentBag<InputInterpreter> bag = new ConcurrentBag<InputInterpreter>();

            collection.AsParallel().ForAll(input =>
            {
                InputInterpreter interpreter = new InputInterpreter();
                interpreter.Interpret(input);
                bag.Add(interpreter);
            });

            return bag;
        }

        /// <summary> Enumerates input in parallel this collection, returning a selection on the interpreter. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="collection"> The collection to act on. </param>
        /// <param name="selection">  The selection. </param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process interpret in parallel&lt; t&gt; in
        /// this collection.
        /// </returns>
        public static IEnumerable<T> InterpretInParallel<T>(this IEnumerable<string> collection, Func<InputInterpreter, T> selection)
        {
            ConcurrentBag<T> bag = new ConcurrentBag<T>();

            collection.AsParallel().ForAll(input =>
            {
                InputInterpreter interpreter = new InputInterpreter();
                interpreter.Interpret(input);
                bag.Add(selection(interpreter));
            });

            return bag;
        } 

        #endregion

        #region ParallelQuery<string>

        /// <summary>
        /// A string extension method that interprets as input the strings that calls it in parallel.
        /// </summary>
        /// <param name="collection"> The collection to act on. </param>
        /// <returns> The output from the interpretation of the string. </returns>
        public static IEnumerable<InputInterpreter> Interpret(this ParallelQuery<string> collection)
        {
            ConcurrentBag<InputInterpreter> bag = new ConcurrentBag<InputInterpreter>();

            collection.ForAll(input =>
            {
                InputInterpreter interpreter = new InputInterpreter();
                interpreter.Interpret(input);
                bag.Add(interpreter);
            });

            return bag;
        }

        #endregion
    }
}

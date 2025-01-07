using System;
using System.Text;

namespace OldPhone
{
    class Program
    {
        //Initialize a jagged array where the first item of the array is the input and rest are 
        //the outputs for the input
        private static char[][] _inputMapping = new char[][]
        {
            new char[] { '1', '&', '\'', '(' },
            new char[] { '2', 'A', 'B', 'C' },
            new char[] { '3', 'D', 'E', 'F' },
            new char[] { '4', 'G', 'H', 'I' },
            new char[] { '5', 'J', 'K', 'L' },
            new char[] { '6', 'M', 'N', 'O' },
            new char[] { '7', 'P', 'Q', 'R', 'S' },
            new char[] { '8', 'T', 'U', 'V' },
            new char[] { '9', 'W', 'X', 'Y', 'Z' }
        };

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">Array of arguments to the function</param>
        static void Main(string[] args)
        {
            //Test values
            string _value = OldPhonePad("33#");
            string _value1 = OldPhonePad("227*#");
            string _value2 = OldPhonePad("4433555 555666#");
            string _value3 = OldPhonePad("8 88777444666*664#");
            string _value4 = OldPhonePad("44 444#");

            Console.WriteLine("value: 33# -> " + _value);
            Console.WriteLine("value1: 227*# -> " + _value1);
            Console.WriteLine("value2: 4433555 555666# -> " + _value2);
            Console.WriteLine("value3: 8 88777444666*664# -> " + _value3);
            Console.WriteLine("value3: 44 444# -> " + _value4);
        }

        /// <summary>
        /// Returns the output char array for the input
        /// </summary>
        /// <param name="input">Input as a char</param>
        /// <returns>Output array for the input</returns>
        private static char[] ReturnOutput(char input)
        {
            //Use LINQ to find the array where the first item equals the input
            char[] _outputArray = _inputMapping.FirstOrDefault(c => c[0] == input);

            if (_outputArray != null)
            {
                return _outputArray;
            }

            //If nothing is found, return null
            return null;
        }

        /// <summary>
        /// Returns the value for the input
        /// </summary>
        /// <param name="input">Input as a string</param>
        /// <returns>Output for the input</returns>
        public static String OldPhonePad(string input)
        {
            //Initialize StringBuilder to return the value
            StringBuilder _return = new StringBuilder();

            //Initialize initial old input char as an empty char
            char _oldChar = '\0';
            //Start at index 1 for the _inputMapping static variable since index 0 is the input and rest are 
            //the outputs
            int _count = 1;
            //Initialize initial value as empty char
            char _output = '\0';

            //For each char in input
            foreach (var _inputChar in input)
            {
                //If it matches '#', assume that this the ending of processing and append the 
                //existing/pending output char to the return value and return
                if (_inputChar == '#')
                {
                    //Make sure the pending output has a value before appending it to the return value
                    if (_output != '\0')
                    {
                        _return.Append(_output);
                    }
                    _count = 1;
                    break;
                }
                //If it matches * (which is a delete command), don't attach the pending output (same as deletion 
                //since the output is not being appended to the return value) and reset the count back to 1 
                //and continue
                if (_inputChar == '*')
                {
                    _count = 1;
                    //Set the pending output to empty
                    _output = '\0';
                    continue;
                }

                //If it matches 0 (which is a space), append a space to the return value, reset the count back to 1
                //and then continue
                if (_inputChar == '0')
                {
                    _count = 1;
                    _return.Append(' ');
                    continue;
                }

                //If it matches an empty space assume that this is a one second pause, append the pending output
                //and reset the count back to 1 and continue
                if (_inputChar == ' ')
                {
                    _return.Append(_output);
                    _count = 1;
                    continue;
                }

                //If there is an old input char and it doesn't match the current input char, assume
                //that this a new char and append the previous pending char to the return value and 
                //reset the count back to 1
                if (_oldChar != '\0' && _oldChar != _inputChar)
                {
                    _count = 1;
                    _return.Append(_output);
                }

                //Get the output array for the input
                char[] _outputMapping = ReturnOutput(_inputChar);

                //if there is corresponding output array for the input, get the output char based on 
                //the count and set the current input char as the old char and increase the count.
                //If there is no corresponding output array, don't do anything and skip
                if (_outputMapping != null)
                {
                    //If char count is greater than the output array, reset the counter back to 1 
                    //for the output's first char
                    if (_count >= _outputMapping.Length)
                    {
                        _count = 1;
                    }
                    else
                    {
                        //get actual char based on the count and save it to _output
                        _output = _outputMapping[_count];
                    }

                    _oldChar = _inputChar;
                    _count++;
                }
            }

            //return the value after looping through all the chars in the input
            return _return.ToString();
        }
    }
}
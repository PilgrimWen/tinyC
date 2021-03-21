using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class LexicalAnalyze
    {
        List<Token> tokens = new List<Token>();
        public LexicalAnalyze()
        {

        }
        void AddToken(string value)
        {
            Token token = new Token();
            token.row = token.column = -1;
            token.value = value;
            tokens.Add(token);
        }

        public List<Token> StartLexicalAnalyze(string codes)
        {
            var length = codes.Length;
            var index = 0;
            var start = 0;
            State state = State.Begin;
            Token token = new Token();
            token.row = token.column = -1;
            while (index < length)
            {
                var c = codes[index];
                switch (state)
                {
                    case State.Begin:
                        switch (c)
                        {
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '(':
                            case ')':
                                string v = codes.Substring(index, 1);
                                AddToken(v);
                                break;
                            case ' ':
                            case '\t':
                            case '\r':
                                break;
                            case '\n':
                                break;
                            default:
                                if ('0' <= c && c <= '9')
                                {
                                    state = State.InIdentifier;
                                    start = index;
                                }
                                else if ('a' <= c && c <= 'z' || 'A' <= c && c <= 'Z' || c == '_')
                                {
                                    state = State.InIdentifier;
                                    start = index;
                                }
                                else
                                {
                                    //error
                                }
                                break;
                        }
                        break;
                    case State.InInteger:
                        if ('0' <= c && c <= '9')
                        {
                            // stay still
                        }
                        else if (c == '.' && '0' <= codes[index + 1] && codes[index + 1] <= '9')
                        {
                            state = State.InFloat;
                        }
                        else
                        {
                            string v = codes.Substring(start, index - start);
                            AddToken(v);
                            state = State.Begin;
                            index--;
                            start = 0;
                        }
                        break;
                    case State.InIdentifier:
                        if ('a' <= c && c <= 'z' || 'A' <= c && c <= 'Z' || c == '_' || c == '.' || c == '-')
                        {
                            // stay still
                        }
                        else
                        {
                            string v = codes.Substring(start, index - start);
                            AddToken(v);
                            state = State.Begin;
                            index--;
                            start = 0;
                        }
                        break;
                    default:
                        break;
                }
                index++;

            }
            switch (state)
            {

                case State.InInteger:
                    string v = codes.Substring(start, index - start);
                    AddToken(v);
                    break;
                case State.InFloat:
                    v = codes.Substring(start, index - start);
                    AddToken(v);
                    break;
                case State.InIdentifier:
                    v = codes.Substring(start, index - start);
                    AddToken(v);
                    break;
                case State.InString:
                case State.InStringEscaping:
                    v = codes.Substring(start, index - start);
                    AddToken(v);
                    break;
            }
            return tokens;
        }
    }
}

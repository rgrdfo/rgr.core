using System;
using System.Collections.Generic;

namespace Danko.TextJobs
{
    public enum LexemeType : byte{Void, Keyword, Separator, Comment, Quote, DoubleQuote, StringValue, IntValue, FractValue};

    public class Lexeme : object
    {
        public string      content;
        public LexemeType  type;

        public Lexeme()
        {
            content = "";
            type = LexemeType.Void;
        }

        public Lexeme(string content, LexemeType type)
        {
            this.content = content;
            this.type = type;
        }
    }

	public static class TextAnalyzer : object
	{
		//Разбор строки на лексемы (слова). Выводит список слов
		public static List<Lexeme> GetLexemes (string source)
		{
			List<Lexeme> lexemes     = new List<Lexeme>();
			int 		 carette   = 0;
			bool 		 inComment = false;
			bool 		 inValue   = false;
            bool GuessWeHaveCommentHere = false;

            lexemes.Add (new Lexeme());
			
			for (carette = 0; carette < source.Length; carette++) 
			{
				char chr = source[carette];
                char lst = new char();
                if (carette > 0)
                    lst = source[carette-1];

                switch (chr)
                {
                    //Кавычка, переключение режима "значение" (т.е. текст в кавычках записывается в одно слово целиком)			
                    case '"':
                        if (lst != '\'' && !inComment) //Если кавычка - не символ
                        {
                            if (lexemes[lexemes.Count - 1].content != "")
                                lexemes.Add(new Lexeme(char.ToString(chr), LexemeType.DoubleQuote));
                            else
                                lexemes[lexemes.Count - 1] = new Lexeme(char.ToString(chr), LexemeType.DoubleQuote);
                            lexemes.Add(new Lexeme());
                            inValue = !inValue;
                        }
                        else
                            lexemes[lexemes.Count - 1].content += char.ToString(chr);
                        break;

                        /*
                    //Апостроф, переключение режима "значение" (т.е. текст в одиночных кавычках записывается в одно слово целиком)	
                    case '\'':
                        if (lexemes[lexemes.Count - 1].content != "")
                            lexemes.Add(new Lexeme(char.ToString(chr), LexemeType.Quote));
                        else
                            lexemes[lexemes.Count - 1] = new Lexeme(char.ToString(chr), LexemeType.Quote);
                        lexemes.Add(new Lexeme());
                        inValue = !inValue;
                        break;
                        */
                 
                    case '/':
                        lexemes[lexemes.Count - 1].content += char.ToString(chr);
                        lexemes[lexemes.Count - 1].type  = LexemeType.Separator;

                        if (!GuessWeHaveCommentHere)
                            GuessWeHaveCommentHere = true;
                        else
                        {
                            GuessWeHaveCommentHere = false;
                            inComment = true;
                            lexemes.Add(new Lexeme());
                        }
                            
                    break;
                  
                    //Перобел, табуляция. Записывается, если идёт запись в значение или комментарий, иначе игнорируется
                     case '\u0020': case '\u0009':
                        if (inValue || inComment)
                            lexemes[lexemes.Count - 1].content += char.ToString(chr);
                        else lexemes.Add(new Lexeme());
                    break;

                     
                    //Перенос строки. Записывается, если идёт запись в значение. Отключает режим комментария
                    case '\n': case '\r':
                        if (inValue)
                            lexemes[lexemes.Count - 1].content += char.ToString(chr);
                        else
                        {
                            inComment = false;
                            GuessWeHaveCommentHere = false;
                            lexemes.Add(new Lexeme());
                        }
                    break;

                    //Числа
                    case '0': case '1': case '2': case '3': case '4':
                    case '5': case '6': case '7': case '8': case '9':
                        if (!inValue && !inComment && lexemes[lexemes.Count - 1].type != LexemeType.FractValue)
                        {
                            //проверка,я вляется ли предыдущий символ цифрой или
                            //точкой (отделяющей дробные от целых). Если нет - 
                            //создаётся новая лексема
                            string digits = "1234567890";
                            bool LastCharIsDigit = digits.Contains(char.ToString(lst));
                            if (!LastCharIsDigit)
                                lexemes.Add(new Lexeme(char.ToString(chr), LexemeType.IntValue));
                            else
                            {
                                lexemes[lexemes.Count - 1].content += char.ToString(chr);
                            }

                        }
                        else
                            lexemes[lexemes.Count - 1].content += char.ToString(chr);
                    break;

                    //Точка либо работает аналогично разделителям ниже, либо идентифицирует дробное число
                    case '.':
                        if (lexemes[lexemes.Count-1].type == LexemeType.IntValue)
                        {
                            lexemes[lexemes.Count - 1].type = LexemeType.FractValue;
                            lexemes[lexemes.Count - 1].content += char.ToString(chr);
                        }
                        else
                        {
                            if (!inValue && !inComment)
                            {
                                if (lexemes[lexemes.Count - 1].content != "")
                                    lexemes.Add(new Lexeme(char.ToString(chr), LexemeType.Separator));
                                else
                                    lexemes[lexemes.Count - 1] = new Lexeme(char.ToString(chr), LexemeType.Separator);
                                lexemes.Add(new Lexeme());
                            }
                            else
                                lexemes[lexemes.Count - 1].content += char.ToString(chr);
                        }
                    break;

                    //Разделители, записываются отдельным словом (если не идёт запись в значение или комментарий)    
                    case '№': case ':': case ',': case '[': case '?':
                    case ']': case '{': case '}': case '(': case ')':
					case '&': case '|': case '+': case '-': case '*':
					case '!': case '=': case '<': case '>': case '#':
                    case ';': case '@': case '%': case '^': case '`':
                    case '~': case '\\':case '\'':
                        if (!inValue && !inComment)
						{
							if (lexemes[lexemes.Count-1].content != "")
                            	lexemes.Add (new Lexeme(char.ToString(chr), LexemeType.Separator));
							else
								lexemes[lexemes.Count-1] = new Lexeme(char.ToString(chr), LexemeType.Separator);
							lexemes.Add (new Lexeme());
						}
                        else
							lexemes[lexemes.Count-1].content += char.ToString(chr);
                    break;

                    //Остальное добавляется к текущему слову    
                    default:
						lexemes[lexemes.Count-1].content += char.ToString(chr);
                    break;
                }

                //Контрольное определение типа лексемы: если тип - пустота, но слово непустое, то что-то тут не так!
                if (lexemes[lexemes.Count - 1].type == LexemeType.Void && !string.IsNullOrEmpty(lexemes[lexemes.Count - 1].content))
                {
                    if (inValue)
                        lexemes[lexemes.Count - 1].type = LexemeType.StringValue;
                    
                    else if (inComment)
                        lexemes[lexemes.Count - 1].type = LexemeType.Comment;

                    else
                        lexemes[lexemes.Count - 1].type = LexemeType.Keyword;
                }
            }
            
            //Контрольный фильтр (убираем пустые слова и слова-переносы)
			//lexemes = lexemes.Where(n => n != Environment.NewLine && !string.IsNullOrEmpty(n));
		    List<Lexeme> output = new List<Lexeme>();
            foreach (Lexeme lexeme in lexemes)
            {
                if (lexeme.type != LexemeType.Void)
                    output.Add(lexeme);
            }
                          
			return output;
        }


        public static bool WordExists(List<Lexeme> lexemes, string wordToCheck)
        {
            foreach (Lexeme lexeme in lexemes)
            {
                if (lexeme.content == wordToCheck)
                    return true;
            }

            return false;
        }
        public static bool WordExists(string source, string wordToCheck)
        {
            List<Lexeme> lexemes = GetLexemes(source);
            return WordExists(lexemes, wordToCheck);
        }


        //Получить значение. Вариант 1: прямое указание, на сколько слов отстоит значение от ключа
        public static string GetValue(List<Lexeme> lexemes, int offset, string key)
        {
			for (int i = 0; i < lexemes.Count; i++)
				if (lexemes[i].content == key)
					return lexemes[i+offset].content;

			//Ключ не найден
			return null;
        }
        public static string GetValue(string source, int offset, string key)
        {
            List<Lexeme> lexemes = GetLexemes(source);
            return GetValue(lexemes, offset, key);
        }

        //Получить значение. Вариант 2: в качестве значениz возвращается свойство
        //content первой лексемы типа LexemeType.StringValue, следующей за ключом
        public static string GetValue(List<Lexeme> lexemes, string key)
        {
            bool SearchingForValue = false;

            for (int i = 0; i < lexemes.Count; i++)
            {
                if (!SearchingForValue)
                {
                    if (lexemes[i].content == key)
                        SearchingForValue = true;
                }
                else
                {
                    if (lexemes[i].type == LexemeType.StringValue)
                        return lexemes[i].content;
                }
            }

            //Ключ или значение не найдены
            return null;
        }
        public static string GetValue(string source, string key)
        {
            List<Lexeme> lexemes = GetLexemes(source);
            return GetValue(lexemes, key);
        }
    }
}


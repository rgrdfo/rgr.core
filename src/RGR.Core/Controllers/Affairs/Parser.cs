using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eastwing.Parser
{
    public enum Category : byte
    {
        /// <summary>
        /// Используется утилитарно. Токен этой категории не может быть получен в ходе разбора строки
        /// </summary>
        Void,
        /// <summary>
        /// Строка - текст, заключённы в кавычки или апострофы
        /// </summary>
        String,
        /// <summary>
        /// Целое число
        /// </summary>
        Integer,
        /// <summary>
        /// Вещественное число
        /// </summary>
        Real,
        /// <summary>
        /// Слово, не указанное ключевым
        /// </summary>
        Word,
        /// <summary>
        /// Слово из списка ключевых слов
        /// </summary>
        Keyword,
        /// <summary>
        /// Кавычка или апостроф
        /// </summary>
        Quote,
        Plus,
        Minus,
        /// <summary>
        /// Т.н. "звёздочка"
        /// </summary>
        Asterisk,
        Equals,
        Slash,
        Backslash,
        /// <summary>
        /// Различные символы, не вошедшие в прочие категории
        /// </summary>
        Separator,
        /// <summary>
        /// Скобка
        /// </summary>
        Bracket,
        /// <summary>
        /// Пробел или табуляция
        /// </summary>
        Space,
        /// <summary>
        /// Неизвестный токен
        /// </summary>
        Unknown
    }

    public struct Token
    {
        public string Lexeme;
        public Category Category;

        /// <summary>
        /// Вывод лексемы
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Lexeme;

        /// <summary>
        /// Вывод обоих полей в формате "(Категория) Лексема"
        /// </summary>
        /// <returns></returns>
        public string Full => $"({Category}) {Lexeme}";

        public Token(string Lexeme, Category Category)
        {
            this.Lexeme = Lexeme;
            this.Category = Category;
        }

        /// <summary>
        /// Присваивает существующему экземпляру новые значения без необходимости создавать новый экхемпляр
        /// </summary>
        /// <param name="Lexeme"></param>
        /// <param name="Category"></param>
        /// <returns></returns>
        public Token Reinit (string Lexeme, Category Category)
        {
            this.Lexeme = Lexeme;
            this.Category = Category;

            return this;
        }
    }

    public class Parser
    {
        public IEnumerable<string> Keywords { get; set; } = new string[0];

        public string Letters { get; set; } = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_";
        public string Digits  { get; set; } = "0123456789";
        public string Quotes  { get; set; } = "\"'";
        public string Separators { get; set; } = "!@#$%^&?|`~№;:,";
        public string Brackets { get; set; } = "()<>{}[]";
        public char RadixPoint { get; set; } = '.';

        private static Category[] SetCategories = new Category[] { Category.String, Category.Integer, Category.Real, Category.Word, Category.Keyword, Category.Unknown };

        private Token OutToken(ref Token token, object lexContainer, Category cat)
        {
            string lexeme = lexContainer.ToString();
            if (cat == Category.Word && Keywords.Contains(lexeme))
                cat = Category.Keyword;

            return token.Reinit(lexeme, cat);
        }

        public IEnumerable<Token> Parse(string Text)
        {
            var cToken = new Token("", Category.Void);
            var sbLex  = new StringBuilder();
            var curCat = new Category();
            char stringOpener = default(char);

            for (int i = 0; i < Text.Length; i++)
            {
                if (curCat == Category.String && !(Quotes.Contains(Text[i]) && Text[i] == stringOpener))
                    sbLex.Append(Text[i]);
                else
                {
                    if (Text[i] == ' ' || Text[i] == '\t')
                    {
                        if(curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return cToken.Reinit(Text[i].ToString(), Category.Space);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '\n' || Text[i] == '\r')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Letters.Contains(Text[i]))
                    {
                        if (curCat != Category.Word && curCat != Category.Unknown)
                            curCat = Category.Word;
                        sbLex.Append(Text[i]);
                    }

                    else if (Digits.Contains(Text[i]))
                    {
                        if (curCat != Category.Integer && curCat != Category.Real && curCat != Category.Word && curCat != Category.Unknown)
                            curCat = Category.Integer;
                        sbLex.Append(Text[i]);
                    }

                    else if (Text[i] == RadixPoint)
                    {
                        if (i + 1 != Text.Length)
                        {
                            if (curCat == Category.Integer)
                            {
                                if (Digits.Contains(Text[i + 1]))
                                {
                                    curCat = Category.Real;
                                    sbLex.Append(Text[i]);
                                }
                                else
                                {
                                    if (curCat != Category.Void)
                                        yield return OutToken(ref cToken, sbLex, curCat);
                                    yield return OutToken(ref cToken, Text[i], Category.Separator);
                                    curCat = Category.Void;
                                    sbLex.Clear();
                                }
                            }
                            else
                            {
                                if (curCat != Category.Void)
                                    yield return OutToken(ref cToken, sbLex, curCat);
                                yield return OutToken(ref cToken, Text[i], Category.Separator);
                                curCat = Category.Void;
                                sbLex.Clear();
                            }
     
                        }
                        
                    }

                    else if (Quotes.Contains(Text[i]))
                    {
                        if (curCat != Category.String)
                        {
                            stringOpener = Text[i];
                            curCat = Category.String;
                            yield return cToken.Reinit(Text[i].ToString(), Category.Quote);
                        }
                        else
                        {
                            if (Text[i] == stringOpener)
                            {
                                yield return cToken.Reinit(sbLex.ToString(), Category.String);
                                yield return cToken.Reinit(Text[i].ToString(), Category.Quote);
                                curCat = Category.Void;
                                sbLex.Clear();
                            }
                            else
                                sbLex.Append(Text[i]);
                        }
                    }

                    else if (Separators.Contains(Text[i]))
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Separator);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Brackets.Contains(Text[i]))
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Bracket);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '=')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Equals);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '+')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Plus);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '-')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Minus);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '*')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Asterisk);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '/')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Slash);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else if (Text[i] == '\\')
                    {
                        if (curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                        yield return OutToken(ref cToken, Text[i], Category.Backslash);
                        curCat = Category.Void;
                        sbLex.Clear();
                    }

                    else
                    {
                        curCat = Category.Unknown; //Зашквар
                        sbLex.Append(Text[i]);
                    }

                    if (i == Text.Length-1)
                    {
                        if (SetCategories.Contains(curCat) && curCat != Category.Void)
                            yield return OutToken(ref cToken, sbLex, curCat);
                    }
                } 
            }
        }
    }
}

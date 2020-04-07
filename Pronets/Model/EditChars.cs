using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model
{
    public static class EditChars
    {
        /// <summary>
        /// <para> Edit Russian chars to English</para>
        /// </summary> 
        public static string ToEnglish(string word)
        {
            var result = "";
            if (word != null)
            {
                Dictionary<string, string> dictionaryChar = new Dictionary<string, string>()
            {
                {"а","f"},
                {"б",","},
                {"в","d"},
                {"г","u"},
                {"д","l"},
                {"е","t"},
                {"ё","`"},
                {"ж",";"},
                {"з","p"},
                {"и","b"},
                {"й","q"},
                {"к","r"},
                {"л","k"},
                {"м","v"},
                {"н","y"},
                {"о","j"},
                {"п","g"},
                {"р","h"},
                {"с","c"},
                {"т","n"},
                {"у","e"},
                {"ф","a"},
                {"х","["},
                {"ц","w"},
                {"ч","x"},
                {"ш","i"},
                {"щ","o"},
                {"ъ","]"},
                {"ы","s"},
                {"ь","m"},
                {"э","'"},
                {"ю","."},
                {"я","z"},

                {"А","f"},
                {"Б",","},
                {"В","d"},
                {"Г","u"},
                {"Д","l"},
                {"Е","t"},
                {"Ё","`"},
                {"Ж",";"},
                {"З","p"},
                {"И","b"},
                {"Й","q"},
                {"К","r"},
                {"Л","k"},
                {"М","v"},
                {"Н","y"},
                {"О","j"},
                {"П","g"},
                {"Р","h"},
                {"С","c"},
                {"Т","n"},
                {"У","e"},
                {"Ф","a"},
                {"Х","["},
                {"Ц","w"},
                {"Ч","x"},
                {"Ш","i"},
                {"Щ","o"},
                {"Ъ","]"},
                {"Ы","s"},
                {"Ь","m"},
                {"Э","'"},
                {"Ю","."},
                {"Я","z"}

            };
                // проход по строке для поиска символов подлежащих замене которые находятся в словаре dictionaryChar
                foreach (var ch in word)
                {
                    var ss = "";
                    // берём каждый символ строки и проверяем его на нахождение его в словаре для замены,
                    // если в словаре есть ключ с таким значением то получаем true 
                    // и добавляем значение из словаря соответствующее ключу
                    if (dictionaryChar.TryGetValue(ch.ToString(), out ss))
                    {
                        result += ss;
                    }
                    // иначе добавляем тот же символ
                    // иначе добавляем тот же символ
                    else result += ch;
                }
            }
            return result;
        }
    }
}

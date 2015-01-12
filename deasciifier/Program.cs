using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace deasciifier
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] asciiStrings = {
			        "Acimasizca acelya gorunen bir sacmaliktansa acilip sacilmak...",
			        "Acisindan bagirip cagirarak sacma sozler soylemek.",
			        "Bogurtuler opucukler.",
			        "BUYUKCE BIR TOPAC TOPARLAGI VE DE YUMAGI yumagi.",
			        "Bilgisayarlarda uc adet bellek turu bulunur. Islemci icerisinde yer alan yazmaclar, son derece hizli ancak cok sinirli hafizaya sahiptirler. Islemcinin cok daha yavas olan ana bellege olan erisim gereksinimini gidermek icin kullanilirlar. Ana bellek ise Rastgele erisimli bellek (REB veya RAM, Random Access Memory) ve Salt okunur bellek (SOB veya ROM, Read Only Memory) olmak uzere ikiye ayrilir. RAM'a istenildigi zaman yazilabilir ve icerigi ancak guc surdugu surece korunur. ROM'sa sâdece okunabilen ve onceden yerlestirilmis bilgiler icerir. Bu icerigi gucten bagimsiz olarak korur. Ornegin herhangi bir veri veya komut RAM'da bulunurken, bilgisayar donanimini duzenleyen BIOS ROM'da yer alir.",
			        "1969 yilinda 15 yasindayken 1000 lira transfer parasi alarak Camialti Spor Kulubu'nde amator futbolcu oldu. Daha sonra IETT Spor Kulubu'nun amator futbolcusu oldu. 1976 yilinda, IETT sampiyon oldugunda, Erdogan da bu takimda oynamaktaydi. Erokspor Kulubunde de futbola devam etti ve 16 yillik futbol yasamini 12 Eylul 1980 Askeri Darbesi sonrasinda birakti ve daha fazla siyasi faaliyet..." };

	        string[] turkishStrings = {
			        "Acımasızca açelya görünen bir saçmalıktansa açılıp saçılmak...",
			        "Acısından bağırıp çağırarak saçma sözler söylemek.",
			        "Böğürtüler öpücükler.",
			        "BÜYÜKÇE BİR TOPAÇ TOPARLAĞI VE DE YUMAĞI yumağı.",
			        "Bilgisayarlarda üç adet bellek turu bulunur. İşlemci içerisinde yer alan yazmaçlar, son derece hızlı ancak çok sınırlı hafızaya sahiptirler. İşlemcinin çok daha yavaş olan ana bellege olan erişim gereksinimini gidermek için kullanılırlar. Ana bellek ise Rastgele erişimli bellek (REB veya RAM, Random Access Memory) ve Salt okunur bellek (SOB veya ROM, Read Only Memory) olmak üzere ikiye ayrılır. RAM'a istenildiği zaman yazılabilir ve içeriği ancak güç sürdüğü sürece korunur. ROM'sa sâdece okunabilen ve önceden yerleştirilmiş bilgiler içerir. Bu içeriği güçten bağımsız olarak korur. Örneğin herhangi bir veri veya komut RAM'da bulunurken, bilgisayar donanımını düzenleyen BİOS ROM'da yer alır.",
			        "1969 yılında 15 yaşındayken 1000 lira transfer parası alarak Camialtı Spor Kulübü'nde amatör futbolcu oldu. Daha sonra İETT Spor Kulübü'nün amatör futbolcusu oldu. 1976 yılında, İETT şampiyon olduğunda, Erdoğan da bu takımda oynamaktaydı. Erokspor Kulübünde de futbola devam etti ve 16 yıllık futbol yaşamını 12 Eylül 1980 Askeri Darbesi sonrasında bıraktı ve daha fazla siyasi faaliyet..." };

            for (int i = 0; i < asciiStrings.Length; i++) {
			Console.WriteLine("Test "
					+ (i + 1)
					+ " : "
					+ new Deasciifier(asciiStrings[i]).ConvertToTurkish()
							.Equals(turkishStrings[i]));
		}
            Console.ReadKey();
        }
    }

    public class Deasciifier
    {
        private string asciistring;
        private string turkishstring;

        private static readonly string[] uppercaseLetters = { "A", "B", "C", "D", "E", "F", "G",
                "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                "U", "V", "W", "X", "Y", "Z" };

        private static CultureInfo CultureUS = new CultureInfo("en-US");

        private static Dictionary<string, Dictionary<string, int>> turkishPatternTable = null;

        private static readonly int turkishContextSize = 10;

        private static readonly Dictionary<string, string> turkishAsciifyTable = new Dictionary<string, string>();

        private static readonly Dictionary<string, string> turkishDowncaseAsciifyTable = new Dictionary<string, string>();

        private static readonly Dictionary<string, string> turkishUpcaseAccentsTable = new Dictionary<string, string>();

        private static readonly Dictionary<string, string> turkishToggleAccentTable = new Dictionary<string, string>();

        static Deasciifier()
        {
            turkishAsciifyTable.Add("ç", "c");
            turkishAsciifyTable.Add("Ç", "C");
            turkishAsciifyTable.Add("ğ", "g");
            turkishAsciifyTable.Add("Ğ", "G");
            turkishAsciifyTable.Add("ö", "o");
            turkishAsciifyTable.Add("Ö", "O");
            turkishAsciifyTable.Add("ı", "i");
            turkishAsciifyTable.Add("İ", "I");
            turkishAsciifyTable.Add("ş", "s");
            turkishAsciifyTable.Add("Ş", "S");

            foreach (string c in uppercaseLetters)
            {
                turkishDowncaseAsciifyTable.Add(c, c.ToLower(CultureUS));
                turkishDowncaseAsciifyTable.Add(c.ToLower(CultureUS), c.ToLower(CultureUS));
            }

            turkishDowncaseAsciifyTable.Add("ç", "c");
            turkishDowncaseAsciifyTable.Add("Ç", "c");
            turkishDowncaseAsciifyTable.Add("ğ", "g");
            turkishDowncaseAsciifyTable.Add("Ğ", "g");
            turkishDowncaseAsciifyTable.Add("ö", "o");
            turkishDowncaseAsciifyTable.Add("Ö", "o");
            turkishDowncaseAsciifyTable.Add("ı", "i");
            turkishDowncaseAsciifyTable.Add("İ", "i");
            turkishDowncaseAsciifyTable.Add("ş", "s");
            turkishDowncaseAsciifyTable.Add("Ş", "s");
            turkishDowncaseAsciifyTable.Add("ü", "u");
            turkishDowncaseAsciifyTable.Add("Ü", "u");

            foreach (string c in uppercaseLetters)
            {
                turkishUpcaseAccentsTable.Add(c, c.ToLower(CultureUS));
                turkishUpcaseAccentsTable.Add(c.ToLower(CultureUS), c.ToLower(CultureUS));
            }

            turkishUpcaseAccentsTable.Add("ç", "C");
            turkishUpcaseAccentsTable.Add("Ç", "C");
            turkishUpcaseAccentsTable.Add("ğ", "G");
            turkishUpcaseAccentsTable.Add("Ğ", "G");
            turkishUpcaseAccentsTable.Add("ö", "O");
            turkishUpcaseAccentsTable.Add("Ö", "O");
            turkishUpcaseAccentsTable.Add("ı", "I");
            turkishUpcaseAccentsTable.Add("İ", "i");
            turkishUpcaseAccentsTable.Add("ş", "S");
            turkishUpcaseAccentsTable.Add("Ş", "S");
            turkishUpcaseAccentsTable.Add("ü", "U");
            turkishUpcaseAccentsTable.Add("Ü", "U");

            turkishToggleAccentTable.Add("c", "ç"); // initial direction
            turkishToggleAccentTable.Add("C", "Ç");
            turkishToggleAccentTable.Add("g", "ğ");
            turkishToggleAccentTable.Add("G", "Ğ");
            turkishToggleAccentTable.Add("o", "ö");
            turkishToggleAccentTable.Add("O", "Ö");
            turkishToggleAccentTable.Add("u", "ü");
            turkishToggleAccentTable.Add("U", "Ü");
            turkishToggleAccentTable.Add("i", "ı");
            turkishToggleAccentTable.Add("I", "İ");
            turkishToggleAccentTable.Add("s", "ş");
            turkishToggleAccentTable.Add("S", "Ş");
            turkishToggleAccentTable.Add("ç", "c"); // other direction
            turkishToggleAccentTable.Add("Ç", "C");
            turkishToggleAccentTable.Add("ğ", "g");
            turkishToggleAccentTable.Add("Ğ", "G");
            turkishToggleAccentTable.Add("ö", "o");
            turkishToggleAccentTable.Add("Ö", "O");
            turkishToggleAccentTable.Add("ü", "u");
            turkishToggleAccentTable.Add("Ü", "U");
            turkishToggleAccentTable.Add("ı", "i");
            turkishToggleAccentTable.Add("İ", "I");
            turkishToggleAccentTable.Add("ş", "s");
            turkishToggleAccentTable.Add("Ş", "S");
        }

        public Deasciifier()
        {
            LoadPatternTable();
        }

        public void setAsciistring(string asciistring)
        {
            this.asciistring = asciistring;
            this.turkishstring = asciistring;
        }

        public Deasciifier(string asciistring)
            : this()
        {
            this.asciistring = asciistring;
            this.turkishstring = asciistring;
        }

        private static string SetCharAt(string mystr, int pos, string c)
        {
            return string.Concat(mystr.Substring(0, pos), c, mystr.Substring(pos + 1, mystr.Length - pos - 1));
        }

        private string TurkishToggleAccent(string c)
        {
            string result = turkishToggleAccentTable[c];
            return (result == null) ? c : result;
        }

        private static string Repeatstring(string haystack, int times)
        {
            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < times; i++)
                tmp.Append(haystack);

            return tmp.ToString();
        }

        private bool TurkishMatchPattern(Dictionary<string, int> dlist, int point)
        {
            int rank = dlist.Count() * 2;
            string str = TurkishGetContext(turkishContextSize, point);

            int start = 0;
            int end = 0;
            int _len = str.Length;

            while (start <= turkishContextSize)
            {
                end = turkishContextSize + 1;

                while (end <= _len)
                {
                    string s = str.Substring(start, end - start);

                    int? r = null;
                    if (dlist.ContainsKey(s))
                        r = dlist[s];

                    if (r != null && r.HasValue && Math.Abs(r.Value) < Math.Abs(rank))
                    {
                        rank = r.Value;
                    }

                    end++;
                }
                start++;
            }
            return rank > 0;
        }

        private bool TurkishMatchPattern(Dictionary<string, int> dlist)
        {
            return TurkishMatchPattern(dlist, 0);
        }

        //TODO: return char instead of string
        private static string CharAt(string source, int index)
        {
            return source[index].ToString();
        }

        private string TurkishGetContext(int size, int point)
        {
            string s = Repeatstring(" ", (1 + (2 * size)));
            s = SetCharAt(s, size, "X");

            int i = size + 1;
            bool space = false;
            int index = point;
            index++;

            string currentChar;

            while (i < s.Length && !space && index < asciistring.Length)
            {
                currentChar = CharAt(turkishstring, index);

                if (turkishDowncaseAsciifyTable.ContainsKey(currentChar))
                {
                    string x = turkishDowncaseAsciifyTable[currentChar];

                    s = SetCharAt(s, i, x);
                    i++;
                    space = false;
                }
                else
                {
                    if (!space)
                    {
                        i++;
                        space = true;
                    }
                }

                index++;
            }

            s = s.Substring(0, i);

            index = point;
            i = size - 1;
            space = false;

            index--;

            while (i >= 0 && index >= 0)
            {
                currentChar = CharAt(turkishstring, index);

                if (turkishUpcaseAccentsTable.ContainsKey(currentChar))
                {
                    string x = turkishUpcaseAccentsTable[currentChar];

                    s = SetCharAt(s, i, x);
                    i--;
                    space = false;
                }
                else
                {
                    if (!space)
                    {
                        i--;
                        space = true;
                    }
                }


                index--;
            }

            return s;
        }

        private bool TurkishNeedCorrection(string c, int point)
        {
            string ch = c;

            string tr;
            if (turkishAsciifyTable.ContainsKey(ch))
            {
                tr = turkishAsciifyTable[ch];

            }
            else
            {
                tr = ch;
            }


            Dictionary<string, int> pl;
            bool m = false;

            string loweredTR = tr.ToLower(CultureUS);
            if (turkishPatternTable.ContainsKey(loweredTR))
            {
                pl = turkishPatternTable[loweredTR];
                m = TurkishMatchPattern(pl, point);
            }

            if (tr.Equals("I"))
            {
                if (ch.Equals(tr))
                {
                    return !m;
                }
                else
                {
                    return m;
                }
            }
            else
            {
                if (ch.Equals(tr))
                {
                    return m;
                }
                else
                {
                    return !m;
                }
            }
        }

        public bool IsTurkishNeedCorrection(string c)
        {
            return TurkishNeedCorrection(c, 0);
        }

        /// <summary>
        /// Convert a string with ASCII-only letters into one with Turkish letters.
        /// </summary>
        /// <returns></returns>
        public string ConvertToTurkish()
        {
            for (int i = 0; i < turkishstring.Length; i++)
            {
                string c = CharAt(turkishstring, i);

                if (TurkishNeedCorrection(c, i))
                {
                    turkishstring = SetCharAt(turkishstring, i,
                            TurkishToggleAccent(c));
                }
                else
                {
                    turkishstring = SetCharAt(turkishstring, i, c);
                }
            }
            return turkishstring;
        }

        private string TurkishGetContext()
        {
            return TurkishGetContext(turkishContextSize, 0);
        }

        private void LoadPatternTable()
        {
            if (turkishPatternTable != null) return;

            XDocument xDoc = XDocument.Load("patterns.xml");
            var nodes = xDoc.Descendants("character");

            Dictionary<string, Dictionary<string, int>> _dictionary = new Dictionary<string, Dictionary<string, int>>();

            foreach (var character in nodes)
            {
                var childDictionary = new Dictionary<string, int>();
                foreach (var item in character.Descendants("pattern"))
                {
                    childDictionary.Add(item.FirstAttribute.Value, int.Parse(item.LastAttribute.Value));
                }

                _dictionary.Add(character.FirstAttribute.Value, childDictionary);
            }

            turkishPatternTable = _dictionary;
        }


    }
}

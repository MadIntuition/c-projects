using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDES
{
    class Program
    {
        public static readonly int[] IP ={ 58, 50, 42, 34, 26, 18, 10, 2, 60, 52,
                                           44, 36, 28, 20, 12, 4, 62, 54, 46, 38,
                                           30, 22, 14, 6, 64, 56, 48, 40, 32, 24,
                                           16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59,
                                           51, 43, 35, 27, 19, 11, 3, 61, 53, 45,
                                           37, 29, 21, 13, 5, 63, 55, 47, 39, 31,
                                           23, 15, 7 };
        public static readonly int[] revIP ={ 40, 8, 48, 16, 56, 24, 64, 32, 39, 7,
                                            47, 15, 55, 23, 63, 31, 38, 6, 46, 14,
                                            54, 22, 62, 30, 37, 5, 45, 13, 53, 21,
                                            61, 29, 36, 4, 44, 12, 52, 20, 60, 28,
                                            35, 3, 43, 11, 51, 19, 59, 27, 34, 2,
                                            42, 10, 50, 18, 58, 26, 33, 1, 41, 9,
                                            49, 17, 57, 25 };
        public static readonly int[,] s1 = { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
         { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
         { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
         { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} };


        public static readonly int[,] s2 = { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
         { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
         { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
         { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9} };

        public static readonly int[,] s3 = { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
         { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
         { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
         { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} };

        public static readonly int[,] s4 = { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
         { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
         {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
         {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}};

        public static readonly int[,] s5 = { {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
         { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
         {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14} ,
         {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}};

        public static readonly int[,] s6 = { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
         { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
         {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
         {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}};

        public static readonly int[,] s7 = { {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
         { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
         { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
         { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12} };

        public static readonly int[,] s8 = { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
         { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
		 {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
		 {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}};

        public static readonly int[] pc_e ={ 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8,
                                          9, 10, 11, 12, 13, 12, 13, 14, 15, 16,
                                          17, 16, 17, 18, 19, 20, 21, 20, 21, 22,
                                          23, 24, 25, 24, 25, 26, 27, 28, 29, 28,
                                          29, 30, 31, 32, 1 };
        public static readonly int[] pc_p ={ 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23,
                                          26, 5, 18, 31, 10, 2, 8, 24, 14, 32,
                                          27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };

        public static readonly int[] ShiftsLen ={ 0, 1, 1, 2, 2, 2, 2, 2, 2, 1,
                                                2, 2, 2, 2, 2, 2, 1 };

        public static readonly int[] compressBox1 ={ 57, 49, 41, 33, 25, 17, 9, 1, 58, 50,
                                          42, 34, 26, 18, 10, 2, 59, 51, 43, 35,
                                          27, 19, 11, 3, 60, 52, 44, 36, 63, 55,
                                          47, 39, 31, 23, 15, 7, 62, 54, 46, 38,
                                          30, 22, 14, 6, 61, 53, 45, 37, 29, 21,
                                          13, 5, 28, 20, 12, 4 };
        public static readonly int[] compressBox2 ={ 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21,
                                          10, 23, 19, 12, 4, 26, 8, 16, 7, 27,
                                          20, 13, 2, 41, 52, 31, 37, 47, 55, 30,
                                          40, 51, 45, 33, 48, 44, 49, 39, 56, 34,
                                          53, 46, 42, 50, 36, 29, 32 };

        public static List<int[,]> SBOXS = new List<int[,]>();
        public class Keys
        {
            public string[] Ci = new string[17];
            public string[] Di = new string[17];
            public string[] Ki = new string[16];
        }

        static void Main(string[] args)
        {
            SBOXS.Add(s1);
            SBOXS.Add(s2);
            SBOXS.Add(s3);
            SBOXS.Add(s4);
            SBOXS.Add(s5);
            SBOXS.Add(s6);
            SBOXS.Add(s7);
            SBOXS.Add(s8);
            //string text = "athggggavfgdgda";
            //string text = "0000000000000010000000000000000000000000000000000000000000000001";
            string text = File.ReadAllText("A:\\111.txt");
            string key1 = "01010000010100000101000001010000010100000101000001010000";
            string key2 = "01010000010100100101000001010000010100000101000001010000";
            string key3 = "01010000010101110101000001010000010100000101000001010000";
            //key1 = ExtKey(key1);
            Console.WriteLine(text);
            
            string code = Encryption3DES(text, key1, key2, key3, false);
            //Console.WriteLine("binary code:"+code);
            //Console.WriteLine(BinaryToText(Decryption3DES(BinaryToText(code), key1,key2, key3, false)));
            File.AppendAllText("A:\\222.txt", BinaryToText(code));
            File.AppendAllText("A:\\333.txt", BinaryToText(Decryption3DES(BinaryToText(code), key1, key2, key3, false)));
        }
        static public string ExtKey(string key)  //расширение ключа до 64 бит (на каждую 8 позицию добавляем бит четности)
        {
            StringBuilder Result = new StringBuilder("");
            int parity=0;
            for (int i=0, j=1; i<key.Length;i++,j++)
            {
                Result.Append(key[i]);
                parity += int.Parse(key[i].ToString());
                if (j==7)
                {
                    if (parity % 2 == 0) Result.Append("1");
                    else Result.Append("0");
                    parity = 0;j = 0;
                }
            }
            return Result.ToString();
        }
        static public string Encryption3DES(string text, string key1, string key2, string key3, bool IsTextBinary)
        {
            return EncryptionDES(EncryptionDES(EncryptionDES(text, key3, IsTextBinary),key2, true),key1,true);
        }
        static public string Decryption3DES(string text, string key1, string key2, string key3, bool IsTextBinary)
        {
            return DecryptionDES(DecryptionDES(DecryptionDES(text, key1, IsTextBinary),key2, true), key3, true);
        }

        static public string EncryptionDES(string text, string key, bool IsTextBinary)
        {
            string key_plus = Permuting(ExtKey(key), compressBox1); //расширяем ключ до 64 бит, потом сжимаем перестановкой по таблице
            Console.WriteLine("binary ext key=" + key_plus);
            //получаем Co и Do делением ключа на 2 блока
            string C0 = GetLeftKeyPart(key_plus);
            string D0 = GetRightKeyPart(key_plus);

            Keys keys = GenerateKeys(C0, D0);
            
            //string hex_text = this.FromTextToHex(text);
            string binaryText = "";

            if (IsTextBinary == false) binaryText = TextToBinary(text);
            else binaryText = text;

            binaryText = ExtensionTo64bitBlocks(binaryText);
            
            StringBuilder EncryptedTextBuilder = new StringBuilder(binaryText.Length);
            //кодирование блоков
            for (int i = 0; i < (binaryText.Length / 64); i++)
            {
                string PermutatedText = Permuting(binaryText.Substring(i * 64, 64), IP); //начальная перестановка
                
                string L0 = GetLeftKeyPart(PermutatedText);
                string R0 = GetRightKeyPart(PermutatedText);

                string FinalText = FinalEncription(L0, R0, keys, false);

                EncryptedTextBuilder.Append(FinalText);
            }
            return EncryptedTextBuilder.ToString();
        }

        static public string DecryptionDES(string text, string key, bool IsTextBinary)
        {
            string key_plus = Permuting(ExtKey(key), compressBox1);

            string C0 = "", D0 = "";
            C0 = GetLeftKeyPart(key_plus);
            D0 = GetRightKeyPart(key_plus);

            Keys keys = GenerateKeys(C0, D0);
            string binaryText = "";

            if (IsTextBinary == false) binaryText = TextToBinary(text);
            else binaryText = text;
            binaryText = ExtensionTo64bitBlocks(binaryText);
            
            StringBuilder DecryptedTextBuilder = new StringBuilder(binaryText.Length);

            for (int i = 0; i < (binaryText.Length / 64); i++)
            {
                string PermutatedText = Permuting(binaryText.Substring(i * 64, 64), IP);

                string L0 = "", R0 = "";

                L0 = GetLeftKeyPart(PermutatedText);
                R0 = GetRightKeyPart(PermutatedText);

                string FinalText = FinalEncription(L0, R0, keys, true);

                if ((i * 64 + 64) == binaryText.Length)
                {
                    StringBuilder last_text = new StringBuilder(FinalText.TrimEnd('0'));

                    int count = FinalText.Length - last_text.Length;

                    if ((count % 8) != 0)
                        count = 8 - (count % 8);
                    string append_text = "";

                    for (int k = 0; k < count; k++)
                        append_text += "0";
                    DecryptedTextBuilder.Append(last_text.ToString() + append_text);
                }
                else DecryptedTextBuilder.Append(FinalText);
            }

            return DecryptedTextBuilder.ToString();//.TrimEnd('0');
        }
        
        static public string ExtensionTo64bitBlocks(string text)
        {
            if ((text.Length % 64) != 0)
            {
                int maxLength = ((text.Length / 64) + 1) * 64;
                text = text.PadRight(maxLength, '0');
            }
            return text;
        }
        
        static public string Permuting(string text, int[] table)          //перестановка в соответствии с таблицей перестановок
        {
            StringBuilder Result = new StringBuilder(table.Length);
            for (int i = 0; i < table.Length; i++)
                Result.Append(text[table[i] - 1]);
            return Result.ToString();
        }
        
        static public string VectorFromSbox(string text, int[,] table) //нахождение вектора через s-box
        {
            int row = Convert.ToInt32(text[0].ToString() + text[text.Length - 1].ToString(), 2); //получаем номер строки из 1-ого и ласт битов
            int col = Convert.ToInt32(text.Substring(1, 4), 2);           //из 2..5 номер столбца
            return DecimallToBinary(table[row, col]);          //возвращаем ответ
        }

        static public string GetLeftKeyPart(string text)      //получить левую часть
        {
            return text.Substring(0, (text.Length / 2));
        }
        static public string GetRightKeyPart(string text)        //получить правую часть
        {
            return text.Substring((text.Length / 2));
        }

        static public string LeftShift(string text, int count)    //сдвиг влево
        {
            string temp = text.Substring(0, count);
            StringBuilder shifted = new StringBuilder(text.Length);
            shifted.Append(text.Substring(count) + temp);
            return shifted.ToString();
        }
        static public Keys GenerateKeys(string C0, string D0)                //Генерация Ci и Di путём циклического сдвига и раундовых ключа на их основе
        {
            Keys keys = new Keys();
            keys.Ci[0] = C0;
            keys.Di[0] = D0;
            for (int i = 1; i < keys.Ci.Length; i++)
            {
                keys.Ci[i] = LeftShift(keys.Ci[i - 1],ShiftsLen[i]);
                keys.Di[i] = LeftShift(keys.Di[i - 1], ShiftsLen[i]);
                keys.Ki[i - 1] = Permuting(keys.Ci[i] + keys.Di[i], compressBox2);          //получение раундового ключа
            }
            return keys;
        }

        static public string FinalEncription(string L0, string R0, Keys keys, bool IsReverse)
        {
            string Li = "", Ri = "", Ln_1 = L0, Rn_1 = R0;
            int i = 0;
            if (IsReverse == true) i = 15;

            while (CounterIndicator(i, IsReverse))
            {
                Li = Rn_1;
                Ri = XOR(Ln_1, f(Rn_1, keys.Ki[i]));
                Ln_1 = Li;
                Rn_1 = Ri;
                if (IsReverse == false) i ++;
                else i--;
            }

            string R16L16 = Ri + Li;
            return Permuting(R16L16, revIP);      //возвращаем прогон через конечную перестановку
        }
        static public bool CounterIndicator(int i, bool IsReverse)
        {
            return (IsReverse == false) ? i < 16 : i >= 0;
        } //индикатор счетчика
        static public string f(string Ri_1, string Ki) //DES-функция
        {
            return P(sBox_Transform(XOR(ExtensionByPbox(Ri_1), Ki))); //расширяем до 48 бит, ксорим с ключом раунда и прогоняем через S-боксы
        }  
        static public string P(string text)              //прямой P-бокс
        {
            return Permuting(text, pc_p);
        }
        static public string sBox_Transform(string text)       //модификация S-боксом
        {
            StringBuilder SboxResult = new StringBuilder(32);
            string temp;
            for (int i = 0; i < 8; i++)          //по очереди заменяем 6-битовый вектор на 4-х битовый
            {
                temp = text.Substring(i * 6, 6);
                SboxResult.Append(VectorFromSbox(temp, SBOXS[i]));
            }
            return SboxResult.ToString();
        }   
        static public string ExtensionByPbox(string Rn_1)              //Прогон через P-box расширения
        {
            return Permuting(Rn_1, pc_e);
        }
        static public string XOR(string block1, string block2) //XOR двух блоков
        {
            StringBuilder XORResult = new StringBuilder(block1.Length);
            for (int i = 0; i < block1.Length; i++)                             
                if (block1[i] == block2[i]) XORResult.Append("0");           //складываем ро модулю 2
                else XORResult.Append("1");
            return XORResult.ToString();
        }
        
        static public string BinaryToText(string binarystring)
        {
            StringBuilder text = new StringBuilder(binarystring.Length / 8);

            for (int i = 0; i < (binarystring.Length / 8); i++)
            {
                string word = binarystring.Substring(i * 8, 8);
                text.Append((char)Convert.ToInt32(word, 2));
            }
            return text.ToString();
        }
        static public string TextToBinary(string text)
        {
            StringBuilder binarystring = new StringBuilder(text.Length * 8);

            foreach (char word in text)
            {
                int binary = (int)word;
                int factor = 128;

                for (int i = 0; i < 8; i++)
                {
                    if (binary >= factor)
                    {
                        binary -= factor;
                        binarystring.Append("1");
                    }
                    else binarystring.Append("0");
                    factor /= 2;
                }
            }
            return binarystring.ToString();
        }
        static public string DecimallToBinary(int binary)
        {
            string binarystring = "";
            int factor = 128;

            for (int i = 0; i < 8; i++)
            {
                if (binary >= factor)
                {
                    binary -= factor;
                    binarystring += "1";
                }
                else binarystring += "0";
                factor /= 2;
            }
            return binarystring;
        }
    }
}

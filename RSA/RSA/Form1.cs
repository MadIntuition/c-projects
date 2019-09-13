using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class Form1 : Form
    {
        public static Random rand = new Random();
        BigInteger[] keys;
        public BigInteger ExtendedEvklidAlgorithm(BigInteger A, BigInteger B)       // A это p, B - для чего ищем обратный элемент
        {
            BigInteger prevA = A, prevB = B, AmodB = A % B;
            List<BigInteger> arrayAdivB = new List<BigInteger>();
            arrayAdivB.Add(A / B);
            int i = 0;
            for (; AmodB != 0; i++)
            {
                prevA = prevB;
                prevB = AmodB;
                AmodB = prevA % prevB;
                arrayAdivB.Add(prevA / prevB);
            }
            BigInteger Xi = 0, Yi = 1, temp;
            for (i = i - 1; i >= 0; i--)
            {
                temp = Xi;
                Xi = Yi;
                Yi = temp - Yi * (arrayAdivB[i]);
            }
            if (Yi < 0) Yi += A;
            return Yi;
        }

        public bool MillerRabinTest(BigInteger n, int c)
        {
            if (n % 2 == 0)
                return false;
            BigInteger step = n - 1, s = 0, d;
            while (step % 2 == 0)
            {
                step /= 2;
                s++;
            }
            d = step;
            // MessageBox.Show(s.ToString()+" "+d.ToString());
            BigInteger[] k = new BigInteger[c];
            for (int i = 0; i < c; i++)
            {
                int x = rand.Next(2, c + 3);
                while (k.Contains(x))
                {
                    x = rand.Next(2, c + 3);
                }
                k[i] = x;
                bool flajok = false;
                if (BigInteger.ModPow(x, d, n) == 1 || BigInteger.ModPow(x, d, n) % n == n - 1) return true;
                else
                {
                    for (int r = 1; r < s; r++)
                    {
                        if (BigInteger.ModPow(x, (int)Math.Pow(2, r) * d, n) % n == n - 1)
                        {
                            flajok = true;
                            break;
                        }
                    }
                }
                if (flajok == false) return false;
            }
            return true;
        }
        
        public BigInteger Generation(int p)
        {
            //int R = rand.Next(p, 4 * p + 3);
            //BigInteger n = (BigInteger)p * (BigInteger)R + 1;
            BigInteger R;
            p /= 8;
            do
            {
                //R = rand.Next(p, 4 * p + 3);
                byte[] bytes = new byte[p];
                rand.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F;
                R = new BigInteger(bytes);
                //n = (BigInteger)p * (BigInteger)R + 1;
            } while (!MillerRabinTest(R, 20));

            return R;
        }

        public static BigInteger RandomIntegerBelow(BigInteger N)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;
            do
            {
                rand.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; 
                R = new BigInteger(bytes);
            } while (R >= N||R<2);

            return R;
        }

        public BigInteger[] GenerationKeys(int basis)
        {
            BigInteger p, q, e;
            do
            {
                p = Generation(basis);
                q = Generation(basis);
            } while (p == q);
            BigInteger n = p * q;//открый ключ
            BigInteger EilerFunc = (p - 1) * (q - 1);
            do
            {
                e = RandomIntegerBelow(n); //открый ключ
            } while (BigInteger.GreatestCommonDivisor(EilerFunc, e) != 1) ;

            BigInteger d = ExtendedEvklidAlgorithm(EilerFunc, e); //закрытыйключ
            BigInteger[] result = { e, n, d};
            richTextBox3.Text = "p=" + p.ToString() + "\nq=" + q.ToString() + "\nn=" + n.ToString() + "\nEilerFunc=" + EilerFunc.ToString() + "\ne=" + e.ToString() + "\nd=" + d.ToString();
            return result; 
        }
        public string RSA(string text, BigInteger e, BigInteger n)
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(text);
            BigInteger[] shifr = new BigInteger[utf8.Length];
            StringBuilder ciptext = new StringBuilder("");
            for (int i = 0; i < utf8.Length; i++)
            {
                shifr[i] = BigInteger.ModPow((int)utf8[i], e , n);
                //utf8[i] = (byte)((utf8[i] + randombytes[i]) % 256);
                ciptext.Append(shifr[i]);
                ciptext.Append(" ");
            }
            return ciptext.ToString();
        }

        public string encryptRSA (string text, BigInteger d, BigInteger n)
        {
            StringBuilder encrtext = new StringBuilder("");
            string[] strArr = null;
            char[] splitchar = {' '};
            strArr = text.Split(splitchar);
            byte[] str = new byte[strArr.Length];

            for (int i = 0; i < strArr.Length-1 ; i++)
            {
                BigInteger code = BigInteger.ModPow(BigInteger.Parse(strArr[i]), d, n);
                str[i] = (byte)(code);

            }
            encrtext.Append(Encoding.UTF8.GetString(str));
            return encrtext.ToString();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keys = GenerationKeys(int.Parse(textBox1.Text));
            richTextBox2.Text = RSA(richTextBox1.Text, keys[0], keys[1]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = encryptRSA(richTextBox1.Text, keys[2], keys[1]);
        }
    }
}

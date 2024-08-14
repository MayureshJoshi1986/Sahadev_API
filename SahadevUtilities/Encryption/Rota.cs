using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahadevUtilities.Encryption
{
    public class Rota
    {
        public enum KeyType
        {
            HW, SW
        }

        static int[] keyCH = { 7, 11, 8, 13, 12, 6, 4, 9, 5, 15, 17, 19, 16, 2, 14, 18, 3, 10, 20, 1 };
        static int[] keyVH = { 6, 1, 5, 2, 4, 3 };
        static int[] keyNH = { 10, 8, 6, 1, 7, 5, 3, 4, 9, 2 };
        static string consonantsH = "bcdfghjklnoprstvwxyz";
        static string vowelsH = "aeimqu";
        static int[] keyCS = { 7, 11, 8, 13, 12, 6, 4, 9, 5, 15, 17, 19, 16, 2, 14, 18, 3, 10, 20, 1 };
        static int[] keyVS = { 6, 1, 5, 2, 4, 3 };
        static int[] keyNS = { 5, 4, 3, 2, 9, 10, 6, 8, 7, 4 };
        static string consonantsS = "abcefgijkmnoqrsuvwyz";
        static string vowelsS = "dhlptx";

        /// <summary>
        /// This method encrpyt the given string
        /// </summary>
        /// <param name="s">string input</param>
        /// <param name="type">Keytype HW:Hardware| SW: License </param>
        /// <returns></returns>
        public static string Encrypt(string s, KeyType type)
        {
            if (String.IsNullOrEmpty(s)) return s;

            int lenC = (s.Length - 1) % 20;
            int lenV = (s.Length - 1) % 6;
            int lenN = (s.Length - 1) % 10;
            int[] keyC = new int[] { };
            int[] keyV = new int[] { };
            int[] keyN = new int[] { };
            string consonants = string.Empty;
            string vowels = string.Empty;
            if (KeyType.HW == type)
            {
                keyC = keyCH;
                keyV = keyVH;
                keyN = keyNH;
                consonants = consonantsH;
                vowels = vowelsH;
            }
            else if (KeyType.SW == type)
            {
                keyC = keyCS;
                keyV = keyVS;
                keyN = keyNS;
                consonants = consonantsS;
                vowels = vowelsS;
            }
            else
            {
                return string.Empty;
            }
            char[] ca = new char[s.Length];



            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                int j;

                if ((j = vowels.IndexOf(c)) > -1)
                {
                    j += keyV[(i + lenV) % 6];
                    if (j > 5) j -= 6;
                    ca[i] = vowels[j];
                }
                else if ((j = consonants.IndexOf(c)) > -1)
                {
                    j += keyC[(i + lenC) % 20];
                    if (j > 19) j -= 20;
                    ca[i] = consonants[j];
                }
                else if ((j = vowels.ToUpper().IndexOf(c)) > -1)
                {
                    j += keyV[(i + lenV) % 6];
                    if (j > 5) j -= 6;
                    ca[i] = vowels.ToUpper()[j];
                }
                else if ((j = consonants.ToUpper().IndexOf(c)) > -1)
                {
                    j += keyC[(i + lenC) % 20];
                    if (j > 19) j -= 20;
                    ca[i] = consonants.ToUpper()[j];
                }
                else if (c >= 48 && c <= 57)
                {
                    j = c + keyN[(i + lenN) % 10];
                    if (j > 57) j -= 10;
                    ca[i] = (char)j;
                }
                else
                {
                    ca[i] = c;
                }
            }

            return new string(ca);
        }

        /// <summary>
        /// This method decrypts the encrypted string
        /// </summary>
        /// <param name="s">string input</param>
        /// <param name="type">Keytype HW:Hardware| SW: License </param>
        /// <returns></returns>
        public static string Decrypt(string s, KeyType type)
        {
            if (String.IsNullOrEmpty(s)) return s;

            int lenC = (s.Length - 1) % 20;
            int lenV = (s.Length - 1) % 6;
            int lenN = (s.Length - 1) % 10;
            int[] keyC = new int[] { };
            int[] keyV = new int[] { };
            int[] keyN = new int[] { };
            string consonants = string.Empty;
            string vowels = string.Empty;
            if (KeyType.HW == type)
            {
                keyC = keyCH;
                keyV = keyVH;
                keyN = keyNH;
                consonants = consonantsH;
                vowels = vowelsH;
            }
            else if (KeyType.SW == type)
            {
                keyC = keyCS;
                keyV = keyVS;
                keyN = keyNS;
                consonants = consonantsS;
                vowels = vowelsS;
            }
            else
            {
                return string.Empty;
            }

            char[] ca = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                int j;
                if ((j = vowels.IndexOf(c)) > -1)
                {
                    j -= keyV[(i + lenV) % 6];
                    if (j < 0) j += 6;
                    ca[i] = vowels[j];
                }
                else if ((j = consonants.IndexOf(c)) > -1)
                {
                    j -= keyC[(i + lenC) % 20];
                    if (j < 0) j += 20;
                    ca[i] = consonants[j];
                }
                else if ((j = vowels.ToUpper().IndexOf(c)) > -1)
                {
                    j -= keyV[(i + lenV) % 6];
                    if (j < 0) j += 6;
                    ca[i] = vowels.ToUpper()[j];
                }
                else if ((j = consonants.ToUpper().IndexOf(c)) > -1)
                {
                    j -= keyC[(i + lenC) % 20];
                    if (j < 0) j += 20;
                    ca[i] = consonants.ToUpper()[j];
                }
                else if (c >= 48 && c <= 57)
                {
                    j = c - keyN[(i + lenN) % 10];
                    if (j < 48) j += 10;
                    ca[i] = (char)j;
                }
                else
                {
                    ca[i] = c;
                }
            }
            return new string(ca);
        }
    }
}

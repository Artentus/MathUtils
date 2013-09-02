using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Globalization;
using System.Runtime.InteropServices;

//BigFloat-Klasse von ~blaze~
namespace Test
{
    public struct BigFloat : IEquatable<BigFloat>, IComparable<BigFloat>
    {
        private BigInteger _mantissa;
        private int _exponent;

        private BigFloat(BigInteger mantissa, int exponent)
        {
            if (mantissa.IsZero)
            {
                _mantissa = BigInteger.Zero;
                _exponent = 0;
                return;
            }
            else
            {
                BigInteger comparand = ((BigInteger)1 >> (8 * exponent));
                int sign = mantissa.Sign; //Vorzeichen ignorieren(?)
                mantissa = BigInteger.Abs(mantissa);

                while ((byte)(mantissa & 0xff) == 0) //ueberhaengende 0-en entfernen
                {
                    mantissa >>= 8;
                    exponent--;
                }
                _mantissa = mantissa * sign;
                _exponent = exponent;
            }
        }

        #region comparison

        public override int GetHashCode()
        {
            return _exponent.GetHashCode() ^ _mantissa.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is BigFloat && Equals((BigFloat)obj);
        }

        public bool Equals(BigFloat other)
        {
            return this == other;
        }

        public int CompareTo(BigFloat other)
        {
            if (this == other)
                return 0;
            else
                return this < other ? 1 : -1;
        }

        #endregion

        #region string I/O
        public static bool TryParse(string input, out BigFloat value)
        {
            return TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
        }

        public static bool TryParse(string input, NumberStyles style, IFormatProvider formatProvider, out BigFloat value)
        {
            NumberFormatInfo nfi = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo));
            if (nfi == null)
                throw new ArgumentException("Unsupported format provider.");
            value = default(BigFloat);
            //TODO: implement using style
            int exponent;
            BigInteger mantissa; //Mantisse
            if (!GetData(input, nfi.NumberDecimalSeparator, out exponent, out mantissa))
                return false;
            value = new BigFloat(mantissa, exponent);
            return true;
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            NumberFormatInfo nfi = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo));
            //if (nfi == null)
            //    throw new ArgumentException("Unsupported format provider.");
            //StringBuilder l = new StringBuilder(), r = new StringBuilder();
            //char[] cbout;
            //BigFloat bfl = this, bfr;
            //string separator = string.Empty;
            //string sign = string.Empty;
            //int cdest = 0;

            //if (bfl._mantissa.Sign < 0)
            //{
            //    sign = nfi.NegativeSign;
            //    bfl = -bfl;
            //}
            //bfr = bfl % 1;
            //bfl -= bfr;

            //do
            //{
            //    //ggf. Redundanz eliminieren, da bfl % 10 auf bfl / 10 basiert
            //    l.Append((int)(bfl % 10));
            //    bfl /= 10;
            //} while (bfl > 0);

            //if (bfr != 0)
            //{
            //    separator = nfi.NumberDecimalSeparator;
            //    do
            //    {
            //        bfr /= 10;
            //        l.Append((int)(bfr % 10));
            //    } while (bfr != 0);
            //}
            //cbout = new char[l.Length + r.Length + separator.Length + sign.Length];

            //sign.CopyTo(0, cbout, cdest, sign.Length);
            //cdest += sign.Length;
            //l.CopyTo(0, cbout, cdest, l.Length);
            //Array.Reverse(cbout, cdest, l.Length);
            //cdest += l.Length;

            //separator.CopyTo(0, cbout, cdest, separator.Length);
            //cdest += separator.Length;
            //l.CopyTo(0, cbout, cdest, r.Length);
            //Array.Reverse(cbout, cdest, r.Length);
            //cdest += r.Length;
            //return new string(cbout);
            if (_exponent < 0)
                return (_mantissa << (_exponent * 8)).ToString() + nfi.NumberDecimalSeparator + (_mantissa >> (_exponent * 8)).ToString();
            else
                return (_mantissa << (_exponent * 8)).ToString();
        }

        private static bool GetData(string data, string separator, out int amount, out BigInteger value)
        {
            int ca = amount = 0;
            int digit, temp = 255, cfct = 1;
            BigInteger cdig = 1;
            bool sephit = false;

            value = BigInteger.Zero;

            if (data.Length == 0)
                return false;

            for (int i = data.Length - 1; i >= 0; i--)
            {
                char cp = data[i];
                if (cp < '0' || cp > '9')
                    if (sephit || IndEquals(data, separator, ref i))
                    {
                        sephit = true;
                        continue;
                    }
                    else
                        return false;
                digit = cp - '0';
                temp = temp + digit * cfct;
                if (temp >= 256) //stellenzahl um eins erhoehen, sobald 2 Bytes von temp besetzt sind
                {
                    temp >>= 8;
                    if (!sephit)
                        ca--;
                    cfct = 1; //das richtig so?
                }
                cfct *= 10;
                value = value + digit * cdig;
                cdig *= 10;
            }
            amount = ca;
            return true;
        }

        private static bool IndEquals(string left, string right, ref int leftIndex)
        {
            int cind = leftIndex;
            for (int cr = 0; cr < right.Length; cr++, cind++)
                if (left[cind] != right[cr])
                    return false;
            leftIndex = cind;
            return true;
        }
        #endregion

        #region operators

        public static BigFloat operator -(BigFloat value)
        {
            return new BigFloat(-value._mantissa, value._exponent);
        }

        public static BigFloat operator +(BigFloat value)
        {
            return value;
        }

        public static bool operator <(BigFloat left, BigFloat right)
        {
            //TODO: proof :P, war nur schnell niedergeschrieben, immer zuerst Exponenten, dann Mantisse vergleichen wegen Effizienz
            return !(left._exponent > right._exponent) || (left._exponent == right._exponent && left._mantissa < right._mantissa);
        }

        public static bool operator <=(BigFloat left, BigFloat right)
        {
            //TODO: schoener?
            return left < right || left == right;
        }

        public static bool operator ==(BigFloat left, BigFloat right)
        {
            return left._exponent == right._exponent && left._mantissa == right._mantissa;
        }

        public static bool operator !=(BigFloat left, BigFloat right)
        {
            return !(left == right);
        }

        public static bool operator >(BigFloat left, BigFloat right)
        {
            return !(left <= right);
        }

        public static bool operator >=(BigFloat left, BigFloat right)
        {
            return !(left < right);
        }

        public static BigFloat operator +(BigFloat left, BigFloat right)
        {
            if (left._exponent < right._exponent)
                return right + left;
            //k + l = (mk * 256^ek) + (ml * 256^el) = (mk + ml * 256^(el - ek)) * 256^ek

            int cv = 8 * (right._exponent - left._exponent);
            BigFloat b = new BigFloat(left._mantissa + (right._mantissa >> cv), left._exponent);
            return b;
        }

        public static BigFloat operator *(BigFloat left, BigFloat right)
        {
            return new BigFloat(left._mantissa * right._mantissa, left._exponent + right._exponent);
        }

        public static BigFloat operator /(BigFloat left, BigFloat right)
        {
            int exponent;
            return new BigFloat(DivUnit(left._mantissa, right._mantissa, 2048, out exponent), left._exponent - right._exponent - exponent);
        }

        public static BigFloat operator %(BigFloat left, BigFloat right)
        {
            //k % l = left - floor(left / right) * right
            BigFloat bf = left - Floor(left / right) * right;
            System.Diagnostics.Debug.WriteLine(left._mantissa.ToString() + "*2^" + left._exponent.ToString() + "%" + right._mantissa.ToString() + "*2^" + right._exponent.ToString() + "=" + bf._mantissa.ToString() + "*2^" + bf._exponent.ToString());
            return bf;
        }

        public static BigFloat operator -(BigFloat left, BigFloat right)
        {
            return left + -right;
        }

        public static implicit operator BigFloat(int value)
        {
            return new BigFloat(value, 0);
        }

        public static unsafe implicit operator BigFloat(float value)
        {
            int raw = *(int*)&value;
            return new BigFloat((raw & 0x7fffff) * (1 >> 31), (raw & 0x78000000) >> 23);
        }

        public static explicit operator int(BigFloat value)
        {
            return (int)(value._mantissa << value._exponent);
        }
        #endregion

        public static BigFloat Floor(BigFloat value)
        {
            int shift = value._exponent * 8;
            value = new BigFloat((value._mantissa << shift) >> shift, value._exponent);
            return value;
        }

        private static BigInteger DivUnit(BigInteger left, BigInteger right, int maxAccurancy, out int exponent)
        {
            int ca = 0;
            BigInteger result = left / right;
            left = (left - result * right) * right;

            while (ca < maxAccurancy && left.IsZero == false)
            {
                BigInteger divf = left / right;
                left = (left - divf * right) * right;
                result *= right;
                result += divf;

                ca++;
            }
            exponent = -ca;
            return result;
        }
    }
}
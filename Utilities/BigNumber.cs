using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics.PackedVector;

public struct BigNumber : IComparable<BigNumber>
{
    private static readonly string[] Suffixes =
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc"
    };

    // Between 1 (inclusive) and 10 (exclusiv), or 0 if value is zero
    public double Mantissa;
    // Power of 10
    public int Exponent;

    public static readonly BigNumber Zero = new BigNumber(0);

    public BigNumber(double value)
    {
        if (value == 0)
        {
            Mantissa = 0;
            Exponent = 0;
        }
        else
        {
            Exponent = 0;
            Mantissa = value;
            Normalize();
        }
    }

    public BigNumber(long value) : this((double)value)
    {

    }

    private void Normalize()
    {
        if (Mantissa == 0)
        {
            Exponent = 0;
            return;
        }

        while (Math.Abs(Mantissa) >= 10)
        {
            Mantissa /= 10;
            Exponent++;
        }

        while (Math.Abs(Mantissa) > 0 && Math.Abs(Mantissa) < 1)
        {
            Mantissa *= 10;
            Exponent--;
        }
    }

    // Addition
    public static BigNumber operator +(BigNumber a, BigNumber b)
    {
        if (a.Mantissa == 0)
        {
            return b;
        }

        if (b.Mantissa == 0)
        {
            return a;
        }

        // Align exponents
        if (a.Exponent == b.Exponent)
        {
            return new BigNumber(a.Mantissa + b.Mantissa) { Exponent = a.Exponent };
        }
        else if (a.Exponent > b.Exponent)
        {
            int diff = b.Exponent - a.Exponent;
            double adjustedMantissaA = a.Mantissa / Math.Pow(10, diff);

            return new BigNumber(b.Mantissa + adjustedMantissaA) { Exponent = a.Exponent };
        }
        else
        {
            int diff = b.Exponent - a.Exponent;
            double adjustedMantissaA = a.Mantissa / Math.Pow(10, diff);

            return new BigNumber(b.Mantissa + adjustedMantissaA) { Exponent = b.Exponent };
        }
    }

    // Multiplication
    public static BigNumber operator *(BigNumber a, BigNumber b)
    {
        if (a.Mantissa == 0 || b.Mantissa == 0)
        {
            return Zero;
        }

        return new BigNumber(a.Mantissa * b.Mantissa)
        {
            Exponent = a.Exponent + b.Exponent
        };
    }

    // CompareTo
    public int CompareTo(BigNumber other)
    {
        if (Exponent != other.Exponent)
        {
            return Exponent.CompareTo(other.Exponent);
        }

        return Mantissa.CompareTo(other.Mantissa);
    }

    public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
    public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;
    public static bool operator >=(BigNumber a, BigNumber b) => a.CompareTo(b) >= 0;
    public static bool operator <=(BigNumber a, BigNumber b) => a.CompareTo(b) <= 0;
    public static bool operator ==(BigNumber a, BigNumber b) => a.CompareTo(b) == 0;
    public static bool operator !=(BigNumber a, BigNumber b) => a.CompareTo(b) != 0;

    public override bool Equals(object obj)
    {
        if (obj is BigNumber other)
        {
            return this == other;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Mantissa.GetHashCode() ^ Exponent.GetHashCode();
    }

    // ToString() with Suffixes
    public override string ToString()
    {
        if (Mantissa == 0)
        {
            return "0";
        }

        int suffixIndex = Exponent / 3;

        if (suffixIndex < 0 || suffixIndex >= Suffixes.Length)
        {
            // For huse exponents beyond suffixes, fallback to scientific notation
            return $"{Mantissa:E2}";
        }

        double displayMantissa = Mantissa * Math.Pow(10, Exponent % 3);

        return $"{displayMantissa:F2}{Suffixes[suffixIndex]}";
    }

    // Convenience methods
    public static implicit operator BigNumber(double value) => new BigNumber(value);
    public static implicit operator BigNumber(long value) => new BigNumber(value);
}
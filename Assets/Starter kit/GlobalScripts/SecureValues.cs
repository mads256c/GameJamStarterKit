using System;

namespace GameJamStarterKit
{
    static class SecureRandom {public static Random random = new Random(); }
    
    public class SecureBool
    {
        private int _value;
        private int _randomValue;

        private bool Value
        {
            get
            {
                unchecked
                {
                    if (_value - _randomValue == 0)
                        return false;
                    else
                        return true;
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = SecureRandom.random.Next();
                    int i = value ? 1 : 0;
                    _value = i + _randomValue;
                }
            }
            
        }

        public SecureBool(bool value = false) { Value = value; }

        public static implicit operator SecureBool(bool value) { return new SecureBool(value); }

        public static implicit operator bool(SecureBool value) { return value.Value; }

        public static bool operator ==(SecureBool a, SecureBool b) { return a.Value == b.Value; }

        public static bool operator !=(SecureBool a, SecureBool b) { return a.Value != b.Value; }

        public static bool operator ! (SecureBool a) { return !a.Value; }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureBool).Value); }

    }

    public class SecureByte
    {
        private byte _value;
        private byte _randomValue;

        private byte Value
        {
            get
            {
                unchecked
                {
                    return (byte)(_value - _randomValue);
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = (byte)SecureRandom.random.Next();
                    _value = (byte)(value + _randomValue);
                }
            }
        }

        public SecureByte(byte value = 0) { Value = value; }

        public static implicit operator SecureByte(byte value) { return new SecureByte(value); }

        public static implicit operator byte(SecureByte value) { return value.Value; }

        public static bool operator ==(SecureByte a, SecureByte b) { return a.Value == b.Value; }

        public static bool operator !=(SecureByte a, SecureByte b) { return a.Value != b.Value; }

        public static SecureByte operator ++ (SecureByte a)
        {
            a.Value++;
            return a;
        }

        public static SecureByte operator -- (SecureByte a)
        {
            a.Value++;
            return a;
        }

        public static SecureByte operator +(SecureByte a, SecureByte b) { return new SecureByte((byte)(a.Value + b.Value)); }

        public static SecureByte operator -(SecureByte a, SecureByte b) { return new SecureByte((byte)(a.Value - b.Value)); }

        public static SecureByte operator *(SecureByte a, SecureByte b) { return new SecureByte((byte)(a.Value * b.Value)); }

        public static SecureByte operator /(SecureByte a, SecureByte b) { return new SecureByte((byte)(a.Value / b.Value)); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureByte).Value); }

    }

    public class SecureChar
    {
        private char _value;
        private char _randomValue;

        private char Value
        {
            get
            {
                unchecked
                {
                    return (char)(_value - _randomValue);
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = (char)SecureRandom.random.Next();
                    _value = (char)(value + _randomValue);
                }
            }
        }
        public SecureChar(char value = '\x0000') { Value = value; }

        public static implicit operator SecureChar(char value) { return new SecureChar(value); }

        public static implicit operator char(SecureChar value) { return value.Value; }

        public static bool operator ==(SecureChar a, SecureChar b) { return a.Value == b.Value; }

        public static bool operator !=(SecureChar a, SecureChar b) { return a.Value != b.Value; }

        public static SecureChar operator +(SecureChar a, SecureChar b) { return new SecureChar((char)(a.Value + b.Value)); }

        public static SecureChar operator -(SecureChar a, SecureChar b) { return new SecureChar((char)(a.Value - b.Value)); }

        public static SecureChar operator *(SecureChar a, SecureChar b) { return new SecureChar((char)(a.Value * b.Value)); }

        public static SecureChar operator /(SecureChar a, SecureChar b) { return new SecureChar((char)(a.Value / b.Value)); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureChar).Value); }


    }

    public class SecureDecimal
    {
        private decimal _value;
        private decimal _randomValue;

        private decimal Value
        {
            get
            {
                unchecked
                {
                    return _value - _randomValue;
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = SecureRandom.random.Next(-1024, 1024);
                    _value = value + _randomValue;
                }
            }
        }

        public SecureDecimal(decimal value = 0.0m) { Value = value; }

        public static implicit operator SecureDecimal(decimal value) { return new SecureDecimal(value); }

        public static implicit operator decimal(SecureDecimal value) { return value.Value; }

        public static bool operator ==(SecureDecimal a, SecureDecimal b) { return a.Value == b.Value; }

        public static bool operator !=(SecureDecimal a, SecureDecimal b) { return a.Value != b.Value; }

        public static SecureDecimal operator ++ (SecureDecimal a)
        {
            a.Value++;
            return a;
        }

        public static SecureDecimal operator -- (SecureDecimal a)
        {
            a.Value--;
            return a;
        }

        public static SecureDecimal operator +(SecureDecimal a, SecureDecimal b) { return new SecureDecimal(a.Value + b.Value); }

        public static SecureDecimal operator -(SecureDecimal a, SecureDecimal b) { return new SecureDecimal(a.Value - b.Value); }

        public static SecureDecimal operator *(SecureDecimal a, SecureDecimal b) { return new SecureDecimal(a.Value * b.Value); }

        public static SecureDecimal operator /(SecureDecimal a, SecureDecimal b) { return new SecureDecimal(a.Value / b.Value); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureDecimal).Value); }
    }

    public class SecureDouble
    {

        private double _value;
        private double _randomValue;

        private double Value
        {
            get
            {
                unchecked
                {
                    return _value - _randomValue;
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = SecureRandom.random.Next(-1024, 1024);
                    _value = value + _randomValue;
                }
            }
        }

        public SecureDouble(double value = 0.0d) { Value = value; }

        public static implicit operator SecureDouble(double value) { return new SecureDouble(value); }

        public static implicit operator double(SecureDouble value) { return value.Value; }

        public static bool operator ==(SecureDouble a, SecureDouble b) { return a.Value == b.Value; }

        public static bool operator !=(SecureDouble a, SecureDouble b) { return a.Value != b.Value; }

        public static SecureDouble operator ++(SecureDouble a)
        {
            a.Value++;
            return a;
        }

        public static SecureDouble operator --(SecureDouble a)
        {
            a.Value--;
            return a;
        }

        public static SecureDouble operator +(SecureDouble a, SecureDouble b) { return new SecureDouble(a.Value + b.Value); }

        public static SecureDouble operator -(SecureDouble a, SecureDouble b) { return new SecureDouble(a.Value - b.Value); }

        public static SecureDouble operator *(SecureDouble a, SecureDouble b) { return new SecureDouble(a.Value * b.Value); }

        public static SecureDouble operator /(SecureDouble a, SecureDouble b) { return new SecureDouble(a.Value / b.Value); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureDouble).Value); }

    }

    public class SecureFloat
    {

        private float _value;
        private float _randomValue;

        private float Value
        {
            get
            {
                unchecked
                {
                    return _value - _randomValue;
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = SecureRandom.random.Next(-1024, 1024);
                    _value = value + _randomValue;
                }
            }
        }

        public SecureFloat(float value = 0.0f) { Value = value; }

        public static implicit operator SecureFloat(float value) { return new SecureFloat(value); }

        public static implicit operator float(SecureFloat value) { return value.Value; }

        public static bool operator ==(SecureFloat a, SecureFloat b) { return a.Value == b.Value; }

        public static bool operator !=(SecureFloat a, SecureFloat b) { return a.Value != b.Value; }

        public static SecureFloat operator ++(SecureFloat a)
        {
            a.Value++;
            return a;
        }

        public static SecureFloat operator --(SecureFloat a)
        {
            a.Value--;
            return a;
        }

        public static SecureFloat operator +(SecureFloat a, SecureFloat b) { return new SecureFloat(a.Value + b.Value); }

        public static SecureFloat operator -(SecureFloat a, SecureFloat b) { return new SecureFloat(a.Value - b.Value); }

        public static SecureFloat operator *(SecureFloat a, SecureFloat b) { return new SecureFloat(a.Value * b.Value); }

        public static SecureFloat operator /(SecureFloat a, SecureFloat b) { return new SecureFloat(a.Value / b.Value); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureFloat).Value); }

    }

    public class SecureInt
    {

        private int _value;
        private int _randomValue;

        private int Value
        {
            get
            {
                unchecked
                {
                    return _value - _randomValue;
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = SecureRandom.random.Next();
                    _value = value + _randomValue;
                }
            }
        }


        public SecureInt(int value = 0) { Value = value; }

        public static implicit operator SecureInt(int value) { return new SecureInt(value); }

        public static implicit operator int(SecureInt value) { return value.Value; }

        public static bool operator ==(SecureInt a, SecureInt b) { return a.Value == b.Value; }

        public static bool operator !=(SecureInt a, SecureInt b) { return a.Value != b.Value; }

        public static SecureInt operator ++ (SecureInt a)
        {
            a.Value++;
            return a;
        }

        public static SecureInt operator -- (SecureInt a)
        {
            a.Value--;
            return a;
        }

        public static SecureInt operator +(SecureInt a, SecureInt b) { return new SecureInt(a.Value + b.Value); }

        public static SecureInt operator -(SecureInt a, SecureInt b) { return new SecureInt(a.Value - b.Value); }

        public static SecureInt operator *(SecureInt a, SecureInt b) { return new SecureInt(a.Value * b.Value); }

        public static SecureInt operator /(SecureInt a, SecureInt b) { return new SecureInt(a.Value / b.Value); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureInt).Value); }

    }

    public class SecureLong
    {
        private long _value;
        private long _randomValue;

        private long Value
        {
            get
            {
                unchecked
                {
                    return _value - _randomValue;
                }
            }

            set
            {
                unchecked
                {
                    _randomValue = SecureRandom.random.Next();
                    _value = value + _randomValue;
                }
            }
        }

        public SecureLong(long value = 0L) { Value = value; }

        public static implicit operator SecureLong(long value) { return new SecureLong(value); }

        public static implicit operator long(SecureLong value) { return value.Value; }

        public static bool operator ==(SecureLong a, SecureLong b) { return a.Value == b.Value; }

        public static bool operator !=(SecureLong a, SecureLong b) { return a.Value != b.Value; }

        public static SecureLong operator ++ (SecureLong a)
        {
            a.Value++;
            return a;
        }

        public static SecureLong operator -- (SecureLong a)
        {
            a.Value--;
            return a;
        }

        public static SecureLong operator +(SecureLong a, SecureLong b) { return new SecureLong(a.Value + b.Value); }

        public static SecureLong operator -(SecureLong a, SecureLong b) { return new SecureLong(a.Value - b.Value); }

        public static SecureLong operator *(SecureLong a, SecureLong b) { return new SecureLong(a.Value * b.Value); }

        public static SecureLong operator /(SecureLong a, SecureLong b) { return new SecureLong(a.Value / b.Value); }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }

        public override bool Equals(object obj) { return Value.Equals((obj as SecureLong).Value); }
    }


}

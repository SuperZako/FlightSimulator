

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class DispFormat
{
    public static String DoubleFormat(double value_ren, int low)
    {
        double x = Math.Round(value_ren * Math.Pow(10.0D, low), MidpointRounding.AwayFromZero) / Math.Pow(10.0D, low);
        String val = String.Concat(x);
        int pos = val.IndexOf('.');
        String ret;

        if (pos <= -1)
        {
            ret = "";
        }
        else
        {
            ret = val;
            String h = val.Substring(0, (pos) - (0));
            String l = Rpad(val.Substring(pos + 1), low, '0');

            if (low > 0)
                ret = h + "." + l;
            else
            {
                ret = h;
            }
        }

        return ret;
    }

    public static String DoubleFormat(double value_ren, int high, int low)
    {
        double x = Math.Round(value_ren * Math.Pow(10.0D, low), MidpointRounding.AwayFromZero) / Math.Pow(10.0D, low);
        String val = String.Concat(x);
        int pos = val.IndexOf('.');
        String ret;

        if (pos <= -1)
        {
            ret = "";
        }
        else
        {
            ret = val;
            String h = Lpad(val.Substring(0, (pos) - (0)), high, ' ');
            String l = Rpad(val.Substring(pos + 1), low, '0');

            if (low > 0)
                ret = h + "." + l;
            else
            {
                ret = h;
            }
        }
        return ret;
    }

    public static String DoubleFormatZ(double value_ren, int high, int low)
    {
        double x = Math.Round(value_ren * Math.Pow(10.0D, low), MidpointRounding.AwayFromZero) / Math.Pow(10.0D, low);
        String val = String.Concat(x);
        int pos = val.IndexOf('.');
        String ret;

        if (pos <= -1)
        {
            ret = "";
        }
        else
        {
            ret = val;
            String h = Lpad(val.Substring(0, (pos) - (0)), high, '0');
            String l = Rpad(val.Substring(pos + 1), low, '0');

            if (low > 0)
                ret = h + "." + l;
            else
            {
                ret = h;
            }
        }
        return ret;
    }

    public static String HtmlString(String str)
    {
        String ret;
        if (str == null)
            ret = "";
        else
        {
            ret = str;
        }

        ret = Replace(ret, 38, "&amp;");
        ret = Replace(ret, 62, "&gt;");
        ret = Replace(ret, 60, "&lt;");
        ret = Replace(ret, 34, "&quot;");
        ret = Replace(ret, 39, "&#39;");

        return ret;
    }

    public static String Lpad(String str, int length)
    {
        return Lpad(str, length, ' ');
    }

    public static String Lpad(String str, int length, char ch)
    {
        int l = str.Length;
        String ret = str;

        if (l < length)
        {
            for (int i = 0; i < length - l; i++)
            {
                ret = ch + ret;
            }
        }
        return ret;
    }

    public static String Replace(String replaceString, int c, String str2)
    {
        String ret = "";

        for (int i = 0; i < replaceString.Length; i++)
        {
            if (replaceString[i] == c)
                ret = ret + str2;
            else
            {
                ret = ret + replaceString[i];
            }
        }

        return ret;
    }

    public static String Rpad(String str, int length)
    {
        return Rpad(str, length, ' ');
    }

    public static String Rpad(String str, int length, char ch)
    {
        int l = str.Length;
        String ret = str;

        if (l < length)
        {
            for (int i = 0; i < length - l; i++)
            {
                ret = ret + ch;
            }
        }
        return ret;
    }

    public static String Rtrm(String str, int length)
    {
        String ret = str;

        if (ret.Length > length)
        {
            ret = ret.Substring(0, (length) - (0));
        }
        return ret;
    }

    public static String FirstUpper(String str)
    {
        return str.Substring(0, (1) - (0)).ToUpper()
                + str.Substring(1).ToLower();
    }

    public static String Ltrm(String str)
    {
        String ret = "";

        int len = str.Length;
        int flag = 0;
        for (int i = 0; i < len; i++)
        {
            char c = str[i];
            if ((c != ' ') && (c != '\t'))
                flag = 1;
            if (flag != 1)
                continue;
            ret = ret + c;
        }
        return ret;
    }

    public static String Rtrm(String str)
    {
        String ret = "";

        int len = str.Length;
        len = str.Length;
        int flag = 0;
        for (int i = len - 1; i >= 0; i--)
        {
            char c = str[i];
            if ((c != ' ') && (c != '\t'))
                flag = 1;
            if (flag != 1)
                continue;
            ret = c + ret;
        }
        return ret;
    }

    public static String Trm(String str)
    {
        return Rtrm(Ltrm(str));
    }
}
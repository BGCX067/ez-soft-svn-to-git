using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Data; 

namespace Sales
{
    public static class clsSupport
    {
        public static string ProductName = "Salse\\";
        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static Boolean KiemTraTenDangNhap(string Chuoi, string ThongBao)
        {
            try
            {
                if (Chuoi == "" || CkeckKeyCode(Chuoi)==false)
                {
                    System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập " + ThongBao + " là các ký tự thông thường(ký tự thuộc bảng chữ cái và số) và không có khoảng trắng!");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập " + ThongBao + " là các ký tự thông thường(ký tự thuộc bảng chữ cái và số) và không có khoảng trắng!");
                return true;
            }
        }
        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static bool CkeckKeyCode(string temp)
        {
            string susername = null;
            char[] cut = null;

            cut = temp.ToCharArray(0, temp.Length);
            string a = "";
            for (int i = 0; i <= cut.Length - 1; i++)
            {
                a = cut[i].ToString();
                if (a == "ẩ" || a == "ẫ" || a == "ầ" || a == "ấ" || a == "ậ" || a == "ẵ" || a == "ẳ" || a == "ặ" || a == "ằ" || a == "ắ" || a == "ă"
                    || a == "á" || a == "à" || a == "?" || a == "ã" || a == "ả" || a == "ạ" || a == "?" || a == "a" || a == "?" || a == "?" || a == "?"
                    || a == "?" || a == "?" || a == "â" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'a';
                }

                else if (a == "Ẩ" || a == "Ẫ" || a == "Ầ" || a == "Ấ" || a == "Ậ" || a == "Ẵ" || a == "Ẳ" || a == "Ặ" || a == "Ằ" || a == "Ắ" || a == "Ă"
                    || a == "Á" || a == "À" || a == "?" || a == "Ã" || a == "Ả" || a == "Ạ" || a == "?" || a == "A" || a == "?" || a == "?" || a == "?"
                    || a == "?" || a == "?" || a == "Â" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'A';
                }

                else if (a == "ó" || a == "ò" || a == "?" || a == "õ" || a == "ỏ" || a == "ọ" || a == "?" || a == "ố" || a == "ỗ"
                    || a == "ổ" || a == "ồ" || a == "ộ" || a == "ô" || a == "ơ" || a == "ớ" || a == "ở" || a == "ỡ" || a == "ờ" || a == "ợ"
                    || a == "?" || a == "?" || a == "?" || a == "?" || a == "?" || a == "o" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'o';
                }
                else if (a == "Ó" || a == "Ò" || a == "?" || a == "Õ" || a == "Ỏ" || a == "Ọ" || a == "?" || a == "Ố" || a == "Ỗ"
               || a == "Ỗ" || a == "Ồ" || a == "Ộ" || a == "Ô" || a == "Ơ" || a == "Ớ" || a == "Ở" || a == "Ỡ" || a == "Ờ" || a == "Ợ"
               || a == "?" || a == "?" || a == "?" || a == "?" || a == "?" || a == "O" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'O';
                }
                else if (a == "d" || a == "đ")
                {
                    cut[i] = 'd';
                }
                else if (a == "D" || a == "Đ")
                {
                    cut[i] = 'D';
                }
                else if (a == "é" || a == "è" || a == "ẹ" || a == "ẽ" || a == "ẻ" || a == "?" || a == "ệ" || a == "ế" || a == "ề" || a == "ể" || a == "ễ"
                    || a == "?" || a == "?" || a == "ê" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'e';
                }
                else if (a == "É" || a == "È" || a == "Ẹ" || a == "Ẽ" || a == "Ẻ" || a == "?" || a == "Ệ" || a == "Ế" || a == "Ề" || a == "Ể" || a == "Ễ"
               || a == "?" || a == "?" || a == "ê" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'E';
                }
                else if (a == "í" || a == "ì" || a == "ị" || a == "ĩ" || a == "ỉ" || a == "?" || a == "i" || a == "?")
                {
                    cut[i] = 'i';
                }
                else if (a == "Í" || a == "Ì" || a == "Ị" || a == "Ĩ" || a == "Ỉ" || a == "?" || a == "I" || a == "?")
                {
                    cut[i] = 'I';
                }
                else if (a == "ý" || a == "ỳ" || a == "ỹ" || a == "ỷ" || a == "ỵ" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'y';
                }
                else if (a == "Ý" || a == "Ỳ" || a == "Ỹ" || a == "Ỷ" || a == "Ỵ" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'Y';
                }
                else if (a == "ú" || a == "ù" || a == "ủ" || a == "ũ" || a == "ụ" || a == "ư" || a == "ứ" || a == "ừ" || a == "ự" || a == "ử"
                    || a == "ữ" || a == "?" || a == "u" || a == "?" || a == "u" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'u';
                }
                else if (a == "Ú" || a == "Ù" || a == "Ủ" || a == "Ũ" || a == "Ụ" || a == "Ư" || a == "Ứ" || a == "Ừ" || a == "Ự" || a == "Ử"
               || a == "Ữ" || a == "?" || a == "u" || a == "?" || a == "u" || a == "?" || a == "?" || a == "?" || a == "?" || a == "?")
                {
                    cut[i] = 'U';
                }
                else
                {
                    if (!a.Equals("/") && !a.Equals(".") && !a.Equals("`") && !a.Equals(",") && !a.Equals("<") && !a.Equals(">") && !cut[i].Equals("\\") &&
                        !a.Equals("{") && !a.Equals("}") && !a.Equals("[") && !a.Equals("]") && !a.Equals("~") && !a.Equals("!") && !a.Equals("@") &&
                        !a.Equals("$") && !a.Equals("%") && !a.Equals("^") && !a.Equals("&") && !a.Equals("*") && !a.Equals("(") && !a.Equals(")") &&
                        !a.Equals('"') && !a.Equals(" ") && !a.Equals("+") && !a.Equals("–") && !a.Equals(":") && !a.Equals("|") && !a.Equals('"') && !a.Equals("'") && !a.Equals(";"))
                    {
                        cut[i] = cut[i];
                    }
                    else
                    {
                        cut[i] = '_';

                    }
                }
            }
            susername = "";
            for (int j = 0; j < cut.Length; j++)
            {

                susername = susername + cut[j].ToString();
            }
            if (susername == temp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static Boolean KiemTraSoThuc(string Chuoi, string ThongBao)
        {
            try
            {
                Double So = Double.Parse(Chuoi);
                if (!(So > 0 && So <= double.MaxValue))
                {
                    System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập số " + ThongBao + " là số thực dương!");
                   return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập số " + ThongBao + " là số thực dương!");
                return true;
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static Boolean KiemTraPhanTram(string Chuoi, string ThongBao)
        {
            try
            {
                Chuoi = Chuoi.Replace("%", "");
                Double So = Double.Parse(Chuoi);
                if (!(So >= 0 && So <= double.MaxValue))
                {
                    System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập số " + ThongBao + " là số thực dương!ví dụ nhập hợp lệ: 4%, 2");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập số " + ThongBao + " là số thực dương!ví dụ nhập hợp lệ: 4%, 2");
                return true;
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static Boolean KiemTraSoNguyen(string Chuoi, string ThongBao)
        {
            try
            {
                int So = int.Parse(Chuoi);
                if (!(So > 0 && So <= int.MaxValue))
                {
                    System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập " + ThongBao + " là số nguyên dương!");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập " + ThongBao + " là số nguyên dương!");
                return true;
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static Boolean KiemChuoiNot255(string Chuoi , string ThongBao)
        {
            try
            {
                string ChuoiMoi = Chuoi.Replace(" ", "");
                if (!(Chuoi != null && ChuoiMoi.Length >= 0 && Chuoi.Length <= 255))
                {
                    System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập " + ThongBao + ", không được bỏ trống hoặc nhập hơn 255 ký tự!");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Xin vui lòng nhập " + ThongBao +", không được bỏ trống hoặc nhập hơn 255 ký tự!");
                return true;
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static string CurrencyNumber(string ins_sNumber)
        {
            string temp = ins_sNumber;
            string[] temp2 = null;
            if (temp != null)
            {
                temp=temp.Replace(",", "");
                temp2 = temp.Split('.');
                temp = temp2[0];
            }
            if (temp.Length > 3)
            {
                temp = temp.Insert(temp.Length - 3, ",");
                if (temp.Length > 7)
                {
                    temp = temp.Insert(temp.Length - 7, ",");
                    if (temp.Length > 11)
                    {
                        temp = temp.Insert(temp.Length - 11, ",");
                        if (temp.Length > 15)
                        {
                            temp = temp.Insert(temp.Length - 15, ",");
                            if (temp.Length > 19)
                            {
                                temp = temp.Insert(temp.Length - 19, ",");
                                if (temp.Length > 23)
                                {
                                    temp = temp.Insert(temp.Length - 23, ",");
                                }
                            }
                        }
                    }
                }
            }
            if (temp != null && temp2.Length == 2)
            {
                temp+="." + temp2[1];
            }
            return temp;
        }
        
        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static string ConvertMoneyToText(string csMoney)
        {
            string csResult = "";
            if (csMoney == "") return "";
            while (csMoney.Substring(0, 1) == "0")
            {
                if (csMoney.Length > 1)
                    csMoney = csMoney.Substring(1, csMoney.Length - 1);
                else
                    if (csMoney == "0")
                    {
                        csMoney = "";
                        break;
                    }
            }

            if (csMoney != "")
            {
                string csTram = "";
                string csNghin = "";
                string csTrieu = "";
                string csPart1 = "";
                string csPart2 = "";
                string delimStr = ",.";
                char[] delimiter = delimStr.ToCharArray();
                string[] split = null;
                split = csMoney.Split(delimiter, 2);
                csPart1 = split[0];
                if (split.Length > 1)
                {
                    csPart2 = split[1];
                    if (csPart2.Length > 3)
                        csPart2 = csPart2.Substring(0, 3);

                    csPart2 = SplipNumber(csPart2);

                    if (csPart2 != "")
                        csResult = "phay " + csPart2;
                }

                if (csPart1.Length > 9)
                {
                    //  MessageBox.Show("Tien thanh toan len den tien ty la dieu khong the\r\nHay xem du lieu va tinh toan lai di Oai vo van");
                    return "";
                }

                while (csPart1.Length > 3)
                {
                    if (csTram == "")
                        csTram = csPart1.Substring(csPart1.Length - 3, 3);
                    else
                        csNghin = csPart1.Substring(csPart1.Length - 3, 3);

                    csPart1 = csPart1.Substring(0, csPart1.Length - 3);

                }

                if (csTram == "")
                    csTram = csPart1;
                else
                    if (csNghin == "")
                        csNghin = csPart1;
                    else
                        csTrieu = csPart1;

                if (csTram != "")
                    csTram = SplipNumber(csTram);
                if (csNghin != "")
                    csNghin = SplipNumber(csNghin);
                if (csTrieu != "")
                    csTrieu = SplipNumber(csTrieu);

                if (csTram != "")
                    csResult = csTram + csResult;
                if (csNghin != "")
                    csResult = csNghin + "nghìn " + csResult;
                if (csTrieu != "")
                    csResult = csTrieu + "triệu " + csResult;

                csResult += "đồng";
            }
            return csResult;
        }

        //──────────────────────────────────────────────────────────────────────────────────────────     
        public static string SplipNumber(string csNumber)
        {
            string csText = "";
            string csResult = "";

            if (csNumber == "000" || csNumber == "00" || csNumber == "0")
                return csResult;

            char[] NumberArr = null;
            int nCount = 0;
            NumberArr = csNumber.ToCharArray();
            for (int i = NumberArr.Length - 1; i >= 0; i--)
            {
                switch (NumberArr[i])
                {
                    case '0':
                        if (i == NumberArr.Length - 1)
                            csText = "";
                        else
                            if (i == NumberArr.Length - 2)
                                if (NumberArr[NumberArr.Length - 1] != '0')
                                    csText = "lẻ ";
                                else
                                    csText = "";
                            else
                                csText = "không ";
                        break;
                    case '1':
                        if (i == NumberArr.Length - 2)
                            csText = "mười ";
                        else
                            csText = "một ";
                        break;
                    case '2':
                        csText = "hai ";
                        break;
                    case '3':
                        csText = "ba ";
                        break;
                    case '4':
                        csText = "bốn ";
                        break;
                    case '5':
                        if ((i == NumberArr.Length - 1) && (NumberArr.Length != 1))
                            csText = "lăm ";
                        else
                            csText = "năm ";
                        break;
                    case '6':
                        csText = "sáu ";
                        break;
                    case '7':
                        csText = "bảy ";
                        break;
                    case '8':
                        csText = "tám ";
                        break;
                    case '9':
                        csText = "chín ";
                        break;
                }
                if ((nCount == 1) && (NumberArr[i] != '0') && (NumberArr[i] != '1'))
                    csText += "mươi ";
                else
                    if (nCount == 2)
                        csText += "trăm ";
                csResult = csText + csResult;
                nCount++;
            }
            return csResult;
        }

        //────────────────────────────────────────────────────────────────────────────────────────────
        public static string ReadRegistry(string ins_keyName)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey skl = rk.OpenSubKey("SOFTWARE\\" + ProductName);

            if (skl == null)
                return null;
            else
            {
                try
                {
                   string value= (string)skl.GetValue(ins_keyName.ToUpper());
                   char[] data = value.ToCharArray();
                    Base64Decoder myDecoder = new Base64Decoder(data);
                    StringBuilder sb = new StringBuilder();

                    byte[] temp = myDecoder.GetDecoded();
                    sb.Append(System.Text.UTF8Encoding.UTF8.GetChars(temp));

                   return sb.ToString();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    return null;
                }
            }

        }// End of ReadRegistry pro
        
        //────────────────────────────────────────────────────────────────────────────────────────────
        public static bool WriteRegistry(string ins_keyName, string ins_keyValue)
        {
            try
            {
                byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(ins_keyValue);
                Base64Encoder myEncoder = new Base64Encoder(data);
                StringBuilder sb = new StringBuilder();
                sb.Append(myEncoder.GetEncoded());

                RegistryKey rk = Registry.LocalMachine;
                RegistryKey skl = rk.CreateSubKey("SOFTWARE\\" + ProductName);
                skl.SetValue(ins_keyName.ToUpper(), sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return false;
            }
        }// End of WriteRegistry pro
        
        //────────────────────────────────────────────────────────────────────────────────────────────
        public static bool DeleteRegistry(string ins_keyName)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey skl = rk.CreateSubKey("SOFTWARE\\" + ProductName);
                if (skl == null)
                    return true;
                else
                    skl.DeleteValue(ins_keyName);
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return false;
            }
        }// End function
        
    }
}

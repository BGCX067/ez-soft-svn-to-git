using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Sales
{
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public partial class CurrencyTextBox : TextBox
    {
        private Double _DecimalValue = 0;
        int _SelectionStart = 0;

        public Double DecimalValue
        {
            get { return _DecimalValue; }
        }


        public CurrencyTextBox()
        {
            InitializeComponent();
        }

        public CurrencyTextBox(IContainer container)
        {
            container.Add(this);
            this.TextAlign = HorizontalAlignment.Right;
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            InitializeComponent();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            
            string strTam = this.Text.Replace(",", "");
            if (strTam != "")
            {
                decimal tam = decimal.Parse(strTam);
                this.Text = tam.ToString("#,##0.############");
                if (_SelectionStart <= 0)
                {
                    if (_SelectionStart == 0)
                    {
                        this.SelectionStart = _SelectionStart;
                    }
                    else
                    {
                        this.SelectionStart = 0;
                    }
                }
                else
                {
                    this.SelectionStart = _SelectionStart;
                }
            }
            ////this.Text = CurrencyNumber(this.Text);
            ////if (_SelectionStart <= 0)
            ////{
            ////    if (_SelectionStart == 0)
            ////    {
            ////        this.SelectionStart = _SelectionStart;
            ////    }
            ////    else
            ////    {
            ////        this.SelectionStart = 0;
            ////    }
            ////}
            ////else
            ////{
            ////    this.SelectionStart = _SelectionStart;
            ////}
            Double value;
            if (Double.TryParse(this.Text.Replace(",", ""), out value) == true)
            {
                _DecimalValue = value;
            }
            base.OnTextChanged(e);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            e.Cancel = KiemTraSoThuc(this.Text, "Số tiền");
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            // mã decimal trong bảng mã ASCII của số là từ 48-57, của Delete: 127, Backspace: 8, dấu ".":46
            if ((Char.IsNumber(e.KeyChar) || e.KeyChar == Convert.ToChar(127) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                _SelectionStart = this.SelectionStart;
                if (e.KeyChar == Convert.ToChar(8))
                {
                    if (this.Text.Length > 0)
                    {
                        if (_SelectionStart > 0 && (this.Text.Length == 5 ||
                            this.Text.Length == 9 || this.Text.Length == 13
                            || this.Text.Length == 17 || this.Text.Length == 21
                            || this.Text.Length == 25))
                        {
                            if ((this.Text.Length - this.SelectionStart) > 0 && (this.Text.Length - this.SelectionStart == 4 ||
                               this.Text.Length - this.SelectionStart == 8 || this.Text.Length - this.SelectionStart == 12
                               || this.Text.Length - this.SelectionStart == 14 || this.Text.Length - this.SelectionStart == 20
                               || this.Text.Length - this.SelectionStart == 24))
                            {
                                _SelectionStart -= 1;
                            }
                            else
                            {
                                _SelectionStart -= 2;
                            }
                        }
                        else
                        {
                            if (!((this.Text.Length - this.SelectionStart) > 0 && (this.Text.Length - this.SelectionStart == 4 ||
                               this.Text.Length - this.SelectionStart == 8 || this.Text.Length - this.SelectionStart == 12
                               || this.Text.Length - this.SelectionStart == 14 || this.Text.Length - this.SelectionStart == 20
                               || this.Text.Length - this.SelectionStart == 24)))
                            {
                                _SelectionStart -= 1;
                            }
                        }
                    }
                    else
                    {
                        _SelectionStart = 0;
                    }
                }
                else
                {
                    if (e.KeyChar == Convert.ToChar(127))
                    {
                        if (this.Text.Length == 0)
                        {
                            _SelectionStart = 0;
                        }
                    }
                    else
                    {
                        if (this.SelectionStart == this.Text.Length)
                        {
                            if (CountDot(this.Text) != 0 || this.Text.Length == 3 ||
                                this.Text.Length == 7 || this.Text.Length == 11
                                || this.Text.Length == 15 || this.Text.Length == 19
                                || this.Text.Length == 23)
                            {
                                _SelectionStart += 2;
                            }
                            else
                            {
                                _SelectionStart += 1;
                            }
                        }
                        else
                        {
                            if (this.Text.Length == 3 ||
                                this.Text.Length == 7 || this.Text.Length == 11
                                || this.Text.Length == 15 || this.Text.Length == 19
                                || this.Text.Length == 23)
                            {
                                _SelectionStart += 2;
                            }
                            else
                            {
                                _SelectionStart += 1;
                            }
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (this.Text.Length > 0)
                {
                    if (this.SelectionStart > 0 && (this.Text.Length - this.SelectionStart) > 0 && (this.Text.Length - this.SelectionStart == 3 ||
                            this.Text.Length - this.SelectionStart == 7 || this.Text.Length - this.SelectionStart == 11
                            || this.Text.Length - this.SelectionStart == 15 || this.Text.Length - this.SelectionStart == 19
                            || this.Text.Length - this.SelectionStart == 23))
                    {
                        this.SelectionStart -= 1;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Right)
                {
                    if (this.Text.Length > 0)
                    {
                        if (this.SelectionStart < this.Text.Length && (this.Text.Length - this.SelectionStart == 5 ||
                                this.Text.Length - this.SelectionStart == 9 || this.Text.Length - this.SelectionStart == 13
                                || this.Text.Length - this.SelectionStart == 17 || this.Text.Length - this.SelectionStart == 21
                                || this.Text.Length - this.SelectionStart == 25))
                        {
                            this.SelectionStart += 1;
                        }
                    }
                }
            }

            if (e.KeyCode == Keys.Back)
            {
                if (this.Text.Length > 0)
                {
                    if (this.SelectionStart > 0 && (this.Text.Length - this.SelectionStart) > 0 && (this.Text.Length - this.SelectionStart == 3 ||
                        this.Text.Length - this.SelectionStart == 7 || this.Text.Length - this.SelectionStart == 11
                        || this.Text.Length - this.SelectionStart == 15 || this.Text.Length - this.SelectionStart == 19
                        || this.Text.Length - this.SelectionStart == 23))
                    {
                        this.SelectionStart -= 1;
                    }
                }
                else
                {
                    this.SelectionStart = 0;
                }
                //_SelectionStart = this.SelectionStart;
            }

            if (e.KeyCode == Keys.Delete)
            {
                if (this.SelectionStart > 0)
                {
                    if (this.SelectionStart < this.Text.Length && (this.Text.Length - this.SelectionStart == 5 ||
                            this.Text.Length - this.SelectionStart == 9 || this.Text.Length - this.SelectionStart == 13
                            || this.Text.Length - this.SelectionStart == 17 || this.Text.Length - this.SelectionStart == 21
                            || this.Text.Length - this.SelectionStart == 25))
                    {
                        if (this.SelectionStart < this.Text.Length && (this.Text.Length == 5 ||
                            this.Text.Length == 9 || this.Text.Length == 13
                            || this.Text.Length == 17 || this.Text.Length == 21
                            || this.Text.Length == 25))
                        {
                            _SelectionStart = this.SelectionStart;
                        }
                        else
                        {
                            _SelectionStart = this.SelectionStart + 1;
                        }

                    }
                    else
                    {
                        if (this.SelectionStart < this.Text.Length && (this.Text.Length == 5 ||
                            this.Text.Length == 9 || this.Text.Length == 13
                            || this.Text.Length == 17 || this.Text.Length == 21
                            || this.Text.Length == 25))
                        {
                            _SelectionStart = this.SelectionStart - 1;
                        }
                        else
                        {
                            _SelectionStart = this.SelectionStart;
                        }
                    }
                }
                else
                {
                    _SelectionStart = 0;
                }
            }
            base.OnKeyDown(e);

        }

        private int CountDot(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ',')
                    count++;
            }
            return count;
        }


        public Boolean KiemTraSoThuc(string Chuoi, string ThongBao)
        {
            try
            {
                Chuoi.Replace(",", "");
                Double So = Double.Parse(Chuoi);
                if (!(So >= 0 && So <= double.MaxValue))
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

        public string CurrencyNumber(string ins_sNumber)
        {
            string temp = ins_sNumber;
            string[] temp2 = null;
            if (temp != null)
            {
                temp = temp.Replace(",", "");
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
                temp += "." + temp2[1];
            }
            return temp;
        }
    }
}

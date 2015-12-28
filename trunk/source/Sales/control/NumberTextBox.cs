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
    public partial class NumberTextBox : TextBox
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }

        public NumberTextBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.TextAlign = HorizontalAlignment.Right;
        }

         protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            // mã decimal trong bảng mã ASCII của số là từ 48-57, của Delete: 127, Backspace: 8, Enter: 13
            if ((Char.IsNumber(e.KeyChar) || e.KeyChar == Convert.ToChar(127) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(13)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            e.Cancel = KiemTraSoNguyen(this.Text, "Số lượng");
        }

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
    }
}

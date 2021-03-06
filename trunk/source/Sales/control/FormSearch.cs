using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class FormSearch : Form
    {
        MultiColumnComboBox _ComboBox;
        int _Column = 0;
        int _Row = 0;

        public FormSearch(MultiColumnComboBox comboBox)
        {
            InitializeComponent();
            _ComboBox = comboBox;
            Init();
            txtSearch.Focus();
        }

        private void Init()
        {
            //Set the datasource of the grid = datasource of the combobox
            dataGridView1.DataSource = _ComboBox.DataSource;
            txtStatus.Text = "";

            //Set all the columns invisible
            for (int idx = 0; idx < dataGridView1.Columns.Count; idx++)
            {
                dataGridView1.Columns[idx].Visible = false;
            }

            //Iterate through the ColumnNameCollection. If a column isn't listed
            //don't set it to visible. If a column's width is zero, don't set it to
            //visible. Set it's DisplayIndex equal to it's index in the collection.
            for (int idx = 0; idx < _ComboBox.ColumnNameCollection.Count; idx++)
            {
                dataGridView1.Columns[_ComboBox.ColumnNameCollection[idx]].DisplayIndex = idx;
                dataGridView1.Columns[_ComboBox.ColumnNameCollection[idx]].Visible =
                                      _ComboBox.ColumnWidthCollection[idx] > 0 ? true : false;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //Set the SelectedIndex of the combobox to the index 
            //if the double-clicked grid row
            _ComboBox.SelectedIndex = dataGridView1.CurrentRow.Index;
            this.Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //If the user hits the <Enter> key, peform a search
            if (e.KeyCode == Keys.Enter)
            {
                SearchGrid();
            }
        }

        private void SearchGrid()
        {
            //maxSearches = the # of cells in the grid
            int maxSearches = dataGridView1.Rows.Count * dataGridView1.Columns.Count + 1;
            int idx = 1;
            bool isFound = false;
            string searchValue = txtSearch.Text.ToUpper();

            if (Convert.ToBoolean(txtSearch.Text.Length))
            {
                // If the item is not found and you haven't looked at every cell, keep searching
                while ((!isFound) & (idx < maxSearches))
                {
                    // Only search visible cells
                    if (dataGridView1.Columns[_Column].Visible)
                    {
                        // Do all comparing in UpperCase so it is case insensitive
                        if (dataGridView1[_Column, _Row].Value.ToString().ToUpper().Contains(searchValue))
                        {
                            // If found position on the item
                            dataGridView1.FirstDisplayedScrollingRowIndex = _Row;
                            dataGridView1[_Column, _Row].Selected = true;
                            isFound = true;
                            txtStatus.Text = "Tìm thấy!";
                        }
                    }

                    // Increment the column.
                    _Column++;

                    // If it exceeds the column count
                    if (_Column == dataGridView1.Columns.Count)
                    {
                        _Column = 0; //Go to 0 column
                        _Row++;      //Go to the next row

                        // If it exceeds the row count
                        if (_Row == dataGridView1.Rows.Count)
                        {
                            _Row = 0; //Start over at the top
                        }
                    }

                    idx++;
                }

                // If isFound = false then the phrase has not been found in the grid
                if (! isFound)
                {
                    txtStatus.Text = "Không tìm thấy!";
                }
            }
        }
    }
}
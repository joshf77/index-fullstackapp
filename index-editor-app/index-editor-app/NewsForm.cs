﻿using index_editor_app_engine.JsonClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app
{
    public partial class Form1
    {
        int editingNewsIndex = -1;
        bool NewsSystemEditing = true;

        public void InitializeNews()
        {
            InitializeNewsDataGrid();
            InitializeNewsPage();
        }

        public void InitializeNewsPage()
        {
            NewsPageSubtitleTextBox.Text = newsHandler.newsPage.PageSubtitle;
            NewsPageTitleTextBox.Text = newsHandler.newsPage.PageTitle;
        }

        public void InitializeNewsDataGrid()
        {
            this.NewsDataGridView3.CancelEdit();
            this.NewsDataGridView3.Columns.Clear();
            this.NewsDataGridView3.Rows.Clear();
            this.NewsDataGridView3.DataSource = null;
            this.NewsDataGridView3.Columns.Add("name", "Name");
            this.NewsDataGridView3.Rows.Add(newsHandler.NewsCount());

            foreach (DataGridViewRow row in this.NewsDataGridView3.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            for (int i = 0; i < newsHandler.NewsCount(); i++)
            {
                NewsDataGridView3[0, i].Value = newsHandler.newsPage.NewsItems[i].Title;
            }

            //add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            NewsDataGridView3.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
        }

        private void EditNewsButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in NewsDataGridView3.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            //column 4 = edit button
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                editingNewsIndex = e.RowIndex;
                //highlight the column being edited
                NewsDataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                //load memebr into fields
                LoadNewsDataAsync(e.RowIndex);
            }
        }

        public async Task LoadNewsDataAsync(int index)
        {
            NewsSystemEditing = true;
            ClearNewsFields();
            NewsItem n = newsHandler.newsPage.NewsItems[index];

            NewsDescriptionTextBox.Text = n.Description;
            NewsLinkTextBox.Text = n.NewsLink;
            NewsTitleTextBox.Text = n.Title;
            NewsPostedByTextBox.Text = n.PostedBy;

            NewsPictureBox.Image = null;
            if (n.Image != "")
            {
                NewsPictureBox.Image = System.Drawing.Image.FromStream(await newsHandler.LoadNewsImageHandlerAsync(n.Title));
            }

            NewsDateTimePicker.Value = DateTime.Now;

            string date = n.EditorDateTime;
            DateTime parsedDate = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            NewsDateTimePicker.Value = parsedDate;

            NewsSystemEditing = false;
        }

        public void ClearNewsFields()
        {
            NewsDescriptionTextBox.Text = "";
            NewsLinkTextBox.Text = "";
            NewsTitleTextBox.Text = "";
            NewsPostedByTextBox.Text = "";
            NewsDateTimePicker.Value = DateTime.Now;
            NewsPictureBox.Image = null;
        }

        private void NewsDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (NoNewsSelectedCheck() || NewsSystemEditing) { return; }
            NewsItem n = newsHandler.newsPage.NewsItems[editingNewsIndex];
            n.Date = NewsDateTimePicker.Text.Substring(0, NewsDateTimePicker.Text.Length - 6);
            n.EditorDateTime = NewsDateTimePicker.Value.ToString();
        }

        public bool NoNewsSelectedCheck()
        {
            if (editingNewsIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("No NewsLetter selected! Edit a NewsLetter or click create new.");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void NewsPageTitleTextBox_TextChanged(object sender, EventArgs e)
        {
            newsHandler.newsPage.PageTitle = NewsPageTitleTextBox.Text;
        }

        private void NewsPageSubtitleTextBox_TextChanged(object sender, EventArgs e)
        {
            newsHandler.newsPage.PageSubtitle = NewsPageSubtitleTextBox.Text;
        }


        private void NewsTitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoNewsSelectedCheck() || NewsSystemEditing) { return; }
            newsHandler.newsPage.NewsItems[editingNewsIndex].Title = NewsTitleTextBox.Text;
        }

        private void NewsDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoNewsSelectedCheck() || NewsSystemEditing) { return; }
            newsHandler.newsPage.NewsItems[editingNewsIndex].Description = NewsDescriptionTextBox.Text;
        }

        private void NewsLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoNewsSelectedCheck() || NewsSystemEditing) { return; }
            newsHandler.newsPage.NewsItems[editingNewsIndex].NewsLink = NewsLinkTextBox.Text;
        }

        private void NewsPostedByTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NoNewsSelectedCheck() || NewsSystemEditing) { return; }
            newsHandler.newsPage.NewsItems[editingNewsIndex].PostedBy = NewsPostedByTextBox.Text;
        }

        private void UpdateNewsletterButton_Click(object sender, EventArgs e)
        {
            var httpreseponse = newsHandler.Upload();
            System.Windows.Forms.MessageBox.Show(httpreseponse.ToString());
        }


        private void CreateNewsLetterButton_Click(object sender, EventArgs e)
        {
            newsHandler.CreateNewNews();
            InitializeNewsDataGrid();
        }

        private void DeleteNewsButton_Click(object sender, EventArgs e)
        {
            newsHandler.DeleteNews(editingNewsIndex);
            InitializeNewsDataGrid();
            NewsSystemEditing = true;
            ClearNewsFields();
            NewsSystemEditing = false;
            editingNewsIndex = -1;
        }



        private void AddNewsImageButton_Click(object sender, EventArgs e)
        {
            if (NoNewsSelectedCheck()) { return; }
            if (string.IsNullOrEmpty(newsHandler.newsPage.NewsItems[editingNewsIndex].Title))
            {
                System.Windows.Forms.MessageBox.Show("Warning, A Title is required before adding an image");
                return;
            }

            this.openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",                                         //change to images
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                newsHandler.AddNewsImage(openFileDialog1.FileName, editingNewsIndex);
                LoadNewsDataAsync(editingNewsIndex);
            }
        }

    }

}

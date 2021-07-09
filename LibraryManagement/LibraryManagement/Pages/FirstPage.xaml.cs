﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public event PageChanger changeToLoginPage;

        public FirstPage(PageChanger changeToLoginPage)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }
    }
}

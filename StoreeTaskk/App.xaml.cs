﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StoreeTaskk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Grid MainWindowGrid { get; set; } = new Grid();
        public static WrapPanel wrapPanel { get; set; } = new WrapPanel();

    }
}

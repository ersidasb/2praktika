using System;
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

namespace _2praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repository = new Repository();
        User loggedInUser = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loggedInUser=repository.GetUser(textBoxUsername.Text, textBoxPassword.Text);
                if(loggedInUser.GetGroupId()==1)
                {
                    try
                    {
                        WindowTeacher window = new WindowTeacher(loggedInUser);
                        Close();
                        window.Show();
                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
                else if(loggedInUser.GetGroupId()==2)
                {
                    WindowAdmin window = new WindowAdmin();
                    window.SetLoggedInUser(loggedInUser);
                    Close();
                    window.Show();
                }
                else
                {
                    try
                    {
                        WindowStudent window = new WindowStudent(loggedInUser);
                        Close();
                        window.Show();
                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}

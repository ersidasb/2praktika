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
    /// Interaction logic for ControlAdminViewSubjects.xaml
    /// </summary>
    public partial class ControlAdminViewSubjects : UserControl
    {
        public static Delegate update;
        public static Delegate window;
        Repository repository = new Repository();
        public ControlAdminViewSubjects()
        {
            InitializeComponent();
        }

        private void buttonViewSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                window.DynamicInvoke(labelSubjectName.Content.ToString());
            }
            catch
            {
                MessageBox.Show("view subject error");
            }
        }

        private void buttonRemoveSubject_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ar tikrai norite pašalinti šį dalyką?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    repository.RemoveSubject(repository.GetSubjectId(labelSubjectName.Content.ToString()));
                    update.DynamicInvoke();
                }
                catch
                {
                    MessageBox.Show("remove subject error");
                }
            }
        }
    }
}

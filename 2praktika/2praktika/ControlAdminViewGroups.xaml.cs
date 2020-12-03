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
    /// Interaction logic for ControlAdminViewGroups.xaml
    /// </summary>
    public partial class ControlAdminViewGroups : UserControl
    {
        public static Delegate update;
        Repository repository = new Repository();
        public ControlAdminViewGroups()
        {
            InitializeComponent();
        }

        private void buttonRemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ar tikrai norite pašalinti šią grupę?\nTai taip pat pašalins visus joje esančius studentus.", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    repository.RemoveGroup(repository.GetGroupId(labelGroupName.Content.ToString()));
                    update.DynamicInvoke();
                }
                catch
                {
                    MessageBox.Show("error");
                }
            }
        }
    }
}

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
using System.Windows.Shapes;

namespace _2praktika
{
    /// <summary>
    /// Interaction logic for WindowStudent.xaml
    /// </summary>
    public partial class WindowStudent : Window
    {

        private User loggedInUser;
        Repository repository = new Repository();
        public WindowStudent(User user)
        {
            InitializeComponent();
            loggedInUser = user;
            try
            {
                UpdateGradesList();
            }
            catch(Exception)
            {
                Close();
                throw new Exception("error logging in");
            }
        }

        private void UpdateGradesList()
        {
            try
            {
                stackPanelGrades.Children.Clear();
                List<string> subjectsList = new List<string>();
                subjectsList = repository.GetSubjectsList(loggedInUser.GetGroupId(),0);
                foreach (string subject in subjectsList)
                {
                    ControlGrades control = new ControlGrades();
                    control.labelSubject.Content = subject;
                    int teacherId = repository.GetUserId(subject,0);
                    if (teacherId == 0)
                        control.labelTeacher.Content = "Nėra dėstytojo";
                    else
                    {
                        User teacher = repository.GetUser(teacherId);
                        control.labelTeacher.Content = $"{teacher.GetName()}  {teacher.GetSurname()}";
                    }
                    int grade = repository.GetGrade(repository.GetUserId(loggedInUser.GetUsername()), repository.GetSubjectId(subject));
                    if (grade == -1)
                        control.labelGrade.Content = "Neįvesta";
                    else
                        control.labelGrade.Content = grade.ToString();
                    stackPanelGrades.Children.Add(control);
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowLogin = new MainWindow();
            Close();
            windowLogin.Show();
        }
    }
}

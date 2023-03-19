using Kursovaya.View;

namespace Kursovaya.DialogView
{
    public class DialogShow : IDialogShow
    {
        public void CreateNewWindow()
        {
            DialogWindow dialogView = new DialogWindow();
            dialogView.ShowDialog();
        }
    }
}

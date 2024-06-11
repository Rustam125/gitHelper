using System.Windows.Forms;

namespace GitHelper.Helpers
{
    internal static class ShowMessageHelper
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(
                    message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        public static void ShowInfo(string message)
        {
            MessageBox.Show(
                    message,
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
    }
}
